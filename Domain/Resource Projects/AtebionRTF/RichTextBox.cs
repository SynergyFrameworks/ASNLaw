using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.IO;
using System.Drawing.Printing;
//using System.Windows.
using HWND = System.IntPtr;
//using Microsoft.Win32;  


namespace Atebion.RTFBox
{
	public partial class RichTextBox : System.Windows.Forms.RichTextBox
	{
		public RichTextBox()
		{
			InitializeComponent();
		}
		const int SB_VERT = 1;
		const int EM_SETSCROLLPOS = 0x0400 + 222;

		[DllImport("user32", CharSet = CharSet.Auto)]
		public static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos);

		[DllImport("user32", CharSet = CharSet.Auto)]
		public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, POINT lParam);

		[StructLayout(LayoutKind.Sequential)]
		public struct POINT 
		{
			public int x;
			public int y;
		}

		//[StructLayout(LayoutKind.Sequential)]
		//public class POINT
		//{
		//    public int x;
		//    public int y;

		//    public POINT()
		//    {
		//    }

		//    public POINT(int x, int y)
		//    {
		//        this.x = x;
		//        this.y = y;
		//    }
	   // }


        public bool Print()
        {
            bool returnValue = false;

            PrintDialog printDialog1 = new PrintDialog();
            System.Drawing.Printing.PrintDocument DocumentToPrint = new System.Drawing.Printing.PrintDocument();
            printDialog1.Document = DocumentToPrint;
            if (printDialog1.ShowDialog() == DialogResult.OK)
            {
                StringReader Reader = new StringReader(this.Text);
                DocumentToPrint.Print();
                returnValue = true;
            }

            return returnValue;

        }

		public unsafe void GoToLineColumn3(int Line, int Column)
		{

			bool bWordWrap = this.WordWrap;
			this.WordWrap = false; // WordWarp must be off for GoToLineColumn function properly

			// Example -- scroll the RTB so the bottom of the text is always visible. 
			int min, max;
			GetScrollRange(Handle, SB_VERT, out min, out max);
			//	SendMessage(RTB.Handle, EM_SETSCROLLPOS, 0, new POINT(0, max - RTB.Height)); 

		  //  SendMessage(RTB.Handle, EM_SETSCROLLPOS, 0, new POINT(Column, Line));

			Win32.POINT res = new Win32.POINT();
			res.x = Line;
			res.y = Column;
			IntPtr ptr = new IntPtr(&res);

			Win32.SendMessage(Handle, Win32.EM_SETSCROLLPOS, 0, ptr);

			this.ScrollToCaret();

			this.WordWrap = bWordWrap; // Preserves the previous settings

		}




		public void GoToLineColumn2(int Line, int Column)
		{
			//Cursor.Current = Cursors.WaitCursor;

			bool bWordWrap = this.WordWrap;
			this.WordWrap = false; // WordWarp must be off for GoToLineColumn function properly

			int Offset = 0;
			int i = 0;

			foreach (String L in this.Lines)
			{
				if (i < Line - 1)
				{
					Offset += L.Length + 1;
				}
				else
				{
					break;
				}

				i++;
			}

			this.Select(Offset + Column - 1, 0);

			this.ScrollToCaret();

			this.WordWrap = bWordWrap; // Preserves the previous settings

			//Cursor.Current = Cursors.Arrow;
		}

        public void SelectFromIndex(int iIndexStart, int iLength)
        {
            this.Focus();

            this.Select(iIndexStart, iLength);
        }


		public void GoToLineColumn(int Line, int Column, int SelLenght)
		{

			// Corrections:
			int yLine = Line + 1;
			int xColumn = Column;

			bool bWordWrap = this.WordWrap;
			this.WordWrap = false;
			//Application.DoEvents();

			int offset = 0;

			for (int i = 0; i < yLine - 1 && i < this.Lines.Length; i++)
			{
			   if (yLine == CurrentLine())
					 break;

				offset += this.Lines[i].Length + 1;
				//offset += RTB.Lines[i].Length + CursorPosition.GetCorrection(RTB);

			}

			this.Focus();

		   // int nCol_1 = offset + xColumn + 1; // Removed 02.24.2011
			int nCol_1 = offset + xColumn + 1; // Added 02.24.2011
			this.Select(nCol_1, SelLenght); // Removed 02.24.2011

			return; // added 02.24.2011

			// --- added 02.24.2011 ----

		 //   int nCol_1 = 0;


			int curColumn = 0; // added 02.24.2011
			curColumn = CurrentColumn(); // added 02/24/2011
			int nDelta = 0;
			if (curColumn < xColumn)
			{
				nCol_1 = (xColumn - curColumn) + offset;
				this.Select(nCol_1, SelLenght);
			}
			else
			{
				nCol_1 = offset - (curColumn - xColumn);
				this.Select(nCol_1, SelLenght);
			}

			

			// ---------------------------




			//int nDelta = 0; // Correction factor

			//			// Row Correction
			//			int curRow = 0;
			//			while (CurrentLine(RTB) > Line)
			//			{
			//				curRow = CurrentLine(RTB);
			//				nDelta = curRow - Line;
			// 				RTB.Select(curRow - nDelta, 0);
			//				nCol_1 = nCol_1 - RTB.Lines[curRow - nDelta].Length;
			//				RTB.Select(nCol_1, SelLenght);
			//			}


			// Column Correction
			//int curColumn = 0;
			//GetCorrection
			nDelta = 0;

			curColumn = CurrentColumn(); // added 02/24/2011
			while (xColumn != CurrentColumn()) // Was an If statement, but w/ long documents was wrong
			{
				curColumn = CurrentColumn();
				if (xColumn > curColumn)
				{
					nDelta = (xColumn - curColumn);
					this.Select(nCol_1 + nDelta, SelLenght);
					if (xColumn != CurrentColumn())
					{
						this.Select((nCol_1 + nDelta) + 1, SelLenght);
					}
				}
				else // xColumn < curColumn
				{
					nDelta = (curColumn - xColumn);
					this.Select(nCol_1 - nDelta, SelLenght);
					if (xColumn != CurrentColumn())
					{
						this.Select((nCol_1 - nDelta) - 1, SelLenght);
					}
				}
			}

			this.WordWrap = bWordWrap;
			
		}

#region GoTo LineColumn 4
		// ------------- GoTo LineColumn 4 -------------------------

		[System.Runtime.InteropServices.DllImport("user32")]
		private static extern int GetCaretPos(ref Point lpPoint);

		private  int GetCorrection(int index) 
		{ 
			Point pt1 = Point.Empty; 
			GetCaretPos(ref pt1); 
			Point pt2 = GetPositionFromCharIndex(index); 
 
			if ( pt1 != pt2 ) 
				return 1; 
			else 
				return 0; 
		} 
 
		private  int getLine4(int intLine ) 
		{
			int correction = GetCorrection(intLine);
			return GetLineFromCharIndex(intLine) - correction + 1; 
		} 
 
		private  int getColumn4(int intColumn ) 
		{
			int correction = GetCorrection(intColumn);
			Point p = GetPositionFromCharIndex(intColumn - correction); 
 
			 if ( p.X == 1 ) 
				 return 1; 
 
			 p.X = 0; 
			 int index2 = GetCharIndexFromPosition( p );

			 int col = intColumn - index2 + 1; 
 
			 return col; 
		 }

		public void GoToLineColumn4(int intLine, int intColumn)
		{
			bool bWordWrap = this.WordWrap;
			this.WordWrap = false;
			this.Focus();
			getLine4(intLine);
			getColumn4(intColumn);

			this.WordWrap = bWordWrap;
		}
	


		//---------------------------------------------------------
#endregion


		// New as of 03/12/06

		public int CurrentColumn()
		{
			return CursorPosition.Column(this);
		}

		public int CurrentLine()
		{
			return CursorPosition.Line(this);
		}

		public int CurrentPosition()
		{
			return this.SelectionStart;
		}

		public int SelectionEnd()
		{
			return this.SelectionStart + this.SelectionLength;
		}


		







		
		#region "Property: SelectionBackColor"
		[StructLayout(LayoutKind.Sequential)]
		private struct CharFormat2
		{
			public Int32 cbSize;
			public Int32 dwMask;
			public Int32 dwEffects;
			public Int32 yHeight;
			public Int32 yOffset;
			public Int32 crTextColor;
			public byte bCharSet;
			public byte bPitchAndFamily;
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string szFaceName;
			public Int16 wWeight;
			public Int16 sSpacing;
			public Int32 crBackColor;
			public Int32 lcid;
			public Int32 dwReserved;
			public Int16 sStyle;
			public Int16 wKerning;
			public byte bUnderlineType;
			public byte bAnimation;
			public byte bRevAuthor;
			public byte bReserved1;
		}

		//private const  LF_FACESIZE = 32;
		private const  int CFM_BACKCOLOR = 0x4000000;
		private const  int CFE_AUTOBACKCOLOR = CFM_BACKCOLOR;
		private const  int WM_USER = 0x400;
		private const  int EM_SETCHARFORMAT = (WM_USER + 68);
		//private const  EM_SETBKGNDCOLOR = (WM_USER + 67);
		private const  int EM_GETCHARFORMAT = (WM_USER + 58);
		//private const  WM_SETTEXT = 0xc;

		private const int SCF_SELECTION = 0x01; 

		[DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
		private static extern bool SendMessage(IntPtr hWnd, int Msg, int wParam, ref CharFormat2 lParam);

		#region "Keywords"

		string[] sKeywordFound;
		public string[] KeywordsFound {
			get { return sKeywordFound; }
		}


		public void ClearKeywords()
		{
			sKeywordFound = new string[1];
		}


		#endregion

		// Here we do the magic...
		//public Color SelectionBackColor {
		//    get {
		//        // We need to ask the RTB for the backcolor of the current selection.
		//        // This is done using SendMessage with a format structure which the RTB will fill in for us.
		//        IntPtr HWND = this.Handle;
		//        // Force the creation of the window handle...
		//        CharFormat2 Format = new CharFormat2();
		//        Format.dwMask = CFM_BACKCOLOR;
		//        Format.cbSize = Marshal.SizeOf(Format);
		//        SendMessage(this.Handle, EM_GETCHARFORMAT, SCF_SELECTION, ref Format);
		//        return ColorTranslator.FromOle(Format.crBackColor);
		//    }
		//    set {
		//        // Here we do relatively the same thing as in Get, but we are telling the RTB to set
		//        // the color this time instead of returning it to us.
		//        IntPtr HWND = this.Handle;
		//        // Force the creation of the window handle...
		//        CharFormat2 Format = new CharFormat2();
		//        Format.crBackColor = ColorTranslator.ToOle(value);
		//        Format.dwMask = CFM_BACKCOLOR;
		//        Format.cbSize = Marshal.SizeOf(Format);
		//        SendMessage(this.Handle, EM_SETCHARFORMAT, SCF_SELECTION, ref Format);
		//    }
		//}



		#endregion
		#region "Proc: ClearBackColor"
		#region "ScrollBarTypes"
		private enum ScrollBarTypes
		{
			SB_HORZ = 0,
			SB_VERT = 1,
			SB_CTL = 2,
			SB_BOTH = 3
		}
		#endregion
		#region "SrollBarInfoFlags"
		private enum ScrollBarInfoFlags
		{
			SIF_RANGE = 0x1,
			SIF_PAGE = 0x2,
			SIF_POS = 0x4,
			SIF_DISABLENOSCROLL = 0x8,
			SIF_TRACKPOS = 0x10,
			SIF_ALL = (SIF_RANGE | SIF_PAGE | SIF_POS | SIF_TRACKPOS)
		}
		#endregion

		public unsafe void ClearBackColor(bool ClearAll = true)
		{
			IntPtr HWND = this.Handle;
			// Force the creation of the window handle...

			LockWindowUpdate(this.Handle);
			// Lock drawing...
			this.SuspendLayout();
			int ScrollPosVert = this.GetScrollBarPos(this.Handle, ScrollBarTypes.SB_VERT);
			int ScrollPosHoriz = this.GetScrollBarPos(this.Handle, ScrollBarTypes.SB_HORZ);
			int SelStart = this.SelectionStart;
			int SelLength = this.SelectionLength;

			if (ClearAll)
				this.SelectAll();
			// Should we clear everything or just use the current selection?
			CharFormat2 Format = new CharFormat2();
			Format.crBackColor = -1;
			Format.dwMask = CFM_BACKCOLOR;
			Format.dwEffects = CFE_AUTOBACKCOLOR;
			// Clears the backcolor
			Format.cbSize = Marshal.SizeOf(Format);
			SendMessage(this.Handle, EM_SETCHARFORMAT, SCF_SELECTION, ref Format);

			// Return the previous values...
			this.SelectionStart = SelStart;
			this.SelectionLength = SelLength;
			//SendMessage(this.Handle, EM_SETSCROLLPOS, 0, new RichTextBox.POINT(ScrollPosHoriz, ScrollPosVert));
		  //  Point rtfPoint = new Point(ScrollPosHoriz, ScrollPosVert);
		   // SendMessage(this.Handle, EM_SETSCROLLPOS, 0, new POINT(ScrollPosHoriz, ScrollPosVert));

			Win32.POINT res = new Win32.POINT();
			res.x = ScrollPosHoriz;
			res.y = ScrollPosVert;
			IntPtr ptr = new IntPtr(&res);

			Win32.SendMessage(Handle, Win32.EM_SETSCROLLPOS, 0, ptr);
			// SendMessage(this.Handle, EM_SETSCROLLPOS, (int) 0, ref rtfPoint); 

			this.ResumeLayout();
			LockWindowUpdate(IntPtr.Zero);
			// Unlock drawing...
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct SCROLLINFO
		{
				// UINT cbSize; 
			public int cbSize;
				// UINT fMask; 
			public ScrollBarInfoFlags fMask;
				//int  nMin; 
			public int nMin;
				//int  nMax; 
			public int nMax;
				//UINT nPage;  
			public int nPage;
				// int  nPos; 
			public int nPos;
				// int  nTrackPos; 
			public int nTrackPos;
		}
		[DllImport("User32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

		private static extern bool GetScrollInfo(IntPtr hWnd, ScrollBarTypes fnBar, ref SCROLLINFO lpsi);
		private int GetScrollBarPos(IntPtr hWnd, ScrollBarTypes BarType)
		{
			SCROLLINFO INFO = default(SCROLLINFO);
			INFO.fMask = ScrollBarInfoFlags.SIF_POS;
			INFO.cbSize = Marshal.SizeOf(INFO);
			GetScrollInfo(hWnd, BarType, ref INFO);
			return INFO.nPos;
		}
		#endregion

		#region "Proc: GoToRowCol"

		public void GoToRowCol(int yRow, int xCol, int SelLength = 0)
		{
			//RichTextExCs.RichTextBoxFunctions objTemp = default(RichTextExCs.RichTextBoxFunctions);
			//objTemp = new RichTextExCs.RichTextBoxFunctions();


			//RichTextExCs.RichTextBoxFunctions objRT = new RichTextExCs.RichTextBoxFunctions();

			//objRT.GoToLineColumn(Me, yRow, xCol)
			//objRT.GoToLineColumn2(Me, yRow, xCol)
			this.GoToLineColumn(yRow, xCol, SelLength);

		}

		//public void ShowToolTip(string sText)
		//{
		//    //RichTextExCs.TextBoxBallonTip objRT = new RichTextExCs.TextBoxBallonTip();
		//    //this.ShowBalloonTip(this, "Test", sText);
		//    //  objRT.ShowBalloonTip(Me, sText, "Test", RichTextExCs.TextBoxBallonTip.BallonTipIcons.Info)
		//}

		#endregion

		#region "Function: Highlight"

		public DataSet GetDataset_KeywordsFound()
		{
			// Create a new DataTable.
			DataTable table = new DataTable("KeyFound");

			// Declare variables for DataColumn and DataRow objects.
			DataColumn column = null;

			// Create new DataColumn, set DataType, ColumnName 
			// and add to DataTable.    
			column = new DataColumn();
			column.DataType = System.Type.GetType("System.String");
			column.ColumnName = "Keyword";
			column.ReadOnly = false;
			column.Unique = false;

			// Add the Column to the DataColumnCollection.
			table.Columns.Add(column);

			//' Create new DataColumn, set DataType, ColumnName 
			//' and add to DataTable.    
			//column = New DataColumn()
			//column.DataType = System.Type.GetType("System.String")
			//column.ColumnName = "Category"
			//column.ReadOnly = False
			//column.Unique = False

			//' Add the Column to the DataColumnCollection.
			//table.Columns.Add(column)

			// Create new DataColumn, set DataType, ColumnName 
			// and add to DataTable.    
			column = new DataColumn();
			column.DataType = System.Type.GetType("System.Int32");
			column.ColumnName = "Line";
			column.ReadOnly = false;
			column.Unique = false;

			// Add the Column to the DataColumnCollection.
			table.Columns.Add(column);

			// Create new DataColumn, set DataType, ColumnName 
			// and add to DataTable.    
			column = new DataColumn();
			column.DataType = System.Type.GetType("System.Int32");
			column.ColumnName = "Column";
			column.ReadOnly = false;
			column.Unique = false;

			// Add the Column to the DataColumnCollection.
			table.Columns.Add(column);

			// Create new DataColumn, set DataType, ColumnName 
			// and add to DataTable.    
			column = new DataColumn();
			column.DataType = System.Type.GetType("System.Int32");
			column.ColumnName = "Index";
			column.ReadOnly = false;
			column.Unique = false;

			// Add the Column to the DataColumnCollection.
			table.Columns.Add(column);


			// Make the ID column the primary key column. -- Commented out to fix compund keywords 6.29.2014
            //DataColumn[] PrimaryKeyColumns = new DataColumn[1];
            //PrimaryKeyColumns[0] = table.Columns["Index"];
            //table.PrimaryKey = PrimaryKeyColumns;

            // Create new DataColumn, set DataType, ColumnName 
            // and add to DataTable.    
            column = new DataColumn();
            column.DataType = System.Type.GetType("System.String");
            column.ColumnName = "FoundInPSections"; // Keyword found in Parsed Sections -- Comma delimited string
            column.ReadOnly = false;
            column.Unique = false;

            // Add the Column to the DataColumnCollection.
            table.Columns.Add(column);

			// Instantiate the DataSet variable.
			DataSet dataSet = null;
			dataSet = new DataSet();

			// Add the new DataTable to the DataSet.
			//     dataSet.Tables.Add(table)

			// Populate the dataset
			string keywordInfor = null;
			string[] keywordInforArray = null;
			DataRow dsNewRow = null;
			for (int i = 0; i < (sKeywordFound.GetUpperBound(0) + 1); i++) {
				if (sKeywordFound[i] != null) {
					keywordInfor = sKeywordFound[i].ToString();
					keywordInforArray = keywordInfor.Split(new char[] { '|' });
					//String Def.: Keyword|row|column|index


					// dsNewRow = dataSet.Tables("table").NewRow()
					dsNewRow = table.NewRow();
					dsNewRow["Keyword"] = keywordInforArray[0];
					dsNewRow["Line"] = keywordInforArray[1];
					dsNewRow["Column"] = keywordInforArray[2];
					dsNewRow["Index"] = keywordInforArray[3];

					//  dataSet.Tables("table").Rows.Add(dsNewRow)
					table.Rows.Add(dsNewRow); // Index error need to check 6.29.2013

                    Application.DoEvents();
				}
			}

			// Add the new DataTable to the DataSet.
			dataSet.Tables.Add(table);

			//Return dataset
			return dataSet;

		}

		[DllImport("user32.dll", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

		private static extern bool LockWindowUpdate(IntPtr hWndLock);
		public unsafe void Highlight(string FindWhat, Color Highlight, bool MatchCase, bool MatchWholeWord)
		{        
			
			LockWindowUpdate(this.Handle);
			// Lock drawing...
			this.SuspendLayout();
			int ScrollPosVert = this.GetScrollBarPos(this.Handle, ScrollBarTypes.SB_VERT);
			int ScrollPosHoriz = this.GetScrollBarPos(this.Handle, ScrollBarTypes.SB_HORZ);
			int SelStart = this.SelectionStart;
			int SelLength = this.SelectionLength;

			int StartFrom = 0;
			int Length = FindWhat.Length;
			RichTextBoxFinds Finds = default(RichTextBoxFinds);
			// Setup the flags for searching.
			if (MatchCase)
				Finds = Finds | RichTextBoxFinds.MatchCase;
			if (MatchWholeWord)
				Finds = Finds | RichTextBoxFinds.WholeWord;

			int row = 0;
			int column = 0;
			int index = 0;
			bool flag = false;

			//RichTextExCs.RichTextBoxFunctions objRT = new RichTextExCs.RichTextBoxFunctions();

			//          Dim dsKeyword As DataSet 'New 12.19.2010

			//          dsKeyword = CreateDataset_KeywordsFound()

			// Do the search.
			flag = true;
			//>> Me.Find(FindWhat, StartFrom, Finds) > -1
			while (flag == true) {
				index = this.Find(FindWhat, StartFrom, Finds);
				if (index < 0) {
					break; // TODO: might not be correct. Was : Exit While
				}
				this.SelectionBackColor = Highlight;

				column = this.GetCurrentColumn();
				row = this.GetCurrentLine();
				//row = Me.GetCurrentLine
				//column = Me.SelectionStart
				if (sKeywordFound == null) {
					sKeywordFound = new string[1];
				} else {
					Array.Resize(ref sKeywordFound, sKeywordFound.GetUpperBound(0) + 2);
				}
				sKeywordFound[sKeywordFound.GetUpperBound(0)] = FindWhat + "|" + row.ToString() + "|" + column.ToString() + "|" + index.ToString();

				// Select Keyword
				StartFrom = this.SelectionStart + this.SelectionLength;
				// Continue after the one we found..
			}

			// Return the previous values...
			this.SelectionStart = SelStart;
			this.SelectionLength = SelLength;

			Win32.POINT res = new Win32.POINT();
			res.x = ScrollPosHoriz;
			res.y = ScrollPosVert;
			IntPtr ptr = new IntPtr(&res);

			Win32.SendMessage(Handle, Win32.EM_GETSCROLLPOS, 0, ptr);

			LockWindowUpdate(IntPtr.Zero);
 
			// Unlock drawing...
		}
		#endregion
		#region "Proc: ScrollToBottom"
		#region "Scroller Flags"
		private enum EMFlags
		{
			 EM_SETSCROLLPOS = 0x400 + 222
		}
		#endregion
		#region "ScrollBarFlags"
		private enum ScrollBarFlags
		{
			SBS_HORZ = 0x0,
			SBS_VERT = 0x1,
			SBS_TOPALIGN = 0x2,
			SBS_LEFTALIGN = 0x2,
			SBS_BOTTOMALIGN = 0x4,
			SBS_RIGHTALIGN = 0x4,
			SBS_SIZEBOXTOPLEFTALIGN = 0x2,
			SBS_SIZEBOXBOTTOMRIGHTALIGN = 0x4,
			SBS_SIZEBOX = 0x8,
			SBS_SIZEGRIP = 0x10
		}
		#endregion
		//#region "Structure: POINT"
		//[StructLayout(LayoutKind.Sequential)]
		//private class POINT
		//{
		//    public int x;

		//    public int y;
		//    public POINT()
		//    {
		//    }

		//    public POINT(int x, int y)
		//    {
		//        this.x = x;
		//        this.y = y;
		//    }
		//}
		//[DllImport("User32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]
		//#endregion

		//private static extern bool GetScrollRange(IntPtr hWnd, int nBar, ref int lpMinPos, ref int lpMaxPos);
		//[DllImport("user32", EntryPoint = "SendMessageA", CharSet = CharSet.Auto, SetLastError = true, ExactSpelling = true)]
		//private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, RichTextBox.POINT lParam);
		//public void ScrollToBottom()
		//{
		//    int Min = 0;
		//    int Max = 0;
		//    GetScrollRange(this.Handle, ScrollBarFlags.SBS_VERT, ref Min, ref Max);
		//    SendMessage(this.Handle, EMFlags.EM_SETSCROLLPOS, 0, new RichTextBox.POINT(0, Max - this.Height));
		//}



		#endregion

		//private const  EM_LINELENGTH = 0xc1;
		//private const  EM_GETLINE = 0xc4;
		private const  int EM_GETLINECOUNT = 0xba;
		private const  int EM_LINEFROMCHAR = 0xc9;
		private const  int EM_LINEINDEX = 0xbb;
		private const  int EM_GETFIRSTVISIBLELINE = 0xce;
		//private const  EM_LINESCROLL = 0xb6;
		//private const  EM_POSFROMCHAR = 0xd6;
		//private const  EM_GETSEL = 0xb0;
		//private const  EM_SETSEL = 0xb1;
		//private const  EM_CANUNDO = 0xc6;
		//private const  EM_SETTEXTEX = (WM_USER + 97);
		//private const  EM_GETTEXTEX = (WM_USER + 94);
		//private const  EM_GETSELTEXT = (WM_USER + 62);
		//private const  EM_REPLACESEL = 0xc2;
		//private const  EM_STREAMOUT = (WM_USER + 74);
		//private const  EM_STREAMIN = (WM_USER + 73);
		//private const  EM_SCROLLCARET = 0xb7;
		//private const  EM_FINDTEXTEX = (WM_USER + 79);
		//private const  EM_CHARFROMPOS = 0xd7;
		//private const  EM_GETRECT = 0xb2;

		//private const  EM_SETRECT = 0xb3;


		public int GetLine(int charindex)
		{
			Message m = Message.Create(this.Handle, EM_LINEFROMCHAR, new IntPtr(charindex), new IntPtr(0));
			base.WndProc(ref m);
			return (int) m.Result;
		}

		public int GetLineStart(int line)
		{
			Message m = Message.Create(this.Handle, EM_LINEINDEX, new IntPtr(line), new IntPtr(0));
			base.WndProc(ref m);
			return (int) m.Result;
		}

		public int GetLineLength(int line)
		{
			//Dim m As Message = Message.Create(Me.Handle, EM_LINELENGTH, New IntPtr(line), New IntPtr(0))
			//MyBase.WndProc(m)
			//Return m.Result.ToInt32

			//>> Problem with the above code 
            try // added try 03.11.2014
            {
                return this.Lines[line].Length;
            }
            catch
            {
                return 0;
            }
		}

		public int GetLineCount()
		{
			Message m = Message.Create(this.Handle, EM_GETLINECOUNT, new IntPtr(0), new IntPtr(0));
			base.WndProc(ref m);
			return (int) m.Result;
		}

		public int GetCurrentLineStart()
		{
			Message m = Message.Create(this.Handle, EM_LINEINDEX, new IntPtr(-1), new IntPtr(0));
			base.WndProc(ref m);
			return (int) m.Result;
		}

		public string GetCurrentLineText()
		{
			string functionReturnValue = null;
			int line = 0;
				int x = 0;

			line = GetCurrentLine();
			//lineLength = GetLineLength(line)
			if (line > this.Lines.Count() - 1) // If Exceeded qty of lines in array
				return string.Empty;

			x = this.Find(this.Lines[line]);
			if (x < 0) {
				functionReturnValue = "";
				return functionReturnValue;
			}
			this.Select(x, this.Lines[line].Length);
			functionReturnValue = this.SelectedText; 
			return functionReturnValue;

		}

		public int GetCurrentColumn()
		{
			return CurrentColumn();
		}

		public int GetCurrentLine()
		{
			Message m = Message.Create(this.Handle, EM_LINEFROMCHAR, new IntPtr(-1), new IntPtr(0));
			base.WndProc(ref m);
			return (int) m.Result;
		}

		public int GetFirstVisibleLine()
		{
			Message m = Message.Create(this.Handle, EM_GETFIRSTVISIBLELINE, new IntPtr(0), new IntPtr(0));
			base.WndProc(ref m);
			return (int) m.Result;
		}

		//Public Function GetLastVisibleLine() As Integer
		//    '  Return Me.GetLine(Me.GetCharIndexFromPosition(New Point(0, Me.Height - iHortBorderWidth)))
		//    '   Return Me.GetLine(Me.GetCharIndexFromPosition(New Point(0, Me.Height)))

		//End Function



internal class CursorPosition
		{
			[System.Runtime.InteropServices.DllImport("user32")]
			public static extern int GetCaretPos(ref Point lpPoint);

			public static int GetCorrection(RichTextBox cRTB)
			{
				int index = cRTB.SelectionStart;
				Point pt1 = Point.Empty;
				GetCaretPos(ref pt1);
				Point pt2 = cRTB.GetPositionFromCharIndex(index);

				if (pt1 != pt2)
					return 1;
				else
					return 0;
			}

			public static int Line(RichTextBox cRTB)
			{
				int index = cRTB.SelectionStart;
				int correction = GetCorrection(cRTB);
				return cRTB.GetLineFromCharIndex(index) - correction + 1;
			}

			public static int Column(RichTextBox cRTB)
			{

				int index1 = cRTB.SelectionStart;

				int correction = GetCorrection(cRTB);
				Point p = cRTB.GetPositionFromCharIndex(index1 - correction);

				if (p.X == 1)
					return 1;

				p.X = 0;
				int index2 = cRTB.GetCharIndexFromPosition(p);

				int col = index1 - index2 + 1;
				return col;
			}
		}
	}


}
