namespace ProfessionalDocAnalyzer
{
    partial class ucAcroDictionaries
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panHeader = new System.Windows.Forms.Panel();
            this.butIgnoreDictionaries = new System.Windows.Forms.Button();
            this.butAcronymsDictionaries = new System.Windows.Forms.Button();
            this.panFind = new System.Windows.Forms.Panel();
            this.butFind = new MetroFramework.Controls.MetroButton();
            this.txtbFind = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.panAcronymsDictionaries = new System.Windows.Forms.Panel();
            this.dgvAcronyms = new System.Windows.Forms.DataGridView();
            this.panFooter = new System.Windows.Forms.Panel();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.lstbDics = new System.Windows.Forms.ListBox();
            this.panIgnoreDictionaries = new System.Windows.Forms.Panel();
            this.dgvIgnore = new System.Windows.Forms.DataGridView();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblMessage2 = new System.Windows.Forms.Label();
            this.txtbMessage2 = new System.Windows.Forms.TextBox();
            this.lstbIgnore = new System.Windows.Forms.ListBox();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.panHeader.SuspendLayout();
            this.panFind.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.panAcronymsDictionaries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcronyms)).BeginInit();
            this.panFooter.SuspendLayout();
            this.panIgnoreDictionaries.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvIgnore)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.White;
            this.panHeader.Controls.Add(this.butIgnoreDictionaries);
            this.panHeader.Controls.Add(this.butAcronymsDictionaries);
            this.panHeader.Controls.Add(this.panFind);
            this.panHeader.Controls.Add(this.lblMessage);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Controls.Add(this.picHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(915, 80);
            this.panHeader.TabIndex = 8;
            // 
            // butIgnoreDictionaries
            // 
            this.butIgnoreDictionaries.BackColor = System.Drawing.Color.WhiteSmoke;
            this.butIgnoreDictionaries.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butIgnoreDictionaries.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butIgnoreDictionaries.Location = new System.Drawing.Point(155, 44);
            this.butIgnoreDictionaries.Name = "butIgnoreDictionaries";
            this.butIgnoreDictionaries.Size = new System.Drawing.Size(140, 29);
            this.butIgnoreDictionaries.TabIndex = 19;
            this.butIgnoreDictionaries.Text = "Ignore";
            this.butIgnoreDictionaries.UseVisualStyleBackColor = false;
            this.butIgnoreDictionaries.Click += new System.EventHandler(this.butIgnoreDictionaries_Click);
            this.butIgnoreDictionaries.MouseEnter += new System.EventHandler(this.butIgnoreDictionaries_MouseEnter);
            this.butIgnoreDictionaries.MouseLeave += new System.EventHandler(this.butIgnoreDictionaries_MouseLeave);
            // 
            // butAcronymsDictionaries
            // 
            this.butAcronymsDictionaries.BackColor = System.Drawing.Color.LightGreen;
            this.butAcronymsDictionaries.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.butAcronymsDictionaries.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butAcronymsDictionaries.Location = new System.Drawing.Point(9, 44);
            this.butAcronymsDictionaries.Name = "butAcronymsDictionaries";
            this.butAcronymsDictionaries.Size = new System.Drawing.Size(140, 29);
            this.butAcronymsDictionaries.TabIndex = 18;
            this.butAcronymsDictionaries.Text = "Acronyms";
            this.butAcronymsDictionaries.UseVisualStyleBackColor = false;
            this.butAcronymsDictionaries.Click += new System.EventHandler(this.butAcronymsDictionaries_Click);
            this.butAcronymsDictionaries.MouseEnter += new System.EventHandler(this.butAcronymsDictionaries_MouseEnter);
            this.butAcronymsDictionaries.MouseLeave += new System.EventHandler(this.butAcronymsDictionaries_MouseLeave);
            // 
            // panFind
            // 
            this.panFind.BackColor = System.Drawing.Color.White;
            this.panFind.Controls.Add(this.butFind);
            this.panFind.Controls.Add(this.txtbFind);
            this.panFind.Controls.Add(this.label1);
            this.panFind.Dock = System.Windows.Forms.DockStyle.Right;
            this.panFind.Location = new System.Drawing.Point(299, 0);
            this.panFind.Name = "panFind";
            this.panFind.Size = new System.Drawing.Size(616, 80);
            this.panFind.TabIndex = 17;
            // 
            // butFind
            // 
            this.butFind.Location = new System.Drawing.Point(464, 20);
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
            this.txtbFind.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbFind.Location = new System.Drawing.Point(263, 19);
            this.txtbFind.Name = "txtbFind";
            this.txtbFind.Size = new System.Drawing.Size(196, 25);
            this.txtbFind.TabIndex = 18;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(25, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 17);
            this.label1.TabIndex = 16;
            this.label1.Text = "In the selected dictionary find Acronym";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(320, 11);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 16;
            this.lblMessage.Visible = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(56, 11);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(211, 30);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Acronym Dictionaries";
            // 
            // picHeader
            // 
            this.picHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHeader.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_book_list;
            this.picHeader.Location = new System.Drawing.Point(9, 6);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(38, 38);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 14;
            this.picHeader.TabStop = false;
            this.picHeader.Click += new System.EventHandler(this.picHeader_Click_1);
            // 
            // panAcronymsDictionaries
            // 
            this.panAcronymsDictionaries.BackColor = System.Drawing.Color.White;
            this.panAcronymsDictionaries.Controls.Add(this.dgvAcronyms);
            this.panAcronymsDictionaries.Controls.Add(this.panFooter);
            this.panAcronymsDictionaries.Controls.Add(this.lstbDics);
            this.panAcronymsDictionaries.Location = new System.Drawing.Point(0, 86);
            this.panAcronymsDictionaries.Name = "panAcronymsDictionaries";
            this.panAcronymsDictionaries.Size = new System.Drawing.Size(825, 320);
            this.panAcronymsDictionaries.TabIndex = 9;
            // 
            // dgvAcronyms
            // 
            this.dgvAcronyms.AllowUserToAddRows = false;
            this.dgvAcronyms.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAcronyms.BackgroundColor = System.Drawing.Color.White;
            this.dgvAcronyms.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAcronyms.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Gainsboro;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAcronyms.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvAcronyms.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.MenuHighlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvAcronyms.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvAcronyms.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAcronyms.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAcronyms.GridColor = System.Drawing.Color.White;
            this.dgvAcronyms.Location = new System.Drawing.Point(247, 0);
            this.dgvAcronyms.MultiSelect = false;
            this.dgvAcronyms.Name = "dgvAcronyms";
            this.dgvAcronyms.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvAcronyms.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvAcronyms.RowHeadersVisible = false;
            this.dgvAcronyms.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAcronyms.Size = new System.Drawing.Size(578, 257);
            this.dgvAcronyms.TabIndex = 51;
            // 
            // panFooter
            // 
            this.panFooter.BackColor = System.Drawing.Color.White;
            this.panFooter.Controls.Add(this.txtbMessage);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(247, 257);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(578, 63);
            this.panFooter.TabIndex = 50;
            // 
            // txtbMessage
            // 
            this.txtbMessage.BackColor = System.Drawing.Color.White;
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.Black;
            this.txtbMessage.Location = new System.Drawing.Point(44, 2);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(522, 61);
            this.txtbMessage.TabIndex = 44;
            // 
            // lstbDics
            // 
            this.lstbDics.BackColor = System.Drawing.Color.White;
            this.lstbDics.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbDics.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstbDics.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbDics.ForeColor = System.Drawing.Color.Black;
            this.lstbDics.FormattingEnabled = true;
            this.lstbDics.ItemHeight = 21;
            this.lstbDics.Location = new System.Drawing.Point(0, 0);
            this.lstbDics.Name = "lstbDics";
            this.lstbDics.Size = new System.Drawing.Size(247, 320);
            this.lstbDics.TabIndex = 49;
            this.lstbDics.SelectedIndexChanged += new System.EventHandler(this.lstbDics_SelectedIndexChanged);
            // 
            // panIgnoreDictionaries
            // 
            this.panIgnoreDictionaries.BackColor = System.Drawing.Color.White;
            this.panIgnoreDictionaries.Controls.Add(this.dgvIgnore);
            this.panIgnoreDictionaries.Controls.Add(this.panel1);
            this.panIgnoreDictionaries.Controls.Add(this.lstbIgnore);
            this.panIgnoreDictionaries.Location = new System.Drawing.Point(247, 150);
            this.panIgnoreDictionaries.Name = "panIgnoreDictionaries";
            this.panIgnoreDictionaries.Size = new System.Drawing.Size(1430, 311);
            this.panIgnoreDictionaries.TabIndex = 10;
            this.panIgnoreDictionaries.Visible = false;
            // 
            // dgvIgnore
            // 
            this.dgvIgnore.AllowUserToAddRows = false;
            this.dgvIgnore.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvIgnore.BackgroundColor = System.Drawing.Color.White;
            this.dgvIgnore.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvIgnore.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIgnore.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvIgnore.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvIgnore.DefaultCellStyle = dataGridViewCellStyle5;
            this.dgvIgnore.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvIgnore.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvIgnore.GridColor = System.Drawing.Color.White;
            this.dgvIgnore.Location = new System.Drawing.Point(247, 0);
            this.dgvIgnore.Name = "dgvIgnore";
            this.dgvIgnore.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvIgnore.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dgvIgnore.RowHeadersVisible = false;
            this.dgvIgnore.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvIgnore.Size = new System.Drawing.Size(1183, 248);
            this.dgvIgnore.TabIndex = 53;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.lblMessage2);
            this.panel1.Controls.Add(this.txtbMessage2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(247, 248);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1183, 63);
            this.panel1.TabIndex = 52;
            // 
            // lblMessage2
            // 
            this.lblMessage2.AutoSize = true;
            this.lblMessage2.Location = new System.Drawing.Point(6, 18);
            this.lblMessage2.Name = "lblMessage2";
            this.lblMessage2.Size = new System.Drawing.Size(35, 13);
            this.lblMessage2.TabIndex = 46;
            this.lblMessage2.Text = "label1";
            this.lblMessage2.Visible = false;
            // 
            // txtbMessage2
            // 
            this.txtbMessage2.BackColor = System.Drawing.Color.White;
            this.txtbMessage2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage2.ForeColor = System.Drawing.Color.Black;
            this.txtbMessage2.Location = new System.Drawing.Point(69, 1);
            this.txtbMessage2.Multiline = true;
            this.txtbMessage2.Name = "txtbMessage2";
            this.txtbMessage2.Size = new System.Drawing.Size(522, 61);
            this.txtbMessage2.TabIndex = 45;
            // 
            // lstbIgnore
            // 
            this.lstbIgnore.BackColor = System.Drawing.Color.White;
            this.lstbIgnore.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbIgnore.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstbIgnore.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbIgnore.ForeColor = System.Drawing.Color.Black;
            this.lstbIgnore.FormattingEnabled = true;
            this.lstbIgnore.ItemHeight = 21;
            this.lstbIgnore.Location = new System.Drawing.Point(0, 0);
            this.lstbIgnore.Name = "lstbIgnore";
            this.lstbIgnore.Size = new System.Drawing.Size(247, 311);
            this.lstbIgnore.TabIndex = 51;
            this.lstbIgnore.SelectedIndexChanged += new System.EventHandler(this.lstbIgnore_SelectedIndexChanged);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ucAcroDictionaries
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panIgnoreDictionaries);
            this.Controls.Add(this.panAcronymsDictionaries);
            this.Controls.Add(this.panHeader);
            this.Name = "ucAcroDictionaries";
            this.Size = new System.Drawing.Size(915, 424);
            this.Load += new System.EventHandler(this.ucAcroDictionaries_Load);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panFind.ResumeLayout(false);
            this.panFind.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.panAcronymsDictionaries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAcronyms)).EndInit();
            this.panFooter.ResumeLayout(false);
            this.panFooter.PerformLayout();
            this.panIgnoreDictionaries.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvIgnore)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Panel panFind;
        private MetroFramework.Controls.MetroButton butFind;
        private System.Windows.Forms.TextBox txtbFind;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.Button butIgnoreDictionaries;
        private System.Windows.Forms.Button butAcronymsDictionaries;
        private System.Windows.Forms.Panel panAcronymsDictionaries;
        private System.Windows.Forms.DataGridView dgvAcronyms;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.TextBox txtbMessage;
        private System.Windows.Forms.ListBox lstbDics;
        private System.Windows.Forms.Panel panIgnoreDictionaries;
        private System.Windows.Forms.DataGridView dgvIgnore;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblMessage2;
        private System.Windows.Forms.TextBox txtbMessage2;
        private System.Windows.Forms.ListBox lstbIgnore;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}
