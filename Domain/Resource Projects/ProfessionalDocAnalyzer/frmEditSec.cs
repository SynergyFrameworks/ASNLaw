using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using MetroFramework.Forms;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class frmEditSec : MetroFramework.Forms.MetroForm
    {
        public frmEditSec(string UID, string Number, string Caption, string RTFFile, string ParseResultsFile)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            this.AcceptButton = this.butOK;
            this.CancelButton = this.butCancel;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

            _UID = UID;
            _ParseResultsFile = ParseResultsFile;
            _RTFFile = RTFFile;

            txtbNumber.Text = Number;
            txtbCaption.Text = Caption;


            LoadData();
        }

        #region Private Var.s
  

        private string _UID = string.Empty;
        private string _ParseResultsFile = string.Empty;
        private string _RTFFile = string.Empty;


        DataSet _ds;


        #endregion

        #region Private functions 
        private void LoadData()
        {
 
          //  string ParseResultsFile = Path.Combine(_XMLPath, "ParseResults.xml");
            _ds = Files.LoadDatasetFromXml(_ParseResultsFile);

            _ds.Tables[0].DefaultView.Sort = "SortOrder ASC";

          //  string rfpFile = string.Concat(_UID, ".rtf");
            this.richerTextBox1.LoadFile(_RTFFile);
        }


        private void SaveChange()
        {
            richerTextBox1.SaveFile(); // Save textual changes

            foreach (DataRow row in _ds.Tables[0].Rows) // Loop thru rows.
            {
                if (row["UID"].ToString() == _UID)
                {
                    row["Number"] = this.txtbNumber.Text.Trim();
                    row["Caption"] = this.txtbCaption.Text.Trim();

                    GenericDataManger gdManager = new GenericDataManger();

                    gdManager.SaveDataXML(_ds, _ParseResultsFile);

                    return;
                }
            }
        }


        #endregion

        private void butOK_Click(object sender, EventArgs e)
        {
            SaveChange();

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
