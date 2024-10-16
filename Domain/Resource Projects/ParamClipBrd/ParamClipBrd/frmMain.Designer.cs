namespace ParamClipBrd
{
    partial class frmMain
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.lstParameters = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.txtMergeText = new System.Windows.Forms.TextBox();
            this.butSave2Clipbrd = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.chkbTransparent = new System.Windows.Forms.CheckBox();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panNotice = new System.Windows.Forms.Panel();
            this.txtbNote3 = new System.Windows.Forms.TextBox();
            this.butOK = new System.Windows.Forms.Button();
            this.txtbNote2 = new System.Windows.Forms.TextBox();
            this.txtbNote1 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox5 = new System.Windows.Forms.PictureBox();
            this.pictureBox4 = new System.Windows.Forms.PictureBox();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.chkListClick = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panNotice.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // lstParameters
            // 
            this.lstParameters.BackColor = System.Drawing.Color.Black;
            this.lstParameters.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstParameters.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstParameters.ForeColor = System.Drawing.Color.White;
            this.lstParameters.FormattingEnabled = true;
            this.lstParameters.ItemHeight = 17;
            this.lstParameters.Location = new System.Drawing.Point(12, 161);
            this.lstParameters.Name = "lstParameters";
            this.lstParameters.Size = new System.Drawing.Size(260, 255);
            this.lstParameters.Sorted = true;
            this.lstParameters.TabIndex = 0;
            this.lstParameters.Click += new System.EventHandler(this.lstParameters_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Black;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 135);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 21);
            this.label1.TabIndex = 1;
            this.label1.Text = "Fields";
            // 
            // txtMergeText
            // 
            this.txtMergeText.BackColor = System.Drawing.Color.Black;
            this.txtMergeText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtMergeText.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMergeText.ForeColor = System.Drawing.Color.LightSteelBlue;
            this.txtMergeText.Location = new System.Drawing.Point(12, 46);
            this.txtMergeText.Multiline = true;
            this.txtMergeText.Name = "txtMergeText";
            this.txtMergeText.Size = new System.Drawing.Size(260, 29);
            this.txtMergeText.TabIndex = 2;
            // 
            // butSave2Clipbrd
            // 
            this.butSave2Clipbrd.Location = new System.Drawing.Point(12, 101);
            this.butSave2Clipbrd.Name = "butSave2Clipbrd";
            this.butSave2Clipbrd.Size = new System.Drawing.Size(106, 23);
            this.butSave2Clipbrd.TabIndex = 3;
            this.butSave2Clipbrd.Text = "Copy to Clipboard";
            this.butSave2Clipbrd.UseVisualStyleBackColor = true;
            this.butSave2Clipbrd.Visible = false;
            this.butSave2Clipbrd.Click += new System.EventHandler(this.butSave2Clipbrd_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Black;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(42, 12);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(201, 25);
            this.label2.TabIndex = 4;
            this.label2.Text = "Storyboard Data Fields";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(192, 435);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 5;
            this.button2.Text = "Close";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // chkbTransparent
            // 
            this.chkbTransparent.AutoSize = true;
            this.chkbTransparent.BackColor = System.Drawing.Color.Black;
            this.chkbTransparent.ForeColor = System.Drawing.Color.White;
            this.chkbTransparent.Location = new System.Drawing.Point(16, 441);
            this.chkbTransparent.Name = "chkbTransparent";
            this.chkbTransparent.Size = new System.Drawing.Size(83, 17);
            this.chkbTransparent.TabIndex = 6;
            this.chkbTransparent.Text = "Transparent";
            this.chkbTransparent.UseVisualStyleBackColor = false;
            this.chkbTransparent.CheckedChanged += new System.EventHandler(this.chkbTransparent_CheckedChanged);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(5, 5);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(38, 38);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 7;
            this.pictureBox1.TabStop = false;
            // 
            // panNotice
            // 
            this.panNotice.BackColor = System.Drawing.Color.White;
            this.panNotice.Controls.Add(this.txtbNote3);
            this.panNotice.Controls.Add(this.butOK);
            this.panNotice.Controls.Add(this.txtbNote2);
            this.panNotice.Controls.Add(this.txtbNote1);
            this.panNotice.Controls.Add(this.label3);
            this.panNotice.Controls.Add(this.pictureBox5);
            this.panNotice.Controls.Add(this.pictureBox4);
            this.panNotice.Controls.Add(this.pictureBox3);
            this.panNotice.Controls.Add(this.pictureBox2);
            this.panNotice.Location = new System.Drawing.Point(281, 7);
            this.panNotice.Name = "panNotice";
            this.panNotice.Size = new System.Drawing.Size(289, 455);
            this.panNotice.TabIndex = 8;
            // 
            // txtbNote3
            // 
            this.txtbNote3.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtbNote3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbNote3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbNote3.Location = new System.Drawing.Point(12, 345);
            this.txtbNote3.Multiline = true;
            this.txtbNote3.Name = "txtbNote3";
            this.txtbNote3.Size = new System.Drawing.Size(263, 72);
            this.txtbNote3.TabIndex = 8;
            this.txtbNote3.Text = "Atebion Data Fields in MS Word are often shown as a misspelled word. Atebion prod" +
    "ucts cannot populate Data Fields denoted with a squiggly red line (misspelled).";
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(200, 422);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(75, 23);
            this.butOK.TabIndex = 7;
            this.butOK.Text = "Hide";
            this.butOK.UseVisualStyleBackColor = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // txtbNote2
            // 
            this.txtbNote2.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtbNote2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbNote2.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbNote2.Location = new System.Drawing.Point(16, 193);
            this.txtbNote2.Multiline = true;
            this.txtbNote2.Name = "txtbNote2";
            this.txtbNote2.Size = new System.Drawing.Size(259, 114);
            this.txtbNote2.TabIndex = 6;
            this.txtbNote2.Text = "To enable Atebion products to populate a Data Field:";
            // 
            // txtbNote1
            // 
            this.txtbNote1.BackColor = System.Drawing.Color.LightGoldenrodYellow;
            this.txtbNote1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbNote1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbNote1.Location = new System.Drawing.Point(15, 76);
            this.txtbNote1.Multiline = true;
            this.txtbNote1.Name = "txtbNote1";
            this.txtbNote1.Size = new System.Drawing.Size(260, 79);
            this.txtbNote1.TabIndex = 5;
            this.txtbNote1.Text = "Atebion Data Fields in MS Word are often shown as a misspelled word. Atebion prod" +
    "ucts cannot populate Data Fields denoted with a squiggly red line (misspelled).";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(58, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(191, 30);
            this.label3.TabIndex = 4;
            this.label3.Text = "Important Notice:";
            // 
            // pictureBox5
            // 
            this.pictureBox5.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox5.Image")));
            this.pictureBox5.InitialImage = null;
            this.pictureBox5.Location = new System.Drawing.Point(12, 6);
            this.pictureBox5.Name = "pictureBox5";
            this.pictureBox5.Size = new System.Drawing.Size(42, 38);
            this.pictureBox5.TabIndex = 3;
            this.pictureBox5.TabStop = false;
            // 
            // pictureBox4
            // 
            this.pictureBox4.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox4.Image")));
            this.pictureBox4.InitialImage = null;
            this.pictureBox4.Location = new System.Drawing.Point(12, 314);
            this.pictureBox4.Name = "pictureBox4";
            this.pictureBox4.Size = new System.Drawing.Size(208, 29);
            this.pictureBox4.TabIndex = 2;
            this.pictureBox4.TabStop = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox3.Image")));
            this.pictureBox3.InitialImage = null;
            this.pictureBox3.Location = new System.Drawing.Point(16, 162);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(208, 29);
            this.pictureBox3.TabIndex = 1;
            this.pictureBox3.TabStop = false;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.InitialImage = null;
            this.pictureBox2.Location = new System.Drawing.Point(15, 45);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(208, 29);
            this.pictureBox2.TabIndex = 0;
            this.pictureBox2.TabStop = false;
            // 
            // chkListClick
            // 
            this.chkListClick.AutoSize = true;
            this.chkListClick.BackColor = System.Drawing.Color.Black;
            this.chkListClick.Checked = true;
            this.chkListClick.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkListClick.ForeColor = System.Drawing.Color.White;
            this.chkListClick.Location = new System.Drawing.Point(12, 81);
            this.chkListClick.Name = "chkListClick";
            this.chkListClick.Size = new System.Drawing.Size(194, 17);
            this.chkListClick.TabIndex = 9;
            this.chkListClick.Text = "Copy to Clipboard on Field List Click";
            this.chkListClick.UseVisualStyleBackColor = false;
            this.chkListClick.CheckedChanged += new System.EventHandler(this.chkListClick_CheckedChanged);
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(578, 470);
            this.Controls.Add(this.panNotice);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.chkbTransparent);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.butSave2Clipbrd);
            this.Controls.Add(this.txtMergeText);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstParameters);
            this.Controls.Add(this.chkListClick);
            this.Name = "frmMain";
            this.Text = "Storyboard Data Fields";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panNotice.ResumeLayout(false);
            this.panNotice.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox lstParameters;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtMergeText;
        private System.Windows.Forms.Button butSave2Clipbrd;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.CheckBox chkbTransparent;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Panel panNotice;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.TextBox txtbNote2;
        private System.Windows.Forms.TextBox txtbNote1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.PictureBox pictureBox5;
        private System.Windows.Forms.PictureBox pictureBox4;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.Button butOK;
        private System.Windows.Forms.TextBox txtbNote3;
        private System.Windows.Forms.CheckBox chkListClick;
    }
}

