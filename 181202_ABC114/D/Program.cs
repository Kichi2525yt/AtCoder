using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
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

        private Dictionary<int, int[]> prime;
        private int[] p;

        private void Solve()
        {
            prime = new Dictionary<int, int[]>
            {
                {2, new[] {2}},
                {3, new[] {3}},
                {4, new[] {2, 2}},
                {5, new[] {5}},
                {6, new[] {2, 3}},
                {7, new[] {7}},
                {8, new[] {2, 2, 2}},
                {9, new[] {3, 3}},
                {10, new[] {2, 5}},
                {11, new[] {11}},
                {12, new[] {2, 2, 3}},
                {13, new[] {13}},
                {14, new[] {2, 7}},
                {15, new[] {3, 5}},
                {16, new[] {2, 2, 2, 2}},
                {17, new[] {17}},
                {18, new[] {2, 3, 3}},
                {19, new[] {19}},
                {20, new[] {2, 2, 5}},
                {21, new[] {3, 7}},
                {22, new[] {2, 11}},
                {23, new[] {23}},
                {24, new[] {2, 2, 2, 3}},
                {25, new[] {5, 5}},
                {26, new[] {2, 13}},
                {27, new[] {3, 3, 3}},
                {28, new[] {2, 2, 7}},
                {29, new[] {29}},
                {30, new[] {2, 3, 5}},
                {31, new[] {31}},
                {32, new[] {2, 2, 2, 2, 2}},
                {33, new[] {3, 11}},
                {34, new[] {2, 17}},
                {35, new[] {5, 7}},
                {36, new[] {2, 2, 3, 3}},
                {37, new[] {37}},
                {38, new[] {2, 19}},
                {39, new[] {3, 13}},
                {40, new[] {2, 2, 2, 5}},
                {41, new[] {41}},
                {42, new[] {2, 3, 7}},
                {43, new[] {43}},
                {44, new[] {2, 2, 11}},
                {45, new[] {3, 3, 5}},
                {46, new[] {2, 23}},
                {47, new[] {47}},
                {48, new[] {2, 2, 2, 2, 3}},
                {49, new[] {7, 7}},
                {50, new[] {2, 5, 5}},
                {51, new[] {3, 17}},
                {52, new[] {2, 2, 13}},
                {53, new[] {53}},
                {54, new[] {2, 3, 3, 3}},
                {55, new[] {5, 11}},
                {56, new[] {2, 2, 2, 7}},
                {57, new[] {3, 19}},
                {58, new[] {2, 29}},
                {59, new[] {59}},
                {60, new[] {2, 2, 3, 5}},
                {61, new[] {61}},
                {62, new[] {2, 31}},
                {63, new[] {3, 3, 7}},
                {64, new[] {2, 2, 2, 2, 2, 2}},
                {65, new[] {5, 13}},
                {66, new[] {2, 3, 11}},
                {67, new[] {67}},
                {68, new[] {2, 2, 17}},
                {69, new[] {3, 23}},
                {70, new[] {2, 5, 7}},
                {71, new[] {71}},
                {72, new[] {2, 2, 2, 3, 3}},
                {73, new[] {73}},
                {74, new[] {2, 37}},
                {75, new[] {3, 5, 5}},
                {76, new[] {2, 2, 19}},
                {77, new[] {11, 7}},
                {78, new[] {2, 3, 13}},
                {79, new[] {79}},
                {80, new[] {2, 2, 2, 2, 5}},
                {81, new[] {3, 3, 3, 3}},
                {82, new[] {2, 41}},
                {83, new[] {83}},
                {84, new[] {2, 2, 3, 7}},
                {85, new[] {5, 17}},
                {86, new[] {2, 43}},
                {87, new[] {3, 29}},
                {88, new[] {2, 2, 2, 11}},
                {89, new[] {89}},
                {90, new[] {2, 3, 3, 5}},
                {91, new[] {13, 7}},
                {92, new[] {2, 2, 23}},
                {93, new[] {3, 31}},
                {94, new[] {2, 47}},
                {95, new[] {5, 19}},
                {96, new[] {2, 2, 2, 2, 2, 3}},
                {97, new[] {97}},
                {98, new[] {2, 7, 7}},
                {99, new[] {3, 3, 11}},
                {100, new[] {2, 2, 5, 5}},
            };

            p = new[]
            {
                2, 3, 5, 7, 11, 13, 17, 19, 23, 29, 31, 37, 41, 43,
                47, 53, 59, 61, 67, 71, 73, 79, 83, 89, 97
            };

            //100までの数を素因数分解して出力する
            var N = cin.Int;

            //N!の素因数
            //最大値:97

            //list = N! の素因数分解した数
            var list = new List<int>();
            for (int i = 2; i <= N; i++)
            {
                list.AddRange(prime[i]);
            }

            var dict = p.ToDictionary(i => i, i => list.Count(x => x == i));

            //!N の約数の個数は、 dict.Sum(x => x.Value) - dict.Count(x => x != 0);
            Console.WriteLine(dict.Sum(x => x.Value) - dict.Count(x => x.Value != 0));
            //約数が57個かどうかを判定
        }


        static long Gcd(long a, long b)
        {
            while (true)
            {
                if (a < b)
                {
                    var a1 = a;
                    a = b;
                    b = a1;
                    continue;
                }

                if (b > 0)
                {
                    var a1 = a;
                    a = b;
                    b = a1 % b;
                    continue;
                }

                return a;
            }
        }

        static long Lcm(long a, long b)
        {
            return a / Gcd(a, b) * b;
        }

        
        public static IEnumerable<IEnumerable<T>> Perm<T>(IEnumerable<T> items, int? k = null)
        {
            if (k == null)
                k = items.Count();

            if (k == 0)
            {
                yield return Enumerable.Empty<T>();
            }
            else
            {
                var i = 0;
                foreach (var x in items)
                {
                    var xs = items.Where((_, index) => i != index);
                    foreach (var c in Perm(xs, k - 1))
                        yield return Before(c, x);

                    i++;
                }
            }
        }

// 要素をシーケンスに追加するユーティリティ
        public static IEnumerable<T> Before<T>(IEnumerable<T> items, T first)
        {
            yield return first;

            foreach (var i in items)
                yield return i;
        }
    }



    public class PrimeFactor
    {

        public IEnumerable<long> Enumerate(long n)
        {
            while (n > 1)
            {
                long factor = GetFactor(n);
                yield return factor;
                n = n / factor;
            }
        }

        private long GetFactor(long n, int seed = 1)
        {
            while (true)
            {
                if (n % 2 == 0) return 2;
                if (IsPrime(n)) return n;
                long x = 2;
                long y = 2;
                long d = 1;
                long count = 0;
                while (d == 1)
                {
                    count++;
                    x = f(x, n, seed);
                    y = f(f(y, n, seed), n, seed);
                    d = Gcd(Math.Abs(x - y), n);
                }

                if (d == n)
                    // 見つからなかった、乱数発生のシードを変えて再挑戦。
                {
                    seed = seed + 1;
                    continue;
                }

                // 素数でない可能性もあるので、再度呼び出す
                n = d;
                seed = 1;
            }
        }

        private readonly int[] seeds = new int[] {3, 5, 7, 11, 13, 17};

        private long f(long x, long n, int seed)
        {
            return (seeds[seed % 6] * x + seed) % n;
        }

        private static long Gcd(long a, long b)
        {
            while (true)
            {
                if (a < b)
                {
                    var a1 = a;
                    a = b;
                    b = a1;
                    continue;
                }

                if (b == 0) return a;
                long d;
                do
                {
                    d = a % b;
                    a = b;
                    b = d;
                } while (d != 0);

                return a;
            }
        }

        // 効率は良くないが、これでも十分な速度がでたので、良しとする。
        private static bool IsPrime(long number)
        {
            long boundary = (long) Floor(Sqrt(number));

            if (number == 1)
                return false;
            if (number == 2)
                return true;

            for (long i = 2; i <= boundary; ++i)
            {
                if (number % i == 0)
                    return false;
            }

            return true;
        }
    }

#pragma warning disable IDE1006 // 命名スタイル
    public static class cin
#pragma warning restore IDE1006 // 命名スタイル
    {
        private static readonly char _separator = ' ';
        private static readonly Queue<string> _input = new Queue<string>();


        public static string ReadLine => Console.ReadLine();

        public static string Str => Read;
        public static string Read
        {
            get {
                if (_input.Count != 0) return _input.Dequeue();

                // ReSharper disable once PossibleNullReferenceException
                var tmp = Console.ReadLine().Split(_separator);
                foreach (var val in tmp) {
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
        static T ChangeType<T, U>(U a) => (T)System.Convert.ChangeType(a, typeof(T));
        static T Convert<T>(string s) => TypeEquals<T, int>() ? ChangeType<T, int>(int.Parse(s))
            : TypeEquals<T, long>() ? ChangeType<T, long>(long.Parse(s))
            : TypeEquals<T, double>() ? ChangeType<T, double>(double.Parse(s))
            : TypeEquals<T, char>() ? ChangeType<T, char>(s[0])
            : ChangeType<T, string>(s);

        static void Multi<T>(out T a) => a = Convert<T>(Str);
        static void Multi<T, U>(out T a, out U b)
        {
            var ar = StrArray(2); a = Convert<T>(ar[0]); b = Convert<U>(ar[1]);
        }
        static void Multi<T, U, V>(out T a, out U b, out V c)
        {
            var ar = StrArray(3); a = Convert<T>(ar[0]); b = Convert<U>(ar[1]); c = Convert<V>(ar[2]);
        }
    }
}
