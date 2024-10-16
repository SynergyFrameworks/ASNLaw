using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;
using Atebion_Dictionary;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class ucDictionarySelect : UserControl
    {
        public ucDictionarySelect()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }


        // Declare delegate for when a project has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Dictionary is selected")]
        public event ProcessHandler DicSelected;

        public bool SynonymsFind
        {
            get { return chkbSynonyms.Checked; }
        }

        public bool WholeWordsFind
        {
            get { return chkbWholeWords.Checked; }
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


            AutoSizeCols();

            if (DicSelected != null)
                DicSelected();


        }

        private void butFind_Click(object sender, EventArgs e)
        {
            FindItem();

        }

        private void txtbFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindItem();
            }
        }

    }
}
