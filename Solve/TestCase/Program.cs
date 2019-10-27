using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TestCase
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Solve(1,2,new[]{(0,2)},1,new[]{(0,2)},1,new[]{1}));
            Console.ReadLine();
            const int start = 2, end = 10;
            var rand = new Random();
            for (int test = start; test <= end; test++)
            {
                int N = rand.Next(1, 2001);
                int H = rand.Next(2, 3001);
                int A = rand.Next(1, 1001);
                int B = rand.Next(1, 1001);
                

                var xy = new (int X, int Y)[N];
                var st = new (int s, int t)[A];
                var M = new int[B];
                for (int i = 0; i < N; i++)
                {
                    int x = rand.Next(0, H);
                    int y = rand.Next(x + 1, H + 1);
                    xy[i] = (x, y);
                }

                for (int i = 0; i < A; i++)
                {
                    int s = rand.Next(-1, H);
                    int t = rand.Next(s + 1, H + 1);
                    st[i] = (s, t);
                }

                st[0] = (0, H - 1);

                
                var cus = new int[H + 1];
                for (int i = 0; i < N; i++)
                {
                    cus[xy[i].X]++;
                    cus[xy[i].Y]--;
                }
            
                var set = new SortedSet<int>();
                for (int i = 1; i < H; i++)
                {
                    set.Add(cus[i] += cus[i - 1]);
                }
                
                
                for (int i = 0; i < B; i++)
                {
                    M[i] = rand.Next(1, set.Count + 1);
                }

                string inputFileName = $"customers\\input\\input{test:00}.txt",
                    outputFileName = $"customers\\output\\output{test:00}.txt";
                var inputText = FormatInput(N, H, xy, A, st, B, M);
                var outputText = Solve(N, H, xy, A, st, B, M);
                using var input = new StreamWriter(inputFileName);
                using var output = new StreamWriter(outputFileName);
                input.Write(inputText);
                output.Write(outputText);
            }
            
        }

        public static string FormatInput(int N, int H, (int X, int Y)[] XY, int A, (int s, int t)[] st, int B, int[] M)
        {
            var cus = new int[H + 1];
            for (int i = 0; i < N; i++)
            {
                cus[XY[i].X]++;
                cus[XY[i].Y]--;
            }
            
            var set = new SortedSet<int>();
            for (int i = 1; i <= H; i++)
            {
                set.Add(cus[i] += cus[i - 1]);
            }
            
            var check = new List<bool>
            {
                1 <= N && N <= 200000,
                1 <= A && A <= 100000,
                1 <= B && B <= 100000,
                2 <= H && H <= 300000,
                XY.All(tuple => 0 <= tuple.X && tuple.X < tuple.Y && tuple.Y <= H),
                st.All(tuple => -1 <= tuple.s && tuple.s < tuple.t && tuple.t <= H),
                XY.Length == N,
                st.Length == A,
                M.Length == B,
                set.Count >= M.Max()
            };


            if (check.Any(x => !x))
            {
                throw new Exception("Assert Failed.");
            }

            var ret = new StringBuilder();
            ret.AppendLine($"{N} {H}");
            foreach (var s in XY.Select(tuple => $"{tuple.X} {tuple.Y}"))
                ret.AppendLine(s);
            

            ret.AppendLine($"{A}");
            foreach (var s in st.Select(tuple => $"{tuple.s} {tuple.t}"))
                ret.AppendLine(s);

            ret.AppendLine($"{B}");
            ret.AppendLine(string.Join(" ", M));
            return ret.ToString();

        }

        public static string Solve(int N, int H, (int X, int Y)[] XY, int A, (int s, int t)[] st, int B, int[] M)
        {
            var ret = new StringBuilder();
            var memo = new int[H + 1];
            int[] ins = new int[H + 1], outs = new int[H + 1];
            for (int i = 0; i < N; i++)
            {
                memo[XY[i].X]++;
                memo[XY[i].Y]--;
                ins[XY[i].X]++;
                outs[XY[i].Y]++;
            }

            var customers = new int[H];
            customers[0] = memo[0];
            var set = new SortedSet<int>{memo[0]};
            var dict = new Dictionary<int, List<int>>
            {
                {memo[0], new List<int> {0}}
            };
            for (int i = 1; i < H; i++)
            {
                set.Add(customers[i] = customers[i - 1] + memo[i]);
                if(!dict.ContainsKey(customers[i])) dict.Add(customers[i], new List<int>());
                dict[customers[i]].Add(i);
            }
            
            for (int i = 1; i < H+1; i++)
            {
                ins[i] += ins[i - 1];
                outs[i] += outs[i - 1];
            }

            // 順位
            var counts = set.ToArray();
            Array.Sort(counts);
            Array.Reverse(counts);

            // クエリ処理 A
            for (int i = 0; i < A; i++)
            {
                var (s, t) = st[i];
                int p = ins[t];
                if (s != -1) p -= ins[s];
                int q = outs[t];
                if (s != -1) q -= outs[s];
                
                ret.AppendLine($"{p} {q}");
            }

            for (int i = 0; i < B; i++)
            {
                var value = counts[M[i] - 1];
                ret.AppendLine($"{value} {dict[value].Count} {string.Join(" ", dict[value])}");
            }
            
            
            return ret.ToString();
        }
    }
}