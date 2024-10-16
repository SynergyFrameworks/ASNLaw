namespace ProfessionalDocAnalyzer
{
    partial class ucResultsDicItemsFilter
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucResultsDicItemsFilter));
            this.lblHeaderCaption = new System.Windows.Forms.Label();
            this.dvgDicItems = new System.Windows.Forms.DataGridView();
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblFound = new System.Windows.Forms.Label();
            this.butRefreshFind = new MetroFramework.Controls.MetroButton();
            this.butFilter = new MetroFramework.Controls.MetroButton();
            ((System.ComponentModel.ISupportInitialize)(this.dvgDicItems)).BeginInit();
            this.panHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblHeaderCaption
            // 
            this.lblHeaderCaption.AutoSize = true;
            this.lblHeaderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblHeaderCaption.Location = new System.Drawing.Point(4, 7);
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            this.lblHeaderCaption.Size = new System.Drawing.Size(123, 21);
            this.lblHeaderCaption.TabIndex = 1;
            this.lblHeaderCaption.Text = "Dictionary Items";
            // 
            // dvgDicItems
            // 
            this.dvgDicItems.AllowUserToAddRows = false;
            this.dvgDicItems.AllowUserToDeleteRows = false;
            this.dvgDicItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgDicItems.BackgroundColor = System.Drawing.Color.Black;
            this.dvgDicItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgDicItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvgDicItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgDicItems.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvgDicItems.DefaultCellStyle = dataGridViewCellStyle2;
            this.dvgDicItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgDicItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dvgDicItems.Location = new System.Drawing.Point(0, 37);
            this.dvgDicItems.MultiSelect = false;
            this.dvgDicItems.Name = "dvgDicItems";
            this.dvgDicItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgDicItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dvgDicItems.RowHeadersWidth = 5;
            this.dvgDicItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgDicItems.Size = new System.Drawing.Size(692, 363);
            this.dvgDicItems.TabIndex = 17;
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.PowderBlue;
            this.panHeader.Controls.Add(this.butFilter);
            this.panHeader.Controls.Add(this.lblFound);
            this.panHeader.Controls.Add(this.butRefreshFind);
            this.panHeader.Controls.Add(this.lblHeaderCaption);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(692, 37);
            this.panHeader.TabIndex = 15;
            // 
            // lblFound
            // 
            this.lblFound.AutoSize = true;
            this.lblFound.BackColor = System.Drawing.Color.Transparent;
            this.lblFound.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFound.ForeColor = System.Drawing.Color.Black;
            this.lblFound.Location = new System.Drawing.Point(250, 10);
            this.lblFound.Name = "lblFound";
            this.lblFound.Size = new System.Drawing.Size(44, 17);
            this.lblFound.TabIndex = 185;
            this.lblFound.Text = "Found";
            this.lblFound.Visible = false;
            // 
            // butRefreshFind
            // 
            this.butRefreshFind.BackColor = System.Drawing.Color.Black;
            this.butRefreshFind.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butRefreshFind.BackgroundImage")));
            this.butRefreshFind.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butRefreshFind.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butRefreshFind.ForeColor = System.Drawing.Color.White;
            this.butRefreshFind.Location = new System.Drawing.Point(210, 5);
            this.butRefreshFind.Name = "butRefreshFind";
            this.butRefreshFind.Size = new System.Drawing.Size(28, 28);
            this.butRefreshFind.TabIndex = 183;
            this.butRefreshFind.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butRefreshFind.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butRefreshFind.UseSelectable = true;
            this.butRefreshFind.Visible = false;
            this.butRefreshFind.Click += new System.EventHandler(this.butRefreshFind_Click);
            // 
            // butFilter
            // 
            this.butFilter.Location = new System.Drawing.Point(133, 7);
            this.butFilter.Name = "butFilter";
            this.butFilter.Size = new System.Drawing.Size(70, 23);
            this.butFilter.TabIndex = 187;
            this.butFilter.Text = "Filter";
            this.butFilter.UseSelectable = true;
            this.butFilter.Click += new System.EventHandler(this.butFilter_Click);
            // 
            // ucResultsDicItemsFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.dvgDicItems);
            this.Controls.Add(this.panHeader);
            this.Name = "ucResultsDicItemsFilter";
            this.Size = new System.Drawing.Size(692, 400);
            ((System.ComponentModel.ISupportInitialize)(this.dvgDicItems)).EndInit();
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblHeaderCaption;
        private System.Windows.Forms.DataGridView dvgDicItems;
        private System.Windows.Forms.Panel panHeader;
        private MetroFramework.Controls.MetroButton butRefreshFind;
        private System.Windows.Forms.Label lblFound;
        private MetroFramework.Controls.MetroButton butFilter;
    }
}
