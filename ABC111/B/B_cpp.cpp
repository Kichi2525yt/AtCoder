#include <iostream>
#include <string>
#include <vector>
#include <sstream>

using namespace std;

#define var auto
#define ll long long
#define FOR(i, a, b) for(int (i) = (a); (i) < (b); (i)++)
#define rep(i, n) for(int (i) = 0; (i) < (n); (i)++) 
#define inp(a) int a; cin >> a;
#define inps(s) std::string s; cin >> s;
#define out(a) cout<<a<<endl;
#define pb(a) push_back((a))

ll conv(const string num) { stringstream ss; ss << num << flush; ll n; ss >> n; return n; }
string conv(const ll n) { stringstream ss; ss << n << flush; return ss.str(); }


int main()
{
	int x;
	cin >> x;

	for(var i = 111; i <= 999; i += 111)
	{
		if (i < x) continue;
		cout << i << endl;
		return 0;
	}

}