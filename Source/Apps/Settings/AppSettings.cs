using KindaGoodPrivacy.Source.Core.Settings;
using System;

namespace KindaGoodPrivacy
{
    public partial class AppSettings : Form
    {
        private int iterations = 0;
        private int memory = 0;
        private int degree = 0;

        public AppSettings()
        {
            InitializeComponent();
        }

        private void App_Load(object sender, EventArgs e)
        {
            IterationsVal.Text = Settings.Iterations.ToString();
            MemSizeVal.Text = Settings.MemorySize.ToString();
            ParallelismVal.Text = Settings.Parallelism.ToString();

            iterations = Settings.Iterations;
            memory = Settings.MemorySize;
            degree = Settings.Parallelism;
        }

        private void App_FormClosing(object sender, EventArgs e)
        {
            SaveButton_Click(sender, e);
        }

        private void IterationsVal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.IterationsVal.Text))
                return;

            if (string.IsNullOrWhiteSpace(this.IterationsVal.Text))
                return;

            if (!int.TryParse(this.IterationsVal.Text, out int val))
            {
                MessageBox.Show(
                    "You need to enter a number.",
                    "KGP - invalid iterations",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (val < 1 || val > (int)Math.Pow(2, 32) - 1)
            {
                MessageBox.Show(
                    "Number must be between 1 and " + ((int)Math.Pow(2, 32) - 1).ToString(),
                    "KGP - invalid iterations",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            iterations = val;
        }

        private void MemSizeVal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.MemSizeVal.Text))
                return;

            if (string.IsNullOrWhiteSpace(this.MemSizeVal.Text))
                return;

            if (!int.TryParse(this.MemSizeVal.Text, out int val))
            {
                MessageBox.Show(
                    "You need to enter a number.",
                    "KGP - invalid memory size",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (val < 8 || val > (int)Math.Pow(2, 32) - 1)
            {
                MessageBox.Show(
                    "Number must be between 8 and " + ((int)Math.Pow(2, 32) - 1).ToString(),
                    "KGP - invalid memory size",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            memory = val;
        }

        private void ParallelismVal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ParallelismVal.Text))
                return;

            if (string.IsNullOrWhiteSpace(this.ParallelismVal.Text))
                return;

            if (!int.TryParse(this.ParallelismVal.Text, out int val))
            {
                MessageBox.Show(
                    "You need to enter a number.",
                    "KGP - invalid degree of parallelism",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (val < 1 || val > Environment.ProcessorCount)
            {
                MessageBox.Show(
                    "Number must be between 1 and " + Environment.ProcessorCount,
                    "KGP - invalid degree of parallelism",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            degree = val;
        }

        private void SaveButton_Click(object sender, EventArgs e)
        {
            Settings.Iterations = iterations;
            Settings.MemorySize = memory;
            Settings.Parallelism = degree;

            Settings.Save();
        }
    }
}
