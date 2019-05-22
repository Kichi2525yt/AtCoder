using System;

namespace B
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var n = int.Parse(Console.ReadLine());

            while (n > 3)
            {
                if (n % 7 == 0)
                {
                    Console.WriteLine("Yes");
                    return;
                }

                n -= 4;
            }

            Console.WriteLine(n == 0 ? "Yes" : "No");
        }
    }
}
