using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace ProfessionalDocAnalyzer
{
    partial class ucTasksSelect
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private IContainer components = null;

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
            this.splitContainer1 = new SplitContainer();
            this.panTasks = new FlowLayoutPanel();
            this.panLeftHeader = new Panel();
            this.lblTaskQty = new Label();
            this.lblIWantTo = new Label();
            this.txtInformation = new TextBox();
            this.panRightHeader = new Panel();
            this.chkbShowDetails = new CheckBox();
            this.lblInstructions = new Label();
            this.lblInformation = new Label();
            ((ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panTasks.SuspendLayout();
            this.panLeftHeader.SuspendLayout();
            this.panRightHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = DockStyle.Fill;
            this.splitContainer1.Location = new Point(30, 30);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panTasks);
            this.splitContainer1.Panel1.Controls.Add(this.panLeftHeader);
            this.splitContainer1.Panel1MinSize = 400;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtInformation);
            this.splitContainer1.Panel2.Controls.Add(this.panRightHeader);
            this.splitContainer1.Panel2MinSize = 600;
            this.splitContainer1.Size = new Size(861, 452);
            this.splitContainer1.SplitterDistance = 416;
            this.splitContainer1.TabIndex = 4;
            // 
            // panTasks
            // 
            this.panTasks.HorizontalScroll.Maximum = 0;
            this.panTasks.AutoScroll = false;
            this.panTasks.VerticalScroll.Visible = true;
            this.panTasks.AutoScroll = true;
            this.panTasks.Dock = DockStyle.Fill;
            this.panTasks.FlowDirection = FlowDirection.TopDown;
            this.panTasks.Height = splitContainer1.Panel1.Height;
            this.panTasks.Location = new Point(0, 70);
            this.panTasks.Name = "panTasks";
            this.panTasks.Size = new Size(416, 382);
            this.panTasks.TabIndex = 2;
            //this.panTasks.BackColor = Color.LightGray;
            this.panTasks.WrapContents = false;
            this.panTasks.SizeChanged += new EventHandler(this.panTasks_SizeChanged);
            // 
            // panLeftHeader
            // 
            this.panLeftHeader.Controls.Add(this.lblTaskQty);
            this.panLeftHeader.Controls.Add(this.lblIWantTo);
            this.panLeftHeader.Dock = DockStyle.Top;
            this.panLeftHeader.Location = new Point(0, 0);
            this.panLeftHeader.Name = "panLeftHeader";
            this.panLeftHeader.Size = new Size(416, 70);
            this.panLeftHeader.TabIndex = 0;
            // 
            // lblTaskQty
            // 
            this.lblTaskQty.AutoSize = true;
            this.lblTaskQty.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblTaskQty.ForeColor = Color.SteelBlue;
            this.lblTaskQty.Location = new Point(81, 26);
            this.lblTaskQty.Name = "lblTaskQty";
            this.lblTaskQty.Size = new Size(19, 21);
            this.lblTaskQty.TabIndex = 1;
            this.lblTaskQty.Text = "0";
            this.lblTaskQty.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // lblIWantTo
            // 
            this.lblIWantTo.AutoSize = true;
            this.lblIWantTo.Font = new Font("Segoe UI", 21.75F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblIWantTo.Location = new Point(-7, 11);
            this.lblIWantTo.Name = "lblIWantTo";
            this.lblIWantTo.Size = new Size(82, 40);
            this.lblIWantTo.TabIndex = 0;
            this.lblIWantTo.Text = "Tasks";
            this.lblIWantTo.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // txtInformation
            // 
            this.txtInformation.BorderStyle = BorderStyle.None;
            this.txtInformation.Dock = DockStyle.Fill;
            this.txtInformation.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.txtInformation.ForeColor = Color.SteelBlue;
            this.txtInformation.Location = new Point(0, 82);
            this.txtInformation.Multiline = true;
            this.txtInformation.Name = "txtInformation";
            this.txtInformation.Size = new Size(441, 370);
            this.txtInformation.TabIndex = 2;
            this.txtInformation.TextChanged += new EventHandler(this.txtInformation_TextChanged);
            // 
            // panRightHeader
            // 
            this.panRightHeader.Controls.Add(this.chkbShowDetails);
            this.panRightHeader.Controls.Add(this.lblInstructions);
            this.panRightHeader.Controls.Add(this.lblInformation);
            this.panRightHeader.Dock = DockStyle.Top;
            this.panRightHeader.Location = new Point(0, 0);
            this.panRightHeader.Name = "panRightHeader";
            this.panRightHeader.Size = new Size(441, 82);
            this.panRightHeader.TabIndex = 1;
            // 
            // chkbShowDetails
            // 
            this.chkbShowDetails.AutoSize = true;
            this.chkbShowDetails.Font = new Font("Segoe UI", 9F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.chkbShowDetails.ForeColor = Color.SteelBlue;
            this.chkbShowDetails.Location = new Point(20, 3);
            this.chkbShowDetails.Name = "chkbShowDetails";
            this.chkbShowDetails.Size = new Size(93, 19);
            this.chkbShowDetails.TabIndex = 3;
            this.chkbShowDetails.Text = "Show Details";
            this.chkbShowDetails.UseVisualStyleBackColor = true;
            this.chkbShowDetails.Visible = false;
            this.chkbShowDetails.CheckedChanged += new EventHandler(this.chkbShowDetails_CheckedChanged);
            // 
            // lblInstructions
            // 
            this.lblInstructions.AutoSize = true;
            this.lblInstructions.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0)));
            this.lblInstructions.ForeColor = Color.SteelBlue;
            this.lblInstructions.Location = new Point(16, 49);
            this.lblInstructions.Name = "lblInstructions";
            this.lblInstructions.Size = new Size(56, 21);
            this.lblInstructions.TabIndex = 2;
            this.lblInstructions.Text = "dsfsdf";
            // 
            // lblInformation
            // 
            this.lblInformation.AutoSize = true;
            this.lblInformation.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            this.lblInformation.Location = new Point(16, 3);
            this.lblInformation.Name = "lblInformation";
            this.lblInformation.Size = new Size(0, 21);
            this.lblInformation.TabIndex = 1;
            this.lblInformation.Visible = false;
            this.lblInformation.TextChanged += new EventHandler(this.lblInformation_TextChanged);
            // 
            // ucTasksSelect
            // 
            this.AutoScaleDimensions = new SizeF(6F, 13F);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.BackColor = Color.White;
            this.Controls.Add(this.splitContainer1);
            this.Margin = new Padding(30, 3, 3, 3);
            this.Name = "ucTasksSelect";
            this.Padding = new Padding(30, 30, 30, 0);
            this.Size = new Size(921, 482);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panTasks.ResumeLayout(false);
            this.panTasks.PerformLayout();
            this.panLeftHeader.ResumeLayout(false);
            this.panLeftHeader.PerformLayout();
            this.panRightHeader.ResumeLayout(false);
            this.panRightHeader.PerformLayout();
            this.ResumeLayout(false);
        }

        #region Private Event Methods

        private void TaskButton_MouseEnter(object sender, EventArgs e)
        {
            RadioButton task = sender as RadioButton;
            if (!task.Checked)
            {
                task.BackColor = Color.PowderBlue;
            }
        }

        private void TaskButton_MouseLeave(object sender, EventArgs e)
        {
            RadioButton task = sender as RadioButton;
            if (!task.Checked)
            {
                task.BackColor = Color.White;
            }
        }

        private void TaskButton_Click(object sender, EventArgs e)
        {
            RadioButton task = sender as RadioButton;
            TaskName = task.Text;
            DisplayTaskInformation();
        }

        private void TaskButton_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton task = sender as RadioButton;
            task.BackColor = task.Checked ? Color.Black : Color.White;
            task.ForeColor = task.Checked ? Color.PowderBlue : Color.Black;
        }

        #endregion

        #endregion

        private SplitContainer splitContainer1;
        private Panel panLeftHeader;
        private Label lblIWantTo;
        private TextBox txtInformation;
        private Panel panRightHeader;
        private Label lblInformation;
        private Label lblInstructions;
        private CheckBox chkbShowDetails;
        private FlowLayoutPanel panTasks;
        private Label lblTaskQty;
    }
}
