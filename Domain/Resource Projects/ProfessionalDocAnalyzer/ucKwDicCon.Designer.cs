namespace ProfessionalDocAnalyzer
{
    partial class ucKwDicCon
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
            this.lblFilterMessage = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tagCloudControl1 = new TagCloud.TagCloudControl();
            this.dvgKeywords = new System.Windows.Forms.DataGridView();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.panHeader.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
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
            this.panHeader.Size = new System.Drawing.Size(754, 34);
            this.panHeader.TabIndex = 15;
            // 
            // lblHeaderCaption
            // 
            this.lblHeaderCaption.AutoSize = true;
            this.lblHeaderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblHeaderCaption.Location = new System.Drawing.Point(11, 4);
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            this.lblHeaderCaption.Size = new System.Drawing.Size(151, 25);
            this.lblHeaderCaption.TabIndex = 1;
            this.lblHeaderCaption.Text = "Keywords Found";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Controls.Add(this.butFilter);
            this.panel1.Controls.Add(this.lblFilterMessage);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 398);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(754, 40);
            this.panel1.TabIndex = 16;
            // 
            // butFilter
            // 
            this.butFilter.Location = new System.Drawing.Point(16, 8);
            this.butFilter.Name = "butFilter";
            this.butFilter.Size = new System.Drawing.Size(75, 23);
            this.butFilter.TabIndex = 91;
            this.butFilter.Text = "Filter";
            this.butFilter.UseSelectable = true;
            this.butFilter.Click += new System.EventHandler(this.butFilter_Click_1);
            // 
            // lblFilterMessage
            // 
            this.lblFilterMessage.AutoSize = true;
            this.lblFilterMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblFilterMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFilterMessage.ForeColor = System.Drawing.Color.Black;
            this.lblFilterMessage.Location = new System.Drawing.Point(97, 14);
            this.lblFilterMessage.Name = "lblFilterMessage";
            this.lblFilterMessage.Size = new System.Drawing.Size(218, 17);
            this.lblFilterMessage.TabIndex = 25;
            this.lblFilterMessage.Text = "Filter Results per selected Keywords";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 34);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tagCloudControl1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.dvgKeywords);
            this.splitContainer1.Size = new System.Drawing.Size(754, 364);
            this.splitContainer1.SplitterDistance = 167;
            this.splitContainer1.TabIndex = 17;
            // 
            // tagCloudControl1
            // 
            this.tagCloudControl1.AllowDrop = true;
            this.tagCloudControl1.AutoScroll = true;
            this.tagCloudControl1.AutoSize = true;
            this.tagCloudControl1.BackColor = System.Drawing.Color.Black;
            this.tagCloudControl1.ControlBackColor = System.Drawing.Color.Black;
            this.tagCloudControl1.ControlHeight = 198;
            this.tagCloudControl1.ControlTextFrame = false;
            this.tagCloudControl1.ControlTextUnderline = false;
            this.tagCloudControl1.ControlWidth = 298;
            this.tagCloudControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tagCloudControl1.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tagCloudControl1.Location = new System.Drawing.Point(0, 0);
            this.tagCloudControl1.Margin = new System.Windows.Forms.Padding(6, 7, 6, 7);
            this.tagCloudControl1.Name = "tagCloudControl1";
            this.tagCloudControl1.Size = new System.Drawing.Size(754, 167);
            this.tagCloudControl1.TabIndex = 22;
            // 
            // dvgKeywords
            // 
            this.dvgKeywords.AllowUserToAddRows = false;
            this.dvgKeywords.AllowUserToDeleteRows = false;
            this.dvgKeywords.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgKeywords.BackgroundColor = System.Drawing.Color.White;
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
            this.dvgKeywords.Location = new System.Drawing.Point(0, 0);
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
            this.dvgKeywords.Size = new System.Drawing.Size(754, 193);
            this.dvgKeywords.TabIndex = 19;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ucKwDicCon
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.panel1);
            this.Name = "ucKwDicCon";
            this.Size = new System.Drawing.Size(754, 438);
            this.VisibleChanged += new System.EventHandler(this.ucKwDicCon_VisibleChanged);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgKeywords)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblHeaderCaption;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblFilterMessage;
        private MetroFramework.Controls.MetroButton butFilter;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private TagCloud.TagCloudControl tagCloudControl1;
        private System.Windows.Forms.DataGridView dvgKeywords;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}
