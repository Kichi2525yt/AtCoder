#include <iostream>
#include <cstdio>
#include <cstdlib>
#include <cstring>
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

using namespace std;

#define var auto
#define ll long long
#define FOR(i, a, b) for(int (i) = (a); (i) < (b); (i)++)
#define rep(i, n) for(int (i) = 0; (i) < (n); (i)++) 
#define in(a) int a; cin >> a;
#define instr(s) std::string s; cin >> s;
#define out(a) cout<<a<<endl;
#define pb(a) push_back((a))
#define endl "\n"
#define vi vector<int>
#define invi(vec, n) vi(vec)(n); invec((vec), (n));
#define invec(vec, n) rep(i, (n)) cin >> vec[i];

int main() {
	cin.tie(nullptr);
	ios::sync_with_stdio(false);


	in(D);
	in(N);

	if (N == 100)
		N = 101;
	if(D == 0)
	{
		cout << N << endl;
	} else if (D == 1)
	{
		cout << N * 100 << endl;
	}
	else
	{
		cout << N * 10000 << endl;
	}
	return 0;
}

ll conv(const string num) { stringstream ss; ss << num << flush; ll n; ss >> n; return n; }
string conv(const ll n) { stringstream ss; ss << n << flush; return ss.str(); }

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