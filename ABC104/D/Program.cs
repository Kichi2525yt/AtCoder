using System;
using System.Linq;

namespace D
{
    public class Program
    {
        public static void Main(string[] args)
        {
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
