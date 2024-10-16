using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;
using WorkgroupMgr;

using unvell.ReoGrid;
using unvell.ReoGrid.Events;
using unvell.ReoGrid.CellTypes;


namespace MatrixBuilder
{
    public partial class frmMatrixSB : MetroFramework.Forms.MetroForm
    {
        public frmMatrixSB(string MatrixPathFile_XLSX, MatrixSB MatrixSBMgr, string MatrixTemplateName, int StartDataRow)
        {
            InitializeComponent();

            
            _MatrixPathFile_XLSX = MatrixPathFile_XLSX;
            _MatrixSBMgr = MatrixSBMgr;
            _MatrixTemplateName = MatrixTemplateName;

            _StartDataRow = StartDataRow;

            _isNew = true;

         //   LoadData();
        }

        public frmMatrixSB(int CurrentSBUID, string MatrixPathFile_XLSX, MatrixSB MatrixSBMgr, string MatrixTemplateName, int StartDataRow)
        {
            InitializeComponent();

            _CurrentSBUID = CurrentSBUID;
            _MatrixPathFile_XLSX = MatrixPathFile_XLSX;
            _MatrixSBMgr = MatrixSBMgr;
            _MatrixTemplateName = MatrixTemplateName;

            _StartDataRow = StartDataRow;

            _isNew = false;          

           // LoadData();
        }

        private bool _isNew = true;

        private string _MatrixPathFile_XLSX = string.Empty;
        private MatrixSB _MatrixSBMgr;
        private string _MatrixTemplateName = string.Empty;

        private int _StartDataRow = 1;

        private Worksheet worksheet;

        private string _origSBName = string.Empty; // Only populated for Edit mode

        private string _NewSBFile_docx = string.Empty;
        public string NewSBFile_docx
        {
            get { return _NewSBFile_docx; }
        }

        private int _CurrentSBUID = -1;
        public int CurrentSBUID
        {
            get { return _CurrentSBUID; }
        }


        private int _NewSBUID;
        public int NewSBUID
        {
            get { return _NewSBUID; }
        }

        public void LoadData()
        {

            if (!File.Exists(_MatrixPathFile_XLSX))
            {
                string msg = string.Concat("Unable to find Matrix Excel file: ", _MatrixPathFile_XLSX);
                MessageBox.Show(msg, "Unable to Generate a Storyboard without a Matrix", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            this.reoGridControl1.Load(_MatrixPathFile_XLSX);

            worksheet = reoGridControl1.CurrentWorksheet;

            worksheet.SetScale(10 / 10f);

            LoadMatrixRows();

            string[] SBTemplateNames = _MatrixSBMgr.GetSBNames(_MatrixTemplateName);
            if (SBTemplateNames == null)
            {
                MessageBox.Show(_MatrixSBMgr.ErrorMessage, "Unable to Get Storyboard Templates", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            if (SBTemplateNames.Length == 0)
            {
                MessageBox.Show("Unable to find Storyboard Templates associated with the current Matrix. ", "Unable to Get Storyboard Templates", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();

                return;
            }

            this.cboSBTemplate.DataSource = SBTemplateNames;

            if (_isNew) // New
            {
                lblDefinition.Text = "Select the Matrix Row(s) you want to populate this new Storyboard.";

                _origSBName = _MatrixSBMgr.GetNextSBName();

                txtbSBName.Text = _origSBName;

                if (cboSBTemplate.Items.Count > 0)
                    cboSBTemplate.SelectedIndex = 0;
            }
            else // Edit Mode
            {
                lblDefinition.Text = "Any changes made to this Storyboard will be replaced once you click the 'Generate Storyboard' button.";

                txtbSBName.Enabled = false;

                DataRow row = _MatrixSBMgr.GetSBDataRow(_CurrentSBUID);

                if (row== null)
                {

                    MessageBox.Show(_MatrixSBMgr.ErrorMessage, "Unable to Get the Selected Storyboard Data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();

                    return;
                }

                txtbSBName.Text = row[MatrixSBFields.Name].ToString();
                string SBTemplate = row[MatrixSBFields.SBTemplateName].ToString();

                int indexSBTemplate = cboSBTemplate.FindStringExact(SBTemplate);
                cboSBTemplate.SelectedIndex = indexSBTemplate;

                txtbDescription.Text = SBTemplate = row[MatrixSBFields.Description].ToString();

                string[] selectedmatrixRows = _MatrixSBMgr.GetMatrixRows(_CurrentSBUID);

                foreach (string matrixRow in selectedmatrixRows)
                {
                    lstbMatrixRows.SetItemChecked(Convert.ToInt32(matrixRow) - _StartDataRow, true);
                }

                DisplaySelectedRows();

            }
                     
           
        }

        private void LoadMatrixRows()
        {
          
            for (int i = _StartDataRow; i < 2001; i++)
            {
                lstbMatrixRows.Items.Add(i.ToString());
            }

        }

        private bool Validate(string[] selectedRows)
        {
            if (selectedRows.Length == 0)
            {
                MessageBox.Show("Please select rows from the Matrix prior to generating a Storyboard.", "Matrix Rows Selections Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (_isNew)
            {
                string[] rowsAlreadyUsed = _MatrixSBMgr.GetRowsAlreadyUsed(selectedRows);
                if (rowsAlreadyUsed.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string r in rowsAlreadyUsed)
                    {
                        string[] rx = r.Split('|');
                        sb.AppendLine(string.Concat("Row: ", rx[0], "  Storyboard: ", rx[1]));
                    }

                    string msg = string.Concat("The following Matrix Row(s) have been used in other Storyboard(s):", Environment.NewLine, sb.ToString());

                    if (MessageBox.Show(msg, "Do you want to continue with the Storyboard Generation?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return false;
                }

                if (_MatrixSBMgr.SBExists(this.txtbSBName.Text.Trim()))
                {
                    MessageBox.Show("Please enter another Storyboard Name.", "Storyboard Name Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                string[] rowsAlreadyUsed = _MatrixSBMgr.GetRowsAlreadyUsed(selectedRows, _CurrentSBUID);
                if (rowsAlreadyUsed.Length > 0)
                {
                    StringBuilder sb = new StringBuilder();
                    foreach (string r in rowsAlreadyUsed)
                    {
                        string[] rx = r.Split('|');
                        sb.AppendLine(string.Concat("Row: ", rx[0], "  Storyboard: ", rx[1]));
                    }

                    string msg = string.Concat("The following Matrix Row(s) have been used in other Storyboard(s):", Environment.NewLine, sb.ToString());

                    if (MessageBox.Show(msg, "Do you want to continue with the Storyboard Re-Generation?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return false;
                }
            }

            // ToDo check for rows used in other Storyboards

            return true;
        }

        private bool GenerateSB(string[] selectedRows)
        {
            DataTable dtSBFields = _MatrixSBMgr.GetFieldsEmptyTable();
            string[] fields = _MatrixSBMgr.GetAvailableFields();

            string cellNotation = string.Empty;
            string cellValue = string.Empty;
           // string cellValueAdj = string.Empty;
            string FieldName  = string.Empty;

           
            DataRow dr = dtSBFields.NewRow();
            foreach (string column in _MatrixSBMgr.Columns)
            {
                cellValue = string.Empty;
                foreach (string selectedRow in selectedRows)
                {
                    cellNotation = string.Concat(column, selectedRow); // e.g. A1, C7 ...

                    var cell = worksheet.Cells[cellNotation];
                    if (cell != null)
                    {
                        if (cell.Data != null)
                        {
                            if (cellValue.Length == 0)
                            {
                                cellValue = cell.Data.ToString();
                            }
                            else
                            {
                                cellValue = string.Concat(cellValue, Environment.NewLine, Environment.NewLine, cell.Data.ToString());
                            }
                        }
                        else
                        {
                            cellValue = string.Concat(cellValue, "");
                        }
                    }

                    FieldName = getFieldName(fields, column); 
                    if (FieldName.Length > 0)
                    {
                        cellValue = CallValue_Adjust(cellValue); // Added 08.24.2019
                        dr[FieldName] = cellValue;
                    }
                }
            }

            dr["DocAnalyzer_ProjName"] = AppFolders.ProjectCurrent;

            dtSBFields.Rows.Add(dr);

            if (_isNew)
            {
                if (!_MatrixSBMgr.GenerateSB(dtSBFields, txtbSBName.Text, cboSBTemplate.Text, txtbDescription.Text, selectedRows, AppFolders.UserName))
                {
                    MessageBox.Show(_MatrixSBMgr.ErrorMessage, "An Error Occurred While Saving Storyboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                
                
                if (!_MatrixSBMgr.UpdateSB(_CurrentSBUID, txtbSBName.Text, dtSBFields, cboSBTemplate.Text, txtbDescription.Text, selectedRows, AppFolders.UserName))
                {
                    MessageBox.Show(_MatrixSBMgr.ErrorMessage, "An Error Occurred While Re-Generating the Storyboard", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            

            return true;
        }

        private string CallValue_Adjust(string cellValue) // Added 08.24.2019
        {
          //  string[] lines = cellValue.Split(new string[] { "\r\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            string[] lines = cellValue.Split(new string[] { "\r\n", "\r", "\n"}, StringSplitOptions.None);

            List<string> lstCellValues_Adj = new List<string>();
            foreach (string line in lines)
            {
                //lstCellValues_Adj.Add(string.Concat(Environment.NewLine, Environment.NewLine, line, Environment.NewLine, Environment.NewLine));
                lstCellValues_Adj.Add(string.Concat(line, "<w:br/>"));
                //<w:br/>
            }

            StringBuilder sb = new StringBuilder();
            foreach (string adjLine in lstCellValues_Adj)
            {
                sb.Append(adjLine);
            }

            return sb.ToString();
        }

        private string getFieldName(string[] fields, string column)
        {
            string xCol = string.Concat("_", column);
            foreach (string field in fields)
            {
                if (field.IndexOf(xCol) > 0)
                {
                    return field;
                }
            }

            return string.Empty;
        }



        private string[] GetSelectedMatrixRows()
        {
            List<string> selRows = new List<string>();

            for (int i = 0; i < lstbMatrixRows.CheckedItems.Count; i++)
            {
                selRows.Add(lstbMatrixRows.CheckedItems[i].ToString());
            }

            return selRows.ToArray();
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            string[] selectedRows = GetSelectedMatrixRows();

            if (!Validate(selectedRows))
                return;

            Cursor.Current = Cursors.WaitCursor;

            if (!GenerateSB(selectedRows))
            {
                Cursor.Current = Cursors.Default;
                return;
            }

            _NewSBFile_docx = _MatrixSBMgr.NewSBFile_docx;
            _NewSBUID = _MatrixSBMgr.NewSBUID;

            if (togOpenSB.Checked)
            {
                Process.Start(_NewSBFile_docx);
            }

            Cursor.Current = Cursors.Default; // Done

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        //private void worksheet_SelectionRangeChanged(object sender, RangeEventArgs args)
        //{
        //    MessageBox.Show("  changed: " + args.Range.ToAddress());
        //}

        //private void worksheet_CellMouseUp(object sender, RangeEventArgs args)
        //{

        //}

        private void mtrb_Scroll(object sender, ScrollEventArgs e)
        {
            worksheet.ZoomReset();
            worksheet.SetScale(mtrb.Value / 10f);

          //  worksheet.ScaleFactor = mtrb.Value / 10f;

            lblMatrixScale.Text = (worksheet.ScaleFactor * 100) + "%";

            this.Refresh();
          //  this.reoGridControl1.Refresh();
           
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            StartPickRange();
        }

        private void StartPickRange()
        {
           
            reoGridControl1.PickRange((inst, range) =>
            {
                // this delegate will be invoked after range picked by user
                //

                int startRowNo = range.StartPos.Row + 1;
                int endRowNo = range.EndRow + 1;
                int index = -1;

                if (startRowNo < _StartDataRow)
                    return false;

                for (int i = startRowNo; i < endRowNo + 1; i++)
                {
                    index = lstbMatrixRows.FindStringExact(i.ToString());

                    lstbMatrixRows.SetItemChecked(index, true);
                }

                int selIndex = startRowNo - _StartDataRow;
                if (selIndex < 0)
                    return true;

                lstbMatrixRows.SelectedIndex = selIndex;

                DisplaySelectedRows();

                return false;

            },

            // cursor when user picking range on spreadsheet
            Cursors.Hand);
        }

        private void DisplaySelectedRows()
        {
            string selectedRows = string.Empty;

            for (int i = 0; i < lstbMatrixRows.CheckedItems.Count; i++)
            {
                if (selectedRows == string.Empty)
                {
                    selectedRows = lstbMatrixRows.CheckedItems[i].ToString();
                }
                else
                {
                    selectedRows = string.Concat(selectedRows, ", ", lstbMatrixRows.CheckedItems[i].ToString());
                }
            }

            if (selectedRows == string.Empty)
            {
                selectedRows = " Selected Matrix Rows: None";
            }
            else
            {
                selectedRows = string.Concat(" Selected Matrix Rows: ", selectedRows);
            }

            txtNotices.ForeColor = Color.White;

            lblNotices.Text = selectedRows;
        }

        private void frmMatrixSB_Load(object sender, EventArgs e)
        {
           // worksheet.SetScale(10 / 10f);
           // worksheet.ScaleFactor = 10 / 10f; // 100% scale, Default
           // this.reoGridControl1.Refresh();
        }

        private void lstbMatrixRows_Click(object sender, EventArgs e)
        {
            

            //if (lstbMatrixRows.Text == string.Empty)
            //    return;

            //int rowNo = Convert.ToInt32(lstbMatrixRows.Text);

            //CellPosition pos = new CellPosition(rowNo, 1);           
            //worksheet.ScrollToCell(pos);
        }

        private void butPlus_Click(object sender, EventArgs e)
        {

            //worksheet.ZoomIn();
            worksheet.SetScale(2f);
        }

        private void lblNotices_TextChanged(object sender, EventArgs e)
        {
            txtNotices.Text = lblNotices.Text;
        }

        private void txtNotices_TextChanged(object sender, EventArgs e)
        {
            txtNotices.Text = lblNotices.Text;
        }

        private void lstbMatrixRows_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
        }

        private void lstbMatrixRows_MouseUp(object sender, MouseEventArgs e)
        {
            DisplaySelectedRows();
        }
    }
}
