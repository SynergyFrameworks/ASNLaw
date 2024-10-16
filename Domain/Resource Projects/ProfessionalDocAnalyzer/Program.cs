using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProfessionalDocAnalyzer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           
            //Gnostice.Documents.Framework.ActivateLicense("A157-6C29-7007-896C-8479-04BA-C229-2913-64C3-5D34-E485-61F1");
            Application.Run(new frmMain());
        }
    }
}
