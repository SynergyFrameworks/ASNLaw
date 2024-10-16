namespace ProfessionalDocAnalyzer
{
    partial class frmSynonymNew
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
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.txtNewSynonym = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(297, 86);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 101;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(207, 86);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 102;
            this.butOK.Text = "Save";
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // txtNewSynonym
            // 
            this.txtNewSynonym.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtNewSynonym.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNewSynonym.Location = new System.Drawing.Point(25, 42);
            this.txtNewSynonym.MaxLength = 50;
            this.txtNewSynonym.Name = "txtNewSynonym";
            this.txtNewSynonym.Size = new System.Drawing.Size(347, 25);
            this.txtNewSynonym.TabIndex = 99;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(23, 18);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 21);
            this.label4.TabIndex = 100;
            this.label4.Text = "New Synonym";
            // 
            // frmSynonymNew
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(395, 121);
            this.ControlBox = false;
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.Controls.Add(this.txtNewSynonym);
            this.Controls.Add(this.label4);
            this.Name = "frmSynonymNew";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOK;
        private System.Windows.Forms.TextBox txtNewSynonym;
        private System.Windows.Forms.Label label4;
    }
}