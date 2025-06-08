namespace KindaGoodPrivacy.Source.Utils
{
    public class Saving
    {
        private static readonly string mainFolderName = "Kinda Good Privacy";
        public static readonly string mainFolderPath = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
            mainFolderName
        );

        public static void Guard()
        {
            if (Directory.Exists(mainFolderPath))
                return;

            Directory.CreateDirectory(mainFolderPath);
        }

        public static string CreateFolder(string name)
        {
            Guard();

            string path = Path.Combine(mainFolderPath, name);

            if (Directory.Exists(path))
                return path;

            Directory.CreateDirectory(path);
            return path;
        }

        public static string CreateFile(string name)
        {
            Guard();

            string path = Path.Combine(mainFolderPath, name);

            if (File.Exists(path))
                return path;

            using (File.Create(path)) { };
            return path;
        }

        public static bool WriteFile(string name, string content)
        {
            Guard();

            string path = Path.Combine(mainFolderPath, name);

            if (!File.Exists(path))
                return false;

            File.WriteAllText(path, content);
            return true;
        }
    }
}
