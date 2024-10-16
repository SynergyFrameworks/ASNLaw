namespace MatrixBuilder
{
    partial class ucProjects
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucProjects));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblDefinition = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panFooter = new System.Windows.Forms.Panel();
            this.splitContMain = new System.Windows.Forms.SplitContainer();
            this.lstbProjects = new System.Windows.Forms.ListBox();
            this.panLeftTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContDocs = new System.Windows.Forms.SplitContainer();
            this.dvgDocs = new System.Windows.Forms.DataGridView();
            this.richerTextBox1 = new RicherTextBox.RicherTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).BeginInit();
            this.splitContMain.Panel1.SuspendLayout();
            this.splitContMain.Panel2.SuspendLayout();
            this.splitContMain.SuspendLayout();
            this.panLeftTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContDocs)).BeginInit();
            this.splitContDocs.Panel1.SuspendLayout();
            this.splitContDocs.Panel2.SuspendLayout();
            this.splitContDocs.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgDocs)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.lblDefinition);
            this.panHeader.Controls.Add(this.lblMessage);
            this.panHeader.Controls.Add(this.picHeader);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(944, 50);
            this.panHeader.TabIndex = 19;
            // 
            // lblDefinition
            // 
            this.lblDefinition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefinition.ForeColor = System.Drawing.Color.White;
            this.lblDefinition.Location = new System.Drawing.Point(150, 8);
            this.lblDefinition.Name = "lblDefinition";
            this.lblDefinition.Size = new System.Drawing.Size(746, 35);
            this.lblDefinition.TabIndex = 186;
            this.lblDefinition.Text = resources.GetString("lblDefinition.Text");
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(206, 21);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 17);
            this.lblMessage.TabIndex = 24;
            // 
            // picHeader
            // 
            this.picHeader.Image = ((System.Drawing.Image)(resources.GetObject("picHeader.Image")));
            this.picHeader.Location = new System.Drawing.Point(9, 6);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(41, 38);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 16;
            this.picHeader.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(56, 11);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(86, 30);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Projects";
            // 
            // panFooter
            // 
            this.panFooter.BackColor = System.Drawing.Color.Black;
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(0, 453);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(944, 38);
            this.panFooter.TabIndex = 20;
            // 
            // splitContMain
            // 
            this.splitContMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContMain.Location = new System.Drawing.Point(0, 50);
            this.splitContMain.Name = "splitContMain";
            // 
            // splitContMain.Panel1
            // 
            this.splitContMain.Panel1.Controls.Add(this.lstbProjects);
            this.splitContMain.Panel1.Controls.Add(this.panLeftTop);
            // 
            // splitContMain.Panel2
            // 
            this.splitContMain.Panel2.Controls.Add(this.splitContDocs);
            this.splitContMain.Panel2.Controls.Add(this.panel1);
            this.splitContMain.Size = new System.Drawing.Size(944, 403);
            this.splitContMain.SplitterDistance = 314;
            this.splitContMain.TabIndex = 21;
            // 
            // lstbProjects
            // 
            this.lstbProjects.BackColor = System.Drawing.Color.Black;
            this.lstbProjects.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbProjects.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbProjects.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbProjects.ForeColor = System.Drawing.Color.White;
            this.lstbProjects.FormattingEnabled = true;
            this.lstbProjects.ItemHeight = 21;
            this.lstbProjects.Location = new System.Drawing.Point(0, 39);
            this.lstbProjects.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.lstbProjects.Name = "lstbProjects";
            this.lstbProjects.Size = new System.Drawing.Size(314, 364);
            this.lstbProjects.TabIndex = 3;
            this.lstbProjects.SelectedIndexChanged += new System.EventHandler(this.lstbProjects_SelectedIndexChanged);
            // 
            // panLeftTop
            // 
            this.panLeftTop.BackColor = System.Drawing.Color.Black;
            this.panLeftTop.Controls.Add(this.label1);
            this.panLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panLeftTop.Location = new System.Drawing.Point(0, 0);
            this.panLeftTop.Name = "panLeftTop";
            this.panLeftTop.Size = new System.Drawing.Size(314, 39);
            this.panLeftTop.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(141, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Select a Project";
            // 
            // splitContDocs
            // 
            this.splitContDocs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContDocs.Location = new System.Drawing.Point(0, 39);
            this.splitContDocs.Name = "splitContDocs";
            this.splitContDocs.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContDocs.Panel1
            // 
            this.splitContDocs.Panel1.Controls.Add(this.dvgDocs);
            // 
            // splitContDocs.Panel2
            // 
            this.splitContDocs.Panel2.Controls.Add(this.richerTextBox1);
            this.splitContDocs.Size = new System.Drawing.Size(626, 364);
            this.splitContDocs.SplitterDistance = 170;
            this.splitContDocs.TabIndex = 2;
            // 
            // dvgDocs
            // 
            this.dvgDocs.AllowUserToAddRows = false;
            this.dvgDocs.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgDocs.BackgroundColor = System.Drawing.Color.Black;
            this.dvgDocs.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dvgDocs.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgDocs.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvgDocs.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgDocs.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvgDocs.DefaultCellStyle = dataGridViewCellStyle2;
            this.dvgDocs.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgDocs.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dvgDocs.GridColor = System.Drawing.Color.Black;
            this.dvgDocs.Location = new System.Drawing.Point(0, 0);
            this.dvgDocs.MultiSelect = false;
            this.dvgDocs.Name = "dvgDocs";
            this.dvgDocs.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgDocs.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dvgDocs.RowHeadersVisible = false;
            this.dvgDocs.RowHeadersWidth = 5;
            this.dvgDocs.RowTemplate.DefaultCellStyle.BackColor = System.Drawing.Color.Black;
            this.dvgDocs.RowTemplate.DefaultCellStyle.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dvgDocs.RowTemplate.DefaultCellStyle.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            this.dvgDocs.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgDocs.ShowEditingIcon = false;
            this.dvgDocs.Size = new System.Drawing.Size(626, 170);
            this.dvgDocs.TabIndex = 87;
            this.dvgDocs.SelectionChanged += new System.EventHandler(this.dvgDocs_SelectionChanged);
            // 
            // richerTextBox1
            // 
            this.richerTextBox1.AlignCenterVisible = false;
            this.richerTextBox1.AlignLeftVisible = false;
            this.richerTextBox1.AlignRightVisible = false;
            this.richerTextBox1.BoldVisible = false;
            this.richerTextBox1.BulletsVisible = false;
            this.richerTextBox1.ChooseFontVisible = false;
            this.richerTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richerTextBox1.FindReplaceVisible = true;
            this.richerTextBox1.FontColorVisible = false;
            this.richerTextBox1.FontFamilyVisible = false;
            this.richerTextBox1.FontSizeVisible = false;
            this.richerTextBox1.GroupAlignmentVisible = false;
            this.richerTextBox1.GroupBoldUnderlineItalicVisible = false;
            this.richerTextBox1.GroupFontColorVisible = false;
            this.richerTextBox1.GroupFontNameAndSizeVisible = false;
            this.richerTextBox1.GroupIndentationAndBulletsVisible = false;
            this.richerTextBox1.GroupInsertVisible = false;
            this.richerTextBox1.GroupSaveAndLoadVisible = false;
            this.richerTextBox1.GroupZoomVisible = false;
            this.richerTextBox1.INDENT = 10;
            this.richerTextBox1.IndentVisible = false;
            this.richerTextBox1.InsertPictureVisible = false;
            this.richerTextBox1.ItalicVisible = false;
            this.richerTextBox1.LoadedFile = "";
            this.richerTextBox1.LoadVisible = false;
            this.richerTextBox1.Location = new System.Drawing.Point(0, 0);
            this.richerTextBox1.Name = "richerTextBox1";
            this.richerTextBox1.OutdentVisible = false;
            this.richerTextBox1.ReplaceVisible = false;
            this.richerTextBox1.Rtf = "{\\rtf1\\ansi\\ansicpg1251\\deff0\\deflang1026{\\fonttbl{\\f0\\fnil\\fcharset204 Microsoft" +
    " Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs18\\par\r\n}\r\n";
            this.richerTextBox1.SaveVisible = false;
            this.richerTextBox1.SeparatorAlignVisible = false;
            this.richerTextBox1.SeparatorBoldUnderlineItalicVisible = false;
            this.richerTextBox1.SeparatorFontColorVisible = false;
            this.richerTextBox1.SeparatorFontVisible = false;
            this.richerTextBox1.SeparatorIndentAndBulletsVisible = false;
            this.richerTextBox1.SeparatorInsertVisible = false;
            this.richerTextBox1.SeparatorSaveLoadVisible = false;
            this.richerTextBox1.Size = new System.Drawing.Size(626, 190);
            this.richerTextBox1.TabIndex = 9;
            this.richerTextBox1.ToolStripVisible = false;
            this.richerTextBox1.UnderlineVisible = false;
            this.richerTextBox1.WordWrapVisible = false;
            this.richerTextBox1.ZoomFactorTextVisible = false;
            this.richerTextBox1.ZoomInVisible = false;
            this.richerTextBox1.ZoomOutVisible = false;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(626, 39);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(6, 1);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 32;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(45, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(107, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "Documents";
            // 
            // ucProjects
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContMain);
            this.Controls.Add(this.panFooter);
            this.Controls.Add(this.panHeader);
            this.Name = "ucProjects";
            this.Size = new System.Drawing.Size(944, 491);
            this.VisibleChanged += new System.EventHandler(this.ucProjects_VisibleChanged);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.splitContMain.Panel1.ResumeLayout(false);
            this.splitContMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).EndInit();
            this.splitContMain.ResumeLayout(false);
            this.panLeftTop.ResumeLayout(false);
            this.panLeftTop.PerformLayout();
            this.splitContDocs.Panel1.ResumeLayout(false);
            this.splitContDocs.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContDocs)).EndInit();
            this.splitContDocs.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgDocs)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.SplitContainer splitContMain;
        private System.Windows.Forms.Panel panLeftTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContDocs;
        private RicherTextBox.RicherTextBox richerTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ListBox lstbProjects;
        private System.Windows.Forms.DataGridView dvgDocs;
        private System.Windows.Forms.Label lblDefinition;
        private System.Windows.Forms.PictureBox pictureBox2;
    }
}
