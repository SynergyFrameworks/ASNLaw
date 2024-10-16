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

namespace ProfessionalDocAnalyzer
{
    public partial class ucRAMModels : UserControl
    {
        public ucRAMModels()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private string _ModelPath = string.Empty;
        private string _CurrentModel = string.Empty;

        public bool LoadData()
        {
            lstbRAMModels.Items.Clear();
            dvgModelItems.DataSource = null;

            _ModelPath = AppFolders.AppDataPathToolsRAMDefs;

            if (_ModelPath == string.Empty)
            {
                MessageBox.Show("Unable to file RAM Models folder.", "RAM Models Folder Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            string[] files = Directory.GetFiles(_ModelPath, "*.ram");

            string filename = string.Empty;
            foreach (string file in files)
            {
                filename = Files.GetFileNameWOExt(file);
                lstbRAMModels.Items.Add(filename);
            }

            return true;
        }

        public bool Edit()
        {
            if (this.lstbRAMModels.Text == string.Empty)
                return false;

            frmRAMModel frm = new frmRAMModel(_ModelPath, this.lstbRAMModels.Text);
            if (!frm.LoadData())
                return false;

            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
            }


            return true;

        }

        public bool New()
        {
            frmRAMModel frm = new frmRAMModel(_ModelPath);
            frm.LoadData();
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                LoadData();
            }

            return true;
        }


        public bool Delete()
        {
            
            bool returnValue = false;
            if (lstbRAMModels.Items.Count == 0)
                return returnValue;

            if (this.lstbRAMModels.SelectedIndex < 0)
                this.lstbRAMModels.SelectedIndex = 0;

            string msg = string.Concat("Are you sure you want to remove the selected Model: ", this.lstbRAMModels.SelectedItem.ToString());
            if (MessageBox.Show(msg, "Confirm Removal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {

                string file = string.Concat(lstbRAMModels.Text, ".ram");

                string fromPathFile = Path.Combine(_ModelPath, file);
                string toPathFile = Path.Combine(AppFolders.AppDataPathToolsRAMDefs_Removed, file);
                if (File.Exists(fromPathFile))
                {
                    if (File.Exists(toPathFile))
                    {
                        toPathFile = Files.SetFileName2Obsolete(toPathFile);
                        if (toPathFile == string.Empty)
                        {
                            toPathFile = Path.Combine(AppFolders.AppDataPathToolsRAMDefs_Removed, file);
                        }
                    }

                    File.Copy(fromPathFile, toPathFile, true);

                    File.Delete(fromPathFile);

                    LoadData();

                    returnValue = true;
                }
            }

            return returnValue;

        }

        private void lstbRAMModels_SelectedIndexChanged(object sender, EventArgs e)
        {
       //     lblDicDescription.Text = string.Empty;

            _CurrentModel = lstbRAMModels.Text;

            dvgModelItems.DataSource = null;

            string  file = string.Concat(_CurrentModel, ".ram");
            string pathFile = Path.Combine(_ModelPath, file);

            if (!File.Exists(pathFile))
            {
                string msg = string.Concat("Unable to file the selected RAM Model file: ", pathFile);
                MessageBox.Show(msg, "RAM Model Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            GenericDataManger gDMgr = new GenericDataManger();
            DataSet ds = gDMgr.LoadDatasetFromXml(pathFile);

            if (ds == null)
            {
                MessageBox.Show(gDMgr.ErrorMessage, "Unable to Retrieve the Selected RAM Model Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            if (ds.Tables["RAMDef"].Rows.Count == 0)
            {
                MessageBox.Show("The selected RAM Model does not have any items", "No Model Items Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }


            dvgModelItems.DataSource = ds.Tables["RAMDef"];

            dvgModelItems.Columns["RoleDescription"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dvgModelItems.Columns["UID"].Visible = false;
            dvgModelItems.Columns["RoleColor"].Visible = false;

            dvgModelItems.Columns["RoleNotation"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dvgModelItems.ColumnHeadersVisible = false;

            AutoSizeCols();

        }


        private void AutoSizeCols()
        {
            //set autosize mode
            dvgModelItems.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dvgModelItems.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
          //  dvgModelItems.Columns[3].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dvgModelItems.Columns[4].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            ////datagrid has calculated it's widths so we can store them
            //for (int i = 2; i <= 5 - 1; i++)
            //{
            //    //store autosized widths
            //    int colw = dvgModelItems.Columns[i].Width;
            //    float colh = dvgModelItems.Columns[i].FillWeight;
            //    //remove autosizing
            //    dvgModelItems.Columns[i].AutoSizeMode = DataGridViewAutoSizeColumnMode.None;
            //    //set width to calculated by autosize
            //    dvgModelItems.Columns[i].Width = colw;
            //    dvgModelItems.Columns[i].FillWeight = colh;

            //    dvgModelItems.Columns[i].DefaultCellStyle.WrapMode = DataGridViewTriState.True;


            //}

        }


        private void setBackColor()
        {

            string colorHighlight = string.Empty;

            foreach (DataGridViewRow row in dvgModelItems.Rows)
            {
                if (row.Cells["RoleColor"].Value != null)
                {
                    colorHighlight = row.Cells["RoleColor"].Value.ToString();
                    row.Cells["RoleNotation"].Style.BackColor = Color.FromName(colorHighlight);
                }

            }

       //     dvgModelItems.RowHeadersVisible = false;


        }

        private void dvgModelItems_Paint(object sender, PaintEventArgs e)
        {
            setBackColor();
        }

        private void dvgModelItems_SelectionChanged(object sender, EventArgs e)
        {
            dvgModelItems.ClearSelection();  
        }

        private void picHeader_Click(object sender, EventArgs e)
        {
            if (_ModelPath.Length > 0)
                System.Diagnostics.Process.Start("explorer.exe", _ModelPath);
        }

    }
}
