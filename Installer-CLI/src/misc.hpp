#include <string>
#include <shlobj.h>
#include <Windows.h>

using std::string;
using std::wstring;

namespace misc {
	inline bool create_shortcut(const wstring& path, const wstring& dir) {
		HRESULT res = CoInitialize(NULL);
		if (FAILED(res))
			return false;

		IShellLinkW* shell = NULL;

		res = CoCreateInstance(
			CLSID_ShellLink, NULL,
			CLSCTX_INPROC_SERVER,
			IID_IShellLinkW,
			(void**)&shell
		);

		if (FAILED(res)) {
			CoUninitialize();
			return false;
		}

		shell->SetPath(path.c_str());
		shell->SetWorkingDirectory(dir.c_str());

		IPersistFile* file = NULL;

		res = shell->QueryInterface(
			IID_IPersistFile,
			(void**)&file
		);

		if (FAILED(res)) {
			shell->Release();
			CoUninitialize();
			return false;
		}

		char desktop_path[MAX_PATH];
		res = SHGetFolderPathA(
			NULL, CSIDL_DESKTOP,
			NULL, NULL, desktop_path
		);

		if (FAILED(res)) {
			file->Release();
			shell->Release();
			CoUninitialize();
			return false;
		}

		wstring desktop_path_wide = wstring(desktop_path, desktop_path + strlen(desktop_path));
		wstring shortcut_path = desktop_path_wide + L"\\KGP.lnk";
		res = file->Save(shortcut_path.c_str(), TRUE);

		file->Release();
		shell->Release();
		CoUninitialize();

		return SUCCEEDED(res);
	}

	inline bool delete_dir(const string& path) {
		string file_name = path + "\\*";

		WIN32_FIND_DATAA find_data;
		HANDLE handle = FindFirstFileA(
			file_name.c_str(),
			&find_data
		);

		if (handle == INVALID_HANDLE_VALUE)
			return false;

		do {
			if (
				strcmp(find_data.cFileName, ".") == 0 ||
				strcmp(find_data.cFileName, "..") == 0
			) {
				continue;
			}

			string full_path = path + "\\" + find_data.cFileName;

			if (
				find_data.dwFileAttributes &
				FILE_ATTRIBUTE_DIRECTORY
			) {
				misc::delete_dir(full_path);
			}
			else {
				SetFileAttributesA(
					full_path.c_str(),
					FILE_ATTRIBUTE_NORMAL
				);

				DeleteFileA(full_path.c_str());
			}
		} while (FindNextFileA(handle, &find_data));

		FindClose(handle);

		return RemoveDirectoryA(path.c_str());
	}

	inline wstring str_to_wstr(const string& str) {
		int size = MultiByteToWideChar(
			CP_UTF8, NULL,
			str.c_str(),
			(int)str.length(),
			NULL, 0
		);

		wstring str_wide(size, 0);

		MultiByteToWideChar(
			CP_UTF8, NULL,
			str.c_str(),
			(int)str.length(),
			&str_wide[0],
			size
		);

		return str_wide;
	}
}