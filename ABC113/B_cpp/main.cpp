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
		//H[i+1]�n�_�̋C��
		double current = T - H[i] * 0.006;
		//current �� A �̍�
		double current_e = abs(A - current);

		//�����o��
		cerr << i << " " << current << " " << current_e << endl;

		//������菭�Ȃ��ꍇ
		if (current_e < e) {
			//�ł����������������X�V�A�����_�ł̓�����i�Ƃ���
			e = current_e;
			answer = i;
		}
	}
	cout << answer + 1 << endl;
	return 0;
}