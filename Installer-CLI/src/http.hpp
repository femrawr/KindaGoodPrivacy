#include <string>
#include <fstream>
#include <Windows.h>
#include <winhttp.h>

#pragma comment(lib, "winhttp.lib")

using std::string;
using std::wstring;

const wstring agent = L"KGP Installer/1.0";

namespace http {
	inline bool fetch(const wstring& server, const wstring& url, string& out) {
		HINTERNET session = NULL;
		HINTERNET connect = NULL;
		HINTERNET request = NULL;

		session = WinHttpOpen(
			agent.c_str(),
			WINHTTP_ACCESS_TYPE_DEFAULT_PROXY,
			WINHTTP_NO_PROXY_NAME,
			WINHTTP_NO_PROXY_BYPASS,
			NULL
		);

		if (!session) {
			return false;
		}

		connect = WinHttpConnect(
			session,
			server.c_str(),
			INTERNET_DEFAULT_HTTPS_PORT,
			NULL
		);

		if (!connect) {
			WinHttpCloseHandle(session);
			return false;
		}

		request = WinHttpOpenRequest(
			connect, L"GET",
			url.c_str(), 0,
			WINHTTP_NO_REFERER,
			WINHTTP_DEFAULT_ACCEPT_TYPES,
			WINHTTP_FLAG_SECURE
		);

		if (!request) {
			WinHttpCloseHandle(connect);
			WinHttpCloseHandle(session);

			return false;
		}

		BOOL res = WinHttpSendRequest(
			request,
			WINHTTP_NO_ADDITIONAL_HEADERS,
			0, WINHTTP_NO_REQUEST_DATA,
			0, 0, NULL
		);

		if (!res || !WinHttpReceiveResponse(
			request, NULL
		)) {
			WinHttpCloseHandle(request);
			WinHttpCloseHandle(connect);
			WinHttpCloseHandle(session);

			return false;
		}

		DWORD size = 0;
		out.clear();

		do {
			if (!WinHttpQueryDataAvailable(
				request, &size) || size == 0
			) break;

			char* buffer = new char[size + 1];
			if (!buffer) break;

			ZeroMemory(buffer, size + 1);

			DWORD read = 0;
			if (WinHttpReadData(
				request, buffer, size, &read) &&
				read > 0
			) {
				out.append(buffer, read);
			}

			delete[] buffer;
			if (read == 0) break;
		} while (size > 0);

		return true;
	}

	inline string request(const wstring& server, const wstring& url) {
		string res;
		bool suc = fetch(server, url, res);
		if (suc) {
			return res;
		}

		return "";
	}

	inline bool download(const string& url, const string& to) {
		if (url.rfind("https://", 0) != 0)
			return false;

		string stripped = url.substr(8);
		size_t slash = stripped.find('/');
		if (slash == string::npos)
			return false;

		wstring server_wide(stripped.begin(), stripped.begin() + slash);
		wstring url_wide(stripped.begin() + slash, stripped.end());

		string data;
		if (!fetch(server_wide, url_wide, data))
			return false;

		std::ofstream stream(to, std::ios::binary);
		if (!stream.is_open())
			return false;

		stream.write(data.data(), data.size());
		return stream.good();
	}
}