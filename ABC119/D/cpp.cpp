#include "bits/stdc++.h"
using namespace std;
#pragma region define/typedef
//auto
#define var auto
//a..b-1
#define FOR(i, a, b) for(var (i) = (a); (i) < (b); (i)++)
//b..a
#define FORR(i, a, b) for(var (i) = (b); (i) >= (a); (i)--)
//0..n-1
#define rep(i, n) for(var (i) = 0; (i) < (n); (i)++) 
//1..n
#define rep1(i, n) for(var (i) = 1; (i) <= (n); (i)++)
//n-1..0
#define repr(i, n) for(var (i) = (n) - 1; (i) >= 0; (i)--)
//n-1..1
#define repr1(i, n) for(var (i) = (n); (i) > 0; (i)--)
#define pb push_back
#define mp make_pair
#define mt make_tuple
#define endl "\n";
#define outif(b, t, f) cout << ((b) ? (t) : (f)) << endl;
#define sort(vec) sort((vec).begin(), (vec).end())
#define all(vec) (vec).begin(), (vec).end()
#define even(i) (!(i&1))
#define odd(i) (i&1)
typedef long long ll;
typedef unsigned int uint;
typedef vector<int> vint;
typedef vector<ll> vlong;
typedef vector<pair<int, int>> vpair;
const int MAX = 2147483647;
const int MIN = 0 - 2147483648;
const ll MAXL = 922337203685775807;
const ll MINL = 0 - 922337203685775808;
#pragma endregion
#pragma region methods/operators
ll parse(const string num) { stringstream ss; ss << num << flush; ll n; ss >> n; return n; }
string tostring(const ll n) { stringstream ss; ss << n << flush; return ss.str(); }

vector<string> split(string s, string delim)
{
  vector<string> res;
  auto pos = 0;
  while (true) {
    const int found = s.find(delim, pos);
    if (found >= 0) {
      res.push_back(s.substr(pos, found - pos));
    } else {
      res.push_back(s.substr(pos));
      break;
    }
    pos = found + delim.size();
  }
  return res;
}

template<typename T>
string join(vector<T>& vec, string sep = " ")
{
  var size = vec.size();
  if (size == 0) return "";
  stringstream ss;
  for (int i = 0; i < size - 1; i++) {
    ss << vec[i] << sep;
  }
  ss << vec[size - 1];
  return ss.str();
}

template<typename T>
istream& operator >> (istream& is, vector<T>& vec)
{
  for (T& x : vec) is >> x;
  return is;
}

template<typename T>
void print(T t)
{
  cout << t << endl;
}

ll powmod(const ll a, const ll b, const ll p)
{
  if (b == 0) return 1;
  if (b % 2) return a % p * (powmod(a, b - 1, p) % p) % p;

  ll d = powmod(a, b / 2, p) % p;
  return (d*d) % p;
}

ll gcd(ll a, ll b)
{
  if (a < b) gcd(b, a);
  ll r;
  while ((r = a % b)) {
    a = b;
    b = r;
  }
  return b;
}
#pragma endregion


int main()
{
  int dx[] = { 0, 0, 1, 1 };
  int dy[] = { 0, 1, 0, 1 };

  int A, B, Q;
  while (cin >> A >> B >> Q) {
    vlong s(A); cin >> s;
    vlong t(B); cin >> t;
    vlong X(Q); cin >> X;

    rep(i, Q)
    {
      ll x = X[i];
      var ans = ll(1e12);
      rep(i, 4)
      {
        var a = dx[i], b = dy[i];

        var n_itr = lower_bound(s.begin(), s.end(), x);
        if (n_itr == (a ? s.end() : s.begin())) continue;

        var m_itr = lower_bound(t.begin(), t.end(), x);
        if (m_itr == (b ? t.end() : t.begin())) continue;

        if (!a) --n_itr; if (!b)--m_itr;
        ll n = *n_itr, m = *m_itr;

        ll d = a == b ? abs((a ? max(n, m) : min(n, m)) - x) : abs(x - n) + abs(x - m) + min(abs(x - n), abs(x - m));

        ans = min(ans, d);
      }
      cout << ans << endl;
    }
  }


  return 0;
}