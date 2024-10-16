using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
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
    public partial class frmDictionary2 : MetroFramework.Forms.MetroForm
    {
        public frmDictionary2(string DictionaryRootPath)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            this.CancelButton = this.butCancel;
            //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            _DictionaryRootPath = DictionaryRootPath;

            _isNew = true;

            LoadData();
        }

        public frmDictionary2(string DictionaryRootPath, string DicName)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            this.CancelButton = this.butCancel;
            //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            _DictionaryRootPath = DictionaryRootPath;

            _DicName = DicName;
            _OrgDicName = DicName;

            LoadData();
        }

        private Atebion_Dictionary.Dictionary _DictionaryMgr;
        private string _DictionaryRootPath;

        private int _CurrentrowIndex = 0;

        private string _ErrorMessage = string.Empty;

        private string _DicName = string.Empty;
        private string _OrgDicName = string.Empty;
        private bool _isNew = false;
        private DataSet _ds;
        private int _dicUID = -1;

        private bool _isLoaded = false;



        private void LoadData()
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

            if (_isNew)
            {
                _DictionaryMgr = new Atebion_Dictionary.Dictionary(_DictionaryRootPath);
                _ds = _DictionaryMgr.GetDataset_Empty();
            }
            else
            {
                if (_DicName == string.Empty)
                {
                    MessageBox.Show("Error: The Dictionary Name has not been defined.", "Dictionary Name Unknown", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }

                _DictionaryMgr = new Atebion_Dictionary.Dictionary(_DictionaryRootPath, _DicName);
                _ds = _DictionaryMgr.GetDataset();
            }

            if (_ds == null)
            {
                string errMsg = _DictionaryMgr.ErrorMessage;
                if (errMsg != string.Empty)
                {
                    errMsg = string.Concat("Error: ", errMsg);
                    MessageBox.Show(errMsg, "Unable to get Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Unable to get the selected Dictionary data.", "Unable to get Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    this.Close();
                }
            }

            Populate();

        }

        private void Populate()
        {
            LoadCategory();
            LoadWeights();

            if (!_isNew)
            {
                LoadDictionary();

            }

            // Load colors into drop-down list
            this.cmbboxClr.Items.Clear();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                if (c.Name != "Transparent")
                    this.cmbboxClr.Items.Add(c.Name);
            }
        }

        private int LoadDictionary()
        {
            int count = 0;

            if (!_isLoaded)
                txtbDictionaryName.Text = _DicName;
            //  txtbDictionaryName.ReadOnly = true;

            lstbSynonyms.DataSource = null;
            dvgDictionaries.DataSource = null;

            if (_ds.Tables[DictionaryFieldConst.TableName].Rows.Count == 0)
            {
                return count;
            }

            DataView dv = _DictionaryMgr.GetDataView_Transformation();

            dvgDictionaries.DataSource = dv;

            count = _ds.Tables[DictionaryFieldConst.TableName].Rows.Count;

            dvgDictionaries.Columns[0].Visible = false;
            dvgDictionaries.Columns[1].Visible = false;

            //  AutoSizeCols();

            if (!_isLoaded)
                txtbDescription.Text = _DictionaryMgr.GetDictionaryDescription();

            setDictionariesBackColor();

            _isLoaded = true;

            return count;

        }

        private void AutoSizeCols()
        {
            dvgDictionaries.AutoResizeColumns();
            dvgDictionaries.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            //set autosize mode
            //dvgDictionaries.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dvgDictionaries.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dvgDictionaries.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dvgDictionaries.Columns[5].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            //dvgDictionaries.Columns[6].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ////datagrid has calculated it's widths so we can store them
            //for (int i = 2; i <= 6; i++)
            //{
            //    //store autosized widths
            //    int colw = dvgDictionaries.Columns[i].Width;
            //    //remove autosizing
            //    dvgDictionaries.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //    //set width to calculated by autosize
            //    dvgDictionaries.Columns[i].Width = colw;
            //}

        }

        private void setDictionariesBackColor()
        {
            string colorHighlight = string.Empty;

            if (dvgDictionaries.Rows.Count == 0)
                return;

            try
            {
                foreach (DataGridViewRow row in dvgDictionaries.Rows)
                {
                    if (row.Cells[DictionaryFieldConst.HighlightColor].Value != null)
                    {
                        colorHighlight = row.Cells[DictionaryFieldConst.HighlightColor].Value.ToString();
                        row.Cells[DictionaryFieldConst.HighlightColor].Style.BackColor = Color.FromName(colorHighlight);

                    }

                }

                dvgDictionaries.Columns[DictionaryFieldConst.Weight].Width = 35;
                dvgDictionaries.Columns[DictionaryFieldConst.HighlightColor].Width = 100;

                dvgDictionaries.Refresh();
            }
            catch { }
        }

        private int LoadSynonyms()
        {
            string[] Synonyms = _DictionaryMgr.GetSynonymsPerDicItem(_dicUID, _ds);
            this.lstbSynonyms.DataSource = Synonyms;

            if (Synonyms != null)
                return Synonyms.Length;
            else
                return 0;
        }

        private int LoadCategory()
        {
            cboCategory.Items.Clear();

            int count = 0;

            foreach (DataRow row in _ds.Tables[CategoryFieldConst.TableName].Rows)
            {
                cboCategory.Items.Add(row[CategoryFieldConst.Name].ToString());
                count++;
            }

            return count;
        }

        private void LoadWeights()
        {
            string[] weights = _DictionaryMgr.GetWeightsRange();
            cboDicWeight.DataSource = weights;
        }




        private void LoadCategories()
        {
            //cboCategory.Items.Clear();

            //if (_isNew)
            //{
            //    cboCategory.Items.Add("Default");
            //}

            string[] cats = _DictionaryMgr.GetCategories();

            cboCategory.DataSource = cats;
        }

        private void butSynonyms_Click(object sender, EventArgs e)
        {

        }

        private void SelectionChanged()
        {
            if (dvgDictionaries.Rows.Count == 0)
                return;

            if (dvgDictionaries.SelectedRows.Count == 0)
            {
                dvgDictionaries.Rows[0].Selected = true;
            }

            DataGridViewRow row = dvgDictionaries.SelectedRows[0];
            if (row == null)
            {

                dvgDictionaries.Rows[_CurrentrowIndex].Selected = true;
                row = dvgDictionaries.SelectedRows[0];
            }

            _CurrentrowIndex = dvgDictionaries.SelectedRows[0].Index;

            _dicUID = Convert.ToInt32(row.Cells[DictionaryFieldConst.UID].Value.ToString());

            if (row.Cells[DictionaryFieldConst.HighlightColor].Value != null)
            {
                string colorHighlight = row.Cells[DictionaryFieldConst.HighlightColor].Value.ToString();

                int index = cmbboxClr.FindString(colorHighlight);
                if (index != -1)
                    cmbboxClr.SelectedIndex = index;
            }


            ShowCurrentDicDetails();

            LoadSynonyms();
        }

        private void dvgDictionaries_SelectionChanged(object sender, EventArgs e)
        {

            SelectionChanged();

            //if (dvgDictionaries.Rows.Count == 0)
            //    return;

            //if (dvgDictionaries.SelectedRows.Count == 0)
            //{
            //    dvgDictionaries.Rows[0].Selected = true;
            //}

            //DataGridViewRow row = dvgDictionaries.SelectedRows[0];
            //if (row == null)
            //{

            //    dvgDictionaries.Rows[_CurrentrowIndex].Selected = true;
            //    row = dvgDictionaries.SelectedRows[0];           
            //}

            //_CurrentrowIndex = dvgDictionaries.SelectedRows[0].Index;

            //_dicUID = Convert.ToInt32(row.Cells[DictionaryFieldConst.UID].Value.ToString());

            //if (row.Cells[DictionaryFieldConst.HighlightColor].Value != null)
            //{
            //    string colorHighlight = row.Cells[DictionaryFieldConst.HighlightColor].Value.ToString();

            //    int index = cmbboxClr.FindString(colorHighlight);
            //    if (index != -1)
            //        cmbboxClr.SelectedIndex = index;
            //}


            //ShowCurrentDicDetails();

            //LoadSynonyms();

        }

        private void ShowCurrentDicDetails()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            // Clear values
            lstbSynonyms.DataSource = null;

            //DataGridViewRow row = dvgDictionaries.CurrentRow;
            DataGridViewRow row = dvgDictionaries.Rows[_CurrentrowIndex];

            txtDicItem.Text = string.Empty;
            txtDefinition.Text = string.Empty;

            if (row == null) // Check, sometimes data has not been loaded yet
            {
                Cursor.Current = Cursors.Default; // Back to normal
                return;
            }

            string dicName = row.Cells[DictionaryFieldConst.Item].Value.ToString();
            string dicCategory = row.Cells[DictionaryFieldConst.Category].Value.ToString();
            string dicDefinition = row.Cells[DictionaryFieldConst.Definition].Value.ToString();
            string dicWeight = row.Cells[DictionaryFieldConst.Weight].Value.ToString();

            string dicUID = row.Cells[DictionaryFieldConst.UID].Value.ToString();

            txtDicItem.Text = dicName;
            txtDefinition.Text = dicDefinition;

            cboCategory.SelectedIndex = cboCategory.FindString(dicCategory);
            cboDicWeight.SelectedIndex = cboDicWeight.FindString(dicWeight);

            string[] synonyms = _DictionaryMgr.GetSynonymsPerDicItem(Convert.ToInt32(dicUID));

            lstbSynonyms.DataSource = synonyms;

            Cursor.Current = Cursors.Default;
        }

        private void butNew_Click(object sender, EventArgs e)
        {
            _isLoaded = true;

            string dicItem = txtDicItem.Text.Trim();
            string def = txtDefinition.Text.Trim();
            double weight = Convert.ToDouble(cboDicWeight.Text);

            string msg = string.Empty;

            if (dicItem.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Dictionary Item before attempting to insert into your Dictionary.", "Dictionary Item Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDicItem.Focus();
                return;
            }

            if (DicExists(dicItem))
            {
                msg = string.Concat("The Dictionary Item ", dicItem, " already exists in your dictionary.");
                MessageBox.Show(msg, "Dictionary Item Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            string category = cboCategory.Text;

            int catUID = _DictionaryMgr.GetCategoryUID(category);
            if (catUID == -1)
            {
                string msgTitle = string.Concat("Unable to find Category: ", category);
                MessageBox.Show("Reloading the Category list. Try again!", msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LoadCategories();
                return;
            }

            int UID = DataFunctions.FindMaxValue(_ds.Tables[DictionaryFieldConst.TableName], DictionaryFieldConst.UID);

            UID = UID + 1;


            DataRow row = _ds.Tables[DictionaryFieldConst.TableName].NewRow();

            row[DictionaryFieldConst.UID] = UID.ToString();
            row[DictionaryFieldConst.Item] = dicItem;
            row[DictionaryFieldConst.Definition] = def;
            row[DictionaryFieldConst.Weight] = weight;
            row[DictionaryFieldConst.Category_UID] = catUID;
            row[DictionaryFieldConst.HighlightColor] = cmbboxClr.Text;

            _ds.Tables[0].Rows.Add(row);

            _DictionaryMgr.Dictionary_DataSet = _ds;

            LoadDictionary();



            foreach (DataGridViewRow dvgRow in dvgDictionaries.Rows)
            {
                // 0 is the column index
                if (dvgRow.Cells[DictionaryFieldConst.Item].Value.ToString().Equals(dicItem))
                {
                    dvgRow.Selected = true;
                    break;
                }
            }

            dvgDictionaries.FirstDisplayedScrollingRowIndex = dvgDictionaries.SelectedRows[0].Index;


        }

        private bool DicExists(string dicItem)
        {
            string selectStatment = string.Concat(DictionaryFieldConst.Item, " = '", dicItem, "'");

            DataRow[] foundDic = _ds.Tables[DictionaryFieldConst.TableName].Select(selectStatment);
            if (foundDic.Length != 0)
            {
                return true;
            }

            return false;
        }

        private void butReplace_Click(object sender, EventArgs e)
        {
            _isLoaded = true;

            if (dvgDictionaries.Rows.Count == 0)
                return;

            if (dvgDictionaries.CurrentCell == null)
            {
                dvgDictionaries.Rows[_CurrentrowIndex].Selected = true;
            }

            //  int rowID = dvgDictionaries.CurrentCell.RowIndex;

            string dicItem = txtDicItem.Text.Trim();
            string def = txtDefinition.Text.Trim();
            double weight = Convert.ToDouble(cboDicWeight.Text);

            string msg = string.Empty;

            if (dicItem.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter Dictionary Item before attempting to insert into your Dictionary.", "Dictionary Item Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDicItem.Focus();
                return;
            }

            //if (DicExists(dicItem))
            //{
            //    msg = string.Concat("The Dictionary Item ", dicItem, " already exists in your dictionary.");
            //    MessageBox.Show(msg, "Dictionary Item Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    return;
            //}

            string category = cboCategory.Text;

            int catUID = _DictionaryMgr.GetCategoryUID(category);
            if (catUID == -1)
            {
                string msgTitle = string.Concat("Unable to find Category: ", category);
                MessageBox.Show("Reloading Category list. This Category may have been removed by another user.", msgTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LoadCategories();
                return;
            }

            string highlightColor = cmbboxClr.Text;

            int UID = Convert.ToInt32(dvgDictionaries.SelectedRows[0].Cells[DictionaryFieldConst.UID].Value.ToString());


            foreach (DataRow row in _ds.Tables[DictionaryFieldConst.TableName].Rows)
            {
                if (Convert.ToInt32(row[DictionaryFieldConst.UID].ToString()) == UID)
                {
                    row[DictionaryFieldConst.Item] = dicItem;
                    row[DictionaryFieldConst.Definition] = def;
                    row[DictionaryFieldConst.Weight] = weight;
                    row[DictionaryFieldConst.Category_UID] = catUID;
                    row[DictionaryFieldConst.HighlightColor] = highlightColor;

                    break;
                }
            }

            _ds.AcceptChanges();

            _DictionaryMgr.Dictionary_DataSet = _ds;

            LoadDictionary();

            setDictionariesBackColor();

            foreach (DataGridViewRow dvgRow in dvgDictionaries.Rows)
            {
                // 0 is the column index
                if (dvgRow.Cells[DictionaryFieldConst.Item].Value.ToString().Equals(dicItem))
                {
                    dvgRow.Selected = true;
                    break;
                }
            }

            dvgDictionaries.FirstDisplayedScrollingRowIndex = dvgDictionaries.SelectedRows[0].Index;

        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            _isLoaded = true;

            if (dvgDictionaries.CurrentCell == null)
                return;

            string dicItem = dvgDictionaries.SelectedRows[0].Cells[DictionaryFieldConst.Item].Value.ToString();

            string msg = string.Concat("Are you sure you want to Delete item '", dicItem, "'?");
            if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                return;

            int rowID = dvgDictionaries.CurrentCell.RowIndex;

            int UID = Convert.ToInt32(dvgDictionaries.SelectedRows[0].Cells[DictionaryFieldConst.UID].Value.ToString());

            foreach (DataRow row in _ds.Tables[DictionaryFieldConst.TableName].Rows)
            {
                if (Convert.ToInt32(row[DictionaryFieldConst.UID].ToString()) == UID)
                {
                    _ds.Tables[DictionaryFieldConst.TableName].Rows.Remove(row);

                    break;
                }
            }

            _ds.AcceptChanges();

            _DictionaryMgr.Dictionary_DataSet = _ds;

            LoadDictionary();

            try
            {
                dvgDictionaries.FirstDisplayedScrollingRowIndex = rowID;

            }
            catch
            {
                return;
            }

        }

        private void butAddSynonym_Click(object sender, EventArgs e)
        {
            frmSynonymNew frm = new frmSynonymNew();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                string newSynonym = frm.Synonym;

                if (_DictionaryMgr.SynonymExists(_dicUID, newSynonym))
                {
                    string mstTitle = string.Concat("Synonym '", newSynonym, "' Already Exists");
                    MessageBox.Show("The Synonym you entered is already assigned to the current Dictionary Item.", mstTitle, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                if (!_DictionaryMgr.SetNewSynonym(_dicUID, newSynonym))
                {
                    MessageBox.Show(_DictionaryMgr.ErrorMessage, "New Synonym was Not Created", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    return;
                }

                LoadSynonyms();
            }

        }

        private void butRemoveSynonym_Click(object sender, EventArgs e)
        {
            string synonym = lstbSynonyms.Text;

            if (!_DictionaryMgr.RemoveSynonym(_dicUID, synonym))
            {
                MessageBox.Show(_DictionaryMgr.ErrorMessage, "Synonym was not removed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            // _ds = _DictionaryMgr.Dictionary_DataSet; // Reset Dataset since the new Synonym was added via the DictionaryMgr

            LoadSynonyms();
        }

        private bool Validation()
        {
            if (txtbDictionaryName.Text.Trim().Length == 0)
            {
                _ErrorMessage = "Please enter a Dictionary Name.";
                return false;
            }

            if (_isNew)
            {
                if (_DictionaryMgr.CheckDictionaryExists(txtbDictionaryName.Text.Trim()))
                {
                    _ErrorMessage = "Dictionary Name alreay exists. Please enter another Dictionary Name.";
                    return false;
                }
            }
            else
            {
                if (_OrgDicName != txtbDictionaryName.Text.Trim())
                {
                    if (_DictionaryMgr.CheckDictionaryExists(txtbDictionaryName.Text.Trim()))
                    {
                        _ErrorMessage = "Dictionary Name alreay exists. Please enter another Dictionary Name.";
                        return false;
                    }
                }
            }




            return true;
        }

        private void metroButton1_Click(object sender, EventArgs e)
        {

        }

        private void butOK_Click(object sender, EventArgs e)
        {
            _ErrorMessage = string.Empty;

            if (!Validation())
            {
                MessageBox.Show(_ErrorMessage, "Unable to Save Changes", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }


            if (!_DictionaryMgr.SaveDicDataset(_ds, txtbDescription.Text.Trim()))
            {
                MessageBox.Show(_ErrorMessage, "Unable to Save", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmDictionary2_Shown(object sender, EventArgs e)
        {
            setDictionariesBackColor();
        }

        private void cmbboxClr_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }


        private void FindItem()
        {
            String searchValue = this.txtbFind.Text.Trim().ToUpper();

            if (searchValue == string.Empty)
                return;

            string selX = "Item"; // cboFind.SelectedItem.ToString();


            int rowIndex = -1;
            foreach (DataGridViewRow row in dvgDictionaries.Rows)
            {
                //if (row.Cells[selX].Value.ToString().Equals(searchValue))
                if (row.Cells[selX].Value.ToString().ToUpper() == searchValue)
                {
                    rowIndex = row.Index;
                    dvgDictionaries.Rows[rowIndex].Selected = true;
                    dvgDictionaries.FirstDisplayedScrollingRowIndex = dvgDictionaries.SelectedRows[0].Index;

                    SelectionChanged();
                    return;
                }
            }
        }

        private void txtbFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindItem();
            }
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            FindItem();
        }

        private void butCategories_Click(object sender, EventArgs e)
        {
            frmCategory frm = new frmCategory(_ds);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                _ds = frm.DictionaryDataset;
                LoadCategories();
                LoadDictionary();
            }
        }

        private void cmbboxClr_DrawItem_1(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        public void DataSetToExcel()
        {
            // frmDictionary2 fg = new frmDictionary2();

            //LoadDictionary();
            using (ExcelPackage pck = new ExcelPackage())
            {
                //DataView dv = 
                DataTable dt = _DictionaryMgr.GetDataTable_TransformationForExport();
                
                ExcelWorksheet workSheet = pck.Workbook.Worksheets.Add(dt.TableName);
                workSheet.Cells["A1"].LoadFromDataTable(dt, true);

                //create a SaveFileDialog instance with some properties
                SaveFileDialog saveFileDialog1 = new SaveFileDialog();
                saveFileDialog1.Title = "Save Excel sheet";
                saveFileDialog1.Filter = "Excel files|*.xlsx|All files|*.*";
                saveFileDialog1.FileName = _DicName + "_" + DateTime.Now.ToString("dd-MM-yyyy") + ".xlsx";

                //check if user clicked the save button
                if (saveFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    //Get the FileInfo
                    FileInfo fi = new FileInfo(saveFileDialog1.FileName);
                    var ws = pck.Workbook.Worksheets.FirstOrDefault();
                    // Should Delete rows 1-1 and shift up the rows after deletion
                    ws.DeleteRow(1, 1, true);
                    //write the file to the disk
                    pck.SaveAs(fi);
                }




                //pck.SaveAs(new FileInfo(filePath));
            }
        }





    }
}
