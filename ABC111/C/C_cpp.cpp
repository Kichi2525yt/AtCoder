#include <iostream>
#include <string>
#include <vector>
#include <sstream>
#include <algorithm>

using namespace std;

#define var auto
#define ll long long
#define FOR(i, a, b) for(int (i) = (a); (i) < (b); (i)++)
#define rep(i, n) for(int (i) = 0; (i) < (n); (i)++) 
#define inp(a) int a; cin >> a;
#define inps(s) std::string s; cin >> s;
#define out(a) cout<<a<<endl;
#define pb(a) push_back((a))
#define vi vector<int>

ll conv(const string num) { stringstream ss; ss << num << flush; ll n; ss >> n; return n; }
string conv(const ll n) { stringstream ss; ss << n << flush; return ss.str(); }

int main() {
	inp(n);
	vi v(n);
	rep(i, n)
	{
		int tmp;
		cin >> tmp;
		v[i] = tmp;
	}

	var g = vector<int>(100000, 0);
	var k = vector<int>(100000, 0);
	//偶数
	for (var i = 0; i < n; i++)
	{
		if (i % 2 == 0)
			++k[v[i]];
		else
			++g[v[i]];
	}

	var maxE1 = max_element(k.begin(), k.end());
	var maxE2 = max_element(g.begin(), g.end());
	var max1 = distance(k.begin(), maxE1);
	var max2 = distance(g.begin(), maxE2);

	const var checked = *maxE1 <= *maxE2 ? 1 : 2;

	var answer = n - *maxE1 - *maxE2;

	if (checked == 1) {
		*maxE1 = 0;
		maxE1 = max_element(k.begin(), k.end());
		max1 = distance(k.begin(), maxE1);
		answer = n - *maxE1 - *maxE2;
	}
	else
	{
		*maxE2 = 0;
		maxE2 = max_element(g.begin(), g.end());
		max2 = distance(g.begin(), maxE2);
		answer = min(n - *maxE1 - *maxE2, answer);
	}

	//cerr << "奇数 最頻値: " << max1 << endl << "偶数 最頻値: " << max2 << endl;

	cout << answer;
}