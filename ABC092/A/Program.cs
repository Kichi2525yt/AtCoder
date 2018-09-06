using System;

namespace A
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = int.Parse(Console.ReadLine());
            var b = int.Parse(Console.ReadLine());
            var c = int.Parse(Console.ReadLine());
            var d = int.Parse(Console.ReadLine());
            Console.WriteLine(Math.Min(a, b) + Math.Min(c, d));
        }
    }
}
