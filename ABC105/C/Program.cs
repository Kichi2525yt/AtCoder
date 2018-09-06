using System;

namespace C
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            if (n == 0)
            {
                Console.WriteLine(0);
                return;
            }

            var ans = "";
            while (n != 0)
            {
                if (n % 2 == 0)
                    ans = "0" + ans;
                else
                {
                    n--;
                    ans = "1" + ans;
                }

                n /= -2;
            }

            Console.WriteLine(ans);
        }
    }
}

