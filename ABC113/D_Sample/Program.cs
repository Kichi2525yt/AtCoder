using System;
using System.IO;

namespace D_Sample
{
    class Program
    {
        static void Main(string[] args)
        {
            var rand = new Random();
            const int Y_MAX = 1000000000;

            var N = rand.Next(1, 100000);
            var M = 100000;

            using (var f = File.CreateText("sample.txt"))
            {
                var s = $"{N} {M}";
                Console.WriteLine("0" + ":\t" + s);
                f.WriteLine(s);
                for (int i = 0; i < M; ++i)
                {
                    s = $"{rand.Next(1, N)} {rand.Next(1, Y_MAX)}";
                    Console.WriteLine(i + ":\t" + s);
                    f.WriteLine(s);
                }
            }


        }
    }
}
