namespace KindaGoodPrivacy.Source.Core
{
    public class Pages
    {
        public static void SetHomePage(MainApp app, bool state)
        {
            app.CreateAccountButton.Visible = state;
            app.LogInButton.Visible = state;
            app.UsernameLabel.Visible = state;
            app.UsernameBox.Visible = state;
            app.PasswordLabel.Visible = state;
            app.PasswordBox.Visible = state;
            app.StatusLabel.Visible = state;

            app.TextTabButton.Visible = !state;
            app.ImageTabButton.Visible = !state;
            app.VideoTabButton.Visible = !state;

            app.OperationStatusLabel.Visible = !state;
            app.FileDisplayListView.Visible = !state;
            app.SaveButton.Visible = !state;
            app.NewButton.Visible = !state;
            app.DeleteButton.Visible = !state;
            app.RefreshButton.Visible = !state;

            if (!state)
            {
                app.SaveButton.Click += Display.SaveFile;
                app.DeleteButton.Click += Display.DeleteFile;
                app.RefreshButton.Click += Display.RefreshFiles;
            }
        }

        public static void SetTextPage(MainApp app, bool state)
        {
            if (state && Store.page != "text")
            {
                Store.page = "text";
                app.OperationStatusLabel.Text = "";

                app.PictureDisplay.Visible = false;
                app.VideoDisplay.Visible = false;

                app.TextDisplayRBox.Visible = true;

                app.NewButton.Click += Display.NewFileText;
                app.FileDisplayListView.Click += Display.LoadText;
                app.RefreshButton.PerformClick();
            }
            else if (!state)
            {
                app.TextDisplayRBox.Visible = false;
                app.NewButton.Click -= Display.NewFileText;
                app.FileDisplayListView.Click -= Display.LoadText;
            }
        }

        public static void SetImagePage(MainApp app, bool state)
        {
            if (state && Store.page != "image")
            {
                Store.page = "image";
                app.OperationStatusLabel.Text = "";

                app.VideoDisplay.Visible = false;
                app.TextDisplayRBox.Visible = true;

                app.PictureDisplay.Visible = true;

                app.NewButton.Click += Display.NewFileCopy;
                app.FileDisplayListView.Click += Display.LoadImage;
                app.RefreshButton.PerformClick();
            }
            else if (!state)
            {
                app.PictureDisplay.Visible = false;
                app.NewButton.Click -= Display.NewFileCopy;
                app.FileDisplayListView.Click -= Display.LoadImage;
            }
        }

        public static void SetVideoPage(MainApp app, bool state)
        {
            if (state && Store.page != "video")
            {
                Store.page = "video";

                if (!Utility.Warning.VideoPlayerWarning())
                {
                    return;
                }

                app.OperationStatusLabel.Text = "";

                app.TextDisplayRBox.Visible = false;
                app.PictureDisplay.Visible = false;

                app.VideoDisplay.Visible = true;

                app.NewButton.Click += Display.NewFileCopy;
                app.FileDisplayListView.Click += Display.LoadVideo;
                app.RefreshButton.PerformClick();
            }
            else if (!state)
            {
                app.VideoDisplay.Visible = false;
                app.VideoDisplay.Ctlcontrols.stop();
                app.VideoDisplay.URL = null;

                app.NewButton.Click -= Display.NewFileCopy;
                app.FileDisplayListView.Click -= Display.LoadVideo;
            }
        }
    }
}
