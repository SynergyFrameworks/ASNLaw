using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using WorkgroupMgr;

using unvell.ReoGrid;
using unvell.ReoGrid.Events;

namespace MatrixBuilder
{
    public partial class frmMatrix : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// New Matrix
        /// </summary>
        /// <param name="ProjectRootFolder"></param>
        /// <param name="ProjectName"></param>
        public frmMatrix(string ProjectRootFolder, string ProjectName, string MatrixTempPathTemplates)
        {
            InitializeComponent();

            _ProjectRootFolder = ProjectRootFolder;
            _ProjectName = ProjectName;
          //  _DocTypePath = DocTypePath;
            _MatrixTempPathTemplates = MatrixTempPathTemplates;

            _isNew = true;

            LoadData();
        }

        /// <summary>
        /// Edit Matrix
        /// </summary>
        /// <param name="ProjectRootFolder"></param>
        /// <param name="ProjectName"></param>
        /// <param name="MatrixName"></param>
        public frmMatrix(string ProjectRootFolder, string ProjectName, string MatrixName, string MatrixTempPathTemplates)
        {
            InitializeComponent();

            _ProjectRootFolder = ProjectRootFolder;
            _ProjectName = ProjectName;
            _MatrixName = MatrixName;
         //   _DocTypePath = DocTypePath;
            _MatrixTempPathTemplates = MatrixTempPathTemplates;

            _isNew = false;
        }

        private bool _isNew = true;

        private string _ProjectRootFolder = string.Empty;
        private string _ProjectName = string.Empty;
        private string _MatrixName = string.Empty;
   //     private string _DocTypePath = string.Empty;
        private string _MatrixTempPathTemplates = string.Empty;

        private StringBuilder _sbSummary = new StringBuilder();
        private StringBuilder _sbAlerts = new StringBuilder();


        private Matrices _MatricesMgr;
        private Projects _ProjectMgr;
        private DocTypesMgr _DocTypesMgr;

        private string _MatrixSelected = string.Empty;
        private DataSet _dsMatrixSettings;

        private DataTable _dtDocsAssociation;

        private string _MATRIX_FILE_XLSX = "MatrixTemp.xlsx";
        private string _MATRIX_DATA_FILE_XLSX = "MatrixTempData.xlsx";
      //  private string _MATRIX_FILE_HTML = "Matrix.html";

        ////Auto-Populate - 1st Document
        //private string _AutoPop1Source = string.Empty;
        //private string _AutoPop1Document = string.Empty;
        //private string _AutoPop1DocType = string.Empty;
        //private string _AutoPop1Col = string.Empty;

        ////Auto-Populate - 2nd Document
        //private string _AutoPop2Source = string.Empty;
        //private string _AutoPop2Document = string.Empty;
        //private string _AutoPop2DocType = string.Empty;
        //private string _AutoPop2Col = string.Empty;

        private strucAutoPop _AutoPop1;
        private strucAutoPop _AutoPop2;
        private struct strucAutoPop
        {
            public string Source;
            public string Document;
            public string DocType;
            public string Column;
            public string Text;
            public bool UserSelectionsOK;
           
        };


        private Modes _currentMode = Modes.Matrix;
        private enum Modes
        {
            Matrix = 0,
            DocTypes = 1,
            Summary = 2
        }

        private void LoadData()
        {
          //  _DocTypesMgr = new DocTypesMgr(_DocTypePath);

            panMatrix.Dock = DockStyle.Fill;

            if (_isNew)
            {
                this.Text = "     Matrix - New";
                this.Refresh();
                _MatricesMgr = new Matrices(_ProjectRootFolder, _MatrixTempPathTemplates, _ProjectName);
                _ProjectMgr = new Projects(_ProjectRootFolder);

                _currentMode = Modes.Matrix;

                AdjMode();

                LoadMatrices();

                
            }
            else
            {

            }

        }

        private void LoadMatrices()
        {
            int count = 0;
            string[] matrices = Directory.GetDirectories(_MatrixTempPathTemplates);
            string matrix = string.Empty;

            foreach (string matrixFolder in matrices)
            {
                matrix = Directories.GetLastFolder(matrixFolder);
                if (matrix.IndexOf('~') == -1)
                {
                    cboMatrixTemplate.Items.Add(matrix);
                    count++;
                }
            }
        }

        private void LoadDocs()
        {
            if (_isNew) // New
            {
               // string selectedMatrixFile = string.Concat(cboMatrixTemplate.Text, ".xlsx");
                string selectedMatrixPathFile = Path.Combine(_MatrixTempPathTemplates, cboMatrixTemplate.Text, _MATRIX_FILE_XLSX);
                if (File.Exists(selectedMatrixPathFile))
                {
                    reoGridControl1.Load(selectedMatrixPathFile);
                    var worksheet = reoGridControl1.CurrentWorksheet;
                    worksheet.SetSettings(WorksheetSettings.Edit_Readonly, true);

                    reoGridControl1.Visible = true;

                    string selectedMatrixSumPathFile = Path.Combine(_MatrixTempPathTemplates, cboMatrixTemplate.Text, "Summary.txt");
                    if (File.Exists(selectedMatrixSumPathFile))
                    {
                        lblSelTempSumValue.Text = Files.ReadFile(selectedMatrixSumPathFile);

                        lblSelTempSummary.Visible = true;

                        txtbSelTempSummary.Visible = true;

                        picTempSum.Visible = true;
                    }
                    else
                    {
                        lblSelTempSummary.Visible = false;

                        txtbSelTempSummary.Visible = false;

                        picTempSum.Visible = false;
                    }

                }
                else
                {
                    reoGridControl1.Visible = false;
                }

                dgvDocTypes.Rows.Clear();
                dgvDocTypes.Columns.Clear();
                    
                this.lblDefinition.Text = "Create a new Matrix for your selected Project by setting the project’s document associations to Document Types.";
                _dsMatrixSettings = _MatricesMgr.GetMatrixSettings(_MatrixSelected);
                if (_dsMatrixSettings == null)
                {
                    MessageBox.Show(_MatricesMgr.ErrorMessage, "Unable to Get Matrix Template Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblDefinition.Text = _MatricesMgr.ErrorMessage;
                    lblDefinition.ForeColor = Color.Red;
                    return;
                }

                string[] cols = new string[]
                {   
                    "A",
                    "B",
                    "C",
                    "D",
                    "E",
                    "F",
                    "G",
                    "H",
                    "I",
                    "J",
                    "K",
                    "L",
                    "M",
                    "N",
                    "O",
                    "P",
                    "Q",
                    "R",
                    "S",
                    "T",
                    "U",
                    "V",
                    "W",
                    "X",
                    "Y",
                    "Z"
                };


               // cboAutoPop1Col.Items.Clear();
                cboAutoPop1Col.DataSource = null;
                cboAutoPop1Col.DataSource = cols;

              //  cboAutoPop2Col.Items.Clear();
                cboAutoPop2Col.DataSource = null;
                cboAutoPop2Col.DataSource = cols;


                string[] docs = _ProjectMgr.GetDocuemntsNames(_ProjectName);

                dgvDocTypes.Columns.Add(MatricesFields.ProjectDocumentName, "Documents");
                foreach (string doc in docs)
                {             
                    int rowIndex = dgvDocTypes.Rows.Add();
                    DataGridViewRow row = dgvDocTypes.Rows[rowIndex];
                    row.Cells[MatricesFields.ProjectDocumentName].Value = doc;
                }

                cboAutoPop1.Items.Clear();
                cboAutoPop2.Items.Clear();
                List<string> docTypes = new List<string>();
                string item = string.Empty;
                docTypes.Add(""); // Add Blank 
                cboAutoPop1.Items.Add(""); // Add Blank -- Added 12.04.2017
                cboAutoPop2.Items.Add(""); // Add Blank -- Added 12.04.2017
                foreach (DataRow row in _dsMatrixSettings.Tables[DocTypesFields.TableName].Rows)
                {
                    item = row[DocTypesFields.Item].ToString();
                    docTypes.Add(item);
                    cboAutoPop1.Items.Add(item); // Added 12.04.2017
                    cboAutoPop2.Items.Add(item); // Added 12.04.2017
                }





                DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
                column.Name = WorkgroupMgr.MatricesFields.DocTypeItem;
                column.DataSource = docTypes;
                column.HeaderText = "Document Types";
                column.DataPropertyName = WorkgroupMgr.MatricesFields.DocTypeItem;

              //  dgvDocTypes.DataSource = docs;

                dgvDocTypes.Columns.Add(column);

                dgvDocTypes.Columns[0].ReadOnly = true;

                dgvDocTypes.AllowUserToAddRows = false; // Remove blank last row
            }
            else // Edit
            {
                this.Text = "     Matrix - Edit";

            }

            butDocTypes.Visible = true;
            
        }

        private void AdjMode()
        {
            panMatrix.Visible = false;
            panMatrix.Dock = DockStyle.None;
            panDocTypes.Visible = false;
            panDocTypes.Dock = DockStyle.None;
            panSummary.Visible = false;
            panSummary.Dock = DockStyle.None;

            butDocTypes.Highlight = false;
            butDocTypes.Refresh();
            butMatrix.Highlight = false;
            butMatrix.Refresh();
            butSummary.Highlight = false;
            butSummary.Refresh();

            switch (_currentMode)
            {
                case Modes.Matrix:
                    butMatrix.Highlight = true;
                    butMatrix.Refresh();

                    panMatrix.Visible = true;
                    panMatrix.Dock = DockStyle.Fill;
                    break;

                case Modes.DocTypes:
                    butDocTypes.Highlight = true;
                    butDocTypes.Refresh();

                    panDocTypes.Visible = true;
                    panDocTypes.Dock = DockStyle.Fill;

             //       adjDocTypeItemColumns();
                    break;

                case Modes.Summary:
                    butSummary.Highlight = true;
                    butSummary.Refresh();

                    panSummary.Visible = true;
                    panSummary.Dock = DockStyle.Fill;

                    break;
            }
        }

        private void butMatrix_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.Matrix;
            AdjMode();
        }

        private void butDocTypes_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.DocTypes;
            AdjMode();
        }

        private void butSummary_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.Summary;
            AdjMode();
        }

        private void butBack_Click(object sender, EventArgs e)
        {
            _currentMode--;

            if ((int)_currentMode < 0)
                _currentMode = (Modes)0;

            _currentMode = (Modes)_currentMode;
            AdjMode();
        }

        private void butNext_Click(object sender, EventArgs e)
        {

            if ((int)_currentMode > 2)
                _currentMode = (Modes)2;

            _currentMode = (Modes)_currentMode;
            AdjMode();
        }

        private void cboMatrixTemplate_SelectedIndexChanged(object sender, EventArgs e)
        {
            _MatrixSelected = cboMatrixTemplate.Text;

            if (_MatrixSelected == string.Empty)
                return;

            LoadDocs();

        }

        private bool ValidateAndSummary()
        {
            bool result = true;

            lblMatrixName.ForeColor = Color.White;
            lblMatrixTemplate.ForeColor = Color.White;
            lblMatrixDescription.ForeColor = Color.White;
            lblDocTypeCaption.ForeColor = Color.White;

            _sbSummary.Clear();
            _sbAlerts.Clear();

            _sbSummary.AppendLine("Matrix Settings Summary");
            _sbSummary.AppendLine("");
            _sbSummary.AppendLine("**** Matrix ****");
            _sbSummary.AppendLine("");

            _sbAlerts.AppendLine("Alerts");
            _sbAlerts.AppendLine("");
            _sbAlerts.AppendLine("**** Matrix ****");
            _sbAlerts.AppendLine("");


            if (txtbMatrixName.Text.Trim() == string.Empty)
            {
                lblMatrixName.ForeColor = Color.Red;
                _sbAlerts.AppendLine("Matrix Name is Required!");
                _sbSummary.AppendLine("Matrix Name was Not Defined!");
                result = false;
            }
            else
            {
                _sbSummary.AppendLine(string.Concat("Matrix Name: ", txtbMatrixName.Text.Trim()));
            }

            if (cboMatrixTemplate.Text == string.Empty)
            {
                lblMatrixTemplate.ForeColor = Color.Red;

                _sbAlerts.AppendLine("Matrix Template is Required!");
                _sbSummary.AppendLine("Matrix Template was NOT Selected!");
                result = false;
            }
            else
            {
                _sbSummary.AppendLine(string.Concat("Matrix Template: ", cboMatrixTemplate.Text));
            }

            if (txtbDescription.Text.Trim() == string.Empty)
            {
                lblMatrixDescription.ForeColor = Color.Red;
                
                _sbAlerts.AppendLine("Matrix Description is Required!");
                _sbSummary.AppendLine("Matrix Description was Not Entered!");
                result = false;
            }
            else
            {
                _sbSummary.AppendLine(string.Concat("Matrix Description: ", txtbDescription.Text.Trim()));
            }


            // Document Types
            _sbSummary.AppendLine("");
            _sbSummary.AppendLine("**** Document Types ****");
            _sbSummary.AppendLine("");

            _sbAlerts.AppendLine("");
            _sbAlerts.AppendLine("**** Document Types ****");
            _sbAlerts.AppendLine("");

            _dtDocsAssociation = CreateEmptyTable();

            string docType = string.Empty;
            List<string> docTypes = new List<string>();
            foreach (DataGridViewRow row in this.dgvDocTypes.Rows)
            {
                if (row.Cells[1].Value != null)
                {
                    docType = row.Cells[1].Value.ToString();
                    if (docType != string.Empty)
                    {
                        docTypes.Add(docType);
                        _sbSummary.AppendLine(string.Concat("Document: ", row.Cells[0].Value.ToString(), "  =  ", docType));
                        DataRow dRow = _dtDocsAssociation.NewRow();
                        dRow[MatricesFields.DocTypeItem] = docType;
                        dRow[MatricesFields.ProjectDocumentName] = row.Cells[0].Value.ToString();
                        _dtDocsAssociation.Rows.Add(dRow);
                    }
                }
            }

            if (docTypes.Count == 0)
            {
                _sbAlerts.AppendLine("Documents require Document Types association!");
                _sbSummary.AppendLine("No Document Type has been assocatied with your Documents");
                lblDocTypeCaption.ForeColor = Color.Red;
                result = false;
            }
            else
            {
                var dups = docTypes.GroupBy(x => x)
                            .Where(group => group.Count() > 1)
                            .Select(group => group.Key);


                foreach (string dup in dups)
                {
                    _sbAlerts.AppendLine(string.Concat("Duplicate Selection: ", dup));
                    lblDocTypeCaption.ForeColor = Color.Red;
                    result = false;
                }
            }

            this.txtAlerts.Text = _sbAlerts.ToString();
            this.txtbSummaryText.Text = _sbSummary.ToString();

            return result;
        }

        private bool ValidateAutoPopulateSettings()
        {
            _AutoPop1.Column = string.Empty;
            _AutoPop1.DocType = string.Empty;
            _AutoPop1.Document = string.Empty;
            _AutoPop1.Source = string.Empty;
            _AutoPop1.Text = string.Empty;
            _AutoPop1.UserSelectionsOK = false;

            _AutoPop2.Column = string.Empty;
            _AutoPop2.DocType = string.Empty;
            _AutoPop2.Document = string.Empty;
            _AutoPop2.Source = string.Empty;
            _AutoPop2.Text = string.Empty;
            _AutoPop2.UserSelectionsOK = false;

   

            if (cboAutoPop1.Text.Trim() == string.Empty && cboAutoPop2.Text.Trim() == string.Empty)
                return true;

            // Set Vars for the Auto-Populate parsed the 1st selected document 
            if (cboAutoPop1.Text.Trim() != string.Empty)
            {
                foreach (DataRow row in _dtDocsAssociation.Rows)
                {
                    if (row[MatricesFields.DocTypeItem].ToString() == cboAutoPop1.Text)
                    {
                        _AutoPop1.Document = row[MatricesFields.ProjectDocumentName].ToString();
                        _AutoPop1.Text = cboAutoPop1Col.Text;
                        _AutoPop1.DocType = row[MatricesFields.DocTypeItem].ToString();
                        _AutoPop1.Source = row[MatricesFields.DocTypeSource].ToString(); // e.g.  Analysis Results or Deep Analysis

                        // Check if User is using this Document Type
                        foreach (DataGridViewRow dgvRow in dgvDocTypes.Rows)
                        {
                            if (dgvRow.Cells[WorkgroupMgr.MatricesFields.DocTypeItem].Value != null)
                            {
                                if (_AutoPop1.DocType == dgvRow.Cells[WorkgroupMgr.MatricesFields.DocTypeItem].Value.ToString())
                                {
                                    _AutoPop1.UserSelectionsOK = true;

                                    _sbSummary.AppendLine("");
                                    _sbSummary.AppendLine("**** Auto-Populate ****");
                                    _sbSummary.AppendLine(string.Concat("Document Type: ", _AutoPop1.DocType));
                                    _sbSummary.AppendLine(string.Concat("Document: ", _AutoPop1.Document));
                                    _sbSummary.AppendLine(string.Concat("Source: ", _AutoPop1.Source));
                                    _sbSummary.AppendLine("");

                                    _AutoPop1.UserSelectionsOK = true;
                                }
                            }
                        }

                        break;
                    }
                }
            }
            else
            {
                _AutoPop1.UserSelectionsOK = true;
            }

            // Set Vars for the Auto-Populate parsed the 2nd selected document 
            if (cboAutoPop2.Text.Trim() != string.Empty)
            {
                foreach (DataRow row in _dtDocsAssociation.Rows)
                {
                    if (row[MatricesFields.DocTypeItem].ToString() == cboAutoPop2.Text)
                    {
                        _AutoPop2.Document = row[MatricesFields.ProjectDocumentName].ToString();
                        _AutoPop2.Text = cboAutoPop1Col.Text;
                        _AutoPop2.DocType = row[MatricesFields.DocTypeItem].ToString();
                        _AutoPop2.Source = row[MatricesFields.DocTypeSource].ToString(); // e.g.  Analysis Results or Deep Analysis

                        // Check if User is using this Document Type
                        foreach (DataGridViewRow dgvRow in dgvDocTypes.Rows)
                        {
                            if (dgvRow.Cells[WorkgroupMgr.MatricesFields.DocTypeItem].Value != null)
                            {
                                if (_AutoPop2.DocType == dgvRow.Cells[WorkgroupMgr.MatricesFields.DocTypeItem].Value.ToString())
                                {
                                    _AutoPop2.UserSelectionsOK = true;

                                    _sbSummary.AppendLine("");
                                    _sbSummary.AppendLine("**** Auto-Populate ****");
                                    _sbSummary.AppendLine(string.Concat("Document Type: ", _AutoPop2.DocType));
                                    _sbSummary.AppendLine(string.Concat("Document: ", _AutoPop2.Document));
                                    _sbSummary.AppendLine(string.Concat("Source: ", _AutoPop2.Source));
                                    _sbSummary.AppendLine("");

                                    _AutoPop2.UserSelectionsOK = true;
                                }
                            }
                        }

                        break;
                    }
                }
            }
            else
            {
                _AutoPop2.UserSelectionsOK = true;
            }


            if (_AutoPop1.UserSelectionsOK && _AutoPop2.UserSelectionsOK)
            {
                return true;
            }
            else
            {
                if (!_AutoPop1.UserSelectionsOK)
                {
                    _sbAlerts.AppendLine("");
                    _sbAlerts.AppendLine("**** Auto-Populate ****");
                    _sbAlerts.AppendLine(string.Concat("Document Type ", _AutoPop1.DocType, " has Not been associated with a document."));
                    _sbAlerts.AppendLine("");
                }

                if (!_AutoPop2.UserSelectionsOK)
                {
                    _sbAlerts.AppendLine("");
                    _sbAlerts.AppendLine("**** Auto-Populate ****");
                    _sbAlerts.AppendLine(string.Concat("Document Type ", _AutoPop2.DocType, " has Not been associated with a document."));
                    _sbAlerts.AppendLine("");
                }

                return false;
            }
        }

        private DataTable CreateEmptyTable()
        {
            DataTable dtDocsAssociation = new DataTable(MatricesFields.DocsAssociationTableName);
            dtDocsAssociation.Columns.Add(MatricesFields.DocTypeItem, typeof(string));
            dtDocsAssociation.Columns.Add(MatricesFields.ProjectDocumentName, typeof(string));
            dtDocsAssociation.Columns.Add(MatricesFields.DocTypeSource, typeof(string));

            return dtDocsAssociation;
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            if (!ValidateAndSummary())
            {
                MessageBox.Show("This Matrix settings failed validation. See Alerts in the Summary for details and make the appropriate changes and try to Save again.", "Failed Validation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                butSummary.Visible = true;

                _currentMode = Modes.Summary;
                AdjMode();
                Cursor.Current = Cursors.Default; // Done
                return;
            }

            if (!ValidateAutoPopulateSettings())
            {
                MessageBox.Show("This Matrix's parsed document Auto Populating settings failed validation. See Alerts in the Summary for details and make the appropriate changes and try to Save again.", "Failed Validation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                butSummary.Visible = true;

                _currentMode = Modes.Summary;
                AdjMode();
                Cursor.Current = Cursors.Default; // Done
                return;
            }

            if (_isNew)
            {
                if (!_MatricesMgr.CreateMatrix(txtbMatrixName.Text.Trim(), txtbDescription.Text.Trim(), cboMatrixTemplate.Text, AppFolders.UserName, _dtDocsAssociation))
                {
                    MessageBox.Show(_MatricesMgr.ErrorMessage, "Unable to Save New Matrix", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    lblDefinition.Text = _MatricesMgr.ErrorMessage;
                    lblDefinition.ForeColor = Color.Red;
                }

                // Check to see if the Matrix should be auto-populated
                if (cboAutoPop1.Text.Trim() != string.Empty || cboAutoPop2.Text.Trim() != string.Empty)
                {
                    AutoPopulate();
                }
            }
            else
            {
                // ToDo Write Edit Save
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private bool AutoPopulate()
        {
            //ToDo
            string matrixPath = _MatricesMgr.CurrentMatrixPath;

            MatrixAllocations matrixAllocations = new MatrixAllocations(matrixPath);

            if (matrixAllocations.ErrorMessage.Length > 0)
            {

                return false;
            }

            string item = cboAutoPop1.Text.Trim();
            if (item.Length == 0)
                return false;

            string column = cboAutoPop1Col.Text.Trim();
            if (column.Length == 0)
                return false;

            string contenttype;
            string source = GetSource(_AutoPop1.DocType, out contenttype);

            int startDataRowNo = GetStartDataRowNo();

            string currentDocPath = Path.Combine(_ProjectRootFolder, _ProjectName, "Docs", _AutoPop1.Document, "Current");

            bool result = matrixAllocations.AutoAllocateDocType(item, column, source, contenttype, startDataRowNo, _AutoPop1.Document, currentDocPath, _MatricesMgr.CurrentMatrixPath, AppFolders.UserName);

            if (result)
            {
                if (panMatrixPreview.Visible)
                {
                    string matrixPathFile = Path.Combine(_MatricesMgr.CurrentMatrixPath, _MATRIX_FILE_XLSX);
                    if (File.Exists(matrixPathFile))
                    {
                        reoGridControl1.Load(matrixPathFile);
                        var worksheet = reoGridControl1.CurrentWorksheet;

                        var colwidth = worksheet.GetColumnWidth(0);
                        worksheet.SetColumnsWidth(0, 1, 100);
                        worksheet.SetColumnsWidth(0, 1, colwidth);
                        reoGridControl1.Refresh();

                        SaveMatrix(matrixPathFile);
                    }
                }
            }

            return result;
        }

        private bool SaveMatrix(string MatrixPathFile)
        {
            try
            {
              //  Cursor.Current = Cursors.WaitCursor; // Waiting 
                reoGridControl1.Save(MatrixPathFile);
            }

            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(string.Concat("An Error occurred while saving the Matrix.      Error: ", ex.Message), "Matrix Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            Application.DoEvents();

            //try
            //{
            //    string htmlTemplate;
            //    var worksheet = reoGridControl1.CurrentWorksheet;
            //    using (MemoryStream ms = new MemoryStream(8192))
            //    {
            //        worksheet.ExportAsHTML(ms);
            //        htmlTemplate = Encoding.Default.GetString(ms.ToArray());
            //    }

            //    string pathFile = Path.Combine(_MatricesMgr.CurrentMatrixPath, _MATRIX_FILE_HTML);
            //    Files.WriteStringToFile(htmlTemplate, pathFile);

            // //   Cursor.Current = Cursors.Default;
            //}
            //catch 
            //{

            //}
  

            return true;
        }

        private string GetSource(string item, out string contenttype)
        {
            contenttype = string.Empty;

            if (_dsMatrixSettings == null)
                return string.Empty;

            string itemX = string.Empty;
            string source = string.Empty;
            

            foreach (DataRow row in _dsMatrixSettings.Tables[DocTypesFields.TableName].Rows)
            {
                itemX = row[DocTypesFields.Item].ToString();

                if (item == itemX)
                {
                   source = row[DocTypesFields.Source].ToString();
                   contenttype = row[DocTypesFields.ContentType].ToString();
                   return source;
                }
            }

            return source;
        }

        private int GetStartDataRowNo()
        {
            if (_dsMatrixSettings == null)
                return -1;

            string rowNumber = _dsMatrixSettings.Tables[MatrixTemplateFields.TableName].Rows[0][MatrixTemplateFields.RowDataStarts].ToString();

            if (DataFunctions.IsNumeric(rowNumber))
            {
                return Convert.ToInt32(rowNumber);
            }

            return -1;
        }

        private bool CovertSegments2HTML()
        {
            // Convert Parsed RTF files into HTML files 
            AtebionRTFf2HTMLf.Convert convert = new AtebionRTFf2HTMLf.Convert();

            if (AppFolders.DocParsedSec == string.Empty)
            {
                MessageBox.Show("Unable to locate the current Project's parsed segments folder.", "Project's Parsed Segments Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            int qtyConverted = convert.ConvertFiles(AppFolders.DocParsedSec, AppFolders.DocParsedSecHTML);

            if (convert.ErrorMessage != string.Empty)
            {               
                MessageBox.Show(convert.ErrorMessage, "Unable to Convert Parsed Text to HTML", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return false;
            }

            return true;
        }

        private void cboAutoPop1_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboAutoPop1Col.Visible = false;
            lblAutoPop1Col.Visible = false;
            _AutoPop1.Source = string.Empty;

            if (cboAutoPop1.Text == string.Empty)
                return;

            foreach (DataRow row in _dsMatrixSettings.Tables[DocTypesFields.TableName].Rows)
            {
                if (cboAutoPop1.Text.Trim() == row[DocTypesFields.Item].ToString())
                {
                    if (row[CommonFields.Column].ToString() == "Any")
                    {
                        cboAutoPop1Col.Visible = true;
                        lblAutoPop1Col.Visible = true;
                    }
                    else
                    {
                        int selectedIndex = cboAutoPop1Col.FindString(row[CommonFields.Column].ToString());
                        cboAutoPop1Col.SelectedIndex = selectedIndex; // Added 06.22.2018
                    }

                    _AutoPop1.Source = row[DocTypesFields.Source].ToString(); // e.g. Analysis Results

                    return;

                }
            }
        }

        private void cboAutoPop2_SelectedIndexChanged(object sender, EventArgs e)
        {
            cboAutoPop2Col.Visible = false;
            lblAutoPop2Col.Visible = false;
            _AutoPop2.Source = string.Empty;

            if (cboAutoPop2.Text.Trim() == string.Empty)
                return;

            foreach (DataRow row in _dsMatrixSettings.Tables[DocTypesFields.TableName].Rows)
            {
                if (cboAutoPop2.Text == row[DocTypesFields.Item].ToString())
                {
                    if (row[CommonFields.Column].ToString() == "Any")
                    {
                        cboAutoPop2Col.Visible = true;
                        lblAutoPop2Col.Visible = true;
                    }
                    else
                    {
                        cboAutoPop2Col.FindString(row[CommonFields.Column].ToString());
                    }

                    _AutoPop2.Source = row[DocTypesFields.Source].ToString(); // e.g. Analysis Results
                }
            }
        }

        private void lblSelTempSumValue_TextChanged(object sender, EventArgs e)
        {
            txtbSelTempSummary.Text = lblSelTempSumValue.Text;
        }

        private void txtbSelTempSummary_TextChanged(object sender, EventArgs e)
        {
             txtbSelTempSummary.Text = lblSelTempSumValue.Text;
        }


    }
}
