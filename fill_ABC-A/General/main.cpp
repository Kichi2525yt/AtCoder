#include <iostream>
#include <cstdio>
#include <cstdlib>
#include <cstring>
#include <cmath>
#include <cstdlib>
#include <algorithm>
#include <string>
#include <sstream>
#include <complex>
#include <vector>
#include <list>
#include <queue>
#include <deque>
#include <stack>
#include <map>
#include <set>
#include <unordered_map>
#include <functional>
#include <utility>
#include <tuple>
#include <cctype>
#include <bitset>
#include <complex>
#include <cmath>
#include <array>
#include <iomanip>

using namespace std;

#define var auto
#define FOR(i, a, b) for(int (i) = (a); (i) < (b); (i)++)
#define rep(i, n) for(int (i) = 0; (i) < (n); (i)++) 
#define in(a) int a; cin >> a;
#define instr(s) std::string s; cin >> s;
#define out(a) cout<<a<<endl;
#define pb(a) push_back((a))
#define endl "\n"
#define invi(vec, n) vint (vec)(n); invec((vec), (n));
#define invec(vec, n) rep(i, (n)) cin >> vec[i];
#define mp make_pair
#define mt make_tuple

typedef unsigned int uint;
typedef long long ll;
typedef vector<int> vint;
typedef vector<ll> vlong;
typedef vector<pair<int, int>> vpair;
const int MAX = 2149483647;
const int MIN = -214748368;;
const ll MAXL = 922337203685775807;
const ll MINL = -922337203685775808;

ll conv(const string num) { stringstream ss; ss << num << flush; ll n; ss >> n; return n; }
string conv(const ll n) { stringstream ss; ss << n << flush; return ss.str(); }

bool contains(vector<int> list, int a)
{
	rep(i, list.size())
	{
		if (list[i] == a) return true;
	}
	return false;
}

int main() {
	cin.tie(nullptr);
	ios::sync_with_stdio(false);

	int A, B, C;
	





	return 0;
}



vector<string> split(string s, string delim) {
	vector<string> res;
	auto pos = 0;
	while (true) {
		const int found = s.find(delim, pos);
		if (found >= 0) {
			res.push_back(s.substr(pos, found - pos));
		}
		else {
			res.push_back(s.substr(pos));
			break;
		}
		pos = found + delim.size();
	}
	return res;
}