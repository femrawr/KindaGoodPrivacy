namespace KindaGoodPrivacy.Source.Core
{
    public class SaveManager
    {
        private static readonly TimeSpan FILE_EXPIRE_TIME = TimeSpan.FromDays(5);

        private static readonly string extension = ".kgp";

        private static readonly string saveFolderName = "TextSaves";
        private static readonly string tempFolderName = "TempSaves";

        private static readonly string tempMetadataName = "_meta" + extension + "METADATA";

        private static readonly string saveFolderPath = Path.Combine(
            Utils.Saving.mainFolderPath, saveFolderName
        );

        private static readonly string tempFolderPath = Path.Combine(
            Utils.Saving.mainFolderPath, tempFolderName
        );

        private static readonly string tempMetadataPath = Path.Combine(
             Utils.Saving.mainFolderPath, tempFolderName, tempMetadataName
        );

        public static void Save(string data)
        {
            using var saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = saveFolderPath;
            saveFile.FileName = Utils.Util.GenString(10) + extension;
            saveFile.DefaultExt = extension;

            if (saveFile.ShowDialog() != DialogResult.OK)
                return;

            string filePath = saveFile.FileName;
            Utils.Saving.CreateFile(filePath);
            Utils.Saving.WriteFile(filePath, data);
        }

        public static string? Load()
        {
            using var openFile = new OpenFileDialog();
            openFile.InitialDirectory = saveFolderPath;
            openFile.Filter = $"Text Files (*{extension})|*{extension}";

            if (openFile.ShowDialog() != DialogResult.OK)
                return null;

            string filePath = openFile.FileName;

            if (File.Exists(filePath))
                return File.ReadAllText(filePath);

            return null;
        }

        public static string SaveTemp(string data)
        {
            string fileName = DateTimeOffset.Now.ToUnixTimeSeconds().ToString() + extension;
            string filePath = Path.Combine(tempFolderPath, fileName);

            string encrypted = CryptManager.SetEncrypted(data, fileName + filePath);

            Utils.Saving.CreateFile(filePath);
            Utils.Saving.WriteFile(filePath, encrypted);

            return filePath;
        }

        public static string? LoadTemp()
        {
            var files = Directory.GetFiles(tempFolderPath, $"*{extension}");
            int fileLen = files.Length;
            if (fileLen == 0)
                return null;

            var openedFiles = new HashSet<string>();

            var metaContents = File.ReadAllLines(tempMetadataPath);
            foreach (var line in metaContents)
                openedFiles.Add(line.Trim());

            var unprocessedFiles = files
                .Where(file => !openedFiles.Contains(Path.GetFileName(file)) && Path.GetFileName(file) != tempMetadataName)
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

            File.AppendAllText(tempMetadataPath, fileName + '\n');

            if (!File.Exists(file))
                return null;

            string contents = File.ReadAllText(file);
            return CryptManager.SetDecrypted(contents, fileName + file);
        }

        public static void Init()
        {
            Utils.Saving.CreateFolder(saveFolderName);
            Utils.Saving.CreateFolder(tempFolderName);

            Utils.Saving.CreateFile(Path.Combine(tempFolderName, tempMetadataName));

            Task.Run(() =>
            {
                string[] files = Directory.GetFiles(tempFolderPath, $"*{extension}");
                foreach (var file in files)
                {
                    var info = new FileInfo(file);

                    if (info.Name == tempMetadataName)
                        continue;

                    if (DateTime.Now - info.CreationTime > FILE_EXPIRE_TIME)
                        File.Delete(file);
                }
            });
        }
    }
}
