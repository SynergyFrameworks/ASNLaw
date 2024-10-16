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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblHeaderCaption = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butFilter = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.dvgKeywords = new System.Windows.Forms.DataGridView();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.panHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgKeywords)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.White;
            this.panHeader.Controls.Add(this.lblHeaderCaption);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(793, 40);
            this.panHeader.TabIndex = 12;
            // 
            // lblHeaderCaption
            // 
            this.lblHeaderCaption.AutoSize = true;
            this.lblHeaderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblHeaderCaption.Location = new System.Drawing.Point(11, 9);
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            this.lblHeaderCaption.Size = new System.Drawing.Size(126, 21);
            this.lblHeaderCaption.TabIndex = 1;
            this.lblHeaderCaption.Text = "Keywords Found";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.butFilter);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 350);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(793, 40);
            this.panel1.TabIndex = 13;
            // 
            // butFilter
            // 
            this.butFilter.Location = new System.Drawing.Point(15, 9);
            this.butFilter.Name = "butFilter";
            this.butFilter.Size = new System.Drawing.Size(75, 23);
            this.butFilter.TabIndex = 92;
            this.butFilter.Text = "Filter";
            this.butFilter.UseSelectable = true;
            this.butFilter.Click += new System.EventHandler(this.butFilter_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(96, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(218, 17);
            this.label1.TabIndex = 25;
            this.label1.Text = "Filter Results per selected Keywords";
            // 
            // dvgKeywords
            // 
            this.dvgKeywords.AllowUserToAddRows = false;
            this.dvgKeywords.AllowUserToDeleteRows = false;
            this.dvgKeywords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgKeywords.BackgroundColor = System.Drawing.Color.White;
            this.dvgKeywords.BorderStyle = System.Windows.Forms.BorderStyle.None;
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
            this.dvgKeywords.TabIndex = 15;
            this.dvgKeywords.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dvgKeywords_CellContentClick);
            this.dvgKeywords.CurrentCellDirtyStateChanged += new System.EventHandler(this.dvgKeywords_CurrentCellDirtyStateChanged);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
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
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgKeywords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblHeaderCaption;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView dvgKeywords;
        private MetroFramework.Controls.MetroButton butFilter;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}
