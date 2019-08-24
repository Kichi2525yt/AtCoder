using System;

namespace Solve
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
                    new Solver().Main();
                }
                catch (FormatException)
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("入力に失敗しました。");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                catch (ArgumentOutOfRangeException) { }

                Console.WriteLine("---------------");
            }
        }
    }
}
