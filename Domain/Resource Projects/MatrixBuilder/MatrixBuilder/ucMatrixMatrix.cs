using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatrixBuilder
{
    public partial class ucMatrixMatrix : UserControl
    {
        public ucMatrixMatrix()
        {
            InitializeComponent();
        }

        private string _MatrixPath = string.Empty;

        public void LoadData(string matrixPath)
        {
            _MatrixPath = matrixPath;

        }

        private void lblCaption_Click(object sender, EventArgs e)
        {

        }
    }
}
