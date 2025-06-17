#include <string>
#include <vector>

using std::stoi;
using std::string;
using std::vector;

const string quote = "\"";

namespace extractor {
	inline string json(const string& json, const string& key) {
		string start = quote + key + quote + ":";

		size_t pos = json.find(start, 0);
		if (pos == string::npos)
			return "";

		pos += start.length();

		while (
			pos < json.length() && (
				json[pos] == ' ' ||
				json[pos] == '\t'
			)
		) pos++;

		if (pos >= json.length())
			return "";

		if (json[pos] == '"') {
			pos++;

			size_t end = pos;
			while (
				end < json.length() &&
				json[end] != '"'
			) {
				if (json[end] == '\\')
					end += 2;
				else
					end++;
			}

			return json.substr(pos, end - pos);
		}
		else if (
			json[pos] == 't' &&
			json.substr(pos, 4) == "true"
		) {
			return "true";
		}
		else if (
			json[pos] == 'f' &&
			json.substr(pos, 5) == "false"
		) {
			return "false";
		}
		else if (
			json[pos] == 'n' &&
			json.substr(pos, 4) == "null"
		) {
			return "null";
		}
		else {
			size_t end = pos;
			while (
				end < json.length() &&
				json[end] != ',' &&
				json[end] != '}' &&
				json[end] != ']'
			) {
				end++;
			}

			string val = json.substr(pos, end - pos);

			while (!val.empty() && (
					val.back() == ' ' ||
					val.back() == '\t' ||
					val.back() == '\n' ||
					val.back() == '\r'
			)) {
				val.pop_back();
			}

			return val;
		}
	}

	inline vector<string> app_releases(const string& json) {
		vector<string> apps;

		size_t pos = json.find('[');
		if (pos == string::npos)
			return apps;

		pos++;
		int curly_count = 0;
		size_t start = pos;

		for (size_t i = pos; i < json.length(); i++) {
			if (json[i] == '{') {
				if (curly_count == 0)
					start = i;

				curly_count++;
			}
			else if (json[i] == '}') {
				curly_count--;
				if (curly_count != 0)
					continue;

				apps.push_back(json.substr(
					start, i - start + 1
				));
			}
		}

		return apps;
	}

	inline string app_downloads(const string& json) {
		size_t assets = json.find("\"assets\":");
		if (assets == string::npos)
			return "";

		size_t array = json.find('[', assets);
		if (array == string::npos)
			return "";

		size_t array_end = array + 1;
		int square_count = 1;

		while (array_end < json.length() && square_count > 0) {
			if (json[array_end] == '[')
				square_count++;
			else if (json[array_end] == ']')
				square_count--;

			array_end++;
		}

		string all_assets = json.substr(array, array_end - array);

		size_t pos = 1;
		int curly_count = 0;
		size_t start = pos;

		for (size_t i = pos; i < all_assets.length(); i++) {
			if (all_assets[i] == '{') {
				if (curly_count == 0)
					start = i;

				curly_count++;
			}
			else if (all_assets[i] == '}') {
				curly_count--;
				if (curly_count != 0)
					continue;

				string asset = all_assets.substr(start, i - start + 1);
				string name = extractor::json(asset, "name");

				if (name.length() > 4 && name.substr(name.length() - 4) == ".zip") {
					return extractor::json(asset, "browser_download_url");
				}
			}
		}

		return "";
	}

	inline vector<int> app_versions(const string& version) {
		vector<int> versions;

		string temp;
		for (char c : version) {
			if (c >= '0' && c <= '9') {
				temp += c;
			}
			else if (!temp.empty()) {
				versions.push_back(stoi(temp));
				temp.clear();
			}
		}

		if (!temp.empty()) {
			versions.push_back(stoi(temp));
		}

		return versions;
	}

	inline bool new_version(const vector<int>& a, const vector<int>& b) {
		size_t max = (a.size() > b.size()) ? a.size() : b.size();

		for (size_t i = 0; i < max; i++) {
			int n1 = (i < a.size()) ? a[i] : 0;
			int n2 = (i < b.size()) ? b[i] : 0;

			if (n1 > n2) return true;
			if (n1 < n2) return false;
		}

		return false;
	}
}