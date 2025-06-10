namespace KindaGoodPrivacy
{
    public partial class AppSettings : Form
    {
        public AppSettings()
        {
            InitializeComponent();
        }

        private void IterationsVal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.IterationsVal.Text))
                return;

            if (string.IsNullOrWhiteSpace(this.IterationsVal.Text))
                return;

            if (!int.TryParse(this.IterationsVal.Text, out int iterations))
            {
                MessageBox.Show(
                    "You need to enter a number.",
                    "KGP - invalid setting value",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (iterations < 1 || iterations > (int)Math.Pow(2, 32) - 1)
            {
                MessageBox.Show(
                    "number must be between 1 and " + ((int)Math.Pow(2, 32) - 1).ToString(),
                    "KGP - invalid setting value",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // todo
        }

        private void MemSizeVal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.MemSizeVal.Text))
                return;

            if (string.IsNullOrWhiteSpace(this.MemSizeVal.Text))
                return;

            if (!int.TryParse(this.MemSizeVal.Text, out int memory))
            {
                MessageBox.Show(
                    "You need to enter a number.",
                    "KGP - invalid setting value",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (memory < 8 || memory > (int)Math.Pow(2, 32) - 1)
            {
                MessageBox.Show(
                    "number must be between 8 and " + ((int)Math.Pow(2, 32) - 1).ToString(),
                    "KGP - invalid setting value",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // todo
        }

        private void ParallelismVal_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.ParallelismVal.Text))
                return;

            if (string.IsNullOrWhiteSpace(this.ParallelismVal.Text))
                return;

            if (!int.TryParse(this.ParallelismVal.Text, out int degree))
            {
                MessageBox.Show(
                    "You need to enter a number.",
                    "KGP - invalid setting value",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (degree < 1 || degree > Environment.ProcessorCount)
            {
                MessageBox.Show(
                    "number must be between 1 and " + Environment.ProcessorCount,
                    "KGP - invalid setting value",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            // todo
        }
    }
}
