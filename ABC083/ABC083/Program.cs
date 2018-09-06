using System;

namespace A
{
    class Program
    {
        static void Main(string[] args)
        {

            var abcd = Array.ConvertAll(Console.ReadLine().Split(' '), int.Parse);
            var left = abcd[0] + abcd[1];
            var right = abcd[2] + abcd[3];
            if(left > right)
                Console.WriteLine("Left");
            else if(left < right)
                Console.WriteLine("Right");
            else
                Console.WriteLine("Balanced");

        }
    }
}
