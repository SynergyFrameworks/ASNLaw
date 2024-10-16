using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using System.Diagnostics;

using Atebion.Outlook;
using Atebion.Common;


namespace ProfessionalDocAnalyzer
{
    public partial class ucExported : UserControl
    {
        public ucExported()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private int _Count = 0;
        public int Count
        {
            get { return _Count; }
        }

        // Declare delegate for when an Exported file is deleted
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when an Exported file is deleted")]
        public event ProcessHandler DocumentDeleted;

        private string _path = string.Empty;
        private FileInformation _selectedFileInfo;

        public void LoadData(string ExportPath)
        {
            _Count = 0; // Set Default
            this.lstbExportedFiles.Items.Clear();

            _path = ExportPath; //_path;

            if (_path == string.Empty)
            {
                return;
            }

            string[] files = Files.GetFilesFromDir(_path, "*.*");

            if (files != null)
            {
                lstbExportedFiles.Items.AddRange(files);
                _Count = lstbExportedFiles.Items.Count;
                if (_Count == 0)
                {
                    butOpen.Visible = false;
                }
                else
                {
                    // Remove Temp files from list
                    lstbExportedFiles.BeginUpdate();
                    try
                    {
                        for (int i = lstbExportedFiles.Items.Count - 1; i >= 0; i--)
                        {

                            if (lstbExportedFiles.Items[i].ToString().IndexOf('~') > -1)
                            {
                                lstbExportedFiles.Items.RemoveAt(i);
                            }
                        }
                    }
                    finally
                    {
                        lstbExportedFiles.EndUpdate();
                    }
                    _Count = lstbExportedFiles.Items.Count;
                    butOpen.Visible = true;
                }
            }

           // OutLook
            //if (files != null)
            //{
            //    if (_Count > 0)
            //    {
                    Atebion.Outlook.Email email2 = new Email();
                    try
                    {
                        if (OutLookMgr.isOutLookRunning())
                        {
                            if (email2.IsOutlookConnectable())
                            {
                                butEmail.Visible = true;
                            }
                            else
                            {
                                butEmail.Visible = false;
                            }
                        }
                        else
                            butEmail.Visible = false;
                    }
                    catch
                    {
                        butEmail.Visible = false;
                    }
            //    }
            //}

        }

        private void OpenFile()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting

            if (lstbExportedFiles.Items.Count == 0)
                return;

            if (lstbExportedFiles.SelectedIndex == -1)
            {
                lstbExportedFiles.SelectedIndex = 0;
            }

            string selFile = lstbExportedFiles.GetItemText(lstbExportedFiles.SelectedItem);

            string exportPathFile = Path.Combine(_path, selFile);

            try // Added Error handling 
            {
                if (chkbOpenWMSWord.Visible) 
                {
                    if (chkbOpenWMSWord.Checked)
                        MSWordAutomation.OpenMSWord(exportPathFile);
                    else
                        Process.Start(exportPathFile);
                }
                else
                    Process.Start(exportPathFile);
            }
            catch (Exception e)
            {
                string msgErr = string.Concat("Unable to open file ", exportPathFile, "  -- ", e.Message);
                MessageBox.Show(msgErr, "Unable to Open Exported File", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                Cursor.Current = Cursors.Default; // Back to normal
            }
        }

        private void butOpen_Click(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void lstbExportedFiles_DoubleClick(object sender, EventArgs e)
        {
            OpenFile();
        }

        private void lstbExportedFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lstbExportedFiles.Items.Count == 0)
                return;

            if (lstbExportedFiles.SelectedItem == null)
                return;

            string selFile = Path.Combine(_path, lstbExportedFiles.SelectedItem.ToString());
            _selectedFileInfo = new FileInformation(selFile);

            double fileSize = 0;
            if (_selectedFileInfo.FileSize.Length > 0)
            {
                fileSize =  Math.Round(Convert.ToDouble(_selectedFileInfo.FileSize) / 1000, 2);
            }

            lblMessage.Text = string.Concat(
    "File: ", _selectedFileInfo.FileName, "\r\n",
    "Created: ", _selectedFileInfo.CreationDate, " ", _selectedFileInfo.CreationTime, "\r\n",
    "Size: ", fileSize.ToString(), " KB"
        );

            string ext = string.Empty;

            Files.GetFileName(selFile, out ext);

            if (ext.ToLower() == "html")
            {
                chkbOpenWMSWord.Visible = true;
            }
            else
            {
                chkbOpenWMSWord.Visible = false;
            }

        }

        private void txtbMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            if (lstbExportedFiles.Items.Count == 0)
                return;

            if (lstbExportedFiles.SelectedIndex == -1)
                lstbExportedFiles.SelectedIndex = 0;

            string msgDel = string.Format("Are you sure you want to Delete the Exported File: {0}", lstbExportedFiles.SelectedItem.ToString());
            if (MessageBox.Show(msgDel, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;

            string selFile = Path.Combine(_path, lstbExportedFiles.SelectedItem.ToString());

            Files.SetFileName2Obsolete(selFile);

            if (File.Exists(selFile))
            {
                MessageBox.Show("Please check if the selected file is open, then close it and try again.", "Unable to Remove the Selected Exported File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            lblMessage.Text = string.Empty; // Clear informtion for the deleted exported file

            LoadData(_path);

            if (DocumentDeleted != null) // so the Count on the Export button can be updated
                DocumentDeleted();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (_path == string.Empty)
                return;

            System.Diagnostics.Process.Start("explorer.exe", _path);
        }

        private void butEmail_Click(object sender, EventArgs e)
        {
            if (lstbExportedFiles.SelectedItem == null)
                return;

            List<string> sAttachments = new List<string>();

            string selFile = lstbExportedFiles.GetItemText(lstbExportedFiles.SelectedItem);

            string exportPathFile = string.Concat(_path, @"\", selFile);

            if (File.Exists(exportPathFile))
                sAttachments.Add(exportPathFile);
            else
                return;



            string subject = "Professional Document Analyzer's Analysis Results for Project: " + AppFolders.ProjectName + " & Document: " + AppFolders.DocName;
            string body = Environment.NewLine + Environment.NewLine + "Please see the attached file: " + selFile;


            Atebion.Outlook.Email email2 = new Email();
            email2.OpenEmailWithAttachments(string.Empty, subject, body, sAttachments.ToArray());

        }


    }
}
