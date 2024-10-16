namespace ProfessionalDocAnalyzer
{
    partial class frmImportDic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmImportDic));
            this.lblDicFileDicName = new System.Windows.Forms.Label();
            this.panLeft = new System.Windows.Forms.Panel();
            this.butExcel = new MetroFramework.Controls.MetroButton();
            this.butDictionary = new MetroFramework.Controls.MetroButton();
            this.butWorkgroup = new MetroFramework.Controls.MetroButton();
            this.butKeywordGroup = new MetroFramework.Controls.MetroButton();
            this.lblKeywordGroup = new System.Windows.Forms.Label();
            this.lblWorkgroup = new System.Windows.Forms.Label();
            this.lblDictionary = new System.Windows.Forms.Label();
            this.lblExcel = new System.Windows.Forms.Label();
            this.txtDicFileDicName = new System.Windows.Forms.TextBox();
            this.lblDicFileXML = new System.Windows.Forms.Label();
            this.butSelectDicFile = new MetroFramework.Controls.MetroButton();
            this.txtDictionaryFile = new System.Windows.Forms.TextBox();
            this.lblDictionaryFile = new System.Windows.Forms.Label();
            this.txtbDicFileMessage = new System.Windows.Forms.TextBox();
            this.lblStatus = new System.Windows.Forms.Label();
            this.lblDicFileMessage = new System.Windows.Forms.Label();
            this.panDictionary = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.panFooter = new System.Windows.Forms.Panel();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panExcel = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.lblExcelColumns = new System.Windows.Forms.Label();
            this.txtbExcelColumns = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbExcelDictionaryName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.butSelectExcelFile = new MetroFramework.Controls.MetroButton();
            this.txtExcelFile = new System.Windows.Forms.TextBox();
            this.picExcelCaption = new System.Windows.Forms.PictureBox();
            this.lblExcelCaption = new System.Windows.Forms.Label();
            this.panKeywordGroup = new System.Windows.Forms.Panel();
            this.lstKeywordGroups = new System.Windows.Forms.ListBox();
            this.lblbKwG = new System.Windows.Forms.Label();
            this.txtbKwG = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtbDicNameKeywords = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboSheetName = new System.Windows.Forms.ComboBox();
            this.panLeft.SuspendLayout();
            this.panDictionary.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panFooter.SuspendLayout();
            this.panExcel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcelCaption)).BeginInit();
            this.panKeywordGroup.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDicFileDicName
            // 
            this.lblDicFileDicName.AutoSize = true;
            this.lblDicFileDicName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDicFileDicName.ForeColor = System.Drawing.Color.Black;
            this.lblDicFileDicName.Location = new System.Drawing.Point(12, 137);
            this.lblDicFileDicName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDicFileDicName.Name = "lblDicFileDicName";
            this.lblDicFileDicName.Size = new System.Drawing.Size(138, 23);
            this.lblDicFileDicName.TabIndex = 151;
            this.lblDicFileDicName.Text = "Dictionary Name";
            // 
            // panLeft
            // 
            this.panLeft.Controls.Add(this.butExcel);
            this.panLeft.Controls.Add(this.butDictionary);
            this.panLeft.Controls.Add(this.butWorkgroup);
            this.panLeft.Controls.Add(this.butKeywordGroup);
            this.panLeft.Controls.Add(this.lblKeywordGroup);
            this.panLeft.Controls.Add(this.lblWorkgroup);
            this.panLeft.Controls.Add(this.lblDictionary);
            this.panLeft.Controls.Add(this.lblExcel);
            this.panLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panLeft.Location = new System.Drawing.Point(27, 74);
            this.panLeft.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panLeft.Name = "panLeft";
            this.panLeft.Size = new System.Drawing.Size(171, 324);
            this.panLeft.TabIndex = 148;
            // 
            // butExcel
            // 
            this.butExcel.BackgroundImage = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_office_excel;
            this.butExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butExcel.ForeColor = System.Drawing.Color.White;
            this.butExcel.Location = new System.Drawing.Point(8, 6);
            this.butExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butExcel.Name = "butExcel";
            this.butExcel.Size = new System.Drawing.Size(51, 47);
            this.butExcel.TabIndex = 158;
            this.butExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butExcel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butExcel.UseSelectable = true;
            this.butExcel.Click += new System.EventHandler(this.butExcel_Click);
            // 
            // butDictionary
            // 
            this.butDictionary.BackgroundImage = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_book_perspective;
            this.butDictionary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butDictionary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butDictionary.ForeColor = System.Drawing.Color.White;
            this.butDictionary.Location = new System.Drawing.Point(8, 80);
            this.butDictionary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butDictionary.Name = "butDictionary";
            this.butDictionary.Size = new System.Drawing.Size(51, 47);
            this.butDictionary.TabIndex = 157;
            this.butDictionary.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butDictionary.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butDictionary.UseSelectable = true;
            this.butDictionary.Click += new System.EventHandler(this.butDictionary_Click);
            // 
            // butWorkgroup
            // 
            this.butWorkgroup.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butWorkgroup.BackgroundImage")));
            this.butWorkgroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butWorkgroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butWorkgroup.ForeColor = System.Drawing.Color.White;
            this.butWorkgroup.Location = new System.Drawing.Point(5, 304);
            this.butWorkgroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butWorkgroup.Name = "butWorkgroup";
            this.butWorkgroup.Size = new System.Drawing.Size(51, 47);
            this.butWorkgroup.TabIndex = 156;
            this.butWorkgroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butWorkgroup.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butWorkgroup.UseSelectable = true;
            this.butWorkgroup.Visible = false;
            // 
            // butKeywordGroup
            // 
            this.butKeywordGroup.BackgroundImage = global::ProfessionalDocAnalyzer.Properties.Resources.appbar2;
            this.butKeywordGroup.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butKeywordGroup.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butKeywordGroup.ForeColor = System.Drawing.Color.White;
            this.butKeywordGroup.Location = new System.Drawing.Point(8, 150);
            this.butKeywordGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butKeywordGroup.Name = "butKeywordGroup";
            this.butKeywordGroup.Size = new System.Drawing.Size(51, 47);
            this.butKeywordGroup.TabIndex = 155;
            this.butKeywordGroup.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butKeywordGroup.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butKeywordGroup.UseSelectable = true;
            this.butKeywordGroup.Click += new System.EventHandler(this.butKeywordGroup_Click);
            // 
            // lblKeywordGroup
            // 
            this.lblKeywordGroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblKeywordGroup.ForeColor = System.Drawing.Color.Black;
            this.lblKeywordGroup.Location = new System.Drawing.Point(64, 150);
            this.lblKeywordGroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblKeywordGroup.Name = "lblKeywordGroup";
            this.lblKeywordGroup.Size = new System.Drawing.Size(96, 73);
            this.lblKeywordGroup.TabIndex = 153;
            this.lblKeywordGroup.Text = "Keyword Group";
            this.lblKeywordGroup.Click += new System.EventHandler(this.lblKeywordGroup_Click);
            // 
            // lblWorkgroup
            // 
            this.lblWorkgroup.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkgroup.ForeColor = System.Drawing.Color.White;
            this.lblWorkgroup.Location = new System.Drawing.Point(64, 304);
            this.lblWorkgroup.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWorkgroup.Name = "lblWorkgroup";
            this.lblWorkgroup.Size = new System.Drawing.Size(99, 53);
            this.lblWorkgroup.TabIndex = 150;
            this.lblWorkgroup.Text = "Copy from Workgroup";
            this.lblWorkgroup.Visible = false;
            // 
            // lblDictionary
            // 
            this.lblDictionary.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDictionary.ForeColor = System.Drawing.Color.Black;
            this.lblDictionary.Location = new System.Drawing.Point(67, 80);
            this.lblDictionary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDictionary.Name = "lblDictionary";
            this.lblDictionary.Size = new System.Drawing.Size(96, 49);
            this.lblDictionary.TabIndex = 147;
            this.lblDictionary.Text = "Dictionary File";
            this.lblDictionary.Click += new System.EventHandler(this.lblDictionary_Click);
            // 
            // lblExcel
            // 
            this.lblExcel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExcel.ForeColor = System.Drawing.Color.Black;
            this.lblExcel.Location = new System.Drawing.Point(67, 6);
            this.lblExcel.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExcel.Name = "lblExcel";
            this.lblExcel.Size = new System.Drawing.Size(75, 54);
            this.lblExcel.TabIndex = 144;
            this.lblExcel.Text = "Excel File";
            this.lblExcel.Click += new System.EventHandler(this.lblExcel_Click);
            // 
            // txtDicFileDicName
            // 
            this.txtDicFileDicName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDicFileDicName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDicFileDicName.Location = new System.Drawing.Point(16, 160);
            this.txtDicFileDicName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDicFileDicName.MaxLength = 50;
            this.txtDicFileDicName.Name = "txtDicFileDicName";
            this.txtDicFileDicName.Size = new System.Drawing.Size(222, 29);
            this.txtDicFileDicName.TabIndex = 150;
            // 
            // lblDicFileXML
            // 
            this.lblDicFileXML.AutoSize = true;
            this.lblDicFileXML.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDicFileXML.ForeColor = System.Drawing.Color.Black;
            this.lblDicFileXML.Location = new System.Drawing.Point(12, 60);
            this.lblDicFileXML.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDicFileXML.Name = "lblDicFileXML";
            this.lblDicFileXML.Size = new System.Drawing.Size(173, 23);
            this.lblDicFileXML.TabIndex = 149;
            this.lblDicFileXML.Text = "Dictionary File (*.dicx)";
            // 
            // butSelectDicFile
            // 
            this.butSelectDicFile.Location = new System.Drawing.Point(675, 86);
            this.butSelectDicFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butSelectDicFile.Name = "butSelectDicFile";
            this.butSelectDicFile.Size = new System.Drawing.Size(100, 28);
            this.butSelectDicFile.TabIndex = 148;
            this.butSelectDicFile.Text = "Select";
            this.butSelectDicFile.UseSelectable = true;
            this.butSelectDicFile.Click += new System.EventHandler(this.butSelectDicFile_Click);
            // 
            // txtDictionaryFile
            // 
            this.txtDictionaryFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDictionaryFile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDictionaryFile.Location = new System.Drawing.Point(16, 84);
            this.txtDictionaryFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtDictionaryFile.MaxLength = 50;
            this.txtDictionaryFile.Name = "txtDictionaryFile";
            this.txtDictionaryFile.Size = new System.Drawing.Size(650, 29);
            this.txtDictionaryFile.TabIndex = 147;
            // 
            // lblDictionaryFile
            // 
            this.lblDictionaryFile.AutoSize = true;
            this.lblDictionaryFile.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDictionaryFile.ForeColor = System.Drawing.Color.Black;
            this.lblDictionaryFile.Location = new System.Drawing.Point(75, 17);
            this.lblDictionaryFile.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDictionaryFile.Name = "lblDictionaryFile";
            this.lblDictionaryFile.Size = new System.Drawing.Size(202, 28);
            this.lblDictionaryFile.TabIndex = 146;
            this.lblDictionaryFile.Text = "Import Dictionary File";
            // 
            // txtbDicFileMessage
            // 
            this.txtbDicFileMessage.BackColor = System.Drawing.Color.White;
            this.txtbDicFileMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbDicFileMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDicFileMessage.ForeColor = System.Drawing.Color.Black;
            this.txtbDicFileMessage.Location = new System.Drawing.Point(256, 137);
            this.txtbDicFileMessage.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbDicFileMessage.Multiline = true;
            this.txtbDicFileMessage.Name = "txtbDicFileMessage";
            this.txtbDicFileMessage.Size = new System.Drawing.Size(528, 146);
            this.txtbDicFileMessage.TabIndex = 152;
            // 
            // lblStatus
            // 
            this.lblStatus.AutoSize = true;
            this.lblStatus.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStatus.ForeColor = System.Drawing.Color.White;
            this.lblStatus.Location = new System.Drawing.Point(171, 34);
            this.lblStatus.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblStatus.Name = "lblStatus";
            this.lblStatus.Size = new System.Drawing.Size(0, 28);
            this.lblStatus.TabIndex = 153;
            // 
            // lblDicFileMessage
            // 
            this.lblDicFileMessage.AutoSize = true;
            this.lblDicFileMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDicFileMessage.ForeColor = System.Drawing.Color.White;
            this.lblDicFileMessage.Location = new System.Drawing.Point(76, 213);
            this.lblDicFileMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDicFileMessage.Name = "lblDicFileMessage";
            this.lblDicFileMessage.Size = new System.Drawing.Size(0, 23);
            this.lblDicFileMessage.TabIndex = 153;
            this.lblDicFileMessage.Visible = false;
            // 
            // panDictionary
            // 
            this.panDictionary.Controls.Add(this.lblDicFileMessage);
            this.panDictionary.Controls.Add(this.txtbDicFileMessage);
            this.panDictionary.Controls.Add(this.lblDicFileDicName);
            this.panDictionary.Controls.Add(this.txtDicFileDicName);
            this.panDictionary.Controls.Add(this.lblDicFileXML);
            this.panDictionary.Controls.Add(this.butSelectDicFile);
            this.panDictionary.Controls.Add(this.txtDictionaryFile);
            this.panDictionary.Controls.Add(this.pictureBox1);
            this.panDictionary.Controls.Add(this.lblDictionaryFile);
            this.panDictionary.Location = new System.Drawing.Point(872, 101);
            this.panDictionary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panDictionary.Name = "panDictionary";
            this.panDictionary.Size = new System.Drawing.Size(823, 302);
            this.panDictionary.TabIndex = 151;
            this.panDictionary.Visible = false;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_book_perspective;
            this.pictureBox1.Location = new System.Drawing.Point(16, 10);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(51, 47);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 145;
            this.pictureBox1.TabStop = false;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(857, 17);
            this.butCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(100, 28);
            this.butCancel.TabIndex = 101;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(737, 17);
            this.butOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(100, 28);
            this.butOK.TabIndex = 102;
            this.butOK.Text = "Import";
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // panFooter
            // 
            this.panFooter.Controls.Add(this.butCancel);
            this.panFooter.Controls.Add(this.butOK);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(27, 398);
            this.panFooter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(961, 49);
            this.panFooter.TabIndex = 149;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // panExcel
            // 
            this.panExcel.Controls.Add(this.cboSheetName);
            this.panExcel.Controls.Add(this.label3);
            this.panExcel.Controls.Add(this.lblExcelColumns);
            this.panExcel.Controls.Add(this.txtbExcelColumns);
            this.panExcel.Controls.Add(this.label2);
            this.panExcel.Controls.Add(this.txtbExcelDictionaryName);
            this.panExcel.Controls.Add(this.label1);
            this.panExcel.Controls.Add(this.butSelectExcelFile);
            this.panExcel.Controls.Add(this.txtExcelFile);
            this.panExcel.Controls.Add(this.picExcelCaption);
            this.panExcel.Controls.Add(this.lblExcelCaption);
            this.panExcel.Location = new System.Drawing.Point(260, 49);
            this.panExcel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panExcel.Name = "panExcel";
            this.panExcel.Size = new System.Drawing.Size(801, 302);
            this.panExcel.TabIndex = 154;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(12, 130);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(147, 23);
            this.label3.TabIndex = 155;
            this.label3.Text = "Excel Sheet Name";
            // 
            // lblExcelColumns
            // 
            this.lblExcelColumns.AutoSize = true;
            this.lblExcelColumns.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExcelColumns.ForeColor = System.Drawing.Color.White;
            this.lblExcelColumns.Location = new System.Drawing.Point(76, 213);
            this.lblExcelColumns.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExcelColumns.Name = "lblExcelColumns";
            this.lblExcelColumns.Size = new System.Drawing.Size(0, 23);
            this.lblExcelColumns.TabIndex = 153;
            this.lblExcelColumns.Visible = false;
            // 
            // txtbExcelColumns
            // 
            this.txtbExcelColumns.BackColor = System.Drawing.Color.White;
            this.txtbExcelColumns.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbExcelColumns.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbExcelColumns.ForeColor = System.Drawing.Color.Black;
            this.txtbExcelColumns.Location = new System.Drawing.Point(268, 130);
            this.txtbExcelColumns.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbExcelColumns.Multiline = true;
            this.txtbExcelColumns.Name = "txtbExcelColumns";
            this.txtbExcelColumns.Size = new System.Drawing.Size(511, 153);
            this.txtbExcelColumns.TabIndex = 152;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(12, 202);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(138, 23);
            this.label2.TabIndex = 151;
            this.label2.Text = "Dictionary Name";
            // 
            // txtbExcelDictionaryName
            // 
            this.txtbExcelDictionaryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbExcelDictionaryName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbExcelDictionaryName.Location = new System.Drawing.Point(16, 231);
            this.txtbExcelDictionaryName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbExcelDictionaryName.MaxLength = 50;
            this.txtbExcelDictionaryName.Name = "txtbExcelDictionaryName";
            this.txtbExcelDictionaryName.Size = new System.Drawing.Size(222, 29);
            this.txtbExcelDictionaryName.TabIndex = 150;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(78, 23);
            this.label1.TabIndex = 149;
            this.label1.Text = "Excel File";
            // 
            // butSelectExcelFile
            // 
            this.butSelectExcelFile.Location = new System.Drawing.Point(675, 86);
            this.butSelectExcelFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butSelectExcelFile.Name = "butSelectExcelFile";
            this.butSelectExcelFile.Size = new System.Drawing.Size(100, 28);
            this.butSelectExcelFile.TabIndex = 148;
            this.butSelectExcelFile.Text = "Select";
            this.butSelectExcelFile.UseSelectable = true;
            this.butSelectExcelFile.Click += new System.EventHandler(this.butSelectExcelFile_Click);
            // 
            // txtExcelFile
            // 
            this.txtExcelFile.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtExcelFile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExcelFile.Location = new System.Drawing.Point(16, 84);
            this.txtExcelFile.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtExcelFile.MaxLength = 255;
            this.txtExcelFile.Name = "txtExcelFile";
            this.txtExcelFile.Size = new System.Drawing.Size(650, 29);
            this.txtExcelFile.TabIndex = 147;
            // 
            // picExcelCaption
            // 
            this.picExcelCaption.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picExcelCaption.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_office_excel;
            this.picExcelCaption.Location = new System.Drawing.Point(16, 10);
            this.picExcelCaption.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picExcelCaption.Name = "picExcelCaption";
            this.picExcelCaption.Size = new System.Drawing.Size(51, 47);
            this.picExcelCaption.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picExcelCaption.TabIndex = 145;
            this.picExcelCaption.TabStop = false;
            // 
            // lblExcelCaption
            // 
            this.lblExcelCaption.AutoSize = true;
            this.lblExcelCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExcelCaption.ForeColor = System.Drawing.Color.Black;
            this.lblExcelCaption.Location = new System.Drawing.Point(75, 17);
            this.lblExcelCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblExcelCaption.Name = "lblExcelCaption";
            this.lblExcelCaption.Size = new System.Drawing.Size(155, 28);
            this.lblExcelCaption.TabIndex = 146;
            this.lblExcelCaption.Text = "Import Excel File";
            // 
            // panKeywordGroup
            // 
            this.panKeywordGroup.Controls.Add(this.lstKeywordGroups);
            this.panKeywordGroup.Controls.Add(this.lblbKwG);
            this.panKeywordGroup.Controls.Add(this.txtbKwG);
            this.panKeywordGroup.Controls.Add(this.label5);
            this.panKeywordGroup.Controls.Add(this.txtbDicNameKeywords);
            this.panKeywordGroup.Controls.Add(this.label6);
            this.panKeywordGroup.Controls.Add(this.pictureBox2);
            this.panKeywordGroup.Controls.Add(this.label7);
            this.panKeywordGroup.Location = new System.Drawing.Point(238, 66);
            this.panKeywordGroup.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panKeywordGroup.Name = "panKeywordGroup";
            this.panKeywordGroup.Size = new System.Drawing.Size(801, 302);
            this.panKeywordGroup.TabIndex = 155;
            this.panKeywordGroup.Visible = false;
            // 
            // lstKeywordGroups
            // 
            this.lstKeywordGroups.BackColor = System.Drawing.Color.White;
            this.lstKeywordGroups.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstKeywordGroups.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstKeywordGroups.ForeColor = System.Drawing.Color.Black;
            this.lstKeywordGroups.FormattingEnabled = true;
            this.lstKeywordGroups.ItemHeight = 20;
            this.lstKeywordGroups.Location = new System.Drawing.Point(16, 103);
            this.lstKeywordGroups.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstKeywordGroups.Name = "lstKeywordGroups";
            this.lstKeywordGroups.Size = new System.Drawing.Size(415, 160);
            this.lstKeywordGroups.TabIndex = 154;
            this.lstKeywordGroups.SelectedIndexChanged += new System.EventHandler(this.lstKeywordGroups_SelectedIndexChanged);
            // 
            // lblbKwG
            // 
            this.lblbKwG.AutoSize = true;
            this.lblbKwG.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblbKwG.ForeColor = System.Drawing.Color.White;
            this.lblbKwG.Location = new System.Drawing.Point(699, 12);
            this.lblbKwG.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblbKwG.Name = "lblbKwG";
            this.lblbKwG.Size = new System.Drawing.Size(42, 23);
            this.lblbKwG.TabIndex = 153;
            this.lblbKwG.Text = "jkjkjl";
            this.lblbKwG.Visible = false;
            // 
            // txtbKwG
            // 
            this.txtbKwG.BackColor = System.Drawing.Color.White;
            this.txtbKwG.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbKwG.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbKwG.ForeColor = System.Drawing.Color.Black;
            this.txtbKwG.Location = new System.Drawing.Point(472, 153);
            this.txtbKwG.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbKwG.Multiline = true;
            this.txtbKwG.Name = "txtbKwG";
            this.txtbKwG.Size = new System.Drawing.Size(307, 129);
            this.txtbKwG.TabIndex = 152;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(468, 68);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 23);
            this.label5.TabIndex = 151;
            this.label5.Text = "Dictionary Name";
            // 
            // txtbDicNameKeywords
            // 
            this.txtbDicNameKeywords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDicNameKeywords.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDicNameKeywords.Location = new System.Drawing.Point(472, 92);
            this.txtbDicNameKeywords.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbDicNameKeywords.MaxLength = 50;
            this.txtbDicNameKeywords.Name = "txtbDicNameKeywords";
            this.txtbDicNameKeywords.Size = new System.Drawing.Size(310, 29);
            this.txtbDicNameKeywords.TabIndex = 150;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(13, 68);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(135, 23);
            this.label6.TabIndex = 149;
            this.label6.Text = "Keyword Groups";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox2.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_key;
            this.pictureBox2.Location = new System.Drawing.Point(16, 10);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(51, 47);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 145;
            this.pictureBox2.TabStop = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(75, 17);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(215, 28);
            this.label7.TabIndex = 146;
            this.label7.Text = "Import Keyword Group";
            // 
            // cboSheetName
            // 
            this.cboSheetName.DropDownHeight = 206;
            this.cboSheetName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboSheetName.FormattingEnabled = true;
            this.cboSheetName.IntegralHeight = false;
            this.cboSheetName.ItemHeight = 16;
            this.cboSheetName.Location = new System.Drawing.Point(16, 156);
            this.cboSheetName.Name = "cboSheetName";
            this.cboSheetName.Size = new System.Drawing.Size(121, 24);
            this.cboSheetName.TabIndex = 155;
            // 
            // frmImportDic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1015, 459);
            this.ControlBox = false;
            this.Controls.Add(this.panKeywordGroup);
            this.Controls.Add(this.panDictionary);
            this.Controls.Add(this.panExcel);
            this.Controls.Add(this.panLeft);
            this.Controls.Add(this.lblStatus);
            this.Controls.Add(this.panFooter);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmImportDic";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 12);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Import";
            this.Load += new System.EventHandler(this.frmImportDic_Load);
            this.panLeft.ResumeLayout(false);
            this.panDictionary.ResumeLayout(false);
            this.panDictionary.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panFooter.ResumeLayout(false);
            this.panExcel.ResumeLayout(false);
            this.panExcel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picExcelCaption)).EndInit();
            this.panKeywordGroup.ResumeLayout(false);
            this.panKeywordGroup.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDicFileDicName;
        private System.Windows.Forms.Panel panLeft;
        private MetroFramework.Controls.MetroButton butExcel;
        private MetroFramework.Controls.MetroButton butDictionary;
        private MetroFramework.Controls.MetroButton butWorkgroup;
        private MetroFramework.Controls.MetroButton butKeywordGroup;
        private System.Windows.Forms.Label lblKeywordGroup;
        private System.Windows.Forms.Label lblWorkgroup;
        private System.Windows.Forms.Label lblDictionary;
        private System.Windows.Forms.Label lblExcel;
        private System.Windows.Forms.TextBox txtDicFileDicName;
        private System.Windows.Forms.Label lblDicFileXML;
        private MetroFramework.Controls.MetroButton butSelectDicFile;
        private System.Windows.Forms.TextBox txtDictionaryFile;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblDictionaryFile;
        private System.Windows.Forms.TextBox txtbDicFileMessage;
        private System.Windows.Forms.Label lblStatus;
        private System.Windows.Forms.Label lblDicFileMessage;
        private System.Windows.Forms.Panel panDictionary;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOK;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panExcel;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblExcelColumns;
        private System.Windows.Forms.TextBox txtbExcelColumns;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbExcelDictionaryName;
        private System.Windows.Forms.Label label1;
        private MetroFramework.Controls.MetroButton butSelectExcelFile;
        private System.Windows.Forms.TextBox txtExcelFile;
        private System.Windows.Forms.PictureBox picExcelCaption;
        private System.Windows.Forms.Label lblExcelCaption;
        private System.Windows.Forms.Panel panKeywordGroup;
        private System.Windows.Forms.ListBox lstKeywordGroups;
        private System.Windows.Forms.Label lblbKwG;
        private System.Windows.Forms.TextBox txtbKwG;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtbDicNameKeywords;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cboSheetName;
    }
}