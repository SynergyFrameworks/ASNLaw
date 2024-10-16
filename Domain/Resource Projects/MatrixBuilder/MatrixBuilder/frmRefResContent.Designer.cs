namespace MatrixBuilder
{
    partial class frmRefResContent
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmRefResContent));
            this.lblDefinition = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panBottom = new System.Windows.Forms.Panel();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOk = new MetroFramework.Controls.MetroButton();
            this.txtbContent = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtbContentName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblDefinition
            // 
            this.lblDefinition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefinition.ForeColor = System.Drawing.Color.White;
            this.lblDefinition.Location = new System.Drawing.Point(315, 14);
            this.lblDefinition.Name = "lblDefinition";
            this.lblDefinition.Size = new System.Drawing.Size(490, 45);
            this.lblDefinition.TabIndex = 184;
            this.lblDefinition.Text = "Reference Resources - A collection of reusable excerpts content that can be alloc" +
    "ated into a Matrix, such as Win Themes && Discriminators.";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(12, 11);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 183;
            this.pictureBox2.TabStop = false;
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.butCancel);
            this.panBottom.Controls.Add(this.butOk);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(20, 357);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(771, 47);
            this.panBottom.TabIndex = 185;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(677, 10);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(585, 10);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 0;
            this.butOk.Text = "Save";
            this.butOk.UseSelectable = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // txtbContent
            // 
            this.txtbContent.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbContent.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbContent.Location = new System.Drawing.Point(23, 158);
            this.txtbContent.MaxLength = 32000;
            this.txtbContent.Multiline = true;
            this.txtbContent.Name = "txtbContent";
            this.txtbContent.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbContent.Size = new System.Drawing.Size(765, 193);
            this.txtbContent.TabIndex = 188;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(20, 135);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 20);
            this.label2.TabIndex = 189;
            this.label2.Text = "Content";
            // 
            // txtbContentName
            // 
            this.txtbContentName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbContentName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbContentName.Location = new System.Drawing.Point(23, 94);
            this.txtbContentName.MaxLength = 50;
            this.txtbContentName.Name = "txtbContentName";
            this.txtbContentName.Size = new System.Drawing.Size(292, 25);
            this.txtbContentName.TabIndex = 186;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(20, 71);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(105, 20);
            this.label4.TabIndex = 187;
            this.label4.Text = "Content Name";
            // 
            // frmRefResContent
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(811, 424);
            this.ControlBox = false;
            this.Controls.Add(this.txtbContent);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtbContentName);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.lblDefinition);
            this.Controls.Add(this.pictureBox2);
            this.Name = "frmRefResContent";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "     Ref. Resource Content";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panBottom.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblDefinition;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panBottom;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOk;
        private System.Windows.Forms.TextBox txtbContent;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtbContentName;
        private System.Windows.Forms.Label label4;
    }
}