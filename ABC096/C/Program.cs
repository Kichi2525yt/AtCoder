using System;
using System.Linq;

namespace C
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int h = input[0], w = input[1];

            var s = new bool[h][];
            var a = new bool[h][];
            for (int i = 0; i < h; i++) {
                s[i] = Console.ReadLine().Select(x => x == '#').ToArray();
                a[i] = new bool[w];
            }


            for (int i = 0; i < h; i++) {
                for (int j = 0; j < w; j++) {
                    if (!s[i][j]) continue;
                    //左
                    if (j - 1 != -1 && s[i][j - 1])
                        a[i][j] = a[i][j - 1] = true;
                    //右
                    if (j + 1 != w && s[i][j + 1])
                        a[i][j] = a[i][j + 1] = true;
                    //上
                    if (i - 1 != -1 && s[i - 1][j])
                        a[i][j] = a[i - 1][j] = true;
                    //下
                    if (i + 1 != h && s[i + 1][j])
                        a[i][j] = a[i + 1][j] = true;
                    
                }
            }
            
            Console.WriteLine(s.Where((x, i) => x.SequenceEqual(a[i])).Any() ? "Yes" : "No");
        }
    }
}
