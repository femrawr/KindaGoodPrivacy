namespace KindaGoodPrivacy
{
    partial class App
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(App));
            MainTextBox = new RichTextBox();
            EncryptButton = new Button();
            SaveButton = new Button();
            LoadButton = new Button();
            DecryptButton = new Button();
            SettingsButton = new Button();
            TextTab = new Button();
            ImgTab = new Button();
            MediaTextBox = new RichTextBox();
            MediaDisplay = new PictureBox();
            HelpButton = new Button();
            OptionsButton = new Button();
            ((System.ComponentModel.ISupportInitialize)MediaDisplay).BeginInit();
            SuspendLayout();
            // 
            // MainTextBox
            // 
            MainTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            MainTextBox.BackColor = SystemColors.ScrollBar;
            MainTextBox.BorderStyle = BorderStyle.FixedSingle;
            MainTextBox.Location = new Point(14, 50);
            MainTextBox.Name = "MainTextBox";
            MainTextBox.Size = new Size(1460, 715);
            MainTextBox.TabIndex = 0;
            MainTextBox.Text = " ";
            // 
            // EncryptButton
            // 
            EncryptButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            EncryptButton.BackColor = SystemColors.ScrollBar;
            EncryptButton.Location = new Point(1335, 772);
            EncryptButton.Name = "EncryptButton";
            EncryptButton.Size = new Size(138, 33);
            EncryptButton.TabIndex = 1;
            EncryptButton.Text = "Encrypt";
            EncryptButton.UseVisualStyleBackColor = false;
            EncryptButton.Click += EncryptButton_Click;
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            SaveButton.BackColor = SystemColors.ScrollBar;
            SaveButton.Location = new Point(14, 772);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(138, 33);
            SaveButton.TabIndex = 2;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = false;
            SaveButton.Click += SaveButton_Click;
            // 
            // LoadButton
            // 
            LoadButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            LoadButton.BackColor = SystemColors.ScrollBar;
            LoadButton.Location = new Point(159, 772);
            LoadButton.Name = "LoadButton";
            LoadButton.Size = new Size(138, 33);
            LoadButton.TabIndex = 3;
            LoadButton.Text = "Load";
            LoadButton.UseVisualStyleBackColor = false;
            LoadButton.Click += LoadButton_Click;
            // 
            // DecryptButton
            // 
            DecryptButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            DecryptButton.BackColor = SystemColors.ScrollBar;
            DecryptButton.Location = new Point(1190, 772);
            DecryptButton.Name = "DecryptButton";
            DecryptButton.Size = new Size(138, 33);
            DecryptButton.TabIndex = 4;
            DecryptButton.Text = "Decrypt";
            DecryptButton.UseVisualStyleBackColor = false;
            DecryptButton.Click += DecryptButton_Click;
            // 
            // SettingsButton
            // 
            SettingsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            SettingsButton.BackColor = SystemColors.ScrollBar;
            SettingsButton.Image = (Image)resources.GetObject("SettingsButton.Image");
            SettingsButton.Location = new Point(1438, 12);
            SettingsButton.Name = "SettingsButton";
            SettingsButton.Size = new Size(35, 33);
            SettingsButton.TabIndex = 5;
            SettingsButton.UseVisualStyleBackColor = false;
            SettingsButton.Click += SettingsButton_Click;
            // 
            // TextTab
            // 
            TextTab.Location = new Point(14, 12);
            TextTab.Name = "TextTab";
            TextTab.Size = new Size(94, 29);
            TextTab.TabIndex = 6;
            TextTab.Text = "Text";
            TextTab.UseVisualStyleBackColor = true;
            TextTab.Click += TextTab_Click;
            // 
            // ImgTab
            // 
            ImgTab.Location = new Point(114, 12);
            ImgTab.Name = "ImgTab";
            ImgTab.Size = new Size(94, 29);
            ImgTab.TabIndex = 7;
            ImgTab.Text = "Media";
            ImgTab.UseVisualStyleBackColor = true;
            ImgTab.Click += ImgTab_Click;
            // 
            // MediaTextBox
            // 
            MediaTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            MediaTextBox.BackColor = SystemColors.ScrollBar;
            MediaTextBox.BorderStyle = BorderStyle.FixedSingle;
            MediaTextBox.Enabled = false;
            MediaTextBox.Font = new Font("Segoe UI", 14F);
            MediaTextBox.Location = new Point(14, 51);
            MediaTextBox.Name = "MediaTextBox";
            MediaTextBox.ReadOnly = true;
            MediaTextBox.Size = new Size(720, 715);
            MediaTextBox.TabIndex = 8;
            MediaTextBox.Text = " ";
            MediaTextBox.Visible = false;
            // 
            // MediaDisplay
            // 
            MediaDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            MediaDisplay.Enabled = false;
            MediaDisplay.Location = new Point(733, 51);
            MediaDisplay.Name = "MediaDisplay";
            MediaDisplay.Size = new Size(740, 714);
            MediaDisplay.TabIndex = 9;
            MediaDisplay.TabStop = false;
            MediaDisplay.Visible = false;
            // 
            // HelpButton
            // 
            HelpButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            HelpButton.BackColor = SystemColors.ScrollBar;
            HelpButton.Image = (Image)resources.GetObject("HelpButton.Image");
            HelpButton.Location = new Point(1397, 12);
            HelpButton.Name = "HelpButton";
            HelpButton.Size = new Size(35, 33);
            HelpButton.TabIndex = 11;
            HelpButton.UseVisualStyleBackColor = false;
            // 
            // OptionsButton
            // 
            OptionsButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            OptionsButton.BackColor = SystemColors.ScrollBar;
            OptionsButton.Image = (Image)resources.GetObject("OptionsButton.Image");
            OptionsButton.Location = new Point(1356, 12);
            OptionsButton.Name = "OptionsButton";
            OptionsButton.Size = new Size(35, 33);
            OptionsButton.TabIndex = 12;
            OptionsButton.UseVisualStyleBackColor = false;
            // 
            // App
            // 
            AutoScaleDimensions = new SizeF(9F, 23F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.Control;
            ClientSize = new Size(1487, 814);
            Controls.Add(OptionsButton);
            Controls.Add(HelpButton);
            Controls.Add(MediaDisplay);
            Controls.Add(MediaTextBox);
            Controls.Add(ImgTab);
            Controls.Add(TextTab);
            Controls.Add(SettingsButton);
            Controls.Add(DecryptButton);
            Controls.Add(LoadButton);
            Controls.Add(SaveButton);
            Controls.Add(EncryptButton);
            Controls.Add(MainTextBox);
            Font = new Font("Segoe UI", 10F);
            Name = "App";
            Text = "Kinda Good Privacy";
            FormClosing += App_FormClosing;
            Load += App_Load;
            ((System.ComponentModel.ISupportInitialize)MediaDisplay).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private RichTextBox MainTextBox;
        private Button EncryptButton;
        private Button SaveButton;
        private Button LoadButton;
        private Button DecryptButton;
        private Button SettingsButton;
        private Button TextTab;
        private Button ImgTab;
        private RichTextBox MediaTextBox;
        private PictureBox MediaDisplay;
        private Button HelpButton;
        private Button OptionsButton;
    }
}
