using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Windows.Forms;
using WorkgroupMgr;
using Atebion.Outlook;



namespace MatrixBuilder
{
    public partial class ucMatrixSB : UserControl
    {
        public ucMatrixSB()
        {
            InitializeComponent();
        }

        private MatrixSB _MatrixSBMgr;
        private string _SBTempPath = string.Empty;
        private string _MatrixPath = string.Empty;
        private string _MatrixPathFile_XLSX = string.Empty;
        private string _MatrixTemplateName = string.Empty;
        private int _StartDataRow = 1;

        private DataSet _dsSBMatrix;

        private Atebion.Outlook.Email _EmailOutLook;

        private string _Last_sbViewPathFile = string.Empty;

        // Declare delegate for when a project has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Matrix Row is selected")]
        public event ProcessHandler RowSelected;

        private int _SelectedSBUID;
        public int CurrentSBUID
        {
            get { return _SelectedSBUID; }
        }

        private string _SelectedMatrixRow = string.Empty;
        public string SelectedMatrixRow
        {
            get { return _SelectedMatrixRow; }
        }


        public void LoadData(string SBTempPath, string MatrixPath, string MatrixPathFile_XLSX, string MatrixTemplateName, int StartDataRow)
        {
            _SBTempPath = SBTempPath;
            _MatrixPath = MatrixPath;
            _MatrixPathFile_XLSX = MatrixPathFile_XLSX;
            _MatrixTemplateName = MatrixTemplateName;
            _StartDataRow = StartDataRow;

            _MatrixSBMgr = new MatrixSB(SBTempPath, MatrixPath);

            if (_MatrixSBMgr.ErrorMessage.Length > 0)
            {
                MessageBox.Show(_MatrixSBMgr.ErrorMessage, "A Matrix-Storyboard Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            LoadSBMatrix();

            _EmailOutLook = new Email();
            if (_EmailOutLook.IsOutlookConnectable())
                butEmail.Visible = true;
        }

        private void LoadSBMatrix()
        {
            _dsSBMatrix = _MatrixSBMgr.dsSBMatrix;

            dvgSBs.DataSource = _dsSBMatrix.Tables[0];

            dvgSBs.Columns[MatrixSBFields.SB_UID].Visible = false;
            dvgSBs.Columns[MatrixSBFields.SBTemplateName].Visible = false;
            dvgSBs.Columns[MatrixSBFields.Description].Visible = false;
            dvgSBs.Columns[MatrixSBFields.CreatedBy].Visible = false;
            dvgSBs.Columns[MatrixSBFields.CreatedDateTime].Visible = false;

            dvgSBs.ColumnHeadersVisible = false;

            dvgSBs.RowHeadersVisible = false;
           // dvgSBs.ColumnHeadersVisible = false;

            dvgSBs.AllowUserToAddRows = false; // Remove blank last row

           // splitContMain.Panel1.SplitterDistance = 50;
            //splitContRight.Width = 50;

        }

        private void butPrintSBTemplate_Click(object sender, EventArgs e)
        {
           // string pathFile = _sbMgr.GetSBTempViewPathFile(_StoryboardSelected); // Load "View" SB Template to prevent locking errors
            //if (pathFile != string.Empty)
            //{
            //    ProcessStartInfo info = new ProcessStartInfo(pathFile);
            //    info.Verb = "Print";
            //    info.CreateNoWindow = true;
            //    info.WindowStyle = ProcessWindowStyle.Hidden;
            //    Process.Start(info);

            //}
        }

        private void butNew_Click(object sender, EventArgs e)
        {
            try
            {

                frmMatrixSB frm1 = new frmMatrixSB(_MatrixPathFile_XLSX, _MatrixSBMgr, _MatrixTemplateName, _StartDataRow);
                frm1.LoadData(); // Added 03.29.2019
                if (frm1.ShowDialog() == DialogResult.OK)
                {
                    _SelectedSBUID = frm1.NewSBUID;
                    LoadSBMatrix();
                }
            }
            catch
            {
                return;
            }

        }

        private void dvgSBs_SelectionChanged(object sender, EventArgs e)
        {          

            DataGridViewRow row = dvgSBs.CurrentRow;

            if (row == null)
                return;

            _SelectedSBUID = (int) row.Cells[MatrixSBFields.SB_UID].Value;

            string description = row.Cells[MatrixSBFields.SB_UID].Value.ToString();
            if (description == string.Empty)
            {
                lblCaptionDescription.Text = string.Empty;
            }
            else
            {
                lblCaptionDescription.Text = string.Concat(row.Cells[MatrixSBFields.Name].Value.ToString(), ":  ", row.Cells[MatrixSBFields.Description].Value.ToString());
            }

            string[] matrixRowNos = _MatrixSBMgr.GetMatrixRows(_SelectedSBUID);
            lstbMatrixRows.DataSource = matrixRowNos;

            string sbViewPathFile = _MatrixSBMgr.GetSBViewFileDocx(_SelectedSBUID);

            if (sbViewPathFile == _Last_sbViewPathFile) // added 08.01.2020
            {
                return;
            }
            _Last_sbViewPathFile = sbViewPathFile;

            if (sbViewPathFile.Length == 0)
            {
                MessageBox.Show(_MatrixSBMgr.ErrorMessage, "An Storyboard Document Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                documentViewer1.Visible = false;
                return;
            }

            documentViewer1.CloseDocument(); // Added 08.01.2020

            documentViewer1.Visible = true;
            try
            {
                this.documentViewer1.LoadDocument(sbViewPathFile);
            }
            catch
            {
                documentViewer1.Visible = false;

                MessageBox.Show("An error occured in the Document Viewer.", "Unable to View Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void butOpenSB_Click(object sender, EventArgs e)
        {
            string SBDocx = _MatrixSBMgr.GetSBFileDocx(_SelectedSBUID);
            if (SBDocx == string.Empty)
            {
                MessageBox.Show("An error has occurred while getting the selected Storyboard document.    Error: ", _MatrixSBMgr.ErrorMessage);
                return;
            }

            Process.Start(SBDocx);
        }

        private void lstbMatrixRows_Click(object sender, EventArgs e)
        {

            _SelectedMatrixRow = lstbMatrixRows.Text;
            if (_SelectedMatrixRow == string.Empty)
                return;

            if (RowSelected != null)
            {
                RowSelected();
            }

  
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            if (dvgSBs.RowCount == 0)
                return;

            documentViewer1.CloseDocument();
            documentViewer1.Refresh();

            Application.DoEvents();

            frmMatrixSB frm1 = new frmMatrixSB(_SelectedSBUID, _MatrixPathFile_XLSX, _MatrixSBMgr, _MatrixTemplateName, _StartDataRow);
            frm1.LoadData(); // Added 03.29.2019
            if (frm1.ShowDialog() == DialogResult.OK)
            {
                _SelectedSBUID = frm1.NewSBUID;
                LoadSBMatrix();
            }
            else
            {
                string sbViewPathFile = _MatrixSBMgr.GetSBViewFileDocx(_SelectedSBUID);
                if (sbViewPathFile.Length == 0)
                {
                    MessageBox.Show(_MatrixSBMgr.ErrorMessage, "An Storyboard Document Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    documentViewer1.Visible = false;
                    return;
                }

                this.documentViewer1.LoadDocument(sbViewPathFile); 
            }

        }

        private void butEmail_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 
      
            string sPathFile = _MatrixSBMgr.GetSBFileDocx(_SelectedSBUID);
            if (sPathFile.Length == 0)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(_MatrixSBMgr.ErrorMessage, "An Storyboard Document Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> sAttachments = new List<string>();

            sAttachments.Add(sPathFile);

            string file = Files.GetFileName(sPathFile);

            string subject = "Storyboard";
            string body = string.Concat(Environment.NewLine, Environment.NewLine + "Please see the attached file: ", file);

            _EmailOutLook.OpenEmailWithAttachments(string.Empty, subject, body, sAttachments.ToArray());

            Cursor.Current = Cursors.Default;
        }
    }
}
