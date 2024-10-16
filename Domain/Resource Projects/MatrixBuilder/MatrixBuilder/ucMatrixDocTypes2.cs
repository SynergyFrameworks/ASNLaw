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



namespace MatrixBuilder
{
    public partial class ucMatrixDocTypes2 : UserControl
    {
        public ucMatrixDocTypes2()
        {
            InitializeComponent();
        }

        // Declare delegate for when a project has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Document is selected")]
        public event ProcessHandler DocSelected;

        private MatrixDocTypes _MatrixDocTypes;
        private Projects _ProjectsMgr;

        private string _MatrixPath = string.Empty;
        private string _ProjectsRootFolder = string.Empty;
        private string _ProjectName = string.Empty;

        
        private string _DocParsedSecIndexPath = string.Empty;
        private string _DocParsedSecPath = string.Empty;

        private DataTable _dtDocsAssocations;
        private DataTable _dtParseResults;

        private Image _Notepad; 
        private Image _Blank; 

        private frmDocumentView _frmDocumentView;

      //  Hoot _hoot;

        private string _SelectedDoc = string.Empty;
        public string CurrentDocument
        {
            get { return _SelectedDoc; }
        }

        private string _Column = string.Empty;
        public string Column
        {
            get { return _Column; }
        }

        private string _ContentType = string.Empty;
        public string ContentType
        {
            get { return _ContentType; }
        }

        private string _Source = string.Empty;
        public string Source
        {
            get { return _Source; }
        }

        private string _UID = string.Empty;
        public string UID
        {
            get { return _UID; }
        }

        private string _TextAllocate = string.Empty; // Text for allocation to the Matrix
        public string TextAllocate
        {
            get { return _TextAllocate; }
        }

        private string _Content = string.Empty; // All Text for the allocation to the Matrix -- 
        public string Content
        {
            get { return _Content; }
        }

        private int _AllocatedQty = 0;
        public int AllocatedQty
        {
            get { return _AllocatedQty; }
            set 
            { 
                _AllocatedQty = value;
                if (_AllocatedQty > mpbarAllocated.Maximum) // Added 12.21.2017
                    _AllocatedQty = mpbarAllocated.Maximum;

                mpbarAllocated.Value = _AllocatedQty;
                lblAllocatedQty.Text = string.Concat(_AllocatedQty.ToString(), "/", mpbarAllocated.Maximum.ToString());
            }
        }

        private DataTable _MetadataTable;
        public DataTable MetadataTable
        {
            get { return _MetadataTable; }
        }

        private void ucMatrixDocTypes2_Load(object sender, EventArgs e)
        {

        }


        public void LoadData(string matrixPath, string projectsRootFolder, string projectName)
        {
            lblCaptionDescription.ForeColor = Color.White;
            lblCaptionDescription.Text = "These parsed segments/paragraphs and/or sentences (items) and their associated notes are from the Document Analyzer.  You can allocate these items by dragging-and-dropping them into the Matrix.";

            _ProjectName = projectName;
            _ProjectsRootFolder = projectsRootFolder;

            _MatrixPath = matrixPath;
            _MatrixDocTypes = new MatrixDocTypes(_MatrixPath);

            // check if validation was okay
            if (_MatrixDocTypes.ErrorMessage.Length > 0)
            {
                MessageBox.Show(_MatrixDocTypes.ErrorMessage, "Unable to Load Data for Document Types", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblCaptionDescription.Text = _MatrixDocTypes.ErrorMessage;
                lblCaptionDescription.ForeColor = Color.Red;
                return;
            }

            // Load Documents (associations to Doctypes) 
            _dtDocsAssocations = _MatrixDocTypes.GetDocsAssocationsTable();

            if (_dtDocsAssocations == null)
            {
                MessageBox.Show(_MatrixDocTypes.ErrorMessage, "Unable to Load Documents association with Document Types", MessageBoxButtons.OK, MessageBoxIcon.Error);
                lblCaptionDescription.Text = _MatrixDocTypes.ErrorMessage;
                lblCaptionDescription.ForeColor = Color.Red;
                return;
            }

            dvgDocTypeItems.DataSource = _dtDocsAssocations;

            dvgDocTypeItems.ColumnHeadersVisible = false;
            dvgDocTypeItems.Columns[2].Visible = false;
            dvgDocTypeItems.Columns[3].Visible = false;
            dvgDocTypeItems.Columns[4].Visible = false;

            // Project
            _ProjectsMgr = new Projects(_ProjectsRootFolder);

          //  ChkRunIndexer(); // Check Search Indexer

            _MetadataTable = _MatrixDocTypes.MetadataTable;

            ucSearchHoot1.LoadData();

            _Notepad = Image.FromFile(string.Concat(Application.StartupPath, @"\Notepad16x16.jpg"));
            _Blank = Image.FromFile(string.Concat(Application.StartupPath, @"\Blank16x16.jpg"));

            //Test_LoadDocTypes();
            //Test_LoadParseResults();
        }

        public void Unload()
        {
  
            if (_frmDocumentView != null)
            {
                //_frmDocumentView.Close();

                chkDocView.Checked = false;

                Application.DoEvents();
                _frmDocumentView = null;
            }

            GC.Collect();
            GC.WaitForPendingFinalizers();
            
        }

        //private void Test_LoadParseResults()
        //{
        //    string _ParsedFile = @"C:\Users\Tom\AppData\Local\Atebion DA\Projects\Demo\Docs\DEMO_RFP\Current\ParseSec\XML\ParseResults.xml";

        //    DataSet _dsParseResults = DataFunctions.LoadDatasetFromXml(_ParsedFile);

        //    dvgParsedResults.Columns.Clear(); // Added 10.08.2013


        //    DataView view;
        //    view = _dsParseResults.Tables[0].DefaultView;
        //    view.Sort = "SortOrder ASC";
        //    dvgParsedResults.DataSource = view;


        //    dvgParsedResults.Columns[0].Visible = false;
        //    dvgParsedResults.Columns[1].Visible = false;
        //    dvgParsedResults.Columns[2].Visible = false;
        //    dvgParsedResults.Columns[3].Visible = false;
        //    dvgParsedResults.Columns[4].Visible = false;
        //    dvgParsedResults.Columns[8].Visible = false;
        //    dvgParsedResults.Columns[9].Visible = false;
        //    dvgParsedResults.Columns[12].Visible = false;
        //    dvgParsedResults.Columns["FileName"].Visible = false;


        //    dvgParsedResults.Columns[5].Visible = false;
        //    dvgParsedResults.Columns[6].Visible = false;
        //    dvgParsedResults.Columns[7].Visible = false;

        //    dvgParsedResults.AllowUserToAddRows = false; // Remove blank last row


        //    DataGridViewCellStyle dgvColumnHeaderStyle = new DataGridViewCellStyle();
        //    dgvColumnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        //    dvgParsedResults.ColumnHeadersDefaultCellStyle = dgvColumnHeaderStyle;

        //    if (dvgParsedResults.Columns.Contains("Quality"))
        //    {
        //        dvgParsedResults.Columns["Quality"].DefaultCellStyle = dgvColumnHeaderStyle;
        //    }

        //    splitContLeft.Panel2Collapsed = true;
        //    splitContLeft.Panel2.Hide();
        //}


        private void Test_LoadDocTypes()
        {

            DataTable dt = new DataTable(DocTypesFields.TableName);

            //    dt.Columns.Add(DocTypesFields.UID, typeof(int));
            dt.Columns.Add(DocTypesFields.Item, typeof(string));
            dt.Columns.Add(DocTypesFields.Description, typeof(string));

            DataRow row = dt.NewRow();
            //    row[DocTypesFields.UID] = i;
            row[DocTypesFields.Item] = "C";
            row[DocTypesFields.Description] = "Description/Specifications/Work Statement - SOW: 1. Scope, 2. Reference Doc. 3. Requirements";

            dt.Rows.Add(row);

            row = dt.NewRow();

            row[DocTypesFields.Item] = "L";
            row[DocTypesFields.Description] = "Instructions, Conditions, and Notices to Offerors - Type of Contract, Solicitation Definitions, Prop Requirments, Progress Payments, etc";

            dt.Rows.Add(row);

            dvgDocTypeItems.DataSource = dt;
  
            dvgDocTypeItems.ColumnHeadersVisible = false;
        }

  


        private void lblNotes_TextChanged(object sender, EventArgs e)
        {
            //txtbNotes.Text = lblNotes.Text;
        }

        private void txtbNotes_TextChanged(object sender, EventArgs e)
        {
            txtbNotes.Text = rtfbNotes.Text; //lblNotes.Text;
        }

        private void dvgParsedResults_SelectionChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            _TextAllocate = string.Empty;

            DataGridViewRow row = dvgParsedResults.CurrentRow;

            if (row == null) // Check, sometimes data has not been loaded yet
            {
                Cursor.Current = Cursors.Default; // Back to normal
                return;
            }

            if (_Source == "Deep Analysis")
            {

                if (dvgParsedResults.Columns.Contains("FileName"))
                {
                    return;
                }

                string path = _ProjectsMgr.GetProjectDocDARPath_ParseSentences(_ProjectName, _SelectedDoc);
                if (path == string.Empty)
                {
                    MessageBox.Show(_ProjectsMgr.ErrorMessage, "Unable to Open the Parsed Sentence for the selected item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                _UID = row.Cells["UID"].Value.ToString();

                string file = string.Concat(_UID, ".rtf");

                string pathFile = Path.Combine(path, file);

                if (!File.Exists(pathFile))
                {
                    string msg = string.Concat("Unable to Find the Parsed Sentence file: ", pathFile);
                    MessageBox.Show(msg, "Parsed Sentence File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    richTextBox1.Text = string.Empty;
                    return;
                }

                richTextBox1.LoadFile(pathFile);

                GetDARNote(_UID);

                _Content = richTextBox1.Text; // Added 12.11.2017

                switch (_ContentType)
                {
                    case "Number":
                        _TextAllocate = row.Cells["Number"].Value.ToString();
                        break;
                    case "Caption":
                        _TextAllocate = row.Cells["Caption"].Value.ToString();
                        break;

                    case "No & Caption":
                        _TextAllocate = string.Concat(row.Cells["Number"].Value.ToString(), " ", row.Cells["Caption"].Value.ToString());
                        break;

                    case "Text":
                        if (chkbIncludeNoWithText.Checked) // Added 06.19.2018
                        {
                            if (richTextBox1.Text.IndexOf(row.Cells["Number"].Value.ToString()) == -1) // Added 06.19.2018
                                _TextAllocate = string.Concat(row.Cells["Number"].Value.ToString(), "  ", richTextBox1.Text);
                            else
                                _TextAllocate = richTextBox1.Text;
                        }
                        else
                            _TextAllocate = richTextBox1.Text;
                        break;


                }


            }
            else // Anlaysis Results
            {
                if (!dvgParsedResults.Columns.Contains("FileName"))
                {
                    return;
                }

                string path = _ProjectsMgr.GetProjectDocARPath_ParsedSections(_ProjectName, _SelectedDoc);
                if (path == string.Empty)
                {
                    MessageBox.Show(_ProjectsMgr.ErrorMessage, "Unable to Open the Parsed Segement/Paragraph for the selected item", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                string pathFile = Path.Combine(path, row.Cells["FileName"].Value.ToString());

                //ToDo -- Change after testing
                //   string pathFile = string.Concat(@"C:\Users\Tom\AppData\Local\Atebion DA\Projects\Demo\Docs\DEMO_RFP\Current\ParseSec\", row.Cells["FileName"].Value.ToString()); // ToDo: Fix Error -- Null --Combine: When clicking on the Cancel button 

                _UID = row.Cells["UID"].Value.ToString();

                if (_UID == string.Empty)
                {
                    richTextBox1.Text = string.Empty;
                    Cursor.Current = Cursors.Default; // Back to normal
                    return; // Most likely last row, which is empty
                }


                GetARNote(_UID);

                if (File.Exists(pathFile))
                {

                    if (Files.FileIsLocked(pathFile))
                    {
                        string msg = "The selected document is currently opened by another application. Please close this document file and try again.";
                        MessageBox.Show(msg, "Unable to Open this Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        richTextBox1.Text = msg;

                        return;

                    }

                    richTextBox1.LoadFile(pathFile);

                    switch (_ContentType)
                    {
                        case "Number":
                            _TextAllocate = row.Cells["Number"].Value.ToString();
                            break;
                        case "Caption":
                            _TextAllocate = row.Cells["Caption"].Value.ToString();
                            break;

                        case "No & Caption":
                            _TextAllocate = string.Concat(row.Cells["Number"].Value.ToString(), " ", row.Cells["Caption"].Value.ToString());
                            break;

                        case "Text":
                            if (chkbIncludeNoWithText.Checked) // Added 06.19.2018
                            {
                                if (richTextBox1.Text.IndexOf(row.Cells["Number"].Value.ToString()) == -1) // Added 06.19.2018
                                    _TextAllocate = string.Concat(row.Cells["Number"].Value.ToString(), "  ", richTextBox1.Text);
                                else
                                    _TextAllocate = richTextBox1.Text;
                            }
                            else
                                _TextAllocate = richTextBox1.Text;
                            break;


                    }

                    _Content = richTextBox1.Text;
                    //_TextAllocate
                }
                else
                {
                    richTextBox1.Text = string.Empty;
                    richTextBox1.Text = string.Concat("Error: Cannot find file: ", pathFile);

                }
            }

            if (dvgParsedResults.Columns.Contains("LineStart"))
            {
                dvgParsedResults.Columns["LineStart"].Visible = false;
            }

            if (dvgParsedResults.Columns.Contains("Page"))
            {
                dvgParsedResults.Columns["Page"].Width = 40;
                dvgParsedResults.Columns["Page"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            }


            if (dvgParsedResults.Columns.Contains("Notes")) // Added for bug fix 11.18.2017 -- Bug found by Joseph W. Mancy <Joseph.Mancy@elbitsystems-us.com>  
            {
                dvgParsedResults.Columns["Notes"].Width = 20;
            }

            if (this.chkDocView.Checked)
            {
                if (_frmDocumentView == null)
                {
                    this.chkDocView.Checked = false;
                    return;
                }

                if (!CheckOpened("Document View"))
                {
                    this.chkDocView.Checked = false;
                    return;
                }

                int line = 0;
               // if (row.Cells["LineStart"].Value != null)
                if (row.Cells["LineStart"].Value.ToString() != string.Empty) // Changed on 12.19.2018
                {
                    line = Convert.ToInt32(row.Cells["LineStart"].Value);
                }
                line++; // parser engine is zero based, while document viewer is one base. Therefore add one.


                _frmDocumentView.ShowSegment(line, richTextBox1.Text);
            }
        }

        private bool CheckOpened(string name)
        {
            FormCollection fc = Application.OpenForms;

            foreach (Form frm in fc)
            {
                if (frm.Text == name)
                {
                    return true;
                }
            }
            return false;
        }


        private void SetTextAllocate()
        {
            DataGridViewRow row = dvgParsedResults.CurrentRow;

            switch (_ContentType)
            {
                case "Number":
                    _TextAllocate = row.Cells["Number"].Value.ToString();
                    break;
                case "Caption":
                    _TextAllocate = row.Cells["Caption"].Value.ToString();
                    break;

                case "No & Caption":
                    _TextAllocate = string.Concat(row.Cells["Number"].Value.ToString(), " ", row.Cells["Caption"].Value.ToString());
                    break;

                case "Text":
                    if (chkbIncludeNoWithText.Checked) // Added 06.19.2018
                    {
                        if (richTextBox1.Text.IndexOf(row.Cells["Number"].Value.ToString()) == -1) // Added 06.19.2018
                            _TextAllocate = string.Concat(row.Cells["Number"].Value.ToString(), "  ", richTextBox1.Text);
                        else
                            _TextAllocate = richTextBox1.Text;
                    }
                    else
                        _TextAllocate = richTextBox1.Text;
                    break;

            }

        }

        //public void ChkRunIndexer()
        //{
        //    if (_DocParsedSecIndexPath == string.Empty)
        //        return;
           
        //    DateTime dtIndex = Files.GetLatestFileDatetime(_DocParsedSecIndexPath);
        //    DateTime dtParsedFiles = Files.GetLatestFileDatetime(_DocParsedSecPath);


        //    try
        //    {
        //        _hoot = new Hoot(_DocParsedSecIndexPath, "index", true);

        //        if (dtIndex == null)
        //        {
        //            RunIndexer();
        //        }
        //        else
        //        {
        //            int result = DateTime.Compare(dtIndex, dtParsedFiles);
        //            if (result <= 0) // "is earlier than"
        //                RunIndexer();
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //       // lblInformation.Text = string.Concat("Search Loading Error: ", ex.Message);
        //    }
        //}

        //private void RunIndexer()
        //{
        //    Cursor.Current = Cursors.WaitCursor; // Waiting

        //    if (_hoot == null)
        //        _hoot = new Hoot(_DocParsedSecIndexPath, "index", true);

        //    string[] files = Directory.GetFiles(_DocParsedSecPath, "*", SearchOption.TopDirectoryOnly);


        //    //  backgroundWorker1.RunWorkerAsync(files);

        //    //string[] files = e.Argument as string[];
        //    //BackgroundWorker wrk = sender as BackgroundWorker;
        //    int i = 0;
        //    foreach (string fn in files)
        //    {
        //        //if (wrk.CancellationPending)
        //        //{
        //        //    e.Cancel = true;
        //        //    break;
        //        //}
        //        backgroundWorker1.ReportProgress(1, fn);
        //        try
        //        {
        //            if (_hoot.IsIndexed(fn) == false)
        //            {
        //                TextReader tf = new EPocalipse.IFilter.FilterReader(fn);
        //                string s = "";
        //                if (tf != null)
        //                    s = tf.ReadToEnd();

        //                _hoot.Index(new Document(new FileInfo(fn), s), true);
        //            }
        //        }
        //        catch { }
        //        i++;
        //        if (i > 1000)
        //        {
        //            i = 0;
        //            _hoot.Save();
        //        }
        //    }
        //    _hoot.Save();
        //    _hoot.OptimizeIndex();

        //    Cursor.Current = Cursors.Default; // Back to normal
        //}

        private bool GetDARNote(string uid)
        {
            string path = _ProjectsMgr.GetProjectDocDARPath_Notes(_ProjectName, _SelectedDoc);
            if (path == string.Empty)
            {
                MessageBox.Show(_ProjectsMgr.ErrorMessage, "Parsed Sentence Notes Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            string file = string.Concat(uid, ".rtf");

            string pathFile = Path.Combine(path, file);

            if (File.Exists(pathFile))
            {
                splitContLeft.Panel2Collapsed = false;
                splitContLeft.Panel2.Show();
                rtfbNotes.LoadFile(pathFile);

                return true;
            }
            else
            {
                rtfbNotes.Text = string.Empty;
                splitContLeft.Panel2Collapsed = true;
                splitContLeft.Panel2.Hide();

                return false;
            }


        }

        private bool GetARNote(string uid)
        {
            string filePattern = string.Concat(uid, "_1.rtf");


            string pathFile = Path.Combine(_ProjectsRootFolder, _ProjectName, "Docs", _SelectedDoc, "Current", "ParseSec", "Notes", filePattern);

          //  string[] pathFile = Directory.GetFiles(path);

            //if (pathFile.Length == 0)
            //{
            //    rtfbNotes.Text = string.Empty;
            //    splitContLeft.Panel2Collapsed = true;
            //    splitContLeft.Panel2.Hide();

            //    return false;
            //}

            if (File.Exists(pathFile))
            {
                splitContLeft.Panel2Collapsed = false;
                splitContLeft.Panel2.Show();
                rtfbNotes.LoadFile(pathFile);

                return true;
            }
            else
            {
                rtfbNotes.Text = string.Empty;
                splitContLeft.Panel2Collapsed = true;
                splitContLeft.Panel2.Hide();

                return false;
            }

        }

        private void dvgDocTypeItems_SelectionChanged(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            DataGridViewRow row = dvgDocTypeItems.CurrentRow;

            if (row == null) // Check, sometimes data has not been loaded yet
            {
                return;
            }

            butUnallocated.Highlight = false;
            butUnallocated.Refresh();

            ShowUnfiltered();

            if (_ProjectsMgr == null)
            {
                _ProjectsMgr = new Projects(_ProjectsRootFolder);
            }

            _SelectedDoc = row.Cells[MatricesFields.ProjectDocumentName].Value.ToString();

            _Column = row.Cells[MatricesFields.Column].Value.ToString();
            lblColumn.Text = string.Concat("Column: ", _Column);

            string contentType = row.Cells[MatricesFields.DocTypeContentType].Value.ToString();
            _ContentType = contentType;
            if (contentType == "No & Caption")
                contentType = "No && Caption";

            if (contentType == "Text") // Added 06.19.2018
                chkbIncludeNoWithText.Visible = true;
            else
                chkbIncludeNoWithText.Visible = false;

            lblContentType.Text = string.Concat("Content Type: ", contentType);

            _Source = row.Cells[MatricesFields.DocTypeSource].Value.ToString();
            lblSource.Text = string.Concat("Source: ", _Source);

            // ToDo - Add if statement to support Deep Analysis
            if (_Source == "Deep Analysis")
            { 
                if (!_ProjectsMgr.DeepAnalysis_Exists(_ProjectName, _SelectedDoc))
                {
                    string msg = string.Concat("The Deep Analysis has Not been run for document: ", _SelectedDoc, Environment.NewLine, Environment.NewLine, "Please run the Deep Analysis in the Document Analyzer.");
                    MessageBox.Show(msg, "No Deep Analysis Results", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    dvgParsedResults.Visible = false;
                    richTextBox1.Visible = false;

                    rtfbNotes.Text = string.Empty;
                    splitContLeft.Panel2Collapsed = true;
                    splitContLeft.Panel2.Hide();

                    return;
                }

                dvgParsedResults.Visible = true;
                richTextBox1.Visible = true;

                LoadDeepAnalysis();
            }
            else // Analysis Results
            {              
                
                if (!_ProjectsMgr.AnalysisResults_Exists(_ProjectName, _SelectedDoc))
                {
                    string msg = string.Concat("The Analyze has Not been run for document: ",_SelectedDoc, Environment.NewLine, Environment.NewLine, "Please run the Analyze in the Document Analyzer.");
                    MessageBox.Show(msg, "No Analysis Results", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                    dvgParsedResults.Visible = false;
                    richTextBox1.Visible = false;

                    rtfbNotes.Text = string.Empty;
                    splitContLeft.Panel2Collapsed = true;
                    splitContLeft.Panel2.Hide();
                }


                dvgParsedResults.Visible = true;
                richTextBox1.Visible = true;

                LoadParseResults();
            }

            mpbarAllocated.Maximum = dvgParsedResults.RowCount;

            // Document View
            if (chkDocView.Checked && _frmDocumentView != null)
            {
                string file = string.Concat(_SelectedDoc, ".txt");
                string currentDocPath = Path.Combine(_ProjectsRootFolder, _ProjectName, "Docs", _SelectedDoc, "Current", "ParseSec");
                string docParsedSecKeywords = Path.Combine(currentDocPath, "Keywords");
                string pathFile = Path.Combine(currentDocPath, file);
                string fileRTF = string.Concat(_SelectedDoc, ".rtf");
                string keywordsPathFile = Path.Combine(docParsedSecKeywords, fileRTF);
                if (File.Exists(keywordsPathFile))
                {
                   // _frmDocumentView = new frmDocumentView();
                    _frmDocumentView.LoadData(keywordsPathFile, docParsedSecKeywords, _SelectedDoc);

                }
                else
                {
                    if (!File.Exists(pathFile))
                    {
                        string msg = string.Concat("Unable to find file: ", pathFile);
                        MessageBox.Show(msg, "Unable to Display Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                   // _frmDocumentView = new frmDocumentView();
                    _frmDocumentView.LoadData(pathFile, docParsedSecKeywords, _SelectedDoc);
                }

              //  _frmDocumentView.Show();

                // Show current segment
                if (dvgParsedResults.Columns.Contains("LineStart"))
                {
                    DataGridViewRow row2 = dvgParsedResults.CurrentRow;
                    if (row2 == null)
                        return;

                    int line = 0;
                  //  if (row2.Cells["LineStart"].Value != null)
                    if (row.Cells["LineStart"].Value.ToString() != string.Empty) // Changed on 12.19.2018
                    {
                        line = Convert.ToInt32(row2.Cells["LineStart"].Value);
                    }
                    line++; // parser engine is zero based, while document viewer is one base. Therefore add one.

                    _frmDocumentView.ShowSegment(line, richTextBox1.Text);
                }

            }
            else
            {
                if (_frmDocumentView != null)
                {
                    _frmDocumentView.Close();
                    _frmDocumentView = null;
                }
            }
            // --> End Document View



            if (DocSelected != null)
                DocSelected();

            Cursor.Current = Cursors.Default; 
        }

        private void InsertNotesColumn()
        {
            // Notes
            if (dvgParsedResults.Columns.Contains("Notes"))
            {
                dvgParsedResults.Columns.Remove("Notes");
            }
            //if (!dvgParsedResults.Columns.Contains("Notes"))
            //{
                DataGridViewImageColumn imageCol = new DataGridViewImageColumn();

                imageCol.HeaderText = "";
                imageCol.Name = "Notes";

                int colCount = dvgParsedResults.Columns.Count;

                dvgParsedResults.Columns.Insert(colCount, imageCol);
                // dvgParsedResults.Columns.Add(imageCol);

                dvgParsedResults.Columns["Notes"].Visible = true;

                dvgParsedResults.Columns["Notes"].Width = 20;
            //}
            //  
        }

        private void rtfbNotes_TextChanged(object sender, EventArgs e)
        {
            txtbNotes.Text = rtfbNotes.Text;
        }

        private void LoadDeepAnalysis()
        {
            _dtParseResults = _ProjectsMgr.GetDeepAnalysisResults(_ProjectName, _SelectedDoc);

            if (_dtParseResults == null)
            {
                MessageBox.Show(_ProjectsMgr.ErrorMessage, "Unable to Load Deep Analysis Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dvgParsedResults.DataSource = null;
                richTextBox1.Text = string.Empty;
                return;
            }

            DataView view;
            view = _dtParseResults.DefaultView;
            view.Sort = "SortOrder ASC";
            
            dvgParsedResults.DataSource = view;

            dvgParsedResults.Columns["UID"].Visible = false; // Col. index 0
            dvgParsedResults.Columns["SortOrder"].Visible = false; // Col. index 5
            dvgParsedResults.Columns["Sentence"].Visible = false;

            ucSearchHoot1.Visible = false;
            ucSearchDAHoot1.Visible = false;
            ucSearchLucene1.Visible = false;

            _DocParsedSecIndexPath = _ProjectsMgr.GetProjectDocDARPath_Index(_ProjectName, _SelectedDoc);
            if (_DocParsedSecIndexPath == string.Empty)
            {
                MessageBox.Show(_ProjectsMgr.ErrorMessage, "Unable to Use Deep Analysis Search", MessageBoxButtons.OK, MessageBoxIcon.Error);

                return;
            }

            // 07.31.2019 - check for Error.txt file -- If Error.txt is found use Hoot search engine
            string pathErrorFile = Path.Combine(_DocParsedSecIndexPath, "Error.txt");
            if (File.Exists(pathErrorFile))
            {
                _DocParsedSecIndexPath = _ProjectsMgr.GetProjectDocDARPath_HootIndex(_ProjectName, _SelectedDoc);
                _DocParsedSecPath = _ProjectsMgr.GetProjectDocDARPath_ParseSentences(_ProjectName, _SelectedDoc);

                ucSearchDAHoot1.IndexPath = _DocParsedSecIndexPath;
                ucSearchDAHoot1.DocParsedSecPath = _DocParsedSecPath;

                ucSearchDAHoot1.Visible = true;

            } //
            else
            {
                ucSearchLucene1.IndexPath = _DocParsedSecIndexPath;

                ucSearchLucene1.Visible = true;
            }

            dvgParsedResults.AllowUserToAddRows = false; // Remove blank last row

            InsertNotesColumn();

        }

        private void LoadParseResults()
        {
            _dtParseResults = _ProjectsMgr.GetAnalysisResults(_ProjectName, _SelectedDoc);
            if (_dtParseResults == null)
            {
                MessageBox.Show(_ProjectsMgr.ErrorMessage, "Unable to Load Analysis Results", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dvgParsedResults.DataSource = null;
                richTextBox1.Text = string.Empty;
                return;
            }

            DataView view;
            view = _dtParseResults.DefaultView;
            view.Sort = "SortOrder ASC";
            dvgParsedResults.DataSource = view;

            Application.DoEvents();

          //  dvgParsedResults.DataSource = view;

            dvgParsedResults.Columns[0].Visible = false;
            dvgParsedResults.Columns[1].Visible = false;
            dvgParsedResults.Columns[2].Visible = false;
            dvgParsedResults.Columns[3].Visible = false;
            dvgParsedResults.Columns[4].Visible = false;
            dvgParsedResults.Columns[8].Visible = false;
            dvgParsedResults.Columns[9].Visible = false;
            dvgParsedResults.Columns[12].Visible = false;
            dvgParsedResults.Columns["FileName"].Visible = false;
            dvgParsedResults.Columns["IndexEnd"].Visible = false;

            dvgParsedResults.Columns["Caption"].Visible = true;
            dvgParsedResults.Columns[5].Visible = false;
            dvgParsedResults.Columns[6].Visible = false;
            dvgParsedResults.Columns[7].Visible = false;

            if (dvgParsedResults.Columns.Contains("LineStart"))
            {
                dvgParsedResults.Columns["LineStart"].Visible = false;
            }

 

            dvgParsedResults.AllowUserToAddRows = false; // Remove blank last row


            DataGridViewCellStyle dgvColumnHeaderStyle = new DataGridViewCellStyle();
            dgvColumnHeaderStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dvgParsedResults.ColumnHeadersDefaultCellStyle = dgvColumnHeaderStyle;

            _DocParsedSecPath = Path.Combine(_ProjectsRootFolder, _ProjectName, "Docs", _SelectedDoc, "Current", "ParseSec");

            // Search Index
            ucSearchLucene1.Visible = false;
            ucSearchHoot1.Visible = false;
            ucSearchDAHoot1.Visible = false;

            _DocParsedSecIndexPath = Path.Combine(_DocParsedSecPath, "Index");

            if (Directory.Exists(_DocParsedSecIndexPath)) // Added If statements 03.30.2020
            {
                ucSearchHoot1.DocParsedSecPath = _DocParsedSecPath;
                ucSearchHoot1.IndexPath = _DocParsedSecIndexPath;
                ucSearchHoot1.LoadData();
                ucSearchHoot1.Visible = true;
            }
            else
            {
                _DocParsedSecIndexPath = Path.Combine(_DocParsedSecPath, "Index2");
                if (Directory.Exists(_DocParsedSecIndexPath))
                {
                    ucSearchLucene1.IndexPath = _DocParsedSecIndexPath;
                    ucSearchLucene1.Visible = true;
                }
            }
           

            dvgParsedResults.AllowUserToAddRows = false; // Remove blank last row

            InsertNotesColumn();
            
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            //lblFound.Text = string.Empty;

            //if (_Source == "Deep Analysis") // Search
            //{

            //}
            //else // Analysis Results Search -- hoot
            //{
            //    try
            //    {
            //        //  ChkRunIndexer(); // Added 07.12.2014 -- Re-Index every time

            //        if (_hoot == null)
            //        {
            //            _hoot = new Hoot(_DocParsedSecIndexPath, "index", true);
   
            //        }


            //        List<string> lstFound = new List<string>();

            //        string fileName = string.Empty;
            //        foreach (var d in _hoot.FindDocumentFileNames(txtbSearch.Text))
            //        {
            //            fileName = Files.GetFileNameWOExt(d);
            //            if (DataFunctions.IsNumeric(fileName))
            //                lstFound.Add(fileName);
            //            // listBox1.Items.Add(fileName);
            //        }

            //        lblFound.Text = string.Concat("Found: ", lstFound.Count.ToString());

            //        if (lstFound.Count == 0)
            //            return;

  

                    //string[] UIDResults = lstFound.ToArray(); //ucSearch1.FoundResults;

                    

                //    string filter = string.Empty;
                //    int i = 0;
                //    foreach (string s in UIDResults)
                //    {
                //        if (i == 0)
                //            filter = s;
                //        else
                //            filter = string.Concat(filter, ", ", s);

                //        i++;
                //    }

                //    filter = string.Concat("UID IN (", filter, ")");


                //    DataView dv = new DataView(_dtParseResults);

                //    dv.RowFilter = filter;
                //    dv.Sort = "SortOrder ASC";

                //    this.dvgParsedResults.DataSource = dv;

                //    butRefresh.Visible = true;
   
                //}
                //catch (Exception ex)
                //{
                //    // lblInformation.Text = string.Concat("Error: ", ex.Message);
                //}
            //}
        }

        private void butRefresh_Click(object sender, EventArgs e)
        {
            if (ucSearchLucene1.Visible)
            {
                LoadDeepAnalysis();
                ucSearchLucene1.Clear();
            }
            else if (ucSearchHoot1.Visible)
            {
                LoadParseResults();
                ucSearchHoot1.Clear();
            }
            else if (ucSearchDAHoot1.Visible) // Added 07.31.2019
            {
                LoadDeepAnalysis();
                ucSearchDAHoot1.Clear();
            }
 

            butRefresh.Visible = false;
            ShowUnfiltered();
        }

        //private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        //{
        //    string[] files = e.Argument as string[];
        //    BackgroundWorker wrk = sender as BackgroundWorker;
        //    int i = 0;
        //    foreach (string fn in files)
        //    {
        //        if (wrk.CancellationPending)
        //        {
        //            e.Cancel = true;
        //            break;
        //        }
        //        backgroundWorker1.ReportProgress(1, fn);
        //        try
        //        {
        //            if (_hoot.IsIndexed(fn) == false)
        //            {
        //                TextReader tf = new EPocalipse.IFilter.FilterReader(fn);
        //                string s = "";
        //                if (tf != null)
        //                    s = tf.ReadToEnd();

        //                _hoot.Index(new Document(new FileInfo(fn), s), true);
        //            }
        //        }
        //        catch { }
        //        i++;
        //        if (i > 1000)
        //        {
        //            i = 0;
        //            _hoot.Save();
        //        }
        //    }
        //    _hoot.Save();
        //    _hoot.OptimizeIndex();
        //}

        private void dvgParsedResults_MouseDown(object sender, MouseEventArgs e)
        {
            DragDropEffects dde1 = DoDragDrop("Test", DragDropEffects.All);
        }

        private void ucSearchLucene1_SearchCompleted()
        {
            if (_dtParseResults == null)
                return;

            string[] foundUIDs = ucSearchLucene1.FoundResults;

            string filter = string.Empty;
            int i = 0;
            foreach (string s in foundUIDs)
            {
                if (i == 0)
                    filter = string.Concat("'", s, "'");
                else
                    filter = string.Concat(filter, ", ", "'", s, "'");

                i++;
            }

            filter = string.Concat("UID IN (", filter, ")");

            DataView dv = new DataView(_dtParseResults);

            dv.RowFilter = filter;
            dv.Sort = "SortOrder ASC";

            this.dvgParsedResults.DataSource = dv;

            butRefresh.Visible = true;
            butUnallocated.Highlight = false;
            butUnallocated.Refresh();

            //Found [] Segments/Paragraphs or Sentences containing []
            ShowFiltered(string.Concat("Found ", ucSearchLucene1.FoundQty.ToString(), " Sentences containing '", ucSearchLucene1.SearchCriteria, "'"));
        }

        private void ucSearchHoot1_SearchCompleted()
        {
            if (_dtParseResults == null)
                return;

            string[] foundUIDs = ucSearchHoot1.FoundResults;

            string filter = string.Empty;
            int i = 0;
            foreach (string s in foundUIDs)
            {
                if (i == 0)
                    filter = string.Concat("'", s, "'");
                else
                    filter = string.Concat(filter, ", ", "'", s, "'");

                i++;
            }

            filter = string.Concat("UID IN (", filter, ")");

            DataView dv = new DataView(_dtParseResults);

            dv.RowFilter = filter;
            dv.Sort = "SortOrder ASC";

            this.dvgParsedResults.DataSource = dv;

            butRefresh.Visible = true;
            butUnallocated.Highlight = false;
            butUnallocated.Refresh();

            //Found [] Segments/Paragraphs or Sentences containing []
            ShowFiltered(string.Concat("Found ", ucSearchHoot1.FoundQty.ToString(), " Segments/Paragraphs containing '", ucSearchHoot1.SearchCriteria, "'"));

        }

        public void ShowUnallocated()
        {
            if (!butUnallocated.Highlight)
                return;

            string[] unallocated = _MatrixDocTypes.GetAllocatedUIDs(_SelectedDoc);
            if (unallocated == null)
            {
                if (_MatrixDocTypes.ErrorMessage != string.Empty)
                {
                    MessageBox.Show(_MatrixDocTypes.ErrorMessage, "Unable to Show Unallocated", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }

                return;
            }

            string filter = string.Empty;
            int i = 0;
            foreach (string s in unallocated)
            {
                if (i == 0)
                    filter = string.Concat("'", s, "'");
                else
                    filter = string.Concat(filter, ", ", "'", s, "'");

                i++;
            }

            filter = string.Concat("UID NOT IN (", filter, ")");

            DataView dv = new DataView(_dtParseResults);

            dv.RowFilter = filter;
            dv.Sort = "SortOrder ASC";

            this.dvgParsedResults.DataSource = dv;

            ShowFiltered("Unallocated Segments/Paragraphs or Sentences are shown. As you allocate the list will be updated with unallocated items.");
        }

        public void ShowFiltered(string description)
        {
            picFilter.Visible = true;
            picFilter.Refresh();
            this.lblCaptionDescription.Text = description;
        }

        public void ShowUnfiltered()
        {
            picFilter.Visible = false;
            this.lblCaptionDescription.Text = "All parsed segments/paragraphs and/or sentences (items) and their associated notes are from the Document Analyzer are shown.  You can allocate these items by dragging-and-dropping them into the Matrix.";

            butRefresh.Visible = false;

            ucSearchHoot1.Clear();
            ucSearchLucene1.Clear();

        }

        private void butUnallocated_Click(object sender, EventArgs e)
        {
            if (butUnallocated.Highlight) // Refresh and Show All
            {
                butUnallocated.Highlight = false;

                this.dvgParsedResults.DataSource = _dtParseResults;

                ShowUnfiltered();
                
                // ToDo Change ToolTip
            }
            else // Show only unallocated
            {
                butUnallocated.Highlight = true;

                ShowUnallocated();

               
                // ToDo Change ToolTip
            }
        }

        private void dvgParsedResults_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dvgParsedResults.Columns[e.ColumnIndex].Name == "Notes")
            {
                // Your code would go here - below is just the code I used to test 

                if (dvgParsedResults.Rows[e.RowIndex].Cells["UID"].Value == null)
                    return;

                string sUID = dvgParsedResults.Rows[e.RowIndex].Cells["UID"].Value.ToString();

                if (_Source == "Deep Analysis")
                {
                    if (dvgParsedResults.Columns.Contains("FileName"))
                    {
                        return;
                    }

                    string path = _ProjectsMgr.GetProjectDocDARPath_Notes(_ProjectName, _SelectedDoc);
                    if (path == string.Empty)
                    {
                       return;
                    }

                    string file = string.Concat(sUID, ".rtf");

                    string pathFile = Path.Combine(path, file);

                    if (File.Exists(pathFile))
                    {
                        e.Value = _Notepad;
                    }
                    else
                    {
                        e.Value = _Blank;
                    }
                }
                else
                {
                    if (!dvgParsedResults.Columns.Contains("FileName"))
                    {
                        return;
                    }

                    string filePattern = string.Concat(sUID, "_1.rtf");


                    string pathFile = Path.Combine(_ProjectsRootFolder, _ProjectName, "Docs", _SelectedDoc, "Current", "ParseSec", "Notes", filePattern);


                    if (File.Exists(pathFile))
                    {
                        e.Value = _Notepad;
                    }
                    else
                    {
                        e.Value = _Blank;
                    }
                }
            } 
        }

        private void txtbNotes_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
            {
                if (sender != null)
                    ((TextBox)sender).SelectAll();
            }
        }

        private void chkbIncludeNoWithText_CheckedChanged(object sender, EventArgs e)
        {
            SetTextAllocate();
        }

        private void chkDocView_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDocView.Checked)
            {
                string file = string.Concat(_SelectedDoc, ".txt");
                string currentDocPath = Path.Combine(_ProjectsRootFolder, _ProjectName, "Docs", _SelectedDoc, "Current", "ParseSec");
                string docParsedSecKeywords = Path.Combine(currentDocPath, "Keywords");
                string pathFile = Path.Combine(currentDocPath, file);
                string fileRTF = string.Concat(_SelectedDoc, ".rtf");
                string keywordsPathFile = Path.Combine(docParsedSecKeywords, fileRTF);
                if (File.Exists(keywordsPathFile))
                {
                    _frmDocumentView = new frmDocumentView();
                    _frmDocumentView.LoadData(keywordsPathFile, docParsedSecKeywords, _SelectedDoc);

                }
                else
                {
                    if (!File.Exists(pathFile))
                    {
                        string msg = string.Concat("Unable to find file: ", pathFile);
                        MessageBox.Show(msg, "Unable to Display Document", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }

                    _frmDocumentView = new frmDocumentView();
                    _frmDocumentView.LoadData(pathFile, docParsedSecKeywords, _SelectedDoc);
                }

                _frmDocumentView.Show();

                // Show current segment
                if (dvgParsedResults.Columns.Contains("LineStart"))
                {
                    DataGridViewRow row = dvgParsedResults.CurrentRow;
                    if (row == null)
                        return;

                    int line = 0;
                   // if (row.Cells["LineStart"].Value != null)
                    if (row.Cells["LineStart"].Value.ToString() != string.Empty) // Changed on 12.19.2018
                    {
                        line = Convert.ToInt32(row.Cells["LineStart"].Value);
                    }
                    line++; // parser engine is zero based, while document viewer is one base. Therefore add one.

                    _frmDocumentView.ShowSegment(line, richTextBox1.Text);
                }

            }
            else
            {
                if (_frmDocumentView != null)
                {
                    _frmDocumentView.Close();
                    _frmDocumentView = null;
                }
            }

        }

        private void ucMatrixDocTypes2_VisibleChanged(object sender, EventArgs e)
        {
    
        }
        
    }
}
