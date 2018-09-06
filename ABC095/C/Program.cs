using System;
using System.Collections.Generic;
using System.Linq;

namespace C
{
    public class Program
    {
        public static void Main(string[] args) {
            var abcxy = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            int a = abcxy[0], b = abcxy[1], c = abcxy[2], 
                x = abcxy[3], y = abcxy[4];
            
            //個数の大きい方・小さい方
            int max = Math.Max(x, y), min = Math.Min(x, y);
            var answers = new List<int> {
                //普通に
                a * x + b * y,
                //ABを少ない分、足りないのを買い足し
                c * 2 * min + (x < y ? b : a) * max - min,
                //全部ABで
                c * 2 * max
            };
            Console.WriteLine(answers.Min());
        }
    }
}
