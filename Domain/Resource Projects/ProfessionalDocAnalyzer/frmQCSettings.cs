using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;

using Atebion.Common;
using Atebion.QC;

namespace ProfessionalDocAnalyzer
{
    public partial class frmQCSettings : MetroFramework.Forms.MetroForm
    {
        public frmQCSettings()
        {
            StackTrace st = new StackTrace(false);
            
            InitializeComponent();
        }

        private string _QCSettingsPath = string.Empty;
        private string _DictionariesPath = string.Empty;

        Atebion.QC.Analysis _Analysis;

        public void LoadData()
        {
            LoadColorCombo(this.cboColorA);
            LoadColorCombo(this.cboColorCW);
            LoadColorCombo(this.cboColorDT);
            LoadColorCombo(this.cboColorLS);
            LoadColorCombo(this.cboColorPV);

            LoadImportanceCombo(this.cboImportantA);
            LoadImportanceCombo(this.cboImportantCW);
            LoadImportanceCombo(this.cboImportantLS);
            LoadImportanceCombo(this.cboImportantPV);

            string s = AppFolders.AppDataPathTools;
            _QCSettingsPath = AppFolders.AppDataPathToolsQC;
            _DictionariesPath = AppFolders.DictionariesPath;

         //   LoadDictionaries();

            _Analysis = new Analysis();
            _Analysis.LoadSettings(_QCSettingsPath);


            // Adverbs
            cboColorA.SelectedIndex = cboColorA.FindStringExact(_Analysis.Color_Adverbs);
            cboImportantA.SelectedIndex = cboImportantA.FindStringExact(_Analysis.Importance_Adverbs);

            // Complex Words
            cboColorCW.SelectedIndex = cboColorCW.FindStringExact(_Analysis.Color_ComplexWords);
            cboImportantCW.SelectedIndex = cboImportantCW.FindStringExact(_Analysis.Importance_ComplexWords);

            // Dictionary
            cboColorDT.SelectedIndex = cboColorDT.FindStringExact(_Analysis.Color_DictionaryTerms);

            // Long Sentence
            cboColorLS.SelectedIndex = cboColorLS.FindStringExact(_Analysis.Color_LongSentence);
            cboImportantLS.SelectedIndex = cboImportantLS.FindStringExact(_Analysis.Importance_LongSentence);
            udWordsGreaterThan.Value = _Analysis.LongSentence_Def;

            // Passive Voice
            cboColorPV.SelectedIndex = cboColorPV.FindStringExact(_Analysis.Color_PassiveVoice);
            cboImportantPV.SelectedIndex = cboImportantPV.FindStringExact(_Analysis.Importance_PassiveVoice);

        }

        private void LoadDictionaries()
        {
            string[] files = Directory.GetFiles(_DictionariesPath, "*.dicx");

            cboDictionary.Items.Clear();
            cboDictionary.Items.Add("");

            string fileName = string.Empty;
            foreach (string file in files)
            {
                fileName = Files.GetFileNameWOExt(file);
                cboDictionary.Items.Add(fileName);
            }

        }

        private void LoadColorCombo(ComboBox cbo)
        {
            // Load colors into drop-down list
            cbo.Items.Clear();
            Type colorType = typeof(System.Drawing.Color);
            PropertyInfo[] propInfoList = colorType.GetProperties(BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.Public);
            foreach (PropertyInfo c in propInfoList)
            {
                if (c.Name != "Transparent")
                    cbo.Items.Add(c.Name);
            }
        }

        private void LoadImportanceCombo(ComboBox cbo)
        {
            cbo.Items.Clear();
            cbo.Items.Add("Not Important");
            cbo.Items.Add("Somewhat Important");
            cbo.Items.Add("Important");
            cbo.Items.Add("Very Important");

        }

        private void cboColorLS_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void cboColorCW_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void cboColorPV_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void cboColorA_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void cboColorDT_DrawItem(object sender, DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle rect = e.Bounds;
            if (e.Index >= 0)
            {
                string n = ((ComboBox)sender).Items[e.Index].ToString();
                Font f = new Font("Arial", 9, FontStyle.Regular);
                Color c = Color.FromName(n);
                Brush b = new SolidBrush(c);
                g.DrawString(n, f, Brushes.Black, rect.X, rect.Top);
                g.FillRectangle(b, rect.X + 110, rect.Y + 5, rect.Width - 10, rect.Height - 10);
            }
        }

        private void butOK_Click(object sender, EventArgs e)
        {
            Cursor.Current = Cursors.WaitCursor;

            // Adverbs
            _Analysis.Color_Adverbs = cboColorA.Text;
            _Analysis.Importance_Adverbs = cboImportantA.Text;

            // Complex Words
            _Analysis.Color_ComplexWords = cboColorCW.Text;
            _Analysis.Importance_ComplexWords = cboImportantCW.Text;

            // Dictionary
            _Analysis.Color_DictionaryTerms = cboColorDT.Text;

            // Long Sentence
            _Analysis.Color_LongSentence = cboColorLS.Text;
            _Analysis.Importance_LongSentence = cboImportantLS.Text;
            _Analysis.LongSentence_Def = Convert.ToInt32(udWordsGreaterThan.Value);

            // Passive Voice
            _Analysis.Color_PassiveVoice = cboColorPV.Text;
            _Analysis.Importance_PassiveVoice = cboImportantPV.Text;

            if (!_Analysis.SaveSettings(_QCSettingsPath))
            {
                Cursor.Current = Cursors.Default; // Back to normal
                MessageBox.Show(_Analysis.ErrorMessage, "Unable to Save QC Settings", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            Cursor.Current = Cursors.Default; // Back to normal

            this.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.Close();

        }
    }
}
