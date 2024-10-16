using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using WorkgroupMgr;

namespace MatrixBuilder
{
    public partial class frmDocTypes : MetroFramework.Forms.MetroForm
    {
        public frmDocTypes(WorkgroupMgr.DocTypesMgr DoctypeMgr)
        {
            InitializeComponent();

            _DoctypeMgr = DoctypeMgr;

            LoadData();
        }

        public frmDocTypes(string DocTypeName, WorkgroupMgr.DocTypesMgr DoctypeMgr)
        {
            InitializeComponent();

            _DocTypeName = DocTypeName;
            _DoctypeMgr = DoctypeMgr;

            LoadData();
        }

        private string _DocTypeName = string.Empty;
        private WorkgroupMgr.DocTypesMgr _DoctypeMgr;
        private DataTable _dt;

        private int _DocTypeCount = 0;
        private int _DupsCount = 0;
        List<string> _Dups = new List<string>();
        private int _UID = 0;

        private string BATCH_LOAD_MSG = string.Concat("Batch Load Syntax (each Document Type must be on an individual line): ", Environment.NewLine,
                                                "Item1, Description1", Environment.NewLine,
                                                "Item2, Description2", Environment.NewLine,
                                                "Item3, Description3", Environment.NewLine,
                                                "Item4, Description4"
                                                );

        private bool LoadData()
        {
            lblBatchInstructions.Text = BATCH_LOAD_MSG;

            if (_DocTypeName.Length > 0)
            {
                txtbDocTypeName.Text = _DocTypeName;
                txtbDescription.Text = _DoctypeMgr.GetDescription(_DocTypeName);

                _dt = _DoctypeMgr.GetDocTypeItems(_DocTypeName);
            }
            else
            {
                _dt = _DoctypeMgr.GetEmptyDataTable();               
            }

            dvgDocTypes.DataSource = _dt;
            dvgDocTypes.Columns[0].Visible = false;

            return true;
        }


        private void frmDocTypes_Load(object sender, EventArgs e)
        {


        }

        private void butAddBatch_Click(object sender, EventArgs e)
        {

            this.txtBatch.Text = WorkgroupMgr.DataFunctions.ReplaceSingleQuote(txtBatch.Text);

            AddBatch();
        }

        private int AddBatch()
        {
            if (this.txtBatch.Text.Trim().Length == 0)
                return 0;

            string input = txtBatch.Text.Trim();

            int uid;

            if (_dt.Rows.Count > 0)
                uid = WorkgroupMgr.DataFunctions.FindMaxValue(_dt, WorkgroupMgr.DocTypesFields.UID);

            Cursor.Current = Cursors.WaitCursor; // Waiting 

            string line = string.Empty;
            string[] ItemDescrip;
            string[] spaceDelim;


            using (StringReader reader = new StringReader(input))
            {
                while ((line = reader.ReadLine()) != null)
                {
                    if (line.Length > 0)
                    {
                        int delimitersCount = line.Split(',').Length - 1;
                        if (delimitersCount > 1)
                        {
                            spaceDelim = line.Split(',');
                            if (spaceDelim.Length > 0)
                            {
                                foreach (string ad in spaceDelim)
                                {
                                    if (ad.IndexOf('|') > 0)
                                    {
                                        ItemDescrip = ad.Split(',');
                                        AddBatchDocTypes1(ItemDescrip[0].Trim(), ItemDescrip[1].Trim());
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (line.IndexOf(',') > 0)
                            {
                                ItemDescrip = line.Split(',');
                                AddBatchDocTypes1(ItemDescrip[0].Trim(), ItemDescrip[1].Trim());
                            }
                        }
                    }
                }
            }

            dvgDocTypes.DataSource = _dt;

            Cursor.Current = Cursors.Default; // Done 

            string msg = string.Concat("You added ", _DocTypeCount.ToString(), " Document Types to the library.", System.Environment.NewLine, System.Environment.NewLine, "Document Type Duplicates Found = ", _DupsCount.ToString());
            MessageBox.Show(msg, "Added New Document Types", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Added 3.15.2017
            if (_DupsCount > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string dup in _Dups)
                {
                    sb.AppendLine(dup);
                }
                msg = sb.ToString();
                MessageBox.Show(msg, "Document Types Duplicates Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return _DocTypeCount;

        }


        private void AddBatchDocTypes1(string DocTypeName, string Description)
        {
 
            // Search for duplicates
            DataRow[] rows = DataFunctions.GetDupsRows(_dt, DocTypeName.Trim(), WorkgroupMgr.DocTypesFields.Item);
            if (rows != null)
            {
                if (rows.Length > 0)
                {
                    // Added Definition concatenation linked via " or " -- 2/13/2017
                //    rows[0][AcroParser.DefinedAcronymsFieldConst.Definition] = string.Concat(rows[0][AcroParser.DefinedAcronymsFieldConst.Definition].ToString(), " or ", Description.Trim());

                    _Dups.Add(DocTypeName.Trim());
                    _DupsCount++;
                    return;
                }
            }

            DataRow row = _dt.NewRow();
            if (DocTypeName.Trim().Length < Description.Trim().Length)
            {
                row[WorkgroupMgr.DocTypesFields.Item] = DocTypeName.Trim();
                row[WorkgroupMgr.DocTypesFields.Description] = Description.Trim();
            }
            else
            {
                row[WorkgroupMgr.DocTypesFields.Item] = Description.Trim();
                row[WorkgroupMgr.DocTypesFields.Description] = DocTypeName.Trim();
            }

            _UID++;
            row[WorkgroupMgr.DocTypesFields.UID] = _UID;

            _dt.Rows.Add(row);

            _DocTypeCount++;
        }

        private bool DocTypesExists(string item, DataTable dt)
        {
            if (dt.Rows.Count == 0)
                return false;

            string selectStatment = string.Concat(WorkgroupMgr.DocTypesFields.Item, " = '", item, "'");
            DataRow[] foundItem = dt.Select(selectStatment);
            if (foundItem.Length != 0)
            {
                return true;
            }

            return false;
        }

        private void butNew_Click(object sender, EventArgs e)
        {
            string docType = txtAcron.Text.Trim();
            string descrip = txtDefinition.Text.Trim();

            string msg = string.Empty;

            if (docType.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter a Document Type before attempting to insert into your listing.", "Document Type Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAcron.Focus();
                return;

            }
            
            DataSet dsDocTypes = DataFunctions.CreateDataSetFromDataGridView(dvgDocTypes);
            _dt = dsDocTypes.Tables[0];

            if (DocTypesExists(docType, dsDocTypes.Tables[0]))
            {
                msg = string.Concat("The Document Type ", docType, " already exists in your Library.");
                MessageBox.Show(msg, "Document Type Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _UID = DataFunctions.FindMaxValue(_dt, WorkgroupMgr.DocTypesFields.UID);

            _UID++;

            DataRow row = _dt.NewRow();

            row[WorkgroupMgr.DocTypesFields.UID] = _UID;
            row[WorkgroupMgr.DocTypesFields.Item] = docType;
            row[WorkgroupMgr.DocTypesFields.Description] = descrip;

            _dt.Rows.Add(row);


            dvgDocTypes.DataSource = _dt;
        }

        private void butReplace_Click(object sender, EventArgs e)
        {
            string docType = txtAcron.Text.Trim();
            string descrip = txtDefinition.Text.Trim();

            string msg = string.Empty;

            if (docType.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter a Document Type before attempting to insert into your Library.", "Document Type Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAcron.Focus();
                return;
            }

            int rowID = dvgDocTypes.CurrentCell.RowIndex;

            if (rowID < 0)
                return;

            dvgDocTypes.Rows[rowID].Cells[WorkgroupMgr.DocTypesFields.Item].Value = docType;
            dvgDocTypes.Rows[rowID].Cells[WorkgroupMgr.DocTypesFields.Description].Value = descrip;

            DataSet ds = DataFunctions.CreateDataSetFromDataGridView(dvgDocTypes);
            _dt = ds.Tables[0];
        }

        private void butDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dvgDocTypes.SelectedRows)
            {
                dvgDocTypes.Rows.RemoveAt(item.Index);
            }

            DataSet ds = DataFunctions.CreateDataSetFromDataGridView(dvgDocTypes);
            _dt = ds.Tables[0];
        }

        private void butOk_Click(object sender, EventArgs e)
        {
            string DocTypeName = txtbDocTypeName.Text.Trim();
            DataSet ds = DataFunctions.CreateDataSetFromDataGridView(dvgDocTypes);
            _dt = ds.Tables[0];

            if (!_DoctypeMgr.CreateDocType(DocTypeName, txtbDescription.Text, _dt))
            {
                if (_DoctypeMgr.ErrorMessage.Length > 0)
                {
                    string msg = string.Concat("An error has occurred while attempting to save Document Type changes.", Environment.NewLine , Environment.NewLine, _DoctypeMgr.ErrorMessage.Length);

                    MessageBox.Show(msg, "An Error Occurred", MessageBoxButtons.OK, MessageBoxIcon.Error);

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
