using System;

namespace B
{
    class Program
    {
        static void Main(string[] args) {

            var ab = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            var s = Console.ReadLine();
            int a, b;
            Console.WriteLine(
                int.TryParse(s.Substring(0, ab[0]), out a) &&
                s[ab[0]] == '-' &&
                int.TryParse(s.Substring(ab[0] + 1, ab[1]), out b) 
                    ? "Yes" : "No");
        }
    }
}
