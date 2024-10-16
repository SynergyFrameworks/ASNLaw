using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Atebion_Dictionary;
using Atebion.Common;
using OfficeOpenXml;

namespace ProfessionalDocAnalyzer
{
    public partial class frmImportDic : MetroFramework.Forms.MetroForm
    {
        public frmImportDic(string DictionaryRootPath, string KeywordGroupPath)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            _DictionaryRootPath = DictionaryRootPath;
            _KeywordGroupPath = KeywordGroupPath;
        }

        private Atebion_Dictionary.Dictionary _DictionaryMgr;
        private string _DictionaryRootPath;
        private string _KeywordGroupPath;
        private string _DicName = string.Empty;

        private DataSet _ds;

        private Modes _currentMode;
        private enum Modes
        {
            Excel = 0,
            Dictionary = 1,
            Workgroup = 2,
            KeywordGroup = 3
        }

        public string DictionaryName
        {
            get { return _DicName; }
        }

        public void LoadData()
        {
            if (_DictionaryRootPath == string.Empty)
            {
                MessageBox.Show("Error: The Dictionary Path has not been defined.", "Dictionary Path Unknown", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }

            if (!Directory.Exists(_DictionaryRootPath))
            {
                MessageBox.Show("Error: The Dictionary Path does not exists.", "Dictionary Path Not Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                this.Close();
            }


            _DictionaryMgr = new Atebion_Dictionary.Dictionary(_DictionaryRootPath);
            _ds = _DictionaryMgr.GetDataset_Empty();



            lblExcelColumns.Text = string.Concat("Excel Columns Assignments", Environment.NewLine, Environment.NewLine,
                                                 "Column A  Category (If empty, then the Default category will be used)", Environment.NewLine,
                                                 "Column B  Term  (Required)", Environment.NewLine,
                                                 "Column C  Definition (Optional)", Environment.NewLine,
                                                 "Column D  Weight (If empty, then will set to the default value of zero)", Environment.NewLine,
                                                 "Column E  Synonyms  (Optional, Comma Separated Values)", Environment.NewLine,
                                                 "Column F  Highlight Color (If empty, then will set to the default color to YellowGreen)");


            lblDicFileMessage.Text = string.Concat("Select an Concept Dictionary file (*.dicx).", Environment.NewLine, Environment.NewLine,
                                                    "If you want to import a Dictionary from another company’s solution/tool,",
                                                    "suggest exporting their dictionary to a CSV or Excel file. If the exported Dictionary file is a CSV file, then open it in Excel",
                                                    " and save as an Excel XLSX file. Then import the Excel file.", Environment.NewLine, Environment.NewLine,
                                                    "AcroSeeker’s dictionaries are not supported in the Concept Analyzer.");


            lblbKwG.Text = string.Concat("Select a Document Analyzer Keyword Group.", Environment.NewLine, Environment.NewLine,
                                        "Imports Keywords into a new Dictionary.", Environment.NewLine,
                                        "All keywords will be set to the 'Default' category and Weight = 0");

            LoadKeywordGroups();

            _currentMode = Modes.Excel; // Set import Excel as the defualt
            ModeAdjustments();

        }

        private void LoadKeywordGroups()
        {
            lstKeywordGroups.Items.Clear();

            string[] files = Directory.GetFiles(_KeywordGroupPath);

            int i = 0;
            string fileNameNoExt = string.Empty;

            foreach (string fileName in files)
            {
                fileNameNoExt = Files.GetFileNameWOExt(fileName);
                if (fileNameNoExt != string.Empty)
                {
                    lstKeywordGroups.Items.Add(fileNameNoExt);
                    i++;
                }
            }

        }

        private void ModeAdjustments()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            panExcel.Visible = false;
            panExcel.Dock = DockStyle.None;
            butExcel.Highlight = false;
            butExcel.Refresh();

            panDictionary.Visible = false;
            panDictionary.Dock = DockStyle.None;
            butDictionary.Highlight = false;
            butDictionary.Refresh();

            panKeywordGroup.Visible = false;
            panKeywordGroup.Dock = DockStyle.None;
            butKeywordGroup.Highlight = false;
            butKeywordGroup.Refresh();

            switch (_currentMode)
            {

                case Modes.Excel:
                    this.panExcel.Visible = true;
                    this.panExcel.Dock = DockStyle.Fill;
                    butExcel.Highlight = true;
                    butExcel.Refresh();

                    this.openFileDialog1.Filter = "Excel files (*.xlsx)|*.xlsx";

                    break;

                case Modes.Dictionary:
                    this.panDictionary.Visible = true;
                    this.panDictionary.Dock = DockStyle.Fill;
                    butDictionary.Highlight = true;
                    butDictionary.Refresh();

                    this.openFileDialog1.Filter = "Dictionary files (*.dicx)|*.dicx";

                    break;

                case Modes.KeywordGroup:
                    this.panKeywordGroup.Visible = true;
                    this.panKeywordGroup.Dock = DockStyle.Fill;
                    butKeywordGroup.Highlight = true;
                    butKeywordGroup.Refresh();

                    break;

            }

            this.Refresh();

            Cursor.Current = Cursors.Default;
        }




        private void lblExcelColumns_TextChanged(object sender, EventArgs e)
        {
            txtbExcelColumns.Text = lblExcelColumns.Text;
        }

        private void txtbExcelColumns_TextChanged(object sender, EventArgs e)
        {
            txtbExcelColumns.Text = lblExcelColumns.Text;
        }

        private void butSelectExcelFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;


            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor; // Waiting 

                openFileDialog1.Filter = "Excel Workbook (*.xlsx)|*.xlsx";

                this.txtExcelFile.Text = openFileDialog1.FileName;
                PopulateSheetNames(txtExcelFile.Text.Trim());
                // Check file type
                string _ext = string.Empty;
                string fileName = Files.GetFileName(txtExcelFile.Text, out _ext);
                string fileNameWOExt = Files.GetFileNameWOExt(txtExcelFile.Text);

                if (txtbExcelDictionaryName.Text.Length == 0)
                {
                    txtbExcelDictionaryName.Text = fileNameWOExt;
                }


            }

        }


        private bool ValidateInput()
        {
            switch (_currentMode)
            {

                case Modes.Excel:
                    if (txtExcelFile.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Please select an Excel file.", "No Excel File has been Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (!File.Exists(txtExcelFile.Text.Trim()))
                    {
                        MessageBox.Show("Could not find the Excel file.", "Excel File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (cboSheetName.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Please enter an Excel Sheet Name.", "Sheet Name Not Defined", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;

                    }

                    if (txtbExcelDictionaryName.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Please enter a new Dictionary Name.", "New Dictionary Name Not Defined", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;

                    }

                    break;

                case Modes.Dictionary:

                    if (txtDictionaryFile.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Please select an Atebion Dictionary file.", "No Dictionary File has been Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (!File.Exists(txtDictionaryFile.Text.Trim()))
                    {
                        MessageBox.Show("Could not find the Atebion Dictionary file.", "Dictionary File Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (txtDicFileDicName.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Please enter a new Dictionary Name.", "New Dictionary Name Not Defined", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;

                    }

                    break;

                case Modes.KeywordGroup:
                    if (lstKeywordGroups.Items.Count == 0)
                    {
                        MessageBox.Show("No Keyword Groups have been defined.", "No Keyword Groups Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (lstKeywordGroups.SelectedIndex == -1)
                    {
                        MessageBox.Show("Please select a new Keyword Group.", "Keyword Group Not Selected", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    if (txtbDicNameKeywords.Text.Trim() == string.Empty)
                    {
                        MessageBox.Show("Please enter a new Dictionary Name.", "New Dictionary Name Not Defined", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        return false;
                    }

                    break;

            }

            return true;
        }

        private int PopulateSheetNames(string TemplatePathFile)
        {

            int count = 0;
            if (TemplatePathFile == string.Empty)
                return 0;

            FileInfo fileInfo = new FileInfo(TemplatePathFile);
            var excel = new ExcelPackage(fileInfo);
            cboSheetName.Items.Clear();
            foreach (var worksheet in excel.Workbook.Worksheets)
            {
                cboSheetName.Items.Add(worksheet.Name);
                count++;
            }
            if (count != -1)
                       cboSheetName.SelectedIndex = 0;


                //if (_Mode == Modes.New)
                //{
                //    if (count == 0)
                //        cboSheetName.SelectedIndex = 0;
                //}
                //else
                //{
                //    int index = cboSheetName.FindStringExact(_SheetName);
                //    if (index != -1)
                //        cboSheetName.SelectedIndex = index;

                //}

                return count;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (!ValidateInput())
                return;

            Cursor.Current = Cursors.WaitCursor; // Waiting 

            //txtExcelFile.Text.Trim()

            

            switch (_currentMode)
            {

                case Modes.Excel:
                    lblStatus.Text = "Please wait while the Concept Analyzer imports your selected Excel file.";
                    lblStatus.Refresh();


                    _DicName = txtbExcelDictionaryName.Text.Trim();
                    int qty = _DictionaryMgr.ImportExcelFile(txtExcelFile.Text.Trim(), cboSheetName.Text.Trim(), _DicName);

                    if (qty == -1)
                    {
                        if (_DictionaryMgr.ErrorMessage.Length > 0)
                        {
                            MessageBox.Show(_DictionaryMgr.ErrorMessage, "Unable to Import Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            lblStatus.Text = string.Concat("Error: ", _DictionaryMgr.ErrorMessage);
                            Cursor.Current = Cursors.Default;
                            return;
                        }
                        else
                        {
                            MessageBox.Show("No Data was found", "Unable to Import Excel", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            lblStatus.Text = "No Data was found";
                            Cursor.Current = Cursors.Default;
                            return;
                        }
                    }
                    else if (qty == -5)
                    {
                        MessageBox.Show(_DictionaryMgr.ErrorMessage, "Enter another Dictionary Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        lblStatus.Text = string.Concat("Notice: ", _DictionaryMgr.ErrorMessage);
                        Cursor.Current = Cursors.Default;
                        return;
                    }



                    //_ds = _DictionaryMgr.Dictionary_DataSet;
                    //if (!_DictionaryMgr.SaveDicDataset(_ds, _DicName))
                    //{
                    //    MessageBox.Show(_DictionaryMgr.ErrorMessage, "Unable to Create New Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    //    Cursor.Current = Cursors.Default;
                    //    return;
                    //}                

                    break;

                case Modes.Dictionary:

                    //_DictionaryRootPath

                    string newDictionaryFile = string.Concat(txtDicFileDicName.Text, ".dicx");
                    string newDictionaryPathFile = Path.Combine(_DictionaryRootPath, newDictionaryFile);

                    if (File.Exists(newDictionaryPathFile))
                    {
                        MessageBox.Show("Please enter another Dictionary name.", "Dictionary Name Already Used", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Cursor.Current = Cursors.Default;
                        return;
                    }

                    File.Copy(txtDictionaryFile.Text.Trim(), newDictionaryPathFile);


                    break;

                case Modes.KeywordGroup:

                    if (_DictionaryMgr.CheckDictionaryExists(txtbDicNameKeywords.Text.Trim()))
                    {
                        MessageBox.Show("Please enter another Dictionary name.", "Dictionary Name Already Used", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                        Cursor.Current = Cursors.Default;
                        return;
                    }

                    if (lstKeywordGroups.Text == string.Empty)
                        return;

                    string keywordFile = string.Concat(lstKeywordGroups.Text, ".xml");
                    string keywordPathFile = Path.Combine(_KeywordGroupPath, keywordFile);

                    if (_DictionaryMgr.ImportKeywordGroup(keywordPathFile, txtbDicNameKeywords.Text.Trim()) == -1)
                    {
                        MessageBox.Show(_DictionaryMgr.ErrorMessage, "Unable to Create New Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Cursor.Current = Cursors.Default;
                        return;
                    }

                    break;


            }

            lblStatus.Text = string.Empty;
            lblStatus.Refresh();
            lblStatus.Text = "Import Completed";
            lblStatus.Refresh();

            System.Threading.Thread.Sleep(3000);

            Cursor.Current = Cursors.Default;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }

        private void lblDicFileMessage_TextChanged(object sender, EventArgs e)
        {
            txtbDicFileMessage.Text = lblDicFileMessage.Text;
        }

        private void txtbDicFileMessage_TextChanged(object sender, EventArgs e)
        {
            txtbDicFileMessage.Text = lblDicFileMessage.Text;
        }

        private void butSelectDicFile_Click(object sender, EventArgs e)
        {
            openFileDialog1.FileName = string.Empty;


            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor; // Waiting 

                openFileDialog1.Filter = "Atebion Dictionary (*.dicx)|*.dicx";

                txtDictionaryFile.Text = openFileDialog1.FileName;

                // Check file type
                string _ext = string.Empty;
                string fileName = Files.GetFileName(txtDictionaryFile.Text, out _ext);
                string fileNameWOExt = Files.GetFileNameWOExt(txtDictionaryFile.Text);

                if (txtDicFileDicName.Text.Length == 0)
                {
                    txtDicFileDicName.Text = fileNameWOExt;
                }
            }
        }

        private void lblbKwG_TextChanged(object sender, EventArgs e)
        {
            txtbKwG.Text = lblbKwG.Text;
        }

        private void txtbKwG_TextChanged(object sender, EventArgs e)
        {
            txtbKwG.Text = lblbKwG.Text;
        }

        private void lstKeywordGroups_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbDicNameKeywords.Text = lstKeywordGroups.Text;
        }

        private void butExcel_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.Excel;
            ModeAdjustments();
        }

        private void lblExcel_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.Excel;
            ModeAdjustments();
        }

        private void butDictionary_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.Dictionary;
            ModeAdjustments();
        }

        private void lblDictionary_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.Dictionary;
            ModeAdjustments();
        }

        private void butKeywordGroup_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.KeywordGroup;
            ModeAdjustments();
        }

        private void lblKeywordGroup_Click(object sender, EventArgs e)
        {
            _currentMode = Modes.KeywordGroup;
            ModeAdjustments();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmImportDic_Load(object sender, EventArgs e)
        {
            
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }
    }
}
