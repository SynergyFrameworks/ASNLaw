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

using Atebion.QC;

namespace ProfessionalDocAnalyzer
{
    public partial class ucQCSettings : UserControl
    {
        public ucQCSettings()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        Atebion.QC.Analysis _Analysis;

        string _QCSettingsPath = string.Empty;

        // Display Fields
        private const string _Element = "Element";
        private const string _HighlightColor = "HighlightColor";
        private const string _Importance = "Importance";

        private bool _isLoaded = false;

        public void LoadData()
        {
            Cursor.Current = Cursors.WaitCursor;

            _QCSettingsPath = AppFolders.AppDataPathToolsQC;

            DataTable dt = CreatePopulateDataTable();

            _Analysis = new Analysis();
            _Analysis.LoadSettings(_QCSettingsPath);

            // Adverbs
            DataRow row = dt.NewRow();
            row[_Element] = "Adverbs";
            row[_HighlightColor] = _Analysis.Color_Adverbs;
            row[_Importance] = _Analysis.Importance_Adverbs;
            dt.Rows.Add(row);


            // Complex Words
            row = dt.NewRow();
            row[_Element] = "Complex Words";
            row[_HighlightColor] = _Analysis.Color_ComplexWords;
            row[_Importance] = _Analysis.Importance_ComplexWords;
            dt.Rows.Add(row);

            // Dictionary
            row = dt.NewRow();
            row[_Element] = "Dictionary Terms";
            row[_HighlightColor] = _Analysis.Color_DictionaryTerms;
            row[_Importance] = "Importance is determined by a Dictionary’s Weighted Values";
            dt.Rows.Add(row);

            // Long Sentence
            row = dt.NewRow();
            row[_Element] = "Long Sentence";
            row[_HighlightColor] = _Analysis.Color_LongSentence;
            row[_Importance] = _Analysis.Importance_LongSentence;
            dt.Rows.Add(row);

            // Passive Voice
            row = dt.NewRow();
            row[_Element] = "Passive Voice";
            row[_HighlightColor] = _Analysis.Color_PassiveVoice;
            row[_Importance] = _Analysis.Importance_PassiveVoice;
            dt.Rows.Add(row);

            dvgQCSettings.DataSource = dt;

            AutoSizeCols();

            _isLoaded = true;

            Cursor.Current = Cursors.Default;
        }

        private DataTable CreatePopulateDataTable()
        {
            DataTable table = new DataTable("QCSettings");

            table.Columns.Add(_Element, typeof(string));
            table.Columns.Add(_HighlightColor, typeof(string));
            table.Columns.Add(_Importance, typeof(string));

            return table;
        }

        private void AutoSizeCols()
        {
            dvgQCSettings.ColumnHeadersVisible = false;

            //set autosize mode
            dvgQCSettings.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dvgQCSettings.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
            dvgQCSettings.Columns[2].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
        }

        private void setBackColor()
        {
            if (!_isLoaded)
                return;

            string colorHighlight = string.Empty;

            foreach (DataGridViewRow row in dvgQCSettings.Rows)
            {
                if (row.Cells[_HighlightColor].Value != null)
                {
                    colorHighlight = row.Cells[_HighlightColor].Value.ToString();
                    row.Cells[_HighlightColor].Style.BackColor = Color.FromName(colorHighlight);
                }

            }

        }

        public bool Edit()
        {
            Cursor.Current = Cursors.WaitCursor;
            frmQCSettings frm = new frmQCSettings();
            frm.LoadData();
            Cursor.Current = Cursors.Default;
            if (frm.ShowDialog(this) == DialogResult.OK)
            {
                this.LoadData();
                return true;
            }

            return false;
        }

        private void dvgQCSettings_Paint(object sender, PaintEventArgs e)
        {
            setBackColor();
        }

        private void dvgQCSettings_SelectionChanged(object sender, EventArgs e)
        {
            dvgQCSettings.ClearSelection();  
        }
    }
}
