using System;
using System.Diagnostics;
using System.Linq;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = Console.ReadLine();
            var t = Console.ReadLine();
            Console.WriteLine(Run(s, t));
        }

        static string Run(string s, string t)
        {
            s = new string(s.OrderBy(x => x).ToArray());
            t = new string(t.OrderByDescending(x => x).ToArray());
            Debug.WriteLine(s);
            Debug.WriteLine(t);
            var flag = string.Compare(s, t) < 0;
            Debug.WriteLine(flag);
            return flag ? "Yes" : "No";
        }
    }
}
