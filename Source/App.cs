using KindaGoodPrivacy.Source.Core;
using KindaGoodPrivacy.Source.Core.SaveManager;
using KindaGoodPrivacy.Source.Utils.Crypto;
using Microsoft.VisualBasic;
using System.Text;

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
            tab = "text";

            Activate();
            BringToFront();
            Focus();

            MainTextBox.Text = "";
            MainTextBox.Focus();

            string? loaded = SaveManager.LoadTemp();
            if (!string.IsNullOrEmpty(loaded))
            {
                MainTextBox.Text = loaded;
                lastSaved = FastCrypt.Hash(loaded);
            }
        }

        private void App_FormClosing(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(MainTextBox.Text))
                return;

            if (string.IsNullOrWhiteSpace(MainTextBox.Text))
                return;

            if (lastSaved == FastCrypt.Hash(MainTextBox.Text))
                return;

            SaveManager.SaveTemp(MainTextBox.Text);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            switch (tab)
            {
                case "text":
                    SaveManager.Save(MainTextBox.Text, Variables.SAVETYPE_TEXT);
                    lastSaved = FastCrypt.Hash(MainTextBox.Text);
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
                    lastSaved = FastCrypt.Hash(tLoaded);
                    break;

                case "media":
                    MediaDisplay.Image = null;
                    MediaTextBox.Text = "";

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

                    try
                    {
                        using var stream = new MemoryStream(mLoaded);
                        MediaDisplay.Image = Image.FromStream(stream);

                        string str = Convert.ToBase64String(mLoaded);
                        MediaTextBox.Text = str;
                    }
                    catch (Exception)
                    {
                        string str = Encoding.UTF8.GetString(mLoaded);
                        MediaTextBox.Text = str;
                    }

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

            switch (tab)
            {
                case "text":
                    string tEncrypted = CryptManager.SetEncrypted(MainTextBox.Text, password);
                    MainTextBox.Text = tEncrypted;
                    lastSaved = FastCrypt.Hash(tEncrypted);
                    break;

                case "media":
                    string mEncrypted = CryptManager.SetEncrypted(MediaTextBox.Text, password);
                    MediaTextBox.Text = mEncrypted;
                    break;

                default:
                    break;
            }
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

            switch (tab) // TODO
            {
                case "text":
                    string tDecrypted = CryptManager.SetDecrypted(MainTextBox.Text, password);
                    MainTextBox.Text = tDecrypted;
                    lastSaved = FastCrypt.Hash(tDecrypted);
                    break;

                case "media":
                    string mDecrypted = CryptManager.SetDecrypted(MediaTextBox.Text, password);
                    MediaTextBox.Text = mDecrypted;

                    using (var stream = new MemoryStream(Convert.FromBase64String(mDecrypted)))
                        MediaDisplay.Image = Image.FromStream(stream);
                    break;

                default:
                    break;
            }
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
            string str = Convert.ToBase64String(bytes);

            using (var stream = new MemoryStream(bytes))
            MediaDisplay.Image = Image.FromStream(stream);

            MediaTextBox.Text = str;
#endif
        }
    }
}
