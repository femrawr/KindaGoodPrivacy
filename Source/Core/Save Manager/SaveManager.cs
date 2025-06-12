namespace KindaGoodPrivacy.Source.Core.SaveManager
{
    public class SaveManager
    {
        public static void Save(string data, int type)
        {
            switch (type)
            {
                case Variables.SAVETYPE_TEXT:
                    TextSave.SaveText(data);
                    break;

                case Variables.SAVETYPE_IMG:
                    ImageSave.SaveImg(data);
                    break;

                case Variables.SAVETYPE_VID:
                    break;

                default:
                    break;
            }
        }

        public static object? Load(int type)
        {
            switch (type)
            {
                case Variables.SAVETYPE_TEXT:
                    return TextSave.LoadText();

                case Variables.SAVETYPE_IMG:
                    return ImageSave.LoadImg();

                case Variables.SAVETYPE_VID:
                    break;

                default:
                    break;
            }

            return null;
        }

        public static string SaveTemp(string data)
        {
            string fileName = DateTimeOffset.Now
                .ToUnixTimeSeconds()
                .ToString()
                + Variables.extension;

            string filePath = Path.Combine(Variables.tempFolderPath, fileName);

            string encrypted = CryptManager.SetEncrypted(data, fileName + filePath);

            Utils.Saving.CreateFile(filePath);
            Utils.Saving.WriteFile(filePath, encrypted);

            return filePath;
        }

        public static string? LoadTemp()
        {
            var files = Directory.GetFiles(
                Variables.tempFolderPath,
                $"*{Variables.extension}"
            );

            int fileLen = files.Length;
            if (fileLen == 0)
                return null;

            var openedFiles = new HashSet<string>();

            var metaContents = File.ReadAllLines(Variables.tempMetadataPath);
            foreach (var line in metaContents)
                openedFiles.Add(line.Trim());

            var unprocessedFiles = files
                .Where(file => !openedFiles.Contains(Path.GetFileName(file)) && Path.GetFileName(file) != Variables.tempMetadataName)
                .ToList();

            if (unprocessedFiles.Count == 0)
                return null;

            var result = MessageBox.Show(
                $"There {(unprocessedFiles.Count == 1 ? "is 1 temp file" : $"are {unprocessedFiles.Count} temp files")} not properly encrypted and saved. Do you want to open one of them?",
                "KGP - Temp files detected",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
                return null;

            string file = unprocessedFiles.First();
            string fileName = Path.GetFileName(file);

            File.AppendAllText(Variables.tempMetadataPath, fileName + '\n');

            if (!File.Exists(file))
                return null;

            string contents = File.ReadAllText(file);
            return CryptManager.SetDecrypted(contents, fileName + file);
        }

        public static void Init()
        {
            Utils.Saving.CreateFolder(Variables.textFolderName);
            Utils.Saving.CreateFolder(Variables.mediaFolderName);
            Utils.Saving.CreateFolder(Variables.tempFolderName);

            Utils.Saving.CreateFolder(Path.Combine(
                 Variables.mediaFolderName,Variables.imgFolderName
            ));

            Utils.Saving.CreateFolder(Path.Combine(
                Variables.mediaFolderName, Variables.vidFolderName
            ));

            Utils.Saving.CreateFile(Path.Combine(
                Variables.tempFolderName, Variables.tempMetadataName
            ));

            Task.Run(() =>
            {
                string[] files = Directory.GetFiles(
                    Variables.tempFolderPath,
                    $"*{Variables.extension}"
                );

                foreach (var file in files)
                {
                    var info = new FileInfo(file);

                    if (info.Name == Variables.tempMetadataName)
                        continue;

                    if (DateTime.Now - info.CreationTime > Variables.FILE_EXPIRE_TIME)
                        File.Delete(file);
                }
            });
        }
    }
}
