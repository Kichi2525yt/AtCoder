using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using static System.Math;
using static Library.Methods;
// ReSharper disable LoopCanBeConvertedToQuery
// ReSharper disable JoinDeclarationAndInitializer
// ReSharper disable MemberCanBeMadeStatic.Local
// ReSharper disable PossibleNullReferenceException
// ReSharper disable UnusedMember.Global
// ReSharper disable UnusedMember.Local
// ReSharper disable ArrangeTypeMemberModifiers
// ReSharper disable SuggestVarOrType_BuiltInTypes
// ReSharper disable SuggestVarOrType_Elsewhere
// ReSharper disable InvertIf
// ReSharper disable InconsistentNaming
// ReSharper disable ConvertIfStatementToSwitchStatement
// ReSharper disable UseObjectOrCollectionInitializer
// ReSharper disable TailRecursiveCall
// ReSharper disable RedundantUsingDirective
// ReSharper disable InlineOutVariableDeclaration
// ReSharper disable FunctionRecursiveOnAllPaths
#pragma warning disable

namespace Library
{
    class Treap<T> where T : IComparable
    {
        private class Node
        {
            private static Random g_rand = new Random();
            private readonly int m_rank = g_rand.Next();
            private readonly T m_value;
            private Node m_lch;
            private Node m_rch;
            private int m_count;

            public Node(T value)
            {
                m_value = value;
                m_count = 1;
            }

            private static int Count(Node node)
            {
                return (node != null) ? node.m_count : 0;
            }

            private Node Update()
            {
                m_count = Count(m_lch) + Count(m_rch) + 1;
                return this;
            }

            public static Node Merge(Node a, Node b)
            {
                if (a == null) { return b; }
                if (b == null) { return a; }

                if (a.m_rank < b.m_rank)
                {
                    a.m_rch = Merge(a.m_rch, b);
                    return a.Update();
                }
                else
                {
                    b.m_lch = Merge(a, b.m_lch);
                    return b.Update();
                }
            }

            public static Tuple<Node, Node> Split(Node t, int k)
            {
                if (t == null) { return new Tuple<Node, Node>(null, null); }

                if (k <= Count(t.m_lch))
                {
                    var pair = Split(t.m_lch, k);
                    t.m_lch = pair.Item2;
                    return new Tuple<Node, Node>(pair.Item1, t.Update());
                }
                else
                {
                    var pair = Split(t.m_rch, k - Count(t.m_lch) - 1);
                    t.m_rch = pair.Item1;
                    return new Tuple<Node, Node>(t.Update(), pair.Item2);
                }
            }

            public int FindIndex(T value)
            {
                int L = Count(m_lch);
                if (value.CompareTo(m_value) < 0)
                    return m_lch?.FindIndex(value) ?? 0;

                if (value.CompareTo(m_value) > 0)
                    return m_rch?.FindIndex(value) + L + 1 ?? L + 1;

                return L;
            }

            public T this[int i]
            {
                get {
                    int L = Count(m_lch);
                    if (i < L)
                        return m_lch[i];

                    return i > L ? m_rch[i - L - 1] : m_value;
                }
            }
        }

        private Node node;

        public void Insert(T value)
        {
            if (node != null)
            {
                int k = node.FindIndex(value);
                var pair = Node.Split(node, k);
                node = Node.Merge(Node.Merge(pair.Item1, new Node(value)), pair.Item2);
            }
            else
            {
                node = new Node(value);
            }
        }

        public int FindIndex(T value)
        {
            return node.FindIndex(value);
        }

        public T this[int i] => node[i];
    }
    public class Tree
    {
        private readonly int _N;
        private readonly List<int>[] _graph;

        public Tree(int N)
        {
            _N = N;
            _graph = Enumerable.Repeat((List<int>)null, N).Select(x => new List<int>()).ToArray();
        }

        public void AddEdge(int a, int b)
        {
            if (a >= _N || b >= _N) throw new Exception("頂点の値を超えた数です。");
            _graph[a].Add(b);
            _graph[b].Add(a);
        }

        public bool IsConnected(int A, int B) => _graph[A].Contains(B);
        public IEnumerable<int> EnumrateEdgesFromA(int A) => _graph[A];
    }


    /// <summary>
    /// Union-Find 木の集合を表します。
    /// </summary>
    public class UnionFind
    {
        //親の場合は ( 集合のサイズ × -1 )
        private readonly int[] _parent;

        public UnionFind(int count)
        {
            _parent = Enumerable.Repeat(-1, count).ToArray();
        }

        /// <summary>すべての根を列挙します。</summary>
        public IEnumerable<int> AllParents => 
            _parent.Select((x, i) => make_pair(x, i))
            .Where(x => x.first < 0)
            .Select(x => x.second);

        /// <summary>xの木のサイズ(要素数)を返します。</summary>
        public int Size(int x) => -_parent[Find(x)];

        /// <summary>xの根を返します。</summary>
        public int Find(int x) => _parent[x] < 0 ? x : _parent[x] = Find(_parent[x]);

        /// <summary>xとyの木を併合します。</summary>
        /// <returns>併合に成功したかどうか。(falseの場合は既に同じ木)</returns>
        public bool Connect(int x, int y)
        {
            int rx = Find(x), ry = Find(y);
            if (rx == ry)
                return false;
            if (Size(rx) > Size(ry))
                Swap(ref rx, ref ry);
            _parent[rx] += _parent[ry];
            _parent[ry] = rx;

            return true;
        }

        /// <summary>xとyの根が同じかどうかを返します。/summary>
        public bool Same(int x, int y) => Find(x) == Find(y);
    }



    /// <inheritdoc />
    /// <summary>
    /// 常にソートされた配列
    /// </summary>
    /// <typeparam name="T">要素の型</typeparam>
    /// <remarks>
    /// 検索: O(log N)
    /// 挿入, 削除: O(N)
    /// </remarks>
    public class SortedArray<T>
        : IEnumerable<T>
      where T : IComparable<T>
    {
        readonly List<T> buffer;

        public SortedArray() : this(256) { }

        public SortedArray(int capacity)
        {
            this.buffer = new List<T>(capacity);
        }

        /// <summary>
        /// 要素の挿入。
        /// </summary>
        /// <param name="elem">挿入する要素</param>
        public void Insert(T elem)
        {
            if (this.buffer.Count == 0)
            {
                this.buffer.Add(elem);
                return;
            }

            int r = this.buffer.Count - 1;
            int l = 0;
            int comp;
            while (l < r)
            {
                int m = (r + l) >> 1;
                comp = this.buffer[m].CompareTo(elem);
                if (comp > 0) r = m - 1;
                else if (comp < 0) l = m + 1;
                else return; // 重複不可
            }

            comp = this.buffer[l].CompareTo(elem);
            if (comp < 0)
                this.buffer.Insert(l + 1, elem);
            else if (comp > 0)
                this.buffer.Insert(l, elem);
        }

        /// <summary>
        /// 要素の検索。
        /// </summary>
        /// <param name="elem">検索する要素</param>
        /// <returns>要素の位置（見つからなかった場合、配列長）</returns>
        public int IndexOf(T elem)
        {
            if (this.buffer.Count == 0)
                return 0;

            int r = this.buffer.Count - 1;
            int l = 0;
            while (l < r)
            {
                int m = (r + l) >> 1;
                int comp = this.buffer[m].CompareTo(elem);
                if (comp > 0) r = m - 1;
                else if (comp < 0) l = m + 1;
                else return m;
            }

            return this.buffer[l].CompareTo(elem) == 0 ? l : this.buffer.Count;
        }

        /// <summary>
        /// テーブル中に要素が含まれているかどうか判別。
        /// </summary>
        /// <param name="elem">探したい要素</param>
        /// <returns>含まれていれば true</returns>
        public bool Contains(T elem)
        {
            return this.IndexOf(elem) != this.buffer.Count;
        }

        /// <summary>
        /// テーブル中の要素を検索。
        /// </summary>
        /// <param name="elem">探したい要素</param>
        /// <returns>含まれていればその要素を、なければ default(T)</returns>
        public T Find(T elem)
        {
            int i = this.IndexOf(elem);
            if (i == this.buffer.Count)
                return default(T);
            return this.buffer[i];
        }

        /// <summary>
        /// 要素の削除。
        /// </summary>
        /// <param name="elem">削除する要素</param>
        public void Erase(T elem)
        {
            int i = this.IndexOf(elem);
            if (i < this.buffer.Count)
                this.buffer.RemoveAt(i);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var t in this.buffer)
                yield return t;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
    /// <summary>
    /// 優先度付きキュー
    /// </summary>
    /// <typeparam name="T">要素の型</typeparam>
    [System.Diagnostics.DebuggerDisplay("Count = {_buffer.Count}")]
    public class PriorityQueue<T>
      where T : IComparable<T>
    {
        private readonly List<T> _buffer;
        private readonly IComparer<T> _comparer;
        public PriorityQueue()
        {
            this._buffer = new List<T>();
            _comparer = Comparer<T>.Default;
        }

        public PriorityQueue(int capacity)
        {
            this._buffer = new List<T>(capacity);
            _comparer = Comparer<T>.Default;
        }

        public PriorityQueue(int capacity, Comparison<T> comparison)
        {
            this._buffer = new List<T>(capacity);
            _comparer = Comparer<T>.Create(comparison);
        }

        public PriorityQueue(Comparison<T> comparison)
        {
            this._buffer = new List<T>();
            _comparer = Comparer<T>.Create(comparison);
        }

        static void PushHeap(IList<T> array, T elem, IComparer<T> comparer)
        {
            int n = array.Count;
            array.Add(elem);

            while (n != 0)
            {
                int i = (n - 1) / 2;
                if (comparer.Compare(array[n], array[i]) > 0)
                {
                    var tmp = array[n];
                    array[n] = array[i];
                    array[i] = tmp;
                }
                n = i;
            }
        }

        static void PopHeap(IList<T> array, IComparer<T> comparer)
        {
            int n = array.Count - 1;
            array[0] = array[n];
            array.RemoveAt(n);

            for (int i = 0, j; (j = 2 * i + 1) < n;)
            {
                if (j != n - 1 && array[j].CompareTo(array[j + 1]) < 0)
                    j++;
                if (comparer.Compare(array[i], array[j]) < 0)
                {
                    var tmp = array[j];
                    array[j] = array[i];
                    array[i] = tmp;
                }
                i = j;
            }
        }


        /// <summary>
        /// 要素のプッシュ。
        /// </summary>
        /// <param name="elem">挿入したい要素</param>
        public void Push(T elem)
        {
            PushHeap(this._buffer, elem, _comparer);
        }

        /// <summary>
        /// 要素を1つポップ。
        /// </summary>
        /// <remarks>
        /// 今回の実装では、先頭要素の読み出しと削除は別に行う。
        /// この Pop では削除のみ。
        /// 読み出しには Top プロパティを使う。
        /// </remarks>
        public void Pop()
        {
            PopHeap(this._buffer, _comparer);
        }

        /// <summary>
        /// 先頭要素の読み出し。
        /// </summary>
        public T Top => this._buffer[0];

        public int Count => this._buffer.Count;
    }

    public class Map<TKey, TValue> : IDictionary<TKey, TValue>
    {
        public Map()
        {
            _dictionary = new SortedDictionary<TKey, TValue>();
        }

        public Map(IDictionary<TKey, TValue> dictionary)
        {
            _dictionary = new SortedDictionary<TKey, TValue>(dictionary);
        }

        public Map(IDictionary<TKey, TValue> dictionary, IComparer<TKey> comparer)
        {
            _dictionary = new SortedDictionary<TKey, TValue>(dictionary, comparer);
        }

        public Map(IComparer<TKey> comparer)
        {
            _dictionary = new SortedDictionary<TKey, TValue>(comparer);
        }


        private readonly SortedDictionary<TKey, TValue> _dictionary;
        public IEnumerator<KeyValuePair<TKey, TValue>> GetEnumerator()
        {
            return _dictionary.GetEnumerator();
        }

        public void Add(KeyValuePair<TKey, TValue> item)
        {
            _dictionary.Add(item.Key, item.Value);
        }

        public void Clear()
        {
            _dictionary.Clear();
        }

        public bool Contains(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Contains(item);
        }

        public void CopyTo(KeyValuePair<TKey, TValue>[] array, int arrayIndex)
        {
            array = _dictionary.Skip(arrayIndex).ToArray();
        }

        public bool Remove(KeyValuePair<TKey, TValue> item)
        {
            return _dictionary.Remove(item.Key);
        }

        public int Count => _dictionary.Count;
        public bool IsReadOnly => false;

        public bool ContainsKey(TKey key)
        {
            return _dictionary.ContainsKey(key);
        }

        public void Add(TKey key, TValue value)
        {
            _dictionary.Add(key, value);
        }

        public bool Remove(TKey key)
        {
            return _dictionary.Remove(key);
        }

        public bool TryGetValue(TKey key, out TValue value)
        {
            return TryGetValue(key, out value);
        }

        public TValue this[TKey key]
        {
            get {
                if (!_dictionary.ContainsKey(key))
                {
                    _dictionary.Add(key, default(TValue));
                    return default(TValue);
                }

                return _dictionary[key];
            }
            set {
                if (!_dictionary.ContainsKey(key))
                {
                    _dictionary.Add(key, value);
                }
                else
                {
                    _dictionary[key] = value;
                }
            }
        }

        public ICollection<TKey> Keys => _dictionary.Keys;
        public ICollection<TValue> Values => _dictionary.Values;
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public static class Trans_Methods
    {
        public static Trans Maketrans(string a, string b) => new Trans(a, b);
        public static Trans Maketrans(string a, string b, string c) => new Trans(a, b, c);

        public static string Translate(this string str, Trans trans)
        {
            var s = str;
            for (int i = 0; i < trans.Before.Length; i++)
            {
                if (trans.Delete.Contains(trans.Before[i]))
                    continue;
                s = s.Replace(trans.Before[i], trans.After[i]);
            }

            return s;
        }

    }

    public class Trans
    {
        public Trans(string a, string b)
        {
            if (a.Length != b.Length)
                throw new ArgumentException($"引数 {nameof(a)}と{nameof(b)}は同じ長さである必要があります。");

            Before = a.ToCharArray();
            After = b.ToCharArray();
            Delete = new char[0];
        }

        public Trans(string a, string b, string c) : this(a, b)
        {
            Delete = c.ToCharArray();
        }

        public char[] Before { get; }
        public char[] After { get; }
        public char[] Delete { get; }
    }

    //既にあるテンプレ
    #region 

    public static class Methods
    {

        /// <summary>aとbをスワップする</summary>
        public static void Swap<T>(ref T a, ref T b) where T : struct
        {
            var tmp = b;
            b = a;
            a = tmp;
        }

        /// <summary>aとbの最大公約数を求める</summary>
        public static long Gcd(long a, long b)
        {
            if (a < b)
                Swap(ref a, ref b);
            return a % b == 0 ? b : Gcd(b, a % b);
        }

        /// <summary>aとbの最小公倍数を求める</summary>
        public static long Lcm(long a, long b) => a / Gcd(a, b) * b;

        public static Pair<T1, T2> make_pair<T1, T2>(T1 f, T2 l) => new Pair<T1, T2>(f, l);


        /// <summary>
        /// 指定した値以上の先頭のインデクスを返す
        /// </summary>
        /// <typeparam name="T">比較する値の型</typeparam>
        /// <param name="arr">対象の配列（※ソート済みであること）</param>
        /// <param name="start">開始インデクス [inclusive]</param>
        /// <param name="end">終了インデクス [exclusive]</param>
        /// <param name="value">検索する値</param>
        /// <param name="comparer">比較関数(インターフェイス)</param>
        /// <returns>指定した値以上の先頭のインデクス</returns>
        public static int LowerBound<T>(T[] arr, int start, int end, T value, IComparer<T> comparer)
        {
            int low = start;
            int high = end;
            while (low < high)
            {
                var mid = ((high - low) >> 1) + low;
                if (comparer.Compare(arr[mid], value) < 0)
                    low = mid + 1;
                else
                    high = mid;
            }
            return low;
        }

        /// <summary>
        /// 指定した値以上の先頭のインデクスを返す
        /// </summary>
        /// <typeparam name="T">比較する値の型</typeparam>
        /// <param name="arr">対象の配列（※ソート済みであること）</param>
        /// <param name="value">検索する値</param>
        /// <returns>指定した値以上の先頭のインデクス</returns>
        public static int LowerBound<T>(T[] arr, T value) where T : IComparable
        {
            return LowerBound(arr, 0, arr.Length, value, Comparer<T>.Default);
        }

        /// <summary>
        /// 指定した値より大きい先頭のインデクスを返す
        /// </summary>
        /// <typeparam name="T">比較する値の型</typeparam>
        /// <param name="arr">対象の配列（※ソート済みであること）</param>
        /// <param name="start">開始インデクス [inclusive]</param>
        /// <param name="end">終了インデクス [exclusive]</param>
        /// <param name="value">検索する値</param>
        /// <param name="comparer">比較関数(インターフェイス)</param>
        /// <returns>指定した値より大きい先頭のインデクス</returns>
        public static int UpperBound<T>(T[] arr, int start, int end, T value, IComparer<T> comparer)
        {
            int low = start;
            int high = end;
            while (low < high)
            {
                var mid = ((high - low) >> 1) + low;
                if (comparer.Compare(arr[mid], value) <= 0)
                    low = mid + 1;
                else
                    high = mid;
            }
            return low;
        }

        /// <summary>
        /// 指定した値より大きい先頭のインデクスを返す
        /// </summary>
        /// <typeparam name="T">比較する値の型</typeparam>
        /// <param name="arr">対象の配列（※ソート済みであること）</param>
        /// <param name="value">検索する値</param>
        /// <returns>指定した値より大きい先頭のインデクス</returns>
        public static int UpperBound<T>(T[] arr, T value)
        {
            return UpperBound(arr, 0, arr.Length, value, Comparer<T>.Default);
        }

        public static bool IsEven(this int x) => x % 2 == 0;
        public static bool IsOdd(this int x) => x % 2 != 0;
        public static bool IsEven(this long x) => x % 2 == 0;
        public static bool IsOdd(this long x) => x % 2 != 0;
        public static double Log2(double x) => Log(x, 2);

        public static T Min<T>(params T[] col) => col.Min();
        public static T Max<T>(params T[] col) => col.Max();

    }
    public class Pair<T1, T2>
    {
        public Pair(T1 first, T2 second)
        {
            this.first = first;
            this.second = second;
        }

        public T1 first;
        public T2 second;
    }
    #endregion
}
