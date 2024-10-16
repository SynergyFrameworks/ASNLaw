using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Runtime;
using System.Runtime.InteropServices;

namespace MatrixBuilder
{
    public partial class ucWelcome : UserControl
    {
        public ucWelcome()
        {
            InitializeComponent();
        }

        [DllImport("wininet.dll")]
        private extern static bool InternetGetConnectedState(out int description, int reservedValue);

        public static bool IsInternetAvailable()
        {
            int description;
            return InternetGetConnectedState(out description, 0);
        }

        private void ucWelcome_Load(object sender, EventArgs e)
        {
            // Test
            //Gnostice.Documents.Framework.ActivateLicense("A157-6C29-7007-896C-8479-04BA-C229-2913-64C3-5D34-E485-61F1");

            //this.documentViewer1.LoadDocument(@"I:\Tom\Atebion\Business\Marketing\Functional Matrix 20170603V10.docx"); // Test

           // this.documentViewer1.LoadDocument(@"
            
        }

        private void ucWelcome_VisibleChanged(object sender, EventArgs e)
        {
            if (this.Visible) 
            {
                
                if (IsInternetAvailable())
                {
                    webBStart.Navigate(new Uri("http://www.atebionllc.com/MatrixBuilder.html"));
                    lblNoInternet.Visible = false;
                    webBStart.Visible = true;
                }
                else
                {
                    webBStart.Visible = false;
                    lblNoInternet.Visible = true;
                }

                //string infoFile = string.Concat(Application.StartupPath, @"\Document Analyzer.pdf");
                //if (File.Exists(infoFile))
                //{
                //    this.butInformation.Visible = true;
                //}
                //else
                //{
                //    this.butInformation.Visible = false;
                //}
            }
        }

        private void butInformation_Click(object sender, EventArgs e)
        {

        }

      
    }
}
