namespace MatrixBuilder
{
    partial class frmMatrixSB
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMatrixSB));
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblDefinition = new System.Windows.Forms.Label();
            this.panBottom = new System.Windows.Forms.Panel();
            this.butPlus = new MetroFramework.Controls.MetroButton();
            this.lblMatrixScale = new System.Windows.Forms.Label();
            this.mtrb = new MetroFramework.Controls.MetroTrackBar();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panFooterRight = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.togOpenSB = new MetroFramework.Controls.MetroToggle();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOk = new MetroFramework.Controls.MetroButton();
            this.splitContMain = new System.Windows.Forms.SplitContainer();
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.panLeftHeader = new System.Windows.Forms.Panel();
            this.lblSelectRowDescription = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblMatrixCaption = new System.Windows.Forms.Label();
            this.lblNotices = new System.Windows.Forms.Label();
            this.txtNotices = new System.Windows.Forms.TextBox();
            this.panRightLeft = new System.Windows.Forms.Panel();
            this.lstbMatrixRows = new System.Windows.Forms.CheckedListBox();
            this.panRightLeftTop = new System.Windows.Forms.Panel();
            this.lblMatrixRows = new System.Windows.Forms.Label();
            this.lblMatrixDescription = new System.Windows.Forms.Label();
            this.txtbDescription = new System.Windows.Forms.TextBox();
            this.lblSBName = new System.Windows.Forms.Label();
            this.txtbSBName = new System.Windows.Forms.TextBox();
            this.lblSBTemplate = new System.Windows.Forms.Label();
            this.cboSBTemplate = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.lblSBDetails = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panBottom.SuspendLayout();
            this.panFooterRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).BeginInit();
            this.splitContMain.Panel1.SuspendLayout();
            this.splitContMain.Panel2.SuspendLayout();
            this.splitContMain.SuspendLayout();
            this.panLeftHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panRightLeft.SuspendLayout();
            this.panRightLeftTop.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(10, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 187;
            this.pictureBox2.TabStop = false;
            // 
            // panHeader
            // 
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(10, 60);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(994, 12);
            this.panHeader.TabIndex = 188;
            // 
            // lblDefinition
            // 
            this.lblDefinition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefinition.ForeColor = System.Drawing.Color.White;
            this.lblDefinition.Location = new System.Drawing.Point(311, 26);
            this.lblDefinition.Name = "lblDefinition";
            this.lblDefinition.Size = new System.Drawing.Size(688, 34);
            this.lblDefinition.TabIndex = 189;
            this.lblDefinition.Text = "Storyboards are MS Word documents with fields associated with a particular Matrix" +
    " Template. A Matrix Template may have one or more Storyboard templates.";
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.butPlus);
            this.panBottom.Controls.Add(this.lblMatrixScale);
            this.panBottom.Controls.Add(this.mtrb);
            this.panBottom.Controls.Add(this.lblMessage);
            this.panBottom.Controls.Add(this.panFooterRight);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(10, 403);
            this.panBottom.Name = "panBottom";
            this.panBottom.Padding = new System.Windows.Forms.Padding(0, 10, 0, 0);
            this.panBottom.Size = new System.Drawing.Size(994, 47);
            this.panBottom.TabIndex = 190;
            // 
            // butPlus
            // 
            this.butPlus.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butPlus.BackgroundImage")));
            this.butPlus.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butPlus.ForeColor = System.Drawing.Color.White;
            this.butPlus.Location = new System.Drawing.Point(304, 6);
            this.butPlus.Name = "butPlus";
            this.butPlus.Size = new System.Drawing.Size(28, 28);
            this.butPlus.TabIndex = 210;
            this.butPlus.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butPlus.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butPlus.UseSelectable = true;
            this.butPlus.Visible = false;
            this.butPlus.Click += new System.EventHandler(this.butPlus_Click);
            // 
            // lblMatrixScale
            // 
            this.lblMatrixScale.AutoSize = true;
            this.lblMatrixScale.Dock = System.Windows.Forms.DockStyle.Left;
            this.lblMatrixScale.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatrixScale.ForeColor = System.Drawing.Color.White;
            this.lblMatrixScale.Location = new System.Drawing.Point(0, 10);
            this.lblMatrixScale.Name = "lblMatrixScale";
            this.lblMatrixScale.Size = new System.Drawing.Size(40, 17);
            this.lblMatrixScale.TabIndex = 209;
            this.lblMatrixScale.Text = "100%";
            // 
            // mtrb
            // 
            this.mtrb.BackColor = System.Drawing.Color.Transparent;
            this.mtrb.Location = new System.Drawing.Point(46, 12);
            this.mtrb.Maximum = 40;
            this.mtrb.Name = "mtrb";
            this.mtrb.Size = new System.Drawing.Size(239, 15);
            this.mtrb.TabIndex = 208;
            this.mtrb.Text = "metroTrackBar1";
            this.mtrb.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.mtrb.Value = 10;
            this.mtrb.Scroll += new System.Windows.Forms.ScrollEventHandler(this.mtrb_Scroll);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(16, 26);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 17);
            this.lblMessage.TabIndex = 207;
            // 
            // panFooterRight
            // 
            this.panFooterRight.Controls.Add(this.label1);
            this.panFooterRight.Controls.Add(this.togOpenSB);
            this.panFooterRight.Controls.Add(this.butCancel);
            this.panFooterRight.Controls.Add(this.butOk);
            this.panFooterRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panFooterRight.Location = new System.Drawing.Point(513, 10);
            this.panFooterRight.Name = "panFooterRight";
            this.panFooterRight.Size = new System.Drawing.Size(481, 37);
            this.panFooterRight.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(110, 17);
            this.label1.TabIndex = 203;
            this.label1.Text = "Open Storyboard";
            // 
            // togOpenSB
            // 
            this.togOpenSB.AutoSize = true;
            this.togOpenSB.DisplayStatus = false;
            this.togOpenSB.Location = new System.Drawing.Point(125, 15);
            this.togOpenSB.Name = "togOpenSB";
            this.togOpenSB.Size = new System.Drawing.Size(50, 17);
            this.togOpenSB.TabIndex = 12;
            this.togOpenSB.Text = "Off";
            this.togOpenSB.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.togOpenSB.UseSelectable = true;
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(401, 9);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 10;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(255, 9);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(131, 23);
            this.butOk.TabIndex = 9;
            this.butOk.Text = "Generate Storyboard";
            this.butOk.UseSelectable = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // splitContMain
            // 
            this.splitContMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContMain.Location = new System.Drawing.Point(10, 72);
            this.splitContMain.Name = "splitContMain";
            // 
            // splitContMain.Panel1
            // 
            this.splitContMain.Panel1.Controls.Add(this.reoGridControl1);
            this.splitContMain.Panel1.Controls.Add(this.panLeftHeader);
            // 
            // splitContMain.Panel2
            // 
            this.splitContMain.Panel2.Controls.Add(this.lblNotices);
            this.splitContMain.Panel2.Controls.Add(this.txtNotices);
            this.splitContMain.Panel2.Controls.Add(this.panRightLeft);
            this.splitContMain.Panel2.Controls.Add(this.lblMatrixDescription);
            this.splitContMain.Panel2.Controls.Add(this.txtbDescription);
            this.splitContMain.Panel2.Controls.Add(this.lblSBName);
            this.splitContMain.Panel2.Controls.Add(this.txtbSBName);
            this.splitContMain.Panel2.Controls.Add(this.lblSBTemplate);
            this.splitContMain.Panel2.Controls.Add(this.cboSBTemplate);
            this.splitContMain.Panel2.Controls.Add(this.panel1);
            this.splitContMain.Size = new System.Drawing.Size(994, 331);
            this.splitContMain.SplitterDistance = 538;
            this.splitContMain.TabIndex = 191;
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(0, 35);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.SheetTabVisible = true;
            this.reoGridControl1.SheetTabWidth = 60;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.Size = new System.Drawing.Size(538, 296);
            this.reoGridControl1.TabIndex = 3;
            this.reoGridControl1.Text = "reoGridControl1";
            // 
            // panLeftHeader
            // 
            this.panLeftHeader.Controls.Add(this.lblSelectRowDescription);
            this.panLeftHeader.Controls.Add(this.pictureBox1);
            this.panLeftHeader.Controls.Add(this.lblMatrixCaption);
            this.panLeftHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panLeftHeader.Location = new System.Drawing.Point(0, 0);
            this.panLeftHeader.Name = "panLeftHeader";
            this.panLeftHeader.Size = new System.Drawing.Size(538, 35);
            this.panLeftHeader.TabIndex = 2;
            // 
            // lblSelectRowDescription
            // 
            this.lblSelectRowDescription.AutoSize = true;
            this.lblSelectRowDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectRowDescription.ForeColor = System.Drawing.Color.White;
            this.lblSelectRowDescription.Location = new System.Drawing.Point(223, 9);
            this.lblSelectRowDescription.Name = "lblSelectRowDescription";
            this.lblSelectRowDescription.Size = new System.Drawing.Size(196, 17);
            this.lblSelectRowDescription.TabIndex = 190;
            this.lblSelectRowDescription.Text = "Click on a row or select a range.";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(6, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(28, 28);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 187;
            this.pictureBox1.TabStop = false;
            // 
            // lblMatrixCaption
            // 
            this.lblMatrixCaption.AutoSize = true;
            this.lblMatrixCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatrixCaption.ForeColor = System.Drawing.Color.White;
            this.lblMatrixCaption.Location = new System.Drawing.Point(38, 6);
            this.lblMatrixCaption.Name = "lblMatrixCaption";
            this.lblMatrixCaption.Size = new System.Drawing.Size(151, 21);
            this.lblMatrixCaption.TabIndex = 186;
            this.lblMatrixCaption.Text = "Matrix - Select Rows";
            // 
            // lblNotices
            // 
            this.lblNotices.AutoSize = true;
            this.lblNotices.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotices.ForeColor = System.Drawing.Color.White;
            this.lblNotices.Location = new System.Drawing.Point(304, 149);
            this.lblNotices.Name = "lblNotices";
            this.lblNotices.Size = new System.Drawing.Size(133, 17);
            this.lblNotices.TabIndex = 210;
            this.lblNotices.Text = "Selected Matrix Rows";
            this.lblNotices.Visible = false;
            this.lblNotices.TextChanged += new System.EventHandler(this.lblNotices_TextChanged);
            // 
            // txtNotices
            // 
            this.txtNotices.BackColor = System.Drawing.Color.Black;
            this.txtNotices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtNotices.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.txtNotices.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNotices.ForeColor = System.Drawing.Color.Lime;
            this.txtNotices.Location = new System.Drawing.Point(155, 275);
            this.txtNotices.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.txtNotices.MaxLength = 50;
            this.txtNotices.Multiline = true;
            this.txtNotices.Name = "txtNotices";
            this.txtNotices.Size = new System.Drawing.Size(297, 56);
            this.txtNotices.TabIndex = 209;
            this.txtNotices.TextChanged += new System.EventHandler(this.txtNotices_TextChanged);
            // 
            // panRightLeft
            // 
            this.panRightLeft.Controls.Add(this.lstbMatrixRows);
            this.panRightLeft.Controls.Add(this.panRightLeftTop);
            this.panRightLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panRightLeft.Location = new System.Drawing.Point(0, 35);
            this.panRightLeft.Name = "panRightLeft";
            this.panRightLeft.Padding = new System.Windows.Forms.Padding(5, 0, 0, 0);
            this.panRightLeft.Size = new System.Drawing.Size(155, 296);
            this.panRightLeft.TabIndex = 208;
            // 
            // lstbMatrixRows
            // 
            this.lstbMatrixRows.BackColor = System.Drawing.Color.Black;
            this.lstbMatrixRows.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbMatrixRows.CheckOnClick = true;
            this.lstbMatrixRows.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbMatrixRows.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbMatrixRows.ForeColor = System.Drawing.Color.White;
            this.lstbMatrixRows.FormattingEnabled = true;
            this.lstbMatrixRows.Location = new System.Drawing.Point(5, 32);
            this.lstbMatrixRows.Name = "lstbMatrixRows";
            this.lstbMatrixRows.Size = new System.Drawing.Size(150, 264);
            this.lstbMatrixRows.TabIndex = 208;
            this.lstbMatrixRows.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstbMatrixRows_ItemCheck);
            this.lstbMatrixRows.Click += new System.EventHandler(this.lstbMatrixRows_Click);
            this.lstbMatrixRows.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstbMatrixRows_MouseUp);
            // 
            // panRightLeftTop
            // 
            this.panRightLeftTop.Controls.Add(this.lblMatrixRows);
            this.panRightLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panRightLeftTop.Location = new System.Drawing.Point(5, 0);
            this.panRightLeftTop.Name = "panRightLeftTop";
            this.panRightLeftTop.Size = new System.Drawing.Size(150, 32);
            this.panRightLeftTop.TabIndex = 0;
            // 
            // lblMatrixRows
            // 
            this.lblMatrixRows.AutoSize = true;
            this.lblMatrixRows.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatrixRows.ForeColor = System.Drawing.Color.White;
            this.lblMatrixRows.Location = new System.Drawing.Point(4, 10);
            this.lblMatrixRows.Name = "lblMatrixRows";
            this.lblMatrixRows.Size = new System.Drawing.Size(133, 17);
            this.lblMatrixRows.TabIndex = 202;
            this.lblMatrixRows.Text = "Selected Matrix Rows";
            // 
            // lblMatrixDescription
            // 
            this.lblMatrixDescription.AutoSize = true;
            this.lblMatrixDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatrixDescription.ForeColor = System.Drawing.Color.White;
            this.lblMatrixDescription.Location = new System.Drawing.Point(173, 160);
            this.lblMatrixDescription.Name = "lblMatrixDescription";
            this.lblMatrixDescription.Size = new System.Drawing.Size(134, 17);
            this.lblMatrixDescription.TabIndex = 205;
            this.lblMatrixDescription.Text = "Description (optional)";
            // 
            // txtbDescription
            // 
            this.txtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDescription.Location = new System.Drawing.Point(176, 180);
            this.txtbDescription.MaxLength = 250;
            this.txtbDescription.Multiline = true;
            this.txtbDescription.Name = "txtbDescription";
            this.txtbDescription.Size = new System.Drawing.Size(261, 74);
            this.txtbDescription.TabIndex = 8;
            // 
            // lblSBName
            // 
            this.lblSBName.AutoSize = true;
            this.lblSBName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSBName.ForeColor = System.Drawing.Color.White;
            this.lblSBName.Location = new System.Drawing.Point(175, 37);
            this.lblSBName.Name = "lblSBName";
            this.lblSBName.Size = new System.Drawing.Size(113, 17);
            this.lblSBName.TabIndex = 203;
            this.lblSBName.Text = "Storyboard Name";
            // 
            // txtbSBName
            // 
            this.txtbSBName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbSBName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbSBName.Location = new System.Drawing.Point(177, 57);
            this.txtbSBName.MaxLength = 50;
            this.txtbSBName.Name = "txtbSBName";
            this.txtbSBName.Size = new System.Drawing.Size(205, 25);
            this.txtbSBName.TabIndex = 0;
            // 
            // lblSBTemplate
            // 
            this.lblSBTemplate.AutoSize = true;
            this.lblSBTemplate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSBTemplate.ForeColor = System.Drawing.Color.White;
            this.lblSBTemplate.Location = new System.Drawing.Point(176, 99);
            this.lblSBTemplate.Name = "lblSBTemplate";
            this.lblSBTemplate.Size = new System.Drawing.Size(180, 17);
            this.lblSBTemplate.TabIndex = 191;
            this.lblSBTemplate.Text = "Select a Storyboard Template";
            // 
            // cboSBTemplate
            // 
            this.cboSBTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSBTemplate.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cboSBTemplate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSBTemplate.FormattingEnabled = true;
            this.cboSBTemplate.ItemHeight = 17;
            this.cboSBTemplate.Location = new System.Drawing.Point(179, 121);
            this.cboSBTemplate.Name = "cboSBTemplate";
            this.cboSBTemplate.Size = new System.Drawing.Size(203, 25);
            this.cboSBTemplate.TabIndex = 190;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.pictureBox3);
            this.panel1.Controls.Add(this.lblSBDetails);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(452, 35);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.Location = new System.Drawing.Point(8, 3);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(28, 28);
            this.pictureBox3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox3.TabIndex = 187;
            this.pictureBox3.TabStop = false;
            // 
            // lblSBDetails
            // 
            this.lblSBDetails.AutoSize = true;
            this.lblSBDetails.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSBDetails.ForeColor = System.Drawing.Color.White;
            this.lblSBDetails.Location = new System.Drawing.Point(38, 6);
            this.lblSBDetails.Name = "lblSBDetails";
            this.lblSBDetails.Size = new System.Drawing.Size(138, 21);
            this.lblSBDetails.TabIndex = 186;
            this.lblSBDetails.Text = "Storyboard Details";
            // 
            // frmMatrixSB
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1014, 460);
            this.Controls.Add(this.splitContMain);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.lblDefinition);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.pictureBox2);
            this.Name = "frmMatrixSB";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.Text = "      Storyboard Generation";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmMatrixSB_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panBottom.ResumeLayout(false);
            this.panBottom.PerformLayout();
            this.panFooterRight.ResumeLayout(false);
            this.panFooterRight.PerformLayout();
            this.splitContMain.Panel1.ResumeLayout(false);
            this.splitContMain.Panel2.ResumeLayout(false);
            this.splitContMain.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).EndInit();
            this.splitContMain.ResumeLayout(false);
            this.panLeftHeader.ResumeLayout(false);
            this.panLeftHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panRightLeft.ResumeLayout(false);
            this.panRightLeftTop.ResumeLayout(false);
            this.panRightLeftTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblDefinition;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel panFooterRight;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOk;
        private System.Windows.Forms.SplitContainer splitContMain;
        private System.Windows.Forms.Panel panLeftHeader;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblMatrixCaption;
        private System.Windows.Forms.Label lblMatrixDescription;
        private System.Windows.Forms.TextBox txtbDescription;
        private System.Windows.Forms.Label lblSBName;
        private System.Windows.Forms.TextBox txtbSBName;
        private System.Windows.Forms.Label lblSBTemplate;
        private System.Windows.Forms.ComboBox cboSBTemplate;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Label lblSBDetails;
        private System.Windows.Forms.Label lblMatrixScale;
        private MetroFramework.Controls.MetroTrackBar mtrb;
        private System.Windows.Forms.TextBox txtNotices;
        private System.Windows.Forms.Panel panRightLeft;
        private System.Windows.Forms.CheckedListBox lstbMatrixRows;
        private System.Windows.Forms.Panel panRightLeftTop;
        private System.Windows.Forms.Label lblMatrixRows;
        private MetroFramework.Controls.MetroButton butPlus;
        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private System.Windows.Forms.Label lblSelectRowDescription;
        private System.Windows.Forms.Label lblNotices;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroToggle togOpenSB;

    }
}