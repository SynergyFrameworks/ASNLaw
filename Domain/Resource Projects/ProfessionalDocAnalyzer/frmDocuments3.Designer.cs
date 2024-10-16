namespace ProfessionalDocAnalyzer
{
    partial class frmDocuments3
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocuments3));
            this.butLoadFile = new MetroFramework.Controls.MetroButton();
            this.lblSelectDoc = new System.Windows.Forms.Label();
            this.mtxtbFile = new MetroFramework.Controls.MetroTextBox();
            this.lblStep1Instructions = new System.Windows.Forms.Label();
            this.txtbDocName = new System.Windows.Forms.TextBox();
            this.lblDocName = new System.Windows.Forms.Label();
            this.butSave = new MetroFramework.Controls.MetroButton();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panAnalyzeNotice = new System.Windows.Forms.Panel();
            this.lblNotice = new System.Windows.Forms.Label();
            this.lblWait = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.chkbConvertUsingWord = new System.Windows.Forms.CheckBox();
            this.panAnalyzeNotice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // butLoadFile
            // 
            this.butLoadFile.Location = new System.Drawing.Point(673, 90);
            this.butLoadFile.Name = "butLoadFile";
            this.butLoadFile.Size = new System.Drawing.Size(75, 23);
            this.butLoadFile.TabIndex = 84;
            this.butLoadFile.Text = "Select File";
            this.butLoadFile.UseSelectable = true;
            this.butLoadFile.Click += new System.EventHandler(this.butLoadFile_Click);
            // 
            // lblSelectDoc
            // 
            this.lblSelectDoc.AutoSize = true;
            this.lblSelectDoc.BackColor = System.Drawing.Color.White;
            this.lblSelectDoc.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelectDoc.ForeColor = System.Drawing.Color.Black;
            this.lblSelectDoc.Location = new System.Drawing.Point(12, 70);
            this.lblSelectDoc.Name = "lblSelectDoc";
            this.lblSelectDoc.Size = new System.Drawing.Size(30, 17);
            this.lblSelectDoc.TabIndex = 83;
            this.lblSelectDoc.Text = "File:";
            // 
            // mtxtbFile
            // 
            this.mtxtbFile.Location = new System.Drawing.Point(13, 90);
            this.mtxtbFile.MaxLength = 32767;
            this.mtxtbFile.Name = "mtxtbFile";
            this.mtxtbFile.PasswordChar = '\0';
            this.mtxtbFile.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.mtxtbFile.SelectedText = "";
            this.mtxtbFile.Size = new System.Drawing.Size(654, 23);
            this.mtxtbFile.TabIndex = 79;
            this.mtxtbFile.UseSelectable = true;
            // 
            // lblStep1Instructions
            // 
            this.lblStep1Instructions.BackColor = System.Drawing.Color.White;
            this.lblStep1Instructions.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblStep1Instructions.ForeColor = System.Drawing.Color.Black;
            this.lblStep1Instructions.Location = new System.Drawing.Point(14, 182);
            this.lblStep1Instructions.Name = "lblStep1Instructions";
            this.lblStep1Instructions.Size = new System.Drawing.Size(275, 34);
            this.lblStep1Instructions.TabIndex = 82;
            this.lblStep1Instructions.Text = "*Must be an unique name for the current project, unless you are replacing an exis" +
    "ting document.";
            // 
            // txtbDocName
            // 
            this.txtbDocName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDocName.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDocName.Location = new System.Drawing.Point(15, 146);
            this.txtbDocName.MaxLength = 40;
            this.txtbDocName.Name = "txtbDocName";
            this.txtbDocName.Size = new System.Drawing.Size(193, 22);
            this.txtbDocName.TabIndex = 80;
            this.txtbDocName.TextChanged += new System.EventHandler(this.txtbDocName_TextChanged);
            // 
            // lblDocName
            // 
            this.lblDocName.AutoSize = true;
            this.lblDocName.BackColor = System.Drawing.Color.White;
            this.lblDocName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDocName.ForeColor = System.Drawing.Color.Black;
            this.lblDocName.Location = new System.Drawing.Point(12, 125);
            this.lblDocName.Name = "lblDocName";
            this.lblDocName.Size = new System.Drawing.Size(114, 17);
            this.lblDocName.TabIndex = 81;
            this.lblDocName.Text = "*Document Name:";
            // 
            // butSave
            // 
            this.butSave.Location = new System.Drawing.Point(583, 245);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(75, 23);
            this.butSave.TabIndex = 86;
            this.butSave.Text = "Import";
            this.butSave.UseSelectable = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(673, 245);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 85;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            this.openFileDialog1.Title = "Select File for Current Project";
            // 
            // panAnalyzeNotice
            // 
            this.panAnalyzeNotice.Controls.Add(this.lblNotice);
            this.panAnalyzeNotice.Controls.Add(this.lblWait);
            this.panAnalyzeNotice.Location = new System.Drawing.Point(320, 135);
            this.panAnalyzeNotice.Name = "panAnalyzeNotice";
            this.panAnalyzeNotice.Size = new System.Drawing.Size(428, 81);
            this.panAnalyzeNotice.TabIndex = 162;
            this.panAnalyzeNotice.Visible = false;
            // 
            // lblNotice
            // 
            this.lblNotice.BackColor = System.Drawing.Color.Transparent;
            this.lblNotice.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.lblNotice.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNotice.ForeColor = System.Drawing.Color.Black;
            this.lblNotice.Location = new System.Drawing.Point(0, 35);
            this.lblNotice.Name = "lblNotice";
            this.lblNotice.Size = new System.Drawing.Size(428, 46);
            this.lblNotice.TabIndex = 117;
            // 
            // lblWait
            // 
            this.lblWait.AutoSize = true;
            this.lblWait.BackColor = System.Drawing.Color.Transparent;
            this.lblWait.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWait.ForeColor = System.Drawing.Color.Black;
            this.lblWait.Location = new System.Drawing.Point(6, 0);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(96, 21);
            this.lblWait.TabIndex = 116;
            this.lblWait.Text = "Please wait...";
            // 
            // lblMessage
            // 
            this.lblMessage.BackColor = System.Drawing.Color.White;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Black;
            this.lblMessage.Location = new System.Drawing.Point(316, 134);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(431, 91);
            this.lblMessage.TabIndex = 163;
            this.lblMessage.Text = "*Must be an unique name for the current project, unless you are replacing an exis" +
    "ting document.";
            // 
            // picHeader
            // 
            this.picHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHeader.Image = ((System.Drawing.Image)(resources.GetObject("picHeader.Image")));
            this.picHeader.Location = new System.Drawing.Point(10, 17);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(41, 38);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 51;
            this.picHeader.TabStop = false;
            // 
            // chkbConvertUsingWord
            // 
            this.chkbConvertUsingWord.AutoSize = true;
            this.chkbConvertUsingWord.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbConvertUsingWord.Location = new System.Drawing.Point(396, 248);
            this.chkbConvertUsingWord.Name = "chkbConvertUsingWord";
            this.chkbConvertUsingWord.Size = new System.Drawing.Size(149, 17);
            this.chkbConvertUsingWord.TabIndex = 164;
            this.chkbConvertUsingWord.Text = "Convert using MS Word";
            this.chkbConvertUsingWord.UseVisualStyleBackColor = true;
            this.chkbConvertUsingWord.Visible = false;
            // 
            // frmDocuments3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(762, 278);
            this.ControlBox = false;
            this.Controls.Add(this.chkbConvertUsingWord);
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.panAnalyzeNotice);
            this.Controls.Add(this.butSave);
            this.Controls.Add(this.butCancel);
            this.Controls.Add(this.butLoadFile);
            this.Controls.Add(this.lblSelectDoc);
            this.Controls.Add(this.mtxtbFile);
            this.Controls.Add(this.lblStep1Instructions);
            this.Controls.Add(this.txtbDocName);
            this.Controls.Add(this.lblDocName);
            this.Controls.Add(this.picHeader);
            this.Name = "frmDocuments3";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.DropShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "    Add Document";
            this.panAnalyzeNotice.ResumeLayout(false);
            this.panAnalyzeNotice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox picHeader;
        private MetroFramework.Controls.MetroButton butLoadFile;
        private System.Windows.Forms.Label lblSelectDoc;
        private MetroFramework.Controls.MetroTextBox mtxtbFile;
        private System.Windows.Forms.Label lblStep1Instructions;
        private System.Windows.Forms.TextBox txtbDocName;
        private System.Windows.Forms.Label lblDocName;
        private MetroFramework.Controls.MetroButton butSave;
        private MetroFramework.Controls.MetroButton butCancel;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Panel panAnalyzeNotice;
        private System.Windows.Forms.Label lblNotice;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.CheckBox chkbConvertUsingWord;
    }
}