namespace ProfessionalDocAnalyzer
{
    partial class ucSettings
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
            this.panHeader = new System.Windows.Forms.Panel();
            this.panHeaderRight = new System.Windows.Forms.Panel();
            this.butDefaultReset = new MetroFramework.Controls.MetroButton();
            this.lblSelected = new System.Windows.Forms.Label();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panRight = new System.Windows.Forms.Panel();
            this.lstbSettings = new System.Windows.Forms.ListBox();
            this.ucQCSettings1 = new ProfessionalDocAnalyzer.ucQCSettings();
            this.ucExcelTemplates1 = new ProfessionalDocAnalyzer.ucExcelTemplates();
            this.ucRAMModels1 = new ProfessionalDocAnalyzer.ucRAMModels();
            this.ucTasksSettings1 = new ProfessionalDocAnalyzer.ucTasksSettings();
            this.ucSettings_Home1 = new ProfessionalDocAnalyzer.ucSettings_Home();
            this.ucAcroDictionaries1 = new ProfessionalDocAnalyzer.ucAcroDictionaries();
            this.ucDictionaries1 = new ProfessionalDocAnalyzer.ucDictionaries();
            this.ucKeywordGroups1 = new ProfessionalDocAnalyzer.ucKeywordGroups();
            this.panHeader.SuspendLayout();
            this.panHeaderRight.SuspendLayout();
            this.panRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.White;
            this.panHeader.Controls.Add(this.panHeaderRight);
            this.panHeader.Controls.Add(this.lblSelected);
            this.panHeader.Controls.Add(this.txtbMessage);
            this.panHeader.Controls.Add(this.lblMessage);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.ForeColor = System.Drawing.Color.White;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(1348, 50);
            this.panHeader.TabIndex = 6;
            // 
            // panHeaderRight
            // 
            this.panHeaderRight.Controls.Add(this.butDefaultReset);
            this.panHeaderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panHeaderRight.Location = new System.Drawing.Point(1156, 0);
            this.panHeaderRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panHeaderRight.Name = "panHeaderRight";
            this.panHeaderRight.Size = new System.Drawing.Size(192, 50);
            this.panHeaderRight.TabIndex = 65;
            // 
            // butDefaultReset
            // 
            this.butDefaultReset.Location = new System.Drawing.Point(4, 11);
            this.butDefaultReset.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butDefaultReset.Name = "butDefaultReset";
            this.butDefaultReset.Size = new System.Drawing.Size(167, 28);
            this.butDefaultReset.TabIndex = 64;
            this.butDefaultReset.Text = "Reset Default Settings";
            this.butDefaultReset.UseSelectable = true;
            this.butDefaultReset.Click += new System.EventHandler(this.butDefaultReset_Click);
            // 
            // lblSelected
            // 
            this.lblSelected.AutoSize = true;
            this.lblSelected.BackColor = System.Drawing.Color.White;
            this.lblSelected.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSelected.ForeColor = System.Drawing.Color.Black;
            this.lblSelected.Location = new System.Drawing.Point(116, 11);
            this.lblSelected.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblSelected.Name = "lblSelected";
            this.lblSelected.Size = new System.Drawing.Size(0, 32);
            this.lblSelected.TabIndex = 19;
            // 
            // txtbMessage
            // 
            this.txtbMessage.BackColor = System.Drawing.Color.White;
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Enabled = false;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.Blue;
            this.txtbMessage.Location = new System.Drawing.Point(144, 17);
            this.txtbMessage.Margin = new System.Windows.Forms.Padding(40, 4, 40, 4);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(812, 22);
            this.txtbMessage.TabIndex = 18;
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(296, 7);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 17);
            this.lblMessage.TabIndex = 17;
            this.lblMessage.Visible = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(4, 7);
            this.lblHeader.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(112, 37);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Settings";
            // 
            // panRight
            // 
            this.panRight.Controls.Add(this.lstbSettings);
            this.panRight.Dock = System.Windows.Forms.DockStyle.Left;
            this.panRight.Location = new System.Drawing.Point(0, 50);
            this.panRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panRight.Name = "panRight";
            this.panRight.Size = new System.Drawing.Size(267, 639);
            this.panRight.TabIndex = 7;
            // 
            // lstbSettings
            // 
            this.lstbSettings.BackColor = System.Drawing.Color.Black;
            this.lstbSettings.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbSettings.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lstbSettings.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbSettings.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbSettings.ForeColor = System.Drawing.Color.White;
            this.lstbSettings.FormattingEnabled = true;
            this.lstbSettings.ItemHeight = 28;
            this.lstbSettings.Location = new System.Drawing.Point(0, 0);
            this.lstbSettings.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.lstbSettings.Name = "lstbSettings";
            this.lstbSettings.Size = new System.Drawing.Size(267, 639);
            this.lstbSettings.Sorted = true;
            this.lstbSettings.TabIndex = 3;
            this.lstbSettings.SelectedIndexChanged += new System.EventHandler(this.lstbSettings_SelectedIndexChanged);
            // 
            // ucQCSettings1
            // 
            this.ucQCSettings1.BackColor = System.Drawing.Color.White;
            this.ucQCSettings1.Location = new System.Drawing.Point(1043, 78);
            this.ucQCSettings1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucQCSettings1.Name = "ucQCSettings1";
            this.ucQCSettings1.Size = new System.Drawing.Size(988, 540);
            this.ucQCSettings1.TabIndex = 16;
            this.ucQCSettings1.Visible = false;
            // 
            // ucExcelTemplates1
            // 
            this.ucExcelTemplates1.BackColor = System.Drawing.Color.White;
            this.ucExcelTemplates1.Location = new System.Drawing.Point(977, 117);
            this.ucExcelTemplates1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucExcelTemplates1.Name = "ucExcelTemplates1";
            this.ucExcelTemplates1.Size = new System.Drawing.Size(1095, 529);
            this.ucExcelTemplates1.TabIndex = 15;
            this.ucExcelTemplates1.Visible = false;
            // 
            // ucRAMModels1
            // 
            this.ucRAMModels1.BackColor = System.Drawing.Color.White;
            this.ucRAMModels1.Location = new System.Drawing.Point(892, 142);
            this.ucRAMModels1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucRAMModels1.Name = "ucRAMModels1";
            this.ucRAMModels1.Size = new System.Drawing.Size(1005, 530);
            this.ucRAMModels1.TabIndex = 14;
            this.ucRAMModels1.Visible = false;
            // 
            // ucTasksSettings1
            // 
            this.ucTasksSettings1.Location = new System.Drawing.Point(787, 177);
            this.ucTasksSettings1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucTasksSettings1.Name = "ucTasksSettings1";
            this.ucTasksSettings1.Size = new System.Drawing.Size(745, 556);
            this.ucTasksSettings1.TabIndex = 13;
            this.ucTasksSettings1.Visible = false;
            // 
            // ucSettings_Home1
            // 
            this.ucSettings_Home1.BackColor = System.Drawing.Color.White;
            this.ucSettings_Home1.Location = new System.Drawing.Point(541, 236);
            this.ucSettings_Home1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucSettings_Home1.Name = "ucSettings_Home1";
            this.ucSettings_Home1.Size = new System.Drawing.Size(928, 465);
            this.ucSettings_Home1.TabIndex = 11;
            this.ucSettings_Home1.Visible = false;
            // 
            // ucAcroDictionaries1
            // 
            this.ucAcroDictionaries1.Location = new System.Drawing.Point(457, 282);
            this.ucAcroDictionaries1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucAcroDictionaries1.Name = "ucAcroDictionaries1";
            this.ucAcroDictionaries1.Size = new System.Drawing.Size(1220, 522);
            this.ucAcroDictionaries1.TabIndex = 10;
            this.ucAcroDictionaries1.Visible = false;
            // 
            // ucDictionaries1
            // 
            this.ucDictionaries1.BackColor = System.Drawing.Color.White;
            this.ucDictionaries1.Location = new System.Drawing.Point(388, 361);
            this.ucDictionaries1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucDictionaries1.Name = "ucDictionaries1";
            this.ucDictionaries1.Size = new System.Drawing.Size(1233, 649);
            this.ucDictionaries1.TabIndex = 9;
            this.ucDictionaries1.Visible = false;
            // 
            // ucKeywordGroups1
            // 
            this.ucKeywordGroups1.BackColor = System.Drawing.Color.White;
            this.ucKeywordGroups1.Location = new System.Drawing.Point(316, 437);
            this.ucKeywordGroups1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucKeywordGroups1.Name = "ucKeywordGroups1";
            this.ucKeywordGroups1.Size = new System.Drawing.Size(983, 618);
            this.ucKeywordGroups1.TabIndex = 8;
            this.ucKeywordGroups1.Visible = false;
            // 
            // ucSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.ucQCSettings1);
            this.Controls.Add(this.ucExcelTemplates1);
            this.Controls.Add(this.ucRAMModels1);
            this.Controls.Add(this.ucTasksSettings1);
            this.Controls.Add(this.ucSettings_Home1);
            this.Controls.Add(this.ucAcroDictionaries1);
            this.Controls.Add(this.ucDictionaries1);
            this.Controls.Add(this.ucKeywordGroups1);
            this.Controls.Add(this.panRight);
            this.Controls.Add(this.panHeader);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "ucSettings";
            this.Size = new System.Drawing.Size(1348, 689);
            this.VisibleChanged += new System.EventHandler(this.ucSettings_VisibleChanged);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panHeaderRight.ResumeLayout(false);
            this.panRight.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblSelected;
        private System.Windows.Forms.TextBox txtbMessage;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panRight;
        private System.Windows.Forms.ListBox lstbSettings;
        private ucKeywordGroups ucKeywordGroups1;
        private ucDictionaries ucDictionaries1;
        private ucAcroDictionaries ucAcroDictionaries1;
        private ucSettings_Home ucSettings_Home1;
     //   private ucQuestionsInstructions ucQuestionsInstructions1;
        private ucTasksSettings ucTasksSettings1;
        private ucRAMModels ucRAMModels1;
        private ucExcelTemplates ucExcelTemplates1;
        private ucQCSettings ucQCSettings1;
        private System.Windows.Forms.Panel panHeaderRight;
        private MetroFramework.Controls.MetroButton butDefaultReset;
    }
}
