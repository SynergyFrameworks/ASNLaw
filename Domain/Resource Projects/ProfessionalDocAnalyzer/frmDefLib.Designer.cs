namespace ProfessionalDocAnalyzer
{
    partial class frmDefLib
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle13 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle14 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle15 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panFooter = new System.Windows.Forms.Panel();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.lblHeader = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.dvgAcronyms = new System.Windows.Forms.DataGridView();
            this.panFind = new System.Windows.Forms.Panel();
            this.butFind = new MetroFramework.Controls.MetroButton();
            this.txtbFind = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbFileName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panBatchLoad = new System.Windows.Forms.Panel();
            this.butImportFile = new MetroFramework.Controls.MetroButton();
            this.butAddBatchAcronyms = new MetroFramework.Controls.MetroButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatchAcronyms = new System.Windows.Forms.TextBox();
            this.panMaintain = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDefinition = new System.Windows.Forms.TextBox();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.butReplace = new MetroFramework.Controls.MetroButton();
            this.butNew = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAcron = new System.Windows.Forms.TextBox();
            this.panRightHeader = new System.Windows.Forms.Panel();
            this.butMaintain = new System.Windows.Forms.Button();
            this.butBatchLoad = new System.Windows.Forms.Button();
            this.lblMessage = new System.Windows.Forms.Label();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panFooter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgAcronyms)).BeginInit();
            this.panFind.SuspendLayout();
            this.panBatchLoad.SuspendLayout();
            this.panMaintain.SuspendLayout();
            this.panRightHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panFooter
            // 
            this.panFooter.Controls.Add(this.butCancel);
            this.panFooter.Controls.Add(this.butOK);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(10, 422);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(1063, 33);
            this.panFooter.TabIndex = 0;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(955, 6);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 83;
            this.butCancel.Text = "Cancel";
            this.metroToolTip1.SetToolTip(this.butCancel, "Cancel Changes");
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(865, 6);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 84;
            this.butOK.Text = "Save";
            this.metroToolTip1.SetToolTip(this.butOK, "Save Acronym Dictionary");
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(43, 14);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(205, 30);
            this.lblHeader.TabIndex = 110;
            this.lblHeader.Text = "Acronyms Dictionary";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(10, 60);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dvgAcronyms);
            this.splitContainer1.Panel1.Controls.Add(this.panFind);
            this.splitContainer1.Panel1.Controls.Add(this.label2);
            this.splitContainer1.Panel1.Controls.Add(this.txtbFileName);
            this.splitContainer1.Panel1.Controls.Add(this.label4);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panBatchLoad);
            this.splitContainer1.Panel2.Controls.Add(this.panMaintain);
            this.splitContainer1.Panel2.Controls.Add(this.panRightHeader);
            this.splitContainer1.Size = new System.Drawing.Size(1063, 362);
            this.splitContainer1.SplitterDistance = 498;
            this.splitContainer1.TabIndex = 112;
            // 
            // dvgAcronyms
            // 
            this.dvgAcronyms.AllowUserToAddRows = false;
            this.dvgAcronyms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgAcronyms.BackgroundColor = System.Drawing.Color.White;
            this.dvgAcronyms.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle13.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle13.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle13.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle13.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle13.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle13.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgAcronyms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle13;
            this.dvgAcronyms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgAcronyms.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle14.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle14.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle14.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle14.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle14.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle14.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle14.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvgAcronyms.DefaultCellStyle = dataGridViewCellStyle14;
            this.dvgAcronyms.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dvgAcronyms.Location = new System.Drawing.Point(13, 99);
            this.dvgAcronyms.MultiSelect = false;
            this.dvgAcronyms.Name = "dvgAcronyms";
            this.dvgAcronyms.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle15.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle15.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle15.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle15.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle15.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle15.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgAcronyms.RowHeadersDefaultCellStyle = dataGridViewCellStyle15;
            this.dvgAcronyms.RowHeadersWidth = 5;
            this.dvgAcronyms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgAcronyms.ShowEditingIcon = false;
            this.dvgAcronyms.Size = new System.Drawing.Size(463, 242);
            this.dvgAcronyms.TabIndex = 116;
            this.dvgAcronyms.SelectionChanged += new System.EventHandler(this.dvgAcronyms_SelectionChanged);
            // 
            // panFind
            // 
            this.panFind.Controls.Add(this.butFind);
            this.panFind.Controls.Add(this.txtbFind);
            this.panFind.Controls.Add(this.label6);
            this.panFind.Location = new System.Drawing.Point(13, 43);
            this.panFind.Name = "panFind";
            this.panFind.Size = new System.Drawing.Size(409, 50);
            this.panFind.TabIndex = 115;
            // 
            // butFind
            // 
            this.butFind.Location = new System.Drawing.Point(301, 20);
            this.butFind.Name = "butFind";
            this.butFind.Size = new System.Drawing.Size(75, 23);
            this.butFind.TabIndex = 83;
            this.butFind.Text = "Find";
            this.butFind.UseSelectable = true;
            this.butFind.Click += new System.EventHandler(this.butFind_Click);
            // 
            // txtbFind
            // 
            this.txtbFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbFind.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbFind.Location = new System.Drawing.Point(95, 17);
            this.txtbFind.Name = "txtbFind";
            this.txtbFind.Size = new System.Drawing.Size(196, 27);
            this.txtbFind.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(3, 20);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(87, 17);
            this.label6.TabIndex = 16;
            this.label6.Text = "Find Acronym";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(15, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(184, 20);
            this.label2.TabIndex = 113;
            this.label2.Text = "Acronym Dictionary Name";
            // 
            // txtbFileName
            // 
            this.txtbFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbFileName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbFileName.Location = new System.Drawing.Point(205, 12);
            this.txtbFileName.MaxLength = 50;
            this.txtbFileName.Name = "txtbFileName";
            this.txtbFileName.Size = new System.Drawing.Size(271, 25);
            this.txtbFileName.TabIndex = 111;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(-9, -35);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(190, 20);
            this.label4.TabIndex = 112;
            this.label4.Text = "Acronyms Dictionary Name";
            // 
            // panBatchLoad
            // 
            this.panBatchLoad.BackColor = System.Drawing.Color.Black;
            this.panBatchLoad.Controls.Add(this.butImportFile);
            this.panBatchLoad.Controls.Add(this.butAddBatchAcronyms);
            this.panBatchLoad.Controls.Add(this.label5);
            this.panBatchLoad.Controls.Add(this.txtBatchAcronyms);
            this.panBatchLoad.Location = new System.Drawing.Point(54, 106);
            this.panBatchLoad.Name = "panBatchLoad";
            this.panBatchLoad.Size = new System.Drawing.Size(544, 241);
            this.panBatchLoad.TabIndex = 1;
            // 
            // butImportFile
            // 
            this.butImportFile.Location = new System.Drawing.Point(58, 211);
            this.butImportFile.Name = "butImportFile";
            this.butImportFile.Size = new System.Drawing.Size(75, 23);
            this.butImportFile.TabIndex = 81;
            this.butImportFile.Text = "Import File";
            this.butImportFile.UseSelectable = true;
            this.butImportFile.Visible = false;
            this.butImportFile.Click += new System.EventHandler(this.butImportFile_Click);
            // 
            // butAddBatchAcronyms
            // 
            this.butAddBatchAcronyms.Location = new System.Drawing.Point(458, 211);
            this.butAddBatchAcronyms.Name = "butAddBatchAcronyms";
            this.butAddBatchAcronyms.Size = new System.Drawing.Size(75, 23);
            this.butAddBatchAcronyms.TabIndex = 80;
            this.butAddBatchAcronyms.Text = "Add";
            this.butAddBatchAcronyms.UseSelectable = true;
            this.butAddBatchAcronyms.Click += new System.EventHandler(this.butAddBatchAcronyms_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(11, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(124, 17);
            this.label5.TabIndex = 79;
            this.label5.Text = "Delimited Acronyms";
            // 
            // txtBatchAcronyms
            // 
            this.txtBatchAcronyms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBatchAcronyms.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatchAcronyms.Location = new System.Drawing.Point(14, 34);
            this.txtBatchAcronyms.Multiline = true;
            this.txtBatchAcronyms.Name = "txtBatchAcronyms";
            this.txtBatchAcronyms.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBatchAcronyms.Size = new System.Drawing.Size(519, 159);
            this.txtBatchAcronyms.TabIndex = 78;
            // 
            // panMaintain
            // 
            this.panMaintain.BackColor = System.Drawing.Color.Black;
            this.panMaintain.Controls.Add(this.label3);
            this.panMaintain.Controls.Add(this.txtDefinition);
            this.panMaintain.Controls.Add(this.butDelete);
            this.panMaintain.Controls.Add(this.butReplace);
            this.panMaintain.Controls.Add(this.butNew);
            this.panMaintain.Controls.Add(this.label1);
            this.panMaintain.Controls.Add(this.txtAcron);
            this.panMaintain.Location = new System.Drawing.Point(40, 141);
            this.panMaintain.Name = "panMaintain";
            this.panMaintain.Size = new System.Drawing.Size(293, 180);
            this.panMaintain.TabIndex = 2;
            this.panMaintain.Visible = false;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(104, 76);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 83;
            this.label3.Text = "Definition";
            // 
            // txtDefinition
            // 
            this.txtDefinition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDefinition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefinition.Location = new System.Drawing.Point(107, 98);
            this.txtDefinition.Name = "txtDefinition";
            this.txtDefinition.Size = new System.Drawing.Size(390, 25);
            this.txtDefinition.TabIndex = 82;
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(14, 100);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(75, 23);
            this.butDelete.TabIndex = 81;
            this.butDelete.Text = "Delete";
            this.butDelete.UseSelectable = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // butReplace
            // 
            this.butReplace.Location = new System.Drawing.Point(14, 68);
            this.butReplace.Name = "butReplace";
            this.butReplace.Size = new System.Drawing.Size(75, 23);
            this.butReplace.TabIndex = 80;
            this.butReplace.Text = "Replace";
            this.butReplace.UseSelectable = true;
            this.butReplace.Click += new System.EventHandler(this.butReplace_Click);
            // 
            // butNew
            // 
            this.butNew.Location = new System.Drawing.Point(14, 36);
            this.butNew.Name = "butNew";
            this.butNew.Size = new System.Drawing.Size(75, 23);
            this.butNew.TabIndex = 79;
            this.butNew.Text = "Add";
            this.butNew.UseSelectable = true;
            this.butNew.Click += new System.EventHandler(this.butNew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(104, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 17);
            this.label1.TabIndex = 78;
            this.label1.Text = "Acronym";
            // 
            // txtAcron
            // 
            this.txtAcron.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAcron.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcron.Location = new System.Drawing.Point(107, 35);
            this.txtAcron.Name = "txtAcron";
            this.txtAcron.Size = new System.Drawing.Size(148, 25);
            this.txtAcron.TabIndex = 77;
            // 
            // panRightHeader
            // 
            this.panRightHeader.Controls.Add(this.butMaintain);
            this.panRightHeader.Controls.Add(this.butBatchLoad);
            this.panRightHeader.Controls.Add(this.lblMessage);
            this.panRightHeader.Controls.Add(this.txtbMessage);
            this.panRightHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panRightHeader.Location = new System.Drawing.Point(0, 0);
            this.panRightHeader.Name = "panRightHeader";
            this.panRightHeader.Size = new System.Drawing.Size(561, 100);
            this.panRightHeader.TabIndex = 0;
            // 
            // butMaintain
            // 
            this.butMaintain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.butMaintain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butMaintain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butMaintain.Location = new System.Drawing.Point(95, 71);
            this.butMaintain.Name = "butMaintain";
            this.butMaintain.Size = new System.Drawing.Size(75, 23);
            this.butMaintain.TabIndex = 79;
            this.butMaintain.Text = "Maintain";
            this.butMaintain.UseVisualStyleBackColor = false;
            this.butMaintain.Click += new System.EventHandler(this.butMaintain_Click);
            this.butMaintain.MouseEnter += new System.EventHandler(this.butMaintain_MouseEnter);
            this.butMaintain.MouseLeave += new System.EventHandler(this.butMaintain_MouseLeave);
            // 
            // butBatchLoad
            // 
            this.butBatchLoad.BackColor = System.Drawing.Color.LightGreen;
            this.butBatchLoad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butBatchLoad.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butBatchLoad.Location = new System.Drawing.Point(14, 71);
            this.butBatchLoad.Name = "butBatchLoad";
            this.butBatchLoad.Size = new System.Drawing.Size(75, 23);
            this.butBatchLoad.TabIndex = 78;
            this.butBatchLoad.Text = "Batch Load";
            this.butBatchLoad.UseVisualStyleBackColor = false;
            this.butBatchLoad.Click += new System.EventHandler(this.butBatchLoad_Click);
            this.butBatchLoad.MouseEnter += new System.EventHandler(this.butBatchLoad_MouseEnter);
            this.butBatchLoad.MouseLeave += new System.EventHandler(this.butBatchLoad_MouseLeave);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(275, 44);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 77;
            this.lblMessage.Visible = false;
            this.lblMessage.TextChanged += new System.EventHandler(this.lblMessage_TextChanged);
            // 
            // txtbMessage
            // 
            this.txtbMessage.BackColor = System.Drawing.Color.White;
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.Blue;
            this.txtbMessage.Location = new System.Drawing.Point(14, 6);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(522, 59);
            this.txtbMessage.TabIndex = 76;
            this.txtbMessage.TextChanged += new System.EventHandler(this.txtbMessage_TextChanged);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_book_list;
            this.pictureBox2.Location = new System.Drawing.Point(9, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 111;
            this.pictureBox2.TabStop = false;
            // 
            // frmDefLib
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1083, 460);
            this.ControlBox = false;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.panFooter);
            this.Name = "frmDefLib";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 5);
            this.Resizable = false;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Activated += new System.EventHandler(this.frmDefLib_Activated);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmDefLib_Paint);
            this.panFooter.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgAcronyms)).EndInit();
            this.panFind.ResumeLayout(false);
            this.panFind.PerformLayout();
            this.panBatchLoad.ResumeLayout(false);
            this.panBatchLoad.PerformLayout();
            this.panMaintain.ResumeLayout(false);
            this.panMaintain.PerformLayout();
            this.panRightHeader.ResumeLayout(false);
            this.panRightHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panFind;
        private MetroFramework.Controls.MetroButton butFind;
        private System.Windows.Forms.TextBox txtbFind;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbFileName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panRightHeader;
        private System.Windows.Forms.TextBox txtbMessage;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private MetroFramework.Controls.MetroButton butOK;
        private System.Windows.Forms.Panel panMaintain;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDefinition;
        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Controls.MetroButton butReplace;
        private MetroFramework.Controls.MetroButton butNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAcron;
        private System.Windows.Forms.Panel panBatchLoad;
        private MetroFramework.Controls.MetroButton butImportFile;
        private MetroFramework.Controls.MetroButton butAddBatchAcronyms;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBatchAcronyms;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button butMaintain;
        private System.Windows.Forms.Button butBatchLoad;
        private System.Windows.Forms.DataGridView dvgAcronyms;
    }
}