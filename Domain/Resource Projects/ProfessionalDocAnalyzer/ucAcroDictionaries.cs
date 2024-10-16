using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using AcroParser;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class ucAcroDictionaries : UserControl
    {
        public ucAcroDictionaries()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private void ucAcroDictionaries_Load(object sender, EventArgs e)
        {

        }

        private subPanels _subPanel = subPanels.Dictionary;
        private enum subPanels
        {
            Dictionary = 0,
            Ignore = 1,
        }
        

        public void LoadData()
        {
            // Initialize basic DataGridView properties.
            //  dgvAcronyms.Dock = DockStyle.Fill;
            //dgvAcronyms.BackgroundColor = Color.Black;
            //dgvAcronyms.BorderStyle = BorderStyle.None;

            //// Set property values appropriate for read-only display and 
            //// limited interactivity. 
            //dgvAcronyms.AllowUserToAddRows = false;
            //dgvAcronyms.AllowUserToDeleteRows = false;
            //dgvAcronyms.AllowUserToOrderColumns = false;
            //dgvAcronyms.ReadOnly = true;
            //dgvAcronyms.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            //dgvAcronyms.MultiSelect = false;
            //dgvAcronyms.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            //dgvAcronyms.AllowUserToResizeColumns = false;
            //dgvAcronyms.ColumnHeadersHeightSizeMode = 
            //    DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            //dgvAcronyms.AllowUserToResizeRows = false;
            //dgvAcronyms.RowHeadersWidthSizeMode = 
            //    DataGridViewRowHeadersWidthSizeMode.DisableResizing;


            string yfile;

            // Load Dictionaries ...
            lstbDics.Items.Clear();
            this.dgvAcronyms.DataSource = null;
            string[] files = Files.GetFilesFromDir(AppFolders.AppDataPathToolsAcroSeekerDefLib, "*.xml");
            foreach (string xfile in files)
            {
                yfile = Files.GetFileNameWOExt(xfile);
                lstbDics.Items.Add(yfile);
            }



            // Load Ignore List ...
            lstbIgnore.Items.Clear();
            this.dgvIgnore.DataSource = null;
            files = Files.GetFilesFromDir(AppFolders.AppDataPathToolsAcroSeekerIgnoreLib, "*.xml");
            foreach (string xfile in files)
            {
                yfile = Files.GetFileNameWOExt(xfile);
                lstbIgnore.Items.Add(yfile);
            }

            _subPanel = subPanels.Dictionary;

            ModeAdjust();
            

        }

        public void AddNew()
        {

            if (_subPanel == subPanels.Dictionary)
            {

                //      string selDefLib = string.Concat(lstbDics.GetItemText(lstbDics.SelectedItem));
                frmDefLib frm = new frmDefLib();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadData();
                }
            }
            else 
            {
                frmIgnoreLst frm = new frmIgnoreLst();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadData();
                }
            }


        }

  

        public bool New()
        {
            if (_subPanel == subPanels.Dictionary)
            {

                frmDefLib frm = new frmDefLib();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadData();
                    return true;
                }
            }
            else
            {
                frmIgnoreLst frm = new frmIgnoreLst();
                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadData();
                    return true;
                }
            }

            return false;
        }

        public bool Edit()
        {

            if (_subPanel == subPanels.Dictionary)
            {

                string selDefLib = string.Concat(lstbDics.GetItemText(lstbDics.SelectedItem));
                int selectedIndex = lstbDics.SelectedIndex;
                frmDefLib frm = new frmDefLib(selDefLib);
                frm.LoadData();

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadData();
                    lstbDics.SelectedIndex = selectedIndex;
                    // lstbDics.SelectedItem = "selDefLib";
                    SelectDicLib();
                    return true;
                }
            }
            else 
            {
                string selDefLib = string.Concat(this.lstbIgnore.GetItemText(lstbIgnore.SelectedItem));
                int selectedIndex = lstbIgnore.SelectedIndex;
                frmIgnoreLst frm = new frmIgnoreLst(selDefLib);
                

                if (frm.ShowDialog(this) == DialogResult.OK)
                {
                    LoadData();
                    lstbIgnore.SelectedIndex = selectedIndex;
                    SelectIgnoreLib();
                    return true;
                }
            }

            return false;
        }

        public void Download()
        {

            Cursor.Current = Cursors.WaitCursor; // Waiting

            frmDownLoad frm = new frmDownLoad();

            if (_subPanel == subPanels.Dictionary) // Acronym Dictionaries
            {
                frm.LoadData("xml", AppFolders.AppDataPathToolsAcroSeekerDefLib, ContentTypes.Acronym_Dictionaries);
            }
            else // Ignore Dictionaries
            {
                frm.LoadData("xml", AppFolders.AppDataPathToolsAcroSeekerIgnoreLib, ContentTypes.Acronym_Ignore_Dictionaries);
            }

            Cursor.Current = Cursors.Default; // Back to normal

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
            }

        }

        public bool Delete()
        {

            if (_subPanel == subPanels.Dictionary)
            {
                string selDefLib = string.Concat(lstbDics.GetItemText(lstbDics.SelectedItem));
                int selectedIndex = lstbDics.SelectedIndex;
                if (selectedIndex == -1)
                {
                    MessageBox.Show("Please select a Dictionary to Delete.", "No Dictionary Library Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                string msg = string.Concat("Are you sure you want to Delete the selected Dictionary ", selDefLib, "?");
                if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string pathFile = Path.Combine(AppFolders.AppDataPathToolsAcroSeekerDefLib, selDefLib);
                    pathFile = pathFile + ".xml";
                    File.Delete(pathFile);
                    this.LoadData();
                    return true;
                }

            }
            else
            {
                string selIgnoreLib = string.Concat(lstbIgnore.GetItemText(lstbIgnore.SelectedItem));
                int selectedIndex = lstbIgnore.SelectedIndex;
                if (selectedIndex == -1)
                {
                    MessageBox.Show("Please select a Ignore Dictionary to Delete.", "No Ignore Dictionary Selected", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return false;
                }
                string msg = string.Concat("Are you sure you want to Delete the selected Ignore Dictionary ", selIgnoreLib, "?");
                if (MessageBox.Show(msg, "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    string pathFile = Path.Combine(AppFolders.AppDataPathToolsAcroSeekerIgnoreLib, selIgnoreLib);
                    pathFile = pathFile + ".xml";
                    File.Delete(pathFile);
                    this.LoadData();
                    return true;
                }
            }

            return false;

        }

        private void HidePanels()
        {
            panIgnoreDictionaries.Visible = false;
            panAcronymsDictionaries.Visible = false;

            butAcronymsDictionaries.BackColor = Color.WhiteSmoke;
            butIgnoreDictionaries.BackColor = Color.WhiteSmoke;

        }
        
        private void ModeAdjust()
        {
            HidePanels();

            switch (_subPanel)
            {
                case subPanels.Dictionary:
                    panAcronymsDictionaries.Visible = true;
                    panAcronymsDictionaries.Dock = DockStyle.Fill;
                    butAcronymsDictionaries.BackColor = Color.LightGreen;
                    butAcronymsDictionaries.Font = new Font(butAcronymsDictionaries.Font, FontStyle.Bold);

                    break;
                case subPanels.Ignore:
                    panIgnoreDictionaries.Visible = true;
                    panIgnoreDictionaries.Dock = DockStyle.Fill;
                    butIgnoreDictionaries.BackColor = Color.LightGreen;
                    butIgnoreDictionaries.Font = new Font(butIgnoreDictionaries.Font, FontStyle.Bold);

                    break;

            }
        }

        private void lblHeader_Click(object sender, EventArgs e)
        {

        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void txtbMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }


        private void SelectDicLib()
        {
            if (lstbDics.SelectedItem == null)
                return;

            string selFile = string.Concat(AppFolders.AppDataPathToolsAcroSeekerDefLib, @"\", lstbDics.SelectedItem.ToString(), ".xml");

            if (!File.Exists(selFile)) // Added in Beta 2.1 04.13.2016
            {
                MessageBox.Show("Your selected dictionary does not exist.", "Selected dictionary Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                LoadData();
                return;
            }

            DefinitionsLibs defLib = new DefinitionsLibs(AppFolders.AppDataPathToolsAcroSeekerDefLib);
            DataSet dsDefLib = defLib.GetDataset(lstbDics.SelectedItem.ToString());
            dgvAcronyms.DataSource = dsDefLib.Tables[0];

            dgvAcronyms.Columns[0].Visible = false;


            dgvAcronyms.Sort(this.dgvAcronyms.Columns["Acronym"], ListSortDirection.Ascending); // Added 04.10.2016 for Beta 2.1


            FileInformation selectedFileInfo = new FileInformation(selFile);

            lblMessage.Text = string.Concat(
                " ", "\r\n",
                "Created: ", selectedFileInfo.CreationDate, " ", "\r\n",
                "Modified: ", selectedFileInfo.LastWriteTime, " ", "\r\n",
                "Qty: ", dgvAcronyms.RowCount.ToString()
                    );

        }

        private void lstbDics_Click(object sender, EventArgs e)
        {
            SelectDicLib();
        }


        private void picHeader_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("explorer.exe", AppFolders.AppDataPathToolsAcroSeekerDefLib);
        }

        private void SelectIgnoreLib()
        {
            if (lstbIgnore.SelectedItem == null)
                return;

            string selFile = string.Concat(AppFolders.AppDataPathToolsAcroSeekerIgnoreLib, @"\", lstbIgnore.SelectedItem.ToString(), ".xml");

            AcroIgnoreList defLib = new AcroIgnoreList(AppFolders.AppDataPathToolsAcroSeekerIgnoreLib);
            DataSet dsIgnLib = defLib.GetDataset(lstbIgnore.SelectedItem.ToString());
            dgvIgnore.DataSource = dsIgnLib.Tables[0];

            dgvIgnore.Columns[0].Visible = false;

            dgvIgnore.Sort(this.dgvIgnore.Columns["Acronym"], ListSortDirection.Ascending); // Added 04.10.2016 for Beta 2.1


            FileInformation selectedFileInfo = new FileInformation(selFile);

            lblMessage2.Text = string.Concat(
                " ", "\r\n",
                "Created: ", selectedFileInfo.CreationDate, " ", "\r\n",
                "Modified: ", selectedFileInfo.LastWriteTime, " ", "\r\n",
                "Qty: ", dgvIgnore.RowCount.ToString()
                    );
        }

        private void lstbIgnore_Click(object sender, EventArgs e)
        {
            SelectIgnoreLib();
        }

        private void lblMessage2_TextChanged(object sender, EventArgs e)
        {
            txtbMessage2.Text = lblMessage2.Text;
        }

        private void txtbMessage2_TextChanged(object sender, EventArgs e)
        {
            txtbMessage2.Text = lblMessage2.Text;
        }

        private void FindAcronym()
        {
            String searchValue = this.txtbFind.Text.Trim().ToUpper();

            if (searchValue == string.Empty)
                return;

            string selX = "Acronym"; // cboFind.SelectedItem.ToString();


            int rowIndex = -1;
            if (_subPanel == subPanels.Dictionary)
            {
                foreach (DataGridViewRow row in dgvAcronyms.Rows)
                {
                    if (row.Cells[selX].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = row.Index;
                        dgvAcronyms.Rows[rowIndex].Selected = true;
                        dgvAcronyms.FirstDisplayedScrollingRowIndex = dgvAcronyms.SelectedRows[0].Index;
                        break;
                    }
                }
            }
            else
            {
                foreach (DataGridViewRow row in dgvIgnore.Rows)
                {
                    if (row.Cells[selX].Value.ToString().Equals(searchValue))
                    {
                        rowIndex = row.Index;
                        dgvIgnore.Rows[rowIndex].Selected = true;
                        dgvIgnore.FirstDisplayedScrollingRowIndex = dgvIgnore.SelectedRows[0].Index;
                        break;
                    }
                }
            }

        }

        private void butFind_Click(object sender, EventArgs e)
        {
            FindAcronym();
        }

        private void txtbFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindAcronym();
            }
        }

        private void butAcronymsDictionaries_MouseEnter(object sender, EventArgs e)
        {
            if (butAcronymsDictionaries.BackColor == Color.WhiteSmoke)
            {
                butAcronymsDictionaries.BackColor = Color.LightGreen;
                butAcronymsDictionaries.Font = new Font(butAcronymsDictionaries.Font, FontStyle.Bold);
            }

        }

        private void butAcronymsDictionaries_MouseLeave(object sender, EventArgs e)
        {
            if (_subPanel != subPanels.Dictionary)
            {
                butAcronymsDictionaries.BackColor = Color.WhiteSmoke;
                butAcronymsDictionaries.Font = new Font(butAcronymsDictionaries.Font, FontStyle.Regular);
            }
        }

        private void butIgnoreDictionaries_MouseEnter(object sender, EventArgs e)
        {
            if (butIgnoreDictionaries.BackColor == Color.WhiteSmoke)
            {
                butIgnoreDictionaries.BackColor = Color.LightGreen;
                butIgnoreDictionaries.Font = new Font(butIgnoreDictionaries.Font, FontStyle.Bold);
            }
        }

        private void butIgnoreDictionaries_MouseLeave(object sender, EventArgs e)
        {
            if (_subPanel != subPanels.Ignore)
            {
                butIgnoreDictionaries.BackColor = Color.WhiteSmoke;
                butIgnoreDictionaries.Font = new Font(butIgnoreDictionaries.Font, FontStyle.Regular);
            }
        }

        private void butAcronymsDictionaries_Click(object sender, EventArgs e)
        {
            _subPanel = subPanels.Dictionary;
            ModeAdjust();

        }

        private void butIgnoreDictionaries_Click(object sender, EventArgs e)
        {
            _subPanel = subPanels.Ignore;
            ModeAdjust();

        }

        private void lstbDics_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectDicLib();
        }

        private void lstbIgnore_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectIgnoreLib();
        }

        private void picHeader_Click_1(object sender, EventArgs e)
        {
            if (_subPanel == subPanels.Dictionary)
            {
                if (AppFolders.AppDataPathToolsAcroSeekerDefLib.Length > 0)
                    System.Diagnostics.Process.Start("explorer.exe", AppFolders.AppDataPathToolsAcroSeekerDefLib);
            }
            else
            {
                if (AppFolders.AppDataPathToolsAcroSeekerDefLib.Length > 0)
                    System.Diagnostics.Process.Start("explorer.exe", AppFolders.AppDataPathToolsAcroSeekerIgnoreLib);
            }
        }

 

    }
}
