namespace KindaGoodPrivacy.Source
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Utility.FileSystem.Guard();

            ApplicationConfiguration.Initialize();
            Application.Run(new MainApp());
        }
    }
}