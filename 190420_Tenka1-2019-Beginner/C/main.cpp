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

int main()
{
	cin.tie(nullptr);
	ios::sync_with_stdio(false);
	
	inp(n);
	vector<int> A;

	rep(i, n) {
		int tmp;
		cin >> tmp;
		A.pb(tmp);
	}
	sort(A.begin(), A.end());

	vector<int> a;
	
	var last = A[n / 2];
	A.erase(A.begin() + n / 2);
	a.pb(last);

	for(var i = 1; i < n; i++)
	{
		var max = *A.end();
		var min = *A.begin();
		var mmax = abs(max - last),
			mmin = abs(last - min);
		if(mmax > mmin)
		{
			A.erase(A.end());
			a.pb(mmax);
			last = mmax;
		}
		else
		{
			A.erase(A.begin());
			a.pb(mmin);
			last = mmin;
		}
	}


	return 0;
}