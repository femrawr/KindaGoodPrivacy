using Microsoft.VisualBasic;
using System.Drawing.Imaging;

namespace KindaGoodPrivacy.Source.Core
{
    public class Display
    {
        public static void LoadText(object sender, EventArgs e)
        {
            var (app, listView) = Utility.Form.FindFromListView(sender);
            if (app == null || listView == null)
            {
                return;
            }

            string filePath = listView.SelectedItems[0].Tag.ToString();
            Store.file = filePath;

            string text = "", decrypted = "";

            try
            {
                text = File.ReadAllText(filePath);
                decrypted = Utility.Crypt.DecryptAES(text, Store.key);
            }
            catch
            {
                app.TextDisplayRBox.Text = text;
            }

            app.TextDisplayRBox.Text = decrypted;
            app.OperationStatusLabel.Text = $"Viewing - {Store.file}";
        }

        public static void LoadImage(object sender, EventArgs e)
        {
            var (app, listView) = Utility.Form.FindFromListView(sender);
            if (app == null || listView == null)
            {
                return;
            }

            string filePath = listView.SelectedItems[0].Tag.ToString();
            Store.file = filePath;

            byte[] image = File.ReadAllBytes(filePath);

            try
            {
                using var stream = new MemoryStream(image);
                app.PictureDisplay.Image = Image.FromStream(stream);
            }
            catch (ArgumentException ex)
            {
                string data = File.ReadAllText(filePath);
                string decrypted = Utility.Crypt.DecryptAES(data, Store.key);

                byte[] decoded = Convert.FromBase64String(decrypted);

                using var stream = new MemoryStream(decoded);
                app.PictureDisplay.Image = Image.FromStream(stream);
            }
            catch (Exception)
            {
                app.OperationStatusLabel.Text = $"Corrupted or empty file - {Store.file}";
            }

            app.OperationStatusLabel.Text = $"Viewing - {Store.file}";
        }

        public static void LoadVideo(object sender, EventArgs e)
        {
            var (app, listView) = Utility.Form.FindFromListView(sender);
            if (app == null || listView == null)
            {
                return;
            }

            string filePath = listView.SelectedItems[0].Tag.ToString();
            Store.file = filePath;

            string tempPath = Path.Join(Store.path, "Temp");
            if (!Directory.Exists(tempPath))
            {
                return;
            }

            string fileName = Path.GetFileName(filePath);

            string realName = fileName.Replace(".kgpv", "");
            string videoPath = Path.Join(tempPath, realName);

            app.OperationStatusLabel.Text = "Loading video...";

            try
            {
                string data = File.ReadAllText(filePath);
                string decrypted = Utility.Crypt.DecryptAES(data, Store.key);

                byte[] decoded = Convert.FromBase64String(decrypted);

                File.WriteAllBytes(videoPath, decoded);

                app.VideoDisplay.URL = videoPath;
            }
            catch (Exception)
            {
                File.WriteAllBytes(videoPath, File.ReadAllBytes(filePath));
                app.VideoDisplay.URL = videoPath;
            }

            app.OperationStatusLabel.Text = $"Viewing - {Store.file}";
        }

        public static void NewFileText(object sender, EventArgs e)
        {
            var app = Utility.Form.FindFromButton(sender);
            if (app == null)
            {
                return;
            }

            string name = Interaction.InputBox(
                "The name of the new file",
                "New file"
            );

            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show(
                    "The name cannot be empty",
                    "New file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            string file = Path.Join(
                Store.path,
                Store.page,
                $"{name}.kgp{Store.page.ToLower()[0]}"
            );

            if (File.Exists(file))
            {
                MessageBox.Show(
                    "A file with this name already exists.",
                    "New file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                File.Create(file).Dispose();

                Store.file = file;
                app.OperationStatusLabel.Text = $"Created at - {Store.file}";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to create file: {ex.Message}",
                    "New file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            app.RefreshButton.PerformClick();
        }

        public static void NewFileCopy(object sender, EventArgs e)
        {
            var app = Utility.Form.FindFromButton(sender);
            if (app == null)
            {
                return;
            }

            using var dialog = new OpenFileDialog();
            dialog.Title = "Select a file";
            dialog.Filter = "All files (*.*)|*.*";

            if (dialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            string filePath = dialog.FileName;

            string newFile = Path.Join(
                Store.path,
                Store.page,
                $"{Path.GetFileName(filePath)}.kgp{Store.page.ToLower()[0]}"
            );

            Store.file = newFile;

            switch (Store.page)
            {
                case "image":
                    app.PictureDisplay.Image = Image.FromFile(filePath);
                    break;

                case "video":
                    string tempPath = Path.Join(Store.path, "Temp");
                    if (!Directory.Exists(tempPath))
                    {
                        return;
                    }

                    string video = Path.Join(tempPath, Path.GetFileName(filePath));

                    File.Copy(filePath, video, true);
                    app.VideoDisplay.URL = video;
                    break;
            }

            app.SaveButton.PerformClick();
            app.RefreshButton.PerformClick();
        }

        public static void SaveFile(object sender, EventArgs e)
        {
            var app = Utility.Form.FindFromButton(sender);
            if (app == null)
            {
                return;
            }

            string encrypted = "";

            switch (Store.page)
            {
                case "text":
                    encrypted = Utility.Crypt.EncryptAES(
                        app.TextDisplayRBox.Text,
                        Store.key
                    );

                    break;

                case "image":
                    byte[] image = [];

                    if (app.PictureDisplay.Image != null)
                    {
                        using var stream = new MemoryStream();
                        app.PictureDisplay.Image.Save(stream, ImageFormat.Png);
                        image = stream.ToArray();
                    }

                    string iEncoded = Convert.ToBase64String(image);
                    encrypted = Utility.Crypt.EncryptAES(iEncoded, Store.key);

                    break;

                case "video":
                    byte[] video = [];

                    if (!string.IsNullOrEmpty(app.VideoDisplay.URL))
                    {
                        video = File.ReadAllBytes(app.VideoDisplay.URL);
                    }

                    string vEncoded = Convert.ToBase64String(video);
                    encrypted = Utility.Crypt.EncryptAES(vEncoded, Store.key);

                    break;
            }

            File.WriteAllText(Store.file, encrypted);
            app.OperationStatusLabel.Text = $"Saved to - {Store.file}";
        }

        public static void DeleteFile(object sender, EventArgs e)
        {
            var app = Utility.Form.FindFromButton(sender);
            if (app == null)
            {
                return;
            }

            var result = MessageBox.Show(
                "Are you sure you want to delete this file?",
                "Delete File",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result != DialogResult.Yes)
            {
                return;
            }

            try
            {
                File.WriteAllText(Store.file, "");
                File.Delete(Store.file);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to create file: {ex.Message}",
                    "New file",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }

            app.OperationStatusLabel.Text = $"Deleted - {Store.file}";
            app.RefreshButton.PerformClick();
        }

        public static void RefreshFiles(object sender, EventArgs e)
        {
            var app = Utility.Form.FindFromButton(sender);
            if (app == null)
            {
                return;
            }

            app.TextDisplayRBox.Text = "";
            app.FileDisplayListView.Items.Clear();

            try
            {
                string[] files = Directory.GetFiles(
                    Path.Join(Store.path, Store.page),
                    $"*.kgp{Store.page.ToLower()[0]}"
                );

                foreach (string file in files)
                {
                    var info = new FileInfo(file);
                    var item = new ListViewItem(info.Name);
                    item.Tag = file;

                    app.FileDisplayListView.Items.Add(item);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Failed to load files: {ex.Message}",
                    "Refresh Text",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
        }
    }
}