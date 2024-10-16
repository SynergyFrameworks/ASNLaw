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

namespace ProfessionalDocAnalyzer
{
    public partial class ucDictionaries : UserControl
    {
        public ucDictionaries()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }


        private Atebion_Dictionary.Dictionary _DictionaryMgr;
        private string _DictionaryRootPath = string.Empty;

        private string _CurrentDictionary = string.Empty;
        public string DictionarySelected
        {
            get { return _CurrentDictionary; }
        }

        public void LoadData(string DictionaryRootPath)
        {
            dvgDictionaries.DataSource = null;

            _DictionaryRootPath = DictionaryRootPath;

            _DictionaryMgr = new Atebion_Dictionary.Dictionary(_DictionaryRootPath);

            string[] dictionaries = _DictionaryMgr.Get_Dictionaries();

            lstbDictionaries.DataSource = dictionaries;

        }

        public void Download()
        {
            frmDownLoad frm = new frmDownLoad();
            frm.LoadData("dicx", _DictionaryRootPath, ContentTypes.Dictionaries);

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData(_DictionaryRootPath);
            }
        }

 

        private void lstbDictionaries_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblDicDescription.Text = string.Empty;

            _CurrentDictionary = lstbDictionaries.Text;

            dvgDictionaries.DataSource = null;

            DataSet ds = _DictionaryMgr.GetDataset(_CurrentDictionary);
            if (ds == null)
            {
                MessageBox.Show(_DictionaryMgr.ErrorMessage, "Unable to Retrieve the Selected Dictionary Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (ds.Tables[DictionaryFieldConst.TableName].Rows.Count == 0)
            {
                MessageBox.Show("The selected Dictionary does not have any items", "No Dictionary Items Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DataView dv = _DictionaryMgr.GetDataView_Transformation();

            dvgDictionaries.DataSource = dv;

            dvgDictionaries.Columns[0].Visible = false;
            dvgDictionaries.Columns[1].Visible = false;
            dvgDictionaries.Columns[5].Visible = false;
            dvgDictionaries.Columns[6].Visible = false;

           

            lblDicDescription.Text = _DictionaryMgr.GetDictionaryDescription();

  //          dvgDictionaries.Columns["Definition"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            AutoSizeCols();


            // setDictionariesBackColor();

        }

        private void AutoSizeCols()
        {
            //set autosize mode
            dvgDictionaries.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dvgDictionaries.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dvgDictionaries.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            //datagrid has calculated it's widths so we can store them
            for (int i = 2; i <= 5 - 1; i++)
            {
                //store autosized widths
                int colw = dvgDictionaries.Columns[i].Width;
                float colh = dvgDictionaries.Columns[i].FillWeight;
                //remove autosizing
                dvgDictionaries.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
                //set width to calculated by autosize
                dvgDictionaries.Columns[i].Width = colw;
                dvgDictionaries.Columns[i].FillWeight = colh;

                dvgDictionaries.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            }

        }


        private void setDictionariesBackColor()
        {
            dvgDictionaries.Columns[DictionaryFieldConst.Weight].Visible = false;

            //string colorHighlight = string.Empty;

            //foreach (DataGridViewRow row in dvgDictionaries.Rows)
            //{
            //    if (row.Cells[DictionaryFieldConst.HighlightColor].Value != null)
            //    {
            //        colorHighlight = row.Cells[DictionaryFieldConst.HighlightColor].Value.ToString();
            //        row.Cells[DictionaryFieldConst.HighlightColor].Style.BackColor = Color.FromName(colorHighlight);
            //    }

            //}
        }

        public bool Import()
        {
            frmImportDic frm = new frmImportDic(AppFolders.DictionariesPath, AppFolders.KeywordGrpPath);

            frm.LoadData();

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData(AppFolders.DictionariesPath);
            }

            return true;
        }

        public bool Edit()
        {
            if (this.lstbDictionaries.Text == string.Empty)
                return false;

            frmDictionary2 frm = new frmDictionary2(AppFolders.DictionariesPath, this.lstbDictionaries.Text);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData(AppFolders.DictionariesPath);
            }


            return true;

        }

        public bool Export()
        {
            if (this.lstbDictionaries.Text == string.Empty)
                return false;

            frmDictionary2 frm = new frmDictionary2(AppFolders.DictionariesPath, this.lstbDictionaries.Text);
            LoadData(AppFolders.DictionariesPath);
            frm.DataSetToExcel();
            frm.Visible = false;
            return true;

        }
        public bool New()
        {
            frmDictionary2 frm = new frmDictionary2(AppFolders.DictionariesPath);
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData(AppFolders.DictionariesPath);
            }

            return true;
        }


        public bool Delete()
        {
            bool returnValue = false;
            if (lstbDictionaries.Items.Count == 0)
                return returnValue;

            if (this.lstbDictionaries.SelectedIndex < 0)
                this.lstbDictionaries.SelectedIndex = 0;

            string msg = string.Concat("Are you sure you want to remove Dictionary: ", this.lstbDictionaries.SelectedItem.ToString());
            if (MessageBox.Show(msg, "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string file = string.Concat(lstbDictionaries.Text, ".dicx");

                string fromPathFile = Path.Combine(AppFolders.DictionariesPath, file);
                string toPathFile = Path.Combine(AppFolders.DictionariesRemovedPath, file);
                if (File.Exists(fromPathFile))
                {
                    if (File.Exists(toPathFile))
                    {
                        toPathFile = Files.SetFileName2Obsolete(toPathFile);
                        if (toPathFile == string.Empty)
                        {
                            toPathFile = Path.Combine(AppFolders.DictionariesRemovedPath, file);
                        }
                    }

                    File.Copy(fromPathFile, toPathFile, true);

                    File.Delete(fromPathFile);

                    LoadData(AppFolders.DictionariesPath);

                    returnValue = true;
                }
            }

            return returnValue;

        }

        private void lblDicDescription_TextChanged(object sender, EventArgs e)
        {
            txtDicDescription.Text = lblDicDescription.Text;
        }

        private void txtDicDescription_TextChanged(object sender, EventArgs e)
        {
            txtDicDescription.Text = lblDicDescription.Text;
        }

        private void butFind_Click(object sender, EventArgs e)
        {
            FindItem();
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
                    break;
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

        private void lblDictionaries_Click(object sender, EventArgs e)
        {
            Process.Start(_DictionaryRootPath);
        }

        private void picDictionaries_Click(object sender, EventArgs e)
        {
            if (_DictionaryRootPath.Length > 0)
                System.Diagnostics.Process.Start("explorer.exe", _DictionaryRootPath);
        }

    }
}
