// This was so confusing holy shit

#include <string>
#include <shlobj.h>
#include<Windows.h>

#pragma comment(lib, "shell32.lib")

using std::wstring;

namespace zip {
	inline bool unzip(const wstring& from, const wstring& to) {
		auto _ = CoInitialize(NULL);

		IShellDispatch* shell = NULL;

		HRESULT res = CoCreateInstance(
			CLSID_Shell, NULL,
			CLSCTX_INPROC_SERVER,
			IID_IShellDispatch,
			(void**)&shell
		);

		if (FAILED(res)) {
			CoUninitialize();
			return false;
		}

		// The VARIANT to the path of the zip file.
		VARIANT zipped;
		VariantInit(&zipped);
		zipped.vt = VT_BSTR;
		zipped.bstrVal = SysAllocString(from.c_str());

		// The pointer to the zip file as a folder.
		Folder* zipped_folder = NULL;
		res = shell->NameSpace(zipped, &zipped_folder);

		if (FAILED(res) || !zipped_folder) {
			VariantClear(&zipped);

			shell->Release();
			CoUninitialize();

			return false;
		}

		// The VARIANT to the destination of
		// where the files will be extracted.
		VARIANT unzip_to;
		VariantInit(&unzip_to);
		unzip_to.vt = VT_BSTR;
		unzip_to.bstrVal = SysAllocString(to.c_str());

		// The pointer to the extraction folder.
		Folder* unzip_folder = NULL;
		res = shell->NameSpace(unzip_to, &unzip_folder);

		if (FAILED(res) || !unzip_folder) {
			VariantClear(&zipped);
			VariantClear(&unzip_to);

			zipped_folder->Release();
			shell->Release();
			CoUninitialize();

			return false;
		}

		// The pointer to the files in the zipped folder.
		FolderItems* zipped_files = NULL;
		res = zipped_folder->Items(&zipped_files);

		if (FAILED(res) || !zipped_files) {
			unzip_folder->Release();
			zipped_folder->Release();

			VariantClear(&zipped);
			VariantClear(&unzip_to);
			shell->Release();

			CoUninitialize();
			return false;
		}

		// Extract the items.

		VARIANT options;
		VariantInit(&options);
		options.vt = VT_I4;
		options.lVal = FOF_NO_UI | FOF_NOCONFIRMATION | FOF_NOCONFIRMMKDIR;

		VARIANT items;
		VariantInit(&items);
		items.vt = VT_DISPATCH;
		items.pdispVal = zipped_files;

		res = unzip_folder->CopyHere(items, options);

		// Clear

		VariantClear(&items);
		VariantClear(&options);
		VariantClear(&zipped);
		VariantClear(&unzip_to);

		zipped_files->Release();
		unzip_folder->Release();
		zipped_folder->Release();

		shell->Release();
		CoUninitialize();

		return SUCCEEDED(res);
	}
}