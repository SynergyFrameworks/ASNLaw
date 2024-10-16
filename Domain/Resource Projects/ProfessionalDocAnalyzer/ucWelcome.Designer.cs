using System;
using System.Drawing;
using System.Windows.Forms;

namespace ProfessionalDocAnalyzer
{
    partial class ucWelcome
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucWelcome));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panLeftPadding = new System.Windows.Forms.Panel();
            this.lblImagine = new System.Windows.Forms.Label();
            this.lblScion = new System.Windows.Forms.Label();
            this.butWorkgroup_Disconnect = new MetroFramework.Controls.MetroButton();
            this.butWorkgroup_Connect = new MetroFramework.Controls.MetroButton();
            this.butWorkgroup_Add = new MetroFramework.Controls.MetroButton();
            this.lblWorkgroupPath = new System.Windows.Forms.Label();
            this.lblMessage = new System.Windows.Forms.Label();
            this.panMembers = new System.Windows.Forms.Panel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.lblMemberInfo = new System.Windows.Forms.Label();
            this.lstbMembers = new System.Windows.Forms.ListBox();
            this.panMembersHeader = new System.Windows.Forms.Panel();
            this.butEmailNewMembers = new MetroFramework.Controls.MetroButton();
            this.lblMembers = new System.Windows.Forms.Label();
            this.butLasr = new System.Windows.Forms.Button();
            this.lblWorkgroups = new System.Windows.Forms.Label();
            this.cboWorkgroups = new System.Windows.Forms.ComboBox();
            this.panHeader = new System.Windows.Forms.Panel();
            this.lblCaption = new System.Windows.Forms.Label();
            this.pnlSetting = new System.Windows.Forms.Panel();
            this.picSettings = new System.Windows.Forms.PictureBox();
            this.lblButtonInfo = new System.Windows.Forms.Label();
            this.panPaddingTopRight = new System.Windows.Forms.Panel();
            this.panRight = new System.Windows.Forms.Panel();
            this.picResults = new System.Windows.Forms.PictureBox();
            this.picStart = new System.Windows.Forms.PictureBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.butSettings = new System.Windows.Forms.Button();
            this.butResults = new System.Windows.Forms.Button();
            this.butStart = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.butQCTest = new System.Windows.Forms.Button();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panLeftPadding.SuspendLayout();
            this.panMembers.SuspendLayout();
            this.panMembersHeader.SuspendLayout();
            this.panHeader.SuspendLayout();
            this.pnlSetting.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picSettings)).BeginInit();
            this.panRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picResults)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStart)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.IsSplitterFixed = true;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panLeftPadding);
            this.splitContainer1.Panel1.Controls.Add(this.panHeader);
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.pnlSetting);
            this.splitContainer1.Panel2.Controls.Add(this.lblButtonInfo);
            this.splitContainer1.Panel2.Controls.Add(this.panPaddingTopRight);
            this.splitContainer1.Panel2.Controls.Add(this.panRight);
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1392, 622);
            this.splitContainer1.SplitterDistance = 387;
            this.splitContainer1.SplitterWidth = 5;
            this.splitContainer1.TabIndex = 3;
            // 
            // panLeftPadding
            // 
            this.panLeftPadding.BackColor = System.Drawing.Color.Black;
            this.panLeftPadding.Controls.Add(this.lblImagine);
            this.panLeftPadding.Controls.Add(this.lblScion);
            this.panLeftPadding.Controls.Add(this.butWorkgroup_Disconnect);
            this.panLeftPadding.Controls.Add(this.butWorkgroup_Connect);
            this.panLeftPadding.Controls.Add(this.butWorkgroup_Add);
            this.panLeftPadding.Controls.Add(this.lblWorkgroupPath);
            this.panLeftPadding.Controls.Add(this.lblMessage);
            this.panLeftPadding.Controls.Add(this.panMembers);
            this.panLeftPadding.Controls.Add(this.lblWorkgroups);
            this.panLeftPadding.Controls.Add(this.cboWorkgroups);
            this.panLeftPadding.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panLeftPadding.Location = new System.Drawing.Point(0, 94);
            this.panLeftPadding.Margin = new System.Windows.Forms.Padding(4);
            this.panLeftPadding.Name = "panLeftPadding";
            this.panLeftPadding.Size = new System.Drawing.Size(387, 528);
            this.panLeftPadding.TabIndex = 4;
            this.panLeftPadding.Paint += new System.Windows.Forms.PaintEventHandler(this.panLeftPadding_Paint);
            // 
            // lblImagine
            // 
            this.lblImagine.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblImagine.AutoSize = true;
            this.lblImagine.BackColor = System.Drawing.Color.Transparent;
            this.lblImagine.Font = new System.Drawing.Font("MS Reference Sans Serif", 16F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImagine.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(3)))), ((int)(((byte)(51)))));
            this.lblImagine.Location = new System.Drawing.Point(7, 491);
            this.lblImagine.Name = "lblImagine";
            this.lblImagine.Size = new System.Drawing.Size(375, 34);
            this.lblImagine.TabIndex = 33;
            this.lblImagine.Text = "Imagine What You Can Do";
            this.lblImagine.Click += new System.EventHandler(this.lblImagine_Click);
            // 
            // lblScion
            // 
            this.lblScion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblScion.AutoSize = true;
            this.lblScion.Font = new System.Drawing.Font("MS Reference Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblScion.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(3)))), ((int)(((byte)(51)))));
            this.lblScion.Location = new System.Drawing.Point(6, 449);
            this.lblScion.Name = "lblScion";
            this.lblScion.Size = new System.Drawing.Size(335, 42);
            this.lblScion.TabIndex = 1;
            this.lblScion.Text = "    Scion Analytics ";
            this.lblScion.Click += new System.EventHandler(this.lblScion_Click);
            // 
            // butWorkgroup_Disconnect
            // 
            this.butWorkgroup_Disconnect.BackColor = System.Drawing.Color.Gainsboro;
            this.butWorkgroup_Disconnect.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butWorkgroup_Disconnect.BackgroundImage")));
            this.butWorkgroup_Disconnect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butWorkgroup_Disconnect.Location = new System.Drawing.Point(315, 17);
            this.butWorkgroup_Disconnect.Margin = new System.Windows.Forms.Padding(4);
            this.butWorkgroup_Disconnect.Name = "butWorkgroup_Disconnect";
            this.butWorkgroup_Disconnect.Size = new System.Drawing.Size(40, 37);
            this.butWorkgroup_Disconnect.TabIndex = 54;
            this.butWorkgroup_Disconnect.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroToolTip1.SetToolTip(this.butWorkgroup_Disconnect, "Disconnect from the selected Workgroup");
            this.butWorkgroup_Disconnect.UseSelectable = true;
            this.butWorkgroup_Disconnect.Click += new System.EventHandler(this.butWorkgroup_Disconnect_Click);
            // 
            // butWorkgroup_Connect
            // 
            this.butWorkgroup_Connect.BackColor = System.Drawing.Color.Gainsboro;
            this.butWorkgroup_Connect.BackgroundImage = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_link;
            this.butWorkgroup_Connect.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butWorkgroup_Connect.Location = new System.Drawing.Point(267, 17);
            this.butWorkgroup_Connect.Margin = new System.Windows.Forms.Padding(4);
            this.butWorkgroup_Connect.Name = "butWorkgroup_Connect";
            this.butWorkgroup_Connect.Size = new System.Drawing.Size(40, 37);
            this.butWorkgroup_Connect.TabIndex = 54;
            this.butWorkgroup_Connect.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroToolTip1.SetToolTip(this.butWorkgroup_Connect, "Connect to an existing Workgroup");
            this.butWorkgroup_Connect.UseSelectable = true;
            this.butWorkgroup_Connect.Click += new System.EventHandler(this.butWorkgroup_Connect_Click);
            // 
            // butWorkgroup_Add
            // 
            this.butWorkgroup_Add.BackColor = System.Drawing.Color.Gainsboro;
            this.butWorkgroup_Add.BackgroundImage = global::ProfessionalDocAnalyzer.Properties.Resources.appbar_add;
            this.butWorkgroup_Add.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butWorkgroup_Add.Location = new System.Drawing.Point(219, 17);
            this.butWorkgroup_Add.Margin = new System.Windows.Forms.Padding(4);
            this.butWorkgroup_Add.Name = "butWorkgroup_Add";
            this.butWorkgroup_Add.Size = new System.Drawing.Size(40, 37);
            this.butWorkgroup_Add.TabIndex = 53;
            this.butWorkgroup_Add.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroToolTip1.SetToolTip(this.butWorkgroup_Add, "Add a new Workgroup");
            this.butWorkgroup_Add.UseSelectable = true;
            this.butWorkgroup_Add.Click += new System.EventHandler(this.butWorkgroup_Add_Click);
            // 
            // lblWorkgroupPath
            // 
            this.lblWorkgroupPath.AutoSize = true;
            this.lblWorkgroupPath.BackColor = System.Drawing.Color.Transparent;
            this.lblWorkgroupPath.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lblWorkgroupPath.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkgroupPath.ForeColor = System.Drawing.Color.White;
            this.lblWorkgroupPath.Location = new System.Drawing.Point(23, 107);
            this.lblWorkgroupPath.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWorkgroupPath.Name = "lblWorkgroupPath";
            this.lblWorkgroupPath.Size = new System.Drawing.Size(40, 20);
            this.lblWorkgroupPath.TabIndex = 48;
            this.lblWorkgroupPath.Text = "Path:";
            this.lblWorkgroupPath.Click += new System.EventHandler(this.lblWorkgroupPath_Click);
            // 
            // lblMessage
            // 
            this.lblMessage.AutoSize = true;
            this.lblMessage.BackColor = System.Drawing.Color.Transparent;
            this.lblMessage.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMessage.ForeColor = System.Drawing.Color.Black;
            this.lblMessage.Location = new System.Drawing.Point(23, 134);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMessage.Name = "lblMessage";
            this.lblMessage.Size = new System.Drawing.Size(61, 19);
            this.lblMessage.TabIndex = 30;
            this.lblMessage.Text = "dfdsffsdf";
            // 
            // panMembers
            // 
            this.panMembers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.panMembers.Controls.Add(this.flowLayoutPanel1);
            this.panMembers.Controls.Add(this.lblMemberInfo);
            this.panMembers.Controls.Add(this.lstbMembers);
            this.panMembers.Controls.Add(this.panMembersHeader);
            this.panMembers.Controls.Add(this.butLasr);
            this.panMembers.Location = new System.Drawing.Point(4, 308);
            this.panMembers.Margin = new System.Windows.Forms.Padding(4);
            this.panMembers.Name = "panMembers";
            this.panMembers.Padding = new System.Windows.Forms.Padding(40, 0, 0, 0);
            this.panMembers.Size = new System.Drawing.Size(387, 143);
            this.panMembers.TabIndex = 28;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(311, 208);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(79, 47);
            this.flowLayoutPanel1.TabIndex = 33;
            // 
            // lblMemberInfo
            // 
            this.lblMemberInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.lblMemberInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMemberInfo.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMemberInfo.ForeColor = System.Drawing.Color.Black;
            this.lblMemberInfo.Location = new System.Drawing.Point(244, 69);
            this.lblMemberInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMemberInfo.Name = "lblMemberInfo";
            this.lblMemberInfo.Size = new System.Drawing.Size(143, 74);
            this.lblMemberInfo.TabIndex = 31;
            // 
            // lstbMembers
            // 
            this.lstbMembers.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(229)))), ((int)(((byte)(229)))), ((int)(((byte)(229)))));
            this.lstbMembers.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.lstbMembers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.lstbMembers.Dock = System.Windows.Forms.DockStyle.Left;
            this.lstbMembers.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstbMembers.ForeColor = System.Drawing.Color.Black;
            this.lstbMembers.FormattingEnabled = true;
            this.lstbMembers.ItemHeight = 21;
            this.lstbMembers.Location = new System.Drawing.Point(40, 69);
            this.lstbMembers.Margin = new System.Windows.Forms.Padding(4);
            this.lstbMembers.Name = "lstbMembers";
            this.lstbMembers.Size = new System.Drawing.Size(204, 74);
            this.lstbMembers.Sorted = true;
            this.lstbMembers.TabIndex = 30;
            this.lstbMembers.SelectedIndexChanged += new System.EventHandler(this.lstbMembers_SelectedIndexChanged);
            // 
            // panMembersHeader
            // 
            this.panMembersHeader.BackColor = System.Drawing.Color.Gainsboro;
            this.panMembersHeader.Controls.Add(this.butEmailNewMembers);
            this.panMembersHeader.Controls.Add(this.lblMembers);
            this.panMembersHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panMembersHeader.Location = new System.Drawing.Point(40, 0);
            this.panMembersHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panMembersHeader.Name = "panMembersHeader";
            this.panMembersHeader.Size = new System.Drawing.Size(347, 69);
            this.panMembersHeader.TabIndex = 29;
            // 
            // butEmailNewMembers
            // 
            this.butEmailNewMembers.BackColor = System.Drawing.Color.Gainsboro;
            this.butEmailNewMembers.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butEmailNewMembers.BackgroundImage")));
            this.butEmailNewMembers.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butEmailNewMembers.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butEmailNewMembers.Location = new System.Drawing.Point(5, 4);
            this.butEmailNewMembers.Margin = new System.Windows.Forms.Padding(4);
            this.butEmailNewMembers.Name = "butEmailNewMembers";
            this.butEmailNewMembers.Size = new System.Drawing.Size(51, 47);
            this.butEmailNewMembers.TabIndex = 30;
            this.butEmailNewMembers.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroToolTip1.SetToolTip(this.butEmailNewMembers, "Send email to invite new members");
            this.butEmailNewMembers.UseSelectable = true;
            this.butEmailNewMembers.Click += new System.EventHandler(this.butEmailNewMembers_Click);
            // 
            // lblMembers
            // 
            this.lblMembers.AutoSize = true;
            this.lblMembers.BackColor = System.Drawing.Color.Transparent;
            this.lblMembers.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMembers.ForeColor = System.Drawing.Color.Black;
            this.lblMembers.Location = new System.Drawing.Point(64, 17);
            this.lblMembers.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblMembers.Name = "lblMembers";
            this.lblMembers.Size = new System.Drawing.Size(91, 25);
            this.lblMembers.TabIndex = 29;
            this.lblMembers.Text = "Members";
            // 
            // butLasr
            // 
            this.butLasr.BackColor = System.Drawing.Color.Sienna;
            this.butLasr.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butLasr.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.butLasr.ForeColor = System.Drawing.Color.White;
            this.butLasr.Location = new System.Drawing.Point(117, 338);
            this.butLasr.Margin = new System.Windows.Forms.Padding(4);
            this.butLasr.Name = "butLasr";
            this.butLasr.Size = new System.Drawing.Size(223, 149);
            this.butLasr.TabIndex = 8;
            this.butLasr.Text = "Last";
            this.butLasr.UseVisualStyleBackColor = false;
            this.butLasr.Visible = false;
            this.butLasr.MouseEnter += new System.EventHandler(this.butLasr_MouseEnter);
            this.butLasr.MouseLeave += new System.EventHandler(this.butLasr_MouseLeave);
            // 
            // lblWorkgroups
            // 
            this.lblWorkgroups.AutoSize = true;
            this.lblWorkgroups.BackColor = System.Drawing.Color.Transparent;
            this.lblWorkgroups.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWorkgroups.ForeColor = System.Drawing.Color.White;
            this.lblWorkgroups.Location = new System.Drawing.Point(19, 15);
            this.lblWorkgroups.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWorkgroups.Name = "lblWorkgroups";
            this.lblWorkgroups.Size = new System.Drawing.Size(182, 41);
            this.lblWorkgroups.TabIndex = 10;
            this.lblWorkgroups.Text = "Workgroups";
            // 
            // cboWorkgroups
            // 
            this.cboWorkgroups.BackColor = System.Drawing.SystemColors.HighlightText;
            this.cboWorkgroups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboWorkgroups.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.cboWorkgroups.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboWorkgroups.FormattingEnabled = true;
            this.cboWorkgroups.Location = new System.Drawing.Point(27, 58);
            this.cboWorkgroups.Margin = new System.Windows.Forms.Padding(4);
            this.cboWorkgroups.Name = "cboWorkgroups";
            this.cboWorkgroups.Size = new System.Drawing.Size(467, 36);
            this.cboWorkgroups.TabIndex = 9;
            this.cboWorkgroups.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboWorkgroups_DrawItem);
            this.cboWorkgroups.SelectedIndexChanged += new System.EventHandler(this.cboWorkgroups_SelectedIndexChanged);
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Navy;
            this.panHeader.Controls.Add(this.lblCaption);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Margin = new System.Windows.Forms.Padding(4);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(387, 94);
            this.panHeader.TabIndex = 3;
            // 
            // lblCaption
            // 
            this.lblCaption.AutoSize = true;
            this.lblCaption.Font = new System.Drawing.Font("Segoe UI", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCaption.ForeColor = System.Drawing.SystemColors.Window;
            this.lblCaption.Location = new System.Drawing.Point(12, 7);
            this.lblCaption.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Size = new System.Drawing.Size(281, 81);
            this.lblCaption.TabIndex = 1;
            this.lblCaption.Text = "Welcome";
            // 
            // pnlSetting
            // 
            this.pnlSetting.BackColor = System.Drawing.Color.Transparent;
            this.pnlSetting.Controls.Add(this.picSettings);
            this.pnlSetting.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnlSetting.Location = new System.Drawing.Point(548, 503);
            this.pnlSetting.Name = "pnlSetting";
            this.pnlSetting.Size = new System.Drawing.Size(452, 119);
            this.pnlSetting.TabIndex = 12;
            // 
            // picSettings
            // 
            this.picSettings.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.picSettings.Image = global::ProfessionalDocAnalyzer.Properties.Resources.settingsIcon97;
            this.picSettings.Location = new System.Drawing.Point(279, 7);
            this.picSettings.Margin = new System.Windows.Forms.Padding(4);
            this.picSettings.Name = "picSettings";
            this.picSettings.Size = new System.Drawing.Size(120, 112);
            this.picSettings.TabIndex = 12;
            this.picSettings.TabStop = false;
            this.picSettings.Click += new System.EventHandler(this.picSettings_Click);
            this.picSettings.MouseEnter += new System.EventHandler(this.picSettings_MouseEnter);
            this.picSettings.MouseLeave += new System.EventHandler(this.picSettings_MouseLeave);
            this.picSettings.MouseHover += new System.EventHandler(this.picSettings_MouseHover);
            // 
            // lblButtonInfo
            // 
            this.lblButtonInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblButtonInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblButtonInfo.ForeColor = System.Drawing.Color.SteelBlue;
            this.lblButtonInfo.Location = new System.Drawing.Point(548, 199);
            this.lblButtonInfo.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblButtonInfo.Name = "lblButtonInfo";
            this.lblButtonInfo.Size = new System.Drawing.Size(452, 423);
            this.lblButtonInfo.TabIndex = 11;
            // 
            // panPaddingTopRight
            // 
            this.panPaddingTopRight.Dock = System.Windows.Forms.DockStyle.Top;
            this.panPaddingTopRight.Location = new System.Drawing.Point(548, 0);
            this.panPaddingTopRight.Margin = new System.Windows.Forms.Padding(4);
            this.panPaddingTopRight.Name = "panPaddingTopRight";
            this.panPaddingTopRight.Size = new System.Drawing.Size(452, 199);
            this.panPaddingTopRight.TabIndex = 10;
            // 
            // panRight
            // 
            this.panRight.Controls.Add(this.picResults);
            this.panRight.Controls.Add(this.picStart);
            this.panRight.Controls.Add(this.panel1);
            this.panRight.Controls.Add(this.butSettings);
            this.panRight.Controls.Add(this.butResults);
            this.panRight.Controls.Add(this.butStart);
            this.panRight.Dock = System.Windows.Forms.DockStyle.Left;
            this.panRight.Location = new System.Drawing.Point(0, 0);
            this.panRight.Margin = new System.Windows.Forms.Padding(4);
            this.panRight.Name = "panRight";
            this.panRight.Size = new System.Drawing.Size(548, 622);
            this.panRight.TabIndex = 4;
            this.panRight.Paint += new System.Windows.Forms.PaintEventHandler(this.panRight_Paint);
            // 
            // picResults
            // 
            this.picResults.Image = global::ProfessionalDocAnalyzer.Properties.Resources.resultsIcon110;
            this.picResults.Location = new System.Drawing.Point(35, 320);
            this.picResults.Margin = new System.Windows.Forms.Padding(4);
            this.picResults.Name = "picResults";
            this.picResults.Size = new System.Drawing.Size(153, 140);
            this.picResults.TabIndex = 10;
            this.picResults.TabStop = false;
            this.picResults.Click += new System.EventHandler(this.picResults_Click);
            this.picResults.MouseEnter += new System.EventHandler(this.picResults_MouseEnter);
            this.picResults.MouseLeave += new System.EventHandler(this.picResults_MouseLeave);
            this.picResults.MouseHover += new System.EventHandler(this.picResults_MouseHover);
            // 
            // picStart
            // 
            this.picStart.Image = global::ProfessionalDocAnalyzer.Properties.Resources.ScionIcon110;
            this.picStart.Location = new System.Drawing.Point(35, 111);
            this.picStart.Margin = new System.Windows.Forms.Padding(4);
            this.picStart.Name = "picStart";
            this.picStart.Size = new System.Drawing.Size(153, 145);
            this.picStart.TabIndex = 11;
            this.picStart.TabStop = false;
            this.picStart.Click += new System.EventHandler(this.picStart_Click);
            this.picStart.MouseEnter += new System.EventHandler(this.picStart_MouseEnter);
            this.picStart.MouseLeave += new System.EventHandler(this.picStart_MouseLeave);
            this.picStart.MouseHover += new System.EventHandler(this.picStart_MouseHover);
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Margin = new System.Windows.Forms.Padding(4);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(548, 94);
            this.panel1.TabIndex = 7;
            this.panel1.Visible = false;
            // 
            // butSettings
            // 
            this.butSettings.Cursor = System.Windows.Forms.Cursors.No;
            this.butSettings.FlatAppearance.BorderSize = 0;
            this.butSettings.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butSettings.Location = new System.Drawing.Point(0, 0);
            this.butSettings.Margin = new System.Windows.Forms.Padding(4);
            this.butSettings.Name = "butSettings";
            this.butSettings.Size = new System.Drawing.Size(10, 10);
            this.butSettings.TabIndex = 13;
            this.butSettings.Visible = false;
            // 
            // butResults
            // 
            this.butResults.Cursor = System.Windows.Forms.Cursors.No;
            this.butResults.FlatAppearance.BorderSize = 0;
            this.butResults.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butResults.Location = new System.Drawing.Point(0, 0);
            this.butResults.Margin = new System.Windows.Forms.Padding(4);
            this.butResults.Name = "butResults";
            this.butResults.Size = new System.Drawing.Size(10, 10);
            this.butResults.TabIndex = 14;
            this.butResults.UseVisualStyleBackColor = false;
            this.butResults.Visible = false;
            // 
            // butStart
            // 
            this.butStart.Cursor = System.Windows.Forms.Cursors.No;
            this.butStart.FlatAppearance.BorderSize = 0;
            this.butStart.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.butStart.Location = new System.Drawing.Point(0, 0);
            this.butStart.Margin = new System.Windows.Forms.Padding(4);
            this.butStart.Name = "butStart";
            this.butStart.Size = new System.Drawing.Size(10, 10);
            this.butStart.TabIndex = 15;
            this.butStart.Visible = false;
            // 
            // panel2
            // 
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(200, 100);
            this.panel2.TabIndex = 0;
            // 
            // butQCTest
            // 
            this.butQCTest.Location = new System.Drawing.Point(63, 17);
            this.butQCTest.Name = "butQCTest";
            this.butQCTest.Size = new System.Drawing.Size(75, 23);
            this.butQCTest.TabIndex = 10;
            this.butQCTest.Text = "QC Test";
            this.butQCTest.UseVisualStyleBackColor = true;
            this.butQCTest.Visible = false;
            this.butQCTest.Click += new System.EventHandler(this.butQCTest_Click);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            this.metroToolTip1.Popup += new System.Windows.Forms.PopupEventHandler(this.metroToolTip1_Popup);
            // 
            // ucWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.splitContainer1);
            this.Location = new System.Drawing.Point(1920, 1080);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "ucWelcome";
            this.Size = new System.Drawing.Size(1392, 622);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panLeftPadding.ResumeLayout(false);
            this.panLeftPadding.PerformLayout();
            this.panMembers.ResumeLayout(false);
            this.panMembersHeader.ResumeLayout(false);
            this.panMembersHeader.PerformLayout();
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.pnlSetting.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picSettings)).EndInit();
            this.panRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picResults)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picStart)).EndInit();
            this.ResumeLayout(false);

        }

        //MouseHover Events
        private void picStart_MouseHover(object sender, EventArgs e)
        {
            picStart.Image = Properties.Resources.picStart110;               
        }

        private void picResults_MouseHover(object sender, EventArgs e)
        {
            picResults.Image = Properties.Resources.picResults110;
        }

        private void picSettings_MouseHover(object sender, EventArgs e)
        {
            picSettings.Image = Properties.Resources.settingsSmall;            
        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panLeftPadding;
        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Label lblCaption;
        private System.Windows.Forms.Panel panRight;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Label lblWorkgroups;
        private System.Windows.Forms.ComboBox cboWorkgroups;
        private System.Windows.Forms.Label lblWorkgroupPath;
        private System.Windows.Forms.Panel panMembers;
        private System.Windows.Forms.Label lblMemberInfo;
        private System.Windows.Forms.ListBox lstbMembers;
        private System.Windows.Forms.Panel panMembersHeader;
        private MetroFramework.Controls.MetroButton butEmailNewMembers;
        private System.Windows.Forms.Label lblMembers;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private MetroFramework.Controls.MetroButton butWorkgroup_Disconnect;
        private MetroFramework.Controls.MetroButton butWorkgroup_Connect;
        private MetroFramework.Controls.MetroButton butWorkgroup_Add;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Button butLasr;
        private System.Windows.Forms.Button butQCTest;
        private System.Windows.Forms.Label lblButtonInfo;
        private System.Windows.Forms.Panel panPaddingTopRight;
        private System.Windows.Forms.PictureBox picStart;
        private System.Windows.Forms.PictureBox picResults;
        private System.Windows.Forms.PictureBox picSettings;
        private Button butResults;
        private Button butStart;
        private Panel panel1;
        private Button butSettings;
        private Panel pnlSetting;
        private Label lblScion;
        private Label lblImagine;
        private FlowLayoutPanel flowLayoutPanel1;
    }
}
