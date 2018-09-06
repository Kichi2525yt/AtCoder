using System;
using System.Linq;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            var s = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            Console.WriteLine(Enumerable.Range(1, s[0]).Where(x => {
                var s_ = Sum(x);
                return s_ >= s[1] && s_ <= s[2];
            }).Sum());
        }

        static int Sum(int i)
        {
            var sum = 0;
            foreach(var c in i.ToString()) 
                sum += int.Parse(new string (new [] { c }));
            return sum;
        }
    }
}
