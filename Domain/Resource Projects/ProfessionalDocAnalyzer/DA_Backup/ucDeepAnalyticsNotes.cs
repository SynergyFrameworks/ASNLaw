using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Atebion.DeepAnalytics;

namespace ProfessionalDocAnalyzer
{
    public partial class ucDeepAnalyticsNotes : UserControl
    {
        public ucDeepAnalyticsNotes()
        {
            InitializeComponent();

            cboNoteNo.Visible = false;
            lblSelectedNote.Visible = false;
        }

        private string _path = string.Empty;
        private string _UID = string.Empty;
        private string _pathFile = string.Empty;

        private Analysis _DeepAnalysis = new Analysis();

        public string Path
        {
            get { return _path; }
            set { _path = value; }

        }

        public string UID
        {
            get { return _UID; }
            set 
            {
                _UID = value;

                if (_UID == string.Empty)
                    return;

                cboNoteNo.Items.Clear();
                richerTextBox1.Text = string.Empty;


                string NoteName = string.Empty;

                if (_UID == string.Empty)
                    return;

                _DeepAnalysis.CurrentDocPath = AppFolders.CurrentDocPath;
                _pathFile = string.Concat(_DeepAnalysis.NotesPath, @"\", _UID, ".rtf");

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

        private void richerTextBox1_Leave(object sender, EventArgs e)
        {
            
            richerTextBox1.SaveFile();
        }
 
    }
}
