using System;
using System.Linq;

namespace B
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var s = Console.ReadLine();
            var flag = false;
            if (s[0] == 'A')
            {
                flag = true;
                var countedC = false;
                var ok = false;
                for (int i = 0; i < s.Length; i++)
                {
                    if (i < 2 || i > s.Length - 2)
                    {
                        if (s[i] == 'C')
                        {
                            ok = false;
                            break;
                        }

                        continue;
                    }

                    if (s[i] != 'C') continue;
                    if (countedC)
                    {
                        ok = false;
                        break;
                    }

                    ok = true;
                    countedC = true;
                }

                if (countedC && ok)
                {
                    foreach (var ss in "ABDEFGHIJKLMNOPQRSTUVWXYZ")
                    {
                        if (s.Skip(1).Contains(ss))
                            flag = false;
                    }
                }
                else
                {
                    flag = false;
                }
            }

            Console.WriteLine(flag ? "AC" : "WA");
        }
    }
}
