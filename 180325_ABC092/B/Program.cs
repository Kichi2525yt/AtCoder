using System;

namespace B
{
    class Program
    {
        static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine()); //人数
            var dx = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            //日数, 余り
            int d = dx[0], x = dx[1]; 
            var a = new int[n];
            for (int i = 0; i < n; i++) {
                a[i] = int.Parse(Console.ReadLine());
            }

            var answer = x;
            foreach(var i in a) {
                int j = 1;
                do {
                    answer++;
                    j += i;
                } while (j <= d);
            }
            Console.WriteLine(answer);
        }
    }
}
