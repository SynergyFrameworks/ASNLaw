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
using Atebion.DeepAnalytics;
using Atebion.Outlook;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class ucDeepAnalyticsExports : UserControl
    {
        public ucDeepAnalyticsExports()
        {
            InitializeComponent();
        }


        private Analysis _DeepAnalysis = new Analysis();

        private int _Count = 0;
        public int Count
        {
            get { return _Count; }
        }

        private string _path = string.Empty;
        private FileInformation _selectedFileInfo;

        public void LoadData()
        {
            _DeepAnalysis.CurrentDocPath = AppFolders.CurrentDocPath;

            _Count = 0; // Set Default
            this.lstbExportedFiles.Items.Clear();

            _path = _DeepAnalysis.ExportPath;

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

            // check if OutLook is installed -- Added 04/22/2016
            //dynamic oApp;
            //oApp = Activator.CreateInstance(Type.GetTypeFromProgID("Outlook.Application"));

            //if (oApp != null)
            //    butEmail.Visible = true;

            Atebion.Outlook.Email email2 = new Email();
            if (email2.IsOutlookConnectable())
                butEmail.Visible = true;



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

            string exportPathFile = string.Concat(_path, @"\", selFile);

            try 
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
            string selFile = string.Concat(_DeepAnalysis.ExportPath, @"\", lstbExportedFiles.SelectedItem.ToString());
            _selectedFileInfo = new FileInformation(selFile);

            lblMessage.Text = string.Concat(
    "File: ", _selectedFileInfo.FileName, "\r\n",
    "Created: ", _selectedFileInfo.CreationDate, " ", _selectedFileInfo.CreationTime, "\r\n",
    "Size: ", _selectedFileInfo.FileSize, " Bytes"
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

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void txtbMessage_TextChanged(object sender, EventArgs e)
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

            string selFile = string.Concat(_DeepAnalysis.ExportPath, @"\", lstbExportedFiles.SelectedItem.ToString());

            Files.SetFileName2Obsolete(selFile);

            if (File.Exists(selFile))
            {
                MessageBox.Show("Please check if the selected file is open, then close it and try again.", "Unable to Remove the Selected Exported File", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            LoadData();
              
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (_DeepAnalysis.ExportPath == string.Empty)
                return;

            System.Diagnostics.Process.Start("explorer.exe", _DeepAnalysis.ExportPath);
        }

        private void butEmail_Click(object sender, EventArgs e)
        {
            List<string> sAttachments = new List<string>();

            string selFile = lstbExportedFiles.GetItemText(lstbExportedFiles.SelectedItem);

            string exportPathFile = string.Concat(_path, @"\", selFile);

            if (File.Exists(exportPathFile))
                sAttachments.Add(exportPathFile);
            else
                return;



            string subject = "Professional Document Analyzer's Deep Analysis Results for Project: " + AppFolders.ProjectName + " & Document: " + AppFolders.DocName;
            string body = Environment.NewLine + Environment.NewLine + "Please see the attached file: " + selFile;

            Atebion.Outlook.Email email2 = new Email();
            email2.OpenEmailWithAttachments(string.Empty, subject, body, sAttachments.ToArray());

            //try
            //{
            //    dynamic oApp;
            //    dynamic oMsg;
            //    dynamic oAttachs;
            //    dynamic oAttach;

            //    oApp = Activator.CreateInstance(Type.GetTypeFromProgID("Outlook.Application"));

            //    oMsg = oApp.CreateItem(0);

            //    oMsg.To = "";
            //    oMsg.Subject = subject;
            //    oMsg.Body = body;

            //    if (sAttachments.Count > 0)
            //    {
            //        oAttachs = oMsg.Attachments;
            //        for (int i = 0; i < sAttachments.Count; i++)
            //        {
            //            oAttach = oAttachs.Add(sAttachments[i], Type.Missing, oMsg.Body.Length + 1, sAttachments[i]);
            //        }
            //    }

            //        txtbMessage.Visible = false;     
            //        lblEmailMsg.Visible = true;
            //        this.Refresh();
            //        oMsg.Display(true);
            //        lblEmailMsg.Visible = false;
            //        txtbMessage.Visible = true;
            //}
            //catch (Exception ex)
            //{
            //    string msg = string.Concat("An error has occurred while connecting with Outlook.", Environment.NewLine, Environment.NewLine, "Error: ", ex.Message);
            //    MessageBox.Show(msg, "Unable to Open OutLook", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
    }
}
