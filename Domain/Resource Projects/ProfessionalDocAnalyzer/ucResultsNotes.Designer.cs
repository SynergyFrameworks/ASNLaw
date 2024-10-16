namespace ProfessionalDocAnalyzer
{
    partial class ucResultsNotes
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
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblHeaderCaption = new System.Windows.Forms.Label();
            this.richerTextBox1 = new RicherTextBox.RicherTextBox();
            this.panHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.PowderBlue;
            this.panHeader.Controls.Add(this.lblHeaderCaption);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(502, 31);
            this.panHeader.TabIndex = 16;
            // 
            // lblHeaderCaption
            // 
            this.lblHeaderCaption.AutoSize = true;
            this.lblHeaderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblHeaderCaption.Location = new System.Drawing.Point(4, 7);
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            this.lblHeaderCaption.Size = new System.Drawing.Size(51, 21);
            this.lblHeaderCaption.TabIndex = 1;
            this.lblHeaderCaption.Text = "Notes";
            // 
            // richerTextBox1
            // 
            this.richerTextBox1.AlignCenterVisible = true;
            this.richerTextBox1.AlignLeftVisible = true;
            this.richerTextBox1.AlignRightVisible = true;
            this.richerTextBox1.BoldVisible = true;
            this.richerTextBox1.BulletsVisible = false;
            this.richerTextBox1.ChooseFontVisible = true;
            this.richerTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richerTextBox1.FindReplaceVisible = true;
            this.richerTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richerTextBox1.FontColorVisible = true;
            this.richerTextBox1.FontFamilyVisible = true;
            this.richerTextBox1.FontSizeVisible = true;
            this.richerTextBox1.GroupAlignmentVisible = true;
            this.richerTextBox1.GroupBoldUnderlineItalicVisible = true;
            this.richerTextBox1.GroupFontColorVisible = true;
            this.richerTextBox1.GroupFontNameAndSizeVisible = true;
            this.richerTextBox1.GroupIndentationAndBulletsVisible = false;
            this.richerTextBox1.GroupInsertVisible = false;
            this.richerTextBox1.GroupSaveAndLoadVisible = false;
            this.richerTextBox1.GroupZoomVisible = false;
            this.richerTextBox1.INDENT = 10;
            this.richerTextBox1.IndentVisible = true;
            this.richerTextBox1.InsertPictureVisible = false;
            this.richerTextBox1.ItalicVisible = true;
            this.richerTextBox1.LoadedFile = "";
            this.richerTextBox1.LoadVisible = false;
            this.richerTextBox1.Location = new System.Drawing.Point(0, 31);
            this.richerTextBox1.Name = "richerTextBox1";
            this.richerTextBox1.OutdentVisible = true;
            this.richerTextBox1.ReplaceVisible = true;
            this.richerTextBox1.Rtf = "{\\rtf1\\ansi\\ansicpg1251\\deff0\\deflang1026{\\fonttbl{\\f0\\fnil\\fcharset204 Microsoft" +
    " Sans Serif;}}\r\n\\viewkind4\\uc1\\pard\\f0\\fs18\\par\r\n}\r\n";
            this.richerTextBox1.SaveVisible = false;
            this.richerTextBox1.SeparatorAlignVisible = true;
            this.richerTextBox1.SeparatorBoldUnderlineItalicVisible = true;
            this.richerTextBox1.SeparatorFontColorVisible = true;
            this.richerTextBox1.SeparatorFontVisible = true;
            this.richerTextBox1.SeparatorIndentAndBulletsVisible = false;
            this.richerTextBox1.SeparatorInsertVisible = false;
            this.richerTextBox1.SeparatorSaveLoadVisible = false;
            this.richerTextBox1.Size = new System.Drawing.Size(502, 223);
            this.richerTextBox1.TabIndex = 17;
            this.richerTextBox1.ToolStripVisible = true;
            this.richerTextBox1.UnderlineVisible = true;
            this.richerTextBox1.WordWrapVisible = true;
            this.richerTextBox1.ZoomFactorTextVisible = false;
            this.richerTextBox1.ZoomInVisible = false;
            this.richerTextBox1.ZoomOutVisible = false;
            this.richerTextBox1.Leave += new System.EventHandler(this.richerTextBox1_Leave);
            // 
            // ucResultsNotes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.richerTextBox1);
            this.Controls.Add(this.panHeader);
            this.Name = "ucResultsNotes";
            this.Size = new System.Drawing.Size(502, 254);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblHeaderCaption;
        private RicherTextBox.RicherTextBox richerTextBox1;
    }
}
