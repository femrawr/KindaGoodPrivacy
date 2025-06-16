using KindaGoodPrivacy.Source.Core.SaveManager;
using KindaGoodPrivacy.Source.Core.Settings;

namespace KindaGoodPrivacy.Source
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Utils.Saving.Guard();
            SaveManager.Init();

            Settings.Load();

            ApplicationConfiguration.Initialize();
            Application.Run(new App());
        }
    }
}