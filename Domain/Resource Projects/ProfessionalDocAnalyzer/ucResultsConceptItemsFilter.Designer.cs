namespace ProfessionalDocAnalyzer
{
    partial class ucResultsConceptItemsFilter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucResultsConceptItemsFilter));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle5 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle6 = new System.Windows.Forms.DataGridViewCellStyle();
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblFound = new System.Windows.Forms.Label();
            this.butRefreshFind = new MetroFramework.Controls.MetroButton();
            this.lblHeaderCaption = new System.Windows.Forms.Label();
            this.dvgConceptItems = new System.Windows.Forms.DataGridView();
            this.butFilter = new MetroFramework.Controls.MetroButton();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgConceptItems)).BeginInit();
            this.SuspendLayout();
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
            this.panHeader.Size = new System.Drawing.Size(692, 31);
            this.panHeader.TabIndex = 16;
            // 
            // lblFound
            // 
            this.lblFound.AutoSize = true;
            this.lblFound.BackColor = System.Drawing.Color.Transparent;
            this.lblFound.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblFound.ForeColor = System.Drawing.Color.Black;
            this.lblFound.Location = new System.Drawing.Point(250, 7);
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
            this.butRefreshFind.Location = new System.Drawing.Point(210, 2);
            this.butRefreshFind.Name = "butRefreshFind";
            this.butRefreshFind.Size = new System.Drawing.Size(28, 28);
            this.butRefreshFind.TabIndex = 183;
            this.butRefreshFind.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butRefreshFind.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butRefreshFind.UseSelectable = true;
            this.butRefreshFind.Visible = false;
            this.butRefreshFind.Click += new System.EventHandler(this.butRefreshFind_Click);
            // 
            // lblHeaderCaption
            // 
            this.lblHeaderCaption.AutoSize = true;
            this.lblHeaderCaption.BackColor = System.Drawing.Color.Transparent;
            this.lblHeaderCaption.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCaption.ForeColor = System.Drawing.Color.Black;
            this.lblHeaderCaption.Location = new System.Drawing.Point(4, 4);
            this.lblHeaderCaption.Name = "lblHeaderCaption";
            this.lblHeaderCaption.Size = new System.Drawing.Size(109, 21);
            this.lblHeaderCaption.TabIndex = 1;
            this.lblHeaderCaption.Text = "Concept Items";
            // 
            // dvgConceptItems
            // 
            this.dvgConceptItems.AllowUserToAddRows = false;
            this.dvgConceptItems.AllowUserToDeleteRows = false;
            this.dvgConceptItems.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgConceptItems.BackgroundColor = System.Drawing.Color.White;
            this.dvgConceptItems.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle4.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle4.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle4.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle4.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgConceptItems.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle4;
            this.dvgConceptItems.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgConceptItems.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle5.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle5.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle5.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvgConceptItems.DefaultCellStyle = dataGridViewCellStyle5;
            this.dvgConceptItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dvgConceptItems.EditMode = System.Windows.Forms.DataGridViewEditMode.EditOnKeystroke;
            this.dvgConceptItems.Location = new System.Drawing.Point(0, 31);
            this.dvgConceptItems.MultiSelect = false;
            this.dvgConceptItems.Name = "dvgConceptItems";
            this.dvgConceptItems.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle6.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle6.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle6.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle6.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle6.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgConceptItems.RowHeadersDefaultCellStyle = dataGridViewCellStyle6;
            this.dvgConceptItems.RowHeadersWidth = 5;
            this.dvgConceptItems.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgConceptItems.Size = new System.Drawing.Size(692, 369);
            this.dvgConceptItems.TabIndex = 18;
            // 
            // butFilter
            // 
            this.butFilter.Location = new System.Drawing.Point(134, 4);
            this.butFilter.Name = "butFilter";
            this.butFilter.Size = new System.Drawing.Size(70, 23);
            this.butFilter.TabIndex = 186;
            this.butFilter.Text = "Filter";
            this.butFilter.UseSelectable = true;
            this.butFilter.Click += new System.EventHandler(this.butFilter_Click);
            // 
            // ucResultsConceptItemsFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.dvgConceptItems);
            this.Controls.Add(this.panHeader);
            this.Name = "ucResultsConceptItemsFilter";
            this.Size = new System.Drawing.Size(692, 400);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgConceptItems)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblFound;
        private MetroFramework.Controls.MetroButton butRefreshFind;
        private System.Windows.Forms.Label lblHeaderCaption;
        private System.Windows.Forms.DataGridView dvgConceptItems;
        private MetroFramework.Controls.MetroButton butFilter;
    }
}
