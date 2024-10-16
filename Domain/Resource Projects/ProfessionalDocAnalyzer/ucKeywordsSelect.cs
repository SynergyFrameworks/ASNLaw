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
using System.Diagnostics;

using Atebion.Tasks;
using Atebion.Common;


namespace ProfessionalDocAnalyzer
{
    public partial class ucKeywordsSelect : UserControl
    {
        public ucKeywordsSelect()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        // Declare delegate for when a project has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Keyword Lib. is selected")]
        public event ProcessHandler KeywordLibSelected;

        [Category("Action")]
        [Description("Fires when Keyword selections are cleared")]
        public event ProcessHandler KeywordsNotSelected;


        private KeywordsMgr2 _KeywordsMgr;
        private Atebion.Tasks.Manager _TaskManager;
        private string _Task;

        private DataView _ViewAttributes;

     //   private string _KeywordsPath = string.Empty;

        public bool isWholeKeyword
        {
            get { return chkbFindWholeWords.Checked; }
        }

        public string[] Keywords
        {
            get { return lstbKeywords.CheckedItems.OfType<string>().ToArray(); }
        }

        private string _Task_ProcessObject = string.Empty;
        public string Task_ProcessObject
        {
            get { return _Task_ProcessObject; }
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
        
        public bool LoadData(Atebion.Tasks.Manager TaskManager, string Task)
        {
            _TaskManager = TaskManager;
            _Task = Task;

            _ViewAttributes = _TaskManager.GetTaskPropertiesAndAttributes(2);
            if (_ViewAttributes == null)
            {
                MessageBox.Show("Unable to get the current Task's information.", "Task Information Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            _Task_ProcessObject = _TaskManager.Task_ProcessObject_Current;

            _Next_TaskProcessObject = _TaskManager.Next_TaskProcessObject;

            _Next_TaskUID = _TaskManager.Next_TaskUID;

            _NextButtonType = _TaskManager.GetNextButtonType(_Next_TaskProcessObject, _Next_TaskUID);

            _isUseDefaultParseAnalysis = _TaskManager.isUseDefaultParseAnalysis();

            lblInstructions.Text = "Select one or more Keyword Libraries";
            LoadKeywordsGrps();
            return true;
        }

        public DataSet GetSelectedKeywords(out int CountSelected)
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
            
            foreach (int indexChecked in lstbKeywords.CheckedIndices)
            {
                keyword = lstbKeywords.Items[indexChecked].ToString();
                color = this.lstbKeywordColors.Items[indexChecked].ToString();
                keywordLib = this.lstbLibrary.Items[indexChecked].ToString();

                Keywords.Rows.Add(true, keyword, 0, color, keywordLib); // Category, Keyword, Color, Keyword Library

                i++;

            }


            CountSelected = i;

            // Added 09.24.2018
            string xmlKeywordsSelected = Path.Combine(AppFolders.DocParsedSecXML, "KeywordsSelected.xml");
            //if (File.Exists(xmlKeywordsSelected))
            //{
            //    File.Delete(xmlKeywordsSelected);
            //}

            GenericDataManger gDMgr = new GenericDataManger();
            //  gDMgr.SaveDataXML(UseKeywords, Path.Combine(AppFolders.DocParsedSecXML, "KeywordsSelected.xml"));
            gDMgr.SaveDataXML(UseKeywords, xmlKeywordsSelected);
            Application.DoEvents();
            gDMgr = null;

            return UseKeywords;
        }

        /// <summary>
        /// Load Keywords Groups
        /// </summary>
        /// <returns>QTY Found</returns>
        private int LoadKeywordsGrps()
        {
            this.lstbLibraries.Items.Clear();
            this.lstbKeywords.Items.Clear();

            string msg = Directories.DirExistsOrCreate(AppFolders.KeywordGrpPath);
            if (msg != string.Empty)
            {
                MessageBox.Show(msg, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return 0;
            }

            string[] files = Directory.GetFiles(AppFolders.KeywordGrpPath, "*.xml");

            int i = 0;
            string fileNameNoExt = string.Empty;

            foreach (string fileName in files)
            {
                fileNameNoExt = Files.GetFileNameWOExt(fileName);
                if (fileNameNoExt != string.Empty)
                {
                    lstbLibraries.Items.Add(fileNameNoExt);
                    i++;
                }
            }

            return i;
        }

        private void lstbKeywords_ItemCheck(object sender, ItemCheckEventArgs e)
        {

        }

        private bool RemoveItem(CheckedListBox lst, ListBox lstColors, string sItem)
        {

            for (int i = 0; i < lst.Items.Count; ++i)
            {
                string s = lst.Items[i].ToString();
                if (sItem == s)
                {
                    lstColors.Items.RemoveAt(i);
                    lst.Items.Remove(s);
                    lstbLibrary.Items.RemoveAt(i); ;
                    return true;
                }
            }

            return false;
        }

        private void ChangeColorItem(CheckedListBox lst, ListBox lstColors, string sItem, string color, string Library)
        {
            int i = 0;
            foreach (string s in lst.Items)
            {
                if (sItem == s)
                {
                    if (i < lstColors.Items.Count)
                    {
                        lstColors.Items[i] = color;
                        return;
                    }
                    return;
                }

                i++;
            }

            lst.Items.Add(sItem, true);
            lstColors.Items.Add(color);
            lstbLibrary.Items.Add(Library);
        }

        private void AddItem(CheckedListBox lst, ListBox lstColors, string sItem, string color, string KeywordLibrary)
        {
            foreach (string s in lst.Items)
            {
                if (sItem == s)
                {
                    return;
                }
            }

            lst.Items.Add(sItem, true);
            lstColors.Items.Add(color);
            lstbLibrary.Items.Add(KeywordLibrary);
        }

        private void lstbLibraries_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            string selKeyLib = lstbLibraries.Items[e.Index].ToString();
            string keywordFile = selKeyLib + ".xml";
            string keywordLibPathFile = Path.Combine(AppFolders.KeywordGrpPath, keywordFile);

            _KeywordsMgr = new KeywordsMgr2();

            DataSet ds = _KeywordsMgr.GetKeywordsLib(keywordLibPathFile, "YellowGreen");

            if (ds == null)
            {
                MessageBox.Show("Error: " + _KeywordsMgr.ErrorMessage, "Unable to Load Keyword Group", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            string sItem = string.Empty;
            string sColor = string.Empty;
            string sLib = lstbLibraries.Text;

            if (e.NewValue == CheckState.Unchecked)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sItem = dr[KeywordsFoundFields.Keyword].ToString();
                    RemoveItem(this.lstbKeywords, this.lstbKeywordColors, sItem);
                }

                if (lstbKeywords.Items.Count == 0)
                {
                    if (KeywordsNotSelected != null)
                        KeywordsNotSelected();
                }
            }
            else if (e.NewValue == CheckState.Checked)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    sItem = dr[KeywordsFoundFields.Keyword].ToString();
                    sColor = dr[KeywordsFoundFields.ColorHighlight].ToString();

                    if (lstbKeywords.FindStringExact(sItem) > 0) // Added 9.23.2018
                    {
                        ChangeColorItem(this.lstbKeywords, this.lstbKeywordColors, sItem, sColor, sLib);
                    }
                    else
                    {
                        AddItem(this.lstbKeywords, this.lstbKeywordColors, sItem, sColor, sLib);
                    }

                    // ToDo add Lib. Name
                    //lstbLibrary
                }

                if (KeywordLibSelected != null)
                    KeywordLibSelected();
            } 
        }
    }
}
