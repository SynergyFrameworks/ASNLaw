using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using AtebionParse;
using System.IO;
using Atebion.Common;
//using Microsoft.VisualBasic;

namespace ProfessionalDocAnalyzer
{
    public partial class frmSplitSec : MetroFramework.Forms.MetroForm
	{
        public frmSplitSec(string pathFile, string ParseResultsFile, string XMLPath, string DocParsedSecPath, AnalysisResultsType.SearchType SearchType, string Number, string Title, string UID)
		{
            StackTrace st = new StackTrace(false);

			InitializeComponent();

			this.AcceptButton = this.butOK;
			this.CancelButton = this.butCancel;
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            //this.MaximizeBox = false;
            //this.MinimizeBox = false;
            //this.ShowInTaskbar = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;

			_pathFile = pathFile;
            _ParseResultsFile = ParseResultsFile;
            _XMLPath = XMLPath;
            _SearchType = SearchType;
            _DocParsedSecPath = DocParsedSecPath;
			
			_UID = UID;

			LoadData();

			txtbTopSecNo.Text = Number;
			txtbTopSecTitle.Text = Title;
			lblOrgSecNo.Text = Number;
			lblOrgSecTitle.Text = Title;
			panBottomSec.Visible = false;

            lblMesssage.Text = Message1;
		}

		#region Private Vars

		private string _pathFile = string.Empty;
        private string _ParseResultsFile = string.Empty;
        private string _XMLPath = string.Empty;
        private string _DocParsedSecPath = string.Empty;
        private AnalysisResultsType.SearchType _SearchType;

     //   private ProposalDetailMgr _ProposalDetailMgr;

        private const string Message1 = "Place the cursor where you want to Split the text into segments and then click the Split button.";
        private const string Message2 = "You can change the Number and Caption for both segments. -- To save the Split, Click the Save button.";


        private GenericDataManger _genericDataManger = new GenericDataManger();
		private DataSet _ds;

		private string _UID = string.Empty;
		private int _column;
		private int _row;

 
		int _indexB; // lIndex location of split

		string _Dir_CurrentParsedSecXML = string.Empty;

        string _BottomUID = string.Empty;

		#endregion

		#region Private Functions
		private void LoadData()
		{

            if (Files.FileIsLocked(_pathFile)) 
            {
                MessageBox.Show("The selected segment is currently opened by another application. Please close this segment file and try again.", "Unable to Open this Segment for Splitting", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

                this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
                this.Close();

                return;

            }

			rtfOrgSec.LoadFile(_pathFile);

			_Dir_CurrentParsedSecXML = AppFolders.DocParsedSecXML;

			
           // string ParseResultsFile = string.Concat(_Dir_CurrentParsedSecXML, @"\ParseResults.xml");

            _ds = Files.LoadDatasetFromXml(_ParseResultsFile);

			_ds.Tables[0].DefaultView.Sort = "SortOrder ASC"; 
		}
		#endregion

		private void butSplit_Click(object sender, EventArgs e)
		{
            lblMesssage.Text = Message2;
			Spit();
            butOK.Visible = true;
		}

		private void splitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Spit();
		}

		private void Spit()
		{
			_column = rtfOrgSec.GetCurrentColumn();
			_row = rtfOrgSec.GetCurrentLine();

			// Select the from cursor to bottom of the Org. RTF control
			_indexB = rtfOrgSec.SelectionStart;
			rtfOrgSec.SelectFromIndex(_indexB, rtfOrgSec.TextLength - 1);
			rtfBottomSec.Rtf = rtfOrgSec.SelectedRtf;

			// Select the from cursor to Top of the Org. RTF control
			int indexT = 0;
			rtfOrgSec.SelectFromIndex(indexT, _indexB - 1);
			rtfTopSec.Rtf = rtfOrgSec.SelectedRtf;

            string newBottomSecNo = GetNewBottomSecNo(lblOrgSecNo.Text); // Added 02.09.2019
            txtbBottomSecNo.Text = newBottomSecNo;
			AtebionParse.Parse parse = new Parse();
			string sBottonText = rtfBottomSec.Text;
			bool useDots = true;

            // Generate new caption
            string modText = Get1stCarriageReturn(sBottonText); // Added 07.70.2013
            string newCaption = parse.Truncate_String(modText, 50, ref useDots);
			txtbBottomTitle.Text = newCaption;
			

			panOrgSec.Visible = false;
			butSplit.Visible = false;

			panTopSec.Visible = true;
			panBottomSec.Visible = true;
			butBack.Visible = true;
			butOK.Visible = true;
		}

        private string GetNewBottomSecNo(string OrgSecNo) // Added 02.09.2019
        {
            string newBottomSecNo = string.Empty;

            int length = OrgSecNo.Length;
            int index = -1;

            if (length > 2)
            {
                index = OrgSecNo.IndexOf('-');
                if (index != -1)
                {
                    
                    if (DataFunctions.IsNumeric(OrgSecNo.Substring((index + 1))))
                    {
                        int number = Convert.ToInt32(OrgSecNo.Substring((index + 1)));
                        number++;
                        newBottomSecNo = string.Concat(OrgSecNo.Substring(0, index), "-", number.ToString());
                    }
                    else
                    {
                        newBottomSecNo = string.Concat(OrgSecNo, "-1");
                    }
                }
                else
                {
                    newBottomSecNo = string.Concat(OrgSecNo, "-1");
                }
            }
            else
            {
                newBottomSecNo = string.Concat(OrgSecNo, "-1");
            }


            return newBottomSecNo;

        }

        // Added 07.07.2013
        // Returns the 1st section of a string that contains a Carriage Return
        private string Get1stCarriageReturn(string input)
        {
            string output = input;

           // if (input.Contains('\n'))
           // if (input.Contains(Convert.ToChar("\n"))) 
            if (input.Contains('\n'))
            {  
                string[] capSegments = input.Split('\n');
                output = capSegments[0];  
            }

            return output;

        }

		private DataSet GetSelectedKeywords(string DelimitedKeywords)
		{

			if (DelimitedKeywords.Trim() == string.Empty)
				return null;


			// Use this Dataset to past to the Parser
			DataSet UseKeywords = new DataSet();
			DataTable Keywords = UseKeywords.Tables.Add();
			Keywords.Columns.Add("Category", typeof(string));
			Keywords.Columns.Add("Keyword", typeof(string));

			
			if (DelimitedKeywords.IndexOf(",") > -1)
			{
				string[] keywordsWQty = DelimitedKeywords.Split(',');
				foreach (string s in keywordsWQty)
				{
					string[] KeywordsArray = s.Split(' ');
					Keywords.Rows.Add(string.Empty, KeywordsArray[0]); // Category, Keyword
				}
			}
			else
			{
				if (DelimitedKeywords.IndexOf(" ") > -1)
				{
					string[] KeywordsArray = DelimitedKeywords.Split(' ');
					Keywords.Rows.Add(string.Empty, KeywordsArray[0]); // Category, Keyword
				}
			}


			return UseKeywords;

		}

		private string getNewUID(int orgUID)
		{
			int UID = -1;
			int newUID = -1;
			foreach (DataRow row in _ds.Tables[0].Rows) // Loop thru rows.
			{
				UID = Convert.ToInt32(row["UID"].ToString());
				if (UID > newUID)
				{
                    newUID = UID + 1;
				}
			}

            newUID++; // Add one more so to be < the largest value

			return newUID.ToString();
			   
		}

        private void SaveSplitColorKeywords()
        {
            int newSortOrder = -1;

            string topFileName = string.Empty;
            string bottomFileName = string.Empty;
            _BottomUID = string.Empty;

            DataView dv = new DataView(_ds.Tables[0]);
            dv.Sort = "SortOrder";

            foreach (DataRowView row in dv) // Loop thru rows.
            {
                if (row["UID"].ToString() == _UID)
                {
                    newSortOrder = Convert.ToInt32(row["SortOrder"].ToString()) + 2;
                }
                else
                {
                    // Reset Sort Order for sections below the Split Section
                    if (newSortOrder != -1)
                    {
                        newSortOrder++;
                        row["SortOrder"] = newSortOrder.ToString();
                    }
                }
            }

            _ds.Tables[0].AcceptChanges();

            dv = new DataView(_ds.Tables[0]);
            dv.Sort = "SortOrder";

            bool pageColumnExists = _ds.Tables[0].Columns.Contains("Page"); // Added 09.26.2018

            DataRow newRow = _ds.Tables[0].NewRow();

            int i = 0;

            
            int currentPageNo = 0;
            int lastPageNo = 0;

            foreach (DataRowView row in dv) // Loop thru rows.
            {
                if (row["UID"].ToString() == _UID)
                {
                    topFileName = string.Concat(_UID, ".rtf");

                    newRow["Parameter"] = "ManualSplit";

                    _BottomUID = getNewUID(Convert.ToInt32(_UID));
                    newRow["UID"] = _BottomUID;
                    bottomFileName = string.Concat(_BottomUID, ".rtf");

                    newRow["Parent"] = _UID;


                    int topLineEnd;
                    if (_column == 0)
                    {
                        topLineEnd = _row - 1;
                    }
                    else
                    {
                        topLineEnd = _row;
                    }

                    int topSectionLength = rtfTopSec.TextLength;
                    int bottomSectionLength = rtfBottomSec.TextLength;


                    // Bottom Section Length = Bottom Text Length
                    newRow["SectionLength"] = bottomSectionLength.ToString();

  

                    string topNumber = txtbTopSecNo.Text.Trim();
                    string topCaption = txtbTopSecTitle.Text.Trim();

                    newRow["Number"] = txtbBottomSecNo.Text.Trim();
                    newRow["Caption"] = txtbBottomTitle.Text.Trim();

                    int bottomSortOrder = Convert.ToInt32(row["SortOrder"].ToString()) + 1;
                    newRow["SortOrder"] = bottomSortOrder.ToString();

                    newRow["FileName"] = string.Concat(_BottomUID, ".rtf");


                    row["SectionLength"] = topSectionLength.ToString();
                     row["Number"] = topNumber;
                    row["Caption"] = topCaption;

                    if (pageColumnExists) // Added 09.26.2018
                    {
                        if (row["Page"] == null)
                            break;

                        if (row["Page"].ToString() == string.Empty) // Added 01.15.2019
                            break;

                        if (!DataFunctions.IsNumeric(row["Page"].ToString())) // Added 01.15.2019
                            break;

                        try // 02.09.2019 sometimes an error occurs while trying to convert the page number, unable to reproduce it, occured for Dan at Harris, so added the 'try'
                        {
                            currentPageNo = Convert.ToInt32(row["Page"].ToString()); //An Error is still occuring sometime on line of code, why?

                            lastPageNo = Convert.ToInt32(dv.Table.Rows[i]["Page"].ToString());
                        }
                        catch
                        {
                            break;
                        }

                        DocumentAnalysis da = new DocumentAnalysis();
                        int pageNo = da.FindTextPageSource(AppFolders.DocParsePage, currentPageNo, lastPageNo, rtfBottomSec.Text.Trim());

                        if (pageNo > 0)
                        {
                            newRow["Page"] = pageNo;
                        }
                        
                      }

                    break;

                }
                
                i++;
            }

            // Add New Row for Split
            _ds.Tables[0].Rows.Add(newRow);

            _ds.Tables[0].AcceptChanges();


       //     string ParseResultsFile = string.Concat(AppFolders.DocParsedSecXML, @"\ParseResults.xml");
            _genericDataManger.SaveDataXML(_ds, _ParseResultsFile);

            // Save Top Section of the Split Section
            rtfTopSec.SaveFile(Path.Combine(_DocParsedSecPath, topFileName));

            // Save Bottom Section of the Split Section
            rtfBottomSec.SaveFile(Path.Combine(_DocParsedSecPath, bottomFileName));

            if (_SearchType == AnalysisResultsType.SearchType.Keywords)
            {
                KeywordsMgr2 keywordMgr = new KeywordsMgr2();
                keywordMgr.FindKeywordsSec(_UID);
                keywordMgr.FindKeywordsSec(_BottomUID);
                keywordMgr = null;
            }

        }

        private void AdjustQueriesFound() // Added 12.11.2016
        {
            // Parse Sgements XML file
            //string parseSegFile = "ParseResults.xml";
            //parseSegFile = Path.Combine(AppFolders.DocParsedSecXML, parseSegFile);

            // Query XML file          
            string queryFile = "Queries.xml";
            queryFile = Path.Combine(AppFolders.DocParsedSecXML, queryFile);

            QueryFinder queriesFinder = new QueryFinder();
            queriesFinder.SplitSegQueries(_UID, _BottomUID, _ParseResultsFile, queryFile, AppFolders.DocParsedSec);
        }

        private void SaveSplit()
        {
            if (_SearchType == AnalysisResultsType.SearchType.Keywords)
            {
                if (File.Exists(Path.Combine(_XMLPath, "KeywordsFound2.xml")))
                {
                    SaveSplitColorKeywords();
                }
                else
                {
                    SaveSplitLegal();
                }
            }
            else
            {
                SaveSplitLegal();
            }
        }

        private void SaveSplitLegal()
		{

            int newSortOrder = -1;

            string topFileName = string.Empty;
            string bottomFileName = string.Empty;
            _BottomUID = string.Empty;

            DataView dv = new DataView(_ds.Tables[0]);
            dv.Sort = "SortOrder";

            foreach (DataRowView row in dv) // Loop thru rows.
            {
                if (row["UID"].ToString() == _UID)
                {
                    newSortOrder = Convert.ToInt32(row["SortOrder"].ToString()) + 2;
                }
                else
                {
                    // Reset Sort Order for sections below the Split Section
                    if (newSortOrder != -1)
                    {
                        newSortOrder++;
                        row["SortOrder"] = newSortOrder.ToString();
                    }
                }
            }

            _ds.Tables[0].AcceptChanges();

            dv = new DataView(_ds.Tables[0]);
            dv.Sort = "SortOrder";

            DataRow newRow = _ds.Tables[0].NewRow();

            bool pageColumnExists = _ds.Tables[0].Columns.Contains("Page"); // Added 09.26.2018

            int i = 0;

            //foreach (DataRow row in _ds.Tables[0].Rows) // Loop thru rows.
            foreach (DataRowView row in dv) // Loop thru rows.
            {
                if (row["UID"].ToString() == _UID)
                {

                    //    row["IndexEnd"] = _indexB - 1;

                    topFileName = string.Concat(_UID, ".rtf");

                    newRow["Parameter"] = "ManualSplit";

                    _BottomUID = getNewUID(Convert.ToInt32(_UID));
                    newRow["UID"] = _BottomUID;
                    bottomFileName = string.Concat(_BottomUID, ".rtf");

                    newRow["Parent"] = _UID;


                    int topLineEnd;
                    if (_column == 0)
                    {
                        topLineEnd = _row - 1;
                    }
                    else
                    {
                        topLineEnd = _row;
                    }

                    int topSectionLength = rtfTopSec.TextLength;
                    int bottomSectionLength = rtfBottomSec.TextLength;

                    // Bottom Line Start = Row selected to be split
                    int bottomLineStart = Convert.ToInt32(row["LineStart"].ToString()) + _row;
                    newRow["LineStart"] = bottomLineStart.ToString();

                    // Bottom Line End = Bottom Line Start + Bottom Row Count
                    int bottomRowCount = rtfBottomSec.GetLineCount();
                    //int bottomLineEnd = Convert.ToInt32(row["LineEnd"].ToString());
                    int bottomLineEnd = _row + bottomRowCount;
                    newRow["LineEnd"] = bottomLineEnd.ToString();

                    // Bottom Section Length = Bottom Text Length
                    newRow["SectionLength"] = bottomSectionLength.ToString();

                    // Bottom Column Start = Column selected at the Split
                    newRow["ColumnStart"] = _column.ToString();

                    // *** Bottom Start Index = Top Start Index + Top Text Length + 1
                    //int bottomIndexStart = Convert.ToInt32(row["IndexStart"].ToString()) + _indexB;
                    int bottomIndexStart = Convert.ToInt32(row["IndexStart"].ToString()) + topSectionLength + 1;
                    newRow["IndexStart"] = bottomIndexStart.ToString();
                    Debug.WriteLine("bottomIndexStart = " + bottomIndexStart.ToString());

                    // *** Bottom End Index = Top End Index before its value is changed
                    int bottomIndexEnd = Convert.ToInt32(row["IndexEnd"].ToString());
                    newRow["IndexEnd"] = bottomIndexEnd.ToString();
                    Debug.WriteLine("bottomIndexEnd = " + bottomIndexEnd.ToString());


                    // Top Index End = Top Index Start + Top Section Length
                    // *** int topIndexEnd = Convert.ToInt32(row["IndexEnd"].ToString()) - _indexB;
                    int topIndexEnd = Convert.ToInt32(row["IndexStart"].ToString()) + topSectionLength;
                    Debug.WriteLine("IndexStart = " + row["IndexStart"].ToString());
                    Debug.WriteLine("topIndexEnd = " + topIndexEnd.ToString());

                    // Top End Row = Top Row Start + Top RTF Line Count
                    int topLineStart = Convert.ToInt32(row["LineStart"].ToString());
                    row["LineEnd"] = topLineStart + rtfTopSec.GetLineCount();

                    string topNumber = txtbTopSecNo.Text.Trim();
                    string topCaption = txtbTopSecTitle.Text.Trim();

                    newRow["Number"] = txtbBottomSecNo.Text.Trim();
                    newRow["Caption"] = txtbBottomTitle.Text.Trim();

                    int bottomSortOrder = Convert.ToInt32(row["SortOrder"].ToString()) + 1;
                    newRow["SortOrder"] = bottomSortOrder.ToString();

                    newRow["FileName"] = string.Concat(_BottomUID, ".rtf");


                    row["SectionLength"] = topSectionLength.ToString();
                    row["IndexEnd"] = topIndexEnd.ToString();
                    row["Number"] = topNumber;
                    row["Caption"] = topCaption;

                    if (pageColumnExists) // Added 09.26.2018
                    {
                        if (row["Page"] == null)
                            break;

                        if (row["Page"].ToString() == string.Empty) // Added 01.15.2019
                            break;

                        if (!DataFunctions.IsNumeric(row["Page"].ToString())) // Added 01.15.2019
                            break;



                        int currentPageNo = Convert.ToInt32(row["Page"].ToString());

                        int lastPageNo = Convert.ToInt32(dv.Table.Rows[i]["Page"].ToString());

                        DocumentAnalysis da = new DocumentAnalysis();
                        int pageNo = da.FindTextPageSource(AppFolders.DocParsePage, currentPageNo, lastPageNo, rtfBottomSec.Text.Trim());

                        if (pageNo > 0)
                        {
                            newRow["Page"] = pageNo;
                        }

                    }

                    break;

                }

                i++;
            }

            // Add New Row for Split
            _ds.Tables[0].Rows.Add(newRow);

            _ds.Tables[0].AcceptChanges();

          //  string ParseResultsFile = string.Concat(AppFolders.DocParsedSecXML, @"\ParseResults.xml");
            _genericDataManger.SaveDataXML(_ds, _ParseResultsFile);

            // Save Top Section of the Split Section
            rtfTopSec.SaveFile(string.Concat(AppFolders.DocParsedSec, @"\", topFileName));

            // Save Bottom Section of the Split Section
            rtfBottomSec.SaveFile(string.Concat(AppFolders.DocParsedSec, @"\", bottomFileName));

            if (_SearchType == AnalysisResultsType.SearchType.Keywords)
                FindSaveKeywords(_UID, _BottomUID);
	
		}

        private void FindSaveKeywords(string topUID, string bottomUID)
        {
           // string ParseResultsFile = string.Concat(AppFolders.DocParsedSecXML, @"\ParseResults.xml");
            _ds = Files.LoadDatasetFromXml(_ParseResultsFile);
                       int qtyUIDFound = 0;
           string topKeywords = string.Empty;
           string bottomKeywords = string.Empty;

           foreach (DataRow row in _ds.Tables[0].Rows) // Loop thru rows.
           {
               if (row["UID"].ToString() == topUID)
               {
                   topKeywords = GetKeywords4Section(topUID);
                   row["Keywords"] = topKeywords;

                   qtyUIDFound++;
               }

               if (row["UID"].ToString() == bottomUID)
               {
                   bottomKeywords = GetKeywords4Section(bottomUID);
                   row["Keywords"] = bottomKeywords;

                   qtyUIDFound++;
               }

               if (qtyUIDFound == 2) // Found both the Top and Bottom split sections
                   break;
           }

           _ds.Tables[0].AcceptChanges();

           GenericDataManger gdManager = new GenericDataManger();

           gdManager.SaveDataXML(_ds, _ParseResultsFile);

        }

		private void butBack_Click(object sender, EventArgs e)
		{
			panOrgSec.Visible = true;
			butSplit.Visible = true;

			panTopSec.Visible = false;
			panBottomSec.Visible = false;
			butBack.Visible = false;
			butOK.Visible = false;
            lblMesssage.Text = Message1;
		}

		private void butOK_Click(object sender, EventArgs e)
		{
            if (rtfBottomSec.Text == string.Empty) // Added 07.29.2017
            {
                MessageBox.Show("Set Split Location with cursor and click the Split button.", "No Spliter Text Defined", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
			SaveSplit();

         //   AdjustQueriesFound(); 

			this.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.Close();
		}

        private string GetKeywords4Section(string UID)
        {
          //  string ParseResultsFile = string.Concat(AppFolders.DocParsedSecXML, @"\", "ParseResults.xml");
            if (!File.Exists(_ParseResultsFile))
                return string.Empty;

            string KeywordsFoundFile = Path.Combine(_XMLPath, "KeywordsFound.xml");
            if (!File.Exists(KeywordsFoundFile))
                return string.Empty;


           // GenericDataManger genDataMgr = new GenericDataManger();

            DataSet dsParseResultsFile = Files.LoadDatasetFromXml(_ParseResultsFile);
            DataSet dsKeywordsFoundFile = Files.LoadDatasetFromXml(KeywordsFoundFile);

            DataView view = dsKeywordsFoundFile.Tables[0].DefaultView;
            view.Sort = "Index";

            StringBuilder sb = new StringBuilder();

            foreach (DataRow row in dsParseResultsFile.Tables[0].Rows)
            {
                if (row["UID"].ToString() == UID)
                {
                    int indexStart = (int)row["IndexStart"] - 1;
                    int indexEnd = (int)row["IndexEnd"] + 1;


                    int index;
                    string keyword = string.Empty;
                    foreach (DataRowView rowKw in view)
                    {
                        index = (int)rowKw["Index"];
                        keyword = rowKw["Keyword"].ToString();
                        if (index > indexStart && index < indexEnd)
                        {
                            sb = AppendKeywordsSB(sb, keyword);
                        }
                    }

                    return sb.ToString();
                }

            }

            return string.Empty;

        }

        private StringBuilder AppendKeywordsSB(StringBuilder sb, string Keyword)
        {
            string sContent = null;
            int intLoc = 0;

            if (sb.Length == 0)
            {
                sb.Append(Keyword);
                sb.Append(" ");
                sb.Append("[1]");
                return sb;
            }

            sContent = sb.ToString();

            intLoc = sContent.IndexOf(Keyword, 0);
            //>> Keyword was not found in String Builder, so add it
            if (intLoc < 0)
            {
                sb.Append(", ");
                sb.Append(Keyword);
                sb.Append(" ");
                sb.Append("[1]");
                return sb;
            }


            string sLeft = null;
            int intLoc2 = 0;
            string sMid = null;
            string sRight = null;
            int intNumber = 0;
            int intLoc3 = 0;

            sLeft = sContent.Substring(0, intLoc + Keyword.Length + 1);
            intLoc2 = sContent.IndexOf("]", intLoc);
            sRight = sContent.Substring(intLoc2 + 1);
            intLoc3 = sContent.IndexOf("[", intLoc);

            sMid = sContent.Substring(intLoc3 + 1, intLoc2 - intLoc3 - 1);
            sMid = sMid.Trim();

            if (DataFunctions.IsNumeric(sMid) == true)
            {
                intNumber = Convert.ToInt32(sMid);
                intNumber = intNumber + 1;
                //>> Increment Qty 
                sMid = " [" + intNumber.ToString() + "]";
                //>> Reformat
                sb.Clear();
                //>> Rebuild
                sb.Append(sLeft);
                sb.Append(sMid);
                sb.Append(sRight);
                return sb;
            }

            return sb;
            //>> Should never occur

        }

        private void butCancel_Click(object sender, EventArgs e) // Added 11.02.213
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.Close();
        }

        private void butHelp_Click(object sender, EventArgs e)
        {
            Process.Start(@"http://www.atebionllc.com/documentation/da/#!/split");
        }

		
	}
}
