namespace ProfessionalDocAnalyzer
{
    partial class frmDocumentEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentEdit));
            this.picDictionaries = new System.Windows.Forms.PictureBox();
            this.panFooter = new System.Windows.Forms.Panel();
            this.panFooterRight = new System.Windows.Forms.Panel();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.richerTextBox1 = new RicherTextBox.RicherTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDictionaries)).BeginInit();
            this.panFooter.SuspendLayout();
            this.panFooterRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // picDictionaries
            // 
            this.picDictionaries.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDictionaries.Image = ((System.Drawing.Image)(resources.GetObject("picDictionaries.Image")));
            this.picDictionaries.Location = new System.Drawing.Point(23, 17);
            this.picDictionaries.Name = "picDictionaries";
            this.picDictionaries.Size = new System.Drawing.Size(38, 38);
            this.picDictionaries.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDictionaries.TabIndex = 16;
            this.picDictionaries.TabStop = false;
            // 
            // panFooter
            // 
            this.panFooter.Controls.Add(this.panFooterRight);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(10, 417);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(917, 43);
            this.panFooter.TabIndex = 17;
            // 
            // panFooterRight
            // 
            this.panFooterRight.Controls.Add(this.butCancel);
            this.panFooterRight.Controls.Add(this.butOK);
            this.panFooterRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panFooterRight.Location = new System.Drawing.Point(717, 0);
            this.panFooterRight.Name = "panFooterRight";
            this.panFooterRight.Size = new System.Drawing.Size(200, 43);
            this.panFooterRight.TabIndex = 15;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(121, 11);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 16;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(31, 11);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 15;
            this.butOK.Text = "Save";
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // richerTextBox1
            // 
            this.richerTextBox1.AlignCenterVisible = false;
            this.richerTextBox1.AlignLeftVisible = false;
            this.richerTextBox1.AlignRightVisible = false;
            this.richerTextBox1.BoldVisible = false;
            this.richerTextBox1.BulletsVisible = false;
            this.richerTextBox1.ChooseFontVisible = true;
            this.richerTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richerTextBox1.FindReplaceVisible = true;
            this.richerTextBox1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.richerTextBox1.FontColorVisible = false;
            this.richerTextBox1.FontFamilyVisible = true;
            this.richerTextBox1.FontSizeVisible = true;
            this.richerTextBox1.GroupAlignmentVisible = false;
            this.richerTextBox1.GroupBoldUnderlineItalicVisible = false;
            this.richerTextBox1.GroupFontColorVisible = false;
            this.richerTextBox1.GroupFontNameAndSizeVisible = true;
            this.richerTextBox1.GroupIndentationAndBulletsVisible = false;
            this.richerTextBox1.GroupInsertVisible = false;
            this.richerTextBox1.GroupSaveAndLoadVisible = false;
            this.richerTextBox1.GroupZoomVisible = true;
            this.richerTextBox1.INDENT = 10;
            this.richerTextBox1.IndentVisible = false;
            this.richerTextBox1.InsertPictureVisible = false;
            this.richerTextBox1.ItalicVisible = false;
            this.richerTextBox1.LoadedFile = "";
            this.richerTextBox1.LoadVisible = false;
            this.richerTextBox1.Location = new System.Drawing.Point(10, 60);
            this.richerTextBox1.Name = "richerTextBox1";
            this.richerTextBox1.OutdentVisible = false;
            this.richerTextBox1.ReplaceVisible = true;
            this.richerTextBox1.Rtf = "{\\rtf1\\ansi\\ansicpg1251\\deff0{\\fonttbl{\\f0\\fnil\\fcharset204 Microsoft Sans Serif;" +
    "}}\r\n\\viewkind4\\uc1\\pard\\lang1033\\f0\\fs18 richerTextBox1\\par\r\n}\r\n";
            this.richerTextBox1.SaveVisible = false;
            this.richerTextBox1.SeparatorAlignVisible = false;
            this.richerTextBox1.SeparatorBoldUnderlineItalicVisible = false;
            this.richerTextBox1.SeparatorFontColorVisible = false;
            this.richerTextBox1.SeparatorFontVisible = true;
            this.richerTextBox1.SeparatorIndentAndBulletsVisible = false;
            this.richerTextBox1.SeparatorInsertVisible = false;
            this.richerTextBox1.SeparatorSaveLoadVisible = false;
            this.richerTextBox1.Size = new System.Drawing.Size(917, 357);
            this.richerTextBox1.TabIndex = 18;
            this.richerTextBox1.ToolStripVisible = true;
            this.richerTextBox1.UnderlineVisible = false;
            this.richerTextBox1.WordWrapVisible = false;
            this.richerTextBox1.ZoomFactorTextVisible = true;
            this.richerTextBox1.ZoomInVisible = true;
            this.richerTextBox1.ZoomOutVisible = true;
            // 
            // frmDocumentEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(937, 470);
            this.Controls.Add(this.richerTextBox1);
            this.Controls.Add(this.panFooter);
            this.Controls.Add(this.picDictionaries);
            this.Name = "frmDocumentEdit";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Text = "       Edit Document";
            this.Load += new System.EventHandler(this.frmDocumentEdit_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picDictionaries)).EndInit();
            this.panFooter.ResumeLayout(false);
            this.panFooterRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox picDictionaries;
        private System.Windows.Forms.Panel panFooter;
        private RicherTextBox.RicherTextBox richerTextBox1;
        private System.Windows.Forms.Panel panFooterRight;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOK;
    }
}