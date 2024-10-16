using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;


namespace ProfessionalDocAnalyzer
{
    public partial class ucResults_Home : UserControl
    {
        public ucResults_Home()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private void ucResults_Home_Load(object sender, EventArgs e)
        {
            
            picAnalysisResults.Image = imageList1.Images[8];
            picDocuments.Image = imageList1.Images[4];
            picExcel.Image = imageList1.Images[5];
            picFolder.Image = imageList1.Images[1];
            picHTML.Image = imageList1.Images[9];
            picDocument.Image = imageList1.Images[3];
            picAnalysis.Image = imageList1.Images[2];
            picWord.Image = imageList1.Images[6];
            picHTML.Image = imageList1.Images[9];
        }

  

   
    }
}
