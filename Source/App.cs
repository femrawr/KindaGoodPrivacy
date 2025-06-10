using KindaGoodPrivacy.Source.Core;
using Microsoft.VisualBasic;

namespace KindaGoodPrivacy
{
    public partial class App : Form
    {
        private string? lastSaved = null;

        public App()
        {
            InitializeComponent();
        }

        private void App_Load(object sender, EventArgs e)
        {
            string? loaded = SaveManager.LoadTemp();
            if (string.IsNullOrEmpty(loaded))
                return;

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

            if (lastSaved == MainTextBox.Text)
                return;

            SaveManager.SaveTemp(MainTextBox.Text);
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveManager.Save(MainTextBox.Text);
            lastSaved = MainTextBox.Text;
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            string? loaded = SaveManager.Load();
            if (loaded == null)
            {
                MessageBox.Show(
                    "No data was returned by the Save Manager.",
                    "KGP - Failed to load",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if (loaded == MainTextBox.Text)
            {
                MessageBox.Show(
                    "The file you have loaded has the same contents as the text box.",
                    "KGP - No change",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            MainTextBox.Text = loaded;
            lastSaved = loaded;
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
    }
}
