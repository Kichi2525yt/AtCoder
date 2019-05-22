using System;
using System.Linq;

namespace A
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var n = input[0];
            var k = input[1];
            Console.WriteLine(n % k == 0 ? 0 : 1);
        }
    }
}
