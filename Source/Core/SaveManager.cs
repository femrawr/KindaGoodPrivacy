namespace KindaGoodPrivacy.Source.Core
{
    public class SaveManager
    {
        private static readonly string saveFolderName = "TextSaves";
        private static readonly string extension = ".txt";

        private static readonly string saveFolderPath = Path.Combine(
            Utils.Saving.mainFolderPath, saveFolderName
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
            openFile.Filter = "Text Files (*.txt)|*.txt";

            if (openFile.ShowDialog() != DialogResult.OK)
                return null;

            string filePath = openFile.FileName;

            if (File.Exists(filePath))
                return File.ReadAllText(filePath);

            return null;
        }

        public static void Init()
        {
            Utils.Saving.CreateFolder(saveFolderName);
        }
    }
}
