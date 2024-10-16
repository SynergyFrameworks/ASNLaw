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
using Atebion.Common;
using AcroParser;


namespace ProfessionalDocAnalyzer
{
    public partial class frmDefLib : MetroFramework.Forms.MetroForm
    {
        public frmDefLib()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
            this.CancelButton = this.butCancel;
            //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            
        }

        public frmDefLib(string DefName)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
            this.CancelButton = this.butCancel;
            //    this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            _DefName = DefName;
        }

        private subPanels _subPanel = subPanels.BatchLoad;
        private enum subPanels
        {
            BatchLoad = 0,
            Maintain = 1,
        }



        //     private string _LibPath = string.Empty;
        private string _DefName = string.Empty;
        private DataSet _dsDefLib;
        private DataSet _dsDefLibEdit;
        private string _SelectedFile = string.Empty;
        private int _UID = 0;
        private int _CurrentrowIndex = 0;

        private AcroParser.DefinitionsLibs defLib;

        private int AcroCount = 0;
        private int DupsCount = 0;
        List<string> _Dups = new List<string>();

        private string _defualtMessage = string.Concat("Batch Load Syntax (each acronym must be on an individual line): ", Environment.NewLine,
                                                "Acronym1, Definition1", Environment.NewLine,
                                                "Acronym2, Definition2", Environment.NewLine,
                                                "Acronym3, Definition3", Environment.NewLine,
                                                "Acronym4, Definition4"
                                                );

        private string _maintainMessage = string.Concat("New: Enter New Acronym with Definition and click the Add button.", Environment.NewLine,
                                                      "Replace: Select an Acronym and either enter a new Acronym and/or Change the Definition.", Environment.NewLine,
                                                      "Delete: To Remove, select an Acronym and click the Delete button.");

        public void LoadData()
        {

            defLib = new DefinitionsLibs(AppFolders.AppDataPathToolsAcroSeekerDefLib);


            if (_DefName != string.Empty)
            {
                _dsDefLib = defLib.GetDataset(_DefName);
                this.txtbFileName.Text = _DefName;
            }
            else
            {
                _dsDefLib = defLib.CreateEmptyDS();
            }

            dvgAcronyms.DataSource = _dsDefLib.Tables[0];

            dvgAcronyms.Columns[0].Visible = false;

            dvgAcronyms.Sort(this.dvgAcronyms.Columns[AcroParser.DefinedAcronymsFieldConst.Acronym], ListSortDirection.Ascending); // Added 04.10.2016 for Beta 2.1

            lblMessage.Text = _defualtMessage;

            _subPanel = subPanels.BatchLoad;
            ModeAdjust();

            this.Refresh();



        }

        private void AdJustColumns()
        {
            try
            {
                if (_dsDefLib == null)
                {
                    //_dsDefLib = defLib.CreateEmptyDS();	
                    _UID = 0;
                    DataTable dt = new DataTable();
                    dt.Clear();
                    dt.Columns.Add("UID");
                    dt.Columns.Add(AcroParser.DefinedAcronymsFieldConst.Acronym);
                    dvgAcronyms.DataSource = dt;
                }
                dvgAcronyms.Columns["UID"].Visible = false;
                dvgAcronyms.Columns[AcroParser.DefinedAcronymsFieldConst.Acronym].Width = 50;
            }
            catch { }
        }


        private void butImportFile_Click(object sender, EventArgs e)
        {
            this.openFileDialog1.Filter = "Rich Text Format|*.rtf|MS Word Files (*.doc)|*.doc|MS Word Files  (*.docx)|*.docx|Text Files (*.txt)|*.txt|Portable Document Format|*.pdf|All Files|*.*";
            this.openFileDialog1.FilterIndex = 6;
            openFileDialog1.FileName = string.Empty;

            DialogResult result = openFileDialog1.ShowDialog(); // Show the dialog.
            if (result == DialogResult.OK)
            {
                Cursor.Current = Cursors.WaitCursor; // Waiting 

                txtBatchAcronyms.Text = string.Empty;

                _SelectedFile = openFileDialog1.FileName;


                Files.ReadFileIntoControl(_SelectedFile, txtBatchAcronyms);

                Cursor.Current = Cursors.Default;
            }
        }

        //private void AddNewAcronym(string Acronym, string Definition)
        //{


        //}

        private void AddBatchAcronyms1(string Acronym, string Definition)
        {
            // Test code
            //if (Acronym == "ZIP")
            //{
            //    MessageBox.Show("ZIP", "Test");
            //}



            // Search for Acronym duplicates

            DataRow[] rows = Atebion.Common.DataFunctions.GetDupsRows(_dsDefLib.Tables[0], Acronym.Trim(), AcroParser.DefinedAcronymsFieldConst.Acronym);
            if (rows != null)
            {
                if (rows.Length > 0)
                {
                    // Added Definition concatenation linked via " or " -- 2/13/2017
                    rows[0][AcroParser.DefinedAcronymsFieldConst.Definition] = string.Concat(rows[0][AcroParser.DefinedAcronymsFieldConst.Definition].ToString(), " or ", Definition.Trim());

                    _Dups.Add(Acronym.Trim());
                    DupsCount++;
                    return;
                }
            }

            DataRow row = _dsDefLib.Tables[0].NewRow();
            if (Acronym.Trim().Length < Definition.Trim().Length)
            {
                row[AcroParser.DefinedAcronymsFieldConst.Acronym] = Acronym.Trim();
                row[AcroParser.DefinedAcronymsFieldConst.Definition] = Definition.Trim();
            }
            else
            {
                row[AcroParser.DefinedAcronymsFieldConst.Acronym] = Definition.Trim();
                row[AcroParser.DefinedAcronymsFieldConst.Definition] = Acronym.Trim();
            }

            _UID++;
            row[AcroParser.DefinedAcronymsFieldConst.UID] = _UID;

            _dsDefLib.Tables[0].Rows.Add(row);

            AcroCount++;
        }

        private int AddBatchAcronyms()
        {
            AcroCount = 0;
            DupsCount = 0;
            _Dups.Clear();

            if (defLib == null)
            {
                defLib = new DefinitionsLibs(AppFolders.AppDataPathToolsAcroSeekerDefLib);
            }

            if (_dsDefLib == null)
            {
                _dsDefLib = defLib.CreateEmptyDS();
                _UID = 0;
            }
            else
            {
                Atebion.Common.NonStaticFunctions dataFunction = new Atebion.Common.NonStaticFunctions();


                _UID = dataFunction.FindMaxValue(_dsDefLib.Tables[0], "UID");
            }

            string input = txtBatchAcronyms.Text;
            if (input.Trim() == string.Empty)
            {

                return AcroCount;
            }

            Cursor.Current = Cursors.WaitCursor; // Waiting 

            string line = string.Empty;
            string[] AcroDef;
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
                                        AcroDef = ad.Split(',');
                                        AddBatchAcronyms1(AcroDef[0].Trim(), AcroDef[1].Trim());
                                    }
                                }

                            }
                        }
                        else
                        {
                            if (line.IndexOf(',') > 0)
                            {
                                AcroDef = line.Split(',');
                                AddBatchAcronyms1(AcroDef[0].Trim(), AcroDef[1].Trim());
                            }
                        }
                    }
                }
            }

            dvgAcronyms.DataSource = _dsDefLib.Tables[0];

            AdJustColumns();

            Cursor.Current = Cursors.Default; // Done 

            string msg = string.Concat("You added ", AcroCount.ToString(), " acronyms to the Dictionary.", System.Environment.NewLine, System.Environment.NewLine, "Acronym Duplicates Found = ", DupsCount.ToString(), System.Environment.NewLine, System.Environment.NewLine, "Duplicate Acronyms' Definitionhave been concatenated via ' or '.");
            MessageBox.Show(msg, "Added New Acronyms", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Added 3.15.2017
            if (DupsCount > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (string dup in _Dups)
                {
                    sb.AppendLine(dup);
                }
                msg = sb.ToString();
                MessageBox.Show(msg, "Acronym Duplicates Found", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return AcroCount;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            string pathFile = string.Empty;

            if (txtbFileName.Text.Trim() == string.Empty)
            {
                string msg = string.Concat("Please enter the name of your new acronym dictionary name.");
                MessageBox.Show(msg, "Acronym Dictionary Name Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtbFileName.Focus();
                return;
            }

            pathFile = string.Concat(AppFolders.AppDataPathToolsAcroSeekerDefLib, @"\", txtbFileName.Text.Trim(), ".xml");

            if (!File.Exists(pathFile))
            {
                Files.SetFileName2Obsolete(pathFile);
            }

            if (defLib == null)
                defLib = new DefinitionsLibs(AppFolders.AppDataPathToolsAcroSeekerDefLib);

            _dsDefLib = defLib.CreateEmptyDS();

            DataTable dt = (DataTable)(dvgAcronyms.DataSource);

            _dsDefLibEdit = defLib.CreateEmptyDS();

            foreach (DataRow drtableNew in dt.Rows)
            {

                _dsDefLib.Tables[0].ImportRow(drtableNew);
                _dsDefLibEdit.Tables[0].ImportRow(drtableNew);
            }

            defLib.SaveDataset(_dsDefLibEdit, pathFile);

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();


        }

        private void FormatDataGridView()
        {
            this.dvgAcronyms.BackgroundColor = Color.Black;
            // this.dvgAcronyms.ForeColor = Color.White;
        }

        private void butAddBatchAcronyms_Click(object sender, EventArgs e)
        {
            this.txtBatchAcronyms.Text = Atebion.Common.DataFunctions.ReplaceSingleQuote(txtBatchAcronyms.Text); // Added 04.09.2016
            AddBatchAcronyms();
            FormatDataGridView();
        }

        private bool AcroExists(string acro, DataSet dsDefLib)
        {
            string selectStatment = string.Concat(AcroParser.DefinedAcronymsFieldConst.Acronym, " = '", acro, "'");
            DataRow[] foundArco = dsDefLib.Tables[0].Select(selectStatment);
            if (foundArco.Length != 0)
            {
                return true;
            }

            return false;
        }

        private void butNew_Click(object sender, EventArgs e)
        {
            string acro = txtAcron.Text.Trim();
            string def = txtDefinition.Text.Trim();

            string msg = string.Empty;

            if (acro.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter an acronym before attempting to insert into your Dictionary.", "Acronym Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAcron.Focus();
                return;
            }

            _dsDefLib = Atebion.Common.DataFunctions.CreateDataSetFromDataGridView(dvgAcronyms);

            if (AcroExists(acro, _dsDefLib))
            {
                msg = string.Concat("The acronym ", acro, " already exists in your dictionary.");
                MessageBox.Show(msg, "Acronym Already Exists", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            _UID = Atebion.Common.DataFunctions.FindMaxValue(_dsDefLib.Tables[0], AcroParser.DefinedAcronymsFieldConst.UID);

            _UID = _UID + 1;

            DataRow row = _dsDefLib.Tables[0].NewRow();

            row[AcroParser.DefinedAcronymsFieldConst.UID] = _UID.ToString();
            row[AcroParser.DefinedAcronymsFieldConst.Acronym] = acro;
            row[AcroParser.DefinedAcronymsFieldConst.Definition] = def;

            _dsDefLib.Tables[0].Rows.Add(row);

            dvgAcronyms.DataSource = _dsDefLib.Tables[0];

            //int rowID = dvgAcronyms.Rows.Add();
            //dvgAcronyms.Rows[rowID].Cells[AcroParser.DefinedAcronymsFieldConst.UID].Value = UID.ToString();
            //dvgAcronyms.Rows[rowID].Cells[AcroParser.DefinedAcronymsFieldConst.Acronym].Value = acro;
            //dvgAcronyms.Rows[rowID].Cells[AcroParser.DefinedAcronymsFieldConst.Definition].Value = def;

        }

        private void butReplace_Click(object sender, EventArgs e)
        {
            string acro = txtAcron.Text.Trim();
            string def = txtDefinition.Text.Trim();

            string msg = string.Empty;

            if (acro.Trim() == string.Empty)
            {
                MessageBox.Show("Please enter an acronym before attempting to insert into your Dictionary.", "Acronym Required", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtAcron.Focus();
                return;
            }


            if (dvgAcronyms.Rows.Count == 0)
                return;

            if (dvgAcronyms.CurrentCell == null)
            {
                dvgAcronyms.Rows[_CurrentrowIndex].Selected = true;
            }
            int UID = Convert.ToInt32(dvgAcronyms.SelectedRows[0].Cells["UID"].Value.ToString());

            foreach (DataRow row in _dsDefLib.Tables[0].Rows)
            {
                if (Convert.ToInt32(row["UID"].ToString()) == UID)
                {
                    row[AcroParser.DefinedAcronymsFieldConst.Acronym] = acro;
                    row[AcroParser.DefinedAcronymsFieldConst.Definition] = def;


                    break;
                }
            }
            _dsDefLib.AcceptChanges();

            LoadAcroDic();
        }
        private bool LoadAcroDic()
        {

            if (_dsDefLib.Tables[0].Rows.Count == 0)
            {
                MessageBox.Show("The selected dictionary does not have any items", "No data", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }


            dvgAcronyms.DataSource = _dsDefLib.Tables[0];

            return true;

        }

        private void butDelete_Click(object sender, EventArgs e)
        {

            foreach (DataGridViewRow item in this.dvgAcronyms.SelectedRows)
            {
                dvgAcronyms.Rows.RemoveAt(item.Index);
            }
        }

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void txtBatchAcronyms_Enter(object sender, EventArgs e)
        {
            this.lblMessage.Text = _defualtMessage;
        }

        private void txtbMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }

        private void lblMessage_TextChanged(object sender, EventArgs e)
        {
            txtbMessage.Text = lblMessage.Text;
        }


        private void metroTabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_subPanel == subPanels.BatchLoad)
            {
                this.lblMessage.Text = _defualtMessage;
            }
            else
            {
                this.lblMessage.Text = _maintainMessage;
            }
        }

        private void frmDefLib_Load(object sender, EventArgs e)
        {

        }

        private void FindAcronym()
        {
            String searchValue = this.txtbFind.Text.Trim().ToUpper();

            if (searchValue == string.Empty)
                return;

            string selX = "Acronym"; // cboFind.SelectedItem.ToString();


            int rowIndex = -1;
            foreach (DataGridViewRow row in dvgAcronyms.Rows)
            {
                if (row.Cells[selX].Value.ToString().Equals(searchValue))
                {
                    rowIndex = row.Index;
                    dvgAcronyms.Rows[rowIndex].Selected = true;
                    dvgAcronyms.FirstDisplayedScrollingRowIndex = dvgAcronyms.SelectedRows[0].Index;
                    break;
                }
            }
        }

        private void txtbFind_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                FindAcronym();
            }
        }

        private void butFind_Click(object sender, EventArgs e)
        {
            FindAcronym();
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

        private void frmDefLib_Paint(object sender, PaintEventArgs e)
        {
            AdJustColumns();
        }

        private void dvgAcronyms_SelectionChanged(object sender, EventArgs e)
        {
            SelectionChanged();
        }

        private void SelectionChanged()
        {

            if (dvgAcronyms.Rows.Count == 0)
                return;

            if (dvgAcronyms.SelectedRows.Count == 0)
            {
                dvgAcronyms.Rows[0].Selected = true;
            }

            DataGridViewRow row = dvgAcronyms.SelectedRows[0];
            if (row == null)
            {

                dvgAcronyms.Rows[_CurrentrowIndex].Selected = true;
                row = dvgAcronyms.SelectedRows[0];
            }

            _CurrentrowIndex = dvgAcronyms.SelectedRows[0].Index;

            _UID = Convert.ToInt32(row.Cells[AcroParser.DefinedAcronymsFieldConst.UID].Value.ToString());


            ShowCurrentDicDetails();

           
        }

        private void ShowCurrentDicDetails()
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 


            DataGridViewRow row = dvgAcronyms.Rows[_CurrentrowIndex];

            txtAcron.Text = string.Empty;
            txtDefinition.Text = string.Empty;

            if (row == null) // Check, sometimes data has not been loaded yet
            {
                Cursor.Current = Cursors.Default; // Back to normal
                return;
            }

            string name = row.Cells[AcroParser.DefinedAcronymsFieldConst.Acronym].Value.ToString();
            string definition = row.Cells[AcroParser.DefinedAcronymsFieldConst.Definition].Value.ToString();

            txtAcron.Text = name;
            txtDefinition.Text = definition;
   

            Cursor.Current = Cursors.Default;
        }

        private void frmDefLib_Activated(object sender, EventArgs e)
        {
            ModeAdjust();
            AdJustColumns();
        }

 

    }
}
