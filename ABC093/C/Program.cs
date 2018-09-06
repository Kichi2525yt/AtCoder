using System;
using System.Linq;

namespace C
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var abc = Console.ReadLine().Split(' ');
            int a = int.Parse(abc[0]), b = int.Parse(abc[1]), c = int.Parse(abc[2]);
            int i = 0;
            while (!IsEndable(a, b, c)) {
                操作(ref a, ref b, ref c);
                i++;
            }

            Console.WriteLine(i);
        }

        static void 操作(ref int a, ref int b, ref int c) {
            int min = Min(a, b, c), max = Max(a, b, c);
            var flag = false;

            if (a == min)
                flag = a + 2 <= Min(b, c);
            else if (b == min)
                flag = b + 2 <= Min(a, c);
            else {
                flag = c + 2 <= Min(a, b);
            }
            if(flag) 操作2(ref a, ref b, ref c);
            else 操作1(ref a, ref b, ref c);
        }


        static void 操作1(ref int a, ref int b, ref int c) {
            var min = Min(a, b, c);
            if (a == min) {
                a++;
                if (b <= c) b++;
                else c++;
            } else if (b == min) {
                b++;
                if (a <= c) a++;
                else c++;
            } else if (c == min) {
                c++;
                if (a <= b) a++;
                else b++;
            }
        }

        static void 操作2(ref int a, ref int b, ref int c) {
            var min = Min(a, b, c);
            if (a == min)
                a += 2;
            else if (b == min)
                b += 2;
            else
                c += 2;
        }



        static bool IsEndable(int a, int b, int c) => a == b && b == c;

        static int Min(params int[] values) => values.Min();
        static int Max(params int[] values) => values.Max();
    }
}
