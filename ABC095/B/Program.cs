using System;
using System.Linq;

namespace B
{
    public class Program
    {
        public static void Main(string[] args) {
            var input = Console.ReadLine().Split(' ');
            int n = int.Parse(input[0]), x = int.Parse(input[1]);
            var m = new int[n];
            for (var i = 0; i < n; i++) m[i] = int.Parse(Console.ReadLine());
            
            Console.WriteLine((x - m.Sum()) / m.Min() + n);
        }
    }
}
