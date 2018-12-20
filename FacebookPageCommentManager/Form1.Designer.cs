namespace FacebookPageCommentManager
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.likeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.replyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.unhideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.sendMessageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.deleteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.banFromPageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.AutoLikeBox = new System.Windows.Forms.TextBox();
            this.AutoReplyBox = new System.Windows.Forms.TextBox();
            this.AutoHideBox = new System.Windows.Forms.TextBox();
            this.AutoDeleteBox = new System.Windows.Forms.TextBox();
            this.AutoBlockBox = new System.Windows.Forms.TextBox();
            this.AutoSendMessageBox = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.btnFBLogin = new System.Windows.Forms.Button();
            this.btnRun = new System.Windows.Forms.Button();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.profileViewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.commentsScraperToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.commentsAnalysisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exportDataToHTMLToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.minimizeToSystemtrayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutUsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.PostlistBox = new System.Windows.Forms.ListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.btnAddMessage = new System.Windows.Forms.Button();
            this.HideAttachmentCheckBox = new System.Windows.Forms.CheckBox();
            this.HideLinkCheckBox = new System.Windows.Forms.CheckBox();
            this.labelStatus = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView1.ContextMenuStrip = this.contextMenuStrip1;
            this.dataGridView1.Location = new System.Drawing.Point(0, 22);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(1038, 601);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            this.dataGridView1.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_ColumnHeaderMouseClick);
            this.dataGridView1.DataError += new System.Windows.Forms.DataGridViewDataErrorEventHandler(this.dataGridView1_DataError);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.likeToolStripMenuItem,
            this.toolStripSeparator3,
            this.replyToolStripMenuItem,
            this.unhideToolStripMenuItem,
            this.toolStripSeparator4,
            this.sendMessageToolStripMenuItem,
            this.toolStripSeparator5,
            this.deleteToolStripMenuItem,
            this.banFromPageToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(153, 154);
            // 
            // likeToolStripMenuItem
            // 
            this.likeToolStripMenuItem.Name = "likeToolStripMenuItem";
            this.likeToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.likeToolStripMenuItem.Text = "Like";
            this.likeToolStripMenuItem.Click += new System.EventHandler(this.likeToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(149, 6);
            // 
            // replyToolStripMenuItem
            // 
            this.replyToolStripMenuItem.Name = "replyToolStripMenuItem";
            this.replyToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.replyToolStripMenuItem.Text = "Hide";
            this.replyToolStripMenuItem.Click += new System.EventHandler(this.replyToolStripMenuItem_Click);
            // 
            // unhideToolStripMenuItem
            // 
            this.unhideToolStripMenuItem.Name = "unhideToolStripMenuItem";
            this.unhideToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.unhideToolStripMenuItem.Text = "Unhide";
            this.unhideToolStripMenuItem.Click += new System.EventHandler(this.unhideToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(149, 6);
            // 
            // sendMessageToolStripMenuItem
            // 
            this.sendMessageToolStripMenuItem.Name = "sendMessageToolStripMenuItem";
            this.sendMessageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.sendMessageToolStripMenuItem.Text = "Send Message";
            this.sendMessageToolStripMenuItem.Click += new System.EventHandler(this.sendMessageToolStripMenuItem_ClickAsync);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(149, 6);
            // 
            // deleteToolStripMenuItem
            // 
            this.deleteToolStripMenuItem.Name = "deleteToolStripMenuItem";
            this.deleteToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.deleteToolStripMenuItem.Text = "Delete";
            this.deleteToolStripMenuItem.Click += new System.EventHandler(this.deleteToolStripMenuItem_Click);
            // 
            // banFromPageToolStripMenuItem
            // 
            this.banFromPageToolStripMenuItem.Name = "banFromPageToolStripMenuItem";
            this.banFromPageToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.banFromPageToolStripMenuItem.Text = "Ban from page";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(1047, 27);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(214, 94);
            this.checkedListBox1.TabIndex = 3;
            this.checkedListBox1.SelectedIndexChanged += new System.EventHandler(this.checkedListBox1_SelectedIndexChangedAsync);
            // 
            // AutoLikeBox
            // 
            this.AutoLikeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoLikeBox.Location = new System.Drawing.Point(1047, 390);
            this.AutoLikeBox.Multiline = true;
            this.AutoLikeBox.Name = "AutoLikeBox";
            this.AutoLikeBox.Size = new System.Drawing.Size(100, 56);
            this.AutoLikeBox.TabIndex = 4;
            // 
            // AutoReplyBox
            // 
            this.AutoReplyBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoReplyBox.Location = new System.Drawing.Point(1161, 390);
            this.AutoReplyBox.Multiline = true;
            this.AutoReplyBox.Name = "AutoReplyBox";
            this.AutoReplyBox.Size = new System.Drawing.Size(112, 56);
            this.AutoReplyBox.TabIndex = 5;
            // 
            // AutoHideBox
            // 
            this.AutoHideBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoHideBox.Location = new System.Drawing.Point(1047, 475);
            this.AutoHideBox.Multiline = true;
            this.AutoHideBox.Name = "AutoHideBox";
            this.AutoHideBox.Size = new System.Drawing.Size(100, 56);
            this.AutoHideBox.TabIndex = 6;
            // 
            // AutoDeleteBox
            // 
            this.AutoDeleteBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoDeleteBox.Location = new System.Drawing.Point(1161, 475);
            this.AutoDeleteBox.Multiline = true;
            this.AutoDeleteBox.Name = "AutoDeleteBox";
            this.AutoDeleteBox.Size = new System.Drawing.Size(112, 56);
            this.AutoDeleteBox.TabIndex = 7;
            // 
            // AutoBlockBox
            // 
            this.AutoBlockBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoBlockBox.Location = new System.Drawing.Point(1047, 565);
            this.AutoBlockBox.Multiline = true;
            this.AutoBlockBox.Name = "AutoBlockBox";
            this.AutoBlockBox.Size = new System.Drawing.Size(100, 56);
            this.AutoBlockBox.TabIndex = 8;
            // 
            // AutoSendMessageBox
            // 
            this.AutoSendMessageBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AutoSendMessageBox.Location = new System.Drawing.Point(1044, 305);
            this.AutoSendMessageBox.Multiline = true;
            this.AutoSendMessageBox.Name = "AutoSendMessageBox";
            this.AutoSendMessageBox.Size = new System.Drawing.Size(194, 56);
            this.AutoSendMessageBox.TabIndex = 9;
            // 
            // textBox8
            // 
            this.textBox8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.textBox8.Location = new System.Drawing.Point(87, 647);
            this.textBox8.Name = "textBox8";
            this.textBox8.Size = new System.Drawing.Size(133, 20);
            this.textBox8.TabIndex = 10;
            this.textBox8.TextChanged += new System.EventHandler(this.textBox8_TextChanged);
            // 
            // btnFBLogin
            // 
            this.btnFBLogin.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFBLogin.Location = new System.Drawing.Point(1082, 647);
            this.btnFBLogin.Name = "btnFBLogin";
            this.btnFBLogin.Size = new System.Drawing.Size(81, 35);
            this.btnFBLogin.TabIndex = 11;
            this.btnFBLogin.Text = "Login";
            this.btnFBLogin.UseVisualStyleBackColor = true;
            this.btnFBLogin.Click += new System.EventHandler(this.btnFBLogin_Click);
            // 
            // btnRun
            // 
            this.btnRun.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnRun.BackColor = System.Drawing.Color.PaleGreen;
            this.btnRun.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRun.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnRun.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.btnRun.Location = new System.Drawing.Point(1186, 647);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(75, 35);
            this.btnRun.TabIndex = 12;
            this.btnRun.Text = "Run";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1289, 24);
            this.menuStrip1.TabIndex = 13;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.profileViewToolStripMenuItem,
            this.toolStripSeparator1,
            this.commentsScraperToolStripMenuItem,
            this.commentsAnalysisToolStripMenuItem,
            this.toolStripSeparator2,
            this.exportDataToHTMLToolStripMenuItem,
            this.minimizeToSystemtrayToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // profileViewToolStripMenuItem
            // 
            this.profileViewToolStripMenuItem.Name = "profileViewToolStripMenuItem";
            this.profileViewToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.profileViewToolStripMenuItem.Text = "Profile View";
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(194, 6);
            // 
            // commentsScraperToolStripMenuItem
            // 
            this.commentsScraperToolStripMenuItem.Name = "commentsScraperToolStripMenuItem";
            this.commentsScraperToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.commentsScraperToolStripMenuItem.Text = "Comments Scraper";
            this.commentsScraperToolStripMenuItem.Click += new System.EventHandler(this.commentsScraperToolStripMenuItem_Click);
            // 
            // commentsAnalysisToolStripMenuItem
            // 
            this.commentsAnalysisToolStripMenuItem.Name = "commentsAnalysisToolStripMenuItem";
            this.commentsAnalysisToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.commentsAnalysisToolStripMenuItem.Text = "Comments Analysis";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(194, 6);
            // 
            // exportDataToHTMLToolStripMenuItem
            // 
            this.exportDataToHTMLToolStripMenuItem.Name = "exportDataToHTMLToolStripMenuItem";
            this.exportDataToHTMLToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.exportDataToHTMLToolStripMenuItem.Text = "Export data to HTML";
            // 
            // minimizeToSystemtrayToolStripMenuItem
            // 
            this.minimizeToSystemtrayToolStripMenuItem.Name = "minimizeToSystemtrayToolStripMenuItem";
            this.minimizeToSystemtrayToolStripMenuItem.Size = new System.Drawing.Size(197, 22);
            this.minimizeToSystemtrayToolStripMenuItem.Text = "Minimize to systemtray";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutUsToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutUsToolStripMenuItem
            // 
            this.aboutUsToolStripMenuItem.Name = "aboutUsToolStripMenuItem";
            this.aboutUsToolStripMenuItem.Size = new System.Drawing.Size(123, 22);
            this.aboutUsToolStripMenuItem.Text = "About Us";
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.progressBar1.Location = new System.Drawing.Point(0, 673);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(106, 23);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Marquee;
            this.progressBar1.TabIndex = 14;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 626);
            this.label1.Margin = new System.Windows.Forms.Padding(0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(10, 13);
            this.label1.TabIndex = 15;
            this.label1.Text = ".";
            // 
            // linkLabel1
            // 
            this.linkLabel1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.Location = new System.Drawing.Point(112, 673);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(67, 13);
            this.linkLabel1.TabIndex = 16;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "Admin Name";
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_TickAsync);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(31, 650);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 13);
            this.label2.TabIndex = 17;
            this.label2.Text = "Search";
            // 
            // label4
            // 
            this.label4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(1044, 374);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(99, 13);
            this.label4.TabIndex = 19;
            this.label4.Text = "Auto Like Comment";
            // 
            // label5
            // 
            this.label5.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(1158, 374);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(106, 13);
            this.label5.TabIndex = 20;
            this.label5.Text = "Auto Reply Comment";
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(1044, 459);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 13);
            this.label6.TabIndex = 21;
            this.label6.Text = "Auto Hide Comment";
            // 
            // label7
            // 
            this.label7.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(1158, 459);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(110, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Auto Delete Comment";
            // 
            // label8
            // 
            this.label8.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(1044, 549);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(95, 13);
            this.label8.TabIndex = 23;
            this.label8.Text = "Auto Block People";
            // 
            // label9
            // 
            this.label9.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(1044, 279);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(164, 13);
            this.label9.TabIndex = 24;
            this.label9.Text = "Add Auto Send Private Message:";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon1.Icon")));
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // PostlistBox
            // 
            this.PostlistBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.PostlistBox.FormattingEnabled = true;
            this.PostlistBox.Location = new System.Drawing.Point(1047, 159);
            this.PostlistBox.Name = "PostlistBox";
            this.PostlistBox.Size = new System.Drawing.Size(221, 108);
            this.PostlistBox.TabIndex = 25;
            this.PostlistBox.SelectedIndexChanged += new System.EventHandler(this.PostlistBox_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(1044, 135);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 13);
            this.label3.TabIndex = 26;
            this.label3.Text = "Post List";
            // 
            // btnAddMessage
            // 
            this.btnAddMessage.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAddMessage.FlatAppearance.BorderColor = System.Drawing.Color.Silver;
            this.btnAddMessage.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnAddMessage.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnAddMessage.Location = new System.Drawing.Point(1241, 317);
            this.btnAddMessage.Margin = new System.Windows.Forms.Padding(0);
            this.btnAddMessage.Name = "btnAddMessage";
            this.btnAddMessage.Size = new System.Drawing.Size(45, 35);
            this.btnAddMessage.TabIndex = 29;
            this.btnAddMessage.Text = "Add";
            this.btnAddMessage.UseVisualStyleBackColor = true;
            this.btnAddMessage.Click += new System.EventHandler(this.btnAddMessage_Click);
            // 
            // HideAttachmentCheckBox
            // 
            this.HideAttachmentCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HideAttachmentCheckBox.AutoSize = true;
            this.HideAttachmentCheckBox.Location = new System.Drawing.Point(1161, 565);
            this.HideAttachmentCheckBox.Name = "HideAttachmentCheckBox";
            this.HideAttachmentCheckBox.Size = new System.Drawing.Size(110, 17);
            this.HideAttachmentCheckBox.TabIndex = 30;
            this.HideAttachmentCheckBox.Text = "Hide Attachments";
            this.HideAttachmentCheckBox.UseVisualStyleBackColor = true;
            // 
            // HideLinkCheckBox
            // 
            this.HideLinkCheckBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.HideLinkCheckBox.AutoSize = true;
            this.HideLinkCheckBox.Location = new System.Drawing.Point(1161, 588);
            this.HideLinkCheckBox.Name = "HideLinkCheckBox";
            this.HideLinkCheckBox.Size = new System.Drawing.Size(76, 17);
            this.HideLinkCheckBox.TabIndex = 31;
            this.HideLinkCheckBox.Text = "Hide Links";
            this.HideLinkCheckBox.UseVisualStyleBackColor = true;
            // 
            // labelStatus
            // 
            this.labelStatus.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.labelStatus.AutoSize = true;
            this.labelStatus.ForeColor = System.Drawing.Color.Lime;
            this.labelStatus.Location = new System.Drawing.Point(211, 673);
            this.labelStatus.Name = "labelStatus";
            this.labelStatus.Size = new System.Drawing.Size(95, 13);
            this.labelStatus.TabIndex = 32;
            this.labelStatus.Text = "Status: Connected";
            this.labelStatus.Click += new System.EventHandler(this.labelStatus_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1289, 694);
            this.Controls.Add(this.labelStatus);
            this.Controls.Add(this.HideLinkCheckBox);
            this.Controls.Add(this.HideAttachmentCheckBox);
            this.Controls.Add(this.btnAddMessage);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.PostlistBox);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.linkLabel1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btnRun);
            this.Controls.Add(this.btnFBLogin);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.AutoSendMessageBox);
            this.Controls.Add(this.AutoBlockBox);
            this.Controls.Add(this.AutoDeleteBox);
            this.Controls.Add(this.AutoHideBox);
            this.Controls.Add(this.AutoReplyBox);
            this.Controls.Add(this.AutoLikeBox);
            this.Controls.Add(this.checkedListBox1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "Facebook Comment Manage";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.TextBox AutoLikeBox;
        private System.Windows.Forms.TextBox AutoReplyBox;
        private System.Windows.Forms.TextBox AutoHideBox;
        private System.Windows.Forms.TextBox AutoDeleteBox;
        private System.Windows.Forms.TextBox AutoBlockBox;
        private System.Windows.Forms.TextBox AutoSendMessageBox;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Button btnFBLogin;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem profileViewToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem commentsScraperToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem commentsAnalysisToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exportDataToHTMLToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem minimizeToSystemtrayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutUsToolStripMenuItem;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem likeToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem replyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem unhideToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem sendMessageToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem deleteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem banFromPageToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.ListBox PostlistBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button btnAddMessage;
        private System.Windows.Forms.CheckBox HideAttachmentCheckBox;
        private System.Windows.Forms.CheckBox HideLinkCheckBox;
        private System.Windows.Forms.Label labelStatus;
    }
}

