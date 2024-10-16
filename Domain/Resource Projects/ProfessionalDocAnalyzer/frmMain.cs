using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Atebion.Common;
using Atebion.Tasks;
using Atebion.ConceptAnalyzer;
using Atebion.Word.OpenXML;
using AcroParser2;


namespace ProfessionalDocAnalyzer
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private LicenseMgr _lMgr = new LicenseMgr();

        private Atebion.Tasks.Manager _TaskManager;
        private AnalyzerManager _AnalyzerManager;

        private Atebion.ConceptAnalyzer.Analysis _ConceptDicAnalyzer;

        private string _Task_Current = string.Empty;
        private int _TaskNo = -1;
        private string _TaskProcessObject = string.Empty;
        private string _Next_TaskProcessObject = string.Empty;
        private DataView _dvAttributes;
        private string[] _SelectedDocs;

        private KeywordsMgr2 _KeywordsMgr;

        private bool _isAlreadyOpen = false;

        private AnalysisResultsType.Selection _AnalysisResultsType;
        private AnalysisResultsType.SearchType _SearchType;

        private string _AnalysisPath = string.Empty;

        private string _ParsedSecPath = string.Empty;
        private string _ParsedSecXMLPath = string.Empty;
        private string _ParsedSecNotesPath = string.Empty;
        private string _ParsedSecIndexPath = string.Empty;
        private string _ParsedSecExportPath = string.Empty;

        private bool _isUseDefaultParseAnalysis = false;

        private string _Settings_SelectedMenuItem = string.Empty;

        private List<string> _DocsSelected;
        private string _DictionarySelected = string.Empty;
        private bool _SynonymsFind = false;
        private bool _WholeWordsFind = false;

        private bool _Results_isProjectSelected = false;


        private int _currentStep = -1;
        private Modes _PreviousMode;
        private Modes _currentMode;
        private enum Modes
        {
            Prestart = -1,
            Start = 0,
            TaskList = 1,
            ProjDocs = 2,
            KeywordsSelection = 3,
            DictionarySelection = 4,
            AnalysisResults = 5,
            AnalysisResults_Concepts = 6,
            AnalysisResults_CompareDocsConcepts = 7,
            AnalysisResults_Dictionary = 8,
            AnalysisResults_CompareDocsDictionary = 9,
            AnalysisResults_QCReadability = 10,
            AnalysisResults_CompareDocsDiff = 11,
            AnalysisResults_AcroSeeker = 12,
            AnalysisResults_DeepAnalysis = 14,

            Results = 50,
            Settings = 60,
        }

        private void Check4HootSearchEngFile()
        {
            string pathfile = Path.Combine(AppFolders.AppDataPath, "UseHootSearchEng.txt");
            if (File.Exists(pathfile))
            {
                AppFolders.UseHootSearchEngine = true;
            }
            else
            {
                AppFolders.UseHootSearchEngine = false;
            }
        }

        private void ModeAdjustments()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            HideAllPrimaryButtons();
            HideAllUCs();

            // Hide User controls



            switch (_currentMode)
            {
                case Modes.Prestart:
                    ucStart1.Visible = true;
                    ucStart1.Dock = DockStyle.Fill;
                    this.Refresh();
                    ucStart1.StartValidate();
                    break;

                case Modes.Start:
                    Check4HootSearchEngFile();
                   // ucStart1.Location = new Point(250, 0);
                    panHeader.Visible = true;
                    ucWelcome1.Dock = DockStyle.Fill;
                    if (!_isAlreadyOpen)
                        ucWelcome1.LoadData(_TaskManager);
                    ucWelcome1.Visible = true;
                    _isAlreadyOpen = true;

                    break;

                case Modes.TaskList:
                    this.ucTasks1.LoadData(AppFolders.WorkgroupName, string.Empty, string.Empty, string.Empty, null, -1);
                    ucTasks1.Visible = true;
                    ucTasks1.Refresh();
                    Butnext.Text = "Next";
                    panFooter.Visible = true;
                    ucTasksSelect1.LoadData();
                    ucTasksSelect1.Dock = DockStyle.Fill;
                    ucTasksSelect1.Visible = true;

                    break;

                case Modes.ProjDocs:
                    Butnext.Visible = false;
                    ucProjectsDocs1.Visible = true;
                    panFooter.Visible = true;
                    ucTasks1.CurrentStep = _TaskNo; // 0 1st Step -- Zero based
                    ucTasks1.Visible = true;
                    _TaskManager = ucTasksSelect1.TaskManager;
                    _Task_Current = ucTasksSelect1.Task;
                    ucProjectsDocs1.LoadData(_TaskManager, _Task_Current);
                    _Next_TaskProcessObject = ucProjectsDocs1.Next_TaskProcessObject;
                    ucProjectsDocs1.Dock = DockStyle.Fill;
                    ucProjectsDocs1.Visible = true;

                    break;

                case Modes.KeywordsSelection:
                    _SearchType = AnalysisResultsType.SearchType.Keywords;
                    Butnext.Visible = false;
                    panFooter.Visible = true;
                    ucTasks1.CurrentStep = _TaskNo; // 1 2nd Step -- Zero based
                    ucTasks1.Visible = true;
                  //  _Task_Current = ucProjectsDocs1.Next_TaskProcessObject;
                    _dvAttributes = _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo + 1); // 1 Based in the Task Lib. -- Next task, which is Keywords
                    //_Task_Current = _TaskManager.Task_Process_Current;
                    _TaskProcessObject = _TaskManager.Task_ProcessObject_Current;
                    _Next_TaskProcessObject = _TaskManager.Next_TaskProcessObject;
                    ucKeywordsSelect1.Dock = DockStyle.Fill;
                    ucKeywordsSelect1.LoadData(_TaskManager, _Task_Current);
                 //   _Next_TaskProcessObject = ucProjectsDocs1.Next_TaskProcessObject;
                    ucKeywordsSelect1.Visible = true;

                    break;

                case Modes.DictionarySelection:
                    Butnext.Visible = false;
                    panFooter.Visible = true;
                    ucTasks1.CurrentStep = _TaskNo; // 1 2nd Step -- Zero based
                    ucTasks1.Visible = true;
                    _dvAttributes = _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo + 1); // 1 Based in the Task Lib. -- Next task, which is Keywords
                    _TaskProcessObject = _TaskManager.Task_ProcessObject_Current;
                    _Next_TaskProcessObject = _TaskManager.Next_TaskProcessObject;

                    ucDictionarySelect1.LoadData(AppFolders.DictionariesPath);
                    ucDictionarySelect1.Dock = DockStyle.Fill;
                    ucDictionarySelect1.Visible = true;

                    break;

                case Modes.AnalysisResults:
                    Butnext.Visible = false;
                    panFooter.Visible = true;
                    ucTasks1.CurrentStep = _TaskNo; // 2 3rd Step -- Zero based
                    ucTasks1.Visible = true;
                    _TaskProcessObject = _TaskManager.Task_ProcessObject_Current;
                    _Next_TaskProcessObject = _TaskManager.Next_TaskProcessObject;

                    string pathKeyword = string.Empty;
                    bool isAnalysis = false;
                    if (_isUseDefaultParseAnalysis) // Defualt
                    {
                        pathKeyword = AppFolders.DocParsedSecKeywords;
                    }
                    else // Analysis 
                    {
                        pathKeyword = AppFolders.AnalysisParseSegKeywords;
                        isAnalysis = true;
                    }

                    ucAnalysisResults1.LoadData(_AnalysisResultsType, _SearchType, _ParsedSecPath, _ParsedSecXMLPath, _ParsedSecExportPath, _ParsedSecNotesPath, _ParsedSecIndexPath, pathKeyword, isAnalysis);
                    ucAnalysisResults1.Visible = true;
                    ucAnalysisResults1.Dock = DockStyle.Fill;
                    _Next_TaskProcessObject = "None";
                    Butnext.Visible = false;

                    break;

                case Modes.AnalysisResults_Concepts:
                    Butnext.Visible = false;
                    panFooter.Visible = true;
                    ucTasks1.CurrentStep = _TaskNo; // 2 3rd Step -- Zero based
                    ucTasks1.Visible = true;
                    _TaskProcessObject = _TaskManager.Task_ProcessObject_Current;
                    _Next_TaskProcessObject = _TaskManager.Next_TaskProcessObject;
                    ucResultsConcepts1.LoadData(_ConceptDicAnalyzer, AppFolders.ProjectName, AppFolders.AnalysisName, AppFolders.DocName);
                    ucResultsConcepts1.Visible = true;
                    ucResultsConcepts1.adjColumns();
                    ucResultsConcepts1.Dock = DockStyle.Fill;
                    Butnext.Visible = false;

                    break;

                case Modes.AnalysisResults_CompareDocsConcepts:
                    Butnext.Visible = false;
                    panFooter.Visible = true;
                    ucTasks1.CurrentStep = _TaskNo; // 2 3rd Step -- Zero based
                    ucTasks1.Visible = true;
                    ucResultsMultiConcepts1.LoadData(_ConceptDicAnalyzer, AppFolders.ProjectName, AppFolders.AnalysisName);
                    ucResultsMultiConcepts1.Visible = true;
                    ucResultsMultiConcepts1.Dock = DockStyle.Fill;
                    ucResultsMultiConcepts1.adjColumns();

                    break;

                case Modes.AnalysisResults_Dictionary:
                    Butnext.Visible = false;
                    panFooter.Visible = true;
                    ucTasks1.CurrentStep = _TaskNo; // 2 3rd Step -- Zero based
                    ucTasks1.Visible = true;

                    ucResultsDic1.LoadData(_ConceptDicAnalyzer, AppFolders.ProjectName, AppFolders.AnalysisName, _SelectedDocs[0], _DictionarySelected);
                    ucResultsDic1.Visible = true;
                    ucResultsDic1.Dock = DockStyle.Fill;
                    ucResultsDic1.adjColumns();

                    break;

                case Modes.AnalysisResults_CompareDocsDictionary:
                    Butnext.Visible = false;
                    panFooter.Visible = true;
                    ucTasks1.CurrentStep = _TaskNo; // 2 3rd Step -- Zero based
                    ucTasks1.Visible = true;

                    ucResultsMultiDic1.LoadData(_ConceptDicAnalyzer, AppFolders.ProjectName, AppFolders.AnalysisName, _DictionarySelected);
                    ucResultsMultiDic1.Visible = true;
                    ucResultsMultiDic1.Dock = DockStyle.Fill;
                    ucResultsMultiDic1.adjColumns();

                    break;

                case Modes.AnalysisResults_QCReadability:
                    Butnext.Visible = false;
                    panFooter.Visible = true;
                    ucTasks1.Visible = false;

                    // data was already loaded
                    ucQCAnalysisResults1.Visible = true;
                    ucQCAnalysisResults1.Dock = DockStyle.Fill;
                    ucQCAnalysisResults1.AdjustColumns();

                    break;

                case Modes.AnalysisResults_CompareDocsDiff:
                    Butnext.Visible = false;
                    panFooter.Visible = true;
                    ucTasks1.Visible = false;

                // data was already loaded
                    ucDiffSxS1.Visible = true;
                    ucDiffSxS1.Dock = DockStyle.Fill;

                    break;

                case Modes.AnalysisResults_AcroSeeker:
                    Butnext.Visible = false;
                    panFooter.Visible = true;
                    ucTasks1.Visible = false;

                    ucAcroSeekerResults1.LoadData(AppFolders.AnalysisCurrent);
                    ucAcroSeekerResults1.Visible = true;
                    ucAcroSeekerResults1.Dock = DockStyle.Fill;

                    break;

                case Modes.AnalysisResults_DeepAnalysis:
                    Butnext.Visible = false;
                    panFooter.Visible = true;
                    ucTasks1.Visible = false;

                    ucDeepAnalyticsResults1.LoadData();
                    ucDeepAnalyticsResults1.Visible = true;
                    ucDeepAnalyticsResults1.AdjustColumns();
                    ucDeepAnalyticsResults1.Dock = DockStyle.Fill;

                    break;

            }
        }

        private void HideAllPrimaryButtons()
        {

        }

        private void HideAllUCs()
        {

            panHeader.Visible = false;
            ucTasks1.Visible = false;
            panFooter.Visible = false;

            ucStart1.Visible = false;
            ucWelcome1.Visible = false;
            ucTasksSelect1.Visible = false;
            ucProjectsDocs1.Visible = false;
            ucKeywordsSelect1.Visible = false;
            ucAnalysisResults1.Visible = false;
            ucSettings1.Visible = false;
            ucResultsConcepts1.Visible = false;
            ucResultsMultiConcepts1.Visible = false;
            ucDictionarySelect1.Visible = false;
            ucResultsDic1.Visible = false;
            ucResults1.Visible = false;
            ucQCAnalysisResults1.Visible = false;
            ucDiffSxS1.Visible = false;
            ucAcroSeekerResults1.Visible = false;
            ucDeepAnalyticsResults1.Visible = false;

            Butnext.Visible = false;

        }

        private void Butnext_MouseEnter(object sender, EventArgs e)
        {
            Butnext.BackColor = Color.Lime;
        }

        private void Butnext_MouseLeave(object sender, EventArgs e)
        {
            Butnext.BackColor = Color.DarkGreen;
        }

        private void butBack_MouseEnter(object sender, EventArgs e)
        {
            butBack.BackColor = Color.Blue; 
        }

        private void butBack_MouseLeave(object sender, EventArgs e)
        {
            butBack.BackColor = Color.Navy;
        }

        private void butBackToWelcome_MouseEnter(object sender, EventArgs e)
        {
            butBackToWelcome.BackColor = Color.White;
            butBackToWelcome.ForeColor = Color.FromArgb(0, 3, 51);
        }

        private void butBackToWelcome_MouseLeave(object sender, EventArgs e)
        {
            butBackToWelcome.BackColor = Color.FromArgb(0, 3, 51);
            butBackToWelcome.ForeColor = Color.White;
        }

        private bool Parse_Analyze()
        {
             Cursor.Current = Cursors.WaitCursor; // Waiting

            
            _TaskNo = 1;

            this.ucTasks1.UpdateProcessStatus("Starting Analysis ...", true);

            foreach (string doc in _SelectedDocs)
            {
                _AnalyzerManager = new AnalyzerManager();
                AppFolders.DocName = doc;
                string[] currentDoc = new string[] {doc};
                string previousParseType = GetPreviousParseType4Defualt();
                _AnalyzerManager.DocumentFilesNames = currentDoc;
                _AnalyzerManager.isUseDefaultParseAnalysis = ucProjectsDocs1.isUseDefaultParseAnalysis;

                _isUseDefaultParseAnalysis = ucProjectsDocs1.isUseDefaultParseAnalysis;

                _AnalyzerManager.AnalysisName = ucProjectsDocs1.AnalysisName;
                AppFolders.AnalysisName = ucProjectsDocs1.AnalysisName;

                _ParsedSecPath = _AnalyzerManager.ParsedSecPath;
                _ParsedSecXMLPath = _AnalyzerManager.ParsedSecXMLPath;
                _ParsedSecNotesPath = _AnalyzerManager.ParsedSecNotesPath;
                _ParsedSecIndexPath = _AnalyzerManager.ParsedSecIndexPath;
                _ParsedSecExportPath = _AnalyzerManager.ParsedSecExportPath;

                bool parseTypelegal = false;
                if (ucProjectsDocs1.ParseType == "Legal")
                {
                    parseTypelegal = true;
                    _AnalysisResultsType = AnalysisResultsType.Selection.Logic_Segments;
                }
                else
                {
                    parseTypelegal = false;
                    _AnalysisResultsType = AnalysisResultsType.Selection.Paragraph_Segments;
                }

                this.ucTasks1.UpdateProcessStatus(string.Concat("Parsing Document: ", doc, " ..."), true);
                bool useNumericHierarchy = ucProjectsDocs1.UseNumericHierarchy;
                //_AnalyzerManager.ParseDocuments2(parseTypelegal, useNumericHierarchy, ucProjectsDocs1.isUseDefaultParseAnalysis);
                _AnalyzerManager.ParseDocuments2(parseTypelegal, false, ucProjectsDocs1.isUseDefaultParseAnalysis);  
                string errMsg = _AnalyzerManager.ErrorMessage;

                if (errMsg.Length > 0)
                {
                    errMsg = string.Concat("Document: ", doc, " ", errMsg);
                    this.ucTasks1.UpdateProcessStatus("errMsg", false);
                    MessageBox.Show(errMsg, "Analysis Error(s) Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Cursor.Current = Cursors.Default;
                    return false; 
                }

                this.ucTasks1.UpdateProcessStatus(string.Concat("Mapping the Document ", doc, " pages (only for DOCX and PDF file types) ..."), true);
                _AnalyzerManager.MapPages(ucProjectsDocs1.isUseDefaultParseAnalysis);

                this.ucTasks1.UpdateProcessStatus(string.Concat("Indexing the Document ", doc, " for the Search function..."), true);
                Indexer indexer = new Indexer();
                if (ucProjectsDocs1.isUseDefaultParseAnalysis) // Results held under the orginal DA path structure. Use for Quick CM and Requirement Matrix
                {
                    if (!indexer.CreateIndex(AppFolders.DocParsedSecIndex2, AppFolders.DocParsedSec)) // if Lucene fails (Lucene search index does not work in Windows FIPS mode), they try Hoot
                    {
                        frmHootProcessing frm = new frmHootProcessing();
                        frm.CreateIndex(AppFolders.DocParsedSecIndex, AppFolders.DocParsedSec);
                        Application.DoEvents();
                        frm.Close();
                    }
                }
                else
                {
                    if (!indexer.CreateIndex(AppFolders.AnalysisParseSegIndex2, AppFolders.AnalysisParseSeg)) // if Lucene fails (Lucene search index does not work in Windows FIPS mode), they try Hoot
                    {
                        frmHootProcessing frm = new frmHootProcessing();
                        frm.CreateIndex(AppFolders.DocParsedSecIndex, AppFolders.DocParsedSec);
                        Application.DoEvents();
                        frm.Close();
                    }
                }

            }
                    




            //_TaskNo++;

            //_TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);

            //_TaskProcessObject = _TaskManager.Task_ProcessObject_Current;

            return true;
        }

        private void Butnext_Click(object sender, EventArgs e)
        {
             _TaskNo++;

            if (Butnext.Text == "Create")
            {
                if (_TaskManager.Task_ProcessObject_Current == Atebion.Tasks.ProcessObject.CreateXRefMatrix)
                {
                    string pathFile_MatrixBuilder = Path.Combine(Application.StartupPath, "MatrixBuilder.exe");
                  //  pathFile_MatrixBuilder = @"I:\Tom\Atebion\ProfessionalDocAnalyzer\Atebion_MatrixBuilder\MatrixBuilder\MatrixBuilder\bin\Debug\MatrixBuilder.exe"; //ToDo Remove after Testing
                    if (File.Exists(pathFile_MatrixBuilder))
                    {
                        
                        string parameters = string.Concat("Action=GoTo_CreateMatrix", " ", "Workgroup=", AppFolders.WorkgroupName, " ", "Project=", AppFolders.ProjectName, " ", "WorkgroupRootPath=", AppFolders.WorkgroupRootPath);

                        //System.Diagnostics.Process.Start(pathFile_MatrixBuilder, parameters);

                        Process proc = new Process();
                        proc.StartInfo.FileName = pathFile_MatrixBuilder;
                        proc.StartInfo.Arguments = parameters;
                        proc.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                        proc.Start();
                      //  proc.WaitForExit();

                        
                    }
                    else
                    {
                        string mbNotFoundMsg = "Unable to find the Matrix Builder.";
                        MessageBox.Show(mbNotFoundMsg, "Matrix Builder NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return;
                    }
                }
            }
            else if (Butnext.Text == "X-Ref")
            {

                //GoTo_Matrices

                string pathFile_MatrixBuilder = Path.Combine(Application.StartupPath, "MatrixBuilder.exe");
               // pathFile_MatrixBuilder = @"I:\Tom\Atebion\ProfessionalDocAnalyzer\Atebion_MatrixBuilder\MatrixBuilder\MatrixBuilder\bin\Debug\MatrixBuilder.exe"; //ToDo Remove after Testing
                if (File.Exists(pathFile_MatrixBuilder))
                {

                    string parameters = string.Concat("Action=GoTo_Matrices", " ", "Workgroup=", AppFolders.WorkgroupName, " ", "Project=", AppFolders.ProjectName, " ", "WorkgroupRootPath=", AppFolders.WorkgroupRootPath);

                    //System.Diagnostics.Process.Start(pathFile_MatrixBuilder, parameters);

                    Process proc = new Process();
                    proc.StartInfo.FileName = pathFile_MatrixBuilder;
                    proc.StartInfo.Arguments = parameters;
                    proc.StartInfo.WindowStyle = ProcessWindowStyle.Maximized;
                    proc.Start();
                    //  proc.WaitForExit();


                }
                else
                {
                    string mbNotFoundMsg = "Unable to find the Matrix Builder.";
                    MessageBox.Show(mbNotFoundMsg, "Matrix Builder NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }
            }
            else if (Butnext.Text == "Next")
            {
                _PreviousMode = _currentMode;

                if (ucProjectsDocs1.Visible)
                {
                    if (ucProjectsDocs1.Next_TaskProcessObject == Atebion.Tasks.ProcessObject.FindKeywordsPerLib)
                    {
                        _currentMode = Modes.KeywordsSelection;
                    }
                    else if (ucProjectsDocs1.Next_TaskProcessObject == Atebion.Tasks.ProcessObject.FindDictionaryTerms)
                    {
                        _currentMode = Modes.DictionarySelection;
                    }
                    else if (ucProjectsDocs1.Next_TaskProcessObject == Atebion.Tasks.ProcessObject.CompareDocsDictionary)
                    {
                        _currentMode = Modes.DictionarySelection;
                    }
                }
                else
                {

                    _currentMode = _currentMode + 1;
                }
                ModeAdjustments();
            }
            else if (Butnext.Text == "Analyze")
            {
                string parseType = string.Empty;

                _AnalyzerManager = new AnalyzerManager();

                List<string> docFiles = ucProjectsDocs1.Documents_Selected;
                _SelectedDocs = docFiles.ToArray();


                if (_currentMode == Modes.ProjDocs || _currentMode == Modes.KeywordsSelection || _currentMode == Modes.DictionarySelection)
                {

                    _TaskManager = ucProjectsDocs1.TaskManager;

                 //   _AnalyzerManager.DocumentFilesNames = this.ucProjectsDocs1.Documents_Selected.ToArray<string>();
                    _AnalyzerManager.DocumentFilesNames = _SelectedDocs;


                    if (_SelectedDocs.Length == 0)
                    {
                        MessageBox.Show("Plese select a document.", "No Document has been Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        return;
                    }

                    if (ucProjectsDocs1.Task_ProcessObject == ProcessObject.Parse || _currentMode == Modes.KeywordsSelection)
                    {
                        if (!Parse_Analyze())
                            return;


                      //  _TaskNo++;

                        _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);

                        _TaskProcessObject = _TaskManager.Task_ProcessObject_Current;
                    }
                    else if (ucProjectsDocs1.Task_ProcessObject == ProcessObject.AcroSeeker)
                    {
                        if (_SelectedDocs.Length == 0)
                        {
                            MessageBox.Show("Please select one or more documents to analyze.", "1 or More Documents are Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        this.ucTasks1.UpdateProcessStatus("Analyzing Document(s) for identifying and validating acronyms…", true);

                        DataView dv =_TaskManager.GetTaskPropertiesAndAttributes(0);
                        DataRow taskRow;
                        string dictionary1 = string.Empty;
                        string ignoreDictionary = string.Empty;
                        foreach (DataRowView rowView in dv)
                        {
                            taskRow = rowView.Row;
                            // Dictionary
                            if (taskRow[Atebion.Tasks.Attributes.Attribute_Name].ToString() == Atebion.Tasks.AcroSeeker_Attributes.Dictionary1)
                            {
                                if (taskRow[Atebion.Tasks.Attributes.Attribute_Value] != null)
                                {
                                    dictionary1 = taskRow[Atebion.Tasks.Attributes.Attribute_Value].ToString();
                                }
                            }

                            // Ignore Dictionary
                            if (taskRow[Atebion.Tasks.Attributes.Attribute_Name].ToString() == Atebion.Tasks.AcroSeeker_Attributes.IgnoreDictionary)
                            {
                                if (taskRow[Atebion.Tasks.Attributes.Attribute_Value] != null)
                                {
                                    ignoreDictionary = taskRow[Atebion.Tasks.Attributes.Attribute_Value].ToString();
                                }
                            }

                        }


                        _AnalyzerManager = new AnalyzerManager();

                        _AnalyzerManager.DocumentFilesNames = _SelectedDocs;

                        Cursor.Current = Cursors.WaitCursor; // Waiting

                        if (!_AnalyzerManager.AcroSeekerAnalyze(ucProjectsDocs1.AnalysisName, AppFolders.AppModel, dictionary1, AppFolders.AppDataPathToolsAcroSeekerDefLib, ignoreDictionary, AppFolders.AppDataPathToolsAcroSeekerIgnoreLib)) 
                        {
                            Cursor.Current = Cursors.Default; // Done
                            MessageBox.Show(_AnalyzerManager.ErrorMessage, "Unable to Analyze Document(s)", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        _currentMode = Modes.AnalysisResults_AcroSeeker;

                        ModeAdjustments();

                        Cursor.Current = Cursors.Default; // Done

      

                    }
                    else if (ucProjectsDocs1.Task_ProcessObject == Atebion.Tasks.ProcessObject.CompareDocsDiff)
                    {
                        //_SelectedDocs

                        if (_SelectedDocs.Length > 2)
                        {
                            MessageBox.Show("Please select two documents to compare.", "2 Documents are Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        if (_SelectedDocs.Length < 2)
                        {
                            MessageBox.Show("Please select only two documents to compare.", "Only 2 Documents are Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }

                        Cursor.Current = Cursors.WaitCursor; // Waiting
                        ucTasks1.UpdateProcessStatus("Running the Diff comparison analysis for the two selected documents… ", true);

                        //string pathDoc = Path.GetDirectoryName(_SelectedDocs[0]);
                        //string doc1PathFile = GetCurrentDocument(pathDoc);

                        //pathDoc = Path.GetDirectoryName(_SelectedDocs[1]);
                        //string doc2PathFile = GetCurrentDocument(pathDoc);

                        // Old & New Docs -- Added 06.23.2020
                        string doc_New = string.Empty;
                        string doc_Old = string.Empty;
                        
                        string diff_OldDoc = ucProjectsDocs1.DiffOldDoc;
                        if (diff_OldDoc.Length > 0)
                        {
                            if (_SelectedDocs[0] == diff_OldDoc)
                            {
                                doc_Old = _SelectedDocs[0];
                                doc_New = _SelectedDocs[1];
                            }
                            else
                            {
                                doc_New = _SelectedDocs[0];
                                doc_Old = _SelectedDocs[1];
                            }
                        }
                        else
                        {
                            doc_Old = _SelectedDocs[0];
                            doc_New = _SelectedDocs[1];

                        }

                        //if (!_AnalyzerManager.CompareDocsDiff(_SelectedDocs[0], _SelectedDocs[1], ucProjectsDocs1.AnalysisName))
                        if (!_AnalyzerManager.CompareDocsDiff(doc_Old, doc_New, ucProjectsDocs1.AnalysisName))
                        {
                            Cursor.Current = Cursors.Default;
                            MessageBox.Show(_AnalyzerManager.ErrorMessage, "Unable to do Different Comparisons between Documents", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            return;
                        }
                        if (!ucDiffSxS1.LoadData(_AnalyzerManager.AnalysisPath, AppFolders.AnalysisCurrentDiffNotes, AppFolders.AnalysisCurrentDiffNotesHTML, AppFolders.AnalysisCurrentDiffModsPart, AppFolders.AnalysisCurrentDiffMods, AppFolders.AnalysisCurrentDiffModsWhole))
                        {
                            Cursor.Current = Cursors.Default;
                            return;
                        }

                        _currentMode = Modes.AnalysisResults_CompareDocsDiff;
                        ModeAdjustments();

                        // ToDo open Diff Analysis Results

                        Cursor.Current = Cursors.Default; // Done
                        ucTasks1.Visible = false;
                    }

                    //    Cursor.Current = Cursors.WaitCursor; // Waiting

 
                    
                }
                if (_TaskProcessObject == Atebion.Tasks.ProcessObject.CompareDocsDictionary)
                {
                    Cursor.Current = Cursors.WaitCursor; // Waiting

                    _dvAttributes = _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);

                    if (!ucDictionarySelect1.Visible)
                    {
                        DataView dv = _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);

                        foreach (DataRowView taskRow in dv)
                        {
                            if (taskRow[Atebion.Tasks.Attributes.Attribute_Name].ToString() == Atebion.Tasks.FindDictionaryTerms_Attributes.FindSynonyms)
                            {
                                if (taskRow[Atebion.Tasks.Attributes.Attribute_Value].ToString().ToUpper() == "YES")
                                {
                                    _SynonymsFind = true;
                                }
                                else
                                {
                                    _SynonymsFind = false;
                                }
                            }

                            if (taskRow[Atebion.Tasks.Attributes.Attribute_Name].ToString() == Atebion.Tasks.FindDictionaryTerms_Attributes.FindWholeWords)
                            {
                                if (taskRow[Atebion.Tasks.Attributes.Attribute_Value].ToString().ToUpper() == "YES")
                                {
                                    _WholeWordsFind = true;
                                }
                                else
                                {
                                    _WholeWordsFind = false;
                                }
                            }

                        }
                    }
                    
                    {
                        DataRow row;
                        foreach (DataRowView drv in _dvAttributes)
                        {
                            row = drv.Row;

                            if (row[Attributes.Attribute_Name].ToString() == FindDictionaryTerms_Attributes.UseDictionaryLibrary)
                            {
                                _DictionarySelected = row[Attributes.Attribute_Value].ToString();
                            }
                            else if (row[Attributes.Attribute_Name].ToString() == FindDictionaryTerms_Attributes.UserSelectsDictionaryLib)
                            {
                                _DictionarySelected = ucDictionarySelect1.DictionarySelected;
                            }
                            if (row[Attributes.Attribute_Name].ToString() == FindDictionaryTerms_Attributes.FindWholeWords)
                            {
                                string wholewords = row[Attributes.Attribute_Value].ToString();
                                if (wholewords.ToUpper() == "YES")
                                {
                                    _WholeWordsFind = true;
                                }
                                else
                                {
                                    _WholeWordsFind = false;
                                }
                            }
                            if (row[Attributes.Attribute_Name].ToString() == FindDictionaryTerms_Attributes.FindSynonyms)
                            {
                                string findSynonyms = row[Attributes.Attribute_Value].ToString();
                                if (findSynonyms.ToUpper() == "YES")
                                {
                                    _SynonymsFind = true;
                                }
                                else
                                {
                                    _SynonymsFind = false;
                                }

                            }


                        }
                    }

                    _AnalyzerManager.AnalysisName = ucProjectsDocs1.AnalysisName;

                    AppFolders.AnalysisName = ucProjectsDocs1.AnalysisName;

                    _AnalysisPath = AppFolders.AnalysisPath;



                    if (_AnalyzerManager == null)
                        _AnalyzerManager = new AnalyzerManager();

                    _SearchType = AnalysisResultsType.SearchType.Dictionary;

                    Cursor.Current = Cursors.WaitCursor; // Waiting

                    foreach (string doc in _SelectedDocs)
                    {

                        this.ucTasks1.UpdateProcessStatus("Finding Dictionary Terms...", true);

                        AppFolders.DocName = doc;

                        _ConceptDicAnalyzer = new Analysis(_AnalysisPath, AppFolders.AppDataPath, AppFolders.ProjectName);

                        if ((ucDictionarySelect1.Visible))
                        {
                            _DictionarySelected = ucDictionarySelect1.DictionarySelected;
                        }

                        if (!_ConceptDicAnalyzer.Analyze4DictionaryTerms(AppFolders.ProjectName, AppFolders.AnalysisName, _DictionarySelected, AppFolders.ProjectCurrent, AppFolders.DictionariesPath, AppFolders.AnalysisParseSegXML, AppFolders.AnalysisParseSeg, doc, _WholeWordsFind, _SynonymsFind, false))
                        {
                            string errMsg = string.Concat("Exiting the process!", Environment.NewLine, Environment.NewLine, _ConceptDicAnalyzer.ErrorMessage);
                            string errCaption = string.Concat("Unable to Find Dictionary Terms for Document '", doc, "'");
                            MessageBox.Show(errMsg, errCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);

                            return;
                        }

                    }

                    string XRefSumPathFile = string.Empty;

                    DataSet dsDicFilter;

                    this.ucTasks1.UpdateProcessStatus("Comparing Document per Dictionary Terms...", true);

                    if ((ucDictionarySelect1.Visible))
                    {
                        _DictionarySelected = ucDictionarySelect1.DictionarySelected;
                    }
                    if (!_ConceptDicAnalyzer.Analyze4DictionaryDocsCompare(AppFolders.ProjectName, AppFolders.AnalysisName, _SelectedDocs, _DictionarySelected, AppFolders.DictionariesPath, out XRefSumPathFile, out dsDicFilter))
                    {
                        string errMsg = string.Concat("Exiting the process!", Environment.NewLine, Environment.NewLine, _ConceptDicAnalyzer.ErrorMessage);
                        MessageBox.Show(errMsg, "An Error Occured", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        this.ucTasks1.UpdateProcessStatus(string.Concat("Error: ", _ConceptDicAnalyzer.ErrorMessage), false);
                        Cursor.Current = Cursors.Default;

                        return;
                    }

                 //   this.ucTasks1.UpdateProcessStatus("Completed", false);

                    _TaskNo++;

                    _dvAttributes = _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);

                    _TaskProcessObject = _TaskManager.Task_ProcessObject_Current;

                    if (_TaskProcessObject == Atebion.Tasks.ProcessObject.DisplayAnalysisResults)
                    {
                        _currentMode = Modes.AnalysisResults_CompareDocsDictionary;

                        ModeAdjustments();

                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    else
                    {
  

                     bool useWeightedColors = _TaskManager.UsedDicReportWeightColors(_Task_Current); // Used with Reports

                        string excelTemplateName = string.Empty;
                        _TaskManager.GetExportFileType(_Task_Current, out excelTemplateName);

                        string xRefPathFile = string.Empty;
                        DataSet dsDicFiltering;

                        //Cleanup and force to regenerate files
                        File.Delete(Path.Combine(AppFolders.AnalysisCurrent, DocsDictionariesAnalysisFieldConst.xDocsTotalsFile));
                        File.Delete(Path.Combine(AppFolders.AnalysisCurrent, DocsDictionariesAnalysisFieldConst.XMLFile));
                        File.Delete(Path.Combine(AppFolders.AnalysisCurrent, DocsDictionariesAnalysisFieldConst.xRefFile));
                        File.Delete(Path.Combine(AppFolders.AnalysisCurrent, DocsDictionariesAnalysisFieldConst.xFilterFile));
                        File.Delete(Path.Combine(AppFolders.AnalysisCurrent, DocsDictionariesAnalysisFieldConst.xDocsTotalsFile));

                        DataSet dsDicDocsSumResults = _ConceptDicAnalyzer.Get_Documents_Dic_Summary(AppFolders.ProjectName, AppFolders.AnalysisName, _SelectedDocs, _DictionarySelected, AppFolders.DictionariesPath, out xRefPathFile, out dsDicFiltering);

                        DataView dv = dsDicDocsSumResults.Tables[0].DefaultView;
                        dv.Sort = string.Concat(DictionariesAnalysisFieldConst.Category, ",", DictionariesAnalysisFieldConst.DicItem);
                        DataTable dtSorted = dv.ToTable();

                        //DataView dv2 = dsDicFiltering.Tables[0].DefaultView;
                        //dv2.Sort = DocsDictionariesAnalysisFieldConst.DicItem;
                        //DataTable dtSorted2 = dv2.ToTable();

                        string summaryPathFile = Path.Combine(AppFolders.AnalysisCurrent, DocsDictionariesAnalysisFieldConst.xDocsTotalsFile);
                        if (!File.Exists(summaryPathFile))
                        {
                            string notfoundMsg = string.Concat("Unable to find Dictionary Document Compare Summary data file: ", summaryPathFile);
                            MessageBox.Show(notfoundMsg, "Summary Data File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.ucTasks1.UpdateProcessStatus(notfoundMsg, false);
                            Cursor.Current = Cursors.Default;
                            return;

                        }
                        GenericDataManger gDMgr = new GenericDataManger();
                        DataSet dsSummary = gDMgr.LoadDatasetFromXml(summaryPathFile);
                        

                       ExportManager exportMgr = new ExportManager();
                       string reportName = exportMgr.GetNewRptFileName(AppFolders.AnalysisCurrentCompareDocsReport);


                       if (!_ConceptDicAnalyzer.ExportDicDocs(dtSorted, dsSummary.Tables[0], _SelectedDocs, excelTemplateName, AppFolders.AppDataPathToolsExcelTempDicDocs, reportName, AppFolders.ProjectName, AppFolders.AnalysisName, useWeightedColors))
                       {
                           string errMsg = string.Concat("Error: ", _ConceptDicAnalyzer.ErrorMessage);
                           MessageBox.Show(errMsg, "Unable to Generate Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                           this.ucTasks1.UpdateProcessStatus(errMsg, false);
                           Cursor.Current = Cursors.Default;
                           return;
                       }
                        
                    }

                    this.ucTasks1.ShowTaskFlowStatus();

                    Cursor.Current = Cursors.Default;

                    _currentMode = Modes.ProjDocs;

                    ModeAdjustments();

                    //Cleanup analysis foler - operation
                    string cleanupDir = _AnalysisPath + "\\" + ucProjectsDocs1.AnalysisName + "\\Docs\\";
                    string[] dirs = Directory.GetDirectories(cleanupDir);

                    bool dist = false;
                    foreach (string dir in dirs)
                    {
                        foreach (string doc in _SelectedDocs)
                        {
                            if (dir.Split('\\').LastOrDefault() == doc)
                            {
                                dist = true;
                            }
                        }
                        if (dist == false)
                        {
                            // delete
                            //Console.WriteLine("path"+ cleanupDir + "\\" + dir.Split('\\').LastOrDefault());
                            Directory.Delete(cleanupDir + "\\" + dir.Split('\\').LastOrDefault(), true);
                        }

                        dist = false;
                    }

                    return;

                }
                if (_TaskProcessObject == Atebion.Tasks.ProcessObject.ReadabilityTest)
                {
                    Cursor.Current = Cursors.WaitCursor; // Waiting


                    string attributeValue = string.Empty;
                    bool Find_Adverbs = true;
                    bool Find_ComplexWords = true;
                    bool Find_LongSentences = true;
                    bool Find_PassiveVoice = true;
                    string dictionaryFileName = string.Empty;
                    int Words_LongSentences = 21;

                    // Get Attributes....
                    _dvAttributes = _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);
                    if (_dvAttributes == null)
                        return;

                    foreach (DataRowView taskRow in _dvAttributes)
                    {
                        //// Find Adverbs
                        //if (taskRow[Attributes.Attribute_Name].ToString() == ReadabilityTest_Attributes.Find_Adverbs)
                        //{
                        //    attributeValue = taskRow[Attributes.Attribute_Value].ToString();
                        //    if (attributeValue == "No")
                        //    {
                        //        Find_Adverbs = false;
                        //    }
                        //}

                        //// Find Complex Words
                        //if (taskRow[Attributes.Attribute_Name].ToString() == ReadabilityTest_Attributes.Find_ComplexWords)
                        //{
                        //    attributeValue = taskRow[Attributes.Attribute_Value].ToString();
                        //    if (attributeValue == "No")
                        //    {
                        //        Find_ComplexWords = false;
                        //    }
                        //}

                        //// Find Long Sentences
                        //if (taskRow[Attributes.Attribute_Name].ToString() == ReadabilityTest_Attributes.Find_LongSentences)
                        //{
                        //    attributeValue = taskRow[Attributes.Attribute_Value].ToString();
                        //    if (attributeValue == "No")
                        //    {
                        //        Find_LongSentences = false;
                        //    }
                        //}

                        //// Find Passive Voice
                        //if (taskRow[Attributes.Attribute_Name].ToString() == ReadabilityTest_Attributes.Find_PassiveVoice)
                        //{
                        //    attributeValue = taskRow[Attributes.Attribute_Value].ToString();
                        //    if (attributeValue == "No")
                        //    {
                        //        Find_PassiveVoice = false;
                        //    }
                        //}

                        //// Find Passive Voice
                        //if (taskRow[Attributes.Attribute_Name].ToString() == ReadabilityTest_Attributes.Find_PassiveVoice)
                        //{
                        //    attributeValue = taskRow[Attributes.Attribute_Value].ToString();
                        //    if (attributeValue == "No")
                        //    {
                        //        Find_PassiveVoice = false;
                        //    }
                        //}

                        // Use Dictionary Library
                        if (taskRow[Attributes.Attribute_Name].ToString() == ReadabilityTest_Attributes.UseDictionaryLibrary)
                        {
                            attributeValue = taskRow[Attributes.Attribute_Value].ToString();
                            if (attributeValue != "")
                            {
                                dictionaryFileName = attributeValue;
                            }
                        }

                        //if (taskRow[Attributes.Attribute_Name].ToString() == ReadabilityTest_Attributes.Words_LongSentences)
                        //{
                        //    attributeValue = taskRow[Attributes.Attribute_Value].ToString();
                        //    if (attributeValue != "")
                        //    {
                        //        Words_LongSentences = Convert.ToInt32(attributeValue);
                        //    }
                        //}
                    }

                    string s = AppFolders.AppDataPath;
                    s = AppFolders.AppDataPathTools;
                    string qcSettingsPath = AppFolders.AppDataPathToolsQC;

                    Atebion.QC.Analysis qcAnalysis = new Atebion.QC.Analysis(qcSettingsPath);

                    // Set QC Analysis properties
                    qcAnalysis.Find_Adverbs = Find_Adverbs;
                    qcAnalysis.Find_ComplexWords = Find_ComplexWords;
                    qcAnalysis.Find_LongSentence = Find_LongSentences;
                    qcAnalysis.Find_PassiveVoice = Find_PassiveVoice;
                    


                    // QCAnalysis2 qcAnalysis = new QCAnalysis2();

                    string xmlPath = _ParsedSecXMLPath; // @"F:\QCTest1\T-ADC(X) Ship RFP Section C 600\Current\ParseSec\XML";
                    string parseSegPath = _ParsedSecPath;  // @"F:\QCTest1\T-ADC(X) Ship RFP Section C 600\Current\ParseSec";
                    string lsqcPath = AppFolders.AnalysisParseSegLSQC; //@"F:\QCTest1\T-ADC(X) Ship RFP Section C 600\Current\ParseSec\LSQC";
                    string modelPath = AppFolders.AppModel; // @"I:\Tom\Atebion\ProfessionalDocAnalyzer\ProfessionalDocAnalyzer\bin\Debug\Model";

                    string dictionary = string.Empty;
                    if (dictionaryFileName != string.Empty)
                    {
                        string dictionaryPath = AppFolders.DictionariesPath;
                        string dictionaryFile = string.Concat(dictionaryFileName, ".dicx");
                        dictionary = Path.Combine(dictionaryPath, dictionaryFile);    //@"F:\Test6\Dictionaries\Words2Avoid.dicx";
                    }

                   // string document = GetCurrentDocument(true); //@"F:\QCTest1\T-ADC(X) Ship RFP Section C 600\Current\T-ADC(X) Ship RFP Section C.docx";

                    Cursor.Current = Cursors.WaitCursor;

                    qcAnalysis.ComplexWords_SyllableCountGreaterThan = 4;
                    qcAnalysis.AnalyzeDocs(xmlPath, parseSegPath, lsqcPath, modelPath, dictionary);

                    string parentPath = Directory.GetParent(parseSegPath).FullName;
                    string docFileName = GetCurrentDocument(parentPath);
                    ucQCAnalysisResults1.LoadData(parseSegPath, xmlPath, docFileName);

                    _currentMode = Modes.AnalysisResults_QCReadability;

                    ModeAdjustments();

                //    HideAllUCs();

                    Cursor.Current = Cursors.Default; // Back to normal

                    //ucQCAnalysisResults1.Visible = true;

                    //ucQCAnalysisResults1.Dock = DockStyle.Fill;

                    //ucQCAnalysisResults1.AdjustColumns();

                }
                if (_TaskProcessObject == Atebion.Tasks.ProcessObject.GenerateRAMRpt)
                {
                    Cursor.Current = Cursors.WaitCursor; // Waiting

                    // Get Attributes....
                    _dvAttributes = _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);

                    if (_dvAttributes == null)
                        return;

                    string RAMTemplate = string.Empty;
                    bool findSynonyms = false;
                    bool useColor = false;

                    foreach (DataRowView taskRow in _dvAttributes)
                    {
                        if (taskRow[Attributes.Attribute_Name].ToString() == GenerateRAM_Attributes.UseRAMTemplate)
                        {
                            RAMTemplate = taskRow[Attributes.Attribute_Value].ToString();
                        }
                        if (taskRow[Attributes.Attribute_Name].ToString() == GenerateRAM_Attributes.FindSynonyms)
                        {
                            string sFindSynonyms = taskRow[Attributes.Attribute_Value].ToString();
                            if (sFindSynonyms == "Yes")
                            {
                                findSynonyms = true;
                            }
                            else
                            {
                                findSynonyms = false;
                            }
                        }
                        if (taskRow[Attributes.Attribute_Name].ToString() == GenerateRAM_Attributes.UseColor)
                        {
                            string sUseColor = taskRow[Attributes.Attribute_Value].ToString();
                            if (sUseColor == "Yes")
                            {
                                useColor = true;
                            }
                            else
                            {
                                useColor = false;
                            }

                        }  
                    }


                    AppFolders.AnalysisName = ucProjectsDocs1.AnalysisName;

                    _AnalysisPath = AppFolders.AnalysisPath;

                    // Check if the RAM Template Exists
                    string templateFile = string.Concat(RAMTemplate, ".xml");
                    string templatePathFile = Path.Combine(AppFolders.AppDataPathToolsExcelTempDicRAM, templateFile);
                    if (!File.Exists(templatePathFile))
                    {
                            string errMsg = string.Concat("Unable to find the RAM Template: ", templatePathFile );
                            MessageBox.Show(errMsg, "RAM Template Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.ucTasks1.UpdateProcessStatus(errMsg, false);
                            Cursor.Current = Cursors.Default;

                            return;
                    }

                    if (_AnalyzerManager == null)
                        _AnalyzerManager = new AnalyzerManager();

                    _SearchType = AnalysisResultsType.SearchType.Dictionary;

                    // Get RAM Template configuration data
                    GenericDataManger gMgr = new GenericDataManger();
                    DataSet dsRAMTemplate = gMgr.LoadDatasetFromXml(templatePathFile);
                    if (dsRAMTemplate == null)
                    {
                            string errMsg = string.Concat("Unable to read RAM Template data file: ", _DictionarySelected );
                            MessageBox.Show(errMsg, "Teamplate Data File Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.ucTasks1.UpdateProcessStatus(errMsg, false);
                            Cursor.Current = Cursors.Default;

                            return;

                    }

                    // Get Dictionary Name from RAM Template xml file
                    ResponsAssigMatrix RAM_mgr = new ResponsAssigMatrix();
                    string modelName = string.Empty;

                    string dicName = RAM_mgr.Get_DictionaryName_FromRAMTemplate(dsRAMTemplate, out modelName); //_DictionarySelected
                    if (dicName.Length == 0)
                    {
                        string errMsg = string.Concat("Unable to find the Dictionary in the current RAM Template.  Error: ", RAM_mgr.ErrorMessage);
                        MessageBox.Show(errMsg, "Dictionary Not Identified", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ucTasks1.UpdateProcessStatus(errMsg, false);
                        Cursor.Current = Cursors.Default;

                        return;
                    }

                    // Check if the Dictionary file exists
                    string dicFile = string.Concat(dicName, ".dicx");
                    _DictionarySelected = Path.Combine(AppFolders.DictionariesPath, dicFile);
                    if (!File.Exists(_DictionarySelected))
                    {
                        string errMsg = string.Concat("Unable to find the Dictionary for the current RAM Template.  Dictionary: ", _DictionarySelected);
                        MessageBox.Show(errMsg, "Dictionary Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        this.ucTasks1.UpdateProcessStatus(errMsg, false);
                        Cursor.Current = Cursors.Default;

                        return;
                    }


                    // Loop through each selected document and generate RAM report
                    foreach (string doc in _SelectedDocs)
                    {
                       this.ucTasks1.UpdateProcessStatus("Finding Dictionary Terms...", true);

                        AppFolders.DocName = doc;

                        _ConceptDicAnalyzer = new Analysis(_AnalysisPath, AppFolders.AppDataPath, AppFolders.ProjectName);

                        // Find dictionary terms in current document
                        if (!_ConceptDicAnalyzer.Analyze4DictionaryTerms(AppFolders.ProjectName, AppFolders.AnalysisName, dicName, AppFolders.ProjectCurrent, AppFolders.DictionariesPath, AppFolders.AnalysisParseSegXML, AppFolders.AnalysisParseSeg, doc, true, _SynonymsFind, false))
                        {
                            string errMsg = string.Concat("Exiting the process!", Environment.NewLine, Environment.NewLine, _ConceptDicAnalyzer.ErrorMessage);
                            string errCaption = string.Concat("Unable to Find Dictionary Terms for Document '", doc, "'");
                            MessageBox.Show(errMsg, errCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.ucTasks1.UpdateProcessStatus(errMsg, false);
                            Cursor.Current = Cursors.Default;

                            return;
                        }

                        // Analysis Results
                        string pathFileXML = Path.Combine(AppFolders.AnalysisParseSegXML, "ParseResults.xml");
                        if (!File.Exists(pathFileXML))
                        {
                            string msg = string.Concat("Unable to find Analysis File: ", pathFileXML);
                            MessageBox.Show(msg, "Unable to Generate Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.ucTasks1.UpdateProcessStatus(msg, false);
                            Cursor.Current = Cursors.Default;
                                
                            return;
                        }
     
                        DataSet ds = gMgr.LoadDatasetFromXml(pathFileXML);
                        //

 

                        // Dictionary Analysis
                        pathFileXML = Path.Combine(AppFolders.AnalysisXML, "DicAnalysis.xml");
                        if (!File.Exists(pathFileXML))
                        {
                            string msg = string.Concat("Unable to find Dictionary Analysis File: ", pathFileXML);
                            MessageBox.Show(msg, "Unable to Generate Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.ucTasks1.UpdateProcessStatus(msg, false);
                            Cursor.Current = Cursors.Default;
                                
                            return;
                        }

                        DataSet dsDicAnalysis = gMgr.LoadDatasetFromXml(pathFileXML);
                        //

                        ExportManager exportMgr = new ExportManager();
                        string reportName = exportMgr.GetNewRptFileName(AppFolders.AnalysisParseSegExport);

                        this.ucTasks1.UpdateProcessStatus("Generating Report...", true);

                        if (!_ConceptDicAnalyzer.ExportDicDocRAM(ds.Tables[0], dsDicAnalysis.Tables[0], RAMTemplate, AppFolders.AppDataPathToolsExcelTempDicRAM, AppFolders.DictionariesPath, reportName, AppFolders.ProjectCurrent, AppFolders.AnalysisName, doc, useColor))
                        {
                            string errMsg = string.Concat("Error: ", _ConceptDicAnalyzer.ErrorMessage);
                            MessageBox.Show(errMsg, "An Error Occurred while Generating a RAM Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.ucTasks1.UpdateProcessStatus(errMsg, false);
                            Cursor.Current = Cursors.Default;

                            return;

                        }

                    }

                    // Hack to fix extra doc folder -- remove after source of doc folder creation found
                    string[] xfolders = Directory.GetDirectories(AppFolders.AnalysisCurrentDocs);
                    string[] xfiles;
                    if (xfolders.Length > 1)
                    {
                        foreach (string xfolder in xfolders)
                        {
                            xfiles = Directory.GetFiles(xfolder);
                            if (xfiles.Length == 0)
                            {
                                try
                                {
                                    Application.DoEvents();
                                    Directory.Delete(xfolder);
                                }
                                catch { }
                            }
                        }
                    }
                    // end hack

                    //Cleanup analysis foler - operation
                    string cleanupDir = _AnalysisPath + "\\" + ucProjectsDocs1.AnalysisName + "\\Docs\\";
                    string[] dirs = Directory.GetDirectories(cleanupDir);

                    bool dist = false;
                    foreach (string dir in dirs)
                    {
                        foreach (string doc in _SelectedDocs)
                        {
                            if (dir.Split('\\').LastOrDefault() == doc)
                            {
                                dist = true;
                            }
                        }
                        if (dist == false)
                        {
                            // delete
                            //Console.WriteLine("path"+ cleanupDir + "\\" + dir.Split('\\').LastOrDefault());
                            Directory.Delete(cleanupDir + "\\" + dir.Split('\\').LastOrDefault(), true);
                        }

                        dist = false;
                    }

                    this.ucTasks1.ShowTaskFlowStatus();
                    Cursor.Current = Cursors.Default;

                    GoBack(); // Go back to the Previous panel

                    return;

                }

                if (_TaskProcessObject == Atebion.Tasks.ProcessObject.FindDictionaryTerms)
                {
                    Cursor.Current = Cursors.WaitCursor; // Waiting

                    _dvAttributes = _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);

                    if (_dvAttributes == null)
                        return;

                    if (!ucDictionarySelect1.Visible)
                    {
                        DataView dv = _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);

                        if (dv == null)
                            return;

                        foreach (DataRowView taskRow in dv)
                        {
                            if (taskRow[Attributes.Attribute_Name].ToString() == FindDictionaryTerms_Attributes.UseDictionaryLibrary)
                            {
                                _DictionarySelected = taskRow[Attributes.Attribute_Value].ToString();
                            }
                            if (taskRow[Atebion.Tasks.Attributes.Attribute_Name].ToString() == Atebion.Tasks.FindDictionaryTerms_Attributes.FindSynonyms)
                            {
                                if (taskRow[Atebion.Tasks.Attributes.Attribute_Value].ToString().ToUpper() == "YES")
                                {
                                    _SynonymsFind = true;
                                }
                                else
                                {
                                    _SynonymsFind = false;
                                }
                            }

                            if (taskRow[Atebion.Tasks.Attributes.Attribute_Name].ToString() == Atebion.Tasks.FindDictionaryTerms_Attributes.FindWholeWords)
                            {
                                if (taskRow[Atebion.Tasks.Attributes.Attribute_Value].ToString().ToUpper() == "YES")
                                {
                                    _WholeWordsFind = true;
                                }
                                else
                                {
                                    _WholeWordsFind = false;
                                }
                            }

                        }
                    }
                    else // User Selects the Dictionary
                    {

                        _DictionarySelected = ucDictionarySelect1.DictionarySelected;
                        _WholeWordsFind = ucDictionarySelect1.WholeWordsFind;
                        _SynonymsFind = ucDictionarySelect1.SynonymsFind;

                        //DataRow row;
                        //foreach (DataRowView drv in _dvAttributes)
                        //{
                        //    row = drv.Row;

                        //    if (row[Attributes.Attribute_Name].ToString() == FindDictionaryTerms_Attributes.UseDictionaryLibrary)
                        //    {
                        //        _DictionarySelected = row[Attributes.Attribute_Value].ToString();
                        //    }
                        //    if (row[Attributes.Attribute_Name].ToString() == FindDictionaryTerms_Attributes.FindWholeWords)
                        //    {
                        //        string wholewords = row[Attributes.Attribute_Value].ToString();
                        //        if (wholewords.ToUpper() == "YES")
                        //        {
                        //            _WholeWordsFind = true;
                        //        }
                        //        else
                        //        {
                        //            _WholeWordsFind = false;
                        //        }
                        //    }
                        //    if (row[Attributes.Attribute_Name].ToString() == FindDictionaryTerms_Attributes.FindSynonyms)
                        //    {
                        //        string findSynonyms = row[Attributes.Attribute_Value].ToString();
                        //        if (findSynonyms.ToUpper() == "YES")
                        //        {
                        //            _SynonymsFind = true;
                        //        }
                        //        else
                        //        {
                        //            _SynonymsFind = false;
                        //        }

                        //    }


                        //}
                    }

                    _AnalyzerManager.AnalysisName = ucProjectsDocs1.AnalysisName;

                    AppFolders.AnalysisName = ucProjectsDocs1.AnalysisName;

                    _AnalysisPath = AppFolders.AnalysisPath;



                    if (_AnalyzerManager == null)
                        _AnalyzerManager = new AnalyzerManager();

                    _SearchType = AnalysisResultsType.SearchType.Dictionary;

                    Cursor.Current = Cursors.WaitCursor; // Waiting

                    foreach (string doc in _SelectedDocs)
                    {

                        this.ucTasks1.UpdateProcessStatus("Finding Dictionary Terms...", true);

                        AppFolders.DocName = doc;


                    //    string txtDocPathFile = ucProjectsDocs1.TxtDocPathFile;

                        // ToDo populate _DictionarySelected

                        _ConceptDicAnalyzer = new Analysis(_AnalysisPath, AppFolders.AppDataPath, AppFolders.ProjectName);

                        if (!_ConceptDicAnalyzer.Analyze4DictionaryTerms(AppFolders.ProjectName, AppFolders.AnalysisName, _DictionarySelected, AppFolders.ProjectCurrent, AppFolders.DictionariesPath, AppFolders.AnalysisParseSegXML, AppFolders.AnalysisParseSeg, doc, _WholeWordsFind, _SynonymsFind, false))
                        {
                            string errMsg = string.Concat("Exiting the process!", Environment.NewLine, Environment.NewLine, _ConceptDicAnalyzer.ErrorMessage);
                            string errCaption = string.Concat("Unable to Find Dictionary Terms for Document '", doc, "'");
                            MessageBox.Show(errMsg, errCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
                            this.ucTasks1.UpdateProcessStatus(errMsg, false);
                            Cursor.Current = Cursors.Default;

                            return;
                        }

                    }


                    _TaskNo++;

                    _dvAttributes = _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);

                    if (_dvAttributes == null)
                        return;


                    //Cleanup analysis foler - operation
                    string cleanupDir = _AnalysisPath + "\\" + ucProjectsDocs1.AnalysisName + "\\Docs\\";
                    string[] dirs = Directory.GetDirectories(cleanupDir);

                    bool dist = false;
                    foreach (string dir in dirs)
                    {
                        foreach (string doc in _SelectedDocs)
                        {
                            if (dir.Split('\\').LastOrDefault() == doc)
                            {
                                dist = true;
                            }
                        }
                        if (dist == false)
                        {
                            // delete
                            //Console.WriteLine("path"+ cleanupDir + "\\" + dir.Split('\\').LastOrDefault());
                            Directory.Delete(cleanupDir + "\\" + dir.Split('\\').LastOrDefault(), true);
                        }

                        dist = false;
                    }

                    _TaskProcessObject = _TaskManager.Task_ProcessObject_Current;

                    if (_TaskProcessObject == Atebion.Tasks.ProcessObject.DisplayAnalysisResults)
                    {
                        _currentMode = Modes.AnalysisResults_Dictionary;

                        ModeAdjustments();

                        Cursor.Current = Cursors.Default;
                        return;
                    }
                    else
                    {
                        bool useWeightedColors = _TaskManager.UsedDicReportWeightColors(_Task_Current); // Used with Reports

                        string excelTemplateName = string.Empty;
                        _TaskManager.GetExportFileType(_Task_Current, out excelTemplateName);
                     //   ExportManager.Modes exportMode = ExportManager.Modes.Excel;

                       

                        foreach (string doc in _SelectedDocs)
                        {
                            AppFolders.DocName = doc;

                            string pathFileXML = Path.Combine(AppFolders.AnalysisXML, "ParseResults.xml");
                            if (!File.Exists(pathFileXML))
                            {
                                string msg = string.Concat("Unable to find Analysis File: ", pathFileXML);
                                MessageBox.Show(msg, "Unable to Generate Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                this.ucTasks1.UpdateProcessStatus(msg, false);
                                Cursor.Current = Cursors.Default;
                                
                                return;
                            }


                            GenericDataManger gMgr = new GenericDataManger();
                            DataSet ds = gMgr.LoadDatasetFromXml(pathFileXML);

                            ExportManager exportMgr = new ExportManager();
                            string reportName = exportMgr.GetNewRptFileName(AppFolders.AnalysisParseSegExport);

                            if (!_ConceptDicAnalyzer.ExportDicDoc(ds.Tables[0], excelTemplateName, AppFolders.AppDataPathToolsExcelTempDicDoc, reportName, AppFolders.ProjectName, AppFolders.AnalysisName, doc, useWeightedColors))
                            {
                                string errMsg = string.Concat("Error: ", _ConceptDicAnalyzer.ErrorMessage);
                                MessageBox.Show(errMsg, "Unable to Generate Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                                this.ucTasks1.UpdateProcessStatus(errMsg, false);
                                Cursor.Current = Cursors.Default;
                                return;
                            }

                            _currentMode = Modes.ProjDocs;
                            ModeAdjustments();
                            //ExportManager_Dictionaries exportMgrDic = new ExportManager_Dictionaries();
                            //if (!exportMgrDic.Generate_ExportDicDoc(_ConceptDicAnalyzer, ds.Tables[0], AppFolders.ProjectName, AppFolders.AnalysisName, doc, excelTemplateName, useWeightedColors))
                            //{
                            //    string errMsg = string.Concat("Error: ", exportMgrDic.ErrorMessage);
                            //    MessageBox.Show(errMsg, "Unable to Generate Report", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            //    this.ucTasks1.UpdateProcessStatus(errMsg, false);
                            //    Cursor.Current = Cursors.Default;
                            //    return;
                            //}

                        }
                    }

                    this.ucTasks1.ShowTaskFlowStatus();
                    Cursor.Current = Cursors.Default;

                    return;
                    
                }

                //GetNextSet_AnalysisName
                if (_TaskProcessObject == Atebion.Tasks.ProcessObject.FindConcepts)
                {
                    Cursor.Current = Cursors.WaitCursor; // Waiting

                    _AnalyzerManager.AnalysisName = ucProjectsDocs1.AnalysisName;

                    _TaskNo++;

                    _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);


                    _TaskProcessObject = _TaskManager.Task_ProcessObject_Current;


                    if (_AnalyzerManager == null)
                        _AnalyzerManager = new AnalyzerManager();

                    _SearchType = AnalysisResultsType.SearchType.Concepts;

                    this.ucTasks1.UpdateProcessStatus("Finding Common Concepts...", true);

                    //  _AnalyzerManager.GetNextSet_AnalysisName(_Next_TaskProcessObject, true); // Generate new Analysis folder and set as current

                    _AnalysisPath = AppFolders.AnalysisPath;

                    string txtDocPathFile = ucProjectsDocs1.TxtDocPathFile;

                    _ConceptDicAnalyzer = new Analysis(_AnalysisPath, AppFolders.AppDataPath, AppFolders.ProjectName);

                    if (!_ConceptDicAnalyzer.Analyze4Concepts(AppFolders.ProjectName, AppFolders.AnalysisName, AppFolders.ProjectCurrent, _AnalyzerManager.DocumentFilesNames, false))
                    {
                        string errMsg = string.Concat("Error(s) have occured while finding Common Concepts.  Error(s): ", _ConceptDicAnalyzer.ErrorMessage);

                        MessageBox.Show(errMsg, "Common Concepts Error(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    }

                    Cursor.Current = Cursors.WaitCursor; // Waiting


                }
                else if (_TaskProcessObject == Atebion.Tasks.ProcessObject.CompareDocsConcepts)
                {
                    Cursor.Current = Cursors.WaitCursor; // Waiting

                    //_TaskNo++;

                    _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);

                    // _TaskProcessObject = _TaskManager.Task_ProcessObject_Current;

                    _DocsSelected = ucProjectsDocs1.Documents_Selected;

                    if (_DocsSelected.Count == 0)
                    {
                        MessageBox.Show("Cannot run analysis without selected Documents.", "No Documents Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        return;
                    }

                    if (_AnalyzerManager == null)
                        _AnalyzerManager = new AnalyzerManager();

                    _SearchType = AnalysisResultsType.SearchType.Concepts;

                    this.ucTasks1.UpdateProcessStatus("Finding Common Concepts between documents...", true);

                    _AnalysisPath = AppFolders.AnalysisPath;

                    _ConceptDicAnalyzer = new Analysis(_AnalysisPath, AppFolders.AppDataPath, AppFolders.ProjectName);

                    if (!_ConceptDicAnalyzer.Analyze4Concepts(AppFolders.ProjectName, AppFolders.AnalysisName, AppFolders.ProjectCurrent, _SelectedDocs, true))
                    {
                        string errMsg = string.Concat("Error(s) have occured while finding Common Concepts.  Error(s): ", _ConceptDicAnalyzer.ErrorMessage);

                        MessageBox.Show(errMsg, "Common Concepts Error(s)", MessageBoxButtons.OK, MessageBoxIcon.Error);

                        return;

                    }

                    _TaskNo++;

                    _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo);

                    _TaskProcessObject = _TaskManager.Task_ProcessObject_Current;

                    if (_TaskProcessObject == Atebion.Tasks.ProcessObject.DisplayAnalysisResults)
                    {
                        _currentMode = Modes.AnalysisResults_CompareDocsConcepts;
                        ModeAdjustments();

                    }
                    else
                    {

                    }



                }
                else if (_Next_TaskProcessObject == Atebion.Tasks.ProcessObject.FindKeywordsPerLib || _TaskProcessObject == Atebion.Tasks.ProcessObject.FindKeywordsPerLib)
                {
                    _SearchType = AnalysisResultsType.SearchType.Keywords;

                    if (!_TaskManager.GetDoesUserSelectsKeywordLib(_Task_Current))
                    {
                        Cursor.Current = Cursors.WaitCursor; // Waiting
                        _TaskNo++;

                        foreach (string doc in _SelectedDocs)
                        {
                        _AnalyzerManager = new AnalyzerManager();
                        AppFolders.DocName = doc;
                        string[] currentDoc = new string[] { doc };
                        string previousParseType = GetPreviousParseType4Defualt();
                        _AnalyzerManager.DocumentFilesNames = currentDoc;
                        _AnalyzerManager.isUseDefaultParseAnalysis = ucProjectsDocs1.isUseDefaultParseAnalysis;

                        _isUseDefaultParseAnalysis = ucProjectsDocs1.isUseDefaultParseAnalysis;

                        _AnalyzerManager.AnalysisName = ucProjectsDocs1.AnalysisName;
                        AppFolders.AnalysisName = ucProjectsDocs1.AnalysisName;

                        _ParsedSecPath = _AnalyzerManager.ParsedSecPath;
                        _ParsedSecXMLPath = _AnalyzerManager.ParsedSecXMLPath;
                        _ParsedSecNotesPath = _AnalyzerManager.ParsedSecNotesPath;
                        _ParsedSecIndexPath = _AnalyzerManager.ParsedSecIndexPath;
                        _ParsedSecExportPath = _AnalyzerManager.ParsedSecExportPath;

                        this.ucTasks1.UpdateProcessStatus("Finding Keywords...", true);

                        string keywordLibrary = _TaskManager.GetKeywordLibrary(_Task_Current);

                        _KeywordsMgr = new KeywordsMgr2();

                        // Get keywords per Library -- Use Keyword manager
                        string libFile = string.Concat(keywordLibrary, ".xml");
                        string libPathFile = Path.Combine(AppFolders.KeywordGrpPath, libFile);
                        if (!File.Exists(libPathFile))
                        {

                            string msg = string.Concat("Unable to find Keyword Library: ", libPathFile);
                            MessageBox.Show(msg, "Keyword Library Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.ucTasks1.UpdateProcessStatus(msg, false);
                            Cursor.Current = Cursors.Default;
                            return;
                        }

                        DataSet dsSelectedKeywords = _KeywordsMgr.GetKeywordsLib(libPathFile, "YellowGreen");

                        if (dsSelectedKeywords.Tables[0].Rows.Count == 0)
                        {
                            string msg = string.Concat("No Keywords were found in the Keyword Library: ", libPathFile);
                            MessageBox.Show(msg, "Keyword Library has no Keywords", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            this.ucTasks1.UpdateProcessStatus(msg, false);
                            Cursor.Current = Cursors.Default;
                            return;
                        }

                        // Write Keywords to a file 
                        List<string> lstOutKeywords = new List<string>();
                        foreach (DataRow row in dsSelectedKeywords.Tables[0].Rows)
                        {
                            lstOutKeywords.Add(row[KeywordsFoundFields.Keyword].ToString());
                        }
                        string[] outKeywords = lstOutKeywords.ToArray();

                        if (ucProjectsDocs1.isUseDefaultParseAnalysis) // Defualt
                        {
                            File.WriteAllLines(AppFolders.DocParsedSecKeywords + @"\Keywords.txt", outKeywords, Encoding.UTF8);
                        }
                        else // Analysis 
                        {
                            File.WriteAllLines(AppFolders.AnalysisParseSegKeywords + @"\Keywords.txt", outKeywords, Encoding.UTF8);
                        }

                        // Write IsWholeKeyword to a file
                        string isWholeKeyword = "No";
                        if (ucKeywordsSelect1.Visible)
                        {
                            if (ucKeywordsSelect1.isWholeKeyword)
                            {
                                isWholeKeyword = "Yes";
                            }
                        }
                        else
                        {
                            if (_TaskManager.GetKeywordIsWholeWord(_Task_Current))
                            {
                                isWholeKeyword = "Yes";
                            }
                        }

                        bool booIsWholeKeyword = false;
                        if (isWholeKeyword == "Yes")
                        {
                            booIsWholeKeyword = true;
                        }



                        if (ucProjectsDocs1.isUseDefaultParseAnalysis) // Defualt
                        {
                            Atebion.Common.Files.WriteStringToFile(isWholeKeyword, AppFolders.DocParsedSecKeywords + @"\isWholeKeywords.txt", true);
                        }
                        else // Analysis 
                        {
                            Atebion.Common.Files.WriteStringToFile(isWholeKeyword, AppFolders.AnalysisParseSegKeywords + @"\isWholeKeywords.txt", true);
                        }

                        // --- Find Keywords
                        int KeywordsFound = _KeywordsMgr.FindKeywords(dsSelectedKeywords, Color.YellowGreen, booIsWholeKeyword, ucProjectsDocs1.isUseDefaultParseAnalysis);

                        string file = string.Concat(AppFolders.DocName, ".txt");
                        string pathFile = Path.Combine(AppFolders.CurrentDocPath, file);

                        // Added 9.20.2018 - This fixes a bug caused when a User renames the default Document Name.
                        if (!File.Exists(pathFile))
                        {
                            string[] txt_Files = Directory.GetFiles(AppFolders.CurrentDocPath, "*.txt");
                            if (txt_Files.Length == 0)
                                return;

                            pathFile = Path.Combine(AppFolders.CurrentDocPath, txt_Files[0]);

                            if (ucProjectsDocs1.isUseDefaultParseAnalysis) // Default
                            {
                                this.ucTasks1.UpdateProcessStatus("Finding Keywords...", true);
                                _KeywordsMgr.FindKeywordsInDoc(pathFile, AppFolders.DocParsedSecKeywords, dsSelectedKeywords, booIsWholeKeyword);
                            }
                            else
                            {
                                this.ucTasks1.UpdateProcessStatus("Finding Keywords...", true);
                                _KeywordsMgr.FindKeywordsInDoc(pathFile, AppFolders.AnalysisParseSegKeywords, dsSelectedKeywords, booIsWholeKeyword);
                            }
                        }

                        if (ucProjectsDocs1.isUseDefaultParseAnalysis) // Defualt
                        {
                            GenerateKeywordsSelectedXMLFile(dsSelectedKeywords, keywordLibrary); // For Deep Analysis
                        }

                        this.ucTasks1.ShowTaskFlowStatus();
                        Cursor.Current = Cursors.Default;

                    }
                }
                    _Next_TaskProcessObject = _TaskManager.Next_TaskProcessObject;
                    _TaskNo++;
                }
                if (_currentMode == Modes.KeywordsSelection)
                {
                    _SearchType = AnalysisResultsType.SearchType.Keywords;

                    
                    int KeywordsQtySel = 0;

                    DataSet dsSelectedKeywords = ucKeywordsSelect1.GetSelectedKeywords(out KeywordsQtySel);

                    if (KeywordsQtySel > 0)
                    {
                        Cursor.Current = Cursors.WaitCursor; // Waiting

                        this.ucTasks1.UpdateProcessStatus("Finding Keywords...", true);

                        _TaskNo++;

                        foreach (string doc in _SelectedDocs)
                        {
                            _AnalyzerManager = new AnalyzerManager();
                            AppFolders.DocName = doc;
                            string[] currentDoc = new string[] { doc };
                            string previousParseType = GetPreviousParseType4Defualt();
                            _AnalyzerManager.DocumentFilesNames = currentDoc;
                            _AnalyzerManager.isUseDefaultParseAnalysis = ucProjectsDocs1.isUseDefaultParseAnalysis;

                            _isUseDefaultParseAnalysis = ucProjectsDocs1.isUseDefaultParseAnalysis;

                            _AnalyzerManager.AnalysisName = ucProjectsDocs1.AnalysisName;
                            AppFolders.AnalysisName = ucProjectsDocs1.AnalysisName;

                            // Write Keywords to a file 
                            string[] outKeywords = ucKeywordsSelect1.Keywords;

                            if (ucProjectsDocs1.isUseDefaultParseAnalysis) // Defualt
                            {
                                File.WriteAllLines(AppFolders.DocParsedSecKeywords + @"\Keywords.txt", outKeywords, Encoding.UTF8);
                            }
                            else // Analysis 
                            {
                                File.WriteAllLines(AppFolders.AnalysisParseSegKeywords + @"\Keywords.txt", outKeywords, Encoding.UTF8);
                            }

                            // Write IsWholeKeyword to a file
                            string isWholeKeyword = "No";
                            if (ucKeywordsSelect1.Visible)
                            {
                                if (ucKeywordsSelect1.isWholeKeyword)
                                {
                                    isWholeKeyword = "Yes";
                                }
                            }
                            else
                            {
                                if (_TaskManager.GetKeywordIsWholeWord(_Task_Current))
                                {
                                    isWholeKeyword = "Yes";
                                }
                            }

                            bool booIsWholeKeyword = false;
                            if (isWholeKeyword == "Yes")
                            {
                                booIsWholeKeyword = true;
                            }

                            if (ucProjectsDocs1.isUseDefaultParseAnalysis) // Defualt
                            {
                                Atebion.Common.Files.WriteStringToFile(isWholeKeyword, AppFolders.DocParsedSecKeywords + @"\isWholeKeywords.txt", true);
                            }
                            else // Analysis 
                            {
                                Atebion.Common.Files.WriteStringToFile(isWholeKeyword, AppFolders.AnalysisParseSegKeywords + @"\isWholeKeywords.txt", true);
                            }

                            _KeywordsMgr = new KeywordsMgr2();
                            // --- Find Keywords
                            int KeywordsFound = _KeywordsMgr.FindKeywords(dsSelectedKeywords, Color.YellowGreen, booIsWholeKeyword, ucProjectsDocs1.isUseDefaultParseAnalysis);

                            string file = string.Concat(AppFolders.DocName, ".txt");
                            string pathFile = Path.Combine(AppFolders.CurrentDocPath, file);
                            // Added 9.20.2018 - This fixes a bug caused when a User renames the default Document Name.
                            if (!File.Exists(pathFile))
                            {
                                string[] txt_Files = Directory.GetFiles(AppFolders.CurrentDocPath, "*.txt");
                                if (txt_Files.Length == 0)
                                    return;

                                pathFile = Path.Combine(AppFolders.CurrentDocPath, txt_Files[0]);

                                if (ucProjectsDocs1.isUseDefaultParseAnalysis) // Default
                                {
                                    _KeywordsMgr.FindKeywordsInDoc(pathFile, AppFolders.DocParsedSecKeywords, dsSelectedKeywords, booIsWholeKeyword);
                                }
                                else
                                {
                                    _KeywordsMgr.FindKeywordsInDoc(pathFile, AppFolders.AnalysisParseSegKeywords, dsSelectedKeywords, booIsWholeKeyword);
                                }


                            }

                        }
                    }

                    _Next_TaskProcessObject = _TaskManager.Next_TaskProcessObject;
                    _TaskNo++;
                }
                if (_Next_TaskProcessObject == Atebion.Tasks.ProcessObject.DeepAnalyze)
                {
                    DataView dvDeepAnalysisAttributes = _TaskManager.GetTaskPropertiesAndAttributes(_TaskNo -1); // _TaskManager.Get_Config_Attributes_for_ProcessObject(Atebion.Tasks.ProcessObject.DeepAnalyze, _TaskNo);
                    bool doEditAnalysisResults = false;
                    //if (dvDeepAnalysisAttributes.Count > 0)
                    //{
                        foreach (DataRowView rowDA in dvDeepAnalysisAttributes)
                        {
                            if (rowDA["Attribute_Name"].ToString() == "EditAnalysisResults")
                            {
                                if (rowDA["Attribute_Value"].ToString() == "Yes")
                                {
                                    doEditAnalysisResults = true;
                                    string EditAnalysisResultsPathFile = Path.Combine(_ParsedSecXMLPath, "EditAnalysisResults.txt");
                                    Files.WriteStringToFile("Yes", EditAnalysisResultsPathFile);
                                    _currentMode = Modes.AnalysisResults;
                                    ModeAdjustments();
                                }
    
                            }
                        }
                    //}
                    // Run Analysis
                    if (!doEditAnalysisResults)
                    {
                        RunDeepAnalytics();

                    }


                }
                if (_Next_TaskProcessObject == Atebion.Tasks.ProcessObject.DisplayAnalysisResults)
                {
                    _currentMode = Modes.AnalysisResults;
                    ModeAdjustments();
                }
                if (_Next_TaskProcessObject == Atebion.Tasks.ProcessObject.GenerateReport)
                {
                    this.ucTasks1.UpdateProcessStatus("Generating Report...", true);
                    Cursor.Current = Cursors.WaitCursor;

                    string excelTemplateName = string.Empty;
                    string exportFileType = _TaskManager.GetExportFileType(_Task_Current, out excelTemplateName);
                    ExportManager.Modes exportMode = ExportManager.Modes.Excel;
                    if (exportFileType == "Excel")
                    {
                        exportMode = ExportManager.Modes.Excel;
                    }
                    else if (exportFileType == "Word")
                    {
                        exportMode = ExportManager.Modes.Word;
                    }
                    else if (exportFileType == "HTML")
                    {
                        exportMode = ExportManager.Modes.HTML;
                    }
                    
                    ExportManager exportMgr = new ExportManager();
                    exportMgr.Number_Use = true;
                    exportMgr.Caption_Use = true;
                    exportMgr.Keywords_Use = true;
                    exportMgr.SegmentText_Use = true;
                    exportMgr.Note_Use = true;
                    exportMgr.Page_Use = true;

                    if (!exportMgr.ExportAnalysisResults(ucProjectsDocs1.Documents_Selected.ToArray(), exportMode, ucProjectsDocs1.isUseDefaultParseAnalysis, excelTemplateName))
                    {
                        string exportError = exportMgr.ErrorMessage;
                        MessageBox.Show(exportError, "Unable to Generate Report for Analysis Results", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                        this.ucTasks1.UpdateProcessStatus(exportError, false);
                        Cursor.Current = Cursors.Default;
                        return;

                    }

                    this.ucTasks1.ShowTaskFlowStatus();
                    Cursor.Current = Cursors.Default;
                    _currentMode = Modes.ProjDocs;
                    ModeAdjustments();
                }
                string processObject = _TaskManager.GetProcessObject(_Task_Current, _TaskNo + 1);
                string currentProcessObject = _TaskManager.GetProcessObject(_Task_Current, _TaskNo);
                if (currentProcessObject == Atebion.Tasks.ProcessObject.FindConcepts && processObject == Atebion.Tasks.ProcessObject.DisplayAnalysisResults)
                {
                    this.ucTasks1.CurrentStep = _TaskNo + 1;
                    Cursor.Current = Cursors.WaitCursor;

                    _currentMode = Modes.AnalysisResults_Concepts;
                    ModeAdjustments();

                    Cursor.Current = Cursors.Default;
                }

                this.ucProjectsDocs1.ShowFileDetails();
                
            }

            this.ucTasks1.ShowTaskFlowStatus();
            Cursor.Current = Cursors.Default;

        }
        


        private string _ParseTypeFile = "ParseType.par";

        private string GetPreviousParseType4Defualt()
        {
            string parseTypePathFile = Path.Combine(AppFolders.DocInformation, _ParseTypeFile);
            string previousParseType = string.Empty;
            if (File.Exists(parseTypePathFile))
            {
                previousParseType = Atebion.Common.Files.ReadFile(parseTypePathFile);
                previousParseType.Trim();
            }

            return previousParseType;
        }

        private void butBack_Click(object sender, EventArgs e)
        {
            GoBack();

            //if (_currentMode == Modes.ProjDocs)
            //{
            //    Butnext.Text = "Next";
                
            //}

            //_currentMode = _PreviousMode;
            //_PreviousMode = _PreviousMode - 1;
            //_TaskNo--;
            //ModeAdjustments();
        }

        private void GoBack()
        {
            if (_currentMode == Modes.ProjDocs)
            {
                Butnext.Text = "Next";

            }

            _currentMode = _PreviousMode;
            _PreviousMode = _PreviousMode - 1;
            _TaskNo--;
            ModeAdjustments();
        }

        private void butBackToWelcome_Click(object sender, EventArgs e)
        {
            if (_currentMode == Modes.ProjDocs)
                Butnext.Text = "Next";

            _currentMode = 0;
            _PreviousMode = 0;
            _TaskNo = -1;
            HideCRUDButtons();
            ModeAdjustments();
           
        }

        public void ucWelcome1_StartClicked()
        {
            _PreviousMode = Modes.Start;
            _currentMode = Modes.TaskList;
            ModeAdjustments();

        }

        private void ucWelcome1_WorkgroupSelected()
        {
            AppFolders.WorkgroupRootPath = ucWelcome1.WorkgroupRootPath;
            AppFolders.WorkgroupName = ucWelcome1.Workgroup;

            this.Text = string.Concat("Professional Document Analyzer – New Edition – Workgroup: ", AppFolders.WorkgroupName);

         //   this.ucTasks1.LoadData(AppFolders.WorkgroupName, string.Empty, string.Empty, string.Empty, null, -1);
        }

        private void ucTasksSelect1_TaskSelected()
        {
            AppFolders.TaskName = ucTasksSelect1.TaskName;
            AppFolders.Task = ucTasksSelect1.Task;

            string[] steps = ucTasksSelect1.TaskSteps;

            this.ucTasks1.LoadData(AppFolders.WorkgroupName, AppFolders.Task, string.Empty, string.Empty, steps, -1);

            Butnext.Visible = true;
        }

        private void ucProjectsDocs1_DocumentsSelected()
        {
            if (ucProjectsDocs1.NextButtonType == Manager.ButtonNextAnalyze.Hide)
            {
                Butnext.Visible = false;
            }
            else if (ucProjectsDocs1.NextButtonType == Manager.ButtonNextAnalyze.Next)
            {
                Butnext.Text = "Next";
                Butnext.Visible = true;
            }
            else if (ucProjectsDocs1.NextButtonType == Manager.ButtonNextAnalyze.Analyze)
            {
                Butnext.Text = "Analyze";
                Butnext.Visible = true;
            }

        }

        private void ucKeywordsSelect1_KeywordLibSelected()
        {
            Butnext.Text = "Analyze";
            Butnext.Visible = true;
        }

        private void ucKeywordsSelect1_KeywordsNotSelected()
        {
            Butnext.Visible = false;
        }

        private void ucWelcome1_SettingsClicked()
        {
            ucWelcome1.Visible = false;
            panHeader.Visible = false;

            ucSettings1.LoadData();
            ucSettings1.Visible = true;
            ucSettings1.Dock = DockStyle.Fill;

            panFooter.Visible = true;
            butBack.Visible = false;
            butBackToWelcome.Visible = true;
        }

        private void butImport_MouseEnter(object sender, EventArgs e)
        {
            butImport.BackColor = Color.Blue;
        }

        private void butDownload_MouseEnter(object sender, EventArgs e)
        {
            butDownload.BackColor = Color.Blue; 
        }

        private void butEdit_MouseEnter(object sender, EventArgs e)
        {
            butEdit.BackColor = Color.Lime;
        }

        private void butImport_MouseLeave(object sender, EventArgs e)
        {
            butImport.BackColor = Color.Navy;
        }

        private void butDownload_MouseLeave(object sender, EventArgs e)
        {
            butDownload.BackColor = Color.Navy;
        }

        private void butEdit_MouseLeave(object sender, EventArgs e)
        {
            butEdit.BackColor = Color.DarkGreen;
        }

        private void butNew_MouseEnter(object sender, EventArgs e)
        {
            butNew.BackColor = Color.Blue; 
        }

        private void butNew_MouseLeave(object sender, EventArgs e)
        {
            butNew.BackColor = Color.Navy;
        }

        private void butDelete_MouseEnter(object sender, EventArgs e)
        {
            butDelete.BackColor = Color.Red;
        }

        private void butDelete_MouseLeave(object sender, EventArgs e)
        {
            butDelete.BackColor = Color.DarkRed;
        }

        private void butEdit_Click(object sender, EventArgs e)
        {
            if (_currentMode == Modes.Settings)
            {
                ucSettings1.Edit();

            }
        }

        private void butNew_Click(object sender, EventArgs e)
        {
            if (_currentMode == Modes.Settings)
            {
                ucSettings1.New();

            }
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            if (_currentMode == Modes.Settings)
            {
                ucSettings1.Delete();

            }
            else if (_currentMode == Modes.Results)
            {
                ucResults1.Delete(_Results_isProjectSelected);
            }

        }

        private void butDownload_Click(object sender, EventArgs e)
        {
            ucSettings1.Download();
        }

        private void butImport_Click(object sender, EventArgs e)
        {
            if (_currentMode == Modes.Settings)
            {
                ucSettings1.Import();

            }
        }

        private void HideCRUDButtons()
        {
            this.butDelete.Visible = false;
            this.butDownload.Visible = false;
            this.butEdit.Visible = false;
            this.butImport.Visible = false;
            this.butExport.Visible = false;
            this.butNew.Visible = false;
        }

        private void ucSettings1_MenuItemSelected()
        {
            HideCRUDButtons();
            _Settings_SelectedMenuItem = ucSettings1.SelectedMenuItem;

            switch (_Settings_SelectedMenuItem)
            {
                case SettingsMenu.Acronym_Dictionaries:
                    butDelete.Visible = true;
                    butDownload.Visible = true;
                    butEdit.Visible = true;
                    butNew.Visible = true;
                    break;

                case SettingsMenu.Dictionaries:
                    butDelete.Visible = true;
                    butDownload.Visible = true;
                    butEdit.Visible = true;
                    butImport.Visible = true;
                    butExport.Visible = true;
                    butNew.Visible = true;
                    break;

                case SettingsMenu.FARs_DFARs:
                    butDelete.Visible = true;
                    butDownload.Visible = true;
                    butEdit.Visible = true;
                    butImport.Visible = true;
                    butNew.Visible = true;

                    break;

                case SettingsMenu.InstructionsAndQuestions:
                    butEdit.Visible = true;

                    break;

                case SettingsMenu.KeywordGroups:
                    butDelete.Visible = true;
                    butDownload.Visible = true;
                    butEdit.Visible = true;
                    butNew.Visible = true;

                    break;

                case SettingsMenu.Tasks:
                    butDelete.Visible = true;
                    butDownload.Visible = true;
                    butEdit.Visible = true;
                    butNew.Visible = true;

                    break;

                case SettingsMenu.Templates:
                    butDelete.Visible = true;
                    butDownload.Visible = true;
                    butEdit.Visible = true;
                    butNew.Visible = true;

                    break;

                case SettingsMenu.RAM_Models:
                    butDelete.Visible = true;
                  //  butDownload.Visible = true;
                    butEdit.Visible = true;
                    butNew.Visible = true;

                    break;

                case SettingsMenu.QCReadability:
                    butEdit.Visible = true;

                    break;

            }

            _currentMode = Modes.Settings;
        }

        private void ucProjectsDocs1_HasReset()
        {
            Butnext.Visible = false;
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            _currentMode = Modes.Prestart;

            ModeAdjustments();
        }

        private void ucDictionarySelect1_DicSelected()
        {
            _DictionarySelected = ucDictionarySelect1.DictionarySelected;
            _SynonymsFind = ucDictionarySelect1.SynonymsFind;
            _WholeWordsFind = ucDictionarySelect1.WholeWordsFind;

            Butnext.Text = "Analyze";
            Butnext.Visible = true;
            
        }

        private void ucWelcome1_ResultsClicked()
        {
            _currentMode = Modes.Results;

            ucWelcome1.Visible = false;
            panHeader.Visible = false;

            ucResults1.LoadData();
            ucResults1.Visible = true;
            ucResults1.Dock = DockStyle.Fill;

            panFooter.Visible = true;
            butBack.Visible = false;
            butBackToWelcome.Visible = true;
        }

        private string GetCurrentDocument(bool isAnalysis)
        {
            string documentPathFile = string.Empty;
            string currentPath = string.Empty;

            if (isAnalysis)
            {
                currentPath = AppFolders.AnalysisCurrent;
            }
            else
            {
                currentPath = AppFolders.CurrentDocPath;
            }

            if (currentPath == string.Empty)
                return string.Empty;

            string txtFile = string.Empty;
            string otherTextFile = string.Empty;

            string[] files = Directory.GetFiles(currentPath);

            if (files.Length == 0)
                return string.Empty;


            string ext = string.Empty;
            foreach (string file in files)
            {
                Atebion.Common.Files.GetFileName(file, out ext);
                if (ext.ToLower() == ".txt")
                {
                    txtFile = file;
                }
                else
                {
                   otherTextFile = file;
                }
            }

            if ( otherTextFile != string.Empty)
            {
                return otherTextFile; 
            }

            return txtFile;

        }

        private void butTest_Click(object sender, EventArgs e)
        {
            // --- QC Test -----------
            //Atebion.QC.Analysis qcAnalysis = new Atebion.QC.Analysis();

            //string xmlPath = @"F:\QCTest1\T-ADC(X) Ship RFP Section C 600\Current\ParseSec\XML";
            //string parseSegPath = @"F:\QCTest1\T-ADC(X) Ship RFP Section C 600\Current\ParseSec";
            //string lsqcPath = @"F:\QCTest1\T-ADC(X) Ship RFP Section C 600\Current\ParseSec\LSQC";
            //string modelPath = @"I:\Tom\Atebion\ProfessionalDocAnalyzer\ProfessionalDocAnalyzer\bin\Debug\Model";
            //string dictionary = @"F:\Test6\Dictionaries\Words2Avoid.dicx";
            //string document = @"F:\QCTest1\T-ADC(X) Ship RFP Section C 600\Current\T-ADC(X) Ship RFP Section C.docx";

            //Cursor.Current = Cursors.WaitCursor;

            //qcAnalysis.ComplexWords_SyllableCountGreaterThan = 4;
            //qcAnalysis.AnalyzeDocs(xmlPath, parseSegPath, lsqcPath, modelPath, dictionary);

            //ucQCAnalysisResults1.LoadData(parseSegPath, xmlPath, document);

            //HideAllUCs();

            //Cursor.Current = Cursors.Default; // Back to normal

            //ucQCAnalysisResults1.Visible = true;

            //ucQCAnalysisResults1.Dock = DockStyle.Fill;

            //ucQCAnalysisResults1.AdjustColumns();

            // --- End QC Test -----

            Atebion.Word.OpenXML.CreateWordChart createWordChart = null;

            string wordDoc = @"F:\QCTest1\Testchrat1.docx ";
            
            try
            {
                createWordChart = new CreateWordChart(wordDoc);
                List<ChartSubArea> chartAreas = new List<ChartSubArea>();
                chartAreas.Add(new ChartSubArea() { Color = DocumentFormat.OpenXml.Drawing.SchemeColorValues.Accent1, Label = "1st Qtr", Value = "8.2" });
                chartAreas.Add(new ChartSubArea() { Color = DocumentFormat.OpenXml.Drawing.SchemeColorValues.Accent2, Label = "2st Qtr", Value = "3.2" });
                chartAreas.Add(new ChartSubArea() { Color = DocumentFormat.OpenXml.Drawing.SchemeColorValues.Accent3, Label = "3st Qtr", Value = "1.4" });
                chartAreas.Add(new ChartSubArea() { Color = DocumentFormat.OpenXml.Drawing.SchemeColorValues.Accent4, Label = "4st Qtr", Value = "1.2" });
                createWordChart.CreateChart(chartAreas);
                MessageBox.Show("Create Chart successfully, you can check your document!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                if (createWordChart != null)
                {
                    createWordChart.Dispose();
                }
            }
            
        }

        private string GetCurrentDocument(string path)
        {
            if (path == string.Empty)
                return string.Empty;

            string txtFile = string.Empty;
            string otherTextFile = string.Empty;

            string[] files = Directory.GetFiles(path);

            if (files.Length == 0)
                return string.Empty;


            string ext = string.Empty;
            foreach (string file in files)
            {
                Atebion.Common.Files.GetFileName(file, out ext);
                if (ext.ToLower() == ".txt")
                {
                    txtFile = file;
                }
                else
                {
                    otherTextFile = file;
                }
            }

            if (otherTextFile != string.Empty)
            {
                return otherTextFile;
            }

            return txtFile;

        }

        public int RunDeepAnalytics()
        {
            ucTasks1.Visible = true;
           
            ucTasks1.UpdateProcessStatus("Running Deep Analysis...", true);

            Cursor.Current = Cursors.WaitCursor; // Waiting 

            GenericDataManger gDataMgr = new GenericDataManger();

            string selectedKeywords = Path.Combine(AppFolders.DocParsedSecXML, "KeywordsSelected.xml");
            if (!File.Exists(selectedKeywords))
            {
                string MsgRerun = string.Concat("Unable to find File: ", selectedKeywords);

                ucTasks1.UpdateProcessStatus(MsgRerun, false);

                Cursor.Current = Cursors.Default;
                MessageBox.Show(MsgRerun, "Unable to Run Deep Analyzer", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }


             DataSet dsSelectedKeywords = gDataMgr.LoadDatasetFromXml(selectedKeywords);

            Atebion.DeepAnalytics.Analysis deepAnalysis = new Atebion.DeepAnalytics.Analysis();

            int results = deepAnalysis.Analyze(AppFolders.CurrentDocPath, AppFolders.DocParsedSec, AppFolders.AppModel, dsSelectedKeywords);


            deepAnalysis.CurrentDocPath = AppFolders.CurrentDocPath;

            ucTasks1.UpdateProcessStatus("Compiling Deep AnalysisResults...", true);
            Application.DoEvents();

            CovertSegSent2HTML(deepAnalysis);

            //      deepAnalysis = null; // release memory and unlock files 

            if (deepAnalysis != null) // Added 05.26.2016 for free memory. An out of memory was occuring during Deep Analysis
            {
                deepAnalysis = null;

                GC.Collect();
                GC.WaitForPendingFinalizers();

            }

            Cursor.Current = Cursors.Default;
            ucTasks1.UpdateProcessStatus("Deep Analysis Completed ...", false);
            Application.DoEvents();

            //if (!ucResults1.Visible)
            //{
                _currentMode = Modes.AnalysisResults_DeepAnalysis;
                ModeAdjustments();
           // }

            return results;
        }

        private bool CovertSegSent2HTML(Atebion.DeepAnalytics.Analysis deepAnalysis)
        {

            // Convert Parsed RTF files into HTML files 
            AtebionRTFf2HTMLf.Convert convert = new AtebionRTFf2HTMLf.Convert();

            int qtyConverted = convert.ConvertFiles(deepAnalysis.ParseSentencesPath, deepAnalysis.HTMLPath);

            if (convert.ErrorMessage != string.Empty)
            {
                ucTasks1.UpdateProcessStatus(string.Concat("Convert Sentences to HTML Error: ", convert.ErrorMessage), false);
                return false;
            }

            qtyConverted = convert.ConvertFiles(AppFolders.DocParsedSec, AppFolders.DocParsedSecHTML);

            if (convert.ErrorMessage != string.Empty)
            {
                ucTasks1.UpdateProcessStatus(string.Concat("Convert Sentences to HTML Error: ", convert.ErrorMessage), false);
                return false;
            }


            return true;
        }

        private void ucAnalysisResults1_RunDeepAnalysisResults()
        {
            RunDeepAnalytics();
        }

        private bool GenerateKeywordsSelectedXMLFile(DataSet dsSelectedKeywords, string KeywordGroupName)
        {
            // Use this Dataset to past to the Parser
            DataSet UseKeywords = new DataSet();
            DataTable Keywords = UseKeywords.Tables.Add();
            //     Keywords.Columns.Add(KeywordsFoundFields.KeywordGroup, typeof(string));
            Keywords.Columns.Add(KeywordsFoundFields.Select, typeof(bool)); // For Deep Analysis
            Keywords.Columns.Add(KeywordsFoundFields.Keyword, typeof(string));
            Keywords.Columns.Add(KeywordsFoundFields.Count, typeof(int)); // For Deep Analysis
            Keywords.Columns.Add(KeywordsFoundFields.ColorHighlight, typeof(string));
            Keywords.Columns.Add(KeywordsFoundFields.KeywordLib, typeof(string));

            int i = 0;

            string keyword = string.Empty;
            string color = string.Empty;
            string keywordLib = string.Empty;

            DataRow newRow;
            foreach (DataRow row in dsSelectedKeywords.Tables[0].Rows)
            {
                newRow = UseKeywords.Tables[0].NewRow();
                newRow[KeywordsFoundFields.Select] = true;
                newRow[KeywordsFoundFields.Keyword] = row[KeywordsFoundFields.Keyword].ToString();
                newRow[KeywordsFoundFields.Count] = 0;
                newRow[KeywordsFoundFields.ColorHighlight] = row[KeywordsFoundFields.ColorHighlight].ToString();
                newRow[KeywordsFoundFields.KeywordLib] = KeywordGroupName;

                UseKeywords.Tables[0].Rows.Add(newRow);

                i++;
            }

            string xmlKeywordsSelected = Path.Combine(AppFolders.DocParsedSecXML, "KeywordsSelected.xml");

            GenericDataManger gDMgr = new GenericDataManger();
            gDMgr.SaveDataXML(UseKeywords, xmlKeywordsSelected);
            Application.DoEvents();
            gDMgr = null;

            if (i > 0)
                return true;
            else
                return false;
        }

        private void ucResults1_AnalysisSelected()
        {
            _Results_isProjectSelected = false;
            butDelete.Visible = true;
        }

        private void ucResults1_AnalysisUnselected()
        {
            butDelete.Visible = false;
        }

        private void ucResults1_ProjectSelected()
        {
            _Results_isProjectSelected = true;
            butDelete.Visible = true;
        }

        private void ucResults1_ProjectUnselected()
        {
            butDelete.Visible = false;
        }

        private void ucResults1_RunDeepAnalysisResults()
        {
            RunDeepAnalytics();
        }

        private void butPerson_Click(object sender, EventArgs e)
        {
            if (_lMgr.Validate() == "Invalid")
            {
                OpenUserInfor(true);
            }
            else
            {
                OpenUserInfor(false);
            }
        }

        private void OpenUserInfor(bool isExpired)
        {
            UserCardMgr usrMgr = new UserCardMgr();

            string[] users = usrMgr.GetUserInforFiles4App();

            frmUserCard user;

            if (users.Length == 0)
            {
                user = new frmUserCard();

                if (user.ShowDialog(this) == DialogResult.OK)
                {
                    return;
                }
                else
                {
                    if (_lMgr.Validate() == "Invalid")
                    {
                        this.Close();
                        Application.Exit();
                    }
                }
            }
            else
            {
                //if (openIfFound)
                //{
                user = new frmUserCard(users[0]);

                if (user.ShowDialog(this) == DialogResult.OK)
                {
                    return;
                }
                else
                {
                    if (isExpired)
                    {
                        this.Close();
                        Application.Exit();
                    }
                }
                //}
            }

        }

        private void ucStart1_Completed()
        {
            lblUser.Text = string.Concat(AppFolders.UserName, "       ", "License expires in ", ucStart1.DaysLeft.ToString(), " Days on ", ucStart1.ExpirationDate);
            lblUser.Refresh();
            _currentMode = Modes.Start;
            ModeAdjustments();
        }

        private void butInformation_Click(object sender, EventArgs e)
        {
            LastestRelease lastestRelease;
            bool IsLatestReleaseNewer = false;
            try
            {
                lastestRelease = new LastestRelease();

                IsLatestReleaseNewer = lastestRelease.IsLatestReleaseNewer();
            }
            catch
            {
                string msgNotice = "Please check your internet connection or have your System Administrator check your company’s firewall to ensure Atebion’s website (http://www.atebionllc.com/) is White Listed.";
                string cap = "Unable to Check for the Newest Version of the Professional Document Analyzer";
                MessageBox.Show(this, msgNotice, cap, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            string cureentRelease = lastestRelease.CurrentRelease;
            string latestRelease = lastestRelease.LatestRelease;

            string msg = string.Empty;

            string log = lastestRelease.ReleasesLog();
            if (latestRelease.Trim() == string.Empty)
            {

                msg = string.Concat("Notice:",
                    Environment.NewLine,
                    Environment.NewLine,
                    "Unable to get the Lastest Release value.",
                    Environment.NewLine,
                    "You might not have internet connection.");
            }
            else if (IsLatestReleaseNewer)
            {
                msg = string.Concat("Notice: The Professional Document Analyzer release you are using is older than the latest release.",
                Environment.NewLine,
                "Current Release: ", cureentRelease,
                Environment.NewLine,
                "Lastest Release:  ", latestRelease);
            }
            else
            {
                msg = string.Concat("The Professional Document Analyzer release you are using is the same as the latest release, or is a prerelease edition.",
                    Environment.NewLine,
                    "Current Release: ", cureentRelease,
                    Environment.NewLine,
                    "Lastest Release:  ", latestRelease);
            }

            IniFile inifile = new IniFile();
            inifile.Load(AppFolders.InIFile);
            string iniLatestRelease = inifile.GetKeyValue(CurrentSettings.configSecUserSettings, CurrentSettings.configKeyLatestRelease);

            frmLastestReleaseInfo xfrmLastestReleaseInfo = new frmLastestReleaseInfo(msg, log, latestRelease.ToString(), false);
            xfrmLastestReleaseInfo.ShowDialog();
            
        }

        private void ucProjectsDocs1_ProjectSelected()
        {
            if (ucProjectsDocs1.NextButtonType == Manager.ButtonNextAnalyze.Create)
            {
                Butnext.Text = "Create";
                Butnext.Visible = true;
            }
            else if (ucProjectsDocs1.NextButtonType == Manager.ButtonNextAnalyze.XRefMatrices)
            {
                Butnext.Text = "X-Ref";
                Butnext.Visible = true;
            }
        }

        private void butExport_Click(object sender, EventArgs e)
        {
            if (_currentMode == Modes.Settings)
            {
                ucSettings1.Export();

            }
        }

        private void butExport_MouseEnter(object sender, EventArgs e)
        {
            butExport.BackColor = Color.Blue;
        }

        private void butExport_MouseLeave(object sender, EventArgs e)
        {
            butExport.BackColor = Color.Navy;
        }
    }
}
