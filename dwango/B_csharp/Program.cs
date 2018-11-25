using System;
using System.Collections.Generic;
using System.Diagnostics;
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

        private void Solve()
        {
            var N = cin.Int;
            var K = cin.Int;
            var a = cin.LongArray(N);

            //
            var ruiseki = new long[N + 1];
            for (int i = 0; i < N; i++) {
                ruiseki[i+1] = ruiseki[i] + a[i];
            }
            
            
            var b = new List<long>();
            
            for (int l = 0; l < N; l++)
            {
                for (int r = l; r < N; r++)
                {
                    //l～rの合計
                    b.Add(ruiseki[r+1] - ruiseki[l]);
                }
            }

            
            Console.WriteLine(0);
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

        static Pair<T, U> make_pair<T, U>(T v1, U v2) where T : IComparable<T> where U : IComparable<U> => new Pair<T, U>(v1, v2);
    }

    public static class Util
    {

        public static IEnumerable<IEnumerable<T>> Comb<T>(this IEnumerable<T> items, int r)
        {
            if (r == 0) {
                yield return Enumerable.Empty<T>();
            } else {
                var i = 1;
                var l = items.ToList();
                foreach (var x in l) {
                    var xs = l.Skip(i);
                    foreach (var c in Comb(xs, r - 1))
                        yield return c.Before(x);

                    i++;
                }
            }
        }


        public static IEnumerable<T> Before<T>(this IEnumerable<T> items, T first)
        {
            yield return first;

            foreach (var i in items)
                yield return i;
        }

        public static long Product(this IEnumerable<long> enu)
        {
            return enu.Aggregate((a, b) => a & b);
        }


        public static long[] CombAndProduct(this IEnumerable<long> items, int r)
        {
            return items.Comb(r).Select(x => x.Product()).ToArray();
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

    static class Permutation<T>
    {
        private static void Search(ICollection<T[]> perms, Stack<T> stack, T[] a)
        {
            int N = a.Length;
            if (N == 0) {
                perms.Add(stack.Reverse().ToArray());
            } else {
                var b = new T[N - 1];
                Array.Copy(a, 1, b, 0, N - 1);
                for (int i = 0; i < a.Length; ++i) {
                    stack.Push(a[i]);
                    Search(perms, stack, b);
                    if (i < b.Length) { b[i] = a[i]; }
                    stack.Pop();
                }
            }
        }
        public static IEnumerable<T[]> All(IEnumerable<T> src)
        {
            var perms = new List<T[]>();
            Search(perms, new Stack<T>(), src.ToArray());
            return perms;
        }
    }

    class Pair<T, U> : IComparable<Pair<T, U>> where T : IComparable<T> where U : IComparable<U>
    {
        public T v1;
        public U v2;
        public Pair(T v1, U v2)
        {
            this.v1 = v1;
            this.v2 = v2;
        }
        public int CompareTo(Pair<T, U> a) => v1.CompareTo(a.v1) != 0 ? v1.CompareTo(a.v1) : v2.CompareTo(a.v2);
        public override string ToString() => v1 + " " + v2;
    }

}
