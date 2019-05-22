#include "bits/stdc++.h"

using namespace std;

#pragma region define/typedef
#define var auto
#define FOR(i, a, b) for(int (i) = (a); (i) < (b); (i)++)
#define rep(i, n) for(int (i) = 0; (i) < (n); (i)++) 
#define rep1(i, n) for(int (i) = 1; (i) <= (n); (i)++)
#define out(a) cout<<a<<endl;
#define pb(a) push_back((a))
#define invi(vec, n) vi(vec)(n); invec((vec), (n));
#define invec(vec, n) rep(i, (n)) cin >> vec[i];
#define mp make_pair
#define mt make_tuple
#define endl "\n";
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

template<typename T>
string join(vector<T>& vec, string sep = " ")
{
	stringstream ss;
	for (int i = 0; i < vec.size(); i++) {
		ss << vec[i] << (i + 1 == vec.size() ? "" : sep);
	}
	return ss.str();
}

template<typename T>
istream& operator >> (istream& is, vector<T>& vec)
{
	for (T& x : vec) is >> x;
	return is;
}
#pragma endregion


int main() {
	cin.tie(nullptr);
	ios::sync_with_stdio(false);

	int X;
	cin >> X;
	cout << (X == 7 || X == 5 || X == 3 ? "YES" : "NO") << endl;



}
