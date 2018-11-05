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


int main() {
	cin.tie(nullptr);
	ios::sync_with_stdio(false);

	int N, T, A;
	cin >> N >> T >> A;
	vi H(N);
	rep(i, N) {
		cin >> H[i];
	}

	int answer = -1;
	double e = 1 << 20;


	rep(i, N) {
		//H[i+1]地点の気温
		double current = T - H[i] * 0.006;
		//current と A の差
		double current_e = abs(A - current);

		//メモ出力
		cerr << i << " " << current << " " << current_e << endl;

		//差がより少ない場合
		if (current_e < e) {
			//最も差が小さい数を更新、現時点での答えをiとする
			e = current_e;
			answer = i;
		}
	}
	cout << answer + 1 << endl;
	return 0;
}