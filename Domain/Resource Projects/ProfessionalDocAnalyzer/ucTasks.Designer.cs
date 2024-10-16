namespace ProfessionalDocAnalyzer
{
    partial class ucTasks
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
            this.htmlLabel = new MetroFramework.Drawing.Html.HtmlLabel();
            this.htmlLabelStatus = new MetroFramework.Drawing.Html.HtmlLabel();
            this.SuspendLayout();
            // 
            // htmlLabel
            // 
            this.htmlLabel.AutoScroll = true;
            this.htmlLabel.AutoScrollMinSize = new System.Drawing.Size(922, 28);
            this.htmlLabel.AutoSize = false;
            this.htmlLabel.BackColor = System.Drawing.Color.WhiteSmoke;
            this.htmlLabel.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.htmlLabel.Location = new System.Drawing.Point(0, 0);
            this.htmlLabel.Name = "htmlLabel";
            this.htmlLabel.Size = new System.Drawing.Size(1044, 31);
            this.htmlLabel.TabIndex = 0;
            this.htmlLabel.TabStop = false;
            this.htmlLabel.Text = "<b>Workgroup: </b> Test555 -- <font color=blue> I want to Generate a Requirement " +
    "Matrix -> </font> Select a Folder and Document > Run Generate Requirement Matrix" +
    " > View Results";
            this.htmlLabel.Click += new System.EventHandler(this.htmlLabel_Click);
            // 
            // htmlLabelStatus
            // 
            this.htmlLabelStatus.AutoScroll = true;
            this.htmlLabelStatus.AutoScrollMinSize = new System.Drawing.Size(140, 28);
            this.htmlLabelStatus.AutoSize = false;
            this.htmlLabelStatus.BackColor = System.Drawing.Color.WhiteSmoke;
            this.htmlLabelStatus.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.htmlLabelStatus.Location = new System.Drawing.Point(0, 37);
            this.htmlLabelStatus.Name = "htmlLabelStatus";
            this.htmlLabelStatus.Size = new System.Drawing.Size(1044, 31);
            this.htmlLabelStatus.TabIndex = 1;
            this.htmlLabelStatus.TabStop = false;
            this.htmlLabelStatus.Text = "<b>Please Wait </b> Test555 ";
            this.htmlLabelStatus.Visible = false;
            // 
            // ucTasks
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.htmlLabelStatus);
            this.Controls.Add(this.htmlLabel);
            this.Name = "ucTasks";
            this.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.Size = new System.Drawing.Size(1044, 30);
            this.Load += new System.EventHandler(this.ucTasks_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private MetroFramework.Drawing.Html.HtmlLabel htmlLabel;
        private MetroFramework.Drawing.Html.HtmlLabel htmlLabelStatus;
    }
}
