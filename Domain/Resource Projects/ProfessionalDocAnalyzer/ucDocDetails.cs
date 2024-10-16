using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

namespace ProfessionalDocAnalyzer
{
    public partial class ucDocDetails : UserControl
    {
        public ucDocDetails()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        public void Clear()
        {
            picExcel.Visible = false;
            picPDF.Visible = false;
            picPowerPoint.Visible = false;
            picRTF.Visible = false;
            picTXT.Visible = false;
            picWord.Visible = false;
            picYes.Visible = false;
            picNo.Visible = false;

            lblFile.Visible = false;

            picYes.Visible = false;
            picNo.Visible = false;

            lblParsed.Visible = false;

            this.lblFileInfo.Text = string.Empty;

        }

        public void ShowFileInformation(string ext, string FileName, bool Parsed, string FileDetails)
        {
            Clear();

            // File Types
            switch (ext.ToUpper())
            {
                case ".DOCX":
                case ".DOC":
                    picWord.Visible = true;
                    break;
                case ".XLSX":
                    picExcel.Visible = true;
                    break;
                case ".pptx":
                    picPowerPoint.Visible = true;
                    break;
                case ".RTF":
                    picRTF.Visible = true;
                    break;
                case ".PDF":
                    picPDF.Visible = true;
                    break;
                case ".TXT":
                    picTXT.Visible = true;
                    break;
                case ".PPTX":
                    picPowerPoint.Visible = true;
                    break;
            }

            // File Name
            lblFile.Text = FileName;
            lblFile.Visible = true;

            // Parsed?
            if (Parsed)
            {
                picYes.Visible = true;
                lblParsed.Visible = true;
                lblParsed.Text = "Default Analyzed: Yes";
            }
            else
            {
                picNo.Visible = true;
                lblParsed.Visible = true;
                lblParsed.Text = "Default Analyzed: No";
            }

            // File Information
            lblFileInfo.Text = FileDetails;     
        }

        private void lblFileInfo_TextChanged(object sender, EventArgs e)
        {
            txtFileInfo.Text = lblFileInfo.Text;
        }

        private void txtFileInfo_TextChanged(object sender, EventArgs e)
        {
            txtFileInfo.Text = lblFileInfo.Text;
        }
    }
}
