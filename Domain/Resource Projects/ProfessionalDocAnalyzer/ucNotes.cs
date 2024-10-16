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
using System.Diagnostics;

namespace ProfessionalDocAnalyzer
{
    public partial class ucNotes : UserControl
    {
        public ucNotes()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private string _path = string.Empty;
        private string _UID = string.Empty;
        private string _pathFile = string.Empty;
        private string _PostFix = string.Empty;

        public void LoadData(string NotePath, string PostFix)
        {
            _path = NotePath;
            _PostFix = PostFix;
        }

        public string UID
        {
            get { return _UID; }
            set
            {
                _UID = value;

                if (_UID == string.Empty)
                    return;

                
                richerTextBox1.Text = string.Empty;


                string NoteName = string.Empty;

                if (_UID == string.Empty)
                    return;

                string file = string.Concat(_UID, _PostFix, ".rtf");
                _pathFile = Path.Combine(_path, file);

                if (!File.Exists(_pathFile))
                {
                    this.richerTextBox1.LoadedFile = _pathFile; // File doesn't exists yet, but if the saved button is pressed this will be the file
                }
                else
                {
                    richerTextBox1.LoadFile(_pathFile);
                }

            }
        }

 

        private void richerTextBox1_Leave_1(object sender, EventArgs e)
        {
            richerTextBox1.SaveFile();
        }
    }
}
