namespace MatrixBuilder
{
    partial class ucMatrixMatrix
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMatrixMatrix));
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblCaptionDescription = new System.Windows.Forms.Label();
            this.lblCaption = new System.Windows.Forms.Label();
            this.picMatrixBuilder = new System.Windows.Forms.PictureBox();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMatrixBuilder)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.Controls.Add(this.picMatrixBuilder);
            this.panHeader.Controls.Add(this.lblCaptionDescription);
            this.panHeader.Controls.Add(this.lblCaption);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(667, 48);
            this.panHeader.TabIndex = 38;
            // 
            // lblCaptionDescription
            // 
            this.lblCaptionDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaptionDescription.ForeColor = System.Drawing.Color.White;
            this.lblCaptionDescription.Location = new System.Drawing.Point(114, 3);
            this.lblCaptionDescription.Name = "lblCaptionDescription";
            this.lblCaptionDescription.Size = new System.Drawing.Size(678, 42);
            this.lblCaptionDescription.TabIndex = 189;
            this.lblCaptionDescription.Text = "Matrix overview ...";
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.Color.White;
            this.lblCaption.Location = new System.Drawing.Point(47, 9);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(66, 25);
            this.lblCaption.TabIndex = 34;
            this.lblCaption.Text = "Matrix";
            this.lblCaption.Click += new System.EventHandler(this.lblCaption_Click);
            // 
            // picMatrixBuilder
            // 
            this.picMatrixBuilder.Image = ((System.Drawing.Image)(resources.GetObject("picMatrixBuilder.Image")));
            this.picMatrixBuilder.Location = new System.Drawing.Point(3, 3);
            this.picMatrixBuilder.Name = "picMatrixBuilder";
            this.picMatrixBuilder.Size = new System.Drawing.Size(38, 38);
            this.picMatrixBuilder.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picMatrixBuilder.TabIndex = 190;
            this.picMatrixBuilder.TabStop = false;
            // 
            // ucMatrixMatrix
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.panHeader);
            this.Name = "ucMatrixMatrix";
            this.Size = new System.Drawing.Size(667, 346);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picMatrixBuilder)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblCaptionDescription;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.PictureBox picMatrixBuilder;
    }
}
