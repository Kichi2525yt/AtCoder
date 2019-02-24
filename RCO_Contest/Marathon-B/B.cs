using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using static System.Math;
using static Marathon_B.Methods;
using static Marathon_B.cin;
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable JoinDeclarationAndInitializer
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
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable TailRecursiveCall
// ReSharper disable RedundantUsingDirective
// ReSharper disable InlineOutVariableDeclaration
// ReSharper disable SuggestBaseTypeForParameter
#pragma warning disable

namespace Marathon_B
{
    public class Program
    {
        private readonly Random rand = new Random();

        private Stopwatch stopwatch;

        private const int LIMIT = 1880;
        private StreamWriter sw;

        /* 入力値 */
        private const int N = 50, M = 2500;
        private int[][] A;

        /* 出力値 */
        private List<Pair<bool, Position>> ans;

        /* フィールド */
        private int maxPoint;



        private void Input()
        {
            int _;
            _ = ReadInt;
            _ = ReadInt;
            A = new int[N][];
            for (int i = 0; i < N; i++)
            {
                A[i] = IntArray(N);
            }

            ans = new List<Pair<bool, Position>>(N);
        }

        private void Output()
        {
            for (int i = 0; i < Min(ans.Count, M); i++)
            {
                var op = ans[i].first ? "2" : "1";
                var r = ans[i].second.Row;
                var c = ans[i].second.Column;
                sw.WriteLine($"{op} {r} {c}");
            }
            sw.Flush();
        }

        private void Calc()
        {
            //それぞれの区画について、手入れしたら収穫できる最大値で収穫する
            var point = 0;
            var tmp = new List<Pair<bool, Position>>(N);
            var a = A.ToArray();
            for (int i = 0; i < N; i++)
            {
                for (int j = 0; j < N; j++)
                {
                    if (a[i][j] == -1 || tmp.Count > M) continue;

                    for (int s = 10; s >= a[i][j]; s--)
                    {
                        var sr = Search(a, i, j, s);
                        var res = sr.first;
                        if (res.Count < s)
                        {
                            if (a[i][j] == 1 && s == 1)
                                tmp.Add(Make(true, i, j));
                            continue;
                        }

                        //収穫できるなら収穫する
                        while (a[i][j] < s)
                        {
                            tmp.Add(Make(false, i, j));
                            a[i][j]++;
                        }

                        foreach (var p in sr.second)
                        {
                            tmp.Add(Make(false, p.Row, p.Column));
                            a[p.Row][p.Column]++;
                        }

                        tmp.Add(Make(true, i, j));
                        a[i][j] = -1;
                        foreach (var p in res)
                        {
                            a[p.Row][p.Column] = -1;
                        }

                        point += res.Count * s;
                        if (a[i][j] == s) point += s;

                        break;
                    }
                }
            }

            if (point > maxPoint)
            {
                maxPoint = point;
                ans = tmp.Take(M).ToList();
            }
        }

        private Pair<List<Position>, List<Position>> Search(int[][] a, int r, int c, int s)
        {
            var que = new Queue<Position>();
            que.Enqueue(new Position(r, c));
            var list = new List<Position>();
            var add = new List<Position>();
            //if (a[r][c] == s) list.Add(new Position(r, c));
            var start = new Position(r, c);

            while (que.Any())
            {
                var dq = que.Dequeue();
                var cr = dq.Row;
                var cc = dq.Column;

                for (int i = 0; i < 4; i++)
                {
                    var nr = cr + dx[i];
                    var nc = cc + dy[i];
                    if (!(nr >= 0 && nr < N && nc >= 0 && nc < N) || a[nr][nc] == -1 ||
                        list.Contains(new Position(nr, nc))) continue;
                    var diff = s - a[nr][nc];
                    if (diff >= 0 && diff <= 2)
                    {
                        var ps = new Position(nr, nc);
                        que.Enqueue(ps);
                        list.Add(ps);
                        if (ps.Equals(start)) continue;
                        for (int j = 0; j < diff; j++)
                        {
                            add.Add(ps);
                        }
                    }
                }
            }

            return make_pair(list, add);
        }


        private Pair<bool, Position> Make(bool b, int r, int c) => make_pair(b, new Position(r, c));
        /*
        
        private int Point(IEnumerable<Pair<bool, Position>> res)
        {
            var a = A.ToArray();
            var score = 0;
            var c = 0;
            foreach (var pair in res)
            {
                c++;
                int cr = pair.second.Row;
                var cc = pair.second.Column;

                if (a[cr][cc] == -1)
                {
                    Console.Error.WriteLine($"[warn] {c}: 収穫済みの場所を収穫しようとしました ({cr}, {cc})");
                    continue;
                }

                if (pair.first)
                {
                    var diff = Remove(a, cr, cc);
                    if (diff == 0)
                    {
                        Console.Error.WriteLine($"[warn] {c}: 区画が条件を満たしていません（{cr}, {cc}）");
                    }
                    else
                        Console.Error.WriteLine($"[info] {c}: 収穫(+{diff}) （{cr}, {cc}）");
                    score += diff;
                    continue;
                }
                a[cr][cc]++;
                Console.Error.WriteLine($"[info] {c}: 手入れ {cr}, {cc}）");
            }

            return score;

        }

        int Remove(int[][] a, int r, int c)
        {
            int s = a[r][c];
            if (s == -1) return 0;
            var que = new Queue<Position>();
            que.Enqueue(new Position(r, c));
            var list = new List<Position>(){que.Peek()};
            
            while (que.Any())
            {
                var dq = que.Dequeue();
                var cr = dq.Row;
                var cc = dq.Column;

                for (int i = 0; i < 4; i++)
                {
                    var nr = cr + dx[i];
                    var nc = cc + dy[i];
                    if (!(nr >= 0 && nr < N && nc >= 0 && nc < N)) continue;
                    if (a[nr][nc] == s && !list.Contains(new Position(nr, nc)))
                    {
                        que.Enqueue(new Position(nr, nc));
                        list.Add(new Position(nr, nc));
                    }
                }
            }

            if (list.Count < s) return s == 1 ? 1 : 0;

            foreach (var p in list)
            {
                a[p.Row][p.Column] = -1;
            }

            return list.Count * s;
        }
        */
        private void Solve()
        {
            stopwatch = Stopwatch.StartNew();
            sw =
#if LOCAL
                new StreamWriter("out.txt");
#else
            new StreamWriter(Console.OpenStandardOutput()) { AutoFlush = false };
#endif
            Console.SetOut(sw);
            Input();
            Calc();
            Output();
#if !LOCAL
            Console.Out.Flush();
#endif
            Console.Read();
        }

        private readonly int[] dx = { -1, 0, 0, 1 };
        private readonly int[] dy = { 0, 1, -1, 0 };
        private const int MOD = 1000000007;

        /// <summary>aとbをスワップする</summary>
        private static void Swap<T>(ref T a, ref T b) where T : struct
        {
            var tmp = b;
            b = a;
            a = tmp;
        }

        /// <summary>aとbの最大公約数を求める</summary>
        private static long Gcd(long a, long b)
        {
            if (a < b) Swap(ref a, ref b);
            return a % b == 0 ? b : Gcd(b, a % b);
        }

        /// <summary>aとbの最小公約数を求める</summary>
        private static long Lcm(long a, long b) => a / Gcd(a, b) * b;

        public static void Print(params object[] args)
        {
            foreach (var s in args)
            {
                Console.WriteLine(s);
            }
        }

        private static void PrintBool(bool val, string yes = "Yes", string no = "No")
            => Console.WriteLine(val ? yes : no);

        public static void PrintDebug(params object[] args)
            => Console.Error.WriteLine(string.Join(" ", args));

        public static void Main(string[] args)
        {
            new Program().Solve();
        }
    }

    public class Position : IComparable<Position>, IEquatable<Position>
    {
        public Position(int row, int column)
        {
            this.Row = row;
            this.Column = column;
        }

        public int Row;
        public int Column;

        public int CompareTo(Position other)
        {
            throw new Exception();
        }

        public bool Equals(Position other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Row == other.Row && Column == other.Column;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Position)obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (Row * 397) ^ Column;
            }
        }
    }

    public class Pair<T1, T2>
    {
        public Pair(T1 first, T2 second)
        {
            this.first = first;
            this.second = second;
        }

        public T1 first;
        public T2 second;
    }

    public static class Methods
    {
        public static Pair<T1, T2> make_pair<T1, T2>(T1 f, T2 l)
            => new Pair<T1, T2>(f, l);

        public static Trans Maketrans(string a, string b) => new Trans(a, b);
        public static Trans Maketrans(string a, string b, string c) => new Trans(a, b, c);

        public static string Translate(this string str, Trans trans)
        {
            var s = str;
            for (int i = 0; i < trans.Before.Length; i++)
            {
                if (trans.Delete.Contains(trans.Before[i])) continue;
                s = s.Replace(trans.Before[i], trans.After[i]);
            }

            return s;
        }

        /// <summary>
        /// 指定した値以上の先頭のインデクスを返す
        /// </summary>
        /// <typeparam name="T">比較する値の型</typeparam>
        /// <param name="arr">対象の配列（※ソート済みであること）</param>
        /// <param name="start">開始インデクス [inclusive]</param>
        /// <param name="end">終了インデクス [exclusive]</param>
        /// <param name="value">検索する値</param>
        /// <param name="comparer">比較関数(インターフェイス)</param>
        /// <returns>指定した値以上の先頭のインデクス</returns>
        public static int LowerBound<T>(T[] arr, int start, int end, T value, IComparer<T> comparer)
        {
            int low = start;
            int high = end;
            while (low < high)
            {
                var mid = ((high - low) >> 1) + low;
                if (comparer.Compare(arr[mid], value) < 0)
                    low = mid + 1;
                else
                    high = mid;
            }
            return low;
        }

        //引数省略のオーバーロード
        public static int LowerBound<T>(T[] arr, T value) where T : IComparable
        {
            return LowerBound(arr, 0, arr.Length, value, Comparer<T>.Default);
        }

        /// <summary>
        /// 指定した値より大きい先頭のインデクスを返す
        /// </summary>
        /// <typeparam name="T">比較する値の型</typeparam>
        /// <param name="arr">対象の配列（※ソート済みであること）</param>
        /// <param name="start">開始インデクス [inclusive]</param>
        /// <param name="end">終了インデクス [exclusive]</param>
        /// <param name="value">検索する値</param>
        /// <param name="comparer">比較関数(インターフェイス)</param>
        /// <returns>指定した値より大きい先頭のインデクス</returns>
        public static int UpperBound<T>(T[] arr, int start, int end, T value, IComparer<T> comparer)
        {
            int low = start;
            int high = end;
            while (low < high)
            {
                var mid = ((high - low) >> 1) + low;
                if (comparer.Compare(arr[mid], value) <= 0)
                    low = mid + 1;
                else
                    high = mid;
            }
            return low;
        }

        //引数省略のオーバーロード
        public static int UpperBound<T>(T[] arr, T value)
        {
            return UpperBound(arr, 0, arr.Length, value, Comparer<T>.Default);
        }

        public class Trans
        {

            public Trans(string a, string b)
            {
                if (a.Length != b.Length)
                    throw new ArgumentException($"引数 {nameof(a)}と{nameof(b)}は同じ長さである必要があります。");

                Before = a.ToCharArray();
                After = b.ToCharArray();
                Delete = new char[0];
            }

            public Trans(string a, string b, string c) : this(a, b)
            {
                Delete = c.ToCharArray();
            }

            public char[] Before { get; }
            public char[] After { get; }
            public char[] Delete { get; }
        }
    }

    public static class cin
    {
        private const char _separator = ' ';
        private static readonly Queue<string> _input = new Queue<string>();
        private static readonly StreamReader sr =
#if LOCAL
            new StreamReader("input.txt");
#else
            new StreamReader(Console.OpenStandardInput());
#endif

        public static string ReadLine => sr.ReadLine();

        public static string ReadStr => Read;

        public static string Read
        {
            get {
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

        public static int ReadInt => int.Parse(Read);

        public static long ReadLong => long.Parse(Read);

        public static double ReadDouble => double.Parse(Read);

        public static string[] StrArray() => ReadLine.Split(' ');

        public static int[] IntArray() => ReadLine.Split(' ').Select(int.Parse).ToArray();

        public static long[] LongArray() => ReadLine.Split(' ').Select(long.Parse).ToArray();

        public static string[] StrArray(long n)
        {
            var ret = new string[n];
            for (long i = 0; i < n; ++i) ret[i] = Read;
            return ret;
        }

        public static int[] IntArray(long n) => StrArray(n).Select(int.Parse).ToArray();

        public static long[] LongArray(long n) => StrArray(n).Select(long.Parse).ToArray();

        static bool TypeEquals<T, U>() => typeof(T) == typeof(U);
        static T ChangeType<T, U>(U a) => (T)System.Convert.ChangeType(a, typeof(T));

        static T Convert<T>(string s) => TypeEquals<T, int>() ? ChangeType<T, int>(int.Parse(s))
            : TypeEquals<T, long>() ? ChangeType<T, long>(long.Parse(s))
            : TypeEquals<T, double>() ? ChangeType<T, double>(double.Parse(s))
            : TypeEquals<T, char>() ? ChangeType<T, char>(s[0])
            : ChangeType<T, string>(s);

        public static void Multi<T>(out T a) => a = Convert<T>(ReadStr);

        public static void Multi<T, U>(out T a, out U b)
        {
            var ar = StrArray(2);
            a = Convert<T>(ar[0]);
            b = Convert<U>(ar[1]);
        }

        public static void Multi<T, U, V>(out T a, out U b, out V c)
        {
            var ar = StrArray(3);
            a = Convert<T>(ar[0]);
            b = Convert<U>(ar[1]);
            c = Convert<V>(ar[2]);
        }
    }
}
