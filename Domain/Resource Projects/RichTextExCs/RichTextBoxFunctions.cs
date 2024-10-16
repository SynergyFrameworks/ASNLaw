using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Atebion.RichTextExtra
{
	/// <summary>
	/// Summary description for Class1.
	/// </summary>
	
	public class RichTextBoxFunctions
	{
		public RichTextBoxFunctions()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		//		public void ShowBalloonTip2(RichTextBox textBox, string text)
		//		{
		//			RichTextExCs.TextBoxBallonTip.ShowBalloonTip(textBox, text);			
		//		}

		
			const int SB_VERT = 1; 
			const int EM_SETSCROLLPOS = 0x0400 + 222; 

			[DllImport("user32", CharSet=CharSet.Auto)] 
			public static extern bool GetScrollRange(IntPtr hWnd, int nBar, out int lpMinPos, out int lpMaxPos); 

			[DllImport("user32", CharSet=CharSet.Auto)] 
			public static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, POINT lParam); 

			[StructLayout(LayoutKind.Sequential)] 
			public class POINT 
			{ 
				public int x; 
				public int y; 

				public POINT() 
				{ 
				} 

				public POINT(int x, int y) 
				{ 
					this.x = x; 
					this.y = y; 
				} 
			} 

		public void GoToLineColumn3(RichTextBox RTB, int Line, int Column)
		{
				
			// Example -- scroll the RTB so the bottom of the text is always visible. 
			int min, max; 
			GetScrollRange(RTB.Handle, SB_VERT, out min, out max); 
		//	SendMessage(RTB.Handle, EM_SETSCROLLPOS, 0, new POINT(0, max - RTB.Height)); 

			SendMessage(RTB.Handle, EM_SETSCROLLPOS, 0, new POINT(Column, Line)); 
		}



		
		public void GoToLineColumn2(RichTextBox RTB, int Line, int Column)
		{
			//Cursor.Current = Cursors.WaitCursor;

			int Offset = 0;
			int i = 0;

			foreach (String L in RTB.Lines)
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

			RTB.Select(Offset + Column - 1, 0);

			//Cursor.Current = Cursors.Arrow;
		}

		public void GoToLineColumn(RichTextBox RTB, int Line, int Column, int SelLenght)
		{ 
 
			// Corrections:
			int yLine = Line + 1;
			int xColumn = Column; 

			bool bWordWrap = RTB.WordWrap;
			RTB.WordWrap = false;
			Application.DoEvents();

			int offset = 0; 
 
			for (int i = 0; i < yLine - 1 && i < RTB.Lines.Length; i++) 
 
			{ 
				if (yLine == CurrentLine(RTB))
					break;

				offset += RTB.Lines[i].Length + 1; 
				//offset += RTB.Lines[i].Length + CursorPosition.GetCorrection(RTB);
 
			} 
 
			RTB.Focus(); 
 
			int nCol_1 = offset + xColumn + 1;
			RTB.Select(nCol_1, SelLenght); 


			int nDelta = 0; // Correction factor

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
			int curColumn = 0;
			nDelta = 0;
			while (xColumn != CurrentColumn(RTB)) // Was an If statement, but w/ long documents was wrong
			{
				curColumn = CurrentColumn(RTB);
				if (xColumn > curColumn)
				{
					nDelta = (xColumn - curColumn);
					RTB.Select(nCol_1 + nDelta, SelLenght);
					if (xColumn != CurrentColumn(RTB))
					{
						RTB.Select((nCol_1 + nDelta) + 1, SelLenght);
					}
				}
				else // xColumn < curColumn
				{
					nDelta = (curColumn - xColumn) ;
					RTB.Select(nCol_1 - nDelta, SelLenght);
					if (xColumn != CurrentColumn(RTB))
					{
						RTB.Select((nCol_1 - nDelta) - 1, SelLenght);
					}
				}
			}

			RTB.WordWrap = bWordWrap;
 
		}

		// New as of 03/12/06
	
		public int CurrentColumn(RichTextBox cRTB)
		{
			return CursorPosition.Column(cRTB); 
		}
			
		public int CurrentLine(RichTextBox cRTB)
		{
			return CursorPosition.Line(cRTB); 
		}

		public int CurrentPosition(RichTextBox cRTB)
		{
			return cRTB.SelectionStart; 
		}

		public int SelectionEnd(RichTextBox cRTB)
		{
			return cRTB.SelectionStart + cRTB.SelectionLength; 
		}

	
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

			if ( pt1 != pt2 )
				return 1;
			else
				return 0;
		}

		public static int Line(RichTextBox cRTB)
		{
			int index = cRTB.SelectionStart;
			int correction = GetCorrection( cRTB);
			return cRTB.GetLineFromCharIndex( index ) - correction + 1;
		}

		public static int Column(RichTextBox cRTB)
		{
			
			int index1 = cRTB.SelectionStart;

			int correction = GetCorrection( cRTB);
			Point p = cRTB.GetPositionFromCharIndex( index1 - correction );
			
			if ( p.X == 1 )
				return 1;
			
			p.X = 0;
			int index2 = cRTB.GetCharIndexFromPosition( p );
			
			int col = index1 - index2 + 1;
			return col;
		}
	}
	}
}
