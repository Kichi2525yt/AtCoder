using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using static System.Math;
// ReSharper disable MemberCanBeMadeStatic.Local
// ReSharper disable PossibleNullReferenceException
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable InvertIf
// ReSharper disable InconsistentNaming
// ReSharper disable ConvertIfStatementToSwitchStatement

namespace Solve
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Solve();
            Console.Read();
        }
        
        public static StreamWriter sw;
        private readonly Random rand = new Random();

        private Stopwatch stopwatch;
        private const int LIMIT =
#if LOCAL
//4コアなので4倍
        2962 * 2;
#else
            2962;
#endif

        private void Solve()
        {
            stopwatch = Stopwatch.StartNew();
            sw = new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false};
            Console.SetOut(sw);
            Input();
            Calc();
            Output();
            Console.Out.Flush();
        }
        
        private int T, N, M;
        private List<Monster> Monsters;
        private IList<string> Out;
        private IList<int> Level;
        private long money = 1000;
        private long[] rewards;
        private Tuple<Monster, int>[][] dp;

        private void Calc()
        {
            var max = 0L;
            var loop = 0;
            var tuples = Monsters
                .Select((x, j) => Tuple.Create(x, j)).ToArray();
            var steps = new List<Tuple<int, int>>();
            while (loop < 50)
            {
                Level = new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                var money = 1000L;
                var tmpStep = new List<Tuple<int, int>>();
                var set = new HashSet<int>();
                var p = loop % 10;
                var pp = loop % 20;
                var ii = 0;
                for (int i = 0; money < p * 20000; i++)
                {
                    ii++;
                    if (dp[i] == null)
                    {
                        dp[i] = tuples
                            .Where(x => i+1 >= x.Item1.Start  && i+1 <= x.Item1.End )
                            .ToArray();
                    }

                    var w = dp[i];

                    var at = rand.Next(w.Length);
                    var rew = Reward(i, w[at].Item1);
                    if (rew < 1000 || set.Contains(at))
                    {
                        tmpStep.Add(Tuple.Create(3, -1));
                        money += 1000;
                        continue;
                    }

                    money += rew;
                    tmpStep.Add(Tuple.Create(2, w[at].Item2 + 1));
                    set.Add(at);
                }

                var tmp = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}.OrderBy(_ =>new Random().Next()).Take(p).ToArray();
                
                for (int i = 0; i < p; i++)
                {
                    money -= 20000;
                    Level[tmp[i]]++;
                    
                    tmpStep.Add(Tuple.Create(1, i+1));
                    ii++;
                }

                if (pp >= 10)
                {
                    for (int i = 0; money < (pp - 9) * 40000; i++)
                    {
                        ii++;
                        if (dp[i] == null)
                        {
                            dp[i] = tuples
                                .Where(x => i+1 >= x.Item1.Start  && i+1 <= x.Item1.End )
                                .ToArray();
                        }

                        var w = dp[i];

                        var at = rand.Next(w.Length);
                        var rew = Reward(i, w[at].Item1);
                        if (rew < 1000 || set.Contains(at))
                        {
                            tmpStep.Add(Tuple.Create(3, -1));
                            money += 1000;
                            continue;
                        }

                        money += rew;
                        tmpStep.Add(Tuple.Create(2, w[at].Item2 + 1));
                        set.Add(at);
                    }
                    tmp = new[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9}.OrderBy(_ => new Random().Next()).Take(pp - 9).ToArray();


                    for (int i = 0; i < pp - 9; i++)
                    {
                        money -= 40000;
                        Level[tmp[i]]++;
                        
                        tmpStep.Add(Tuple.Create(1, i+1));
                        tmpStep.Add(Tuple.Create(1, i+1));
                        ii += 2;
                    }
                }

                for (int i = ii; i < T; i++)
                {
                    ii++;
                    if (dp[i] == null)
                    {
                        dp[i] = tuples
                            .Where(x => i+1 >= x.Item1.Start  && i+1 <= x.Item1.End )
                            .ToArray();
                    }

                    var w = dp[i];

                    var at = rand.Next(w.Length);
                    var rew = Reward(i, w[at].Item1);
                    if (rew < 1000 || set.Contains(at))
                    {
                        tmpStep.Add(Tuple.Create(3, -1));
                        money += 1000;
                        continue;
                    }

                    money += rew;
                    tmpStep.Add(Tuple.Create(2, w[at].Item2 + 1));
                    set.Add(at);
                }

                if (money > max)
                {
                    steps = tmpStep;
                    max = money;
                }

                this.money = max;
                loop += 1;
                //if (loop % 500 == 0)
                    Console.Error.WriteLine($"[{loop:00000}] MAX:{max}");
            }
            Console.Error.WriteLine("loopCount: " + loop + " Money: " + max * 0.0001d);

            while (stopwatch.ElapsedMilliseconds < LIMIT)
            {
                //現在の最適解
                var tmpSteps = steps.ToArray();

                //変更するところ
                var i = rand.Next(1000);

                if (tmpSteps[i].Item1 == 1) continue;

                
                if (dp[i] == null)
                {
                    dp[i] = tuples
                        .Where(x => i > x.Item1.Start + 1 && i < x.Item1.End + 1)
                        .ToArray();
                }

                var w = dp[i];

                var at = rand.Next(w.Length);
                
                var new_ = Reward(i, w[at].Item1);
                tmpSteps[i] = Tuple.Create(2, w[at].Item2 + 1);

                var mon = 1000L;
                //再計算
                var lv = new[] {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};
                for (var j = 0; j < tmpSteps.Length; j++)
                {
                    var step = tmpSteps[j];
                    if (step.Item1 == 3) mon += 1000;
                    else if (step.Item1 == 1)
                    {
                        var a = step.Item2 - 1;
                        if (lv[a] == 0)
                        {
                            mon -= 20000;
                            lv[a]++;
                        }
                        else
                        {
                            mon -= 40000;
                            lv[a]++;
                        }
                    }
                    else
                    {
                        mon += Reward(j, Monsters[step.Item2 - 1]);
                    }
                }

                if (max < mon)
                {
                    steps = tmpSteps.ToList();
                    max = mon;
                }

                loop++;
            }

            Console.Error.WriteLine("loopCount: " + loop + " Money: " + max * 0.0001d);




            foreach (var tuple in steps)
            {
                Out.Add(tuple.Item1 == 3 ? "3" : $"{tuple.Item1} {tuple.Item2}");
            }
        }



        private void Input()
        {
            Out = new List<string>();
            Monsters = new List<Monster>();
            Level = new List<int> {0, 0, 0, 0, 0, 0, 0, 0, 0, 0};

            T = cin.Int; //1000
            N = cin.Int; //10
            M = cin.Int; //30000

            for (int i = 0; i < M; i++)
            {
                var a = cin.Int;
                var b = cin.Int;
                var c = cin.Long;
                var s = cin.IntArray(N);
                Monsters.Add(new Monster(a, b, c, s));
            }
            dp = new Tuple<Monster, int>[T][];
        }

        private void Output()
        {
            var sb = new StringBuilder();

            //出力処理
            sb.AppendLine(string.Join("\n", Out));

            sw.WriteLine(sb);
            var sw2 = new StreamWriter("out.txt");
            sw2.Write(sb);
            sw2.Dispose();
        }

        private long Reward(int turn, Monster monster)
        {
            if (monster.Did) return 0;
            double add = monster.Reward;
            add *= 1 + 9 * (double) (turn - monster.Start) / (monster.End - monster.Start);
            int lack = 0;
            for (int j = 0; j < N; j++)
                lack += Max(0, monster.Requires[j] - Level[j]);
            if (lack == 0) add *= 10;
            else
            {
                add *= Pow(0.5, lack);
                add += 1e-9;
            }
            
            return (long) add;
        }
    }

    public struct Monster
    {
        public Monster(int start, int end, long reward, IEnumerable<int> requires)
        {
            Start = start;
            End = end;
            Reward = reward;
            Requires = requires.ToArray();
            Did = false;
        }
        public int Start;
        public int End;
        public long Reward;
        public int[] Requires;
        public bool Did { get; set; }

        public bool Do()
        {
            if (Did) return false;
            Did = true;
            return true;
        }
    }


    /* 以下テンプレ */

#pragma warning disable IDE1006 // 命名スタイル
    public static class cin
#pragma warning restore IDE1006 // 命名スタイル
    {
        private static readonly char _separator = ' ';
        private static readonly Queue<string> _input = new Queue<string>();
        private static readonly StreamReader sr =
#if LOCAL
            new StreamReader("in.txt");
#else
            new StreamReader(Console.OpenStandardInput());
#endif

        public static string ReadLine => Console.ReadLine();

        public static string Str => Read;

        public static string Read
        {
            get
            {
                if (_input.Count != 0) return _input.Dequeue();

                // ReSharper disable once PossibleNullReferenceException

                var tmp = sr.ReadLine().Split(_separator);
                foreach (var val in tmp)
                {
                    _input.Enqueue(val);
                }

                return _input.Dequeue();
            }
        }

        public static int Int => int.Parse(Read);

        public static long Long => long.Parse(Read);

        public static double Double => double.Parse(Read);

        public static string[] StrArray(long n)
        {
            var ret = new string[n];
            for (long i = 0; i < n; ++i) ret[i] = Read;
            return ret;
        }

        public static int[] IntArray(long n)
        {
            var ret = new int[n];
            for (long i = 0; i < n; ++i) ret[i] = Int;
            return ret;
        }

        public static long[] LongArray(long n)
        {
            var ret = new long[n];
            for (long i = 0; i < n; ++i) ret[i] = Long;
            return ret;
        }

        static bool TypeEquals<T, U>() => typeof(T) == typeof(U);
        static T ChangeType<T, U>(U a) => (T) System.Convert.ChangeType(a, typeof(T));

        static T Convert<T>(string s) => TypeEquals<T, int>() ? ChangeType<T, int>(int.Parse(s))
            : TypeEquals<T, long>() ? ChangeType<T, long>(long.Parse(s))
            : TypeEquals<T, double>() ? ChangeType<T, double>(double.Parse(s))
            : TypeEquals<T, char>() ? ChangeType<T, char>(s[0])
            : ChangeType<T, string>(s);

        static void Multi<T>(out T a) => a = Convert<T>(Str);

        static void Multi<T, U>(out T a, out U b)
        {
            var ar = StrArray(2);
            a = Convert<T>(ar[0]);
            b = Convert<U>(ar[1]);
        }

        static void Multi<T, U, V>(out T a, out U b, out V c)
        {
            var ar = StrArray(3);
            a = Convert<T>(ar[0]);
            b = Convert<U>(ar[1]);
            c = Convert<V>(ar[2]);
        }
    }

}
