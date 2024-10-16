using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using Atebion.Outlook;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class frmExported : MetroFramework.Forms.MetroForm
    {
        public frmExported()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _ExportPath = string.Empty;
        private string _Subject = string.Empty;

        public bool LoadData(string ExportPath, string Subject)
        {
            _ExportPath = ExportPath;
            _Subject = Subject;

            _ErrorMessage = string.Empty;

            lstbExportedFiles.Items.Clear();

            string[] files = Directory.GetFiles(_ExportPath);

            if (files.Length == 0)
            {
                _ErrorMessage = string.Concat("Unable to find Exported Excel files in folder: ", _ExportPath);
                return false;
            }

            string fileX = string.Empty;

            foreach (string file in files)
            {
                fileX= Files.GetFileName(file);
                if (fileX.Substring(0,1) != "~")
                    lstbExportedFiles.Items.Add(fileX);

            }


            return true;
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbExportedFiles.Items.Count == 0)
                return;

            if (lstbExportedFiles.SelectedItem == null)
                return;

            string selFile = Path.Combine(_ExportPath, lstbExportedFiles.SelectedItem.ToString());
            FileInformation _selectedFileInfo = new FileInformation(selFile);

            lblMessage.Text = string.Concat(
    "File: ", _selectedFileInfo.FileName, "\r\n",
    "Created: ", _selectedFileInfo.CreationDate, " ", _selectedFileInfo.CreationTime, "\r\n",
    "Size: ", _selectedFileInfo.FileSize, " Bytes"
        );
        }

        private void butEditFile_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting

            if (lstbExportedFiles.Items.Count == 0)
                return;

            if (lstbExportedFiles.SelectedIndex == -1)
            {
                lstbExportedFiles.SelectedIndex = 0;
            }

            string selFile = lstbExportedFiles.Text;

            string exportPathFile = Path.Combine(_ExportPath, selFile);

            try
            {
                Process.Start(exportPathFile);
            }
            catch (Exception ex)
            {
                string msgErr = string.Concat("Unable to open file ", exportPathFile, "  -- ", ex.Message);
                MessageBox.Show(msgErr, "Unable to Open Exported File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default; // Back to normal
            }
        }

        private void butRemoveFile_Click(object sender, EventArgs e)
        {
            if (lstbExportedFiles.Items.Count == 0)
                return;

            if (lstbExportedFiles.SelectedIndex == -1)
                lstbExportedFiles.SelectedIndex = 0;

            string msgDel = string.Format("Are you sure you want to Delete the Exported File: {0}", lstbExportedFiles.SelectedItem.ToString());
            if (MessageBox.Show(msgDel, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string selFile = Path.Combine(_ExportPath, lstbExportedFiles.SelectedItem.ToString());

            try
            {
                File.Delete(selFile);
            }
            catch
            {
                MessageBox.Show("Unable to delete the selected file. The file may be open.", "File Not Deleted", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


         //   Files.SetFileName2Obsolete(selFile);

            //if (File.Exists(selFile))
            //{
            //    MessageBox.Show("Please check if the selected file is open, then close it and try again.", "Unable to Remove the Selected Exported File", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

        //    lblMessage.Text = string.Empty; // Added 3.29.2015 -- clear informtion for the deleted exported file

            LoadData(_ExportPath, _Subject);

              
        }

        private void butEmailFile_Click(object sender, EventArgs e)
        {
            if (lstbExportedFiles.Items.Count == 0)
                return;

            if (lstbExportedFiles.SelectedIndex == -1)
                lstbExportedFiles.SelectedIndex = 0;

            Cursor.Current = Cursors.WaitCursor; // Waiting

            List<string> sAttachments = new List<string>();

            string selFile = lstbExportedFiles.Text;

            string exportPathFile = Path.Combine(_ExportPath, lstbExportedFiles.Text);

            if (File.Exists(exportPathFile))
                sAttachments.Add(exportPathFile);
            else
            {
                Cursor.Current = Cursors.Default;
                return;
            }



            string subject = string.Concat("Concept Analyzer's Analysis Results for ", _Subject) ;
            string body = string.Concat(Environment.NewLine, Environment.NewLine, "Please see the attached file: " + selFile);


            Atebion.Outlook.Email email2 = new Email();
            email2.OpenEmailWithAttachments(string.Empty, subject, body, sAttachments.ToArray());

            Cursor.Current = Cursors.Default;
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }
    }
}
