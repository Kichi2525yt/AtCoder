using System;
using System.Linq;

namespace A
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var i = int.Parse(Console.ReadLine());
            if (i < 1200) Console.WriteLine("ABC");
            else if(i < 2800) Console.WriteLine("ARC");
            else Console.WriteLine("AGC");
        }
    }

    public static class Input
    {
        public static string ReadString() => Console.ReadLine();
        public static string[] ReadStringAsArray = Console.ReadLine().Split(' ');
        public static int ReadInt() => int.Parse(Console.ReadLine());
        public static int[] ReadIntAsArray() => Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
    }
}
