using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using Atebion.Common;


namespace Atebion.Tasks
{
    public class Manager
    {
        // <summary>
        /// For Local Workgroup
        /// </summary>
        /// <param name="PathLocalData">Local Data Path</param>
        /// <param name="UserName">User Name</param>
        public Manager(string PathLocalData, string UserName)
        {
            _PathLocalData = PathLocalData;
            _UserName = UserName;

            _isWorkgroupLocal = true;

            ValidateFix();
        }

        /// <summary>
        /// For Non-Local Workgroup
        /// </summary>
        /// <param name="PathLocalData">Local Data Path</param>
        /// <param name="PathWorkgroupRoot"></param>
        /// <param name="UserName">User Name</param>
        public Manager(string PathLocalData, string PathWorkgroupRoot, string UserName)
        {
            _PathLocalData = PathLocalData;
            _UserName = UserName;
            _PathWorkgroupRoot = PathWorkgroupRoot;

            _isWorkgroupLocal = false;

            ValidateFix();
        }

        private bool _isWorkgroupLocal = true;

        string _ErrorMessage = string.Empty;
        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        private string _PathLocalData = string.Empty;
        public string PathLocalData
        {
            get { return _PathLocalData; }
            set
            {
                _PathLocalData = value;

            }
        }

        private string _WorkgroupPath = string.Empty;

        private string _UserName = string.Empty;

        private string _PathWorkgroupRoot = string.Empty;
        public string PathCurrent
        {
            get { return _PathWorkgroupRoot; }
            set
            {
                _PathWorkgroupRoot = value;
            }
        }

        private string _TasksPath = string.Empty;
        public string TasksPath
        {
            get { return _TasksPath; }

        }

        private string _TaskCatalogue_PathFile = string.Empty;
        public string TaskCatalogue_PathFile
        {
            get { return _TaskCatalogue_PathFile; }
        }

        private DataSet _dsTaskCatalogue;
        public DataSet dsTaskCatalogue
        {
            get { return _dsTaskCatalogue; }
        }

        private int _TaskCurrentUID = -1;
        public int Task_UID_Current
        {
            get { return _TaskCurrentUID; }
        }

        private string _TaskCurrentProcessObject = string.Empty;
        public string Task_ProcessObject_Current
        {
            get { return _TaskCurrentProcessObject; }
        }

        private string _TaskCurrentProcess = string.Empty;
        public string Task_Process_Current
        {
            get { return _TaskCurrentProcess; }
        }

        private string _TaskCurrentProcessName = string.Empty;
        public string Task_ProcessName_Current
        {
            get {return _TaskCurrentProcessName;}
        }

        private string _TaskCurrentProcessDescription = string.Empty;
        public string Task_ProcessDescription_Current
        {
            get { return _TaskCurrentProcessDescription; }
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

        private DataSet _dsTask;

        //private ButtonNextAnalyze _is_ButtonNextAnalyze = ButtonNextAnalyze.Next; // Default
        //public ButtonNextAnalyze is_ButtonNextAnalyze
        //{
        //    get { return _is_ButtonNextAnalyze; }
        //}

        public enum ButtonNextAnalyze
        {
            Next = 0,
            Analyze = 1,
            Hide = 3,
            Create = 4,
            XRefMatrices = 5
        }

        #region Task Configuration

        //public System.Collections.Generic.Dictionary<string, string> Get_ProcessObject_Base_Level()
        //{

        //    string baseLevel = ProcessObject_Level.Base_Level;

        //    string[] baseLevels = baseLevel.Split('|');

        //    // Base_Level = string.Concat(ProcessObject.Parse, "|", ProcessObject.Summary, "|", ProcessObject.CompareDocsDiff, "|", ProcessObject.CompareDocsConcepts, "|", ProcessObject.Txt2Speech, "|", ProcessObject.AcroSeeker, "|", ProcessObject.ReadabilityTest);

        //    int count = baseLevels.Length;

        //    System.Collections.Generic.Dictionary<string, string> dicBaseLevels = new System.Collections.Generic.Dictionary<string, string>();

        //    for (int i = 0; i < count; i++)
        //    {
        //        switch (i)
        //        {
        //            case 0:
        //            dicBaseLevels.Add(ProcessObject.Parse, 
        //        }

        //    }

        //}

        private string _nullPlaceHolder = "~";

        public DataTable Get_Config_Attributes_for_ProcessObject(string SelectedProcessObject, int TaskFlow_UID)
        {
            
            switch (SelectedProcessObject)
            {
                case ProcessObject.Parse:
                    return Get_Config_Attributes_Parse(TaskFlow_UID);
                    

                case ProcessObject.FindKeywordsPerLib:
                    return Get_Config_Attributes_FindKeywordsPerLib(TaskFlow_UID);
                    

                case ProcessObject.FindDictionaryTerms:
                    return Get_Config_Attributes_FindDictionaryTerms(TaskFlow_UID);

                case ProcessObject.FindValues:
                    return Get_Config_Attributes_FindValues(TaskFlow_UID);

                //case ProcessObject.CompareDocsConcepts:
                //    return Get_Config_Attributes_CompareDocs(TaskFlow_UID);

                case ProcessObject.CompareDocsDictionary:
                    return Get_Config_Attributes_FindDictionaryTerms(TaskFlow_UID);
                //    return Get_Config_Attributes_CompareDocs(TaskFlow_UID);

                    // No Attributes are needed for the Display Analysis Get_Config_Attributes_ReadabilityTest -- Keep it Simple!
                //case ProcessObject.DisplayAnalysisResults:
                //    return Get_Config_Attributes_DisplayAnalysisResults(TaskFlow_UID);

                case ProcessObject.GenerateReport:
                    return Get_Config_Attributes_GenerateReport(TaskFlow_UID);

                //case ProcessObject.ReadabilityTest:
                //    return Get_Config_Attributes_ReadabilityTest(TaskFlow_UID);

                case ProcessObject.GenerateRAMRpt:
                    return Get_Config_Attributes_GenerateRAMRpt(TaskFlow_UID);

                case ProcessObject.ReadabilityTest:
                    return Get_Config_Attributes_ReadabilityTest(TaskFlow_UID);

                case ProcessObject.AcroSeeker:
                    return Get_Config_Attributes_AcroSeeker(TaskFlow_UID);

                case ProcessObject.DeepAnalyze:
                    return Get_Config_Attributes_DeepAnalyze(TaskFlow_UID);

                    

            }

            return null;
        }

        public string  GenerateReport_UseForValueOptions = string.Concat(ReportFor.CompareConcepts, "|", ReportFor.CompareDictionary, "|", ReportFor.DeepAnalysis, "|", ReportFor.ParseConcepts, "|", ReportFor.ParseDictionary, "|", ReportFor.ParseFARsDFARs, "|", ReportFor.ParseKeywords);

        private DataTable Get_Config_Attributes_GenerateReport(int TaskFlow_UID)
        {
            DataTable dt = createTable_Attributes();

            // ReportFileType
            DataRow row = dt.NewRow();
            row[Attributes.UID] = 0;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = GenerateReport_Attributes.ReportFileType;
            row[Attributes.Attribute_Caption] = GenerateReport_Attributes.ReportFileType_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = GenerateReport_Attributes.ReportFileType_ValueOptions;
            row[Attributes.Attribute_Instructions] = GenerateReport_Attributes.ReportFileType_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 1;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = GenerateReport_Attributes.UseExcelTemplate;
            row[Attributes.Attribute_Caption] = GenerateReport_Attributes.UseExcelTemplate_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = GenerateReport_Attributes.UseExcelTemplate_ValueOptions;
            row[Attributes.Attribute_Instructions] = GenerateReport_Attributes.UseExcelTemplate_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 2;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = GenerateReport_Attributes.UseWeightColors4Report;
            row[Attributes.Attribute_Caption] = GenerateReport_Attributes.UseWeightColors4Report_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = GenerateReport_Attributes.UseWeightColors4Report_ValueOptions;
            row[Attributes.Attribute_Instructions] = GenerateReport_Attributes.UseWeightColors4Report_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 3;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = GenerateReport_Attributes.UseFor;
            row[Attributes.Attribute_Caption] = GenerateReport_Attributes.UserFor_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = GenerateReport_Attributes.UseFor_ValueOptions;
            row[Attributes.Attribute_Instructions] = GenerateReport_Attributes.UseFor_Instructions;
            dt.Rows.Add(row);

  

            return dt;
        }

        private DataTable Get_Config_Attributes_DisplayAnalysisResults(int TaskFlow_UID)
        {
            DataTable dt = createTable_Attributes();

            // DisplayDeleteButton
            DataRow row = dt.NewRow();
            row[Attributes.UID] = 0;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = DisplayAnalysisResults_Attributes.DisplayDeleteButton;
            row[Attributes.Attribute_Caption] = DisplayAnalysisResults_Attributes.DisplayDeleteButton_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = DisplayAnalysisResults_Attributes.DisplayDeleteButton_ValueOptions;
            row[Attributes.Attribute_Instructions] = DisplayAnalysisResults_Attributes.DisplayDeleteButton_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 1;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = DisplayAnalysisResults_Attributes.DisplayEditButton;
            row[Attributes.Attribute_Caption] = DisplayAnalysisResults_Attributes.DisplayEditButton_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = DisplayAnalysisResults_Attributes.DisplayEditButton_ValueOptions;
            row[Attributes.Attribute_Instructions] = DisplayAnalysisResults_Attributes.DisplayEditButton_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 2;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = DisplayAnalysisResults_Attributes.DisplaySplitAndCombineButtons;
            row[Attributes.Attribute_Caption] = DisplayAnalysisResults_Attributes.DisplaySplitAndCombineButtons_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = DisplayAnalysisResults_Attributes.DisplaySplitAndCombineButtons_ValueOptions;
            row[Attributes.Attribute_Instructions] = DisplayAnalysisResults_Attributes.DisplaySplitAndCombineButtons_Instructions;
            dt.Rows.Add(row);

            return dt;

        }
 

        private DataTable Get_Config_Attributes_FindValues(int TaskFlow_UID)
        {
            DataTable dt = createTable_Attributes();

            // FindCSVs
            DataRow row = dt.NewRow();
            row[Attributes.UID] = 0;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = FindValues_Attributes.FindCSVs;
            row[Attributes.Attribute_Caption] = FindValues_Attributes.FindCSVs_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = FindValues_Attributes.FindCSVs_ValueOptions;
            row[Attributes.Attribute_Instructions] = FindValues_Attributes.FindCSVs_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 1;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = FindValues_Attributes.FindWholeWords;
            row[Attributes.Attribute_Caption] = FindValues_Attributes.FindWholeWords_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = FindValues_Attributes.FindWholeWords_ValueOptions;
            row[Attributes.Attribute_Instructions] = FindValues_Attributes.FindWholeWords_Instructions;
            dt.Rows.Add(row);

            return dt;

        }

        private DataTable Get_Config_Attributes_DeepAnalyze(int TaskFlow_UID)
        {
            DataTable dt = createTable_Attributes();

            DataRow row = dt.NewRow();
            row[Attributes.UID] = 0;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = DeepAnalyze_Attributes.EditAnalysisResults;
            row[Attributes.Attribute_Caption] = DeepAnalyze_Attributes.EditAnalysisResults_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = DeepAnalyze_Attributes.EditAnalysisResults_ValueOptions;
            row[Attributes.Attribute_Instructions] = DeepAnalyze_Attributes.EditAnalysisResults_Instructions;
            dt.Rows.Add(row);

            return dt;
        }

        private DataTable Get_Config_Attributes_AcroSeeker(int TaskFlow_UID)
        {
            DataTable dt = createTable_Attributes();

            // Dictionary
            DataRow row = dt.NewRow();
            row[Attributes.UID] = 0;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = AcroSeeker_Attributes.Dictionary1;
            row[Attributes.Attribute_Caption] = AcroSeeker_Attributes.Dictionary1_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = AcroSeeker_Attributes.Dictionary1_ValueOptions;
            row[Attributes.Attribute_Instructions] = AcroSeeker_Attributes.Dictionary1_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 1;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = AcroSeeker_Attributes.IgnoreDictionary;
            row[Attributes.Attribute_Caption] = AcroSeeker_Attributes.IgnoreDictionary_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = AcroSeeker_Attributes.IgnoreDictionary_ValueOptions;
            row[Attributes.Attribute_Instructions] = AcroSeeker_Attributes.IgnoreDictionary_Instructions;
            dt.Rows.Add(row);

            return dt;
        }

        public bool UseNumericHierarchy()
        {
            foreach (DataRow row in _dsTask.Tables["Attributes"].Rows)
            {
                if (row[Attributes.Attribute_Name].ToString() == Parse_Attributes.NumericalHierarchyConcatenation)
                {
                    if (row[Attributes.Attribute_Value].ToString() == "Yes")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        private DataTable Get_Config_Attributes_FindDictionaryTerms(int TaskFlow_UID)
        {
            DataTable dt = createTable_Attributes();

            // UserSelectsDictionaryLib
            DataRow row = dt.NewRow();
            row[Attributes.UID] = 0;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = FindDictionaryTerms_Attributes.UserSelectsDictionaryLib;
            row[Attributes.Attribute_Caption] = FindDictionaryTerms_Attributes.UserSelectsDictionaryLib_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = FindDictionaryTerms_Attributes.UserSelectsDictionaryLib_ValueOptions;
            row[Attributes.Attribute_Instructions] = FindDictionaryTerms_Attributes.UserSelectsDictionaryLib_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 1;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = FindDictionaryTerms_Attributes.FindWholeWords;
            row[Attributes.Attribute_Caption] = FindDictionaryTerms_Attributes.FindWholeWords_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = FindDictionaryTerms_Attributes.FindWholeWords_ValueOptions;
            row[Attributes.Attribute_Instructions] = FindDictionaryTerms_Attributes.FindWholeWords_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 2;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = FindDictionaryTerms_Attributes.FindSynonyms;
            row[Attributes.Attribute_Caption] = FindDictionaryTerms_Attributes.FindSynonyms_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = FindDictionaryTerms_Attributes.FindSynonyms_ValueOptions;
            row[Attributes.Attribute_Instructions] = FindDictionaryTerms_Attributes.FindSynonyms_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 3;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = FindDictionaryTerms_Attributes.UseDictionaryLibrary;
            row[Attributes.Attribute_Caption] = FindDictionaryTerms_Attributes.UseDictionaryLibrary_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = FindDictionaryTerms_Attributes.UseDictionaryLibrary_ValueOptions;
            row[Attributes.Attribute_Instructions] = FindDictionaryTerms_Attributes.UseDictionaryLibrary_Instructions;
            dt.Rows.Add(row);

            return dt;
        }



        private DataTable Get_Config_Attributes_FindKeywordsPerLib(int TaskFlow_UID)
        {
            DataTable dt = createTable_Attributes();
            // UserSelectsKeywordLib
            DataRow row = dt.NewRow();
            row[Attributes.UID] = 0;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = FindKeywordsPerLib_Attributes.UserSelectsKeywordLib;
            row[Attributes.Attribute_Caption] = FindKeywordsPerLib_Attributes.UserSelectsKeywordLib_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = FindKeywordsPerLib_Attributes.UserSelectsKeywordLib_ValueOptions;
            row[Attributes.Attribute_Instructions] = FindKeywordsPerLib_Attributes.UserSelectsKeywordLib_Instructions;
            dt.Rows.Add(row);

            //FindWholeWords
            row = dt.NewRow();
            row[Attributes.UID] = 1;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = FindKeywordsPerLib_Attributes.FindWholeWords;
            row[Attributes.Attribute_Caption] = FindKeywordsPerLib_Attributes.FindWholeWords_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = FindKeywordsPerLib_Attributes.FindWholeWords_ValueOptions;
            row[Attributes.Attribute_Instructions] = FindKeywordsPerLib_Attributes.FindWholeWords_Instructions;
            dt.Rows.Add(row);

            // UseKeywordLibrary
            row = dt.NewRow();
            row[Attributes.UID] = 1;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = FindKeywordsPerLib_Attributes.UseKeywordLibrary;
            row[Attributes.Attribute_Caption] = FindKeywordsPerLib_Attributes.UseKeywordLibrary_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = FindKeywordsPerLib_Attributes.UseKeywordLibrary_ValueOptions;
            row[Attributes.Attribute_Instructions] = FindKeywordsPerLib_Attributes.UseKeywordLibrary_Instructions;
            dt.Rows.Add(row);

            return dt;
        }

        private DataTable Get_Config_Attributes_CompareDocs(int TaskFlow_UID)
        {
            DataTable dt = createTable_Attributes();

            //Show_ParseType
            //DataRow row = dt.NewRow();
            //row[Attributes.UID] = 0;
            //row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            //row[Attributes.Attribute_Name] = CompareDocs_Attributes.Show_ParseType;
            //row[Attributes.Attribute_Caption] = CompareDocs_Attributes.Show_ParseType_Caption;
            //row[Attributes.Attribute_Value] = _nullPlaceHolder;
            //row[Attributes.Attribute_ValueOptions] = CompareDocs_Attributes.Show_ParseType_ValueOptions;
            //row[Attributes.Attribute_Instructions] = CompareDocs_Attributes.Show_ParseType_Instructions;
            //dt.Rows.Add(row);

            ////ParseType
            //row = dt.NewRow();
            //row[Attributes.UID] = 1;
            //row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            //row[Attributes.Attribute_Name] = CompareDocs_Attributes.ParseType;
            //row[Attributes.Attribute_Caption] = CompareDocs_Attributes.ParseType_Caption;
            //row[Attributes.Attribute_Value] = _nullPlaceHolder;
            //row[Attributes.Attribute_ValueOptions] = CompareDocs_Attributes.ParseType_ValueOptions;
            //row[Attributes.Attribute_Instructions] = CompareDocs_Attributes.ParseType_Instructions;
            //dt.Rows.Add(row);
 

            return dt;


        }

        private DataTable Get_Config_Attributes_Parse(int TaskFlow_UID)
        {
            DataTable dt = createTable_Attributes();

            // UseDefaultParseAnalysis
            DataRow row = dt.NewRow();
            row[Attributes.UID] = 0;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = Parse_Attributes.UseDefaultParseAnalysis;
            row[Attributes.Attribute_Caption] = Parse_Attributes.UseDefaultParseAnalysis_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = Parse_Attributes.UseDefaultParseAnalysis_ValueOptions;
            row[Attributes.Attribute_Instructions] = Parse_Attributes.UseDefaultParseAnalysis_Instructions;
            dt.Rows.Add(row);

            //Show_ParseType
            row = dt.NewRow();
            row[Attributes.UID] = 1;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = Parse_Attributes.Show_ParseType;
            row[Attributes.Attribute_Caption] = Parse_Attributes.Show_ParseType_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = Parse_Attributes.Show_ParseType_ValueOptions;
            row[Attributes.Attribute_Instructions] = Parse_Attributes.Show_ParseType_Instructions;
            dt.Rows.Add(row);

            //ParseType
            row = dt.NewRow();
            row[Attributes.UID] = 1;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = Parse_Attributes.ParseType;
            row[Attributes.Attribute_Caption] = Parse_Attributes.ParseType_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = Parse_Attributes.ParseType_ValueOptions;
            row[Attributes.Attribute_Instructions] = Parse_Attributes.ParseType_Instructions;
            dt.Rows.Add(row);

            //DocQty
            row = dt.NewRow();
            row[Attributes.UID] = 2;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = Parse_Attributes.DocQty;
            row[Attributes.Attribute_Caption] = Parse_Attributes.DocQty_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = Parse_Attributes.DocQty_ValueOptions;
            row[Attributes.Attribute_Instructions] = Parse_Attributes.DocQty_Instructions;
            dt.Rows.Add(row);

            //NumericalHierarchyConcatenation
            // Use Attribute for only Legal parsing type
            //row = dt.NewRow();
            //row[Attributes.UID] = 3;
            //row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            //row[Attributes.Attribute_Name] = Parse_Attributes.NumericalHierarchyConcatenation;
            //row[Attributes.Attribute_Caption] = Parse_Attributes.NumericalHierarchyConcatenation_Caption;
            //row[Attributes.Attribute_Value] = _nullPlaceHolder;
            //row[Attributes.Attribute_ValueOptions] = Parse_Attributes.Show_ParseType_ValueOptions;
            //row[Attributes.Attribute_Instructions] = Parse_Attributes.Show_ParseType_Instructions;
            //dt.Rows.Add(row);

            ////PrefixNumber
            //row = dt.NewRow();
            //row[Attributes.UID] = 4;
            //row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            //row[Attributes.Attribute_Name] = Parse_Attributes.PrefixNumber;
            //row[Attributes.Attribute_Caption] = Parse_Attributes.PrefixNumber_Caption;
            //row[Attributes.Attribute_Value] = _nullPlaceHolder;
            //row[Attributes.Attribute_ValueOptions] = Parse_Attributes.PrefixNumber_ValueOptions;
            //row[Attributes.Attribute_Instructions] = Parse_Attributes.PrefixNumber_Instructions;
            //dt.Rows.Add(row);

            return dt;


        }

        private DataTable Get_Config_Attributes_ReadabilityTest(int TaskFlow_UID)
        {
            DataTable dt = createTable_Attributes();

            //DataRow row = dt.NewRow();
            //row[Attributes.UID] = 0;
            //row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            //row[Attributes.Attribute_Name] = ReadabilityTest_Attributes.Find_Adverbs;
            //row[Attributes.Attribute_Caption] = ReadabilityTest_Attributes.Find_Adverbs_Caption;
            //row[Attributes.Attribute_Value] = "Yes";
            //row[Attributes.Attribute_ValueOptions] = ReadabilityTest_Attributes.Find_Adverbs_ValueOptions;
            //row[Attributes.Attribute_Instructions] = ReadabilityTest_Attributes.Find_Adverbs_Instructions;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row[Attributes.UID] = 1;
            //row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            //row[Attributes.Attribute_Name] = ReadabilityTest_Attributes.Find_ComplexWords;
            //row[Attributes.Attribute_Caption] = ReadabilityTest_Attributes.Find_ComplexWords_Caption;
            //row[Attributes.Attribute_Value] = "Yes";
            //row[Attributes.Attribute_ValueOptions] = ReadabilityTest_Attributes.Find_ComplexWords_ValueOptions;
            //row[Attributes.Attribute_Instructions] = ReadabilityTest_Attributes.Find_ComplexWords_Instructions;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row[Attributes.UID] = 2;
            //row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            //row[Attributes.Attribute_Name] = ReadabilityTest_Attributes.Find_LongSentences;
            //row[Attributes.Attribute_Caption] = ReadabilityTest_Attributes.Find_LongSentences_Caption;
            //row[Attributes.Attribute_Value] = "Yes";
            //row[Attributes.Attribute_ValueOptions] = ReadabilityTest_Attributes.Find_LongSentences_ValueOptions;
            //row[Attributes.Attribute_Instructions] = ReadabilityTest_Attributes.Find_LongSentences_Instructions;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row[Attributes.UID] = 3;
            //row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            //row[Attributes.Attribute_Name] = ReadabilityTest_Attributes.Words_LongSentences;
            //row[Attributes.Attribute_Caption] = ReadabilityTest_Attributes.Words_LongSentences_Caption;
            //row[Attributes.Attribute_Value] = "21";
            //row[Attributes.Attribute_ValueOptions] = ReadabilityTest_Attributes.Words_LongSentences_ValueOptions;
            //row[Attributes.Attribute_Instructions] = ReadabilityTest_Attributes.Words_LongSentences_Instructions;
            //dt.Rows.Add(row);

            //row = dt.NewRow();
            //row[Attributes.UID] = 4;
            //row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            //row[Attributes.Attribute_Name] = ReadabilityTest_Attributes.Find_PassiveVoice;
            //row[Attributes.Attribute_Caption] = ReadabilityTest_Attributes.Find_PassiveVoice_Caption;
            //row[Attributes.Attribute_Value] = "Yes";
            //row[Attributes.Attribute_ValueOptions] = ReadabilityTest_Attributes.Find_PassiveVoice_ValueOptions;
            //row[Attributes.Attribute_Instructions] = ReadabilityTest_Attributes.Find_PassiveVoice_Instructions;
            //dt.Rows.Add(row);

            DataRow row = dt.NewRow();
            row[Attributes.UID] = 0;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = ReadabilityTest_Attributes.UseDictionaryLibrary;
            row[Attributes.Attribute_Caption] = ReadabilityTest_Attributes.UseDictionaryLibrary_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = ReadabilityTest_Attributes.UseDictionaryLibrary_ValueOptions;
            row[Attributes.Attribute_Instructions] = ReadabilityTest_Attributes.UseDictionaryLibrary_Instructions;
            dt.Rows.Add(row);

            return dt;
        }

        private DataTable Get_Config_Attributes_GenerateRAMRpt(int TaskFlow_UID) 
        {

            DataTable dt = createTable_Attributes();

            DataRow row = dt.NewRow();
            row[Attributes.UID] = 0;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = GenerateRAM_Attributes.UseRAMTemplate;
            row[Attributes.Attribute_Caption] = GenerateRAM_Attributes.UseRAMTemplate_Caption;
            row[Attributes.Attribute_Value] = _nullPlaceHolder;
            row[Attributes.Attribute_ValueOptions] = GenerateRAM_Attributes.UseRAMTemplate_ValueOptions;
            row[Attributes.Attribute_Instructions] = GenerateRAM_Attributes.UseRAMTemplate_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 1;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = GenerateRAM_Attributes.FindSynonyms;
            row[Attributes.Attribute_Caption] = GenerateRAM_Attributes.FindSynonyms_Caption;
            row[Attributes.Attribute_Value] = "Yes";
            row[Attributes.Attribute_ValueOptions] = GenerateRAM_Attributes.FindSynonyms_ValueOptions;
            row[Attributes.Attribute_Instructions] = GenerateRAM_Attributes.FindSynonyms_Instructions;
            dt.Rows.Add(row);

            row = dt.NewRow();
            row[Attributes.UID] = 2;
            row[Attributes.TaskFlow_UID] = TaskFlow_UID;
            row[Attributes.Attribute_Name] = GenerateRAM_Attributes.UseColor;
            row[Attributes.Attribute_Caption] = GenerateRAM_Attributes.UseColor_Caption;
            row[Attributes.Attribute_Value] = "Yes";
            row[Attributes.Attribute_ValueOptions] = GenerateRAM_Attributes.UseColor_ValueOptions;
            row[Attributes.Attribute_Instructions] = GenerateRAM_Attributes.UseColor_Instructions;
            dt.Rows.Add(row);


            return dt;
        }


        #endregion

        public string GetTaskSummary(string Task)
        {
            _ErrorMessage = string.Empty;

            string fileName = string.Concat(Task, ".tsk");
            string pathFile = Path.Combine(_TasksPath, fileName);

            _dsTask = GetTaskFlow(Task);

            if (_dsTask == null)
                return _ErrorMessage;

            if (_dsTask.Tables[TaskFlowFields.TableName].Rows.Count == 0)
            {
                _ErrorMessage = string.Concat("No data was found for Task: ", pathFile);
                return null;
            }

            StringBuilder sb = new StringBuilder();
            string TaskCaption = string.Empty;
            string attributesForTask = string.Empty;
            string actionUID;
            string actionCaption = string.Empty;
            int i = 1;
            foreach (DataRow row in _dsTask.Tables[TaskFlowFields.TableName].Rows)
            {
                actionUID = row[TaskFlowFields.UID].ToString();

                TaskCaption = row[TaskFlowFields.ProcessObject].ToString();
                TaskCaption = string.Concat(i.ToString(), ".  ", TaskCaption);

                sb.AppendLine(TaskCaption);
                attributesForTask = GetAtrributesWithValuesPerTaskUID(actionUID);
                sb.AppendLine(attributesForTask);
                sb.AppendLine("");

                i++;

            }

            return sb.ToString();

        }

        private string GetAtrributesWithValuesPerTaskUID(string TaskUID)
        {
            StringBuilder sb = new StringBuilder();
            string attributeWithValue = string.Empty;

            foreach (DataRow row in _dsTask.Tables[Attributes.TableName].Rows)
            {
                if (row[Attributes.TaskFlow_UID].ToString() == TaskUID)
                {
                    attributeWithValue = string.Concat(row[Attributes.Attribute_Caption].ToString(), " = ", row[Attributes.Attribute_Value].ToString());
                    sb.AppendLine(attributeWithValue);
                }
            }

            return sb.ToString();
        }

        public string[] GetTaskSteps(string Task)
        {
            _ErrorMessage = string.Empty;

            string fileName = string.Concat(Task, ".tsk");
            string pathFile = Path.Combine(_TasksPath, fileName);

            //if (!File.Exists(pathFile))
            //{
            //    _ErrorMessage = string.Concat("Unable to find Task file: ", pathFile);
            //    return null;
            //}

            //DataSet _dsTask = Files.LoadDatasetFromXml(pathFile);
            //if (_dsTask == null)
            //{
            //    _ErrorMessage = Files.ErrorMessage;
            //    return null;
            //}

            _dsTask = GetTaskFlow(Task);

            if (_dsTask.Tables[TaskFlowFields.TableName].Rows.Count == 0)
            {
                _ErrorMessage = string.Concat("No data was found for Task: ", pathFile);
                return null;
            }

            string stepsDelim = _dsTask.Tables[TaskFlowFields.TableName].Rows[0][TaskFlowFields.ProcessStepText].ToString();
            if (stepsDelim.IndexOf('|') != -1)
            {
                string[] steps = stepsDelim.Split('|');

                return steps;
            }

            List<string> lstSteps = new List<string>();

            foreach (DataRow row in _dsTask.Tables[TaskFlowFields.TableName].Rows)
            {
                lstSteps.Add(row[TaskFlowFields.ProcessStepText].ToString());
            }

            return lstSteps.ToArray();
        }

        public DataSet GetTaskFlow(string Task)
        {
            _ErrorMessage = string.Empty;

            string fileName = string.Concat(Task, ".tsk");
            string pathFile = Path.Combine(_TasksPath, fileName);

            if (!File.Exists(pathFile))
            {
                _ErrorMessage = string.Concat("Unable to find Task file: ", pathFile);
                return null;
            }

            _dsTask = Files.LoadDatasetFromXml(pathFile);
            if (_dsTask == null)
            {
                _ErrorMessage = Files.ErrorMessage;
                return null;
            }

            return _dsTask;
        }

        public string GetProcessObject(string Task, int TaskNo)
        {
            _dsTask = GetTaskFlow(Task);

            if (_dsTask == null)
                return string.Empty;

            string processObject = string.Empty;

            int i = 1;
            foreach (DataRow row in _dsTask.Tables["TaskFlow"].Rows)
            {
                if (TaskNo == i)
                {
                    processObject = row["ProcessObject"].ToString();
                    return processObject;
                }

                i++;
            }

            return processObject;
        }

        public DataView GetTaskPropertiesAndAttributes(string Task, int TaskNo)
        {
            _dsTask = GetTaskFlow(Task);

            return GetTaskPropertiesAndAttributes(TaskNo);
        }

        public DataView GetTaskPropertiesAndAttributes(int TaskNo)
        {
            
            if (_dsTask == null)
                return null;

            DataView viewTasks;
            viewTasks = _dsTask.Tables["TaskFlow"].DefaultView;
          //  viewTasks.Sort = "SortOrder UID";

            if (viewTasks.Count == 0)
                return null;

            DataView viewAttributes = null;

            int i = 0; // changed from1 to 0 -- 06.05.2019
            foreach (DataRowView taskRow in viewTasks)
            {
                if (TaskNo == i)
                {
                    _TaskCurrentUID = Convert.ToInt32(taskRow["UID"].ToString());
                    _TaskCurrentProcessObject = taskRow["ProcessObject"].ToString();
                    _TaskCurrentProcess = taskRow["Process"].ToString();
                    _TaskCurrentProcessName = taskRow["ProcessName"].ToString();
                    _TaskCurrentProcessDescription = taskRow["ProcessDescription"].ToString();

                    string filter = string.Concat("TaskFlow_UID = ", _TaskCurrentUID);
                    viewAttributes = new DataView(_dsTask.Tables["Attributes"], filter, "UID", DataViewRowState.CurrentRows);


                    //_Next_TaskProcessObject = viewTasks[i]["ProcessObject"].ToString(); // Rows are zero based, but Atebion Tasks are one base. Therefore, Task #1 = Row 0
                    //_Next_TaskUID = Convert.ToInt32(viewTasks[i]["UID"].ToString());

//                    return viewAttributes;

                }
                else if(TaskNo + 1 == i) 
                {
                    if (viewAttributes == null)
                    {
                        _TaskCurrentUID = Convert.ToInt32(taskRow["UID"].ToString());
                        _TaskCurrentProcessObject = taskRow["ProcessObject"].ToString();
                        _TaskCurrentProcess = taskRow["Process"].ToString();
                        _TaskCurrentProcessName = taskRow["ProcessName"].ToString();
                        _TaskCurrentProcessDescription = taskRow["ProcessDescription"].ToString();

                        string filter = string.Concat("TaskFlow_UID = ", _TaskCurrentUID);
                        viewAttributes = new DataView(_dsTask.Tables["Attributes"], filter, "UID", DataViewRowState.CurrentRows);

                        _Next_TaskProcessObject = viewTasks[i]["ProcessObject"].ToString(); // Rows are zero based, but Atebion Tasks are one base. Therefore, Task #1 = Row 0
                        _Next_TaskUID = Convert.ToInt32(viewTasks[i]["UID"].ToString());
                    }
                    else
                    {
                        _Next_TaskProcessObject = viewTasks[i]["ProcessObject"].ToString(); // Rows are zero based, but Atebion Tasks are one base. Therefore, Task #1 = Row 0
                        _Next_TaskUID = Convert.ToInt32(viewTasks[i]["UID"].ToString());
                    }

                }

                i++;
            }

  

            return viewAttributes;
;

        }

        public bool isUseDefaultParseAnalysis()
        {
            foreach (DataRow row in _dsTask.Tables["Attributes"].Rows)
            {
                if (row[Attributes.Attribute_Name].ToString() == Parse_Attributes.UseDefaultParseAnalysis) 
                {
                    if (row[Attributes.Attribute_Value].ToString() == "Yes")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }

        public string GetParseType()
        {
            foreach (DataRow row in _dsTask.Tables["Attributes"].Rows)
            {
                if (row[Attributes.Attribute_Name].ToString() == Parse_Attributes.ParseType) 
                {
                    return row[Attributes.Attribute_Value].ToString();                   
                }
            }

            return string.Empty;
        }

        public bool isShow_ParseType()
        {
            foreach (DataRow row in _dsTask.Tables["Attributes"].Rows)
            {
                if (row[Attributes.Attribute_Name].ToString() == Parse_Attributes.Show_ParseType)
                {
                    if (row[Attributes.Attribute_Value].ToString() == "Yes")
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            return false;
        }
        

        public ButtonNextAnalyze GetNextButtonType(string NextProcessObject, int NextTaskUID)
        {
            ButtonNextAnalyze returnValue = ButtonNextAnalyze.Hide; // Default

            string filter = string.Concat("TaskFlow_UID = ", NextTaskUID);
            DataView viewAttributes = new DataView(_dsTask.Tables["Attributes"], filter, "UID", DataViewRowState.CurrentRows);

            switch (NextProcessObject)
            {
   
                case ProcessObject.FindKeywordsPerLib:
                    foreach (DataRowView row in viewAttributes)
                    {
                        if (row[Attributes.Attribute_Name].ToString() == FindKeywordsPerLib_Attributes.UserSelectsKeywordLib) // User selects the Keywords
                        {
                            if (row[Attributes.Attribute_Value].ToString() == "Yes")
                            {
                                returnValue = ButtonNextAnalyze.Next;
                            }
                            else // Keywords have been preselected per the Task parmeters
                            {
                                returnValue = ButtonNextAnalyze.Analyze;
                            }
                        }
                    }

                    break;


                case ProcessObject.FindDictionaryTerms:
                    foreach (DataRowView row in viewAttributes)
                    {
                        if (row[Attributes.Attribute_Name].ToString() == FindDictionaryTerms_Attributes.UserSelectsDictionaryLib) // User selects the Dictionary Lib.
                        {
                            if (row[Attributes.Attribute_Value].ToString() == "Yes")
                            {
                                returnValue = ButtonNextAnalyze.Next;
                            }
                            else // Keywords have been preselected per the Task parmeters
                            {
                                returnValue = ButtonNextAnalyze.Analyze;
                            }
                        }
                    }

                    break;

                case ProcessObject.CompareDocsDictionary:
                    foreach (DataRowView row in viewAttributes)
                    {
                        if (row[Attributes.Attribute_Name].ToString() == FindDictionaryTerms_Attributes.UserSelectsDictionaryLib) // User selects the Dictionary Lib.
                        {
                            if (row[Attributes.Attribute_Value].ToString() == "Yes")
                            {
                                returnValue = ButtonNextAnalyze.Next;
                            }
                            else // Keywords have been preselected per the Task parmeters
                            {
                                returnValue = ButtonNextAnalyze.Analyze;
                            }
                        }
                    }

                    break;

                case ProcessObject.DeepAnalyze:
                    returnValue = ButtonNextAnalyze.Analyze;

                    //foreach (DataRowView row in viewAttributes)
                    //{
                    //    if (row[Attributes.Attribute_Name].ToString() == DeepAnalyze_Attributes.EditAnalysisResults) 
                    //    {
                    //        if (row[Attributes.Attribute_Value].ToString() == "Yes")
                    //        {

                    //            returnValue = ButtonNextAnalyze.Analyze;
                    //        }
                    //        else // Keywords have been preselected per the Task parmeters
                    //        {
                    //            returnValue = ButtonNextAnalyze.Analyze;
                    //        }
                    //    }
                    //}

                    break;

                case ProcessObject.AcroSeeker:
                    returnValue = ButtonNextAnalyze.Analyze;
                    break;

                    // ToDo in a future release
                    //foreach (DataRowView row in viewAttributes)
                    //{
                    //    if (row[Attributes.Attribute_Name].ToString() == AcroSeeker_Attributes.Dictionary1_ValueOptions) // User selects the Dictionary Lib.
                    //    {
                    //        if (row[Attributes.Attribute_Value].ToString() == "Yes")
                    //        {
                    //            returnValue = ButtonNextAnalyze.Next;
                    //        }
                    //        else // Keywords have been preselected per the Task parmeters
                    //        {
                    //            returnValue = ButtonNextAnalyze.Analyze;
                    //        }
                    //    }

                    //}

                case ProcessObject.CompareDocsDiff:
                    returnValue = ButtonNextAnalyze.Analyze;
                    break;

                case ProcessObject.GenerateRAMRpt:
                   returnValue = ButtonNextAnalyze.Analyze;
                    break;

                case ProcessObject.FindConcepts:
                    returnValue = ButtonNextAnalyze.Analyze;
                    break;

                case ProcessObject.CompareDocsConcepts:
                    returnValue = ButtonNextAnalyze.Analyze;
                    break;

                case ProcessObject.FindValues:
                    returnValue = ButtonNextAnalyze.Analyze;
                    break;

                case ProcessObject.Parse:
                    returnValue = ButtonNextAnalyze.Analyze;
                    break;

                case ProcessObject.DisplayAnalysisResults:
                    returnValue = ButtonNextAnalyze.Analyze;
                    break;

                case ProcessObject.ReadabilityTest:
                    returnValue = ButtonNextAnalyze.Analyze;
                    break;

                case ProcessObject.GenerateReport:
                    returnValue = ButtonNextAnalyze.Analyze;
                    break;

            }

            return returnValue;
        }

        public bool SetDescription(string Task, string UpdatedDescription, string UpdatedLongTaskName)
        {
            _ErrorMessage = string.Empty;

            GetTasks(); // Refresh

            if (_dsTaskCatalogue == null)
                return false;

            string filter = string.Concat(TaskCatalogue.Task, " = '", Task, "'");

            DataRow[] rows = _dsTaskCatalogue.Tables[TaskCatalogue.TableName].Select(filter);

            if (rows.Length == 0)
                return false;

            rows[0][TaskCatalogue.TaskDescription] = UpdatedDescription;
            rows[0][TaskCatalogue.TaskName] = UpdatedLongTaskName;

            _dsTaskCatalogue.AcceptChanges();

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(_dsTaskCatalogue, _TaskCatalogue_PathFile);

            return true;

        }

        /// <summary>
        /// Gets the amount of documents that can be selected for a given Task
        /// </summary>
        /// <param name="Task">Name of Task</param>
        /// <returns>Qty of selectable documents</returns>
        public string GetDocSelectQty(string Task)
        {
            _ErrorMessage = string.Empty;

            string docQty = "1"; // Default value

            DataSet dsTask = GetTaskFlow(Task);
            if (dsTask == null)
                return string.Empty;

            string filter = string.Concat(Attributes.Attribute_Name, " = '", Parse_Attributes.DocQty, "'");
            DataRow[] rows = dsTask.Tables[Attributes.TableName].Select(filter);

            if (rows.Length == 0)
                return docQty; // Default value

            docQty = rows[0][Attributes.Attribute_Value].ToString();

            return docQty;
        }

        /// <summary>
        /// Gets the Keyword Library for the selected Task
        /// </summary>
        /// <param name="Task">Name of Task</param>
        /// <returns>KeywordLibrary</returns>
        public bool GetDoesUserSelectsKeywordLib(string Task)
        {
            _ErrorMessage = string.Empty;

            DataSet dsTask = GetTaskFlow(Task);
            if (dsTask == null)
                return true;

            string filter = string.Concat(Attributes.Attribute_Name, " = '", FindKeywordsPerLib_Attributes.UserSelectsKeywordLib, "'");
            DataRow[] rows = dsTask.Tables[Attributes.TableName].Select(filter);

            if (rows.Length == 0)
                return true; // Default value

            string doesUserSelectsKeywordLib = rows[0][Attributes.Attribute_Value].ToString();

            if (doesUserSelectsKeywordLib == "Yes")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Gets the Keyword Library for the selected Task
        /// </summary>
        /// <param name="Task">Name of Task</param>
        /// <returns>KeywordLibrary</returns>
        public string GetKeywordLibrary(string Task)
        {
            _ErrorMessage = string.Empty;

            DataSet dsTask = GetTaskFlow(Task);
            if (dsTask == null)
                return string.Empty;

            string filter = string.Concat(Attributes.Attribute_Name, " = '", FindKeywordsPerLib_Attributes.UseKeywordLibrary, "'");
            DataRow[] rows = dsTask.Tables[Attributes.TableName].Select(filter);

            if (rows.Length == 0)
                return string.Empty; // Default value

            string keywordLibrary = rows[0][Attributes.Attribute_Value].ToString();

            return keywordLibrary;
        }

        public bool UsedDicReportWeightColors(string Task)
        {
            _ErrorMessage = string.Empty;

            DataSet dsTask = GetTaskFlow(Task);
            if (dsTask == null)
                return false;

            string filter = string.Concat(Attributes.Attribute_Name, " = '", GenerateReport_Attributes.UseWeightColors4Report, "'");
            DataRow[] rows = dsTask.Tables[Attributes.TableName].Select(filter);

            if (rows.Length == 0)
                return false; // Default value

            string isWeightedColors = rows[0][Attributes.Attribute_Value].ToString();

            if (isWeightedColors == "Yes")
                return true;
            else
                return false;

        }

        /// <summary>
        /// Use Whole Words for Keyword Library
        /// </summary>
        /// <param name="Task">Name of Task</param>
        /// <returns>KeywordLibrary</returns>
        public bool GetKeywordIsWholeWord(string Task)
        {
            _ErrorMessage = string.Empty;

            DataSet dsTask = GetTaskFlow(Task);
            if (dsTask == null)
                return false;

            string filter = string.Concat(Attributes.Attribute_Name, " = '", FindKeywordsPerLib_Attributes.FindWholeWords, "'");
            DataRow[] rows = dsTask.Tables[Attributes.TableName].Select(filter);

            if (rows.Length == 0)
                return false; // Default value

            string isWholeWord  = rows[0][Attributes.Attribute_Value].ToString();

            if (isWholeWord == "Yes")
                return true;
            else
                return false;
        }

        /// <summary>
        /// Get Export File Type and Template Name if specified
        /// </summary>
        /// <param name="Task">Name of Task</param>
        /// <returns>Export File Type</returns>
        public string GetExportFileType(string Task, out string TemplateName)
        {
            _ErrorMessage = string.Empty;

            TemplateName = string.Empty;

            DataSet dsTask = GetTaskFlow(Task);
            if (dsTask == null)
                return string.Empty;

            string filter = string.Concat(Attributes.Attribute_Name, " = '", GenerateReport_Attributes.ReportFileType, "'");
            DataRow[] rows = dsTask.Tables[Attributes.TableName].Select(filter);

            if (rows.Length == 0)
                return string.Empty; // Default value

            string fileType = rows[0][Attributes.Attribute_Value].ToString();

            // Get ExcelTemplate Name, it specified
            if (dsTask.Tables[Attributes.TableName].Columns.Contains(Attributes.Attribute_Name))
            {
                filter = string.Concat(Attributes.Attribute_Name, " = '", GenerateReport_Attributes.UseExcelTemplate, "'");
                rows = dsTask.Tables[Attributes.TableName].Select(filter);
                if (rows.Length > 0)
                {
                    TemplateName = rows[0][Attributes.Attribute_Value].ToString();
                }
            }

            return fileType;
     
        }

        public DataSet GetTasks()
        {
            _ErrorMessage = string.Empty;
           
            if (_TaskCatalogue_PathFile.Trim() == string.Empty)
            {
                if (!ValidateFix())
                    return null;
            }

            if (!File.Exists(_TaskCatalogue_PathFile))
            {
                _ErrorMessage = string.Concat("Unable to locate Task Catalogue: ", _TaskCatalogue_PathFile);
                return null;
            }

            if (_dsTaskCatalogue == null)
            {
                GenericDataManger gdManager = new GenericDataManger();
                _dsTaskCatalogue = gdManager.LoadDatasetFromXml(_TaskCatalogue_PathFile);
                return _dsTaskCatalogue;
            }

            return _dsTaskCatalogue;
        }

        public bool ValidateFix()
        {
            _ErrorMessage = string.Empty;      

            if (_isWorkgroupLocal)
            {
                _TasksPath = Path.Combine(_PathLocalData, "Tasks");

            }
            else
            {
                _TasksPath = Path.Combine(_PathWorkgroupRoot, "Tasks");
            }

            if (!Directory.Exists(_TasksPath))
            {
                try
                {
                    Directory.CreateDirectory(_TasksPath);

                }
                catch (Exception ex)
                {
                    string errorMsg = string.Concat("An error occurred while creating a Tasks folder.   Error: ", ex.Message);
                    return false;
                }
            }

            _TaskCatalogue_PathFile = Path.Combine(_TasksPath, "TasksCatalogue.cat");

            string[] files = Directory.GetFiles(_TasksPath, "*.tsk");
            //if (files.Length == 0)
            //{
            //    GenerateDefualtTasks();
            //}

            if (files.Length > 0)
                PopulateTaskCatalogue(); // checks for Tasks that have not been included in the Catalogue and added them.

            
            return true;
        }

        public DataSet GenerateTaskCatalogue_DataSet()
        {
            DataTable dtTaskCatalogue = createTable_TaskCatalogue();
            if (_dsTaskCatalogue == null)
            {
                _dsTaskCatalogue = new DataSet();
            }
           
            _dsTaskCatalogue.Tables.Add(dtTaskCatalogue);

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(_dsTaskCatalogue, _TaskCatalogue_PathFile);


            return _dsTaskCatalogue;
        }

        public int CheckFixTaskCatalogue()
        {
            GenericDataManger gdManager = new GenericDataManger();

            if (!File.Exists(_TaskCatalogue_PathFile))
            {
                _dsTaskCatalogue = GenerateTaskCatalogue_DataSet();
            }
            else
            {
                _dsTaskCatalogue = gdManager.LoadDatasetFromXml(_TaskCatalogue_PathFile);
            }

            string[] files = Directory.GetFiles(_TasksPath, "*.tsk");
            if (files.Length == 0)
                return 0;

            DataSet dsTask;
            DataRow rowTask;
            DataRow rowCat;
            string processName = string.Empty;

            int countChanges = 0;

            string fileName = string.Empty;

            int sortOrderNumber = 0;

            GenericDataManger gDataManager = new GenericDataManger();

            foreach (string taskFile in files)
            {
                fileName = Files.GetFileNameWOExt(taskFile);

                if (fileName.IndexOf('~') == -1) // Check for files that have been removed
                {

                    if (!TaskExistsInCatalogue(fileName))
                    {
                        dsTask = gdManager.LoadDatasetFromXml(taskFile);

                        if (dsTask.Tables[TaskFlowFields.TableName].Rows.Count > 0)
                        {
                            rowTask = dsTask.Tables[TaskFlowFields.TableName].Rows[0];

                            processName = rowTask[TaskFlowFields.ProcessName].ToString();
                            sortOrderNumber = GetNextSortOrderNumber(_dsTaskCatalogue.Tables[TaskCatalogue.TableName]);

                            rowCat = _dsTaskCatalogue.Tables[TaskCatalogue.TableName].NewRow();

                            rowCat[TaskCatalogue.UID] = DataFunctions.GetNewUID(_dsTaskCatalogue.Tables[TaskCatalogue.TableName]);
                            rowCat[TaskCatalogue.SortOrder] = sortOrderNumber;
                            rowCat[TaskCatalogue.Task] = fileName;
                            rowCat[TaskCatalogue.TaskName] = processName;
                            rowCat[TaskCatalogue.TaskDescription] = rowTask[TaskFlowFields.ProcessDescription].ToString();
                            rowCat[TaskCatalogue.ShowTask] = true;

                            _dsTaskCatalogue.Tables[TaskCatalogue.TableName].Rows.Add(rowCat);

                            countChanges++;
                        }

                    }
                }
                
            }

            _dsTaskCatalogue.Tables[TaskCatalogue.TableName].AcceptChanges();

            string pathFile = string.Empty;

            foreach (DataRow rowCat2 in _dsTaskCatalogue.Tables[TaskCatalogue.TableName].Rows)
            {
                fileName = rowCat2[TaskCatalogue.Task].ToString();

                fileName = string.Concat(fileName, ".tsk");
                pathFile = Path.Combine(_TasksPath, fileName);

                if (!File.Exists(pathFile))
                {
                    rowCat2.Delete();
                    countChanges++;
                }
                else if (fileName.IndexOf('~') != -1) // Check for files that have been removed
                {
                    rowCat2.Delete();
                    countChanges++;
                }
            }

            _dsTaskCatalogue.Tables[TaskCatalogue.TableName].AcceptChanges();


            gdManager.SaveDataXML(_dsTaskCatalogue, _TaskCatalogue_PathFile);

            return countChanges;


        }

        public bool TaskExistsInCatalogue(string Task)
        {
            if (_dsTaskCatalogue == null)
                return false;

            foreach (DataRow rowCat in _dsTaskCatalogue.Tables[TaskCatalogue.TableName].Rows)
            {
                if (rowCat[TaskCatalogue.Task].ToString() == Task)
                {
                    return true;
                }
            }

            return false;
        }

        public int PopulateTaskCatalogue()
        {
            GenericDataManger gdManager = new GenericDataManger();

            if (!File.Exists(_TaskCatalogue_PathFile))
            {
                _dsTaskCatalogue = GenerateTaskCatalogue_DataSet();
            }
            else
            {    
                _dsTaskCatalogue = gdManager.LoadDatasetFromXml(_TaskCatalogue_PathFile);
            }



            string[] files = Directory.GetFiles(_TasksPath, "*.tsk");
            if (files.Length == 0)
                return 0;

            DataSet dsTask;
            DataRow rowTask;
            DataRow rowCat;
            string processName = string.Empty;

            int countAdded = 0;

            string fileName = string.Empty;

            int sortOrderNumber = 0;

            GenericDataManger gDataManager = new GenericDataManger();

            foreach (string taskFile in files)
            {
                dsTask = gdManager.LoadDatasetFromXml(taskFile);

                if (dsTask.Tables[TaskFlowFields.TableName].Rows.Count > 0)
                {
                    rowTask = dsTask.Tables[TaskFlowFields.TableName].Rows[0];
                    processName = rowTask[TaskFlowFields.ProcessName].ToString();
                    if (FindValueInDataTableAndGetUID(_dsTaskCatalogue.Tables[TaskCatalogue.TableName], TaskCatalogue.TaskName, processName) == -1) // not Found
                    {
                        sortOrderNumber = GetNextSortOrderNumber(_dsTaskCatalogue.Tables[TaskCatalogue.TableName]);

                        rowCat = _dsTaskCatalogue.Tables[TaskCatalogue.TableName].NewRow();
                        rowCat[TaskCatalogue.UID] = DataFunctions.GetNewUID(_dsTaskCatalogue.Tables[TaskCatalogue.TableName]);
                        rowCat[TaskCatalogue.SortOrder] = sortOrderNumber;

                        fileName = Files.GetFileNameWOExt(taskFile);
                        rowCat[TaskCatalogue.Task] = fileName;
                        rowCat[TaskCatalogue.TaskName] = processName;
                        rowCat[TaskCatalogue.TaskDescription] = rowTask[TaskFlowFields.ProcessDescription].ToString();
                        rowCat[TaskCatalogue.ShowTask] = true;

                        _dsTaskCatalogue.Tables[TaskCatalogue.TableName].Rows.Add(rowCat);

                        countAdded++;
                    }

                }
            }

            gdManager.SaveDataXML(_dsTaskCatalogue, _TaskCatalogue_PathFile);

            return countAdded;

        }

        public int GetNextSortOrderNumber(DataTable dt)
        {
            DataView dv = new DataView(dt);
            dv.Sort = "SortOrder";

            int sortOrder = 0;

            int count = dv.Count;
            if (count > 0)
            {
                sortOrder = Convert.ToInt32(dv[count - 1]["SortOrder"].ToString());
                sortOrder++;
            }

            return sortOrder;
        }

        public int FindValueInDataTableAndGetUID(DataTable dt, string InField, string sValue)
        {
            if (dt == null)
                return -1;

            if (dt.Rows.Count == 0)
                return -1;

            sValue = DataFunctions.ReplaceSingleQuote(sValue);

            DataRow[] foundValue = dt.Select(InField + " = '" + sValue +"'");
            if (foundValue.Length != 0)
            {

                int uid = Convert.ToInt32(foundValue[0]["UID"].ToString());

                return uid;
            }

            return -1; // Not Found
        }

        // Obsolete.. Tasks are outdated
        //public bool GenerateDefualtTasks()
        //{

        //    CreateProcess_QuickStartCM();

        //    CreateProcess_QuickStartCMWithAR();

        //    CreateProcess_QuickStartCMClassic();

        //    return true;
        //}

        private bool CreateProcess_GenerateShallList()
        {
            // --- Task Flow ---
            // Check if this Process already exists
            string fileName = string.Concat(Selectable_Processes.GenerateShallList_Process, ".tsk");
            string xmlPathFile = Path.Combine(_TasksPath, fileName);
            if (File.Exists(xmlPathFile))
            {
                return true;
            }

            DataSet dsTasks = CreateDataSet_Tasks();

            // --- Define process object: Legal Parse  ---
            int uid_ParseLegal = 1;

            DataRow row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_ParseLegal;
            row[TaskFlowFields.ProcessObject] = ProcessObject.Parse;
            row[TaskFlowFields.Process] = Selectable_Processes.GenerateShallList_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.GenerateShallList_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.GenerateShallList_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.GenerateShallList_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCM_Instructions;

            dsTasks.Tables[TaskFlowFields.TableName].Rows.Add(row);

            // ----- Attributes ----

            // Do Not Use Defualt Analysis
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_ParseLegal.ToString(), Parse_Attributes.UseDefaultParseAnalysis, Parse_Attributes.UseDefaultParseAnalysis_Caption, "No");

            // Document Qty
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_ParseLegal.ToString(), Parse_Attributes.DocQty, Parse_Attributes.DocQty_Caption, "1");

            // Parse Type
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_ParseLegal.ToString(), Parse_Attributes.ParseType, Parse_Attributes.ParseType_Caption, "Legal");


            // --- Define process object: Find Keywords ---
            int uid_Keywords = 2;
            row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_Keywords;
            row[TaskFlowFields.ProcessObject] = ProcessObject.FindValues;
            row[TaskFlowFields.Process] = Selectable_Processes.GenerateShallList_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.GenerateShallList_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.GenerateShallList_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.GenerateShallList_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCM_Instructions;

            dsTasks.Tables[TaskFlowFields.TableName].Rows.Add(row);

            // Find Value
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_ParseLegal.ToString(), Selectable_Attributes.FindCommaDelimitedValues, Selectable_Attributes.FindCommaDelimitedValues_Caption, "shall");

            // Find Whole Words
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Keywords.ToString(), Selectable_Attributes.FindWholeWords, Selectable_Attributes.FindWholeWords_Caption, "Yes");


            // --- Define process object: Export ---
            int uid_Export = 3;
            row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_Export;
            row[TaskFlowFields.ProcessObject] = ProcessObject.GenerateReport;
            row[TaskFlowFields.Process] = Selectable_Processes.GenerateShallList_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.GenerateShallList_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.GenerateShallList_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.GenerateShallList_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCM_Instructions;

            // Export Type
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Export.ToString(), Selectable_Attributes.ExportFileType, Selectable_Attributes.ExportFileType_Caption, "Excel");

            // Export Excel 
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Export.ToString(), Selectable_Attributes.UseExcelTemplate, Selectable_Attributes.UseExcelTemplate_Caption, "ShallList_Page");

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(dsTasks, xmlPathFile);

            return true;
        }

        private bool CreateProcess_QuickStartCM()
        {
            // --- Task Flow ---
            // Check if this Process already exists
            string fileName = string.Concat(Selectable_Processes.QuickStartCM_Process, ".tsk");
            string xmlPathFile = Path.Combine(_TasksPath, fileName);
            if (File.Exists(xmlPathFile))
            {
                return true;
            }

            DataSet dsTasks = CreateDataSet_Tasks();

            // --- Define process object: Legal Parse  ---
            int uid_ParseLegal = 1;

            DataRow row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_ParseLegal;
            row[TaskFlowFields.ProcessObject] = ProcessObject.Parse;
            row[TaskFlowFields.Process] = Selectable_Processes.QuickStartCM_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.QuickStartCM_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.QuickStartCM_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.QuickStartCM_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCM_Instructions;

            dsTasks.Tables[TaskFlowFields.TableName].Rows.Add(row);

            // ----- Attributes ----

            // Use Defualt Analysis
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_ParseLegal.ToString(), Parse_Attributes.UseDefaultParseAnalysis, Parse_Attributes.UseDefaultParseAnalysis_Caption, "Yes");  

            // Document Qty
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_ParseLegal.ToString(), Parse_Attributes.DocQty, Parse_Attributes.DocQty_Caption, "1");  

            // Parse Type
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_ParseLegal.ToString(), Parse_Attributes.ParseType, Parse_Attributes.ParseType_Caption, "Legal");


            // --- Define process object: Find Keywords ---
            int uid_Keywords = 2;
            row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_Keywords;
            row[TaskFlowFields.ProcessObject] = ProcessObject.FindKeywordsPerLib;
            row[TaskFlowFields.Process] = Selectable_Processes.QuickStartCM_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.QuickStartCM_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.QuickStartCM_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.QuickStartCM_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCM_Instructions;

            dsTasks.Tables[TaskFlowFields.TableName].Rows.Add(row);

            // Show Keywords Selectable
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Keywords.ToString(), FindKeywordsPerLib_Attributes.UserSelectsKeywordLib, FindKeywordsPerLib_Attributes.UserSelectsKeywordLib_Caption, "No");

            // User Selects Keyword Lib.
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Keywords.ToString(), FindKeywordsPerLib_Attributes.UseKeywordLibrary, FindKeywordsPerLib_Attributes.UseKeywordLibrary_Caption, "Required");

            // Find Whole Words
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Keywords.ToString(), FindKeywordsPerLib_Attributes.FindWholeWords, FindKeywordsPerLib_Attributes.FindWholeWords_Caption, "No");


            // --- Define process object: Export ---
            int uid_Export = 3;
            row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_Export;
            row[TaskFlowFields.ProcessObject] = ProcessObject.GenerateReport;
            row[TaskFlowFields.Process] = Selectable_Processes.QuickStartCM_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.QuickStartCM_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.QuickStartCM_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.QuickStartCM_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCM_Instructions;

            // Export Type
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Export.ToString(), GenerateReport_Attributes.ReportFileType, GenerateReport_Attributes.ReportFileType_Caption, "Excel");

            // Export Excel 
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Export.ToString(), GenerateReport_Attributes.UseExcelTemplate, GenerateReport_Attributes.UseExcelTemplate_Caption, "ShipleySection_C_Page");


            dsTasks.Tables[TaskFlowFields.TableName].Rows.Add(row);

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(dsTasks, xmlPathFile);


            return true;
        }

        private bool CreateProcess_QuickStartCMWithAR()
        {
            // --- Task Flow ---
            // Check if this Process already exists
            string file = string.Concat(Selectable_Processes.QuickStartCMWithAR_Process, ".tsk");
            string xmlPathFile = Path.Combine(_TasksPath, file);
            if (File.Exists(xmlPathFile))
            {
                return true;
            }

            // --- Define process object: Parse
            DataSet dsTasks = CreateDataSet_Tasks();
            int uid_Parse = 1;

            DataRow row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_Parse;
            row[TaskFlowFields.ProcessObject] = ProcessObject.Parse;
            row[TaskFlowFields.Process] = Selectable_Processes.QuickStartCMWithAR_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.QuickStartCMWithAR_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.QuickStartCMWithAR_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.QuickStartCMWithAR_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCMWithAR_Instructions;

            dsTasks.Tables[TaskFlowFields.TableName].Rows.Add(row);

            // ----- Attributes ----
            // Use Defualt Analysis
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Parse.ToString(), Parse_Attributes.UseDefaultParseAnalysis, Parse_Attributes.UseDefaultParseAnalysis_Caption, "Yes");  

            // Document Qty
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Parse.ToString(), Parse_Attributes.DocQty, Parse_Attributes.DocQty_Caption, "1");

            // Parse Type
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Parse.ToString(), Parse_Attributes.ParseType, Parse_Attributes.ParseType_Caption, "Legal");


            // --- Define process object: Find Keywords
            int uid_Keywords = 2;

            row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_Keywords;
            row[TaskFlowFields.ProcessObject] = ProcessObject.FindKeywordsPerLib;
            row[TaskFlowFields.Process] = Selectable_Processes.QuickStartCMWithAR_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.QuickStartCMWithAR_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.QuickStartCMWithAR_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.QuickStartCMWithAR_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCMWithAR_Instructions;

            dsTasks.Tables[TaskFlowFields.TableName].Rows.Add(row);

            // Show Keywords Selectable
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Keywords.ToString(), FindKeywordsPerLib_Attributes.UserSelectsKeywordLib, FindKeywordsPerLib_Attributes.UserSelectsKeywordLib_Caption, "No");

            // User Selects Keyword Lib. - Defualt
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Keywords.ToString(), FindKeywordsPerLib_Attributes.UseKeywordLibrary, FindKeywordsPerLib_Attributes.UseKeywordLibrary, "Required");

 
            // Find Whole Words - Default
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Keywords.ToString(), FindKeywordsPerLib_Attributes.FindWholeWords, FindKeywordsPerLib_Attributes.FindWholeWords_Caption, "No");

            // --- Define process object: Display Analysis Results
            int uid_AR = 3;

            row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_AR;
            row[TaskFlowFields.ProcessObject] = ProcessObject.DisplayAnalysisResults;
            row[TaskFlowFields.Process] = Selectable_Processes.QuickStartCMWithAR_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.QuickStartCMWithAR_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.QuickStartCMWithAR_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.QuickStartCMWithAR_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCMWithAR_Instructions;

            dsTasks.Tables[TaskFlowFields.TableName].Rows.Add(row);

            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_AR.ToString(), DisplayAnalysisResults_Attributes.DisplayDeleteButton, DisplayAnalysisResults_Attributes.DisplayDeleteButton_Caption, "Yes");
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_AR.ToString(), DisplayAnalysisResults_Attributes.DisplayEditButton, DisplayAnalysisResults_Attributes.DisplayEditButton_Caption, "Yes");
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_AR.ToString(), DisplayAnalysisResults_Attributes.DisplaySplitAndCombineButtons, DisplayAnalysisResults_Attributes.DisplaySplitAndCombineButtons_Caption, "Yes");

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(dsTasks, xmlPathFile);

            return true;
        }

        private bool CreateProcess_QuickStartCMClassic()
        {
            // --- Task Flow ---
            // Check if this Process already exists
            string file = string.Concat(Selectable_Processes.QuickStartCMClassic_Process, ".tsk");
            string xmlPathFile = Path.Combine(_TasksPath, file);
            if (File.Exists(xmlPathFile))
            {
                return true;
            }

            DataSet dsTasks = CreateDataSet_Tasks();

            // -- Define process object: Parse
            int uid_Parse = 1;

            DataRow row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_Parse;
            row[TaskFlowFields.ProcessObject] = ProcessObject.Parse;
            row[TaskFlowFields.Process] = Selectable_Processes.QuickStartCMClassic_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.QuickStartCMClassic_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.QuickStartCMClassic_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.QuickStartCMClassic_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCMClassic_Instructions;

            dsTasks.Tables[TaskFlowFields.TableName].Rows.Add(row);

            // ----- Attributes ----

            // Use Defualt Analysis
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Parse.ToString(), Parse_Attributes.UseDefaultParseAnalysis, Parse_Attributes.UseDefaultParseAnalysis_Caption, "Yes");  

            // Document Qty
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Parse.ToString(), Parse_Attributes.DocQty, Parse_Attributes.DocQty_Caption, "1");

            // Parse Type
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Parse.ToString(), Parse_Attributes.ParseType, Parse_Attributes.ParseType_Caption, "Legal");


            // Show Analysis Results panel
 //           Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Parse.ToString(), Selectable_Attributes.ShowAnalysisResults, Selectable_Attributes.ShowAnalysisResults_Caption, "Yes");


            // Identify
      //      Attribute_Add(dsTasks.Tables[Attributes.TableName], uid.ToString(), Selectable_Attributes.Identify, Selectable_Attributes.Identify_Caption, "Keywords");

            int uid_Keywords = 2;

            row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_Keywords;
            row[TaskFlowFields.ProcessObject] = ProcessObject.FindKeywordsPerLib;
            row[TaskFlowFields.Process] = Selectable_Processes.QuickStartCMClassic_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.QuickStartCMClassic_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.QuickStartCMClassic_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.QuickStartCMClassic_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCMClassic_Instructions;

            dsTasks.Tables[TaskFlowFields.TableName].Rows.Add(row);

            // Show Keywords Selectable
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_Keywords.ToString(), FindKeywordsPerLib_Attributes.UserSelectsKeywordLib, FindKeywordsPerLib_Attributes.UserSelectsKeywordLib_Caption, "Yes");


            // Display Analysis Results

            int uid_AR = 3;

            row = dsTasks.Tables[TaskFlowFields.TableName].NewRow();

            row[TaskFlowFields.UID] = uid_AR;
            row[TaskFlowFields.ProcessObject] = ProcessObject.DisplayAnalysisResults;
            row[TaskFlowFields.Process] = Selectable_Processes.QuickStartCMClassic_Process;
            row[TaskFlowFields.ProcessDescription] = Selectable_Processes.QuickStartCMClassic_Description;
            row[TaskFlowFields.ProcessName] = Selectable_Processes.QuickStartCMClassic_Name;
            row[TaskFlowFields.ProcessStepText] = Selectable_Processes.QuickStartCMClassic_StepText;
            row[TaskFlowFields.ProcessInstructions] = Selectable_Processes.QuickStartCMClassic_Instructions;

            dsTasks.Tables[TaskFlowFields.TableName].Rows.Add(row);

            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_AR.ToString(), DisplayAnalysisResults_Attributes.DisplayDeleteButton, DisplayAnalysisResults_Attributes.DisplayDeleteButton_Caption, "Yes");
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_AR.ToString(), DisplayAnalysisResults_Attributes.DisplayEditButton, DisplayAnalysisResults_Attributes.DisplayEditButton_Caption, "Yes");
            Attribute_Add(dsTasks.Tables[Attributes.TableName], uid_AR.ToString(), DisplayAnalysisResults_Attributes.DisplaySplitAndCombineButtons, DisplayAnalysisResults_Attributes.DisplaySplitAndCombineButtons_Caption, "Yes");

            GenericDataManger gdManager = new GenericDataManger();

            gdManager.SaveDataXML(dsTasks, xmlPathFile);

            return true;
        }

        public int Attribute_Add(DataTable dt, string Task_UID, string Attribute_Name, string Attribute_Caption, string Attribute_Value)
        {
            DataRow row = dt.NewRow();

            int uidAttributes = Atebion.Common.DataFunctions.GetNewUID(dt);

            row[Attributes.UID] = uidAttributes;
            row[Attributes.TaskFlow_UID] = Task_UID;

            row[Attributes.Attribute_Name] = Attribute_Name;
            row[Attributes.Attribute_Caption] = Attribute_Caption;
            row[Attributes.Attribute_Value] = Attribute_Value;

            dt.Rows.Add(row);

            return uidAttributes;

        }

        public DataSet CreateDataSet_Tasks()
        {
            DataSet dsTasks = new DataSet();

            DataTable dtTaskFlow = createTable_TaskFlow();
            DataTable dtAttributes = createTable_Attributes();

            dsTasks.Tables.Add(dtTaskFlow);
            dsTasks.Tables.Add(dtAttributes);

            return dsTasks;

        }


        public DataSet CreateTaskCatalogue()
        {
             DataSet dsTaskCatalogue = new DataSet();

             DataTable dtTaskCatalogue = createTable_TaskCatalogue();
            
              dsTaskCatalogue.Tables.Add(dtTaskCatalogue);

            return dsTaskCatalogue;

        }
 

        public DataTable createTable_TaskFlow()
        {
            DataTable table = new DataTable(TaskFlowFields.TableName);

            table.Columns.Add(TaskFlowFields.UID, typeof(int));
            table.Columns.Add(TaskFlowFields.ProcessObject, typeof(string));
            table.Columns.Add(TaskFlowFields.Process, typeof(string));
            table.Columns.Add(TaskFlowFields.ProcessName, typeof(string));
            table.Columns.Add(TaskFlowFields.ProcessDescription, typeof(string));
            table.Columns.Add(TaskFlowFields.ProcessInstructions, typeof(string));
            table.Columns.Add(TaskFlowFields.ProcessStepText, typeof(string));

            return table;

        }

        public DataTable createTable_Attributes()
        {
            DataTable table = new DataTable(Attributes.TableName);

            table.Columns.Add(Attributes.UID, typeof(int));
            table.Columns.Add(Attributes.TaskFlow_UID, typeof(int)); // Foreign Key
            table.Columns.Add(Attributes.Process, typeof(string)); // Foreign Key
            table.Columns.Add(Attributes.Attribute_Name, typeof(string));
            table.Columns.Add(Attributes.Attribute_Value, typeof(string));
            table.Columns.Add(Attributes.Attribute_Caption, typeof(string));
            table.Columns.Add(Attributes.Attribute_Instructions, typeof(string));
            table.Columns.Add(Attributes.Attribute_ValueOptions, typeof(string));




            return table;
        }

        private DataTable createTable_TaskCatalogue()
        {
            DataTable table = new DataTable(TaskCatalogue.TableName);

            table.Columns.Add(TaskCatalogue.UID, typeof(int));
            table.Columns.Add(TaskCatalogue.SortOrder, typeof(int));
            table.Columns.Add(TaskCatalogue.Task, typeof(string));
            table.Columns.Add(TaskCatalogue.TaskName, typeof(string));
            table.Columns.Add(TaskCatalogue.TaskDescription, typeof(string));
            table.Columns.Add(TaskCatalogue.ShowTask, typeof(bool)); 

            return table;
        }




    }
}
