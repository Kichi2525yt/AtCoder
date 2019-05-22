using System;
using System.Linq;

namespace A
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int a = input[0], b = input[1], c = input[2];
            var k = int.Parse(Console.ReadLine());
            if (a >= b & a >= c)
                for (int i = 0; i < k; i++)
                    a *= 2;
                
            else if (b >= c)
                for (int i = 0; i < k; i++)
                    b *= 2;
            else
                for (int i = 0; i < k; i++)
                    c *= 2;

            Console.WriteLine(a + b + c);
        }
    }
}
