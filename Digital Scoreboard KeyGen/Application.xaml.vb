Imports System.Security.Cryptography
Imports System.Text
Imports System.Management

Class Application
    Public Shared Function GenerateLicenseKey(mSerialKey As String, enckey As String) As String
        Dim TVKey As String = Application.CreateDeterministicGUID(Application.EncryptData(enckey + mSerialKey.Trim.ToUpper + enckey)).ToString.ToUpper
        TVKey = TVKey.Replace("-", "")
        Dim partA As String = TVKey.Substring(0, 12)
        Dim partB As String = TVKey.Substring(12, 8)
        Dim partC As String = TVKey.Substring(20, 12)
        Return partA + "-" + partB + "-" + partC
    End Function

    Public Shared Function GenerateSerialKey() As String
        Dim tmp As String = CreateDeterministicGUID(EncryptData(GetProcessorID() + GetUUID() + GetHDDSerialNumber())).ToString.ToUpper.Replace("-", "")
        Dim partA As String = tmp.Substring(0, 8)
        Dim partB As String = tmp.Substring(8, 8)
        Dim partC As String = tmp.Substring(16, 8)
        Dim partD As String = tmp.Substring(24, 8)
        Return partA + "-" + partC + "-" + partB + "-" + partD   ' B and C are swaped
    End Function

    Public Shared Function CreateDeterministicGUID(mdata As String) As Guid
        Dim provider As MD5CryptoServiceProvider = New MD5CryptoServiceProvider
        Dim inputbytes As Byte() = Encoding.Default.GetBytes(mdata)
        Dim hashbytes As Byte() = provider.ComputeHash(inputbytes)
        Dim hasguid As Guid = New Guid(hashbytes)
        Return hasguid
    End Function

    Public Shared Function GetProcessorID() As String
        Dim mbs As Object = New ManagementObjectSearcher("Select ProcessorId From Win32_processor")
        Dim mbsList As ManagementObjectCollection = mbs.Get
        Dim id As String = ""
        For Each mo As ManagementObject In mbsList
            If mo.Properties("ProcessorId").Value Is Nothing Then
                id = ""
                Exit For
            Else
                id = mo.Properties("ProcessorId").Value.ToString
                Exit For
            End If
        Next
        Return id
    End Function
    Public Shared Function GetUUID() As String
        Dim mbs As Object = New ManagementObjectSearcher("Select UUID From Win32_ComputerSystemProduct")
        Dim mbsList As ManagementObjectCollection = mbs.Get
        Dim id As String = ""
        For Each mo As ManagementObject In mbsList
            If mo.Properties("UUID").Value Is Nothing Then
                id = ""
                Exit For
            Else
                id = mo.Properties("UUID").Value.ToString
                Exit For
            End If
        Next
        Return id
    End Function
    Public Shared Function GetHDDSerialNumber() As String
        Dim mbs As Object = New ManagementObjectSearcher("Select SerialNumber From Win32_PhysicalMedia")
        Dim mbsList As ManagementObjectCollection = mbs.Get
        Dim id As String = ""
        For Each mo As ManagementObject In mbsList
            If mo.Properties("SerialNumber").Value Is Nothing Then
                id = ""
                Exit For
            Else
                id = mo.Properties("SerialNumber").Value.ToString
                Exit For
            End If
        Next
        Return id
    End Function
    Public Shared Function EncryptData(strtoEncrypt As String) As String
        Dim objDESCrypto As New TripleDESCryptoServiceProvider()
        Dim objHashMD5 As New MD5CryptoServiceProvider()
        Dim byteHash As Byte()
        Dim byteBuff As Byte()
        byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(PASSPHRASE))
        objHashMD5 = Nothing
        objDESCrypto.Key = byteHash
        objDESCrypto.Mode = CipherMode.ECB
        byteBuff = ASCIIEncoding.ASCII.GetBytes(strtoEncrypt)

        Return Convert.ToBase64String(objDESCrypto.CreateEncryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length))
    End Function
    Public Const PASSPHRASE As String = "SI_Scoreboard"
    Public Shared Function DecryptData(strEncrypted As String) As String
        Dim objDESCrypto As New TripleDESCryptoServiceProvider()
        Dim objHashMD5 As New MD5CryptoServiceProvider()
        Dim byteHash As Byte()
        Dim byteBuff As Byte()
        byteHash = objHashMD5.ComputeHash(ASCIIEncoding.ASCII.GetBytes(PASSPHRASE))
        objHashMD5 = Nothing
        objDESCrypto.Key = byteHash
        objDESCrypto.Mode = CipherMode.ECB
        byteBuff = Convert.FromBase64String(strEncrypted)
        Dim strDecrypted As String = ""
        Try
            strDecrypted = ASCIIEncoding.ASCII.GetString((objDESCrypto.CreateDecryptor().TransformFinalBlock(byteBuff, 0, byteBuff.Length)))
        Catch ex As Exception
            Dim i As Integer = 0
        End Try

        objDESCrypto = Nothing
        Return strDecrypted
    End Function

End Class
