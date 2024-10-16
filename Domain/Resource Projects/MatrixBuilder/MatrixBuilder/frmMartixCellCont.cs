using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MatrixBuilder
{
    public partial class frmMartixCellCont : MetroFramework.Forms.MetroForm
    {
        public frmMartixCellCont()
        {
            InitializeComponent();
        }

        //private int _start = 0;
        //private int _indexOfSearchText = 0;

        private bool ignoreTxtChg = false;

        public void LoadData(string TextualContent, string Title)
        {
            lblTitle.Text = Title;
            lblContent.Text = TextualContent;
            rtb.Text = TextualContent;
        }

        private void butSearch_Click(object sender, EventArgs e)
        {
            ignoreTxtChg = true;

            int count = 0;

            try
            {
                string word = txtbSearch.Text.Trim();

                if (rtb.Text != string.Empty)
                {// if the ritchtextbox is not empty; highlight the search criteria
                    int index = 0;
                    String temp = rtb.Text;
                    rtb.Text = "";
                    rtb.Text = temp;
                    while (index < rtb.Text.LastIndexOf(word))
                    {
                        rtb.Find(word, index, rtb.TextLength, RichTextBoxFinds.None);
                        rtb.SelectionColor = Color.Yellow;
                        index = rtb.Text.IndexOf(word, index) + 1;
                        rtb.Select();
                        count++;
                    }
                }
            }

            catch (Exception ex) { MessageBox.Show(ex.Message, "Error"); }

            lblFoundQty.Text = string.Concat("Found: ", count.ToString());


            // ///////////////////////////////////////////
            //int count = 0;
            //int startIndex = 0;
            //string word = txtbSearch.Text.Trim();

            //while (startIndex < rtb.TextLength)
            //{

            //    int wordStartIndex = rtb.Find(word, startIndex, RichTextBoxFinds.None);
            //    if (wordStartIndex != -1)
            //    {
            //        rtb.SelectionStart = wordStartIndex;
            //        rtb.SelectionLength = word.Length;
            //        rtb.SelectionColor = Color.Yellow;
            //        count++;
            //    }
            //    else
            //        break;
            //    startIndex += wordStartIndex + word.Length;
            //}

            //lblFoundQty.Text = string.Concat("Found: ", count.ToString());


            //=================================

            //int startindex = 0;

            //if (txtbSearch.Text.Length > 0)
            //    startindex = FindMyText(txtbSearch.Text.Trim(), _start, rtb.Text.Length);

            //// If string was found in the RichTextBox, highlight it
            //if (startindex >= 0)
            //{
            //    // Set the highlight color as yellow
            //    rtb.SelectionColor = Color.Yellow;
            //    // Find the end index. End Index = number of characters in textbox
            //    int endindex = txtbSearch.Text.Length;
            //    // Highlight the search string
            //    rtb.Select(startindex, endindex);
            //    // mark the start position after the position of
            //    // last search string
            //    _start = startindex + endindex;
            //}
        }

        //private int FindMyText(string txtToSearch, int searchStart, int searchEnd)
        //{
        //    // Unselect the previously searched string
        //    if (searchStart > 0 && searchEnd > 0 && _indexOfSearchText >= 0)
        //    {
        //        rtb.Undo();
        //    }

        //    // Set the return value to -1 by default.
        //    int retVal = -1;

        //    // A valid starting index should be specified.
        //    // if indexOfSearchText = -1, the end of search
        //    if (searchStart >= 0 && _indexOfSearchText >= 0)
        //    {
        //        // A valid ending index
        //        if (searchEnd > searchStart || searchEnd == -1)
        //        {
        //            // Find the position of search string in RichTextBox
        //            _indexOfSearchText = rtb.Find(txtToSearch, searchStart, searchEnd, RichTextBoxFinds.None);
        //            // Determine whether the text was found in richTextBox1.
        //            if (_indexOfSearchText != -1)
        //            {
        //                // Return the index to the specified search text.
        //                retVal = _indexOfSearchText;
        //            }
        //        }
        //    }
        //    return retVal;
        //}

        private void butCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rtb_TextChanged(object sender, EventArgs e)
        {
            if (ignoreTxtChg)
            {
                ignoreTxtChg = false;
                    return;
            }

            //if (lblContent.Text != rtb.Text)
            //    rtb.Text = lblContent.Text;
        }
    }
}
