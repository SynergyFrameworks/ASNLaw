namespace ProfessionalDocAnalyzer
{
    partial class ucDeepAnalyticsKeywords
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDeepAnalyticsKeywords));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panHeader = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblHeaderCaption = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.butFilter = new System.Windows.Forms.PictureBox();
            this.dvgKeywords = new System.Windows.Forms.DataGridView();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butFilter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgKeywords)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.pictureBox1);
            this.panHeader.Controls.Add(this.lblHeaderCaption);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(793, 40);
            this.panHeader.TabIndex = 11;
            this.panHeader.Paint += new System.Windows.Forms.PaintEventHandler(this.panHeader_Paint);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(3, 2);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 38;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // lblHeaderCaption
            // 
            this.lblHeaderCaption.AutoSize = true;
            this.lblHeaderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.White;
            this.lblHeaderCaption.Location = new System.Drawing.Point(37, 9);
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            this.lblHeaderCaption.Size = new System.Drawing.Size(151, 25);
            this.lblHeaderCaption.TabIndex = 1;
            this.lblHeaderCaption.Text = "Keywords Found";
            this.lblHeaderCaption.Click += new System.EventHandler(this.lblHeaderCaption_Click);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.butFilter);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 350);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(793, 40);
            this.panel1.TabIndex = 12;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(13, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "Filter Results per selected Keywords";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // butFilter
            // 
            this.butFilter.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butFilter.Image = ((System.Drawing.Image)(resources.GetObject("butFilter.Image")));
            this.butFilter.Location = new System.Drawing.Point(237, 6);
            this.butFilter.Name = "butFilter";
            this.butFilter.Size = new System.Drawing.Size(28, 28);
            this.butFilter.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butFilter.TabIndex = 24;
            this.butFilter.TabStop = false;
            this.butFilter.Click += new System.EventHandler(this.butFilter_Click);
            // 
            // dvgKeywords
            // 
            this.dvgKeywords.AllowUserToAddRows = false;
            this.dvgKeywords.AllowUserToDeleteRows = false;
            this.dvgKeywords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgKeywords.BackgroundColor = System.Drawing.Color.Black;
            this.dvgKeywords.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgKeywords.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvgKeywords.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgKeywords.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvgKeywords.DefaultCellStyle = dataGridViewCellStyle2;
            this.dvgKeywords.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgKeywords.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dvgKeywords.Location = new System.Drawing.Point(0, 40);
            this.dvgKeywords.MultiSelect = false;
            this.dvgKeywords.Name = "dvgKeywords";
            this.dvgKeywords.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgKeywords.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dvgKeywords.RowHeadersWidth = 5;
            this.dvgKeywords.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgKeywords.Size = new System.Drawing.Size(793, 310);
            this.dvgKeywords.TabIndex = 14;
            this.dvgKeywords.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgKeywords_CellContentClick);
            this.dvgKeywords.CellValueChanged += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgKeywords_CellValueChanged);
            this.dvgKeywords.CurrentCellDirtyStateChanged += new System.EventHandler(this.dvgKeywords_CurrentCellDirtyStateChanged);
            // 
            // ucDeepAnalyticsKeywords
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dvgKeywords);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panHeader);
            this.Name = "ucDeepAnalyticsKeywords";
            this.Size = new System.Drawing.Size(793, 390);
            this.VisibleChanged += new System.EventHandler(this.ucDeepAnalyticsKeywords_VisibleChanged);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.ucDeepAnalyticsKeywords_Paint);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butFilter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dvgKeywords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblHeaderCaption;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.DataGridView dvgKeywords;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox butFilter;
    }
}
