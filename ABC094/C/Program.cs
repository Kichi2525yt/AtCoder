using System;
using System.Collections.Generic;
using System.Linq;

namespace C
{
    public class Program
    {
        public static void Main(string[] args) {
            var n = int.Parse(Console.ReadLine());
            var x = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            var count = (n / 2) - 1;
            
            for (int i = 0; i < n; i++) {
                var _1 = x.Take(i);
                var _2 = x.Skip(i+1);
                var ar = _1.Concat(_2).OrderByDescending(a => a);
                Console.WriteLine(ar.ElementAt(count));
            }
        }
    }
}
