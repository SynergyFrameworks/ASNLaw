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
    public partial class frmReadabilityRef : MetroFramework.Forms.MetroForm
    {
        public frmReadabilityRef()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        public void LoadData()
        {
           string FleschReadingEase_Msg2 = string.Concat(@"The Flesch Reading Ease is a readability test designed to indicate comprehension difficulty when reading a passage of contemporary academic English.",
            Environment.NewLine, Environment.NewLine, "Higher scores indicate content that is easier to read, while lower values are more difficult to read.");
           
            string FleschReadingEase_Msg = string.Concat(
                                @"0-29", "\t", "College Graduate", "\t", "\t", "Very Confusing", // Red
            Environment.NewLine, "30-49", "\t", "College", "\t", "\t", "\t", "Difficult",     // Light Red
            Environment.NewLine, "50-59", "\t", "10th to 12th Grade", "\t", "\t",  "Fairly Difficult", // Yellow
            Environment.NewLine, "60-69", "\t", "8th & 9th Grade", "\t", "\t",  "Standard", // Light Green
            Environment.NewLine, "70-79", "\t", "7th Grade", "\t", "\t" ,  "Fairly Easy", // Lighter Green
            Environment.NewLine, "80-89", "\t", "6th Grade", "\t", "\t",  "Easy", // Green
            Environment.NewLine, "90-100", "\t", "5th Grade", "\t", "\t",  "Very Easy"); // Dark Green

            lblMsg.Text = string.Concat(FleschReadingEase_Msg2, Environment.NewLine, FleschReadingEase_Msg);
        }

        
        private void butCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void lblMsg_TextChanged(object sender, EventArgs e)
        {
            txtbMsg.Text = lblMsg.Text;
        }

        private void txtbMsg_TextChanged(object sender, EventArgs e)
        {
            txtbMsg.Text = lblMsg.Text;
        }
    }
}
