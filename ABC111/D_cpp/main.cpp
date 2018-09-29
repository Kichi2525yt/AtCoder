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
#define vi vector<int>

int main()
{
	cin.tie(nullptr);
	ios::sync_with_stdio(false);

	inp(n);
	vi X(n);
	vi Y(n);
	int tmp;
	rep(i, n)
	{
		cin >> tmp;
		X[i] = tmp;
		if (tmp > 10)return 0;

		cin >> tmp;
		Y[i] = tmp;
		if (tmp > 10) return 0;
	}




	return 0;
}