using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable InconsistentNaming
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable SuggestVarOrType_BuiltInTypes

namespace B_CS
{
    public class Program
    {
        static Stopwatch sw = new Stopwatch();
        public static void Main(string[] args)
        {
            sw.Start();
            new Program().Solve(new Input(Console.In));
            Console.Read();
        }

        void Solve(Input cin)
        {
            var N = cin.ReadInt;
            var M = cin.ReadInt;

            var ccc = new List<City>();
            var P = new int[M];
            var Y = new int[M];

            //各市を(入出力順<i>, 県<P[i]>, 誕生した年<Y[i]>)として入力
            var cities = new List<City>();
            for (int i = 0; i < M; i++)
            {
                cities.Add(new City(i, P[i] = cin.ReadInt, Y[i] = cin.ReadInt));
            }
            
            //(入出力順, 識別番号)の連想配列
            var answers = new Dictionary<int, string>();
            //(県, [City])としてまとめる→県ごとに処理
            foreach (var x in cities.ToLookup(x => x.Pref))
            {
                var i = 0;
                //県内の市を誕生年順に並び替え→市ごとに処理
                foreach (var city in x.OrderBy(z => z.Year))
                {
                    //(入出力順, 市の識別番号)としてanswersに追加
                    answers.Add(city.No, $"{city.Pref:000000}{++i:000000}");
                }
            }


            Console.WriteLine(
                //各行を改行コードでつなげる
                string.Join("\r\n",
                    //answersを出力順に並び替え、そのうち識別番号のみを使用
                    answers.OrderBy(x => x.Key).Select(y => y.Value)
                )
            );
        }

        struct City
        {
            public City(int no, int pref, int year)
            {
                this.No = no;
                this.Pref = pref;
                this.Year = year;
            }

            public readonly int No; //入力した順
            public readonly int Pref; //県No
            public readonly int Year; //市の誕生年
        }
    }

    public class Input
    {
        private readonly System.IO.TextReader _stream;
        private readonly char _separator;
        private readonly Queue<string> _input;

        public Input(System.IO.TextReader reader, char separator = ' ')
        {
            this._separator = separator;
            this._stream = reader;
            this._input = new Queue<string>();
        }

        public string Read
        {
            get {
                if (this._input.Count != 0) return this._input.Dequeue();

                // ReSharper disable once PossibleNullReferenceException
                var tmp = this._stream.ReadLine().Split(this._separator);
                foreach (var val in tmp) {
                    this._input.Enqueue(val);
                }

                return this._input.Dequeue();
            }
        }

        public string ReadLine => _stream.ReadLine();

        public int ReadInt => int.Parse(Read);

        public long ReadLong => long.Parse(Read);

        public double ReadDouble => double.Parse(Read);

        public string[] ReadStrArray(long n)
        {
            var ret = new string[n];
            for (long i = 0; i < n; ++i) ret[i] = Read;
            return ret;
        }

        public int[] ReadIntArray(long n)
        {
            var ret = new int[n];
            for (long i = 0; i < n; ++i) ret[i] = ReadInt;
            return ret;
        }

        public long[] ReadLongArray(long n)
        {
            var ret = new long[n];
            for (long i = 0; i < n; ++i) ret[i] = ReadLong;
            return ret;
        }
    }
}