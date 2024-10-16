namespace ProfessionalDocAnalyzer
{
    partial class frmExport
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmExport));
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.picExcelTemplate = new System.Windows.Forms.PictureBox();
            this.txtExportFileName = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblExcelTemplate = new System.Windows.Forms.Label();
            this.cboExcelTemplate = new System.Windows.Forms.ComboBox();
            this.chkbWeightColors = new MetroFramework.Controls.MetroCheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.picExcelTemplate)).BeginInit();
            this.SuspendLayout();
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(281, 283);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 9;
            this.butCancel.Text = "Close";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(190, 283);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 8;
            this.butOK.Text = "Export";
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // picExcelTemplate
            // 
            this.picExcelTemplate.Cursor = System.Windows.Forms.Cursors.Default;
            this.picExcelTemplate.Image = ((System.Drawing.Image)(resources.GetObject("picExcelTemplate.Image")));
            this.picExcelTemplate.Location = new System.Drawing.Point(12, 17);
            this.picExcelTemplate.Name = "picExcelTemplate";
            this.picExcelTemplate.Size = new System.Drawing.Size(38, 38);
            this.picExcelTemplate.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picExcelTemplate.TabIndex = 72;
            this.picExcelTemplate.TabStop = false;
            // 
            // txtExportFileName
            // 
            this.txtExportFileName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtExportFileName.Location = new System.Drawing.Point(23, 104);
            this.txtExportFileName.Name = "txtExportFileName";
            this.txtExportFileName.Size = new System.Drawing.Size(333, 25);
            this.txtExportFileName.TabIndex = 74;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(20, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(100, 21);
            this.label3.TabIndex = 73;
            this.label3.Text = "Export Name";
            // 
            // lblExcelTemplate
            // 
            this.lblExcelTemplate.AutoSize = true;
            this.lblExcelTemplate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblExcelTemplate.ForeColor = System.Drawing.Color.White;
            this.lblExcelTemplate.Location = new System.Drawing.Point(19, 151);
            this.lblExcelTemplate.Name = "lblExcelTemplate";
            this.lblExcelTemplate.Size = new System.Drawing.Size(117, 21);
            this.lblExcelTemplate.TabIndex = 76;
            this.lblExcelTemplate.Text = "Excel Templates";
            this.lblExcelTemplate.Visible = false;
            // 
            // cboExcelTemplate
            // 
            this.cboExcelTemplate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboExcelTemplate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboExcelTemplate.FormattingEnabled = true;
            this.cboExcelTemplate.Location = new System.Drawing.Point(23, 175);
            this.cboExcelTemplate.Name = "cboExcelTemplate";
            this.cboExcelTemplate.Size = new System.Drawing.Size(332, 25);
            this.cboExcelTemplate.TabIndex = 75;
            this.cboExcelTemplate.Visible = false;
            // 
            // chkbWeightColors
            // 
            this.chkbWeightColors.AutoSize = true;
            this.chkbWeightColors.Checked = true;
            this.chkbWeightColors.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbWeightColors.Location = new System.Drawing.Point(23, 225);
            this.chkbWeightColors.Name = "chkbWeightColors";
            this.chkbWeightColors.Size = new System.Drawing.Size(115, 15);
            this.chkbWeightColors.TabIndex = 77;
            this.chkbWeightColors.Text = "Use Value Colors?";
            this.chkbWeightColors.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.chkbWeightColors.UseSelectable = true;
            this.chkbWeightColors.Visible = false;
            // 
            // frmExport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.CancelButton = this.butCancel;
            this.ClientSize = new System.Drawing.Size(382, 326);
            this.ControlBox = false;
            this.Controls.Add(this.chkbWeightColors);
            this.Controls.Add(this.lblExcelTemplate);
            this.Controls.Add(this.cboExcelTemplate);
            this.Controls.Add(this.txtExportFileName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.picExcelTemplate);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butOK);
            this.Name = "frmExport";
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.SystemShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "     Excel Report";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.picExcelTemplate)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOK;
        public System.Windows.Forms.PictureBox picExcelTemplate;
        private System.Windows.Forms.TextBox txtExportFileName;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblExcelTemplate;
        private System.Windows.Forms.ComboBox cboExcelTemplate;
        private MetroFramework.Controls.MetroCheckBox chkbWeightColors;
    }
}