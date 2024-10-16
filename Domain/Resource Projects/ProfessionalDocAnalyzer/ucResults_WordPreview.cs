using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Atebion.CovertDoctoRTF;


namespace ProfessionalDocAnalyzer
{
    public partial class ucResults_WordPreview : UserControl
    {
        public ucResults_WordPreview()
        {
            StackTrace st = new StackTrace(false);

            DoubleBuffered = true;

            InitializeComponent();

          //  Gnostice.Documents.Framework.ActivateLicense("A157-6C29-7007-896C-8479-04BA-C229-2913-64C3-5D34-E485-61F1");

           // Gnostice.Licensing.License lic = new Gnostice.Licensing.License();

            //Gnostice.Licensing.GnosticeLicenseProvider lic = new Gnostice.Licensing.GnosticeLicenseProvider();

            //Infralution.Licensing.ASP.AuthenticatedLicense al = new Infralution.Licensing.ASP.AuthenticatedLicense();
          

        }

        private string _WordPathFile = string.Empty;
        private string _Subject = string.Empty;
        private Atebion.Outlook.Email _EmailOutLook;
        private csCovertDoctoRTF _ConvertViaWord;

        public bool LoadData(string WordPathFile, string Subject)
        {

            documentViewer1.Visible = true;
            butDelete.Visible = false;
            butEmail.Visible = false;
            butOpen.Visible = false;

            if (!File.Exists(WordPathFile))
            {
                string msg = string.Concat("Unable to find MS Word file: ", WordPathFile);
                MessageBox.Show(msg, "Word File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

    //        Gnostice.Documents.Framework.ActivateLicense("A157-6C29-7007-896C-8479-04BA-C229-2913-64C3-5D34-E485-61F1");

            _WordPathFile = WordPathFile;
            _Subject = Subject;

            //HackFixDocx4Preview();

            Cursor.Current = Cursors.WaitCursor; // Waiting

            documentViewer1.Visible = true;
            try
            {
                this.documentViewer1.LoadDocument(_WordPathFile);
            }
            catch
            {
                documentViewer1.Visible = false;

                MessageBox.Show("An error occured in the Document Viewer.", "Unable to View Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            _EmailOutLook = new Atebion.Outlook.Email();
            if (OutLookMgr.isOutLookRunning())
            {
                if (_EmailOutLook.IsOutlookConnectable())
                    butEmail.Visible = true;
            }

            butDelete.Visible = true;
            butOpen.Visible = true;


            Cursor.Current = Cursors.Default; // Done

            return true;
        }

        private bool HackFixDocx4Preview()
        {
            _ConvertViaWord = new csCovertDoctoRTF();

            if (_ConvertViaWord.IsMSWordInstalled())
            {
                if (!_ConvertViaWord.CovertDoc(csCovertDoctoRTF.wdFormats.wdFormatText, _WordPathFile, _WordPathFile))
                {
                    string msg = _ConvertViaWord.Error_Message;

                   // MessageBox.Show(msg, "Unable to Convert File into Text", MessageBoxButtons.OK, MessageBoxIcon.Error);
                   
                    return false;
                }
            }

            _ConvertViaWord = null;

            Application.DoEvents();

            return true;
        }

        private void butOpen_Click(object sender, EventArgs e)
        {
            Process.Start(_WordPathFile);
        }

        private void butEmail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 


            List<string> sAttachments = new List<string>();

            sAttachments.Add(_WordPathFile);


            string subject = string.Concat("MS Word file for ", _Subject); 
            string body = string.Concat(Environment.NewLine, Environment.NewLine + "Please see the attached file.");

            _EmailOutLook.OpenEmailWithAttachments(string.Empty, subject, body, sAttachments.ToArray());

            Cursor.Current = Cursors.Default;
        }

        private void butDelete_Click(object sender, EventArgs e)
        {

            string msg = string.Concat("Are you sure you want to delete the selected Word file: ", _WordPathFile, " ?");
            if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo) == DialogResult.No)
                return;

            //documentViewer1.Visible = false;

            //try
            //{
            //    documentViewer1.CloseDocument();
            //    File.Delete(_WordPathFile);
            //}
            //catch
            //{
            //    msg = string.Concat("Unable to deleted the selected Word file.", Environment.NewLine, Environment.NewLine, "Most likely the Word is still opened. Please close the Word file and retry.");
            //    MessageBox.Show(msg, "Unable to Delete the Word File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    documentViewer1.LoadDocument(_WordPathFile);
            //    documentViewer1.Visible = true;
            //}
            
        }

        private void butPrint_Click(object sender, EventArgs e)
        {
            ProcessStartInfo info = new ProcessStartInfo(_WordPathFile);
            info.Verb = "Print";
            info.CreateNoWindow = true;
            info.WindowStyle = ProcessWindowStyle.Hidden;
            Process.Start(info);
        }

    }
}
