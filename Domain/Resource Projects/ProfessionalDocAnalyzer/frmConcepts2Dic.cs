using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace ProfessionalDocAnalyzer
{
    public partial class frmConcepts2Dic : MetroFramework.Forms.MetroForm
    {
        public frmConcepts2Dic()
        {
            StackTrace st = new StackTrace(false);
            InitializeComponent();
        }

        private Atebion.ConceptAnalyzer.Analysis _CAMgr;
        private string _ProjectName = string.Empty;
        private string _AnalysisName = string.Empty;
        private string _DocumentName = string.Empty;

        public void LoadData(Atebion.ConceptAnalyzer.Analysis CAMgr, string ProjectName, string AnalysisName, string DocumentName)
        {
            _CAMgr = CAMgr;
            _ProjectName = ProjectName;
            _AnalysisName = AnalysisName;
            _DocumentName = DocumentName;
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor; // Waiting 

            Atebion_Dictionary.Dictionary dic = new Atebion_Dictionary.Dictionary(AppFolders.DictionariesPath);

            if (dic.CheckDictionaryExists(txtbDicName.Text))
            {
                Cursor.Current = Cursors.Default; // Back to normal
                string msg = string.Concat("Dictionary ", txtbDicName.Text, " already exists.");
                MessageBox.Show(msg, "Please enter another Dictionary Name", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            List<string> concepts = new List<string>();

            DataSet ds = _CAMgr.Get_Document_Concept_Summary(_ProjectName, _AnalysisName, _DocumentName);
            if (ds == null)
            {
                Cursor.Current = Cursors.Default; // Back to normal
                string msg = string.Concat("Unable to Open the Concept Summary data file.");
                MessageBox.Show(msg, "Unable to Create the Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            foreach (DataRow row in ds.Tables[0].Rows)
            {
                concepts.Add(row["Concept"].ToString());
            }


            int qty = dic.ImportConcepts(concepts.ToArray(), txtbDicName.Text, "YellowGreen");
            if (qty == -1)
            {
                Cursor.Current = Cursors.Default; // Back to normal
                MessageBox.Show(dic.ErrorMessage, "Unable to Create the Dictionary", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Cursor.Current = Cursors.Default; // Back to normal


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
