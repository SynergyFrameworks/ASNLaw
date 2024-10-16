namespace ProfessionalDocAnalyzer
{
    partial class ucResultsDic
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucResultsDic));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panHeader = new System.Windows.Forms.Panel();
            this.picDic = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.dvgParsedResults = new System.Windows.Forms.DataGridView();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer3 = new System.Windows.Forms.SplitContainer();
            this.ucResultsDicItemsFilter1 = new ProfessionalDocAnalyzer.ucResultsDicItemsFilter();
            this.ucResultsNotes1 = new ProfessionalDocAnalyzer.ucResultsNotes();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butSpeacker = new System.Windows.Forms.PictureBox();
            this.lblParsedSec = new System.Windows.Forms.Label();
            this.pnLeftTopRight = new System.Windows.Forms.Panel();
            this.butGenerateReport = new MetroFramework.Controls.MetroButton();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDic)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgParsedResults)).BeginInit();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).BeginInit();
            this.splitContainer3.Panel1.SuspendLayout();
            this.splitContainer3.Panel2.SuspendLayout();
            this.splitContainer3.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butSpeacker)).BeginInit();
            this.pnLeftTopRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.picDic);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(1041, 37);
            this.panHeader.TabIndex = 194;
            // 
            // picDic
            // 
            this.picDic.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDic.Image = ((System.Drawing.Image)(resources.GetObject("picDic.Image")));
            this.picDic.Location = new System.Drawing.Point(7, 6);
            this.picDic.Name = "picDic";
            this.picDic.Size = new System.Drawing.Size(28, 28);
            this.picDic.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDic.TabIndex = 19;
            this.picDic.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(33, 10);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(171, 21);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Dictionary Items Found";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 37);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.splitContainer2);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.richTextBox1);
            this.splitContainer1.Panel2.Controls.Add(this.panel1);
            this.splitContainer1.Size = new System.Drawing.Size(1041, 587);
            this.splitContainer1.SplitterDistance = 409;
            this.splitContainer1.TabIndex = 195;
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.dvgParsedResults);
            this.splitContainer2.Panel1.Controls.Add(this.panel2);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.splitContainer3);
            this.splitContainer2.Size = new System.Drawing.Size(1041, 409);
            this.splitContainer2.SplitterDistance = 687;
            this.splitContainer2.TabIndex = 0;
            // 
            // dvgParsedResults
            // 
            this.dvgParsedResults.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgParsedResults.BackgroundColor = System.Drawing.Color.Black;
            this.dvgParsedResults.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgParsedResults.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvgParsedResults.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgParsedResults.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgParsedResults.DefaultCellStyle = dataGridViewCellStyle2;
            this.dvgParsedResults.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgParsedResults.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dvgParsedResults.Location = new System.Drawing.Point(0, 37);
            this.dvgParsedResults.MultiSelect = false;
            this.dvgParsedResults.Name = "dvgParsedResults";
            this.dvgParsedResults.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgParsedResults.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dvgParsedResults.RowHeadersWidth = 5;
            this.dvgParsedResults.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgParsedResults.Size = new System.Drawing.Size(687, 372);
            this.dvgParsedResults.TabIndex = 14;
            this.dvgParsedResults.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.dvgParsedResults_CellFormatting);
            this.dvgParsedResults.ColumnHeaderMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dvgParsedResults_ColumnHeaderMouseClick);
            this.dvgParsedResults.SelectionChanged += new System.EventHandler(this.dvgParsedResults_SelectionChanged);
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.PowderBlue;
            this.panel2.Controls.Add(this.pnLeftTopRight);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(687, 37);
            this.panel2.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 21);
            this.label1.TabIndex = 2;
            this.label1.Text = "Parsed Segments";
            // 
            // splitContainer3
            // 
            this.splitContainer3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer3.Location = new System.Drawing.Point(0, 0);
            this.splitContainer3.Name = "splitContainer3";
            this.splitContainer3.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer3.Panel1
            // 
            this.splitContainer3.Panel1.Controls.Add(this.ucResultsDicItemsFilter1);
            // 
            // splitContainer3.Panel2
            // 
            this.splitContainer3.Panel2.Controls.Add(this.ucResultsNotes1);
            this.splitContainer3.Size = new System.Drawing.Size(350, 409);
            this.splitContainer3.SplitterDistance = 225;
            this.splitContainer3.TabIndex = 0;
            // 
            // ucResultsDicItemsFilter1
            // 
            this.ucResultsDicItemsFilter1.BackColor = System.Drawing.Color.Black;
            this.ucResultsDicItemsFilter1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucResultsDicItemsFilter1.Location = new System.Drawing.Point(0, 0);
            this.ucResultsDicItemsFilter1.Name = "ucResultsDicItemsFilter1";
            this.ucResultsDicItemsFilter1.Size = new System.Drawing.Size(350, 225);
            this.ucResultsDicItemsFilter1.TabIndex = 0;
            this.ucResultsDicItemsFilter1.FilterCompleted += new ProfessionalDocAnalyzer.ucResultsDicItemsFilter.ProcessHandler(this.ucResultsDicItemsFilter1_FilterCompleted);
            this.ucResultsDicItemsFilter1.ShowAll += new ProfessionalDocAnalyzer.ucResultsDicItemsFilter.ProcessHandler(this.ucResultsDicItemsFilter1_ShowAll);
            // 
            // ucResultsNotes1
            // 
            this.ucResultsNotes1.BackColor = System.Drawing.Color.Black;
            this.ucResultsNotes1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucResultsNotes1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucResultsNotes1.Location = new System.Drawing.Point(0, 0);
            this.ucResultsNotes1.Name = "ucResultsNotes1";
            this.ucResultsNotes1.Prefix = "";
            this.ucResultsNotes1.Size = new System.Drawing.Size(350, 180);
            this.ucResultsNotes1.TabIndex = 0;
            this.ucResultsNotes1.UID = "";
            // 
            // richTextBox1
            // 
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.Location = new System.Drawing.Point(0, 35);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(1041, 139);
            this.richTextBox1.TabIndex = 3;
            this.richTextBox1.Text = "";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.PowderBlue;
            this.panel1.Controls.Add(this.butSpeacker);
            this.panel1.Controls.Add(this.lblParsedSec);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1041, 35);
            this.panel1.TabIndex = 2;
            // 
            // butSpeacker
            // 
            this.butSpeacker.BackColor = System.Drawing.Color.Black;
            this.butSpeacker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butSpeacker.Image = ((System.Drawing.Image)(resources.GetObject("butSpeacker.Image")));
            this.butSpeacker.Location = new System.Drawing.Point(143, 4);
            this.butSpeacker.Name = "butSpeacker";
            this.butSpeacker.Size = new System.Drawing.Size(28, 28);
            this.butSpeacker.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butSpeacker.TabIndex = 18;
            this.butSpeacker.TabStop = false;
            this.butSpeacker.Visible = false;
            // 
            // lblParsedSec
            // 
            this.lblParsedSec.AutoSize = true;
            this.lblParsedSec.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParsedSec.ForeColor = System.Drawing.Color.Black;
            this.lblParsedSec.Location = new System.Drawing.Point(3, 7);
            this.lblParsedSec.Name = "lblParsedSec";
            this.lblParsedSec.Size = new System.Drawing.Size(134, 21);
            this.lblParsedSec.TabIndex = 2;
            this.lblParsedSec.Text = "Selected Segment";
            // 
            // pnLeftTopRight
            // 
            this.pnLeftTopRight.Controls.Add(this.butGenerateReport);
            this.pnLeftTopRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.pnLeftTopRight.Location = new System.Drawing.Point(528, 0);
            this.pnLeftTopRight.Name = "pnLeftTopRight";
            this.pnLeftTopRight.Size = new System.Drawing.Size(159, 37);
            this.pnLeftTopRight.TabIndex = 134;
            // 
            // butGenerateReport
            // 
            this.butGenerateReport.Location = new System.Drawing.Point(25, 8);
            this.butGenerateReport.Name = "butGenerateReport";
            this.butGenerateReport.Size = new System.Drawing.Size(124, 23);
            this.butGenerateReport.TabIndex = 134;
            this.butGenerateReport.Text = "Generate Report";
            this.butGenerateReport.UseSelectable = true;
            this.butGenerateReport.Click += new System.EventHandler(this.butGenerateReport_Click);
            // 
            // ucResultsDic
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panHeader);
            this.Name = "ucResultsDic";
            this.Size = new System.Drawing.Size(1041, 624);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picDic)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
            this.splitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dvgParsedResults)).EndInit();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.splitContainer3.Panel1.ResumeLayout(false);
            this.splitContainer3.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer3)).EndInit();
            this.splitContainer3.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butSpeacker)).EndInit();
            this.pnLeftTopRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.DataGridView dvgParsedResults;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer3;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox butSpeacker;
        private System.Windows.Forms.Label lblParsedSec;
        private ucResultsDicItemsFilter ucResultsDicItemsFilter1;
        private ucResultsNotes ucResultsNotes1;
        private System.Windows.Forms.PictureBox picDic;
        private System.Windows.Forms.Panel pnLeftTopRight;
        private MetroFramework.Controls.MetroButton butGenerateReport;
    }
}
