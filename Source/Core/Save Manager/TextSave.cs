namespace KindaGoodPrivacy.Source.Core.SaveManager
{
    internal class TextSave
    {
        public static void SaveText(string data)
        {
            using var saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Variables.textFolderPath;
            saveFile.FileName = Utils.Util.GenString(10) + Variables.extension;
            saveFile.DefaultExt = Variables.extension;

            if (saveFile.ShowDialog() != DialogResult.OK)
                return;

            string filePath = saveFile.FileName;
            Utils.Saving.CreateFile(filePath);
            Utils.Saving.WriteFile(filePath, data);
        }

        public static (string?, bool) LoadText()
        {
            using var openFile = new OpenFileDialog();
            openFile.InitialDirectory = Variables.textFolderPath;
            openFile.Filter = $"Text Files (*{Variables.extension})|*{Variables.extension}";

            if (openFile.ShowDialog() != DialogResult.OK)
                return (null, false);

            string filePath = openFile.FileName;

            if (File.Exists(filePath))
                return (File.ReadAllText(filePath), true);

            return (null, true);
        }
    }
}
