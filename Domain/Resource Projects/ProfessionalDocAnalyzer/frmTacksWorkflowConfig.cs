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
using Atebion.Tasks;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class frmTacksWorkflowConfig : MetroFramework.Forms.MetroForm
    {
        public frmTacksWorkflowConfig()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }
        
        private string _TaskFile = string.Empty;
        private string _TasksCatalogueFile = string.Empty;

        private bool _UseDefaultParseAnalysis = false;
        private bool _isNew = false;

        private DataSet _dsTask;

        private DataTable _dtAttributes1;
        private DataTable _dtAttributes2;
        private DataTable _dtAttributes3;
        private DataTable _dtAttributes4;
        private DataTable _dtAttributes5;
        private DataTable _dtAttributes6;
        private DataTable _dtAttributes7;
        private DataTable _dtAttributes8;


        private Atebion.Tasks.Manager _TaskManager;

        public string TaskName
        {
            get { return txtbTaskName.Text; }
            set { txtbTaskName.Text = value; }
        }

        public string TaskName_Short
        {
            get { return txtbShortTaskName.Text; }
            set { txtbShortTaskName.Text = value; }
        }

        public string TaskDescription
        {
            get {return txtbDescription.Text; }
            set { txtbDescription.Text = value; }
        }

        public bool LoadData(string TaskFile, string TasksCatalogueFile, string PathLocalData, string PathWorkgroupRoot)
        {
            _isNew = false;

            LoadActionCaptionNumbers();

            if (!File.Exists(TaskFile))
            {
                string msg = string.Concat("Unable to find Task Workflow file: ", TaskFile);
                MessageBox.Show(msg, "Task Workflow NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (!File.Exists(TasksCatalogueFile))
            {
                string msg = string.Concat("Unable to find Task Workflow Catalogue file: ", TasksCatalogueFile);
                MessageBox.Show(msg, "Task Workflow Catalogue NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (PathWorkgroupRoot != null && PathWorkgroupRoot.Length != 0)
            {
                _TaskManager = new Manager(PathLocalData, PathWorkgroupRoot, AppFolders.UserName);
                _UseDefaultParseAnalysis = false;
            }
            else
            {
                _TaskManager = new Manager(PathLocalData, AppFolders.UserName);
                _UseDefaultParseAnalysis = true;
            }

            _TaskFile = TaskFile;
            _TasksCatalogueFile = TasksCatalogueFile;

            if (!ReadTaskFile_PopulateFields())
            {
                return false;
            }


            

            return true;
        }

        public bool LoadData(string TasksCatalogueFile, string PathLocalData, string PathWorkgroupRoot)
        {
            _isNew = true;

            LoadActionCaptionNumbers();

            if (!File.Exists(TasksCatalogueFile))
            {
                string msg = string.Concat("Unable to find Task Workflow Catalogue file: ", TasksCatalogueFile);
                MessageBox.Show(msg, "Task Workflow Catalogue NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            if (PathWorkgroupRoot != null && PathWorkgroupRoot.Length != 0)
            {
                _TaskManager = new Manager(PathLocalData, PathWorkgroupRoot, AppFolders.UserName);
                _UseDefaultParseAnalysis = false;
            }
            else
            {
                _TaskManager = new Manager(PathLocalData, AppFolders.UserName);
                _UseDefaultParseAnalysis = true;
            }

            _TasksCatalogueFile = TasksCatalogueFile;

            LoadBaseActions();

            return true;
        }

        private void LoadActionCaptionNumbers()
        {
            this.ucAttributes1.ActionNo = 1;
            this.ucAttributes2.ActionNo = 2;
            this.ucAttributes3.ActionNo = 3;
            this.ucAttributes4.ActionNo = 4;
            this.ucAttributes5.ActionNo = 5;
            this.ucAttributes6.ActionNo = 6;
            this.ucAttributes7.ActionNo = 7;
            this.ucAttributes8.ActionNo = 8;


        }

        private bool LoadBaseActions()
        {
            
            string processObjectsDelimited = string.Empty;
            string[] processObjects;

            processObjectsDelimited = Atebion.Tasks.ProcessObject_Level.Base_Level;
            processObjects = processObjectsDelimited.Split('|');
            this.ucAttributes1.LoadActions(processObjects);

            return true;


        }

        private bool ReadTaskFile_PopulateFields()
        {
            

            _dsTask = Files.LoadDatasetFromXml(_TaskFile);

            if (_dsTask == null)
            {
                string msg = string.Concat("Unable to read Task Workflow file: ", _TaskFile);
                MessageBox.Show(msg, "Task Workflow NOT Readable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;

            }

            int level = 1;
            string processObjectsDelimited = string.Empty;
            string[] processObjects;

            string action = string.Empty;
            DataTable dtAttributes;

            foreach (DataRow row in _dsTask.Tables["TaskFlow"].Rows)
            {
                switch (level)
                {
                    case 1:
                        processObjectsDelimited = Atebion.Tasks.ProcessObject_Level.Base_Level;
                        processObjects = processObjectsDelimited.Split('|');
                        ucAttributes1.LoadActions(processObjects);

                        action = row[TaskFlowFields.ProcessObject].ToString();
                        ucAttributes1.Action = action;

                        _dtAttributes1 = _TaskManager.createTable_Attributes();

                        dtAttributes = _dsTask.Tables[Attributes.TableName];

                        _dtAttributes1 = PopulateAttributeDT(dtAttributes, _dtAttributes1, level);

                        ucAttributes1.LoadAttributes(_dtAttributes1, false);

                        ucAttributes1.StepText = row[TaskFlowFields.ProcessStepText].ToString();

                        break;

                    case 2:
                        action = row[TaskFlowFields.ProcessObject].ToString();

                        ucAttributes2.Action = action;

                        _dtAttributes2 = _TaskManager.createTable_Attributes();

                        dtAttributes = _dsTask.Tables[Attributes.TableName];

                        _dtAttributes2 = PopulateAttributeDT(dtAttributes, _dtAttributes2, level);

                        ucAttributes2.LoadAttributes(_dtAttributes2, false);

                        ucAttributes2.StepText = row[TaskFlowFields.ProcessStepText].ToString();


                        break;

                    case 3:

                         action = row[TaskFlowFields.ProcessObject].ToString();

                        ucAttributes3.Action = action;

                        _dtAttributes3 = _TaskManager.createTable_Attributes();

                        dtAttributes = _dsTask.Tables[Attributes.TableName];

                        _dtAttributes3 = PopulateAttributeDT(dtAttributes, _dtAttributes3, level);

                        ucAttributes3.LoadAttributes(_dtAttributes3, false);

                        ucAttributes3.StepText = row[TaskFlowFields.ProcessStepText].ToString();

                        break;

                    case 4:

                        action = row[TaskFlowFields.ProcessObject].ToString();

                        ucAttributes4.Action = action;

                        _dtAttributes4 = _TaskManager.createTable_Attributes();

                        dtAttributes = _dsTask.Tables[Attributes.TableName];

                        _dtAttributes4 = PopulateAttributeDT(dtAttributes, _dtAttributes4, level);

                        ucAttributes4.LoadAttributes(_dtAttributes4, false);

                        ucAttributes4.StepText = row[TaskFlowFields.ProcessStepText].ToString();

                        break;

                    case 5:
                        action = row[TaskFlowFields.ProcessObject].ToString();

                        ucAttributes5.Action = action;

                        _dtAttributes5 = _TaskManager.createTable_Attributes();

                        dtAttributes = _dsTask.Tables[Attributes.TableName];

                        _dtAttributes5 = PopulateAttributeDT(dtAttributes, _dtAttributes5, level);

                        ucAttributes5.LoadAttributes(_dtAttributes5, false);

                        ucAttributes5.StepText = row[TaskFlowFields.ProcessStepText].ToString();

                        break;

                    case 6:
                        action = row[TaskFlowFields.ProcessObject].ToString();

                        ucAttributes6.Action = action;

                        _dtAttributes6 = _TaskManager.createTable_Attributes();

                        dtAttributes = _dsTask.Tables[Attributes.TableName];

                        _dtAttributes6 = PopulateAttributeDT(dtAttributes, _dtAttributes6, level);

                        ucAttributes6.LoadAttributes(_dtAttributes6, false);

                        ucAttributes6.StepText = row[TaskFlowFields.ProcessStepText].ToString();

                        break;

                    case 7:
                        action = row[TaskFlowFields.ProcessObject].ToString();

                        ucAttributes7.Action = action;

                        _dtAttributes7 = _TaskManager.createTable_Attributes();

                        dtAttributes = _dsTask.Tables[Attributes.TableName];

                        _dtAttributes7 = PopulateAttributeDT(dtAttributes, _dtAttributes7, level);

                        ucAttributes7.LoadAttributes(_dtAttributes7, false);

                        ucAttributes7.StepText = row[TaskFlowFields.ProcessStepText].ToString();

                        break;

                    case 8:
                         action = row[TaskFlowFields.ProcessObject].ToString();

                        ucAttributes8.Action = action;

                        _dtAttributes8 = _TaskManager.createTable_Attributes();

                        dtAttributes = _dsTask.Tables[Attributes.TableName];

                        _dtAttributes8 = PopulateAttributeDT(dtAttributes, _dtAttributes8, level);

                        ucAttributes8.LoadAttributes(_dtAttributes8, false);

                        ucAttributes8.StepText = row[TaskFlowFields.ProcessStepText].ToString();

                        break;

                }


                level++;
            }
            string baseLevel = Atebion.Tasks.ProcessObject_Level.Base_Level;

            return true;
        }

        private DataTable PopulateAttributeDT(DataTable dtSource, DataTable dtToPopulate, int TaskUI)
        {
            string select = string.Concat(Attributes.TaskFlow_UID, " = ", TaskUI.ToString());
            DataRow[] rowsAttributes = dtSource.Select(select);

            foreach (DataRow rowAttribute in rowsAttributes)
            {
                dtToPopulate.ImportRow(rowAttribute);
            }

            return dtToPopulate;
        }


        private void ucAttributes1_ActionSelected()
        {
            string processObjectsDelimited = string.Empty;
            string[] processObjects;

            ucAttributes2.Visible = false;
            ucAttributes3.Visible = false;
            ucAttributes4.Visible = false;
            ucAttributes5.Visible = false;
            ucAttributes6.Visible = false;
            ucAttributes7.Visible = false;
            ucAttributes8.Visible = false;

            string action = ucAttributes1.Action;
            switch (action)
            {
                case ProcessObject.Parse:
                    _dtAttributes1 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.Parse, 1);
                    ucAttributes1.LoadAttributes(_dtAttributes1, _isNew );

                    break;

                case ProcessObject.AcroSeeker:
                    _dtAttributes1 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.AcroSeeker, 1);
                    ucAttributes1.LoadAttributes(_dtAttributes1, _isNew);

                    break;

                case ProcessObject.CreateXRefMatrix:
                    ucAttributes1.HideClearAttributes();

                    break;

                case ProcessObject.XRefMatrices:
                    ucAttributes1.HideClearAttributes();

                    break;


            }
        }

        private void ucAttributes1_UseDefaultParseAnalysisChanged()
        {
            string processObjectsDelimited = string.Empty;
            string[] processObjects;

            _UseDefaultParseAnalysis = ucAttributes1.UseDefaultParseAnalysis;

            if (_UseDefaultParseAnalysis)
            {
                processObjectsDelimited = Atebion.Tasks.ProcessObject_Level.Parse_Defualt_Children;
                processObjects = processObjectsDelimited.Split('|');

            }
            else
            {
                processObjectsDelimited = Atebion.Tasks.ProcessObject_Level.Parse_Children;
                processObjects = processObjectsDelimited.Split('|');
            }

            // Load Actions into the 2nd Attribute control
            ucAttributes2.LoadActions(processObjects);


        }

        private void ucAttributes2_ActionSelected()
        {
            string processObjectsDelimited = string.Empty;
            string[] processObjects;

            ucAttributes3.Visible = false;
            ucAttributes4.Visible = false;
            ucAttributes5.Visible = false;
            ucAttributes6.Visible = false;
            ucAttributes7.Visible = false;
            ucAttributes8.Visible = false;

            ucAttributes2.HideClearAttributes();

            ucAttributes1.PagesRequired = string.Empty;


            string action = ucAttributes2.Action;
            switch (action)
            {
                case ProcessObject.Parse:
                    _dtAttributes2 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.Parse, 2);
                    ucAttributes2.LoadAttributes(_dtAttributes2, _isNew);

                    break;

                case ProcessObject.ReadabilityTest:
                    _dtAttributes2 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.ReadabilityTest, 2);
                    ucAttributes2.LoadAttributes(_dtAttributes2, _isNew);


                    processObjectsDelimited = ProcessObject_Level.ReadabilityTest_Children;
                    processObjects = processObjectsDelimited.Split('|');

                    ucAttributes3.LoadActions(processObjects);

                    ucAttributes1.PagesRequired = "1";
                    ucAttributes1.CheckPages(ucAttributes1.PagesRequired);

                    break;


                case ProcessObject.FindKeywordsPerLib:
                    _dtAttributes2 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.FindKeywordsPerLib, 2);
                    ucAttributes2.LoadAttributes(_dtAttributes2, _isNew);

                    if (_UseDefaultParseAnalysis)
                        processObjectsDelimited = ProcessObject_Level.FindKeywordsPerLib_Defualt_Children;
                    else
                        processObjectsDelimited = ProcessObject_Level.FindKeywordsPerLib_Children;

                    processObjects = processObjectsDelimited.Split('|');
                    ucAttributes3.LoadActions(processObjects);

                    break;

                case ProcessObject.FindDictionaryTerms:
                    _dtAttributes2 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.FindDictionaryTerms, 2);
                    ucAttributes2.LoadAttributes(_dtAttributes2, _isNew);

                    processObjectsDelimited = ProcessObject_Level.FindDictionaryTerms_Children;
                    processObjects = processObjectsDelimited.Split('|');
                    ucAttributes3.LoadActions(processObjects);

                    break;

                case ProcessObject.FindValues:
                    _dtAttributes2 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.FindValues, 2);
                    ucAttributes2.LoadAttributes(_dtAttributes2, _isNew);

                    processObjectsDelimited = ProcessObject_Level.FindValues_Children;
                    processObjects = processObjectsDelimited.Split('|');
                    ucAttributes3.LoadActions(processObjects);

                    break;

                case ProcessObject.DisplayAnalysisResults:
                    _dtAttributes2 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.DisplayAnalysisResults, 2);
                    ucAttributes2.LoadAttributes(_dtAttributes2, _isNew);

                    if (_UseDefaultParseAnalysis)
                    {
                        processObjectsDelimited = ProcessObject_Level.DisplayAnalysisResults_Defualt_Children;
                        processObjects = processObjectsDelimited.Split('|');
                        ucAttributes3.LoadActions(processObjects);
                    }

                    break;

                case ProcessObject.FindConcepts:
                    processObjectsDelimited = ProcessObject_Level.FindConcepts_Children;
                    processObjects = processObjectsDelimited.Split('|');
                    ucAttributes3.LoadActions(processObjects);

                    break;

                case ProcessObject.CompareDocsConcepts:
                    _dtAttributes2 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.CompareDocsConcepts, 1);
                    ucAttributes2.LoadAttributes(_dtAttributes2, _isNew);

                    processObjectsDelimited = ProcessObject_Level.CompareDocsConcepts_Children;
                    processObjects = processObjectsDelimited.Split('|');

                    ucAttributes3.LoadActions(processObjects);

                    ucAttributes1.PagesRequired = "2|2 or more";
                    ucAttributes1.CheckPages(ucAttributes1.PagesRequired);
 
                    break;

                case ProcessObject.CompareDocsDictionary:
                    _dtAttributes2 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.CompareDocsDictionary, 1);
                    ucAttributes2.LoadAttributes(_dtAttributes2, _isNew);

                    processObjectsDelimited = ProcessObject_Level.CompareDocsDictionary_Children;
                    processObjects = processObjectsDelimited.Split('|');

                    ucAttributes3.LoadActions(processObjects);

                    ucAttributes1.PagesRequired = "2|2 or more";
                    ucAttributes1.CheckPages(ucAttributes1.PagesRequired);

                    break;

                case ProcessObject.GenerateRAMRpt:
                    _dtAttributes2 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.GenerateRAMRpt, 1);
                    ucAttributes2.LoadAttributes(_dtAttributes2, _isNew);

                    break;

                case ProcessObject.DeepAnalyze:
                    _dtAttributes2 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.DeepAnalyze, 1);
                    ucAttributes2.LoadAttributes(_dtAttributes2, _isNew);

                    processObjectsDelimited = ProcessObject_Level.DeepAnalyze_Children;
                    processObjects = processObjectsDelimited.Split('|');

                    ucAttributes3.LoadActions(processObjects);

                    break;
 


            }
        }

        private void ucAttributes3_ActionSelected()
        {
            string processObjectsDelimited = string.Empty;
            string[] processObjects;

           
            ucAttributes4.Visible = false;
            ucAttributes5.Visible = false;
            ucAttributes6.Visible = false;
            ucAttributes7.Visible = false;
            ucAttributes8.Visible = false;

            ucAttributes3.HideClearAttributes();

            string action = ucAttributes3.Action;
            switch (action)
            {
                case ProcessObject.Parse:
                    _dtAttributes3 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.Parse, 3);
                    ucAttributes3.LoadAttributes(_dtAttributes3, _isNew);

                    break;

                case ProcessObject.FindKeywordsPerLib:
                    _dtAttributes3 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.FindKeywordsPerLib, 3);
                    ucAttributes3.LoadAttributes(_dtAttributes3, _isNew);

                    if (_UseDefaultParseAnalysis)
                        processObjectsDelimited = ProcessObject_Level.FindKeywordsPerLib_Defualt_Children;
                    else
                        processObjectsDelimited = ProcessObject_Level.FindKeywordsPerLib_Children;

                    processObjects = processObjectsDelimited.Split('|');
                    ucAttributes4.LoadActions(processObjects);

                    break;

                case ProcessObject.FindDictionaryTerms:
                    _dtAttributes3 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.FindDictionaryTerms, 3);
                    ucAttributes3.LoadAttributes(_dtAttributes3, _isNew);

                    processObjectsDelimited = ProcessObject_Level.FindDictionaryTerms_Children;
                    processObjects = processObjectsDelimited.Split('|');
                    ucAttributes4.LoadActions(processObjects);

                    break;

                case ProcessObject.FindValues:
                    _dtAttributes3 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.FindValues, 3);
                    ucAttributes3.LoadAttributes(_dtAttributes3, _isNew);

                    processObjectsDelimited = ProcessObject_Level.FindValues_Children;
                    processObjects = processObjectsDelimited.Split('|');
                    ucAttributes4.LoadActions(processObjects);

                    break;

                case ProcessObject.DeepAnalyze:
                    _dtAttributes3 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.DeepAnalyze, 3);
                    ucAttributes3.LoadAttributes(_dtAttributes3, _isNew);

                    processObjectsDelimited = ProcessObject_Level.FindValues_Children;
                    processObjects = processObjectsDelimited.Split('|');
                    ucAttributes4.LoadActions(processObjects);

                    break;


                case ProcessObject.DisplayAnalysisResults:
                    _dtAttributes3 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.DisplayAnalysisResults, 3);
                    ucAttributes3.LoadAttributes(_dtAttributes3, _isNew);

                    if (ucAttributes2.Action != ProcessObject.DeepAnalyze)
                    {
                        if (_UseDefaultParseAnalysis)
                        {
                            processObjectsDelimited = ProcessObject_Level.DisplayAnalysisResults_Defualt_Children;
                            processObjects = processObjectsDelimited.Split('|');
                            ucAttributes4.LoadActions(processObjects);
                        }
                    }

                    break;

                case ProcessObject.GenerateReport:
                    string reportFor = GetReportUseFor(3);
                    ucAttributes3.ReportForAction = reportFor;

                    _dtAttributes3 = _TaskManager.Get_Config_Attributes_for_ProcessObject(ProcessObject.GenerateReport, 3);
                    ucAttributes3.LoadAttributes(_dtAttributes3, _isNew);

 

                    break;


            }
        }

        private string GetReportUseFor(int ReportAttributeNo)
        {
            string attributeName = string.Empty;
            string action = string.Empty;

            Control[] controls;

            string reportUseFor = string.Empty;


            for (int i = ReportAttributeNo; i != 0; i--)
            {
                attributeName = string.Concat("ucAttributes", i.ToString());

                controls = this.Controls.Find(attributeName, true);
                ucParse_Attributes att = (ucParse_Attributes)controls[0];

                action = att.Action;

                if (action == ProcessObject.FindKeywordsPerLib)
                {
                    reportUseFor =  ProcessObject.FindKeywordsPerLib;
                    return reportUseFor;
                }
                else if (action == ProcessObject.DeepAnalyze)
                {
                    reportUseFor = ReportFor.DeepAnalysis;
                    return reportUseFor;
                }
                else if (action == ProcessObject.FindConcepts)
                {
                    reportUseFor = ReportFor.ParseConcepts;
                    return reportUseFor;
                }
                else if (action == ProcessObject.FindDictionaryTerms)
                {
                    reportUseFor = ReportFor.ParseDictionary;
                    return reportUseFor;
                }
                else if (action == ProcessObject.CompareDocsConcepts)
                {
                    reportUseFor = ReportFor.CompareConcepts;
                    return reportUseFor;
                }
                else if (action == ProcessObject.CompareDocsDictionary)
                {
                    reportUseFor = ReportFor.CompareDictionary;
                    return reportUseFor;
                }

            }

            return reportUseFor;
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();

        }

        public bool Validate()
        {
            this.txtbErrors.Text = string.Empty;
            panErrors.Visible = false;

            string error = string.Empty;

            if (txtbShortTaskName.Text.Length == 0)
            {
                error =  string.Concat(lblShortTaskName.Text, " is not defined");
                lblShortTaskName.ForeColor = Color.Red;
            }
            else
            {
                lblShortTaskName.ForeColor = Color.Black;
            }

            if (txtbTaskName.Text.Length == 0)
            {
                if (error.Length == 0)
                {
                    error = string.Concat(lblTaskName.Text, " is not defined");
                }
                else
                {
                    error = string.Concat(error, Environment.NewLine, lblTaskName.Text, " is not defined");

                }

                lblTaskName.ForeColor = Color.Red;
            }
            else
            {
                lblTaskName.ForeColor = Color.Black;
            }

            if (txtbDescription.Text.Length == 0)
            {
                if (error.Length == 0)
                {
                    error = string.Concat(lblDescription.Text, " is not defined");
                }
                else
                {
                    error = string.Concat(error, Environment.NewLine, lblDescription.Text, " is not defined");

                }
                lblDescription.ForeColor = Color.Red;

            }
            else
            {
                lblDescription.ForeColor = Color.Black;
            }

            if (ucAttributes1.Action == Atebion.Tasks.ProcessObject.AcroSeeker || ucAttributes1.Action == Atebion.Tasks.ProcessObject.CompareDocsDiff || ucAttributes1.Action == ProcessObject.CreateXRefMatrix || ucAttributes1.Action == ProcessObject.XRefMatrices || ucAttributes2.Action == Atebion.Tasks.ProcessObject.ReadabilityTest)
                return true;

            if (ucAttributes2.Action == Atebion.Tasks.ProcessObject.CompareDocsConcepts)
            {
                if(!ucAttributes1.CheckPages("2|2 or more"))
                {
                    error = string.Concat(error, Environment.NewLine, "When comparing documents, more than one document is needed.");
                }
            }

            if (ucAttributes2.Action == Atebion.Tasks.ProcessObject.FindConcepts)
            {
                if (!ucAttributes1.CheckPages("1"))
                {
                    error = string.Concat(error, Environment.NewLine, "Use only one document.");
                }
            }

            if (error.Length > 0)
            {
                error = string.Concat(error, Environment.NewLine, Environment.NewLine);
            }

            string validation = string.Empty;
            string attributeName = string.Empty;
            Control[] controls;

            int lastVisibleAttributeControl = -1;
            for (int i = 1; i < 9; i++)
            {
                attributeName = string.Concat("ucAttributes", i.ToString());

                controls = this.Controls.Find(attributeName, true);
                ucParse_Attributes att = (ucParse_Attributes)controls[0];

                if (att.Visible)
                    lastVisibleAttributeControl = i;

                validation = att.Validate();
                if (validation.Length > 0)
                {
                    error = string.Concat(error, validation);
                }
            }

            if (lastVisibleAttributeControl > -1)
            {
                attributeName = string.Concat("ucAttributes", lastVisibleAttributeControl.ToString());

                controls = this.Controls.Find(attributeName, true);
                ucParse_Attributes att = (ucParse_Attributes)controls[0];

                // ProcessObject.DisplayAnalysisResults, "|", ProcessObject.GenerateReport
                string xAction = att.Action;

                if (lastVisibleAttributeControl == 0)
                {
                    error = string.Concat(error, Environment.NewLine, "Select sub-action(s) and set values for associated attributes prior to saving.");
                }
                else if (xAction == string.Empty)
                {
                    string attributeNamePrevious = string.Concat("ucAttributes", (lastVisibleAttributeControl - 1).ToString());

                    controls = this.Controls.Find(attributeName, true);
                    ucParse_Attributes attPrevious = (ucParse_Attributes)controls[0];

                    string xActionPrevious = attPrevious.Action;

                    if (xActionPrevious != ProcessObject.DisplayAnalysisResults)
                    {
                        if (xActionPrevious != ProcessObject.GenerateReport)
                        {
                            if (xActionPrevious.IndexOf("None") == -1)
                            {

                                string msg = string.Concat("The last action must be either ", ProcessObject.DisplayAnalysisResults, " or ", ProcessObject.GenerateReport);
                                error = string.Concat(error, Environment.NewLine, msg);
                            }
                        }
                    }
                    
                }
                else if (xAction != ProcessObject.DisplayAnalysisResults)
                {
                    if (xAction != ProcessObject.GenerateReport)
                    {
                        if (xAction != ProcessObject.GenerateRAMRpt)
                        {
                            if (xAction.IndexOf("None") == -1)
                            {
                                string msg = string.Concat("Select sub-action(s) and set values for associated attributes prior to saving. - ", "The last action must be either ", ProcessObject.DisplayAnalysisResults, " or ", ProcessObject.GenerateReport);
                                error = string.Concat(error, Environment.NewLine, msg);
                            }
                        }
                    }
                }   
            }


            if (error.Length > 0)
            {
                error = string.Concat("Validations Found:", Environment.NewLine, Environment.NewLine, error);
                txtbErrors.Text = error;
                panErrors.Visible = true;
                return false;
            }

            return true;
        }

        private bool CompileActionsIntoTask()
        {
            _dsTask = _TaskManager.CreateDataSet_Tasks(); // Create empty dataset
            DataTable dtAttributes;
            

            //TaskFlowFields

            string attributeName = string.Empty;
            Control[] controls;

            DataRow taskRow;
            DataRow attributeRow;

            string stepText = string.Empty;

            int x = 1;

            for (int i = 1; i < 9; i++)
            {
                attributeName = string.Concat("ucAttributes", i.ToString());

                controls = this.Controls.Find(attributeName, true);
                ucParse_Attributes att = (ucParse_Attributes)controls[0];

                if (att.Visible)
                {
                    if (att.Action == string.Empty)
                        break;

                    taskRow = _dsTask.Tables[TaskFlowFields.TableName].NewRow();

                    taskRow[TaskFlowFields.UID] = i;
                    taskRow[TaskFlowFields.ProcessObject] = att.Action;
                    taskRow[TaskFlowFields.Process] = txtbShortTaskName.Text.Trim();
                    taskRow[TaskFlowFields.ProcessName] = txtbTaskName.Text.Trim();
                    taskRow[TaskFlowFields.ProcessDescription] = txtbDescription.Text.Trim();

                    stepText = att.StepText;
                    taskRow[TaskFlowFields.ProcessStepText] = stepText;

                    _dsTask.Tables[TaskFlowFields.TableName].Rows.Add(taskRow);

                    _dsTask.AcceptChanges();

                    dtAttributes = att.GetAttributeTable();

                    if (dtAttributes != null)
                    {
                        foreach (DataRow row in dtAttributes.Rows)
                        {
                            attributeRow = _dsTask.Tables[Attributes.TableName].NewRow();

                            attributeRow[Attributes.UID] = x;
                            attributeRow[Attributes.TaskFlow_UID] = i;
                            attributeRow[Attributes.Attribute_Name] = row[Attributes.Attribute_Name].ToString();
                            attributeRow[Attributes.Attribute_Value] = row[Attributes.Attribute_Value].ToString();
                            attributeRow[Attributes.Attribute_Caption] = row[Attributes.Attribute_Caption].ToString();
                            attributeRow[Attributes.Attribute_Instructions] = row[Attributes.Attribute_Instructions].ToString();
                            attributeRow[Attributes.Attribute_ValueOptions] = row[Attributes.Attribute_ValueOptions].ToString();

                            _dsTask.Tables[Attributes.TableName].Rows.Add(attributeRow);

                            _dsTask.AcceptChanges();

                            x++;
                        }
                    }

                }

            }

            _dsTask.AcceptChanges();


            string fileName =  string.Concat(txtbShortTaskName.Text.Trim(), ".tsk");
            string pathFile = Path.Combine(_TaskManager.TasksPath, fileName);

            if (File.Exists(pathFile))
            {
                Files.SetFileName2Obsolete(pathFile);
            }

            GenericDataManger gdMgr = new GenericDataManger();
            gdMgr.SaveDataXML(_dsTask, pathFile);

            string errorMsg = gdMgr.ErrorMessage;

            if (errorMsg.Length > 0)
            {
                if (errorMsg.IndexOf("is denied") != -1)
                {
                    errorMsg = string.Concat(errorMsg, Environment.NewLine, Environment.NewLine, "You may have insufficient privileges for this folder. Please check with your system administer.");
                }

                errorMsg = string.Concat("Saving Error:", Environment.NewLine, Environment.NewLine, errorMsg);

                txtbErrors.Text = errorMsg;

                panErrors.Visible = true;

                return false;

            }

            if (File.Exists(pathFile))
            {
                _TaskManager.SetDescription(txtbShortTaskName.Text.Trim(), this.txtbDescription.Text.Trim(), this.txtbTaskName.Text.Trim()); 
                
                return true;
            }
            else
            {
                return false;
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (txtbShortTaskName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(string.Concat("Please enter a short task name"));
                return;
            }

            if (txtbTaskName.Text.Trim() == string.Empty)
            {
                MessageBox.Show(string.Concat("Please enter a task name"));
                return;
            }

            if (!Validate())
                return;

            if (!CompileActionsIntoTask())
                return;
        
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void txtbDescription_TextChanged(object sender, EventArgs e)
        {

        }

    

 
    }
}
