using System;
using System.Collections.Generic;
using System.Linq;

namespace B
{
    public class Program
    {
        public static void Main(string[] args) {
            var abk = Console.ReadLine().Split(' ');
            int a = int.Parse(abk[0]), b = int.Parse(abk[1]), k = int.Parse(abk[2]);

            var list = new List<int>();
            for (int i = a; i < a + k && i < b; i++)
                list.Add(i);

            for (int i = Math.Max(a, b - k + 1); i <= b; i++)
                list.Add(i);

            foreach (var v in list.Distinct().OrderBy(x => x))
                Console.WriteLine(v);

        }
    }
}
