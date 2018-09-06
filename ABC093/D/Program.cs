using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace D
{
    class Program
    {
        static void Main(string[] args) {
            var q = int.Parse(Console.ReadLine());
            var abInput = new List<int[]>();
            for (int i = 0; i < q; i++) {
                abInput.Add(Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse));
            }

            foreach (var ab in abInput) {
                int a = ab[0], b = ab[1];

            }

        }
    }
}
