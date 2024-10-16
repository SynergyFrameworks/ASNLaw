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

using Atebion.Common;
using Atebion.Tasks;

namespace ProfessionalDocAnalyzer
{
    public partial class ucParse_Attributes : UserControl
    {
        public ucParse_Attributes()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }


        // Declare delegate for when a project has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when an Action has been selected")]
        public event ProcessHandler ActionSelected;

        [Category("Action")]
        [Description("Fires when Parse attribute UseDefaultParseAnalysis has changed")]
        public event ProcessHandler UseDefaultParseAnalysisChanged;


        private const string _NONE = "-- None --";

        bool _HasLoaded = false;
        string _PagesRequired = string.Empty;
        public string PagesRequired
        {
            get { return _PagesRequired; }
            set { _PagesRequired = value; }
    }

        private int _ActionNo = 1;
        public int ActionNo
        {
            get { return _ActionNo; }
            set
            {
                _ActionNo = value;
                lblAction.Text = string.Concat("Action ", _ActionNo.ToString());
            }
        }

        private string _Action = string.Empty;
        public string Action
        {
            get 
            {
                _Action =  cboAction.Text;
                return _Action;
            }
            set
            {
                _Action = value;
                int index = cboAction.FindStringExact(_Action);
                cboAction.SelectedIndex = index;
            }
        }

        public string StepText
        {
            get { return txtbStepText.Text.Trim(); }
            set { txtbStepText.Text = value; }
        }

        private bool _UseDefaultParseAnalysis = false;
        public bool UseDefaultParseAnalysis
        {
            get { return _UseDefaultParseAnalysis; }
        }

        private string _ReportForAction = string.Empty; // only used when an Action is for Generating a Report
        public string ReportForAction
        {
            get { return _ReportForAction; }
            set { _ReportForAction = value; }
        }

        private string[] _Actions;
        private DataTable _dtAttributes;
        private bool _isNew = false;

        private enum ActionMode
        {
            // Level 1
            Prase, // 2nd Level w/ Compares
            Summary,
            CompareDocsDiff,
            Txt2Speech,
            AcroSeeker,
            ReadabilityTest,
            CompareDocsValues,
            CompareDocsKeywords,
            CompareDocsConcepts,
            CompareDocsDictionaryTerms,



            // Level 2
            DeepAnalyze,
            FindValues,
            FindKeywordsPerLib,
            FindConcepts,
            FindDictionaryTerms,
            FindMetaData,

            // Levels 2 or 3
            DisplayAnalysisResults,
            GenerateReport,

        }

        public void LoadActions(string[] actions)
        {
            _Actions = actions;

            cboAction.Items.Clear();
            cboAction.Items.Add(_NONE);

            foreach (string action in actions)
            {
                cboAction.Items.Add(action);
            }

          //  panBody.Visible = false;

            ClearAttributes();

            this.Visible = true;
        }

        public bool CheckPages(string RequiredValue)
        {
            string caption = string.Empty;
            string valueOptions = string.Empty;
            string lblName = string.Empty;
            string cboName = string.Empty;

            if (RequiredValue == string.Empty)
                return true;

            //if (!_HasLoaded)
            //    return true;

          //  Control[] controls;

            string[] values;
            if (RequiredValue.IndexOf("|") != -1)
            {
                values = RequiredValue.Split('|');
            }
            else
            {
                values = new string[] { RequiredValue };
            }

            int docQty_i = -1;

            for (int i = 1; i < 9; i++)
            {
                lblName = string.Concat("lblAttribute", i.ToString());

                if (this.Controls.Find(lblName, true)[0].Text == Atebion.Tasks.Parse_Attributes.DocQty_Caption)
                {
                    cboName = string.Concat("cboAttribute", i.ToString());

                    docQty_i = i;

                    foreach (string valueOption in values)
                    {
                        if (this.Controls.Find(cboName, true)[0].Text == valueOption)
                        {
                            this.Controls.Find(lblName, true)[0].ForeColor = Color.Black;
                            return true;
                        }
                    }
                }
            }

            if (docQty_i > -1)
            {
                lblName = string.Concat("lblAttribute", docQty_i.ToString());

                this.Controls.Find(lblName, true)[0].ForeColor = Color.Red;

                if (values[0] == "2")
                {
                    string msgCaption = string.Concat("Change ", Atebion.Tasks.Parse_Attributes.DocQty_Caption, " to be greater than one");
                    MessageBox.Show("When comparing documents, more than one document is needed.", msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else if (values[0] == "1")
                {
                    string msgCaption = string.Concat("Change ", Atebion.Tasks.Parse_Attributes.DocQty_Caption, " to be equal to one");
                    MessageBox.Show("Some Actions can only work with one document at a time.", msgCaption, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                
            }

            return false;

            
        }

        public int LoadAttributes(DataTable dtAttributes, bool isNew)
        {
            _dtAttributes = dtAttributes;

             _isNew = isNew;
           

            HideClearAttributes();

            if (dtAttributes == null || dtAttributes.Rows.Count == 0)
            {
              //  MessageBox.Show("Unable to find the Action’s Attributes!", "Action’s Attributes NOT Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return 0;
            }

            string caption = string.Empty;
            string valueOptions = string.Empty;
            string name = string.Empty;
            string instructions = string.Empty;

            int attNo = 1;

            foreach (DataRow row in _dtAttributes.Rows)
            {
                name = row[Attributes.Attribute_Name].ToString();
                valueOptions = row[Attributes.Attribute_ValueOptions].ToString();
                caption = row[Attributes.Attribute_Caption].ToString();
                instructions = row[Attributes.Attribute_Instructions].ToString();

                populateAttribute(attNo, caption, valueOptions);

                attNo++;

            }

            if (!_isNew)
            {
                MakeSelections();
            }

            return attNo;

        }

        /// <summary>
        /// Return an Emplty String if no valiations exists
        /// </summary>
        /// <returns></returns>
        public string Validate()
        {
            string error = string.Empty;

            if (cboAction.Text == string.Empty || cboAction.Text.IndexOf("None") > -1)
            {
                return string.Empty;
            }

            string lblName = string.Empty;
            string cboName = string.Empty;

            Control[] controls;
            ComboBox cbo;

            string caption = string.Empty;

            for (int i = 1; i < 9; i++)
            {

                cboName = string.Concat("cboAttribute", i.ToString());
                controls = this.Controls.Find(cboName, true);
                cbo = (ComboBox)controls[0];
                if (cbo.Visible)
                {
                    lblName = string.Concat("lblAttribute", i.ToString());

                    if (cbo.Text == string.Empty)
                    {
                        if (_Action != ProcessObject.ReadabilityTest)
                        {
                            caption = this.Controls.Find(lblName, true)[0].Text;
                            this.Controls.Find(lblName, true)[0].ForeColor = Color.Red;

                            error = string.Concat(error, caption, " - Value not selected", Environment.NewLine);
                        }
                        else
                        {
                            this.Controls.Find(lblName, true)[0].ForeColor = Color.Black;
                        }
                    }
                    else
                    {
                        this.Controls.Find(lblName, true)[0].ForeColor = Color.Black;

                    }
                }
            }

            if (error.Length > 0)
            {
                error = string.Concat("Action ", _ActionNo.ToString(), " - ", cboAction.Text, Environment.NewLine, error, Environment.NewLine, Environment.NewLine);
            }

            return error;
        }

        public DataTable GetAttributeTable()
        {
            if (_dtAttributes == null)
                return null;
        
            Control[] controls;
            ComboBox cbo;
            string cboName = string.Empty;

            string lblName = string.Empty;
            string attributeCaption = string.Empty;
            string value = string.Empty;


            int i = 1;

            foreach (DataRow row in _dtAttributes.Rows)
            {
                cboName = string.Concat("cboAttribute", i.ToString());
                controls = this.Controls.Find(cboName, true);
                cbo = (ComboBox)controls[0];

                lblName = string.Concat("lblAttribute", i.ToString());
                attributeCaption = this.Controls.Find(lblName, true)[0].Text;


                if (cbo.Visible || (_Action == ProcessObject.GenerateReport && attributeCaption == GenerateReport_Attributes.UserFor_Caption))
                {
                    value = cbo.Text;

                    row[Attributes.Attribute_Value] = value;

                    _dtAttributes.AcceptChanges();
                }

                // Testing code
                //if (attributeCaption == GenerateReport_Attributes.UserFor_Caption)
                //{
                //    string xc = cbo.Text;

                //}

                i++;
            }

            return _dtAttributes;
        }

        private bool MakeSelections()
        {

            string caption = string.Empty;
            string value = string.Empty;
            string name = string.Empty;
            string instructions = string.Empty;

            string error = string.Empty;
            bool found = false;
            
            int attNo = 1;

            foreach (DataRow row in _dtAttributes.Rows)
            {
                name = row[Attributes.Attribute_Name].ToString();
                value = row[Attributes.Attribute_Value].ToString();
                caption = row[Attributes.Attribute_Caption].ToString();
                instructions = row[Attributes.Attribute_Instructions].ToString();

                found = SelectValue(attNo, caption, value);
                if (!found)
                {
                    if (error.Length == 0)
                    {
                        error = string.Concat("Unable to find settings for ", caption);
                    }
                    else
                    {
                        error = string.Concat(error, ", ", caption);

                    }

                }

                attNo++;

            }

            if (error.Length == 0)
                return true;
            else
            {
                lblMessage.Text = error;
                txtbMessage.ForeColor = Color.DarkRed;
                return false;
            }

        }

        private bool SelectValue(int AttributeNo, string AttributeCaption, string AttributeValue)
        {

            string lblName = string.Concat("lblAttribute", AttributeNo.ToString());
            string cboName = string.Concat("cboAttribute", AttributeNo.ToString());
            string butName = string.Concat("butAttribute", AttributeNo.ToString());

            Control[] controls;


            this.Controls.Find(lblName, true)[0].Text = AttributeCaption;

            if (AttributeValue != "*")
            {
                controls = this.Controls.Find(cboName, true);
                ComboBox cbo = (ComboBox)controls[0];
                int index = cbo.FindStringExact(AttributeValue);
                cbo.SelectedIndex = index;

                return true;
            }
            else
            {
                if (_Action == ProcessObject.Parse)
                {
                    if (Controls.Find(lblName, true)[0].Text == AttributeCaption)
                    {
                        this.Controls.Find(lblName, true)[0].Visible = true;

                        controls = this.Controls.Find(cboName, true);
                        ComboBox cbo = (ComboBox)controls[0];
                        cbo.Visible = true;

                        controls = this.Controls.Find(butName, true);
                        MetroFramework.Controls.MetroButton but = (MetroFramework.Controls.MetroButton)controls[0];
                        but.Visible = true;
                        //but.Tag = AttributeNo;

                    }
                }
            }

            return false;

        }

        public void HideClearAttributes()
        {
            lblAttribute1.Text = string.Empty;
            cboAttribute1.Items.Clear();
            lblAttribute1.Visible = false;
            cboAttribute1.Visible = false;
            butAttribute1.Visible = false;

            lblAttribute2.Text = string.Empty;
            cboAttribute2.Items.Clear();
            lblAttribute2.Visible = false;
            cboAttribute2.Visible = false;
            butAttribute2.Visible = false;

            lblAttribute3.Text = string.Empty;
            cboAttribute3.Items.Clear();
            lblAttribute3.Visible = false;
            cboAttribute3.Visible = false;
            butAttribute3.Visible = false;

            lblAttribute4.Text = string.Empty;
            cboAttribute4.Items.Clear();
            lblAttribute4.Visible = false;
            cboAttribute4.Visible = false;
            butAttribute4.Visible = false;

            lblAttribute5.Text = string.Empty;
            cboAttribute5.Items.Clear();
            lblAttribute5.Visible = false;
            cboAttribute5.Visible = false;
            butAttribute5.Visible = false;

            lblAttribute6.Text = string.Empty;
            cboAttribute6.Items.Clear();
            lblAttribute6.Visible = false;
            cboAttribute6.Visible = false;
            butAttribute6.Visible = false;

            lblAttribute6.Text = string.Empty;
            cboAttribute6.Items.Clear();
            lblAttribute6.Visible = false;
            cboAttribute6.Visible = false;
            butAttribute6.Visible = false;

            lblAttribute7.Text = string.Empty;
            cboAttribute7.Items.Clear();
            lblAttribute7.Visible = false;
            cboAttribute7.Visible = false;
            butAttribute7.Visible = false;

            lblAttribute8.Text = string.Empty;
            cboAttribute8.Items.Clear();
            lblAttribute8.Visible = false;
            cboAttribute8.Visible = false;
            butAttribute8.Visible = false;


        }


        private void populateAttribute(int AttributeNo, string AttributeCaption, string AttributeValue)
        {
            string[] values;
            if (AttributeValue.IndexOf('|') != -1)
            {
                values = AttributeValue.Split('|');
            }
            else
            {
                values = null;
            }

            string lblName = string.Concat("lblAttribute", AttributeNo.ToString());
            string cboName = string.Concat("cboAttribute", AttributeNo.ToString());
            string butName = string.Concat("butAttribute", AttributeNo.ToString());

            Control[] controls;

      
            this.Controls.Find(lblName, true)[0].Text = AttributeCaption;

            controls = this.Controls.Find(cboName, true);
            ComboBox cbo = (ComboBox)controls[0];

            if (values != null)
            {
                if (_ReportForAction == ReportFor.ParseDictionary || _ReportForAction == ReportFor.CompareDictionary)
                {
                    if (AttributeCaption == GenerateReport_Attributes.ReportFileType_Caption)
                    {
                        cbo.Items.Add("Excel");
                    }
                    else
                    {
                        foreach (string value in values)
                        {
                            //  cboAttribute1.Items.Add(value);
                            cbo.Items.Add(value);
                            
                        }
                    }
                }
                else
                {
                    foreach (string value in values)
                    {
                        //  cboAttribute1.Items.Add(value);
                        cbo.Items.Add(value);
                        
                    }
                }

                this.Controls.Find(lblName, true)[0].Visible = true;
                this.Controls.Find(cboName, true)[0].Visible = true;


                if (_Action == ProcessObject.GenerateReport)
                {

                    if (AttributeCaption == GenerateReport_Attributes.UserFor_Caption)
                    {
                        cbo.Items.Clear();
                        cbo.Items.Add(_ReportForAction);
                        cbo.SelectedIndex = 0;

                        this.Controls.Find(lblName, true)[0].Visible = false;
                        this.Controls.Find(cboName, true)[0].Visible = false;

                    }

                    if (_ReportForAction == ReportFor.ParseDictionary || _ReportForAction == ReportFor.CompareDictionary)
                    {
                        if (AttributeCaption == GenerateReport_Attributes.UseWeightColors4Report_Caption)
                        {
                            this.Controls.Find(lblName, true)[0].Visible = true;
                            this.Controls.Find(cboName, true)[0].Visible = true;
                        }

                    }
                    else
                    {
                        if (AttributeCaption == GenerateReport_Attributes.UseWeightColors4Report_Caption)
                        {
                            this.Controls.Find(lblName, true)[0].Visible = false;
                            this.Controls.Find(cboName, true)[0].Visible = false;
                        }

                    }


                }
                

            }
            //else
            //{
                if (_Action == ProcessObject.FindKeywordsPerLib) // Load Keyword Groups
                {
                    if (AttributeCaption == FindKeywordsPerLib_Attributes.UseKeywordLibrary_Caption)
                    {
                        string[] files = Directory.GetFiles(AppFolders.KeywordGrpPath, "*.xml");

                        int count = LoadFiles(files, cbo);

                        if (count == 0)
                        {
                            Controls.Find(lblName, true)[0].ForeColor = Color.Red;

                            lblMessage.Text = "No Keyword Groups were found for your current Workgroup. Suggest downloading, importing or creating a Keyword Group prior to creating a Task.";
                            txtbMessage.ForeColor = Color.Red;
                        }
                    }
                }
                else if (_Action == ProcessObject.ReadabilityTest)
                {
                    if (AttributeCaption == FindDictionaryTerms_Attributes.UseDictionaryLibrary_Caption)
                    {
                        cbo.Items.Add("");

                        string[] files = Directory.GetFiles(AppFolders.DictionariesPath, "*.dic");

                        int count = LoadFiles(files, cbo);

                        if (count == 0)
                        {
                            Controls.Find(lblName, true)[0].ForeColor = Color.Red;

                            lblMessage.Text = "No Dictionaries were found for your current Workgroup. Suggest downloading, importing or creating a Dictionary prior to creating a Task.";
                            txtbMessage.ForeColor = Color.Red;
                        }
                        else
                        {
                            this.Controls.Find(lblName, true)[0].Visible = true;
                            cbo.Visible = true;
                        }
                    }
                }
                else if (_Action == ProcessObject.FindDictionaryTerms || _Action == ProcessObject.CompareDocsDictionary) // Load Dictionaries
                {
                    if (AttributeCaption == FindDictionaryTerms_Attributes.UseDictionaryLibrary_Caption)
                    {
                        string[] files = Directory.GetFiles(AppFolders.DictionariesPath, "*.dic");

                        int count = LoadFiles(files, cbo);

                        if (count == 0)
                        {
                            Controls.Find(lblName, true)[0].ForeColor = Color.Red;

                            lblMessage.Text = "No Dictionaries were found for your current Workgroup. Suggest downloading, importing or creating a Dictionary prior to creating a Task.";
                            txtbMessage.ForeColor = Color.Red;
                        }
                    }
                }
                else if (_Action == ProcessObject.FindValues) 
                {
                    if (AttributeCaption == FindValues_Attributes.FindCSVs_Caption)
                    {
                        this.Controls.Find(lblName, true)[0].Visible = true;
                        this.Controls.Find(cboName, true)[0].Visible = true;
                        this.Controls.Find(butName, true)[0].Visible = true;

                    }
                }
                else if (_Action == ProcessObject.GenerateRAMRpt)
                {
                    if (cboName == "cboAttribute1")
                    {
                        string[] files;

                        files = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempDicRAM, "*.xml");
                        PopulateReportCombo(cbo, files);

                        this.Controls.Find(lblName, true)[0].Visible = true;
                        this.Controls.Find(cboName, true)[0].Visible = true;
                    }

                }
                else if (_Action == ProcessObject.AcroSeeker)
                {
                    string[] files;

                    if (cboName == "cboAttribute1")
                    {
                        files = Directory.GetFiles(AppFolders.AppDataPathToolsAcroSeekerDefLib, "*.xml");
                        PopulateReportCombo(cbo, files);

                        this.Controls.Find(lblName, true)[0].Visible = true;
                        this.Controls.Find(cboName, true)[0].Visible = true;
                    }
                    else if (cboName == "cboAttribute2")
                    {
                        files = Directory.GetFiles(AppFolders.AppDataPathToolsAcroSeekerIgnoreLib, "*.xml");
                        PopulateReportCombo(cbo, files);

                        this.Controls.Find(lblName, true)[0].Visible = true;
                        this.Controls.Find(cboName, true)[0].Visible = true;
                    }

                   
                }
                else if (_Action == ProcessObject.GenerateReport)
                {
                    if (AttributeCaption == GenerateReport_Attributes.UseExcelTemplate_Caption)
                    {
                        cbo.Items.Clear();

                        string[] files;

                        if (_ReportForAction == ProcessObject.FindKeywordsPerLib)
                        {
                            files = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempAR);
                            PopulateReportCombo(cbo, files);
                        }
                        else if (_ReportForAction == ProcessObject.DeepAnalyze)
                        {
                            files = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempDAR);
                            PopulateReportCombo(cbo, files);
                        }
                        else if (_ReportForAction == ReportFor.ParseConcepts)
                        {
                            files = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempConceptsDoc, "*.xml");
                            PopulateReportCombo(cbo, files);
                        }
                        else if (_ReportForAction == ReportFor.ParseDictionary)
                        {
                            files = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempDicDoc, "*.xml");
                            PopulateReportCombo(cbo, files);
                        }
                        else if (_ReportForAction == ProcessObject.CompareDocsConcepts)
                        {
                            files = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempConceptsDocs);
                            PopulateReportCombo(cbo, files);
                        }
                        else if (_ReportForAction == ProcessObject.CompareDocsDictionary)
                        {
                            files = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempDicDocs, "*.xml");
                            PopulateReportCombo(cbo, files);
                        }
                        else if (_ReportForAction == ProcessObject.FindValues)
                        {
                            files = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempCSVDoc);
                            PopulateReportCombo(cbo, files);
                        }
                        else if (_ReportForAction == ProcessObject.FindFARs)
                        {
                            files = Directory.GetFiles(AppFolders.AppDataPathToolsExcelTempFARDoc);
                            PopulateReportCombo(cbo, files);
                        }

                        this.Controls.Find(lblName, true)[0].Visible = true;
                        this.Controls.Find(cboName, true)[0].Visible = true;
                        this.Controls.Find(butName, true)[0].Visible = true;
                    }
                }

                


                // _ReportForAction

                
                //else
                //{
                //    this.Controls.Find(butName, true)[0].Visible = true;
                //}
            //}
                             
        }

        private int PopulateReportCombo(ComboBox cbo, string[] files)
        {
            cbo.Items.Clear();

            if (files == null)
                return 0;

            //cbo.Items.Add("");

            int i = 0;
            string fileName = string.Empty;
            foreach (string file in files)
            {
                fileName = Files.GetFileNameWOExt(file);
                if (!cbo.Items.Contains(fileName))
                    cbo.Items.Add(fileName);

                i++;
            }

            return i;
        }

        private int LoadFiles(string[] files, ComboBox cbo)
        {
            int i = 0;
            string fileNameNoExt = string.Empty;

            foreach (string fileName in files)
            {
                fileNameNoExt = Files.GetFileNameWOExt(fileName);
                if (fileNameNoExt != string.Empty)
                {
                    cbo.Items.Add(fileNameNoExt);
                    i++;
                }
            }

            return i;
        }



        //private int Load_Parse_Attributes_1of1()
        //{
        //    string caption = string.Empty;
        //    string value = string.Empty;
        //    string name = string.Empty;
        //    string instructions = string.Empty;

        //    int attNo = 1;


        //    foreach (DataRow row in _dtAttributes.Rows)
        //    {

        //        name = row[Attributes.Attribute_Name].ToString();
        //        value = row[Attributes.Attribute_Value].ToString();
        //        caption = row[Attributes.Attribute_Caption].ToString();
        //        instructions = row[Attributes.Attribute_Instructions].ToString();

        //        populateAttribute(attNo, caption, value);

        //        if (name == Parse_Attributes.UseDefaultParseAnalysis)
        //            return attNo;

        //        attNo++;

        //    }

        //    return attNo;

        //}

        //private int Load_Parse_Attributes_2of2()
        //{
        //    string caption = string.Empty;
        //    string value = string.Empty;
        //    string name = string.Empty;
        //    string instructions = string.Empty;

        //    int attNo = 1;


        //    foreach (DataRow row in _dtAttributes.Rows)
        //    {
        //        if (attNo > 1)
        //        {
        //            name = row[Attributes.Attribute_Name].ToString();
        //            value = row[Attributes.Attribute_Value].ToString();
        //            caption = row[Attributes.Attribute_Caption].ToString();
        //            instructions = row[Attributes.Attribute_Instructions].ToString();

        //            populateAttribute(attNo, caption, value);
        //        }

        //        attNo++;

        //    }

        //    return attNo;

        //}

        private void ClearAttributes()
    {
        lblAttribute1.Text = string.Empty;
            lblAttribute2.Text = string.Empty;
            lblAttribute3.Text = string.Empty;
            lblAttribute4.Text = string.Empty;
            lblAttribute5.Text = string.Empty;
            lblAttribute6.Text = string.Empty;
            lblAttribute7.Text = string.Empty;
            lblAttribute8.Text = string.Empty;

           cboAttribute1.Items.Clear();
           cboAttribute2.Items.Clear();
           cboAttribute3.Items.Clear();
           cboAttribute4.Items.Clear();
           cboAttribute5.Items.Clear();
           cboAttribute6.Items.Clear();
           cboAttribute7.Items.Clear();
           cboAttribute8.Items.Clear();

    }

        private string GetInstructions(int AttributeNo)
        {
            string instructions = string.Empty;

           int attNo = 1;

           if (_dtAttributes == null)
               return string.Empty;

            foreach (DataRow row in _dtAttributes.Rows)
            {
                if (AttributeNo == attNo)
                {
                    instructions = row[Attributes.Attribute_Instructions].ToString();
                    return instructions;
                }

                attNo++;

            }

            return instructions;
        }





        private void txtbMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void cboAttribute1_MouseEnter(object sender, EventArgs e)
        {
            string instructions = string.Empty;
            if (cboAttribute1.Text.Length > 14)
            {
                instructions = string.Concat(lblAttribute1.Text, " = ", cboAttribute1.Text);
            }
            else
            {
                instructions = GetInstructions(1);
                instructions = string.Concat(lblAttribute1.Text, " - ", instructions);
            }
            lblMessage.Text = instructions;
        }

        private void cboAttribute2_MouseEnter(object sender, EventArgs e)
        {
            //string instructions = GetInstructions(2);
            //lblMessage.Text = string.Concat(lblAttribute2.Text, " - ", instructions);

            string instructions = string.Empty;
            if (cboAttribute2.Text.Length > 14)
            {
                instructions = string.Concat(lblAttribute2.Text, " = ", cboAttribute2.Text);
            }
            else
            {
                instructions = GetInstructions(2);
                instructions = string.Concat(lblAttribute2.Text, " - ", instructions);
            }
            lblMessage.Text = instructions;
        }

        private void cboAttribute3_MouseEnter(object sender, EventArgs e)
        {
   
            string instructions = string.Empty;
            if (cboAttribute3.Text.Length > 14)
            {
                instructions = string.Concat(lblAttribute3.Text, " = ", cboAttribute3.Text);
            }
            else
            {
                instructions = GetInstructions(3);
                instructions = string.Concat(lblAttribute3.Text, " - ", instructions);
            }
            lblMessage.Text = instructions;

        }

        private void cboAttribute4_MouseEnter(object sender, EventArgs e)
        {
            string instructions = string.Empty;
            if (cboAttribute4.Text.Length > 14)
            {
                instructions = string.Concat(lblAttribute4.Text, " = ", cboAttribute4.Text);
            }
            else
            {
                instructions = GetInstructions(4);
                instructions = string.Concat(lblAttribute4.Text, " - ", instructions);
            }
            lblMessage.Text = instructions;
        }

        private void cboAttribute5_MouseEnter(object sender, EventArgs e)
        {
            string instructions = string.Empty;
            if (cboAttribute5.Text.Length > 14)
            {
                instructions = string.Concat(lblAttribute5.Text, " = ", cboAttribute5.Text);
            }
            else
            {
                instructions = GetInstructions(5);
                instructions = string.Concat(lblAttribute5.Text, " - ", instructions);
            }
            lblMessage.Text = instructions;
        }

        private void cboAttribute6_MouseEnter(object sender, EventArgs e)
        {
            string instructions = string.Empty;
            if (cboAttribute6.Text.Length > 14)
            {
                instructions = string.Concat(lblAttribute6.Text, " = ", cboAttribute6.Text);
            }
            else
            {
                instructions = GetInstructions(6);
                instructions = string.Concat(lblAttribute6.Text, " - ", instructions);
            }
            lblMessage.Text = instructions;
        }

        private void cboAttribute7_MouseEnter(object sender, EventArgs e)
        {
            string instructions = string.Empty;
            if (cboAttribute7.Text.Length > 14)
            {
                instructions = string.Concat(lblAttribute7.Text, " = ", cboAttribute7.Text);
            }
            else
            {
                instructions = GetInstructions(7);
                instructions = string.Concat(lblAttribute7.Text, " - ", instructions);
            }
            lblMessage.Text = instructions;
        }

        private void cboAttribute8_MouseEnter(object sender, EventArgs e)
        {
            string instructions = string.Empty;
            if (cboAttribute8.Text.Length > 14)
            {
                instructions = string.Concat(lblAttribute8.Text, " = ", cboAttribute8.Text);
            }
            else
            {
                instructions = GetInstructions(8);
                instructions = string.Concat(lblAttribute8.Text, " - ", instructions);
            }
            lblMessage.Text = instructions;
        }

        private void cboAttribute1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 0;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.cboAttribute1, cboAttribute1.Items[cboAttribute1.SelectedIndex].ToString());

            AdjustAttributesPerSelection(1);
        }

        private void AdjustAttributesPerSelection(int AttributeNo)
        {
            string lblName = string.Concat("lblAttribute", AttributeNo.ToString());
            string cboName = string.Concat("cboAttribute", AttributeNo.ToString());
            string butName = string.Concat("butAttribute", AttributeNo.ToString());

            Control[] controls;

            controls = this.Controls.Find(lblName, true);
            Label lbl = (Label)controls[0];
            
            controls = this.Controls.Find(cboName, true);
            ComboBox cbo = (ComboBox)controls[0];

            controls = this.Controls.Find(butName, true);
            MetroFramework.Controls.MetroButton but = (MetroFramework.Controls.MetroButton)controls[0];


            if (_Action == ProcessObject.Parse)
            {
                if (lbl.Text == Parse_Attributes.UseDefaultParseAnalysis_Caption)
                {
                    if (cbo.Text == "Yes")
                        {
                            _UseDefaultParseAnalysis = true;
                        }
                        else
                        {
                            _UseDefaultParseAnalysis = false;
                        }

                        if (UseDefaultParseAnalysisChanged != null)
                            UseDefaultParseAnalysisChanged();
                }
                else if (lbl.Text == Parse_Attributes.ParseType_Caption)
                {
                    if (cbo.Text == "Legal")
                    {
                        ShowAtribute(Parse_Attributes.NumericalHierarchyConcatenation_Caption);
                    }
                    else
                    {
                        HideAtribute(Parse_Attributes.NumericalHierarchyConcatenation_Caption);
                    }
                }
                else if (lbl.Text == Parse_Attributes.Show_ParseType_Caption)
                {
                    if (cbo.Text == "No")
                    {
                        ShowAtribute(Parse_Attributes.ParseType_Caption);
                    }
                    else
                    {
                        HideAtribute(Parse_Attributes.ParseType_Caption);
                    }
                }

            }
            else if (_Action == ProcessObject.FindKeywordsPerLib)
            {
                if (lbl.Text == FindKeywordsPerLib_Attributes.UserSelectsKeywordLib_Caption)
                {
                    if (cbo.Text == "Yes")
                    {
                        HideAtribute(FindKeywordsPerLib_Attributes.UseKeywordLibrary_Caption);
                        HideAtribute(FindKeywordsPerLib_Attributes.FindWholeWords_Caption);
                    }
                    else
                    {
                        ShowAtribute(FindKeywordsPerLib_Attributes.UseKeywordLibrary_Caption);
                        ShowAtribute(FindKeywordsPerLib_Attributes.FindWholeWords_Caption);
                    }
                }
            }
            else if (_Action == ProcessObject.FindDictionaryTerms || _Action == ProcessObject.CompareDocsDictionary)
            {
                if (lbl.Text == FindDictionaryTerms_Attributes.UserSelectsDictionaryLib_Caption)
                {
                    if (cbo.Text == "Yes")
                    {
                        HideAtribute(FindDictionaryTerms_Attributes.UseDictionaryLibrary_Caption);
                        HideAtribute(FindDictionaryTerms_Attributes.FindWholeWords_Caption);
                        HideAtribute(FindDictionaryTerms_Attributes.FindSynonyms_Caption);
                    }
                    else
                    {
                        ShowAtribute(FindDictionaryTerms_Attributes.UseDictionaryLibrary_Caption);
                        ShowAtribute(FindDictionaryTerms_Attributes.FindWholeWords_Caption);
                        ShowAtribute(FindDictionaryTerms_Attributes.FindSynonyms_Caption);
                    }
                }
            }
            else if (_Action == ProcessObject.FindValues)
            {
                if (lbl.Text == FindValues_Attributes.FindCSVs_Caption)
                {
                    but.Visible = true;
                    but.Tag = FindValues_Attributes.FindCSVs;
                    
                }
            }
            else if (_Action == ProcessObject.GenerateReport)
            {
                if (lbl.Text == GenerateReport_Attributes.ReportFileType_Caption)
                {
                    if (cbo.Text == "Excel")
                    {
                        ShowAtribute(GenerateReport_Attributes.UseExcelTemplate_Caption);
                    }
                    else
                    {
                        HideAtribute(GenerateReport_Attributes.UseExcelTemplate_Caption);
                    }
                }
            }
            else if (_Action == ProcessObject.ReadabilityTest)
            {
                //if (lbl.Text == ReadabilityTest_Attributes.Find_LongSentences_Caption)
                //{
                //    if (cbo.Text == "Yes")
                //    {
                //        ShowAtribute(ReadabilityTest_Attributes.Words_LongSentences_Caption);
                //    }
                //    else
                //    {
                //        HideAtribute(ReadabilityTest_Attributes.Words_LongSentences_Caption);
                //    }

                //}

            }
 

            //FindValues_Attributes

        }

        private bool HideAtribute(string AttributeCaption)
        {
            string lblName = string.Empty;
            string cboName = string.Empty;
            string butName = string.Empty;

            Control[] controls;

            for (int AttributeNo = 1; AttributeNo < 9; AttributeNo++)
            {
                lblName = string.Concat("lblAttribute", AttributeNo.ToString());

                controls = this.Controls.Find(lblName, true);
                Label lbl = (Label)controls[0];

                if (lbl.Text == AttributeCaption)
                {
                    lbl.Visible = false;

                    cboName = string.Concat("cboAttribute", AttributeNo.ToString());
                    controls = this.Controls.Find(cboName, true);
                    ComboBox cbo = (ComboBox)controls[0];
                    cbo.Visible = false;

                    butName = string.Concat("butAttribute", AttributeNo.ToString());
                    controls = this.Controls.Find(butName, true);
                    MetroFramework.Controls.MetroButton but = (MetroFramework.Controls.MetroButton)controls[0];
                    but.Visible = false;

                    return true;
                }

            }

            return false;

        }

        private bool ShowAtribute(string AttributeCaption)
        {
            string lblName = string.Empty;
            string cboName = string.Empty;
            string butName = string.Empty;

            Control[] controls;

            for (int AttributeNo = 1; AttributeNo < 9; AttributeNo++)
            {
                lblName = string.Concat("lblAttribute", AttributeNo.ToString());

                controls = this.Controls.Find(lblName, true);
                Label lbl = (Label)controls[0];

                if (lbl.Text == AttributeCaption)
                {
                    lbl.Visible = true;

                    cboName = string.Concat("cboAttribute", AttributeNo.ToString());
                    controls = this.Controls.Find(cboName, true);
                    ComboBox cbo = (ComboBox)controls[0];
                    cbo.Visible = true;

                    butName = string.Concat("butAttribute", AttributeNo.ToString());
                    controls = this.Controls.Find(butName, true);
                    MetroFramework.Controls.MetroButton but = (MetroFramework.Controls.MetroButton)controls[0];
                    if (but.Tag != null)
                    {
                        but.Visible = true;
                    }
                    else
                    {
                        but.Visible = false;
                    }

                    return true;
                }

            }

            return false;


        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void cboAction_SelectedIndexChanged(object sender, EventArgs e)
        {
            ToolTip toolTip1 = new ToolTip();
            toolTip1.AutoPopDelay = 0;
            toolTip1.InitialDelay = 0;
            toolTip1.ReshowDelay = 0;
            toolTip1.ShowAlways = true;
            toolTip1.SetToolTip(this.cboAction, cboAction.Items[cboAction.SelectedIndex].ToString()) ;
        

            if (ActionSelected != null)
                ActionSelected();
        }

        private void butAttribute1_Click(object sender, EventArgs e)
        {
            AttributeButton_Method(1);
        }

        private bool AttributeButton_Method(int AttributeNo)
        {
            string returnValue = string.Empty;

            Control[] controls;

            string lblName = string.Concat("lblAttribute", AttributeNo.ToString());

            controls = this.Controls.Find(lblName, true);
            Label lbl = (Label)controls[0];

            string cboName = string.Concat("cboAttribute", AttributeNo.ToString());
            controls = this.Controls.Find(cboName, true);
            ComboBox cbo = (ComboBox)controls[0];

            if (lbl.Text == FindValues_Attributes.FindCSVs_Caption)
            {
                frmInputDialog frm = new frmInputDialog(FindValues_Attributes.FindCSVs_Caption, FindValues_Attributes.FindCSVs_Instructions);
  
                if (cbo.Items.Count > 0)
                {
                    string inputValue = cbo.Items[0].ToString();
                    frm.LoadData(inputValue);
                }

                if (frm.ShowDialog() == DialogResult.OK)
                {
                    returnValue = frm.Results;
                }
            }

            if (returnValue.Trim().Length == 0)
                return false;



            cbo.Items.Clear();
            cbo.Items.Add(returnValue);
            cbo.SelectedIndex = 0;

            return true;

        }

        private void cboAttribute1_MouseLeave(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;
        }

        private void cboAttribute2_MouseLeave(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

        }

        private void cboAttribute3_MouseLeave(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

        }

        private void cboAttribute4_MouseLeave(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

        }

        private void cboAttribute5_MouseLeave(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

        }

        private void cboAttribute6_MouseLeave(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

        }

        private void cboAttribute7_MouseLeave(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

        }

        private void cboAttribute8_MouseLeave(object sender, EventArgs e)
        {
            lblMessage.Text = string.Empty;

        }

        private void cboAttribute2_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdjustAttributesPerSelection(2);
        }

        private void ucParse_Attributes_Paint(object sender, PaintEventArgs e)
        {
            _HasLoaded = true;
        }

        private void cboAttribute4_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (CheckPages(_PagesRequired))
            {
                lblAttribute4.ForeColor = Color.Black;
            }
        }

        private void cboAttribute3_SelectedIndexChanged(object sender, EventArgs e)
        {
            AdjustAttributesPerSelection(3);
        }

 
    }


}
