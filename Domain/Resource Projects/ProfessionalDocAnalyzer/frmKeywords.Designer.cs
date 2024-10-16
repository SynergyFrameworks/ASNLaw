namespace ProfessionalDocAnalyzer
{
    partial class frmKeywords
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle31 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle32 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle33 = new System.Windows.Forms.DataGridViewCellStyle();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.txtbFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.dvgKeywords = new System.Windows.Forms.DataGridView();
            this.panContainer = new System.Windows.Forms.Panel();
            this.panBatchLoad = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.cmbboxClr2 = new System.Windows.Forms.ComboBox();
            this.butAddBatchKeywords = new MetroFramework.Controls.MetroButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtbBatchKeywords = new System.Windows.Forms.TextBox();
            this.panMaintain = new System.Windows.Forms.Panel();
            this.cmbboxClr = new System.Windows.Forms.ComboBox();
            this.butNew = new MetroFramework.Controls.MetroButton();
            this.butReplace = new MetroFramework.Controls.MetroButton();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbSection = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.butMaintain = new System.Windows.Forms.Button();
            this.butBatchLoad = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgKeywords)).BeginInit();
            this.panContainer.SuspendLayout();
            this.panBatchLoad.SuspendLayout();
            this.panMaintain.SuspendLayout();
            this.SuspendLayout();
            // 
            // picHeader
            // 
            this.picHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHeader.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_key;
            this.picHeader.Location = new System.Drawing.Point(11, 9);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(41, 38);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 65;
            this.picHeader.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(55, 13);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(155, 30);
            this.lblHeader.TabIndex = 64;
            this.lblHeader.Text = "Keyword Group";
            // 
            // txtbMessage
            // 
            this.txtbMessage.BackColor = System.Drawing.Color.White;
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.Blue;
            this.txtbMessage.Location = new System.Drawing.Point(434, 18);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(398, 85);
            this.txtbMessage.TabIndex = 62;
            this.txtbMessage.TextChanged += new System.EventHandler(this.txtbMessage_TextChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(237, 42);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 61;
            this.lblMessage.Visible = false;
            this.lblMessage.TextChanged += new System.EventHandler(this.lblMessage_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(16, 55);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(139, 17);
            this.label4.TabIndex = 60;
            this.label4.Text = "Keyword Group Name";
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // txtbFileName
            // 
            this.txtbFileName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbFileName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbFileName.Location = new System.Drawing.Point(15, 74);
            this.txtbFileName.MaxLength = 50;
            this.txtbFileName.Name = "txtbFileName";
            this.txtbFileName.Size = new System.Drawing.Size(292, 25);
            this.txtbFileName.TabIndex = 59;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(12, 74);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(65, 17);
            this.label3.TabIndex = 58;
            this.label3.Text = "Keywords";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(16, 113);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 17);
            this.label2.TabIndex = 67;
            this.label2.Text = "Keywords";
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(740, 377);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 69;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(650, 377);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 70;
            this.butOK.Text = "Save";
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // dvgKeywords
            // 
            this.dvgKeywords.AllowUserToAddRows = false;
            this.dvgKeywords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgKeywords.BackgroundColor = System.Drawing.Color.White;
            this.dvgKeywords.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle31.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle31.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle31.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle31.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle31.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle31.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgKeywords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle31;
            this.dvgKeywords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgKeywords.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle32.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle32.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle32.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle32.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle32.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle32.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle32.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvgKeywords.DefaultCellStyle = dataGridViewCellStyle32;
            this.dvgKeywords.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dvgKeywords.Location = new System.Drawing.Point(15, 136);
            this.dvgKeywords.MultiSelect = false;
            this.dvgKeywords.Name = "dvgKeywords";
            this.dvgKeywords.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle33.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle33.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle33.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle33.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle33.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle33.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgKeywords.RowHeadersDefaultCellStyle = dataGridViewCellStyle33;
            this.dvgKeywords.RowHeadersWidth = 5;
            this.dvgKeywords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgKeywords.ShowEditingIcon = false;
            this.dvgKeywords.Size = new System.Drawing.Size(399, 231);
            this.dvgKeywords.TabIndex = 86;
            this.dvgKeywords.SelectionChanged += new System.EventHandler(this.dvgKeywords_SelectionChanged);
            // 
            // panContainer
            // 
            this.panContainer.Controls.Add(this.panMaintain);
            this.panContainer.Controls.Add(this.panBatchLoad);
            this.panContainer.Location = new System.Drawing.Point(429, 136);
            this.panContainer.Name = "panContainer";
            this.panContainer.Size = new System.Drawing.Size(403, 231);
            this.panContainer.TabIndex = 87;
            // 
            // panBatchLoad
            // 
            this.panBatchLoad.BackColor = System.Drawing.Color.Black;
            this.panBatchLoad.Controls.Add(this.label7);
            this.panBatchLoad.Controls.Add(this.cmbboxClr2);
            this.panBatchLoad.Controls.Add(this.butAddBatchKeywords);
            this.panBatchLoad.Controls.Add(this.label5);
            this.panBatchLoad.Controls.Add(this.txtbBatchKeywords);
            this.panBatchLoad.Location = new System.Drawing.Point(47, 31);
            this.panBatchLoad.Name = "panBatchLoad";
            this.panBatchLoad.Size = new System.Drawing.Size(401, 225);
            this.panBatchLoad.TabIndex = 0;
            this.panBatchLoad.Visible = false;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(9, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(36, 15);
            this.label7.TabIndex = 86;
            this.label7.Text = "Color";
            // 
            // cmbboxClr2
            // 
            this.cmbboxClr2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbboxClr2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbboxClr2.DropDownWidth = 240;
            this.cmbboxClr2.FormattingEnabled = true;
            this.cmbboxClr2.ItemHeight = 20;
            this.cmbboxClr2.Location = new System.Drawing.Point(50, 182);
            this.cmbboxClr2.Name = "cmbboxClr2";
            this.cmbboxClr2.Size = new System.Drawing.Size(213, 26);
            this.cmbboxClr2.TabIndex = 85;
            this.cmbboxClr2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbboxClr2_DrawItem);
            // 
            // butAddBatchKeywords
            // 
            this.butAddBatchKeywords.Location = new System.Drawing.Point(304, 183);
            this.butAddBatchKeywords.Name = "butAddBatchKeywords";
            this.butAddBatchKeywords.Size = new System.Drawing.Size(75, 23);
            this.butAddBatchKeywords.TabIndex = 84;
            this.butAddBatchKeywords.Text = "Add";
            this.metroToolTip1.SetToolTip(this.butAddBatchKeywords, "Create a new Keywords");
            this.butAddBatchKeywords.UseSelectable = true;
            this.butAddBatchKeywords.Click += new System.EventHandler(this.butAddBatchKeywords_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(10, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(341, 17);
            this.label5.TabIndex = 83;
            this.label5.Text = "Comma-Separated Keywords (e.g. word1, word2, word3)";
            // 
            // txtbBatchKeywords
            // 
            this.txtbBatchKeywords.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbBatchKeywords.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbBatchKeywords.Location = new System.Drawing.Point(13, 37);
            this.txtbBatchKeywords.Multiline = true;
            this.txtbBatchKeywords.Name = "txtbBatchKeywords";
            this.txtbBatchKeywords.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbBatchKeywords.Size = new System.Drawing.Size(369, 124);
            this.txtbBatchKeywords.TabIndex = 82;
            // 
            // panMaintain
            // 
            this.panMaintain.BackColor = System.Drawing.Color.Black;
            this.panMaintain.Controls.Add(this.label6);
            this.panMaintain.Controls.Add(this.cmbboxClr);
            this.panMaintain.Controls.Add(this.butNew);
            this.panMaintain.Controls.Add(this.butReplace);
            this.panMaintain.Controls.Add(this.butDelete);
            this.panMaintain.Controls.Add(this.label1);
            this.panMaintain.Controls.Add(this.txtbSection);
            this.panMaintain.Location = new System.Drawing.Point(311, 23);
            this.panMaintain.Name = "panMaintain";
            this.panMaintain.Size = new System.Drawing.Size(401, 225);
            this.panMaintain.TabIndex = 1;
            this.panMaintain.Visible = false;
            // 
            // cmbboxClr
            // 
            this.cmbboxClr.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbboxClr.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbboxClr.DropDownWidth = 240;
            this.cmbboxClr.FormattingEnabled = true;
            this.cmbboxClr.ItemHeight = 20;
            this.cmbboxClr.Location = new System.Drawing.Point(92, 81);
            this.cmbboxClr.Name = "cmbboxClr";
            this.cmbboxClr.Size = new System.Drawing.Size(213, 26);
            this.cmbboxClr.TabIndex = 84;
            this.cmbboxClr.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cmbboxClr_DrawItem);
            // 
            // butNew
            // 
            this.butNew.Location = new System.Drawing.Point(7, 27);
            this.butNew.Name = "butNew";
            this.butNew.Size = new System.Drawing.Size(75, 23);
            this.butNew.TabIndex = 81;
            this.butNew.Text = "Add";
            this.metroToolTip1.SetToolTip(this.butNew, "Create a new Keyword");
            this.butNew.UseSelectable = true;
            this.butNew.Click += new System.EventHandler(this.butNew_Click);
            // 
            // butReplace
            // 
            this.butReplace.Location = new System.Drawing.Point(7, 59);
            this.butReplace.Name = "butReplace";
            this.butReplace.Size = new System.Drawing.Size(75, 23);
            this.butReplace.TabIndex = 82;
            this.butReplace.Text = "Change";
            this.metroToolTip1.SetToolTip(this.butReplace, "Replace the selected Keyword");
            this.butReplace.UseSelectable = true;
            this.butReplace.Click += new System.EventHandler(this.butReplace_Click);
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(7, 91);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(75, 23);
            this.butDelete.TabIndex = 83;
            this.butDelete.Text = "Delete";
            this.metroToolTip1.SetToolTip(this.butDelete, "Delete the selected Keyword");
            this.butDelete.UseSelectable = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(85, 6);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(206, 17);
            this.label1.TabIndex = 80;
            this.label1.Text = "Keyword (must be a unique value)";
            // 
            // txtbSection
            // 
            this.txtbSection.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbSection.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbSection.Location = new System.Drawing.Point(88, 26);
            this.txtbSection.Name = "txtbSection";
            this.txtbSection.Size = new System.Drawing.Size(292, 25);
            this.txtbSection.TabIndex = 79;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(89, 62);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(36, 15);
            this.label6.TabIndex = 85;
            this.label6.Text = "Color";
            // 
            // butMaintain
            // 
            this.butMaintain.BackColor = System.Drawing.Color.WhiteSmoke;
            this.butMaintain.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butMaintain.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butMaintain.Location = new System.Drawing.Point(525, 111);
            this.butMaintain.Name = "butMaintain";
            this.butMaintain.Size = new System.Drawing.Size(75, 23);
            this.butMaintain.TabIndex = 89;
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
            this.butBatchLoad.Location = new System.Drawing.Point(444, 111);
            this.butBatchLoad.Name = "butBatchLoad";
            this.butBatchLoad.Size = new System.Drawing.Size(75, 23);
            this.butBatchLoad.TabIndex = 88;
            this.butBatchLoad.Text = "Batch Load";
            this.butBatchLoad.UseVisualStyleBackColor = false;
            this.butBatchLoad.Click += new System.EventHandler(this.butBatchLoad_Click);
            this.butBatchLoad.MouseEnter += new System.EventHandler(this.butBatchLoad_MouseEnter);
            this.butBatchLoad.MouseLeave += new System.EventHandler(this.butBatchLoad_MouseLeave);
            // 
            // frmKeywords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(845, 407);
            this.ControlBox = false;
            this.Controls.Add(this.butMaintain);
            this.Controls.Add(this.butBatchLoad);
            this.Controls.Add(this.panContainer);
            this.Controls.Add(this.dvgKeywords);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picHeader);
            this.Controls.Add(this.lblHeader);
            this.Controls.Add(this.txtbMessage);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtbFileName);
            this.Controls.Add(this.label3);
            this.Name = "frmKeywords";
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Load += new System.EventHandler(this.frmKeywords_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgKeywords)).EndInit();
            this.panContainer.ResumeLayout(false);
            this.panBatchLoad.ResumeLayout(false);
            this.panBatchLoad.PerformLayout();
            this.panMaintain.ResumeLayout(false);
            this.panMaintain.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.TextBox txtbMessage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.TextBox txtbFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOK;
        private System.Windows.Forms.DataGridView dvgKeywords;
        private System.Windows.Forms.Panel panContainer;
        private System.Windows.Forms.Panel panMaintain;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox cmbboxClr;
        private MetroFramework.Controls.MetroButton butNew;
        private MetroFramework.Controls.MetroButton butReplace;
        private MetroFramework.Controls.MetroButton butDelete;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbSection;
        private System.Windows.Forms.Panel panBatchLoad;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox cmbboxClr2;
        private MetroFramework.Controls.MetroButton butAddBatchKeywords;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtbBatchKeywords;
        private System.Windows.Forms.Button butMaintain;
        private System.Windows.Forms.Button butBatchLoad;
    }
}