using System;
using System.Linq;

namespace B
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int n = input[0], m = input[1] + 1, x = input[2];
            var a = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            var answer1 = 0;
            for (var i = x; i < n;)
                if (a.Contains(++i))
                    answer1++;

            var answer2 = 0;
            for (var i = x; i > 0;)
                if (a.Contains(--i))
                    answer2++;
            Console.WriteLine(Math.Min(answer1, answer2));
        }
    }
}
