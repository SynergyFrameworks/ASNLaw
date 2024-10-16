Imports System
Imports System.IO

Public Class Email

   

    Public Function SendEmail(ByVal SendTo As String, ByVal Subject As String, ByVal Body As String) As Boolean


        Dim oApp As Object
        Dim oMsg As Object
        'Dim oAttachs As Object
        'Dim oAttach As Object

        Dim returnResult As Boolean

        returnResult = True

        Try

            oApp = CreateObject("Outlook.Application")
            oMsg = oApp.CreateItem(0)
            oMsg.To = SendTo
            oMsg.Subject = Subject
            oMsg.Body = Body

            'oAttachs = oMsg.Attachments

            'For i As Integer = 0 To UBound(sAttachments)
            '    oAttach = oAttachs.Add(sAttachments(i), , oMsg.Body.Length + 1, sAttachments(i))
            'Next i

            oMsg.Send()
        Catch

            returnResult = False

        End Try

        Return returnResult

    End Function

    Public Function SendEmailWithAttachments(ByVal SendTo As String, ByVal Subject As String, ByVal Body As String, ByVal sAttachments As String()) As Boolean


        Dim oApp As Object
        Dim oMsg As Object
        Dim oAttachs As Object
        Dim oAttach As Object

        Dim returnResult As Boolean

        returnResult = True

        Try

            oApp = CreateObject("Outlook.Application")
            oMsg = oApp.CreateItem(0)
            oMsg.To = SendTo
            oMsg.Subject = Subject
            oMsg.Body = Body

            oAttachs = oMsg.Attachments

            For i As Integer = 0 To UBound(sAttachments)
                oAttach = oAttachs.Add(sAttachments(i), , oMsg.Body.Length + 1, sAttachments(i))
            Next i

            oMsg.Send()
        Catch

            returnResult = False

        End Try

        Return returnResult

    End Function

    Public Function IsOutlookConnectable() As Boolean

        Try
            Dim oApp As Object
            oApp = CreateObject("Outlook.Application")

            If Not IsNothing(oApp) Then
                oApp = Nothing
                Return True
            Else
                Return False
            End If
        Catch

            Return False
        End Try

    End Function


    Public Function OpenEmailWithAttachments(ByVal SendTo As String, ByVal Subject As String, ByVal Body As String, ByVal sAttachments As String()) As Boolean

        Dim oApp As Object
        Dim oMsg As Object
        Dim oAttachs As Object
        Dim oAttach As Object

        Dim returnResult As Boolean

        returnResult = True

        Try

            oApp = CreateObject("Outlook.Application")
            oMsg = oApp.CreateItem(0)
            oMsg.To = SendTo
            oMsg.Subject = Subject
            oMsg.Body = Body

            oAttachs = oMsg.Attachments

            For i As Integer = 0 To UBound(sAttachments)
                oAttach = oAttachs.Add(sAttachments(i), , oMsg.Body.Length + 1, sAttachments(i))
            Next i

            oMsg.Display()

            If Not IsNothing(oMsg) Then
                oMsg = Nothing
            End If

            If Not IsNothing(oApp) Then
                oApp = Nothing
            End If
        Catch

            returnResult = False

        End Try

  

        Return returnResult

    End Function
End Class
