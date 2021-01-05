
namespace SteamFriendSearchApp
{
    partial class FriendSearchForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FriendSearchForm));
            this.userStatusGroupBox = new System.Windows.Forms.GroupBox();
            this.userStatusComboBox = new System.Windows.Forms.ComboBox();
            this.profilePicGroupBox = new System.Windows.Forms.GroupBox();
            this.imageSimGroupBox = new System.Windows.Forms.GroupBox();
            this.thresholdLabel = new System.Windows.Forms.Label();
            this.thresholdTrackBar = new System.Windows.Forms.TrackBar();
            this.selectProfilePicButton = new System.Windows.Forms.Button();
            this.profileCustomRadioButton = new System.Windows.Forms.RadioButton();
            this.profileDefaultRadioButton = new System.Windows.Forms.RadioButton();
            this.profileAnyRadioButton = new System.Windows.Forms.RadioButton();
            this.profilePictureBox = new System.Windows.Forms.PictureBox();
            this.gameInfoGroupBox = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.inGameCheckBox = new System.Windows.Forms.CheckBox();
            this.appIdLabel = new System.Windows.Forms.Label();
            this.appIdNumericUpDown = new System.Windows.Forms.NumericUpDown();
            this.recentGamesCheckBox = new System.Windows.Forms.CheckBox();
            this.recentGamesListBox = new System.Windows.Forms.ListBox();
            this.usernameGroupBox = new System.Windows.Forms.GroupBox();
            this.compareAkaCheckBox = new System.Windows.Forms.CheckBox();
            this.compareUsernamesCheckBox = new System.Windows.Forms.CheckBox();
            this.caseSensitiveCheckBox = new System.Windows.Forms.CheckBox();
            this.usernameTextBox = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.recentGamesContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.removeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.startButton = new System.Windows.Forms.Button();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.threadsTrackBar = new System.Windows.Forms.TrackBar();
            this.threadsLabel = new System.Windows.Forms.Label();
            this.matchedUsersListBox = new System.Windows.Forms.ListBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.statusTimer = new System.Windows.Forms.Timer(this.components);
            this.foundUsersContextMenuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copySelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.copyAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.userStatusGroupBox.SuspendLayout();
            this.profilePicGroupBox.SuspendLayout();
            this.imageSimGroupBox.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).BeginInit();
            this.gameInfoGroupBox.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appIdNumericUpDown)).BeginInit();
            this.usernameGroupBox.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.recentGamesContextMenuStrip.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.threadsTrackBar)).BeginInit();
            this.foundUsersContextMenuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // userStatusGroupBox
            // 
            this.userStatusGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.userStatusGroupBox.Controls.Add(this.userStatusComboBox);
            this.userStatusGroupBox.Location = new System.Drawing.Point(205, 3);
            this.userStatusGroupBox.MinimumSize = new System.Drawing.Size(133, 52);
            this.userStatusGroupBox.Name = "userStatusGroupBox";
            this.userStatusGroupBox.Size = new System.Drawing.Size(150, 52);
            this.userStatusGroupBox.TabIndex = 1;
            this.userStatusGroupBox.TabStop = false;
            this.userStatusGroupBox.Text = "User Status";
            // 
            // userStatusComboBox
            // 
            this.userStatusComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.userStatusComboBox.FormattingEnabled = true;
            this.userStatusComboBox.Items.AddRange(new object[] {
            "ANY",
            "Offline",
            "Online",
            "Busy",
            "Away",
            "Snooze",
            "Looking to Trade",
            "Looking to Play"});
            this.userStatusComboBox.Location = new System.Drawing.Point(6, 19);
            this.userStatusComboBox.Name = "userStatusComboBox";
            this.userStatusComboBox.Size = new System.Drawing.Size(121, 21);
            this.userStatusComboBox.TabIndex = 2;
            this.userStatusComboBox.SelectedIndexChanged += new System.EventHandler(this.userStatusComboBox_SelectedIndexChanged);
            // 
            // profilePicGroupBox
            // 
            this.profilePicGroupBox.AutoSize = true;
            this.profilePicGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.profilePicGroupBox.Controls.Add(this.imageSimGroupBox);
            this.profilePicGroupBox.Controls.Add(this.selectProfilePicButton);
            this.profilePicGroupBox.Controls.Add(this.profileCustomRadioButton);
            this.profilePicGroupBox.Controls.Add(this.profileDefaultRadioButton);
            this.profilePicGroupBox.Controls.Add(this.profileAnyRadioButton);
            this.profilePicGroupBox.Controls.Add(this.profilePictureBox);
            this.profilePicGroupBox.Location = new System.Drawing.Point(3, 3);
            this.profilePicGroupBox.MaximumSize = new System.Drawing.Size(196, 0);
            this.profilePicGroupBox.MinimumSize = new System.Drawing.Size(196, 0);
            this.profilePicGroupBox.Name = "profilePicGroupBox";
            this.profilePicGroupBox.Size = new System.Drawing.Size(196, 396);
            this.profilePicGroupBox.TabIndex = 2;
            this.profilePicGroupBox.TabStop = false;
            this.profilePicGroupBox.Text = "Profile Picture";
            // 
            // imageSimGroupBox
            // 
            this.imageSimGroupBox.Controls.Add(this.thresholdLabel);
            this.imageSimGroupBox.Controls.Add(this.thresholdTrackBar);
            this.imageSimGroupBox.Location = new System.Drawing.Point(6, 278);
            this.imageSimGroupBox.Name = "imageSimGroupBox";
            this.imageSimGroupBox.Size = new System.Drawing.Size(184, 70);
            this.imageSimGroupBox.TabIndex = 5;
            this.imageSimGroupBox.TabStop = false;
            this.imageSimGroupBox.Text = "Image Similarity";
            this.imageSimGroupBox.Visible = false;
            // 
            // thresholdLabel
            // 
            this.thresholdLabel.AutoSize = true;
            this.thresholdLabel.Location = new System.Drawing.Point(6, 51);
            this.thresholdLabel.Name = "thresholdLabel";
            this.thresholdLabel.Size = new System.Drawing.Size(74, 13);
            this.thresholdLabel.TabIndex = 6;
            this.thresholdLabel.Text = "Threshold: 0%";
            // 
            // thresholdTrackBar
            // 
            this.thresholdTrackBar.Location = new System.Drawing.Point(6, 19);
            this.thresholdTrackBar.Maximum = 100;
            this.thresholdTrackBar.Name = "thresholdTrackBar";
            this.thresholdTrackBar.Size = new System.Drawing.Size(172, 45);
            this.thresholdTrackBar.TabIndex = 5;
            this.thresholdTrackBar.TickFrequency = 10;
            this.thresholdTrackBar.Scroll += new System.EventHandler(this.thresholdTrackBar_Scroll);
            // 
            // selectProfilePicButton
            // 
            this.selectProfilePicButton.Location = new System.Drawing.Point(6, 354);
            this.selectProfilePicButton.Name = "selectProfilePicButton";
            this.selectProfilePicButton.Size = new System.Drawing.Size(184, 23);
            this.selectProfilePicButton.TabIndex = 3;
            this.selectProfilePicButton.Text = "Select Profile Picture";
            this.selectProfilePicButton.UseVisualStyleBackColor = true;
            this.selectProfilePicButton.Visible = false;
            this.selectProfilePicButton.Click += new System.EventHandler(this.selectProfilePicButton_Click);
            // 
            // profileCustomRadioButton
            // 
            this.profileCustomRadioButton.AutoSize = true;
            this.profileCustomRadioButton.Location = new System.Drawing.Point(6, 65);
            this.profileCustomRadioButton.Name = "profileCustomRadioButton";
            this.profileCustomRadioButton.Size = new System.Drawing.Size(60, 17);
            this.profileCustomRadioButton.TabIndex = 3;
            this.profileCustomRadioButton.Text = "Custom";
            this.profileCustomRadioButton.UseVisualStyleBackColor = true;
            this.profileCustomRadioButton.CheckedChanged += new System.EventHandler(this.profileCustomRadioButton_CheckedChanged);
            // 
            // profileDefaultRadioButton
            // 
            this.profileDefaultRadioButton.AutoSize = true;
            this.profileDefaultRadioButton.Location = new System.Drawing.Point(6, 42);
            this.profileDefaultRadioButton.Name = "profileDefaultRadioButton";
            this.profileDefaultRadioButton.Size = new System.Drawing.Size(59, 17);
            this.profileDefaultRadioButton.TabIndex = 2;
            this.profileDefaultRadioButton.Text = "Default";
            this.profileDefaultRadioButton.UseVisualStyleBackColor = true;
            this.profileDefaultRadioButton.CheckedChanged += new System.EventHandler(this.profileDefaultRadioButton_CheckedChanged);
            // 
            // profileAnyRadioButton
            // 
            this.profileAnyRadioButton.AutoSize = true;
            this.profileAnyRadioButton.Checked = true;
            this.profileAnyRadioButton.Location = new System.Drawing.Point(6, 19);
            this.profileAnyRadioButton.Name = "profileAnyRadioButton";
            this.profileAnyRadioButton.Size = new System.Drawing.Size(43, 17);
            this.profileAnyRadioButton.TabIndex = 1;
            this.profileAnyRadioButton.TabStop = true;
            this.profileAnyRadioButton.Text = "Any";
            this.profileAnyRadioButton.UseVisualStyleBackColor = true;
            this.profileAnyRadioButton.CheckedChanged += new System.EventHandler(this.profileAnyRadioButton_CheckedChanged);
            // 
            // profilePictureBox
            // 
            this.profilePictureBox.Image = ((System.Drawing.Image)(resources.GetObject("profilePictureBox.Image")));
            this.profilePictureBox.Location = new System.Drawing.Point(6, 88);
            this.profilePictureBox.Name = "profilePictureBox";
            this.profilePictureBox.Size = new System.Drawing.Size(184, 184);
            this.profilePictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.profilePictureBox.TabIndex = 0;
            this.profilePictureBox.TabStop = false;
            this.profilePictureBox.Visible = false;
            // 
            // gameInfoGroupBox
            // 
            this.gameInfoGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gameInfoGroupBox.AutoSize = true;
            this.gameInfoGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.gameInfoGroupBox.Controls.Add(this.flowLayoutPanel2);
            this.gameInfoGroupBox.Location = new System.Drawing.Point(205, 61);
            this.gameInfoGroupBox.MinimumSize = new System.Drawing.Size(133, 0);
            this.gameInfoGroupBox.Name = "gameInfoGroupBox";
            this.gameInfoGroupBox.Size = new System.Drawing.Size(150, 224);
            this.gameInfoGroupBox.TabIndex = 3;
            this.gameInfoGroupBox.TabStop = false;
            this.gameInfoGroupBox.Text = "Game Info";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.AutoSize = true;
            this.flowLayoutPanel2.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel2.Controls.Add(this.inGameCheckBox);
            this.flowLayoutPanel2.Controls.Add(this.appIdLabel);
            this.flowLayoutPanel2.Controls.Add(this.appIdNumericUpDown);
            this.flowLayoutPanel2.Controls.Add(this.recentGamesCheckBox);
            this.flowLayoutPanel2.Controls.Add(this.recentGamesListBox);
            this.flowLayoutPanel2.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(6, 19);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(138, 186);
            this.flowLayoutPanel2.TabIndex = 5;
            // 
            // inGameCheckBox
            // 
            this.inGameCheckBox.AutoSize = true;
            this.inGameCheckBox.Location = new System.Drawing.Point(3, 3);
            this.inGameCheckBox.Name = "inGameCheckBox";
            this.inGameCheckBox.Size = new System.Drawing.Size(66, 17);
            this.inGameCheckBox.TabIndex = 0;
            this.inGameCheckBox.Text = "In Game";
            this.inGameCheckBox.UseVisualStyleBackColor = true;
            this.inGameCheckBox.CheckedChanged += new System.EventHandler(this.inGameCheckBox_CheckedChanged);
            // 
            // appIdLabel
            // 
            this.appIdLabel.AutoSize = true;
            this.appIdLabel.Location = new System.Drawing.Point(3, 23);
            this.appIdLabel.Name = "appIdLabel";
            this.appIdLabel.Size = new System.Drawing.Size(40, 13);
            this.appIdLabel.TabIndex = 5;
            this.appIdLabel.Text = "AppID:";
            this.appIdLabel.Visible = false;
            // 
            // appIdNumericUpDown
            // 
            this.appIdNumericUpDown.Location = new System.Drawing.Point(3, 39);
            this.appIdNumericUpDown.Maximum = new decimal(new int[] {
            -1,
            -1,
            -1,
            0});
            this.appIdNumericUpDown.Name = "appIdNumericUpDown";
            this.appIdNumericUpDown.Size = new System.Drawing.Size(121, 20);
            this.appIdNumericUpDown.TabIndex = 5;
            this.appIdNumericUpDown.Visible = false;
            // 
            // recentGamesCheckBox
            // 
            this.recentGamesCheckBox.AutoSize = true;
            this.recentGamesCheckBox.Location = new System.Drawing.Point(3, 65);
            this.recentGamesCheckBox.Name = "recentGamesCheckBox";
            this.recentGamesCheckBox.Size = new System.Drawing.Size(132, 17);
            this.recentGamesCheckBox.TabIndex = 7;
            this.recentGamesCheckBox.Text = "Recent Games Played";
            this.recentGamesCheckBox.UseVisualStyleBackColor = true;
            this.recentGamesCheckBox.CheckedChanged += new System.EventHandler(this.recentGamesCheckBox_CheckedChanged);
            // 
            // recentGamesListBox
            // 
            this.recentGamesListBox.FormattingEnabled = true;
            this.recentGamesListBox.Location = new System.Drawing.Point(3, 88);
            this.recentGamesListBox.Name = "recentGamesListBox";
            this.recentGamesListBox.Size = new System.Drawing.Size(132, 95);
            this.recentGamesListBox.TabIndex = 6;
            this.recentGamesListBox.Visible = false;
            this.recentGamesListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.recentGamesListBox_MouseUp);
            // 
            // usernameGroupBox
            // 
            this.usernameGroupBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.usernameGroupBox.AutoSize = true;
            this.usernameGroupBox.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.usernameGroupBox.Controls.Add(this.compareAkaCheckBox);
            this.usernameGroupBox.Controls.Add(this.compareUsernamesCheckBox);
            this.usernameGroupBox.Controls.Add(this.caseSensitiveCheckBox);
            this.usernameGroupBox.Controls.Add(this.usernameTextBox);
            this.usernameGroupBox.Location = new System.Drawing.Point(205, 291);
            this.usernameGroupBox.MinimumSize = new System.Drawing.Size(133, 0);
            this.usernameGroupBox.Name = "usernameGroupBox";
            this.usernameGroupBox.Size = new System.Drawing.Size(150, 127);
            this.usernameGroupBox.TabIndex = 4;
            this.usernameGroupBox.TabStop = false;
            this.usernameGroupBox.Text = "Username";
            // 
            // compareAkaCheckBox
            // 
            this.compareAkaCheckBox.AutoSize = true;
            this.compareAkaCheckBox.Location = new System.Drawing.Point(6, 68);
            this.compareAkaCheckBox.Name = "compareAkaCheckBox";
            this.compareAkaCheckBox.Size = new System.Drawing.Size(83, 17);
            this.compareAkaCheckBox.TabIndex = 3;
            this.compareAkaCheckBox.Text = "AKA Names";
            this.compareAkaCheckBox.UseVisualStyleBackColor = true;
            this.compareAkaCheckBox.Visible = false;
            this.compareAkaCheckBox.MouseHover += new System.EventHandler(this.compareAkaCheckBox_MouseHover);
            // 
            // compareUsernamesCheckBox
            // 
            this.compareUsernamesCheckBox.AutoSize = true;
            this.compareUsernamesCheckBox.Location = new System.Drawing.Point(6, 45);
            this.compareUsernamesCheckBox.Name = "compareUsernamesCheckBox";
            this.compareUsernamesCheckBox.Size = new System.Drawing.Size(124, 17);
            this.compareUsernamesCheckBox.TabIndex = 2;
            this.compareUsernamesCheckBox.Text = "Compare Usernames";
            this.compareUsernamesCheckBox.UseVisualStyleBackColor = true;
            this.compareUsernamesCheckBox.CheckedChanged += new System.EventHandler(this.compareUsernamesCheckBox_CheckedChanged);
            this.compareUsernamesCheckBox.MouseHover += new System.EventHandler(this.compareUsernamesCheckBox_MouseHover);
            // 
            // caseSensitiveCheckBox
            // 
            this.caseSensitiveCheckBox.AutoSize = true;
            this.caseSensitiveCheckBox.Location = new System.Drawing.Point(6, 91);
            this.caseSensitiveCheckBox.Name = "caseSensitiveCheckBox";
            this.caseSensitiveCheckBox.Size = new System.Drawing.Size(96, 17);
            this.caseSensitiveCheckBox.TabIndex = 1;
            this.caseSensitiveCheckBox.Text = "Case Sensitive";
            this.caseSensitiveCheckBox.UseVisualStyleBackColor = true;
            this.caseSensitiveCheckBox.Visible = false;
            // 
            // usernameTextBox
            // 
            this.usernameTextBox.Location = new System.Drawing.Point(6, 19);
            this.usernameTextBox.Name = "usernameTextBox";
            this.usernameTextBox.Size = new System.Drawing.Size(121, 20);
            this.usernameTextBox.TabIndex = 0;
            this.usernameTextBox.TextChanged += new System.EventHandler(this.usernameTextBox_TextChanged);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel1.Controls.Add(this.profilePicGroupBox);
            this.flowLayoutPanel1.Controls.Add(this.userStatusGroupBox);
            this.flowLayoutPanel1.Controls.Add(this.gameInfoGroupBox);
            this.flowLayoutPanel1.Controls.Add(this.usernameGroupBox);
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.MaximumSize = new System.Drawing.Size(4000, 450);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(358, 421);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // recentGamesContextMenuStrip
            // 
            this.recentGamesContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.removeToolStripMenuItem});
            this.recentGamesContextMenuStrip.Name = "recentGamesContextMenuStrip";
            this.recentGamesContextMenuStrip.Size = new System.Drawing.Size(181, 70);
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.addToolStripMenuItem.Text = "Add";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // removeToolStripMenuItem
            // 
            this.removeToolStripMenuItem.Name = "removeToolStripMenuItem";
            this.removeToolStripMenuItem.Size = new System.Drawing.Size(117, 22);
            this.removeToolStripMenuItem.Text = "Remove";
            this.removeToolStripMenuItem.Click += new System.EventHandler(this.removeToolStripMenuItem_Click);
            // 
            // startButton
            // 
            this.startButton.Enabled = false;
            this.startButton.Location = new System.Drawing.Point(3, 494);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(358, 23);
            this.startButton.TabIndex = 6;
            this.startButton.Text = "Start Search";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.AutoSize = true;
            this.flowLayoutPanel3.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.flowLayoutPanel3.Controls.Add(this.flowLayoutPanel1);
            this.flowLayoutPanel3.Controls.Add(this.threadsTrackBar);
            this.flowLayoutPanel3.Controls.Add(this.threadsLabel);
            this.flowLayoutPanel3.Controls.Add(this.startButton);
            this.flowLayoutPanel3.Controls.Add(this.matchedUsersListBox);
            this.flowLayoutPanel3.Controls.Add(this.statusLabel);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 1);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Padding = new System.Windows.Forms.Padding(0, 0, 0, 5);
            this.flowLayoutPanel3.Size = new System.Drawing.Size(364, 639);
            this.flowLayoutPanel3.TabIndex = 7;
            // 
            // threadsTrackBar
            // 
            this.threadsTrackBar.LargeChange = 1;
            this.threadsTrackBar.Location = new System.Drawing.Point(3, 430);
            this.threadsTrackBar.Maximum = 20;
            this.threadsTrackBar.Minimum = 1;
            this.threadsTrackBar.Name = "threadsTrackBar";
            this.threadsTrackBar.Size = new System.Drawing.Size(358, 45);
            this.threadsTrackBar.TabIndex = 7;
            this.threadsTrackBar.Value = 1;
            this.threadsTrackBar.Scroll += new System.EventHandler(this.threadsTrackBar_Scroll);
            // 
            // threadsLabel
            // 
            this.threadsLabel.AutoSize = true;
            this.threadsLabel.Location = new System.Drawing.Point(3, 478);
            this.threadsLabel.Name = "threadsLabel";
            this.threadsLabel.Size = new System.Drawing.Size(61, 13);
            this.threadsLabel.TabIndex = 8;
            this.threadsLabel.Text = "Threads:  1";
            // 
            // matchedUsersListBox
            // 
            this.matchedUsersListBox.FormattingEnabled = true;
            this.matchedUsersListBox.Location = new System.Drawing.Point(3, 523);
            this.matchedUsersListBox.Name = "matchedUsersListBox";
            this.matchedUsersListBox.Size = new System.Drawing.Size(358, 95);
            this.matchedUsersListBox.TabIndex = 11;
            this.matchedUsersListBox.MouseUp += new System.Windows.Forms.MouseEventHandler(this.matchedUsersListBox_MouseUp);
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(3, 621);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(40, 13);
            this.statusLabel.TabIndex = 10;
            this.statusLabel.Text = "Status:";
            // 
            // statusTimer
            // 
            this.statusTimer.Tick += new System.EventHandler(this.statusTimer_Tick);
            // 
            // foundUsersContextMenuStrip
            // 
            this.foundUsersContextMenuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copySelectedToolStripMenuItem,
            this.copyAllToolStripMenuItem});
            this.foundUsersContextMenuStrip.Name = "foundUsersContextMenuStrip";
            this.foundUsersContextMenuStrip.Size = new System.Drawing.Size(150, 48);
            // 
            // copySelectedToolStripMenuItem
            // 
            this.copySelectedToolStripMenuItem.Name = "copySelectedToolStripMenuItem";
            this.copySelectedToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.copySelectedToolStripMenuItem.Text = "Copy Selected";
            this.copySelectedToolStripMenuItem.Click += new System.EventHandler(this.copySelectedToolStripMenuItem_Click);
            // 
            // copyAllToolStripMenuItem
            // 
            this.copyAllToolStripMenuItem.Name = "copyAllToolStripMenuItem";
            this.copyAllToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.copyAllToolStripMenuItem.Text = "Copy All";
            this.copyAllToolStripMenuItem.Click += new System.EventHandler(this.copyAllToolStripMenuItem_Click);
            // 
            // FriendSearchForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(376, 648);
            this.Controls.Add(this.flowLayoutPanel3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "FriendSearchForm";
            this.Text = "Steam Friend Search";
            this.Load += new System.EventHandler(this.FriendSearchForm_Load);
            this.userStatusGroupBox.ResumeLayout(false);
            this.profilePicGroupBox.ResumeLayout(false);
            this.profilePicGroupBox.PerformLayout();
            this.imageSimGroupBox.ResumeLayout(false);
            this.imageSimGroupBox.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.thresholdTrackBar)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.profilePictureBox)).EndInit();
            this.gameInfoGroupBox.ResumeLayout(false);
            this.gameInfoGroupBox.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.appIdNumericUpDown)).EndInit();
            this.usernameGroupBox.ResumeLayout(false);
            this.usernameGroupBox.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.recentGamesContextMenuStrip.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.threadsTrackBar)).EndInit();
            this.foundUsersContextMenuStrip.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox userStatusGroupBox;
        private System.Windows.Forms.ComboBox userStatusComboBox;
        private System.Windows.Forms.GroupBox profilePicGroupBox;
        private System.Windows.Forms.Button selectProfilePicButton;
        private System.Windows.Forms.RadioButton profileCustomRadioButton;
        private System.Windows.Forms.RadioButton profileDefaultRadioButton;
        private System.Windows.Forms.RadioButton profileAnyRadioButton;
        private System.Windows.Forms.PictureBox profilePictureBox;
        private System.Windows.Forms.GroupBox gameInfoGroupBox;
        private System.Windows.Forms.CheckBox inGameCheckBox;
        private System.Windows.Forms.GroupBox usernameGroupBox;
        private System.Windows.Forms.CheckBox caseSensitiveCheckBox;
        private System.Windows.Forms.TextBox usernameTextBox;
        private System.Windows.Forms.Label thresholdLabel;
        private System.Windows.Forms.GroupBox imageSimGroupBox;
        private System.Windows.Forms.TrackBar thresholdTrackBar;
        private System.Windows.Forms.Label appIdLabel;
        private System.Windows.Forms.NumericUpDown appIdNumericUpDown;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.CheckBox recentGamesCheckBox;
        private System.Windows.Forms.ListBox recentGamesListBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.ContextMenuStrip recentGamesContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem removeToolStripMenuItem;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.CheckBox compareAkaCheckBox;
        private System.Windows.Forms.CheckBox compareUsernamesCheckBox;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.TrackBar threadsTrackBar;
        private System.Windows.Forms.Label threadsLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Timer statusTimer;
        private System.Windows.Forms.ListBox matchedUsersListBox;
        private System.Windows.Forms.ContextMenuStrip foundUsersContextMenuStrip;
        private System.Windows.Forms.ToolStripMenuItem copySelectedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem copyAllToolStripMenuItem;
    }
}

