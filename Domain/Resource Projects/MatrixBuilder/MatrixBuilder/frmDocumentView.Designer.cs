﻿namespace MatrixBuilder
{
    partial class frmDocumentView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocumentView));
            IRTB.XMLViewerSettings xmlViewerSettings2 = new IRTB.XMLViewerSettings();
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblHeader = new System.Windows.Forms.Label();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.panLeft = new System.Windows.Forms.Panel();
            this.BMLinesTool = new System.Windows.Forms.ListBox();
            this.panLeftLeft = new System.Windows.Forms.Panel();
            this.LeftTop = new System.Windows.Forms.Panel();
            this.lblBookmarks = new System.Windows.Forms.Label();
            this.Irtb1 = new IRTB.IRTB();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.panLeft.SuspendLayout();
            this.LeftTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Controls.Add(this.picHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(615, 33);
            this.panHeader.TabIndex = 3;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(40, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(134, 21);
            this.lblHeader.TabIndex = 67;
            this.lblHeader.Text = "Document Viewer";
            // 
            // picHeader
            // 
            this.picHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHeader.Image = ((System.Drawing.Image)(resources.GetObject("picHeader.Image")));
            this.picHeader.Location = new System.Drawing.Point(12, 3);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(28, 28);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 66;
            this.picHeader.TabStop = false;
            // 
            // panLeft
            // 
            this.panLeft.BackColor = System.Drawing.Color.Black;
            this.panLeft.Controls.Add(this.BMLinesTool);
            this.panLeft.Controls.Add(this.panLeftLeft);
            this.panLeft.Controls.Add(this.LeftTop);
            this.panLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panLeft.Location = new System.Drawing.Point(0, 33);
            this.panLeft.Name = "panLeft";
            this.panLeft.Size = new System.Drawing.Size(114, 489);
            this.panLeft.TabIndex = 4;
            // 
            // BMLinesTool
            // 
            this.BMLinesTool.BackColor = System.Drawing.Color.Black;
            this.BMLinesTool.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.BMLinesTool.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BMLinesTool.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BMLinesTool.ForeColor = System.Drawing.Color.White;
            this.BMLinesTool.FormattingEnabled = true;
            this.BMLinesTool.ItemHeight = 17;
            this.BMLinesTool.Location = new System.Drawing.Point(28, 35);
            this.BMLinesTool.Name = "BMLinesTool";
            this.BMLinesTool.Size = new System.Drawing.Size(86, 454);
            this.BMLinesTool.TabIndex = 3;
            this.BMLinesTool.SelectedIndexChanged += new System.EventHandler(this.BMLinesTool_SelectedIndexChanged);
            // 
            // panLeftLeft
            // 
            this.panLeftLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.panLeftLeft.Location = new System.Drawing.Point(0, 35);
            this.panLeftLeft.Name = "panLeftLeft";
            this.panLeftLeft.Size = new System.Drawing.Size(28, 454);
            this.panLeftLeft.TabIndex = 1;
            // 
            // LeftTop
            // 
            this.LeftTop.Controls.Add(this.lblBookmarks);
            this.LeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.LeftTop.Location = new System.Drawing.Point(0, 0);
            this.LeftTop.Name = "LeftTop";
            this.LeftTop.Size = new System.Drawing.Size(114, 35);
            this.LeftTop.TabIndex = 0;
            // 
            // lblBookmarks
            // 
            this.lblBookmarks.AutoSize = true;
            this.lblBookmarks.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBookmarks.ForeColor = System.Drawing.Color.White;
            this.lblBookmarks.Location = new System.Drawing.Point(10, 9);
            this.lblBookmarks.Name = "lblBookmarks";
            this.lblBookmarks.Size = new System.Drawing.Size(82, 20);
            this.lblBookmarks.TabIndex = 68;
            this.lblBookmarks.Text = "Bookmarks";
            // 
            // Irtb1
            // 
            this.Irtb1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Irtb1.IRTBAlignNumbers = IRTB.IRTB.IRTBAlignPos.Left;
            this.Irtb1.IRTBEnableHighLight = true;
            this.Irtb1.IRTBEnableNumbering = true;
            this.Irtb1.IRTBFileToLoad = "";
            this.Irtb1.IRTBFont = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Irtb1.IRTBHighLightColor = System.Drawing.Color.Red;
            this.Irtb1.IRTBLNFontColor = System.Drawing.Color.DarkBlue;
            this.Irtb1.IRTBPrefix = false;
            xmlViewerSettings2.AttributeKey = System.Drawing.Color.Red;
            xmlViewerSettings2.AttributeValue = System.Drawing.Color.Blue;
            xmlViewerSettings2.Element = System.Drawing.Color.DarkRed;
            xmlViewerSettings2.Tag = System.Drawing.Color.Blue;
            xmlViewerSettings2.Value = System.Drawing.Color.Black;
            this.Irtb1.IRTBXMLSettings = xmlViewerSettings2;
            this.Irtb1.Location = new System.Drawing.Point(114, 33);
            this.Irtb1.Name = "Irtb1";
            this.Irtb1.Size = new System.Drawing.Size(501, 489);
            this.Irtb1.TabIndex = 5;
            this.Irtb1.BMLInformation += new IRTB.IRTB.BMLInformationEventHandler(this.Irtb1_BMLInformation);
            // 
            // frmDocumentView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(625, 532);
            this.Controls.Add(this.Irtb1);
            this.Controls.Add(this.panLeft);
            this.Controls.Add(this.panHeader);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "frmDocumentView";
            this.Padding = new System.Windows.Forms.Padding(0, 0, 10, 10);
            this.Text = "Document View";
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.panLeft.ResumeLayout(false);
            this.LeftTop.ResumeLayout(false);
            this.LeftTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.Panel panLeft;
        internal System.Windows.Forms.ListBox BMLinesTool;
        private System.Windows.Forms.Panel panLeftLeft;
        private System.Windows.Forms.Panel LeftTop;
        private System.Windows.Forms.Label lblBookmarks;
        internal IRTB.IRTB Irtb1;
    }
}