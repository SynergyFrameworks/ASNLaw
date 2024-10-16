namespace MatrixBuilder
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
            this.panHeader = new System.Windows.Forms.Panel();
            this.butInformation = new System.Windows.Forms.PictureBox();
            this.picHeader = new System.Windows.Forms.PictureBox();
            this.lblHeader = new System.Windows.Forms.Label();
            this.metroToolTip1 = new MetroFramework.Components.MetroToolTip();
            this.lblNoInternet = new System.Windows.Forms.Label();
            this.webBStart = new System.Windows.Forms.WebBrowser();
            this.panHeader.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butInformation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).BeginInit();
            this.SuspendLayout();
            // 
            // panHeader
            // 
            this.panHeader.BackColor = System.Drawing.Color.Black;
            this.panHeader.Controls.Add(this.butInformation);
            this.panHeader.Controls.Add(this.picHeader);
            this.panHeader.Controls.Add(this.lblHeader);
            this.panHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.panHeader.Location = new System.Drawing.Point(0, 0);
            this.panHeader.Name = "panHeader";
            this.panHeader.Size = new System.Drawing.Size(1011, 50);
            this.panHeader.TabIndex = 17;
            // 
            // butInformation
            // 
            this.butInformation.Cursor = System.Windows.Forms.Cursors.Hand;
            this.butInformation.Image = ((System.Drawing.Image)(resources.GetObject("butInformation.Image")));
            this.butInformation.Location = new System.Drawing.Point(166, 15);
            this.butInformation.Name = "butInformation";
            this.butInformation.Size = new System.Drawing.Size(28, 28);
            this.butInformation.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.butInformation.TabIndex = 17;
            this.butInformation.TabStop = false;
            this.metroToolTip1.SetToolTip(this.butInformation, "Release Information");
            this.butInformation.Visible = false;
            this.butInformation.Click += new System.EventHandler(this.butInformation_Click);
            // 
            // picHeader
            // 
            this.picHeader.Image = ((System.Drawing.Image)(resources.GetObject("picHeader.Image")));
            this.picHeader.Location = new System.Drawing.Point(9, 6);
            this.picHeader.Name = "picHeader";
            this.picHeader.Size = new System.Drawing.Size(41, 38);
            this.picHeader.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.picHeader.TabIndex = 16;
            this.picHeader.TabStop = false;
            // 
            // lblHeader
            // 
            this.lblHeader.AutoSize = true;
            this.lblHeader.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblHeader.ForeColor = System.Drawing.Color.White;
            this.lblHeader.Location = new System.Drawing.Point(56, 11);
            this.lblHeader.Name = "lblHeader";
            this.lblHeader.Size = new System.Drawing.Size(99, 30);
            this.lblHeader.TabIndex = 15;
            this.lblHeader.Text = "Welcome";
            // 
            // metroToolTip1
            // 
            this.metroToolTip1.Style = MetroFramework.MetroColorStyle.Blue;
            this.metroToolTip1.StyleManager = null;
            this.metroToolTip1.Theme = MetroFramework.MetroThemeStyle.Light;
            // 
            // lblNoInternet
            // 
            this.lblNoInternet.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNoInternet.ForeColor = System.Drawing.Color.White;
            this.lblNoInternet.Location = new System.Drawing.Point(5, 73);
            this.lblNoInternet.Name = "lblNoInternet";
            this.lblNoInternet.Size = new System.Drawing.Size(800, 70);
            this.lblNoInternet.TabIndex = 19;
            this.lblNoInternet.Text = "No Internet connect has been detected. Emailing and Downloading from within the M" +
    "atrix Builder will not be operational.  Otherwise, the Document Analyzer should " +
    "functional normally without issues.";
            this.lblNoInternet.Visible = false;
            // 
            // webBStart
            // 
            this.webBStart.Dock = System.Windows.Forms.DockStyle.Fill;
            this.webBStart.Location = new System.Drawing.Point(0, 50);
            this.webBStart.MinimumSize = new System.Drawing.Size(20, 20);
            this.webBStart.Name = "webBStart";
            this.webBStart.Size = new System.Drawing.Size(1011, 448);
            this.webBStart.TabIndex = 20;
            // 
            // ucWelcome
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.Controls.Add(this.webBStart);
            this.Controls.Add(this.panHeader);
            this.Controls.Add(this.lblNoInternet);
            this.Name = "ucWelcome";
            this.Size = new System.Drawing.Size(1011, 498);
            this.Load += new System.EventHandler(this.ucWelcome_Load);
            this.VisibleChanged += new System.EventHandler(this.ucWelcome_VisibleChanged);
            this.panHeader.ResumeLayout(false);
            this.panHeader.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.butInformation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picHeader)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panHeader;
        private MetroFramework.Components.MetroToolTip metroToolTip1;
        private System.Windows.Forms.PictureBox butInformation;
        private System.Windows.Forms.PictureBox picHeader;
        private System.Windows.Forms.Label lblHeader;
        private System.Windows.Forms.Label lblNoInternet;
        private System.Windows.Forms.WebBrowser webBStart;
    }
}
