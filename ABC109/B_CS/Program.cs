using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace B_CS
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Solve(new Input(Console.In));
        }

        void Solve(Input cin)
        {
            var N = cin.ReadInt;
            var W = cin.ReadStrArray(N);
            var list = new List<string>();

            
            foreach (var s in W)
            {
                if (list.Contains(s))
                {
                    Console.WriteLine("No");
                    return;
                }

                list.Add(s);
            }

            var last = W[0].Last();
            foreach (var s in W.Skip(1))
            {
                if (s[0] != last)
                {
                    Console.WriteLine("No");
                    return;
                }

                last = s.Last();
            }

            Console.WriteLine("Yes");
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
