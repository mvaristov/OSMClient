namespace Amv.GeoClient.WinForms
{
    partial class MainForm
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent() {
            this.lviAvialableLocations = new System.Windows.Forms.ListView();
            this.clmnNamePlace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnTypePlace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmnClassPlace = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.txtSearchLoaction = new System.Windows.Forms.TextBox();
            this.btnSearch = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlMain = new System.Windows.Forms.Panel();
            this.pnlMapBackground = new System.Windows.Forms.Panel();
            this.btnZoomDown = new System.Windows.Forms.Button();
            this.btnZoomUp = new System.Windows.Forms.Button();
            this.lblPlaceZoom = new System.Windows.Forms.Label();
            this.splitter1 = new System.Windows.Forms.Splitter();
            this.pnlLeft = new System.Windows.Forms.Panel();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.btnCancelSearch = new System.Windows.Forms.Button();
            this.menuStripMain = new System.Windows.Forms.MenuStrip();
            this.tsmiFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiFile_MapProviders = new System.Windows.Forms.ToolStripMenuItem();
            this.miFile_MapProviders_Items = new System.Windows.Forms.ToolStripComboBox();
            this.lblProviderMap = new System.Windows.Forms.Label();
            this.pnlLocationInfo = new Amv.GeoClient.WinForms.MessagePanel();
            this.lLblClosePnlInfoLocation = new System.Windows.Forms.LinkLabel();
            this.pnlMap = new Amv.GeoClient.WinForms.DoubleBufferedPanel();
            this.pnlMain.SuspendLayout();
            this.pnlMapBackground.SuspendLayout();
            this.pnlLeft.SuspendLayout();
            this.menuStripMain.SuspendLayout();
            this.pnlLocationInfo.SuspendLayout();
            this.SuspendLayout();
            // 
            // lviAvialableLocations
            // 
            this.lviAvialableLocations.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmnNamePlace,
            this.clmnTypePlace,
            this.clmnClassPlace});
            this.lviAvialableLocations.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lviAvialableLocations.FullRowSelect = true;
            this.lviAvialableLocations.HideSelection = false;
            this.lviAvialableLocations.Location = new System.Drawing.Point(0, 0);
            this.lviAvialableLocations.MultiSelect = false;
            this.lviAvialableLocations.Name = "lviAvialableLocations";
            this.lviAvialableLocations.Size = new System.Drawing.Size(300, 521);
            this.lviAvialableLocations.TabIndex = 14;
            this.lviAvialableLocations.UseCompatibleStateImageBehavior = false;
            this.lviAvialableLocations.View = System.Windows.Forms.View.Details;
            this.lviAvialableLocations.ItemSelectionChanged += new System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.lviAvialableLocations_ItemSelectionChanged);
            // 
            // clmnNamePlace
            // 
            this.clmnNamePlace.Text = "Название";
            this.clmnNamePlace.Width = 200;
            // 
            // clmnTypePlace
            // 
            this.clmnTypePlace.Text = "Тип";
            this.clmnTypePlace.Width = 53;
            // 
            // clmnClassPlace
            // 
            this.clmnClassPlace.Text = "Класс";
            // 
            // txtSearchLoaction
            // 
            this.txtSearchLoaction.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.txtSearchLoaction.Location = new System.Drawing.Point(16, 25);
            this.txtSearchLoaction.Name = "txtSearchLoaction";
            this.txtSearchLoaction.Size = new System.Drawing.Size(617, 23);
            this.txtSearchLoaction.TabIndex = 15;
            this.txtSearchLoaction.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtSearchLoaction_KeyPress);
            // 
            // btnSearch
            // 
            this.btnSearch.Location = new System.Drawing.Point(636, 24);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.TabIndex = 16;
            this.btnSearch.Text = "Найти";
            this.btnSearch.UseVisualStyleBackColor = true;
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(404, 13);
            this.label1.TabIndex = 18;
            this.label1.Text = "Пример: Cтрана, Город, Улица, №дома (только названия без ул. г. пр.  и др...)";
            // 
            // pnlMain
            // 
            this.pnlMain.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.pnlMain.Controls.Add(this.pnlMapBackground);
            this.pnlMain.Controls.Add(this.splitter1);
            this.pnlMain.Controls.Add(this.pnlLeft);
            this.pnlMain.Location = new System.Drawing.Point(12, 76);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.Size = new System.Drawing.Size(1036, 521);
            this.pnlMain.TabIndex = 19;
            // 
            // pnlMapBackground
            // 
            this.pnlMapBackground.BackColor = System.Drawing.SystemColors.ControlLight;
            this.pnlMapBackground.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlMapBackground.Controls.Add(this.btnZoomDown);
            this.pnlMapBackground.Controls.Add(this.btnZoomUp);
            this.pnlMapBackground.Controls.Add(this.lblPlaceZoom);
            this.pnlMapBackground.Controls.Add(this.pnlLocationInfo);
            this.pnlMapBackground.Controls.Add(this.pnlMap);
            this.pnlMapBackground.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMapBackground.Location = new System.Drawing.Point(305, 0);
            this.pnlMapBackground.Name = "pnlMapBackground";
            this.pnlMapBackground.Size = new System.Drawing.Size(731, 521);
            this.pnlMapBackground.TabIndex = 13;
            // 
            // btnZoomDown
            // 
            this.btnZoomDown.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnZoomDown.Location = new System.Drawing.Point(5, 45);
            this.btnZoomDown.Name = "btnZoomDown";
            this.btnZoomDown.Size = new System.Drawing.Size(25, 23);
            this.btnZoomDown.TabIndex = 1;
            this.btnZoomDown.Tag = "0";
            this.btnZoomDown.Text = "-";
            this.btnZoomDown.UseVisualStyleBackColor = true;
            this.btnZoomDown.Click += new System.EventHandler(this.btnZoomDown_Click);
            // 
            // btnZoomUp
            // 
            this.btnZoomUp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.btnZoomUp.Location = new System.Drawing.Point(5, 6);
            this.btnZoomUp.Name = "btnZoomUp";
            this.btnZoomUp.Size = new System.Drawing.Size(25, 23);
            this.btnZoomUp.TabIndex = 0;
            this.btnZoomUp.Text = "+";
            this.btnZoomUp.UseVisualStyleBackColor = true;
            this.btnZoomUp.Click += new System.EventHandler(this.btnZoomUp_Click);
            // 
            // lblPlaceZoom
            // 
            this.lblPlaceZoom.AutoSize = true;
            this.lblPlaceZoom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.lblPlaceZoom.ForeColor = System.Drawing.Color.Black;
            this.lblPlaceZoom.Location = new System.Drawing.Point(8, 30);
            this.lblPlaceZoom.Name = "lblPlaceZoom";
            this.lblPlaceZoom.Size = new System.Drawing.Size(16, 13);
            this.lblPlaceZoom.TabIndex = 17;
            this.lblPlaceZoom.Text = "...";
            // 
            // splitter1
            // 
            this.splitter1.BackColor = System.Drawing.SystemColors.Control;
            this.splitter1.Location = new System.Drawing.Point(300, 0);
            this.splitter1.Name = "splitter1";
            this.splitter1.Size = new System.Drawing.Size(5, 521);
            this.splitter1.TabIndex = 16;
            this.splitter1.TabStop = false;
            // 
            // pnlLeft
            // 
            this.pnlLeft.BackColor = System.Drawing.SystemColors.Control;
            this.pnlLeft.Controls.Add(this.lviAvialableLocations);
            this.pnlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnlLeft.Location = new System.Drawing.Point(0, 0);
            this.pnlLeft.Name = "pnlLeft";
            this.pnlLeft.Size = new System.Drawing.Size(300, 521);
            this.pnlLeft.TabIndex = 15;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(531, 52);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(100, 10);
            this.progressBar1.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.progressBar1.TabIndex = 20;
            this.progressBar1.Visible = false;
            // 
            // btnCancelSearch
            // 
            this.btnCancelSearch.Location = new System.Drawing.Point(637, 25);
            this.btnCancelSearch.Name = "btnCancelSearch";
            this.btnCancelSearch.Size = new System.Drawing.Size(75, 23);
            this.btnCancelSearch.TabIndex = 21;
            this.btnCancelSearch.Text = "Отмена";
            this.btnCancelSearch.UseVisualStyleBackColor = true;
            this.btnCancelSearch.Visible = false;
            this.btnCancelSearch.Click += new System.EventHandler(this.btnCancelSearch_Click);
            // 
            // menuStripMain
            // 
            this.menuStripMain.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile});
            this.menuStripMain.Location = new System.Drawing.Point(0, 0);
            this.menuStripMain.Name = "menuStripMain";
            this.menuStripMain.Size = new System.Drawing.Size(1060, 24);
            this.menuStripMain.TabIndex = 22;
            // 
            // tsmiFile
            // 
            this.tsmiFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiFile_MapProviders});
            this.tsmiFile.Name = "tsmiFile";
            this.tsmiFile.Size = new System.Drawing.Size(48, 20);
            this.tsmiFile.Text = "Файл";
            // 
            // tsmiFile_MapProviders
            // 
            this.tsmiFile_MapProviders.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miFile_MapProviders_Items});
            this.tsmiFile_MapProviders.Name = "tsmiFile_MapProviders";
            this.tsmiFile_MapProviders.Size = new System.Drawing.Size(180, 22);
            this.tsmiFile_MapProviders.Text = "Поставщики карты";
            // 
            // miFile_MapProviders_Items
            // 
            this.miFile_MapProviders_Items.Items.AddRange(new object[] {
            "OpenStreetMap",
            "Яндекс Карты"});
            this.miFile_MapProviders_Items.Name = "miFile_MapProviders_Items";
            this.miFile_MapProviders_Items.Size = new System.Drawing.Size(121, 23);
            this.miFile_MapProviders_Items.SelectedIndexChanged += new System.EventHandler(this.miFile_MapProviders_Items_SelectedIndexChanged);
            // 
            // lblProviderMap
            // 
            this.lblProviderMap.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblProviderMap.AutoSize = true;
            this.lblProviderMap.Location = new System.Drawing.Point(14, 598);
            this.lblProviderMap.Name = "lblProviderMap";
            this.lblProviderMap.Size = new System.Drawing.Size(16, 13);
            this.lblProviderMap.TabIndex = 23;
            this.lblProviderMap.Text = "...";
            // 
            // pnlLocationInfo
            // 
            this.pnlLocationInfo.AutoHeigth = true;
            this.pnlLocationInfo.BackColor = System.Drawing.SystemColors.Control;
            this.pnlLocationInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnlLocationInfo.Controls.Add(this.lLblClosePnlInfoLocation);
            this.pnlLocationInfo.Location = new System.Drawing.Point(491, 6);
            this.pnlLocationInfo.Message = null;
            this.pnlLocationInfo.Name = "pnlLocationInfo";
            this.pnlLocationInfo.OffsetTextTop = 16;
            this.pnlLocationInfo.Size = new System.Drawing.Size(231, 51);
            this.pnlLocationInfo.TabIndex = 20;
            // 
            // lLblClosePnlInfoLocation
            // 
            this.lLblClosePnlInfoLocation.AutoSize = true;
            this.lLblClosePnlInfoLocation.Location = new System.Drawing.Point(179, 0);
            this.lLblClosePnlInfoLocation.Name = "lLblClosePnlInfoLocation";
            this.lLblClosePnlInfoLocation.Size = new System.Drawing.Size(50, 13);
            this.lLblClosePnlInfoLocation.TabIndex = 21;
            this.lLblClosePnlInfoLocation.TabStop = true;
            this.lLblClosePnlInfoLocation.Text = "закрыть";
            this.lLblClosePnlInfoLocation.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lLblClosePnlInfoLocation_LinkClicked);
            // 
            // pnlMap
            // 
            this.pnlMap.BackColor = System.Drawing.SystemColors.Control;
            this.pnlMap.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMap.Location = new System.Drawing.Point(0, 0);
            this.pnlMap.Name = "pnlMap";
            this.pnlMap.Size = new System.Drawing.Size(729, 519);
            this.pnlMap.TabIndex = 18;
            this.pnlMap.Paint += new System.Windows.Forms.PaintEventHandler(this.pnlMap_Paint);
            this.pnlMap.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnlMap_MouseDown);
            this.pnlMap.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pnlMap_MouseMove);
            this.pnlMap.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pnlMap_MouseUp);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 613);
            this.Controls.Add(this.lblProviderMap);
            this.Controls.Add(this.btnCancelSearch);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.pnlMain);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnSearch);
            this.Controls.Add(this.txtSearchLoaction);
            this.Controls.Add(this.menuStripMain);
            this.DoubleBuffered = true;
            this.MainMenuStrip = this.menuStripMain;
            this.Name = "MainForm";
            this.Text = "А_М_В - Геолокация";
            this.pnlMain.ResumeLayout(false);
            this.pnlMapBackground.ResumeLayout(false);
            this.pnlMapBackground.PerformLayout();
            this.pnlLeft.ResumeLayout(false);
            this.menuStripMain.ResumeLayout(false);
            this.menuStripMain.PerformLayout();
            this.pnlLocationInfo.ResumeLayout(false);
            this.pnlLocationInfo.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DoubleBufferedPanel pnlMap;
        private System.Windows.Forms.ListView lviAvialableLocations;
        private System.Windows.Forms.TextBox txtSearchLoaction;
        private System.Windows.Forms.Button btnSearch;
        private System.Windows.Forms.ColumnHeader clmnNamePlace;
        private System.Windows.Forms.ColumnHeader clmnTypePlace;
        private System.Windows.Forms.ColumnHeader clmnClassPlace;
        private System.Windows.Forms.Button btnZoomDown;
        private System.Windows.Forms.Button btnZoomUp;
        private System.Windows.Forms.Label lblPlaceZoom;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel pnlMain;
        private System.Windows.Forms.Panel pnlLeft;
        private System.Windows.Forms.Splitter splitter1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Button btnCancelSearch;
        private MessagePanel pnlLocationInfo;
        private System.Windows.Forms.Panel pnlMapBackground;
        private System.Windows.Forms.LinkLabel lLblClosePnlInfoLocation;
        private System.Windows.Forms.MenuStrip menuStripMain;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile;
        private System.Windows.Forms.ToolStripMenuItem tsmiFile_MapProviders;
        private System.Windows.Forms.ToolStripComboBox miFile_MapProviders_Items;
        private System.Windows.Forms.Label lblProviderMap;
    }
}

