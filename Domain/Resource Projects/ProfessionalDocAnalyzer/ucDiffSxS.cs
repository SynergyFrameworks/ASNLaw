using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Collections;
using System.Data;
using System.IO;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;

//using DiffPlex;
//using DiffPlex.DiffBuilder;
//using DiffPlex.DiffBuilder.Model;

using Atebion.Common;
using Atebion.DiffSxS;

namespace ProfessionalDocAnalyzer
{
    public partial class ucDiffSxS : UserControl
    {
        public ucDiffSxS()
        {
            StackTrace st = new StackTrace(false);

            InitializeComponent();
        }

        // Folders
        private string _CompareDir = string.Empty;
        private string _CompareDirNotes = string.Empty;
        private string _CompareDirNotesHTML = string.Empty;
        private string _CompareDirModsPart = string.Empty;
        private string _CompareDirMods = string.Empty;
        private string _CompareDirModsWhole = string.Empty;

        private DataSet _dsDiffResults;

        private struct ResultsSum
        {
            public int Total;
            public int Added;
            public int Deleted;
            public int Changed;
            public int Unchanged;

        }

        private ResultsSum _ResultsSum;

        private ArrayList _lstChangedLines;

        // Files
        private string _OldFile = string.Empty;
        private string _OldOrgFile = string.Empty;
        private string _NewFile = string.Empty;
        private string _NewOrgFile = string.Empty;

        private int _SelectedLine = 0;
        private int _currentChangeLineIndex = 0;

        public bool LoadData(string CompareDir, string CompareDirNotes, string CompareDirNotesHTML, string CompareDirModsPart, string CompareDirMods, string CompareDirModsWhole)
        {
            _CompareDir = CompareDir;
            _CompareDirNotes = CompareDirNotes;
            _CompareDirNotesHTML = CompareDirNotesHTML;
            _CompareDirModsPart = CompareDirModsPart;
            _CompareDirMods = CompareDirMods;
            _CompareDirModsWhole = CompareDirModsWhole;

            string pathFile = string.Empty;
            string msg = string.Empty;

            _lstChangedLines = new ArrayList();

            LoadRecords();

            // Summary information
            pathFile = Path.Combine(_CompareDir, "DiffQtySum.txt");
            if (!File.Exists(pathFile))
            {
                msg = string.Concat("Unable to find file: ", pathFile);
                MessageBox.Show(msg, "Unable to Open Diff Results", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            string[] sumQty = Files.ReadFile2Array(pathFile);
            string name = string.Empty;
            int value = -1;
            foreach (string qty in sumQty)
            {
                if (qty.IndexOf('=') > -1)
                {
                    value = getValue(qty, out name); //_ResultsSum.Added

                    switch (name)
                    {
                        case "Modified":
                            _ResultsSum.Changed = value;
                            break;
                        case "Inserted":
                            _ResultsSum.Added = value;
                            break;
                        case "Deleted":
                            _ResultsSum.Deleted = value;
                            break;
                        case "Total":
                            _ResultsSum.Total = value;
                            break;
                    }
                }
            }

            // Files
            pathFile = Path.Combine(_CompareDir, "DiffFiles.txt");
            if (!File.Exists(pathFile))
            {
                msg = string.Concat("Unable to find file: ", pathFile);
                MessageBox.Show(msg, "Unable to Open Diff Results", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }
            string[] files = Files.ReadFile2Array(pathFile);
            if (files.Length > 3)
            {
                _OldFile = files[0];
                _NewFile = files[1];
                _OldOrgFile = files[2];
                _NewOrgFile = files[3];
            }


            int chgQty = LoadChangeList();

            if (chgQty > 0)
            {
                butBack.Visible = true;
                butNext.Visible = true;
            }
            else
            {
                butBack.Visible = false;
                butNext.Visible = false;
            }

            DisplaySum();

            

            return true;
        }

        private bool LoadRecords()
        {
            string pathFile = Path.Combine(_CompareDir, "DiffResults.xml");

            if (!File.Exists(pathFile))
            {
                MessageBox.Show("Unable to find Diff Data file DiffResults.xml", "Unable to Load Data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return false;
            }

            GenericDataManger gDataMgr = new GenericDataManger();

            _dsDiffResults = gDataMgr.LoadDatasetFromXml(pathFile);
            if (_dsDiffResults.Tables.Count == 0)
            {
                return false;
            }


            lvSource.Items.Clear();
            lvDestination.Items.Clear();

            ListViewItem lviS;
            ListViewItem lviD;


            string changeType = string.Empty;

            foreach (DataRow row in _dsDiffResults.Tables[0].Rows)
            {

                lviS = new ListViewItem(row[ResultsFields.LineNo].ToString());
                lviD = new ListViewItem(row[ResultsFields.LineNo].ToString());

                lviS.SubItems.Add(row[ResultsFields.OldText].ToString());
                lviD.SubItems.Add(row[ResultsFields.NewText].ToString());

                changeType = row[ResultsFields.ChangeType].ToString();

                if (changeType == "Inserted")
                {
                    lviS.BackColor = Color.LightGreen;
                    lviD.BackColor = Color.LightGray;
                }
                else if (changeType == "Deleted")
                {
                    lviS.BackColor = Color.LightGray;
                    lviD.BackColor = Color.Tomato;
                    _ResultsSum.Deleted++;
                }
                else if (changeType == "Modified")
                {
                    lviS.BackColor = Color.Yellow;
                    lviD.BackColor = Color.LightGray;
                    _ResultsSum.Changed++;
                }
                else
                {
                    lviS.BackColor = Color.White;
                    lviD.BackColor = Color.White;
                }

                lvSource.Items.Add(lviS);
                lvDestination.Items.Add(lviD);
            }


            return true;
        }

        private int getValue(string input, out string name)
        {
            string[] nameValue = input.Split('=');
            name = nameValue[0];
            string value = nameValue[1];

            return Convert.ToInt32(value);
        }

        private int LoadChangeList()
        {
            _lstChangedLines.Clear();

            int i = 0;

            string pathFile = Path.Combine(_CompareDir, "DiffLines.txt");
            if (!File.Exists(pathFile))
                return 0;

            string[] chnagedLines = Files.ReadFile2Array(pathFile);
            int convertedLine = -1;
            foreach (string line in chnagedLines)
            {
                if (line.Trim().Length > 0)
                {
                    if (DataFunctions.IsNumeric(line.Trim()))
                    {
                        convertedLine = Convert.ToInt32(line);
                        _lstChangedLines.Add(convertedLine);
                        i++;
                    }
                }
            }

            return i;
        }

        private void DisplaySum()
        {
            lblScrFile.Text = Files.GetFileName(_OldOrgFile);
            lblDesFile.Text = Files.GetFileName(_NewOrgFile); 

            lblSumModifiedQty.Text = string.Concat("Modified: ", _ResultsSum.Changed.ToString());
            lblSumAddedQty.Text = string.Concat("Inserted: ", _ResultsSum.Added.ToString());
            lblSumRemovedQty.Text = string.Concat("Deleted: ", _ResultsSum.Deleted.ToString());
            int totalChanged = _ResultsSum.Changed + _ResultsSum.Added + _ResultsSum.Deleted;
            lblSumTotalChgQty.Text = string.Concat("Total: ", totalChanged.ToString());

            spltconMain.Dock = DockStyle.Fill;
            spltconMain.Visible = true;


        }

        private void butGenerateReport_Click(object sender, EventArgs e)
        {
            this.Cursor = Cursors.WaitCursor;

            DiffHTMLRpt rpt = new DiffHTMLRpt();

            string sGenReport = "Generating Report";
            UpdateStatus(sGenReport);

            UpdateStatus(string.Concat("Step 1 of 8  ", sGenReport, ": Reading Notes"));
            rpt.CovertNotes2HTML(_CompareDirNotes, _CompareDirNotesHTML);

            UpdateStatus(string.Concat("Step 2 of 8  ", sGenReport, ": Reading Collecting Information"));
            string pathFile = Path.Combine(_CompareDir, "DiffReport.html");
            rpt.StartRpt_1(pathFile);

            UpdateStatus(string.Concat("Step 3 of 8  ", sGenReport, ": Defining Report Style"));
            rpt.GenRtpStyles_2();

            UpdateStatus(string.Concat("Step 4 of 8  ", sGenReport, ": Creating Header"));

            rpt.GenRtpHeader_2(_OldOrgFile, _NewOrgFile);

            UpdateStatus(string.Concat("Step 5 of 8  ", sGenReport, ": Start Summary"));
            rpt.GenSummaryArea_3();

            UpdateStatus(string.Concat("Step 5 of 8  ", sGenReport, ": Creating Summary"));
            rpt.GenSumQty_4(_ResultsSum.Changed.ToString(), "yellow", "Modified");
            rpt.GenSumQty_4(_ResultsSum.Added.ToString(), "lightgreen", "Inserted");
            rpt.GenSumQty_4(_ResultsSum.Deleted.ToString(), "tomato", "Deleted");
            int totalChanged = _ResultsSum.Changed + _ResultsSum.Added + _ResultsSum.Deleted;
            rpt.GenSumQty_4(totalChanged.ToString(), "lightsteelblue", "Total");
            rpt.EndsumBlockArea_5();

            UpdateStatus(string.Concat("Step 6 of 8  ", sGenReport, ": Creating Changed Lines List"));
            rpt.GenChangeLines_6(_lstChangedLines);

            UpdateStatus(string.Concat("Step 7 of 8  ", sGenReport, ": Creating Details"));
            rpt.GenDetails_7(_dsDiffResults.Tables[0], _CompareDirModsPart);

            UpdateStatus(string.Concat("Step 8 of 8  ", sGenReport, ": Completing Report"));
            rpt.End_8();

            DisplaySum();

            UpdateStatus("");

            this.Cursor = Cursors.Default;

            System.Diagnostics.Process.Start(pathFile);


        }

        private void UpdateStatus(string status)
        {
            lblProcessStatus.Text = status;
            lblProcessStatus.Refresh();
        }

        private void UpdateNotes()
        {
            if (_SelectedLine == -1)
            {
                lblLineNo.Text = "Line: None";
                richerTextBox1.ResetText();
            }

            lblLineNo.Text = _SelectedLine.ToString("00000");
            string file = _SelectedLine.ToString() + ".rtf";
            string pathFile = Path.Combine(_CompareDirNotes, file);
            if (File.Exists(pathFile))
            {
                richerTextBox1.LoadFile(pathFile);
            }
            else
            {
                richerTextBox1.ResetText();
                this.richerTextBox1.LoadedFile = pathFile; // File doesn't exists yet
            }

            this.richerTextBox1.LoadedFile = pathFile; // File doesn't exists yet


            // Load Modified Details if it exists
            string s = _CompareDirMods;
            file = _SelectedLine.ToString() + ".html";
            pathFile = Path.Combine(_CompareDirModsWhole, file);
            //if (AppFolders.isTest)
            //{
            //    MessageBox.Show("Mod File: " + pathFile);
            //}
            if (File.Exists(pathFile))
            {
                this.webBrowser1.Navigate(pathFile);
                this.lblLineNo2.Text = lblLineNo.Text;
                panLeftMidBottom.Visible = true;
            }
            else
            {
                panLeftMidBottom.Visible = false;
            }

        }

        private void lvSource_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (lvSource.SelectedItems.Count > 0)
            {
                _SelectedLine = lvSource.SelectedItems[0].Index;
                UpdateNotes();

                if (_currentChangeLineIndex > -1)
                {
                    int lineX = (int)_lstChangedLines[_currentChangeLineIndex];

                    int selectedIndex = GetSelectedIndex(lvSource);

                    if (selectedIndex == -1)
                        return;

                    if (selectedIndex == lineX) // Already has the same index selected
                        return;
                }

                ListViewItem lvi = lvDestination.Items[lvSource.SelectedItems[0].Index];
                lvi.Selected = true;
                lvi.EnsureVisible();
            }
        }

        private void lvDestination_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (lvDestination.SelectedItems.Count > 0)
            {
                _SelectedLine = lvDestination.SelectedItems[0].Index;
                UpdateNotes();

                if (_currentChangeLineIndex > -1)
                {
                    int lineX = (int)_lstChangedLines[_currentChangeLineIndex];

                    int selectedIndex = GetSelectedIndex(lvDestination);

                    if (selectedIndex == -1)
                        return;

                    if (selectedIndex == lineX) // Already has the same index selected
                        return;
                }


                ListViewItem lvi = lvSource.Items[lvDestination.SelectedItems[0].Index];
                lvi.Selected = true;
                lvi.EnsureVisible();
            }
        }

          private int GetSelectedIndex(ListView lv)
        {
            ListView.SelectedIndexCollection selected = lv.SelectedIndices;

            if (selected.Count > 0)
            {
                return selected[0];
            }
            else
            {
                return -1;
            }
        }

          private void richerTextBox1_Leave(object sender, EventArgs e)
          {
              SaveNote();
          }

          private void SaveNote()
          {

              if (_SelectedLine > -1)
              {
                  if (richerTextBox1.Text.Length > 0)
                      richerTextBox1.SaveFile();
              }
          }

          private void butNext_Click(object sender, EventArgs e)
          {
              SaveNote();

              if (_lstChangedLines.Count > 0)
              {
                  if (_currentChangeLineIndex == -1)
                  {
                      _currentChangeLineIndex = 0;
                  }
                  else if (_currentChangeLineIndex == 0)
                  {
                      if (_lstChangedLines.Count > 1)
                          _currentChangeLineIndex = _currentChangeLineIndex + 1;

                  }
                  else if (_currentChangeLineIndex == _lstChangedLines.Count - 1)
                  {
                      _currentChangeLineIndex = 0;
                  }
                  else
                  {
                      if (_lstChangedLines.Count > 1)
                          _currentChangeLineIndex = _currentChangeLineIndex + 1;
                  }

                  _SelectedLine = _currentChangeLineIndex;

                  //if (AppFolders.isTest)
                  //{
                  //    MessageBox.Show("_currentChangeLineIndex = " + _currentChangeLineIndex.ToString(), "Test");
                  //}

                  int lineX = (int)_lstChangedLines[_currentChangeLineIndex];

                  lvDestination.Items[lineX].Selected = true;
                  lvDestination.Select();
                  lvDestination.EnsureVisible(lineX);

                  lvSource.Items[lineX].Selected = true;
                  lvSource.Select();
                  lvSource.EnsureVisible(lineX);
              }
          }

          private void butBack_Click(object sender, EventArgs e)
          {
              SaveNote();

              if (_lstChangedLines.Count > 0)
              {
                  if (_currentChangeLineIndex == -1)
                  {
                      _currentChangeLineIndex = 0;
                  }
                  else if (_currentChangeLineIndex == 0)
                  {
                      if (_lstChangedLines.Count > 1)
                          _currentChangeLineIndex = _lstChangedLines.Count - 1;
                      else
                          _currentChangeLineIndex = 0;
                  }
                  else
                  {
                      _currentChangeLineIndex = _currentChangeLineIndex - 1;
                  }

                  _SelectedLine = _currentChangeLineIndex;

                  //if (AppFolders.isTest)
                  //{
                  //    MessageBox.Show("_currentChangeLineIndex = " + _currentChangeLineIndex.ToString(), "Test");
                  //}

                  int lineX = (int)_lstChangedLines[_currentChangeLineIndex];

                  lvDestination.Items[lineX].Selected = true;
                  lvDestination.Select();
                  lvDestination.EnsureVisible(lineX);

                  lvSource.Items[lineX].Selected = true;
                  lvSource.Select();
                  lvSource.EnsureVisible(lineX);



                  //string lineS = lineX.ToString("00000");

                  //ListViewItem lvi = lvDestination.Items[lvSource.SelectedItems[lineS].Index];
                  //lvi.Selected = true;
                  //lvi.EnsureVisible();

                  //lvi = lvSource.Items[lvDestination.SelectedItems[lineS].Index];
                  //lvi.Selected = true;
                  //lvi.EnsureVisible();

              }

          }

          private void lvSource_Resize(object sender, EventArgs e)
          {
              if (lvSource.Width > 100)
              {
                  lvSource.Columns[1].Width = -2; // Exspand last column to fill 
              }
          }

          private void lvDestination_Resize(object sender, EventArgs e)
          {
              if (lvDestination.Width > 100)
              {
                  lvDestination.Columns[1].Width = -2; // Exspand last column to fill 
              }
          }

  
    
    }
}
