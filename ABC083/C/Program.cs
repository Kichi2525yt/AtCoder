using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C
{
    class Program
    {
        static void Main(string[] args)
        {
            

            var list = new List<string>();
            list.Add("A");
            list.Add("B");
            list.Add("C");
            list.Remove("B");

            Console.WriteLine(list[2]);




        }
    }
}
