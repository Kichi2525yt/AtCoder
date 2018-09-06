using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Program
{
    public static void Main(string[] args)
    {
        Console.WriteLine(Run(Console.ReadLine()));
    }
    public static string Run(string s)
    {
        var ab = Array.ConvertAll(s.Split(' '), int.Parse);
        return Math.Ceiling((ab[0] + ab[1]) / 2f).ToString();
    }
}
