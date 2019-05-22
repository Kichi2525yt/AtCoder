#include "bits/stdc++.h"
using namespace std;
#pragma region define / typedef
#pragma warning(disable : 4996)
//auto
#define var auto
#define cvar const auto &
//a..b-1
#define FOR(i, a, b) for (int(i) = (a), i___cnt = (b); (i) < i___cnt; (i)++)
//b..a
#define FORR(i, a, b) for (int(i) = (b), i___cnt = (a); (i) >= i___cnt; (i)--)
//0..n-1
#define rep(i, n) for (int(i) = 0, i___cnt = (n); (i) < i___cnt; (i)++)
//1..n
#define rep1(i, n) for (int(i) = 1, i___cnt = (n); (i) <= i___cnt; (i)++)
//n-1..0
#define repr(i, n) for (int(i) = (n)-1; (i) >= 0; (i)--)
//n-1..1
#define repr1(i, n) for (int(i) = (n); (i) > 0; (i)--)
#define in1(a) cin >> a
#define in2(a, b) cin >> a >> b
#define in3(a, b, c) cin >> a >> b >> c
#define in4(a, b, c, d) cin >> a >> b >> c >> d
#define in5(a, b, c, d, e) cin >> a >> b >> c >> d >> e
#define pb push_back
#define mp make_pair
#define mt make_tuple
#define endl "\n";
#define outif(b, t, f) cout << ((b) ? (t) : (f)) << endl;
#define bsort(vec) sort((vec).begin(), (vec).end())
#define rsort(vec) sort((vec).rbegin(), (vec).rend());
#define all(vec) (vec).begin(), (vec).end()
#define even(i) (!(i & 1))
#define odd(i) (i & 1)
#define sz(x) (int((x).size()))
#define mset(v, n) memset((v), n, sizeof(v))
#define setminus(v) mset(v, -1)
#define setzero(v) mset(v, 0)
#define BIT(N) (1LL << (N))
typedef long long ll;
typedef long double lld;
typedef unsigned int uint;
typedef vector<int> vint;
typedef vector<ll> vlong;
typedef vector<pair<ll, ll>> vpair;
const int MAX = 2147483647;
const int MIN = 0 - 2147483648;
const ll MAXL = 922337203685775807;
const ll MINL = 0 - 922337203685775808;
#pragma endregion
#pragma region methods / operator
#pragma warning(disable : 6031)
ll read_long()
{
    ll i;
    scanf("%lld", &i);
    return i;
}
ll read_ll() { return read_long(); }
int read_int() { return int(read_long()); }

template <class T1>
void read_vec(const int n, vector<T1> &vec1)
{
    rep(i, n)
    {
        cin >> vec1[i];
    }
}

template <class T1, class T2>
void read_vec(const int n, vector<T1> &vec1, vector<T2> &vec2)
{
    rep(i, n)
    {
        cin >> vec1[i] >> vec2[i];
    }
}

template <class T1, class T2, class T3>
void read_vec(const int n, vector<T1> &vec1, vector<T2> &vec2, vector<T3> &vec3)
{
    rep(i, n)
    {
        cin >> vec1[i] >> vec2[i] >> vec3[i];
    }
}

ll parse(const string num)
{
    stringstream ss;
    ss << num << flush;
    ll n;
    ss >> n;
    return n;
}
string to_string(const ll n)
{
    stringstream ss;
    ss << n << flush;
    return ss.str();
}

vector<string> split(string s, string delim)
{
    vector<string> res;
    auto pos = 0;
    while (true)
    {
        const int found = s.find(delim, pos);
        if (found >= 0)
        {
            res.push_back(s.substr(pos, found - pos));
        }
        else
        {
            res.push_back(s.substr(pos));
            break;
        }
        pos = found + delim.size();
    }
    return res;
}

template <typename T>
string join(vector<T> &vec, string sep = " ")
{
    var size = vec.size();
    if (size == 0)
        return "";
    stringstream ss;
    for (int i = 0; i < size - 1; i++)
    {
        ss << vec[i] << sep;
    }
    ss << vec[size - 1];
    return ss.str();
}

template <typename T>
istream &operator>>(istream &is, vector<T> &vec)
{
    for (T &x : vec)
        is >> x;
    return is;
}

template <typename T>
void print(T t)
{
    cout << t << endl;
}

ll powmod(const ll a, const ll b, const ll p)
{
    if (b == 0)
        return 1;
    if (b % 2)
        return a % p * (powmod(a, b - 1, p) % p) % p;

    ll d = powmod(a, b / 2, p) % p;
    return (d * d) % p;
}

ll gcd(ll a, ll b)
{
    if (a < b)
        gcd(b, a);
    ll r;
    while ((r = a % b))
    {
        a = b;
        b = r;
    }
    return b;
}

template <class T>
bool chmax(T &a, const T &b)
{
    if (a < b)
    {
        a = b;
        return true;
    }
    return false;
}

template <class T>
bool chmin(T &a, const T &b)
{
    if (a > b)
    {
        a = b;
        return true;
    }
    return false;
}
#pragma endregion
#pragma region initialize
struct init
{
    init()
    {
        cin.tie(nullptr);
        ios::sync_with_stdio(false);
        cout << fixed << setprecision(20);
    }
} it;
#pragma endregion

int main()
{
    int N, K;
    cin >> N >> K;
    cout << N - K + 1 << endl;
}
