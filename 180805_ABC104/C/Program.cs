using System;
using System.Collections.Generic;
using System.Linq;

namespace C
{
    public class Program
    {
        static Dictionary<int, Obj> dict = new Dictionary<int, Obj>();
        static int points = 0;
        static List<int> counts = new List<int>();
        static int G;
        public static void Main(string[] args)
        {
            var iarray = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
            int D = iarray[0];
            G = iarray[1];
            for (int i = 0; i < D; i++)
            {
                var ar = Console.ReadLine().Split(' ').Select(int.Parse).ToArray();
                dict.Add(i + 1, new Obj(ar[0], ar[1]));
            }

            IEnumerable<int[]> order;

            var digits = Enumerable.Range(0, D).ToArray();
            switch (D)
            {
                case 1:
                    counts.Add(Function(dict.First()));
                    break;
                case 2:
                    order =
                        from a in digits
                        from b in digits.Except(new[] { a })
                        select new List<int> { a, b }.ToArray();
                    Count(order);
                    break;
                case 3:
                    order =
                        from a in digits
                        from b in digits.Except(new[] { a })
                        from c in digits.Except(new[] { a, b })
                        select new List<int> { a, b, c }.ToArray();
                    Count(order);
                    break;
                case 4:
                    order =
                        from a in digits
                        from b in digits.Except(new[] {a})
                        from c in digits.Except(new[] {a, b})
                        from d in digits.Except(new[] { a, b, c })
                        select new List<int> {a, b, c, d}.ToArray();
                    Count(order);
                    break;
                case 5:
                    order =
                        from a in digits
                        from b in digits.Except(new[] { a })
                        from c in digits.Except(new[] { a, b })
                        from d in digits.Except(new[] { a, b, c })
                        from e in digits.Except(new[] { a, b, c, d })
                        select new List<int> { a, b, c, d, e }.ToArray();
                    Count(order);
                    break;
                case 6:
                    order =
                        from a in digits
                        from b in digits.Except(new[] { a })
                        from c in digits.Except(new[] { a, b })
                        from d in digits.Except(new[] { a, b, c })
                        from e in digits.Except(new[] { a, b, c, d })
                        from f in digits.Except(new[] { a, b, c, d, e })
                        select new List<int> { a, b, c, d, e, f }.ToArray();
                    Count(order);
                    break;
                case 7:
                    order =
                        from a in digits
                        from b in digits.Except(new[] { a })
                        from c in digits.Except(new[] { a, b })
                        from d in digits.Except(new[] { a, b, c })
                        from e in digits.Except(new[] { a, b, c, d })
                        from f in digits.Except(new[] { a, b, c, d, e })
                        from g in digits.Except(new[] { a, b, c, d, e, f })
                        select new List<int> { a, b, c, d, e, f, g }.ToArray();
                    Count(order);
                    break;
                case 8:
                    order =
                        from a in digits
                        from b in digits.Except(new[] { a })
                        from c in digits.Except(new[] { a, b })
                        from d in digits.Except(new[] { a, b, c })
                        from e in digits.Except(new[] { a, b, c, d })
                        from f in digits.Except(new[] { a, b, c, d, e })
                        from g in digits.Except(new[] { a, b, c, d, e, f })
                        from h in digits.Except(new[] { a, b, c, d, e, f, g })
                        select new List<int> { a, b, c, d, e, f, g, h }.ToArray();
                    Count(order);
                    break;
                case 9:
                    order =
                        from a in digits
                        from b in digits.Except(new[] { a })
                        from c in digits.Except(new[] { a, b })
                        from d in digits.Except(new[] { a, b, c })
                        from e in digits.Except(new[] { a, b, c, d })
                        from f in digits.Except(new[] { a, b, c, d, e })
                        from g in digits.Except(new[] { a, b, c, d, e, f })
                        from h in digits.Except(new[] { a, b, c, d, e, f, g })
                        from i in digits.Except(new[] { a, b, c, d, e, f, g, h })
                        select new List<int> { a, b, c, d, e, f, g, h, i }.ToArray();
                    Count(order);
                    break;
                case 10:
                    order =
                        from a in digits
                        from b in digits.Except(new[] { a })
                        from c in digits.Except(new[] { a, b })
                        from d in digits.Except(new[] { a, b, c })
                        from e in digits.Except(new[] { a, b, c, d })
                        from f in digits.Except(new[] { a, b, c, d, e })
                        from g in digits.Except(new[] { a, b, c, d, e, f })
                        from h in digits.Except(new[] { a, b, c, d, e, f, g })
                        from i in digits.Except(new[] { a, b, c, d, e, f, g, h })
                        from j in digits.Except(new[] { a, b, c, d, e, f, g, h, i})
                        select new List<int> { a, b, c, d, e, f, g, h, i, j }.ToArray();
                    Count(order);
                    break;
            }

            Console.WriteLine(counts.Min());
        }

        static void Count(IEnumerable<int[]> order)
        {
            foreach (var o in order) {
                var count = 0;
                foreach (var oo in o)
                    count += Function(dict.ElementAt(oo));
                counts.Add(count);
                points = 0;
            }
        }
        static int Function(KeyValuePair<int, Obj> a)
        {
            var count = 0;
            for (int i = 0; i < a.Value.count; i++) {
                if (points >= G) break;
                points += a.Key * 100;
                count++;
            }

            points += a.Value.bonus;
            return count;
        }

        public struct Obj
        {
            public Obj(int c, int b)
            {
                count = c;
                this.bonus = b;
            }

            public int count;
            public int bonus;
        }
    }
}

