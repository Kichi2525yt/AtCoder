using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace D
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            var n = input[0];
            var m = input[1];
            var a = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();

            var map = new Dictionary<int, int>();
            var sum = new int[n + 1];
            sum[0] = 0;
            for (int i = 0; i < n; i++)
            {
                sum[i] = (sum[i] + a[i]) % m;
            }
            
            long result = 0;
            foreach (var i in sum)
            {
                if(!map.ContainsKey(i))
                    map.Add(i, 0);
                result += map[i];
                map[i]++;
            }
            
            Console.WriteLine(result);
        }
    }
}
