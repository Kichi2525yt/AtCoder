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
	in(A); in(B);
	int diff = B - A;

	for(int i = 2; i <= 999; i++)
	{
		int n = 0;
		for (int j = 1; j <= i; j++)
			n += j;

		int last = n - i;

		if(n - last == diff)
		{
			cout << n - B << endl;
			return 0;
		}
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