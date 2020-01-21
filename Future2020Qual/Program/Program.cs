//#define NO_LOCAL
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Program
{
    /* 提出前に REPEATAS = 1 を確認！ */
    public class Program
    {
        private const int N = 40, M = 100, B = 300;
        private const int LIMIT_OFFSET = 28, REPEATS = 5;
        public const string IN_TXT = "in1.txt";

        private int GoalX, GoalY;

        private bool[,] Blocks;
        private int[][] BlockList;

        private bool[,] NoBlocks;
        private int[][] NoBlockList;

        private Robot[] Robots;

        private char?[,] DirectionGuides;

        private void Solve()
        {
            // ランダムに構築して最適解を取る

            int maxPoint, bestPoint = 0;
            int[][] cg;
            var maxPattern = Randoms(out maxPoint, out cg);
            char?[,] bestPattern = maxPattern;

            var lp = 0;

            HashSet<int> goal;
            int pass, guides;
            List<int[]>[] ways;
            HashSet<int>[,] passes;
            SecondEval(maxPattern, out goal, out pass, out guides, out ways, out passes);

            var T = Limit - stopwatch.ElapsedMilliseconds;
            var start = stopwatch.ElapsedMilliseconds;
            long time = 0;
            var ls = new int[100];
            while ((time = stopwatch.ElapsedMilliseconds) < Limit)
            {
                lp++;
                // 適当に足していく / 消していく / 向きを変えてみる
                var place = NoBlockList[rand.Next(NoBlockList.Length)];
                int y = place[0], x = place[1];
//                Debug(cg.Length);
                if (rand.Next(100) < cg.Length)
                {
                    var rbt = rand.Next(cg.Length);
                    y = cg[rbt][0];
                    x = cg[rbt][1];
                }
                char c = toChar(rand.Next(4));
                if (Blocks[y, x] || IsGoal(y, x)) continue;
                if (c == 'R' && Blocks[y, (x + 1) % N] ||
                    c == 'L' && Blocks[y, (x - 1 + N) % N] ||
                    c == 'U' && Blocks[(y - 1 + N) % N, x] ||
                    c == 'D' && Blocks[(y + 1) % N, x] &&
                    maxPattern[y, x] != c) continue;
                char? now = maxPattern[y, x];

                int nowGuides = guides;
                if (maxPattern[y, x] == c)
                {
                    maxPattern[y, x] = null;
                    guides--;
                }
                else
                {
                    if (maxPattern[y, x] == null) guides++;
                    maxPattern[y, x] = c;
                }

                var point = UpdateEval(maxPattern, y, x, goal, ref pass, guides, ways, passes, out cg);
                var diff = maxPoint - point;
                var tt = (time - start);
                var t = tt * tt / 1500;
                var p = diff * t;

                if (maxPoint < point || p <= rand.Next(1850))
                {
                    if (bestPoint < point)
                    {
                        bestPoint = point;
                        bestPattern = maxPattern;
                    }
                    maxPoint = point;
                }
                else
                {
                    // Rollback
                    guides = nowGuides;
                    maxPattern[y, x] = now;
                    UpdateEval(maxPattern, y, x, goal, ref pass, guides, ways, passes, out cg);
                }
            }

            Console.Error.WriteLine($"Change: {lp}");
            Debug(cg.Length);
            Console.Error.WriteLine($"{bestPoint} ({bestPattern.Cast<char?>().Count(x => x.HasValue)})\n");
//            Debug($"100 × {goal.Count} - 10 × {guides} + {pass}");
            #if LOCAL
//            var sw = new StreamWriter("passed.txt");
//            for (int i = 0; i < N; i++)
//            {
//                for (int j = 0; j < N; j++)
//                {
//                    if(passes[i,j].Any()) sw.WriteLine($"{i} {j}");
//                }
//            }
//            sw.Dispose();
            #endif
            DirectionGuides = bestPattern;
        }

        // ways: ロボットごとの道順
        // squares: そのマスが通るロボット一覧
        private int UpdateEval(char?[,] directionGuides, int changeY, int changeX, ISet<int> goal, ref int pass, int guides, List<int[]>[] ways, HashSet<int>[,] passes, out int[][] cantGoal)
        {
            var change = passes[changeY, changeX].ToArray();
            // change が絡むロボットだけ再計算
            var cgList = new List<int[]>();
            foreach (var rbt in change)
            {
                foreach (var w in ways[rbt])
                {
                    if (passes[w[0], w[1]].Remove(rbt) && !passes[w[0], w[1]].Any())
                        pass--;
                }

                goal.Remove(rbt);
            }

            foreach (var rbt in change)
            {
                var robot = Robots[rbt];
                
                var passed = new bool[N, N, 4];
                int y = robot.Y, x = robot.X;
                char c = robot.C;
                var way = new List<int[]>();
                do
                {
                    passed[y, x, toInt(c)] = true;
                    if (!passes[y, x].Any()) pass++;
                    passes[y, x].Add(rbt);
                    way.Add(new[] {y, x});
                    if (directionGuides[y, x] != null)
                        c = directionGuides[y, x].Value;
                    switch (c)
                    {
                        case 'U':
                            --y;
                            break;
                        case 'D':
                            ++y;
                            break;
                        case 'L':
                            --x;
                            break;
                        case 'R':
                            ++x;
                            break;
                        default: throw new Exception();
                    }

                    if (y == -1) y = N - 1;
                    if (y == N) y = 0;
                    if (x == -1) x = N - 1;
                    if (x == N) x = 0;

                    if (!NoBlocks[y, x]) break;
                } while (!passed[y, x, toInt(c)]);

                if (IsGoal(y, x)) goal.Add(rbt);
                else if (Blocks[y, x]) cgList.Add(new[] {y, x});
                ways[rbt] = way;
            }
            cantGoal = cgList.ToArray();
            return goal.Count * 1000 - guides * 10 + pass;
        }

        
        private int SecondEval(char?[,] directionGuides, out HashSet<int> goal, out int pass, out int guides, out List<int[]>[] ways, out HashSet<int>[,] passes)
        {
            passes = new HashSet<int>[N, N];
            for(int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
            {
                passes[i, j] = new HashSet<int>();
            }

            goal = new HashSet<int>();
            ways = new List<int[]>[M];
            for (int i = 0; i < M; i++)
            {
                var robot = Robots[i];
                var passed = new bool[N, N, 4];
                int y = robot.Y, x = robot.X;
                char c = robot.C;
                var way = new List<int[]>();
                do
                {
                    passed[y, x, toInt(c)] = true;
                    passes[y, x].Add(i);
                    way.Add(new[] {y, x});
                    if (directionGuides[y, x] != null)
                        c = directionGuides[y, x].Value;
                    switch (c)
                    {
                        case 'U':
                            --y;
                            break;
                        case 'D':
                            ++y;
                            break;
                        case 'L':
                            --x;
                            break;
                        case 'R':
                            ++x;
                            break;
                        default: throw new Exception();
                    }

                    if (y == -1) y = N - 1;
                    if (y == N) y = 0;
                    if (x == -1) x = N - 1;
                    if (x == N) x = 0;

                    if (!NoBlocks[y, x]) break;
                } while (!passed[y, x, toInt(c)]);

                if (IsGoal(y, x)) goal.Add(i);
                ways[i] = way;
            }

            guides = 0;
            pass = 0;
            if (goal.Any()) pass = 1;
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (passes[i, j].Count > 0) ++pass;
                    if (directionGuides[i, j] != null)
                    {
                        if (NoBlocks[i, j])
                            ++guides;
                        else directionGuides[i, j] = null;
                    }
                }
            }

            return goal.Count * 1000 - guides * 10 + pass;
        }
        
        
        private char?[,] Randoms(out int point, out int[][] cantGoal)
        {
            var maxPoint = -10000000;
            char?[,] maxPattern = null;
            int[][] maxCantGoal = null;
            int lp = 0;

//            while (stopwatch.ElapsedMilliseconds < 1200)
            for (int _ = 0; _ < 4200; _++)
            {
                var rnd = Random();
                int[][] n;
                int[][] cg;
                var p = FirstEval(rnd, out n, out cg);
                foreach (var ints in n)
                {
                    rnd[ints[0], ints[1]] = null;
                }
                if (p > maxPoint)
                {
                    maxPoint = p;
                    maxPattern = rnd;
                    maxCantGoal = cg;
                }

                // 右端，左端，上端，下端 の順
                var cs = new int?[4];
                for (int i = 0; i < N; i++)
                {
                    if (cs[0] == null && Blocks[GoalY, (GoalX + i) % N])
                        cs[0] = (GoalX + i - 1) % N;
                    if (cs[1] == null && Blocks[GoalY, (GoalX - i + N) % N])
                        cs[1] = (GoalX - i + 1 + N) % N;
                    if (cs[2] == null && Blocks[(GoalY + i) % N, GoalX])
                        cs[2] = (GoalY + i - 1) % N;
                    if (cs[3] == null && Blocks[(GoalY - i + N) % N, GoalX])
                        cs[3] = (GoalY - i + 1 + N) % N;
                }

                {
                    if (cs[0] != null)
                    {
                        rnd[GoalY, cs[0] ?? 0] = 'L';
                        rnd[GoalY, cs[1] ?? 0] = 'R';
                    }

                    if (cs[2] != null)
                    {
                        rnd[cs[2] ?? 0, GoalX] = 'U';
                        rnd[cs[3] ?? 0, GoalX] = 'D';
                    }

                    p = FirstEval(rnd, out n, out cg);
                    foreach (var ints in n)
                    {
                        rnd[ints[0], ints[1]] = null;
                    }
                    if (p > maxPoint)
                    {
                        maxPoint = p;
                        maxPattern = rnd;
                        maxCantGoal = cg;
                    }
                }

                ++lp;
            }

            Console.Error.WriteLine($"Random: {lp}");
            cantGoal = maxCantGoal;
            point = maxPoint;
            return maxPattern;
        }

        private char?[,] Random()
        {
            var directionCount = rand.Next(100, 300);
            var noBlocksCount = NoBlockList.Length;
            var ret = new char?[N, N];
            for (int i = 0; i < directionCount; i++)
            {
                var place = NoBlockList[rand.Next(noBlocksCount)];
                int y = place[0], x = place[1];
                char c = toChar(rand.Next(4));
                ret[y, x] = c;
            }

            return ret;
        }

        private static int toInt(char c)
        {
            if (c == 'U') return 0;
            if (c == 'D') return 1;
            if (c == 'L') return 2;
            if (c == 'R') return 3;
            return -1;
        }

        private static char toChar(int i)
        {
            if (i == 0) return 'U';
            if (i == 1) return 'D';
            if (i == 2) return 'L';
            if (i == 3) return 'R';
            throw new ArgumentException();
        }

        private bool IsGoal(int y, int x) => y == GoalY && x == GoalX;

        private int FirstEval(char?[,] directionGuides, out int[][] notPlaced, out int[][] cantGoal)
        {
            int[,] passedAll = new int[N, N];
            int goal = 0;
            var cgList = new List<int[]>();
            for (int i = 0; i < M; i++)
            {
                var robot = Robots[i];
                var passed = new bool[N, N, 4];
                int y = robot.Y, x = robot.X;
                char c = robot.C;
                do
                {
                    passed[y, x, toInt(c)] = true;
                    passedAll[y, x]++;
                    if (directionGuides[y, x] != null)
                        c = directionGuides[y, x].Value;
                    switch (c)
                    {
                        case 'U':
                            --y;
                            break;
                        case 'D':
                            ++y;
                            break;
                        case 'L':
                            --x;
                            break;
                        case 'R':
                            ++x;
                            break;
                        default: throw new Exception();
                    }

                    if (y == -1) y = N - 1;
                    if (y == N) y = 0;
                    if (x == -1) x = N - 1;
                    if (x == N) x = 0;

                    if (!NoBlocks[y, x]) break;
                } while (!passed[y, x, toInt(c)]);

                if (IsGoal(y, x)) goal++;
                else if (Blocks[y, x]) cgList.Add(new[] {y, x});
            }

            int guides = 0, pass = 0;
            if (goal > 0) pass = 1;
            var ret2 = new List<int[]>();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (passedAll[i, j] > 0) ++pass;
                    if (directionGuides[i, j] != null && NoBlocks[i, j])
                    {
                        if (passedAll[i, j] == 0) ret2.Add(new[] {i, j});
                        else ++guides;
                    }
                }
            }

            notPlaced = ret2.ToArray();
            cantGoal = cgList.ToArray();
            
            return goal * 1000 - guides * 10 + pass;
        }

        private void Input()
        {
            var _ = ReadLine;
            var g = ReadIntArray;
            GoalY = g[0];
            GoalX = g[1];
            var robot = new Robot[M];
            for (int i = 0; i < M; i++)
            {
                var r = ReadArray;
                robot[i] = new Robot(int.Parse(r[0]), int.Parse(r[1]), r[2][0]);
            }

            Robots = robot.ToArray();
            BlockList = new int[B][];
            Blocks = new bool[N, N];
            for (int i = 0; i < B; i++)
            {
                var b = ReadIntArray;
                BlockList[i] = new[] {b[0], b[1]};
                Blocks[b[0], b[1]] = true;
            }

            NoBlocks = new bool[N, N];
            var noBlockList = new List<int[]>();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (!Blocks[i, j] && !IsGoal(i, j))
                    {
                        NoBlocks[i, j] = true;
                        noBlockList.Add(new[] {i, j});
                    }
                }
            }

            NoBlockList = noBlockList.ToArray();
        }

        private void Output()
        {
            var ls = new List<string>();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (DirectionGuides[i, j] == null || Blocks[i, j] || IsGoal(i, j)) continue;
                    ls.Add($"{i} {j} {DirectionGuides[i, j]}");
                }
            }

            sw.WriteLine(ls.Count);
            ls.ForEach(x => sw.WriteLine(x));
        }



        /* 以下テンプレート等 */

        #region

        private readonly int Limit;
        private readonly Random rand;
        private StreamWriter sw;
        private readonly Stopwatch stopwatch;

        public Program(int limit)
        {
            stopwatch = new Stopwatch();
            rand = new Random();
            Limit = limit;
        }

        public void Main()
        {
#if LOCAL && !NO_LOCAL
            sw = new StreamWriter("out.txt");
            sr = new StreamReader(IN_TXT);
#else
            sw = new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false};
            Console.SetOut(sw);
#endif
            stopwatch.Start();
            Input();
            Solve();
            Output();
            sw.Flush();
#if LOCAL && !NO_LOCAL
            sw.Dispose();
//            using (var r = new StreamReader("out.txt"))
//            {
//                while (!r.EndOfStream)
//                {
//                    Console.WriteLine(r.ReadLine());
//                }
//            }
#endif
        }

        public static void Main(string[] args)
        {
            int limit = 3000 - LIMIT_OFFSET;
//            if (args.Any()) limit *= 2;
            for (int i = 0; i < REPEATS; i++)
            {
                new Program(limit).Main();
#if !LOCAL
break;
#endif
            }
        }

        public void Debug(object o)
        {
#if LOCAL
            Console.WriteLine(o);
#endif
        }

        private StreamReader sr;

        public string ReadLine
        {
            get
            {
#if LOCAL && !NO_LOCAL
                return sr.ReadLine();
#else
                return Console.ReadLine();
#endif
            }
        }

        public string[] ReadArray => ReadLine.Split(' ');
        public int[] ReadIntArray => ReadArray.Select(int.Parse).ToArray();
        public int ReadInt => int.Parse(ReadLine);

        #endregion
    }

    public struct Robot
    {
        public Robot(int y, int x, char c)
        {
            X = x;
            Y = y;
            C = c;
        }

        public int X { get; set; }
        public int Y { get; set; }
        public char C { get; set; }
    }

    public struct DirectionGuide
    {
        public DirectionGuide(int y, int x, char c)
        {
            Y = y;
            X = x;
            C = c;
        }

        public int Y { get; }
        public int X { get; }
        public char C { get; }
    }

}
