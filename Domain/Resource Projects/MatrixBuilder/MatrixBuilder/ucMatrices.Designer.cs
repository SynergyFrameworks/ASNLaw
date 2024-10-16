namespace MatrixBuilder
{
    partial class ucMatrices
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMatrices));
            this.panHeader = new System.Windows.Forms.Panel();
            this.picMatrixIcon = new System.Windows.Forms.PictureBox();
            this.lblDefinition = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panFooter = new System.Windows.Forms.Panel();
            this.splitContMain = new System.Windows.Forms.SplitContainer();
            this.lstbMatrices = new System.Windows.Forms.ListBox();
            this.panLeftTop = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.panMatrixLeftPadding = new System.Windows.Forms.Panel();
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMatrixIcon)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).BeginInit();
            this.splitContMain.Panel1.SuspendLayout();
            this.splitContMain.Panel2.SuspendLayout();
            this.splitContMain.SuspendLayout();
            this.panLeftTop.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.picMatrixIcon);
            this.panHeader.Controls.Add(this.lblDefinition);
            this.panHeader.Controls.Add(this.lblMessage);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(918, 50);
            this.panHeader.TabIndex = 20;
            // 
            // picMatrixIcon
            // 
            this.picMatrixIcon.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picMatrixIcon.Image = ((System.Drawing.Image)(resources.GetObject("picMatrixIcon.Image")));
            this.picMatrixIcon.Location = new System.Drawing.Point(3, 0);
            this.picMatrixIcon.Name = "picMatrixIcon";
            this.picMatrixIcon.Size = new System.Drawing.Size(48, 48);
            this.picMatrixIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMatrixIcon.TabIndex = 187;
            this.picMatrixIcon.TabStop = false;
            this.picMatrixIcon.Click += new System.EventHandler(this.picMatrixIcon_Click);
            // 
            // lblDefinition
            // 
            this.lblDefinition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefinition.ForeColor = System.Drawing.Color.White;
            this.lblDefinition.Location = new System.Drawing.Point(150, 19);
            this.lblDefinition.Name = "lblDefinition";
            this.lblDefinition.Size = new System.Drawing.Size(746, 22);
            this.lblDefinition.TabIndex = 186;
            this.lblDefinition.Text = "Below Matrices are for the selected project.   Click the Next button to edit.";
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(206, 21);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 17);
            this.lblMessage.TabIndex = 24;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(56, 11);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(92, 30);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Matrices";
            // 
            // panFooter
            // 
            this.panFooter.BackColor = System.Drawing.Color.Black;
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(0, 380);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(918, 38);
            this.panFooter.TabIndex = 21;
            // 
            // splitContMain
            // 
            this.splitContMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContMain.Location = new System.Drawing.Point(0, 50);
            this.splitContMain.Name = "splitContMain";
            // 
            // splitContMain.Panel1
            // 
            this.splitContMain.Panel1.Controls.Add(this.lstbMatrices);
            this.splitContMain.Panel1.Controls.Add(this.panLeftTop);
            // 
            // splitContMain.Panel2
            // 
            this.splitContMain.Panel2.Controls.Add(this.reoGridControl1);
            this.splitContMain.Panel2.Controls.Add(this.panMatrixLeftPadding);
            this.splitContMain.Panel2.Controls.Add(this.panel1);
            this.splitContMain.Size = new System.Drawing.Size(918, 330);
            this.splitContMain.SplitterDistance = 305;
            this.splitContMain.TabIndex = 22;
            // 
            // lstbMatrices
            // 
            this.lstbMatrices.BackColor = System.Drawing.Color.Black;
            this.lstbMatrices.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbMatrices.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbMatrices.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbMatrices.ForeColor = System.Drawing.Color.White;
            this.lstbMatrices.FormattingEnabled = true;
            this.lstbMatrices.ItemHeight = 21;
            this.lstbMatrices.Location = new System.Drawing.Point(0, 39);
            this.lstbMatrices.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.lstbMatrices.Name = "lstbMatrices";
            this.lstbMatrices.Size = new System.Drawing.Size(305, 291);
            this.lstbMatrices.TabIndex = 3;
            this.lstbMatrices.SelectedIndexChanged += new System.EventHandler(this.lstbMatrices_SelectedIndexChanged);
            // 
            // panLeftTop
            // 
            this.panLeftTop.BackColor = System.Drawing.Color.Black;
            this.panLeftTop.Controls.Add(this.label1);
            this.panLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panLeftTop.Location = new System.Drawing.Point(0, 0);
            this.panLeftTop.Name = "panLeftTop";
            this.panLeftTop.Size = new System.Drawing.Size(305, 39);
            this.panLeftTop.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(4, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 25);
            this.label1.TabIndex = 16;
            this.label1.Text = "Select a Matrix";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Black;
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(609, 39);
            this.panel1.TabIndex = 1;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 0);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 188;
            this.pictureBox2.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(45, 8);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 25);
            this.label2.TabIndex = 16;
            this.label2.Text = "Matrix";
            // 
            // panMatrixLeftPadding
            // 
            this.panMatrixLeftPadding.BackColor = System.Drawing.Color.Black;
            this.panMatrixLeftPadding.Dock = System.Windows.Forms.DockStyle.Right;
            this.panMatrixLeftPadding.Location = new System.Drawing.Point(592, 39);
            this.panMatrixLeftPadding.Name = "panMatrixLeftPadding";
            this.panMatrixLeftPadding.Size = new System.Drawing.Size(17, 291);
            this.panMatrixLeftPadding.TabIndex = 6;
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(0, 39);
            this.reoGridControl1.Margin = new System.Windows.Forms.Padding(3, 3, 3, 30);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.Padding = new System.Windows.Forms.Padding(0, 0, 30, 0);
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.SheetTabVisible = true;
            this.reoGridControl1.SheetTabWidth = 60;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.Size = new System.Drawing.Size(592, 291);
            this.reoGridControl1.TabIndex = 7;
            this.reoGridControl1.Text = "reoGridControl1";
            this.reoGridControl1.Visible = false;
            // 
            // ucMatrices
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContMain);
            this.Controls.Add(this.panFooter);
            this.Controls.Add(this.panHeader);
            this.Name = "ucMatrices";
            this.Size = new System.Drawing.Size(918, 418);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMatrixIcon)).EndInit();
            this.splitContMain.Panel1.ResumeLayout(false);
            this.splitContMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).EndInit();
            this.splitContMain.ResumeLayout(false);
            this.panLeftTop.ResumeLayout(false);
            this.panLeftTop.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblDefinition;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.SplitContainer splitContMain;
        private System.Windows.Forms.ListBox lstbMatrices;
        private System.Windows.Forms.Panel panLeftTop;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.PictureBox picMatrixIcon;
        private System.Windows.Forms.PictureBox pictureBox2;
        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private System.Windows.Forms.Panel panMatrixLeftPadding;
    }
}
