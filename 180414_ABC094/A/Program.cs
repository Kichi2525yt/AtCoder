using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace A
{
    public class Program
    {
        public static void Main(string[] args) {
            var input = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int a = input[0], b = input[1], x = input[2];
            Console.WriteLine(a + b >= x && a <= x ? "YES" : "NO");
        }
    }
}
