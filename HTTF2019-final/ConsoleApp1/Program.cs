using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

namespace ConsoleApp1
{
    class Program
    {
        private int T, N, M;
        private List<Monster> Monsters;
        private List<Lev> Level;
        static void Main(string[] args)
        {
            new Program().Run();
            Console.Read();
        }

        void Run()
        {
            
            Monsters = new List<Monster>();
            Level = new List<Lev>
            {
                new Lev(),
                new Lev(),
                new Lev(),
                new Lev(),
                new Lev(),
                new Lev(),
                new Lev(),
                new Lev(),
                new Lev(),
                new Lev()
            };

            T = cin.Int; //1000
            N = cin.Int; //10
            M = cin.Int; //30000

            for (int j = 0; j < M; j++)
            {
                var a = cin.Int;
                var b = cin.Int;
                var c = cin.Long;
                var s = cin.IntArray(N);
                Monsters.Add(new Monster(a, b, c, s));
            }


            var stream_out =
                new StreamReader(@"C:\Users\kichi\Documents\develop\AtCoder\HTTF2019-final\Marason\bin\Debug\out.txt");

            var money = 1000L;
            var i = -1;
            while (stream_out.Peek() > -1)
            {
                var rl = stream_out.ReadLine();
                var output = rl.Split(' ').Select(int.Parse).ToArray();

                if(output[0] == 3)
                {
                    money += 1000;
                    Console.WriteLine($"[{i+1}] Part! money has increased to {money}.");
                }
                else if(output[0] == 2)
                {
                    //i+1 が現在のターン
                    if (Monsters[output[1] - 1].Start < i + 1 || Monsters[output[1] - 1].End > i + 1)
                    {
                        Console.WriteLine($"[{i+1}] Couldn't subjugation {output[1]}!");
                        continue;
                    }

                    var rew = Reward(i, output[1] - 1);
                    money += rew;
                    Console.WriteLine($"[{i+1}] Subjugation {output[1]}! money has increased by {rew}. Now {money}.");
                }
                else
                {
                    var req = 10000 * (long) Pow(2, Level[output[1]-1].Level + 1);
                    if (req > money)
                    {
                        Console.WriteLine($"[{i+1}] No Money to traning {output[1]}! Requies {req} but you have {money}.");
                        continue;
                    }

                    money -= req;
                    var more = Level[output[1] - 1].Traning();
                    var msg = more == 0 ? $"Level up to {Level[output[1]-1].Level}." : $"More {more} times for level up to {Level[output[1]-1].Level+1}.";
                    Console.WriteLine($"[{i+1}] Traning {output[1]}! {msg}");
                }
                i++;
            }

            Console.WriteLine($"Money: " +money);

        }

        
        private long Reward(int turn, int i)
        {
            var monster = Monsters[i];
            if (monster.Did) return 0;
            double add = monster.Reward;
            add *= 1 + 9 * (double) (turn - monster.Start) / (monster.End - monster.Start);
            int lack = 0;
            for (int j = 0; j < N; j++)
                lack += Max(0, monster.Requires[j] - Level[j].Level);
            if (lack == 0) add *= 10;
            else
            {
                add *= Pow(0.5, lack);
                add += 1e-9;
            }
            
            return (long) add;
        }
    }

    public class Lev
    {
        public int Level { get; set; }
        public int Count { get; set; }

        public int Traning()
        {
            Count++;
            var ret = Level+1 - Count;
            if (ret == 0)
            {
                Level++;
                Count = 0;
            }
            return ret;
        }
    }

#pragma warning disable IDE1006 // 命名スタイル
    public static class cin
#pragma warning restore IDE1006 // 命名スタイル
    {
        private static readonly char _separator = ' ';
        private static readonly Queue<string> _input = new Queue<string>();

        private static readonly StreamReader sr =
            new StreamReader(@"C:\Users\kichi\Documents\develop\AtCoder\HTTF2019-final\Marason\bin\Debug\in.txt");

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
}
