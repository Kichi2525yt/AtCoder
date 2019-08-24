using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Text;
using static System.Math;
using static Solve.Input;
using static Solve.Output;
using static Solve.Methods;
using MethodImpl = System.Runtime.CompilerServices.MethodImplAttribute;
using MethodImplOptions = System.Runtime.CompilerServices.MethodImplOptions;
using pii = Solve.Pair<int, int>;
using pll = Solve.Pair<long, long>;
using pli = Solve.Pair<long, int>;
using pil = Solve.Pair<int, long>;
using pss = Solve.Pair<string, string>;
using psi = Solve.Pair<string, int>;
using vint = Solve.vector<int>;
using vlong = Solve.vector<long>;
using vstr = Solve.vector<string>;
using vii = Solve.vector<Solve.Pair<int, int>>;
using vll = Solve.vector<Solve.Pair<long, long>>;
using vli = Solve.vector<Solve.Pair<long, int>>;
using vil = Solve.vector<Solve.Pair<int, long>>;

namespace Solve
{
    //ライブラリ置き場
    
    
    
    //ライブラリ置き場ここまで

    public class Solver
    {
        public void Main()
        {
            
            
            
        }


        // ReSharper disable UnusedMember.Local
        private const int MOD = (int) 1e9 + 7, INF = 1000000010;
    }
    
    #region Templete

#if !LOCAL
namespace Library { }
#endif
    public static class Methods
    {
        public static readonly int[] dx = {-1, 0, 0, 1};

        public static readonly int[] dy = {0, 1, -1, 0};

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Assert(bool b, string message = null)
        {
            if (!b) throw new Exception(message ?? "Assert failed.");
        }

        /*
        public static Comparison<T> greater<T>() 
            where T : IComparable<T> 
            => (a, b) => b.CompareTo(a);
        */

        public static string JoinSpace<T>(this IEnumerable<T> source) => source.Join(" ");
        public static string JoinEndline<T>(this IEnumerable<T> source) => source.Join("\n");
        public static string Join<T>(this IEnumerable<T> source, string s) => string.Join(s, source);
        public static string Join<T>(this IEnumerable<T> source, char c) => string.Join(c.ToString(), source);

        /// <summary>
        /// <see cref="Pair{T1, T2}"/> クラスのインスタンスを作成します。
        /// </summary>
        /// <typeparam name="T1">firstの型</typeparam>
        /// <typeparam name="T2">secondの型</typeparam>
        /// <param name="first">firstの値</param>
        /// <param name="second">secondの値</param>
        /// <returns>作成した<see cref="Pair{T1, T2}"/> クラスのインスタンス</returns>
        public static Pair<T1, T2> make_pair<T1, T2>(T1 first, T2 second)
            where T1 : IComparable<T1>
            where T2 : IComparable<T2>
            => new Pair<T1, T2>(first, second);

        /// <summary>aとbをスワップします。に</summary>
        public static void Swap<T>(ref T a, ref T b) where T : struct
        {
            var tmp = b;
            b = a;
            a = tmp;
        }

        /// <summary>aとbの最大公約数を求めます。</summary>
        /// <returns>aとbの最大公約数</returns>
        public static long Gcd(long a, long b)
        {
            while (true)
            {
                if (a < b) Swap(ref a, ref b);
                if (a % b == 0) return b;
                var x = a;
                a = b;
                b = x % b;
            }
        }

        /// <summary>aとbの最小公倍数を求めます。</summary>
        /// <returns>aとbの最小公倍数</returns>
        public static long Lcm(long a, long b) => a / Gcd(a, b) * b;

        /// <summary>
        /// 指定した数値が素数であるかを判定します。
        /// </summary>
        /// <remarks>計算量 (sqrt(value)) </remarks>
        /// <param name="value">判定する数値</param>
        /// <returns>value が素数であるか</returns>
        public static bool IsPrime(long value)
        {
            if (value <= 1) return false;
            for (long i = 2; i * i <= value; i++)
                if (value % i == 0)
                    return false;
            return true;
        }

        /// <summary>
        /// <see cref="a"/> ^ <see cref="b"/> (mod <see cref="p"/>) を求める
        /// </summary>
        /// <returns><see cref="a"/> ^ <see cref="b"/> (mod <see cref="p"/>) の値</returns>
        public static long PowMod(long a, long b, long p)
        {
            long res = 1;
            while (b > 0)
            {
                if (b % 2 != 0) res = res * a % p;
                a = a * a % p;
                b >>= 1;
            }

            return res;
        }

        /// <summary>
        /// mod pにおけるaの逆元を求めます。
        /// </summary>
        /// <param name="a"></param>
        /// <param name="p">法</param>
        /// <returns></returns>
        public static long ModInv(long a, long p)
            => PowMod(a, p - 2, p);

        public static int DivCeil(int left, int right)
            => left / right + (left % right == 0 ? 0 : 1);

        public static long DivCeil(long left, long right)
            => left / right + (left % right == 0L ? 0L : 1L);

        /// <summary>
        /// src の順列を求めます。
        /// </summary>
        /// <typeparam name="T">型</typeparam>
        /// <param name="src">順列を求める配列</param>
        /// <returns>src の順列</returns>
        public static IEnumerable<T[]> Permutations<T>(IEnumerable<T> src)
        {
            var ret = new List<T[]>();
            Search(ret, new Stack<T>(), src.ToArray());
            return ret;
        }

        private static void Search<T>(ICollection<T[]> perms, Stack<T> stack, T[] a)
        {
            int N = a.Length;
            if (N == 0) perms.Add(stack.Reverse().ToArray());
            else
            {
                var b = new T[N - 1];
                Array.Copy(a, 1, b, 0, N - 1);
                for (int i = 0; i < a.Length; ++i)
                {
                    stack.Push(a[i]);
                    Search(perms, stack, b);
                    if (i < b.Length) b[i] = a[i];
                    stack.Pop();
                }
            }
        }

        /// <summary>
        /// 指定した条件を満たす最小の数値を返します。
        /// </summary>
        /// <param name="low">検索する数値の最小値</param>
        /// <param name="high">検索する数値の最大値</param>
        /// <param name="expression">条件</param>
        /// <returns>条件を満たす最小の数値</returns>
        public static int BinarySearch(int low, int high, Func<int, bool> expression)
        {
            while (low < high)
            {
                int middle = (high - low) / 2 + low;
                if (expression(middle))
                    high = middle;
                else
                    low = middle + 1;
            }

            return high;
        }

        /// <summary>
        /// 指定した値以上の先頭のインデクスを返します。
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

        /// <summary>
        /// 指定した値以上の先頭のインデクスを返します。
        /// </summary>
        /// <typeparam name="T">比較する値の型</typeparam>
        /// <param name="arr">対象の配列（※ソート済みであること）</param>
        /// <param name="value">検索する値</param>
        /// <returns>指定した値以上の先頭のインデクス</returns>
        public static int LowerBound<T>(T[] arr, T value) where T : IComparable
        {
            return LowerBound(arr, 0, arr.Length, value, Comparer<T>.Default);
        }

        /// <summary>
        /// 指定した値より大きい先頭のインデクスを返します。
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

        /// <summary>
        /// 指定した値より大きい先頭のインデクスを返します。
        /// </summary>Z
        /// <typeparam name="T">比較する値の型</typeparam>
        /// <param name="arr">対象の配列（※ソート済みであること）</param>
        /// <param name="value">検索する値</param>
        /// <returns>指定した値より大きい先頭のインデクス</returns>
        public static int UpperBound<T>(T[] arr, T value)
        {
            return UpperBound(arr, 0, arr.Length, value, Comparer<T>.Default);
        }

        public static IEnumerable<TResult> SelectNotNull<TSource, TResult>(this IEnumerable<TSource> source,
            Func<TSource, TResult> func)
            => source.Where(val => val != null).Select(func);

        public static IEnumerable<TSource> WhereNotNull<TSource>(this IEnumerable<TSource> source)
            => source.Where(val => val != null);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T[] ArrayOf<T>(params T[] arr) => arr;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static List<T> ListOf<T>(params T[] arr) => new List<T>(arr);

        public static IList<TSource> MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            return MaxBy(source, keySelector, Comparer<TKey>.Default);
        }

        public static IList<TSource> MaxBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            return ExtremaBy(source, keySelector, comparer.Compare);
        }

        public static IList<TSource> MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            return MinBy(source, keySelector, Comparer<TKey>.Default);
        }

        public static IList<TSource> MinBy<TSource, TKey>(this IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, IComparer<TKey> comparer)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            if (comparer == null) throw new ArgumentNullException(nameof(comparer));
            return ExtremaBy(source, keySelector, (key, minValue) => -comparer.Compare(key, minValue));
        }

        private static IList<TSource> ExtremaBy<TSource, TKey>(IEnumerable<TSource> source,
            Func<TSource, TKey> keySelector, Func<TKey, TKey, int> compare)
        {
            var result = new List<TSource>();

            using (var e = source.GetEnumerator())
            {
                if (!e.MoveNext())
                    throw new InvalidOperationException("シーケンスに要素がありませんでした。");

                var current = e.Current;
                var resKey = keySelector(current);
                result.Add(current);

                while (e.MoveNext())
                {
                    var cur = e.Current;
                    var key = keySelector(cur);

                    var cmp = compare(key, resKey);
                    if (cmp == 0)
                    {
                        result.Add(cur);
                    }
                    else if (cmp > 0)
                    {
                        result = new List<TSource> {cur};
                        resKey = key;
                    }
                }
            }

            return result;
        }

        public static IEnumerable<TResult> Repeat<TResult>(TResult value)
        {
            while (true) yield return value;
            // ReSharper disable once IteratorNeverReturns
        }

        public static IEnumerable<TResult> Repeat<TResult>(TResult value, int count)
            => Enumerable.Repeat(value, count);

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<TResult> Repeat<TResult>(this IEnumerable<TResult> source, int count)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (count < 0) throw new ArgumentOutOfRangeException(nameof(count));
            for (int i = 0; i < count; i++)
                foreach (var v in source)
                    yield return v;
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("ReSharper", "PossibleMultipleEnumeration")]
        public static IEnumerable<TResult> Repeat<TResult>(this IEnumerable<TResult> source)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            while (true)
                foreach (var v in source)
                    yield return v;
        }

        /// <summary>
        /// 文字の配列を文字列に変換します。
        /// </summary>
        /// <param name="source">文字の配列</param>
        /// <returns>変換した文字列</returns>
        public static string AsString(this IEnumerable<char> source) => new string(source.ToArray());

        /// <summary>
        /// <see cref="source"/> の累積和を返します。
        /// </summary>
        /// <returns><see cref="source"/> の累積和</returns>
        public static IEnumerable<long> CumSum(this IEnumerable<long> source)
        {
            long sum = 0;
            foreach (var item in source)
                yield return sum += item;
        }

        /// <summary>
        /// <see cref="source"/> の累積和を返します。
        /// </summary>
        /// <returns><see cref="source"/> の累積和</returns>
        public static IEnumerable<int> CumSum(this IEnumerable<int> source)
        {
            int sum = 0;
            foreach (var item in source)
                yield return sum += item;
        }

        /// <summary>
        /// <see cref="value"/>が l以上 r未満の範囲に含まれているかを返します。
        /// </summary>
        /// <typeparam name="T">要素の型</typeparam>
        /// <param name="value">判定する値</param>
        /// <param name="l">下限の値 (含まれる)</param>
        /// <param name="r">下限の値 (含まれない)</param>
        /// <returns><see cref="value"/> が指定した範囲に含まれているか</returns>
        public static bool IsIn<T>(this T value, T l, T r)
            where T : IComparable<T>
        {
            if (l.CompareTo(r) > 0) throw new ArgumentException();
            return l.CompareTo(value) <= 0 && value.CompareTo(r) < 0;
        }

        /// <summary>
        /// <see cref="start"/> 以上 <see cref="end"/> 未満の値を <see cref="step"/> ずつ増やした結果を列挙します。
        /// </summary>
        /// <param name="start">値の下限 (含まれる)</param>
        /// <param name="end">値の上限 (含まれない)</param>
        /// <param name="step">1要素ごとに増やす値</param>
        /// <returns>範囲の結果</returns>
        public static IEnumerable<int> Range(int start, int end, int step = 1)
        {
            for (var i = start; i < end; i += step) yield return i;
        }

        /// <summary>
        /// 0 以上 <see cref="end"/> 未満の値を 1 ずつ増やした結果を列挙します。
        /// </summary>
        /// <param name="end">値の上限 (含まれない)</param>
        /// <returns>範囲の結果</returns>
        public static IEnumerable<int> Range(int end) => Range(0, end);

        /// <summary>
        /// <see cref="start"/> 以上 <see cref="end"/> 未満の値を <see cref="step"/> ずつ増やした結果を逆順に列挙します。
        /// </summary>
        /// <param name="start">値の下限 (含まれる)</param>
        /// <param name="end">値の上限 (含まれない)</param>
        /// <param name="step">1要素ごとに増やす値</param>
        /// <returns>範囲の結果</returns>
        public static IEnumerable<int> RangeReverse(int start, int end, int step = 1)
        {
            for (var i = end - 1; i >= start; i -= step) yield return i;
        }

        /// <summary>
        /// 0 以上 <see cref="end"/> 未満の値を 1 ずつ増やした結果を逆順に列挙します。
        /// </summary>
        /// <param name="end">値の上限 (含まれない)</param>
        /// <returns>範囲の結果</returns>
        public static IEnumerable<int> RangeReverse(int end) => RangeReverse(0, end);

        /// <summary>
        /// valueを素因数分解し、素因数を列挙します。
        /// </summary>
        /// <param name="value">素因数分解する値</param>
        /// <returns>素因数の集合</returns>
        public static IEnumerable<int> Factorize(int value)
        {
            int first = value;
            for (int i = 2; i * i < first; i++)
            {
                while (value % i == 0)
                {
                    value /= i;
                    yield return i;
                }
            }

            if (value > 1)
                yield return value;
        }

        /// <summary>
        /// valueを素因数分解し、素因数を列挙します。
        /// </summary>
        /// <param name="value">素因数分解する値</param>
        /// <returns>素因数の集合</returns>
        public static IEnumerable<long> Factorize(long value)
        {
            long first = value;
            for (long i = 2; i * i < first; i++)
            {
                while (value % i == 0)
                {
                    value /= i;
                    yield return i;
                }
            }

            if (value > 1)
                yield return value;
        }

        /// <summary>
        /// valueを素因数分解し、素因数とその個数の連想配列を返します。
        /// </summary>
        /// <param name="value">素因数分解する値</param>
        /// <returns>素因数の連想配列</returns>
        public static Dictionary<long, int> FactorizeAsMap(long value)
        {
            var dict = new Dictionary<long, int>();
            long first = value;
            for (int i = 2; i * i < first; i++)
            {
                if (value % i > 0) continue;
                int cnt = 0;
                while (value % i == 0)
                {
                    value /= i;
                    cnt++;
                }

                dict.Add(i, cnt);
            }

            if (value > 1) dict.Add(value, 1);
            return dict;
        }

        /// <summary>
        /// valueを素因数分解し、素因数とその個数の連想配列を返します。
        /// </summary>
        /// <param name="value">素因数分解する値</param>
        /// <returns>素因数の連想配列</returns>
        public static Dictionary<int, int> FactorizeAsMap(int value)
        {
            var dict = new Dictionary<int, int>();
            int first = value;
            for (int i = 2; i * i < first; i++)
            {
                if (value % i > 0) continue;
                int cnt = 0;
                while (value % i == 0)
                {
                    value /= i;
                    cnt++;
                }

                dict.Add(i, cnt);
            }

            if (value > 1) dict.Add(value, 1);
            return dict;
        }

        /// <summary>
        /// 指定した配列をコピーして昇順ソートします。（非破壊的）
        /// </summary>
        /// <typeparam name="T">ソートする配列の型</typeparam>
        /// <param name="arr">ソートする配列</param>
        /// <returns>ソートされた配列</returns>
        public static T[] Sort<T>(this T[] arr)
        {
            var array = new T[arr.Length];
            arr.CopyTo(array, 0);
            Array.Sort(array);
            return array;
        }

        /// <summary>
        /// 指定した配列をコピーして降順ソートします。（非破壊的）
        /// </summary>
        /// <typeparam name="T">ソートする配列の型</typeparam>
        /// <param name="arr">ソートする配列</param>
        /// <returns>ソートされた配列</returns>
        public static T[] SortDescending<T>(this T[] arr)
        {
            var array = new T[arr.Length];
            arr.CopyTo(array, 0);
            Array.Sort(array);
            Array.Reverse(array);
            return array;
        }

        /// <summary>
        /// valueの約数の個数を求めます。
        /// </summary>
        /// <param name="value">約数の個数を求める数</param>
        /// <returns>valueの約数の個数</returns>
        public static long Divisors(long value)
        {
            var fact = FactorizeAsMap(value);
            return fact.Select(x => x.Value + 1L).Aggregate((m, x) => m * x);
        }

        public static double Log2(double x) => Log(x, 2);

        public static bool chmin<T>(ref T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) > 0)
            {
                a = b;
                return true;
            }

            return false;
        }

        public static bool chmax<T>(ref T a, T b) where T : IComparable<T>
        {
            if (a.CompareTo(b) < 0)
            {
                a = b;
                return true;
            }

            return false;
        }

        public static T Min<T>(params T[] col) => col.Min();
        public static T Max<T>(params T[] col) => col.Max();


        /// <summary>
        /// 要素数 (a, b) の、defaultValue で満たされた配列を作成します。
        /// </summary>
        /// <typeparam name="T">配列の型</typeparam>
        /// <param name="a">1次元の要素数</param>
        /// <param name="b">2次元の要素数</param>
        /// <param name="defaultValue">デフォルト値</param>
        /// <returns>指定した条件で初期化された配列</returns>
        public static T[][] Array2D<T>(int a, int b, T defaultValue = default(T))
        {
            var ret = new T[a][];
            for (int i = 0; i < a; i++)
            {
                ret[i] = Enumerable.Repeat(defaultValue, b).ToArray();
            }

            return ret;
        }

        public static vector<T> ToVector<T>(IEnumerable<T> source) => new vector<T>(source);
    }

    public static class Input
    {
        private const char _separator = ' ';
        private static readonly Queue<string> _input = new Queue<string>();
        private static readonly StreamReader sr =
#if FILE
            new StreamReader("in.txt");
#else
            new StreamReader(Console.OpenStandardInput());
#endif

        public static string ReadLine => sr.ReadLine();
        public static string ReadStr => Read;

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

        public static int ReadInt => int.Parse(Read);
        public static long ReadLong => long.Parse(Read);
        public static double ReadDouble => double.Parse(Read);
        public static string[] StrArray() => ReadLine.Split(' ');
        public static int[] IntArray() => ReadLine.Split(' ').Select(int.Parse).ToArray();
        public static long[] LongArray() => ReadLine.Split(' ').Select(long.Parse).ToArray();

        public static string[] StrArray(int n)
        {
            var ret = new string[n];
            for (long i = 0; i < n; ++i) ret[i] = Read;
            return ret;
        }

        public static int[] IntArray(int n, int offset = 0, bool sorted = false)
        {
            var ret = StrArray(n).Select(x => int.Parse(x) + offset).ToArray();
            if (sorted) Array.Sort(ret);
            return ret;
        }

        public static long[] LongArray(int n, long offset = 0, bool sorted = false)
        {
            var ret = StrArray(n).Select(x => long.Parse(x) + offset).ToArray();
            if (sorted) Array.Sort(ret);
            return ret;
        }

        public static string[][] Str2DArray(int n, int m)
            => Enumerable.Repeat((string[]) null, n).Select(_ => StrArray(m)).ToArray();

        public static int[][] Int2DArray(int n, int m, int offset = 0)
            => Enumerable.Repeat((int[]) null, n).Select(_ => IntArray(m, offset)).ToArray();

        public static long[][] Long2DArray(int n, int m, long offset = 0)
            => Enumerable.Repeat((long[]) null, n).Select(_ => LongArray(m, offset)).ToArray();

        public static Tuple<string[], string[]> StrArrays2(int n)
        {
            var ret1 = new string[n];
            var ret2 = new string[n];
            for (int i = 0; i < n; i++)
            {
                ret1[i] = ReadStr;
                ret2[i] = ReadStr;
            }

            return Tuple.Create(ret1, ret2);
        }

        public static Tuple<int[], int[]> IntArrays2(int n, int offset1 = 0, int offset2 = 0)
        {
            var ret1 = new int[n];
            var ret2 = new int[n];
            for (int i = 0; i < n; i++)
            {
                ret1[i] = ReadInt + offset1;
                ret2[i] = ReadInt + offset2;
            }

            return Tuple.Create(ret1, ret2);
        }

        public static Tuple<long[], long[]> LongArrays2(int n, long offset1 = 0, long offset2 = 0)
        {
            var ret1 = new long[n];
            var ret2 = new long[n];
            for (int i = 0; i < n; i++)
            {
                ret1[i] = ReadLong + offset1;
                ret2[i] = ReadLong + offset2;
            }

            return Tuple.Create(ret1, ret2);
        }

        public static Tuple<string[], string[], string[]> StrArrays3(int n)
        {
            var ret1 = new string[n];
            var ret2 = new string[n];
            var ret3 = new string[n];
            for (int i = 0; i < n; i++)
            {
                ret1[i] = ReadStr;
                ret2[i] = ReadStr;
            }

            return Tuple.Create(ret1, ret2, ret3);
        }

        public static Tuple<int[], int[], int[]> IntArrays3(int n, int offset1 = 0, int offset2 = 0, int offset3 = 0)
        {
            var ret1 = new int[n];
            var ret2 = new int[n];
            var ret3 = new int[n];
            for (int i = 0; i < n; i++)
            {
                ret1[i] = ReadInt + offset1;
                ret2[i] = ReadInt + offset2;
                ret3[i] = ReadInt + offset3;
            }

            return Tuple.Create(ret1, ret2, ret3);
        }

        public static Tuple<long[], long[], long[]> LongArrays3(int n, long offset1 = 0, long offset2 = 0,
            long offset3 = 0)
        {
            var ret1 = new long[n];
            var ret2 = new long[n];
            var ret3 = new long[n];
            for (int i = 0; i < n; i++)
            {
                ret1[i] = ReadLong + offset1;
                ret2[i] = ReadLong + offset2;
                ret3[i] = ReadLong + offset3;
            }

            return Tuple.Create(ret1, ret2, ret3);
        }

        static bool TypeEquals<T, U>() => typeof(T) == typeof(U);
        static T ChangeType<T, U>(U a) => (T) System.Convert.ChangeType(a, typeof(T));

        static T Convert<T>(string s) => TypeEquals<T, int>() ? ChangeType<T, int>(int.Parse(s))
            : TypeEquals<T, long>() ? ChangeType<T, long>(long.Parse(s))
            : TypeEquals<T, double>() ? ChangeType<T, double>(double.Parse(s))
            : TypeEquals<T, char>() ? ChangeType<T, char>(s[0])
            : ChangeType<T, string>(s);

        public static bool In<T>(out T a)
        {
            try
            {
                a = Convert<T>(Read);
                return true;
            }
            catch
            {
                a = default(T);
                return false;
            }
        }

        public static bool In<T, U>(out T a, out U b)
        {
            try
            {
                var ar = StrArray(2);
                a = Convert<T>(ar[0]);
                b = Convert<U>(ar[1]);
                return true;
            }
            catch
            {
                a = default(T);
                b = default(U);
                return false;
            }
        }

        public static bool In<T, U, V>(out T a, out U b, out V c)
        {
            try
            {
                var ar = StrArray(3);
                a = Convert<T>(ar[0]);
                b = Convert<U>(ar[1]);
                c = Convert<V>(ar[2]);
                return true;
            }
            catch
            {
                a = default(T);
                b = default(U);
                c = default(V);
                return false;
            }
        }

        public static bool In<T, U, V, W>(out T a, out U b, out V c, out W d)
        {
            try
            {
                var ar = StrArray(4);
                a = Convert<T>(ar[0]);
                b = Convert<U>(ar[1]);
                c = Convert<V>(ar[2]);
                d = Convert<W>(ar[3]);
                return true;
            }
            catch
            {
                a = default(T);
                b = default(U);
                c = default(V);
                d = default(W);
                return false;
            }
        }

        public static bool In<T, U, V, W, X>(out T a, out U b, out V c, out W d, out X e)
        {
            try
            {
                var ar = StrArray(5);
                a = Convert<T>(ar[0]);
                b = Convert<U>(ar[1]);
                c = Convert<V>(ar[2]);
                d = Convert<W>(ar[3]);
                e = Convert<X>(ar[4]);
                return true;
            }
            catch
            {
                a = default(T);
                b = default(U);
                c = default(V);
                d = default(W);
                e = default(X);
                return false;
            }
        }
    }

    public static class Output
    {
        public static void print<T>(T t) => Console.WriteLine(t);
        public static void print(params object[] o) => Console.WriteLine(o.Join(" "));

        public static void PrintBool(bool val, string yes = "Yes", string no = "No")
            => Console.WriteLine(val ? yes : no);
        public static void PrintYn(bool val) => PrintBool(val);
        public static void PrintYN(bool val) => PrintBool(val, "YES", "NO");
        public static void PrintPossible(bool val) => PrintBool(val, "Possible", "Impossible");
        public static void PrintYay(bool val) => PrintBool(val, "Yay!", ":(");
        public static void PrintDebug(params object[] args)
            => Console.Error.WriteLine(string.Join(" ", args));
        
        /// <summary>
        /// setter で設定された値を標準出力に出力します。
        /// </summary>
        public static object cout
        {
            set
            {
                Console.WriteLine(value);
            }
        }

        /// <summary>
        /// Local環境のみ，setter で設定された値を標準出力に出力します。
        /// </summary>
        public static object dout
        {
            set
            {
                #if LOCAL
                Console.WriteLine(value);
                #endif
            }
        }
        
        /// <summary>
        /// setter で設定された値を標準エラー出力に出力します。
        /// </summary>
        public static object cerr
        {
            set
            {
                Console.Error.WriteLine(value);
            }
        }

        public const string endl = "\n";
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var sw = new StreamWriter(Console.OpenStandardOutput()) {AutoFlush = false};
            Console.SetOut(sw);
            new Solver().Main();

            Console.Out.Flush();
            Console.Read();
        }
    }

    [DebuggerDisplay("({first}, {second})")]
    public class Pair<T1, T2> : IComparable<Pair<T1, T2>>, IEquatable<Pair<T1, T2>>
        where T1 : IComparable<T1>
        where T2 : IComparable<T2>
    {
        public Pair(T1 first, T2 second)
        {
            this.first = first;
            this.second = second;
        }

        public T1 first;
        public T2 second;

        public int CompareTo(Pair<T1, T2> other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            var firstComparison = first.CompareTo(other.first);
            return firstComparison != 0 ? firstComparison : second.CompareTo(other.second);
        }

        public override string ToString() => $"({first}, {second})";

        public bool Equals(Pair<T1, T2> other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return EqualityComparer<T1>.Default.Equals(first, other.first) &&
                   EqualityComparer<T2>.Default.Equals(second, other.second);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Pair<T1, T2>) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (EqualityComparer<T1>.Default.GetHashCode(first) * 397) ^
                       EqualityComparer<T2>.Default.GetHashCode(second);
            }
        }
    }

    public class vector<T> : IEnumerable<T>
    {
        private List<T> _list;

        public vector() : this(0)
        {
        }

        public vector(int n, T value = default(T))
        {
            _list = new List<T>(n);
            for(int i = 0; i < n; i++) _list.Add(value);
        }
        public vector(IEnumerable<T> x)
        {
            _list = new List<T>();
            _list.AddRange(x);
        }
        public void Add(T value)
        {
            _list.Add(value);
        }
        public void PushBack(T value) => Add(value);
        public void Resize(int newsize)
        {
            var newList = new List<T>(newsize);
            newList.AddRange(_list);
            for(int i = 0; i < newsize - newList.Count; i++)
                newList.Add(default(T));
            _list = newList;
        }
        public T this[int index] => _list[index];
        public T At(int index) => this[index];
        public void PopBack() => _list.RemoveAt(_list.Count - 1);
        public void Insert(int position, T value) => _list.Insert(position, value);
        public void Erase(int position) => _list.RemoveAt(position);
        public void Erase(int first, int last) => _list.RemoveRange(first, last - first);
        public void Reserve(int capacity) => _list.Capacity = capacity;
        public void ShrinkToFit() => _list.Capacity = _list.Count;
        public IEnumerator<T> GetEnumerator() => _list.GetEnumerator();
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        public static implicit operator vector<T>(T[] array) => new vector<T>(array);
        public static implicit operator vector<T>(List<T> list) => new vector<T>(list);
    }
    
    #endregion
    
}
 