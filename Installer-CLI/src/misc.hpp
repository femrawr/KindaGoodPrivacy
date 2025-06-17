#include <string>
#include <Windows.h>

using std::string;
using std::wstring;

namespace misc {
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