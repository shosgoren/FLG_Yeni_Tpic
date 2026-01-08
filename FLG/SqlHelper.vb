Imports System.Configuration
Imports System.Data.SqlClient

Public Class SqlHelper
    Private ReadOnly _connectionString As String
    Public Sub New()
        _connectionString = String.Format("Server=" & FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("ServerName"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9") & "; database=" & FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("LogoDB"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9") & "; uid=" & FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("SqlUserName"), "BKumqFS<Xss|hITUb3fN9HaTI#y6n9") & "; pwd=" & FrmSettings.AES_Decrypt(ConfigurationManager.AppSettings.Get("SqlPassword"), "jjxQThC[^N4-nmF,va_+#>Ee%q_-?}") & "; pooling=true; connection timeout=30;Max Pool Size=200")
    End Sub
    Public Function GetSqlConnection() As SqlConnection
        Dim connection As New SqlConnection(_connectionString)
        If connection.State = ConnectionState.Open Then

            connection.Close()
            connection.Open()

        Else

            connection.Open()

        End If


        Return connection
    End Function
    Public Function GetSqlCommand() As SqlCommand

        Dim command As New SqlCommand()

        command.Connection = GetSqlConnection()
        command.CommandTimeout = 300

        Return command
    End Function

End Class
