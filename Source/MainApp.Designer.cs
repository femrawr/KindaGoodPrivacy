namespace KindaGoodPrivacy
{
    partial class MainApp
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainApp));
            UsernameLabel = new Label();
            PasswordBox = new TextBox();
            UsernameBox = new TextBox();
            PasswordLabel = new Label();
            LogInButton = new Button();
            StatusLabel = new Label();
            CreateAccountButton = new Button();
            TextDisplayRBox = new RichTextBox();
            FileDisplayListView = new ListView();
            RefreshButton = new Button();
            SaveButton = new Button();
            DeleteButton = new Button();
            NewButton = new Button();
            OperationStatusLabel = new Label();
            VideoTabButton = new Button();
            ImageTabButton = new Button();
            TextTabButton = new Button();
            PictureDisplay = new PictureBox();
            VideoDisplay = new AxWMPLib.AxWindowsMediaPlayer();
            ((System.ComponentModel.ISupportInitialize)PictureDisplay).BeginInit();
            ((System.ComponentModel.ISupportInitialize)VideoDisplay).BeginInit();
            SuspendLayout();
            // 
            // UsernameLabel
            // 
            UsernameLabel.AutoSize = true;
            UsernameLabel.Location = new Point(291, 178);
            UsernameLabel.Name = "UsernameLabel";
            UsernameLabel.Size = new Size(75, 20);
            UsernameLabel.TabIndex = 1;
            UsernameLabel.Text = "Username";
            // 
            // PasswordBox
            // 
            PasswordBox.Location = new Point(291, 280);
            PasswordBox.Name = "PasswordBox";
            PasswordBox.Size = new Size(288, 27);
            PasswordBox.TabIndex = 3;
            // 
            // UsernameBox
            // 
            UsernameBox.Location = new Point(291, 201);
            UsernameBox.Name = "UsernameBox";
            UsernameBox.Size = new Size(288, 27);
            UsernameBox.TabIndex = 4;
            // 
            // PasswordLabel
            // 
            PasswordLabel.AutoSize = true;
            PasswordLabel.Location = new Point(291, 257);
            PasswordLabel.Name = "PasswordLabel";
            PasswordLabel.Size = new Size(70, 20);
            PasswordLabel.TabIndex = 5;
            PasswordLabel.Text = "Password";
            // 
            // LogInButton
            // 
            LogInButton.Location = new Point(595, 280);
            LogInButton.Name = "LogInButton";
            LogInButton.Size = new Size(94, 29);
            LogInButton.TabIndex = 6;
            LogInButton.Text = "Log In";
            LogInButton.UseVisualStyleBackColor = true;
            LogInButton.Click += LogInButton_Click;
            // 
            // StatusLabel
            // 
            StatusLabel.AutoSize = true;
            StatusLabel.Font = new Font("Segoe UI", 10F);
            StatusLabel.Location = new Point(291, 353);
            StatusLabel.Name = "StatusLabel";
            StatusLabel.Size = new Size(0, 23);
            StatusLabel.TabIndex = 7;
            StatusLabel.Visible = false;
            // 
            // CreateAccountButton
            // 
            CreateAccountButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            CreateAccountButton.Location = new Point(12, 473);
            CreateAccountButton.Name = "CreateAccountButton";
            CreateAccountButton.Size = new Size(128, 29);
            CreateAccountButton.TabIndex = 8;
            CreateAccountButton.Text = "Create Account";
            CreateAccountButton.UseVisualStyleBackColor = true;
            CreateAccountButton.Click += CreateAccountButton_Click;
            // 
            // TextDisplayRBox
            // 
            TextDisplayRBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            TextDisplayRBox.Font = new Font("Segoe UI", 12F);
            TextDisplayRBox.Location = new Point(224, 42);
            TextDisplayRBox.Name = "TextDisplayRBox";
            TextDisplayRBox.Size = new Size(634, 460);
            TextDisplayRBox.TabIndex = 9;
            TextDisplayRBox.Text = "";
            TextDisplayRBox.Visible = false;
            // 
            // FileDisplayListView
            // 
            FileDisplayListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            FileDisplayListView.Font = new Font("Segoe UI", 11F);
            FileDisplayListView.Location = new Point(12, 42);
            FileDisplayListView.Name = "FileDisplayListView";
            FileDisplayListView.Size = new Size(206, 390);
            FileDisplayListView.TabIndex = 10;
            FileDisplayListView.UseCompatibleStateImageBehavior = false;
            FileDisplayListView.View = View.List;
            FileDisplayListView.Visible = false;
            // 
            // RefreshButton
            // 
            RefreshButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            RefreshButton.Location = new Point(12, 473);
            RefreshButton.Name = "RefreshButton";
            RefreshButton.Size = new Size(100, 29);
            RefreshButton.TabIndex = 11;
            RefreshButton.Text = "Refresh";
            RefreshButton.UseVisualStyleBackColor = true;
            RefreshButton.Visible = false;
            // 
            // SaveButton
            // 
            SaveButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            SaveButton.Location = new Point(118, 438);
            SaveButton.Name = "SaveButton";
            SaveButton.Size = new Size(100, 29);
            SaveButton.TabIndex = 12;
            SaveButton.Text = "Save";
            SaveButton.UseVisualStyleBackColor = true;
            SaveButton.Visible = false;
            // 
            // DeleteButton
            // 
            DeleteButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            DeleteButton.Location = new Point(118, 473);
            DeleteButton.Name = "DeleteButton";
            DeleteButton.Size = new Size(100, 29);
            DeleteButton.TabIndex = 13;
            DeleteButton.Text = "Delete";
            DeleteButton.UseVisualStyleBackColor = true;
            DeleteButton.Visible = false;
            // 
            // NewButton
            // 
            NewButton.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            NewButton.Location = new Point(12, 438);
            NewButton.Name = "NewButton";
            NewButton.Size = new Size(100, 29);
            NewButton.TabIndex = 14;
            NewButton.Text = "New";
            NewButton.UseVisualStyleBackColor = true;
            NewButton.Visible = false;
            // 
            // OperationStatusLabel
            // 
            OperationStatusLabel.AutoSize = true;
            OperationStatusLabel.Font = new Font("Segoe UI", 10F);
            OperationStatusLabel.Location = new Point(12, 11);
            OperationStatusLabel.Name = "OperationStatusLabel";
            OperationStatusLabel.Size = new Size(0, 23);
            OperationStatusLabel.TabIndex = 15;
            OperationStatusLabel.Visible = false;
            // 
            // VideoTabButton
            // 
            VideoTabButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            VideoTabButton.Location = new Point(758, 7);
            VideoTabButton.Name = "VideoTabButton";
            VideoTabButton.Size = new Size(100, 29);
            VideoTabButton.TabIndex = 16;
            VideoTabButton.Text = "Videos";
            VideoTabButton.UseVisualStyleBackColor = true;
            VideoTabButton.Visible = false;
            VideoTabButton.Click += VideoTabButton_Click;
            // 
            // ImageTabButton
            // 
            ImageTabButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ImageTabButton.Location = new Point(652, 7);
            ImageTabButton.Name = "ImageTabButton";
            ImageTabButton.Size = new Size(100, 29);
            ImageTabButton.TabIndex = 17;
            ImageTabButton.Text = "Images";
            ImageTabButton.UseVisualStyleBackColor = true;
            ImageTabButton.Visible = false;
            ImageTabButton.Click += ImageTabButton_Click;
            // 
            // TextTabButton
            // 
            TextTabButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            TextTabButton.Location = new Point(546, 9);
            TextTabButton.Name = "TextTabButton";
            TextTabButton.Size = new Size(100, 29);
            TextTabButton.TabIndex = 18;
            TextTabButton.Text = "Text";
            TextTabButton.UseVisualStyleBackColor = true;
            TextTabButton.Visible = false;
            TextTabButton.Click += TextTabButton_Click;
            // 
            // PictureDisplay
            // 
            PictureDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            PictureDisplay.Location = new Point(224, 42);
            PictureDisplay.Name = "PictureDisplay";
            PictureDisplay.Size = new Size(634, 460);
            PictureDisplay.TabIndex = 19;
            PictureDisplay.TabStop = false;
            PictureDisplay.Visible = false;
            // 
            // VideoDisplay
            // 
            VideoDisplay.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            VideoDisplay.Enabled = true;
            VideoDisplay.Location = new Point(224, 42);
            VideoDisplay.Name = "VideoDisplay";
            VideoDisplay.OcxState = (AxHost.State)resources.GetObject("VideoDisplay.OcxState");
            VideoDisplay.Size = new Size(634, 460);
            VideoDisplay.TabIndex = 20;
            VideoDisplay.Visible = false;
            // 
            // MainApp
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(870, 514);
            Controls.Add(VideoDisplay);
            Controls.Add(PictureDisplay);
            Controls.Add(TextTabButton);
            Controls.Add(ImageTabButton);
            Controls.Add(VideoTabButton);
            Controls.Add(OperationStatusLabel);
            Controls.Add(NewButton);
            Controls.Add(DeleteButton);
            Controls.Add(SaveButton);
            Controls.Add(RefreshButton);
            Controls.Add(FileDisplayListView);
            Controls.Add(TextDisplayRBox);
            Controls.Add(CreateAccountButton);
            Controls.Add(StatusLabel);
            Controls.Add(LogInButton);
            Controls.Add(PasswordLabel);
            Controls.Add(UsernameBox);
            Controls.Add(PasswordBox);
            Controls.Add(UsernameLabel);
            Font = new Font("Segoe UI", 9F);
            MaximizeBox = false;
            Name = "MainApp";
            Text = "Kinda Good Privacy";
            ((System.ComponentModel.ISupportInitialize)PictureDisplay).EndInit();
            ((System.ComponentModel.ISupportInitialize)VideoDisplay).EndInit();
            FormClosed += MainApp_FormClosed;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        public Label UsernameLabel;
        public TextBox PasswordBox;
        public TextBox UsernameBox;
        public Label PasswordLabel;
        public Button LogInButton;
        public Label StatusLabel;
        public Button CreateAccountButton;
        public RichTextBox TextDisplayRBox;
        public ListView FileDisplayListView;
        public Button RefreshButton;
        public Button SaveButton;
        public Button DeleteButton;
        public Button NewButton;
        public Label OperationStatusLabel;
        public Button VideoTabButton;
        public Button ImageTabButton;
        public Button TextTabButton;
        public PictureBox PictureDisplay;
        public AxWMPLib.AxWindowsMediaPlayer VideoDisplay;
    }
}
