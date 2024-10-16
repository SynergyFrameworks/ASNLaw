using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

using Atebion.Tasks;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class ucProjectsDocs : UserControl
    {
        public ucProjectsDocs()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        // Declare delegate for when a project has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Project is selected")]
        public event ProcessHandler ProjectSelected;

        [Category("Action")]
        [Description("Fires when Document/Documents are selected")]
        public event ProcessHandler DocumentsSelected;

        [Category("Action")]
        [Description("Fires when Document/Documents are unselected")]
        public event ProcessHandler HasReset;


        private int _ProjCount = 0;

        private ProjectManager _ProjectManager;
        private Atebion.Tasks.Manager _TaskManager;
        private DocumentManager _DocumentManager;
        private DataView _ViewAttributes;

        private int _TaskCurrentUID = 1; // Default


        private string _Task = string.Empty;
        private string _DocSelectQty = "1";
        private bool _HasBeenParsed = false;

        private bool _HasCheckedChanged = false;


        public Atebion.Tasks.Manager TaskManager
        {
            get { return _TaskManager; }
        }

        private string _TxtDocPathFile = string.Empty;
        public string TxtDocPathFile
        {
            get { return _TxtDocPathFile; }
        }

        public bool UseNumericHierarchy
        {
            get { return _TaskManager.UseNumericHierarchy(); }
        }


        private string _Task_ProcessObject = string.Empty;
        public string Task_ProcessObject
        {
            get {return _Task_ProcessObject;}
        }

        private string _Next_TaskProcessObject = string.Empty;
        public string Next_TaskProcessObject
        {
            get { return _Next_TaskProcessObject; }
        }

        private int _Next_TaskUID = -1;
        public int Next_TaskUID
        {
            get { return _Next_TaskUID; }
        }

        private string _ProjectName = string.Empty;
        public string ProjectName
        {
            get { return _ProjectName; }
        }

        private string _CurrentDocument = string.Empty;
        private bool _AddNewError = false;

        private List<string> _Documents_Selected = new List<string>();
        public List<string> Documents_Selected
        {
            get 
            {
                return _Documents_Selected; 
            }
        }

        private Atebion.Tasks.Manager.ButtonNextAnalyze _NextButtonType = Manager.ButtonNextAnalyze.Hide;
        public Atebion.Tasks.Manager.ButtonNextAnalyze NextButtonType
        {
            get { return _NextButtonType; }
        }

        private bool _isUseDefaultParseAnalysis = false;
        public bool isUseDefaultParseAnalysis
        {
            get { return _isUseDefaultParseAnalysis; }
        }

        public bool Show_ParseType
        {
            get { return _TaskManager.isShow_ParseType(); }
        }

        private string _AnalysisName = string.Empty;
        public string AnalysisName
        {
            get {
                    //_AnalysisName = _AnalyzerManager.GetNextSet_AnalysisName(_Task, false); // Reget the next Analysis Name because the user cound select another analysis run after running a previous analysis
                    //this.lblAnalysisName.Text = string.Concat("Analysis Name: ", _AnalysisName);
                
                    return _AnalysisName; 
                }
        }

        public string ParseType
        {
            get
            {
                if (Show_ParseType)
                {
                    if (rdbLegal.Checked)
                        return "Legal";
                    else
                        return "Paragraph";
                }
                else
                {
                    return _TaskManager.GetParseType();
                }
            }
        }

        public string DiffOldDoc
        {
            get
            {
                if (cboDiff.Items.Count == 0)
                    return string.Empty;

                return cboDiff.Text;

            }
        }


        private AnalyzerManager _AnalyzerManager;


        public bool LoadData()
        {
            return LoadData(_TaskManager, _Task);
        }

       



        public bool LoadData(Atebion.Tasks.Manager TaskManager, string Task)
        {
            this.splitContainer1.Width = lstbProjects.Width + 10;

            _TaskManager = TaskManager;
            _Task = Task;
            _ProjectManager = new ProjectManager();
            _DocumentManager = new DocumentManager();

            lblInstructions_Documents.ForeColor = Color.Blue;
            lblInstructions_Projects.Text = "Select or Create a new Project for Documents";

            lstbProjects.Items.Clear();
            lstbDocs.Items.Clear();
            txtbProjectDescription.Text = string.Empty;
            ucDocDetails1.Clear();
            _Documents_Selected.Clear();
            _CurrentDocument = string.Empty;

            HideDocControls();

            _DocSelectQty = _TaskManager.GetDocSelectQty(Task);
            if (_DocSelectQty == string.Empty)
            {
                string ErrCaption = string.Concat("Unable to Load Projects for Task '", Task, "'");
                if (_TaskManager.ErrorMessage.Length > 0)
                {
                    MessageBox.Show(_TaskManager.ErrorMessage, ErrCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                else
                {
                    MessageBox.Show("Error: Missing Qty of Documents to Select", ErrCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            List<string> projects = _ProjectManager.GetProjectNames();

            if (projects == null)
            {
                _ProjCount = 0;
                return false;

            }

            foreach (string s in projects)
            {
                lstbProjects.Items.Add(s);
            }

            _ViewAttributes = _TaskManager.GetTaskPropertiesAndAttributes(0); // Tasks numbers are zero base, while TaskFlow UIDs are one base
            if (_ViewAttributes == null)
            {
                MessageBox.Show("Unable to get the current Task's information.", "Task Information Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _Task_ProcessObject = _TaskManager.Task_ProcessObject_Current;

            // Set Diff Defaults -- Added 06.23.2020
            panDiff.Visible = false;  
            cboDiff.Items.Clear();

            if (_Task_ProcessObject == ProcessObject.AcroSeeker)
            {
                _DocSelectQty = "2 or more";
            }
            else if (_Task_ProcessObject == ProcessObject.CompareDocsDiff)
            {
                _DocSelectQty = "2";
                panDiff.Visible = true; // Added 06.23.2020
            }
            else if (_Task_ProcessObject == Atebion.Tasks.ProcessObject.CreateXRefMatrix)
            {
                _DocSelectQty = "1";
            }
            else if (_Task_ProcessObject == Atebion.Tasks.ProcessObject.XRefMatrices)
            {
                _DocSelectQty = "1";
            }

            _Next_TaskProcessObject = _TaskManager.Next_TaskProcessObject;

            _Next_TaskUID = _TaskManager.Next_TaskUID;

            if (_Next_TaskProcessObject == string.Empty) // Use for ProcessObject.AcroSeeker & ProcessObject.CompareDocsDiff
            {
                _Next_TaskProcessObject = _Task_ProcessObject;
                _Next_TaskUID = 0;
            }

            if (_Task_ProcessObject == Atebion.Tasks.ProcessObject.CreateXRefMatrix) // Matrix Builder - Create a Matrix
            {
                _NextButtonType = Manager.ButtonNextAnalyze.Create;
            }
            else if (_Task_ProcessObject == Atebion.Tasks.ProcessObject.XRefMatrices) // Matrix Builder - Go to Matrices
            {
                _NextButtonType = Manager.ButtonNextAnalyze.XRefMatrices;
            }
            else
            {
                _NextButtonType = _TaskManager.GetNextButtonType(_Next_TaskProcessObject, _Next_TaskUID);
            }

            _isUseDefaultParseAnalysis = _TaskManager.isUseDefaultParseAnalysis();

            _AnalyzerManager = new AnalyzerManager();

            //if (_isUseDefaultParseAnalysis)
            //{
            //    _AnalysisName = string.Empty;
            //    this.panHeader.Visible = false;
            //}
            //else
            //{

            //    _AnalysisName = _AnalyzerManager.GetNextSet_AnalysisName(_Next_TaskProcessObject, false);
            //    this.lblAnalysisName.Text = string.Concat("Analysis Name: ", _AnalysisName);
            //    this.panHeader.Visible = true;
            //}

            panParseType.Visible = Show_ParseType;

            _ProjCount = lstbProjects.Items.Count;

            return true;
        }

        private void HideDocControls()
        {
            lstbDocs.Visible = false;
            lstbDocsMulti.Visible = false;

            lstbDocs.Dock = DockStyle.None;
            lstbDocsMulti.Dock = DockStyle.None;

            ucDocDetails1.Visible = false;
            panRightHeader.Visible = false;

            butDoc_Edit.Visible = false;
            butDoc_Remove.Visible = false;
            butDoc_Replace.Visible = false;

        }


        private void lblProjectDescription_TextChanged(object sender, EventArgs e)
        {
            txtbProjectDescription.Text = lblProjectDescription.Text;
        }

        private void txtbProjectDescription_TextChanged(object sender, EventArgs e)
        {
            txtbProjectDescription.Text = lblProjectDescription.Text;
        }

        private void lblProjects_Click(object sender, EventArgs e)
        {
  
        }

        private void lstbProjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectProject();
        }

        private void LoadDocuments()
        {
            lstbDocs.Items.Clear();
            lstbDocsMulti.Items.Clear();
            ucDocDetails1.Clear();

        }

        private void selectProject()
        {
            _Documents_Selected.Clear();
            _Next_TaskProcessObject = string.Empty;

            if (this.lstbProjects.Items.Count == 0)
                return;

            if (lstbProjects.SelectedItem != null)
            {
                _ProjectName = lstbProjects.SelectedItem.ToString();
            }
            else
            {
                _ProjectName = AppFolders.ProjectName;
            }

            _ProjectManager.ProjectName = _ProjectName;

            lblProjectDescription.Text = _ProjectManager.Description;

            if (ProjectSelected != null)
                ProjectSelected();

            AppFolders.ProjectName = _ProjectName;

            if (AppFolders.ProjectCurrentTemp == string.Empty)
                return;

            _Next_TaskProcessObject = _TaskManager.Next_TaskProcessObject;

            if (_isUseDefaultParseAnalysis)
            {
                _AnalysisName = string.Empty;
                this.panHeader.Visible = false;
            }
            else
            {

                _AnalysisName = _AnalyzerManager.GetNextSet_AnalysisName(_Task, false);
                this.lblAnalysisName.Text = string.Concat("Analysis Name: ", _AnalysisName);
                this.panHeader.Visible = true;
            }

            // Delete all Temp files -- Added 07.18.2014
            System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(AppFolders.ProjectCurrentTemp);

            foreach (FileInfo file in downloadedMessageInfo.GetFiles())
            {
                try
                {
                    file.Delete();
                }
                catch
                {
                    // added this because a file may be in use, locked and cannot be deleted.
                }
            }

            List<string> docNames = _DocumentManager.GetDocNames();

            int docsQty = 0;
            if (_DocSelectQty == "1")
            {
                // Load Documents
                docsQty = LoadDocuments_SingleSelection(docNames);

                lstbDocsMulti.Visible = false;
                this.lstbDocs.Visible = true;
            }
            else
            {
                // Load Documents
                docsQty = LoadDocuments_MultiSelection(docNames);

                this.lstbDocs.Visible = false;
                lstbDocsMulti.Visible = true;

            }

            if (_Task_ProcessObject == ProcessObject.CreateXRefMatrix) // Matrix Builder
            {
                lblInstructions_Documents.Text = "Select documents and check that they have been analyzed.";
            }
            if (_Task_ProcessObject == ProcessObject.XRefMatrices) // Matrix Builder
            {
                lblInstructions_Documents.Text = "Select a Project and click the X-Ref Matrices button";
            }
            else
            {
                if (docsQty == 0)
                {
                    lblInstructions_Documents.Text = "Click the 'New' button to add a new document.";
                }
                else
                {
                    lblInstructions_Documents.Text = "Select a document to analyze or click the 'New' button to add a new document.";
                }
            }



            ShowDocControls();

            if (ProjectSelected != null)
                ProjectSelected();
        }

        public bool SelectCurrentFile()
        {

            if (lstbDocsMulti.Visible)
            {
                for (int i = 0; i < lstbDocsMulti.Items.Count; i++)
                {
                    if (_CurrentDocument == lstbDocsMulti.Items[i].ToString())
                    {
                        lstbDocsMulti.SelectedIndex = i;
                        return true;
                    }

                }
            }
            else
            {   
                for (int i = 0; i < lstbDocs.Items.Count; i++)
                {
                    if (_CurrentDocument == lstbDocs.Items[i].ToString())
                    {
                        lstbDocs.SelectedIndex = i;
                        return true;
                    }

                }
            }

            return false;

        }

        private void ShowDocControls()
        {
            
            if (_DocSelectQty == "1")
            {
                lstbDocs.Visible = true;
                lstbDocs.Dock = DockStyle.Left;
            }
            else
            {
                lstbDocsMulti.Visible = true;
                lstbDocsMulti.Dock = DockStyle.Left;
            }

            ucDocDetails1.Clear();

            panRightHeader.Visible = true;



        }

        private int LoadDocuments_SingleSelection(List<string> docNames)
        {
            lstbDocs.Items.Clear();

            foreach (string docName in docNames)
            {
                lstbDocs.Items.Add(docName);
            }

            return docNames.Count();

        }

        private int LoadDocuments_MultiSelection(List<string> docNames)
        {
            lstbDocsMulti.Items.Clear();

            foreach (string docName in docNames)
            {
                lstbDocsMulti.Items.Add(docName);
            }

            return docNames.Count();
        }

        private void lblProjectDescription_TextChanged_1(object sender, EventArgs e)
        {
            txtbProjectDescription.Text = lblProjectDescription.Text;
        }

        private void txtbProjectDescription_TextChanged_1(object sender, EventArgs e)
        {
            txtbProjectDescription.Text = lblProjectDescription.Text;
        }

        private void lstbDocs_SelectedIndexChanged(object sender, EventArgs e)
        {
            butDoc_Edit.Visible = true;
            butDoc_Remove.Visible = true;
            butDoc_Replace.Visible = true;

            AppFolders.DocName = lstbDocs.Text;

            _Documents_Selected.Clear();

            _Documents_Selected.Add(lstbDocs.Text);

            ShowFileDetails();

            // -------------- Document Details ----------------
            //string ext = string.Empty;
            //string docInfoPathFile = Path.Combine(AppFolders.DocInformation, "Info.txt");

            //if (File.Exists(docInfoPathFile))
            //{
            //    string parsedXMLPathFile = Path.Combine(AppFolders.DocParsedSecXML, "ParseResults.xml");
            //    _HasBeenParsed = false;
            //    if (File.Exists(parsedXMLPathFile))
            //    {
            //        _HasBeenParsed = true;
            //    }

            //    string fileDetails = Files.ReadFile(docInfoPathFile);
            //    string[] docDetails = Files.ReadFile2Array(docInfoPathFile);
            //    string fileName = string.Empty;
            //    foreach (string detail in docDetails)
            //    {
            //        if (detail.IndexOf("Source: ") != -1)
            //        {
            //            string pathFile = detail.Replace("Source: ", "");
            //            fileName = Files.GetFileName(pathFile, out ext);
            //        }
            //    }

            //    ucDocDetails1.ShowFileInformation(ext, fileName, _HasBeenParsed, fileDetails);

            //    this.ucDocDetails1.Visible = true;

            //    //if (DocumentsSelected != null)
            //    //    DocumentsSelected();
            //}
            //else
            //{
            //    this.ucDocDetails1.Visible = false;

            //    //if (DocumentsSelected != null)
            //    //    DocumentsSelected();
            //}

            if (DocumentsSelected != null)
                    DocumentsSelected();

        }

        public void ShowFileDetails()
        {
            _TxtDocPathFile = string.Empty;

            if (AppFolders.CurrentDocPath == string.Empty)
                return;

            string[] txtFiles = Directory.GetFiles(AppFolders.CurrentDocPath, "*.txt");
            if (txtFiles.Length == 0)
                return;

            _TxtDocPathFile = txtFiles[0];

            // -------------- Document Details ----------------
            string ext = string.Empty;
            string docInfoPathFile = Path.Combine(AppFolders.DocInformation, "Info.txt");

            if (File.Exists(docInfoPathFile))
            {
                string parsedXMLPathFile = Path.Combine(AppFolders.DocParsedSecXML, "ParseResults.xml");
                _HasBeenParsed = false;
                if (File.Exists(parsedXMLPathFile))
                {
                    _HasBeenParsed = true;
                }

                string fileDetails = Files.ReadFile(docInfoPathFile);
                string[] docDetails = Files.ReadFile2Array(docInfoPathFile);
                string fileName = string.Empty;
                foreach (string detail in docDetails)
                {
                    if (detail.IndexOf("Source: ") != -1)
                    {
                        string pathFile = detail.Replace("Source: ", "");
                        fileName = Files.GetFileName(pathFile, out ext);
                    }
                }

                ucDocDetails1.ShowFileInformation(ext, fileName, _HasBeenParsed, fileDetails);

                this.ucDocDetails1.Visible = true;

                if (_HasBeenParsed && _isUseDefaultParseAnalysis)
                {
                    if (_Task_ProcessObject != ProcessObject.CreateXRefMatrix || _Task_ProcessObject != ProcessObject.XRefMatrices)
                    {
                        lblInstructions_Documents.Text = "Notice: If you run the Analyzer again, it will overwrite the current Analysis Results.";
                        lblInstructions_Documents.ForeColor = Color.Red;
                    }
                }
                else
                {
                    lblInstructions_Documents.ForeColor = Color.Blue;
                    lblInstructions_Documents.Text = String.Empty;
                }


                //if (DocumentsSelected != null)
                //    DocumentsSelected();
            }

        }

        private void butDoc_New_Click(object sender, EventArgs e)
        {
            AddNewDocument();
            selectProject();
            SelectCurrentFile();
            lblInstructions_Documents.Text = "Click the 'Open' button to removed unwanted text prior to analysis";
        }

        public bool AddNewDocument()
        {
            frmDocuments3 frm = new frmDocuments3();
            frm.LoadData(string.Empty);
            // Show Dialog as a modal dialog and determine if DialogResult = OK.
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _CurrentDocument = frm.DocumentName;

                LoadDocuments();
                selectProject();
              //  SelectDocument(_CurrentDocument);
                return true;
            }
            else
            {
                _AddNewError = frm.ErrorOccurred;
                if (_AddNewError)
                {
                    _CurrentDocument = frm.DocumentName;
                    frm = null;
                    selectProject();
                    Application.DoEvents();
                    RemoveDocument(_CurrentDocument);
                    _AddNewError = false;
                    _CurrentDocument = string.Empty;
                }
            }

            return false;

        }

        private bool SelectDocument(string DocName)
        {
            for (int i = 0; i < lstbDocs.Items.Count; i++)
            {
                if (lstbDocs.Items[i].ToString() == DocName)
                {

                  //  lstbDocs.SelectedIndex = i;

                    lstbDocs.SetSelected(i, true);
                    
                    return true;
                }
            }

            return false;
        }

        private bool RemoveDocument(string DocName)
        {
            for (int i = 0; i < lstbDocs.Items.Count; i++)
            {
                if (lstbDocs.Items[i].ToString() == DocName)
                {

                    lstbDocs.SelectedIndex = i;
                    RemoveDocument(false);
                    selectProject();

                    return true;
                }
            }

            return false;
        }

        public bool RemoveDocument(bool ShowConfirm)
        {
            bool returnValue = false;

            string selectedDocument = string.Empty;

            

            if (lstbDocs.Visible)
            {
                if (lstbDocs.Items.Count == 0)
                {
                    return returnValue;
                }

                if (lstbDocs.SelectedIndex == -1)
                {
                    return returnValue;
                }

                selectedDocument = lstbDocs.Text;
            }
            else
            {
                if (lstbDocsMulti.Items.Count == 0)
                {
                    return returnValue;
                }

                if (lstbDocsMulti.SelectedIndex == -1)
                {
                    return returnValue;
                }

                selectedDocument = lstbDocsMulti.Text;


            }

            if (ShowConfirm)
            {
                string msg = string.Concat("Are you sure you want to Remove ", selectedDocument, " document?");
                
                if (MessageBox.Show(msg, "Confirm Document Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return returnValue;
            }

            
            Files.UnlockAllFiles(AppFolders.DocNamePath);


            string newPath = AppFolders.DocNamePath;

            string prifix = "~";
            while (Directory.Exists(newPath))
            {
                newPath = string.Concat(AppFolders.ProjectCurrent, @"\Docs", @"\", prifix, AppFolders.DocName);
                if (Directory.Exists(newPath))
                {
                    prifix = "~" + prifix;
                }
                else
                {
                    break;
                }
            }

            try
            {

                Directory.Move(AppFolders.DocNamePath, newPath);

                AppFolders.DocName = string.Empty;

                LoadDocuments();

            }
            catch (Exception ex)
            {
                //  txtbProjects.Text = ex.Message;
                string msgText = "Unable to remove the selected document. A file is still open.";
                MessageBox.Show(msgText, "Document Not Removed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                return returnValue;
            }

            
            LoadDocuments();

            return true;
        }

        private void butProject_New_Click(object sender, EventArgs e)
        {
            frmProject frmNew = new frmProject(false);
            if (frmNew.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
                // ToDo Select new project
            }
        }

        private void butProject_Remove_Click(object sender, EventArgs e)
        {
            //frmProject frmNew = new frmProject(_ProjectName);
            //if (frmNew.ShowDialog(this) == DialogResult.OK)
            //{
            //    _ProjectManager.ProjectName = _ProjectName;

            //    lblProjectDescription.Text = _ProjectManager.Description;
            //}

            DeleteProject();
        }

        private bool DeleteProject()
        {
            bool returnValue = false; 

            if (lstbProjects.Items.Count == 0)
            {
                return returnValue;
            }

            if (lstbProjects.SelectedIndex == -1)
            {
                return returnValue;
            }

            string msg = string.Format("Are you sure you want to Remove the selected {0}?", "Project");
            if (MessageBox.Show(msg, "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                return returnValue;

            _ProjectName = lstbProjects.SelectedItem.ToString();

            AppFolders.ProjectName = _ProjectName;

            string newPath = AppFolders.ProjectCurrent;

            string prifix = "~";
            string obsoleteDirName = string.Empty;
            while (Directory.Exists(newPath))
            {
                obsoleteDirName = string.Concat(prifix, AppFolders.ProjectName);
                newPath = Path.Combine(AppFolders.Project, obsoleteDirName);
                
                if (Directory.Exists(newPath))
                {
                    prifix = "~" + prifix;
                }
                else
                {
                    break;
                }
            }

            try
            {
                Directory.Move(AppFolders.ProjectCurrent, newPath);
                AppFolders.ProjectName = string.Empty;

                lblInstructions_Projects.Text = string.Empty;
                
            }
            catch 
            {
                lblInstructions_Projects.Text = string.Empty;
                return returnValue;
            }

            LoadData();

            return true;
        }

        private void butDoc_Edit_Click(object sender, EventArgs e)
        {
            if (AppFolders.CurrentDocPath == string.Empty) // Added 03.10.2020
                return;
            
            string[] txtFiles = Directory.GetFiles(AppFolders.CurrentDocPath, "*.txt");
            if (txtFiles.Length == 0)
                return;

            string docFile = txtFiles[0];

          //  string pathDoc = Path.Combine(AppFolders.CurrentDocPath, docFile);

            frmDocumentEdit frm = new frmDocumentEdit(docFile);
            frm.ShowDialog(this);

            frm = null;
        }

        private void lstbDocsMulti_SelectedIndexChanged(object sender, EventArgs e)
        {
            butDoc_Edit.Visible = true;
            butDoc_Remove.Visible = true;
            butDoc_Replace.Visible = true;

            AppFolders.DocName = lstbDocsMulti.Text;
        }

        private void butDoc_Remove_Click(object sender, EventArgs e)
        {
            RemoveDocument(true);
            selectProject();
        }

        private void butDoc_Replace_Click(object sender, EventArgs e)
        {
            string msg = "Are you sure you want to replace the existing document? All previous analysis results will be archived, not be avaible, and require re-analysis.";
            if (MessageBox.Show(msg, "Confirm Document Replacement", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            frmDocuments3 frmDocReplace = new frmDocuments3();
            frmDocReplace.LoadData(AppFolders.DocName);
            if (frmDocReplace.ShowDialog(this) == DialogResult.OK)
            {
                
                try
                {
                    string nextDocVerPath = AppFolders.GetDocPathNextVersion();
                    //Directory.Move(AppFolders.CurrentDocPath, nextDocVerPath);

                    //LoadDocuments();
                }
                catch(Exception ex)
                {
                    string errMsg = string.Concat("Error: ", ex.Message);
                    MessageBox.Show(errMsg, "Unable to Replace Document", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }


            }

        }

        private void lstbDocsMulti_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            _HasCheckedChanged = true;

            // Added 06.23.2020
            if (lstbDocsMulti.SelectedIndex < 0)
                return;

            // Added 06.23.2020
            if (lstbDocsMulti.GetItemChecked(lstbDocsMulti.SelectedIndex))
            {
                string doc = lstbDocsMulti.Text;
                int index = cboDiff.FindStringExact(doc);
                if (index == -1)
                {
                    //int checkedCount = lstbDocsMulti.CheckedItems.Count;
                    //if (checkedCount == 0)
                     //   cboDiff.Items.Clear();

                    return;
                }

                cboDiff.Items.RemoveAt(index);
            }

        }

        private int Populate_Documents_Selected()
        {
            
            var checkedItems = lstbDocsMulti.CheckedItems.OfType<String>().ToList();

            _Documents_Selected = checkedItems;

            return checkedItems.Count;

        }

        private void ucProjectsDocs_Load(object sender, EventArgs e)
        {
            
        }

        private void ucProjectsDocs_Paint(object sender, PaintEventArgs e)
        {
            
        }

        private void ucProjectsDocs_Enter(object sender, EventArgs e)
        {
 
        }

        private void lstbDocsMulti_MouseUp(object sender, MouseEventArgs e)
        {
            if (_HasCheckedChanged)
            {

                if (_DocSelectQty == "2")
                {
                    if (lstbDocsMulti.CheckedItems.Count > 2)
                    {
                        string msg = "You can only select 2 documents for this task.";
                        MessageBox.Show(msg, "Document Selection Max is 2", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        lstbDocsMulti.SetItemCheckState(lstbDocsMulti.SelectedIndex, CheckState.Unchecked);

                        Populate_Documents_Selected();

                        _HasCheckedChanged = false;

                        return;
                    }

                    if (lstbDocsMulti.CheckedItems.Count == 2)
                    {
                        cboDiff.Items.Add(lstbDocsMulti.Text); // Added 06.23.2020
                        if (DocumentsSelected != null)
                            DocumentsSelected();
                    }
                    else if (lstbDocsMulti.CheckedItems.Count == 0)
                    {
                        cboDiff.Items.Clear();
                    }
                    else
                    {
                        cboDiff.Items.Clear(); // Added 06.23.2020
                        cboDiff.Items.Add(lstbDocsMulti.Text); // Added 06.23.2020
                        cboDiff.SelectedIndex = 0;
                        if (HasReset != null)
                            HasReset();

                    }

                }
                else
                {
                    if (lstbDocsMulti.CheckedItems.Count == 0)
                    {
                        //int checkedCount = lstbDocsMulti.CheckedItems.Count;
                        //if (checkedCount == 0)
                        cboDiff.Items.Clear();

                        if (HasReset != null)
                            HasReset();
                    }
                    else
                    {
                        if (DocumentsSelected != null)
                            DocumentsSelected();
                    }
                }
                
            }

            Populate_Documents_Selected();

            _HasCheckedChanged = false;             
         
            int countSelected = 0;
            cboDiff.Items.Clear();
            for (int i = 0; i <= (lstbDocsMulti.Items.Count - 1); i++)
            {
                if (lstbDocsMulti.GetItemChecked(i))
                {
                    cboDiff.Items.Add(lstbDocsMulti.Items[i].ToString());
                    countSelected++;
                }
            }

            if (countSelected > 0)
                cboDiff.SelectedIndex = 0;

        }

        private void picProjects_Click(object sender, EventArgs e)
        {
            if (AppFolders.CurrentDocPath.Length > 0)
                System.Diagnostics.Process.Start("explorer.exe", AppFolders.CurrentDocPath);
        }



        

    }
}
