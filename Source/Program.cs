using KindaGoodPrivacy.Source.Core.SaveManager;

namespace KindaGoodPrivacy.Source
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Utils.Saving.Guard();
            SaveManager.Init();

            ApplicationConfiguration.Initialize();
            Application.Run(new App());
        }
    }
}