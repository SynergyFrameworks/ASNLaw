using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class ucAcroSeekerResults : UserControl
    {
        public ucAcroSeekerResults()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        string _AnalysisFolder = string.Empty;
        string _DocsFolder = string.Empty;

       // string _DocFolder = string.Empty;
        string _ResultsFolder = string.Empty;


        public bool LoadData(string AnalysisFolder)
        {
            HideResults();

            if (!Directory.Exists(AnalysisFolder))
            {
                string msg = string.Concat("Unable to find Analysis Folder: ", AnalysisFolder);
                MessageBox.Show(msg, "Analysis Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            _AnalysisFolder = AnalysisFolder;

            _DocsFolder = Path.Combine(_AnalysisFolder, "Docs");
            if (!Directory.Exists(_DocsFolder))
            {
                string msg = string.Concat("Unable to find Analysis Docs Folder: ", _DocsFolder);
                MessageBox.Show(msg, "Analysis Docs Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            lstbDocs.Items.Clear();

            
            string[] docs = Directory.GetDirectories(_DocsFolder);
            if (docs.Length == 0)
            {
                string msg = string.Concat("No Analysis Documents folders were found under the Docs Folder: ", _DocsFolder);
                MessageBox.Show(msg, "No Analysis Documents Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            string docName = string.Empty;
            foreach (string doc in docs)
            {
                docName = Path.GetFileName(doc.TrimEnd(Path.DirectorySeparatorChar));

                lstbDocs.Items.Add(docName);
            }

            lstbDocs.SelectedIndex = 0;

            return true;
        }

        private void mouseEnter(Button button)
        {
            //if (button.BackColor == Color.WhiteSmoke)
            //{
            button.BackColor = Color.Teal;
            button.ForeColor = Color.White;
            //  }

            button.Refresh();
        }

        private void mouseLeave(Button button)
        {
            //if (button.BackColor == Color.Teal)
            //{

            button.BackColor = Color.WhiteSmoke;
            button.ForeColor = Color.Black;
            // }

            button.Refresh();
        }

        private void butMSWordTbl_MouseEnter(object sender, EventArgs e)
        {
            //mouseEnter(butMSWordTbl);
        }

        private void butReport_MouseEnter(object sender, EventArgs e)
        {
            //mouseEnter(butReport);
        }

        private void butMSWordTbl_Click(object sender, EventArgs e)
        {
            ucResults_HTMLPreview1.Visible = false;

            ucResults_WordPreview2.Visible = true;
            ucResults_WordPreview2.Dock = DockStyle.Fill;

            butMSWordTbl.BackColor = Color.Teal;
            butMSWordTbl.ForeColor = Color.White;
            butMSWordTbl.Refresh();

            butReport.BackColor = Color.WhiteSmoke;
            butReport.ForeColor = Color.Black;

 
            this.Refresh();
            
        }

        private void butMSWordTbl_MouseLeave(object sender, EventArgs e)
        {


            if (ucResults_WordPreview2.Visible)
            {
                butMSWordTbl.BackColor = Color.Teal;
                butMSWordTbl.ForeColor = Color.White;
                butMSWordTbl.Refresh();
            }
        }

        private void butReport_MouseLeave(object sender, EventArgs e)
        {
            if (ucResults_HTMLPreview1.Visible)
            {
                butReport.BackColor = Color.Teal;
                butReport.ForeColor = Color.White;
                butReport.Refresh();
            } 
        }

        private void butReport_Click(object sender, EventArgs e)
        {
            ucResults_WordPreview2.Visible = false;

            ucResults_HTMLPreview1.Visible = true;
            ucResults_HTMLPreview1.Dock = DockStyle.Fill;

            butReport.BackColor = Color.Teal;
            butReport.ForeColor = Color.White;
            butReport.Refresh();

            butMSWordTbl.BackColor = Color.WhiteSmoke;
            butMSWordTbl.ForeColor = Color.Black;

 
            this.Refresh();
        }

        private void HideResults()
        {
            this.butMSWordTbl.Visible = false;
            this.butReport.Visible = false;

            this.ucResults_HTMLPreview1.Visible = false;
            this.ucResults_WordPreview2.Visible = false;
        }

        private void lstbDocs_SelectedIndexChanged(object sender, EventArgs e)
        {
            HideResults();

            string doc = lstbDocs.Text;

            _ResultsFolder = Path.Combine(_DocsFolder, doc, "Results");

            string tableFile = "Acronyms.docx";
            string reportFile = "Results.html";

            string tablePathFile = Path.Combine(_ResultsFolder, tableFile);
            string reportPathFile = Path.Combine(_ResultsFolder, reportFile);

            // MS Word - Acronyms Table
            if (File.Exists(tablePathFile))
            {
                string subject = string.Concat("Acronyms Table - ", doc);
                butMSWordTbl.Visible = true;
                ucResults_WordPreview2.LoadData(tablePathFile, subject);
                if (butMSWordTbl.BackColor == Color.Teal)
                {
                    ucResults_WordPreview2.Visible = true;
                    ucResults_WordPreview2.Dock = DockStyle.Fill;
                }
            }

            // Acronyms Report
            if (File.Exists(reportPathFile))
            {
                butReport.Visible = true;
                string subject = string.Concat("Acronyms Report - ", doc);
                ucResults_HTMLPreview1.LoadData(reportPathFile, subject);
                if (butReport.BackColor == Color.Teal)
                {
                    ucResults_HTMLPreview1.Visible = true;
                    ucResults_HTMLPreview1.Dock = DockStyle.Fill;
                }
            }

        }

   

 
    }
}
