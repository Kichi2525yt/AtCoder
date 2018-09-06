using System;
using System.Collections.Generic;
using static System.Math;


namespace C
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());
            var a = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);

            var list = new List<int> { 0 };
            list.AddRange(a);
            list.Add(0);

            var sum = 0L;
            for (int i = 1; i <= list.Count - 1; i++) sum += Abs(list[i] - list[i - 1]);
            for (int i = 1; i <= list.Count - 2; i++) {
                int prev = list[i - 1], current = list[i], next = list[i + 1];

                int num1 = Abs(prev - current), num2 = Abs(current - next), num3 = Abs(prev - next);

                Console.WriteLine(sum - num1 - num2 + num3);
            }
        }
    }
}