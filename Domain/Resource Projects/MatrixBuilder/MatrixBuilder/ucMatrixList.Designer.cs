namespace MatrixBuilder
{
    partial class ucMatrixList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMatrixList));
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblCaptionDescription = new System.Windows.Forms.Label();
            this.lblCaption = new System.Windows.Forms.Label();
            this.picLists = new System.Windows.Forms.PictureBox();
            this.splitContMain = new System.Windows.Forms.SplitContainer();
            this.panLeftTop = new System.Windows.Forms.Panel();
            this.lblSelList = new System.Windows.Forms.Label();
            this.panLeftBottom = new System.Windows.Forms.Panel();
            this.lblColumn = new System.Windows.Forms.Label();
            this.lstbList = new System.Windows.Forms.ListBox();
            this.panRightTop = new System.Windows.Forms.Panel();
            this.lblListItems = new System.Windows.Forms.Label();
            this.lstbListItems = new System.Windows.Forms.ListBox();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLists)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).BeginInit();
            this.splitContMain.Panel1.SuspendLayout();
            this.splitContMain.Panel2.SuspendLayout();
            this.splitContMain.SuspendLayout();
            this.panLeftTop.SuspendLayout();
            this.panLeftBottom.SuspendLayout();
            this.panRightTop.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.Controls.Add(this.picLists);
            this.panHeader.Controls.Add(this.lblCaptionDescription);
            this.panHeader.Controls.Add(this.lblCaption);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(795, 48);
            this.panHeader.TabIndex = 37;
            // 
            // lblCaptionDescription
            // 
            this.lblCaptionDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaptionDescription.ForeColor = System.Drawing.Color.White;
            this.lblCaptionDescription.Location = new System.Drawing.Point(114, 3);
            this.lblCaptionDescription.Name = "lblCaptionDescription";
            this.lblCaptionDescription.Size = new System.Drawing.Size(678, 42);
            this.lblCaptionDescription.TabIndex = 189;
            this.lblCaptionDescription.Text = "Select an List, next select  List Items (values) and then Drag-and-Drop the selec" +
    "ted List Item to a row in the below Matrix. The List item value will populate th" +
    "e designated column in that row.";
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.Color.White;
            this.lblCaption.Location = new System.Drawing.Point(47, 9);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(48, 25);
            this.lblCaption.TabIndex = 34;
            this.lblCaption.Text = "Lists";
            // 
            // picLists
            // 
            this.picLists.Image = ((System.Drawing.Image)(resources.GetObject("picLists.Image")));
            this.picLists.Location = new System.Drawing.Point(8, 4);
            this.picLists.Name = "picLists";
            this.picLists.Size = new System.Drawing.Size(38, 38);
            this.picLists.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picLists.TabIndex = 190;
            this.picLists.TabStop = false;
            // 
            // splitContMain
            // 
            this.splitContMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContMain.Location = new System.Drawing.Point(0, 48);
            this.splitContMain.Name = "splitContMain";
            // 
            // splitContMain.Panel1
            // 
            this.splitContMain.Panel1.Controls.Add(this.lstbList);
            this.splitContMain.Panel1.Controls.Add(this.panLeftBottom);
            this.splitContMain.Panel1.Controls.Add(this.panLeftTop);
            // 
            // splitContMain.Panel2
            // 
            this.splitContMain.Panel2.Controls.Add(this.lstbListItems);
            this.splitContMain.Panel2.Controls.Add(this.panRightTop);
            this.splitContMain.Size = new System.Drawing.Size(795, 287);
            this.splitContMain.SplitterDistance = 265;
            this.splitContMain.TabIndex = 38;
            // 
            // panLeftTop
            // 
            this.panLeftTop.Controls.Add(this.lblSelList);
            this.panLeftTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panLeftTop.Location = new System.Drawing.Point(0, 0);
            this.panLeftTop.Name = "panLeftTop";
            this.panLeftTop.Size = new System.Drawing.Size(265, 35);
            this.panLeftTop.TabIndex = 1;
            // 
            // lblSelList
            // 
            this.lblSelList.AutoSize = true;
            this.lblSelList.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelList.ForeColor = System.Drawing.Color.White;
            this.lblSelList.Location = new System.Drawing.Point(9, 8);
            this.lblSelList.Name = "lblSelList";
            this.lblSelList.Size = new System.Drawing.Size(87, 20);
            this.lblSelList.TabIndex = 188;
            this.lblSelList.Text = "Select a List";
            // 
            // panLeftBottom
            // 
            this.panLeftBottom.Controls.Add(this.lblColumn);
            this.panLeftBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panLeftBottom.Location = new System.Drawing.Point(0, 251);
            this.panLeftBottom.Name = "panLeftBottom";
            this.panLeftBottom.Size = new System.Drawing.Size(265, 36);
            this.panLeftBottom.TabIndex = 3;
            // 
            // lblColumn
            // 
            this.lblColumn.AutoSize = true;
            this.lblColumn.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColumn.ForeColor = System.Drawing.Color.White;
            this.lblColumn.Location = new System.Drawing.Point(10, 8);
            this.lblColumn.Name = "lblColumn";
            this.lblColumn.Size = new System.Drawing.Size(55, 17);
            this.lblColumn.TabIndex = 188;
            this.lblColumn.Text = "Column:";
            // 
            // lstbList
            // 
            this.lstbList.BackColor = System.Drawing.Color.Black;
            this.lstbList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbList.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbList.ForeColor = System.Drawing.Color.White;
            this.lstbList.FormattingEnabled = true;
            this.lstbList.ItemHeight = 20;
            this.lstbList.Location = new System.Drawing.Point(0, 35);
            this.lstbList.Name = "lstbList";
            this.lstbList.Size = new System.Drawing.Size(265, 216);
            this.lstbList.TabIndex = 4;
            this.lstbList.SelectedIndexChanged += new System.EventHandler(this.lstbList_SelectedIndexChanged);
            // 
            // panRightTop
            // 
            this.panRightTop.Controls.Add(this.lblListItems);
            this.panRightTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panRightTop.Location = new System.Drawing.Point(0, 0);
            this.panRightTop.Name = "panRightTop";
            this.panRightTop.Size = new System.Drawing.Size(526, 35);
            this.panRightTop.TabIndex = 2;
            // 
            // lblListItems
            // 
            this.lblListItems.AutoSize = true;
            this.lblListItems.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListItems.ForeColor = System.Drawing.Color.White;
            this.lblListItems.Location = new System.Drawing.Point(9, 8);
            this.lblListItems.Name = "lblListItems";
            this.lblListItems.Size = new System.Drawing.Size(71, 20);
            this.lblListItems.TabIndex = 188;
            this.lblListItems.Text = "List Items";
            // 
            // lstbListItems
            // 
            this.lstbListItems.BackColor = System.Drawing.Color.Black;
            this.lstbListItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbListItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbListItems.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbListItems.ForeColor = System.Drawing.Color.White;
            this.lstbListItems.FormattingEnabled = true;
            this.lstbListItems.ItemHeight = 17;
            this.lstbListItems.Location = new System.Drawing.Point(0, 35);
            this.lstbListItems.Name = "lstbListItems";
            this.lstbListItems.Size = new System.Drawing.Size(526, 252);
            this.lstbListItems.TabIndex = 5;
            this.lstbListItems.SelectedIndexChanged += new System.EventHandler(this.lstbListItems_SelectedIndexChanged);
            this.lstbListItems.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstbListItems_MouseDown);
            // 
            // ucMatrixList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.splitContMain);
            this.Controls.Add(this.panHeader);
            this.Name = "ucMatrixList";
            this.Size = new System.Drawing.Size(795, 335);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picLists)).EndInit();
            this.splitContMain.Panel1.ResumeLayout(false);
            this.splitContMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContMain)).EndInit();
            this.splitContMain.ResumeLayout(false);
            this.panLeftTop.ResumeLayout(false);
            this.panLeftTop.PerformLayout();
            this.panLeftBottom.ResumeLayout(false);
            this.panLeftBottom.PerformLayout();
            this.panRightTop.ResumeLayout(false);
            this.panRightTop.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblCaptionDescription;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.PictureBox picLists;
        private System.Windows.Forms.SplitContainer splitContMain;
        private System.Windows.Forms.Panel panLeftTop;
        private System.Windows.Forms.Label lblSelList;
        private System.Windows.Forms.ListBox lstbList;
        private System.Windows.Forms.Panel panLeftBottom;
        private System.Windows.Forms.Label lblColumn;
        private System.Windows.Forms.ListBox lstbListItems;
        private System.Windows.Forms.Panel panRightTop;
        private System.Windows.Forms.Label lblListItems;
    }
}
