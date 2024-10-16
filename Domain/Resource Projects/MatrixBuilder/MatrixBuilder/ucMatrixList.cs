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
    public partial class ucMatrixList : UserControl
    {
        public ucMatrixList()
        {
            InitializeComponent();
        }

        private string _MatrixPath = string.Empty;
        private string _ListPath = string.Empty;

        private MatrixLists  _MatrixListMgr;
        private DataTable _dtLists;

        private string _SelectedList = string.Empty;
        public string CurrentList
        {
            get { return _SelectedList; }
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
                _TextAllocate = lstbListItems.Text;
                return _TextAllocate; 
            }
        }



        public void LoadData(string matrixPath, string listPath)
        {
            _MatrixPath = matrixPath;
            _ListPath = listPath;

            _MatrixListMgr = new MatrixLists(_MatrixPath, _ListPath);

            _dtLists = _MatrixListMgr.ListDataTable;

            lstbList.Items.Clear();

            if (_dtLists != null)
            {
                foreach (DataRow row in _dtLists.Rows)
                {
                    lstbList.Items.Add(row["Name"].ToString());
                }
            }



           // lstbList.DataSource = _MatrixListMgr.g
        }

        private void lstbListItems_MouseDown(object sender, MouseEventArgs e)
        {
            DragDropEffects dde1 = DoDragDrop("Test", DragDropEffects.All);
        }

        private void lstbList_SelectedIndexChanged(object sender, EventArgs e)
        {
            _SelectedList = lstbList.Text;

            lstbListItems.DataSource = _MatrixListMgr.GetListItems(_SelectedList);

            string name = string.Empty;
            foreach (DataRow row in _dtLists.Rows)
            {
                name = row["Name"].ToString();
                if (name == _SelectedList)
                {
                    _Column = row["Column"].ToString();
                    lblColumn.Text = string.Concat("Column: ", _Column);               
                    return;
                }
            }
        }

        private void lstbListItems_SelectedIndexChanged(object sender, EventArgs e)
        {
            //_TextAllocate = lstbListItems.Text;
        }
    }
}
