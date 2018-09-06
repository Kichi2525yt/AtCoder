using System;

namespace A
{
    class Program
    {
        static void Main(string[] args) {
            var abc = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            Console.WriteLine(abc[0] + abc[1] >= abc[2] ? "Yes" : "No");
        }
    }
}