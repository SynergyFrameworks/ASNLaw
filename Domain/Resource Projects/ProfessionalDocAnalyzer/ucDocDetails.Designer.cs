namespace ProfessionalDocAnalyzer
{
    partial class ucDocDetails
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucDocDetails));
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblParsed = new System.Windows.Forms.Label();
            this.lblFile = new System.Windows.Forms.Label();
            this.lblFileInfo = new System.Windows.Forms.Label();
            this.txtFileInfo = new System.Windows.Forms.TextBox();
            this.picNo = new System.Windows.Forms.PictureBox();
            this.picYes = new System.Windows.Forms.PictureBox();
            this.picPDF = new System.Windows.Forms.PictureBox();
            this.picRTF = new System.Windows.Forms.PictureBox();
            this.picWord = new System.Windows.Forms.PictureBox();
            this.picPowerPoint = new System.Windows.Forms.PictureBox();
            this.picTXT = new System.Windows.Forms.PictureBox();
            this.picExcel = new System.Windows.Forms.PictureBox();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picYes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPDF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRTF)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWord)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPowerPoint)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTXT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.White;
            this.panHeader.Controls.Add(this.picNo);
            this.panHeader.Controls.Add(this.lblParsed);
            this.panHeader.Controls.Add(this.picYes);
            this.panHeader.Controls.Add(this.lblFile);
            this.panHeader.Controls.Add(this.lblFileInfo);
            this.panHeader.Controls.Add(this.picPDF);
            this.panHeader.Controls.Add(this.picRTF);
            this.panHeader.Controls.Add(this.picWord);
            this.panHeader.Controls.Add(this.picPowerPoint);
            this.panHeader.Controls.Add(this.picTXT);
            this.panHeader.Controls.Add(this.picExcel);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.ForeColor = System.Drawing.Color.White;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(492, 50);
            this.panHeader.TabIndex = 6;
            // 
            // lblParsed
            // 
            this.lblParsed.AutoSize = true;
            this.lblParsed.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblParsed.ForeColor = System.Drawing.Color.Black;
            this.lblParsed.Location = new System.Drawing.Point(31, 28);
            this.lblParsed.Name = "lblParsed";
            this.lblParsed.Size = new System.Drawing.Size(48, 17);
            this.lblParsed.TabIndex = 159;
            this.lblParsed.Text = "Parsed";
            this.lblParsed.Visible = false;
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFile.ForeColor = System.Drawing.Color.Black;
            this.lblFile.Location = new System.Drawing.Point(31, 6);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(27, 17);
            this.lblFile.TabIndex = 157;
            this.lblFile.Text = "File";
            this.lblFile.Visible = false;
            // 
            // lblFileInfo
            // 
            this.lblFileInfo.AutoSize = true;
            this.lblFileInfo.BackColor = System.Drawing.Color.White;
            this.lblFileInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFileInfo.ForeColor = System.Drawing.Color.Black;
            this.lblFileInfo.Location = new System.Drawing.Point(394, 19);
            this.lblFileInfo.Name = "lblFileInfo";
            this.lblFileInfo.Size = new System.Drawing.Size(49, 17);
            this.lblFileInfo.TabIndex = 156;
            this.lblFileInfo.Text = "FileInfo";
            this.lblFileInfo.Visible = false;
            this.lblFileInfo.TextChanged += new System.EventHandler(this.lblFileInfo_TextChanged);
            // 
            // txtFileInfo
            // 
            this.txtFileInfo.BackColor = System.Drawing.Color.White;
            this.txtFileInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtFileInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtFileInfo.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFileInfo.ForeColor = System.Drawing.Color.Black;
            this.txtFileInfo.Location = new System.Drawing.Point(0, 50);
            this.txtFileInfo.Margin = new System.Windows.Forms.Padding(30, 3, 30, 3);
            this.txtFileInfo.Multiline = true;
            this.txtFileInfo.Name = "txtFileInfo";
            this.txtFileInfo.Size = new System.Drawing.Size(492, 149);
            this.txtFileInfo.TabIndex = 155;
            this.txtFileInfo.TextChanged += new System.EventHandler(this.txtFileInfo_TextChanged);
            // 
            // picNo
            // 
            this.picNo.Image = ((System.Drawing.Image)(resources.GetObject("picNo.Image")));
            this.picNo.InitialImage = null;
            this.picNo.Location = new System.Drawing.Point(5, 27);
            this.picNo.Name = "picNo";
            this.picNo.Size = new System.Drawing.Size(20, 20);
            this.picNo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picNo.TabIndex = 160;
            this.picNo.TabStop = false;
            this.picNo.Visible = false;
            // 
            // picYes
            // 
            this.picYes.Image = ((System.Drawing.Image)(resources.GetObject("picYes.Image")));
            this.picYes.InitialImage = null;
            this.picYes.Location = new System.Drawing.Point(5, 27);
            this.picYes.Name = "picYes";
            this.picYes.Size = new System.Drawing.Size(20, 20);
            this.picYes.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picYes.TabIndex = 158;
            this.picYes.TabStop = false;
            this.picYes.Visible = false;
            // 
            // picPDF
            // 
            this.picPDF.Image = ((System.Drawing.Image)(resources.GetObject("picPDF.Image")));
            this.picPDF.Location = new System.Drawing.Point(5, 3);
            this.picPDF.Name = "picPDF";
            this.picPDF.Size = new System.Drawing.Size(20, 20);
            this.picPDF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPDF.TabIndex = 0;
            this.picPDF.TabStop = false;
            this.picPDF.Visible = false;
            // 
            // picRTF
            // 
            this.picRTF.Image = ((System.Drawing.Image)(resources.GetObject("picRTF.Image")));
            this.picRTF.Location = new System.Drawing.Point(5, 3);
            this.picRTF.Name = "picRTF";
            this.picRTF.Size = new System.Drawing.Size(20, 20);
            this.picRTF.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picRTF.TabIndex = 4;
            this.picRTF.TabStop = false;
            this.picRTF.Visible = false;
            // 
            // picWord
            // 
            this.picWord.Image = ((System.Drawing.Image)(resources.GetObject("picWord.Image")));
            this.picWord.Location = new System.Drawing.Point(5, 3);
            this.picWord.Name = "picWord";
            this.picWord.Size = new System.Drawing.Size(20, 20);
            this.picWord.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picWord.TabIndex = 5;
            this.picWord.TabStop = false;
            this.picWord.Visible = false;
            // 
            // picPowerPoint
            // 
            this.picPowerPoint.Image = ((System.Drawing.Image)(resources.GetObject("picPowerPoint.Image")));
            this.picPowerPoint.Location = new System.Drawing.Point(5, 3);
            this.picPowerPoint.Name = "picPowerPoint";
            this.picPowerPoint.Size = new System.Drawing.Size(20, 20);
            this.picPowerPoint.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picPowerPoint.TabIndex = 3;
            this.picPowerPoint.TabStop = false;
            this.picPowerPoint.Visible = false;
            // 
            // picTXT
            // 
            this.picTXT.Image = ((System.Drawing.Image)(resources.GetObject("picTXT.Image")));
            this.picTXT.Location = new System.Drawing.Point(5, 3);
            this.picTXT.Name = "picTXT";
            this.picTXT.Size = new System.Drawing.Size(20, 20);
            this.picTXT.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picTXT.TabIndex = 1;
            this.picTXT.TabStop = false;
            this.picTXT.Visible = false;
            // 
            // picExcel
            // 
            this.picExcel.Image = ((System.Drawing.Image)(resources.GetObject("picExcel.Image")));
            this.picExcel.Location = new System.Drawing.Point(5, 3);
            this.picExcel.Name = "picExcel";
            this.picExcel.Size = new System.Drawing.Size(20, 20);
            this.picExcel.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picExcel.TabIndex = 2;
            this.picExcel.TabStop = false;
            this.picExcel.Visible = false;
            // 
            // ucDocDetails
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.txtFileInfo);
            this.Controls.Add(this.panHeader);
            this.Name = "ucDocDetails";
            this.Size = new System.Drawing.Size(492, 199);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picYes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPDF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRTF)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picWord)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picPowerPoint)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picTXT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picExcel)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picPDF;
        private System.Windows.Forms.PictureBox picTXT;
        private System.Windows.Forms.PictureBox picExcel;
        private System.Windows.Forms.PictureBox picPowerPoint;
        private System.Windows.Forms.PictureBox picRTF;
        private System.Windows.Forms.PictureBox picWord;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblFileInfo;
        private System.Windows.Forms.TextBox txtFileInfo;
        private System.Windows.Forms.PictureBox picNo;
        private System.Windows.Forms.Label lblParsed;
        private System.Windows.Forms.PictureBox picYes;
        private System.Windows.Forms.Label lblFile;
    }
}
