using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Diagnostics;

using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class ucResults_RTF_Preview : UserControl
    {
        public ucResults_RTF_Preview()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private string _ErrorMessage = string.Empty;
        private string _RTF_PathFile = string.Empty;
        private string _FileName = string.Empty;
        private string _Subject = string.Empty;
        private Atebion.Outlook.Email _EmailOutLook;


        public bool LoadData(string rtfFile, string Subject)
        {
            _ErrorMessage = string.Empty;

            _FileName = Files.GetFileName(rtfFile);

            lblHeader.Text = string.Concat("Document:   ", _FileName);

            if (!File.Exists(rtfFile))
            {
                _ErrorMessage = string.Concat("Unable to find file: ", rtfFile);
                richTextBox1.Text = _ErrorMessage;
                return false;
            }


            richTextBox1.LoadFile(rtfFile);

            _RTF_PathFile = rtfFile;
            _Subject = Subject;
            

            butDelete.Visible = true;
            butOpen.Visible = true;
            richTextBox1.Visible = true;

            _EmailOutLook = new Atebion.Outlook.Email();
            if (OutLookMgr.isOutLookRunning())
            {
                if (_EmailOutLook.IsOutlookConnectable())
                    butEmail.Visible = true;
            }
   
            return true;
        }

        private void butOpen_Click(object sender, EventArgs e)
        {
            Process.Start(_RTF_PathFile);
        }

        private void butEmail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<string> sAttachments = new List<string>();

            sAttachments.Add(_RTF_PathFile);

            string file = Files.GetFileName(_RTF_PathFile);

            string subject = string.Concat("RTF file for ", _Subject);
            string body = string.Concat(Environment.NewLine, Environment.NewLine + "Please see the attached file: ", file);

            _EmailOutLook.OpenEmailWithAttachments(string.Empty, subject, body, sAttachments.ToArray());

            Cursor.Current = Cursors.Default;
        }

        private void butPrint_Click(object sender, EventArgs e)
        {

            Exception PrintException = null;

            this.richTextBox1.Print(printDocument1.DefaultPageSettings, printDocument1.PrinterSettings, ref PrintException);
        
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            string msg = string.Concat("Are you sure you want to delete the selected RTF file: ", _RTF_PathFile, " ?");
            if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            richTextBox1.Visible = false;

            try
            {
                File.Delete(_RTF_PathFile);
            }
            catch
            {
                msg = string.Concat("Unable to deleted the selected RTF file.", Environment.NewLine, Environment.NewLine, "Most likely the RTF is still opened. Please close the RTF file and retry.");
                MessageBox.Show(msg, "Unable to Delete the RTF File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                richTextBox1.Visible = true;
            }
        }
    }
}
