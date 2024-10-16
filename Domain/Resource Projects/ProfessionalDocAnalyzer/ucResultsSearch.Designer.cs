namespace ConceptAnalyzer
{
    partial class ucResultsSearch
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucResultsSearch));
            this.butSearch = new MetroFramework.Controls.MetroButton();
            this.txtbSearch = new System.Windows.Forms.TextBox();
            this.butRefreshFind = new MetroFramework.Controls.MetroButton();
            this.picInfo_Search = new System.Windows.Forms.PictureBox();
            this.htmlToolTip1 = new MetroFramework.Drawing.Html.HtmlToolTip();
            this.lblInformation = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picInfo_Search)).BeginInit();
            this.SuspendLayout();
            // 
            // butSearch
            // 
            this.butSearch.Location = new System.Drawing.Point(216, 7);
            this.butSearch.Name = "butSearch";
            this.butSearch.Size = new System.Drawing.Size(75, 23);
            this.butSearch.TabIndex = 207;
            this.butSearch.Text = "Search";
            this.butSearch.UseSelectable = true;
            this.butSearch.Click += new System.EventHandler(this.butSearch_Click);
            // 
            // txtbSearch
            // 
            this.txtbSearch.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbSearch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbSearch.Location = new System.Drawing.Point(29, 10);
            this.txtbSearch.Name = "txtbSearch";
            this.txtbSearch.Size = new System.Drawing.Size(181, 18);
            this.txtbSearch.TabIndex = 206;
            // 
            // butRefreshFind
            // 
            this.butRefreshFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butRefreshFind.BackgroundImage")));
            this.butRefreshFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butRefreshFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butRefreshFind.ForeColor = System.Drawing.Color.White;
            this.butRefreshFind.Location = new System.Drawing.Point(306, 5);
            this.butRefreshFind.Name = "butRefreshFind";
            this.butRefreshFind.Size = new System.Drawing.Size(28, 28);
            this.butRefreshFind.TabIndex = 205;
            this.butRefreshFind.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butRefreshFind.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butRefreshFind.UseSelectable = true;
            this.butRefreshFind.Visible = false;
            this.butRefreshFind.Click += new System.EventHandler(this.butRefreshFind_Click);
            // 
            // picInfo_Search
            // 
            this.picInfo_Search.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picInfo_Search.Image = ((System.Drawing.Image)(resources.GetObject("picInfo_Search.Image")));
            this.picInfo_Search.Location = new System.Drawing.Point(3, 8);
            this.picInfo_Search.Name = "picInfo_Search";
            this.picInfo_Search.Size = new System.Drawing.Size(20, 20);
            this.picInfo_Search.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picInfo_Search.TabIndex = 208;
            this.picInfo_Search.TabStop = false;
            this.htmlToolTip1.SetToolTip(this.picInfo_Search, resources.GetString("picInfo_Search.ToolTip"));
            this.picInfo_Search.Click += new System.EventHandler(this.picInfo_Search_Click);
            // 
            // htmlToolTip1
            // 
            this.htmlToolTip1.OwnerDraw = true;
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.BackColor = System.Drawing.Color.Transparent;
            this.lblInformation.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.ForeColor = System.Drawing.Color.White;
            this.lblInformation.Location = new System.Drawing.Point(340, 10);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new System.Drawing.Size(0, 17);
            this.lblInformation.TabIndex = 209;
            this.lblInformation.Visible = false;
            // 
            // ucResultsSearch
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.lblInformation);
            this.Controls.Add(this.picInfo_Search);
            this.Controls.Add(this.butSearch);
            this.Controls.Add(this.txtbSearch);
            this.Controls.Add(this.butRefreshFind);
            this.Name = "ucResultsSearch";
            this.Size = new System.Drawing.Size(657, 36);
            ((System.ComponentModel.ISupportInitialize)(this.picInfo_Search)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton butSearch;
        private System.Windows.Forms.TextBox txtbSearch;
        private MetroFramework.Controls.MetroButton butRefreshFind;
        private System.Windows.Forms.PictureBox picInfo_Search;
        private MetroFramework.Drawing.Html.HtmlToolTip htmlToolTip1;
        private System.Windows.Forms.Label lblInformation;
    }
}
