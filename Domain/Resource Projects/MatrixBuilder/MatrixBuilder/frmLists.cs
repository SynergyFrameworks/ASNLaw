using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WorkgroupMgr;

namespace MatrixBuilder
{
    public partial class frmLists : MetroFramework.Forms.MetroForm
    {

        public frmLists(WorkgroupMgr.ListMgr ListMgr)
        {
            InitializeComponent();
            _ListMgr = ListMgr;

            LoadData();
        }

        public frmLists(string ListName, WorkgroupMgr.ListMgr ListMgr)
        {
            InitializeComponent();

            _ListName = ListName;
            _ListMgr = ListMgr;

            LoadData();
        }

        string _ListName = string.Empty;
        WorkgroupMgr.ListMgr _ListMgr;

        private const string BATCH_LOAD_MSG = "Batch Load give you the ability to import a CSV file. CSV is a delimited text file that uses a comma to separate values. Click the Import File button.";
        
        private bool LoadData()
        {
            lblMessage.Text = BATCH_LOAD_MSG;

            if (_ListName.Length > 0)
            {
                txtbListName.Text = _ListName;
                txtbDescription.Text = _ListMgr.GetDescription(_ListName);

                lstbListItems.Items.Clear();

                string[] items = _ListMgr.GetListItems(_ListName);

                if (items.Length > 0)
                {
                    foreach (string item in items)
                    {
                        lstbListItems.Items.Add(item);
                    }
                }
                else
                {
                    string error = _ListMgr.ErrorMessage;
                    if (error.Length > 0)
                    {
                        lblMessage.Text = error;
                        lblMessage.ForeColor = Color.Tomato;
                        return false;
                    }
                }                
            }

            return true;
        }

        private void butAddBatch_Click(object sender, EventArgs e)
        {
            string batchItems = txtBatch.Text;

            if (batchItems.Trim() == string.Empty)
                return;

            if (batchItems.IndexOf(',') == -1)
                return;

            this.txtBatch.Text = WorkgroupMgr.DataFunctions.ReplaceSingleQuote(txtBatch.Text);

            string[] items = batchItems.Split(',');
            foreach (string item in items)
            {
                if (!lstbListItems.Items.Contains(item.Trim()))
                {
                    lstbListItems.Items.Add(item.Trim());
                }
            }
        }

        private void butImportFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "Text Files (*.txt)|*.txt| Comma Separated Values|*.CSV|All Files|*.*";
            this.openFileDialog1.FilterIndex = 1;
            openFileDialog1.FileName = string.Empty;

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor; // Waiting 

                txtBatch.Text = string.Empty;

                string selectedFile = openFileDialog1.FileName;


                string csv = WorkgroupMgr.Files.ReadFile(selectedFile);
                txtBatch.Text = csv;

                Cursor.Current = Cursors.Default;
            }
        }

        private void butNew_Click(object sender, EventArgs e)
        {
            string item = txtItem.Text.Trim();

            if (!lstbListItems.Items.Contains(item))
            {
                lstbListItems.Items.Add(item);
            }
        }

        private void butReplace_Click(object sender, EventArgs e)
        {
            string item = txtItem.Text.Trim();

            if (!lstbListItems.Items.Contains(item))
            {
                if (lstbListItems.SelectedIndex != -1)
                {
                    lstbListItems.Items[lstbListItems.SelectedIndex] = item;                   
                }
            }
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            if (lstbListItems.SelectedIndex != -1)
            {
                lstbListItems.Items.Remove(lstbListItems.Items[lstbListItems.SelectedIndex]);
            }
        }


        private bool Validate()
        {
            if (txtbListName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter a List Name.", "No List Name", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (lstbListItems.Items.Count == 0)
            {
                MessageBox.Show("Please add List Items prior to saving this List.", "No List Items", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (txtbDescription.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter a List Description before save this List.", "No List Description", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }

            if (_ListName.Length == 0)
            {
                if (_ListMgr.ListNameExists(txtbListName.Text))
                {
                    if (MessageBox.Show("The List Name you entered already exists. Are you sure you want to replace this existing List with your New List?", "Replace Existing List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                        return false;
                }
            }
            else
            {
                if (_ListName != txtbListName.Text)
                {
                    if (_ListMgr.ListNameExists(txtbListName.Text))
                    {
                        if (MessageBox.Show("The List Name you entered already exists. Are you sure you want to replace this existing List with your New List?", "Replace Existing List", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                            return false;
                    }
                }
            }
            return true;
        }
        private void butOk_Click(object sender, EventArgs e)
        {
            if (!Validate())
                return;

            string[] items = new string[lstbListItems.Items.Count];
            int itemCount = lstbListItems.Items.Count;
            for (int i = 0; i < itemCount; i++)
            {
                items[i] = lstbListItems.Items[i].ToString();
            }

            if (!_ListMgr.CreateList(txtbListName.Text.Trim(), txtbDescription.Text.Trim(), items))
            {
                string error = _ListMgr.ErrorMessage;
                if (error.Length > 0)
                {
                    MessageBox.Show(error, "List Not Saved", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

    }
}
