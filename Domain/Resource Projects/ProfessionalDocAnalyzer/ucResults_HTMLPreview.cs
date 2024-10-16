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
using System.IO;
using System.Diagnostics;

using Atebion.Common;



namespace ProfessionalDocAnalyzer
{
    public partial class ucResults_HTMLPreview : UserControl
    {
        public ucResults_HTMLPreview()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        public string _HTMLPathFile = string.Empty;
        public string _Subject = string.Empty;
        private Atebion.Outlook.Email _EmailOutLook;


        public bool LoadData(string HTMLPathFile, string Subject)
        {
            webBrowser1.Visible = false;
            butDelete.Visible = false;
            butEmail.Visible = false;
            butOpen.Visible = false;

            if (!File.Exists(HTMLPathFile))
            {
                string msg = string.Concat("Unable to find HTML file: ", HTMLPathFile);
                MessageBox.Show(msg, "HTML File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            _HTMLPathFile = HTMLPathFile;
            _Subject = Subject;

            Cursor.Current = Cursors.WaitCursor; // Waiting

            webBrowser1.Navigate(new Uri(HTMLPathFile));

            webBrowser1.Visible = true;
            butDelete.Visible = true;
            butOpen.Visible = true;

            _EmailOutLook = new Atebion.Outlook.Email();
            if (OutLookMgr.isOutLookRunning())
            {
                if (_EmailOutLook.IsOutlookConnectable())
                    butEmail.Visible = true;
            }


            Cursor.Current = Cursors.Default;

            return true;
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                webBrowser1.Document.Body.Style = "zoom:75%;";
            }
            catch
            {

            }
        }

        private void butPrint_Click(object sender, EventArgs e)
        {
            webBrowser1.ShowPrintDialog();
        }

        private void butOpen_Click(object sender, EventArgs e)
        {
            Process.Start(_HTMLPathFile);

        }

        private void butEmail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            List<string> sAttachments = new List<string>();

            sAttachments.Add(_HTMLPathFile);

            string file = Files.GetFileName(_HTMLPathFile);

            string subject = string.Concat("HTML file for ", _Subject);
            string body = string.Concat(Environment.NewLine, Environment.NewLine + "Please see the attached file: ", file);

            _EmailOutLook.OpenEmailWithAttachments(string.Empty, subject, body, sAttachments.ToArray());

            Cursor.Current = Cursors.Default;
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            string msg = string.Concat("Are you sure you want to delete the selected HTML file: ", _HTMLPathFile, " ?");
            if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            webBrowser1.Visible = false;

            try
            {
                File.Delete(_HTMLPathFile);
            }
            catch
            {
                msg = string.Concat("Unable to deleted the selected HTML file.", Environment.NewLine, Environment.NewLine, "Most likely the HTML is still opened. Please close the HTML file and retry.");
                MessageBox.Show(msg, "Unable to Delete the HTML File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                webBrowser1.Visible = true;
            }
        }
    }
}
