using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using WorkgroupMgr;

using unvell.ReoGrid;
using unvell.ReoGrid.Events;

namespace MatrixBuilder
{
    public partial class ucMatrices : UserControl
    {
        public ucMatrices()
        {
            InitializeComponent();
        }

        // Declare delegate for when a project has been selected
        public delegate void ProcessHandler();

        [Category("Action")]
        [Description("Fires when a Matrix is selected")]
        public event ProcessHandler MatrixSelected;

        private string _ProjectRootFolder = string.Empty;
        private string _ProjectName = string.Empty;
        private string _MatrixTempPathTemplates = string.Empty;

        private Matrices _MatricesMgr;

        private string _CurrentMatrix = string.Empty;
        public string CurrentMatrix
        {
            get { return _CurrentMatrix; }
        }

        private string _CurrentMatrixPath = string.Empty;
        public string CurrentMatrixPath
        {
            get { return _CurrentMatrixPath; }
        }

        public void LoadData(string projectRootFolder, string projectName, string matrixTempPathTemplates)
        {
            _ProjectRootFolder = projectRootFolder;
            _ProjectName = projectName;
            _MatrixTempPathTemplates = matrixTempPathTemplates;

          //  webBrowser1.Visible = false;

            _MatricesMgr = new Matrices(_ProjectRootFolder, _MatrixTempPathTemplates, _ProjectName);

            string[] matricesNames = _MatricesMgr.GetMatricesNames();
            if (matricesNames != null)
            {
                lstbMatrices.DataSource = matricesNames;
                if (lstbMatrices.Items.Count > 0)
                {
                    lstbMatrices.ClearSelected();
                   // lstbMatrices.SelectedIndex = 0;
                }
            }
            else
            {
                lstbMatrices.DataSource = null;
                if (_MatricesMgr.ErrorMessage.Length > 0)
                {
                    MessageBox.Show(_MatricesMgr.ErrorMessage, "Unable to Load Matrices", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            

        }

        /// <summary>
        /// Opens window to create a new Matrix for the selected Project. LoadData(0 must be called prior to AddNew();
        /// </summary>
        public void AddNew()
        {
            frmMatrix frm = new frmMatrix(_ProjectRootFolder, _ProjectName, _MatrixTempPathTemplates);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                LoadData(_ProjectRootFolder, _ProjectName, _MatrixTempPathTemplates);
            }

        }


        public bool SelectMatrix(string Matrix)
        {
            if (lstbMatrices.Items.Count == 0)
                return false;

            for (int i = 0; i < lstbMatrices.Items.Count; i++)
            {
                if (Matrix == lstbMatrices.Text)
                {
                    lstbMatrices.SelectedIndex = i;
                    return true;
                }
            }

            return false;
        }

        private void lstbMatrices_SelectedIndexChanged(object sender, EventArgs e)
        {
            _CurrentMatrix = lstbMatrices.Text;

            //webBrowser1.DocumentText = string.Empty;

            //webBrowser1.Visible = false;

            reoGridControl1.Visible = false;

            if (_CurrentMatrix.Length == 0)
                return;

            _CurrentMatrixPath = Path.Combine(_ProjectRootFolder, _ProjectName, "Matrices", _CurrentMatrix);
            if (!Directory.Exists(_CurrentMatrixPath))
            {
                MessageBox.Show(string.Concat("Unable to find the Selected Matrix folder: ", _CurrentMatrixPath), "Unable to Load Matrix HTML File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            if (MatrixSelected != null)
                MatrixSelected();

            //string pathFile = Path.Combine(_CurrentMatrixPath, "Matrix.html");
            //if (!File.Exists(pathFile))
            //{
            //    MessageBox.Show(string.Concat("Unable to find the Matrix HTML file: ", pathFile), "Unable to Load Matrix HTML File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            //    return;
            //}

            string pathFile = Path.Combine(_CurrentMatrixPath, "MatrixTemp.xlsx");
            if (!File.Exists(pathFile))
            {
                MessageBox.Show(string.Concat("Unable to find the Matrix file: ", pathFile), "Unable to Load Matrix File", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            reoGridControl1.Load(pathFile);          

            var worksheet = reoGridControl1.CurrentWorksheet;

            worksheet.ScaleFactor = 0.75f; 

            worksheet.SetSettings(WorksheetSettings.Edit_Readonly, true);

            reoGridControl1.Visible = true;

            //webBrowser1.Visible = true;

            //webBrowser1.DocumentText = Files.ReadFile(pathFile);
            
        }

        private void picMatrixIcon_Click(object sender, EventArgs e)
        {
            if (_MatricesMgr == null)
                return;

            if (_MatricesMgr.MatricesPath.Length == 0)
                return;

            if (!Directory.Exists(_MatricesMgr.MatricesPath))
                return;

            System.Diagnostics.Process.Start("explorer.exe", _MatricesMgr.MatricesPath);
        }
    }
}
