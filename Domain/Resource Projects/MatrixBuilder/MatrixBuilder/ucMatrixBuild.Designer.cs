namespace MatrixBuilder
{
    partial class ucMatrixBuild
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucMatrixBuild));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panTopButtons = new System.Windows.Forms.Panel();
            this.butDocs = new MetroFramework.Controls.MetroButton();
            this.butStoryboard = new MetroFramework.Controls.MetroButton();
            this.butSummary = new MetroFramework.Controls.MetroButton();
            this.butRefRes = new MetroFramework.Controls.MetroButton();
            this.butDocTypes = new MetroFramework.Controls.MetroButton();
            this.butList = new MetroFramework.Controls.MetroButton();
            this.butMatrix = new MetroFramework.Controls.MetroButton();
            this.reoGridControl1 = new unvell.ReoGrid.ReoGridControl();
            this.panBottomHeader = new System.Windows.Forms.Panel();
            this.lblMatrix = new System.Windows.Forms.Label();
            this.lblAllocations = new System.Windows.Forms.Label();
            this.lblRows = new System.Windows.Forms.Label();
            this.butRemoveRow = new MetroFramework.Controls.MetroButton();
            this.butAddRow = new MetroFramework.Controls.MetroButton();
            this.butEmail = new MetroFramework.Controls.MetroButton();
            this.panBottomHeaderRight = new System.Windows.Forms.Panel();
            this.metroButton1 = new MetroFramework.Controls.MetroButton();
            this.butExcelEdit = new MetroFramework.Controls.MetroButton();
            this.lblMatrixEditCaption = new System.Windows.Forms.Label();
            this.butSave = new MetroFramework.Controls.MetroButton();
            this.butCellInformation = new MetroFramework.Controls.MetroButton();
            this.butDeleteAllocatedItems = new MetroFramework.Controls.MetroButton();
            this.butAdd = new MetroFramework.Controls.MetroButton();
            this.butExportExcel = new MetroFramework.Controls.MetroButton();
            this.butPrintMatrix = new MetroFramework.Controls.MetroButton();
            this.panFooter = new System.Windows.Forms.Panel();
            this.lblQtyAllocationsRemoved = new System.Windows.Forms.Label();
            this.panFooterRight = new System.Windows.Forms.Panel();
            this.lblMatrixScale = new System.Windows.Forms.Label();
            this.mtrb = new MetroFramework.Controls.MetroTrackBar();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.ucMatrixSB1 = new MatrixBuilder.ucMatrixSB();
            this.ucMatrixRefRes1 = new MatrixBuilder.ucMatrixRefRes();
            this.ucMatrixList1 = new MatrixBuilder.ucMatrixList();
            this.ucMatrixDocTypes21 = new MatrixBuilder.ucMatrixDocTypes2();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panTopButtons.SuspendLayout();
            this.panBottomHeader.SuspendLayout();
            this.panBottomHeaderRight.SuspendLayout();
            this.panFooter.SuspendLayout();
            this.panFooterRight.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.ucMatrixSB1);
            this.splitContainer1.Panel1.Controls.Add(this.ucMatrixRefRes1);
            this.splitContainer1.Panel1.Controls.Add(this.ucMatrixList1);
            this.splitContainer1.Panel1.Controls.Add(this.ucMatrixDocTypes21);
            this.splitContainer1.Panel1.Controls.Add(this.panTopButtons);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.reoGridControl1);
            this.splitContainer1.Panel2.Controls.Add(this.panBottomHeader);
            this.splitContainer1.Size = new System.Drawing.Size(969, 512);
            this.splitContainer1.SplitterDistance = 274;
            this.splitContainer1.SplitterWidth = 8;
            this.splitContainer1.TabIndex = 2;
            // 
            // panTopButtons
            // 
            this.panTopButtons.AutoScroll = true;
            this.panTopButtons.BackColor = System.Drawing.Color.Black;
            this.panTopButtons.Controls.Add(this.butDocs);
            this.panTopButtons.Controls.Add(this.butStoryboard);
            this.panTopButtons.Controls.Add(this.butSummary);
            this.panTopButtons.Controls.Add(this.butRefRes);
            this.panTopButtons.Controls.Add(this.butDocTypes);
            this.panTopButtons.Controls.Add(this.butList);
            this.panTopButtons.Controls.Add(this.butMatrix);
            this.panTopButtons.Dock = System.Windows.Forms.DockStyle.Left;
            this.panTopButtons.Location = new System.Drawing.Point(0, 0);
            this.panTopButtons.Name = "panTopButtons";
            this.panTopButtons.Size = new System.Drawing.Size(87, 274);
            this.panTopButtons.TabIndex = 0;
            // 
            // butDocs
            // 
            this.butDocs.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butDocs.BackgroundImage")));
            this.butDocs.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butDocs.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butDocs.ForeColor = System.Drawing.Color.White;
            this.butDocs.Location = new System.Drawing.Point(9, 6);
            this.butDocs.Name = "butDocs";
            this.butDocs.Size = new System.Drawing.Size(38, 38);
            this.butDocs.TabIndex = 19;
            this.butDocs.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butDocs.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butDocs, "Documents");
            this.butDocs.UseSelectable = true;
            this.butDocs.Click += new System.EventHandler(this.butDocs_Click);
            // 
            // butStoryboard
            // 
            this.butStoryboard.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butStoryboard.BackgroundImage")));
            this.butStoryboard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butStoryboard.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butStoryboard.ForeColor = System.Drawing.Color.White;
            this.butStoryboard.Location = new System.Drawing.Point(9, 138);
            this.butStoryboard.Name = "butStoryboard";
            this.butStoryboard.Size = new System.Drawing.Size(38, 38);
            this.butStoryboard.TabIndex = 18;
            this.butStoryboard.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butStoryboard.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butStoryboard, "Storyboards");
            this.butStoryboard.UseSelectable = true;
            this.butStoryboard.Click += new System.EventHandler(this.butStoryboard_Click);
            // 
            // butSummary
            // 
            this.butSummary.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butSummary.BackgroundImage")));
            this.butSummary.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butSummary.ForeColor = System.Drawing.Color.White;
            this.butSummary.Location = new System.Drawing.Point(9, 225);
            this.butSummary.Name = "butSummary";
            this.butSummary.Size = new System.Drawing.Size(38, 38);
            this.butSummary.TabIndex = 17;
            this.butSummary.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butSummary.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butSummary.UseSelectable = true;
            this.butSummary.Visible = false;
            // 
            // butRefRes
            // 
            this.butRefRes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butRefRes.BackgroundImage")));
            this.butRefRes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butRefRes.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butRefRes.ForeColor = System.Drawing.Color.White;
            this.butRefRes.Location = new System.Drawing.Point(9, 94);
            this.butRefRes.Name = "butRefRes";
            this.butRefRes.Size = new System.Drawing.Size(38, 38);
            this.butRefRes.TabIndex = 16;
            this.butRefRes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butRefRes.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butRefRes, "Reference Resources");
            this.butRefRes.UseSelectable = true;
            this.butRefRes.Click += new System.EventHandler(this.butRefRes_Click);
            // 
            // butDocTypes
            // 
            this.butDocTypes.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butDocTypes.BackgroundImage")));
            this.butDocTypes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butDocTypes.CausesValidation = false;
            this.butDocTypes.ForeColor = System.Drawing.Color.White;
            this.butDocTypes.Highlight = true;
            this.butDocTypes.Location = new System.Drawing.Point(9, 244);
            this.butDocTypes.Name = "butDocTypes";
            this.butDocTypes.Size = new System.Drawing.Size(38, 38);
            this.butDocTypes.TabIndex = 15;
            this.butDocTypes.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butDocTypes.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butDocTypes.UseSelectable = true;
            this.butDocTypes.Visible = false;
            this.butDocTypes.Click += new System.EventHandler(this.butDocTypes_Click);
            this.butDocTypes.MouseDown += new System.Windows.Forms.MouseEventHandler(this.butDocTypes_MouseDown);
            // 
            // butList
            // 
            this.butList.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butList.BackgroundImage")));
            this.butList.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butList.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butList.ForeColor = System.Drawing.Color.White;
            this.butList.Location = new System.Drawing.Point(9, 50);
            this.butList.Name = "butList";
            this.butList.Size = new System.Drawing.Size(38, 38);
            this.butList.TabIndex = 14;
            this.butList.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butList.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butList, "Lists");
            this.butList.UseSelectable = true;
            this.butList.Click += new System.EventHandler(this.butList_Click);
            // 
            // butMatrix
            // 
            this.butMatrix.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butMatrix.BackgroundImage")));
            this.butMatrix.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butMatrix.ForeColor = System.Drawing.Color.White;
            this.butMatrix.Location = new System.Drawing.Point(29, 225);
            this.butMatrix.Name = "butMatrix";
            this.butMatrix.Size = new System.Drawing.Size(38, 38);
            this.butMatrix.TabIndex = 13;
            this.butMatrix.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butMatrix.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butMatrix.UseSelectable = true;
            this.butMatrix.Visible = false;
            this.butMatrix.Click += new System.EventHandler(this.butMatrix_Click);
            // 
            // reoGridControl1
            // 
            this.reoGridControl1.AllowDrop = true;
            this.reoGridControl1.BackColor = System.Drawing.Color.White;
            this.reoGridControl1.ColumnHeaderContextMenuStrip = null;
            this.reoGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reoGridControl1.LeadHeaderContextMenuStrip = null;
            this.reoGridControl1.Location = new System.Drawing.Point(0, 37);
            this.reoGridControl1.Name = "reoGridControl1";
            this.reoGridControl1.RowHeaderContextMenuStrip = null;
            this.reoGridControl1.Script = null;
            this.reoGridControl1.SheetTabContextMenuStrip = null;
            this.reoGridControl1.SheetTabNewButtonVisible = true;
            this.reoGridControl1.SheetTabVisible = true;
            this.reoGridControl1.SheetTabWidth = 60;
            this.reoGridControl1.ShowScrollEndSpacing = true;
            this.reoGridControl1.Size = new System.Drawing.Size(969, 193);
            this.reoGridControl1.TabIndex = 4;
            this.reoGridControl1.Text = "reoGridControl1";
            this.reoGridControl1.DragDrop += new System.Windows.Forms.DragEventHandler(this.reoGridControl1_DragDrop);
            this.reoGridControl1.DragEnter += new System.Windows.Forms.DragEventHandler(this.reoGridControl1_DragEnter);
            this.reoGridControl1.DragOver += new System.Windows.Forms.DragEventHandler(this.reoGridControl1_DragOver);
            // 
            // panBottomHeader
            // 
            this.panBottomHeader.BackColor = System.Drawing.Color.Black;
            this.panBottomHeader.Controls.Add(this.lblMatrix);
            this.panBottomHeader.Controls.Add(this.lblAllocations);
            this.panBottomHeader.Controls.Add(this.lblRows);
            this.panBottomHeader.Controls.Add(this.butRemoveRow);
            this.panBottomHeader.Controls.Add(this.butAddRow);
            this.panBottomHeader.Controls.Add(this.butEmail);
            this.panBottomHeader.Controls.Add(this.panBottomHeaderRight);
            this.panBottomHeader.Controls.Add(this.butSave);
            this.panBottomHeader.Controls.Add(this.butCellInformation);
            this.panBottomHeader.Controls.Add(this.butDeleteAllocatedItems);
            this.panBottomHeader.Controls.Add(this.butAdd);
            this.panBottomHeader.Controls.Add(this.butExportExcel);
            this.panBottomHeader.Controls.Add(this.butPrintMatrix);
            this.panBottomHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panBottomHeader.Location = new System.Drawing.Point(0, 0);
            this.panBottomHeader.Name = "panBottomHeader";
            this.panBottomHeader.Size = new System.Drawing.Size(969, 37);
            this.panBottomHeader.TabIndex = 0;
            // 
            // lblMatrix
            // 
            this.lblMatrix.AutoSize = true;
            this.lblMatrix.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatrix.ForeColor = System.Drawing.Color.White;
            this.lblMatrix.Location = new System.Drawing.Point(6, 11);
            this.lblMatrix.Name = "lblMatrix";
            this.lblMatrix.Size = new System.Drawing.Size(45, 17);
            this.lblMatrix.TabIndex = 193;
            this.lblMatrix.Text = "Matrix";
            // 
            // lblAllocations
            // 
            this.lblAllocations.AutoSize = true;
            this.lblAllocations.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblAllocations.ForeColor = System.Drawing.Color.White;
            this.lblAllocations.Location = new System.Drawing.Point(241, 11);
            this.lblAllocations.Name = "lblAllocations";
            this.lblAllocations.Size = new System.Drawing.Size(71, 17);
            this.lblAllocations.TabIndex = 192;
            this.lblAllocations.Text = "Allocations";
            // 
            // lblRows
            // 
            this.lblRows.AutoSize = true;
            this.lblRows.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblRows.ForeColor = System.Drawing.Color.White;
            this.lblRows.Location = new System.Drawing.Point(411, 11);
            this.lblRows.Name = "lblRows";
            this.lblRows.Size = new System.Drawing.Size(39, 17);
            this.lblRows.TabIndex = 191;
            this.lblRows.Text = "Rows";
            // 
            // butRemoveRow
            // 
            this.butRemoveRow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butRemoveRow.BackgroundImage")));
            this.butRemoveRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butRemoveRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butRemoveRow.ForeColor = System.Drawing.Color.White;
            this.butRemoveRow.Location = new System.Drawing.Point(490, 3);
            this.butRemoveRow.Name = "butRemoveRow";
            this.butRemoveRow.Size = new System.Drawing.Size(28, 28);
            this.butRemoveRow.TabIndex = 18;
            this.butRemoveRow.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butRemoveRow.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butRemoveRow, "Remove selected matrix row");
            this.butRemoveRow.UseSelectable = true;
            this.butRemoveRow.Click += new System.EventHandler(this.butRemoveRow_Click);
            // 
            // butAddRow
            // 
            this.butAddRow.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butAddRow.BackgroundImage")));
            this.butAddRow.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butAddRow.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butAddRow.ForeColor = System.Drawing.Color.White;
            this.butAddRow.Location = new System.Drawing.Point(456, 3);
            this.butAddRow.Name = "butAddRow";
            this.butAddRow.Size = new System.Drawing.Size(28, 28);
            this.butAddRow.TabIndex = 17;
            this.butAddRow.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butAddRow.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butAddRow, "Add new matrix row");
            this.butAddRow.UseSelectable = true;
            this.butAddRow.Click += new System.EventHandler(this.butAddRow_Click);
            // 
            // butEmail
            // 
            this.butEmail.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butEmail.BackgroundImage")));
            this.butEmail.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butEmail.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butEmail.ForeColor = System.Drawing.Color.White;
            this.butEmail.Location = new System.Drawing.Point(165, 4);
            this.butEmail.Name = "butEmail";
            this.butEmail.Size = new System.Drawing.Size(28, 28);
            this.butEmail.TabIndex = 16;
            this.butEmail.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butEmail.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butEmail, "Email Matrix");
            this.butEmail.UseSelectable = true;
            this.butEmail.Visible = false;
            this.butEmail.Click += new System.EventHandler(this.butEmail_Click);
            // 
            // panBottomHeaderRight
            // 
            this.panBottomHeaderRight.Controls.Add(this.metroButton1);
            this.panBottomHeaderRight.Controls.Add(this.butExcelEdit);
            this.panBottomHeaderRight.Controls.Add(this.lblMatrixEditCaption);
            this.panBottomHeaderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panBottomHeaderRight.Location = new System.Drawing.Point(775, 0);
            this.panBottomHeaderRight.Name = "panBottomHeaderRight";
            this.panBottomHeaderRight.Size = new System.Drawing.Size(194, 37);
            this.panBottomHeaderRight.TabIndex = 15;
            // 
            // metroButton1
            // 
            this.metroButton1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("metroButton1.BackgroundImage")));
            this.metroButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.metroButton1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.metroButton1.ForeColor = System.Drawing.Color.White;
            this.metroButton1.Location = new System.Drawing.Point(147, 3);
            this.metroButton1.Name = "metroButton1";
            this.metroButton1.Size = new System.Drawing.Size(28, 28);
            this.metroButton1.TabIndex = 38;
            this.metroButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.metroButton1.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.metroButton1, "Reloads the Matrix Excel file\r\n\r\nNotice: Make certain the \r\nMatrix Excel file has" +
        " been closed.");
            this.metroButton1.UseSelectable = true;
            this.metroButton1.Click += new System.EventHandler(this.metroButton1_Click);
            // 
            // butExcelEdit
            // 
            this.butExcelEdit.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butExcelEdit.BackgroundImage")));
            this.butExcelEdit.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butExcelEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butExcelEdit.ForeColor = System.Drawing.Color.White;
            this.butExcelEdit.Location = new System.Drawing.Point(113, 4);
            this.butExcelEdit.Name = "butExcelEdit";
            this.butExcelEdit.Size = new System.Drawing.Size(28, 28);
            this.butExcelEdit.TabIndex = 37;
            this.butExcelEdit.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butExcelEdit.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butExcelEdit, "Edit Matrix Excel file\r\n\r\nNotice: Be sure to close the Excel file\r\nafter making c" +
        "hanges.");
            this.butExcelEdit.UseSelectable = true;
            this.butExcelEdit.Click += new System.EventHandler(this.butExcelEdit_Click);
            // 
            // lblMatrixEditCaption
            // 
            this.lblMatrixEditCaption.AutoSize = true;
            this.lblMatrixEditCaption.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatrixEditCaption.ForeColor = System.Drawing.Color.White;
            this.lblMatrixEditCaption.Location = new System.Drawing.Point(3, 11);
            this.lblMatrixEditCaption.Name = "lblMatrixEditCaption";
            this.lblMatrixEditCaption.Size = new System.Drawing.Size(104, 17);
            this.lblMatrixEditCaption.TabIndex = 36;
            this.lblMatrixEditCaption.Text = "Edit Excel Matrix";
            // 
            // butSave
            // 
            this.butSave.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butSave.BackgroundImage")));
            this.butSave.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butSave.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butSave.ForeColor = System.Drawing.Color.White;
            this.butSave.Location = new System.Drawing.Point(53, 4);
            this.butSave.Name = "butSave";
            this.butSave.Size = new System.Drawing.Size(28, 28);
            this.butSave.TabIndex = 14;
            this.butSave.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butSave.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butSave, "Save Matrix");
            this.butSave.UseSelectable = true;
            this.butSave.Click += new System.EventHandler(this.butSave_Click);
            // 
            // butCellInformation
            // 
            this.butCellInformation.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butCellInformation.BackgroundImage")));
            this.butCellInformation.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butCellInformation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butCellInformation.ForeColor = System.Drawing.Color.White;
            this.butCellInformation.Location = new System.Drawing.Point(312, 3);
            this.butCellInformation.Name = "butCellInformation";
            this.butCellInformation.Size = new System.Drawing.Size(28, 28);
            this.butCellInformation.TabIndex = 13;
            this.butCellInformation.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butCellInformation.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butCellInformation, "Allocation Information\r\nSelect a Matrix Cell containing allocation(s)");
            this.butCellInformation.UseSelectable = true;
            this.butCellInformation.Click += new System.EventHandler(this.butCellInformation_Click);
            // 
            // butDeleteAllocatedItems
            // 
            this.butDeleteAllocatedItems.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butDeleteAllocatedItems.BackgroundImage")));
            this.butDeleteAllocatedItems.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butDeleteAllocatedItems.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butDeleteAllocatedItems.ForeColor = System.Drawing.Color.White;
            this.butDeleteAllocatedItems.Location = new System.Drawing.Point(346, 4);
            this.butDeleteAllocatedItems.Name = "butDeleteAllocatedItems";
            this.butDeleteAllocatedItems.Size = new System.Drawing.Size(28, 28);
            this.butDeleteAllocatedItems.TabIndex = 12;
            this.butDeleteAllocatedItems.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butDeleteAllocatedItems.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butDeleteAllocatedItems, "Select Allocation to Remove");
            this.butDeleteAllocatedItems.UseSelectable = true;
            this.butDeleteAllocatedItems.Click += new System.EventHandler(this.butDeleteAllocatedItems_Click);
            // 
            // butAdd
            // 
            this.butAdd.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butAdd.BackgroundImage")));
            this.butAdd.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butAdd.ForeColor = System.Drawing.Color.White;
            this.butAdd.Location = new System.Drawing.Point(591, 6);
            this.butAdd.Name = "butAdd";
            this.butAdd.Size = new System.Drawing.Size(28, 28);
            this.butAdd.TabIndex = 11;
            this.butAdd.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butAdd.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.butAdd.UseSelectable = true;
            this.butAdd.Visible = false;
            // 
            // butExportExcel
            // 
            this.butExportExcel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butExportExcel.BackgroundImage")));
            this.butExportExcel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butExportExcel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butExportExcel.ForeColor = System.Drawing.Color.White;
            this.butExportExcel.Location = new System.Drawing.Point(128, 4);
            this.butExportExcel.Name = "butExportExcel";
            this.butExportExcel.Size = new System.Drawing.Size(28, 28);
            this.butExportExcel.TabIndex = 10;
            this.butExportExcel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butExportExcel.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butExportExcel, "Export Matrix to Excel\r\nChanges to a Exported Matrix \r\nwill Not be saved into the" +
        " Matrix.");
            this.butExportExcel.UseSelectable = true;
            this.butExportExcel.Click += new System.EventHandler(this.butExportExcel_Click);
            // 
            // butPrintMatrix
            // 
            this.butPrintMatrix.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("butPrintMatrix.BackgroundImage")));
            this.butPrintMatrix.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.butPrintMatrix.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butPrintMatrix.ForeColor = System.Drawing.Color.White;
            this.butPrintMatrix.Location = new System.Drawing.Point(91, 4);
            this.butPrintMatrix.Name = "butPrintMatrix";
            this.butPrintMatrix.Size = new System.Drawing.Size(28, 28);
            this.butPrintMatrix.TabIndex = 9;
            this.butPrintMatrix.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.butPrintMatrix.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.metroToolTip1.SetToolTip(this.butPrintMatrix, "Print Matrix");
            this.butPrintMatrix.UseSelectable = true;
            this.butPrintMatrix.Click += new System.EventHandler(this.butPrintMatrix_Click);
            // 
            // panFooter
            // 
            this.panFooter.BackColor = System.Drawing.Color.Black;
            this.panFooter.Controls.Add(this.lblQtyAllocationsRemoved);
            this.panFooter.Controls.Add(this.panFooterRight);
            this.panFooter.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panFooter.Location = new System.Drawing.Point(0, 512);
            this.panFooter.Name = "panFooter";
            this.panFooter.Size = new System.Drawing.Size(969, 31);
            this.panFooter.TabIndex = 1;
            // 
            // lblQtyAllocationsRemoved
            // 
            this.lblQtyAllocationsRemoved.AutoSize = true;
            this.lblQtyAllocationsRemoved.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblQtyAllocationsRemoved.ForeColor = System.Drawing.Color.Lime;
            this.lblQtyAllocationsRemoved.Location = new System.Drawing.Point(4, 9);
            this.lblQtyAllocationsRemoved.Name = "lblQtyAllocationsRemoved";
            this.lblQtyAllocationsRemoved.Size = new System.Drawing.Size(154, 17);
            this.lblQtyAllocationsRemoved.TabIndex = 190;
            this.lblQtyAllocationsRemoved.Text = "Qty Allocations Removed";
            this.lblQtyAllocationsRemoved.Visible = false;
            // 
            // panFooterRight
            // 
            this.panFooterRight.Controls.Add(this.lblMatrixScale);
            this.panFooterRight.Controls.Add(this.mtrb);
            this.panFooterRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panFooterRight.Location = new System.Drawing.Point(662, 0);
            this.panFooterRight.Name = "panFooterRight";
            this.panFooterRight.Size = new System.Drawing.Size(307, 31);
            this.panFooterRight.TabIndex = 1;
            // 
            // lblMatrixScale
            // 
            this.lblMatrixScale.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMatrixScale.AutoSize = true;
            this.lblMatrixScale.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblMatrixScale.ForeColor = System.Drawing.Color.White;
            this.lblMatrixScale.Location = new System.Drawing.Point(14, 8);
            this.lblMatrixScale.Name = "lblMatrixScale";
            this.lblMatrixScale.Size = new System.Drawing.Size(40, 17);
            this.lblMatrixScale.TabIndex = 190;
            this.lblMatrixScale.Text = "100%";
            // 
            // mtrb
            // 
            this.mtrb.BackColor = System.Drawing.Color.Transparent;
            this.mtrb.Location = new System.Drawing.Point(57, 10);
            this.mtrb.Maximum = 40;
            this.mtrb.Name = "mtrb";
            this.mtrb.Size = new System.Drawing.Size(239, 15);
            this.mtrb.TabIndex = 1;
            this.mtrb.Text = "metroTrackBar1";
            this.mtrb.Theme = MetroFramework.MetroThemeStyle.Dark;
            this.mtrb.Value = 10;
            this.mtrb.Scroll += new System.Windows.Forms.ScrollEventHandler(this.mtrb_Scroll);
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // ucMatrixSB1
            // 
            this.ucMatrixSB1.BackColor = System.Drawing.Color.Black;
            this.ucMatrixSB1.Location = new System.Drawing.Point(93, 146);
            this.ucMatrixSB1.Name = "ucMatrixSB1";
            this.ucMatrixSB1.Size = new System.Drawing.Size(809, 363);
            this.ucMatrixSB1.TabIndex = 4;
            this.ucMatrixSB1.Visible = false;
            this.ucMatrixSB1.RowSelected += new MatrixBuilder.ucMatrixSB.ProcessHandler(this.ucMatrixSB1_RowSelected);
            // 
            // ucMatrixRefRes1
            // 
            this.ucMatrixRefRes1.BackColor = System.Drawing.Color.Black;
            this.ucMatrixRefRes1.Location = new System.Drawing.Point(139, 83);
            this.ucMatrixRefRes1.Name = "ucMatrixRefRes1";
            this.ucMatrixRefRes1.Size = new System.Drawing.Size(795, 335);
            this.ucMatrixRefRes1.TabIndex = 3;
            this.ucMatrixRefRes1.Visible = false;
            // 
            // ucMatrixList1
            // 
            this.ucMatrixList1.BackColor = System.Drawing.Color.Black;
            this.ucMatrixList1.Location = new System.Drawing.Point(244, 49);
            this.ucMatrixList1.Name = "ucMatrixList1";
            this.ucMatrixList1.Size = new System.Drawing.Size(795, 335);
            this.ucMatrixList1.TabIndex = 2;
            this.ucMatrixList1.Visible = false;
            // 
            // ucMatrixDocTypes21
            // 
            this.ucMatrixDocTypes21.AllocatedQty = 0;
            this.ucMatrixDocTypes21.BackColor = System.Drawing.Color.Black;
            this.ucMatrixDocTypes21.Location = new System.Drawing.Point(337, 5);
            this.ucMatrixDocTypes21.Name = "ucMatrixDocTypes21";
            this.ucMatrixDocTypes21.Size = new System.Drawing.Size(803, 201);
            this.ucMatrixDocTypes21.TabIndex = 1;
            this.ucMatrixDocTypes21.Visible = false;
            this.ucMatrixDocTypes21.DocSelected += new MatrixBuilder.ucMatrixDocTypes2.ProcessHandler(this.ucMatrixDocTypes21_DocSelected);
            this.ucMatrixDocTypes21.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ucMatrixDocTypes21_MouseDown);
            // 
            // ucMatrixBuild
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.panFooter);
            this.Name = "ucMatrixBuild";
            this.Size = new System.Drawing.Size(969, 543);
            this.Load += new System.EventHandler(this.ucMatrixBuild_Load);
            this.VisibleChanged += new System.EventHandler(this.ucMatrixBuild_VisibleChanged);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panTopButtons.ResumeLayout(false);
            this.panBottomHeader.ResumeLayout(false);
            this.panBottomHeader.PerformLayout();
            this.panBottomHeaderRight.ResumeLayout(false);
            this.panBottomHeaderRight.PerformLayout();
            this.panFooter.ResumeLayout(false);
            this.panFooter.PerformLayout();
            this.panFooterRight.ResumeLayout(false);
            this.panFooterRight.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panTopButtons;
        private unvell.ReoGrid.ReoGridControl reoGridControl1;
        private System.Windows.Forms.Panel panBottomHeader;
        private MetroFramework.Controls.MetroButton butDeleteAllocatedItems;
        private MetroFramework.Controls.MetroButton butAdd;
        private MetroFramework.Controls.MetroButton butExportExcel;
        private MetroFramework.Controls.MetroButton butPrintMatrix;
        private ucMatrixDocTypes2 ucMatrixDocTypes21;
        private MetroFramework.Controls.MetroButton butStoryboard;
        private MetroFramework.Controls.MetroButton butSummary;
        private MetroFramework.Controls.MetroButton butRefRes;
        private MetroFramework.Controls.MetroButton butDocTypes;
        private MetroFramework.Controls.MetroButton butList;
        private MetroFramework.Controls.MetroButton butMatrix;
        private System.Windows.Forms.Panel panFooter;
        private MetroFramework.Controls.MetroButton butCellInformation;
        private System.Windows.Forms.Panel panFooterRight;
        private MetroFramework.Controls.MetroTrackBar mtrb;
        private System.Windows.Forms.Label lblMatrixScale;
        private System.Windows.Forms.Label lblQtyAllocationsRemoved;
        private ucMatrixList ucMatrixList1;
        private MetroFramework.Controls.MetroButton butDocs;
        private ucMatrixRefRes ucMatrixRefRes1;
        private ucMatrixSB ucMatrixSB1;
        private MetroFramework.Controls.MetroButton butSave;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.Panel panBottomHeaderRight;
        private MetroFramework.Controls.MetroButton metroButton1;
        private MetroFramework.Controls.MetroButton butExcelEdit;
        private System.Windows.Forms.Label lblMatrixEditCaption;
        private MetroFramework.Controls.MetroButton butEmail;
        private System.Windows.Forms.Label lblMatrix;
        private System.Windows.Forms.Label lblAllocations;
        private System.Windows.Forms.Label lblRows;
        private MetroFramework.Controls.MetroButton butRemoveRow;
        private MetroFramework.Controls.MetroButton butAddRow;
    }
}
