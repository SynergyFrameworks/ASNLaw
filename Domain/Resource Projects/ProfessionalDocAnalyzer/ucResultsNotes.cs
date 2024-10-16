using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;


namespace ProfessionalDocAnalyzer
{
    public partial class ucResultsNotes : UserControl
    {
        public ucResultsNotes()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        private string _NotesPath = string.Empty;

        private string _CurrentNoteFile = string.Empty;

        private string _Prefix = string.Empty;
        public string Prefix
        {
            get { return _Prefix; }
            set { _Prefix = value; }
        }

        private string _UID = string.Empty;
        public string UID
        {
            get { return _UID; }
            set 
            {
                _UID = value;
                richerTextBox1.Text = string.Empty;
                if (_NotesPath == string.Empty)
                    return;

                string file = string.Empty;
                if (_Prefix == string.Empty)
                    file = string.Concat(_UID, ".rtf");
                else
                    file = string.Concat(_Prefix, "_", _UID, ".rtf");

                _CurrentNoteFile = Path.Combine(_NotesPath, file);
                if (File.Exists(_CurrentNoteFile))
                {
                    richerTextBox1.LoadFile(_CurrentNoteFile);
                }
                else
                {
                    richerTextBox1.LoadedFile = _CurrentNoteFile;
                }
            }
        }

        public void LoadData(string NotesPath)
        {
            _NotesPath = NotesPath;
        }

        private void richerTextBox1_Leave(object sender, EventArgs e)
        {
            if (_CurrentNoteFile == string.Empty)
                return;

            if (richerTextBox1.Text != string.Empty)      
                richerTextBox1.SaveFile();


        }



        


    }
}
