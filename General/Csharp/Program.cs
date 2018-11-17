using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static System.Math;
// ReSharper disable MemberCanBeMadeStatic.Local
// ReSharper disable PossibleNullReferenceException
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable InvertIf
// ReSharper disable SwitchStatementMissingSomeCases

namespace Csharp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new Program().Solve(new Input(Console.In));
        }

        private void Solve(Input cin)
        {
            int W = cin.ReadInt,
                H = cin.ReadInt,
                N = cin.ReadInt;
            var rect = new bool[W][];
            for (int i = 0; i < W; i++)
            {
                rect[i] = new bool[H];
            }

            var xya = new XYA[N];
            for (int i = 0; i < N; i++)
            {
                xya[i] = new XYA(cin.ReadInt, cin.ReadInt, cin.ReadInt);
            }

            foreach (var aa in xya)
            {
                var x = aa.x;
                var y = aa.y;
                var a = aa.a;

                switch (a)
                {
                    case 1:
                        for (int i = 0; i < x; i++)
                        {
                            for (int j = 0; j < H; j++)
                            {
                                rect[i][j] = true;
                            }
                        }

                        break;
                    case 2:
                        for (int i = x; i < W; i++)
                        {
                            for (int j = 0; j < H; j++)
                            {
                                rect[i][j] = true;
                            }
                        }

                        break;

                    case 3:
                        for (int i = 0; i < W; i++)
                        {
                            for (int j = 0; j < y; j++)
                            {
                                rect[i][j] = true;
                            }
                        }

                        break;

                    case 4:
                        for (int i = 0; i < W; i++)
                        {
                            for (int j = y; j < H; j++)
                            {
                                rect[i][j] = true;
                            }
                        }

                        break;
                }

                for (int i = H - 1; i >= 0; i--)
                {
                    for (int j = 0; j < W; j++)
                    {
                        Debug.Write(rect[j][i] ? '#' : '.');
                    }
                }
            }

            Console.WriteLine(rect.Sum(y => y.Count(x => !x)));
            Console.Read();
        }

        class XYA
        {
            public XYA(int x, int y, int a)
            {
                this.x = x;
                this.y = y;
                this.a = a;
            }

            public int x;
            public int y;
            public int a;
        }
    }

    public class Input
    {
        private readonly TextReader _stream;
        private readonly char _separator;
        private readonly Queue<string> _input;

        public Input(TextReader reader, char separator = ' ')
        {
            this._separator = separator;
            this._stream = reader;
            this._input = new Queue<string>();
        }

        public string Read
        {
            get {
                if (this._input.Count != 0) return this._input.Dequeue();

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