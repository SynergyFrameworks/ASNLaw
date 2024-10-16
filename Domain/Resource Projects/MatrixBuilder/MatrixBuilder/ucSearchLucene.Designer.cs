namespace MatrixBuilder
{
    partial class ucSearchLucene
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucSearchLucene));
            this.butSearch = new System.Windows.Forms.PictureBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.txtbSearch = new System.Windows.Forms.TextBox();
            this.lblFound = new System.Windows.Forms.Label();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.butSearch)).BeginInit();
            this.SuspendLayout();
            // 
            // butSearch
            // 
            this.butSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butSearch.Image = ((System.Drawing.Image)(resources.GetObject("butSearch.Image")));
            this.butSearch.Location = new System.Drawing.Point(249, 3);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(28, 28);
            this.butSearch.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butSearch.TabIndex = 193;
            this.butSearch.TabStop = false;
            this.metroToolTip1.SetToolTip(this.butSearch, "Search and Filter");
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSearch.ForeColor = System.Drawing.Color.White;
            this.lblSearch.Location = new System.Drawing.Point(4, 8);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(47, 17);
            this.lblSearch.TabIndex = 191;
            this.lblSearch.Text = "Search";
            // 
            // txtbSearch
            // 
            this.txtbSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbSearch.Location = new System.Drawing.Point(62, 9);
            this.txtbSearch.Name = "txtbSearch";
            this.txtbSearch.Size = new System.Drawing.Size(181, 18);
            this.txtbSearch.TabIndex = 192;
            // 
            // lblFound
            // 
            this.lblFound.AutoSize = true;
            this.lblFound.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFound.ForeColor = System.Drawing.Color.White;
            this.lblFound.Location = new System.Drawing.Point(291, 12);
            this.lblFound.Name = "lblFound";
            this.lblFound.Size = new System.Drawing.Size(0, 17);
            this.lblFound.TabIndex = 198;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ucSearchLucene
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.lblFound);
            this.Controls.Add(this.butSearch);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtbSearch);
            this.Name = "ucSearchLucene";
            this.Size = new System.Drawing.Size(423, 35);
            ((System.ComponentModel.ISupportInitialize)(this.butSearch)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox butSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.TextBox txtbSearch;
        private System.Windows.Forms.Label lblFound;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}
