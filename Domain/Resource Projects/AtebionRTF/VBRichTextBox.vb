Imports System.Runtime.InteropServices
Imports System.Data
Imports RichTextExCs
Namespace Atebion.RTFBox

    Public Class VBRichTextBox


#Region "Property: SelectionBackColor"
        <StructLayout(LayoutKind.Sequential)> Private Structure CharFormat2
            Public cbSize As Int32
            Public dwMask As Int32
            Public dwEffects As Int32
            Public yHeight As Int32
            Public yOffset As Int32
            Public crTextColor As Int32
            Public bCharSet As Byte
            Public bPitchAndFamily As Byte
            <MarshalAs(UnmanagedType.ByValTStr, SizeConst:=32)> Public szFaceName As String
            Public wWeight As Int16
            Public sSpacing As Int16
            Public crBackColor As Int32
            Public lcid As Int32
            Public dwReserved As Int32
            Public sStyle As Int16
            Public wKerning As Int16
            Public bUnderlineType As Byte
            Public bAnimation As Byte
            Public bRevAuthor As Byte
            Public bReserved1 As Byte
        End Structure

        Private Const LF_FACESIZE = 32
        Private Const CFM_BACKCOLOR = &H4000000
        Private Const CFE_AUTOBACKCOLOR = CFM_BACKCOLOR
        Private Const WM_USER = &H400
        Private Const EM_SETCHARFORMAT = (WM_USER + 68)
        Private Const EM_SETBKGNDCOLOR = (WM_USER + 67)
        Private Const EM_GETCHARFORMAT = (WM_USER + 58)
        Private Const WM_SETTEXT = &HC
        Private Const SCF_SELECTION = &H1&

        Private Overloads Declare Auto Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal Msg As Integer, ByVal wParam As Integer, ByRef lParam As CharFormat2) As Boolean

#Region "Keywords"

        Dim sKeywordFound() As String
        Public ReadOnly Property KeywordsFound() As String()
            Get
                Return sKeywordFound
            End Get

        End Property

        Public Sub ClearKeywords()
            ReDim sKeywordFound(0)
        End Sub


#End Region

        ' Here we do the magic...
        Public Property SelectionBackColor() As Color
            Get
                ' We need to ask the RTB for the backcolor of the current selection.
                ' This is done using SendMessage with a format structure which the RTB will fill in for us.
                Dim HWND As IntPtr = Me.Handle ' Force the creation of the window handle...
                Dim Format As New CharFormat2
                Format.dwMask = CFM_BACKCOLOR
                Format.cbSize = Marshal.SizeOf(Format)
                SendMessage(Me.Handle, EM_GETCHARFORMAT, SCF_SELECTION, Format)
                Return ColorTranslator.FromOle(Format.crBackColor)
            End Get
            Set(ByVal Value As Color)
                ' Here we do relatively the same thing as in Get, but we are telling the RTB to set
                ' the color this time instead of returning it to us.
                Dim HWND As IntPtr = Me.Handle ' Force the creation of the window handle...
                Dim Format As New CharFormat2
                Format.crBackColor = ColorTranslator.ToOle(Value)
                Format.dwMask = CFM_BACKCOLOR
                Format.cbSize = Marshal.SizeOf(Format)
                SendMessage(Me.Handle, EM_SETCHARFORMAT, SCF_SELECTION, Format)
            End Set
        End Property
#End Region
#Region "Proc: ClearBackColor"
#Region "ScrollBarTypes"
        Private Enum ScrollBarTypes
            SB_HORZ = 0
            SB_VERT = 1
            SB_CTL = 2
            SB_BOTH = 3
        End Enum
#End Region
#Region "SrollBarInfoFlags"
        Private Enum ScrollBarInfoFlags
            SIF_RANGE = &H1
            SIF_PAGE = &H2
            SIF_POS = &H4
            SIF_DISABLENOSCROLL = &H8
            SIF_TRACKPOS = &H10
            SIF_ALL = (SIF_RANGE Or SIF_PAGE Or SIF_POS Or SIF_TRACKPOS)
        End Enum
#End Region

        Public Sub ClearBackColor(Optional ByVal ClearAll As Boolean = True)
            Dim HWND As IntPtr = Me.Handle ' Force the creation of the window handle...

            LockWindowUpdate(Me.Handle)   ' Lock drawing...
            Me.SuspendLayout()
            Dim ScrollPosVert As Integer = Me.GetScrollBarPos(Me.Handle, ScrollBarTypes.SB_VERT)
            Dim ScrollPosHoriz As Integer = Me.GetScrollBarPos(Me.Handle, ScrollBarTypes.SB_HORZ)
            Dim SelStart As Integer = Me.SelectionStart
            Dim SelLength As Integer = Me.SelectionLength

            If ClearAll Then Me.SelectAll() ' Should we clear everything or just use the current selection?
            Dim Format As New CharFormat2
            Format.crBackColor = -1
            Format.dwMask = CFM_BACKCOLOR
            Format.dwEffects = CFE_AUTOBACKCOLOR  ' Clears the backcolor
            Format.cbSize = Marshal.SizeOf(Format)
            SendMessage(Me.Handle, EM_SETCHARFORMAT, SCF_SELECTION, Format)

            ' Return the previous values...
            Me.SelectionStart = SelStart
            Me.SelectionLength = SelLength
            SendMessage(Me.Handle, EMFlags.EM_SETSCROLLPOS, 0, New RichTextBox.POINT(ScrollPosHoriz, ScrollPosVert))
            Me.ResumeLayout()
            LockWindowUpdate(IntPtr.Zero) ' Unlock drawing...
        End Sub

        <StructLayout(LayoutKind.Sequential)> Private Structure SCROLLINFO
            Public cbSize As Integer ' UINT cbSize; 
            Public fMask As ScrollBarInfoFlags ' UINT fMask; 
            Public nMin As Integer 'int  nMin; 
            Public nMax As Integer 'int  nMax; 
            Public nPage As Integer 'UINT nPage;  
            Public nPos As Integer ' int  nPos; 
            Public nTrackPos As Integer ' int  nTrackPos; 
        End Structure

        Private Declare Function GetScrollInfo Lib "User32" (ByVal hWnd As IntPtr, ByVal fnBar As ScrollBarTypes, ByRef lpsi As SCROLLINFO) As Boolean
        Private Function GetScrollBarPos(ByVal hWnd As IntPtr, ByVal BarType As ScrollBarTypes) As Integer
            Dim INFO As SCROLLINFO
            INFO.fMask = ScrollBarInfoFlags.SIF_POS
            INFO.cbSize = Marshal.SizeOf(INFO)
            GetScrollInfo(hWnd, BarType, INFO)
            Return INFO.nPos
        End Function
#End Region

#Region "Proc: GoToRowCol"
        Public Sub GoToRowCol(ByVal yRow As Integer, ByVal xCol As Integer, Optional ByVal SelLength As Integer = 0)

            Dim objTemp As RichTextExCs.RichTextBoxFunctions
            objTemp = New RichTextExCs.RichTextBoxFunctions()


            Dim objRT As New RichTextExCs.RichTextBoxFunctions

            'objRT.GoToLineColumn(Me, yRow, xCol)
            'objRT.GoToLineColumn2(Me, yRow, xCol)
            objRT.GoToLineColumn(Me, yRow, xCol, SelLength)

        End Sub

        Public Sub ShowToolTip(ByVal sText As String)
            Dim objRT As New RichTextExCs.TextBoxBallonTip
            objRT.ShowBalloonTip(Me, "Test", sText)
            '  objRT.ShowBalloonTip(Me, sText, "Test", RichTextExCs.TextBoxBallonTip.BallonTipIcons.Info)
        End Sub

#End Region

#Region "Function: Highlight"

        Public Function GetDataset_KeywordsFound() As DataSet
            ' Create a new DataTable.
            Dim table As DataTable = New DataTable("KeyFound")

            ' Declare variables for DataColumn and DataRow objects.
            Dim column As DataColumn

            ' Create new DataColumn, set DataType, ColumnName 
            ' and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.String")
            column.ColumnName = "Keyword"
            column.ReadOnly = False
            column.Unique = False

            ' Add the Column to the DataColumnCollection.
            table.Columns.Add(column)

            '' Create new DataColumn, set DataType, ColumnName 
            '' and add to DataTable.    
            'column = New DataColumn()
            'column.DataType = System.Type.GetType("System.String")
            'column.ColumnName = "Category"
            'column.ReadOnly = False
            'column.Unique = False

            '' Add the Column to the DataColumnCollection.
            'table.Columns.Add(column)

            ' Create new DataColumn, set DataType, ColumnName 
            ' and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.Int32")
            column.ColumnName = "Line"
            column.ReadOnly = False
            column.Unique = False

            ' Add the Column to the DataColumnCollection.
            table.Columns.Add(column)

            ' Create new DataColumn, set DataType, ColumnName 
            ' and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.Int32")
            column.ColumnName = "Column"
            column.ReadOnly = False
            column.Unique = False

            ' Add the Column to the DataColumnCollection.
            table.Columns.Add(column)

            ' Create new DataColumn, set DataType, ColumnName 
            ' and add to DataTable.    
            column = New DataColumn()
            column.DataType = System.Type.GetType("System.Int32")
            column.ColumnName = "Index"
            column.ReadOnly = False
            column.Unique = False

            ' Add the Column to the DataColumnCollection.
            table.Columns.Add(column)


            ' Make the ID column the primary key column.
            Dim PrimaryKeyColumns(0) As DataColumn
            PrimaryKeyColumns(0) = table.Columns("Index")
            table.PrimaryKey = PrimaryKeyColumns

            ' Instantiate the DataSet variable.
            Dim dataSet As DataSet
            dataSet = New DataSet()

            ' Add the new DataTable to the DataSet.
            '     dataSet.Tables.Add(table)

            ' Populate the dataset
            Dim keywordInfor As String
            Dim keywordInforArray() As String
            Dim dsNewRow As DataRow
            For i As Integer = 0 To (sKeywordFound.GetUpperBound(0) - 1)
                If sKeywordFound(i) <> Nothing Then
                    keywordInfor = sKeywordFound(i).ToString()
                    keywordInforArray = keywordInfor.Split(New Char() {"|"c}) 'String Def.: Keyword|row|column|index


                    ' dsNewRow = dataSet.Tables("table").NewRow()
                    dsNewRow = table.NewRow()
                    dsNewRow.Item("Keyword") = keywordInforArray(0)
                    dsNewRow.Item("Line") = keywordInforArray(1)
                    dsNewRow.Item("Column") = keywordInforArray(2)
                    dsNewRow.Item("Index") = keywordInforArray(3)

                    '  dataSet.Tables("table").Rows.Add(dsNewRow)
                    table.Rows.Add(dsNewRow)
                End If
            Next

            ' Add the new DataTable to the DataSet.
            dataSet.Tables.Add(table)

            'Return dataset
            GetDataset_KeywordsFound = dataSet

        End Function

        Private Declare Function LockWindowUpdate Lib "user32.dll" (ByVal hWndLock As IntPtr) As Boolean
        Public Sub Highlight(ByVal FindWhat As String, ByVal Highlight As Color, ByVal MatchCase As Boolean, ByVal MatchWholeWord As Boolean)
            LockWindowUpdate(Me.Handle)   ' Lock drawing...
            Me.SuspendLayout()
            Dim ScrollPosVert As Integer = Me.GetScrollBarPos(Me.Handle, ScrollBarTypes.SB_VERT)
            Dim ScrollPosHoriz As Integer = Me.GetScrollBarPos(Me.Handle, ScrollBarTypes.SB_HORZ)
            Dim SelStart As Integer = Me.SelectionStart
            Dim SelLength As Integer = Me.SelectionLength

            Dim StartFrom As Integer = 0
            Dim Length As Integer = FindWhat.Length
            Dim Finds As RichTextBoxFinds
            ' Setup the flags for searching.
            If MatchCase Then Finds = Finds Or RichTextBoxFinds.MatchCase
            If MatchWholeWord Then Finds = Finds Or RichTextBoxFinds.WholeWord

            Dim row As Integer
            Dim column As Integer
            Dim index As Integer
            Dim flag As Boolean

            Dim objRT As New RichTextExCs.RichTextBoxFunctions

            '          Dim dsKeyword As DataSet 'New 12.19.2010

            '          dsKeyword = CreateDataset_KeywordsFound()

            ' Do the search.
            flag = True
            While flag = True       '>> Me.Find(FindWhat, StartFrom, Finds) > -1
                index = Me.Find(FindWhat, StartFrom, Finds)
                If index < 0 Then
                    Exit While
                End If
                Me.SelectionBackColor = Highlight

                ' Store Keyword and locations in a string array
                column = objRT.CurrentColumn(Me)
                row = objRT.CurrentLine(Me)

                column = Me.GetCurrentColumn
                row = Me.GetCurrentLine
                'row = Me.GetCurrentLine
                'column = Me.SelectionStart
                If sKeywordFound Is Nothing Then
                    ReDim sKeywordFound(0)
                Else
                    ReDim Preserve sKeywordFound(sKeywordFound.GetUpperBound(0) + 1)
                End If
                sKeywordFound(sKeywordFound.GetUpperBound(0)) = FindWhat + "|" + row.ToString() + "|" + column.ToString() + "|" + index.ToString()

                ' Select Keyword
                StartFrom = Me.SelectionStart + Me.SelectionLength  ' Continue after the one we found..
            End While

            ' Return the previous values...
            Me.SelectionStart = SelStart
            Me.SelectionLength = SelLength
            SendMessage(Me.Handle, EMFlags.EM_SETSCROLLPOS, 0, New RichTextBox.POINT(ScrollPosHoriz, ScrollPosVert))
            Me.ResumeLayout()
            LockWindowUpdate(IntPtr.Zero) ' Unlock drawing...
        End Sub
#End Region
#Region "Proc: ScrollToBottom"
#Region "Scroller Flags"
        Private Enum EMFlags
            EM_SETSCROLLPOS = &H400 + 222
        End Enum
#End Region
#Region "ScrollBarFlags"
        Private Enum ScrollBarFlags
            SBS_HORZ = &H0
            SBS_VERT = &H1
            SBS_TOPALIGN = &H2
            SBS_LEFTALIGN = &H2
            SBS_BOTTOMALIGN = &H4
            SBS_RIGHTALIGN = &H4
            SBS_SIZEBOXTOPLEFTALIGN = &H2
            SBS_SIZEBOXBOTTOMRIGHTALIGN = &H4
            SBS_SIZEBOX = &H8
            SBS_SIZEGRIP = &H10
        End Enum
#End Region
#Region "Structure: POINT"
        <StructLayout(LayoutKind.Sequential)> Private Class POINT
            Public x As Integer
            Public y As Integer

            Public Sub New()
            End Sub

            Public Sub New(ByVal x As Integer, ByVal y As Integer)
                Me.x = x
                Me.y = y
            End Sub
        End Class
#End Region

        Private Declare Function GetScrollRange Lib "User32" (ByVal hWnd As IntPtr, ByVal nBar As Integer, ByRef lpMinPos As Integer, ByRef lpMaxPos As Integer) As Boolean
        Private Overloads Declare Auto Function SendMessage Lib "user32" Alias "SendMessageA" (ByVal hWnd As IntPtr, ByVal msg As Integer, ByVal wParam As Integer, ByVal lParam As RichTextBox.POINT) As IntPtr
        Public Sub ScrollToBottom()
            Dim Min, Max As Integer
            GetScrollRange(Me.Handle, ScrollBarFlags.SBS_VERT, Min, Max)
            SendMessage(Me.Handle, EMFlags.EM_SETSCROLLPOS, 0, New RichTextBox.POINT(0, Max - Me.Height))
        End Sub



#End Region

        Private Const EM_LINELENGTH = &HC1
        Private Const EM_GETLINE = &HC4
        Private Const EM_GETLINECOUNT = &HBA
        Private Const EM_LINEFROMCHAR = &HC9
        Private Const EM_LINEINDEX = &HBB
        Private Const EM_GETFIRSTVISIBLELINE = &HCE
        Private Const EM_LINESCROLL = &HB6
        Private Const EM_POSFROMCHAR = &HD6
        Private Const EM_GETSEL = &HB0
        Private Const EM_SETSEL = &HB1
        Private Const EM_CANUNDO = &HC6
        Private Const EM_SETTEXTEX = (WM_USER + 97)
        Private Const EM_GETTEXTEX = (WM_USER + 94)
        Private Const EM_GETSELTEXT = (WM_USER + 62)
        Private Const EM_REPLACESEL = &HC2
        Private Const EM_STREAMOUT = (WM_USER + 74)
        Private Const EM_STREAMIN = (WM_USER + 73)
        Private Const EM_SCROLLCARET = &HB7
        Private Const EM_FINDTEXTEX = (WM_USER + 79)
        Private Const EM_CHARFROMPOS = &HD7
        Private Const EM_GETRECT = &HB2
        Private Const EM_SETRECT = &HB3



        Public Function GetLine(ByVal charindex As Integer) As Integer
            Dim m As Message = Message.Create(Me.Handle, EM_LINEFROMCHAR, New IntPtr(charindex), New IntPtr(0))
            MyBase.WndProc(m)
            Return m.Result.ToInt32
        End Function

        Public Function GetLineStart(ByVal line As Integer) As Integer
            Dim m As Message = Message.Create(Me.Handle, EM_LINEINDEX, New IntPtr(line), New IntPtr(0))
            MyBase.WndProc(m)
            Return m.Result.ToInt32
        End Function

        Public Function GetLineLength(ByVal line As Integer) As Integer
            'Dim m As Message = Message.Create(Me.Handle, EM_LINELENGTH, New IntPtr(line), New IntPtr(0))
            'MyBase.WndProc(m)
            'Return m.Result.ToInt32

            '>> Problem with the above code 
            Return Me.Lines(line).Length
        End Function

        Public Function GetLineCount() As Integer
            Dim m As Message = Message.Create(Me.Handle, EM_GETLINECOUNT, New IntPtr(0), New IntPtr(0))
            MyBase.WndProc(m)
            Return m.Result.ToInt32
        End Function

        Public Function GetCurrentLineStart() As Integer
            Dim m As Message = Message.Create(Me.Handle, EM_LINEINDEX, New IntPtr(-1), New IntPtr(0))
            MyBase.WndProc(m)
            Return m.Result.ToInt32
        End Function

        Public Function GetCurrentLineText() As String
            Dim line As Integer
            '  Dim lineLength As Integer
            Dim x As Integer

            line = GetCurrentLine()
            'lineLength = GetLineLength(line)
            x = Me.Find(Me.Lines(line))
            If x < 0 Then
                GetCurrentLineText = ""
                Exit Function
            End If
            Me.Select(x, Me.Lines(line).Length)
            GetCurrentLineText = Me.SelectedText()

        End Function

        Public Function GetCurrentColumn() As Integer
            Dim objRT As New RichTextExCs.RichTextBoxFunctions

            Return objRT.CurrentColumn(Me)
        End Function

        Public Function GetCurrentLine() As Integer
            Dim m As Message = Message.Create(Me.Handle, EM_LINEFROMCHAR, New IntPtr(-1), New IntPtr(0))
            MyBase.WndProc(m)
            Return m.Result.ToInt32
        End Function

        Public Function GetFirstVisibleLine() As Integer
            Dim m As Message = Message.Create(Me.Handle, EM_GETFIRSTVISIBLELINE, New IntPtr(0), New IntPtr(0))
            MyBase.WndProc(m)
            Return m.Result.ToInt32
        End Function

        'Public Function GetLastVisibleLine() As Integer
        '    '  Return Me.GetLine(Me.GetCharIndexFromPosition(New Point(0, Me.Height - iHortBorderWidth)))
        '    '   Return Me.GetLine(Me.GetCharIndexFromPosition(New Point(0, Me.Height)))

        'End Function


    End Class
End Namespace







