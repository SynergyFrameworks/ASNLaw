using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using WorkgroupMgr;



namespace MatrixBuilder
{
    public partial class frmSBTemplate : MetroFramework.Forms.MetroForm
    {
        public frmSBTemplate(string SBPath, string MatrixTempPathTemplates, string MatrixTempPathTemp, bool isNew) // New SB
        {
            InitializeComponent();

            _isNew = true;
            _SBPath = SBPath;
            _MatrixTempPathTemplates = MatrixTempPathTemplates;
            _MatrixTempPathTemp = MatrixTempPathTemp;

            LoadData();

        }

        public frmSBTemplate(string SBName, string SBPath, string MatrixTempPathTemplates) // Edit SB
        {
            InitializeComponent();

            _isNew = false;
            _SBName = SBName;
            _SBPath = SBPath;
            _MatrixTempPathTemplates = MatrixTempPathTemplates;
            

            LoadData();

        }

        private bool _isNew = false;
        private string _SBName = string.Empty;
        private string _SBPath = string.Empty;
        private string _MatrixTempPathTemplates = string.Empty;
        private string _MatrixTempPathTemp = string.Empty;

        private string _MatrixTempPathTempFile = string.Empty;



        private bool _NewMSWordTemplate = false;

        private string _MatrixName = string.Empty;

        private SBMgr _SBMgr;

        private void LoadData()
        {
            lblMessage.Text = "Please enter and select values for all fields.";

            _SBMgr = new SBMgr(_SBPath);

            // Load Matrices
            string[] matrices = Directory.GetDirectories(_MatrixTempPathTemplates);
            string matrix = string.Empty;

            foreach (string matrixFolder in matrices)
            {
                matrix = Directories.GetLastFolder(matrixFolder);
                if (matrix.IndexOf('~') == -1)
                {
                    cboMatrixTemplate.Items.Add(matrix);
                }
            }


            if (!_isNew)
            {
                if (_SBName == string.Empty)
                {
                    MessageBox.Show("Storyboard Template Name is not defined.", "Unable to Load Storyboard Template", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                    this.Close();
                }

                DataRow row = _SBMgr.GetSBDataRow(_SBName);

                string description = string.Empty;
                if (row[SBTempsFields.Description] != null)
                    description = row[SBTempsFields.Description].ToString();

                this.txtbDescription.Text = description;
                this.txtbTemplate.Text = row[SBTempsFields.OrgWordTempFile].ToString();
                bool fieldsAdded = (bool)row[SBTempsFields.FieldsAdded];
                this.txtbTemplateName.Text = _SBName;

                string matrixTemplate = row[SBTempsFields.MatrixTemplate].ToString();
                int index = cboMatrixTemplate.FindStringExact(matrixTemplate);
                if (index != -1)
                {
                    cboMatrixTemplate.SelectedIndex = index;
                }

                if (fieldsAdded)
                {
                    rbYes.Checked = true;
                    rbNo.Checked = false;
                }
                else
                {
                    rbNo.Checked = true;
                    rbYes.Checked = false;

                    lblMessage.ForeColor = Color.Yellow;
                    lblMessage.Text = "Insert Matrix fields into your selected Storyboard Template so data can be auto-populated.  Click the 'Edit Storyboard Template' button.";

                }

                lblQuestion.Visible = true;
                rbYes.Visible = true;
                rbNo.Visible = true;
                butEditSBTemp.Visible = true;
                
            }
           

        }

        private void cboMatrixTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboMatrixTemplate.Text == string.Empty)
                return;

            _MatrixName = cboMatrixTemplate.Text;

            string templatePathFile = Path.Combine(_MatrixTempPathTemplates, _MatrixName, "MatrixTemp.xlsx");
            if (!File.Exists(templatePathFile))
            {
                string err = string.Concat("Unable to locate Matrix Excel Template file: ", templatePathFile);

                MessageBox.Show(err, "Matrix Template Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            reoGridControl1.Visible = true;
            reoGridControl1.Load(templatePathFile);
        }

        private void butLoadFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            openFileDialog1.Title = "Select a MS Word Storyboard Template File";
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "DOCX files (*.docx)|*.docx";
            openFileDialog1.FilterIndex = 2;
            openFileDialog1.RestoreDirectory = true;

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {

                    string pathFile = openFileDialog1.FileName;
                    if (pathFile == string.Empty)
                        return;

                    _NewMSWordTemplate = true;

                    txtbTemplate.Text = pathFile;

                    string file = Files.GetFileName(pathFile);
                    _MatrixTempPathTempFile = Path.Combine(_MatrixTempPathTemp, file);

                    try
                    {
                        if (File.Exists(_MatrixTempPathTempFile))
                        {
                            File.Delete(_MatrixTempPathTempFile);
                        }
                        File.Copy(pathFile, _MatrixTempPathTempFile);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(string.Concat("Unable to copy the selected Storyboard to Temp folder due to an error.   Error: ", ex.Message), "Unable to Copy Selected Storyboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }

                    lblQuestion.Visible = true;
                    rbYes.Visible = true;
                    rbNo.Visible = true;
                    butEditSBTemp.Visible = true;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                lblMessage.ForeColor = Color.White;
                lblMessage.Text = "Insert Matrix fields into your selected Storyboard Template so data can be auto-populated.  Click the 'Edit Storyboard Template' button.";

            }
        }

        private void butEditSBTemp_Click(object sender, EventArgs e)
        {
            string appPath = System.Windows.Forms.Application.StartupPath;
            string appPathFile = Path.Combine(appPath, "ParamClipBrd.exe");
            if (!File.Exists(appPathFile))
            {
                MessageBox.Show("Unable to to find ParamClipBrd.exe", "Storyboard Field Apllication Not found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            string availableFields = "DocAnalyzer_ProjName|MatrixColumn_A|MatrixColumn_B|MatrixColumn_C|MatrixColumn_D|MatrixColumn_E|MatrixColumn_F|MatrixColumn_G|MatrixColumn_H|MatrixColumn_I|MatrixColumn_J|MatrixColumn_K|MatrixColumn_L|MatrixColumn_M|MatrixColumn_N|MatrixColumn_O|MatrixColumn_P|MatrixColumn_Q|MatrixColumn_R|MatrixColumn_S|MatrixColumn_T|MatrixColumn_U|MatrixColumn_V|MatrixColumn_W|MatrixColumn_X|MatrixColumn_Y|MatrixColumn_Z";

            Process proc = new Process();
            proc.StartInfo.FileName = appPathFile;
            // proc.StartInfo.Arguments = @"""" + sb.ToString() + @"""";
            proc.StartInfo.Arguments = @"""" + availableFields + @"""";
            proc.Start();

            string pathFile = string.Empty;
            if (_isNew)
            {
                pathFile = _MatrixTempPathTempFile;
            }
            else
            {
                pathFile = _SBMgr.GetSBTempPathFile(_SBName);
            }


            Process.Start(pathFile);
        }

        private bool Validate()
        {
            string msg = string.Empty;

            if (txtbTemplateName.Text.Trim() == string.Empty)
            {
                msg = "Please enter the required Storyboard Name.";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = msg;
                MessageBox.Show(msg, "The Storyboard Name is Required.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return false;
            }

            if (cboMatrixTemplate.Text.Trim() == string.Empty)
            {
                msg = "Please select a Matrix Template. Storyboards should contain fields associated with a particular Matrix Template";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = msg;
                MessageBox.Show(msg, "A Matrix Template Selection is Required.", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return false;
            }

            if (txtbTemplate.Text.Trim() == string.Empty)
            {
                msg = "Please select MS Word document file (*.docx) for your Storyboard template.";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = msg;

                MessageBox.Show(msg, "A MS Word Document is Needed for a Storyboard Template", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return false;
            }

            if (_isNew)
            {
                if (_SBMgr.SBExists(txtbTemplateName.Text.Trim()))
                {
                    msg = "Please enter another Storyboard Template Name.";
                    lblMessage.ForeColor = Color.Red;                   

                    MessageBox.Show(msg, "Storyboard Name Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    lblMessage.Text = string.Concat("Storyboard Name Already Exists.    ", msg);

                    return false;
                }
            }

            if (txtbDescription.Text.Trim() == string.Empty)
            {
                msg = "Please enter a Storyboard description.";
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = msg;

                MessageBox.Show(msg, "A Storyboard Description is Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return false;
            }

            if (rbNo.Checked)
            {
                msg = "You will Not be able to use this Storyboard template until fields have been inserted into the document.";
                lblMessage.ForeColor = Color.Yellow;
                lblMessage.Text = msg;

                msg = string.Concat(msg, Environment.NewLine, Environment.NewLine, "Do you still want to save this Storyboard template now?");
                if (MessageBox.Show(msg, "Storyboard Template Not Active", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == System.Windows.Forms.DialogResult.No)
                {
                    return false;
                }
            }


            return true;
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            if (!Validate())
                return;

            Cursor.Current = Cursors.WaitCursor;

            if (_isNew)
            {
                if (!_SBMgr.CreateSBTemplate(this.txtbTemplateName.Text.Trim(), this.txtbDescription.Text.Trim(), this.txtbTemplate.Text.Trim(), _MatrixTempPathTempFile, cboMatrixTemplate.Text, rbYes.Checked, AppFolders.UserName))
                {
                    string msg = string.Concat("Unable to save your Storyboard Template due to an Error.    ", _SBMgr.ErrorMessage);
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = msg;

                    MessageBox.Show(msg, "Storyboard Template Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Cursor.Current = Cursors.Default; // Done

                    return;
                }
            }
            else // Edit
            {
                string MSWordTemp = string.Empty;
                if (_NewMSWordTemplate)
                {
                    MSWordTemp = this.txtbTemplateName.Text.Trim();
                }

                bool fieldsAdded = false;
                if (rbYes.Checked)
                {
                    fieldsAdded = true;
                }

                if (!_SBMgr.UpdateSBTemplate(this.txtbTemplateName.Text.Trim(), this.txtbDescription.Text.Trim(), MSWordTemp, cboMatrixTemplate.Text, fieldsAdded, AppFolders.UserName))
                {
                    string msg = string.Concat("Unable to save your Storyboard Template due to an Error.    ", _SBMgr.ErrorMessage);
                    lblMessage.ForeColor = Color.Red;
                    lblMessage.Text = msg;

                    MessageBox.Show(msg, "Storyboard Template Update Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    Cursor.Current = Cursors.Default; // Done

                    return;
                }
            }

            Cursor.Current = Cursors.Default; // Done

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }





    }
}
