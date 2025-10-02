using System.ComponentModel;

namespace RabbitPublished
{
    partial class MainForm
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
            components = new Container();
            ComponentResourceManager resources = new ComponentResourceManager(typeof(MainForm));
            msgImages = new ImageList(components);
            panel1 = new Panel();
            splitContainer = new SplitContainer();
            treeView1 = new TreeView();
            searchPanel = new Panel();
            btnClearSearch = new Button();
            btnImages = new ImageList(components);
            SearchTextBox = new TextBox();
            messagePanel = new Panel();
            messageTextBox = new TextBox();
            controlPanel = new Panel();
            chkDeepInitialization = new CheckBox();
            btnNew = new Button();
            btnSave = new Button();
            standLabel = new Label();
            cbRabbitHosts = new ComboBox();
            btnSend = new Button();
            ttBtnClear = new ToolTip(components);
            ttBtnSave = new ToolTip(components);
            ttBtnNew = new ToolTip(components);
            ttBtnSend = new ToolTip(components);
            statusStrip1 = new StatusStrip();
            lineStatusLbl = new ToolStripStatusLabel();
            lineStatusLblValue = new ToolStripStatusLabel();
            colStatusLbl = new ToolStripStatusLabel();
            colStatusLblValue = new ToolStripStatusLabel();
            posStatusLbl = new ToolStripStatusLabel();
            posStatusLblValue = new ToolStripStatusLabel();
            toolStripContainer1 = new ToolStripContainer();
            panel1.SuspendLayout();
            ((ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            searchPanel.SuspendLayout();
            messagePanel.SuspendLayout();
            controlPanel.SuspendLayout();
            statusStrip1.SuspendLayout();
            toolStripContainer1.BottomToolStripPanel.SuspendLayout();
            toolStripContainer1.ContentPanel.SuspendLayout();
            toolStripContainer1.SuspendLayout();
            SuspendLayout();
            // 
            // msgImages
            // 
            msgImages.ColorDepth = ColorDepth.Depth32Bit;
            msgImages.ImageStream = (ImageListStreamer)resources.GetObject("msgImages.ImageStream");
            msgImages.TransparentColor = Color.Transparent;
            msgImages.Images.SetKeyName(0, "email.png");
            msgImages.Images.SetKeyName(1, "checked.png");
            msgImages.Images.SetKeyName(2, "error.png");
            msgImages.Images.SetKeyName(3, "dll.png");
            msgImages.Images.SetKeyName(4, "namespace.png");
            msgImages.Images.SetKeyName(5, "class.png");
            // 
            // panel1
            // 
            panel1.Controls.Add(splitContainer);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1233, 715);
            panel1.TabIndex = 3;
            // 
            // splitContainer
            // 
            splitContainer.Dock = DockStyle.Fill;
            splitContainer.Location = new Point(0, 0);
            splitContainer.Margin = new Padding(4, 3, 4, 3);
            splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            splitContainer.Panel1.Controls.Add(treeView1);
            splitContainer.Panel1.Controls.Add(searchPanel);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(messagePanel);
            splitContainer.Panel2.Controls.Add(controlPanel);
            splitContainer.Size = new Size(1233, 715);
            splitContainer.SplitterDistance = 337;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 2;
            // 
            // treeView1
            // 
            treeView1.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            treeView1.HotTracking = true;
            treeView1.ImageIndex = 0;
            treeView1.ImageList = msgImages;
            treeView1.Location = new Point(0, 34);
            treeView1.Name = "treeView1";
            treeView1.SelectedImageIndex = 0;
            treeView1.ShowNodeToolTips = true;
            treeView1.Size = new Size(337, 681);
            treeView1.TabIndex = 2;
            treeView1.NodeMouseClick += treeView1_NodeMouseClick;
            // 
            // searchPanel
            // 
            searchPanel.Controls.Add(btnClearSearch);
            searchPanel.Controls.Add(SearchTextBox);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 0);
            searchPanel.Margin = new Padding(4, 3, 4, 3);
            searchPanel.Name = "searchPanel";
            searchPanel.Size = new Size(337, 36);
            searchPanel.TabIndex = 1;
            // 
            // btnClearSearch
            // 
            btnClearSearch.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            btnClearSearch.ImageIndex = 1;
            btnClearSearch.ImageList = btnImages;
            btnClearSearch.Location = new Point(299, 6);
            btnClearSearch.Margin = new Padding(4, 3, 4, 3);
            btnClearSearch.Name = "btnClearSearch";
            btnClearSearch.Size = new Size(34, 27);
            btnClearSearch.TabIndex = 1;
            ttBtnClear.SetToolTip(btnClearSearch, "Очистить поиск");
            btnClearSearch.UseVisualStyleBackColor = true;
            btnClearSearch.Click += clearSearchButton_Click;
            // 
            // btnImages
            // 
            btnImages.ColorDepth = ColorDepth.Depth32Bit;
            btnImages.ImageStream = (ImageListStreamer)resources.GetObject("btnImages.ImageStream");
            btnImages.TransparentColor = Color.Transparent;
            btnImages.Images.SetKeyName(0, "icons8-search-24.png");
            btnImages.Images.SetKeyName(1, "cleaning.png");
            btnImages.Images.SetKeyName(2, "paper-plane.png");
            btnImages.Images.SetKeyName(3, "new.png");
            btnImages.Images.SetKeyName(4, "save.png");
            btnImages.Images.SetKeyName(5, "rabbitmq-logo.ico");
            // 
            // SearchTextBox
            // 
            SearchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SearchTextBox.Location = new Point(4, 7);
            SearchTextBox.Margin = new Padding(4, 3, 4, 3);
            SearchTextBox.Name = "SearchTextBox";
            SearchTextBox.PlaceholderText = "Введите строку для поиска типа...";
            SearchTextBox.Size = new Size(293, 23);
            SearchTextBox.TabIndex = 0;
            SearchTextBox.TextChanged += searchTextBox_TextChanged;
            // 
            // messagePanel
            // 
            messagePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            messagePanel.Controls.Add(messageTextBox);
            messagePanel.Location = new Point(0, 34);
            messagePanel.Margin = new Padding(4, 3, 4, 3);
            messagePanel.Name = "messagePanel";
            messagePanel.Size = new Size(884, 681);
            messagePanel.TabIndex = 3;
            // 
            // messageTextBox
            // 
            messageTextBox.AcceptsReturn = true;
            messageTextBox.AcceptsTab = true;
            messageTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            messageTextBox.Font = new Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            messageTextBox.Location = new Point(0, 0);
            messageTextBox.Margin = new Padding(4, 3, 4, 3);
            messageTextBox.Multiline = true;
            messageTextBox.Name = "messageTextBox";
            messageTextBox.ScrollBars = ScrollBars.Both;
            messageTextBox.Size = new Size(886, 681);
            messageTextBox.TabIndex = 1;
            messageTextBox.WordWrap = false;
            messageTextBox.KeyUp += messageTextBox_KeyUp;
            // 
            // controlPanel
            // 
            controlPanel.Controls.Add(chkDeepInitialization);
            controlPanel.Controls.Add(btnNew);
            controlPanel.Controls.Add(btnSave);
            controlPanel.Controls.Add(standLabel);
            controlPanel.Controls.Add(cbRabbitHosts);
            controlPanel.Controls.Add(btnSend);
            controlPanel.Location = new Point(0, 0);
            controlPanel.Margin = new Padding(4, 3, 4, 3);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(891, 36);
            controlPanel.TabIndex = 2;
            // 
            // chkDeepInitialization
            // 
            chkDeepInitialization.AutoSize = true;
            chkDeepInitialization.Location = new Point(203, 13);
            chkDeepInitialization.Margin = new Padding(4, 3, 4, 3);
            chkDeepInitialization.Name = "chkDeepInitialization";
            chkDeepInitialization.Size = new Size(99, 19);
            chkDeepInitialization.TabIndex = 7;
            chkDeepInitialization.Text = "Deep Initialize";
            chkDeepInitialization.UseVisualStyleBackColor = true;
            // 
            // btnNew
            // 
            btnNew.ImageAlign = ContentAlignment.MiddleLeft;
            btnNew.ImageIndex = 3;
            btnNew.ImageList = btnImages;
            btnNew.Location = new Point(100, 5);
            btnNew.Margin = new Padding(4, 3, 4, 3);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(77, 27);
            btnNew.TabIndex = 6;
            btnNew.Text = "Новый";
            btnNew.TextAlign = ContentAlignment.MiddleRight;
            ttBtnNew.SetToolTip(btnNew, "Новое сообщение");
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.ImageAlign = ContentAlignment.MiddleLeft;
            btnSave.ImageIndex = 4;
            btnSave.ImageList = btnImages;
            btnSave.Location = new Point(4, 5);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(97, 27);
            btnSave.TabIndex = 5;
            btnSave.Text = "Сохранить";
            btnSave.TextAlign = ContentAlignment.MiddleRight;
            ttBtnSave.SetToolTip(btnSave, "Сохранить сообщение");
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // standLabel
            // 
            standLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            standLabel.AutoSize = true;
            standLabel.Location = new Point(631, 12);
            standLabel.Margin = new Padding(4, 0, 4, 0);
            standLabel.Name = "standLabel";
            standLabel.Size = new Size(39, 15);
            standLabel.TabIndex = 4;
            standLabel.Text = "Стенд";
            // 
            // cbRabbitHosts
            // 
            cbRabbitHosts.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cbRabbitHosts.DisplayMember = "Name";
            cbRabbitHosts.DropDownStyle = ComboBoxStyle.DropDownList;
            cbRabbitHosts.FormattingEnabled = true;
            cbRabbitHosts.Location = new Point(681, 5);
            cbRabbitHosts.Margin = new Padding(4, 3, 4, 3);
            cbRabbitHosts.Name = "cbRabbitHosts";
            cbRabbitHosts.Size = new Size(206, 23);
            cbRabbitHosts.TabIndex = 3;
            cbRabbitHosts.SelectedValueChanged += cbRabbitHosts_SelectedIndexChanged;
            // 
            // btnSend
            // 
            btnSend.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSend.Enabled = false;
            btnSend.ImageAlign = ContentAlignment.MiddleRight;
            btnSend.ImageIndex = 2;
            btnSend.ImageList = btnImages;
            btnSend.Location = new Point(530, 6);
            btnSend.Margin = new Padding(4, 3, 4, 3);
            btnSend.Name = "btnSend";
            btnSend.Size = new Size(94, 27);
            btnSend.TabIndex = 2;
            btnSend.Text = "Отправить";
            btnSend.TextAlign = ContentAlignment.MiddleLeft;
            ttBtnSend.SetToolTip(btnSend, "Отправить сообщение в шину");
            btnSend.UseVisualStyleBackColor = true;
            btnSend.Click += sendButton_Click;
            // 
            // ttBtnClear
            // 
            ttBtnClear.ToolTipIcon = ToolTipIcon.Info;
            ttBtnClear.ToolTipTitle = "Очистить поиск";
            // 
            // ttBtnSave
            // 
            ttBtnSave.ToolTipIcon = ToolTipIcon.Info;
            ttBtnSave.ToolTipTitle = "Сохранить сообщение";
            // 
            // ttBtnNew
            // 
            ttBtnNew.ToolTipIcon = ToolTipIcon.Warning;
            ttBtnNew.ToolTipTitle = "Создать новое сообщение";
            // 
            // ttBtnSend
            // 
            ttBtnSend.ToolTipIcon = ToolTipIcon.Warning;
            ttBtnSend.ToolTipTitle = "Отправить сообщение";
            // 
            // statusStrip1
            // 
            statusStrip1.Dock = DockStyle.None;
            statusStrip1.Items.AddRange(new ToolStripItem[] { lineStatusLbl, lineStatusLblValue, colStatusLbl, colStatusLblValue, posStatusLbl, posStatusLblValue });
            statusStrip1.Location = new Point(0, 0);
            statusStrip1.Name = "statusStrip1";
            statusStrip1.RenderMode = ToolStripRenderMode.Professional;
            statusStrip1.Size = new Size(1233, 22);
            statusStrip1.TabIndex = 4;
            statusStrip1.Text = "statusStrip1";
            // 
            // lineStatusLbl
            // 
            lineStatusLbl.Name = "lineStatusLbl";
            lineStatusLbl.Size = new Size(23, 17);
            lineStatusLbl.Text = "Ln:";
            // 
            // lineStatusLblValue
            // 
            lineStatusLblValue.AutoSize = false;
            lineStatusLblValue.BorderStyle = Border3DStyle.RaisedOuter;
            lineStatusLblValue.Name = "lineStatusLblValue";
            lineStatusLblValue.Size = new Size(25, 17);
            lineStatusLblValue.ToolTipText = "Позиция курсора";
            // 
            // colStatusLbl
            // 
            colStatusLbl.AutoToolTip = true;
            colStatusLbl.Name = "colStatusLbl";
            colStatusLbl.Size = new Size(28, 17);
            colStatusLbl.Text = "Col:";
            // 
            // colStatusLblValue
            // 
            colStatusLblValue.AutoSize = false;
            colStatusLblValue.Name = "colStatusLblValue";
            colStatusLblValue.Size = new Size(25, 17);
            // 
            // posStatusLbl
            // 
            posStatusLbl.Name = "posStatusLbl";
            posStatusLbl.Size = new Size(29, 17);
            posStatusLbl.Text = "Pos:";
            // 
            // posStatusLblValue
            // 
            posStatusLblValue.AutoSize = false;
            posStatusLblValue.Name = "posStatusLblValue";
            posStatusLblValue.Size = new Size(30, 17);
            // 
            // toolStripContainer1
            // 
            // 
            // toolStripContainer1.BottomToolStripPanel
            // 
            toolStripContainer1.BottomToolStripPanel.Controls.Add(statusStrip1);
            // 
            // toolStripContainer1.ContentPanel
            // 
            toolStripContainer1.ContentPanel.AutoScroll = true;
            toolStripContainer1.ContentPanel.Controls.Add(panel1);
            toolStripContainer1.ContentPanel.Size = new Size(1233, 715);
            toolStripContainer1.Dock = DockStyle.Fill;
            toolStripContainer1.Location = new Point(0, 0);
            toolStripContainer1.Name = "toolStripContainer1";
            toolStripContainer1.Size = new Size(1233, 762);
            toolStripContainer1.TabIndex = 5;
            toolStripContainer1.Text = "toolStripContainer1";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1233, 762);
            Controls.Add(toolStripContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(4, 3, 4, 3);
            Name = "MainForm";
            Text = "Публикатор сообщений в шину";
            FormClosed += MainForm_FormClosed;
            Load += MainForm_Load;
            panel1.ResumeLayout(false);
            splitContainer.Panel1.ResumeLayout(false);
            splitContainer.Panel2.ResumeLayout(false);
            ((ISupportInitialize)splitContainer).EndInit();
            splitContainer.ResumeLayout(false);
            searchPanel.ResumeLayout(false);
            searchPanel.PerformLayout();
            messagePanel.ResumeLayout(false);
            messagePanel.PerformLayout();
            controlPanel.ResumeLayout(false);
            controlPanel.PerformLayout();
            statusStrip1.ResumeLayout(false);
            statusStrip1.PerformLayout();
            toolStripContainer1.BottomToolStripPanel.ResumeLayout(false);
            toolStripContainer1.BottomToolStripPanel.PerformLayout();
            toolStripContainer1.ContentPanel.ResumeLayout(false);
            toolStripContainer1.ResumeLayout(false);
            toolStripContainer1.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private ImageList msgImages;
        private Panel panel1;
        private SplitContainer splitContainer;
        private Panel searchPanel;
        private Button btnClearSearch;
        private TextBox SearchTextBox;
        private Panel messagePanel;
        private TextBox messageTextBox;
        private Panel controlPanel;
        private Button btnSave;
        private Label standLabel;
        private ComboBox cbRabbitHosts;
        private Button btnSend;
        private Button btnNew;
        private CheckBox chkDeepInitialization;
        private ToolTip ttBtnClear;
        private ToolTip ttBtnSave;
        private ToolTip ttBtnNew;
        private ImageList btnImages;
        private ToolTip ttBtnSend;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel lineStatusLblValue;
        private ToolStripStatusLabel lineStatusLbl;
        private ToolStripStatusLabel colStatusLbl;
        private ToolStripContainer toolStripContainer1;
        private ToolStripStatusLabel colStatusLblValue;
        private ToolStripStatusLabel posStatusLbl;
        private ToolStripStatusLabel posStatusLblValue;
        private TreeView treeView1;
    }
}
