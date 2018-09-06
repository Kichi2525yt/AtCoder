using System;
using System.Linq;

namespace A
{
    class Program
    {
        static void Main(string[] args) { 
            Console.WriteLine(700 + 100 * Console.ReadLine().Count(x => x == 'o'));
        }
    }
}
