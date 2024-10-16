namespace ProfessionalDocAnalyzer
{
    partial class frmProject
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
            this.lblHeaderCaption = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtbProjectName = new System.Windows.Forms.TextBox();
            this.txtbDescription = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // lblHeaderCaption
            // 
            this.lblHeaderCaption.AutoSize = true;
            this.lblHeaderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblHeaderCaption.Location = new System.Drawing.Point(48, 25);
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            this.lblHeaderCaption.Size = new System.Drawing.Size(114, 25);
            this.lblHeaderCaption.TabIndex = 31;
            this.lblHeaderCaption.Text = "New Project";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(14, 73);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 21);
            this.label4.TabIndex = 42;
            this.label4.Text = "Project Name";
            // 
            // txtbProjectName
            // 
            this.txtbProjectName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbProjectName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbProjectName.Location = new System.Drawing.Point(12, 97);
            this.txtbProjectName.MaxLength = 20;
            this.txtbProjectName.Name = "txtbProjectName";
            this.txtbProjectName.Size = new System.Drawing.Size(260, 29);
            this.txtbProjectName.TabIndex = 41;
            this.txtbProjectName.TextChanged += new System.EventHandler(this.txtbProjectName_TextChanged);
            // 
            // txtbDescription
            // 
            this.txtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDescription.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDescription.Location = new System.Drawing.Point(12, 162);
            this.txtbDescription.MaxLength = 0;
            this.txtbDescription.Multiline = true;
            this.txtbDescription.Name = "txtbDescription";
            this.txtbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbDescription.Size = new System.Drawing.Size(561, 79);
            this.txtbDescription.TabIndex = 43;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(12, 138);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 21);
            this.label1.TabIndex = 44;
            this.label1.Text = "Description";
            // 
            // txtbMessage
            // 
            this.txtbMessage.BackColor = System.Drawing.Color.White;
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.Black;
            this.txtbMessage.Location = new System.Drawing.Point(294, 31);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(271, 106);
            this.txtbMessage.TabIndex = 70;
            this.txtbMessage.TextChanged += new System.EventHandler(this.txtbMessage_TextChanged);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.Location = new System.Drawing.Point(180, 37);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 71;
            this.lblMessage.Visible = false;
            this.lblMessage.TextChanged += new System.EventHandler(this.lblMessage_TextChanged);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(408, 260);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 89;
            this.butOK.Text = "Save";
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(498, 260);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 88;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_folder;
            this.pictureBox1.Location = new System.Drawing.Point(8, 19);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 30;
            this.pictureBox1.TabStop = false;
            // 
            // frmProject
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(588, 295);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtbMessage);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtbDescription);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtbProjectName);
            this.Controls.Add(this.lblHeaderCaption);
            this.Controls.Add(this.pictureBox1);
            this.DisplayHeader = false;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProject";
            this.Padding = new System.Windows.Forms.Padding(20, 30, 20, 30);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "New Project";
            this.Load += new System.EventHandler(this.frmProject_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblHeaderCaption;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtbProjectName;
        private System.Windows.Forms.TextBox txtbDescription;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtbMessage;
        private System.Windows.Forms.Label lblMessage;
        private MetroFramework.Controls.MetroButton butOK;
        private MetroFramework.Controls.MetroButton butCancel;
    }
}