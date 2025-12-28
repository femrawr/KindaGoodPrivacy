using KindaGoodPrivacy.Source.Core;

namespace KindaGoodPrivacy.Source.Utility
{
    public class Form
    {
        public static (MainApp?, ListView?) FindFromListView(object sender)
        {
            var listView = sender as ListView;
            if (listView == null || listView.SelectedItems.Count == 0)
            {
                return (null, null);
            }

            return (listView.FindForm() as MainApp, listView);
        }

        public static MainApp? FindFromButton(object sender)
        {
            var button = sender as Button;
            if (button == null)
            {
                return null;
            }

            return button.FindForm() as MainApp;
        }
    }
}
