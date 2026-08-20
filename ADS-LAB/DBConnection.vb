Imports MySql.Data.MySqlClient

Public Class DBConnection
    Private Shared ReadOnly connStr As String =
        "server=localhost;user=root;password=;database=DB_Dorado;"

    Public Shared Function GetConnection() As MySqlConnection
        Return New MySqlConnection(connStr)
    End Function
End Class