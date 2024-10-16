using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using Atebion.Common;

namespace ProfessionalDocAnalyzer
{
    public partial class frmDocumentView : Form
    {
        public frmDocumentView()
        {
            StackTrace st = new StackTrace(false);
            InitializeComponent();
        }

        private string _ErrorMessage = string.Empty;

        private Previous _Previous = new Previous();
        private struct Previous
        {
            public  string Text;
            public  int Line;
            public int StartIndex;
        }

        private string _BookmarkPath = string.Empty;
        private const string _BOOKMARKsFILE = "Bookmarks.txt";

        public bool LoadData(string rtfFile, string BookmarkPath, string FileName)
        {
            _ErrorMessage = string.Empty;

            lblHeader.Text = string.Concat("Document:   ", FileName);

            Irtb1.IRTBEnableHighLight = false;

            if (!File.Exists(rtfFile))
            {
                _ErrorMessage = string.Concat("Unable to find file: ", rtfFile);
                return false;
            }

            Irtb1.IRTBFileToLoad = rtfFile;
            Irtb1.IRTBMarkedLines.Clear();
            BMLinesTool.Items.Clear();

            Irtb1.IRTBEnableNumbering = true;
            this.Irtb1.IRTBContainer.BorderStyle = BorderStyle.FixedSingle;

            _BookmarkPath = BookmarkPath;
            string pathFileBookmarks = Path.Combine(_BookmarkPath, _BOOKMARKsFILE);
            if (File.Exists(pathFileBookmarks))
            {
                string[] bookmarks = Atebion.Common.Files.ReadFile2Array(pathFileBookmarks);
                if (bookmarks != null)
                {
                    if (bookmarks.Length > 0)
                    {
                        foreach (string bookmark in bookmarks)
                        {
                            if (bookmark.Trim().Length > 0)
                            {
                                Irtb1.IRTBMarkedLines.Add(Convert.ToInt32(bookmark.Replace("Line ", "")));

                                BMLinesTool.Items.Add(bookmark);
                            }
                        }
                    }
                }
            }

            return true;
        }

        public void ShowSegment(int Line)
        {
            this.Irtb1.GoToBookmark(Line);
 
        }

        private void SaveBookmarks()
        {
            
            string pathFileBookmarks = Path.Combine(_BookmarkPath, _BOOKMARKsFILE);

            StringBuilder sb = new StringBuilder();

            for (int i = 0; i < BMLinesTool.Items.Count; i++) 
            {
               sb.AppendLine(BMLinesTool.Items[i].ToString());

            }

            Atebion.Common.Files.WriteStringToFile(sb.ToString(), pathFileBookmarks, true);
        }

        public void ShowSegment(int Line, string TextualContent)
        {
            this.Irtb1.GoToBookmark(Line);

            int startIndex = this.Irtb1.IRTBContainer.GetFirstCharIndexFromLine(Line);

            if (_Previous.Text != null)
            {
                if (_Previous.Text.Length > 0)
                {
                   // this.Irtb1.GoToBookmark(Line);
                    HighlightText3(_Previous.StartIndex, _Previous.Text, false, "Black", true);
                }
            }

            this.Irtb1.GoToBookmark(Line);
            startIndex = this.Irtb1.IRTBContainer.GetFirstCharIndexFromLine(Line);

            HighlightText3(startIndex, TextualContent, false, "Blue", true);

            _Previous.Text = TextualContent;
            _Previous.StartIndex = startIndex;
            _Previous.Line = Line;

        }

        public int HighlightText3(int startIndex, string word, bool wholeWord, string color, bool HighlightText)
        {
            int count = 0;

            if (color == string.Empty)
                color = "YellowGreen";

            string txt = this.Irtb1.IRTBContainer.Text;         

            string adjWord;
            //adjWord = word.Replace("(", @"\(");
            //adjWord = adjWord.Replace(")", @"\)");

            adjWord = Regex.Escape(word.Trim());

            //if (wholeWord)
            //{
            //    //  adjWord = "\\W?(" + word + ")\\W?";
            //    adjWord = @"\b(" + word + @")\b";
            //}
            //else
            //{
            //    adjWord = word.Replace("(", @"\(");
            //    adjWord = adjWord.Replace(")", @"\)");
                

            //}

            
           // Regex regex = new Regex(@adjWord, RegexOptions.Multiline);

            Regex regex;
            try // Added try 02.22.2019
            {
                regex = new Regex(@adjWord, RegexOptions.Multiline);
            }
            catch
            {
                return -1;
            }

            MatchCollection matches = regex.Matches(txt);
          //  int index = -1;
         //   int startIndex = 0;

            count = matches.Count;

            if (count > 0)
            {
                foreach (Match match in matches)
                {
                    this.Irtb1.IRTBContainer.Select(match.Index, word.Length);

                    if (HighlightText) // Highlight Text
                        this.Irtb1.IRTBContainer.SelectionColor = Color.FromName(color);
                    else // Highlight Background
                        this.Irtb1.IRTBContainer.SelectionBackColor = Color.FromName(color);

                    this.Irtb1.IRTBContainer.DeselectAll();
                    this.Irtb1.IRTBContainer.ScrollToCaret();

                    startIndex = startIndex + word.Length;

                    return 1;
                }              
            }
            else
            {

                using (StringReader sr = new StringReader(word.Trim()))
                {
                    adjWord = sr.ReadLine();
                }

                adjWord = Regex.Escape(adjWord);

                regex = new Regex(@adjWord, RegexOptions.Multiline);
                matches = regex.Matches(txt);

                if (matches.Count > 0)
                {
                    foreach (Match match in matches)
                    {
                        this.Irtb1.IRTBContainer.Select(match.Index, word.Trim().Length);

                        if (HighlightText) // Highlight Text
                            this.Irtb1.IRTBContainer.SelectionColor = Color.FromName(color);
                        else // Highlight Background
                            this.Irtb1.IRTBContainer.SelectionBackColor = Color.FromName(color);

                        this.Irtb1.IRTBContainer.DeselectAll();
                        this.Irtb1.IRTBContainer.ScrollToCaret();

                        startIndex = startIndex + word.Length;

                        return 1;
                    }
                }

            }

            return count;
        }


        private void Irtb1_BMLInformation(bool NewEvent)
        {
            this.BMLinesTool.Items.Clear();
            foreach (var BMLineTemp in Irtb1.IRTBMarkedLines)
            {
                this.BMLinesTool.Items.Add("Line " + BMLineTemp);
            }

            SaveBookmarks();
        }

        private void BMLinesTool_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.BMLinesTool.Items.Count == 0)
                return;

            if (this.BMLinesTool.SelectedIndex == -1)
                return;

            string lineNo = this.BMLinesTool.SelectedItem.ToString().Replace("Line ", "");

            Irtb1.GoToBookmark(Convert.ToInt32(lineNo));
        }


    }
}
