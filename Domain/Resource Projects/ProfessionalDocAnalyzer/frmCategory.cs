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
    public partial class frmCategory : MetroFramework.Forms.MetroForm
    {
        public frmCategory(DataSet ds)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            _ds = ds;

            LoadData();
        }

        private DataSet _ds;

        public DataSet DictionaryDataset
        {
            get { return _ds; }
        }

        private Atebion_Dictionary.Dictionary _DictionaryMgr;


        private void LoadData()
        {
            lstbCategories.Items.Clear();

            _DictionaryMgr = new Dictionary(AppFolders.DictionariesPath);
            _DictionaryMgr.Dictionary_DataSet = _ds;

            string[] categories = _DictionaryMgr.GetCategories();

            if (categories == null) // none found -- Should always have a default category, typically "General"
                return;

            foreach (string category in categories)
            {
                lstbCategories.Items.Add(category);
            }

        }



        private void butOK_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void butNew_Click(object sender, EventArgs e)
        {
            string newCat = txtbName.Text.Trim();

            if (CatExists(newCat))
            {
                MessageBox.Show("Please enter another Category Name.", "Category Name Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            int catUID;
            if (!_DictionaryMgr.SetNewCategory(newCat, out catUID))
            {
                MessageBox.Show(_DictionaryMgr.ErrorMessage, "Unable to Save New Category", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _ds = _DictionaryMgr.Dictionary_DataSet;

            lstbCategories.Items.Add(newCat);

            int index = lstbCategories.Items.IndexOf(newCat);

            lstbCategories.SelectedIndex = index;
        }

        private bool CatExists(string newCat)
        {
            bool found = lstbCategories.Items.Contains(newCat.Trim());

            if (found)
                return true;

            foreach (string cat in lstbCategories.Items)
            {
                if (newCat.ToLower() == cat.Trim().ToLower())
                {
                    return true;
                }
            }


            return false;

        }

        private void butReplace_Click(object sender, EventArgs e)
        {
            string updateCat = txtbName.Text.Trim();

            string selectedCat = lstbCategories.Text;

            int uid = _DictionaryMgr.GetCategoryUID(selectedCat);

            if (!_DictionaryMgr.CategoryUpdate(uid, updateCat))
            {
                MessageBox.Show(_DictionaryMgr.ErrorMessage, "Unable to Save Category Change", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            LoadData();

            int index = lstbCategories.Items.IndexOf(updateCat);

            lstbCategories.SelectedIndex = index;
        }

        private void lstbCategories_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtbName.Text = lstbCategories.Text;
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            string selectedCat = lstbCategories.Text;

            int uid = _DictionaryMgr.GetCategoryUID(selectedCat);

            string[] dicItems = _DictionaryMgr.GetDictionaryItemsPerCat(uid);
            if (dicItems.Length > 0)
            {
                string msg = string.Concat("Unable to Remove the selected Category because it is allocated to ", dicItems.Length.ToString(), " Dictionary Items.");
                string title = string.Concat("Unable to Removed Category: ", selectedCat);
                MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (!_DictionaryMgr.CategoryRemove(uid))
            {
                MessageBox.Show(_DictionaryMgr.ErrorMessage, "Unable to Remove Category", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            LoadData();
        }
    }
}
