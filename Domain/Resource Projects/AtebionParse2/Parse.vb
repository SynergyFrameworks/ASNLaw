Imports System
Imports System.Windows.Forms
Imports System.IO
Imports System.Collections.Generic
Imports System.Drawing
Imports System.Data
Imports System.Diagnostics
Imports System.Text
Imports Microsoft.VisualBasic

Imports Atebion.Windows.Forms.RichTextBox
Imports Atebion.TxtReaderWriter
'Imports Atebion.Logger
Imports Atebion.Attributes
Imports System.Text.RegularExpressions

'Imports Nini.Config

Public Class KeywordsFound_FieldNames
    Public Const Keyword As String = "Keyword"
    Public Const Category As String = "Category"
    Public Const Line As String = "Line"
    Public Const Column As String = "Column"
    Public Const Index As String = "Index"

End Class

Public Class ParseResults_FieldNames
    'Parsed UIDs are sequential, starting with “0” as follows: “0”, “1”, “2”, “3”, “4” 
    '   Modified sections are denoted as shown: 
    '   Split from section “2” would be “S2-1”.
    '       If second Split occured from section "2", then it would be "S2-2", and so-forth
    '   Section “1” Joined with section “0” would be “U0+1”. Joins can only occur with a section above or below per the SortOrder.
    '   Section entered by user would be "M1", "M2", "M3" (and so-forth)
    '       Therefore a Split from "M2" would be "SM2-1", 
    '           thus a Split from SM2-1 would be "SSM2-1-1"
    '               however if SSM2-1-1 was joined back with with its parent section then the UID would be "SM2-1+SSM2-1-1" (hope no one does this)
    Public Const UID As String = "UID" '>> See above note
    Public Const Parameter As String = "Parameter"
    Public Const Parent As String = "Parent"
    Public Const LineStart As String = "LineStart"
    Public Const LineEnd As String = "LineEnd" '>> Currently Not Used
    Public Const ColumnStart As String = "ColumnStart"
    Public Const ColumnEnd As String = "ColumnEnd" '>> Currently Not Used
    Public Const IndexStart As String = "IndexStart"
    Public Const IndexEnd As String = "IndexEnd"
    Public Const Number As String = "Number" '>> Example: 1.2.3  or II or A-1
    Public Const Caption As String = "Caption"
    Public Const SortOrder As String = "SortOrder"
    Public Const Keywords As String = "Keywords" '>> Example: "must, should, will"
    Public Const FileName As String = "FileName"
    Public Const SectionLength As String = "SectionLength"
End Class

Public Class ValidationResults_FieldNames
    Public Const ResultsUID As String = "UID"
    Public Const Number As String = "Number" '>> Example: 1.2.3  or II or A-1
    Public Const Caption As String = "Caption"
    Public Const Severity As String = "Severity"
    Public Const Description As String = "Description"

End Class

Public Class ParametersFound_FieldNames
    Public Const Parameter As String = "Parameter"
    Public Const Parent As String = "Parent"
    Public Const Index As String = "Index"
    Public Const LineStart As String = "LineStart"
    Public Const Found As String = "Found"
    Public Const Caption As String = "Caption"
    Public Const SectionLength As String = "SectionLength"
End Class

Public Class ParseResults '>> New Code - Replaces the ctlListView ListView - 12.30.2009

    Public Parameter As String
    Public Index As Integer
    Public LineStart As Integer
    Public LineEnd As Integer '>> Currently Not Used
    Public ColumnStart As Integer
    Public ColumnEnd As Integer '>> Currently Not Used
    Public Found As String
    Public Caption As String
    Public SortOrder As Integer
    Public FileName As String '>> Currently Not Used

End Class

Public Class ParameterLevels '>> New Code - Replaces ListView - 12.30.2009
    Public Parameter As String
    Public Parent As String

End Class

Public Class Keywords '>> New Code -- Added 03.23.2010 -- Tom Lipscomb -- Loaded from an XML File
    Public Keyword As String
    Public Category As String

End Class

'Public Class KeywordsFound '>> New Code - Replaces ListView - 01.13.2010
'    Public Keyword As String
'    Public Category As String '>> Added 03.23.2010 -- Tom Lipscomb
'    Public Line As Integer
'    Public Column As Integer
'    Public Index As Integer


'End Class

Public Class KeywordsSummary '>> New Code - Replaces ListView - 01.13.2010
    Public Keyword As String
    Public Category As String '>> Added 03.23.2010 -- Tom Lipscomb
    Public Total As Integer

End Class

Public Class Parse
    'Public Sub Parse(ByVal snLogger As Atebion.Logger.LoggerMgr)
    '    msnLogger = snLogger
    'End Sub


    '  Private msnLogger As Atebion.Logger.LoggerMgr

    Private Const mstrSPLIT As String = "Split"

    '>> Key
    Public Const UID As String = "UID"
    Public Const START_COLUMN As String = "_SCl"
    Public Const END_COLUMN As String = "_ECl"
    Public Const START_ROW As String = "_LIN"
    Public Const END_ROW As String = "_ER"


    Public Structure ParsePram
        Public Parse_Hierarchical As Boolean
        Public ParseParmFile As String
    End Structure


    Public listParseResults As New List(Of ParseResults) '>> New Code - Replaces the ctlListView ListView - 12.30.2009
    Public listParameterLevels As New List(Of ParameterLevels) '>> New Code - Replaces ListView - 12.30.2009
    Public listKeywords As New List(Of Keywords) '>> New Code - Replaces ListView - 01.13.2009
    Public listKeywordsFound As New List(Of Keywords) '>> New Code - Log Keywords found
    Public listKeywordsSummary As New List(Of KeywordsSummary) '>> New Code - Replaces ListView - 01.13.2009


    Private parseParm As ParsePram
    Private readerParameters As Atebion.TxtReaderWriter.Manager


#Region "Public Functions"
    '>>> Line Number Documents <<<
    'Steps:
    '   1.	Build cross-reference table, containing Row ID to Document Row Number
    '   2.	Remove Row IDs
    '   3.	Parse into segments
    '   4.	Re-insert Row IDs into parsed segments
    Public Function GenerateXRefRowIDs(ByVal ctrRTF As Atebion.RTFBox.RichTextBox) As DataSet
        Dim i As Integer
        Dim intCount As Integer

        Dim bWordWrap As Boolean

        bWordWrap = ctrRTF.WordWrap '>> Get Wordwrap for resetting after completion
        ctrRTF.WordWrap = False '>> Set to false for Parsing

        '>> Create Dataset to hold a cross-reference between RowIDs and Lines -- RowIDs are numberic content at the begining of a row (line) in a document. Some document lines may not contain a RowID
        mdsXRefRowIDs = CreateDataset_XRefRowIDs()

        intCount = ctrRTF.Lines().GetUpperBound(0)

        Dim strLine As String
        Dim intLen As Integer
        Dim LineIndex As Integer
        For i = 0 To intCount '>> Loop thru each line 
            strLine = ctrRTF.Lines(i)

            LineIndex = ctrRTF.GetFirstCharIndexFromLine(i)

            '  strLine = Remove_Prefix_Tabs(strLine) '>> Remove Prefix Tabs
            '  strLine = Valid_ASCII(strLine) '>> Remove all invalid char.s
            intLen = Len(strLine) '>> Get Lenght of line

            Dim strPString As String
            strPString = String.Empty
            Dim intNumerEnd As Integer
            Dim x As Integer
            Dim StartRowID As Integer '>> Start RowID based on the Index
            Dim EndRowID As Integer '>> End RowID based on the Index
            Dim foundRowID As Boolean
            If intLen > 1 Then
                ' strLine = Remove_Prefix_Tabs(strLine)
                StartRowID = -1 '>> Set Default
                EndRowID = -1 '>> Set Default
                For x = 1 To intLen '!! ToDo: Check if this should be Zero base
                    intNumerEnd = 0
                    strPString = Mid(strLine, 1, x)

                    If IsNumeric(strPString.Trim()) Then
                        StartRowID = x
                    End If
                    '>> Conditions for going to the next line in the selected document
                    ' If Len(strPString) > intMax_Param_Len Then Exit For '>> Check!!

                    '>> Check for Space
                    intNumerEnd = InStr(strPString, " ")
                    If intNumerEnd > 0 Then
                        strPString = Mid(strLine, 1, intNumerEnd - 1)
                        Exit For
                    End If

                    '>> Check for Tab
                    intNumerEnd = InStr(strPString, vbTab)
                    If intNumerEnd > 0 Then
                        strPString = Mid(strLine, 1, intNumerEnd - 1)
                        Exit For
                    End If

                    LineIndex = LineIndex + 1

                Next
                If strPString <> Nothing Or strPString <> String.Empty Then
                    If IsNumeric(strPString) Then
                        With mdsXRefRowIDs.Tables(0)
                            Dim dr As DataRow
                            dr = mdsXRefRowIDs.Tables(0).NewRow
                            dr.Item(XRefRowIDs_FieldNames.RowID) = strPString
                            dr.Item(XRefRowIDs_FieldNames.Index) = LineIndex.ToString()
                            dr.Item(XRefRowIDs_FieldNames.Line) = i.ToString()
                            .Rows.Add(dr)
                        End With

                    End If
                End If
            End If

        Next

        Return mdsXRefRowIDs

    End Function


    Private Function StripControlChars(ByVal s As String) As String
        Return Regex.Replace(s, "[^\x20-\x7F]", "")
    End Function


    Public Function ParseParagraphs(ByVal ctrRTF As Atebion.RTFBox.RichTextBox, ByVal ctrTo_RTF As Atebion.RTFBox.RichTextBox, ByVal QtyBlankRows As Integer, ByVal SavePath As String) As Boolean
        Dim i As Integer
        Dim intCount As Integer
        Dim indexStart As Integer
        Dim indexEnd As Integer
        Dim sSavePath As String
        Dim sFileName As String
        Dim bWordWrap As Boolean
        Dim countBlankLines As Integer
        Dim strLine As String
        Dim intLen As Integer
        Dim isHeaderSec As Boolean
        Dim UID As Integer
        Dim strCaption As String

        bWordWrap = ctrRTF.WordWrap '>> Get Wordwrap for resetting after completion
        ctrRTF.WordWrap = False '>> Set to false for Parsing

        mdsParseResults = Nothing
        mdsParseResults = CreateDataset_ParseResults()

        If SavePath.Substring(SavePath.Length - 1) <> "\" Then
            sSavePath = SavePath + "\"
        Else
            sSavePath = SavePath
        End If

        '  mvarParse_Run_UID = strUniqueCode '>> Not needed?

        intCount = ctrRTF.Lines().GetUpperBound(0)
        countBlankLines = 0 '>> Set Defualt
        isHeaderSec = True
        strCaption = String.Empty

        '    Dim foundError As Boolean
        For i = 0 To intCount '>> Loop through RTF document rows
            strLine = ctrRTF.Lines(i)

            '>> Not Needed?   LineIndex = ctrRTF.GetFirstCharIndexFromLine(i)

            '>> Clean Up row content
            strLine = Remove_Prefix_Tabs(strLine) '>> Remove Prefix Tabs
            strLine = Valid_ASCII(strLine) '>> Remove all invalid char.s
            strLine = strLine.Trim()
            strLine = StripControlChars(strLine)

            intLen = Len(strLine) '>> Get Lenght of line



            'Dim strPString As String
            'strPString = String.Empty
            'Dim intNumerEnd As Integer
            '   Dim x As Integer
            Dim StartRowID As Integer '>> Start RowID based on the Index
            Dim EndRowID As Integer '>> End RowID based on the Index
            Dim foundRowID As Boolean

            If isHeaderSec Then
                foundRowID = True
                StartRowID = 0
                UID = 0
            End If
            If intLen > 1 Then

                If foundRowID = False Then
                    foundRowID = True
                    StartRowID = i
                    strCaption = Truncate_String(strLine, 50, True)
                End If
                '>> Remove?  strPString = strPString + strLine
            Else
                isHeaderSec = False '>> Reset to Default
                foundRowID = False '>> Reset to Default

                EndRowID = i
                indexStart = ctrRTF.GetFirstCharIndexFromLine(StartRowID)
                indexEnd = ctrRTF.GetFirstCharIndexFromLine(EndRowID) - 1

                ctrRTF.SelectFromIndex(indexStart, indexEnd) '>> Select segment
                ctrTo_RTF.Rtf = ctrRTF.SelectedRtf '>> Copy the selected segment into the Copy RTF control

                sFileName = UID.ToString() + ".rtf"

                ctrTo_RTF.SaveFile(sSavePath + sFileName)

                Dim dr As DataRow
                dr = mdsParseResults.Tables(0).NewRow
                With mdsParseResults.Tables(0)

                    dr.Item(ParseResults_FieldNames.Caption) = strCaption
                    '>> Not currently used -- dr.Item(ParseResults_FieldNames.ColumnEnd) = 
                    dr.Item(ParseResults_FieldNames.ColumnStart) = 0
                    dr.Item(ParseResults_FieldNames.FileName) = String.Concat(UID.ToString(), ".rtf")
                    dr.Item(ParseResults_FieldNames.IndexEnd) = indexEnd.ToString()
                    dr.Item(ParseResults_FieldNames.IndexStart) = indexStart.ToString()
                    dr.Item(ParseResults_FieldNames.Keywords) = GetKeywords4PSection(indexStart, indexEnd, UID.ToString())
                    dr.Item(ParseResults_FieldNames.LineEnd) = EndRowID.ToString()
                    dr.Item(ParseResults_FieldNames.LineStart) = StartRowID.ToString()
                    dr.Item(ParseResults_FieldNames.Number) = UID.ToString()
                    dr.Item(ParseResults_FieldNames.Parameter) = "Paragraph"
                    dr.Item(ParseResults_FieldNames.Parent) = "N/A"
                    dr.Item(ParseResults_FieldNames.SectionLength) = ctrTo_RTF.TextLength.ToString()
                    dr.Item(ParseResults_FieldNames.UID) = UID.ToString()
                    dr.Item(ParseResults_FieldNames.SortOrder) = UID.ToString()
                End With
                mdsParseResults.Tables(0).Rows.Add(dr)

                UID = UID + 1

            End If


        Next

        ctrRTF.WordWrap = bWordWrap

        Beep()
        Beep()
        Return True

    End Function

    Public Function RemoveRowIDs(ByVal ctrRTF As Atebion.RTFBox.RichTextBox, ByVal fileSavePathName As String) As Integer
        Dim i As Integer
        Dim count As Integer

        Dim bWordWrap As Boolean

        bWordWrap = ctrRTF.WordWrap '>> Get Wordwrap for resetting after completion
        ctrRTF.WordWrap = False '>> Set to false for Parsing

        Dim rowCount As Integer
        rowCount = mdsXRefRowIDs.Tables(0).Rows.Count
        If rowCount = 0 Then
            Return 0
        End If

        count = 0 '>> Defualt

        rowCount = rowCount - 1
        Dim dr As DataRow
        Dim index As Integer
        Dim line As Integer
        Dim strPString As String
        For i = rowCount To 0 Step -1
            dr = mdsXRefRowIDs.Tables(0).Rows(i)
            strPString = dr.Item(XRefRowIDs_FieldNames.RowID)
            index = CInt(dr.Item(XRefRowIDs_FieldNames.Index))
            line = CInt(dr.Item(XRefRowIDs_FieldNames.Line))
            ctrRTF.Select(index, strPString.Length)
            ctrRTF.Cut() '>> Remove RowID

            count = count + 1
        Next

        ctrRTF.SaveFile(fileSavePathName)

        Return count '>> Returns the Qty of rows that had its RowID removed

    End Function


    Public Function Parse4(ByVal strUniqueCode As String, ByVal ctrRTF As Atebion.RTFBox.RichTextBox, ByVal ctrTo_RTF As Atebion.RTFBox.RichTextBox, ByVal SavePath As String, Optional ByRef strMsg As String = "", Optional ByVal booDisplay_Percentage As Boolean = False) As Boolean
        Dim i As Integer
        Dim intCount As Integer
        Dim Delta As Integer
        Dim indexStart As Integer
        Dim indexEnd As Integer
        Dim sSavePath As String
        Dim sFileName As String
        Dim bWordWrap As Boolean

        bWordWrap = ctrRTF.WordWrap '>> Get Wordwrap for resetting after completion
        ctrRTF.WordWrap = False '>> Set to false for Parsing

        mdsValidationResults = CreateDataset_ValidationResults() '>> Moved here 06.05.2013

        If SavePath.Substring(SavePath.Length - 1) <> "\" Then
            sSavePath = SavePath + "\"
        Else
            sSavePath = SavePath
        End If

        mvarParse_Run_UID = strUniqueCode

        If Not Populate_mdsParseResults(ctrRTF) Then
            Return False
        End If

        intCount = mdsParseResults.Tables(0).Rows.Count
        If intCount = 0 Then
            Return False
        End If

        intCount = intCount - 1
        Dim foundError As Boolean
        For i = 0 To intCount
            With mdsParseResults.Tables(0)
                indexStart = CInt(.Rows(i).Item(ParseResults_FieldNames.IndexStart))

                'If i = intCount Then
                indexEnd = CInt(.Rows(i).Item(ParseResults_FieldNames.IndexEnd))
                'Else
                '    indexEnd = CInt(.Rows(i + 1).Item(ParseResults_FieldNames.IndexEnd)) - 1 'Added 03.08.2014
                'End If
                'If IsDBNull(.Rows(i).Item(ParseResults_FieldNames.SectionLength)) = False Then

                '>> This has major problems!  Delta = CInt(.Rows(i).Item(ParseResults_FieldNames.SectionLength))

                foundError = False
                If indexStart < 0 Then '>> Added 06.05.2013
                    Dim dr As DataRow

                    dr = mdsValidationResults.Tables(0).NewRow
                    dr.Item(ValidationResults_FieldNames.ResultsUID) = .Rows(i).Item(ParseResults_FieldNames.UID)
                    dr.Item(ValidationResults_FieldNames.Number) = .Rows(i).Item(ParseResults_FieldNames.Number)
                    dr.Item(ValidationResults_FieldNames.Caption) = .Rows(i).Item(ParseResults_FieldNames.Caption)
                    dr.Item(ValidationResults_FieldNames.Severity) = 1
                    dr.Item(ValidationResults_FieldNames.Description) = "Start Index Error – Start Index = " + indexEnd.ToString()
                    mdsValidationResults.Tables(0).Rows.Add(dr)
                    foundError = True
                End If
                'If indexEnd < 0 Then '>> Added 06.05.2013
                '    Dim dr As DataRow

                '    dr = mdsValidationResults.Tables(0).NewRow
                '    dr.Item(ValidationResults_FieldNames.ResultsUID) = .Rows(i).Item(ParseResults_FieldNames.UID)
                '    dr.Item(ValidationResults_FieldNames.Number) = .Rows(i).Item(ParseResults_FieldNames.Number)
                '    dr.Item(ValidationResults_FieldNames.Caption) = .Rows(i).Item(ParseResults_FieldNames.Caption)
                '    dr.Item(ValidationResults_FieldNames.Severity) = 1
                '    dr.Item(ValidationResults_FieldNames.Description) = "End Index Error – End Index = " + indexEnd.ToString()
                '    mdsValidationResults.Tables(0).Rows.Add(dr)
                '    foundError = True
                'End If

                If foundError = False Then
                    Delta = indexEnd - indexStart '>> This seems to work, need to continue to test

                    ctrRTF.SelectFromIndex(indexStart, Delta)

                    ctrTo_RTF.Rtf = ctrRTF.SelectedRtf
                    'Else
                    'ctrTo_RTF.Text = ""
                    'End If

                    sFileName = .Rows(i).Item(ParseResults_FieldNames.FileName)

                    ctrTo_RTF.SaveFile(sSavePath + sFileName)


                    Application.DoEvents()
                End If

            End With
        Next

        ' ctrRTF.WordWrap = bWordWrap

        Beep()
        Beep()
        Return True
    End Function

    ' Public Function Parse2(ByRef lngParse_Run_UID As Integer, ByRef ctrRTF As AxEMEDIT2Lib.AxEditor, ByRef ctrTo_RTF As AxEMEDIT2Lib.AxEditor, ByRef ctlListView As AxMSComctlLib.AxListView, ByRef ctrlParam_Levels As System.Windows.Forms.ListBox, Optional ByRef strCap_Terminator As String = "", Optional ByRef strMsg As String = "", Optional ByRef booDisplay_Percentage As Boolean = False) As Integer

    '    Public Function Parse3(ByVal strUniqueCode As String, ByVal ctrRTF As Atebion.RTFBox.RichTextBox, ByVal ctrTo_RTF As Atebion.RTFBox.RichTextBox, Optional ByVal strCap_Terminator As String = "", Optional ByRef strMsg As String = "", Optional ByVal booDisplay_Percentage As Boolean = False) As Integer
    '        Dim i As Integer
    '        Dim intCount As Integer
    '        Dim intTerminator_Location As Integer '>> Caption Terminator Location
    '        Dim booLarge_Caption As Boolean '>> True denotes caption is larger than Max. Length setting. -- Therefore, caption may be part of actual requirement text.
    '        Dim intLen_Current_Num As Integer '>> Used only when booLarge_Caption = True

    '        '>> Data to Store in database
    '        Dim strParse_Parameter As String
    '        Dim strLast_Parameter As String
    '        Dim strParent_Num As String
    '        Dim strCurrent_Num As String
    '        Dim strLast_Num As String
    '        Dim booIsRoot As Boolean
    '        Dim strTitle As String
    '        Dim intFrom_Row_No As Integer
    '        Dim intTo_Row_No As Integer
    '        Dim booNumbers_are_Hierarchical As Boolean
    '        Dim intPercent As Integer
    '        Dim intFileType As Integer
    '        Dim strKey As String

    '        Dim booParse_Item As Boolean

    '        Dim intIndexFrom As Integer
    '        Dim intIndexTo As Integer

    '        On Error GoTo Err_Parse

    '        mvarParse_Run_UID = strUniqueCode

    '        'intCount = ctlListView.ListItems.Count
    '        'If intCount = 0 Then
    '        '    Parse2 = 1
    '        '    Exit Function
    '        'End If

    '        intCount = mdsParametersFound.Tables(0).Rows.Count
    '        If intCount = 0 Then
    '            strMsg = "No Parameters were found during Document Analysis."
    '            Return 1
    '        End If

    '        'If mvarParse_Hierarchical Then
    '        '    booNumbers_are_Hierarchical = Are_Num_Hierarchical(ctlListView)
    '        'End If

    '        booNumbers_are_Hierarchical = True '>> For now always make Numbers Hierarchical

    '        '>> Set Parse Run properties and Write Parse Run Record
    '        'If mvarParse_Run_UID = 0 Then
    '        '    If Not Write_Parse_Run_Record Then
    '        '        Parse2 = 2
    '        '        Exit Function
    '        '    End If
    '        'End If

    '        '>> Save File for furture reference  -- Added 04/19/2001
    '        'intFileType = ctrRTF.TextType
    '        'Call gobjEM_Editor.SaveFileAs(mvarDirectory & "\" & mvarParse_Run_UID & ".rtf", intFileType)



    '        '>> Check if Max. Parse Caption has been set
    '        If mvarMax_Parse_Caption < 1 Then
    '            mvarMax_Parse_Caption = 50 '>> Set Default
    '        End If

    '        For i = 1 To intCount
    '            intLen_Current_Num = 0 '>> Set Default
    '            'ctlListView.ListItems.Item(i).Selected = True '>> Added 02/11/02
    '            'strParse_Parameter = ctlListView.ListItems.Item(i).Text
    '            'intFrom_Row_No = CInt(ctlListView.ListItems.Item(i).SubItems(1))
    '            'strCurrent_Num = Trim(ctlListView.ListItems.Item(i).SubItems(2))
    '            'intLen_Current_Num = Len(ctlListView.ListItems.Item(i).SubItems(2))
    '            'strKey = ctlListView.ListItems(i).Key

    '            strParse_Parameter = mdsParametersFound.Tables(0).Rows(i).Item(ParametersFound_FieldNames.Parameter)
    '            intFrom_Row_No = mdsParametersFound.Tables(0).Rows(i).Item(ParametersFound_FieldNames.LineStart)
    '            strCurrent_Num = mdsParametersFound.Tables(0).Rows(i).Item(ParametersFound_FieldNames.Found)
    '            intLen_Current_Num = strCurrent_Num.Length
    '            strKey = mdsParametersFound.Tables(0).Rows(i).Item(ParametersFound_FieldNames.Index)


    '            '>> Test
    '            '        If strCurrent_Num = "G.2.4" Then
    '            '            Beep
    '            '        End If
    '            '<<

    '            '>> Removed 04/03/04
    '            '      If strCap_Terminator = "" Then '>> No Caption Terminator
    '            '         strTitle = Get_Caption(ctrRTF, intFrom_Row_No, strCurrent_Num, , , booLarge_Caption)
    '            '      Else
    '            '         strTitle = Get_Caption(ctrRTF, intFrom_Row_No, strCurrent_Num, strCap_Terminator, intTerminator_Location, booLarge_Caption)
    '            '      End If
    '            '

    '            ''>> Added 04/03/04
    '            'strTitle = Trim(ctlListView.ListItems.Item(i).SubItems(3))
    '            'strTitle = Truncate_String(strTitle, mvarMax_Parse_Caption, False)
    '            ''<<

    '            strTitle = mdsParametersFound.Tables(0).Rows(i).Item(ParametersFound_FieldNames.Caption)
    '            strTitle = Truncate_String(strTitle, mvarMax_Parse_Caption, False)

    '            If Not booLarge_Caption Then
    '                If strParse_Parameter <> mstrSPLIT Then
    '                    strParent_Num = strCurrent_Num
    '                End If
    '            Else
    '                '>> Do Not Format as this stage -- strCurrent_Num = strParent_Num & mvarNum_Separator & strCurrent_Num
    '            End If

    '            If i + 1 > intCount Then
    '                intTo_Row_No = ctrRTF.Lines.GetUpperBound(0) 'ToDo find base 0 or 1? 'ctrRTF.Count '>> Denotes last Parse Item
    '            Else
    '                '  intTo_Row_No = CShort(ctlListView.ListItems.Item(i + 1).SubItems(1)) - 1
    '                intTo_Row_No = mdsParametersFound.Tables(0).Rows(i + 1).Item(ParametersFound_FieldNames.LineStart) - 1
    '            End If

    '            ctrRTF.GoToRowCol(intFrom_Row_No, 1)
    '            intIndexFrom = ctrRTF.GetCharIndexFromPosition(ctrRTF.Location)
    '            ctrRTF.GoToRowCol(intTo_Row_No, 1)
    '            Dim ToColumn As Integer
    '            ToColumn = ctrRTF.GetCurrentLineText.Length
    '            ctrRTF.GoToRowCol(intTo_Row_No, ToColumn)
    '            intIndexTo = ctrRTF.GetCharIndexFromPosition(ctrRTF.Location)

    '            ctrRTF.Select(intIndexFrom, (intIndexTo - intIndexFrom))
    '            ctrRTF.Copy()
    '            ctrTo_RTF.Paste()
    '            Dim Result_File_Name As String
    '            Result_File_Name = i.ToString() + ".rtf"
    '            ctrTo_RTF.SaveFile(msResultPath + "\" + Result_File_Name)

    '            If mdsParseResults Is Nothing Then
    '                mdsParseResults = CreateDataset_ParseResults()
    '            End If

    '            strTitle = Valid_ASCII(strTitle)
    '            strTitle = Truncate_String(strTitle, mvarMax_Parse_Caption, True)

    '            Dim dr As DataRow
    '            dr = mdsParseResults.Tables(0).NewRow()
    '            dr.Item(ParseResults_FieldNames.Caption) = strTitle
    '            dr.Item(ParseResults_FieldNames.Number) = Valid_ASCII(strCurrent_Num)
    '            dr.Item(ParseResults_FieldNames.Parameter) = strParse_Parameter
    '            dr.Item(ParseResults_FieldNames.LineStart) = intFrom_Row_No
    '            dr.Item(ParseResults_FieldNames.LineEnd) = intTo_Row_No
    '            dr.Item(ParseResults_FieldNames.IndexStart) = intIndexFrom
    '            dr.Item(ParseResults_FieldNames.IndexEnd) = intIndexTo
    '            dr.Item(ParseResults_FieldNames.ColumnStart) = 1 '>> Default
    '            dr.Item(ParseResults_FieldNames.IndexEnd) = ToColumn
    '            dr.Item(ParseResults_FieldNames.SortOrder) = i
    '            dr.Item(ParseResults_FieldNames.Parent) = mdsParametersFound.Tables(0).Rows(i).Item(ParametersFound_FieldNames.Parent)
    '            dr.Item(ParseResults_FieldNames.FileName) = Result_File_Name

    '            mdsParseResults.Tables(0).Rows.Add(dr)



    '            '' If Parse_Item(ctrRTF, ctrTo_RTF, intFrom_Row_No, intTo_Row_No, booLarge_Caption, intLen_Current_Num) Then
    '            'If InStr(strKey, END_ROW) <> 0 Then
    '            '    booParse_Item = Parse_Item3(ctrRTF, ctrTo_RTF, strKey)
    '            'Else
    '            '    booParse_Item = Parse_Item2(ctrRTF, ctrTo_RTF, intFrom_Row_No, ctlListView, strTitle)
    '            'End If
    '            'If booParse_Item Then
    '            '    With mtypParse_Results '>> Set Structure to be saved into the Parse_Results table.
    '            '        If Not mvarParse_Hierarchical Then
    '            '            If strParent_Num = strCurrent_Num Then
    '            '                .Primary_Num = "Root" '>> Denotes parse item is at root level
    '            '                .Parent_No = 0
    '            '            Else
    '            '                .Primary_Num = strParent_Num
    '            '            End If
    '            '        Else '>> Parse_Hierarchical
    '            '            If strParse_Parameter <> mstrSPLIT Then
    '            '                If Is_Last_Parent(ctrlParam_Levels, strParse_Parameter, strLast_Parameter, booIsRoot, strCurrent_Num, i, False) Then
    '            '                    '                  .Primary_Num = strLast_Num
    '            '                    '                  .Parent_No = i - 1
    '            '                    '               Else
    '            '                    '                  If booIsRoot Then
    '            '                    '                     .Primary_Num = "Root"   '>> Denotes parse item is at root level
    '            '                    '                     .Parent_No = 0
    '            '                    '                  End If
    '            '                End If
    '            '                '>> Set values for next loop
    '            '                strLast_Num = strCurrent_Num
    '            '                strLast_Parameter = strParse_Parameter
    '            '                '<<
    '            '            Else
    '            '                Call Get_Split_Parent(ctlListView, i)
    '            '            End If
    '            '        End If
    '            '        Call Valid_ASCII(strCurrent_Num) '>> Cleanup text -- 04/05/04
    '            '        .Current_Num = strCurrent_Num
    '            '        .Result_File_Name = VB6.Format(mvarParse_Run_UID) & "_" & VB6.Format(i) & ".rtf"
    '            '        .From_Row_No = intFrom_Row_No
    '            '        .To_Row_No = intTo_Row_No
    '            '        Call Valid_ASCII(strTitle) '>> Cleanup text -- 02/02/02
    '            '        .Title = strTitle
    '            '        .Sort_Order = i
    '            '        .Parse_Parameter = strParse_Parameter
    '            '        .Key = strKey
    '            '    End With
    '            '    If Not Write_Parse_Results_Record(ctrTo_RTF, mvarDirectory & "\" & mtypParse_Results.Result_File_Name) Then
    '            '        Beep()
    '            '        '>> ToDo -- Add Log for problem items, if required per testing results
    '            '    End If
    '            'Else
    '            '    Beep()
    '            '    '>> ToDo -- Add Log for problem items, if required per testing results
    '            'End If

    '            'If booDisplay_Percentage Then
    '            '    '        intPercent = Get_Percent(i, intCount)
    '            '    '        Call SetMeter(CSng(intPercent), mfrmPercent_Form)
    '            'End If

    '        Next
    '        Beep()
    '        Beep()
    '        Exit Function

    'Err_Parse:
    '        MsgBox(Err.Description & vbCrLf & vbCrLf & "An error has occurred while parsing the current document.", MsgBoxStyle.Exclamation, "Error No.: " & Err.Number)
    '        Exit Function

    '    End Function

    Public Function Analyze_Doc_Parameters2(ByVal ctrRTF As Atebion.RTFBox.RichTextBox, ByVal booParse_Hierarchical As Boolean, Optional ByVal strMsg As String = "", Optional ByVal intMax_Param_Len As Integer = 12, Optional ByVal booDisplay_Percentage As Boolean = False) As Boolean
        '>> Purpose: This function reads the document as determines what parameters are used.
        '               If is unable to determine it a parameter is a Primary or Sub.
        '                   The user must review the document to make such determinations.

        ' Problems: 1 = Unable to open/read Parse_Parameters records
        '
        ' Optional strParameters() As String '>> Replace with ctlListView after Proof of concept

        '  Try

        Dim intCount As Integer
        Dim intFoundCount As Integer
        intFoundCount = -1
        Dim intSectionLength As Integer
        intSectionLength = 0
        Dim intAdjuster As Integer
        Dim bLastLoopFound As Boolean = False


        Dim i As Integer
        Dim strLine As String
        Dim nCharIndex As Integer
        '  Dim nCharIndex_Last As Integer
        Dim strParameter As String
        Dim intLen As Integer
        Dim strPString As String
        Dim x As Integer
        Dim x1 As Integer
        Dim x2 As Integer

        Dim strLast_Parameter As String = String.Empty

        Dim strCaption As String
        strCaption = String.Empty '>> Set Default
        '  Dim xItem As ListViewItem
        Dim bWordWrap As Boolean
        Dim sZoomFactor As Single

        Dim stopWatch As New Stopwatch()
        stopWatch.Start()

        bWordWrap = ctrRTF.WordWrap '>> Get Wordwrap for resetting after completion
        ctrRTF.WordWrap = False '>> Set to false for Parsing
        ctrRTF.ReadOnly = True
        sZoomFactor = ctrRTF.ZoomFactor

        '  ctrRTF.Text = Remove_Prefix_Tabs(ctrRTF.Text) 'Added 03.08.2014
        '  ctrRTF.Text = CleanString(ctrRTF.Text) 'Added 03.11.2014
        'ctrRTF.Text = Remove_ASCII9(ctrRTF.Text) 'Added 03.08.2014
        ctrRTF.ZoomFactor = 1.0
        ' ctrRTF.ZoomFactor = 0.5

        parseParm.Parse_Hierarchical = booParse_Hierarchical
        parseParm.ParseParmFile = Me.ParseParameterFile 'sParseParmFile

        mdsParametersFound = Nothing '>> Reset
        mdsParametersFound = CreateDataset_ParametersFound() '>> Create new DataSet to hold Analysis Results
        Dim dr As DataRow '>> for mdsParametersFound

        'Call Find_Keywords(ctrRTF, ctlLVKeyWords, ctlLstKeywords_Sel, ctlLVTotal_Keywords, True) '>> Added boolean parameter for Keyword Locations

        '>> Added 10.02.2013
        dr = mdsParametersFound.Tables(0).NewRow()

        dr.Item(ParametersFound_FieldNames.Parameter) = "Header"
        dr.Item(ParametersFound_FieldNames.Index) = 0
        dr.Item(ParametersFound_FieldNames.LineStart) = 0
        dr.Item(ParametersFound_FieldNames.Found) = Trim("Header Area")
        dr.Item(ParametersFound_FieldNames.Caption) = Trim("Header Area")
        mdsParametersFound.Tables(0).Rows.Add(dr)
        '<<

        '>> Gets an Array of Parse Parameters for the Parse Parameter File
        Dim al As ArrayList
        al = Get_Parameters_ArrayList(parseParm.ParseParmFile)   'ToDo: Check if file exists, and if not then create file.

        '* intCount = ctrRTF.GetLineCount() - 1 '>> Get the total qty of lines in selected document
        intCount = ctrRTF.Lines().GetUpperBound(0)

        'intTextType = ctrRTF.TextType
        'ctrRTF.TextType = 1 '>> Dos type
        Dim sDblQuote As String
        Dim sSQuote As String
        nCharIndex = 0 'Default
        For i = 0 To intCount '>> Loop thru each line and check for existing parsing parameters and Keywords.

            'ctrRTF.GetFirstCharIndexFromLine(i) '03.07.2014

            ' nCharIndex = ctrRTF.GetFirstCharIndexFromLine(i) '>> Good Most of the time -- Moved up 03.07.2014


            '      Dim chkLine As Integer = ctrRTF.GetLineFromCharIndex(nCharIndex) 'Testing 03.07.2014




            '* strLine = Trim(ctrRTF.GetCurrentLineText) '>> Remove extra spaces
            Application.DoEvents()

            '>> Removed 03.03.2011
            strLine = Trim(ctrRTF.Lines(i))
            'strLine = Remove_Prefix_Tabs(strLine) '>> Remove Prefix Tabs
            'strLine = Valid_ASCII(strLine) '>> Remove all invalid char.s
            intLen = Len(strLine) '>> Get Lenght of line


            Dim intNumerEnd As Integer
            '>> Search for Parsing Parameters
            '*  If intLen > 1 And i = ctrRTF.CurrentLine Then 'nCharIndex > (nCharIndex_Last + 2)
            If intLen > 1 Then
                '   rsRecordset.MoveFirst()
                sDblQuote = Chr(34)
                sSQuote = Chr(39)

                strLine = Remove_Prefix_Tabs(strLine)
                strPString = String.Empty 'Default

                For x = 1 To intLen '!! ToDo: Check if this should be Zero base
                    intNumerEnd = 0
                    strPString = Mid(strLine, 1, x)
                    '>> Conditions for going to the next line in the selected document
                    ' If Len(strPString) > intMax_Param_Len Then Exit For '>> Check!!

                    '>> Check for Space
                    intNumerEnd = InStr(strPString, " ")
                    If intNumerEnd > 0 Then
                        strPString = Mid(strLine, 1, intNumerEnd - 1)
                        Exit For
                    End If

                    '>> Check for Tab
                    intNumerEnd = InStr(strPString, vbTab)
                    If intNumerEnd > 0 Then
                        strPString = Mid(strLine, 1, intNumerEnd - 1)
                        Exit For
                    End If

                    '>> Check for Double Quotes
                    intNumerEnd = InStr(strPString, sDblQuote)
                    If intNumerEnd > 0 Then
                        strPString = Mid(strLine, 1, intNumerEnd - 1)
                        Exit For
                    End If

                    'Check for Single Quotes
                    intNumerEnd = InStr(strPString, sSQuote)
                    If intNumerEnd > 0 Then
                        strPString = Mid(strLine, 1, intNumerEnd - 1)
                        Exit For
                    End If
                    '<<
                Next
                If intNumerEnd <> 0 Then '>> If a possible Number was Not found then skip below
                    If Len(strPString) > 1 Then '>> Changed to > 1 because a pattern is always > 1
                        If Len(strPString) < 21 Then '>> Max. Lenght for Section No. is 20, thus if greater than 20 must not be a Section No.

                            '>> Loop thru the Parsing Parmeters
                            'For Each txtData As Atebion.TxtReaderWriter.Manager.TextData In al // Removed 02.02.2013
                            '  For Each txtData As String In al 'Added 02.02.2013
                            Dim txtData As String
                            txtData = String.Empty
                            Dim p As Integer
                            For p = 0 To al.Count - 1

                                txtData = TryCast(al.Item(p), String)

                                '  Do While Not rsRecordset.EOF
                                ' strParameter = rsRecordset.Fields("Parse_Parameter").Value // Removed 02.02.2013
                                strParameter = txtData 'Added 02.02.2013
                                'Debug.WriteLine("strParameter: " + strParameter)
                                'Debug.WriteLine("strPString: " + strPString)
                                'If Len(strPString) > 1 Then '>> Changed to > 1 because a pattern is always > 1
                                '    If Len(strPString) < 21 Then '>> Max. Lenght for Section No. is 20, thus if greater than 20 must not be a Section No.

                                If strPString Like strParameter Then
                                    '>> Check for invalid results -- Comment-out 5.24.2013, was filtering out numbering "1.", "2", "3.", ...   Wrote 04.10.2013
                                    'Dim xstrPString As String
                                    'xstrPString = strPString.Trim()

                                    'If strParameter = "*[a-z])" Or strParameter = "*[a-z]." Or strParameter = "*[A-Z])" Or strParameter = "*[A-Z]." Then
                                    '    If xstrPString.Length > 2 Then 'If strPString.Length > 3 Then
                                    '        GoTo Get_Next_Line
                                    '    End If
                                    'End If

                                    '>> Added 09.21.2013 -- Skip
                                    Dim xstrPString As String
                                    xstrPString = strPString.Trim()

                                    If strParameter = "*[a-z]." Then
                                        If xstrPString.Length > 4 Then
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If

                                    If strParameter = "*[A-Z]." Then
                                        If xstrPString.Length > 4 Then
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If

                                    If strParameter = "*[a-z])" Then
                                        If xstrPString.Length > 4 Then
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If


                                    If strParameter = "*([A-Z])" Then
                                        If xstrPString.Length > 7 Then
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If

                                    If strParameter = "*[A-Z])" Then '>> Added 10.02.2013
                                        If xstrPString.Length > 7 Then
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If

                                    If strParameter = "*.##" Then '>> Added 10.02.2013
                                        If xstrPString.Trim.Substring(0, 1) = "$" Then
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If

                                    '<< End Added


                                    '>> Get end of Parsed Parameter Number
                                    x1 = InStr(strLine, " ")
                                    x2 = InStr(strLine, vbTab)
                                    If x2 <> 0 Then
                                        If x1 <> 0 Then
                                            If x2 < x1 Then
                                                x = x2
                                            Else
                                                x = x1
                                            End If
                                        Else
                                            x = x2
                                        End If
                                    Else
                                        x = x1
                                    End If

                                    'Redundant! >> strPString = Mid(strLine, 1, x)

                                    ' If Len(strPString) > 0 Then
                                    'If Len(strPString) > 1 Then '>> Changed to > 1 because a pattern is always > 1
                                    '    If Len(strPString) < 21 Then '>> Max. Lenght for Section No. is 20, thus if greater than 20 must not be a Section No.

                                    If Len(x) <> 0 Then
                                        If x <> 0 Then
                                            strCaption = Trim(Mid(strLine, x))
                                            ' If Len(strCaption) > 50 Then
                                            If strCaption.Length > 50 Then 'Change 09.21.2013
                                                strCaption = Truncate_String(strCaption, 50, True)
                                            End If
                                        Else
                                            strCaption = Trim(strLine)
                                            strCaption = Truncate_String(strCaption, 15, True)
                                        End If
                                    End If


                                    '>> Code for Section Length
                                    If intFoundCount <> -1 Then '>> If there were previous sections found
                                        mdsParametersFound.Tables(0).Rows(intFoundCount).Item(ParametersFound_FieldNames.SectionLength) = intSectionLength
                                        intFoundCount = intFoundCount + 1
                                        'intAdjuster = strPString.Length + strCaption.Length + 2 '>> add to 2 for typical spaces
                                        'If strLine.Length < ctrRTF.Lines(i).Length Then
                                        '    intAdjuster = intAdjuster + (ctrRTF.Lines(i).Length - strLine.Length)
                                        'End If
                                    Else
                                        intFoundCount = 0 '>> Zero base, thus the 1st item
                                        intAdjuster = 0
                                    End If

                                    '>> Adjust the lenght to account for the found Number and Title
                                    'intSectionLength = ctrRTF.Lines(i).Length + 0  '>> Reset
                                    'If intAdjuster < intSectionLength Then
                                    '    intSectionLength = intSectionLength - intAdjuster
                                    'End If

                                    bLastLoopFound = True
                                    '<<


                                    '>> Gives false values -- Do Not Use! -- nCharIndex = ctrRTF.GetCharIndexFromPosition(System.Windows.Forms.Cursor.Position) '>> 04/11/06

                                    ctrRTF.Focus()
                                    '   nCharIndex = ctrRTF.GetFirstCharIndexFromLine(i) '>> Good Most of the time






                                    '>> Testing Code -- Remove after testing
                                    'If nCharIndex = 85256 Or nCharIndex = 86187 Or nCharIndex = 87057 Or nCharIndex = 87759 Then
                                    '    Beep()
                                    'End If


                                    '<<

                                    'strPString = Remove_ASCII9(strPString)
                                    'strCaption = Remove_ASCII9(strCaption)

                                    '  nCharIndex = ctrRTF.GetFirstCharIndexFromLine(i) 'Added 03.08.2014
                                    nCharIndex = ctrRTF.GetFirstCharIndexFromLine(i) 'Added 03.08.2014

                                    dr = mdsParametersFound.Tables(0).NewRow()

                                    dr.Item(ParametersFound_FieldNames.Parameter) = strParameter
                                    dr.Item(ParametersFound_FieldNames.Index) = nCharIndex.ToString()
                                    dr.Item(ParametersFound_FieldNames.LineStart) = i
                                    dr.Item(ParametersFound_FieldNames.Found) = Trim(strPString)
                                    dr.Item(ParametersFound_FieldNames.Caption) = Trim(strCaption)

                                    '              dr.Item(ParametersFound_FieldNames.SectionLength) = 

                                    If booParse_Hierarchical Then
                                        If strParameter <> strLast_Parameter Then
                                            If FindParameterLevel(strParameter) = -1 Then
                                                Dim parm As New ParameterLevels

                                                parm.Parameter = strParameter.Trim()
                                                If strLast_Parameter.Length = 0 Then
                                                    parm.Parent = "Parent: Root"
                                                Else
                                                    parm.Parent = "Parent: " & strLast_Parameter
                                                End If
                                                listParameterLevels.Add(parm)
                                                dr.Item(ParametersFound_FieldNames.Parent) = parm.Parent
                                            End If
                                        End If
                                    End If

                                    mdsParametersFound.Tables(0).Rows.Add(dr)

                                    'If booParse_Hierarchical Then
                                    '    If strParameter <> strLast_Parameter Then
                                    '        If lvFind(ctrlParam_Levels, strParameter, True, 0, False) = -1 Then
                                    '            xItem = New ListViewItem
                                    '            xItem.Text = Trim(strParameter)
                                    '            If strLast_Parameter.Length = 0 Then
                                    '                xItem.SubItems.Add("Parent: Root")
                                    '                strFirst_Param_Found = strParameter
                                    '            Else
                                    '                xItem.SubItems.Add("Parent: " & strLast_Parameter)
                                    '            End If
                                    '            ctrlParam_Levels.Items.Add(xItem)
                                    '        End If
                                    '    End If
                                    'End If

                                    strLast_Parameter = strParameter
                                    GoTo Get_Next_Line '>> Not a fan of Goto, but it works well in this case
                                End If
                            Next
                        End If
                    End If
                End If
            End If
Get_Next_Line:
            ' nCharIndex = nCharIndex + ctrRTF.Lines(i).Length
            If bLastLoopFound = False Then
                intSectionLength = intSectionLength + ctrRTF.Lines(i).Length
            End If
            bLastLoopFound = False
        Next


        '>> ToDo
        'If booDisplay_Percentage Then
        '    intPercent = Get_Percent(i, intCount)
        '    Call SetMeter(CSng(intPercent), mfrmPercent_Form)
        'End If


        mdsParseResults = Nothing
        mdsParseResults = CreateDataset_ParseResults()

        '>> Add Header to Parsed sections 
        '>> Added 10.02.2013
        If mdsParametersFound.Tables(0).Rows.Count > 1 Then '>> Changed From: mdsParametersFound.Tables(0).Rows.Count > 0 -- 11.02.2013
            If mdsParametersFound.Tables(0).Rows(1).Item(ParametersFound_FieldNames.LineStart) = 0 Then
                mdsParametersFound.Tables(0).Rows(0).Delete()

            End If
        End If
        '<<




        '>> Save Results to files
        SaveParseResultsList(Me.ParseResultsFile)
        SavelistParameterLevels(Me.ParametersResultsFile)


        ' Call AppLvMgr.AdjustColumnWidth(ctlListView, True)
        ' ctrRTF.TextType = intTextType
        ctrRTF.WordWrap = False 'bWordWrap
        ctrRTF.ZoomFactor = sZoomFactor

        stopWatch.Stop()
        Dim ts As TimeSpan = stopWatch.Elapsed
        mElapsedTime_Analyze_Doc_Parameters = String.Format("{0:00}:{1:00}:{2:00}.{3:00} for {4} Lines", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10, intCount + 1)
        Beep()
        Return True

 




    End Function

    Public Function Analyze_Doc_Parameters(ByVal ctrRTF As Atebion.RTFBox.RichTextBox, ByVal booParse_Hierarchical As Boolean, Optional ByVal strMsg As String = "", Optional ByVal intMax_Param_Len As Integer = 12, Optional ByVal booDisplay_Percentage As Boolean = False) As Boolean
        '>> Purpose: This function reads the document as determines what parameters are used.
        '               If is unable to determine it a parameter is a Primary or Sub.
        '                   The user must review the document to make such determinations.

        ' Problems: 1 = Unable to open/read Parse_Parameters records
        '
        ' Optional strParameters() As String '>> Replace with ctlListView after Proof of concept

        '  Try

        Dim intCount As Integer
        Dim intFoundCount As Integer
        intFoundCount = -1
        Dim intSectionLength As Integer
        intSectionLength = 0
        Dim intAdjuster As Integer
        Dim bLastLoopFound As Boolean = False


        Dim i As Integer
        Dim strLine As String
        Dim nCharIndex As Integer
        '  Dim nCharIndex_Last As Integer
        Dim strParameter As String
        Dim intLen As Integer
        Dim strPString As String
        Dim x As Integer
        Dim x1 As Integer
        Dim x2 As Integer

        Dim strLast_Parameter As String = String.Empty

        Dim strCaption As String
        strCaption = String.Empty '>> Set Default
        '  Dim xItem As ListViewItem
        Dim bWordWrap As Boolean
        Dim sZoomFactor As Single

        Dim stopWatch As New Stopwatch()
        stopWatch.Start()

        bWordWrap = ctrRTF.WordWrap '>> Get Wordwrap for resetting after completion
        ctrRTF.WordWrap = False '>> Set to false for Parsing
        ctrRTF.ReadOnly = True
        sZoomFactor = ctrRTF.ZoomFactor
        ' ctrRTF.ZoomFactor = 0.5

        parseParm.Parse_Hierarchical = booParse_Hierarchical
        parseParm.ParseParmFile = Me.ParseParameterFile 'sParseParmFile

        mdsParametersFound = Nothing '>> Reset
        mdsParametersFound = CreateDataset_ParametersFound() '>> Create new DataSet to hold Analysis Results
        Dim dr As DataRow '>> for mdsParametersFound

        'Call Find_Keywords(ctrRTF, ctlLVKeyWords, ctlLstKeywords_Sel, ctlLVTotal_Keywords, True) '>> Added boolean parameter for Keyword Locations

        '>> Added 10.02.2013
        dr = mdsParametersFound.Tables(0).NewRow()

        dr.Item(ParametersFound_FieldNames.Parameter) = "Header"
        dr.Item(ParametersFound_FieldNames.Index) = 0
        dr.Item(ParametersFound_FieldNames.LineStart) = 0
        dr.Item(ParametersFound_FieldNames.Found) = Trim("Header Area")
        dr.Item(ParametersFound_FieldNames.Caption) = Trim("Header Area")
        mdsParametersFound.Tables(0).Rows.Add(dr)
        '<<

        '>> Gets an Array of Parse Parameters for the Parse Parameter File
        Dim al As ArrayList
        al = Get_Parameters_ArrayList(parseParm.ParseParmFile)   'ToDo: Check if file exists, and if not then create file.

        '* intCount = ctrRTF.GetLineCount() - 1 '>> Get the total qty of lines in selected document
        intCount = ctrRTF.Lines().GetUpperBound(0)


        'intTextType = ctrRTF.TextType
        'ctrRTF.TextType = 1 '>> Dos type
        Dim sDblQuote As String
        Dim sSQuote As String
        nCharIndex = 0 'Default
        For i = 0 To intCount '>> Loop thru each line and check for existing parsing parameters and Keywords.
            'If i = 27 Then '>> Test, remove after test
            '    Beep()
            'End If
            'ctrRTF.GoToRowCol(i, 1)
            'ctrRTF.GoToLineColumn(i, 1, 0)
            'ctrRTF.GoToLineColumn2(i, 1)
            '*  ctrRTF.GoToLineColumn(i, 1, 0)


            ' nCharIndex = ctrRTF.CurrentPosition()
            'If nCharIndex_Last = nCharIndex Then
            '    GoTo Get_Next_Line
            'End If

            'ctrRTF.GetFirstCharIndexFromLine(i) '03.07.2014

            nCharIndex = ctrRTF.GetFirstCharIndexFromLine(i) '>> Good Most of the time -- Moved up 03.07.2014


            '      Dim chkLine As Integer = ctrRTF.GetLineFromCharIndex(nCharIndex) 'Testing 03.07.2014




            '* strLine = Trim(ctrRTF.GetCurrentLineText) '>> Remove extra spaces
            Application.DoEvents()

            If i > ctrRTF.Lines.GetUpperBound(0) Then ' Added 03.22.2018
                Exit For
            End If

            '>> Removed 03.03.2011
            strLine = Trim(ctrRTF.Lines(i))
            'strLine = Remove_Prefix_Tabs(strLine) '>> Remove Prefix Tabs
            'strLine = Valid_ASCII(strLine) '>> Remove all invalid char.s
            intLen = Len(strLine) '>> Get Lenght of line


            'If nCharIndex = 936 Then '>> Use for testing
            '    Beep()
            'End If

            Dim intNumerEnd As Integer
            '>> Search for Parsing Parameters
            '*  If intLen > 1 And i = ctrRTF.CurrentLine Then 'nCharIndex > (nCharIndex_Last + 2)
            If intLen > 1 Then
                '   rsRecordset.MoveFirst()
                sDblQuote = Chr(34)
                sSQuote = Chr(39)

                strLine = Remove_Prefix_Tabs(strLine)
                strPString = String.Empty 'Default

                For x = 1 To intLen '!! ToDo: Check if this should be Zero base
                    intNumerEnd = 0
                    strPString = Mid(strLine, 1, x)
                    '>> Conditions for going to the next line in the selected document
                    ' If Len(strPString) > intMax_Param_Len Then Exit For '>> Check!!

                    '>> Check for Space
                    intNumerEnd = InStr(strPString, " ")
                    If intNumerEnd > 0 Then
                        strPString = Mid(strLine, 1, intNumerEnd - 1)
                        Exit For
                    End If

                    '>> Check for Tab
                    intNumerEnd = InStr(strPString, vbTab)
                    If intNumerEnd > 0 Then
                        strPString = Mid(strLine, 1, intNumerEnd - 1)
                        Exit For
                    End If

                    '>> Check for Double Quotes
                    intNumerEnd = InStr(strPString, sDblQuote)
                    If intNumerEnd > 0 Then
                        strPString = Mid(strLine, 1, intNumerEnd - 1)
                        Exit For
                    End If

                    'Check for Single Quotes
                    intNumerEnd = InStr(strPString, sSQuote)
                    If intNumerEnd > 0 Then
                        strPString = Mid(strLine, 1, intNumerEnd - 1)
                        Exit For
                    End If
                    '<<
                Next
                If intNumerEnd <> 0 Then '>> If a possible Number was Not found then skip below
                    If Len(strPString) > 1 Then '>> Changed to > 1 because a pattern is always > 1
                        If Len(strPString) < 21 Then '>> Max. Lenght for Section No. is 20, thus if greater than 20 must not be a Section No.

                            '>> Loop thru the Parsing Parmeters
                            'For Each txtData As Atebion.TxtReaderWriter.Manager.TextData In al // Removed 02.02.2013
                            '  For Each txtData As String In al 'Added 02.02.2013
                            Dim txtData As String
                            txtData = String.Empty
                            Dim p As Integer
                            For p = 0 To al.Count - 1

                                txtData = TryCast(al.Item(p), String)

                                '  Do While Not rsRecordset.EOF
                                ' strParameter = rsRecordset.Fields("Parse_Parameter").Value // Removed 02.02.2013
                                strParameter = txtData 'Added 02.02.2013
                                'Debug.WriteLine("strParameter: " + strParameter)
                                'Debug.WriteLine("strPString: " + strPString)
                                'If Len(strPString) > 1 Then '>> Changed to > 1 because a pattern is always > 1
                                '    If Len(strPString) < 21 Then '>> Max. Lenght for Section No. is 20, thus if greater than 20 must not be a Section No.

                                If strPString Like strParameter Then
                                    '>> Check for invalid results -- Comment-out 5.24.2013, was filtering out numbering "1.", "2", "3.", ...   Wrote 04.10.2013
                                    'Dim xstrPString As String
                                    'xstrPString = strPString.Trim()

                                    'If strParameter = "*[a-z])" Or strParameter = "*[a-z]." Or strParameter = "*[A-Z])" Or strParameter = "*[A-Z]." Then
                                    '    If xstrPString.Length > 2 Then 'If strPString.Length > 3 Then
                                    '        GoTo Get_Next_Line
                                    '    End If
                                    'End If

                                    '>> Added 09.21.2013 -- Skip
                                    Dim xstrPString As String
                                    xstrPString = strPString.Trim()

                                    If strParameter = "*[a-z]." Then
                                        If xstrPString.Length > 5 Then '>> changed from 4 to 5 -- 7.25.2017
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If

                                    If strParameter = "*[A-Z]." Then
                                        If xstrPString.Length > 5 Then '>> changed from 4 to 5 -- 7.25.2017
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If

                                    If strParameter = "*[a-z])" Then
                                        If xstrPString.Length > 5 Then '>> changed from 4 to 5 -- 7.25.2017
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If


                                    If strParameter = "*([A-Z])" Then
                                        If xstrPString.Length > 7 Then
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If

                                    If strParameter = "*[A-Z])" Then '>> Added 10.02.2013
                                        If xstrPString.Length > 7 Then
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If

                                    If strParameter = "*.##" Then '>> Added 10.02.2013
                                        If xstrPString.Trim.Substring(0, 1) = "$" Then
                                            bLastLoopFound = False
                                            GoTo Get_Next_Line
                                        End If
                                    End If


                                    '<< End Added


                                    '>> Get end of Parsed Parameter Number
                                    x1 = InStr(strLine, " ")
                                    x2 = InStr(strLine, vbTab)
                                    If x2 <> 0 Then
                                        If x1 <> 0 Then
                                            If x2 < x1 Then
                                                x = x2
                                            Else
                                                x = x1
                                            End If
                                        Else
                                            x = x2
                                        End If
                                    Else
                                        x = x1
                                    End If

                                    'Redundant! >> strPString = Mid(strLine, 1, x)

                                    ' If Len(strPString) > 0 Then
                                    'If Len(strPString) > 1 Then '>> Changed to > 1 because a pattern is always > 1
                                    '    If Len(strPString) < 21 Then '>> Max. Lenght for Section No. is 20, thus if greater than 20 must not be a Section No.

                                    If Len(x) <> 0 Then
                                        If x <> 0 Then
                                            strCaption = Trim(Mid(strLine, x))
                                            ' If Len(strCaption) > 50 Then
                                            If strCaption.Length > 50 Then 'Change 09.21.2013
                                                strCaption = Truncate_String(strCaption, 50, True)
                                            End If
                                        Else
                                            strCaption = Trim(strLine)
                                            strCaption = Truncate_String(strCaption, 15, True)
                                        End If
                                    End If


                                    '>> Code for Section Length
                                    If intFoundCount <> -1 Then '>> If there were previous sections found
                                        mdsParametersFound.Tables(0).Rows(intFoundCount).Item(ParametersFound_FieldNames.SectionLength) = intSectionLength
                                        intFoundCount = intFoundCount + 1
                                        'intAdjuster = strPString.Length + strCaption.Length + 2 '>> add to 2 for typical spaces
                                        'If strLine.Length < ctrRTF.Lines(i).Length Then
                                        '    intAdjuster = intAdjuster + (ctrRTF.Lines(i).Length - strLine.Length)
                                        'End If
                                    Else
                                        intFoundCount = 0 '>> Zero base, thus the 1st item
                                        intAdjuster = 0
                                    End If

                                    '>> Adjust the lenght to account for the found Number and Title
                                    'intSectionLength = ctrRTF.Lines(i).Length + 0  '>> Reset
                                    'If intAdjuster < intSectionLength Then
                                    '    intSectionLength = intSectionLength - intAdjuster
                                    'End If

                                    bLastLoopFound = True
                                    '<<


                                    '>> Gives false values -- Do Not Use! -- nCharIndex = ctrRTF.GetCharIndexFromPosition(System.Windows.Forms.Cursor.Position) '>> 04/11/06

                                    ctrRTF.Focus()
                                    '   nCharIndex = ctrRTF.GetFirstCharIndexFromLine(i) '>> Good Most of the time






                                    '>> Testing Code -- Remove after testing
                                    'If nCharIndex = 85256 Or nCharIndex = 86187 Or nCharIndex = 87057 Or nCharIndex = 87759 Then
                                    '    Beep()
                                    'End If


                                    '<<

                                    'strPString = Remove_ASCII9(strPString)
                                    'strCaption = Remove_ASCII9(strCaption)

                                    dr = mdsParametersFound.Tables(0).NewRow()

                                    dr.Item(ParametersFound_FieldNames.Parameter) = strParameter
                                    dr.Item(ParametersFound_FieldNames.Index) = nCharIndex.ToString()
                                    dr.Item(ParametersFound_FieldNames.LineStart) = i
                                    dr.Item(ParametersFound_FieldNames.Found) = Trim(strPString)
                                    dr.Item(ParametersFound_FieldNames.Caption) = Trim(strCaption)

                                    '              dr.Item(ParametersFound_FieldNames.SectionLength) = 

                                    If booParse_Hierarchical Then
                                        If strParameter <> strLast_Parameter Then
                                            If FindParameterLevel(strParameter) = -1 Then
                                                Dim parm As New ParameterLevels

                                                parm.Parameter = strParameter.Trim()
                                                If strLast_Parameter.Length = 0 Then
                                                    parm.Parent = "Parent: Root"
                                                Else
                                                    parm.Parent = "Parent: " & strLast_Parameter
                                                End If
                                                listParameterLevels.Add(parm)
                                                dr.Item(ParametersFound_FieldNames.Parent) = parm.Parent
                                            End If
                                        End If
                                    End If

                                    mdsParametersFound.Tables(0).Rows.Add(dr)

                                    'If booParse_Hierarchical Then
                                    '    If strParameter <> strLast_Parameter Then
                                    '        If lvFind(ctrlParam_Levels, strParameter, True, 0, False) = -1 Then
                                    '            xItem = New ListViewItem
                                    '            xItem.Text = Trim(strParameter)
                                    '            If strLast_Parameter.Length = 0 Then
                                    '                xItem.SubItems.Add("Parent: Root")
                                    '                strFirst_Param_Found = strParameter
                                    '            Else
                                    '                xItem.SubItems.Add("Parent: " & strLast_Parameter)
                                    '            End If
                                    '            ctrlParam_Levels.Items.Add(xItem)
                                    '        End If
                                    '    End If
                                    'End If

                                    strLast_Parameter = strParameter
                                    GoTo Get_Next_Line '>> Not a fan of Goto, but it works well in this case
                                End If
                            Next
                        End If
                    End If
                End If
            End If
Get_Next_Line:
            ' nCharIndex = nCharIndex + ctrRTF.Lines(i).Length
            If bLastLoopFound = False Then
                intSectionLength = intSectionLength + ctrRTF.Lines(i).Length
            End If
            bLastLoopFound = False

        Next


        '>> ToDo
        'If booDisplay_Percentage Then
        '    intPercent = Get_Percent(i, intCount)
        '    Call SetMeter(CSng(intPercent), mfrmPercent_Form)
        'End If


        mdsParseResults = Nothing
        mdsParseResults = CreateDataset_ParseResults()

        '>> Add Header to Parsed sections 
        '>> Added 10.02.2013
        If mdsParametersFound.Tables(0).Rows.Count > 1 Then '>> Changed From: mdsParametersFound.Tables(0).Rows.Count > 0 -- 11.02.2013
            If mdsParametersFound.Tables(0).Rows(1).Item(ParametersFound_FieldNames.LineStart) = 0 Then
                mdsParametersFound.Tables(0).Rows(0).Delete()

            End If
        End If
        '<<


        'If listParseResults.Count > 0 Then
        '    ' If listParseResults(0).Index <> "1" Then 'ToDo -- might make it zero, need to check If listParseResults(0).Index <> "1" Then
        '    If listParseResults(0).Index <> "0" Then 'ToDo -- might make it zero, need to check If listParseResults(0).Index <> "1" Then
        '        Dim pResult As New ParseResults()

        '        pResult.Parameter = Trim("Header")
        '        pResult.Index = "1"
        '        pResult.LineStart = -1 '"N/A"
        '        pResult.Found = "Header Area"
        '        pResult.Caption = "Header Area"
        '        pResult.SortOrder = 1
        '        listParseResults.Add(pResult)

        '        pResult = Nothing

        '        dr = mdsParseResults.Tables(0).NewRow()

        '        dr.Item(ParseResults_FieldNames.Parameter) = "Header"
        '        dr.Item(ParseResults_FieldNames.IndexStart) = 1 'dr.Item(ParseResults_FieldNames.IndexStart) = 1
        '        dr.Item(ParseResults_FieldNames.LineStart) = -1 '"N/A"    dr.Item(ParseResults_FieldNames.LineStart) = -1
        '        dr.Item(ParseResults_FieldNames.Number) = Trim(strPString)
        '        dr.Item(ParseResults_FieldNames.Caption) = Trim(strCaption)

        '        mdsParseResults.Tables(0).Rows.Add(dr)

        '    End If
        'End If


        '>> Save Results to files
        SaveParseResultsList(Me.ParseResultsFile)
        SavelistParameterLevels(Me.ParametersResultsFile)


        ' Call AppLvMgr.AdjustColumnWidth(ctlListView, True)
        ' ctrRTF.TextType = intTextType
        ctrRTF.WordWrap = False 'bWordWrap
        ctrRTF.ZoomFactor = sZoomFactor

        stopWatch.Stop()
        Dim ts As TimeSpan = stopWatch.Elapsed
        mElapsedTime_Analyze_Doc_Parameters = String.Format("{0:00}:{1:00}:{2:00}.{3:00} for {4} Lines", ts.Hours, ts.Minutes, ts.Seconds, ts.Milliseconds / 10, intCount + 1)
        Beep()
        Return True

        'Catch ex As Exception
        '    msnLogger.LogExceptionMessage(ex)
        '    ctrRTF.WordWrap = True
        '    ctrRTF.ZoomFactor = 1.0
        '    Return False
        'End Try




    End Function

    Public Function GetUniqueCode() As String
        Dim allChars As String = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"

        Dim GotUniqueCode As Boolean = False
        Dim uniqueCode As String = ""

        Dim str As New System.Text.StringBuilder

        For i As Byte = 1 To 10 'length of req key
            Dim xx As Integer
            Randomize()
            xx = Rnd() * (Len(allChars) - 1) 'number of rawchars
            str.Append(allChars.Trim.Chars(xx))
        Next

        uniqueCode = str.ToString

        Return uniqueCode

    End Function





    Public Function Load_ParseResults_Listviews(ByVal ctlListView As System.Windows.Forms.ListView, ByVal ctrlParam_Levels As System.Windows.Forms.ListView) As Boolean
        '>> Loads Parsing Results from a Previous Parse Run via files
        Dim bReturn As Boolean

        bReturn = LoadLvFile(Me.ParseResultsFile, ctlListView)
        If bReturn = False Then
            Return False '>> Don't attempt to load the Parameter Results
        End If
        Return LoadLvFile(Me.ParametersResultsFile, ctrlParam_Levels)

    End Function

    'Public Function Find_Keywords(ByVal ctrRTF As Atebion.Windows.Forms.RichTextBox, ByVal ctlLVKeyWords As System.Windows.Forms.ListView, ByVal ctlLstKeywords_Sel As System.Windows.Forms.CheckedListBox, ByVal ctlLVTotal_Keywords As System.Windows.Forms.ListView, Optional ByVal booSaveKeyword_Locations As Boolean = False) As Boolean

    Public Function Find_Keywords(ByVal ctrRTF As Atebion.RTFBox.RichTextBox, ByVal dsKeywords As DataSet, ByVal SelectColor As Color, Optional ByVal booSaveKeyword_Locations As Boolean = False) As DataSet

        Dim intCount As Integer
        Dim intTotalFound As Integer
        Dim i As Integer
        Dim strKeyword As String
        Dim strKeywordsFound() As String
        Dim bReturn As Boolean

        Dim bWordWrap As Boolean

        ' CreateDataset_KeywordsFound

        bWordWrap = ctrRTF.WordWrap
        ctrRTF.WordWrap = False

        ' intCount = ctlLstKeywords_Sel.CheckedItems.Count // Old
        intCount = dsKeywords.Tables(0).Rows.Count


        If intCount = 0 Then
            Return Nothing
            'Exit Function
        End If
        intCount = intCount - 1
        intTotalFound = 0 '>> Counts total qty of keywords found
        ctrRTF.ClearKeywords()

        If SelectColor = Color.Empty Then ' This is passed in, check to see if it was set, if not set it to YellowGreen
            SelectColor = Color.YellowGreen ' ToDo: Wanted to set the color as an optional parameter w/ a default color but couldn't find the correct syntax 
        End If

        Dim dr As DataRow
        Dim dt As DataTable
        dt = dsKeywords.Tables(0)
        For Each dr In dt.Rows
            strKeyword = dr("Keyword")
            ctrRTF.Highlight(strKeyword, SelectColor, False, True)

            Application.DoEvents()
        Next

        'For i = 0 To intCount
        '    strKeyword = ctlLstKeywords_Sel.CheckedItems(i).ToString
        '    ctrRTF.Highlight(strKeyword, color, False, True)
        'Next

        mdsKeywordsFound = ctrRTF.GetDataset_KeywordsFound()

        strKeywordsFound = ctrRTF.KeywordsFound '>> Return a string Array with all findings

        ' ToDo: Remove after testing
        'bReturn = Populate_Keyword_Listviews(strKeywordsFound)

        'If bReturn = True Then
        '    SaveLvFile(KeyWordsFoundFile, ctlLVKeyWords)
        '    SaveLvFile(KeywordsTotalFoundFile, ctlLVTotal_Keywords)
        'End If

        ctrRTF.WordWrap = False 'bWordWrap
        Return mdsKeywordsFound


    End Function

    Public Function Load_Keyword_Listviews(ByRef ctlLVKeyWords As ListView, ByRef ctlLVTotal_Keywords As ListView) As Boolean
        '>> Loads from pervious results that are held in files
        Dim bReturn As Boolean

        bReturn = LoadLvFile(KeyWordsFoundFile, ctlLVKeyWords)
        If bReturn = False Then
            Return False '>> And don't attempt to load Totals 
        End If
        Return (LoadLvFile(KeywordsTotalFoundFile, ctlLVTotal_Keywords))
    End Function

    'Private Function Populate_Keyword_Listviews(ByVal strKeywordsFound() As String) As Boolean
    '    Try

    '        '>> Populates from the Find Keywords function
    '        Dim intCount As Integer
    '        Dim i As Integer
    '        Dim ExtendLeftCol As Integer = -2

    '        'With ctlLVKeyWords
    '        '    .Items.Clear()

    '        '    .Columns.Add("Keywords", 100, HorizontalAlignment.Center)
    '        '    .Columns.Add("Line:", 50, HorizontalAlignment.Center)
    '        '    .Columns.Add("Column:", 50, HorizontalAlignment.Center)
    '        '    .Columns.Add("Index:", 50, HorizontalAlignment.Center)
    '        'End With

    '        'With ctlLVTotal_Keywords
    '        '    .Items.Clear()
    '        '    .Columns.Add("Keyword", 200, HorizontalAlignment.Center)
    '        '    .Columns.Add("Total", 100, HorizontalAlignment.Center)
    '        'End With

    '        listKeywords.Clear()
    '        listKeywordsSummary.Clear()


    '        intCount = strKeywordsFound.Length
    '        If intCount = 0 Then
    '            Return False '>> No Keywords found
    '        End If

    '        'If strKeywordsFound(0) Is Nothing Then
    '        '    Return False '>> No Keywords found
    '        'End If

    '        Dim sLastKeyword As String = ""
    '        Dim intCounter As Integer = 1
    '        '   Dim lvi As ListViewItem
    '        Dim sParse As String()

    '        'For i = 0 To intCount - 1
    '        '    If Not strKeywordsFound(i) Is Nothing Then '>> Added due a bug in the RTF control
    '        '        sParse = strKeywordsFound(i).Split("|")
    '        '        lvi = New ListViewItem(sParse)
    '        '        ctlLVKeyWords.Items.Add(lvi)
    '        '        If sLastKeyword = sParse(0) Then
    '        '            intCounter = intCounter + 1
    '        '        Else
    '        '            If sLastKeyword.Length > 0 Then
    '        '                Dim sArray1 As String() = {sLastKeyword, intCounter.ToString()}
    '        '                lvi = New ListViewItem(sArray1)
    '        '                ctlLVTotal_Keywords.Items.Add(lvi)
    '        '                intCounter = 1 '>> Reset
    '        '            End If
    '        '            sLastKeyword = sParse(0)
    '        '        End If
    '        '    End If
    '        'Next

    '        'New Code -- Replaces Listviews -- 01.13.2010
    '        Dim keywordsFound As New KeywordsFound
    '        Dim Keywords As New Keywords
    '        Dim keywordSummary As KeywordsSummary
    '        For i = 0 To intCount - 1
    '            If Not strKeywordsFound(i) Is Nothing Then '>> Added due a bug in the RTF control
    '                sParse = strKeywordsFound(i).Split("|")
    '                keywordsFound.Keyword = sParse(0)
    '                If sParse(1) <> Nothing Then
    '                    keywordsFound.Line = Convert.ToInt32(sParse(1))
    '                End If
    '                If sParse(2) <> Nothing Then
    '                    keywordsFound.Column = Convert.ToInt32(sParse(2))
    '                End If
    '                If sParse(3) <> Nothing Then
    '                    keywordsFound.Index = Convert.ToInt32(sParse(3))
    '                End If
    '                If sLastKeyword = sParse(0) Then
    '                    intCounter = intCounter + 1
    '                Else
    '                    If sLastKeyword.Length > 0 Then
    '                        keywordSummary = New KeywordsSummary()
    '                        keywordSummary.Keyword = sLastKeyword
    '                        keywordSummary.Total = intCounter
    '                        listKeywordsSummary.Add(keywordSummary)
    '                        keywordSummary = Nothing
    '                    End If

    '                End If

    '                listKeywords.Add(Keywords)
    '                Keywords = Nothing
    '            End If

    '        Next

    '        'Dim sArray2 As String() = {sLastKeyword, intCounter.ToString()}
    '        'lvi = New ListViewItem(sArray2)
    '        'ctlLVTotal_Keywords.Items.Add(lvi)

    '        'New Code -- Replaces Listviews -- 01.13.2010
    '        keywordSummary = New KeywordsSummary()
    '        keywordSummary.Keyword = sLastKeyword
    '        keywordSummary.Total = intCounter
    '        listKeywordsSummary.Add(keywordSummary)
    '        keywordSummary = Nothing

    '        Return True

    '    Catch ex As Exception
    '        msnLogger.LogExceptionMessage(ex)
    '        Return False
    '    End Try


    '    '>> Adjust Right Column to fill LV area
    '    'ctlLVTotal_Keywords.Columns(ctlLVTotal_Keywords.Columns.Count - 1).Width = ExtendLeftCol
    '    'ctlLVKeyWords.Columns(ctlLVKeyWords.Columns.Count - 1).Width = ExtendLeftCol

    'End Function


#End Region

#Region "Public Properties"
    Private Const ProductName As String = "AtebionParse"

    Private mElapsedTime_Analyze_Doc_Parameters As String
    Public ReadOnly Property ElapsedTime_Analyze_Doc_Parameters As String
        Get
            Return mElapsedTime_Analyze_Doc_Parameters
        End Get
    End Property


    Private mvarMax_Parse_Caption As Integer
    Public Property Max_Parse_Caption As Integer
        Get
            Return mvarMax_Parse_Caption
        End Get
        Set(ByVal value As Integer)
            mvarMax_Parse_Caption = value
        End Set
    End Property


    Private mvarParse_Run_UID As String
    Public ReadOnly Property Parse_Run_UID As String
        Get
            Return mvarParse_Run_UID
        End Get
    End Property
    Private mdsKeywordsFound As DataSet
    Public Property KeywordsFound() As DataSet
        Get
            Return mdsKeywordsFound
        End Get
        Set(ByVal value As DataSet)
            mdsKeywordsFound = value
        End Set
    End Property


    Private mdsParametersFound As DataSet
    Public ReadOnly Property ParametersFound() As DataSet
        Get
            Return mdsParametersFound
        End Get
    End Property

    Private msResultPath As String
    Public Property ResultPath() As String
        Get
            Return msResultPath
        End Get
        Set(ByVal Value As String)
            msResultPath = Value
        End Set
    End Property
    ' Private msnLogger As Atebion.Logger.LoggerMgr
    Private mstrLogFile As String
    Public Property LogFile() As String
        Get
            Return mstrLogFile
        End Get
        Set(ByVal Value As String)
            mstrLogFile = Value
            '    msnLogger = New Atebion.Logger.LoggerMgr(mstrLogFile)
        End Set
    End Property

    Public ReadOnly Property ParametersResultsFile() As String
        Get
            Return msResultPath + "\" + Constants_Parse.ParametersResults_File
        End Get
    End Property
    Private mstrKeywordListFile As String
    Public Property KeywordListFile() As String
        Get
            ' 02.17.2013 Return Application.StartupPath + "\" + Constants_Parse.KeywordList_File
            Return Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\" + Constants_Parse.KeywordList_File ' Added 02.17.2013
        End Get
        Set(ByVal value As String)
            mstrKeywordListFile = value
        End Set
    End Property
    Private mstrParseParameterFile As String = String.Empty
    Private mbooUseEncryptedParameters_File As Boolean ' Added 02.01.2013
    Public ReadOnly Property ParseParameterFile() As String
        ' >> Removed 02.17.2013
        'Get
        '    If mstrParseParameterFile = String.Empty Then
        '        If File.Exists(Application.StartupPath + "\" + Constants_Parse.ParseParameters_File) Then
        '            mstrParseParameterFile = Application.StartupPath + "\" + Constants_Parse.ParseParameters_File
        '            mbooUseEncryptedParameters_File = False
        '        ElseIf File.Exists(Application.StartupPath + "\" + Constants_Parse.ParseEncryptedParameters_File) Then ' Added 02.01.2013
        '            mstrParseParameterFile = Application.StartupPath + "\" + Constants_Parse.ParseEncryptedParameters_File
        '            mbooUseEncryptedParameters_File = True
        '        End If
        '    Else
        '        If Not File.Exists(mstrParseParameterFile) Then
        '            mstrParseParameterFile = String.Empty
        '        End If
        '    End If

        '    Return mstrParseParameterFile
        'End Get

        ' >> Added 02.17.2013
        Get
            Dim path As String
            ' removed 2.24.2013 path = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
            path = Application.StartupPath '>> Changed 2.24.2013 

            If mstrParseParameterFile = String.Empty Then
                If File.Exists(path + "\" + Constants_Parse.ParseParameters_File) Then
                    mstrParseParameterFile = path + "\" + Constants_Parse.ParseParameters_File
                    mbooUseEncryptedParameters_File = False
                ElseIf File.Exists(path + "\" + Constants_Parse.ParseEncryptedParameters_File) Then ' Added 02.01.2013
                    mstrParseParameterFile = path + "\" + Constants_Parse.ParseEncryptedParameters_File
                    mbooUseEncryptedParameters_File = True
                End If
            Else
                If Not File.Exists(mstrParseParameterFile) Then
                    mstrParseParameterFile = String.Empty
                End If
            End If

            Return mstrParseParameterFile
        End Get


        'Set(ByVal value As String)
        '    mstrParseParameterFile = value
        'End Set
    End Property

    Public ReadOnly Property Version() As String
        Get
            Return "3.4"
        End Get
    End Property


    Public ReadOnly Property KeyWordsFoundFile() As String
        Get
            Return msResultPath + "\" + Constants_Parse.KeyWordsFound_File
        End Get
    End Property

    Public ReadOnly Property KeywordsTotalFoundFile() As String
        Get
            Return msResultPath + "\" + Constants_Parse.KeyWordsTotalFound_File
        End Get
    End Property

    Public ReadOnly Property ParseResultsFile() As String
        Get
            Return msResultPath + "\" + Constants_Parse.ParseResults_File
        End Get
    End Property

    Private mdsParseResults As DataSet
    Public ReadOnly Property ParseResults() As DataSet
        Get
            Return mdsParseResults
        End Get
    End Property

    Private mdsXRefRowIDs As DataSet
    Public ReadOnly Property XRefRowIDs() As DataSet
        Get
            Return mdsXRefRowIDs
        End Get
    End Property

    Private mdsValidationResults As DataSet
    Public ReadOnly Property ValidationResults() As DataSet
        Get
            Return mdsValidationResults
        End Get
    End Property

#End Region

#Region "Private Functions"

    '    Private Function Parse_Item3(ByRef ctrRTF As Atebion.Windows.Forms.RichTextBox, ByRef ctrTo_RTF As Atebion.Windows.Forms.RichTextBox, ByRef strKey As String) As Boolean
    '        Dim booWrapAutomatically As Boolean
    '        Dim lngTo_Y As Integer
    '        Dim strPKey() As String '>> Parse key
    '        Dim intK_Count As Short
    '        Dim i As Integer
    '        Dim booSubtractY As Boolean

    '        '>> Used for Split
    '        Dim lngSelStartX As Integer
    '        Dim lngSelEndX As Integer

    '        '>> Added 03/14/04
    '        Dim TextType As Short
    '        'TextType = ctrRTF.TextType
    '        'ctrRTF.TextType = 1 'DOS Text
    '        '<<


    '        'booWrapAutomatically = ctrRTF.WrapAutomatically
    '        'If booWrapAutomatically Then
    '        '    ctrRTF.WrapAutomatically = False
    '        'End If

    '        booWrapAutomatically = ctrRTF.WordWrap
    '        If booWrapAutomatically Then
    '            ctrRTF.WordWrap = False
    '        End If


    '        frmMDIParse.DefInstance.cmdGoToSec_Click(frmMDIParse.DefInstance.cmdGoToSec, New System.EventArgs()) '>> Added 11/28/04

    '        ctrRTF.Select()

    '        '  Call mobjParse_Key.Select_Text_by_Key(ctrRTF, strKey) '>> Removed 11/28/04

    '        ctrRTF.Action = 1 '>> Copy to clipboard
    '        System.Windows.Forms.Application.DoEvents()
    '        ctrTo_RTF.TextAll = ""
    '        ctrTo_RTF.Action = 2 ' paste selection

    '        '   '>> Get Split Caption
    '        '    intTextType = ctrTo_RTF.TextType
    '        '    ctrTo_RTF.TextType = 1 '>> Dos type
    '        '    strTitle = ctrTo_RTF.Text
    '        '    strTitle = Truncate_String(strTitle, mvarMax_Parse_Caption, True)
    '        '    ctrTo_RTF.TextType = intTextType

    '        Parse_Item3 = True
    '        Exit Function

    'Err_Parse_Item3:
    '        MsgBox(Err.Description & vbCrLf & vbCrLf & "An error has occurred while parsing an item (Parse Type 3).", MsgBoxStyle.Exclamation, "Error No.: " & Err.Number)
    '        Exit Function

    '    End Function

    Private Function Populate_mdsParseResults(ByVal ctrRTF As Atebion.RTFBox.RichTextBox) As Boolean
        Dim bReuslts As Boolean = False
        If mdsParseResults.Tables.Count < 1 Then
            Return False
        End If

        Dim i As Integer
        Dim intRowCount As Integer
        intRowCount = mdsParametersFound.Tables(0).Rows.Count
        If intRowCount = 0 Then
            Return False
        End If
        intRowCount = intRowCount - 1 '>> Because Rows are zero based
        Dim dr As DataRow

        Dim sIndexStart As String
        Dim sIndexEnd As String

        Dim intLineEnd As Integer

        mdsParseResults.Tables(0).Rows.Clear() '>> Clear previous records

        Dim adjIndexStart As Integer '>> Added 03.31.2011

        For i = 0 To intRowCount
            '>> Testing Code
            'If i = 49 Then
            '    Beep()
            'End If
            '<<

            dr = mdsParseResults.Tables(0).NewRow
            With mdsParametersFound.Tables(0)

                dr.Item(ParseResults_FieldNames.Caption) = .Rows(i).Item(ParametersFound_FieldNames.Caption)
                '>> Not currently used -- dr.Item(ParseResults_FieldNames.ColumnEnd) = 
                dr.Item(ParseResults_FieldNames.ColumnStart) = 0
                dr.Item(ParseResults_FieldNames.FileName) = String.Concat(i.ToString(), ".rtf")
                sIndexStart = .Rows(i).Item(ParametersFound_FieldNames.Index)
                adjIndexStart = CInt(sIndexStart) '>> Added 03.31.2011
                sIndexEnd = GetIndexEnd(ctrRTF, i, intRowCount, adjIndexStart) '>> the IndexStart will be adjusted if IndexStart = IndexEnd
                dr.Item(ParseResults_FieldNames.IndexEnd) = sIndexEnd
                dr.Item(ParseResults_FieldNames.IndexStart) = adjIndexStart.ToString() '>> Changed 03.31.2011
                intLineEnd = GetLineEnd(ctrRTF, i, intRowCount)
                dr.Item(ParseResults_FieldNames.Keywords) = GetKeywords4PSection(CInt(adjIndexStart), CInt(sIndexEnd), i.ToString())
                dr.Item(ParseResults_FieldNames.LineEnd) = intLineEnd
                dr.Item(ParseResults_FieldNames.LineStart) = .Rows(i).Item(ParametersFound_FieldNames.LineStart)
                dr.Item(ParseResults_FieldNames.Number) = .Rows(i).Item(ParametersFound_FieldNames.Found)
                dr.Item(ParseResults_FieldNames.Parameter) = .Rows(i).Item(ParametersFound_FieldNames.Parameter)
                dr.Item(ParseResults_FieldNames.Parent) = .Rows(i).Item(ParametersFound_FieldNames.Parent)
                dr.Item(ParseResults_FieldNames.SectionLength) = .Rows(i).Item(ParametersFound_FieldNames.SectionLength)
                dr.Item(ParseResults_FieldNames.UID) = i.ToString()
                dr.Item(ParseResults_FieldNames.SortOrder) = i
            End With
            mdsParseResults.Tables(0).Rows.Add(dr)
        Next

        Return True

    End Function
    ''' <summary>
    ''' Diagnostic process that checks results
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function Validate_Results() As Integer
        Dim intCount As Integer = 0

        Dim i As Integer
        Dim intRowCount As Integer
        intRowCount = mdsParseResults.Tables(0).Rows.Count
        If intRowCount = 0 Then
            Return intCount
        End If

        '  mdsValidationResults = CreateDataset_ValidationResults()
        Dim dr As DataRow

        intRowCount = intRowCount - 1 '>> For a zero base subtract 1
        For i = 0 To intRowCount
            With mdsParseResults.Tables(0)

                '>> Check 1 -- Does IndexEnd = IndexStart? [Should never occur]
                If .Rows(i).Item(ParseResults_FieldNames.IndexEnd) = .Rows(i).Item(ParseResults_FieldNames.IndexStart) Then

                    dr = mdsValidationResults.Tables(0).NewRow
                    dr.Item(ValidationResults_FieldNames.ResultsUID) = .Rows(i).Item(ParseResults_FieldNames.UID)
                    dr.Item(ValidationResults_FieldNames.Number) = .Rows(i).Item(ParseResults_FieldNames.Number)
                    dr.Item(ValidationResults_FieldNames.Caption) = .Rows(i).Item(ParseResults_FieldNames.Caption)
                    dr.Item(ValidationResults_FieldNames.Severity) = 2
                    dr.Item(ValidationResults_FieldNames.Description) = "Section is blank. – The parsed section begins and ends at the same point."
                    mdsValidationResults.Tables(0).Rows.Add(dr)

                    intCount = intCount + 1
                End If


            End With
            Application.DoEvents()
        Next




        Return intCount
    End Function
    ''' <summary>
    ''' Get Keywords found in entire file. -- Used with Manual Split Section
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Function GetKeywords4Doc(ByVal ParseSection_UID As String) As String
        Dim intRowCount As Integer

        If mdsKeywordsFound Is Nothing Then ' This occurs if No Keyword search has occured.
            Return String.Empty
        End If

        intRowCount = mdsKeywordsFound.Tables(0).Rows.Count

        If mdsKeywordsFound.Tables.Count = 0 Or intRowCount = 0 Then
            Return String.Empty
        End If

        Dim i As Integer
        Dim keywordIndex As Integer
        Dim sb As New StringBuilder
        Dim sKeyword As String
        Dim foundCount As Integer
        Dim sFoundInPSections As String

        foundCount = 0

        intRowCount = intRowCount - 1
        For i = 0 To intRowCount
            sKeyword = mdsKeywordsFound.Tables(0).Rows(i).Item("Keyword")
            foundCount = foundCount + 1
            sb = AppendKeywordsSB(sb, sKeyword)
            'If foundCount > 1 Then
            '    sb.Append(", ") '>> Place a comma after the previous keyword found for this parsed section
            'End If
            'sb.Append(sKeyword) '>> this will be returned

            '>> Save Parsed Section UID as a comma delimited value into the Keywords Found dataset
            If IsDBNull(mdsKeywordsFound.Tables(0).Rows(i).Item("FoundInPSections")) Then
                sFoundInPSections = String.Empty
            Else
                sFoundInPSections = mdsKeywordsFound.Tables(0).Rows(i).Item("FoundInPSections")
            End If

            If Trim(sFoundInPSections) = String.Empty Then
                mdsKeywordsFound.Tables(0).Rows(i).Item("FoundInPSections") = ParseSection_UID
            Else
                mdsKeywordsFound.Tables(0).Rows(i).Item("FoundInPSections") = String.Concat(sFoundInPSections, ",", ParseSection_UID)
            End If
        Next

        Return sb.ToString()

    End Function

    ''' <summary>
    ''' Get Keywords found in parsed section per previous keyword search
    ''' </summary>
    ''' <param name="IndexStart"></param>
    ''' <param name="IndexEnd"></param>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Private Function GetKeywords4PSection(ByVal IndexStart As Integer, ByVal IndexEnd As Integer, ByVal ParseSection_UID As String) As String
        Dim intRowCount As Integer

        If mdsKeywordsFound Is Nothing Then ' This occurs if No Keyword search has occured.
            Return String.Empty
        End If

        intRowCount = mdsKeywordsFound.Tables(0).Rows.Count

        If mdsKeywordsFound.Tables.Count = 0 Or intRowCount = 0 Then
            Return String.Empty
        End If

        Dim i As Integer
        Dim keywordIndex As Integer
        Dim sb As New StringBuilder
        Dim sKeyword As String
        Dim foundCount As Integer
        Dim sFoundInPSections As String

        foundCount = 0

        intRowCount = intRowCount - 1
        For i = 0 To intRowCount
            keywordIndex = mdsKeywordsFound.Tables(0).Rows(i).Item("Index")

            If keywordIndex >= IndexStart And keywordIndex <= IndexEnd Then '>> If keyword is found within the parsed section index range
                sKeyword = mdsKeywordsFound.Tables(0).Rows(i).Item("Keyword")
                foundCount = foundCount + 1
                sb = AppendKeywordsSB(sb, sKeyword)
                'If foundCount > 1 Then
                '    sb.Append(", ") '>> Place a comma after the previous keyword found for this parsed section
                'End If
                'sb.Append(sKeyword) '>> this will be returned

                '>> Save Parsed Section UID as a comma delimited value into the Keywords Found dataset
                If IsDBNull(mdsKeywordsFound.Tables(0).Rows(i).Item("FoundInPSections")) Then
                    sFoundInPSections = String.Empty
                Else
                    sFoundInPSections = mdsKeywordsFound.Tables(0).Rows(i).Item("FoundInPSections")
                End If

                If Trim(sFoundInPSections) = String.Empty Then
                    mdsKeywordsFound.Tables(0).Rows(i).Item("FoundInPSections") = ParseSection_UID
                Else
                    mdsKeywordsFound.Tables(0).Rows(i).Item("FoundInPSections") = String.Concat(sFoundInPSections, ",", ParseSection_UID)
                End If
                '
            End If
        Next

        Return sb.ToString()

    End Function

    Private Function AppendKeywordsSB(ByVal sb As StringBuilder, ByVal Keyword As String) As StringBuilder
        Dim sContent As String
        Dim intLoc As Integer

        If sb.Length = 0 Then
            sb.Append(Keyword)
            sb.Append(" ")
            sb.Append("[1]")
            Return sb
        End If

        sContent = sb.ToString()

        intLoc = sContent.IndexOf(Keyword, 0)
        If intLoc < 0 Then '>> Keyword was not found in String Builder, so add it
            sb.Append(", ")
            sb.Append(Keyword)
            sb.Append(" ")
            sb.Append("[1]")
            Return sb
        End If


        Dim sLeft As String
        Dim intLoc2 As Integer
        Dim sMid As String
        Dim sRight As String
        Dim intNumber As Integer
        Dim intLoc3 As Integer

        sLeft = sContent.Substring(0, intLoc + Keyword.Length + 1)
        intLoc2 = sContent.IndexOf("]", intLoc)
        sRight = sContent.Substring(intLoc2 + 1)
        intLoc3 = sContent.IndexOf("[", intLoc)

        sMid = Trim(sContent.Substring(intLoc3 + 1, intLoc2 - intLoc3 - 1))
        ' sMid = sMid.Substring(1, sMid.Length - 1)
        If IsNumeric(sMid) = True Then
            intNumber = CInt(sMid)
            intNumber = intNumber + 1 '>> Increment Qty 
            sMid = " [" + intNumber.ToString() + "]" '>> Reformat
            sb.Clear()
            '>> Rebuild
            sb.Append(sLeft)
            sb.Append(sMid)
            sb.Append(sRight)
            Return sb
        End If

        Return sb '>> Should never occur

    End Function

    Private Function GetLineEnd(ByVal ctrRTF As Atebion.RTFBox.RichTextBox, ByVal currentRow As Integer, ByVal rowCount As Integer) As Integer
        Dim endRow As Integer

        endRow = 0

        If currentRow = rowCount Then '>> If Last Row
            endRow = ctrRTF.Lines.Length
        Else
            endRow = CInt(mdsParametersFound.Tables(0).Rows(currentRow + 1).Item(ParametersFound_FieldNames.LineStart)) - 1
        End If

        Return endRow.ToString()


    End Function

    Private Function GetIndexEnd(ByVal ctrRTF As Atebion.RTFBox.RichTextBox, ByVal currentRow As Integer, ByVal rowCount As Integer, ByRef startIndex As Integer) As String
        Dim endIndex As Integer

        endIndex = 0

        ctrRTF.WordWrap = False

        If currentRow = rowCount Then '>> If Last Row
            endIndex = ctrRTF.TextLength
        Else
            endIndex = CInt(mdsParametersFound.Tables(0).Rows(currentRow + 1).Item(ParametersFound_FieldNames.Index)) - 1
        End If

        '>> Added 03.06.2014
        If endIndex = startIndex Then
            'Dim intline As Integer
            'intline = ctrRTF.GetLine(startIndex)
            endIndex = startIndex + ctrRTF.GetLineLength(currentRow)
        End If
        '<<

        ''>> Need to Adjust startIndex if endIndex = startIndex 03.31.2011
        'If endIndex = startIndex Then
        '    Dim intline As Integer
        '    intline = ctrRTF.GetLine(startIndex)
        '    startIndex = endIndex - ctrRTF.GetLineLength(intline)

        '    '>> Testing
        '    If intline <> currentRow Then
        '        Beep()
        '    End If

        '    '<<

        '    ' startIndex = ctrRTF.GetLineStart(currentRow)

        '    '>> testing
        '    'If startIndex = 22772 Then
        '    '    Beep()
        '    'End If
        '    ''<<
        'End If

        Return endIndex.ToString()

    End Function

    Private Function CreateDataset_ValidationResults() As DataSet
        ' Create a new DataTable.
        Dim table As DataTable = New DataTable("ValidationResults")

        ' Declare variables for DataColumn and DataRow objects.
        Dim column As DataColumn

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ValidationResults_FieldNames.ResultsUID
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Add the Column to the DataColumnCollection
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ValidationResults_FieldNames.Number
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ValidationResults_FieldNames.Caption
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ValidationResults_FieldNames.Severity
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ValidationResults_FieldNames.Description
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Instantiate the DataSet variable.
        Dim dataSet As DataSet
        dataSet = New DataSet()

        ' Add the new DataTable to the DataSet.
        dataSet.Tables.Add(table)

        Return dataSet
    End Function

    Private Function CreateDataset_ParseResults() As DataSet

        ' Create a new DataTable.
        Dim table As DataTable = New DataTable("ParseResults")

        ' Declare variables for DataColumn and DataRow objects.
        Dim column As DataColumn

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ParseResults_FieldNames.UID
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ParseResults_FieldNames.Parameter
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ParseResults_FieldNames.Parent
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ParseResults_FieldNames.LineStart
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ParseResults_FieldNames.LineEnd
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ParseResults_FieldNames.SectionLength
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ParseResults_FieldNames.ColumnStart
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ParseResults_FieldNames.ColumnEnd
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ParseResults_FieldNames.IndexStart
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ParseResults_FieldNames.IndexEnd
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Add the Column to the DataColumnCollection
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ParseResults_FieldNames.Number
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ParseResults_FieldNames.Caption
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ParseResults_FieldNames.SortOrder
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ParseResults_FieldNames.FileName
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ParseResults_FieldNames.Keywords
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Instantiate the DataSet variable.
        Dim dataSet As DataSet
        dataSet = New DataSet()

        ' Add the new DataTable to the DataSet.
        dataSet.Tables.Add(table)

        Return dataSet

    End Function

    Private Function CreateDataset_ParametersFound() As DataSet
        ' Create a new DataTable.
        Dim table As DataTable = New DataTable("ParametersFound")

        ' Declare variables for DataColumn and DataRow objects.
        Dim column As DataColumn

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ParametersFound_FieldNames.Parameter
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ParametersFound_FieldNames.Parent
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ParametersFound_FieldNames.Index '>> Char. Index
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ParametersFound_FieldNames.LineStart '>> Char. Index
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = ParametersFound_FieldNames.SectionLength '>> Char. Index
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ParametersFound_FieldNames.Found
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = ParametersFound_FieldNames.Caption
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Instantiate the DataSet variable.
        Dim dataSet As DataSet
        dataSet = New DataSet()

        ' Add the new DataTable to the DataSet.
        dataSet.Tables.Add(table)

        Return dataSet
    End Function

    Public Class XRefRowIDs_FieldNames
        Public Const RowID As String = "RowID"
        Public Const Line As String = "Line"
        Public Const Column As String = "Column"
        Public Const Index As String = "Index"

    End Class
    Private Function CreateDataset_XRefRowIDs() As DataSet
        ' Create a new DataTable.
        Dim table As DataTable = New DataTable("XRefRowIDs")

        ' Declare variables for DataColumn and DataRow objects.
        Dim column As DataColumn

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = XRefRowIDs_FieldNames.RowID
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = XRefRowIDs_FieldNames.Line
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = XRefRowIDs_FieldNames.Column
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = XRefRowIDs_FieldNames.Index
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Make the ID column the primary key column.
        Dim PrimaryKeyColumns(0) As DataColumn
        PrimaryKeyColumns(0) = table.Columns(XRefRowIDs_FieldNames.Index)
        table.PrimaryKey = PrimaryKeyColumns

        ' Instantiate the DataSet variable.
        Dim dataSet As DataSet
        dataSet = New DataSet()

        ' Add the new DataTable to the DataSet.
        dataSet.Tables.Add(table)

        'Return dataset
        CreateDataset_XRefRowIDs = dataSet

    End Function


    Private Function CreateDataset_KeywordsFound() As DataSet
        ' Create a new DataTable.
        Dim table As DataTable = New DataTable("KeyFound")

        ' Declare variables for DataColumn and DataRow objects.
        Dim column As DataColumn

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = KeywordsFound_FieldNames.Keyword
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.String")
        column.ColumnName = KeywordsFound_FieldNames.Category
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = KeywordsFound_FieldNames.Line
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = KeywordsFound_FieldNames.Column
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)

        ' Create new DataColumn, set DataType, ColumnName 
        ' and add to DataTable.    
        column = New DataColumn()
        column.DataType = System.Type.GetType("System.Int32")
        column.ColumnName = KeywordsFound_FieldNames.Index
        column.ReadOnly = False
        column.Unique = False

        ' Add the Column to the DataColumnCollection.
        table.Columns.Add(column)


        ' Make the ID column the primary key column.
        Dim PrimaryKeyColumns(0) As DataColumn
        PrimaryKeyColumns(0) = table.Columns(KeywordsFound_FieldNames.Index)
        table.PrimaryKey = PrimaryKeyColumns

        ' Instantiate the DataSet variable.
        Dim dataSet As DataSet
        dataSet = New DataSet()

        ' Add the new DataTable to the DataSet.
        dataSet.Tables.Add(table)

        'Return dataset
        CreateDataset_KeywordsFound = dataSet

    End Function

    Public Function CleanString(ByRef inputString As String) As String 'Added 03.11.2014

        Dim sReturn As String
        ' sReturn = Regex.Replace(s, "[^\u0000-\u007F]", String.Empty)


        sReturn = Encoding.ASCII.GetString(Encoding.Convert(Encoding.UTF8, Encoding.GetEncoding(Encoding.ASCII.EncodingName, New EncoderReplacementFallback(String.Empty), New DecoderExceptionFallback()), Encoding.UTF8.GetBytes(inputString)))


        Return sReturn

    End Function
    Public Function Remove_ASCII9(ByRef strString As String) As String
        If Len(strString) = 0 Then
            Remove_ASCII9 = strString
            Exit Function
        End If

        If Asc(Strings.Right(strString, 1)) = 9 Then
            Remove_ASCII9 = Strings.Left(strString, Len(strString) - 1)
        Else
            Remove_ASCII9 = strString
        End If
    End Function

    Public Function Truncate_String(ByVal strInput As String, ByVal intMax_Length As Integer, Optional ByRef booEnd_With_Dots As Boolean = False) As String
        Const strDOTS As String = " ..."
        Dim i As Integer
        Dim intUBound As Integer
        Dim x As Integer
        Dim intAdjMax_Length As Integer
        Dim strString() As String
        Dim strNew_String As String

        strNew_String = String.Empty '>> Set Default 

        On Error GoTo Err_Truncate_String

        Dim booAdjEnd_With_Dots As Boolean 'Added 09.21.2013
        booAdjEnd_With_Dots = False

        '>>> Added 07.07.2013
        '>> Split on Carriage Returns
        If strInput.IndexOf(vbCrLf) Then
            Dim strNewSegment As String()
            strNewSegment = strInput.Split(vbCrLf)

            strNew_String = strNewSegment(0)

            If strNew_String.Length <= intMax_Length Then
                Truncate_String = strNew_String
                Exit Function
            Else
                strInput = strNew_String
                booAdjEnd_With_Dots = True
            End If
        End If



        If booEnd_With_Dots Then
            ' If intMax_Length > Len(strDOTS) Then
            If intMax_Length < strInput.Length Then 'Change 09.21.2013
                intAdjMax_Length = intMax_Length - Len(strDOTS)
            Else
                intAdjMax_Length = intMax_Length
            End If
        Else
            intAdjMax_Length = intMax_Length
        End If
        strString = Split(Trim(strInput), " ")
        intUBound = UBound(strString)
        strNew_String = String.Empty 'Reset Defualt >> Added 09.21.2013
        For i = 0 To intUBound
            If Len(strNew_String & " " & strString(i)) <= intAdjMax_Length Then
                strNew_String = strNew_String & " " & strString(i)
            Else
                Exit For
            End If
        Next
        'If booEnd_With_Dots Then
        If booAdjEnd_With_Dots Then 'Change 09.21.2013
            strNew_String = strNew_String & strDOTS
        End If

        '>> Cleanup Title
        strNew_String = ReplaceChars(strNew_String, "'", " ") '>> Remove single quotes
        strNew_String = ReplaceChars(strNew_String, Chr(34), " ") '>> Remove double quotes
        Call Valid_ASCII(strNew_String)
        strNew_String = Trim(strNew_String)

        '>> Testing Code
        'If strNew_String.Length > 50 Then
        '    Beep()
        'End If
        '<<

        Truncate_String = strNew_String
        Exit Function

Err_Truncate_String:
        MsgBox(Err.Description & vbCrLf & vbCrLf & "An error has occurred while truncating captions that are too long.", MsgBoxStyle.Exclamation, "Error No.: " & Err.Number)
        Exit Function

    End Function

    Function Valid_ASCII(ByVal strString As String) As String
        Dim x As Integer

        For x = 1 To Len(strString)


            If Asc(Mid(strString, x, 1)) > 126 Or Asc(Mid(strString, x, 1)) < 32 Then
                strString = ReplaceChars(strString, Mid(strString, x, 1), " ")
                Call Valid_ASCII(strString)
            End If
        Next x

        Return strString
    End Function

    Public Function ReplaceChars(ByVal Text As String, ByVal Char_Renamed As String, ByRef ReplaceChar As String) As String
        Dim counter As Integer

        counter = 1
        Do
            counter = InStr(counter, Text, Char_Renamed)
            If counter <> 0 Then
                Mid(Text, counter, Len(ReplaceChar)) = ReplaceChar
            Else
                ReplaceChars = Text
                Exit Do
            End If
        Loop

        ReplaceChars = Text
    End Function

    'Public Function CleanString(ByVal strIn As String) As String
    '    ' Replace invalid characters with empty strings.
    '    Return Regex.Replace(strIn, "[^\w\.@-]", "")
    'End Function

    Private Function Remove_Prefix_Tabs(ByRef sLine As String) As String
        Dim nLen As Integer
        Dim i As Integer
        Dim sTemp As String

        nLen = Len(sLine)
        If nLen = 0 Then
            Remove_Prefix_Tabs = sLine
            Exit Function
        End If

        sTemp = ""
        For i = 1 To nLen
            If Asc(Mid(sLine, i, 1)) <> 9 Then '>> Not a Tab
                sTemp = sTemp & Mid(sLine, i, 1)
            Else
                sTemp = sTemp & " "
            End If
        Next

        Remove_Prefix_Tabs = sTemp

    End Function
    Private Function Get_Parameters_ArrayList(ByVal sListFile As String) As ArrayList
        '     			mtxtReadWriteMgr = new Atebion.TxtReaderWriter.Manager();
        'ArrayList al = mtxtReadWriteMgr.ReadFile(sListFile);

        '// Clear and Load Data
        'listBox1.Items.Clear();
        'foreach (Atebion.TxtReaderWriter.Manager.TextData txtData in al)
        '{
        '	listBox1.Items.Add(txtData.Value);

        '}

        If readerParameters Is Nothing Then
            readerParameters = New Atebion.TxtReaderWriter.Manager
        End If

        Dim al As New ArrayList
        Dim StrArry As String()

        If File.Exists(sListFile) = False Then 'Added 02.02.2013
            Throw New ApplicationException("Unable to find Atebion Parse Paremeter file: " + sListFile)
        End If

        If mbooUseEncryptedParameters_File = True Then 'Added 02.01.2013

            Dim encryptedTxt As String = File.ReadAllText(sListFile)

            Cryptographer.Key = "Today=330"

            Dim dencryptedTxt As String = Cryptographer.Decrypt(encryptedTxt)

            StrArry = dencryptedTxt.Split("|")

            al.AddRange(StrArry)

        Else
            al = readerParameters.ReadFile(sListFile)

        End If

        Get_Parameters_ArrayList = al

    End Function

#End Region

#Region "Other Helper Functions"
    Public Function SaveParseResultsList(ByVal sFile As String) As Boolean
        Try
            Dim fs As FileStream = New FileStream(sFile, FileMode.OpenOrCreate, FileAccess.Write)
            Dim m_streamWriter As StreamWriter = New StreamWriter(fs)
            m_streamWriter.Flush()
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin)
            Dim i As Integer = 0
            Dim z As Integer = listParseResults.Count - 1
            For i = 0 To z
                m_streamWriter.Write(listParseResults(i).Parameter)
                m_streamWriter.Write("|" + listParseResults(i).Index)
                m_streamWriter.Write("|" + listParseResults(i).LineStart)
                m_streamWriter.Write("|" + listParseResults(i).LineEnd)
                m_streamWriter.Write("|" + listParseResults(i).ColumnStart)
                m_streamWriter.Write("|" + listParseResults(i).ColumnEnd)
                m_streamWriter.Write("|" + listParseResults(i).Found)
                m_streamWriter.Write("|" + listParseResults(i).Caption)
                m_streamWriter.Write("|" + listParseResults(i).FileName)
                m_streamWriter.WriteLine()
            Next

            m_streamWriter.Flush()
            m_streamWriter.Close()
            Return True
        Catch em As Exception
            System.Console.WriteLine(em.Message.ToString)
            Return False
        End Try
    End Function

    Public Function SavelistParameterLevels(ByVal sFile As String) As Boolean
        Try

            Dim fs As FileStream = New FileStream(sFile, FileMode.OpenOrCreate, FileAccess.Write)
            Dim m_streamWriter As StreamWriter = New StreamWriter(fs)
            m_streamWriter.Flush()
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin)

            For Each parm As ParameterLevels In listParameterLevels
                m_streamWriter.Write(parm.Parameter)
                m_streamWriter.Write("|" + parm.Parent)
                m_streamWriter.WriteLine()
            Next

            m_streamWriter.Flush()
            m_streamWriter.Close()
            Return True
        Catch em As Exception
            System.Console.WriteLine(em.Message.ToString)
            Return False
        End Try

    End Function



#End Region

#Region "Control Helper Functions"

    Public Function SaveLvFile(ByVal sFile As String, ByVal lv As ListView) As Boolean
        Try
            Dim fs As FileStream = New FileStream(sFile, FileMode.OpenOrCreate, FileAccess.Write)
            Dim m_streamWriter As StreamWriter = New StreamWriter(fs)
            m_streamWriter.Flush()
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.Begin)
            Dim i As Integer = 0
            While i < lv.Items.Count
                Dim j As Integer = 0
                While j < lv.Items(i).SubItems.Count
                    If j = 0 Then
                        m_streamWriter.Write(lv.Items(i).SubItems(j).Text)
                    Else
                        m_streamWriter.Write("|" + lv.Items(i).SubItems(j).Text)
                    End If
                    System.Math.Min(System.Threading.Interlocked.Increment(j), j - 1)
                End While
                m_streamWriter.WriteLine()
                System.Math.Min(System.Threading.Interlocked.Increment(i), i - 1)
            End While
            m_streamWriter.Flush()
            m_streamWriter.Close()
            Return True
        Catch em As Exception
            System.Console.WriteLine(em.Message.ToString)
            Return False
        End Try
    End Function
    Public Function LoadKeywordsFile(ByVal sFile As String) As Integer
        Dim objKeywords As Keywords
        Dim count As Integer = 0
        Dim booDone As Boolean


        If File.Exists(sFile) = False Then
            '>> ToDo - added log message
            Return 0
        End If

        ' Loads the XML file
        '  Dim source As New XmlConfigSource("MyApp.xml")

        ' source.Configs("keyworks")

        Try
            Dim att As Atebion.Attributes.Attributes = New Attributes(sFile)

            listKeywords.Clear()

            booDone = att.Get_First_Record()
            Do Until booDone = False
                objKeywords = New Keywords()
                objKeywords.Category = att.Field_Name
                objKeywords.Keyword = att.Field_Value
                listKeywords.Add(objKeywords)
                count = count + 1

                att.Get_Next_Record()
            Loop
            att = Nothing
            Return count
        Catch
            'ToDo - Add log
            Return count

        End Try


    End Function




    Public Function LoadLvFile(ByVal sFile As String, ByVal lv As ListView) As Boolean
        Try
            Dim fs As FileStream = New FileStream(sFile, FileMode.Open, FileAccess.Read)
            Dim m_streamReader As StreamReader = New StreamReader(fs)
            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin)
            Dim strLine As String = m_streamReader.ReadLine
            While Not (strLine Is Nothing)
                Dim sParseLine As String() = strLine.Split("|")
                Dim lvi As ListViewItem = New ListViewItem(sParseLine)
                lv.Items.Add(lvi)
                strLine = m_streamReader.ReadLine
            End While
            m_streamReader.Close()
            Return True
        Catch em As Exception
            System.Console.WriteLine(em.Message.ToString)
            Return False
        End Try
    End Function

    Private Function lstFind(ByRef lst As ListBox, ByRef sValue As String, ByRef ExactMatch As Boolean, ByRef bSelectFound As Boolean) As Integer
        Dim nRow As Integer
        Dim sValuePartial As String
        Dim i As Integer
        Dim nCount As Integer

        sValuePartial = sValue + "*"
        nRow = 0

        nCount = lst.Items.Count
        If nCount = 0 Then
            Return -1 '>> Not found - No Items Exists
        End If

        For i = 0 To lst.Items.Count
            If ExactMatch Then
                If sValue = lst.Items(i).text Then
                    If bSelectFound Then
                        lst.Focus()
                        lst.SelectedIndex = i
                    End If
                End If
            Else
                If sValuePartial Like lst.Items(i).text Then
                    If bSelectFound Then
                        lst.Focus()
                        lst.SelectedIndex = i
                    End If
                End If
            End If
        Next

        Return -1 '>> Not Found any match
    End Function
    Public Sub lvFindFirstItemOrSubItem(ByVal lvwView As ListView, ByVal sSearchText As String, _
     Optional ByVal bMatchCase As Boolean = False)
        '==============================================================================
        ' Purpose      :  Find first Item or SubItem in a ListView
        ' Inputs       :  ListView, sSearchText
        ' Returns      :  Finds and highlights first Item or SubItem found
        '==============================================================================

        On Error GoTo Error_ErrHandler

        Dim x As Integer = 0
        Dim Columns As Byte = lvwView.Columns.Count - 1
        Dim Column As Byte = 0
        Dim Item As ListViewItem

        If Len(sSearchText) = 0 Then Exit Sub

        With lvwView
            .BeginUpdate()
            .FullRowSelect = True
            .MultiSelect = False

            For Each Item In .SelectedItems
                Item.Selected = False
            Next

            For x = 0 To .Items.Count - 1
                If bMatchCase = True Then ' Search Item using MatchCase
                    If .Items(x).Text Like sSearchText _
                     And Len(.Items(x).Text) = Len(sSearchText) Then
                        .Items(x).Selected = True
                        .Items(x).EnsureVisible()
                        GoTo FoundIt
                    Else
                        For Column = 0 To Columns ' Search SubItems using MatchCase
                            If .Items(x).SubItems(Column).Text Like sSearchText _
                             And Len(.Items(x).SubItems(Column).Text) = Len(sSearchText) Then
                                .Items(x).Selected = True
                                .Items(x).EnsureVisible()
                                GoTo FoundIt
                            End If
                        Next
                    End If

                ElseIf bMatchCase = False Then ' Search without MatchCase

                    If .Items(x).Text.ToLower Like sSearchText.ToLower _
                     And Len(.Items(x).Text) = Len(sSearchText) Then ' Search Item
                        .Items(x).Selected = True
                        .Items(x).EnsureVisible()
                        GoTo FoundIt
                    Else
                        For Column = 1 To Columns ' Search SubItems
                            If .Items(x).SubItems(Column).Text.ToLower Like sSearchText.ToLower _
                             And Len(.Items(x).SubItems(Column).Text) = Len(sSearchText) Then
                                .Items(x).Selected = True
                                .Items(x).EnsureVisible()
                                GoTo FoundIt
                            End If
                        Next
                    End If
                End If
            Next
FoundIt:
            .Focus()
            .EndUpdate()
        End With

        Exit Sub
Error_ErrHandler:

    End Sub
    Public Function lvFindNextItemOrSubItem(ByVal lvwView As ListView, ByVal sSearchText As String, _
 ByVal Index As Integer, Optional ByVal bMatchCase As Boolean = False) As Boolean
        '==============================================================================
        ' Purpose      :  Find next Item or SubItem in ListView
        ' Inputs       :  ListView, sSearchText
        ' Returns      :  HighLights next Item or SubItem in ListView
        '==============================================================================

        On Error GoTo Error_ErrHandler

        Dim x As Integer = 0
        Dim Columns As Byte = lvwView.Columns.Count - 1
        Dim Column As Byte = 0
        Dim Item As ListViewItem

        If Len(sSearchText) = 0 Then Exit Function

        With lvwView
            .BeginUpdate()
            .FullRowSelect = True
            .MultiSelect = False

            For Each Item In .SelectedItems
                Item.Selected = False
            Next

            For x = Index To .Items.Count - 1
                If bMatchCase = True Then ' Search Item using MatchCase
                    If .Items(x).Text Like sSearchText _
                     And Len(.Items(x).Text) = Len(sSearchText) Then
                        .Items(x).Selected = True
                        .Items(x).EnsureVisible()
                        GoTo FoundIt
                    Else
                        For Column = 0 To Columns ' Search SubItems using MatchCase
                            If .Items(x).SubItems(Column).Text Like sSearchText _
                             And Len(.Items(x).SubItems(Column).Text) = Len(sSearchText) Then
                                .Items(x).Selected = True
                                .Items(x).EnsureVisible()
                                GoTo FoundIt
                            End If
                        Next
                    End If

                ElseIf bMatchCase = False Then ' Search without MatchCase

                    If .Items(x).Text.ToLower Like sSearchText.ToLower _
                     And Len(.Items(x).Text) = Len(sSearchText) Then ' Search Item
                        .Items(x).Selected = True
                        .Items(x).EnsureVisible()
                        GoTo FoundIt
                    Else
                        For Column = 1 To Columns ' Search SubItems
                            If .Items(x).SubItems(Column).Text.ToLower Like sSearchText.ToLower _
                             And Len(.Items(x).SubItems(Column).Text) = Len(sSearchText) Then
                                .Items(x).Selected = True
                                .Items(x).EnsureVisible()
                                GoTo FoundIt
                            End If
                        Next
                    End If
                End If
            Next
FoundIt:
            .Focus()
            .EndUpdate()
            Return True
        End With

        Exit Function
Error_ErrHandler:
        Return False

    End Function
    Private Function FindParameterLevel(ByRef sValue As String) As Integer '>> New List/Class code - 12/31/2009
        Dim nRow As Integer = 0

        For Each parm As ParameterLevels In listParameterLevels
            If parm.Parameter = sValue Then
                Return nRow
            End If

            nRow = nRow + 1
        Next

        Return -1 '>> Not Found
    End Function


    Private Function lvFind(ByRef lv As ListView, ByRef sValue As String, ByRef ExactMatch As Boolean, ByRef nCol As Integer, ByRef bSelectFound As Boolean) As Integer
        Dim nRow As Integer
        Dim sValuePartial As String

        sValuePartial = sValue + "*"

        nRow = 0
        For Each item As ListViewItem In lv.Items
            If ExactMatch Then
                If nCol = 0 Then
                    If item.Text = sValue Then
                        If bSelectFound Then
                            lv.Focus()
                            lv.Items(nRow).Selected = True
                        End If
                        Return nRow
                    End If
                Else
                    If item.SubItems(nCol).Text = sValue Then
                        If bSelectFound Then
                            lv.Focus()
                            lv.Items(nRow).Selected = True
                        End If
                        Return nRow
                    End If
                End If
            Else
                If nCol = 0 Then
                    If item.Text Like sValuePartial Then
                        If bSelectFound Then
                            lv.Focus()
                            lv.Items(nRow).Selected = True
                        End If
                        Return nRow
                    End If
                Else
                    If item.SubItems(nCol).Text Like sValuePartial Then
                        If bSelectFound Then
                            lv.Focus()
                            lv.Items(nRow).Selected = True
                        End If
                        Return nRow
                    End If

                End If

            End If
            nRow = nRow + 1
        Next

        Return -1 '>> Not Found

    End Function

#End Region


End Class
