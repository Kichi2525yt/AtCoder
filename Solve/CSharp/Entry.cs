using System;

namespace AtCoder
{
    public class Entry
    {
        public static void Main()
        {
            Console.ForegroundColor = ConsoleColor.White;
            while (true)
            {
                try
                {
                    new Solver().Solve();
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("入力に失敗しました。");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                Console.WriteLine("---------------");
            }
        }
    }
}
