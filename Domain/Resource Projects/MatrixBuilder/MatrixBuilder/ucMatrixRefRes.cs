using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using WorkgroupMgr;

namespace MatrixBuilder
{
    public partial class ucMatrixRefRes : UserControl
    {
        public ucMatrixRefRes()
        {
            InitializeComponent();
        }

        private string _MatrixPath = string.Empty;
        private string _RefResPath = string.Empty;

        //private MatrixLists  _MatrixListMgr;
        //private DataTable _dtLists;

        private MatrixRefRes _MatrixRefResMrg;
        private DataTable _dtRefRes;

        private string _SelectedRefRes = string.Empty;
        public string CurrentRefRes
        {
            get { return _SelectedRefRes; }
        }

        //private string _UID = string.Empty;
        //public string UID
        //{
        //    get 
        //    {
        //        _UID = GetUID();
        //        return _UID; 
        //    }
        //}

        private string _Source = string.Empty;
        public string Source
        {
            get { return _Source; }
        }

        private string _Column = string.Empty;
        public string Column
        {
            get { return _Column; }
        }

        private string _TextAllocate = string.Empty;
        public string TextAllocate
        {
            get 
            {
               // _TextAllocate = lstbRefResItems.Text;
                return _TextAllocate; 
            }
        }



        public void LoadData(string matrixPath, string RefResPath)
        {
            _MatrixPath = matrixPath;
            _RefResPath = RefResPath;

            _MatrixRefResMrg = new MatrixRefRes(matrixPath, RefResPath);

            _dtRefRes = _MatrixRefResMrg.RefResDataTable;
            if (_dtRefRes == null)
            {
                MessageBox.Show(_MatrixRefResMrg.ErrorMessage, "Unable to get Reference Resources due to an Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lstbRefRes.Items.Clear();
            foreach (DataRow row in _dtRefRes.Rows)
            {
                lstbRefRes.Items.Add(row["Name"].ToString());

            }

        }

        //private string GetUID()
        //{
        //    if (_dtRefRes == null)
        //    {
        //        return "-1";
        //    }

        //    foreach (DataRow row in _dtRefRes.Rows)
        //    {
        //        if (row[RefResFields.Name].ToString() == _SelectedRefRes)
        //        {
        //            return row[RefResFields.UID].ToString();
        //        }
        //    }

        //    return "-1";
        //}

        private void lstbRefRes_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SelectedRefRes = lstbRefRes.Text;

            string[] refResItems = _MatrixRefResMrg.GetRefResItems(_SelectedRefRes);

            lstbRefResItems.DataSource = refResItems;

            string name = string.Empty;
            foreach (DataRow row in _dtRefRes.Rows)
            {
                name = row["Name"].ToString();
                if (name == _SelectedRefRes)
                {
                    _Column = row["Column"].ToString();
                    lblColumn.Text = string.Concat("Column: ", _Column);
                    return;
                }
            }

        }

        private void lstbRefResItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblRefResContentText.Text = string.Empty;

            _Source = lstbRefResItems.Text;

            _TextAllocate = _MatrixRefResMrg.GetRefResContentText(_SelectedRefRes, _Source);

            lblRefResContentText.Text = _TextAllocate;
        }

        private void lblRefResContentText_TextChanged(object sender, EventArgs e)
        {
            txtbRefResContentText.Text = lblRefResContentText.Text;
        }

        private void txtbRefResContentText_TextChanged(object sender, EventArgs e)
        {
            txtbRefResContentText.Text = lblRefResContentText.Text;
        }

        private void lstbRefResItems_MouseDown(object sender, MouseEventArgs e)
        {
            lblRefResContentText.Text = string.Empty;

            _Source = lstbRefResItems.Text;

            _TextAllocate = _MatrixRefResMrg.GetRefResContentText(_SelectedRefRes, _Source);

            lblRefResContentText.Text = _TextAllocate;

            DragDropEffects dde1 = DoDragDrop("Test", DragDropEffects.All);
        }

        private void lstbRefResItems_Click(object sender, EventArgs e)
        {
   
        }
    }
}
