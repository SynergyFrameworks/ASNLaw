Imports System
Imports System.Data
Imports System.Configuration
Imports System.Web
Imports System.Security.Cryptography
Imports System.Text
'' Added 02.01.2013
Public Class Cryptographer
    Private Shared _key As String

    Public Sub New()
    End Sub


    Public Shared WriteOnly Property Key() As String
        Set(ByVal value As String)
            _key = value
        End Set
    End Property

    ''' <summary>
    ''' Encrypt the given string using the default key.
    ''' </summary>
    ''' <param name="strToEncrypt">The string to be encrypted.</param>
    ''' <returns>The encrypted string.</returns>
    Public Shared Function Encrypt(ByVal strToEncrypt As String) As String
        Try
            Return Encrypt(strToEncrypt, _key)
        Catch ex As Exception
            Return "Wrong Input. " + ex.Message
        End Try

    End Function

    ''' <summary>
    ''' Decrypt the given string using the default key.
    ''' </summary>
    ''' <param name="strEncrypted">The string to be decrypted.</param>
    ''' <returns>The decrypted string.</returns>
    Public Shared Function Decrypt(ByVal strEncrypted As String) As String
        Try
            Return Decrypt(strEncrypted, _key)
        Catch ex As Exception
            Return "Wrong Input. " + ex.Message
        End Try
    End Function

    ''' <summary>
    ''' Encrypt the given string using the specified key.
    ''' </summary>
    ''' <param name="strToEncrypt">The string to be encrypted.</param>
    ''' <param name="strKey">The encryption key.</param>
    ''' <returns>The encrypted string.</returns>
    Public Shared Function Encrypt(ByVal strToEncrypt As String, ByVal strKey As String) As String
        Try
            Dim objDESCrypto As New TripleDESCryptoServiceProvider()
            Dim objHashMD5 As New MD5CryptoServiceProvider()

            Dim byteHash As Byte(), byteBuff As Byte()
            Dim strTempKey As String = strKey

            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey))
            objHashMD5 = Nothing
            objDESCrypto.Key = byteHash
            objDESCrypto.Mode = CipherMode.ECB
            'CBC, CFB
            byteBuff = ASCIIEncoding.ASCII.GetBytes(strToEncrypt)
            Return Convert.ToBase64String(objDESCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length))
        Catch ex As Exception
            Return "Wrong Input. " + ex.Message
        End Try
    End Function


    ''' <summary>
    ''' Decrypt the given string using the specified key.
    ''' </summary>
    ''' <param name="strEncrypted">The string to be decrypted.</param>
    ''' <param name="strKey">The decryption key.</param>
    ''' <returns>The decrypted string.</returns>
    Public Shared Function Decrypt(ByVal strEncrypted As String, ByVal strKey As String) As String
        Try
            Dim objDESCrypto As New TripleDESCryptoServiceProvider()
            Dim objHashMD5 As New MD5CryptoServiceProvider()

            Dim byteHash As Byte(), byteBuff As Byte()
            Dim strTempKey As String = strKey

            byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(strTempKey))
            objHashMD5 = Nothing
            objDESCrypto.Key = byteHash
            objDESCrypto.Mode = CipherMode.ECB
            'CBC, CFB
            byteBuff = Convert.FromBase64String(strEncrypted)
            Dim strDecrypted As String = ASCIIEncoding.ASCII.GetString(objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length))
            objDESCrypto = Nothing

            Return strDecrypted
        Catch ex As Exception
            Return "Wrong Input. " + ex.Message
        End Try
    End Function

End Class
