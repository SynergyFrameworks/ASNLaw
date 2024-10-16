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
    public partial class frmSynonymNew : MetroFramework.Forms.MetroForm
    {
        public frmSynonymNew()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        public string Synonym
        {
            get { return txtNewSynonym.Text.Trim(); }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
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
