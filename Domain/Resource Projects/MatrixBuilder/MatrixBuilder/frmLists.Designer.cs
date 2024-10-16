namespace MatrixBuilder
{
    partial class frmLists
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLists));
            this.panBottom = new System.Windows.Forms.Panel();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOk = new MetroFramework.Controls.MetroButton();
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblList_Definition = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lstbListItems = new System.Windows.Forms.ListBox();
            this.metroTabControl1 = new MetroFramework.Controls.MetroTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.butImportFile = new MetroFramework.Controls.MetroButton();
            this.butAddBatch = new MetroFramework.Controls.MetroButton();
            this.label5 = new System.Windows.Forms.Label();
            this.txtBatch = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.butReplace = new MetroFramework.Controls.MetroButton();
            this.butNew = new MetroFramework.Controls.MetroButton();
            this.label1 = new System.Windows.Forms.Label();
            this.txtItem = new System.Windows.Forms.TextBox();
            this.txtbListName = new System.Windows.Forms.TextBox();
            this.lblListName = new System.Windows.Forms.Label();
            this.txtbDescription = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.lblMessage = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.panBottom.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.metroTabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.butCancel);
            this.panBottom.Controls.Add(this.butOk);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panBottom.Location = new System.Drawing.Point(10, 448);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(869, 47);
            this.panBottom.TabIndex = 176;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(767, 17);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(75, 23);
            this.butCancel.TabIndex = 1;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOk
            // 
            this.butOk.Location = new System.Drawing.Point(680, 17);
            this.butOk.Name = "butOk";
            this.butOk.Size = new System.Drawing.Size(75, 23);
            this.butOk.TabIndex = 0;
            this.butOk.Text = "Save";
            this.butOk.UseSelectable = true;
            this.butOk.Click += new System.EventHandler(this.butOk_Click);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblList_Definition);
            this.panel1.Controls.Add(this.lblHeader);
            this.panel1.Controls.Add(this.pictureBox2);
            this.panel1.Location = new System.Drawing.Point(10, 10);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(866, 59);
            this.panel1.TabIndex = 177;
            // 
            // lblList_Definition
            // 
            this.lblList_Definition.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblList_Definition.ForeColor = System.Drawing.Color.White;
            this.lblList_Definition.Location = new System.Drawing.Point(113, 21);
            this.lblList_Definition.Name = "lblList_Definition";
            this.lblList_Definition.Size = new System.Drawing.Size(749, 26);
            this.lblList_Definition.TabIndex = 175;
            this.lblList_Definition.Text = "A series of selectable values (e.g. names, statuses, weights, and grades) to be a" +
    "ssociated with a specified column in a matrix.";
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI Semibold", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(52, 14);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(45, 30);
            this.lblHeader.TabIndex = 174;
            this.lblHeader.Text = "List";
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(3, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(48, 48);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 173;
            this.pictureBox2.TabStop = false;
            // 
            // lstbListItems
            // 
            this.lstbListItems.BackColor = System.Drawing.Color.Black;
            this.lstbListItems.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbListItems.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbListItems.ForeColor = System.Drawing.Color.White;
            this.lstbListItems.FormattingEnabled = true;
            this.lstbListItems.ItemHeight = 20;
            this.lstbListItems.Location = new System.Drawing.Point(13, 143);
            this.lstbListItems.Margin = new System.Windows.Forms.Padding(30, 3, 3, 3);
            this.lstbListItems.Name = "lstbListItems";
            this.lstbListItems.Size = new System.Drawing.Size(292, 180);
            this.lstbListItems.TabIndex = 178;
            // 
            // metroTabControl1
            // 
            this.metroTabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.metroTabControl1.Controls.Add(this.tabPage1);
            this.metroTabControl1.Controls.Add(this.tabPage2);
            this.metroTabControl1.FontWeight = MetroFramework.MetroTabControlWeight.Regular;
            this.metroTabControl1.Location = new System.Drawing.Point(346, 143);
            this.metroTabControl1.Name = "metroTabControl1";
            this.metroTabControl1.SelectedIndex = 0;
            this.metroTabControl1.Size = new System.Drawing.Size(530, 286);
            this.metroTabControl1.TabIndex = 181;
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
            this.tabPage1.Location = new System.Drawing.Point(4, 38);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(522, 244);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Batch Load";
            // 
            // butImportFile
            // 
            this.butImportFile.Location = new System.Drawing.Point(427, 208);
            this.butImportFile.Name = "butImportFile";
            this.butImportFile.Size = new System.Drawing.Size(75, 23);
            this.butImportFile.TabIndex = 77;
            this.butImportFile.Text = "Import File";
            this.butImportFile.UseSelectable = true;
            this.butImportFile.Click += new System.EventHandler(this.butImportFile_Click);
            // 
            // butAddBatch
            // 
            this.butAddBatch.Location = new System.Drawing.Point(340, 208);
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
            this.label5.Size = new System.Drawing.Size(147, 17);
            this.label5.TabIndex = 60;
            this.label5.Text = "Comma Delimited Items";
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
            this.tabPage2.Controls.Add(this.butDelete);
            this.tabPage2.Controls.Add(this.butReplace);
            this.tabPage2.Controls.Add(this.butNew);
            this.tabPage2.Controls.Add(this.label1);
            this.tabPage2.Controls.Add(this.txtItem);
            this.tabPage2.Location = new System.Drawing.Point(4, 38);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Size = new System.Drawing.Size(522, 244);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Maintain Items";
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
            this.label1.Size = new System.Drawing.Size(56, 17);
            this.label1.TabIndex = 57;
            this.label1.Text = "List Item";
            // 
            // txtItem
            // 
            this.txtItem.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtItem.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtItem.Location = new System.Drawing.Point(101, 29);
            this.txtItem.Name = "txtItem";
            this.txtItem.Size = new System.Drawing.Size(311, 25);
            this.txtItem.TabIndex = 56;
            // 
            // txtbListName
            // 
            this.txtbListName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbListName.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbListName.Location = new System.Drawing.Point(13, 105);
            this.txtbListName.MaxLength = 50;
            this.txtbListName.Name = "txtbListName";
            this.txtbListName.Size = new System.Drawing.Size(292, 25);
            this.txtbListName.TabIndex = 179;
            // 
            // lblListName
            // 
            this.lblListName.AutoSize = true;
            this.lblListName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblListName.ForeColor = System.Drawing.Color.White;
            this.lblListName.Location = new System.Drawing.Point(14, 84);
            this.lblListName.Name = "lblListName";
            this.lblListName.Size = new System.Drawing.Size(75, 20);
            this.lblListName.TabIndex = 180;
            this.lblListName.Text = "List Name";
            // 
            // txtbDescription
            // 
            this.txtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDescription.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDescription.Location = new System.Drawing.Point(13, 358);
            this.txtbDescription.MaxLength = 100;
            this.txtbDescription.Multiline = true;
            this.txtbDescription.Name = "txtbDescription";
            this.txtbDescription.Size = new System.Drawing.Size(292, 84);
            this.txtbDescription.TabIndex = 182;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(13, 335);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(85, 20);
            this.label2.TabIndex = 183;
            this.label2.Text = "Description";
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // lblMessage
            // 
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.White;
            this.lblMessage.Location = new System.Drawing.Point(347, 92);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(509, 45);
            this.lblMessage.TabIndex = 184;
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // frmLists
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(889, 505);
            this.ControlBox = false;
            this.Controls.Add(this.lblMessage);
            this.Controls.Add(this.txtbDescription);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.metroTabControl1);
            this.Controls.Add(this.txtbListName);
            this.Controls.Add(this.lblListName);
            this.Controls.Add(this.lstbListItems);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panBottom);
            this.DisplayHeader = false;
            this.Name = "frmLists";
            this.Padding = new System.Windows.Forms.Padding(10, 30, 10, 10);
            this.Resizable = false;
            this.ShadowType = MetroFramework.Forms.MetroFormShadowType.AeroShadow;
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "frmLists";
            this.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.TopMost = true;
            this.panBottom.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.metroTabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panBottom;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOk;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.ListBox lstbListItems;
        private MetroFramework.Controls.MetroTabControl metroTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private MetroFramework.Controls.MetroButton butImportFile;
        private MetroFramework.Controls.MetroButton butAddBatch;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtBatch;
        private System.Windows.Forms.TabPage tabPage2;
        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Controls.MetroButton butReplace;
        private MetroFramework.Controls.MetroButton butNew;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtItem;
        private System.Windows.Forms.TextBox txtbListName;
        private System.Windows.Forms.Label lblListName;
        private System.Windows.Forms.TextBox txtbDescription;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblList_Definition;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
    }
}