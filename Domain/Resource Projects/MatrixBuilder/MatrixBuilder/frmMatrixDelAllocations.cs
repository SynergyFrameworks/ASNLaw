using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatrixBuilder
{
    public partial class frmMatrixDelAllocations : MetroFramework.Forms.MetroForm
    {
        public frmMatrixDelAllocations(DataTable dt, string cell)
        {
            InitializeComponent();

            _dtCellAllocations = dt;
            _Cell = cell;
            
        }

        private DataTable _dtCellAllocations;
        private string _Cell = string.Empty;

        private DataTable _dtRemoveAllocations;
        public DataTable RemoveAllocations
        {
            get { return _dtRemoveAllocations; }
        }

        private void LoadData()
        {
            dgvDocTypes.DataSource = _dtCellAllocations;

            dgvDocTypes.Columns[0].Width = 30;
            dgvDocTypes.Columns[1].Visible = false;
            dgvDocTypes.Columns[2].Width = 60;
            dgvDocTypes.Columns[2].ReadOnly = true;
            dgvDocTypes.Columns[3].Width = 80;
            dgvDocTypes.Columns[3].ReadOnly = true;
            dgvDocTypes.Columns[4].DefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvDocTypes.Columns[4].ReadOnly = true;
            dgvDocTypes.Columns[5].Visible = false;
            dgvDocTypes.Columns[6].Visible = false;

            dgvDocTypes.AllowUserToAddRows = false; // Remove blank last row

          //  dgvDocTypes.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            lblDefinition.Text = string.Concat("Below are allocations for the Matrix Cell: '", _Cell, "' . Check the allocations you want to remove");
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void frmMatrixDelAllocations_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void dgvDocTypes_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDocTypes.SelectedRows.Count == 0)
                return;

            lblAllocatedBy.Text = string.Concat("Allocated By: ", dgvDocTypes.Rows[dgvDocTypes.SelectedCells[0].RowIndex].Cells[5].Value.ToString());
            DateTime xdate = (DateTime)dgvDocTypes.Rows[dgvDocTypes.SelectedCells[0].RowIndex].Cells[6].Value;
            lblAllocatedDate.Text = string.Concat("Allocated Date: ", xdate.ToString("F"));
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            _dtRemoveAllocations = (DataTable)dgvDocTypes.DataSource;

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();
        }

    }
}
