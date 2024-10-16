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
    public partial class frmInputDialog : MetroFramework.Forms.MetroForm
    {
        public frmInputDialog(string Caption, string Instructions)
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();

            this.Text = Caption;
            this.lblInstructions.Text = Instructions;
        }

        public void LoadData(string InputValue)
        {
            txtbInput.Text = InputValue;
        }

        public string Results
        {
            get { return txtbInput.Text; }
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
