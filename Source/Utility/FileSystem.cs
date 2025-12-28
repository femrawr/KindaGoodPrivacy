namespace KindaGoodPrivacy.Source.Utility
{
    public class FileSystem
    {
        private static readonly string mainFolderName = "KindaGoodPrivacy";
        private static readonly string accountsFolderName = "Accounts";
        private static readonly string accountsFileName = "Accounts.ini";

        public static string accountsFilePath = "";
        public static string accountsFolderPath = "";

        public static void Guard()
        {
            string mainFolderPath = Path.Join(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                mainFolderName
            );

            if (!Directory.Exists(mainFolderPath))
            {
                Directory.CreateDirectory(mainFolderPath);
            }

            accountsFilePath = Path.Join(
                mainFolderPath,
                accountsFileName
            );

            if (!File.Exists(accountsFilePath))
            {
                File.Create(accountsFilePath).Dispose();
            }

            accountsFolderPath = Path.Join(
                mainFolderPath,
                accountsFolderName
            );

            if (!Directory.Exists(accountsFolderPath))
            {
                Directory.CreateDirectory(accountsFolderPath);
            }
        }
    }
}