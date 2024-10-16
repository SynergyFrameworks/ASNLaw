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
    public partial class ucSettings_Home : UserControl
    {
        public ucSettings_Home()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }
    }
}
