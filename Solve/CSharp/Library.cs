using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading;
using static System.Math;
using AtCoder;
using static AtCoder.Methods;
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

    /// <summary>
    /// Union-Find 木の集合を表します。
    /// </summary>
    [System.Diagnostics.DebuggerDisplay("Count of Parents = {" + nameof(ParentsCount) + "}")]
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
            _parent.Select(make_pair)
                .Where(x => x.first < 0)
                .Select(x => x.second);

        private int ParentsCount => _parent.Count(x => x < 0);

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

        /// <summary>根とその根に所属する葉をグループ化して返します。</summary>
        public ILookup<int, int> ToLookup()
            => this.EnumeratePairs().ToLookup(x => x.Key, x => x.Value);
        private IEnumerable<KeyValuePair<int, int>> EnumeratePairs()
        {
            for (int i = 0; i < _parent.Length; i++) yield return new KeyValuePair<int, int>(this.Find(i), i);
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

    /// <summary>
    /// SegmentTree 二分木 を表します。
    /// </summary>
    /// <typeparam name="T">要素の型</typeparam>
    public class SegmentTree<T> : IEnumerable<T>
    {
        private readonly int _treeHeight, _leafCount;
        private readonly T _defaultValue;
        private readonly T[] _seg;
        private readonly Func<T, T, T> _f;

        /// <summary>
        /// 要素数を表します。
        /// </summary>
        public int Count { get; }

        /// <summary>
        /// 指定した <see cref="index"/> の要素を評価して取得/更新して評価します。
        /// </summary>
        /// <remarks>計算量 O(log N)</remarks>
        /// <param name="index">要素の添字</param>
        /// <returns><see cref="index"/>の要素</returns>
        public T this[int index]
        {
            get {
                index += _leafCount;
                var ret = _seg[index];
                while ((index >>= 1) > 0)
                    ret = _f(ret, _seg[index]);
                return ret;
            }
            set { Update(index, index + 1, value); }
        }

        /// <summary>
        /// 配列を元に <see cref="SegmentTree{T}"/> 二分木を実装します。
        /// </summary>
        /// <remarks>時間計算量 O(N)</remarks>
        /// <param name="elems">元の要素配列</param>
        /// <param name="operatorFunc">操作 (モノイド)</param>
        /// <param name="defaultValue">モノイドの単位元</param>
        public SegmentTree(IReadOnlyCollection<T> elems, Func<T, T, T> operatorFunc, T defaultValue = default(T))
        {
            this.Count = elems.Count;
            _defaultValue = defaultValue;
            _f = operatorFunc;

            int treeHeight = 1, leafCount = 1;
            while (leafCount < elems.Count)
            {
                leafCount <<= 1;
                treeHeight++;
            }

            _treeHeight = treeHeight;
            _leafCount = leafCount;

            _seg = Enumerable.Repeat(_defaultValue, _leafCount)
                .Concat(elems)
                .Concat(Enumerable.Repeat(_defaultValue, _leafCount - this.Count))
                .ToArray();
            this.Build();
        }

        /// <summary>
        /// 要素数を元に <see cref="SegmentTree{T}"/> 二分木を実装します。
        /// </summary>
        /// <remarks>すべての要素は <see cref="defaultValue"/> で初期化されます。時間計算量 O(N)</remarks>
        /// <param name="N">要素数</param>
        /// <param name="operatorFunc">操作 (モノイド)</param>
        /// <param name="defaultValue">モノイドの単位元</param>
        public SegmentTree(int N, Func<T, T, T> operatorFunc, T defaultValue = default(T))
            : this(Enumerable.Repeat(defaultValue, N).ToArray(), operatorFunc, defaultValue) { }

        /// <summary>
        /// <see cref="SegmentTree{T}"/> を構築します。
        /// </summary>
        private void Build()
        {
            for (int i = this.Count - 1; i >= 0; i--)
            {
                _seg[i] = _f(_seg[2 * i], _seg[2 * i + 1]);
            }
        }

        /// <summary>
        /// 指定した区間 [l, r) に対する二項演算の結果を返します。
        /// </summary>
        /// <param name="l"><要素の左端 (lを含む)/param>
        /// <param name="r">要素の右端 (rを含まない)</param>
        /// <returns>指定した区間に対する二項演算の結果</returns>
        public T Query(int l, int r)
        {
            T lValue = _defaultValue, rValue = _defaultValue;
            l += _leafCount;
            r += _leafCount - 1;
            while (l <= r)
            {
                if (l % 2 == 1) lValue = _f(lValue, _seg[l++]);
                if (r % 2 == 0) rValue = _f(_seg[r--], rValue);
                l >>= 1;
                r >>= 1;
            }

            return _f(lValue, rValue);
        }

        public void Update(int l, int r, T x)
        {
            l += _leafCount;
            r += _leafCount - 1;
            while (l <= r)
            {
                if (l % 2 == 1) _seg[l] = _f(x, _seg[l++]);
                if (r % 2 == 0) _seg[r] = _f(x, _seg[r--]);
                l >>= 1;
                r >>= 1;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            for (int i = 0; i < this.Count; i++)
                yield return this[i];
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    //実装途中
    [Obsolete]
    class MultiSet<T> : ICollection<T>
    {
        private SortedDictionary<T, int> _dict;

        public MultiSet()
        {
            _dict = new SortedDictionary<T, int>();
            Count = 0;
        }

        public int Erase(T item, int count)
        {
            if (!_dict.ContainsKey(item)) return 0;
            if (_dict[item] >= count)
            {
                _dict[item] -= count;
                if (_dict[item] == 0) _dict.Remove(item);
                return count;
            }

            var cnt = _dict[item];
            _dict.Remove(item);
            return cnt;
        }

        public void EraseAll(T item, int count)
        {
            if (!_dict.ContainsKey(item)) return;
            this.Erase(item, _dict[item]);
        }


        public IEnumerator<T> GetEnumerator()
        {
            foreach (var kvp in _dict)
                for (int i = 0; i < kvp.Value; i++)
                    yield return kvp.Key;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(T item)
        {
            if (item == null) return;
            if(!_dict.ContainsKey(item))
                _dict.Add(item, 0);
            _dict[item]++;
        }

        public void Clear()
        {
            _dict = new SortedDictionary<T, int>();
            Count = 0;
        }

        public bool Contains(T item) => item != null && _dict.ContainsKey(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            throw new NotImplementedException();
        }

        public bool Remove(T item)
        {
            throw new NotImplementedException();
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; }
    }

    //階乗・組み合わせ（nCr）・順列（nPr）関連
    public struct Fact
    {
        //i!
        private readonly int[] _fact;
        //i!^-1
        private readonly int[] _inv;

        /// <summary>
        /// 1以上N以下の階乗を計算します。
        /// </summary>
        public Fact(int N)
        {
            var lastFact = ModInt.One;
            _fact = new int[N + 1];
            _fact[0] = 1;
            foreach (int i in Range(1, N + 1))
            {
                lastFact *= i;
                _fact[i] = lastFact.Value;
            }

            var lastInv = lastFact.Invert;
            _inv = new int[N + 1];
            _inv[N] = lastInv.Value;
            foreach (int i in RangeReverse(0, N))
            {
                lastInv *= i + 1;
                _inv[i] = lastInv.Value;
            }

            _inv[0] = _inv[1];
        }

        public int this[int i] => _fact[i];

        /// <summary>
        /// 組み合わせの数 nCr を求めます。
        /// </summary>
        /// <remarks>N以上の数で初期化している必要があります。</remarks>
        public int nCr(int n, int r)
        {
            if (n - r < 0) return 0;
            if (n >= _fact.Length) throw new ArgumentOutOfRangeException(nameof(n), "Fact 構造体のインスタンスをn以上で初期化している必要があります。");
            var ret = ModInt.One;
            // n! / (n-r)!r!

            ret *= _fact[n];
            //逆元でかける
            ret *= _inv[n - r];
            ret *= _inv[r];
            return ret.Value;
        }

        /// <summary>
        /// 順列の数 nPr を求めます。
        /// </summary>
        /// <remarks>N以上の数で初期化している必要があります。</remarks>
        public int nPr(int n, int r)
        {
            if (n - r < 0) return 0;
            if (n >= _fact.Length) throw new ArgumentOutOfRangeException(nameof(n), "Fact 構造体のインスタンスをn以上で初期化している必要があります。");
            var ret = ModInt.One;
            // n! / (n-r)!r!

            ret *= _fact[n];
            //逆元でかける
            ret *= _inv[n - r];
            return ret.Value;
        }
    }

    /// <summary>
    /// 最小全域木を求めるアルゴリズムです。
    /// </summary>
    public class Kruskal
    {
        /// <summary>
        /// グラフの辺を表します。
        /// </summary>
        public struct Edge : IComparable<Edge>
        {
            /// <summary>
            /// 辺の始点と終点およびコストを用いて辺を初期化します。
            /// </summary>
            /// <param name="f">始点</param>
            /// <param name="t">終点</param>
            /// <param name="c">コスト</param>
            public Edge(int f, int t, long c)
            {
                From = f;
                To = t;
                Cost = c;
            }
            /// <summary>
            /// 辺の始点
            /// </summary>
            public int From { get; }
            /// <summary>
            /// 辺の終点
            /// </summary>
            public int To { get; }
            /// <summary>
            /// 辺のコスト
            /// </summary>
            public long Cost { get; }

            /// <summary>
            /// この辺とほかの辺をコストで比較します。
            /// </summary>
            /// <param name="other">比較対象の辺</param>
            /// <returns>比較結果</returns>
            public int CompareTo(Edge other) => Cost.CompareTo(other.Cost);
        }

        private readonly UnionFind _uf;
        private readonly List<Edge> _edges, _result;
        /// <summary>
        /// 最小全域木が構築済みかどうかを取得します。
        /// </summary>
        public bool Built { get; private set; }

        /// <summary>
        /// 構築結果を取得します。
        /// </summary>
        public Edge[] Result
        {
            get
            {
                if (!this.Built) throw new InvalidOperationException("構築する前に結果を取得することはできません。");
                return _result.ToArray();
            }
        }

        /// <summary>
        /// 頂点の個数を指定して<see cref="Kruskal"/>クラスを初期化します。
        /// </summary>
        /// <param name="N">頂点の個数</param>
        public Kruskal(int N)
        {
            _uf = new UnionFind(N);
            _edges = new List<Edge>();
            _result = new List<Edge>();
        }

        /// <summary>
        /// 木に辺を追加します。
        /// </summary>
        /// <param name="x">辺の始点</param>
        /// <param name="y">辺の終点</param>
        /// <param name="cost">コスト</param>
        public void AddEdge(int x, int y, long cost) => AddEdge(new Edge(x, y, cost));

        /// <summary>
        /// 木に辺を追加します。
        /// </summary>
        /// <param name="e">追加する辺</param>
        public void AddEdge(Edge e)
        {
            if (this.Built)
                throw new InvalidOperationException("構築後に値を変更することはできません。");

            _edges.Add(e);
        }

        /// <summary>
        /// 最小全域木を構築します。
        /// </summary>
        public void Build()
        {
            if (this.Built) return;
            this.Built = true;
            _edges.Sort();
            foreach (var edge in _edges)
            {
                if (_uf.Same(edge.From, edge.To)) continue;
                _result.Add(edge);
                _uf.Connect(edge.From, edge.To);
            }
        }

    }

    /// <summary>
    /// 2点間の最小コストを求めるアルゴリズムです。
    /// </summary>
    /// <remarks>空間計算量 O(N^3)</remarks>
    public class WarshallFloyd
    {
        private readonly long[][] _costs;
        private int N;
        public bool Built { get; private set; }
        public WarshallFloyd(int N)
        {
            _costs = new long[N][];
            this.Built = false;
            for (int i = 0; i < N; i++)
            {
                _costs[i] = Enumerable.Repeat(long.MaxValue >> 2, N).ToArray();
            }
        }

        public void Build()
        {
            if (this.Built) return;
            this.Built = true;
            for (int k = 0; k < N; k++)
            for (int i = 0; i < N; i++)
            for (int j = 0; j < N; j++)
                chmin(ref _costs[i][j], _costs[i][k] + _costs[k][j]);
        }

        /// <summary>
        /// set: aからbへのコストを設定します。
        /// get: aからbへの最小コストを求めます。
        /// </summary>
        /// <returns>aからbへの最短距離</returns>
        public long this[int a, int b]
        {
            get
            {
                if (!this.Built)
                    throw new InvalidOperationException("構築後に取得する必要があります。");
                if (!a.IsIn(0, N)) throw new ArgumentOutOfRangeException($"引数 {nameof(a)} は 0 以上 N 未満である必要があります。");
                if (!b.IsIn(0, N)) throw new ArgumentOutOfRangeException($"引数 {nameof(b)} は 0 以上 N 未満である必要があります。");
                return _costs[a][b];
            }

            set
            {
                if (this.Built)
                    throw new InvalidOperationException("構築後に値を変更することはできません。");
                if (!a.IsIn(0, N)) throw new ArgumentOutOfRangeException($"引数 {nameof(a)} は 0 以上 N 未満である必要があります。");
                if (!b.IsIn(0, N)) throw new ArgumentOutOfRangeException($"引数 {nameof(b)} は 0 以上 N 未満である必要があります。");
                _costs[a][b] = value;
            }
        }


    }

    public class Treap<T> where T : IComparable
    {
        private class Node
        {
            private static readonly Random g_rand = new Random();
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
                return node?.m_count ?? 0;
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

                b.m_lch = Merge(a, b.m_lch);
                return b.Update();
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
        /// <summary>
        /// 要素を挿入する
        /// </summary>
        /// <param name="value">挿入する要素</param>
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
        /// <summary>
        /// valueのindexを返す
        /// </summary>
        /// <param name="value">探したい値</param>
        /// <returns>求めたindex</returns>
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


    public class Deque<T>
    {
        T[] buf;
        int offset, capacity;
        public int Count { get; private set; }

        public Deque(int cap) { buf = new T[capacity = cap]; }
        public Deque() { buf = new T[capacity = 16]; }
        public T this[int index]
        {
            get { return buf[getIndex(index)]; }
            set { buf[getIndex(index)] = value; }
        }
        private int getIndex(int index)
        {
            if (index >= capacity)
                throw new IndexOutOfRangeException("out of range");
            var ret = index + offset;
            if (ret >= capacity)
                ret -= capacity;
            return ret;
        }
        public void PushFront(T item)
        {
            if (Count == capacity) Extend();
            if (--offset < 0) offset += buf.Length;
            buf[offset] = item;
            ++Count;
        }
        public T PopFront()
        {
            if (Count == 0)
                throw new InvalidOperationException("collection is empty");
            --Count;
            var ret = buf[offset++];
            if (offset >= capacity) offset -= capacity;
            return ret;
        }
        public void PushBack(T item)
        {
            if (Count == capacity) Extend();
            var id = Count++ + offset;
            if (id >= capacity) id -= capacity;
            buf[id] = item;
        }
        public T PopBack()
        {
            if (Count == 0)
                throw new InvalidOperationException("collection is empty");
            return buf[getIndex(--Count)];
        }
        public void Insert(int index, T item)
        {
            if (index > Count) throw new IndexOutOfRangeException();
            this.PushFront(item);
            for (int i = 0; i < index; i++)
                this[i] = this[i + 1];
            this[index] = item;
        }
        public T RemoveAt(int index)
        {
            if (index < 0 || index >= Count) throw new IndexOutOfRangeException();
            var ret = this[index];
            for (int i = index; i > 0; i--)
                this[i] = this[i - 1];
            this.PopFront();
            return ret;
        }
        private void Extend()
        {
            T[] newBuffer = new T[capacity << 1];
            if (offset > capacity - Count)
            {
                var len = buf.Length - offset;
                Array.Copy(buf, offset, newBuffer, 0, len);
                Array.Copy(buf, 0, newBuffer, len, Count - len);
            }
            else Array.Copy(buf, offset, newBuffer, 0, Count);
            buf = newBuffer;
            offset = 0;
            capacity <<= 1;
        }
        public T[] Items
        {
            get {
                var a = new T[Count];
                for (int i = 0; i < Count; i++)
                    a[i] = this[i];
                return a;
            }
        }
    }

    /// <summary>
    /// Map
    /// </summary>
    /// <typeparam name="TKey">Keyの型</typeparam>
    /// <typeparam name="TValue">Valueの型</typeparam>
    [Serializable]
    public class Map<TKey, TValue> : Dictionary<TKey, TValue>
    {
        public new TValue this[TKey i]
        {
            get
            {
                TValue v;
                return TryGetValue(i, out v) ? v : base[i] = default(TValue);
            }
            set { base[i] = value; }
        }
    }

    public class Fraction
    {
        private long _denominator;
        private long _numerator;

        public Fraction(long denominator, long numerator)
        {
            _denominator = denominator;
            _numerator = numerator;
        }

        /// <summary>
        /// 分子を表します。
        /// </summary>
        public long Denominator
        {
            get { return _denominator; }
            set
            {
                _denominator = value;
                Reduction();
            }
        }

        /// <summary>
        /// 分母を表します。
        /// </summary>
        public long Numerator
        {
            get { return _numerator; }
            set
            {
                if(value==0)throw new ArgumentException();
                _numerator = value;
                Reduction();
            }
        }

        public double DoubleNumber => Numerator / (double) Denominator;
        public decimal DecimalNumber => Numerator / (decimal) Denominator;
        public bool IsInteger => Denominator == 1;

        /// <summary>
        /// 現在の分数に対して約分を行います。
        /// </summary>
        private void Reduction()
        {
            if (Numerator == 0) return;
            long gcd = Gcd(Denominator, Numerator);
            Denominator /= gcd;
            Numerator /= gcd;
        }

        public static Fraction operator +(Fraction left, Fraction right)
        {
            long numeratorLcm = Lcm(left.Numerator, right.Numerator);
            return new Fraction(
                left.Denominator * (numeratorLcm / left.Numerator) +
                right.Denominator * (numeratorLcm / right.Numerator), numeratorLcm);
        }

        public static Fraction operator -(Fraction left, Fraction right) => left + new Fraction(right.Denominator * -1, right.Numerator);

        public static Fraction operator +(Fraction left, long right) => left + new Fraction(right, 1);
        public static Fraction operator -(Fraction left, long right) => left + new Fraction(-right, 1);

        public static Fraction operator -(Fraction f) => new Fraction(f.Denominator * -1, f.Numerator);

        public static Fraction operator *(Fraction left, Fraction right)
        {
            //左の分子-右の分母
            var a = Gcd(left.Denominator, right.Numerator);
            //右の分子-左の分母
            var b = Gcd(right.Denominator, left.Numerator);
            return new Fraction((left.Denominator / a) * (right.Denominator / b), (left.Numerator / b) * (right.Numerator / a));
        }

        public static Fraction operator *(Fraction left, long right)
        {
            var gcd = Gcd(left.Numerator, right);
            return new Fraction(left.Denominator * (right / gcd), left.Numerator / gcd);
        }

        public static Fraction operator /(Fraction left, Fraction right)
        {
            return left * new Fraction(right.Numerator, right.Denominator);
        }

        public static Fraction operator /(Fraction left, long right)
        {
            return left * new Fraction(right, 1);
        }

        public override string ToString()
        {
            return Numerator == 1 ? $"{Denominator}" : $"{Denominator} / {Numerator}";
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



}
