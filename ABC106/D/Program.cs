using System;
using System.Collections.Generic;
using System.Linq;

namespace C
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
            var M = cin.ReadInt;
            var Q = cin.ReadInt;
            var trains = new List<Train>();
            for (var i = 0; i < M; i++)
            {
                trains.Add(new Train(cin.ReadInt, cin.ReadInt));
            }

            var q = new List<Train>();
            for (var i = 0; i < Q; i++)
            {
                q.Add(new Train(cin.ReadInt, cin.ReadInt));
            }


        }

        struct Train
        {
            public Train(int l, int r)
            {
                this.l = l;
                this.r = r;
            }
            public int l;
            public int r;

            public bool Equals(Train obj)
            {
                return obj.l == this.l && obj.r == this.r;
            }
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

