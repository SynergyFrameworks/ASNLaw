namespace MatrixBuilder
{
    partial class frmMartixCellCont
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMartixCellCont));
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panBottom = new System.Windows.Forms.Panel();
            this.lblContent = new System.Windows.Forms.Label();
            this.lblFoundQty = new System.Windows.Forms.Label();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtbSearch = new System.Windows.Forms.TextBox();
            this.butSearch = new System.Windows.Forms.PictureBox();
            this.panFooterRight = new System.Windows.Forms.Panel();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.rtb = new System.Windows.Forms.RichTextBox();
            this.lblTitle = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panBottom.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butSearch)).BeginInit();
            this.panFooterRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 188;
            this.pictureBox2.TabStop = false;
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.lblContent);
            this.panBottom.Controls.Add(this.lblFoundQty);
            this.panBottom.Controls.Add(this.lblSearch);
            this.panBottom.Controls.Add(this.txtbSearch);
            this.panBottom.Controls.Add(this.butSearch);
            this.panBottom.Controls.Add(this.panFooterRight);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(20, 383);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(484, 47);
            this.panBottom.TabIndex = 189;
            // 
            // lblContent
            // 
            this.lblContent.AutoSize = true;
            this.lblContent.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContent.ForeColor = System.Drawing.Color.White;
            this.lblContent.Location = new System.Drawing.Point(342, 15);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(47, 17);
            this.lblContent.TabIndex = 200;
            this.lblContent.Text = "Search";
            this.lblContent.Visible = false;
            // 
            // lblFoundQty
            // 
            this.lblFoundQty.AutoSize = true;
            this.lblFoundQty.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFoundQty.ForeColor = System.Drawing.Color.White;
            this.lblFoundQty.Location = new System.Drawing.Point(288, 21);
            this.lblFoundQty.Name = "lblFoundQty";
            this.lblFoundQty.Size = new System.Drawing.Size(0, 17);
            this.lblFoundQty.TabIndex = 199;
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(9, 19);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(47, 17);
            this.lblSearch.TabIndex = 197;
            this.lblSearch.Text = "Search";
            // 
            // txtbSearch
            // 
            this.txtbSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbSearch.Location = new System.Drawing.Point(67, 20);
            this.txtbSearch.Name = "txtbSearch";
            this.txtbSearch.Size = new System.Drawing.Size(181, 18);
            this.txtbSearch.TabIndex = 198;
            // 
            // butSearch
            // 
            this.butSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butSearch.Image = ((System.Drawing.Image)(resources.GetObject("butSearch.Image")));
            this.butSearch.Location = new System.Drawing.Point(254, 15);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(28, 28);
            this.butSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butSearch.TabIndex = 196;
            this.butSearch.TabStop = false;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // panFooterRight
            // 
            this.panFooterRight.Controls.Add(this.butCancel);
            this.panFooterRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panFooterRight.Location = new System.Drawing.Point(395, 0);
            this.panFooterRight.Name = "panFooterRight";
            this.panFooterRight.Size = new System.Drawing.Size(89, 47);
            this.panFooterRight.TabIndex = 2;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(7, 15);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 3;
            this.butCancel.Text = "Close";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // rtb
            // 
            this.rtb.BackColor = System.Drawing.Color.Black;
            this.rtb.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtb.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtb.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rtb.ForeColor = System.Drawing.Color.White;
            this.rtb.Location = new System.Drawing.Point(20, 60);
            this.rtb.Name = "rtb";
            this.rtb.Size = new System.Drawing.Size(484, 323);
            this.rtb.TabIndex = 190;
            this.rtb.Text = "";
            this.rtb.TextChanged += new System.EventHandler(this.rtb_TextChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Location = new System.Drawing.Point(66, 23);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(48, 25);
            this.lblTitle.TabIndex = 198;
            this.lblTitle.Text = "Title";
            // 
            // frmMartixCellCont
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(524, 450);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.rtb);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.pictureBox2);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.MinimizeBox = false;
            this.Name = "frmMartixCellCont";
            this.RightToLeftLayout = true;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panBottom.ResumeLayout(false);
            this.panBottom.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butSearch)).EndInit();
            this.panFooterRight.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Panel panFooterRight;
        private MetroFramework.Controls.MetroButton butCancel;
        private System.Windows.Forms.RichTextBox rtb;
        private System.Windows.Forms.PictureBox butSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtbSearch;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblFoundQty;
        private System.Windows.Forms.Label lblContent;
    }
}