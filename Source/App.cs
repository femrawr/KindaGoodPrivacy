using KindaGoodPrivacy.Source.Core;
using Microsoft.VisualBasic;

namespace KindaGoodPrivacy
{
    public partial class App : Form
    {
        public App()
        {
            InitializeComponent();
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            SaveManager.Save(MainTextBox.Text);
        }

        private void LoadButton_Click(object sender, EventArgs e)
        {
            string? loaded = SaveManager.Load();
            if (loaded == null)
            {
                MessageBox.Show(
                    "No data was returned by the Save Manager.",
                    "Failed to load",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );

                return;
            }

            if (loaded == MainTextBox.Text)
            {
                MessageBox.Show(
                    "The file you have loaded has the same contents as the text box.",
                    "No Change",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            MainTextBox.Text = loaded;
        }

        private void EncryptButton_Click(object sender, EventArgs e)
        {
            string password = Interaction.InputBox(
                "Enter password to encrypt textbox contents",
                "KGP Cryptor"
            );

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Password cannot be empty.",
                    "KGP Cryptor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            string encrypted = CryptManager.SetEncrypted(MainTextBox.Text, password);
            MainTextBox.Text = encrypted;
        }

        private void DecryptButton_Click(object sender, EventArgs e)
        {
            string password = Interaction.InputBox(
                "Enter password to decrypt textbox contents",
                "KGP Cryptor"
            );

            if (string.IsNullOrEmpty(password))
            {
                MessageBox.Show(
                    "Password cannot be empty.",
                    "KGP Cryptor",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            string decrypted = CryptManager.SetDecrypted(MainTextBox.Text, password);
            MainTextBox.Text = decrypted;
        }
    }
}
