Imports System.Configuration

Public Class FrmSettings
    Sub New()

        ' Bu çağrı tasarımcı için gerekli.
        InitializeComponent()
        txtPassword.UseSystemPasswordChar = True

        ' InitializeComponent() çağrısından sonra başlangıç değer ekleyin.

    End Sub

#Region "Config Password Status"


    Private AES As New System.Security.Cryptography.RijndaelManaged
    Private Hash_AES As New System.Security.Cryptography.MD5CryptoServiceProvider
    Private encrypted As String = ""
    Private decrypted As String = ""


    Public Function AES_Encrypt(ByVal input As String, ByVal pass As String) As String

        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESEncrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateEncryptor
            Dim Buffer As Byte() = System.Text.ASCIIEncoding.ASCII.GetBytes(input)
            encrypted = Convert.ToBase64String(DESEncrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return encrypted
        Catch ex As Exception
        End Try

    End Function

    Public Function AES_Decrypt(ByVal input As String, ByVal pass As String) As String

        Try
            Dim hash(31) As Byte
            Dim temp As Byte() = Hash_AES.ComputeHash(System.Text.ASCIIEncoding.ASCII.GetBytes(pass))
            Array.Copy(temp, 0, hash, 0, 16)
            Array.Copy(temp, 0, hash, 15, 16)
            AES.Key = hash
            AES.Mode = Security.Cryptography.CipherMode.ECB
            Dim DESDecrypter As System.Security.Cryptography.ICryptoTransform = AES.CreateDecryptor
            Dim Buffer As Byte() = Convert.FromBase64String(input)
            decrypted = System.Text.ASCIIEncoding.ASCII.GetString(DESDecrypter.TransformFinalBlock(Buffer, 0, Buffer.Length))
            Return decrypted
        Catch ex As Exception
            'MsgBox(ex.ToString(), MsgBoxStyle.OkOnly, "Uyarı")
        End Try

    End Function

#End Region


    Private Sub FrmSettings_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        txtFaysDbName.Text = AES_Decrypt(ConfigurationManager.AppSettings.Get("FaysDB"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        txtServerName.Text = AES_Decrypt(ConfigurationManager.AppSettings.Get("ServerName"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        txtTigerDbName.Text = AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoDB"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        txtTigerMasterDbName.Text = AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoMasterDb"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        txtUserName.Text = AES_Decrypt(ConfigurationManager.AppSettings.Get("SqlUserName"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        txtPassword.Text = AES_Decrypt(ConfigurationManager.AppSettings.Get("SqlPassword"), "jjxQThC[^N4-nmF,va_+#>Ee%q_-?}")
        txtFirmaNo.Text = AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoFirmaNo"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        cbName.Checked = If(ConfigurationManager.AppSettings.Get("NAME").ToString().Equals("1"), True, False)
        cbName3.Checked = If(ConfigurationManager.AppSettings.Get("NAME3").ToString().Equals("1"), True, False)
        cbName4.Checked = If(ConfigurationManager.AppSettings.Get("NAME4").ToString().Equals("1"), True, False)
        cbName5.Checked = If(ConfigurationManager.AppSettings.Get("PRODUCERCODE").ToString().Equals("1"), True, False)
        cbName6.Checked = If(ConfigurationManager.AppSettings.Get("STGRPCODE").ToString().Equals("1"), True, False)
        cbName7.Checked = If(ConfigurationManager.AppSettings.Get("SPECODE").ToString().Equals("1"), True, False)
        cbSpecode2.Checked = If(ConfigurationManager.AppSettings.Get("SPECODE2").ToString().Equals("1"), True, False)
        cbSpecode3.Checked = If(ConfigurationManager.AppSettings.Get("SPECODE3").ToString().Equals("1"), True, False)
        cbSpecode4.Checked = If(ConfigurationManager.AppSettings.Get("SPECODE4").ToString().Equals("1"), True, False)
        cbSpecode5.Checked = If(ConfigurationManager.AppSettings.Get("SPECODE5").ToString().Equals("1"), True, False)
        cbName8.Checked = If(ConfigurationManager.AppSettings.Get("FPM").ToString().Equals("1"), True, False)
        cbOtomatikGuncelleme.Checked = If(ConfigurationManager.AppSettings.Get("OtomatikGuncelleme").ToString().Equals("1"), True, False)
        If Me.txtPassword.Text.Length > 0 Then
            chkShowPass.Visible = False
        End If
    End Sub

    Private Sub btnSave_Click(sender As Object, e As EventArgs) Handles btnSave.Click
        Dim config As Configuration = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None)
        config.AppSettings.Settings("FaysDB").Value = AES_Encrypt(txtFaysDbName.Text, "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        config.AppSettings.Settings("ServerName").Value = AES_Encrypt(txtServerName.Text, "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        config.AppSettings.Settings("LogoDB").Value = AES_Encrypt(txtTigerDbName.Text, "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        config.AppSettings.Settings("LogoMasterDb").Value = AES_Encrypt(txtTigerMasterDbName.Text, "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        config.AppSettings.Settings("SqlUserName").Value = AES_Encrypt(txtUserName.Text, "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        config.AppSettings.Settings("SqlPassword").Value = AES_Encrypt(txtPassword.Text, "jjxQThC[^N4-nmF,va_+#>Ee%q_-?}")
        config.AppSettings.Settings("LogoFirmaNo").Value = AES_Encrypt(txtFirmaNo.Text, "BKumqFS<Xss|hITUb3fN9HaTI#y6n9")
        config.AppSettings.Settings("NAME").Value = If(cbName.Checked, "1", "0")
        config.AppSettings.Settings("NAME3").Value = If(cbName3.Checked, "1", "0")
        config.AppSettings.Settings("NAME4").Value = If(cbName4.Checked, "1", "0")
        config.AppSettings.Settings("SPECODE2").Value = If(cbSpecode2.Checked, "1", "0")
        config.AppSettings.Settings("SPECODE3").Value = If(cbSpecode3.Checked, "1", "0")
        config.AppSettings.Settings("SPECODE4").Value = If(cbSpecode4.Checked, "1", "0")
        config.AppSettings.Settings("SPECODE5").Value = If(cbSpecode5.Checked, "1", "0")
        config.AppSettings.Settings("PRODUCERCODE").Value = If(cbName5.Checked, "1", "0")
        config.AppSettings.Settings("STGRPCODE").Value = If(cbName6.Checked, "1", "0")
        config.AppSettings.Settings("SPECODE").Value = If(cbName7.Checked, "1", "0")
        config.AppSettings.Settings("FPM").Value = If(cbName8.Checked, "1", "0")
        config.AppSettings.Settings("OtomatikGuncelleme").Value = If(cbOtomatikGuncelleme.Checked, "1", "0")
        config.Save(ConfigurationSaveMode.Modified)
        ConfigurationManager.RefreshSection("appSettings")
        Me.Close()
    End Sub

    Private Sub chkShowPass_CheckedChanged(sender As Object, e As EventArgs) Handles chkShowPass.CheckedChanged
        If chkShowPass.Checked Then
            txtPassword.UseSystemPasswordChar = False
        Else
            txtPassword.UseSystemPasswordChar = True

        End If
    End Sub

    Private Sub txtPassword_TextChanged(sender As Object, e As EventArgs) Handles txtPassword.TextChanged
        If txtPassword.Text.Length = 0 Then
            chkShowPass.Visible = True
        End If
    End Sub

End Class