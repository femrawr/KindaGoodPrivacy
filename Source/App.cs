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

                case "image":
                    SaveManager.Save(ImageTextBox.Text, Variables.SAVETYPE_IMG);
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
                    (
                        object? tLoadedObj,
                        bool tSuccess
                    ) = SaveManager.Load(Variables.SAVETYPE_TEXT);

                    if (tLoadedObj == null && tSuccess)
                    {
                        MessageBox.Show(
                            "No data was returned by the Save Manager.",
                            "KGP - Failed to load",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        return;
                    }

                    if (!tSuccess) return;

                    string tLoaded = tLoadedObj as string;

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

                case "image":
                    ImageDisplay.Image = null;
                    ImageTextBox.Text = "";

                    (
                        object? iLoadedObj,
                        bool iSuccess
                    ) = SaveManager.Load(Variables.SAVETYPE_IMG);

                    if (iLoadedObj == null && iSuccess)
                    {
                        MessageBox.Show(
                            "No data was returned by the Save Manager.",
                            "KGP - Failed to load",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );

                        return;
                    }

                    if (!iSuccess) return;

                    byte[] iLoaded = iLoadedObj as byte[];

                    try
                    {
                        using var stream = new MemoryStream(iLoaded);
                        ImageDisplay.Image = Image.FromStream(stream);

                        string str = Convert.ToBase64String(iLoaded);
                        ImageTextBox.Text = str;
                    }
                    catch (Exception)
                    {
                        string str = Encoding.UTF8.GetString(iLoaded);
                        ImageTextBox.Text = str;
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

                case "image":
                    string mEncrypted = CryptManager.SetEncrypted(ImageTextBox.Text, password);
                    ImageTextBox.Text = mEncrypted;
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

            switch (tab)
            {
                case "text":
                    (string tDecrypted, bool tSuccess) = CryptManager.SetDecrypted(
                        MainTextBox.Text,
                        password
                    );

                    if (!tSuccess) return;

                    MainTextBox.Text = tDecrypted;
                    lastSaved = FastCrypt.Hash(tDecrypted);
                    break;

                case "image":
                    (string iDecrypted, bool iSuccess) = CryptManager.SetDecrypted(
                        ImageTextBox.Text,
                        password
                    );

                    if (!iSuccess) return;

                    ImageTextBox.Text = iDecrypted;

                    using (var stream = new MemoryStream(Convert.FromBase64String(iDecrypted)))
                        ImageDisplay.Image = Image.FromStream(stream);

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
            ImageDisplay.Enabled = false;
            ImageDisplay.Visible = false;

            ImageTextBox.Enabled = false;
            ImageTextBox.Visible = false;

            MainTextBox.Enabled = true;
            MainTextBox.Visible = true;
            MainTextBox.Focus();

            tab = "text";
        }

        private void ImgTab_Click(object sender, EventArgs e)
        {
            MainTextBox.Enabled = false;
            MainTextBox.Visible = false;

            ImageDisplay.Enabled = true;
            ImageDisplay.Visible = true;

            ImageTextBox.Enabled = true;
            ImageTextBox.Visible = true;
            ImageTextBox.Focus();

            tab = "image";
        }
    }
}
