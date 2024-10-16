namespace ProfessionalDocAnalyzer
{
    partial class ucTasksSettings
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
            this.lblSort = new System.Windows.Forms.Label();
            this.butSortUP = new MetroFramework.Controls.MetroButton();
            this.butSortDown = new MetroFramework.Controls.MetroButton();
            this.lblMessage = new System.Windows.Forms.Label();
            this.lblHeader = new System.Windows.Forms.Label();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.lstbTasks = new System.Windows.Forms.CheckedListBox();
            this.panRightBottom = new System.Windows.Forms.Panel();
            this.lblInstructions = new System.Windows.Forms.Label();
            this.txtbMessage = new System.Windows.Forms.TextBox();
            this.panRightPadding = new System.Windows.Forms.Panel();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panRightBottom.SuspendLayout();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.White;
            this.panHeader.Controls.Add(this.lblSort);
            this.panHeader.Controls.Add(this.butSortUP);
            this.panHeader.Controls.Add(this.butSortDown);
            this.panHeader.Controls.Add(this.lblMessage);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Controls.Add(this.picHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.ForeColor = System.Drawing.Color.White;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(831, 50);
            this.panHeader.TabIndex = 8;
            // 
            // lblSort
            // 
            this.lblSort.AutoSize = true;
            this.lblSort.BackColor = System.Drawing.Color.Transparent;
            this.lblSort.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSort.ForeColor = System.Drawing.Color.Black;
            this.lblSort.Location = new System.Drawing.Point(170, 20);
            this.lblSort.Name = "lblSort";
            this.lblSort.Size = new System.Drawing.Size(132, 20);
            this.lblSort.TabIndex = 62;
            this.lblSort.Text = "Change Sort Order";
            // 
            // butSortUP
            // 
            this.butSortUP.BackgroundImage = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_arrow_up;
            this.butSortUP.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butSortUP.Location = new System.Drawing.Point(305, 17);
            this.butSortUP.Name = "butSortUP";
            this.butSortUP.Size = new System.Drawing.Size(28, 28);
            this.butSortUP.TabIndex = 61;
            this.butSortUP.UseSelectable = true;
            this.butSortUP.Click += new System.EventHandler(this.butSortUP_Click);
            // 
            // butSortDown
            // 
            this.butSortDown.BackgroundImage = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_arrow_down;
            this.butSortDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butSortDown.Location = new System.Drawing.Point(339, 17);
            this.butSortDown.Name = "butSortDown";
            this.butSortDown.Size = new System.Drawing.Size(28, 28);
            this.butSortDown.TabIndex = 60;
            this.butSortDown.UseSelectable = true;
            this.butSortDown.Click += new System.EventHandler(this.butSortDown_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.Location = new System.Drawing.Point(320, 11);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(0, 13);
            this.lblMessage.TabIndex = 16;
            this.lblMessage.Visible = false;
            this.lblMessage.TextChanged += new System.EventHandler(this.lblMessage_TextChanged);
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.BackColor = System.Drawing.Color.Transparent;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.Black;
            this.lblHeader.Location = new System.Drawing.Point(53, 11);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(61, 30);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Tasks";
            // 
            // picHeader
            // 
            this.picHeader.Cursor = System.Windows.Forms.Cursors.Hand;
            this.picHeader.Image = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_list_gear;
            this.picHeader.Location = new System.Drawing.Point(9, 6);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(41, 38);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 14;
            this.picHeader.TabStop = false;
            this.picHeader.Click += new System.EventHandler(this.picHeader_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 50);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.Controls.Add(this.lstbTasks);
            this.splitContainer1.Panel1.Controls.Add(this.panRightBottom);
            this.splitContainer1.Panel1.Padding = new System.Windows.Forms.Padding(20, 20, 0, 0);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtbMessage);
            this.splitContainer1.Panel2.Controls.Add(this.panRightPadding);
            this.splitContainer1.Size = new System.Drawing.Size(831, 436);
            this.splitContainer1.SplitterDistance = 388;
            this.splitContainer1.TabIndex = 10;
            // 
            // lstbTasks
            // 
            this.lstbTasks.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbTasks.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstbTasks.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbTasks.FormattingEnabled = true;
            this.lstbTasks.Location = new System.Drawing.Point(20, 20);
            this.lstbTasks.Name = "lstbTasks";
            this.lstbTasks.Size = new System.Drawing.Size(368, 371);
            this.lstbTasks.TabIndex = 2;
            this.lstbTasks.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.lstbTasks_ItemCheck);
            this.lstbTasks.SelectedIndexChanged += new System.EventHandler(this.lstbTasks_SelectedIndexChanged);
            this.lstbTasks.MouseUp += new System.Windows.Forms.MouseEventHandler(this.lstbTasks_MouseUp);
            // 
            // panRightBottom
            // 
            this.panRightBottom.Controls.Add(this.lblInstructions);
            this.panRightBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panRightBottom.Location = new System.Drawing.Point(20, 391);
            this.panRightBottom.Name = "panRightBottom";
            this.panRightBottom.Size = new System.Drawing.Size(368, 45);
            this.panRightBottom.TabIndex = 1;
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.ForeColor = System.Drawing.Color.Blue;
            this.lblInstructions.Location = new System.Drawing.Point(3, 13);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new System.Drawing.Size(0, 20);
            this.lblInstructions.TabIndex = 70;
            // 
            // txtbMessage
            // 
            this.txtbMessage.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtbMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtbMessage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtbMessage.ForeColor = System.Drawing.Color.Black;
            this.txtbMessage.Location = new System.Drawing.Point(41, 0);
            this.txtbMessage.Multiline = true;
            this.txtbMessage.Name = "txtbMessage";
            this.txtbMessage.Size = new System.Drawing.Size(398, 436);
            this.txtbMessage.TabIndex = 1;
            this.txtbMessage.TextChanged += new System.EventHandler(this.txtbMessage_TextChanged);
            // 
            // panRightPadding
            // 
            this.panRightPadding.BackColor = System.Drawing.Color.White;
            this.panRightPadding.Dock = System.Windows.Forms.DockStyle.Left;
            this.panRightPadding.Location = new System.Drawing.Point(0, 0);
            this.panRightPadding.Name = "panRightPadding";
            this.panRightPadding.Size = new System.Drawing.Size(41, 436);
            this.panRightPadding.TabIndex = 0;
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ucTasksSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panHeader);
            this.Name = "ucTasksSettings";
            this.Size = new System.Drawing.Size(831, 486);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panRightBottom.ResumeLayout(false);
            this.panRightBottom.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TextBox txtbMessage;
        private System.Windows.Forms.Panel panRightPadding;
        private MetroFramework.Controls.MetroButton butSortDown;
        private MetroFramework.Controls.MetroButton butSortUP;
        private System.Windows.Forms.Label lblSort;
        private System.Windows.Forms.CheckedListBox lstbTasks;
        private System.Windows.Forms.Panel panRightBottom;
        private System.Windows.Forms.Label lblInstructions;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
    }
}
