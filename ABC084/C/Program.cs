using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C
{
    class Program
    {
        static void Main(string[] args) {
            var n = int.Parse(Console.ReadLine());
            var csf = new ValueTuple<int, int, int>[n];
            for (int i = 0; i < n; i++) {
                var csf_ = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
                csf[i] = ValueTuple.Create(csf_[0], csf_[1], csf_[2]);
            }

            foreach (var csf1 in csf) {


            }
            
        }
    }
}
