namespace ProfessionalDocAnalyzer
{
    partial class frmDictionary2
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDictionary2));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.butSearch = new MetroFramework.Controls.MetroButton();
            this.cmbboxClr = new System.Windows.Forms.ComboBox();
            this.txtbFind = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.cboDicWeight = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butCategories = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.butNew = new MetroFramework.Controls.MetroButton();
            this.butReplace = new MetroFramework.Controls.MetroButton();
            this.butRemoveSynonym = new MetroFramework.Controls.MetroButton();
            this.butAddSynonym = new MetroFramework.Controls.MetroButton();
            this.panDictionary = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.txtbDescription = new System.Windows.Forms.TextBox();
            this.txtbDictionaryName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblDictionaryTerms = new System.Windows.Forms.Label();
            this.panMaintainDictionaryTerms = new System.Windows.Forms.Panel();
            this.cboCategory = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDefinition = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtDicItem = new System.Windows.Forms.TextBox();
            this.dvgDictionaries = new System.Windows.Forms.DataGridView();
            this.picDictionaries = new System.Windows.Forms.PictureBox();
            this.panSynonyms = new System.Windows.Forms.Panel();
            this.lblSynonyms = new System.Windows.Forms.Label();
            this.butSynonyms = new MetroFramework.Controls.MetroButton();
            this.lstbSynonyms = new System.Windows.Forms.ListBox();
            this.panDictionary.SuspendLayout();
            this.panMaintainDictionaryTerms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgDictionaries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDictionaries)).BeginInit();
            this.panSynonyms.SuspendLayout();
            this.SuspendLayout();
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(404, 143);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(75, 23);
            this.butSearch.TabIndex = 141;
            this.butSearch.Text = "Search";
            this.butSearch.UseSelectable = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // cmbboxClr
            // 
            this.cmbboxClr.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbboxClr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbboxClr.DropDownWidth = 240;
            this.cmbboxClr.FormattingEnabled = true;
            this.cmbboxClr.ItemHeight = 20;
            this.cmbboxClr.Location = new System.Drawing.Point(18, 188);
            this.cmbboxClr.Name = "cmbboxClr";
            this.cmbboxClr.Size = new System.Drawing.Size(229, 26);
            this.cmbboxClr.TabIndex = 7;
            this.cmbboxClr.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbboxClr_DrawItem_1);
            // 
            // txtbFind
            // 
            this.txtbFind.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbFind.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbFind.Location = new System.Drawing.Point(221, 144);
            this.txtbFind.Name = "txtbFind";
            this.txtbFind.Size = new System.Drawing.Size(177, 22);
            this.txtbFind.TabIndex = 140;
            this.txtbFind.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtbFind_KeyDown);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(169, 121);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(48, 17);
            this.label10.TabIndex = 116;
            this.label10.Text = "Weight";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(183, 144);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 17);
            this.label8.TabIndex = 139;
            this.label8.Text = "Item";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(18, 170);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 15);
            this.label12.TabIndex = 119;
            this.label12.Text = "Color";
            // 
            // cboDicWeight
            // 
            this.cboDicWeight.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboDicWeight.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboDicWeight.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDicWeight.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDicWeight.FormattingEnabled = true;
            this.cboDicWeight.Location = new System.Drawing.Point(171, 143);
            this.cboDicWeight.Name = "cboDicWeight";
            this.cboDicWeight.Size = new System.Drawing.Size(74, 25);
            this.cboDicWeight.TabIndex = 6;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(12, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(182, 20);
            this.label9.TabIndex = 134;
            this.label9.Text = "Maintain Dictionary Terms";
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(874, 649);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 129;
            this.butCancel.Text = "Cancel";
            this.metroToolTip1.SetToolTip(this.butCancel, "Cancel Changes");
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butCategories
            // 
            this.butCategories.Location = new System.Drawing.Point(16, 66);
            this.butCategories.Name = "butCategories";
            this.butCategories.Size = new System.Drawing.Size(84, 20);
            this.butCategories.TabIndex = 4;
            this.butCategories.Text = "Categories...";
            this.metroToolTip1.SetToolTip(this.butCategories, "Open the Maintian Categories window");
            this.butCategories.UseSelectable = true;
            this.butCategories.Click += new System.EventHandler(this.butCategories_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(784, 649);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 128;
            this.butOK.Text = "Save";
            this.metroToolTip1.SetToolTip(this.butOK, "Save Changes");
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butDelete
            // 
            this.butDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butDelete.BackgroundImage")));
            this.butDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butDelete.Location = new System.Drawing.Point(100, 32);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(28, 28);
            this.butDelete.TabIndex = 161;
            this.butDelete.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butDelete, "Delete Selected Dictionary Item");
            this.butDelete.UseSelectable = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // butNew
            // 
            this.butNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butNew.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butNew.BackgroundImage")));
            this.butNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butNew.Location = new System.Drawing.Point(16, 32);
            this.butNew.Name = "butNew";
            this.butNew.Size = new System.Drawing.Size(28, 28);
            this.butNew.TabIndex = 160;
            this.butNew.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butNew, "Add New Dictionary Item");
            this.butNew.UseSelectable = true;
            this.butNew.Click += new System.EventHandler(this.butNew_Click);
            // 
            // butReplace
            // 
            this.butReplace.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butReplace.BackgroundImage")));
            this.butReplace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butReplace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butReplace.ForeColor = System.Drawing.Color.White;
            this.butReplace.Location = new System.Drawing.Point(58, 32);
            this.butReplace.Name = "butReplace";
            this.butReplace.Size = new System.Drawing.Size(28, 28);
            this.butReplace.TabIndex = 159;
            this.butReplace.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butReplace.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butReplace, "Update Selected Dictionary Item");
            this.butReplace.UseSelectable = true;
            this.butReplace.Click += new System.EventHandler(this.butReplace_Click);
            // 
            // butRemoveSynonym
            // 
            this.butRemoveSynonym.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butRemoveSynonym.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butRemoveSynonym.BackgroundImage")));
            this.butRemoveSynonym.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butRemoveSynonym.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butRemoveSynonym.Location = new System.Drawing.Point(125, 9);
            this.butRemoveSynonym.Name = "butRemoveSynonym";
            this.butRemoveSynonym.Size = new System.Drawing.Size(28, 28);
            this.butRemoveSynonym.TabIndex = 124;
            this.butRemoveSynonym.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butRemoveSynonym, "Delete Selected Synonym");
            this.butRemoveSynonym.UseSelectable = true;
            this.butRemoveSynonym.Click += new System.EventHandler(this.butRemoveSynonym_Click);
            // 
            // butAddSynonym
            // 
            this.butAddSynonym.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butAddSynonym.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butAddSynonym.BackgroundImage")));
            this.butAddSynonym.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butAddSynonym.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butAddSynonym.Location = new System.Drawing.Point(86, 9);
            this.butAddSynonym.Name = "butAddSynonym";
            this.butAddSynonym.Size = new System.Drawing.Size(28, 28);
            this.butAddSynonym.TabIndex = 123;
            this.butAddSynonym.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butAddSynonym, "Add New Synonym");
            this.butAddSynonym.UseSelectable = true;
            this.butAddSynonym.Click += new System.EventHandler(this.butAddSynonym_Click);
            // 
            // panDictionary
            // 
            this.panDictionary.Controls.Add(this.label5);
            this.panDictionary.Controls.Add(this.txtbDescription);
            this.panDictionary.Controls.Add(this.txtbDictionaryName);
            this.panDictionary.Controls.Add(this.label4);
            this.panDictionary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panDictionary.Location = new System.Drawing.Point(20, 60);
            this.panDictionary.Name = "panDictionary";
            this.panDictionary.Size = new System.Drawing.Size(930, 80);
            this.panDictionary.TabIndex = 131;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.Transparent;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(474, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 17);
            this.label5.TabIndex = 114;
            this.label5.Text = "Description";
            // 
            // txtbDescription
            // 
            this.txtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDescription.Location = new System.Drawing.Point(477, 30);
            this.txtbDescription.Multiline = true;
            this.txtbDescription.Name = "txtbDescription";
            this.txtbDescription.Size = new System.Drawing.Size(439, 44);
            this.txtbDescription.TabIndex = 2;
            // 
            // txtbDictionaryName
            // 
            this.txtbDictionaryName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDictionaryName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDictionaryName.Location = new System.Drawing.Point(12, 30);
            this.txtbDictionaryName.MaxLength = 50;
            this.txtbDictionaryName.Name = "txtbDictionaryName";
            this.txtbDictionaryName.Size = new System.Drawing.Size(445, 25);
            this.txtbDictionaryName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 17);
            this.label4.TabIndex = 75;
            this.label4.Text = "Dictionary Name";
            // 
            // lblDictionaryTerms
            // 
            this.lblDictionaryTerms.AutoSize = true;
            this.lblDictionaryTerms.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDictionaryTerms.ForeColor = System.Drawing.Color.Black;
            this.lblDictionaryTerms.Location = new System.Drawing.Point(15, 146);
            this.lblDictionaryTerms.Name = "lblDictionaryTerms";
            this.lblDictionaryTerms.Size = new System.Drawing.Size(120, 20);
            this.lblDictionaryTerms.TabIndex = 135;
            this.lblDictionaryTerms.Text = "Dictionary Terms";
            // 
            // panMaintainDictionaryTerms
            // 
            this.panMaintainDictionaryTerms.BackColor = System.Drawing.Color.Black;
            this.panMaintainDictionaryTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panMaintainDictionaryTerms.Controls.Add(this.butDelete);
            this.panMaintainDictionaryTerms.Controls.Add(this.butNew);
            this.panMaintainDictionaryTerms.Controls.Add(this.butReplace);
            this.panMaintainDictionaryTerms.Controls.Add(this.label12);
            this.panMaintainDictionaryTerms.Controls.Add(this.cmbboxClr);
            this.panMaintainDictionaryTerms.Controls.Add(this.label10);
            this.panMaintainDictionaryTerms.Controls.Add(this.cboDicWeight);
            this.panMaintainDictionaryTerms.Controls.Add(this.butCategories);
            this.panMaintainDictionaryTerms.Controls.Add(this.label9);
            this.panMaintainDictionaryTerms.Controls.Add(this.cboCategory);
            this.panMaintainDictionaryTerms.Controls.Add(this.label3);
            this.panMaintainDictionaryTerms.Controls.Add(this.txtDefinition);
            this.panMaintainDictionaryTerms.Controls.Add(this.label1);
            this.panMaintainDictionaryTerms.Controls.Add(this.txtDicItem);
            this.panMaintainDictionaryTerms.Location = new System.Drawing.Point(337, 415);
            this.panMaintainDictionaryTerms.Name = "panMaintainDictionaryTerms";
            this.panMaintainDictionaryTerms.Size = new System.Drawing.Size(612, 228);
            this.panMaintainDictionaryTerms.TabIndex = 133;
            // 
            // cboCategory
            // 
            this.cboCategory.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest;
            this.cboCategory.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.cboCategory.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCategory.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCategory.FormattingEnabled = true;
            this.cboCategory.Location = new System.Drawing.Point(16, 91);
            this.cboCategory.Name = "cboCategory";
            this.cboCategory.Size = new System.Drawing.Size(584, 25);
            this.cboCategory.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(264, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(63, 17);
            this.label3.TabIndex = 108;
            this.label3.Text = "Definition";
            // 
            // txtDefinition
            // 
            this.txtDefinition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDefinition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefinition.Location = new System.Drawing.Point(267, 144);
            this.txtDefinition.Multiline = true;
            this.txtDefinition.Name = "txtDefinition";
            this.txtDefinition.Size = new System.Drawing.Size(333, 68);
            this.txtDefinition.TabIndex = 8;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(15, 123);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(37, 17);
            this.label1.TabIndex = 103;
            this.label1.Text = "Term";
            // 
            // txtDicItem
            // 
            this.txtDicItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDicItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDicItem.Location = new System.Drawing.Point(16, 143);
            this.txtDicItem.Name = "txtDicItem";
            this.txtDicItem.Size = new System.Drawing.Size(148, 25);
            this.txtDicItem.TabIndex = 5;
            // 
            // dvgDictionaries
            // 
            this.dvgDictionaries.AllowUserToAddRows = false;
            this.dvgDictionaries.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgDictionaries.BackgroundColor = System.Drawing.Color.White;
            this.dvgDictionaries.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgDictionaries.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvgDictionaries.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgDictionaries.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvgDictionaries.DefaultCellStyle = dataGridViewCellStyle2;
            this.dvgDictionaries.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dvgDictionaries.Location = new System.Drawing.Point(18, 170);
            this.dvgDictionaries.MultiSelect = false;
            this.dvgDictionaries.Name = "dvgDictionaries";
            this.dvgDictionaries.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgDictionaries.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dvgDictionaries.RowHeadersWidth = 5;
            this.dvgDictionaries.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgDictionaries.ShowEditingIcon = false;
            this.dvgDictionaries.Size = new System.Drawing.Size(931, 231);
            this.dvgDictionaries.TabIndex = 132;
            this.dvgDictionaries.SelectionChanged += new System.EventHandler(this.dvgDictionaries_SelectionChanged);
            // 
            // picDictionaries
            // 
            this.picDictionaries.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDictionaries.Image = ((System.Drawing.Image)(resources.GetObject("picDictionaries.Image")));
            this.picDictionaries.Location = new System.Drawing.Point(18, 16);
            this.picDictionaries.Name = "picDictionaries";
            this.picDictionaries.Size = new System.Drawing.Size(41, 38);
            this.picDictionaries.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDictionaries.TabIndex = 130;
            this.picDictionaries.TabStop = false;
            // 
            // panSynonyms
            // 
            this.panSynonyms.BackColor = System.Drawing.Color.Black;
            this.panSynonyms.Controls.Add(this.butRemoveSynonym);
            this.panSynonyms.Controls.Add(this.butAddSynonym);
            this.panSynonyms.Controls.Add(this.lblSynonyms);
            this.panSynonyms.Controls.Add(this.butSynonyms);
            this.panSynonyms.Controls.Add(this.lstbSynonyms);
            this.panSynonyms.Location = new System.Drawing.Point(18, 415);
            this.panSynonyms.Name = "panSynonyms";
            this.panSynonyms.Size = new System.Drawing.Size(313, 228);
            this.panSynonyms.TabIndex = 142;
            // 
            // lblSynonyms
            // 
            this.lblSynonyms.AutoSize = true;
            this.lblSynonyms.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSynonyms.ForeColor = System.Drawing.Color.White;
            this.lblSynonyms.Location = new System.Drawing.Point(9, 15);
            this.lblSynonyms.Name = "lblSynonyms";
            this.lblSynonyms.Size = new System.Drawing.Size(75, 20);
            this.lblSynonyms.TabIndex = 127;
            this.lblSynonyms.Text = "Synonyms";
            // 
            // butSynonyms
            // 
            this.butSynonyms.Location = new System.Drawing.Point(196, 15);
            this.butSynonyms.Name = "butSynonyms";
            this.butSynonyms.Size = new System.Drawing.Size(108, 23);
            this.butSynonyms.TabIndex = 126;
            this.butSynonyms.Text = "Synonyms...";
            this.butSynonyms.UseSelectable = true;
            this.butSynonyms.Visible = false;
            this.butSynonyms.Click += new System.EventHandler(this.butSynonyms_Click);
            // 
            // lstbSynonyms
            // 
            this.lstbSynonyms.BackColor = System.Drawing.Color.Black;
            this.lstbSynonyms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbSynonyms.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbSynonyms.ForeColor = System.Drawing.Color.White;
            this.lstbSynonyms.FormattingEnabled = true;
            this.lstbSynonyms.ItemHeight = 17;
            this.lstbSynonyms.Location = new System.Drawing.Point(11, 49);
            this.lstbSynonyms.Name = "lstbSynonyms";
            this.lstbSynonyms.Size = new System.Drawing.Size(293, 170);
            this.lstbSynonyms.TabIndex = 125;
            // 
            // frmDictionary2
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(970, 682);
            this.ControlBox = false;
            this.Controls.Add(this.panSynonyms);
            this.Controls.Add(this.butSearch);
            this.Controls.Add(this.txtbFind);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panDictionary);
            this.Controls.Add(this.lblDictionaryTerms);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.panMaintainDictionaryTerms);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.picDictionaries);
            this.Controls.Add(this.dvgDictionaries);
            this.Name = "frmDictionary2";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "     Dictionary";
            this.panDictionary.ResumeLayout(false);
            this.panDictionary.PerformLayout();
            this.panMaintainDictionaryTerms.ResumeLayout(false);
            this.panMaintainDictionaryTerms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgDictionaries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picDictionaries)).EndInit();
            this.panSynonyms.ResumeLayout(false);
            this.panSynonyms.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton butSearch;
        private System.Windows.Forms.ComboBox cmbboxClr;
        private System.Windows.Forms.TextBox txtbFind;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label8;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cboDicWeight;
        private MetroFramework.Controls.MetroButton butReplace;
        private System.Windows.Forms.Label label9;
        private MetroFramework.Controls.MetroButton butNew;
        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butCategories;
        private MetroFramework.Controls.MetroButton butOK;
        private System.Windows.Forms.Panel panDictionary;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtbDescription;
        private System.Windows.Forms.TextBox txtbDictionaryName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblDictionaryTerms;
        private System.Windows.Forms.Panel panMaintainDictionaryTerms;
        private System.Windows.Forms.ComboBox cboCategory;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDefinition;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtDicItem;
        private System.Windows.Forms.PictureBox picDictionaries;
        private System.Windows.Forms.DataGridView dvgDictionaries;
        private System.Windows.Forms.Panel panSynonyms;
        private MetroFramework.Controls.MetroButton butRemoveSynonym;
        private MetroFramework.Controls.MetroButton butAddSynonym;
        private System.Windows.Forms.Label lblSynonyms;
        private MetroFramework.Controls.MetroButton butSynonyms;
        private System.Windows.Forms.ListBox lstbSynonyms;
    }
}