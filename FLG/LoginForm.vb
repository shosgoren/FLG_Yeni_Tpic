Imports System.Configuration
Imports System.Data.SqlClient
Public Class LoginForm

    ' TODO: Insert code to perform custom authentication using the provided username and password 
    ' (See https://go.microsoft.com/fwlink/?LinkId=35339).  
    ' The custom principal can then be attached to the current thread's principal as follows: 
    '     My.User.CurrentPrincipal = CustomPrincipal
    ' where CustomPrincipal is the IPrincipal implementation used to perform authentication. 
    ' Subsequently, My.User will return identity information encapsulated in the CustomPrincipal object
    ' such as the username, display name, etc.

    Private Sub OK_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OK.Click
        Dim frmsettings As New FrmSettings
        If UsernameTextBox.Text = "Admin" And PasswordTextBox.Text = "Anlas.2019" Then
            Me.Hide()
            frmsettings.ShowDialog()
        Else
            MsgBox("Kullanýcý adý veya þifreyi hatalý girdiniz.")
        End If

    End Sub

    Private Sub Cancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Cancel.Click
        Me.Close()
    End Sub
End Class
