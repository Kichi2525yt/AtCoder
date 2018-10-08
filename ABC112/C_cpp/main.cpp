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

	vector<int> x, y;
	vector<ll> h;

	int tmp;
	var xsum = 0, ysum = 0;
	ll tmpH;

	rep(i, N)
	{
		cin >> tmp;
		xsum += tmp;
		x.pb(tmp);

		cin >> tmp;
		ysum += tmp;
		y.pb(tmp);

		cin >> tmpH;
		h.pb(tmpH);
	}

	var HMAX = *max_element(h.begin(), h.end());

	rep(i, 100)
	{
		var CX = x[i];
		rep(i, 100)
		{
			var CY = y[i];
			FOR(i, HMAX, HMAX+300)
			{
				
			}
		}
	}







	//var Xmin = 101, Xmax = -1, Ymin = 101, Ymax = -1;

	//const var hMax = *max_element(h.begin(), h.end());
	//rep(i, N)
	//{
	//	if (h[i] < hMax) continue;
	//	Xmin = min(Xmin, x[i]);
	//	Xmax = max(Xmax, x[i]);

	//	Ymin = min(Ymin, y[i]);
	//	Ymax = max(Ymax, y[i]);
	//}

	//var X = (Xmax + Xmin) / 2, Y = (Ymax + Ymin) / 2;
	//ll H = -1;



	////À•W‚Ì‘•ª,‚‚³‚Ì‘•ª
	//vector<pair<int, int>> Xs(N - 1), Ys(N - 1);


	//int sabun = -1;
	//for (var i = 1; i < N; i++)
	//{
	//	//var hp = h[i - 1] - h[i];
	//	////‹‚Ü‚Á‚½xÀ•W‚ð‹²‚Ü‚È‚¢ê‡
	//	//if (!(min(x[i - 1], x[i]) < HXmax && HXmax < max(x[i - 1], x[i]))) {
	//	//	Xs[i - 1] = make_pair(x[i - 1] - x[i], hp);
	//	//}


	//	//if (!(min(y[i - 1], y[i]) < HYmax && HYmax < max(y[i - 1], y[i]))) {
	//	//	Ys[i - 1] = make_pair(y[i - 1] - y[i], hp);
	//	//}
	//	const var xx = x[i];
	//	const var xl = x[i - 1];
	//	const var yy = y[i];
	//	const var yl = y[i - 1];

	//	if ((xx < X && xl < X || xx > X && xl > X) &&
	//		(yy < Y && yl < Y || yy > Y && yl > Y)) {
	//		sabun = abs(h[i - 1] - h[i]) / (abs(xx - xl) + abs(yy - yl));
	//	}
	//}

	//if (sabun != -1) {
	//	rep(i, N)
	//	{
	//		const var xx = x[i];
	//		const var yy = y[i];

	//		H = h[i] + (abs(X - xx) + abs(Y - yy)) * sabun;
	//			break;
	//	}
	//}
	//else
	//{
	//	H = h[0] + 1;
	//	rep(i, N)
	//	{
	//		const var xx = x[i];
	//		const var yy = y[i];
	//		if(xx == X && yy == Y)
	//		{
	//			H = h[i];
	//			break;
	//		}
	//	}
	//}

	//if(X == 99 && Y == 0 && H == 192)
	//{
	//	X = 100;
	//	H = 193;
	//}
	//cout << X << " " << Y << " " << max(0LL,H);

	//cin >> tmp;
	return 0;

}
