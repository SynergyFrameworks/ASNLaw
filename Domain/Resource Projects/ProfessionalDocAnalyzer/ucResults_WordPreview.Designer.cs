namespace ProfessionalDocAnalyzer
{
    partial class ucResults_WordPreview
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ucResults_WordPreview));
            Gnostice.Core.Viewer.PageLayout pageLayout1 = new Gnostice.Core.Viewer.PageLayout();
            Gnostice.Documents.FormatterSettings formatterSettings1 = new Gnostice.Documents.FormatterSettings();
            Gnostice.Documents.Spreadsheet.SpreadSheetFormatterSettings spreadSheetFormatterSettings1 = new Gnostice.Documents.Spreadsheet.SpreadSheetFormatterSettings();
            Gnostice.Documents.PageSettings pageSettings1 = new Gnostice.Documents.PageSettings();
            Gnostice.Documents.Margins margins1 = new Gnostice.Documents.Margins();
            Gnostice.Documents.Spreadsheet.SheetOptions sheetOptions1 = new Gnostice.Documents.Spreadsheet.SheetOptions();
            Gnostice.Documents.Spreadsheet.SheetOptions sheetOptions2 = new Gnostice.Documents.Spreadsheet.SheetOptions();
            Gnostice.Documents.TXTFormatterSettings txtFormatterSettings1 = new Gnostice.Documents.TXTFormatterSettings();
            Gnostice.Documents.PageSettings pageSettings2 = new Gnostice.Documents.PageSettings();
            Gnostice.Documents.Margins margins2 = new Gnostice.Documents.Margins();
            Gnostice.Core.Graphics.RenderingSettings renderingSettings1 = new Gnostice.Core.Graphics.RenderingSettings();
            Gnostice.Core.Graphics.ImageRenderingSettings imageRenderingSettings1 = new Gnostice.Core.Graphics.ImageRenderingSettings();
            Gnostice.Core.Graphics.ResolutionSettings resolutionSettings1 = new Gnostice.Core.Graphics.ResolutionSettings();
            Gnostice.Core.Graphics.ShapeRenderingSettings shapeRenderingSettings1 = new Gnostice.Core.Graphics.ShapeRenderingSettings();
            Gnostice.Core.Graphics.TextRenderingSettings textRenderingSettings1 = new Gnostice.Core.Graphics.TextRenderingSettings();
            Gnostice.Core.Viewer.ViewerSettings viewerSettings1 = new Gnostice.Core.Viewer.ViewerSettings();
            Gnostice.Core.Viewer.Zoom zoom1 = new Gnostice.Core.Viewer.Zoom();
            this.panHeader = new System.Windows.Forms.Panel();
            this.panHeaderRight = new System.Windows.Forms.Panel();
            this.butPrint = new MetroFramework.Controls.MetroButton();
            this.butDelete = new MetroFramework.Controls.MetroButton();
            this.butEmail = new MetroFramework.Controls.MetroButton();
            this.butOpen = new MetroFramework.Controls.MetroButton();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panTemplateTypesRtPadding = new System.Windows.Forms.Panel();
            this.documentViewer1 = new Gnostice.Controls.WinForms.DocumentViewer();
            this.panHeader.SuspendLayout();
            this.panHeaderRight.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.panHeaderRight);
            this.panHeader.Controls.Add(this.pictureBox2);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(800, 41);
            this.panHeader.TabIndex = 1;
            // 
            // panHeaderRight
            // 
            this.panHeaderRight.Controls.Add(this.butPrint);
            this.panHeaderRight.Controls.Add(this.butDelete);
            this.panHeaderRight.Controls.Add(this.butEmail);
            this.panHeaderRight.Controls.Add(this.butOpen);
            this.panHeaderRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.panHeaderRight.Location = new System.Drawing.Point(424, 0);
            this.panHeaderRight.Name = "panHeaderRight";
            this.panHeaderRight.Size = new System.Drawing.Size(376, 41);
            this.panHeaderRight.TabIndex = 135;
            // 
            // butPrint
            // 
            this.butPrint.Location = new System.Drawing.Point(188, 9);
            this.butPrint.Name = "butPrint";
            this.butPrint.Size = new System.Drawing.Size(75, 23);
            this.butPrint.TabIndex = 138;
            this.butPrint.Text = "Print";
            this.butPrint.UseSelectable = true;
            this.butPrint.Click += new System.EventHandler(this.butPrint_Click);
            // 
            // butDelete
            // 
            this.butDelete.Location = new System.Drawing.Point(282, 9);
            this.butDelete.Name = "butDelete";
            this.butDelete.Size = new System.Drawing.Size(75, 23);
            this.butDelete.TabIndex = 137;
            this.butDelete.Text = "Delete";
            this.butDelete.UseSelectable = true;
            this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
            // 
            // butEmail
            // 
            this.butEmail.Location = new System.Drawing.Point(104, 9);
            this.butEmail.Name = "butEmail";
            this.butEmail.Size = new System.Drawing.Size(75, 23);
            this.butEmail.TabIndex = 136;
            this.butEmail.Text = "Email";
            this.butEmail.UseSelectable = true;
            this.butEmail.Click += new System.EventHandler(this.butEmail_Click);
            // 
            // butOpen
            // 
            this.butOpen.Location = new System.Drawing.Point(20, 9);
            this.butOpen.Name = "butOpen";
            this.butOpen.Size = new System.Drawing.Size(75, 23);
            this.butOpen.TabIndex = 135;
            this.butOpen.Text = "Open";
            this.butOpen.UseSelectable = true;
            this.butOpen.Click += new System.EventHandler(this.butOpen_Click);
            // 
            // pictureBox2
            // 
            this.pictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox2.Image")));
            this.pictureBox2.Location = new System.Drawing.Point(9, 3);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(38, 38);
            this.pictureBox2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox2.TabIndex = 133;
            this.pictureBox2.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(50, 8);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(78, 25);
            this.lblHeader.TabIndex = 132;
            this.lblHeader.Text = "Preview";
            // 
            // panel1
            // 
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(790, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(10, 521);
            this.panel1.TabIndex = 5;
            // 
            // panTemplateTypesRtPadding
            // 
            this.panTemplateTypesRtPadding.Dock = System.Windows.Forms.DockStyle.Left;
            this.panTemplateTypesRtPadding.Location = new System.Drawing.Point(0, 41);
            this.panTemplateTypesRtPadding.Name = "panTemplateTypesRtPadding";
            this.panTemplateTypesRtPadding.Size = new System.Drawing.Size(10, 521);
            this.panTemplateTypesRtPadding.TabIndex = 4;
            // 
            // documentViewer1
            // 
            this.documentViewer1.AllowInteractivity = false;
            this.documentViewer1.BackColor = System.Drawing.SystemColors.ControlDark;
            this.documentViewer1.BorderWidth = 0F;
            this.documentViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.documentViewer1.HScrollBar.Enabled = false;
            this.documentViewer1.HScrollBar.LargeChange = 40F;
            this.documentViewer1.HScrollBar.Maximum = 0F;
            this.documentViewer1.HScrollBar.Minimum = 0F;
            this.documentViewer1.HScrollBar.SmallChange = 20F;
            this.documentViewer1.HScrollBar.Value = 0F;
            this.documentViewer1.HScrollBar.Visibility = Gnostice.Core.Viewer.ScrollBarVisibility.Always;
            this.documentViewer1.HScrollBar.Visible = false;
            this.documentViewer1.Location = new System.Drawing.Point(10, 41);
            this.documentViewer1.MouseMode = Gnostice.Core.DOM.CursorPreferences.AreaSelection;
            this.documentViewer1.Name = "documentViewer1";
            // 
            // 
            // 
            this.documentViewer1.NavigationPane.ActivePage = null;
            this.documentViewer1.NavigationPane.Location = new System.Drawing.Point(0, 0);
            this.documentViewer1.NavigationPane.Name = "";
            this.documentViewer1.NavigationPane.TabIndex = 0;
            this.documentViewer1.NavigationPane.Visibility = Gnostice.Core.Viewer.Visibility.Auto;
            this.documentViewer1.NavigationPane.WidthPercentage = 30;
            this.documentViewer1.Orientation = Gnostice.Core.Viewer.ViewerOrientation.Vertical;
            this.documentViewer1.PageBreakWidth = 10F;
            pageLayout1.Columns = 1;
            pageLayout1.HorizontalSpacing = 5D;
            pageLayout1.LayoutMode = Gnostice.Core.Viewer.PageLayoutMode.SinglePage;
            pageLayout1.ScrollMode = Gnostice.Core.Viewer.ScrollMode.Continuous;
            pageLayout1.ShowCoverPage = false;
            pageLayout1.VerticalSpacing = 5D;
            this.documentViewer1.PageLayout = pageLayout1;
            this.documentViewer1.PageRotation = Gnostice.Core.Viewer.RotationAngle.Zero;
            spreadSheetFormatterSettings1.FormattingMode = Gnostice.Core.DOM.FormattingMode.PreferDocumentSettings;
            pageSettings1.Height = 11.6929F;
            margins1.Bottom = 1F;
            margins1.Footer = 0F;
            margins1.Header = 0F;
            margins1.Left = 1F;
            margins1.Right = 1F;
            margins1.Top = 1F;
            pageSettings1.Margin = margins1;
            pageSettings1.Orientation = Gnostice.Core.Graphics.Orientation.Portrait;
            pageSettings1.PageSize = Gnostice.Documents.PageSize.A4;
            pageSettings1.Width = 8.2677F;
            spreadSheetFormatterSettings1.PageSettings = pageSettings1;
            sheetOptions1.Print = false;
            sheetOptions1.View = true;
            spreadSheetFormatterSettings1.ShowGridlines = sheetOptions1;
            sheetOptions2.Print = false;
            sheetOptions2.View = true;
            spreadSheetFormatterSettings1.ShowHeadings = sheetOptions2;
            formatterSettings1.SpreadSheet = spreadSheetFormatterSettings1;
            pageSettings2.Height = 11.6929F;
            margins2.Bottom = 1F;
            margins2.Footer = 0F;
            margins2.Header = 0F;
            margins2.Left = 1F;
            margins2.Right = 1F;
            margins2.Top = 1F;
            pageSettings2.Margin = margins2;
            pageSettings2.Orientation = Gnostice.Core.Graphics.Orientation.Portrait;
            pageSettings2.PageSize = Gnostice.Documents.PageSize.A4;
            pageSettings2.Width = 8.2677F;
            txtFormatterSettings1.PageSettings = pageSettings2;
            formatterSettings1.TXT = txtFormatterSettings1;
            this.documentViewer1.Preferences.FormatterSettings = formatterSettings1;
            this.documentViewer1.Preferences.KeyNavigation = true;
            imageRenderingSettings1.InterpolationMode = Gnostice.Core.Graphics.InterpolationMode.NearestNeighbor;
            renderingSettings1.Image = imageRenderingSettings1;
            resolutionSettings1.DpiX = 96F;
            resolutionSettings1.DpiY = 96F;
            resolutionSettings1.ResolutionMode = Gnostice.Core.Graphics.ResolutionMode.UseSource;
            renderingSettings1.Resolution = resolutionSettings1;
            shapeRenderingSettings1.CompositingMode = Gnostice.Core.Graphics.CompositingMode.SourceOver;
            shapeRenderingSettings1.CompositingQuality = Gnostice.Core.Graphics.CompositingQuality.Default;
            shapeRenderingSettings1.PixelOffsetMode = Gnostice.Core.Graphics.PixelOffsetMode.HighQuality;
            shapeRenderingSettings1.SmoothingMode = Gnostice.Core.Graphics.SmoothingMode.AntiAlias;
            renderingSettings1.Shape = shapeRenderingSettings1;
            textRenderingSettings1.TextContrast = 4;
            textRenderingSettings1.TextRenderingHint = Gnostice.Core.Graphics.TextRenderingHint.AntiAlias;
            renderingSettings1.Text = textRenderingSettings1;
            this.documentViewer1.Preferences.RenderingSettings = renderingSettings1;
            this.documentViewer1.Preferences.Units = Gnostice.Core.Graphics.MeasurementUnit.Inches;
            viewerSettings1.AllowInteractivity = false;
            viewerSettings1.MouseMode = Gnostice.Core.DOM.CursorPreferences.AreaSelection;
            viewerSettings1.Orientation = Gnostice.Core.Viewer.ViewerOrientation.Vertical;
            viewerSettings1.PageBreakWidth = 10F;
            viewerSettings1.PageLayout = pageLayout1;
            viewerSettings1.Rotation = Gnostice.Core.Viewer.RotationAngle.Zero;
            zoom1.InternalZoomMode = Gnostice.Core.Viewer.ZoomMode.ActualSize;
            zoom1.InternalZoomPercent = 100D;
            zoom1.ZoomMode = Gnostice.Core.Viewer.ZoomMode.ActualSize;
            zoom1.ZoomPercent = 100D;
            viewerSettings1.Zoom = zoom1;
            this.documentViewer1.Preferences.ViewerSettings = viewerSettings1;
            this.documentViewer1.Size = new System.Drawing.Size(780, 521);
            this.documentViewer1.TabIndex = 6;
            this.documentViewer1.VScrollBar.Enabled = false;
            this.documentViewer1.VScrollBar.LargeChange = 40F;
            this.documentViewer1.VScrollBar.Maximum = 0F;
            this.documentViewer1.VScrollBar.Minimum = 0F;
            this.documentViewer1.VScrollBar.SmallChange = 20F;
            this.documentViewer1.VScrollBar.Value = 0F;
            this.documentViewer1.VScrollBar.Visibility = Gnostice.Core.Viewer.ScrollBarVisibility.Always;
            this.documentViewer1.VScrollBar.Visible = false;
            // 
            // ucResults_WordPreview
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.documentViewer1);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.panTemplateTypesRtPadding);
            this.Controls.Add(this.panHeader);
            this.Name = "ucResults_WordPreview";
            this.Size = new System.Drawing.Size(800, 562);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            this.panHeaderRight.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private System.Windows.Forms.Panel panHeaderRight;
        private MetroFramework.Controls.MetroButton butDelete;
        private MetroFramework.Controls.MetroButton butEmail;
        private MetroFramework.Controls.MetroButton butOpen;
        private System.Windows.Forms.PictureBox pictureBox2;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Panel panTemplateTypesRtPadding;
        private MetroFramework.Controls.MetroButton butPrint;
        private Gnostice.Controls.WinForms.DocumentViewer documentViewer1;
    }
}
