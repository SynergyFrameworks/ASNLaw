namespace MatrixBuilder
{
    partial class ucMatrixRefRes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMatrixRefRes));
            this.panHeader = new System.Windows.Forms.Panel();
            this.picLists = new System.Windows.Forms.PictureBox();
            this.lblCaptionDescription = new System.Windows.Forms.Label();
            this.lblCaption = new System.Windows.Forms.Label();
            this.splitContMain = new System.Windows.Forms.SplitContainer();
            this.lstbRefRes = new System.Windows.Forms.ListBox();
            this.panLeftBottom = new System.Windows.Forms.Panel();
            this.lblColumn = new System.Windows.Forms.Label();
            this.panLeftTop = new System.Windows.Forms.Panel();
            this.lblSelRefRes = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstbRefResItems = new System.Windows.Forms.ListBox();
            this.txtbRefResContentText = new System.Windows.Forms.TextBox();
            this.lblRefResContentText = new System.Windows.Forms.Label();
            this.panRightTop = new System.Windows.Forms.Panel();
            this.lblRefResItems = new System.Windows.Forms.Label();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLists)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).BeginInit();
            this.splitContMain.Panel1.SuspendLayout();
            this.splitContMain.Panel2.SuspendLayout();
            this.splitContMain.SuspendLayout();
            this.panLeftBottom.SuspendLayout();
            this.panLeftTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panRightTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.Controls.Add(this.picLists);
            this.panHeader.Controls.Add(this.lblCaptionDescription);
            this.panHeader.Controls.Add(this.lblCaption);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(795, 48);
            this.panHeader.TabIndex = 38;
            // 
            // picLists
            // 
            this.picLists.Image = ((System.Drawing.Image)(resources.GetObject("picLists.Image")));
            this.picLists.Location = new System.Drawing.Point(8, 4);
            this.picLists.Name = "picLists";
            this.picLists.Size = new System.Drawing.Size(38, 38);
            this.picLists.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLists.TabIndex = 190;
            this.picLists.TabStop = false;
            // 
            // lblCaptionDescription
            // 
            this.lblCaptionDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaptionDescription.ForeColor = System.Drawing.Color.White;
            this.lblCaptionDescription.Location = new System.Drawing.Point(237, 13);
            this.lblCaptionDescription.Name = "lblCaptionDescription";
            this.lblCaptionDescription.Size = new System.Drawing.Size(555, 25);
            this.lblCaptionDescription.TabIndex = 189;
            this.lblCaptionDescription.Text = "    (e.g. Win Themes && Discriminators) ";
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.Color.White;
            this.lblCaption.Location = new System.Drawing.Point(47, 9);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(184, 25);
            this.lblCaption.TabIndex = 34;
            this.lblCaption.Text = "Reference Resources";
            // 
            // splitContMain
            // 
            this.splitContMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContMain.Location = new System.Drawing.Point(0, 48);
            this.splitContMain.Name = "splitContMain";
            // 
            // splitContMain.Panel1
            // 
            this.splitContMain.Panel1.Controls.Add(this.lstbRefRes);
            this.splitContMain.Panel1.Controls.Add(this.panLeftBottom);
            this.splitContMain.Panel1.Controls.Add(this.panLeftTop);
            // 
            // splitContMain.Panel2
            // 
            this.splitContMain.Panel2.Controls.Add(this.splitContainer1);
            this.splitContMain.Panel2.Controls.Add(this.panRightTop);
            this.splitContMain.Size = new System.Drawing.Size(795, 287);
            this.splitContMain.SplitterDistance = 265;
            this.splitContMain.TabIndex = 39;
            // 
            // lstbRefRes
            // 
            this.lstbRefRes.BackColor = System.Drawing.Color.Black;
            this.lstbRefRes.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbRefRes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbRefRes.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbRefRes.ForeColor = System.Drawing.Color.White;
            this.lstbRefRes.FormattingEnabled = true;
            this.lstbRefRes.ItemHeight = 20;
            this.lstbRefRes.Location = new System.Drawing.Point(0, 35);
            this.lstbRefRes.Name = "lstbRefRes";
            this.lstbRefRes.Size = new System.Drawing.Size(265, 216);
            this.lstbRefRes.TabIndex = 4;
            this.lstbRefRes.SelectedIndexChanged += new System.EventHandler(this.lstbRefRes_SelectedIndexChanged);
            // 
            // panLeftBottom
            // 
            this.panLeftBottom.Controls.Add(this.lblColumn);
            this.panLeftBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panLeftBottom.Location = new System.Drawing.Point(0, 251);
            this.panLeftBottom.Name = "panLeftBottom";
            this.panLeftBottom.Size = new System.Drawing.Size(265, 36);
            this.panLeftBottom.TabIndex = 3;
            // 
            // lblColumn
            // 
            this.lblColumn.AutoSize = true;
            this.lblColumn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumn.ForeColor = System.Drawing.Color.White;
            this.lblColumn.Location = new System.Drawing.Point(10, 8);
            this.lblColumn.Name = "lblColumn";
            this.lblColumn.Size = new System.Drawing.Size(55, 17);
            this.lblColumn.TabIndex = 188;
            this.lblColumn.Text = "Column:";
            // 
            // panLeftTop
            // 
            this.panLeftTop.Controls.Add(this.lblSelRefRes);
            this.panLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panLeftTop.Location = new System.Drawing.Point(0, 0);
            this.panLeftTop.Name = "panLeftTop";
            this.panLeftTop.Size = new System.Drawing.Size(265, 35);
            this.panLeftTop.TabIndex = 1;
            // 
            // lblSelRefRes
            // 
            this.lblSelRefRes.AutoSize = true;
            this.lblSelRefRes.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelRefRes.ForeColor = System.Drawing.Color.White;
            this.lblSelRefRes.Location = new System.Drawing.Point(9, 8);
            this.lblSelRefRes.Name = "lblSelRefRes";
            this.lblSelRefRes.Size = new System.Drawing.Size(195, 20);
            this.lblSelRefRes.TabIndex = 188;
            this.lblSelRefRes.Text = "Select a Reference Resource";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 35);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.lstbRefResItems);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtbRefResContentText);
            this.splitContainer1.Panel2.Controls.Add(this.lblRefResContentText);
            this.splitContainer1.Size = new System.Drawing.Size(526, 252);
            this.splitContainer1.SplitterDistance = 133;
            this.splitContainer1.TabIndex = 3;
            // 
            // lstbRefResItems
            // 
            this.lstbRefResItems.BackColor = System.Drawing.Color.Black;
            this.lstbRefResItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbRefResItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbRefResItems.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbRefResItems.ForeColor = System.Drawing.Color.White;
            this.lstbRefResItems.FormattingEnabled = true;
            this.lstbRefResItems.ItemHeight = 17;
            this.lstbRefResItems.Location = new System.Drawing.Point(0, 0);
            this.lstbRefResItems.Name = "lstbRefResItems";
            this.lstbRefResItems.Size = new System.Drawing.Size(526, 133);
            this.lstbRefResItems.TabIndex = 6;
            this.lstbRefResItems.Click += new System.EventHandler(this.lstbRefResItems_Click);
            this.lstbRefResItems.SelectedIndexChanged += new System.EventHandler(this.lstbRefResItems_SelectedIndexChanged);
            this.lstbRefResItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstbRefResItems_MouseDown);
            // 
            // txtbRefResContentText
            // 
            this.txtbRefResContentText.BackColor = System.Drawing.Color.Black;
            this.txtbRefResContentText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbRefResContentText.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbRefResContentText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbRefResContentText.ForeColor = System.Drawing.Color.White;
            this.txtbRefResContentText.Location = new System.Drawing.Point(0, 0);
            this.txtbRefResContentText.Multiline = true;
            this.txtbRefResContentText.Name = "txtbRefResContentText";
            this.txtbRefResContentText.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbRefResContentText.Size = new System.Drawing.Size(526, 115);
            this.txtbRefResContentText.TabIndex = 1;
            this.txtbRefResContentText.TextChanged += new System.EventHandler(this.txtbRefResContentText_TextChanged);
            // 
            // lblRefResContentText
            // 
            this.lblRefResContentText.AutoSize = true;
            this.lblRefResContentText.Location = new System.Drawing.Point(37, 36);
            this.lblRefResContentText.Name = "lblRefResContentText";
            this.lblRefResContentText.Size = new System.Drawing.Size(35, 13);
            this.lblRefResContentText.TabIndex = 0;
            this.lblRefResContentText.Text = "label1";
            this.lblRefResContentText.Visible = false;
            this.lblRefResContentText.TextChanged += new System.EventHandler(this.lblRefResContentText_TextChanged);
            // 
            // panRightTop
            // 
            this.panRightTop.Controls.Add(this.lblRefResItems);
            this.panRightTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panRightTop.Location = new System.Drawing.Point(0, 0);
            this.panRightTop.Name = "panRightTop";
            this.panRightTop.Size = new System.Drawing.Size(526, 35);
            this.panRightTop.TabIndex = 2;
            // 
            // lblRefResItems
            // 
            this.lblRefResItems.AutoSize = true;
            this.lblRefResItems.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRefResItems.ForeColor = System.Drawing.Color.White;
            this.lblRefResItems.Location = new System.Drawing.Point(9, 8);
            this.lblRefResItems.Name = "lblRefResItems";
            this.lblRefResItems.Size = new System.Drawing.Size(179, 20);
            this.lblRefResItems.TabIndex = 188;
            this.lblRefResItems.Text = "Reference Resource Items";
            // 
            // ucMatrixRefRes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.splitContMain);
            this.Controls.Add(this.panHeader);
            this.Name = "ucMatrixRefRes";
            this.Size = new System.Drawing.Size(795, 335);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLists)).EndInit();
            this.splitContMain.Panel1.ResumeLayout(false);
            this.splitContMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).EndInit();
            this.splitContMain.ResumeLayout(false);
            this.panLeftBottom.ResumeLayout(false);
            this.panLeftBottom.PerformLayout();
            this.panLeftTop.ResumeLayout(false);
            this.panLeftTop.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panRightTop.ResumeLayout(false);
            this.panRightTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.PictureBox picLists;
        private System.Windows.Forms.Label lblCaptionDescription;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.SplitContainer splitContMain;
        private System.Windows.Forms.ListBox lstbRefRes;
        private System.Windows.Forms.Panel panLeftBottom;
        private System.Windows.Forms.Label lblColumn;
        private System.Windows.Forms.Panel panLeftTop;
        private System.Windows.Forms.Label lblSelRefRes;
        private System.Windows.Forms.Panel panRightTop;
        private System.Windows.Forms.Label lblRefResItems;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstbRefResItems;
        private System.Windows.Forms.TextBox txtbRefResContentText;
        private System.Windows.Forms.Label lblRefResContentText;

    }
}
