using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkgroupMgr;
//using hOOt;
using System.Threading;
using System.Diagnostics;

using unvell.ReoGrid;
using unvell.ReoGrid.Events;
using unvell.ReoGrid.CellTypes;

using Atebion.Outlook;


//using unvell.ReoGrid.Drawing;

namespace MatrixBuilder
{
    public partial class ucMatrixBuild : UserControl
    {
        public ucMatrixBuild()
        {
            InitializeComponent();

            _worksheet = reoGridControl1.CurrentWorksheet;
            _worksheet.CellMouseEnter += Grid_CellMouseEnter;
            _worksheet.CellMouseDown += Grid_CellMouseDown;

            
           // worksheet.AutoFitColumnWidth(0, false); // Added 12.19.2017
        }

        private string _MatrixPath = string.Empty;
        private string _SBTemplatePath = string.Empty;
        private string _ProjectName = string.Empty;
        private string _ProjectRootFolder = string.Empty;
        private string _ListPath = string.Empty;
        private string _RefResPath = string.Empty;

        private string _MATRIX_FILE_XLSX = "MatrixTemp.xlsx";
        private string _MATRIX_DATA_FILE_XLSX = "MatrixTempData.xlsx";
        private string _MATRIX_FILE_HTML = "Matrix.html";

        private string _MatrixPathFile_XLSX = string.Empty;

        private string _MatrixTemplateName = string.Empty;

        private int _StartDataRow = 1;

        Atebion.Outlook.Email _EmailOutLook;

        private Modes _currentMode;
        private enum Modes
        {
            Matrix = 0,
            DocTypes = 1,
            List = 2,
            RefRes = 3,
            StoryBoard = 4,
            Summary = 5
        }

        private structDragDrop _dragDrop;
        private struct structDragDrop
        {
            public string sourceType;
            public string sourceName;
            public bool dragDropOccurred;
            public string cell;
            public string column;
            public string docTypeContentType;
            public string docTypeSource;
            public string sourceUID;
            public string text;
            public string content; // Added 12.11.2017
            public DateTime allocatedDateTime;
        }

        private Worksheet _worksheet;

        MatrixAllocations _AllocationMgr;
       // DataTable _dtAllocations;
        DataSet _dsAllocations;
        List<string> _lstCurrent;
        string CurrentSourceName = string.Empty;

        private DataTable _MetadataTable;

        private int GetDataStartingRow()
        {
            //MatrixTemplateFields

            if (_MetadataTable == null)
            {
                MessageBox.Show("Unable to get Matrix Metadata", "Unable to Get Data Start Row", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return 0;
            }

            if (_MetadataTable.Rows[0][MatrixTemplateFields.RowDataStarts] != null)
            {
                int y = (int)_MetadataTable.Rows[0][MatrixTemplateFields.RowDataStarts];

                return y;
            }

            return 0;
        }

        private string GetMatrixTemplateName()
        {
            if (_MetadataTable == null)
            {
                MessageBox.Show("Unable to get Matrix Metadata", "Unable to Get Metrix Template Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return string.Empty;
            }

            if (_MetadataTable.Rows[0][MatrixTemplateFields.Name] != null)
            {
                return _MetadataTable.Rows[0][MatrixTemplateFields.Name].ToString();                
            }

            return string.Empty;
        }

        public void LoadData(string matrixPath, string projectRootFolder, string projectName, string listPath, string refResPath, string sbTemplatePath)
        {
            _MatrixPath = matrixPath;
            _ProjectName = projectName;
            _ProjectRootFolder = projectRootFolder;
            _ListPath = listPath;
            _RefResPath = refResPath;
            _SBTemplatePath = sbTemplatePath;

            _currentMode = Modes.DocTypes;

            ucMatrixDocTypes21.LoadData(_MatrixPath, _ProjectRootFolder, _ProjectName);

            _MetadataTable = ucMatrixDocTypes21.MetadataTable;

            _StartDataRow = GetDataStartingRow();
            _MatrixTemplateName = GetMatrixTemplateName(); 

            _AllocationMgr = new MatrixAllocations(_MatrixPath);
            _dsAllocations = _AllocationMgr.GetAllocations();
            
            LoadMatrix();
            ucMatrixList1.LoadData(_MatrixPath, _ListPath);

            ucMatrixRefRes1.LoadData(_MatrixPath, _RefResPath);

            ucMatrixSB1.LoadData(_SBTemplatePath, _MatrixPath, _MatrixPathFile_XLSX, _MatrixTemplateName, _StartDataRow);   

            _worksheet = reoGridControl1.CurrentWorksheet;
            _worksheet.CellMouseEnter += Grid_CellMouseEnter;
            _worksheet.CellMouseDown += Grid_CellMouseDown;

            _worksheet.ScaleFactor = 10 / 10f; // 100% scale, Default

            _currentMode = Modes.DocTypes;

            ModeAdjustments();

            // Freeze top area to scrolling
            if (_StartDataRow != 0)
            {
                int freezeRow = _StartDataRow - 1;
                _worksheet.FreezeToCell(freezeRow, 0, FreezeArea.Top);
            }

            _EmailOutLook = new Email();
            if (_EmailOutLook.IsOutlookConnectable())
                butEmail.Visible = true;

           // worksheet.AutoFitColumnWidth(1, false); // Added 12.19.2017

           // Test_LoadMatrix();
        }

        //private void Test_LoadDropListBoolean
        //{
           
        //    //DropdownListCell dropdown = new DropdownListCell( "Apple", "Orange", "Banana", "Pear", "Pumpkin", "Cherry", "Coconut");
        //    //sheet["B2"] = dropdown;

        //}

        private bool LoadMatrix()
        {
            try
            {
                _MatrixPathFile_XLSX = Path.Combine(_MatrixPath, _MATRIX_FILE_XLSX);
                if (!File.Exists(_MatrixPathFile_XLSX))
                {
                    // ToDo Messagebox
                    return false;
                }

                this.reoGridControl1.Load(_MatrixPathFile_XLSX);

                string pathFile = Path.Combine(_MatrixPath, _MATRIX_DATA_FILE_XLSX);
                if (!File.Exists(pathFile))
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                string msg = string.Concat("Error: ", ex.Message);
                MessageBox.Show(msg, "Unable to Load Matrix Template", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

           // reoGridControl2.Load(pathFile);

            return true;
        }

        private void ModeAdjustments()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 
            ucMatrixDocTypes21.Visible = false;
            ucMatrixDocTypes21.Dock = DockStyle.None;
            ucMatrixList1.Visible = false;
            ucMatrixList1.Dock = DockStyle.None;
            ucMatrixRefRes1.Visible = false;
            ucMatrixRefRes1.Dock = DockStyle.None;
            ucMatrixSB1.Visible = false;
            ucMatrixSB1.Dock = DockStyle.None;

            butDocs.Highlight = false;
            butDocs.Refresh();

            butList.Highlight = false;
            butList.Refresh();

            butMatrix.Highlight = false;
            butMatrix.Refresh();

            butRefRes.Highlight = false;
            butRefRes.Refresh();

            butStoryboard.Highlight = false;
            butStoryboard.Refresh();

            //butRefRes
            //butStoryboard
            //butSummary

            switch (_currentMode)
            {
                case Modes.Matrix:


                    break;
                case Modes.DocTypes:
                    ucMatrixDocTypes21.Visible = true;
                    ucMatrixDocTypes21.Dock = DockStyle.Fill;

                    butDocs.Highlight = true;
                    butDocs.Refresh();
                    break;

                case Modes.List:
                    ucMatrixList1.Visible = true;
                    ucMatrixList1.Dock = DockStyle.Fill;

                    butList.Highlight = true;
                    butList.Refresh();
                    break;

                case Modes.RefRes:
                    ucMatrixRefRes1.Visible = true;
                    ucMatrixRefRes1.Dock = DockStyle.Fill;

                    butRefRes.Highlight = true;
                    butRefRes.Refresh();
                    break;

                case Modes.StoryBoard:
                    ucMatrixSB1.Visible = true;
                    ucMatrixSB1.Dock = DockStyle.Fill;

                    butStoryboard.Highlight = true;
                    butStoryboard.Refresh();
                    break;




            }


            Cursor.Current = Cursors.Default;
        }

 
        private void butPrintMatrix_Click(object sender, EventArgs e)
        {
            var session = reoGridControl1.Worksheets[0].CreatePrintSession();
           // var session = worksheet.CreatePrintSession();
            session.Print();
        }

        private void ucMatrixDocTypes21_MouseDown(object sender, MouseEventArgs e)
        {
            if (ucMatrixDocTypes21.TextAllocate == string.Empty)
                return;

            //_dragDrop.sourceName = ucMatrixDocTypes21.CurrentDocument;
            //_dragDrop.column = ucMatrixDocTypes21.Column; // e.g. A or B or C ...
            //_dragDrop.docTypeContentType = ucMatrixDocTypes21.ContentType; // e.g. "Number", "Caption", "No & Caption", "Text"
            //_dragDrop.docTypeSource = ucMatrixDocTypes21.Source; // Analysis Results or Deep Analysis
            //_dragDrop.sourceUID = ucMatrixDocTypes21.UID; // Parse Segment or S
            //_dragDrop.text = ucMatrixDocTypes21.TextAllocate;

            DragDropEffects dde1 = DoDragDrop(_dragDrop.text, DragDropEffects.All);

        }

 
        private int GetColNo(string col)
        {
            string[] columns = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z" };

            for (int i = 0; i < 26; i++)
            {
                if (col == columns[i])
                {
                    return i;
                }
            }

            return -1;
        }

        CellPosition _pos;

        private void Grid_CellMouseDown(object sender, CellMouseEventArgs e)
        {
            _pos = e.CellPosition;
        }

        private void Grid_CellMouseEnter(object sender, CellMouseEventArgs e)
        {
            _pos = e.CellPosition;


            if (_dragDrop.dragDropOccurred)
            {
                // Document Types
                if (_currentMode == Modes.DocTypes)
                {
                    if (ucMatrixDocTypes21.TextAllocate == string.Empty)
                        return;

                    _dragDrop.sourceType = MatrixAllocationsFields.SourceType_DocType;
                    _dragDrop.sourceName = ucMatrixDocTypes21.CurrentDocument;
                    _dragDrop.column = ucMatrixDocTypes21.Column; // e.g. A or B or C ...
                    _dragDrop.docTypeContentType = ucMatrixDocTypes21.ContentType; // e.g. "Number", "Caption", "No & Caption", "Text"
                    _dragDrop.docTypeSource = ucMatrixDocTypes21.Source; // Analysis Results or Deep Analysis
                    _dragDrop.sourceUID = ucMatrixDocTypes21.UID; // Parse Segment or S
                    _dragDrop.text = ucMatrixDocTypes21.TextAllocate;
                    _dragDrop.content = ucMatrixDocTypes21.Content; // Added 12.11.2017

                    if (_dragDrop.column != "Any")
                    {

                        int col = GetColNo(_dragDrop.column);
                        if (col == -1)
                            return;

                        _pos.Col = col;
                    }

                    if (_dragDrop.docTypeContentType != "Number")
                    {
                        if (_worksheet.Cells[_pos].Data == null)
                        {
                            _worksheet.Cells[_pos].Data = _dragDrop.text;
                        }
                        else
                        {
                            _worksheet.Cells[_pos].Data = string.Concat(_worksheet.Cells[_pos].Data, Environment.NewLine, Environment.NewLine, _dragDrop.text);
                        }
                    }
                    else
                    {
                        if (_worksheet.Cells[_pos].Data == null)
                        {
                            _worksheet.Cells[_pos].Data = _dragDrop.text;
                        }
                        else
                        {
                            _worksheet.Cells[_pos].Data = string.Concat(_worksheet.Cells[_pos].Data, ", ", _dragDrop.text);
                        }
                    }

                    _worksheet.Cells[_pos].Tag = _dragDrop.sourceUID;

                    _dragDrop.dragDropOccurred = false; // Reset to Default
                }
                else if (_currentMode == Modes.List)
                {
                    _dragDrop.sourceType = MatrixAllocationsFields.SourceType_List;
                    _dragDrop.sourceName = ucMatrixList1.CurrentList;
                    _dragDrop.column = ucMatrixList1.Column; // e.g. A or B or C ...
                    _dragDrop.docTypeContentType = string.Empty;
                    _dragDrop.docTypeSource = string.Empty;
                    _dragDrop.sourceUID = string.Empty;
                    _dragDrop.text = ucMatrixList1.TextAllocate;

                    int col = GetColNo(_dragDrop.column);
                    if (col == -1)
                        return;

                    _pos.Col = col;

                    _worksheet.Cells[_pos].Data = _dragDrop.text;              

                    _dragDrop.dragDropOccurred = false; // Reset to Default
                    
                }
                else if (_currentMode == Modes.RefRes)
                {
                    _dragDrop.sourceType = MatrixAllocationsFields.SourceType_RefRes;
                    _dragDrop.sourceName = ucMatrixRefRes1.CurrentRefRes; // ucMatrixList1.CurrentList;
                    _dragDrop.column = ucMatrixRefRes1.Column; // e.g. A or B or C ...
                    _dragDrop.docTypeContentType = string.Empty;
                    _dragDrop.docTypeSource = ucMatrixRefRes1.Source;
                    _dragDrop.sourceUID = string.Empty;
                    _dragDrop.text = ucMatrixRefRes1.TextAllocate;

                    int col = GetColNo(_dragDrop.column);
                    if (col == -1)
                        return;

                    _pos.Col = col;

                    _worksheet.Cells[_pos].Data = _dragDrop.text;

                    _dragDrop.dragDropOccurred = false; // Reset to Default
                }

                Cursor.Current = Cursors.WaitCursor; // Waiting 

                _dragDrop.cell = _pos.ToString(); //string.Concat(_pos.Col.ToString(), ",", _pos.Row.ToString());

                // Adjust Matrix row height -- Seems to NOT work!
                int rowIndex = _pos.Row;
                var rowHeader = _worksheet.RowHeaders[rowIndex];
                //rowHeader.IsAutoHeight = true;
                //rowHeader.Height = 10;
                //rowHeader.IsAutoHeight = true;
                rowHeader.FitHeightToCells(true);

                StoreAllocation();
                SaveAllocations();

                if (_currentMode == Modes.DocTypes)
                {
                    ucMatrixDocTypes21.ShowUnallocated();
                }

                _worksheet.FocusPos = new CellPosition(_pos.ToString());

                _worksheet.ScrollToCell(_pos.Row + 1, 1); // Changed 12.28.2017
                

                Cursor.Current = Cursors.Default;
            }

           // if (chkEnter.Checked) Log("cell mouse enter: " + e.CellPosition);
        }

        private bool SaveAllocations()
        {
            //DataSet ds = new DataSet();
            //ds.Tables.Add(_dtAllocations);

            if (!_AllocationMgr.SaveAllocations(_dsAllocations))
            {
               // MessageBox.Show(_AllocationMgr.e

                return false;
            }

            _AllocationMgr.SaveAllocationItems(_lstCurrent, _dragDrop.sourceName, _dragDrop.sourceType);

            string pathFile = Path.Combine(_MatrixPath, _MATRIX_FILE_XLSX);
            reoGridControl1.Save(pathFile);

            // Not needed - 01.23.2019
            //string htmlTemplate;
            //var worksheet = reoGridControl1.CurrentWorksheet;
            //using (MemoryStream ms = new MemoryStream(8192))
            //{
            //    try
            //    {
            //        worksheet.ExportAsHTML(ms);
            //        htmlTemplate = Encoding.Default.GetString(ms.ToArray());
            //    }
            //    catch
            //    {
                 
            //    }

            //}

            //pathFile = Path.Combine(_MatrixPath, _MATRIX_FILE_HTML);
            //Files.WriteStringToFile(htmlTemplate, pathFile);

           

            return true;
        }

        private void StoreAllocation()
        {
            if (_currentMode == Modes.DocTypes)
            {
                DataRow row = _dsAllocations.Tables[0].NewRow();

                row[MatrixAllocationsFields.UID] = DataFunctions.GetNewUID(_dsAllocations.Tables[0]);
                row[MatrixAllocationsFields.SourceType] = _dragDrop.sourceType;
                row[MatrixAllocationsFields.SourceName] = _dragDrop.sourceName;
                row[MatrixAllocationsFields.Column] = _dragDrop.column;
                row[MatrixAllocationsFields.Cell] = _dragDrop.cell;
                row[MatrixAllocationsFields.DocTypeContentType] = _dragDrop.docTypeContentType;
                row[MatrixAllocationsFields.DocTypeSource] = _dragDrop.docTypeSource;
                row[MatrixAllocationsFields.SourceUID] = _dragDrop.sourceUID;
                row[MatrixAllocationsFields.Text] = _dragDrop.text;

                DataColumnCollection columns = _dsAllocations.Tables[0].Columns;
                if (columns.Contains(MatrixAllocationsFields.Content))
                {
                    row[MatrixAllocationsFields.Content] = _dragDrop.content;
                }
                row[MatrixAllocationsFields.AllocatedBy] = AppFolders.UserName;
                row[MatrixAllocationsFields.AllocatedDateTime] = DateTime.Now;

                _dsAllocations.Tables[0].Rows.Add(row);

                string allocation = string.Concat(_dragDrop.sourceUID, "|", _dragDrop.cell);
                _lstCurrent.Add(allocation);

                ucMatrixDocTypes21.AllocatedQty = _lstCurrent.Count;

                

            }
            else if (_currentMode == Modes.RefRes)
            {
                DataRow row = _dsAllocations.Tables[0].NewRow();

                row[MatrixAllocationsFields.UID] = DataFunctions.GetNewUID(_dsAllocations.Tables[0]);
                row[MatrixAllocationsFields.SourceType] = _dragDrop.sourceType;
                row[MatrixAllocationsFields.SourceName] = _dragDrop.sourceName;
                row[MatrixAllocationsFields.Column] = _dragDrop.column;
                row[MatrixAllocationsFields.Cell] = _dragDrop.cell;
                row[MatrixAllocationsFields.DocTypeContentType] = _dragDrop.docTypeContentType;
                row[MatrixAllocationsFields.DocTypeSource] = _dragDrop.docTypeSource;
                row[MatrixAllocationsFields.SourceUID] = _dragDrop.sourceUID;
                row[MatrixAllocationsFields.Text] = _dragDrop.text;
                row[MatrixAllocationsFields.AllocatedBy] = AppFolders.UserName;
                row[MatrixAllocationsFields.AllocatedDateTime] = DateTime.Now;

                _dsAllocations.Tables[0].Rows.Add(row);
            }
            else if (_currentMode == Modes.List)
            {
                string filter = string.Concat(MatrixAllocationsFields.Cell, " = '", _dragDrop.cell, "' AND ", MatrixAllocationsFields.SourceName, " = '", _dragDrop.sourceName, "' AND ", MatrixAllocationsFields.SourceType, " = '", _dragDrop.sourceType, "'");
                DataRow[] rows = _dsAllocations.Tables[0].Select(filter);
                if (rows.Length > 0) // Value already exists from this source, then update record
                {
                    foreach (DataRow rowFound in rows)
                    {
                        rowFound[MatrixAllocationsFields.Text] = _dragDrop.text;

                    }
                    _dsAllocations.Tables[0].AcceptChanges();
                }
                else // Add new record
                {
                    DataRow row = _dsAllocations.Tables[0].NewRow();

                    row[MatrixAllocationsFields.UID] = DataFunctions.GetNewUID(_dsAllocations.Tables[0]);
                    row[MatrixAllocationsFields.SourceType] = _dragDrop.sourceType;
                    row[MatrixAllocationsFields.SourceName] = _dragDrop.sourceName;
                    row[MatrixAllocationsFields.Column] = _dragDrop.column;
                    row[MatrixAllocationsFields.Cell] = _dragDrop.cell;
                    row[MatrixAllocationsFields.DocTypeContentType] = _dragDrop.docTypeContentType;
                    row[MatrixAllocationsFields.DocTypeSource] = _dragDrop.docTypeSource;
                    row[MatrixAllocationsFields.SourceUID] = _dragDrop.sourceUID;
                    row[MatrixAllocationsFields.Text] = _dragDrop.text;
                    row[MatrixAllocationsFields.AllocatedBy] = AppFolders.UserName;
                    row[MatrixAllocationsFields.AllocatedDateTime] = DateTime.Now;

                    _dsAllocations.Tables[0].Rows.Add(row);

                }
            }

            //_dragDrop.sourceType = MatrixAllocationsFields.SourceType_DocType;
            //_dragDrop.sourceName = ucMatrixDocTypes21.CurrentDocument;
            //_dragDrop.column = ucMatrixDocTypes21.Column; // e.g. A or B or C ...
            //_dragDrop.docTypeContentType = ucMatrixDocTypes21.ContentType; // e.g. "Number", "Caption", "No & Caption", "Text"
            //_dragDrop.docTypeSource = ucMatrixDocTypes21.Source; // Analysis Results or Deep Analysis
            //_dragDrop.sourceUID = ucMatrixDocTypes21.UID; // Parse Segment or S
            //_dragDrop.text = ucMatrixDocTypes21.TextAllocate;
        }

        private void reoGridControl1_DragDrop(object sender, DragEventArgs e)
        {
            Point pt = new Point(e.X, e.Y);
            reoGridControl1.PointToClient(pt);

            _dragDrop.dragDropOccurred = true;
        }

        private void reoGridControl1_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.AllowedEffect;
        }

        private void reoGridControl1_DragOver(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.All;
        }

        private void butDocTypes_MouseDown(object sender, MouseEventArgs e)
        {
            if (ucMatrixDocTypes21.TextAllocate == string.Empty)
                return;

            _dragDrop.sourceName = ucMatrixDocTypes21.CurrentDocument;
            _dragDrop.column = ucMatrixDocTypes21.Column; // e.g. A or B or C ...
            _dragDrop.docTypeContentType = ucMatrixDocTypes21.ContentType; // e.g. "Number", "Caption", "No & Caption", "Text"
            _dragDrop.docTypeSource = ucMatrixDocTypes21.Source; // Analysis Results or Deep Analysis
            _dragDrop.sourceUID = ucMatrixDocTypes21.UID; // Parse Segment or S
            _dragDrop.text = ucMatrixDocTypes21.TextAllocate;

            DragDropEffects dde1 = DoDragDrop(_dragDrop.text, DragDropEffects.All);

        }

        private void ucMatrixDocTypes21_DocSelected()
        {
            if (_AllocationMgr == null)
                _AllocationMgr = new MatrixAllocations(_MatrixPath);

            string selectedDoc = ucMatrixDocTypes21.CurrentDocument;
            _lstCurrent = _AllocationMgr.GetAllocationItems(selectedDoc, MatrixAllocationsFields.SourceType_DocType);
            ucMatrixDocTypes21.AllocatedQty = _lstCurrent.Count;

        }

        private void butDeleteAllocatedItems_Click(object sender, EventArgs e)
        {
            const string REMOVE = "Remove";
            string cell = _worksheet.FocusPos.ToString();

     // --- Get Allocations for Delete Allocations window
            string filter = string.Concat(MatrixAllocationsFields.Cell, " = '", cell, "'");
            DataRow[] rows = _dsAllocations.Tables[0].Select(filter);

            DataTable dt = new DataTable(MatrixAllocationsFields.TableName);

            dt.Columns.Add(REMOVE, typeof(bool));
            dt.Columns.Add(MatrixAllocationsFields.UID, typeof(int));
            dt.Columns.Add(MatrixAllocationsFields.SourceType, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.SourceName, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.Text, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.AllocatedBy, typeof(string));
            dt.Columns.Add(MatrixAllocationsFields.AllocatedDateTime, typeof(DateTime));

            string sourceType = string.Empty;
            foreach (DataRow row in rows)
            {
                DataRow newRow = dt.NewRow();
                newRow["Remove"] = false;
                newRow[MatrixAllocationsFields.UID] = row[MatrixAllocationsFields.UID];
                if (row[MatrixAllocationsFields.SourceType].ToString() == "DocType")
                {
                    sourceType = "Documents";
                }
                else
                {
                    sourceType = row[MatrixAllocationsFields.SourceType].ToString();
                }

                newRow[MatrixAllocationsFields.SourceType] = sourceType;
                newRow[MatrixAllocationsFields.SourceName] = row[MatrixAllocationsFields.SourceName];
                newRow[MatrixAllocationsFields.Text] = row[MatrixAllocationsFields.Text];
                newRow[MatrixAllocationsFields.AllocatedBy] = row[MatrixAllocationsFields.AllocatedBy];
                newRow[MatrixAllocationsFields.AllocatedDateTime] = row[MatrixAllocationsFields.AllocatedDateTime];

                dt.Rows.Add(newRow);
            }

            frmMatrixDelAllocations frmDelAllocations = new frmMatrixDelAllocations(dt, cell);
            if (frmDelAllocations.ShowDialog() == DialogResult.OK)
            {
    // -- Remove Allocations selected by User --
                Cursor.Current = Cursors.WaitCursor; // Waiting 

                List<string> allcFileItems4Removal = new List<string>(); 
                string allcItem = string.Empty;

                DataTable dtRemoveAllocations = frmDelAllocations.RemoveAllocations;
                int uid = -1;
                foreach (DataRow rowAllocation in dtRemoveAllocations.Rows)
                {
                    if ((bool)rowAllocation[REMOVE] == true)
                    {
                        uid = (int)rowAllocation[MatrixAllocationsFields.UID];

                        filter = string.Concat(MatrixAllocationsFields.UID, " = ", uid.ToString());
                        DataRow[] drr = _dsAllocations.Tables[0].Select(filter);
                        if (drr.Length > 0)
                        {
                            allcItem = string.Concat(drr[0][MatrixAllocationsFields.SourceType].ToString(), "|",
                                drr[0][MatrixAllocationsFields.SourceName].ToString(), "|",
                                drr[0][MatrixAllocationsFields.SourceUID].ToString(), "|",
                                drr[0][MatrixAllocationsFields.Cell].ToString());

                            allcFileItems4Removal.Add(allcItem);
                            drr[0].Delete();
                        }    
                        _dsAllocations.Tables[0].AcceptChanges();
                    }
                }

                _AllocationMgr.SaveAllocations(_dsAllocations);

                allcFileItemsRemoval(allcFileItems4Removal);

                CellTransformation(cell); // Reload Matrix Cell with allocations not removed, if any

                // Save to Excel and HTML
                string pathFile = Path.Combine(_MatrixPath, _MATRIX_FILE_XLSX);
                reoGridControl1.Save(pathFile);

                string htmlTemplate;
               // var worksheet = reoGridControl1.CurrentWorksheet;
                using (MemoryStream ms = new MemoryStream(8192))
                {
                    _worksheet.ExportAsHTML(ms);
                    htmlTemplate = Encoding.Default.GetString(ms.ToArray());
                }

                pathFile = Path.Combine(_MatrixPath, _MATRIX_FILE_HTML);
                Files.WriteStringToFile(htmlTemplate, pathFile);

                if (allcFileItems4Removal.Count > 1)
                {
                    lblQtyAllocationsRemoved.Text = string.Concat(allcFileItems4Removal.Count.ToString(), " Allocations Removed");
                }
                else
                {
                    lblQtyAllocationsRemoved.Text = string.Concat(allcFileItems4Removal.Count.ToString(), " Allocation Removed");
                }

                // Adjust Matrix row height -- Seems to NOT work!
                int rowIndex = _worksheet.FocusPos.Row + 1;
                var rowHeader = _worksheet.RowHeaders[rowIndex];
                //rowHeader.IsAutoHeight = true;
                rowHeader.Height = 20;
                //rowHeader.IsAutoHeight = true;
                rowHeader.FitHeightToCells(true);


                lblQtyAllocationsRemoved.Visible = true;
                this.Refresh();
                System.Threading.Thread.Sleep(2500);
                lblQtyAllocationsRemoved.Visible = false;
                this.Refresh();


                Cursor.Current = Cursors.Default;
            }
        }

        private void allcFileItemsRemoval(List<string> allcFileItems4Removal)
        {
            allcFileItems4Removal = allcFileItems4Removal.OrderBy(q => q).ToList();
            string item = string.Empty;
            string[] itemDetails;

            List<string> lstAllocationItems = new List<string>();

            string lastSourceType = string.Empty;
            string lastSourceName = string.Empty;

            string sourceType = string.Empty;
            string sourceName = string.Empty;
            string sourceUID = string.Empty;
            string cell = string.Empty;

            string valueX = string.Empty;

            foreach (string xItem in allcFileItems4Removal)
            {
                itemDetails = xItem.Split('|');
                if (itemDetails.Length == 4)
                {
                    sourceType = itemDetails[0];
                    sourceName = itemDetails[1];
                    sourceUID = itemDetails[2];
                    cell = itemDetails[3];

                    if (sourceType != lastSourceType || sourceName == lastSourceName)
                    {
                      if (lastSourceType != string.Empty)
                      {
                          _AllocationMgr.SaveAllocationItems(lstAllocationItems, lastSourceName, lastSourceType);
                      }

                      lstAllocationItems = _AllocationMgr.GetAllocationItems(sourceName, sourceType);
                    }

                    valueX = string.Concat(sourceUID, "|", cell);
                    lstAllocationItems.RemoveAll(x => ((string)x) == valueX);

                    lastSourceType = sourceType;
                    lastSourceName = sourceName;
                }
            }

            if (lastSourceType != string.Empty)
            {
                _AllocationMgr.SaveAllocationItems(lstAllocationItems, lastSourceName, lastSourceType);
            }

            // Refresh allocation bar for DocType
            if (ucMatrixDocTypes21.Visible == true)
            {
                string selectedDoc = ucMatrixDocTypes21.CurrentDocument;
                _lstCurrent = _AllocationMgr.GetAllocationItems(selectedDoc, MatrixAllocationsFields.SourceType_DocType);
                ucMatrixDocTypes21.AllocatedQty = _lstCurrent.Count;
            }

        }

        private void CellTransformation(string cell)
        {
            StringBuilder sb = new StringBuilder();
            string filter = string.Concat(MatrixAllocationsFields.Cell, " = '", cell, "'");
            DataRow[] rows = _dsAllocations.Tables[0].Select(filter);

            int counter = 0;
            foreach (DataRow row in rows)
            {
                sb.AppendLine(row[MatrixAllocationsFields.Text].ToString());
                if (counter > 0)
                {
                    sb.AppendLine(" ");
                    sb.AppendLine(" ");
                }              
                counter++;
            }

            _worksheet.Cells[cell].Data = sb.ToString();
        }

        private void butCellInformation_Click(object sender, EventArgs e)
        {
            string cell = _worksheet.FocusPos.ToString(); 
            string filter = string.Concat(MatrixAllocationsFields.Cell, " = '", cell, "'");
            DataRow[] rows = _dsAllocations.Tables[0].Select(filter);

            // Test Code
            StringBuilder sb = new StringBuilder();
            string testValue = string.Empty;
            foreach (DataRow row in rows)
            {
                testValue = row[MatrixAllocationsFields.SourceType].ToString();
                if (testValue == "DocType")
                    testValue = "Documents";

                if (testValue == "RefRes")
                    testValue = "Reference Resources";

                testValue = string.Concat("Source:   ", testValue);
                
                sb.AppendLine(testValue);
                testValue = string.Concat("Source Name:   ", row[MatrixAllocationsFields.SourceName].ToString());
                sb.AppendLine(testValue);
                testValue = string.Concat("Allocated by:   ", row[MatrixAllocationsFields.AllocatedBy].ToString());
                sb.AppendLine(testValue);
                DateTime xdate = (DateTime)row[MatrixAllocationsFields.AllocatedDateTime];
                testValue = string.Concat("Allocated Date and Time:   ", xdate.ToString("F"));
                sb.AppendLine(testValue);
                sb.AppendLine("");


                if (_dsAllocations.Tables[0].Columns.Contains(MatrixAllocationsFields.Content))
                {
                    string content = row[MatrixAllocationsFields.Content].ToString();
                    testValue = row[MatrixAllocationsFields.Text].ToString();

                    if (content.Length < testValue.Length)
                        sb.AppendLine(testValue);
                    else
                        sb.AppendLine(content);
                }
                else
                {
                    testValue = row[MatrixAllocationsFields.Text].ToString();
                    sb.AppendLine(testValue);
                }

                sb.AppendLine("");
                sb.AppendLine("");
            }

            if (sb.ToString() == string.Empty)
                return;

            frmMartixCellCont frm = new frmMartixCellCont();
            frm.LoadData(sb.ToString(), "Allocation Information for " + cell);

            frm.ShowDialog(this);

          //  MessageBox.Show(sb.ToString(), "Allocation Information for " + cell, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void mtrb_Scroll(object sender, ScrollEventArgs e)
        {
            _worksheet.ScaleFactor = mtrb.Value / 10f;

            lblMatrixScale.Text = (_worksheet.ScaleFactor * 100) + "%";

            // Test code
            //if (lblMatrixScale.Text == "100%")
            //{
            //    MessageBox.Show("Test");
            //}
        }

        private void butMatrix_Click(object sender, EventArgs e)
        {
            ModeAdjustments();
        }

        private void butDocTypes_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.DocTypes;
            ModeAdjustments();
        }

        private void butList_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.List;
            ModeAdjustments();

            
        }

        private void butDocs_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.DocTypes;
            ModeAdjustments();
        }

        private void butRefRes_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.RefRes;
            ModeAdjustments();
        }

        private void butExportExcel_Click(object sender, EventArgs e)
        {
            SaveMatrix();

            string exportedMatrixFile = _AllocationMgr.GetExportMatrix_New();
            if (exportedMatrixFile == string.Empty)
            {
                MessageBox.Show(_AllocationMgr.ErrorMessage, "Unable to get Excel Export due to an Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           // string pathFile = Path.Combine(_MatrixPath, _MATRIX_FILE_XLSX);

            Process.Start(exportedMatrixFile);

        }

        private void ucMatrixBuild_Load(object sender, EventArgs e)
        {
           
        }

        private void ucMatrixBuild_VisibleChanged(object sender, EventArgs e)
        {
           // worksheet.FreezeToCell(6, 0, FreezeArea.Top);

            if (!this.Visible)
            {
                ucMatrixDocTypes21.Unload();
            }
        }

        private void butStoryboard_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.StoryBoard;
            ModeAdjustments();
        }

        private void ucMatrixSB1_RowSelected()
        {
            string sbSelectedMatrixRow = ucMatrixSB1.SelectedMatrixRow;
            if (sbSelectedMatrixRow == string.Empty)
                return;

            CellPosition pos = new CellPosition(Convert.ToInt32(sbSelectedMatrixRow) -1, 1);
            //worksheet.ScrollToCell(pos);

     //       reoGridControl1.CurrentWorksheet.ScrollToCell(pos);

            _worksheet.SelectRows(Convert.ToInt32(sbSelectedMatrixRow) - 1, 1);
         //   worksheet.GetCell(pos);
            //worksheet.ShowRows(Convert.ToInt32(sbSelectedMatrixRow) - 1, 1);

            
        }

        private void butSave_Click(object sender, EventArgs e)
        {
            SaveMatrix();
        }

        private bool SaveMatrix()
        {
            try
            {
                Cursor.Current = Cursors.WaitCursor; // Waiting 
                string pathFile = Path.Combine(_MatrixPath, _MATRIX_FILE_XLSX);
                reoGridControl1.Save(pathFile);

                string htmlTemplate;
                var worksheet = reoGridControl1.CurrentWorksheet;
                using (MemoryStream ms = new MemoryStream(8192))
                {
                    worksheet.ExportAsHTML(ms);
                    htmlTemplate = Encoding.Default.GetString(ms.ToArray());
                }

                pathFile = Path.Combine(_MatrixPath, _MATRIX_FILE_HTML);
                Files.WriteStringToFile(htmlTemplate, pathFile);

                Cursor.Current = Cursors.Default;
            }
            catch (Exception ex)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(string.Concat("An Error occurred while saving the Matrix.      Error: ", ex.Message), "Matrix Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void butExcelEdit_Click(object sender, EventArgs e)
        {
            SaveMatrix();

            string pathFile = Path.Combine(_MatrixPath, _MATRIX_FILE_XLSX);

            Process.Start(pathFile);

        }

        private void metroButton1_Click(object sender, EventArgs e)
        {
            try
            {
                this.reoGridControl1.Load(_MatrixPathFile_XLSX);

                _worksheet = reoGridControl1.CurrentWorksheet;
                _worksheet.CellMouseEnter += Grid_CellMouseEnter;
                _worksheet.CellMouseDown += Grid_CellMouseDown;

                _worksheet.ScaleFactor = mtrb.Value / 10f;

                // Freeze top area to scrolling
                if (_StartDataRow != 0)
                {
                    int freezeRow = _StartDataRow - 1;
                    _worksheet.FreezeToCell(freezeRow, 0, FreezeArea.Top);
                }
            }
            catch (Exception ex)
            {
                string msg = string.Concat("Make sure that you have closed the Matrix Excel file.", Environment.NewLine, Environment.NewLine, "Error: ", ex.Message);
                MessageBox.Show(msg, "Unable to Reload the Excel Matrix file", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void butEmail_Click(object sender, EventArgs e)
        {
            SaveMatrix();

            Cursor.Current = Cursors.WaitCursor; // Waiting 

            string exportedMatrixFile = _AllocationMgr.GetExportMatrix_New();
            if (exportedMatrixFile == string.Empty)
            {
                Cursor.Current = Cursors.Default;
                MessageBox.Show(_AllocationMgr.ErrorMessage, "Unable to get Excel Export due to an Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            List<string> sAttachments = new List<string>();

            sAttachments.Add(exportedMatrixFile);

            string file = Files.GetFileName(exportedMatrixFile);

            string subject = string.Concat("Matrix for ", _ProjectName);
            string body = string.Concat(Environment.NewLine, Environment.NewLine + "Please see the attached file: ", file);

            _EmailOutLook.OpenEmailWithAttachments(string.Empty, subject, body, sAttachments.ToArray());

            Cursor.Current = Cursors.Default;
        }

        private void butAddRow_Click(object sender, EventArgs e)
        {
            if (_AllocationMgr == null)
                return;

            int row = _worksheet.FocusPos.Row;

            if (_AllocationMgr.InsertNewMatrixRow(row + 1))
            {
                _worksheet.InsertRows(row, 1);

                SaveMatrix();

                Application.DoEvents();


                LoadData(_MatrixPath, _ProjectRootFolder, _ProjectName, _ListPath, _RefResPath, _SBTemplatePath);

                Application.DoEvents();

                _worksheet.ScaleFactor = mtrb.Value / 10f;

                reoGridControl1.Refresh();

                Application.DoEvents(); 
            }
            else
            {
                string msg = string.Concat("An error occured while trying to add a new Matrix Row.   Error: ", _AllocationMgr.ErrorMessage);
                MessageBox.Show(msg, "Unable to Insert a New Matrix Row", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveMatrix();

            lblQtyAllocationsRemoved.Text = string.Concat("Inserted new Matrix Row at ", (row + 1).ToString());
            lblQtyAllocationsRemoved.Visible = true;
            this.Refresh();
            System.Threading.Thread.Sleep(2500);
            lblQtyAllocationsRemoved.Visible = false;
            this.Refresh();

            _worksheet.FocusPos = new CellPosition(string.Concat("A", row.ToString()));
            _worksheet.ScrollToCell(row + 1, 1);
            reoGridControl1.Refresh();
        }

        private void butRemoveRow_Click(object sender, EventArgs e)
        {
            if (_AllocationMgr == null)
                return;

            int row = _worksheet.FocusPos.Row;

            if ((row +1) < _StartDataRow) // Fixed bug -- Added +1 since row is zero based
                return;


            string question = string.Concat("Are you sure that your want to Remove Matrix Row Number ", (row + 1).ToString(), " ?");
            if (MessageBox.Show(question, "Confirm Row Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return;
         
            if (_AllocationMgr.RemoveNewMatrixRow(row + 1))
            {
                _worksheet.DeleteRows(row, 1);

                SaveMatrix();

                Application.DoEvents();


                LoadData(_MatrixPath, _ProjectRootFolder, _ProjectName, _ListPath, _RefResPath, _SBTemplatePath);

                Application.DoEvents();

                _worksheet.ScaleFactor = mtrb.Value / 10f;

                reoGridControl1.Refresh();

                Application.DoEvents(); 
            }
            else
            {
                string msg = string.Concat("An error occured while trying to Removed the selected Matrix Row.   Error: ", _AllocationMgr.ErrorMessage);
                MessageBox.Show(msg, "Unable to Remove the Selected Matrix Row", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            SaveMatrix();

            lblQtyAllocationsRemoved.Text = string.Concat("Removed Matrix Row ", (row + 1).ToString());
            lblQtyAllocationsRemoved.Visible = true;
            this.Refresh();
            System.Threading.Thread.Sleep(2500);
            lblQtyAllocationsRemoved.Visible = false;
            this.Refresh();

            _worksheet.FocusPos = new CellPosition(string.Concat("A", row.ToString()));
            _worksheet.ScrollToCell(row + 1, 1);
            reoGridControl1.Refresh();

        }

  

      

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);


        //    var worksheet = reoGridControl1.CurrentWorksheet;

 
        //    // freeze to top
        //    worksheet.FreezeToCell(10, 0, FreezeArea.Top);

        //    worksheet[5, 5] = "frozen region";
        //    worksheet[12, 5] = "active region";
        //}

        //private void Test_LoadMatrix()
        //{

        //    this.reoGridControl1.Load(@"I:\Tom\Atebion\Business\Compliance Matrix\Compliance Matrix1a.xlsx");
        //}
    }
}
