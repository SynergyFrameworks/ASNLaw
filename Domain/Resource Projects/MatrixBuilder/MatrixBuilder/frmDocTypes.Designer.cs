namespace MatrixBuilder
{
    partial class frmDocTypes
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDocTypes));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.panHeader = new System.Windows.Forms.Panel();
            this.panBottom = new System.Windows.Forms.Panel();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOk = new MetroFramework.Controls.MetroButton();
            this.panMain = new System.Windows.Forms.Panel();
            this.txtbDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblBatchInstructions = new System.Windows.Forms.Label();
            this.dvgDocTypes = new System.Windows.Forms.DataGridView();
            this.txtbDocTypeName = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.butImportFile = new MetroFramework.Controls.MetroButton();
            this.butAddBatch = new MetroFramework.Controls.MetroButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.label3 = new System.Windows.Forms.Label();
            this.txtDefinition = new System.Windows.Forms.TextBox();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.butReplace = new MetroFramework.Controls.MetroButton();
            this.butNew = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtAcron = new System.Windows.Forms.TextBox();
            this.lblList_Definition = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.panBottom.SuspendLayout();
            this.panMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgDocTypes)).BeginInit();
            this.metroTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(10, 10);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 172;
            this.pictureBox2.TabStop = false;
            // 
            // panHeader
            // 
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(10, 60);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(1081, 12);
            this.panHeader.TabIndex = 173;
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.butCancel);
            this.panBottom.Controls.Add(this.butOk);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(10, 452);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(1081, 47);
            this.panBottom.TabIndex = 175;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(977, 12);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(885, 12);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 0;
            this.butOk.Text = "Save";
            this.butOk.UseSelectable = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // panMain
            // 
            this.panMain.Controls.Add(this.txtbDescription);
            this.panMain.Controls.Add(this.label2);
            this.panMain.Controls.Add(this.lblBatchInstructions);
            this.panMain.Controls.Add(this.dvgDocTypes);
            this.panMain.Controls.Add(this.txtbDocTypeName);
            this.panMain.Controls.Add(this.label4);
            this.panMain.Controls.Add(this.metroTabControl1);
            this.panMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panMain.Location = new System.Drawing.Point(10, 72);
            this.panMain.Name = "panMain";
            this.panMain.Size = new System.Drawing.Size(1081, 380);
            this.panMain.TabIndex = 176;
            // 
            // txtbDescription
            // 
            this.txtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDescription.Location = new System.Drawing.Point(14, 317);
            this.txtbDescription.MaxLength = 100;
            this.txtbDescription.Multiline = true;
            this.txtbDescription.Name = "txtbDescription";
            this.txtbDescription.Size = new System.Drawing.Size(506, 53);
            this.txtbDescription.TabIndex = 184;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(15, 294);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 185;
            this.label2.Text = "Description";
            // 
            // lblBatchInstructions
            // 
            this.lblBatchInstructions.BackColor = System.Drawing.Color.Transparent;
            this.lblBatchInstructions.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblBatchInstructions.ForeColor = System.Drawing.Color.White;
            this.lblBatchInstructions.Location = new System.Drawing.Point(552, 16);
            this.lblBatchInstructions.Name = "lblBatchInstructions";
            this.lblBatchInstructions.Size = new System.Drawing.Size(522, 69);
            this.lblBatchInstructions.TabIndex = 172;
            this.lblBatchInstructions.Text = "Batch Load Instructions";
            // 
            // dvgDocTypes
            // 
            this.dvgDocTypes.AllowUserToAddRows = false;
            this.dvgDocTypes.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dvgDocTypes.BackgroundColor = System.Drawing.Color.Black;
            this.dvgDocTypes.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.Control;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgDocTypes.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dvgDocTypes.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dvgDocTypes.Cursor = System.Windows.Forms.Cursors.Hand;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dvgDocTypes.DefaultCellStyle = dataGridViewCellStyle2;
            this.dvgDocTypes.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dvgDocTypes.Location = new System.Drawing.Point(14, 60);
            this.dvgDocTypes.MultiSelect = false;
            this.dvgDocTypes.Name = "dvgDocTypes";
            this.dvgDocTypes.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dvgDocTypes.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dvgDocTypes.RowHeadersWidth = 5;
            this.dvgDocTypes.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dvgDocTypes.ShowEditingIcon = false;
            this.dvgDocTypes.Size = new System.Drawing.Size(506, 222);
            this.dvgDocTypes.TabIndex = 88;
            // 
            // txtbDocTypeName
            // 
            this.txtbDocTypeName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDocTypeName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDocTypeName.Location = new System.Drawing.Point(12, 20);
            this.txtbDocTypeName.MaxLength = 50;
            this.txtbDocTypeName.Name = "txtbDocTypeName";
            this.txtbDocTypeName.Size = new System.Drawing.Size(292, 25);
            this.txtbDocTypeName.TabIndex = 86;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(13, -1);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(119, 20);
            this.label4.TabIndex = 87;
            this.label4.Text = "Document Types";
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.metroTabControl1.Controls.Add(this.tabPage1);
            this.metroTabControl1.Controls.Add(this.tabPage2);
            this.metroTabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Regular;
            this.metroTabControl1.Location = new System.Drawing.Point(548, 88);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(530, 286);
            this.metroTabControl1.TabIndex = 81;
            this.metroTabControl1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroTabControl1.UseSelectable = true;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.Color.Black;
            this.tabPage1.Controls.Add(this.butImportFile);
            this.tabPage1.Controls.Add(this.butAddBatch);
            this.tabPage1.Controls.Add(this.label5);
            this.tabPage1.Controls.Add(this.txtBatch);
            this.tabPage1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(522, 244);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Batch Load";
            // 
            // butImportFile
            // 
            this.butImportFile.Location = new System.Drawing.Point(249, 208);
            this.butImportFile.Name = "butImportFile";
            this.butImportFile.Size = new System.Drawing.Size(75, 23);
            this.butImportFile.TabIndex = 77;
            this.butImportFile.Text = "Import File";
            this.butImportFile.UseSelectable = true;
            this.butImportFile.Visible = false;
            // 
            // butAddBatch
            // 
            this.butAddBatch.Location = new System.Drawing.Point(417, 208);
            this.butAddBatch.Name = "butAddBatch";
            this.butAddBatch.Size = new System.Drawing.Size(75, 23);
            this.butAddBatch.TabIndex = 71;
            this.butAddBatch.Text = "Add";
            this.butAddBatch.UseSelectable = true;
            this.butAddBatch.Click += new System.EventHandler(this.butAddBatch_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(12, 9);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(156, 17);
            this.label5.TabIndex = 60;
            this.label5.Text = "Delimited Documet Types";
            // 
            // txtBatch
            // 
            this.txtBatch.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtBatch.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatch.Location = new System.Drawing.Point(15, 31);
            this.txtBatch.Multiline = true;
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtBatch.Size = new System.Drawing.Size(491, 159);
            this.txtBatch.TabIndex = 59;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.Color.Black;
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.txtDefinition);
            this.tabPage2.Controls.Add(this.butDelete);
            this.tabPage2.Controls.Add(this.butReplace);
            this.tabPage2.Controls.Add(this.butNew);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtAcron);
            this.tabPage2.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(522, 244);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Maintain Document Types";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(104, 65);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(74, 17);
            this.label3.TabIndex = 76;
            this.label3.Text = "Description";
            // 
            // txtDefinition
            // 
            this.txtDefinition.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtDefinition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDefinition.Location = new System.Drawing.Point(107, 87);
            this.txtDefinition.Name = "txtDefinition";
            this.txtDefinition.Size = new System.Drawing.Size(390, 25);
            this.txtDefinition.TabIndex = 75;
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(8, 94);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(75, 23);
            this.butDelete.TabIndex = 74;
            this.butDelete.Text = "Delete";
            this.butDelete.UseSelectable = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // butReplace
            // 
            this.butReplace.Location = new System.Drawing.Point(8, 62);
            this.butReplace.Name = "butReplace";
            this.butReplace.Size = new System.Drawing.Size(75, 23);
            this.butReplace.TabIndex = 73;
            this.butReplace.Text = "Replace";
            this.butReplace.UseSelectable = true;
            this.butReplace.Click += new System.EventHandler(this.butReplace_Click);
            // 
            // butNew
            // 
            this.butNew.Location = new System.Drawing.Point(8, 30);
            this.butNew.Name = "butNew";
            this.butNew.Size = new System.Drawing.Size(75, 23);
            this.butNew.TabIndex = 72;
            this.butNew.Text = "Add";
            this.butNew.UseSelectable = true;
            this.butNew.Click += new System.EventHandler(this.butNew_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(98, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 17);
            this.label1.TabIndex = 57;
            this.label1.Text = "Document Type";
            // 
            // txtAcron
            // 
            this.txtAcron.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtAcron.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAcron.Location = new System.Drawing.Point(101, 29);
            this.txtAcron.Name = "txtAcron";
            this.txtAcron.Size = new System.Drawing.Size(148, 25);
            this.txtAcron.TabIndex = 56;
            // 
            // lblList_Definition
            // 
            this.lblList_Definition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblList_Definition.ForeColor = System.Drawing.Color.White;
            this.lblList_Definition.Location = new System.Drawing.Point(252, 14);
            this.lblList_Definition.Name = "lblList_Definition";
            this.lblList_Definition.Size = new System.Drawing.Size(836, 45);
            this.lblList_Definition.TabIndex = 177;
            this.lblList_Definition.Text = resources.GetString("lblList_Definition.Text");
            // 
            // frmDocTypes
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1101, 509);
            this.ControlBox = false;
            this.Controls.Add(this.lblList_Definition);
            this.Controls.Add(this.panMain);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.pictureBox2);
            this.Controls.Add(this.panHeader);
            this.MaximizeBox = false;
            this.Name = "frmDocTypes";
            this.Padding = new System.Windows.Forms.Padding(10, 60, 10, 10);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "     Document Types";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.Load += new System.EventHandler(this.frmDocTypes_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.panBottom.ResumeLayout(false);
            this.panMain.ResumeLayout(false);
            this.panMain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dvgDocTypes)).EndInit();
            this.metroTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.Panel panMain;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOk;
        private System.Windows.Forms.Label lblBatchInstructions;
        private System.Windows.Forms.DataGridView dvgDocTypes;
        private System.Windows.Forms.TextBox txtbDocTypeName;
        private System.Windows.Forms.Label label4;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private MetroFramework.Controls.MetroButton butImportFile;
        private MetroFramework.Controls.MetroButton butAddBatch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtDefinition;
        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Controls.MetroButton butReplace;
        private MetroFramework.Controls.MetroButton butNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtAcron;
        private System.Windows.Forms.Label lblList_Definition;
        private System.Windows.Forms.TextBox txtbDescription;
        private System.Windows.Forms.Label label2;
    }
}