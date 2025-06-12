namespace KindaGoodPrivacy.Source.Core.SaveManager
{
    internal class ImageSave
    {
        public static void SaveImg(string data)
        {
            using var saveFile = new SaveFileDialog();
            saveFile.InitialDirectory = Variables.imgFolderPath;
            saveFile.FileName = Utils.Util.GenString(10) + Variables.extension;
            saveFile.DefaultExt = Variables.extension;

            if (saveFile.ShowDialog() != DialogResult.OK)
                return;

            string filePath = saveFile.FileName;
            Utils.Saving.CreateFile(filePath);
            Utils.Saving.WriteFile(filePath, data);
        }

        public static byte[]? LoadImg()
        {
            using var openFile = new OpenFileDialog();
            openFile.InitialDirectory = Variables.imgFolderPath;
            openFile.Filter = $"All Files|*.*";

            if (openFile.ShowDialog() != DialogResult.OK)
                return null;

            string filePath = openFile.FileName;

            if (File.Exists(filePath))
                return File.ReadAllBytes(filePath);

            return null;
        }
    }
}
