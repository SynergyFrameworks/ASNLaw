namespace ProfessionalDocAnalyzer
{
    partial class frmQCSettings
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
            this.panFooter = new System.Windows.Forms.Panel();
            this.butCancel = new MetroFramework.Controls.MetroButton();
            this.butOK = new MetroFramework.Controls.MetroButton();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.panLS = new System.Windows.Forms.Panel();
            this.lblWordsGreaterThan = new System.Windows.Forms.Label();
            this.udWordsGreaterThan = new System.Windows.Forms.NumericUpDown();
            this.lblColorLS = new System.Windows.Forms.Label();
            this.cboColorLS = new System.Windows.Forms.ComboBox();
            this.cboImportantLS = new System.Windows.Forms.ComboBox();
            this.lblImportantLS = new System.Windows.Forms.Label();
            this.panHeaderLS = new System.Windows.Forms.Panel();
            this.lblHeaderLS = new System.Windows.Forms.Label();
            this.panCW = new System.Windows.Forms.Panel();
            this.lblColorCW = new System.Windows.Forms.Label();
            this.cboColorCW = new System.Windows.Forms.ComboBox();
            this.cboImportantCW = new System.Windows.Forms.ComboBox();
            this.lblImportantCW = new System.Windows.Forms.Label();
            this.panHeaderCW = new System.Windows.Forms.Panel();
            this.lblHeaderCW = new System.Windows.Forms.Label();
            this.panPV = new System.Windows.Forms.Panel();
            this.lblColorPV = new System.Windows.Forms.Label();
            this.cboColorPV = new System.Windows.Forms.ComboBox();
            this.cboImportantPV = new System.Windows.Forms.ComboBox();
            this.lblImportantPV = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.lblHeaderPV = new System.Windows.Forms.Label();
            this.panA = new System.Windows.Forms.Panel();
            this.lblColorA = new System.Windows.Forms.Label();
            this.cboColorA = new System.Windows.Forms.ComboBox();
            this.cboImportantA = new System.Windows.Forms.ComboBox();
            this.lblImportantA = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.lblHeaderA = new System.Windows.Forms.Label();
            this.panDT = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.lblColorDT = new System.Windows.Forms.Label();
            this.cboColorDT = new System.Windows.Forms.ComboBox();
            this.cboDictionary = new System.Windows.Forms.ComboBox();
            this.lblDictionary = new System.Windows.Forms.Label();
            this.panHeaderDT = new System.Windows.Forms.Panel();
            this.lblHeaderDT = new System.Windows.Forms.Label();
            this.panFooter.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.panLS.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udWordsGreaterThan)).BeginInit();
            this.panHeaderLS.SuspendLayout();
            this.panCW.SuspendLayout();
            this.panHeaderCW.SuspendLayout();
            this.panPV.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panA.SuspendLayout();
            this.panel4.SuspendLayout();
            this.panDT.SuspendLayout();
            this.panHeaderDT.SuspendLayout();
            this.SuspendLayout();
            // 
            // panFooter
            // 
            this.panFooter.Controls.Add(this.butCancel);
            this.panFooter.Controls.Add(this.butOK);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(27, 578);
            this.panFooter.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(883, 49);
            this.panFooter.TabIndex = 0;
            // 
            // butCancel
            // 
            this.butCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.butCancel.Location = new System.Drawing.Point(749, 11);
            this.butCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butCancel.Name = "butCancel";
            this.butCancel.Size = new System.Drawing.Size(100, 28);
            this.butCancel.TabIndex = 11;
            this.butCancel.Text = "Cancel";
            this.butCancel.UseSelectable = true;
            // 
            // butOK
            // 
            this.butOK.Location = new System.Drawing.Point(628, 11);
            this.butOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.butOK.Name = "butOK";
            this.butOK.Size = new System.Drawing.Size(100, 28);
            this.butOK.TabIndex = 10;
            this.butOK.Text = "Save";
            this.butOK.UseSelectable = true;
            this.butOK.Click += new System.EventHandler(this.butOK_Click);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoScroll = true;
            this.flowLayoutPanel1.Controls.Add(this.panLS);
            this.flowLayoutPanel1.Controls.Add(this.panCW);
            this.flowLayoutPanel1.Controls.Add(this.panPV);
            this.flowLayoutPanel1.Controls.Add(this.panA);
            this.flowLayoutPanel1.Controls.Add(this.panDT);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(27, 74);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(883, 504);
            this.flowLayoutPanel1.TabIndex = 1;
            this.flowLayoutPanel1.WrapContents = false;
            // 
            // panLS
            // 
            this.panLS.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panLS.Controls.Add(this.lblWordsGreaterThan);
            this.panLS.Controls.Add(this.udWordsGreaterThan);
            this.panLS.Controls.Add(this.lblColorLS);
            this.panLS.Controls.Add(this.cboColorLS);
            this.panLS.Controls.Add(this.cboImportantLS);
            this.panLS.Controls.Add(this.lblImportantLS);
            this.panLS.Controls.Add(this.panHeaderLS);
            this.panLS.Location = new System.Drawing.Point(4, 4);
            this.panLS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panLS.Name = "panLS";
            this.panLS.Size = new System.Drawing.Size(851, 142);
            this.panLS.TabIndex = 0;
            // 
            // lblWordsGreaterThan
            // 
            this.lblWordsGreaterThan.AutoSize = true;
            this.lblWordsGreaterThan.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWordsGreaterThan.ForeColor = System.Drawing.Color.Black;
            this.lblWordsGreaterThan.Location = new System.Drawing.Point(592, 49);
            this.lblWordsGreaterThan.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblWordsGreaterThan.Name = "lblWordsGreaterThan";
            this.lblWordsGreaterThan.Size = new System.Drawing.Size(158, 23);
            this.lblWordsGreaterThan.TabIndex = 86;
            this.lblWordsGreaterThan.Text = "Words greater than";
            // 
            // udWordsGreaterThan
            // 
            this.udWordsGreaterThan.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.udWordsGreaterThan.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.udWordsGreaterThan.Location = new System.Drawing.Point(763, 49);
            this.udWordsGreaterThan.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.udWordsGreaterThan.Minimum = new decimal(new int[] {
            2,
            0,
            0,
            0});
            this.udWordsGreaterThan.Name = "udWordsGreaterThan";
            this.udWordsGreaterThan.Size = new System.Drawing.Size(64, 27);
            this.udWordsGreaterThan.TabIndex = 85;
            this.udWordsGreaterThan.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            // 
            // lblColorLS
            // 
            this.lblColorLS.AutoSize = true;
            this.lblColorLS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorLS.ForeColor = System.Drawing.Color.Black;
            this.lblColorLS.Location = new System.Drawing.Point(183, 102);
            this.lblColorLS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorLS.Name = "lblColorLS";
            this.lblColorLS.Size = new System.Drawing.Size(126, 23);
            this.lblColorLS.TabIndex = 84;
            this.lblColorLS.Text = "Highlight Color";
            this.lblColorLS.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboColorLS
            // 
            this.cboColorLS.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboColorLS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColorLS.DropDownWidth = 240;
            this.cboColorLS.FormattingEnabled = true;
            this.cboColorLS.ItemHeight = 20;
            this.cboColorLS.Location = new System.Drawing.Point(319, 97);
            this.cboColorLS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboColorLS.Name = "cboColorLS";
            this.cboColorLS.Size = new System.Drawing.Size(403, 26);
            this.cboColorLS.TabIndex = 83;
            this.cboColorLS.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboColorLS_DrawItem);
            // 
            // cboImportantLS
            // 
            this.cboImportantLS.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImportantLS.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboImportantLS.FormattingEnabled = true;
            this.cboImportantLS.Location = new System.Drawing.Point(319, 49);
            this.cboImportantLS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboImportantLS.Name = "cboImportantLS";
            this.cboImportantLS.Size = new System.Drawing.Size(192, 28);
            this.cboImportantLS.TabIndex = 82;
            // 
            // lblImportantLS
            // 
            this.lblImportantLS.AutoSize = true;
            this.lblImportantLS.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportantLS.ForeColor = System.Drawing.Color.Black;
            this.lblImportantLS.Location = new System.Drawing.Point(19, 53);
            this.lblImportantLS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImportantLS.Name = "lblImportantLS";
            this.lblImportantLS.Size = new System.Drawing.Size(287, 23);
            this.lblImportantLS.TabIndex = 81;
            this.lblImportantLS.Text = "How important are Long Sentences?";
            // 
            // panHeaderLS
            // 
            this.panHeaderLS.BackColor = System.Drawing.Color.Black;
            this.panHeaderLS.Controls.Add(this.lblHeaderLS);
            this.panHeaderLS.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeaderLS.Location = new System.Drawing.Point(0, 0);
            this.panHeaderLS.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panHeaderLS.Name = "panHeaderLS";
            this.panHeaderLS.Size = new System.Drawing.Size(849, 38);
            this.panHeaderLS.TabIndex = 2;
            // 
            // lblHeaderLS
            // 
            this.lblHeaderLS.AutoSize = true;
            this.lblHeaderLS.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderLS.ForeColor = System.Drawing.Color.White;
            this.lblHeaderLS.Location = new System.Drawing.Point(8, 6);
            this.lblHeaderLS.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeaderLS.Name = "lblHeaderLS";
            this.lblHeaderLS.Size = new System.Drawing.Size(148, 28);
            this.lblHeaderLS.TabIndex = 132;
            this.lblHeaderLS.Text = "Long Sentences";
            // 
            // panCW
            // 
            this.panCW.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panCW.Controls.Add(this.lblColorCW);
            this.panCW.Controls.Add(this.cboColorCW);
            this.panCW.Controls.Add(this.cboImportantCW);
            this.panCW.Controls.Add(this.lblImportantCW);
            this.panCW.Controls.Add(this.panHeaderCW);
            this.panCW.Location = new System.Drawing.Point(4, 154);
            this.panCW.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panCW.Name = "panCW";
            this.panCW.Size = new System.Drawing.Size(851, 142);
            this.panCW.TabIndex = 1;
            // 
            // lblColorCW
            // 
            this.lblColorCW.AutoSize = true;
            this.lblColorCW.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorCW.ForeColor = System.Drawing.Color.Black;
            this.lblColorCW.Location = new System.Drawing.Point(183, 103);
            this.lblColorCW.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorCW.Name = "lblColorCW";
            this.lblColorCW.Size = new System.Drawing.Size(126, 23);
            this.lblColorCW.TabIndex = 84;
            this.lblColorCW.Text = "Highlight Color";
            this.lblColorCW.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboColorCW
            // 
            this.cboColorCW.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboColorCW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColorCW.DropDownWidth = 240;
            this.cboColorCW.FormattingEnabled = true;
            this.cboColorCW.ItemHeight = 20;
            this.cboColorCW.Location = new System.Drawing.Point(319, 98);
            this.cboColorCW.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboColorCW.Name = "cboColorCW";
            this.cboColorCW.Size = new System.Drawing.Size(403, 26);
            this.cboColorCW.TabIndex = 83;
            this.cboColorCW.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboColorCW_DrawItem);
            // 
            // cboImportantCW
            // 
            this.cboImportantCW.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImportantCW.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboImportantCW.FormattingEnabled = true;
            this.cboImportantCW.Location = new System.Drawing.Point(319, 50);
            this.cboImportantCW.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboImportantCW.Name = "cboImportantCW";
            this.cboImportantCW.Size = new System.Drawing.Size(192, 28);
            this.cboImportantCW.TabIndex = 82;
            // 
            // lblImportantCW
            // 
            this.lblImportantCW.AutoSize = true;
            this.lblImportantCW.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportantCW.ForeColor = System.Drawing.Color.Black;
            this.lblImportantCW.Location = new System.Drawing.Point(19, 54);
            this.lblImportantCW.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImportantCW.Name = "lblImportantCW";
            this.lblImportantCW.Size = new System.Drawing.Size(287, 23);
            this.lblImportantCW.TabIndex = 81;
            this.lblImportantCW.Text = "How important are Complex Words?";
            // 
            // panHeaderCW
            // 
            this.panHeaderCW.BackColor = System.Drawing.Color.Black;
            this.panHeaderCW.Controls.Add(this.lblHeaderCW);
            this.panHeaderCW.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeaderCW.Location = new System.Drawing.Point(0, 0);
            this.panHeaderCW.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panHeaderCW.Name = "panHeaderCW";
            this.panHeaderCW.Size = new System.Drawing.Size(849, 38);
            this.panHeaderCW.TabIndex = 2;
            // 
            // lblHeaderCW
            // 
            this.lblHeaderCW.AutoSize = true;
            this.lblHeaderCW.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderCW.ForeColor = System.Drawing.Color.White;
            this.lblHeaderCW.Location = new System.Drawing.Point(8, 6);
            this.lblHeaderCW.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeaderCW.Name = "lblHeaderCW";
            this.lblHeaderCW.Size = new System.Drawing.Size(151, 28);
            this.lblHeaderCW.TabIndex = 132;
            this.lblHeaderCW.Text = "Complex Words";
            // 
            // panPV
            // 
            this.panPV.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panPV.Controls.Add(this.lblColorPV);
            this.panPV.Controls.Add(this.cboColorPV);
            this.panPV.Controls.Add(this.cboImportantPV);
            this.panPV.Controls.Add(this.lblImportantPV);
            this.panPV.Controls.Add(this.panel2);
            this.panPV.Location = new System.Drawing.Point(4, 304);
            this.panPV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panPV.Name = "panPV";
            this.panPV.Size = new System.Drawing.Size(851, 142);
            this.panPV.TabIndex = 3;
            // 
            // lblColorPV
            // 
            this.lblColorPV.AutoSize = true;
            this.lblColorPV.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorPV.ForeColor = System.Drawing.Color.Black;
            this.lblColorPV.Location = new System.Drawing.Point(183, 102);
            this.lblColorPV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorPV.Name = "lblColorPV";
            this.lblColorPV.Size = new System.Drawing.Size(126, 23);
            this.lblColorPV.TabIndex = 84;
            this.lblColorPV.Text = "Highlight Color";
            this.lblColorPV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboColorPV
            // 
            this.cboColorPV.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboColorPV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColorPV.DropDownWidth = 240;
            this.cboColorPV.FormattingEnabled = true;
            this.cboColorPV.ItemHeight = 20;
            this.cboColorPV.Location = new System.Drawing.Point(319, 97);
            this.cboColorPV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboColorPV.Name = "cboColorPV";
            this.cboColorPV.Size = new System.Drawing.Size(403, 26);
            this.cboColorPV.TabIndex = 83;
            this.cboColorPV.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboColorPV_DrawItem);
            // 
            // cboImportantPV
            // 
            this.cboImportantPV.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImportantPV.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboImportantPV.FormattingEnabled = true;
            this.cboImportantPV.Location = new System.Drawing.Point(319, 49);
            this.cboImportantPV.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboImportantPV.Name = "cboImportantPV";
            this.cboImportantPV.Size = new System.Drawing.Size(192, 28);
            this.cboImportantPV.TabIndex = 82;
            // 
            // lblImportantPV
            // 
            this.lblImportantPV.AutoSize = true;
            this.lblImportantPV.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportantPV.ForeColor = System.Drawing.Color.Black;
            this.lblImportantPV.Location = new System.Drawing.Point(51, 50);
            this.lblImportantPV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImportantPV.Name = "lblImportantPV";
            this.lblImportantPV.Size = new System.Drawing.Size(253, 23);
            this.lblImportantPV.TabIndex = 81;
            this.lblImportantPV.Text = "How important is Passive Voice?";
            this.lblImportantPV.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.lblHeaderPV);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 0);
            this.panel2.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(849, 38);
            this.panel2.TabIndex = 2;
            // 
            // lblHeaderPV
            // 
            this.lblHeaderPV.AutoSize = true;
            this.lblHeaderPV.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderPV.ForeColor = System.Drawing.Color.White;
            this.lblHeaderPV.Location = new System.Drawing.Point(8, 6);
            this.lblHeaderPV.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeaderPV.Name = "lblHeaderPV";
            this.lblHeaderPV.Size = new System.Drawing.Size(125, 28);
            this.lblHeaderPV.TabIndex = 132;
            this.lblHeaderPV.Text = "Passive Voice";
            // 
            // panA
            // 
            this.panA.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panA.Controls.Add(this.lblColorA);
            this.panA.Controls.Add(this.cboColorA);
            this.panA.Controls.Add(this.cboImportantA);
            this.panA.Controls.Add(this.lblImportantA);
            this.panA.Controls.Add(this.panel4);
            this.panA.Location = new System.Drawing.Point(4, 454);
            this.panA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panA.Name = "panA";
            this.panA.Size = new System.Drawing.Size(851, 142);
            this.panA.TabIndex = 4;
            // 
            // lblColorA
            // 
            this.lblColorA.AutoSize = true;
            this.lblColorA.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorA.ForeColor = System.Drawing.Color.Black;
            this.lblColorA.Location = new System.Drawing.Point(183, 102);
            this.lblColorA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorA.Name = "lblColorA";
            this.lblColorA.Size = new System.Drawing.Size(126, 23);
            this.lblColorA.TabIndex = 84;
            this.lblColorA.Text = "Highlight Color";
            this.lblColorA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboColorA
            // 
            this.cboColorA.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboColorA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColorA.DropDownWidth = 240;
            this.cboColorA.FormattingEnabled = true;
            this.cboColorA.ItemHeight = 20;
            this.cboColorA.Location = new System.Drawing.Point(319, 97);
            this.cboColorA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboColorA.Name = "cboColorA";
            this.cboColorA.Size = new System.Drawing.Size(403, 26);
            this.cboColorA.TabIndex = 83;
            this.cboColorA.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboColorA_DrawItem);
            // 
            // cboImportantA
            // 
            this.cboImportantA.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboImportantA.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboImportantA.FormattingEnabled = true;
            this.cboImportantA.Location = new System.Drawing.Point(319, 49);
            this.cboImportantA.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboImportantA.Name = "cboImportantA";
            this.cboImportantA.Size = new System.Drawing.Size(192, 28);
            this.cboImportantA.TabIndex = 82;
            // 
            // lblImportantA
            // 
            this.lblImportantA.AutoSize = true;
            this.lblImportantA.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblImportantA.ForeColor = System.Drawing.Color.Black;
            this.lblImportantA.Location = new System.Drawing.Point(76, 49);
            this.lblImportantA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblImportantA.Name = "lblImportantA";
            this.lblImportantA.Size = new System.Drawing.Size(228, 23);
            this.lblImportantA.TabIndex = 81;
            this.lblImportantA.Text = "How important are Adverbs?";
            this.lblImportantA.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel4
            // 
            this.panel4.BackColor = System.Drawing.Color.Black;
            this.panel4.Controls.Add(this.lblHeaderA);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel4.Location = new System.Drawing.Point(0, 0);
            this.panel4.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(849, 38);
            this.panel4.TabIndex = 2;
            // 
            // lblHeaderA
            // 
            this.lblHeaderA.AutoSize = true;
            this.lblHeaderA.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderA.ForeColor = System.Drawing.Color.White;
            this.lblHeaderA.Location = new System.Drawing.Point(8, 6);
            this.lblHeaderA.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeaderA.Name = "lblHeaderA";
            this.lblHeaderA.Size = new System.Drawing.Size(84, 28);
            this.lblHeaderA.TabIndex = 132;
            this.lblHeaderA.Text = "Adverbs";
            // 
            // panDT
            // 
            this.panDT.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panDT.Controls.Add(this.label1);
            this.panDT.Controls.Add(this.lblColorDT);
            this.panDT.Controls.Add(this.cboColorDT);
            this.panDT.Controls.Add(this.cboDictionary);
            this.panDT.Controls.Add(this.lblDictionary);
            this.panDT.Controls.Add(this.panHeaderDT);
            this.panDT.Location = new System.Drawing.Point(4, 604);
            this.panDT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panDT.Name = "panDT";
            this.panDT.Size = new System.Drawing.Size(851, 142);
            this.panDT.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(19, 98);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(640, 23);
            this.label1.TabIndex = 85;
            this.label1.Text = "The Dictionary is selected at the Settings Task level.   Using a Dictionary is op" +
    "tional.";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // lblColorDT
            // 
            this.lblColorDT.AutoSize = true;
            this.lblColorDT.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblColorDT.ForeColor = System.Drawing.Color.Black;
            this.lblColorDT.Location = new System.Drawing.Point(183, 55);
            this.lblColorDT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblColorDT.Name = "lblColorDT";
            this.lblColorDT.Size = new System.Drawing.Size(126, 23);
            this.lblColorDT.TabIndex = 84;
            this.lblColorDT.Text = "Highlight Color";
            this.lblColorDT.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboColorDT
            // 
            this.cboColorDT.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboColorDT.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboColorDT.DropDownWidth = 240;
            this.cboColorDT.FormattingEnabled = true;
            this.cboColorDT.ItemHeight = 20;
            this.cboColorDT.Location = new System.Drawing.Point(319, 50);
            this.cboColorDT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboColorDT.Name = "cboColorDT";
            this.cboColorDT.Size = new System.Drawing.Size(403, 26);
            this.cboColorDT.TabIndex = 83;
            this.cboColorDT.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.cboColorDT_DrawItem);
            // 
            // cboDictionary
            // 
            this.cboDictionary.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDictionary.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDictionary.FormattingEnabled = true;
            this.cboDictionary.Location = new System.Drawing.Point(643, 97);
            this.cboDictionary.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cboDictionary.Name = "cboDictionary";
            this.cboDictionary.Size = new System.Drawing.Size(192, 28);
            this.cboDictionary.TabIndex = 82;
            this.cboDictionary.Visible = false;
            // 
            // lblDictionary
            // 
            this.lblDictionary.AutoSize = true;
            this.lblDictionary.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblDictionary.ForeColor = System.Drawing.Color.Black;
            this.lblDictionary.Location = new System.Drawing.Point(547, 97);
            this.lblDictionary.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblDictionary.Name = "lblDictionary";
            this.lblDictionary.Size = new System.Drawing.Size(87, 23);
            this.lblDictionary.TabIndex = 81;
            this.lblDictionary.Text = "Dictionary";
            this.lblDictionary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblDictionary.Visible = false;
            // 
            // panHeaderDT
            // 
            this.panHeaderDT.BackColor = System.Drawing.Color.Black;
            this.panHeaderDT.Controls.Add(this.lblHeaderDT);
            this.panHeaderDT.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeaderDT.Location = new System.Drawing.Point(0, 0);
            this.panHeaderDT.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.panHeaderDT.Name = "panHeaderDT";
            this.panHeaderDT.Size = new System.Drawing.Size(849, 38);
            this.panHeaderDT.TabIndex = 2;
            // 
            // lblHeaderDT
            // 
            this.lblHeaderDT.AutoSize = true;
            this.lblHeaderDT.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeaderDT.ForeColor = System.Drawing.Color.White;
            this.lblHeaderDT.Location = new System.Drawing.Point(8, 6);
            this.lblHeaderDT.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblHeaderDT.Name = "lblHeaderDT";
            this.lblHeaderDT.Size = new System.Drawing.Size(157, 28);
            this.lblHeaderDT.TabIndex = 132;
            this.lblHeaderDT.Text = "Dictionary Terms";
            // 
            // frmQCSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BorderStyle = MetroFramework.Forms.MetroFormBorderStyle.FixedSingle;
            this.ClientSize = new System.Drawing.Size(937, 629);
            this.ControlBox = false;
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.panFooter);
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.Name = "frmQCSettings";
            this.Padding = new System.Windows.Forms.Padding(27, 74, 27, 2);
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Show;
            this.Text = "QC Analysis Readability Settings";
            this.panFooter.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.panLS.ResumeLayout(false);
            this.panLS.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.udWordsGreaterThan)).EndInit();
            this.panHeaderLS.ResumeLayout(false);
            this.panHeaderLS.PerformLayout();
            this.panCW.ResumeLayout(false);
            this.panCW.PerformLayout();
            this.panHeaderCW.ResumeLayout(false);
            this.panHeaderCW.PerformLayout();
            this.panPV.ResumeLayout(false);
            this.panPV.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.panA.ResumeLayout(false);
            this.panA.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.panDT.ResumeLayout(false);
            this.panDT.PerformLayout();
            this.panHeaderDT.ResumeLayout(false);
            this.panHeaderDT.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panFooter;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Panel panLS;
        private System.Windows.Forms.Panel panHeaderLS;
        private System.Windows.Forms.Label lblHeaderLS;
        private System.Windows.Forms.ComboBox cboImportantLS;
        private System.Windows.Forms.Label lblImportantLS;
        private System.Windows.Forms.Label lblColorLS;
        private System.Windows.Forms.ComboBox cboColorLS;
        private System.Windows.Forms.Panel panCW;
        private System.Windows.Forms.Label lblColorCW;
        private System.Windows.Forms.ComboBox cboColorCW;
        private System.Windows.Forms.ComboBox cboImportantCW;
        private System.Windows.Forms.Label lblImportantCW;
        private System.Windows.Forms.Panel panHeaderCW;
        private System.Windows.Forms.Label lblHeaderCW;
        private System.Windows.Forms.Panel panPV;
        private System.Windows.Forms.Label lblColorPV;
        private System.Windows.Forms.ComboBox cboColorPV;
        private System.Windows.Forms.ComboBox cboImportantPV;
        private System.Windows.Forms.Label lblImportantPV;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label lblHeaderPV;
        private System.Windows.Forms.Label lblWordsGreaterThan;
        private System.Windows.Forms.NumericUpDown udWordsGreaterThan;
        private System.Windows.Forms.Panel panA;
        private System.Windows.Forms.Label lblColorA;
        private System.Windows.Forms.ComboBox cboColorA;
        private System.Windows.Forms.ComboBox cboImportantA;
        private System.Windows.Forms.Label lblImportantA;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label lblHeaderA;
        private System.Windows.Forms.Panel panDT;
        private System.Windows.Forms.Label lblColorDT;
        private System.Windows.Forms.ComboBox cboColorDT;
        private System.Windows.Forms.ComboBox cboDictionary;
        private System.Windows.Forms.Label lblDictionary;
        private System.Windows.Forms.Panel panHeaderDT;
        private System.Windows.Forms.Label lblHeaderDT;
        private MetroFramework.Controls.MetroButton butCancel;
        private MetroFramework.Controls.MetroButton butOK;
        private System.Windows.Forms.Label label1;
    }
}