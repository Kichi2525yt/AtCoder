using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Dynamic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
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

        private int N, M, L;
        private string[] S;
        private char[][] grid;
        private char[][] ans;
        public static StreamWriter sw;
        private readonly Random rand = new Random(2434335);

        
        private Stopwatch stopwatch;
#if LOCAL
        private const int LIMIT = 2978 * 4;
#else
        private const int LIMIT = 2978;
#endif

        private void Solve()
        {
            stopwatch = Stopwatch.StartNew();
            sw = new StreamWriter(Console.OpenStandardOutput()){AutoFlush = false};
            Console.SetOut(sw);
            Input();
            Calc();
            Output();
            Console.Out.Flush();
        }


        private void Calc()
        {
            var arr = new[] {'L', '.', 'R', 'L', 'D', 'D'};
            
            var answers = new List<Tuple<char[][], int>>();
            var max = -1;
            while (stopwatch.ElapsedMilliseconds < LIMIT)
            {
                var ans = new char[M][];

                ans[0] = ans[M - 1] = new string('#', M).ToCharArray();

                ans[1] = Rand(arr, true);
                ans[M - 2] = Rand(arr, true);
                for (int i = 2; i < M - 2; i++)
                {
                    ans[i] = Rand(arr);
                }

                var res = ans.Run(S);
                answers.Add(Tuple.Create(ans, res));
                max = Max(max, res);
            }

            var a = answers.First(x => x.Item2 == max);
            ans = a.Item1;
            Console.Error.WriteLine("Count: " + answers.Count + " Max: " + a.Item2);
        }

        char[] Rand(IReadOnlyList<char> arr, bool all = false)
        {
            var ret = new char[M];
            ret[0] = ret[M - 1] = '#';
            ret[1] = arr[rand.Next(1, 6)];
            ret[M - 2] = arr[rand.Next(1, 6)];
            for (int j = 2; j < M - 2; j++)
            {
                ret[j] = arr[all ? rand.Next(1, 6) : rand.Next(4)];
            }

            return ret;
        }

        private void Input()
        {

            N = cin.Int;
            M = cin.Int;
            L = cin.Int;
            S = cin.StrArray(N);


            this.ans = this.grid = defaulGrid;
        }

        private void Output()
        {
            var sw2 = new StreamWriter("out.txt");
            var sb = new StringBuilder();
            foreach (var c in ans)
            {
                sb.AppendLine(new string(c));
                sw2.WriteLine(new string(c));
            }
            sw.WriteLine(sb);
            sw2.Dispose();
        }

        private char[][] _default;
        private char[][] defaulGrid
        {
            get
            {
                char[][] grid = new char[M][];

                if (_default == null)
                {
                    grid = new char[M][];

                    grid[0] = grid[M - 1] = new string('#', M).ToCharArray();

                    var m = new List<char> {'#'};
                    m.AddRange(new string('.', M - 2).ToCharArray());
                    m.Add('#');
                    for (int i = 1; i < M - 1; i++)
                    {
                        grid[i] = new List<char>(m).ToArray();
                    }

                    _default = new char[M][];
                    Array.Copy(grid, _default, M);
                }
                else
                {
                    Array.Copy(_default, grid, M);
                }
                return grid;
            }
        }

    }

    static class Methods
    {
        public static int Run(this char[][] grid, string[] robots)
        {
            var M = grid.Length;
            var points = new int[M][];
            for (int i = 0; i < M; i++)
            {
                points[i] = new int[M];
            }

            foreach (var robot in robots)
            {
                var r = new Robot(robot, grid);
                while (r.Do()) ;
                points[r.point.Y][r.point.X]++;
            }

            return points.SelectMany(x => x).Select(x =>
            {
                if (x == 1) return 10;
                if (x == 2) return 3;
                if (x == 3) return 1;
                return 0;
            }).Sum();
        }
    }

    public enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }

    class Robot
    {
        public Robot(string s, char[][] grid)
        {
            S = s;
            point = new Point(14, 14);
            direction = Direction.Up;
            at = 0;
            this.grid = grid;
        }
        public Point point;
        public Direction direction;
        public int at;
        public string S;
        public readonly char[][] grid;
        

        public bool Do()
        {
            if (++at >= S.Length) return false;
            var m = grid[point.Y][point.X];
            var count = 1;
            if (m == 'D')
                count = 2;
            if (m == 'T')
                count = 3;
            for(int i = 0; i < count; i++){
                switch (S[at])
                {
                    case 'L':
                        if (m == 'R')
                            goto case 'R';

                        if (direction == Direction.Up)
                            direction = Direction.Left;
                        else
                            direction--;
                        break;
                    case 'R':
                        if (m == 'L')
                            goto case 'L';

                        if (direction == Direction.Left)
                            direction = Direction.Up;
                        else
                            direction++;
                        break;
                    case 'S':
                        switch (direction)
                        {
                            case Direction.Up:
                                if (grid[point.Y + 1][point.X] == '#') break;
                                point.Y++;
                                break;
                            case Direction.Right:
                                if (grid[point.Y][point.X + 1] == '#') break;
                                point.X++;
                                break;
                            case Direction.Down:
                                if (grid[point.Y - 1][point.X] == '#') break;
                                point.Y--;
                                break;
                            case Direction.Left:
                                if (grid[point.Y][point.X - 1] == '#') break;
                                point.X--;
                                break;
                        }

                        break;
                }
            }
            
            return true;
        }
    }

    struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }
        public int X;
        public int Y;
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
