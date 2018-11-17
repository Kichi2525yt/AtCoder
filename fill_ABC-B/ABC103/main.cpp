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

ll conv(const string num) { stringstream ss; ss << num << flush; ll n; ss >> n; return n; }

int main() {
	cin.tie(nullptr);
	ios::sync_with_stdio(false);

	in(n);
	var N = std::to_string(n);

	int sum = 0;
	rep(i, int(N.size()))
	{
		string s(1, N[i]);
		sum += conv(s);
	}

	cerr << sum << endl;
	cout << (n % sum == 0 ? "Yes" : "No") << endl;
	

	return 0;
}