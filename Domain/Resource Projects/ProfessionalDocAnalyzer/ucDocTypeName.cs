using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class ucDocTypeName : UserControl
    {
        public ucDocTypeName()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }


        public void LoadData(string File)
        {
            string ext = string.Empty;

            string FileName = Files.GetFileName(File, out ext);

            Clear();

            // File Types
            switch (ext.ToUpper())
            {
                case ".DOCX":
                case ".DOC":
                    picWord.Location = new Point(3, 3);
                    picWord.Visible = true;
                    break;
                case ".XLSX":
                    picExcel.Location = new Point(3, 3);
                    picExcel.Visible = true;
                    break;
                case ".RTF":
                    picRTF.Location = new Point(3, 3);
                    picRTF.Visible = true;
                    break;
                case ".PDF":
                    picPDF.Location = new Point(3, 3);
                    picPDF.Visible = true;
                    break;
                case ".TXT":
                    picTXT.Location = new Point(3, 3);
                    picTXT.Visible = true;
                    break;
                case "PPTX":
                    picPowerPoint.Location = new Point(3, 3);
                    picPowerPoint.Visible = true;
                    break;
            }

            // File Name
            lblFile.Text = FileName;
            lblFile.Visible = true;
        }


        public void Clear()
        {
            picExcel.Visible = false;
            picPDF.Visible = false;
            picPowerPoint.Visible = false;
            picRTF.Visible = false;
            picTXT.Visible = false;
            picWord.Visible = false;

            lblFile.Visible = false;
        }
    }
}
