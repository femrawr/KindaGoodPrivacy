#include <iostream>
#include <string>
#include <vector>
#include <shlobj.h>

#include "extractor.hpp"
#include "http.hpp"
#include "misc.hpp"
#include "zip.hpp"

using std::cin;
using std::cout;
using std::cerr;
using std::string;
using std::vector;

const char end = '\n';

const int VERSION_MAJOR = 1;
const int VERSION_MINOR = 1;
const int VERSION_PATCH = 2;

struct Release {
	string name;
	string downloadUrl;
	string fileName;
	vector<int> version;
};

int main() {
	cout << "Installer CLI version: v" <<
		VERSION_MAJOR <<
		'.' <<
		VERSION_MINOR <<
		'.' <<
		VERSION_PATCH <<
		end;

	cout << "Featching releases, hold on a while..." << end;

	string res = http::request(
		L"api.github.com",
		L"/repos/femrawr/KindaGoodPrivacy/releases"
	);

	if (res.empty()) {
		cerr << "Failed to fetch releases, try again later." << end;
		return 1;
	}

	vector<string> releases = extractor::app_releases(res);
	vector<Release> apps;

	for (const auto& release_json : releases) {
		string tag = extractor::json(release_json, "tag_name");
		if (!tag.ends_with("app"))
			continue;

		Release release;
		release.name = extractor::json(release_json, "name");
		release.downloadUrl = extractor::app_downloads(release_json);
		release.version = extractor::app_versions(release.name);

		if (!release.downloadUrl.empty()) {
			size_t last_slash = release.downloadUrl.find_last_of('/');
			if (last_slash != string::npos) {
				release.fileName = release.downloadUrl.substr(last_slash + 1);
			}
		}

		if (!release.downloadUrl.empty()) {
			apps.push_back(release);
		}
	}

	if (apps.empty()) {
		cerr << "Failed to find any app releases, maybe try again later?" << end;
		return 1;
	}

	int index = 0;
	for (int i = 1; i < apps.size(); i++) {
		if (extractor::new_version(apps[i].version, apps[index].version)) {
			index = i;
		}
	}

	cout << "\nDownloads:" << end;

	for (int i = 0; i < apps.size(); i++) {
		cout << "[" << (i + 1) << "] " << apps[i].name;
		if (i == index)
			cout << " (LATEST)";

		cout << end;
	}

	int choice;
	cout << "\nChoice: ";
	cin >> choice;

	if (choice < 1 || choice > apps.size()) {
		cout << "Invalid choice" << end;
		return 1;
	}

	Release selected = apps[choice - 1];

	char app_data[MAX_PATH];
	HRESULT hres = SHGetFolderPathA(
		NULL, CSIDL_APPDATA,
		NULL, NULL, app_data
	);

	if (hres != S_OK) {
		cerr << "\nFailed to get Application Data path." << end;
		return 1;
	}

	string app_data_str = string(app_data);

	string main_folder = app_data_str + "\\" + "Kinda Good Privacy";
	string app_folder = main_folder + "\\" + "Application";
	string temp_path = app_data_str + "\\" + "temp_" + std::to_string(choice) + "_" + selected.fileName;

	cout << "\nDownloading " << selected.name << ", hold on a while..." << end;

	bool downloaded =  http::download(selected.downloadUrl, temp_path);
	if (!downloaded) {
		cerr << "Failed to download." << end;
		return 1;
	}

	BOOL created = CreateDirectoryA(main_folder.c_str(), NULL);
	if (!created && GetLastError() != ERROR_ALREADY_EXISTS) {
		cerr << "Failed to create main folder." << end;
		return 1;
	}

	DWORD attribs = GetFileAttributesA(app_folder.c_str());
	if (attribs != INVALID_FILE_ATTRIBUTES) {
		cout << "Old application folder found, attempting to remove it." << end;

		bool deleted = misc::delete_dir(app_folder);
		if (!deleted) {
			cout << "Failed to delete old application folder." << end;
			return 1;
		}
	}

	created = CreateDirectoryA(app_folder.c_str(), NULL);
	if (!created && GetLastError() != ERROR_ALREADY_EXISTS) {
		cerr << "Failed to create application folder." << end;
		return 1;
	}

	cout << "Extracting files..." << end;

	bool unzipped = zip::unzip(
		misc::str_to_wstr(temp_path),
		misc::str_to_wstr(app_folder)
	);

	if (!unzipped) {
		DeleteFileA(temp_path.c_str());

		cerr << "Failed to unzip main files." << end;
		return 1;
	}

	DeleteFileA(temp_path.c_str());

	bool shortcut = misc::create_shortcut(
		misc::str_to_wstr(app_folder) + L"\\KindaGoodPrivacy.exe",
		misc::str_to_wstr(app_folder)
	);

	if (!shortcut) {
		cerr << "Failed to create desktop shortcut." << end;
	}

	std::cout << "Files installed to: " << app_folder << end;
	std::cout << "Installation completed successfully!" << end;

	return 0;
}