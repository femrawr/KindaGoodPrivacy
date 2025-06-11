using KindaGoodPrivacy.Source.Core;
using KindaGoodPrivacy.Source.Core.SaveManager;
using Microsoft.VisualBasic;
using System.IO;

namespace KindaGoodPrivacy
{
    public partial class App : Form
    {
        private string? lastSaved = null;
        private string? tab = "text";

        public App()
        {
            InitializeComponent();
        }

        private void App_Load(object sender, EventArgs e)
        {
            string? loaded = SaveManager.LoadTemp();
            if (string.IsNullOrEmpty(loaded))
                return;

            tab = "text";

            MainTextBox.Text = loaded;
            lastSaved = loaded;

            this.Activate();
            this.BringToFront();
            this.Focus();

            MainTextBox.Focus();
        }

        private void App_FormClosing(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MainTextBox.Text))
                return;

            if (string.IsNullOrWhiteSpace(MainTextBox.Text))
                return;

            if (lastSaved == MainTextBox.Text)
                return;

            SaveManager.SaveTemp(MainTextBox.Text);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            switch (tab)
            {
                case "text":
                    SaveManager.Save(MainTextBox.Text, Variables.SAVETYPE_TEXT);
                    lastSaved = MainTextBox.Text;
                    break;

                case "media":
                    SaveManager.Save(MediaTextBox.Text, Variables.SAVETYPE_IMG);
                    break;

                default:
                    break;
            }
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            switch (tab)
            {
                case "text":
                    string? tLoaded = SaveManager.Load(Variables.SAVETYPE_TEXT) as string;
                    if (tLoaded == null)
                    {
                        MessageBox.Show(
                            "No data was returned by the Save Manager.",
                            "KGP - Failed to load",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        return;
                    }

                    if (tLoaded == MainTextBox.Text)
                    {
                        MessageBox.Show(
                            "The file you have loaded has the same contents as the text box.",
                            "KGP - No change",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Information
                        );

                        return;
                    }

                    MainTextBox.Text = tLoaded;
                    lastSaved = tLoaded;
                    break;

                case "media":
                    byte[]? mLoaded = SaveManager.Load(Variables.SAVETYPE_IMG) as byte[];
                    if (mLoaded == null)
                    {
                        MessageBox.Show(
                            "No data was returned by the Save Manager.",
                            "KGP - Failed to load",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        return;
                    }

                    using (var stream = new MemoryStream(mLoaded))
                        MediaDisplay.Image = Image.FromStream(stream);

                    string str = BitConverter.ToString(mLoaded).Replace("-", " ");
                    MediaTextBox.Text = str;
                    break;

                default:
                    break;
            }
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            string password = Interaction.InputBox(
                "Enter password to encrypt textbox contents",
                "KGP - Encryptor"
            );

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Password cannot be empty.",
                    "KGP - Encryptor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            string encrypted = CryptManager.SetEncrypted(MainTextBox.Text, password);
            MainTextBox.Text = encrypted;
            lastSaved = encrypted;
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            string password = Interaction.InputBox(
                "Enter password to decrypt textbox contents",
                "KGP - Decryptor"
            );

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Password cannot be empty.",
                    "KGP - Decryptor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            string decrypted = CryptManager.SetDecrypted(MainTextBox.Text, password);
            MainTextBox.Text = decrypted;
            lastSaved = decrypted;
        }

        private void SettingsButton_Click(object sender, EventArgs e)
        {
            using var settings = new AppSettings();
            settings.ShowDialog();
        }

        private void TextTab_Click(object sender, EventArgs e)
        {
            MediaDisplay.Enabled = false;
            MediaDisplay.Visible = false;

            MediaTextBox.Enabled = false;
            MediaTextBox.Visible = false;

            MainTextBox.Enabled = true;
            MainTextBox.Visible = true;
            MainTextBox.Focus();

            tab = "text";
        }

        private void ImgTab_Click(object sender, EventArgs e)
        {
            MainTextBox.Enabled = false;
            MainTextBox.Visible = false;

            MediaDisplay.Enabled = true;
            MediaDisplay.Visible = true;

            MediaTextBox.Enabled = true;
            MediaTextBox.Visible = true;
            MediaTextBox.Focus();

            tab = "media";

#if DEBUG
            string path = "C:\\Users\\choc\\Pictures\\mario.png";

            byte[] bytes = File.ReadAllBytes(path);
            string str = BitConverter.ToString(bytes).Replace("-", " ");

            using (var stream = new MemoryStream(bytes))
            MediaDisplay.Image = Image.FromStream(stream);

            MediaTextBox.Text = str;
#endif
        }
    }
}
