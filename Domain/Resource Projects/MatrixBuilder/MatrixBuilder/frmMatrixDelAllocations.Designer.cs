namespace MatrixBuilder
{
    partial class frmMatrixDelAllocations
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMatrixDelAllocations));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.lblDefinition = new System.Windows.Forms.Label();
            this.panBottom = new System.Windows.Forms.Panel();
            this.panBottomRight = new System.Windows.Forms.Panel();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOk = new MetroFramework.Controls.MetroButton();
            this.lblAllocatedDate = new System.Windows.Forms.Label();
            this.lblAllocatedBy = new System.Windows.Forms.Label();
            this.panHeader = new System.Windows.Forms.Panel();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.dgvDocTypes = new System.Windows.Forms.DataGridView();
            this.lblCaption = new System.Windows.Forms.Label();
            this.panBottom.SuspendLayout();
            this.panBottomRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocTypes)).BeginInit();
            this.SuspendLayout();
            // 
            // lblDefinition
            // 
            this.lblDefinition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDefinition.ForeColor = System.Drawing.Color.White;
            this.lblDefinition.Location = new System.Drawing.Point(321, 12);
            this.lblDefinition.Name = "lblDefinition";
            this.lblDefinition.Size = new System.Drawing.Size(427, 45);
            this.lblDefinition.TabIndex = 186;
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.panBottomRight);
            this.panBottom.Controls.Add(this.lblAllocatedDate);
            this.panBottom.Controls.Add(this.lblAllocatedBy);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(10, 413);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(835, 47);
            this.panBottom.TabIndex = 185;
            // 
            // panBottomRight
            // 
            this.panBottomRight.Controls.Add(this.butCancel);
            this.panBottomRight.Controls.Add(this.butOk);
            this.panBottomRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panBottomRight.Location = new System.Drawing.Point(635, 0);
            this.panBottomRight.Name = "panBottomRight";
            this.panBottomRight.Size = new System.Drawing.Size(200, 47);
            this.panBottomRight.TabIndex = 191;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(120, 19);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 3;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(28, 19);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 2;
            this.butOk.Text = "OK";
            this.butOk.UseSelectable = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // lblAllocatedDate
            // 
            this.lblAllocatedDate.AutoSize = true;
            this.lblAllocatedDate.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllocatedDate.ForeColor = System.Drawing.Color.White;
            this.lblAllocatedDate.Location = new System.Drawing.Point(3, 28);
            this.lblAllocatedDate.Name = "lblAllocatedDate";
            this.lblAllocatedDate.Size = new System.Drawing.Size(96, 17);
            this.lblAllocatedDate.TabIndex = 190;
            this.lblAllocatedDate.Text = "Allocated Date:";
            // 
            // lblAllocatedBy
            // 
            this.lblAllocatedBy.AutoSize = true;
            this.lblAllocatedBy.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllocatedBy.ForeColor = System.Drawing.Color.White;
            this.lblAllocatedBy.Location = new System.Drawing.Point(3, 3);
            this.lblAllocatedBy.Name = "lblAllocatedBy";
            this.lblAllocatedBy.Size = new System.Drawing.Size(82, 17);
            this.lblAllocatedBy.TabIndex = 189;
            this.lblAllocatedBy.Text = "Allocated By:";
            // 
            // panHeader
            // 
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(10, 60);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(835, 12);
            this.panHeader.TabIndex = 184;
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(4, 12);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 183;
            this.pictureBox2.TabStop = false;
            // 
            // dgvDocTypes
            // 
            this.dgvDocTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvDocTypes.BackgroundColor = System.Drawing.Color.Black;
            this.dgvDocTypes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDocTypes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvDocTypes.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvDocTypes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dgvDocTypes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvDocTypes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnEnter;
            this.dgvDocTypes.Location = new System.Drawing.Point(10, 72);
            this.dgvDocTypes.MultiSelect = false;
            this.dgvDocTypes.Name = "dgvDocTypes";
            this.dgvDocTypes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvDocTypes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dgvDocTypes.RowHeadersWidth = 5;
            this.dgvDocTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvDocTypes.Size = new System.Drawing.Size(835, 341);
            this.dgvDocTypes.TabIndex = 187;
            this.dgvDocTypes.SelectionChanged += new System.EventHandler(this.dgvDocTypes_SelectionChanged);
            // 
            // lblCaption
            // 
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.Color.White;
            this.lblCaption.Location = new System.Drawing.Point(54, 21);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(257, 29);
            this.lblCaption.TabIndex = 188;
            this.lblCaption.Text = "Select Allocations to Remove";
            // 
            // frmMatrixDelAllocations
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(855, 470);
            this.Controls.Add(this.lblCaption);
            this.Controls.Add(this.dgvDocTypes);
            this.Controls.Add(this.lblDefinition);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.pictureBox2);
            this.MinimizeBox = false;
            this.Name = "frmMatrixDelAllocations";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmMatrixDelAllocations_Load);
            this.panBottom.ResumeLayout(false);
            this.panBottom.PerformLayout();
            this.panBottomRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvDocTypes)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblDefinition;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.DataGridView dgvDocTypes;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Label lblAllocatedDate;
        private System.Windows.Forms.Label lblAllocatedBy;
        private System.Windows.Forms.Panel panBottomRight;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOk;
    }
}