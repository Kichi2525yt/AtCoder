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
#define inp(a) int a; cin >> a;
#define inps(s) std::string s; cin >> s;
#define out(a) cout<<a<<endl;
#define pb(a) push_back((a))
#define endl "\n"

ll conv(const string& num) { stringstream ss; ss << num << flush; ll n; ss >> n; return n; }
string conv(const ll n) { stringstream ss; ss << n << flush; return ss.str(); }

int main()
{
	cin.tie(nullptr);
	ios::sync_with_stdio(false);

	inp(N);
	inp(T);
	//vector<int> c(N); //cost
	//vector<int> t(N); //time

	//int tmp;
	//rep(i, N)
	//{
	//	cin >> tmp;
	//	c[i] = tmp;
	//	cin >> tmp;
	//	t[i] = tmp;
	//}
	int min = 10000;

	rep(i, N)
	{
		int cost, time;
		cin >> cost;
		cin >> time;

		if (time > T) continue;
		if (cost < min)
			min = cost;
	}
	if(min == 10000)
	{
		out("TLE");
		return 0;
	}
	out(min);

	

	return 0;
}