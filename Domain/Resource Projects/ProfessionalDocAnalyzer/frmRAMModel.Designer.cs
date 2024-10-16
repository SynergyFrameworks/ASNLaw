namespace ProfessionalDocAnalyzer
{
    partial class frmRAMModel
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRAMModel));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRoleDescription = new System.Windows.Forms.TextBox();
            this.lblNotation = new System.Windows.Forms.Label();
            this.txtRoleNotation = new System.Windows.Forms.TextBox();
            this.txtbRAMName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.panDictionary = new System.Windows.Forms.Panel();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.label8 = new System.Windows.Forms.Label();
            this.panMaintainDictionaryTerms = new System.Windows.Forms.Panel();
            this.lblRoleName = new System.Windows.Forms.Label();
            this.txtbRoleName = new System.Windows.Forms.TextBox();
            this.butNew = new MetroFramework.Controls.MetroButton();
            this.butReplace = new MetroFramework.Controls.MetroButton();
            this.label12 = new System.Windows.Forms.Label();
            this.cmbboxClr = new System.Windows.Forms.ComboBox();
            this.label9 = new System.Windows.Forms.Label();
            this.picDictionaries = new System.Windows.Forms.PictureBox();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.dvgModelItems = new System.Windows.Forms.DataGridView();
            this.panDictionary.SuspendLayout();
            this.panMaintainDictionaryTerms.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDictionaries)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgModelItems)).BeginInit();
            this.SuspendLayout();
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(258, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 108;
            this.label3.Text = "Description";
            // 
            // txtRoleDescription
            // 
            this.txtRoleDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoleDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoleDescription.Location = new System.Drawing.Point(261, 95);
            this.txtRoleDescription.Multiline = true;
            this.txtRoleDescription.Name = "txtRoleDescription";
            this.txtRoleDescription.Size = new System.Drawing.Size(333, 123);
            this.txtRoleDescription.TabIndex = 8;
            // 
            // lblNotation
            // 
            this.lblNotation.AutoSize = true;
            this.lblNotation.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotation.ForeColor = System.Drawing.Color.White;
            this.lblNotation.Location = new System.Drawing.Point(15, 123);
            this.lblNotation.Name = "lblNotation";
            this.lblNotation.Size = new System.Drawing.Size(59, 17);
            this.lblNotation.TabIndex = 103;
            this.lblNotation.Text = "Notation";
            // 
            // txtRoleNotation
            // 
            this.txtRoleNotation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtRoleNotation.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtRoleNotation.Location = new System.Drawing.Point(16, 143);
            this.txtRoleNotation.MaxLength = 30;
            this.txtRoleNotation.Name = "txtRoleNotation";
            this.txtRoleNotation.Size = new System.Drawing.Size(229, 25);
            this.txtRoleNotation.TabIndex = 5;
            // 
            // txtbRAMName
            // 
            this.txtbRAMName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbRAMName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbRAMName.Location = new System.Drawing.Point(12, 30);
            this.txtbRAMName.MaxLength = 25;
            this.txtbRAMName.Name = "txtbRAMName";
            this.txtbRAMName.Size = new System.Drawing.Size(268, 25);
            this.txtbRAMName.TabIndex = 1;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.Transparent;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(10, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(85, 17);
            this.label4.TabIndex = 75;
            this.label4.Text = "Model Name";
            // 
            // panDictionary
            // 
            this.panDictionary.Controls.Add(this.txtbRAMName);
            this.panDictionary.Controls.Add(this.label4);
            this.panDictionary.Dock = System.Windows.Forms.DockStyle.Top;
            this.panDictionary.Location = new System.Drawing.Point(20, 60);
            this.panDictionary.Name = "panDictionary";
            this.panDictionary.Size = new System.Drawing.Size(607, 63);
            this.panDictionary.TabIndex = 146;
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
            this.metroToolTip1.SetToolTip(this.butDelete, "Delete selected item");
            this.butDelete.UseSelectable = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(186, 103);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(33, 17);
            this.label8.TabIndex = 150;
            this.label8.Text = "Item";
            // 
            // panMaintainDictionaryTerms
            // 
            this.panMaintainDictionaryTerms.BackColor = System.Drawing.Color.Black;
            this.panMaintainDictionaryTerms.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panMaintainDictionaryTerms.Controls.Add(this.lblRoleName);
            this.panMaintainDictionaryTerms.Controls.Add(this.txtbRoleName);
            this.panMaintainDictionaryTerms.Controls.Add(this.butDelete);
            this.panMaintainDictionaryTerms.Controls.Add(this.butNew);
            this.panMaintainDictionaryTerms.Controls.Add(this.butReplace);
            this.panMaintainDictionaryTerms.Controls.Add(this.label12);
            this.panMaintainDictionaryTerms.Controls.Add(this.cmbboxClr);
            this.panMaintainDictionaryTerms.Controls.Add(this.label9);
            this.panMaintainDictionaryTerms.Controls.Add(this.label3);
            this.panMaintainDictionaryTerms.Controls.Add(this.txtRoleDescription);
            this.panMaintainDictionaryTerms.Controls.Add(this.lblNotation);
            this.panMaintainDictionaryTerms.Controls.Add(this.txtRoleNotation);
            this.panMaintainDictionaryTerms.Location = new System.Drawing.Point(20, 366);
            this.panMaintainDictionaryTerms.Name = "panMaintainDictionaryTerms";
            this.panMaintainDictionaryTerms.Size = new System.Drawing.Size(612, 228);
            this.panMaintainDictionaryTerms.TabIndex = 148;
            // 
            // lblRoleName
            // 
            this.lblRoleName.AutoSize = true;
            this.lblRoleName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRoleName.ForeColor = System.Drawing.Color.White;
            this.lblRoleName.Location = new System.Drawing.Point(15, 75);
            this.lblRoleName.Name = "lblRoleName";
            this.lblRoleName.Size = new System.Drawing.Size(43, 17);
            this.lblRoleName.TabIndex = 163;
            this.lblRoleName.Text = "Name";
            // 
            // txtbRoleName
            // 
            this.txtbRoleName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbRoleName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbRoleName.Location = new System.Drawing.Point(16, 95);
            this.txtbRoleName.MaxLength = 25;
            this.txtbRoleName.Name = "txtbRoleName";
            this.txtbRoleName.Size = new System.Drawing.Size(229, 25);
            this.txtbRoleName.TabIndex = 162;
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
            this.metroToolTip1.SetToolTip(this.butNew, "Add new RAM Model Role");
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
            this.metroToolTip1.SetToolTip(this.butReplace, "Update selected item");
            this.butReplace.UseSelectable = true;
            this.butReplace.Click += new System.EventHandler(this.butReplace_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.White;
            this.label12.Location = new System.Drawing.Point(16, 174);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(36, 15);
            this.label12.TabIndex = 119;
            this.label12.Text = "Color";
            // 
            // cmbboxClr
            // 
            this.cmbboxClr.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbboxClr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbboxClr.DropDownWidth = 240;
            this.cmbboxClr.FormattingEnabled = true;
            this.cmbboxClr.ItemHeight = 20;
            this.cmbboxClr.Location = new System.Drawing.Point(16, 192);
            this.cmbboxClr.Name = "cmbboxClr";
            this.cmbboxClr.Size = new System.Drawing.Size(229, 26);
            this.cmbboxClr.TabIndex = 7;
            this.cmbboxClr.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbboxClr_DrawItem);
            this.cmbboxClr.SelectedIndexChanged += new System.EventHandler(this.cmbboxClr_SelectedIndexChanged);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(12, 3);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(154, 20);
            this.label9.TabIndex = 134;
            this.label9.Text = "Maintain Model Roles";
            // 
            // picDictionaries
            // 
            this.picDictionaries.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDictionaries.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_people_checkbox;
            this.picDictionaries.Location = new System.Drawing.Point(20, 16);
            this.picDictionaries.Name = "picDictionaries";
            this.picDictionaries.Size = new System.Drawing.Size(41, 38);
            this.picDictionaries.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDictionaries.TabIndex = 145;
            this.picDictionaries.TabStop = false;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(557, 612);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 144;
            this.butCancel.Text = "Cancel";
            this.metroToolTip1.SetToolTip(this.butCancel, "Cancel Changes");
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(465, 612);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 143;
            this.butOK.Text = "Save";
            this.metroToolTip1.SetToolTip(this.butOK, "Save Changes");
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // dvgModelItems
            // 
            this.dvgModelItems.AllowUserToAddRows = false;
            this.dvgModelItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgModelItems.BackgroundColor = System.Drawing.Color.White;
            this.dvgModelItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dvgModelItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgModelItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvgModelItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvgModelItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.dvgModelItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dvgModelItems.GridColor = System.Drawing.Color.White;
            this.dvgModelItems.Location = new System.Drawing.Point(20, 129);
            this.dvgModelItems.MultiSelect = false;
            this.dvgModelItems.Name = "dvgModelItems";
            this.dvgModelItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgModelItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dvgModelItems.RowHeadersVisible = false;
            this.dvgModelItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgModelItems.Size = new System.Drawing.Size(612, 231);
            this.dvgModelItems.TabIndex = 151;
            this.dvgModelItems.SelectionChanged += new System.EventHandler(this.dvgModelItems_SelectionChanged);
            this.dvgModelItems.Paint += new System.Windows.Forms.PaintEventHandler(this.dvgModelItems_Paint);
            // 
            // frmRAMModel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(647, 648);
            this.ControlBox = false;
            this.Controls.Add(this.dvgModelItems);
            this.Controls.Add(this.panDictionary);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.panMaintainDictionaryTerms);
            this.Controls.Add(this.picDictionaries);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmRAMModel";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "      RAM Model";
            this.TopMost = true;
            this.panDictionary.ResumeLayout(false);
            this.panDictionary.PerformLayout();
            this.panMaintainDictionaryTerms.ResumeLayout(false);
            this.panMaintainDictionaryTerms.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDictionaries)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgModelItems)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRoleDescription;
        private System.Windows.Forms.Label lblNotation;
        private System.Windows.Forms.TextBox txtRoleNotation;
        private System.Windows.Forms.TextBox txtbRAMName;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panDictionary;
        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Panel panMaintainDictionaryTerms;
        private MetroFramework.Controls.MetroButton butNew;
        private MetroFramework.Controls.MetroButton butReplace;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.ComboBox cmbboxClr;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.PictureBox picDictionaries;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOK;
        private System.Windows.Forms.Label lblRoleName;
        private System.Windows.Forms.TextBox txtbRoleName;
        private System.Windows.Forms.DataGridView dvgModelItems;
    }
}