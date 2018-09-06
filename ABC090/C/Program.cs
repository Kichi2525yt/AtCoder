using System;

namespace C
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine(Run(Console.ReadLine()));
        }
        public static int Run(string s)
        {
            var nm = Array.ConvertAll(s.Split(), int.Parse);
            return (nm[0] - 2) * (nm[1] - 2);
        }
    }
}
