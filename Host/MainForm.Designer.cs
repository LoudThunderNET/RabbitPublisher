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
            ListViewGroup listViewGroup1 = new ListViewGroup("ListViewGroup1", HorizontalAlignment.Left);
            ListViewGroup listViewGroup2 = new ListViewGroup("ListViewGroup2", HorizontalAlignment.Left);
            ListViewItem listViewItem1 = new ListViewItem("EntityPublished", 0);
            ListViewItem listViewItem2 = new ListViewItem("UserTradeChanged", 1);
            ListViewItem listViewItem3 = new ListViewItem("TradeLotTRansition");
            images = new ImageList(components);
            panel1 = new Panel();
            splitContainer = new SplitContainer();
            searchPanel = new Panel();
            clearSearchButton = new Button();
            SearchTextBox = new TextBox();
            messageTypesListView = new ListView();
            columnHeader1 = new ColumnHeader();
            messagePanel = new Panel();
            messageTextBox = new TextBox();
            controlPanel = new Panel();
            chkDeepInitialization = new CheckBox();
            btnNew = new Button();
            btnSave = new Button();
            standLabel = new Label();
            cbRabbitHosts = new ComboBox();
            sendButton = new Button();
            clearBtnTooltip = new ToolTip(components);
            saveBtToolTip = new ToolTip(components);
            newBtToolTip = new ToolTip(components);
            panel1.SuspendLayout();
            ((ISupportInitialize)splitContainer).BeginInit();
            splitContainer.Panel1.SuspendLayout();
            splitContainer.Panel2.SuspendLayout();
            splitContainer.SuspendLayout();
            searchPanel.SuspendLayout();
            messagePanel.SuspendLayout();
            controlPanel.SuspendLayout();
            SuspendLayout();
            // 
            // images
            // 
            images.ColorDepth = ColorDepth.Depth32Bit;
            images.ImageStream = (ImageListStreamer)resources.GetObject("images.ImageStream");
            images.TransparentColor = Color.Transparent;
            images.Images.SetKeyName(0, "icons8-search-24.png");
            images.Images.SetKeyName(1, "icons8-clear-24.png");
            images.Images.SetKeyName(2, "icons8-email-send-24.png");
            images.Images.SetKeyName(3, "icons8-new-24.png");
            images.Images.SetKeyName(4, "icons8-save-24.png");
            images.Images.SetKeyName(5, "rabbitmq-logo.ico");
            // 
            // panel1
            // 
            panel1.Controls.Add(splitContainer);
            panel1.Dock = DockStyle.Fill;
            panel1.Location = new Point(0, 0);
            panel1.Margin = new Padding(4, 3, 4, 3);
            panel1.Name = "panel1";
            panel1.Size = new Size(1134, 748);
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
            splitContainer.Panel1.Controls.Add(searchPanel);
            splitContainer.Panel1.Controls.Add(messageTypesListView);
            // 
            // splitContainer.Panel2
            // 
            splitContainer.Panel2.Controls.Add(messagePanel);
            splitContainer.Panel2.Controls.Add(controlPanel);
            splitContainer.Size = new Size(1134, 748);
            splitContainer.SplitterDistance = 310;
            splitContainer.SplitterWidth = 5;
            splitContainer.TabIndex = 2;
            // 
            // searchPanel
            // 
            searchPanel.Controls.Add(clearSearchButton);
            searchPanel.Controls.Add(SearchTextBox);
            searchPanel.Dock = DockStyle.Top;
            searchPanel.Location = new Point(0, 0);
            searchPanel.Margin = new Padding(4, 3, 4, 3);
            searchPanel.Name = "searchPanel";
            searchPanel.Size = new Size(310, 36);
            searchPanel.TabIndex = 1;
            // 
            // clearSearchButton
            // 
            clearSearchButton.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Right;
            clearSearchButton.ImageIndex = 1;
            clearSearchButton.ImageList = images;
            clearSearchButton.Location = new Point(272, 6);
            clearSearchButton.Margin = new Padding(4, 3, 4, 3);
            clearSearchButton.Name = "clearSearchButton";
            clearSearchButton.Size = new Size(34, 25);
            clearSearchButton.TabIndex = 1;
            clearBtnTooltip.SetToolTip(clearSearchButton, "Очистить поиск");
            clearSearchButton.UseVisualStyleBackColor = true;
            clearSearchButton.Click += clearSearchButton_Click;
            // 
            // SearchTextBox
            // 
            SearchTextBox.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            SearchTextBox.Location = new Point(4, 7);
            SearchTextBox.Margin = new Padding(4, 3, 4, 3);
            SearchTextBox.Name = "SearchTextBox";
            SearchTextBox.PlaceholderText = "Введите строку для поиска типа...";
            SearchTextBox.Size = new Size(266, 23);
            SearchTextBox.TabIndex = 0;
            SearchTextBox.TextChanged += searchTextBox_TextChanged;
            // 
            // messageTypesListView
            // 
            messageTypesListView.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            messageTypesListView.Columns.AddRange(new ColumnHeader[] { columnHeader1 });
            messageTypesListView.GridLines = true;
            listViewGroup1.Header = "ListViewGroup1";
            listViewGroup1.Name = "listViewGroup1";
            listViewGroup2.Header = "ListViewGroup2";
            listViewGroup2.Name = "listViewGroup2";
            messageTypesListView.Groups.AddRange(new ListViewGroup[] { listViewGroup1, listViewGroup2 });
            listViewItem1.Group = listViewGroup1;
            listViewItem2.Group = listViewGroup2;
            listViewItem3.Group = listViewGroup2;
            messageTypesListView.Items.AddRange(new ListViewItem[] { listViewItem1, listViewItem2, listViewItem3 });
            messageTypesListView.Location = new Point(0, 36);
            messageTypesListView.Margin = new Padding(4, 3, 4, 3);
            messageTypesListView.Name = "messageTypesListView";
            messageTypesListView.Size = new Size(310, 711);
            messageTypesListView.SmallImageList = images;
            messageTypesListView.TabIndex = 1;
            messageTypesListView.UseCompatibleStateImageBehavior = false;
            messageTypesListView.View = View.Details;
            messageTypesListView.SelectedIndexChanged += messageTypes_SelectedIndexChanged;
            // 
            // columnHeader1
            // 
            columnHeader1.Text = "Типы сообщений шины";
            columnHeader1.Width = 253;
            // 
            // messagePanel
            // 
            messagePanel.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            messagePanel.Controls.Add(messageTextBox);
            messagePanel.Location = new Point(0, 36);
            messagePanel.Margin = new Padding(4, 3, 4, 3);
            messagePanel.Name = "messagePanel";
            messagePanel.Size = new Size(813, 712);
            messagePanel.TabIndex = 3;
            // 
            // messageTextBox
            // 
            messageTextBox.AcceptsReturn = true;
            messageTextBox.AcceptsTab = true;
            messageTextBox.Dock = DockStyle.Fill;
            messageTextBox.Font = new Font("Courier New", 9.75F, FontStyle.Regular, GraphicsUnit.Point, 204);
            messageTextBox.Location = new Point(0, 0);
            messageTextBox.Margin = new Padding(4, 3, 4, 3);
            messageTextBox.Multiline = true;
            messageTextBox.Name = "messageTextBox";
            messageTextBox.ScrollBars = ScrollBars.Both;
            messageTextBox.Size = new Size(813, 712);
            messageTextBox.TabIndex = 1;
            messageTextBox.WordWrap = false;
            messageTextBox.TextChanged += messageTextBox_TextChanged;
            // 
            // controlPanel
            // 
            controlPanel.Controls.Add(chkDeepInitialization);
            controlPanel.Controls.Add(btnNew);
            controlPanel.Controls.Add(btnSave);
            controlPanel.Controls.Add(standLabel);
            controlPanel.Controls.Add(cbRabbitHosts);
            controlPanel.Controls.Add(sendButton);
            controlPanel.Dock = DockStyle.Fill;
            controlPanel.Location = new Point(0, 0);
            controlPanel.Margin = new Padding(4, 3, 4, 3);
            controlPanel.Name = "controlPanel";
            controlPanel.Size = new Size(819, 748);
            controlPanel.TabIndex = 2;
            controlPanel.Paint += controlPanel_Paint;
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
            btnNew.ImageList = images;
            btnNew.Location = new Point(100, 5);
            btnNew.Margin = new Padding(4, 3, 4, 3);
            btnNew.Name = "btnNew";
            btnNew.Size = new Size(77, 27);
            btnNew.TabIndex = 6;
            btnNew.Text = "Новый";
            btnNew.TextAlign = ContentAlignment.MiddleRight;
            newBtToolTip.SetToolTip(btnNew, "Новое сообщение");
            btnNew.UseVisualStyleBackColor = true;
            btnNew.Click += btnNew_Click;
            // 
            // btnSave
            // 
            btnSave.Enabled = false;
            btnSave.ImageAlign = ContentAlignment.MiddleLeft;
            btnSave.ImageIndex = 4;
            btnSave.ImageList = images;
            btnSave.Location = new Point(4, 5);
            btnSave.Margin = new Padding(4, 3, 4, 3);
            btnSave.Name = "btnSave";
            btnSave.Size = new Size(97, 27);
            btnSave.TabIndex = 5;
            btnSave.Text = "Сохранить";
            btnSave.TextAlign = ContentAlignment.MiddleRight;
            clearBtnTooltip.SetToolTip(btnSave, "Сохранить сообщение");
            btnSave.UseVisualStyleBackColor = true;
            btnSave.Click += btnSave_Click;
            // 
            // standLabel
            // 
            standLabel.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            standLabel.AutoSize = true;
            standLabel.Location = new Point(559, 12);
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
            cbRabbitHosts.Location = new Point(609, 5);
            cbRabbitHosts.Margin = new Padding(4, 3, 4, 3);
            cbRabbitHosts.Name = "cbRabbitHosts";
            cbRabbitHosts.Size = new Size(206, 23);
            cbRabbitHosts.TabIndex = 3;
            cbRabbitHosts.SelectedValueChanged += cbRabbitHosts_SelectedIndexChanged;
            // 
            // sendButton
            // 
            sendButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            sendButton.Enabled = false;
            sendButton.ImageAlign = ContentAlignment.MiddleRight;
            sendButton.ImageIndex = 2;
            sendButton.ImageList = images;
            sendButton.Location = new Point(458, 6);
            sendButton.Margin = new Padding(4, 3, 4, 3);
            sendButton.Name = "sendButton";
            sendButton.Size = new Size(94, 25);
            sendButton.TabIndex = 2;
            sendButton.Text = "Отправить";
            sendButton.TextAlign = ContentAlignment.MiddleLeft;
            sendButton.UseVisualStyleBackColor = true;
            sendButton.Click += sendButton_Click;
            // 
            // clearBtnTooltip
            // 
            clearBtnTooltip.ToolTipIcon = ToolTipIcon.Info;
            clearBtnTooltip.ToolTipTitle = "Очистить поиск";
            clearBtnTooltip.Popup += toolTip1_Popup;
            // 
            // saveBtToolTip
            // 
            saveBtToolTip.ToolTipIcon = ToolTipIcon.Info;
            saveBtToolTip.ToolTipTitle = "Сохранить сообщение";
            // 
            // newBtToolTip
            // 
            newBtToolTip.ToolTipIcon = ToolTipIcon.Warning;
            newBtToolTip.ToolTipTitle = "Создать новое сообщение";
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1134, 748);
            Controls.Add(panel1);
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
            ResumeLayout(false);
        }

        #endregion

        private ImageList images;
        private Panel panel1;
        private SplitContainer splitContainer;
        private Panel searchPanel;
        private Button clearSearchButton;
        private TextBox SearchTextBox;
        private ListView messageTypesListView;
        private ColumnHeader columnHeader1;
        private Panel messagePanel;
        private TextBox messageTextBox;
        private Panel controlPanel;
        private Button btnSave;
        private Label standLabel;
        private ComboBox cbRabbitHosts;
        private Button sendButton;
        private Button btnNew;
        private CheckBox chkDeepInitialization;
        private ToolTip clearBtnTooltip;
        private ToolTip saveBtToolTip;
        private ToolTip newBtToolTip;
    }
}
