namespace ProfessionalDocAnalyzer
{
    partial class frmCategory
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCategory));
            this.lstbCategories = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbName = new System.Windows.Forms.TextBox();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.butNew = new MetroFramework.Controls.MetroButton();
            this.butReplace = new MetroFramework.Controls.MetroButton();
            this.picDictionaries = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.picDictionaries)).BeginInit();
            this.SuspendLayout();
            // 
            // lstbCategories
            // 
            this.lstbCategories.BackColor = System.Drawing.Color.White;
            this.lstbCategories.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbCategories.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbCategories.ForeColor = System.Drawing.Color.Black;
            this.lstbCategories.FormattingEnabled = true;
            this.lstbCategories.ItemHeight = 17;
            this.lstbCategories.Location = new System.Drawing.Point(14, 158);
            this.lstbCategories.Name = "lstbCategories";
            this.lstbCategories.Size = new System.Drawing.Size(859, 323);
            this.lstbCategories.TabIndex = 116;
            this.lstbCategories.SelectedIndexChanged += new System.EventHandler(this.lstbCategories_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(10, 67);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(69, 20);
            this.label2.TabIndex = 122;
            this.label2.Text = "Category";
            // 
            // txtbName
            // 
            this.txtbName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbName.Location = new System.Drawing.Point(14, 90);
            this.txtbName.MaxLength = 50;
            this.txtbName.Name = "txtbName";
            this.txtbName.Size = new System.Drawing.Size(852, 25);
            this.txtbName.TabIndex = 112;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(798, 499);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 118;
            this.butCancel.Text = "Cancel";
            this.metroToolTip1.SetToolTip(this.butCancel, "Cancel Changes");
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(707, 499);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 117;
            this.butOK.Text = "Save";
            this.metroToolTip1.SetToolTip(this.butOK, "Save Categories");
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butDelete
            // 
            this.butDelete.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butDelete.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butDelete.BackgroundImage")));
            this.butDelete.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butDelete.Location = new System.Drawing.Point(102, 121);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(28, 28);
            this.butDelete.TabIndex = 115;
            this.butDelete.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butDelete, "Delete Selected Category");
            this.butDelete.UseSelectable = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // butNew
            // 
            this.butNew.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.butNew.BackgroundImage = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_add;
            this.butNew.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butNew.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butNew.Location = new System.Drawing.Point(18, 121);
            this.butNew.Name = "butNew";
            this.butNew.Size = new System.Drawing.Size(28, 28);
            this.butNew.TabIndex = 113;
            this.butNew.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butNew, "Add New Category");
            this.butNew.UseSelectable = true;
            this.butNew.Click += new System.EventHandler(this.butNew_Click);
            // 
            // butReplace
            // 
            this.butReplace.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butReplace.BackgroundImage")));
            this.butReplace.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butReplace.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butReplace.ForeColor = System.Drawing.Color.White;
            this.butReplace.Location = new System.Drawing.Point(60, 121);
            this.butReplace.Name = "butReplace";
            this.butReplace.Size = new System.Drawing.Size(28, 28);
            this.butReplace.TabIndex = 114;
            this.butReplace.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butReplace.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butReplace, "Rename Selected Category");
            this.butReplace.UseSelectable = true;
            this.butReplace.Click += new System.EventHandler(this.butReplace_Click);
            // 
            // picDictionaries
            // 
            this.picDictionaries.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picDictionaries.Image = ((System.Drawing.Image)(resources.GetObject("picDictionaries.Image")));
            this.picDictionaries.Location = new System.Drawing.Point(10, 16);
            this.picDictionaries.Name = "picDictionaries";
            this.picDictionaries.Size = new System.Drawing.Size(41, 38);
            this.picDictionaries.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picDictionaries.TabIndex = 119;
            this.picDictionaries.TabStop = false;
            // 
            // frmCategory
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(887, 537);
            this.ControlBox = false;
            this.Controls.Add(this.butDelete);
            this.Controls.Add(this.butNew);
            this.Controls.Add(this.butReplace);
            this.Controls.Add(this.lstbCategories);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbName);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.picDictionaries);
            this.Name = "frmCategory";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "     Categories";
            ((System.ComponentModel.ISupportInitialize)(this.picDictionaries)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private MetroFramework.Controls.MetroButton butNew;
        private MetroFramework.Controls.MetroButton butReplace;
        private System.Windows.Forms.ListBox lstbCategories;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbName;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOK;
        private System.Windows.Forms.PictureBox picDictionaries;
    }
}