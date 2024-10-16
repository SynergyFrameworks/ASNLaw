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
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class frmKeywords : MetroFramework.Forms.MetroForm
    {
        public frmKeywords(string path, string file)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            this.AcceptButton = this.butOK;
            this.CancelButton = this.butCancel;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            _path = path;
            _file = file;

            
        }


        public frmKeywords(string path, bool CreateDefault)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            this.AcceptButton = this.butOK;
            this.CancelButton = this.butCancel;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

           _path = path;

            //if (CreateDefault)
            //    _KeywordsMgr = new KeywordsMgr(_path, true);
            //else
            //    _KeywordsMgr = new KeywordsMgr(_path, false);

            //_file = _KeywordsMgr.DataFile;

            _isNew = true;
        }

        #region Private Var.s

        private string _path = string.Empty;
        private string _file = string.Empty;
        private string _fileName = string.Empty;

        private bool _isNew = false;

        private KeywordsMgr2 _KeywordsMgr = new KeywordsMgr2();
        private DataSet _ds;

        private subPanels _subPanel = subPanels.BatchLoad;
        private enum subPanels
        {
            BatchLoad = 0,
            Maintain = 1,
        }

        private string _defualtMessage = string.Concat("The Professional Document Analyzer identifies Keywords as follows:",
                Environment.NewLine,
                "* Case-insensitive method, meaning shall = Shall",
                Environment.NewLine,
                "* Phrases are supported, e.g. 'shall be'");

        private string _maintainMessage = string.Concat("New: Enter New Keyword and click the Add button.", Environment.NewLine,
                                               "Replace: Select a Keyword and either enter a new Keyword and/or Change the Color.", Environment.NewLine,
                                               "Delete: To Remove, select a Keyword and click the Delete button.");

        #endregion

        #region Private Functions

        private void ModeAdjust()
        {
            panBatchLoad.Visible = false;
            panMaintain.Visible = false;
            butMaintain.BackColor = Color.WhiteSmoke;
            butBatchLoad.BackColor = Color.WhiteSmoke;

            if (_subPanel == subPanels.BatchLoad)
            {
                panBatchLoad.Visible = true;
                panBatchLoad.Dock = DockStyle.Fill;
                butBatchLoad.BackColor = Color.LightGreen;
                lblMessage.Text = _defualtMessage;
            }
            else
            {
                panMaintain.Visible = true;
                panMaintain.Dock = DockStyle.Fill;
                butMaintain.BackColor = Color.LightGreen;
                lblMessage.Text = _maintainMessage;
            }


        }

        private bool NewGroupExists()
        {
            if (_fileName == txtbFileName.Text)
                return false;

            string newPathFile = string.Concat(_path, @"\", txtbFileName.Text, ".xml");

            if (File.Exists(newPathFile))
                this.lblMessage.Text = "Keyword group already exists! – Saving these changes will overwrite an existing group.";

            return true;
        }

        private bool ValidateKeyword(string newKeyword, bool isReplace)
        {
            // Validation
            if (newKeyword == string.Empty)
            {
                MessageBox.Show("Please enter a Keyword.", "Unable to Enter a Blank Keyword", MessageBoxButtons.OK, MessageBoxIcon.Information);
                lblMessage.Text = "Please enter a Keyword. Blank Keywords are not supported.";
                return false;
            }
            if (!isReplace)
            {
                if (FindKeyword(newKeyword))
                 {
                    MessageBox.Show("Please enter another Keyword.", "Keyword must be Unique", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    lblMessage.Text = "Keyword must be unique. Please enter another Keyword.";
                    return false;
                }
            }

            return true;
        }

        private bool FindKeyword(string keyword)
        {
            bool returnValue = false;

            dvgKeywords.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            try
            {
                foreach (DataGridViewRow row in dvgKeywords.Rows)
                {
                    if (row.Cells[KeywordsFoundFields.Keyword].Value.ToString().Equals(keyword))
                    {
                        row.Selected = true;
                        returnValue = true;
                        break;
                    }
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show("Error: " + exc.Message);    
            }

            return returnValue;
        }

        #endregion



        private void butNew_Click(object sender, EventArgs e)
        {
            string newKeyword = this.txtbSection.Text.Trim();

            if (!ValidateKeyword(newKeyword, false))
                return;
           
            _ds = DataFunctions.CreateDataSetFromDataGridView(dvgKeywords);

            int UID = DataFunctions.FindMaxValue(_ds.Tables[0], KeywordsFoundFields.UID);

            UID++;

            DataRow row = _ds.Tables[0].NewRow();

            row[KeywordsFoundFields.UID] = UID;
            row[KeywordsFoundFields.Keyword] = newKeyword;
            row[KeywordsFoundFields.ColorHighlight] = this.cmbboxClr.SelectedItem.ToString();

            _ds.Tables[0].Rows.Add(row);

            dvgKeywords.DataSource = _ds.Tables[0];

            setKeywordBackColor();


        }

        private void frmKeywords_Load(object sender, EventArgs e)
        {
            lblMessage.Text = _defualtMessage;

            if (_isNew)
            {
                lblHeader.Text = "New Keyword Group";
            }
            else
            {
                lblHeader.Text = "Edit Keyword Group";
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

            // Load colors into drop-down list
            this.cmbboxClr2.Items.Clear();
            colorType = typeof(System.Drawing.Color);
            propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                if (c.Name != "Transparent")
                    this.cmbboxClr2.Items.Add(c.Name);
            }

            if (!_isNew)
            {
                _fileName = Files.GetFileNameWOExt(_file);

                _file = Path.Combine(AppFolders.KeywordGrpPath, _file);

                this.txtbFileName.Text = _fileName;
            }

            // Set Defualt Color
            int index = cmbboxClr.FindString("YellowGreen");
            if (index != -1)
                cmbboxClr.SelectedIndex = index;

            // Set Defualt Color
             index = cmbboxClr2.FindString("YellowGreen");
            if (index != -1)
                cmbboxClr2.SelectedIndex = index;


            if (!_isNew)
            {
                _ds = _KeywordsMgr.GetKeywordsLib(_file, "YellowGreen");
                if (_ds == null)
                {
                    string errMsg = "Error: " + _KeywordsMgr.ErrorMessage;

                    MessageBox.Show(errMsg, "Unable to Load Keywords", MessageBoxButtons.OK, MessageBoxIcon.Error);

                    _ds = _KeywordsMgr.GetEmptyKeywordsLib();

                    this.butReplace.Enabled = false;
                    this.butDelete.Enabled = false;
                    return;
                }
            }
            else
            {
                _ds = _KeywordsMgr.GetEmptyKeywordsLib();
            }

            this.dvgKeywords.DataSource = _ds.Tables[0];
            dvgKeywords.Sort(this.dvgKeywords.Columns[KeywordsFoundFields.Keyword], ListSortDirection.Ascending);
            setKeywordBackColor();
            dvgKeywords.Columns[0].Visible = false;
            if (dvgKeywords.Columns.Contains("Count")) // Added 10.14.2016
            {
                dvgKeywords.Columns["Count"].Visible = false;
            }
            if (dvgKeywords.Columns.Contains("KeywordLib")) 
            {
                dvgKeywords.Columns["KeywordLib"].Visible = false;
            }


            _subPanel = subPanels.BatchLoad;
            ModeAdjust();

 
        }

        private void butReplace_Click(object sender, EventArgs e)
        {
            string newKeyword = this.txtbSection.Text.Trim();

            if (!ValidateKeyword(newKeyword, true))
                return;

            _ds = DataFunctions.CreateDataSetFromDataGridView(dvgKeywords);

            //if (dvgKeywords.CurrentCell == null)
            //    return;

 
            //int rowID = dvgKeywords.CurrentCell.RowIndex;

            //if (rowID < 0)
            //    return;

            foreach (DataGridViewRow r in dvgKeywords.SelectedRows)
            {
                r.Cells[KeywordsFoundFields.Keyword].Value = newKeyword; ;
                r.Cells[KeywordsFoundFields.ColorHighlight].Value = this.cmbboxClr.SelectedItem.ToString();
            }

            //dvgKeywords.Rows[rowID].Cells[KeywordsFoundFields.Keyword].Value = newKeyword;
            //dvgKeywords.Rows[rowID].Cells[KeywordsFoundFields.ColorHighlight].Value = this.cmbboxClr.SelectedItem.ToString();

            setKeywordBackColor();

        }

        private void butDelete_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow item in this.dvgKeywords.SelectedRows)
            {
                dvgKeywords.Rows.RemoveAt(item.Index);
            }
        }

        private void butAddBatchKeywords_Click(object sender, EventArgs e)
        {
            AddBatchKeywords();
        }

        private int AddBatchKeywords()
        {
            int KeywordsAdded = 0;

             string[] Keywords = txtbBatchKeywords.Text.Trim().Split(',');

            if (Keywords.Length == 0)
            {
                string msg = "Please enter comma-separated keywords for batch load.";
                MessageBox.Show(msg, "No Keywords to Load", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return KeywordsAdded;
            }

            Keywords = DataFunctions.RemoveDuplicates(Keywords);

            _ds =  DataFunctions.CreateDataSetFromDataGridView(dvgKeywords);

            if (_ds == null)
            {
                _ds = _KeywordsMgr.GetEmptyKeywordsLib();
            }

            int UID = DataFunctions.FindMaxValue(_ds.Tables[0], KeywordsFoundFields.UID);

            foreach (string newKeyword in Keywords)
            {
                if (!DataFunctions.FindValueInDataTable(_ds.Tables[0], KeywordsFoundFields.Keyword, newKeyword.Trim())) // Check for Dups
                {
                    UID++;

                    DataRow row = _ds.Tables[0].NewRow();

                    row[KeywordsFoundFields.UID] = UID;
                    row[KeywordsFoundFields.Keyword] = newKeyword.Trim();
                    row[KeywordsFoundFields.ColorHighlight] = this.cmbboxClr2.SelectedItem.ToString();

                    _ds.Tables[0].Rows.Add(row);

                    KeywordsAdded++;
                }
            }

            dvgKeywords.DataSource = _ds.Tables[0];

            setKeywordBackColor();

            return KeywordsAdded;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            if (dvgKeywords.RowCount == 0) // Validate
            {
                MessageBox.Show("Please enter Keywords before saving.", "No Keywords", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (txtbFileName.Text.Trim() == string.Empty) // Validate
            {
                MessageBox.Show("Please enter Keyword Group Name before saving.", "No Keyword Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (_isNew) // Validate -- Keyword Exists?
            {
                string fileName = txtbFileName.Text.Trim();


                string newPathFile = string.Concat(_path, @"\", fileName, ".xml");

                if (File.Exists(newPathFile))
                {
                    string msg = "Keyword group already exists! – Saving these changes will overwrite an existing Keyword Group.";
                    if (MessageBox.Show(msg, "Confirm Overwrite Existing Keyword Group", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    {
                        return;
                    }
                }

            }


            _ds = DataFunctions.CreateDataSetFromDataGridView(dvgKeywords);

            string keywordLibFile = string.Concat(txtbFileName.Text.Trim(), ".xml");
            _file = Path.Combine(AppFolders.KeywordGrpPath, keywordLibFile);
            _ds.WriteXml(_file, XmlWriteMode.WriteSchema);
            //GenericDataManger gDataMgr = new GenericDataManger();
            //gDataMgr.SaveDataXML(_ds, _file);
            Application.DoEvents();

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void txtbMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
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

        private void cmbboxClr2_DrawItem(object sender, DrawItemEventArgs e)
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

        private void dvgKeywords_SelectionChanged(object sender, EventArgs e)
        {
            
            DataGridViewRow row = dvgKeywords.CurrentRow;

            if (row == null)
                return;

            if (row.Cells[KeywordsFoundFields.Keyword].Value == null)
                return;

            string selectedKeyword = row.Cells[KeywordsFoundFields.Keyword].Value.ToString();
            string colorHighlight = row.Cells[KeywordsFoundFields.ColorHighlight].Value.ToString();

            txtbSection.Text = selectedKeyword;


            int index = cmbboxClr.FindString(colorHighlight);
             if (index != -1)
                cmbboxClr.SelectedIndex = index;

        }

        private void setKeywordBackColor()
        {
            string colorHighlight = string.Empty;

            foreach (DataGridViewRow row in dvgKeywords.Rows)
            {
                if (row.Cells[KeywordsFoundFields.ColorHighlight].Value != null)
                {
                    colorHighlight = row.Cells[KeywordsFoundFields.ColorHighlight].Value.ToString();
                    row.Cells[KeywordsFoundFields.ColorHighlight].Style.BackColor = Color.FromName(colorHighlight);
                }

            }


         }

        private void butBatchLoad_Click(object sender, EventArgs e)
        {
            _subPanel = subPanels.BatchLoad;
            ModeAdjust();

        }

        private void butMaintain_Click(object sender, EventArgs e)
        {
            _subPanel = subPanels.Maintain;
            ModeAdjust();
        }

        private void butBatchLoad_MouseEnter(object sender, EventArgs e)
        {
            if (butBatchLoad.BackColor == Color.WhiteSmoke)
            {
                butBatchLoad.BackColor = Color.LightGreen;
            }
        }

        private void butBatchLoad_MouseLeave(object sender, EventArgs e)
        {
            if (_subPanel != subPanels.BatchLoad)
            {
                butBatchLoad.BackColor = Color.WhiteSmoke;
            }
        }

        private void butMaintain_MouseEnter(object sender, EventArgs e)
        {
            if (butMaintain.BackColor == Color.WhiteSmoke)
            {
                butMaintain.BackColor = Color.LightGreen;
            }
        }

        private void butMaintain_MouseLeave(object sender, EventArgs e)
        {
            if (_subPanel != subPanels.Maintain)
            {
                butMaintain.BackColor = Color.WhiteSmoke;
            }
        }



    }
    
}
