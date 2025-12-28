using System.Diagnostics;

namespace KindaGoodPrivacy
{
    public partial class MainApp : Form
    {
        public MainApp()
        {
            InitializeComponent();
        }

        private void MainApp_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (string.IsNullOrEmpty(Source.Core.Store.path))
            {
                return;
            }

            string tempPath = Path.Join(Source.Core.Store.path, "Temp");
            if (!Directory.Exists(tempPath))
            {
                return;
            }

            VideoDisplay.URL = null;

            foreach (var file in Directory.GetFiles(tempPath))
            {
                File.WriteAllText(file, "");
                File.Delete(file);
            }

            var root = Path.GetPathRoot(tempPath);

            Task.Run(() =>
            {
                using var cipher = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = "cipher.exe",
                        Arguments = $"/w:\"{root}\"",
                        CreateNoWindow = true,
                        UseShellExecute = false
                    }
                };

                cipher.Start();
                cipher.WaitForExit();
            });
        }

        private void CreateAccountButton_Click(object sender, EventArgs e)
        {
            StatusLabel.Text = "";

            if (CreateAccountButton.Text == "Create Account")
            {
                LogInButton.Text = "Create";
                CreateAccountButton.Text = "Log in";
            }
            else
            {
                LogInButton.Text = "Log In";
                CreateAccountButton.Text = "Create Account";
            }
        }

        private void LogInButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(UsernameBox.Text) || string.IsNullOrEmpty(PasswordBox.Text))
            {
                return;
            }

            StatusLabel.Visible = true;
            LogInButton.Enabled = false;

            if (LogInButton.Text == "Log In")
            {
                StatusLabel.Text = "Logging in...";

                string? status = Source.Core.LogIn.Login(
                    UsernameBox.Text,
                    PasswordBox.Text
                );

                if (status == null)
                {
                    this.MaximizeBox = true;
                    this.WindowState = FormWindowState.Maximized;

                    UsernameBox.Text = "";
                    PasswordBox.Text = "";

                    Source.Core.Pages.SetHomePage(this, false);
                    Source.Core.Pages.SetTextPage(this, true);
                    return;
                }

                StatusLabel.Text = status;
                LogInButton.Enabled = true;
            }
            else
            {
                StatusLabel.Text = "Creating account...";

                string? status = Source.Core.Register.Create(
                    UsernameBox.Text,
                    PasswordBox.Text
                );

                if (status == null)
                {
                    LogInButton.Text = "Log In";
                    CreateAccountButton.Text = "Create Account";

                    StatusLabel.Text = "Account created.";
                    LogInButton.Enabled = true;
                    return;
                }

                StatusLabel.Text = status;
                LogInButton.Enabled = true;
            }
        }

        private void TextTabButton_Click(object sender, EventArgs e)
        {
            Source.Core.Pages.SetImagePage(this, false);
            Source.Core.Pages.SetVideoPage(this, false);

            Source.Core.Pages.SetTextPage(this, true);
        }

        private void ImageTabButton_Click(object sender, EventArgs e)
        {
            Source.Core.Pages.SetTextPage(this, false);
            Source.Core.Pages.SetVideoPage(this, false);

            Source.Core.Pages.SetImagePage(this, true);
        }

        private void VideoTabButton_Click(object sender, EventArgs e)
        {
            Source.Core.Pages.SetTextPage(this, false);
            Source.Core.Pages.SetImagePage(this, false);

            Source.Core.Pages.SetVideoPage(this, true);
        }
    }
}