using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Data;
using Domain.Common;

using DiffPlex;
using DiffPlex.DiffBuilder;
using DiffPlex.DiffBuilder.Model;


namespace Domain.DiffSxS
{
    public class DocsDiff
    {
        private IDiffer differ;
        private SideBySideDiffBuilder sideBySideDiffer;
        private SideBySideDiffModel _results3;
        private DataTable _dtResults;
        private DataSet _dsDiffResults;
        private ArrayList _lstChangedLines;

        private struct ResultsSum
        {
            public int Total;
            public int Added;
            public int Deleted;
            public int Changed;
            public int Unchanged;

        }

        private ResultsSum _ResultsSum;


        // Folders
        private string _CompareDir = string.Empty;
        private string _CompareDirModsWhole = string.Empty;
        private string _CompareDirModsPart = string.Empty;

        // Files
        private string _OldFile = string.Empty;
        private string _OldOrgFile = string.Empty;
        private string _NewFile = string.Empty;
        private string _NewOrgFile = string.Empty;


        private string _ErrorMessage = string.Empty;

        public string ErrorMessage
        {
            get { return _ErrorMessage; }
        }

        public bool CompareDocs3(string OldFile, string NewFile, string OldOrgFile, string NewOrgFile, string CompareDir, string CompareDirModsWhole, string CompareDirModsPart)
        {
            _CompareDir = CompareDir;

            differ = new Differ();
           _dsDiffResults = new DataSet();
           _lstChangedLines = new ArrayList();

            _OldFile = OldFile;
            _NewFile = NewFile;
            _OldOrgFile = OldOrgFile;
            _NewOrgFile = NewOrgFile;
            _CompareDirModsWhole = CompareDirModsWhole;
            _CompareDirModsPart = CompareDirModsPart;

            sideBySideDiffer = new SideBySideDiffBuilder(differ);

            string txtSrc = Files.ReadFile(OldFile);
            string txtDes = Files.ReadFile(NewFile);

            _results3 = sideBySideDiffer.BuildDiffModel(txtSrc, txtDes);

            _dtResults = createTable();

            Populate3();

            SaveResults();

            return true;

        }

        private int Populate3()
        {
            int cnt = 0;

            // Set Defaults
            _ResultsSum.Added = 0;
            _ResultsSum.Changed = 0;
            _ResultsSum.Deleted = 0;
            _ResultsSum.Unchanged = 0;
            _ResultsSum.Total = 0;

            DiffModHTML diffModHTML = new DiffModHTML();

            string sDiffModHTML = string.Empty;
            string pathFile = string.Empty;
            string sDiffModHTMLPath = string.Empty;

            for (int i = 0; i < Math.Min(_results3.OldText.Lines.Count, _results3.NewText.Lines.Count); i++)
            {
                if (_results3.OldText.Lines[i].Type == ChangeType.Inserted)
                {
                    _ResultsSum.Added++;
                    _lstChangedLines.Add(i);

                    TableAddTextLine(cnt.ToString("00000"), "Inserted", "New", _results3.OldText.Lines[i].Text, _results3.NewText.Lines[i].Text);
                }
                else if (_results3.NewText.Lines[i].Type == ChangeType.Inserted)
                {
                    _ResultsSum.Added++;
                    _lstChangedLines.Add(i);

                    TableAddTextLine(cnt.ToString("00000"), "Inserted", "Old", _results3.OldText.Lines[i].Text, _results3.NewText.Lines[i].Text);
                }
                else if (_results3.NewText.Lines[i].Type == ChangeType.Deleted)
                {
                    _ResultsSum.Deleted++;
                    _lstChangedLines.Add(i);

                    TableAddTextLine(cnt.ToString("00000"), "Deleted", "New", _results3.OldText.Lines[i].Text, _results3.NewText.Lines[i].Text);
                }
                else if (_results3.OldText.Lines[i].Type == ChangeType.Deleted)
                {
                    _ResultsSum.Deleted++;
                    _lstChangedLines.Add(i);

                    TableAddTextLine(cnt.ToString("00000"), "Deleted", "Old", _results3.OldText.Lines[i].Text, _results3.NewText.Lines[i].Text);
                }
                else if (_results3.OldText.Lines[i].Type == ChangeType.Modified)
                {
                    _ResultsSum.Changed++;
                    _lstChangedLines.Add(i);

                    TableAddTextLine(cnt.ToString("00000"), "Modified", "Old", _results3.OldText.Lines[i].Text, _results3.NewText.Lines[i].Text);

                    // Save Modified Diff details into an HTML file
                    sDiffModHTML = diffModHTML.GenerateWholeDiffMod(_results3.OldText.Lines[i].Text, _results3.NewText.Lines[i].Text);
                    if (sDiffModHTML.Length > 0)
                    {
                        pathFile = string.Concat(i.ToString(), ".html");
                        sDiffModHTMLPath = Path.Combine(_CompareDirModsWhole, pathFile);
                        if (!File.Exists(sDiffModHTMLPath))
                        {
                            Files.WriteStringToFile(sDiffModHTML, sDiffModHTMLPath);

                            sDiffModHTML = diffModHTML.Build();
                            if (sDiffModHTML.Length > 0)
                            {
                                sDiffModHTMLPath = Path.Combine(_CompareDirModsPart, pathFile);
                                Files.WriteStringToFile(sDiffModHTML, sDiffModHTMLPath);
                            }
                        }
                    }

                }
                else if (_results3.NewText.Lines[i].Type == ChangeType.Modified)
                {
 
                    _ResultsSum.Changed++;
                    _lstChangedLines.Add(i);

                    TableAddTextLine(cnt.ToString("00000"), "Modified", "New", _results3.OldText.Lines[i].Text, _results3.NewText.Lines[i].Text);

                    // Save Modified Diff details into an HTML file
                    sDiffModHTML = diffModHTML.GenerateWholeDiffMod(_results3.OldText.Lines[i].Text, _results3.NewText.Lines[i].Text);
                    if (sDiffModHTML.Length > 0)
                    {
                        pathFile = string.Concat(i.ToString(), ".html");
                        sDiffModHTMLPath = Path.Combine(_CompareDirModsWhole, pathFile);
                        if (!File.Exists(sDiffModHTMLPath))
                        {
                            Files.WriteStringToFile(sDiffModHTML, sDiffModHTMLPath);

                            sDiffModHTML = diffModHTML.Build();
                            if (sDiffModHTML.Length > 0)
                            {
                                sDiffModHTMLPath = Path.Combine(_CompareDirModsPart, pathFile);
                                Files.WriteStringToFile(sDiffModHTML, sDiffModHTMLPath);
                            }
                        }
                    }
                }
                else
                {
                    _ResultsSum.Unchanged++;

                    TableAddTextLine(cnt.ToString("00000"), "Unchanged", "Both", _results3.OldText.Lines[i].Text, _results3.NewText.Lines[i].Text);
                }

                cnt++;
            }


            return cnt;
        }

        private void TableAddTextLine(string LineNo, string ChangeType, string ChangeOldNew, string OldText, string NewText)
        {
            DataRow row = _dtResults.NewRow();
            row[ResultsFields.LineNo] = LineNo;
            row[ResultsFields.ChangeType] = ChangeType;
            row[ResultsFields.ChangeOldNew] = ChangeOldNew;
            row[ResultsFields.NewText] = NewText;
            row[ResultsFields.OldText] = OldText;
            _dtResults.Rows.Add(row);
        }

        private DataTable createTable()
        {
            DataTable table = new DataTable("Results");

            table.Columns.Add(ResultsFields.LineNo, typeof(string));
            table.Columns.Add(ResultsFields.ChangeType, typeof(string));
            table.Columns.Add(ResultsFields.ChangeOldNew, typeof(string));
            table.Columns.Add(ResultsFields.OldText, typeof(string));
            table.Columns.Add(ResultsFields.NewText, typeof(string));

            return table;
        }

        private bool SaveResults()
        {
            _dsDiffResults.Tables.Add(_dtResults);


            string DiffResultsFile = Path.Combine(_CompareDir, "DiffResults.xml");
            if (File.Exists(DiffResultsFile))
                File.Delete(DiffResultsFile);

            if (!SaveDataXML(_dsDiffResults, DiffResultsFile))
            {
                return false;
            }

            string pathFile = string.Empty;
            // Saved Line numbers
            if (_lstChangedLines.Count > 0)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var lineChange in _lstChangedLines)
                {
                    sb.AppendLine(lineChange.ToString());
                }

                pathFile = Path.Combine(_CompareDir, "DiffLines.txt");
                Files.WriteStringToFile(sb.ToString(), pathFile, true);

                if (!File.Exists(pathFile))
                {
                    return false;
                }
            }

            // Save Summary Information
            StringBuilder sbSum = new StringBuilder();
            sbSum.AppendLine(string.Concat("Modified=", _ResultsSum.Changed.ToString()));
            sbSum.AppendLine(string.Concat("Inserted=", _ResultsSum.Added.ToString()));
            sbSum.AppendLine(string.Concat("Deleted=", _ResultsSum.Deleted.ToString()));
            int totalChanged = _ResultsSum.Changed + _ResultsSum.Added + _ResultsSum.Deleted;
            sbSum.AppendLine(string.Concat("Total=", totalChanged.ToString()));

            pathFile = Path.Combine(_CompareDir, "DiffQtySum.txt");
            Files.WriteStringToFile(sbSum.ToString(), pathFile);

            // Save files notations
            sbSum.Clear();
            sbSum.AppendLine(_OldFile);
            sbSum.AppendLine(_NewFile);
            sbSum.AppendLine(_OldOrgFile);
            sbSum.AppendLine(_NewOrgFile);

            pathFile = Path.Combine(_CompareDir, "DiffFiles.txt");
            Files.WriteStringToFile(sbSum.ToString(), pathFile);


            return true;
        }

        private bool SaveDataXML(DataSet ds, string pathFile)
        {
            try
            {
                ds.WriteXml(pathFile, XmlWriteMode.WriteSchema);
                if (File.Exists(pathFile))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                _ErrorMessage = string.Concat("Saving Data an XML file: ", pathFile, " -- Error: ", e.ToString());
            }
            return false;

        }
    }
}
