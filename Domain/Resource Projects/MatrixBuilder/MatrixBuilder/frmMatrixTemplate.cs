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
    public partial class frmMatrixTemplate : MetroFramework.Forms.MetroForm
    {
        public frmMatrixTemplate(string docTypesPath, string listPath, string refResPath, string matrixPath, string matrixTempPath, string matrixTempPathTemplates)
        {
            InitializeComponent();

            _docTypesPath = docTypesPath;
            _listPath = listPath;
            _refResPath = refResPath;
            _matrixPath = matrixPath;
            _matrixTempPath = matrixTempPath;
            _matrixTempPathTemplates = matrixTempPathTemplates;

            _isNew = true;

            LoadData();
        }

        public frmMatrixTemplate(string docTypesPath, string listPath, string refResPath, string matrixPath, string matrixName, string matrixTempPath, string matrixTempPathTemplates)
        {
            InitializeComponent();

            _docTypesPath = docTypesPath;
            _listPath = listPath;
            _refResPath = refResPath;
            _matrixPath = matrixPath;
            _matrixName = matrixName;
            _matrixTempPath = matrixTempPath;
            _matrixTempPathTemplates = matrixTempPathTemplates;

            _isNew = false;

           // LoadData();
        }

        private string _docTypesPath = string.Empty;
        private string _listPath = string.Empty;
        private string _refResPath = string.Empty;
        private string _matrixPath = string.Empty;
        private string _matrixName = string.Empty;
        private string _matrixTempPath = string.Empty;
        private string _matrixTempPathTemplates = string.Empty;

        private DataSet _dsMatrixSettings;
         

        private bool _isNew = true;
        private bool _isNewExcelSelection = false;
        private bool _adjListCols = false;
        private bool _adjRefResCols = false;

        private string _SelectedExcelFile = string.Empty;
 
        private WorkgroupMgr.MatrixTemplate _matrixTemplate;

     //   bool _isLocal = true;

        private string _matrixTempName = string.Empty;

        private bool _useFixedTemplate = false;
        private string _fixedTemplatePathFile = string.Empty;

        private Modes _currentMode = Modes.Matrix;
        private enum Modes
        {
            Matrix = 0,
            DocTypes = 1,
            Lists = 2,
            RefResources = 3,
            Summary = 4

            //Other = 4,
            
        }

        private bool LoadData()
        {
        
            if (_isNew) // New
            {
                _matrixTemplate = new MatrixTemplate(_docTypesPath, _listPath, _refResPath, _matrixPath, _matrixTempPathTemplates);
                cboStartDataRow.DataSource = _matrixTemplate.RowNumbers;
            }
            else // Edit
            {
                _matrixTemplate = new MatrixTemplate(_docTypesPath, _listPath, _refResPath, _matrixPath, _matrixTempPathTemplates, _matrixName);
                _dsMatrixSettings = _matrixTemplate.GetSettings();
                if (_dsMatrixSettings == null)
                {
                    MessageBox.Show(_matrixTemplate.ErrorMessage, "Unable to Get Settings", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblDefinition.Text = _matrixTemplate.ErrorMessage;
                    lblDefinition.ForeColor = Color.Red;
                }
                else
                {
                    cboStartDataRow.DataSource = _matrixTemplate.RowNumbers;

                    LoadMatrix();
                }
            }
             

            LoadDocTypes();
            LoadLists();
            LoadRefRes();        

            AdjMode();
            return true;
        }

        private void LoadMatrix()
        {
            if (_dsMatrixSettings == null) //New
                return;

            txtbTemplate.Text = _dsMatrixSettings.Tables[MatrixTemplateFields.TableName].Rows[0][MatrixTemplateFields.OrgExcelTempFile].ToString();
            txtbTemplateName.Text = _matrixName;

            string rowNumber = _dsMatrixSettings.Tables[MatrixTemplateFields.TableName].Rows[0][MatrixTemplateFields.RowDataStarts].ToString();


            cboStartDataRow.SelectedIndex = cboStartDataRow.FindString(rowNumber);

            txtbDescription.Text = _dsMatrixSettings.Tables[MatrixTemplateFields.TableName].Rows[0][MatrixTemplateFields.Description].ToString();

            
            _SelectedExcelFile = _matrixTemplate.GetTemplatePathFile();
            if (_SelectedExcelFile == string.Empty)
            {
                MessageBox.Show(_matrixTemplate.ErrorMessage, "Unable to Load the Excel Template", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            else
            {
                this.reoGridControl1.Load(_SelectedExcelFile);
            }

            lblSummaryText.Text = _matrixTemplate.GetSummary(_matrixName);

            reoGridControl1.Visible = true;

            SideButtonsShow();

        }

        private void LoadDocTypes()
        {
            string[] docTypesNames = _matrixTemplate.GetDocTypesNames();

            cboDocTypes.Items.Clear();
            cboDocTypes.Items.Add(string.Empty);

            if (docTypesNames.Length == 0)
                return;

            foreach (string docTypeName in docTypesNames)
            {
                cboDocTypes.Items.Add(docTypeName);
            }

            if (!_isNew) //Edit mode
            {
                string selectedDocType = _dsMatrixSettings.Tables[MatrixTemplateFields.TableName].Rows[0][MatrixTemplateFields.DocTypesSelected].ToString();
                int index = cboDocTypes.Items.IndexOf(selectedDocType);
                cboDocTypes.SelectedIndex = index;

                //PopulateDocTypeItems();

                SideButtonsShow();

                // Document Types
                _currentMode = Modes.DocTypes;
                AdjMode();

                Application.DoEvents();

                DataTable dtDocTypes = _dsMatrixSettings.Tables[DocTypesFields.TableName];

                string item = string.Empty;
                string column = string.Empty;
                string source = string.Empty;
                string contentType = string.Empty;

                if (dtDocTypes.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dtDocTypes.Rows)
                    {
                        item = dataRow[DocTypesFields.Item].ToString();
                        column = dataRow[CommonFields.Column].ToString();
                        source = dataRow[DocTypesFields.Source].ToString();
                        contentType = dataRow[DocTypesFields.ContentType].ToString();

                        foreach (DataGridViewRow row in dgvDocTypes.Rows)
                        {
                            if (row.Cells[DocTypesFields.Item].Value.ToString().Equals(item))
                            {
                                row.Cells[CommonFields.Selected].Value = true;
                                row.Cells[CommonFields.Column].Value = column;
                                row.Cells[DocTypesFields.Source].Value = source;
                                row.Cells[DocTypesFields.ContentType].Value = contentType;
                            }
                        }

                    }


                }

            }
   

            
        }

        private void LoadLists()
        {
            DataTable dt;
 
            dt = _matrixTemplate.GetListNames();
            if (dt == null)
            {
                MessageBox.Show(_matrixTemplate.ErrorMessage, "Unable to Get List Names", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.Name = WorkgroupMgr.CommonFields.Column;
            column.DataSource = _matrixTemplate.ColumnOptions2;
            column.HeaderText = "Column";
            column.DataPropertyName = WorkgroupMgr.CommonFields.Column;

            dgvLists.DataSource = dt;
            dgvLists.Columns.Add(column);


            dgvLists.Columns[WorkgroupMgr.CommonFields.Name].ReadOnly = true;

            //Application.DoEvents();

            dgvLists.AllowUserToAddRows = false; // Remove blank last row

           
            if (!_isNew) //Edit mode
            {
                _currentMode = Modes.Lists;
                AdjMode();

                DataTable dt2 = _dsMatrixSettings.Tables[ListFields.TableName];

                string item = string.Empty;
                string column2 = string.Empty;

                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt2.Rows)
                    {
                        item = dataRow[CommonFields.Name].ToString();
                        column2 = dataRow[CommonFields.Column].ToString();

                        foreach (DataGridViewRow row in dgvLists.Rows)
                        {
                            if (row.Cells[CommonFields.Name].Value.ToString().Equals(item))
                            {
                                row.Cells[CommonFields.Selected].Value = true;
                                row.Cells[CommonFields.Column].Value = column2;
                            }
                        }

                    }

                }
            }
        }

        private void LoadRefRes()
        {
            DataTable dt;
 
            dt = _matrixTemplate.GetRefResNames();
            if (dt == null)
            {
                MessageBox.Show(_matrixTemplate.ErrorMessage, "Unable to Get Reference Resource Names", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.Name = WorkgroupMgr.CommonFields.Column;
            column.DataSource = _matrixTemplate.ColumnOptions2;
            column.HeaderText = "Column";
            column.DataPropertyName = WorkgroupMgr.CommonFields.Column;

            dgvRefRes.DataSource = dt;
            dgvRefRes.Columns.Add(column);


            dgvRefRes.Columns[WorkgroupMgr.CommonFields.Name].ReadOnly = true;

            //Application.DoEvents();

            dgvRefRes.AllowUserToAddRows = false; // Remove blank last row

            if (!_isNew) //Edit mode
            {
                _currentMode = Modes.RefResources;
                AdjMode();

                DataTable dt2 = _dsMatrixSettings.Tables[RefResFields.TableName];

                string item = string.Empty;
                string column2 = string.Empty;

                if (dt2.Rows.Count > 0)
                {
                    foreach (DataRow dataRow in dt2.Rows)
                    {
                        item = dataRow[CommonFields.Name].ToString();
                        column2 = dataRow[CommonFields.Column].ToString();

                        foreach (DataGridViewRow row in dgvRefRes.Rows)
                        {
                            if (row.Cells[CommonFields.Name].Value.ToString().Equals(item))
                            {
                                row.Cells[CommonFields.Selected].Value = true;
                                row.Cells[CommonFields.Column].Value = column2;
                            }
                        }

                    }

                }

                _currentMode = Modes.Matrix;
                AdjMode();
            }

        }


        private void AdjMode()
        {
            panMatrix.Visible = false;
            panMatrix.Dock = DockStyle.None;
            panDocTypes.Visible = false;
            panDocTypes.Dock = DockStyle.None;
            panLists.Visible = false;
            panLists.Dock = DockStyle.None;
            panRefRes.Visible = false;
            panRefRes.Dock = DockStyle.None;
            panOther.Visible = false;
            panOther.Dock = DockStyle.None;
            panSummary.Visible = false;
            panSummary.Dock = DockStyle.None;

            butDocTypes.Highlight = false;
            butDocTypes.Refresh();
            butList.Highlight = false;
            butList.Refresh();
            butMatrix.Highlight = false;
            butMatrix.Refresh();
            butOther.Highlight = false;
            butOther.Refresh();
            butRefRes.Highlight = false;
            butRefRes.Refresh();
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

                    adjDocTypeItemColumns();
                    break;

                case Modes.Lists:
                    butList.Highlight = true;
                    butList.Refresh();

       

                    panLists.Visible = true;
                    panLists.Dock = DockStyle.Fill;

                    if (dgvLists.Columns.Count > 2)
                    {
                        if (!_adjListCols)
                        {
                            dgvLists.Columns[CommonFields.Selected].Width = 20;
                            dgvLists.Columns[CommonFields.Column].Width = 30;
                            dgvLists.Columns[CommonFields.Name].Width = 400;

                            _adjListCols = true;
                        }

                    }
                    break;

                case Modes.RefResources:
                    butRefRes.Highlight = true;
                    butRefRes.Refresh();

                    panRefRes.Visible = true;

                    if (dgvRefRes.Columns.Count > 2)
                    {
                        if (!_adjRefResCols)
                        {
                            dgvRefRes.Columns[CommonFields.Selected].Width = 20;
                            dgvRefRes.Columns[CommonFields.Column].Width = 30;
                            dgvRefRes.Columns[CommonFields.Name].Width = 400;

                            _adjRefResCols = true;
                        }

                    }
                    panRefRes.Dock = DockStyle.Fill;
                    break;
                //case Modes.Other:
                //    butOther.Highlight = true;
                //    butOther.Refresh();

                //    panOther.Visible = true;
                //    panOther.Dock = DockStyle.Fill;

                //    break;

                case Modes.Summary:
                    butSummary.Highlight = true;
                    butSummary.Refresh();

                    panSummary.Visible = true;
                    panSummary.Dock = DockStyle.Fill;

                    break;
            }
        }

        

        public bool FindMultiUsedColumns()
        {
            bool foundMultiUsed = false;
             string[] ColumnNotations = {"A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            _sbAlerts.AppendLine("");
            _sbAlerts.AppendLine("**** Multiple Column Allocations ****");
            _sbAlerts.AppendLine("");
           
            for (int i = 0; i < 26; i++)
            {
                List<ColumnAllications> results = lstColumnAllications.FindAll(x => (x.ColumnUsed == ColumnNotations[i]));
                if (results.Count > 1)
                {
                    string column = string.Empty;
                    string msg = string.Empty;
                    int x = 0;
                    foreach (ColumnAllications foundItem in results)
                    {
                        if (x == 0)
                        {
                            column = foundItem.ColumnUsed;
                            msg = string.Concat("Column '", column, "' has multiple allocations from different sources. These sources are: ", Environment.NewLine, "   ", (x + 1).ToString(), ".  ", foundItem.ItemName);
                            SetItemName2RedFont(foundItem.ItemName);
                        }
                        else
                        {
                            msg = string.Concat(msg, Environment.NewLine, "   ", (x + 1).ToString(), ".  ", foundItem.ItemName);
                            SetItemName2RedFont(foundItem.ItemName);
                        }

                        x++;
                    }
                    _sbAlerts.AppendLine(msg);
                    _sbAlerts.AppendLine("");
                    foundMultiUsed = true;
                }
            }

            return foundMultiUsed;
        }

        private void SetItemName2RedFont(string ItemName)
        {
            if (ItemName.IndexOf('-') == -1)
                return;

            string[] parts = ItemName.Split('-');

            string source = parts[0].Trim();
            string item = parts[1].Trim();

            if (source == "Document Type")
            {
                foreach (DataGridViewRow row1 in dgvDocTypes.Rows)
                {
                    if (row1.Cells[2].Value != null)
                    {
                        if (row1.Cells[2].Value.ToString() == item)
                        {
                            row1.Cells[2].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                        }
                    }
                }
            }
            else if (source == "List")
            {
                foreach (DataGridViewRow row2 in dgvLists.Rows)
                {
                    if (row2.Cells[2].Value != null)
                    {
                        if (row2.Cells[2].Value.ToString() == item)
                        {
                            row2.Cells[2].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                        }
                    }
                }
            }
            else if (source == "Reference Resource")
            {            
                foreach (DataGridViewRow row3 in dgvRefRes.Rows)
                {
                    if (row3.Cells[2].Value != null)
                    {
                        if (row3.Cells[2].Value.ToString() == item)
                        {
                            row3.Cells[2].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                        }
                    }
                }
            }

  

        }

        private StringBuilder _sbAlerts = new StringBuilder();

        public bool ValidateAndSummary()
        {
            lstColumnAllications.Clear();

            lblExcelTempFile.ForeColor = Color.White;
            lblTemplateName.ForeColor = Color.White;
            lblStartingRowNo.ForeColor = Color.White;

            lblDocTypesSelect.ForeColor = Color.White;

            
            StringBuilder sbSummary = new StringBuilder();

            bool isOkay = true;

            sbSummary.AppendLine("Matrix Template Settings Summary");
            sbSummary.AppendLine("");
            sbSummary.AppendLine("**** Template ****");
            sbSummary.AppendLine("");

            _sbAlerts.AppendLine("Alerts");
            _sbAlerts.AppendLine("");
            _sbAlerts.AppendLine("**** Template ****");
            _sbAlerts.AppendLine("");

            // Excel Template File
            string value = txtbTemplate.Text.Trim();
            if (value.Length == 0)
            {
                sbSummary.AppendLine("Excel Template File: Missing!");
                _sbAlerts.AppendLine("Template - Excel Template File has Not been Selected.");
                lblExcelTempFile.ForeColor = Color.Red;
                isOkay = false;
            }
            else
            {
                sbSummary.AppendLine(string.Concat("Excel Template File: ", value));
            }

            // Template Name
            value = txtbTemplateName.Text.Trim();
            if (value.Length == 0)
            {
                sbSummary.AppendLine("Excel Template Name: Missing!");
                _sbAlerts.AppendLine("Template - Template Name has Not been defined");
                lblTemplateName.ForeColor = Color.Red;
                isOkay = false;
            }
            else
            {
                if (_isNew)
                {
                    if (_matrixTemplate.MatrixNameExists(value))
                    {
                        _sbAlerts.AppendLine("Template - Template Name already exists!");
                        lblTemplateName.ForeColor = Color.Red;
                        isOkay = false;
                    }
                }
                sbSummary.AppendLine(string.Concat("Template Name: ", value));
            }

            // Start Data Row
            value = cboStartDataRow.Text;
            if (value.Length == 0)
            {
                sbSummary.AppendLine("Start Data Row: Missing!");
                _sbAlerts.AppendLine("Template - Start Data Row has Not been Selected.");
                lblStartingRowNo.ForeColor = Color.Red;
                isOkay = false;
            }
            else
            {
                sbSummary.AppendLine(string.Concat("Start Data Row: ", value));
            }

            // Description
            value = txtbDescription.Text;
             if (value.Length == 0)
            {
                sbSummary.AppendLine("Template Description: Missing!");
                _sbAlerts.AppendLine("Template - Description has Not been entered.");
                lblMatrixDescription.ForeColor = Color.Red;
                isOkay = false;
            }
            else
            {
                sbSummary.AppendLine(string.Concat("Template Description: ", value));
            }

            
             
             string column = string.Empty;
             string item = string.Empty;
             string msg = string.Empty;

             // -> Document Types
             string source = string.Empty;
             string contentType = string.Empty;

             sbSummary.AppendLine("");
             sbSummary.AppendLine("**** Document Types ****");
             sbSummary.AppendLine("");

             _sbAlerts.AppendLine("");
             _sbAlerts.AppendLine("**** Document Types ****");
             _sbAlerts.AppendLine("");

             value = cboDocTypes.Text;
             if (value.Length == 0)
             {
                 sbSummary.AppendLine("Document Type: Not Selected!");
                 _sbAlerts.AppendLine("Document Type - has Not been selected.");
                 if (MessageBox.Show("You have Not selected a Document Type. Are you sure that you want to create a Matrix Template that does use Documents (parsed) Analyzed by the Document Analyzer?", "Create Matrix Template without a Document Type?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                 {
                     lblDocTypesSelect.ForeColor = Color.Red;
                     isOkay = false;
                 }
                 else
                 {      
                     msg = string.Concat(AppFolders.UserName, " has elected to create this Matrix Template without Documents (parsed) Analyzed by the Document Analyzer.");
                     sbSummary.AppendLine(msg);
                     sbSummary.AppendLine(msg);
                 }
             }
             else
             {
                 sbSummary.AppendLine(string.Concat("Document Type: ", value));
                 foreach (DataGridViewRow row in dgvDocTypes.Rows)
                 {
                     item = row.Cells[DocTypesFields.Item].Value.ToString();
                     row.Cells[DocTypesFields.Item].Style = new DataGridViewCellStyle { ForeColor = Color.Black }; // Reset to Black

                     if ((bool)row.Cells[CommonFields.Selected].Value == true) // If checked then check for selections
                     {
                         msg = string.Concat("Document Type - ", item, " has been selected.");
                         sbSummary.AppendLine(msg);

                         if (row.Cells[CommonFields.Column].Value == null)
                         {
                             msg = string.Concat("Document Type - ", item, " does Not have a Column selected.");
                             sbSummary.AppendLine(msg);
                             _sbAlerts.AppendLine(msg);
                          //   lblDocTypesSelect.ForeColor = Color.Red;
                             row.Cells[DocTypesFields.Item].Style = new DataGridViewCellStyle { ForeColor = Color.Red};
                             isOkay = false;
                         }
                         else
                         {

                             column = row.Cells[CommonFields.Column].Value.ToString();
                             sbSummary.AppendLine(string.Concat("Document Type - ", item, " Column: ", column));

                             if (column != "Any")
                             {
                                 lstColumnAllications.Add( new ColumnAllications(column, string.Concat("Document Type - ", item))); 
                             }
                         }


                         if (row.Cells[DocTypesFields.Source].Value == null)
                         {
                             msg = string.Concat("Document Type - ", item, " does Not have a Source selected.");
                             sbSummary.AppendLine(msg);
                             _sbAlerts.AppendLine(msg);
                             row.Cells[DocTypesFields.Item].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                             //lblDocTypesSelect.ForeColor = Color.Red;
                             isOkay = false;
                         }
                         else
                         {
                             source = row.Cells[DocTypesFields.Source].Value.ToString();
                             sbSummary.AppendLine(string.Concat("Document Type - ", item, " Source: ", source));
                         }


                         if (row.Cells[DocTypesFields.ContentType].Value == null)
                         {
                             msg = string.Concat("Document Type - ", item, " does Not have a Content Type selected.");
                             sbSummary.AppendLine(msg);
                             _sbAlerts.AppendLine(msg);
                             //lblDocTypesSelect.ForeColor = Color.Red;
                             row.Cells[DocTypesFields.Item].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                             isOkay = false;
                         }
                         else
                         {
                             contentType = row.Cells[DocTypesFields.ContentType].Value.ToString();
                             sbSummary.AppendLine(string.Concat("Document Type - ", item, " Content Type: ", source));
                         }
                   
                     }
                 }

             }

            // -> List
             sbSummary.AppendLine("");
             sbSummary.AppendLine("**** Lists ****");
             sbSummary.AppendLine("");

             _sbAlerts.AppendLine("");
             _sbAlerts.AppendLine("**** Lists ****");
             _sbAlerts.AppendLine("");

             if (dgvLists.RowCount > 0)
             {
                 foreach (DataGridViewRow row in dgvLists.Rows)
                 {
                     item = row.Cells[CommonFields.Name].Value.ToString();
                     row.Cells[CommonFields.Name].Style = new DataGridViewCellStyle { ForeColor = Color.Black }; // Reset to Black

                     if ((bool)row.Cells[CommonFields.Selected].Value == true) // If checked then check for selections
                     {
                         msg = string.Concat("List - ", item, " has been selected.");
                         sbSummary.AppendLine(msg);

                         if (row.Cells[CommonFields.Column].Value == null)
                         {
                             msg = string.Concat("List - ", item, " does Not have a Column selected.");
                             sbSummary.AppendLine(msg);
                             _sbAlerts.AppendLine(msg);
                             //   lblDocTypesSelect.ForeColor = Color.Red;
                             row.Cells[CommonFields.Name].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                             isOkay = false;
                         }
                         else
                         {
                             column = row.Cells[CommonFields.Column].Value.ToString();
                             sbSummary.AppendLine(string.Concat("List - ", item, " Column: ", column));

                             lstColumnAllications.Add(new ColumnAllications(column, string.Concat("List - ", item)));
                             
                         }

                     }
                 }
             }
  
            // Reference Resources

             sbSummary.AppendLine("");
             sbSummary.AppendLine("**** Reference Resources ****");
             sbSummary.AppendLine("");

             _sbAlerts.AppendLine("");
             _sbAlerts.AppendLine("**** Reference Resources ****");
             _sbAlerts.AppendLine("");

             if (dgvRefRes.RowCount > 0)
             {
                 foreach (DataGridViewRow row in dgvRefRes.Rows)
                 {
                     item = row.Cells[RefResFields.Name].Value.ToString();
                     row.Cells[RefResFields.Name].Style = new DataGridViewCellStyle { ForeColor = Color.Black }; // Reset to Black

                     if ((bool)row.Cells[CommonFields.Selected].Value == true) // If checked then check for selections
                     {
                         msg = string.Concat("Reference Resource - ", item, " has been selected.");
                         sbSummary.AppendLine(msg);


                         if (row.Cells[CommonFields.Column].Value == null)
                         {
                             msg = string.Concat("Reference Resource - ", item, " does Not have a Column selected.");
                             sbSummary.AppendLine(msg);
                             _sbAlerts.AppendLine(msg);
                             //   lblDocTypesSelect.ForeColor = Color.Red;
                             row.Cells[RefResFields.Name].Style = new DataGridViewCellStyle { ForeColor = Color.Red };
                             isOkay = false;
                         }
                         else
                         {
                             column = row.Cells[CommonFields.Column].Value.ToString();
                             sbSummary.AppendLine(string.Concat("Reference Resource - ", item, " Column: ", column));

                             lstColumnAllications.Add(new ColumnAllications(column, string.Concat("Reference Resource - ", item)));
                         }

                     }
                 }
             }

             if (FindMultiUsedColumns())
             {
                 isOkay = false;
             }
           
             this.lblAlertText.Text = _sbAlerts.ToString();
             this.lblSummaryText.Text = sbSummary.ToString();
    

             return isOkay;
        }

        private bool SaveSettings()
        {
  
            // Matrix 
            string matrixTemplateName = this.txtbTemplateName.Text.Trim();
            string matrixTemplateFile = this.txtbTemplate.Text.Trim();
            string matrixTemplateDescription = this.txtbDescription.Text.Trim();
            DataTable dtMatrix = _matrixTemplate.CreateTable_MatrixTemplate();
            DataRow rowMatrix = dtMatrix.NewRow();
            rowMatrix[MatrixTemplateFields.Name] = matrixTemplateName;
            rowMatrix[MatrixTemplateFields.Description] = matrixTemplateDescription;
            rowMatrix[MatrixTemplateFields.OrgExcelTempFile] = matrixTemplateFile;
            rowMatrix[MatrixTemplateFields.RowDataStarts] = this.cboStartDataRow.Text;
            rowMatrix[MatrixTemplateFields.DocTypesSelected] = this.cboDocTypes.Text;

            dtMatrix.Rows.Add(rowMatrix);


            // Document Types
            DataTable dtDocTypes = _matrixTemplate.CreateTable_DocTypes2();
            string item = string.Empty;
            string column = string.Empty;
            string source = string.Empty;
            string contentType = string.Empty;

            DataRow rowData;

            if (dgvDocTypes.Rows.Count > 0)
            {                 
                foreach (DataGridViewRow row in dgvDocTypes.Rows)
                {                       
                    if ((bool)row.Cells[CommonFields.Selected].Value == true) // If checked then check for selections
                    {
                        item = row.Cells[DocTypesFields.Item].Value.ToString();
                        column = row.Cells[CommonFields.Column].Value.ToString();
                        source = row.Cells[DocTypesFields.Source].Value.ToString();
                        contentType = row.Cells[DocTypesFields.ContentType].Value.ToString();

                        rowData = dtDocTypes.NewRow();
                        rowData[DocTypesFields.Item] = item;
                        rowData[CommonFields.Column] = column;
                        rowData[DocTypesFields.Source] = source;
                        rowData[DocTypesFields.ContentType] = contentType;

                        dtDocTypes.Rows.Add(rowData);

                    }
                }
            }

            // List
            DataTable dtLists = _matrixTemplate.CreateTable_Lists2();
            if (dgvLists.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvLists.Rows)
                {
                    if ((bool)row.Cells[CommonFields.Selected].Value == true) // If checked then check for selections
                    {
                        item = row.Cells[CommonFields.Name].Value.ToString();
                        column = row.Cells[CommonFields.Column].Value.ToString();

                        rowData = dtLists.NewRow();
                        rowData[CommonFields.Name] = item;
                        rowData[CommonFields.Column] = column;
        
                        dtLists.Rows.Add(rowData);
                    }
                } 
            }

            // Reference Resource
            DataTable dtRefRes = _matrixTemplate.CreateTable_RefRes2();           
            if (dgvRefRes.Rows.Count > 0)
            {
                foreach (DataGridViewRow row in dgvRefRes.Rows)
                {
                    if ((bool)row.Cells[CommonFields.Selected].Value == true) // If checked then check for selections
                    {
                        item = row.Cells[RefResFields.Name].Value.ToString();
                        column = row.Cells[CommonFields.Column].Value.ToString();

                        rowData = dtRefRes.NewRow();
                        rowData[RefResFields.Name] = item;
                        rowData[CommonFields.Column] = column;

                        dtRefRes.Rows.Add(rowData);
                    }
                }
            }

            DataSet dsMatrixTemplate = new DataSet();
            dsMatrixTemplate.Tables.Add(dtMatrix);
            dsMatrixTemplate.Tables.Add(dtDocTypes);
            dsMatrixTemplate.Tables.Add(dtLists);
            dsMatrixTemplate.Tables.Add(dtRefRes);

            //string htmlTemplate;
            //var worksheet = reoGridControl1.CurrentWorksheet;
            //using (MemoryStream ms = new MemoryStream(8192))
            //{
            //    worksheet.ExportAsHTML(ms);
            //    htmlTemplate = Encoding.Default.GetString(ms.ToArray());
            //}

            if (_useFixedTemplate)
            {
                matrixTemplateFile = _fixedTemplatePathFile;
                _isNewExcelSelection = true;
            }

            bool result = _matrixTemplate.SaveMatrixTemplate(matrixTemplateName, dsMatrixTemplate, matrixTemplateFile, _isNewExcelSelection, matrixTemplateDescription, txtbSummaryText.Text);

            if (!result)
            {
                string alertError = string.Concat(Environment.NewLine, Environment.NewLine, "**** Save Matrix Template Settings ****", Environment.NewLine, _matrixTemplate.ErrorMessage);
                txtAlerts.Text = string.Concat(txtAlerts.Text, alertError);
                MessageBox.Show(_matrixTemplate.ErrorMessage, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

            return result;
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

        private void butList_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.Lists;
            AdjMode();
        }

        private void butRefRes_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.RefResources;
            AdjMode();
        }

        private void butOther_Click(object sender, EventArgs e)
        {
            //_currentMode = Modes.Other;
            //AdjMode();
        }

        private void butSummary_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.Summary;
            AdjMode();
        }

        private void SideButtonsShow()
        {
            butDocTypes.Visible = true;
            butList.Visible = true;
            butRefRes.Visible = true;
          //  butOther.Visible = true;
            butSummary.Visible = true;
            butBack.Visible = true;
            butNext.Visible = true;
        }

        private void butLoadFile_Click(object sender, EventArgs e)
        {
            try
            {
                openFileDialog1.FileName = string.Empty;
                openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";
                openFileDialog1.Title = "Please select an Excel file for a Template";

                DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
                if (result == DialogResult.OK)
                {
                    Cursor.Current = Cursors.WaitCursor; // Waiting 

                    _SelectedExcelFile = openFileDialog1.FileName;

                    if (!CopySelectedExcelFile2Temp(_SelectedExcelFile))
                        return;

                    txtbTemplate.Text = _SelectedExcelFile;

                    this.reoGridControl1.Load(_SelectedExcelFile);

                    _isNewExcelSelection = true;

                    reoGridControl1.Visible = true;

                    SideButtonsShow();

                    Cursor.Current = Cursors.Default;
                }
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                string msg = string.Concat("If you have the selected Excel file open, then please close it.", Environment.NewLine, Environment.NewLine, "Error: ", ex.Message);
                MessageBox.Show(msg, "Unable to Select Excel File", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private bool CopySelectedExcelFile2Temp(string pathFile)
        {
            string file = Files.GetFileName(pathFile);
            string tempPathfile = Path.Combine(_matrixTempPath, file);

            try
            {
                if (File.Exists(tempPathfile))
                {
                    File.Delete(tempPathfile);
                }

                File.Copy(pathFile, tempPathfile);

                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Concat("Unable to copy your selected Excel file due to an Error. Error: ", ex.Message), "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


        }

        private void PopulateDocTypeItems()
        {
            lblDocTypeDescription.Text = string.Empty;

            if (cboDocTypes.Items.Count == 0)
                return;

            string docTypeSelected = cboDocTypes.Text;
            if (docTypeSelected == string.Empty)
            {
                dgvDocTypes.DataSource = null;
                dgvDocTypes.Visible = false;
                return;
            }



            dgvDocTypes.Visible = true;

            DataTable dt;
            //if (_isNew)
            //{
            dt = _matrixTemplate.GetDocTypeItems(docTypeSelected);
            if (dt == null)
            {
                if (_matrixTemplate.ErrorMessage.Length > 0)
                {
                    MessageBox.Show(_matrixTemplate.ErrorMessage, "Unable to Load Document Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    lblDefinition.Text = _matrixTemplate.ErrorMessage;
                    lblDefinition.ForeColor = Color.Red;
                }

                return;
            }


            //DataGridViewTextBoxColumn use = new DataGridViewTextBoxColumn();
            //use.HeaderText = "Use";
            //use.DataPropertyName = WorkgroupMgr.CommonFields.Selected;

            //DataGridViewTextBoxColumn item = new DataGridViewTextBoxColumn();
            //item.HeaderText = "Item";
            //item.DataPropertyName = WorkgroupMgr.DocTypesFields.Item;

            //DataGridViewTextBoxColumn description = new DataGridViewTextBoxColumn();
            //description.HeaderText = "Description";
            //description.DataPropertyName = WorkgroupMgr.DocTypesFields.Description;

            DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            column.Name = WorkgroupMgr.CommonFields.Column;
            column.DataSource = _matrixTemplate.ColumnOptions;
            column.HeaderText = "Column";
            column.DataPropertyName = WorkgroupMgr.CommonFields.Column;

            DataGridViewComboBoxColumn source = new DataGridViewComboBoxColumn();
            source.Name = WorkgroupMgr.DocTypesFields.Source;
            source.DataSource = _matrixTemplate.DocType_Source;
            source.HeaderText = "Source";
            source.DataPropertyName = WorkgroupMgr.DocTypesFields.Source;

            DataGridViewComboBoxColumn contentType = new DataGridViewComboBoxColumn();
            contentType.Name = WorkgroupMgr.DocTypesFields.ContentType;
            contentType.DataSource = _matrixTemplate.DocType_ContentType;
            contentType.HeaderText = "Content Type";
            contentType.DataPropertyName = WorkgroupMgr.DocTypesFields.ContentType;


            dgvDocTypes.DataSource = dt;
            dgvDocTypes.Columns.Add(column);
            dgvDocTypes.Columns.Add(source);
            dgvDocTypes.Columns.Add(contentType);


            //  dgvDocTypes.Columns.AddRange(column, source, contentType);
            //dgvDocTypes.Columns.AddRange(source);
            //dgvDocTypes.Columns.AddRange(contentType);

            dgvDocTypes.Columns[WorkgroupMgr.DocTypesFields.Item].ReadOnly = true;
            dgvDocTypes.Columns[WorkgroupMgr.DocTypesFields.Description].ReadOnly = true;

            //     dgvDocTypes.Columns[3].Visible = false; // identical to dropdown Use column

            dgvDocTypes.Visible = true;

            adjDocTypeItemColumns();
            //dgvDocTypes.Columns[0].Width = 20;
            //dgvDocTypes.Columns[1].Width = 60;
            //dgvDocTypes.Columns[2].Width = 80;
            //dgvDocTypes.Columns[3].Width = 30;
            //dgvDocTypes.Columns[4].Width = 80;
            //dgvDocTypes.Columns[5].Width = 80;

            dgvDocTypes.AllowUserToAddRows = false; // Remove blank last row
            //}
            //else
            //{

            //}

          //  dgvDocTypes.Visible = true;

            lblDocTypeDescription.Text = _matrixTemplate.GetDocTypeDescription(docTypeSelected);
        }

        private void adjDocTypeItemColumns()
        {
            if (_currentMode != Modes.DocTypes)
                return;

            if (dgvDocTypes.ColumnCount == 0)
                return;

            dgvDocTypes.Columns[0].Width = 20;
            dgvDocTypes.Columns[1].Width = 60;
            dgvDocTypes.Columns[2].Width = 80;
            dgvDocTypes.Columns[3].Width = 30;
            dgvDocTypes.Columns[4].Width = 80;
            dgvDocTypes.Columns[5].Width = 80;

            // Test
           /// dgvDocTypes.Rows[2].Cells[CommonFields.Column].Value = "C";
        }

        private void cboDocTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateDocTypeItems();

            //lblDocTypeDescription.Text = string.Empty;

            //if (cboDocTypes.Items.Count == 0)
            //    return;

            //string docTypeSelected = cboDocTypes.Text;
            //if (docTypeSelected == string.Empty)
            //{
            //    dgvDocTypes.DataSource = null;
            //    dgvDocTypes.Visible = false;
            //    return;
            //}

           

            //dgvDocTypes.Visible = true;

            //DataTable dt;
   
            //    dt =_matrixTemplate.GetDocTypeItems(docTypeSelected);
            //    if (dt == null)
            //    {
            //        if (_matrixTemplate.ErrorMessage.Length > 0)
            //        {
            //            MessageBox.Show(_matrixTemplate.ErrorMessage, "Unable to Load Document Type", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //            lblDefinition.Text = _matrixTemplate.ErrorMessage;
            //            lblDefinition.ForeColor = Color.Red;
            //        }

            //        return;
            //    }
  


            //    DataGridViewComboBoxColumn column = new DataGridViewComboBoxColumn();
            //    column.Name = WorkgroupMgr.CommonFields.Column;
            //    column.DataSource = _matrixTemplate.ColumnOptions;
            //    column.HeaderText = "Column";
            //    column.DataPropertyName = WorkgroupMgr.CommonFields.Column;

            //    DataGridViewComboBoxColumn source = new DataGridViewComboBoxColumn();
            //    source.Name = WorkgroupMgr.DocTypesFields.Source;
            //    source.DataSource = _matrixTemplate.DocType_Source;
            //    source.HeaderText = "Source";
            //    source.DataPropertyName = WorkgroupMgr.DocTypesFields.Source;

            //    DataGridViewComboBoxColumn contentType = new DataGridViewComboBoxColumn();
            //    contentType.Name = WorkgroupMgr.DocTypesFields.ContentType;
            //    contentType.DataSource = _matrixTemplate.DocType_ContentType;
            //    contentType.HeaderText = "Content Type";
            //    contentType.DataPropertyName = WorkgroupMgr.DocTypesFields.ContentType;


            //    dgvDocTypes.DataSource = dt;
            //    dgvDocTypes.Columns.Add(column);
            //    dgvDocTypes.Columns.Add(source);
            //    dgvDocTypes.Columns.Add(contentType);
                


            //    dgvDocTypes.Columns[WorkgroupMgr.DocTypesFields.Item].ReadOnly = true;
            //    dgvDocTypes.Columns[WorkgroupMgr.DocTypesFields.Description].ReadOnly = true;

  
            //    dgvDocTypes.Columns[0].Width = 20;
            //    dgvDocTypes.Columns[1].Width = 60;
            //    dgvDocTypes.Columns[2].Width = 80;
            //    dgvDocTypes.Columns[3].Width = 30;
            //    dgvDocTypes.Columns[4].Width = 80;
            //    dgvDocTypes.Columns[5].Width = 80;

            //    dgvDocTypes.AllowUserToAddRows = false; // Remove blank last row
      

            //dgvDocTypes.Visible = true;

            //lblDocTypeDescription.Text = _matrixTemplate.GetDocTypeDescription(docTypeSelected);

        }

        private void dgvDocTypes_SelectionChanged(object sender, EventArgs e)
        {
 
        }

        private void dgvDocTypes_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {

  

        }

        private void dgvDocTypes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Not working !!!

            //if (e.RowIndex == 0)
            //{
            //    bool isSelected = (bool)dgvDocTypes.CurrentCell.Value;
            //    if (isSelected)
            //    {
            //        dgvDocTypes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightYellow;

            //    }
            //    else
            //    {
            //        dgvDocTypes.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;

            //    }

            //    dgvDocTypes.Refresh();
            //}
        }

        private void lblAlertText_TextChanged(object sender, EventArgs e)
        {
            txtAlerts.Text = lblAlertText.Text;
        }

        private void txtAlerts_TextChanged(object sender, EventArgs e)
        {
            txtAlerts.Text = lblAlertText.Text;
        }

        private void lblSummaryText_TextChanged(object sender, EventArgs e)
        {
            txtbSummaryText.Text = lblSummaryText.Text;
        }

        private void txtbSummaryText_TextChanged(object sender, EventArgs e)
        {
            txtbSummaryText.Text = lblSummaryText.Text;
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            if (!ValidateAndSummary())
            {
                MessageBox.Show("This Matrix Template settings failed validation. See Alerts in the Summary for details and make the appropriate changes and try to Save again.", "Failed Validation!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                _currentMode = Modes.Summary;
                AdjMode();
                Cursor.Current = Cursors.Default; // Done
                return;
            }

            if (!SaveSettings())
            {
                _currentMode = Modes.Summary;
                AdjMode();
                Cursor.Current = Cursors.Default; // Done
                return;
            }


            Cursor.Current = Cursors.Default; // Done

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void txtAlerts_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }

        private void txtbSummaryText_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }

        private List<ColumnAllications> lstColumnAllications = new List<ColumnAllications>();

        public class ColumnAllications
        {
            public ColumnAllications(string columnUsed, string itemName)
            {
                this.ColumnUsed = columnUsed;
                this.ItemName = itemName;
            }

            public string ColumnUsed { set; get; }
            public string ItemName { set; get; }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
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
            _currentMode++;

            if ((int)_currentMode > 4)
                _currentMode = (Modes)4;

            _currentMode = (Modes)_currentMode;
            AdjMode();
        }

        private void frmMatrixTemplate_Load(object sender, EventArgs e)
        {
            if (!_isNew)
                LoadData();
        }

        private void butFixTemplate_Click(object sender, EventArgs e)
        {

            Cursor.Current = Cursors.WaitCursor; // Waiting 

            string tempPath = _matrixTemplate.GetMatrixTemporaryPath();
            if (tempPath == string.Empty)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(_matrixTemplate.ErrorMessage, "Unable Fix Template", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string prefix = "0_";
            string baseName = "FixTemplate.rgf";

            string fixedFile = string.Concat(prefix, baseName);
            string fixedPathFile = Path.Combine(tempPath, fixedFile);
            if (File.Exists(fixedPathFile))
            {
                for (int i = 1; i < 101; i++)
                {
                    prefix = string.Concat(i.ToString(), "_");
                    fixedFile = string.Concat(prefix, baseName);
                    fixedPathFile = Path.Combine(tempPath, fixedFile);
                    if (!File.Exists(fixedPathFile))
                    {
                        break;
                    }

                }
            }

            // rgf file
            reoGridControl1.CurrentWorksheet.SaveRGF(fixedPathFile);
            reoGridControl1.CurrentWorksheet.LoadRGF(fixedPathFile);

            string fileName = Files.GetFileNameWOExt(fixedPathFile);
            fixedFile = string.Concat(fileName, ".xlsx");
            fixedPathFile = Path.Combine(tempPath, fixedFile);
            reoGridControl1.Save(fixedPathFile);
            reoGridControl1.Load(fixedPathFile);

            _useFixedTemplate = true;
            _fixedTemplatePathFile = fixedPathFile;

            //reoGridControl1.CurrentWorksheet.SaveRGF(@"G:\Test2\Test.rgf");
            //reoGridControl1.CurrentWorksheet.LoadRGF(@"G:\Test2\Test.rgf");
            //reoGridControl1.Save(@"G:\Test2\Test.xlsx", unvell.ReoGrid.IO.FileFormat.Excel2007);
            //reoGridControl1.Load(@"G:\Test2\Test.xlsx");

            butFixTemplate.Visible = false;
            lblFixMessage.Text = "Click the 'Edit Template in Excel' to re-insert images and color backgrounds.";
            butEditFixedTempExcel.Visible = true;

            Cursor.Current = Cursors.Default;
        }

        private void panFixMatrix_VisibleChanged(object sender, EventArgs e)
        {
            if (panFixMatrix.Visible)
            {
                lblFixMessage.Text = "Click the 'Fix Template' to remove unsupported Cell and Sheet styles.";
            }
        }

        private void butEditFixedTempExcel_Click(object sender, EventArgs e)
        {
            Process.Start(_fixedTemplatePathFile);

            butEditFixedTempExcel.Visible = false;
            lblFixMessage.Text = "Click the 'Reload Edited Template' to re-load the edited template, after closing it in Excel.";
            butReloadEditedTemplate.Visible = true;
        }

        private void butReloadEditedTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                this.reoGridControl1.Load(_fixedTemplatePathFile);
            }
            catch (Exception ex)
            {
                string msg = string.Concat("Make sure that you have closed the Matrix Excel file.", Environment.NewLine, Environment.NewLine, "Error: ", ex.Message);
                MessageBox.Show(msg, "Unable to Reload the Excel Matrix file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            butReloadEditedTemplate.Visible = false;
            lblFixMessage.Text = "Click the 'Edit Template in Excel' to re-insert images and color backgrounds.";
            butEditFixedTempExcel.Visible = true;
        }

        private void rbNoMatrixLooksBad_CheckedChanged(object sender, EventArgs e)
        {
           panFixMatrix.Visible = rbNoMatrixLooksBad.Checked;
        }

    }
}
