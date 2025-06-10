namespace KindaGoodPrivacy
{
    partial class AppSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            IterationsDesc = new Label();
            MainHashing = new Label();
            IterationsLabel = new Label();
            IterationsVal = new TextBox();
            SaveButton = new Button();
            ParallelismDesc = new Label();
            ParallelismLabel = new Label();
            ParallelismVal = new TextBox();
            MemSIzeDesc = new Label();
            MemorySizeLabel = new Label();
            MemSizeVal = new TextBox();
            SuspendLayout();
            // 
            // IterationsDesc
            // 
            IterationsDesc.AutoSize = true;
            IterationsDesc.ForeColor = Color.FromArgb(64, 64, 64);
            IterationsDesc.Location = new Point(12, 72);
            IterationsDesc.Name = "IterationsDesc";
            IterationsDesc.Size = new Size(279, 40);
            IterationsDesc.TabIndex = 13;
            IterationsDesc.Text = "number of times the hash is done,\r\nhigher number will make the hash slower";
            // 
            // MainHashing
            // 
            MainHashing.AutoSize = true;
            MainHashing.Font = new Font("Segoe UI Black", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MainHashing.Location = new Point(12, 9);
            MainHashing.Name = "MainHashing";
            MainHashing.Size = new Size(169, 31);
            MainHashing.TabIndex = 9;
            MainHashing.Text = "Main Hashing";
            // 
            // IterationsLabel
            // 
            IterationsLabel.AutoSize = true;
            IterationsLabel.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            IterationsLabel.Location = new Point(12, 49);
            IterationsLabel.Name = "IterationsLabel";
            IterationsLabel.Size = new Size(82, 23);
            IterationsLabel.TabIndex = 10;
            IterationsLabel.Text = "Iterations";
            // 
            // IterationsVal
            // 
            IterationsVal.Location = new Point(100, 49);
            IterationsVal.Name = "IterationsVal";
            IterationsVal.Size = new Size(116, 27);
            IterationsVal.TabIndex = 11;
            IterationsVal.Leave += IterationsVal_Leave;
            // 
            // SaveButton
            // 
            SaveButton.Location = new Point(12, 398);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(100, 40);
            SaveButton.TabIndex = 12;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            // 
            // ParallelismDesc
            // 
            ParallelismDesc.AutoSize = true;
            ParallelismDesc.ForeColor = Color.FromArgb(64, 64, 64);
            ParallelismDesc.Location = new Point(12, 202);
            ParallelismDesc.Name = "ParallelismDesc";
            ParallelismDesc.Size = new Size(248, 40);
            ParallelismDesc.TabIndex = 16;
            ParallelismDesc.Text = "number of CPU threaded used,\r\nhigher number will make hash faster";
            // 
            // ParallelismLabel
            // 
            ParallelismLabel.AutoSize = true;
            ParallelismLabel.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ParallelismLabel.Location = new Point(12, 173);
            ParallelismLabel.Name = "ParallelismLabel";
            ParallelismLabel.Size = new Size(151, 23);
            ParallelismLabel.TabIndex = 14;
            ParallelismLabel.Text = "Parallelism Degree";
            // 
            // ParallelismVal
            // 
            ParallelismVal.Location = new Point(169, 172);
            ParallelismVal.Name = "ParallelismVal";
            ParallelismVal.Size = new Size(116, 27);
            ParallelismVal.TabIndex = 15;
            ParallelismVal.Leave += ParallelismVal_Leave;
            // 
            // MemSIzeDesc
            // 
            MemSIzeDesc.AutoSize = true;
            MemSIzeDesc.ForeColor = Color.FromArgb(64, 64, 64);
            MemSIzeDesc.Location = new Point(12, 144);
            MemSIzeDesc.Name = "MemSIzeDesc";
            MemSIzeDesc.Size = new Size(247, 20);
            MemSIzeDesc.TabIndex = 19;
            MemSIzeDesc.Text = "amount of memory used in the hash";
            // 
            // MemorySizeLabel
            // 
            MemorySizeLabel.AutoSize = true;
            MemorySizeLabel.Font = new Font("Segoe UI Semibold", 10.2F, FontStyle.Bold, GraphicsUnit.Point, 0);
            MemorySizeLabel.Location = new Point(12, 121);
            MemorySizeLabel.Name = "MemorySizeLabel";
            MemorySizeLabel.Size = new Size(111, 23);
            MemorySizeLabel.TabIndex = 17;
            MemorySizeLabel.Text = "Memory Size";
            // 
            // MemSizeVal
            // 
            MemSizeVal.Location = new Point(129, 120);
            MemSizeVal.Name = "MemSizeVal";
            MemSizeVal.Size = new Size(116, 27);
            MemSizeVal.TabIndex = 18;
            MemSizeVal.Leave += MemSizeVal_Leave;
            // 
            // AppSettings
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(800, 450);
            Controls.Add(MemSIzeDesc);
            Controls.Add(MemorySizeLabel);
            Controls.Add(MemSizeVal);
            Controls.Add(ParallelismDesc);
            Controls.Add(ParallelismLabel);
            Controls.Add(ParallelismVal);
            Controls.Add(IterationsDesc);
            Controls.Add(MainHashing);
            Controls.Add(IterationsLabel);
            Controls.Add(IterationsVal);
            Controls.Add(SaveButton);
            Name = "AppSettings";
            Text = "App Settings";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label IterationsDesc;
        private Label MainHashing;
        private Label IterationsLabel;
        private TextBox IterationsVal;
        private Button SaveButton;
        private Label ParallelismDesc;
        private Label ParallelismLabel;
        private TextBox ParallelismVal;
        private Label MemSIzeDesc;
        private Label MemorySizeLabel;
        private TextBox MemSizeVal;
    }
}