using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ProfessionalDocAnalyzer
{
    public partial class frmDocumentEdit : MetroFramework.Forms.MetroForm
    {
        public frmDocumentEdit(string selectedDoc)
        {
            InitializeComponent();

            _PathFile = selectedDoc;
            LoadData();
        }

        private string _PathFile = string.Empty;

        private void LoadData()
        {
            this.richerTextBox1.LoadFile(_PathFile);
        }

        private void frmDocumentEdit_Load(object sender, EventArgs e)
        {

        }

        private void butOK_Click(object sender, EventArgs e)
        {
            richerTextBox1.SaveFile();
            Directory.Delete(AppFolders.DocParsedSec_Hold, true);
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
