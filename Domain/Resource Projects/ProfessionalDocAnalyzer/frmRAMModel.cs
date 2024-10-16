using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using Atebion.Common;
using Atebion.ConceptAnalyzer;

namespace ProfessionalDocAnalyzer
{
    public partial class frmRAMModel : MetroFramework.Forms.MetroForm
    {
        /// <summary>
        /// New mode
        /// </summary>
        /// <param name="ModelPath"></param>
        public frmRAMModel(string ModelPath)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            _ModelPath = ModelPath;

            _isNew = true;
        }

        /// <summary>
        /// Edit mode
        /// </summary>
        /// <param name="ModelPath"></param>
        /// <param name="SelectedModel"></param>
        public frmRAMModel(string ModelPath, string SelectedModel)
        {
            InitializeComponent();

            _ModelPath = ModelPath;
            _SelectedModel = SelectedModel;

            _isNew = false;
        }

        private string _ModelPath = string.Empty;
        private string _SelectedModel = string.Empty;

        private bool _isNew = true;

        private DataSet _ds;

        private int _CurrentrowIndex = -1;

        private string _RoleName = string.Empty;
        private string _RoleNotation = string.Empty;
        private string _RoleColor = string.Empty;
        private string _RoleDescription = string.Empty;



        public bool LoadData()
        {
            // Load colors into drop-down list
            this.cmbboxClr.Items.Clear();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                if (c.Name != "Transparent")
                    this.cmbboxClr.Items.Add(c.Name);
            }

            if (_isNew)
            {
                if (_ds == null)
                {
                    Atebion.ConceptAnalyzer.ResponsAssigMatrix ramMgr = new Atebion.ConceptAnalyzer.ResponsAssigMatrix();

                    _ds = new DataSet();
                    DataTable dt = ramMgr.CreateTable_Defs();
                    _ds.Tables.Add(dt);
                }

                return true;
            }

            dvgModelItems.DataSource = null;

            string  file = string.Concat(_SelectedModel, ".ram");
            string pathFile = Path.Combine(_ModelPath, file);

            if (!File.Exists(pathFile))
            {
                string msg = string.Concat("Unable to file the selected RAM Model file: ", pathFile);
                MessageBox.Show(msg, "RAM Model Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            txtbRAMName.Text = _SelectedModel;

            GenericDataManger gDMgr = new GenericDataManger();
            _ds = gDMgr.LoadDatasetFromXml(pathFile);

            if (_ds == null)
            {
                MessageBox.Show(gDMgr.ErrorMessage, "Unable to Retrieve the Selected RAM Model Items", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return LoadRAMModel();

        }

        private bool LoadRAMModel()
        {

            if (_ds.Tables["RAMDef"].Rows.Count == 0)
            {
                //MessageBox.Show("The selected RAM Model does not have any items", "No Model Items Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            dvgModelItems.DataSource = _ds.Tables["RAMDef"];

            dvgModelItems.Columns["RoleDescription"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            dvgModelItems.Columns["UID"].Visible = false;
            dvgModelItems.Columns["RoleColor"].Visible = false;

            dvgModelItems.Columns["RoleNotation"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;


            dvgModelItems.ColumnHeadersVisible = false;

            AutoSizeCols();

            return true;

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




        private void cmbboxClr_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void dvgModelItems_Paint(object sender, PaintEventArgs e)
        {
            setBackColor();
        }

        private void dvgModelItems_SelectionChanged(object sender, EventArgs e)
        {
            if (dvgModelItems.CurrentRow != null)
            {
                _CurrentrowIndex = dvgModelItems.CurrentRow.Index;

                txtbRoleName.Text = dvgModelItems.Rows[dvgModelItems.CurrentRow.Index].Cells["RoleName"].Value.ToString();
                txtRoleNotation.Text = dvgModelItems.Rows[dvgModelItems.CurrentRow.Index].Cells["RoleNotation"].Value.ToString();

                txtRoleDescription.Text = dvgModelItems.Rows[dvgModelItems.CurrentRow.Index].Cells["RoleDescription"].Value.ToString();

                if (dvgModelItems.Rows[dvgModelItems.CurrentRow.Index].Cells["RoleColor"].Value != null)
                {
                    string colorHighlight = dvgModelItems.Rows[dvgModelItems.CurrentRow.Index].Cells["RoleColor"].Value.ToString();

                    int index = cmbboxClr.FindString(colorHighlight);
                    if (index != -1)
                        cmbboxClr.SelectedIndex = index;
                }
            }
            else
            {
                MessageBox.Show("The selected RAM Model does not have any items", "No Model Items Found", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

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

        private bool Validation()
        {
            string msg = string.Empty;

            if (_RoleName.Length == 0)
            {
                msg = "Please enter a Role Name.";
                MessageBox.Show(msg, "Enter Role Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtbRAMName.Focus();
                return false;
            }

            if (_RoleNotation.Length == 0)
            {
                msg = "Please enter a Role Notation.";
                MessageBox.Show(msg, "Enter Role Notation", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRoleNotation.Focus();
                return false;
            }

            if (_RoleDescription.Length == 0)
            {
                msg = "Please enter a Description.";
                MessageBox.Show(msg, "Enter Description", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtRoleDescription.Focus();
                return false;
            }

            return true;
        }

        private void butReplace_Click(object sender, EventArgs e)
        {
            if (dvgModelItems.Rows.Count == 0)
                return;

            if (dvgModelItems.CurrentCell == null)
            {
                dvgModelItems.Rows[_CurrentrowIndex].Selected = true;
            }

            _RoleName = this.txtbRoleName.Text.Trim();
            _RoleNotation = this.txtRoleNotation.Text.Trim();
            _RoleColor = this.cmbboxClr.Text;
            _RoleDescription = this.txtRoleDescription.Text.Trim();

            if (!Validation())
                return;

            //_ds


            int UID = Convert.ToInt32(dvgModelItems.SelectedRows[0].Cells["UID"].Value.ToString());


            foreach (DataRow row in _ds.Tables[0].Rows)
            {
                if (Convert.ToInt32(row["UID"].ToString()) == UID)
                {
                    row["RoleName"] = _RoleName;
                    row["RoleNotation"] = _RoleNotation;
                    row["RoleColor"] = _RoleColor;
                    row["RoleDescription"] = _RoleDescription;

                    break;
                }
            }

            _ds.AcceptChanges();

            LoadRAMModel();
           
        }

        private void butNew_Click(object sender, EventArgs e)
        {
            _RoleName = this.txtbRoleName.Text.Trim();
            _RoleNotation = this.txtRoleNotation.Text.Trim();
            _RoleColor = this.cmbboxClr.Text;
            _RoleDescription = this.txtRoleDescription.Text.Trim();

            if (!Validation())
                return;

            int UID = 0;
            if (_ds.Tables[0].Rows.Count > 0)
            {
                UID = DataFunctions.FindMaxValue(_ds.Tables[0], "UID");

                UID = UID + 1;
            }


            DataRow row = _ds.Tables[0].NewRow();

            row["UID"] = UID;
            row["RoleName"] = _RoleName;
            row["RoleNotation"] = _RoleNotation;
            row["RoleColor"] = _RoleColor;
            row["RoleDescription"] = _RoleDescription;

            _ds.Tables[0].Rows.Add(row);

            _ds.AcceptChanges();

            LoadRAMModel();

        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dvgModelItems.SelectedRows)
            {
                dvgModelItems.Rows.RemoveAt(item.Index);
            }
            LoadRAMModel();
        }

        private void butOK_Click(object sender, EventArgs e)
        {

            if (txtbRAMName.Text.Trim().Length == 0)
            {
                MessageBox.Show("Please enter a RAM Name.", "RAM Name Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtbRAMName.Focus();
                return;
            }

            if (dvgModelItems.RowCount == 0)
            {
                MessageBox.Show("Please create a role", "RAM Role Required", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                txtbRAMName.Focus();
                return;
            }

            _SelectedModel = txtbRAMName.Text.Trim();

            // ToDo ---- Need to Test more
            //if (Files.IsValidFilename(_SelectedModel))
            //{
            //    MessageBox.Show("Please enter a vaild RAM Name.", "Invaild RAM Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            string  file = string.Concat(_SelectedModel, ".ram");
            string pathFile = Path.Combine(_ModelPath, file);

            if (_isNew)
            {
                if (File.Exists(pathFile))
                {
                    string msg = string.Concat("Are you sure that you want to replace an existing RAM Model '", _SelectedModel, "'?");
                    if (MessageBox.Show(msg, "Confirm Replacing an Existing RAM Model", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.No)
                        return;
                }
            }

            GenericDataManger gDMgr = new GenericDataManger();

            gDMgr.SaveDataXML(_ds, pathFile);

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
