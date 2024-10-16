namespace ProfessionalDocAnalyzer
{
    partial class frmTacksWorkflowConfig
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
            this.components = new System.ComponentModel.Container();
            this.panTop = new System.Windows.Forms.Panel();
            this.panErrors = new System.Windows.Forms.Panel();
            this.txtbErrors = new System.Windows.Forms.TextBox();
            this.panErrorHeader = new System.Windows.Forms.Panel();
            this.lblErrors = new System.Windows.Forms.Label();
            this.lblShortTaskName = new System.Windows.Forms.Label();
            this.txtbShortTaskName = new System.Windows.Forms.TextBox();
            this.lblDescription = new System.Windows.Forms.Label();
            this.txtbDescription = new System.Windows.Forms.TextBox();
            this.lblTaskName = new System.Windows.Forms.Label();
            this.txtbTaskName = new System.Windows.Forms.TextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panFooter = new System.Windows.Forms.Panel();
            this.panFooterRight = new System.Windows.Forms.Panel();
            this.chkbShow = new System.Windows.Forms.CheckBox();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.panBottom = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ucAttributes1 = new ProfessionalDocAnalyzer.ucParse_Attributes();
            this.ucAttributes2 = new ProfessionalDocAnalyzer.ucParse_Attributes();
            this.ucAttributes3 = new ProfessionalDocAnalyzer.ucParse_Attributes();
            this.ucAttributes4 = new ProfessionalDocAnalyzer.ucParse_Attributes();
            this.ucAttributes5 = new ProfessionalDocAnalyzer.ucParse_Attributes();
            this.ucAttributes6 = new ProfessionalDocAnalyzer.ucParse_Attributes();
            this.ucAttributes7 = new ProfessionalDocAnalyzer.ucParse_Attributes();
            this.ucAttributes8 = new ProfessionalDocAnalyzer.ucParse_Attributes();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.panTop.SuspendLayout();
            this.panErrors.SuspendLayout();
            this.panErrorHeader.SuspendLayout();
            this.panFooter.SuspendLayout();
            this.panFooterRight.SuspendLayout();
            this.panBottom.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // panTop
            // 
            this.panTop.Controls.Add(this.panErrors);
            this.panTop.Controls.Add(this.lblShortTaskName);
            this.panTop.Controls.Add(this.txtbShortTaskName);
            this.panTop.Controls.Add(this.lblDescription);
            this.panTop.Controls.Add(this.txtbDescription);
            this.panTop.Controls.Add(this.lblTaskName);
            this.panTop.Controls.Add(this.txtbTaskName);
            this.panTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.panTop.Location = new System.Drawing.Point(13, 74);
            this.panTop.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panTop.Name = "panTop";
            this.panTop.Size = new System.Drawing.Size(1342, 203);
            this.panTop.TabIndex = 0;
            // 
            // panErrors
            // 
            this.panErrors.Controls.Add(this.txtbErrors);
            this.panErrors.Controls.Add(this.panErrorHeader);
            this.panErrors.Dock = System.Windows.Forms.DockStyle.Right;
            this.panErrors.Location = new System.Drawing.Point(863, 0);
            this.panErrors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panErrors.Name = "panErrors";
            this.panErrors.Size = new System.Drawing.Size(479, 203);
            this.panErrors.TabIndex = 52;
            this.panErrors.Visible = false;
            // 
            // txtbErrors
            // 
            this.txtbErrors.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbErrors.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbErrors.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbErrors.ForeColor = System.Drawing.Color.Red;
            this.txtbErrors.Location = new System.Drawing.Point(0, 36);
            this.txtbErrors.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbErrors.Multiline = true;
            this.txtbErrors.Name = "txtbErrors";
            this.txtbErrors.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbErrors.Size = new System.Drawing.Size(479, 167);
            this.txtbErrors.TabIndex = 15;
            // 
            // panErrorHeader
            // 
            this.panErrorHeader.Controls.Add(this.lblErrors);
            this.panErrorHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panErrorHeader.Location = new System.Drawing.Point(0, 0);
            this.panErrorHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panErrorHeader.Name = "panErrorHeader";
            this.panErrorHeader.Size = new System.Drawing.Size(479, 36);
            this.panErrorHeader.TabIndex = 13;
            // 
            // lblErrors
            // 
            this.lblErrors.AutoSize = true;
            this.lblErrors.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblErrors.ForeColor = System.Drawing.Color.Black;
            this.lblErrors.Location = new System.Drawing.Point(-3, 6);
            this.lblErrors.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblErrors.Name = "lblErrors";
            this.lblErrors.Size = new System.Drawing.Size(108, 28);
            this.lblErrors.TabIndex = 47;
            this.lblErrors.Text = "Please fix ...";
            // 
            // lblShortTaskName
            // 
            this.lblShortTaskName.AutoSize = true;
            this.lblShortTaskName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblShortTaskName.ForeColor = System.Drawing.Color.Black;
            this.lblShortTaskName.Location = new System.Drawing.Point(7, 6);
            this.lblShortTaskName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblShortTaskName.Name = "lblShortTaskName";
            this.lblShortTaskName.Size = new System.Drawing.Size(260, 28);
            this.lblShortTaskName.TabIndex = 51;
            this.lblShortTaskName.Text = "Short Task Name (no spaces)";
            // 
            // txtbShortTaskName
            // 
            this.txtbShortTaskName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbShortTaskName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbShortTaskName.Location = new System.Drawing.Point(4, 36);
            this.txtbShortTaskName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbShortTaskName.MaxLength = 50;
            this.txtbShortTaskName.Name = "txtbShortTaskName";
            this.txtbShortTaskName.Size = new System.Drawing.Size(346, 34);
            this.txtbShortTaskName.TabIndex = 1;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDescription.ForeColor = System.Drawing.Color.Black;
            this.lblDescription.Location = new System.Drawing.Point(4, 91);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(112, 28);
            this.lblDescription.TabIndex = 48;
            this.lblDescription.Text = "Description";
            // 
            // txtbDescription
            // 
            this.txtbDescription.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbDescription.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbDescription.Location = new System.Drawing.Point(4, 121);
            this.txtbDescription.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbDescription.MaxLength = 0;
            this.txtbDescription.Multiline = true;
            this.txtbDescription.Name = "txtbDescription";
            this.txtbDescription.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtbDescription.Size = new System.Drawing.Size(821, 61);
            this.txtbDescription.TabIndex = 4;
            this.txtbDescription.TextChanged += new System.EventHandler(this.txtbDescription_TextChanged);
            // 
            // lblTaskName
            // 
            this.lblTaskName.AutoSize = true;
            this.lblTaskName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskName.ForeColor = System.Drawing.Color.Black;
            this.lblTaskName.Location = new System.Drawing.Point(384, 6);
            this.lblTaskName.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblTaskName.Name = "lblTaskName";
            this.lblTaskName.Size = new System.Drawing.Size(207, 28);
            this.lblTaskName.TabIndex = 46;
            this.lblTaskName.Text = "Descriptive Task Name";
            // 
            // txtbTaskName
            // 
            this.txtbTaskName.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtbTaskName.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbTaskName.Location = new System.Drawing.Point(381, 36);
            this.txtbTaskName.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.txtbTaskName.MaxLength = 50;
            this.txtbTaskName.Name = "txtbTaskName";
            this.txtbTaskName.Size = new System.Drawing.Size(443, 34);
            this.txtbTaskName.TabIndex = 2;
            // 
            // panFooter
            // 
            this.panFooter.Controls.Add(this.panFooterRight);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(13, 1012);
            this.panFooter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(1342, 46);
            this.panFooter.TabIndex = 1;
            // 
            // panFooterRight
            // 
            this.panFooterRight.Controls.Add(this.chkbShow);
            this.panFooterRight.Controls.Add(this.butCancel);
            this.panFooterRight.Controls.Add(this.butOK);
            this.panFooterRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panFooterRight.Location = new System.Drawing.Point(863, 0);
            this.panFooterRight.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panFooterRight.Name = "panFooterRight";
            this.panFooterRight.Size = new System.Drawing.Size(479, 46);
            this.panFooterRight.TabIndex = 15;
            // 
            // chkbShow
            // 
            this.chkbShow.AutoSize = true;
            this.chkbShow.Checked = true;
            this.chkbShow.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkbShow.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkbShow.Location = new System.Drawing.Point(20, 5);
            this.chkbShow.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.chkbShow.Name = "chkbShow";
            this.chkbShow.Size = new System.Drawing.Size(155, 32);
            this.chkbShow.TabIndex = 17;
            this.chkbShow.Text = "Show to users";
            this.chkbShow.UseVisualStyleBackColor = true;
            this.chkbShow.Visible = false;
            // 
            // butCancel
            // 
            this.butCancel.Location = new System.Drawing.Point(375, 7);
            this.butCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(100, 28);
            this.butCancel.TabIndex = 16;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(255, 7);
            this.butOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(100, 28);
            this.butOK.TabIndex = 15;
            this.butOK.Text = "Save";
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // panBottom
            // 
            this.panBottom.Controls.Add(this.flowLayoutPanel1);
            this.panBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panBottom.Location = new System.Drawing.Point(13, 277);
            this.panBottom.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panBottom.Name = "panBottom";
            this.panBottom.Size = new System.Drawing.Size(1342, 735);
            this.panBottom.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.ucAttributes1);
            this.flowLayoutPanel1.Controls.Add(this.ucAttributes2);
            this.flowLayoutPanel1.Controls.Add(this.ucAttributes3);
            this.flowLayoutPanel1.Controls.Add(this.ucAttributes4);
            this.flowLayoutPanel1.Controls.Add(this.ucAttributes5);
            this.flowLayoutPanel1.Controls.Add(this.ucAttributes6);
            this.flowLayoutPanel1.Controls.Add(this.ucAttributes7);
            this.flowLayoutPanel1.Controls.Add(this.ucAttributes8);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(1342, 735);
            this.flowLayoutPanel1.TabIndex = 0;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // ucAttributes1
            // 
            this.ucAttributes1.Action = "";
            this.ucAttributes1.ActionNo = 1;
            this.ucAttributes1.BackColor = System.Drawing.Color.White;
            this.ucAttributes1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucAttributes1.Location = new System.Drawing.Point(5, 5);
            this.ucAttributes1.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucAttributes1.Name = "ucAttributes1";
            this.ucAttributes1.PagesRequired = "";
            this.ucAttributes1.ReportForAction = "";
            this.ucAttributes1.Size = new System.Drawing.Size(486, 539);
            this.ucAttributes1.StepText = "";
            this.ucAttributes1.TabIndex = 5;
            this.ucAttributes1.ActionSelected += new ProfessionalDocAnalyzer.ucParse_Attributes.ProcessHandler(this.ucAttributes1_ActionSelected);
            this.ucAttributes1.UseDefaultParseAnalysisChanged += new ProfessionalDocAnalyzer.ucParse_Attributes.ProcessHandler(this.ucAttributes1_UseDefaultParseAnalysisChanged);
            // 
            // ucAttributes2
            // 
            this.ucAttributes2.Action = "";
            this.ucAttributes2.ActionNo = 1;
            this.ucAttributes2.BackColor = System.Drawing.Color.White;
            this.ucAttributes2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucAttributes2.Location = new System.Drawing.Point(501, 5);
            this.ucAttributes2.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucAttributes2.Name = "ucAttributes2";
            this.ucAttributes2.PagesRequired = "";
            this.ucAttributes2.ReportForAction = "";
            this.ucAttributes2.Size = new System.Drawing.Size(486, 539);
            this.ucAttributes2.StepText = "";
            this.ucAttributes2.TabIndex = 6;
            this.ucAttributes2.Visible = false;
            this.ucAttributes2.ActionSelected += new ProfessionalDocAnalyzer.ucParse_Attributes.ProcessHandler(this.ucAttributes2_ActionSelected);
            // 
            // ucAttributes3
            // 
            this.ucAttributes3.Action = "";
            this.ucAttributes3.ActionNo = 1;
            this.ucAttributes3.BackColor = System.Drawing.Color.White;
            this.ucAttributes3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucAttributes3.Location = new System.Drawing.Point(997, 5);
            this.ucAttributes3.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucAttributes3.Name = "ucAttributes3";
            this.ucAttributes3.PagesRequired = "";
            this.ucAttributes3.ReportForAction = "";
            this.ucAttributes3.Size = new System.Drawing.Size(486, 539);
            this.ucAttributes3.StepText = "";
            this.ucAttributes3.TabIndex = 7;
            this.ucAttributes3.Visible = false;
            this.ucAttributes3.ActionSelected += new ProfessionalDocAnalyzer.ucParse_Attributes.ProcessHandler(this.ucAttributes3_ActionSelected);
            // 
            // ucAttributes4
            // 
            this.ucAttributes4.Action = "";
            this.ucAttributes4.ActionNo = 1;
            this.ucAttributes4.BackColor = System.Drawing.Color.White;
            this.ucAttributes4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucAttributes4.Location = new System.Drawing.Point(1493, 5);
            this.ucAttributes4.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucAttributes4.Name = "ucAttributes4";
            this.ucAttributes4.PagesRequired = "";
            this.ucAttributes4.ReportForAction = "";
            this.ucAttributes4.Size = new System.Drawing.Size(486, 539);
            this.ucAttributes4.StepText = "";
            this.ucAttributes4.TabIndex = 8;
            this.ucAttributes4.Visible = false;
            // 
            // ucAttributes5
            // 
            this.ucAttributes5.Action = "";
            this.ucAttributes5.ActionNo = 1;
            this.ucAttributes5.BackColor = System.Drawing.Color.White;
            this.ucAttributes5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucAttributes5.Location = new System.Drawing.Point(1989, 5);
            this.ucAttributes5.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucAttributes5.Name = "ucAttributes5";
            this.ucAttributes5.PagesRequired = "";
            this.ucAttributes5.ReportForAction = "";
            this.ucAttributes5.Size = new System.Drawing.Size(486, 539);
            this.ucAttributes5.StepText = "";
            this.ucAttributes5.TabIndex = 9;
            this.ucAttributes5.Visible = false;
            // 
            // ucAttributes6
            // 
            this.ucAttributes6.Action = "";
            this.ucAttributes6.ActionNo = 1;
            this.ucAttributes6.BackColor = System.Drawing.Color.White;
            this.ucAttributes6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucAttributes6.Location = new System.Drawing.Point(2485, 5);
            this.ucAttributes6.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucAttributes6.Name = "ucAttributes6";
            this.ucAttributes6.PagesRequired = "";
            this.ucAttributes6.ReportForAction = "";
            this.ucAttributes6.Size = new System.Drawing.Size(486, 539);
            this.ucAttributes6.StepText = "";
            this.ucAttributes6.TabIndex = 10;
            this.ucAttributes6.Visible = false;
            // 
            // ucAttributes7
            // 
            this.ucAttributes7.Action = "";
            this.ucAttributes7.ActionNo = 1;
            this.ucAttributes7.BackColor = System.Drawing.Color.White;
            this.ucAttributes7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucAttributes7.Location = new System.Drawing.Point(2981, 5);
            this.ucAttributes7.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucAttributes7.Name = "ucAttributes7";
            this.ucAttributes7.PagesRequired = "";
            this.ucAttributes7.ReportForAction = "";
            this.ucAttributes7.Size = new System.Drawing.Size(486, 539);
            this.ucAttributes7.StepText = "";
            this.ucAttributes7.TabIndex = 11;
            this.ucAttributes7.Visible = false;
            // 
            // ucAttributes8
            // 
            this.ucAttributes8.Action = "";
            this.ucAttributes8.ActionNo = 1;
            this.ucAttributes8.BackColor = System.Drawing.Color.White;
            this.ucAttributes8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ucAttributes8.Location = new System.Drawing.Point(3477, 5);
            this.ucAttributes8.Margin = new System.Windows.Forms.Padding(5, 5, 5, 5);
            this.ucAttributes8.Name = "ucAttributes8";
            this.ucAttributes8.PagesRequired = "";
            this.ucAttributes8.ReportForAction = "";
            this.ucAttributes8.Size = new System.Drawing.Size(486, 539);
            this.ucAttributes8.StepText = "";
            this.ucAttributes8.TabIndex = 12;
            this.ucAttributes8.Visible = false;
            // 
            // picHeader
            // 
            this.picHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHeader.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_list_gear;
            this.picHeader.Location = new System.Drawing.Point(27, 20);
            this.picHeader.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(55, 47);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 15;
            this.picHeader.TabStop = false;
            // 
            // frmTacksWorkflowConfig
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(1368, 1070);
            this.ControlBox = false;
            this.Controls.Add(this.picHeader);
            this.Controls.Add(this.panBottom);
            this.Controls.Add(this.panFooter);
            this.Controls.Add(this.panTop);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmTacksWorkflowConfig";
            this.Padding = new System.Windows.Forms.Padding(13, 74, 13, 12);
            this.Text = "      Task Workflow Configuration";
            this.panTop.ResumeLayout(false);
            this.panTop.PerformLayout();
            this.panErrors.ResumeLayout(false);
            this.panErrors.PerformLayout();
            this.panErrorHeader.ResumeLayout(false);
            this.panErrorHeader.PerformLayout();
            this.panFooter.ResumeLayout(false);
            this.panFooterRight.ResumeLayout(false);
            this.panFooterRight.PerformLayout();
            this.panBottom.ResumeLayout(false);
            this.panBottom.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panTop;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.Panel panBottom;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private ucParse_Attributes ucAttributes1;
        private ucParse_Attributes ucAttributes2;
        private ucParse_Attributes ucAttributes3;
        private ucParse_Attributes ucAttributes4;
        private ucParse_Attributes ucAttributes5;
        private ucParse_Attributes ucAttributes6;
        private ucParse_Attributes ucAttributes7;
        private ucParse_Attributes ucAttributes8;
        private System.Windows.Forms.Label lblDescription;
        private System.Windows.Forms.TextBox txtbDescription;
        private System.Windows.Forms.Label lblTaskName;
        private System.Windows.Forms.TextBox txtbTaskName;
        private System.Windows.Forms.Label lblShortTaskName;
        private System.Windows.Forms.TextBox txtbShortTaskName;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.Panel panFooterRight;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOK;
        private System.Windows.Forms.Panel panErrors;
        private System.Windows.Forms.TextBox txtbErrors;
        private System.Windows.Forms.Panel panErrorHeader;
        private System.Windows.Forms.Label lblErrors;
        private System.Windows.Forms.CheckBox chkbShow;
    }
}