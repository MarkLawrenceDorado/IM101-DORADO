Imports System.Text.RegularExpressions
Imports MySql.Data.MySqlClient

Public Module Validators

    ' Returns True when the value is valid. Shows or clears the error icon on the control.
    Public Function Check(provider As ErrorProvider, control As Control, isInvalid As Boolean, message As String) As Boolean
        If isInvalid Then
            provider.SetError(control, message)
            Return False
        End If

        provider.SetError(control, "")
        Return True
    End Function

    Public Function IsValidName(text As String) As Boolean
        Return Regex.IsMatch(text, "^\p{L}[\p{L} .'-]{0,49}$")
    End Function

    Public Function IsValidUsername(text As String) As Boolean
        Return Regex.IsMatch(text, "^[A-Za-z0-9._]{3,30}$")
    End Function

    Public Function IsValidEmail(text As String) As Boolean
        Return text.Length <= 100 AndAlso Regex.IsMatch(text, "^[^@\s]+@[^@\s]+\.[^@\s]+$")
    End Function

    ' 2-5 letters followed by 3 digits, e.g. CS101 or ITEC201
    Public Function IsValidCourseCode(text As String) As Boolean
        Return Regex.IsMatch(text, "^[A-Z]{2,5}[0-9]{3}$")
    End Function

    ' Returns an empty string when the password is strong enough
    Public Function PasswordProblem(password As String) As String
        If password.Length < 8 Then Return "Use at least 8 characters."
        If Not Regex.IsMatch(password, "[A-Z]") Then Return "Include an uppercase letter."
        If Not Regex.IsMatch(password, "[a-z]") Then Return "Include a lowercase letter."
        If Not Regex.IsMatch(password, "[0-9]") Then Return "Include a number."
        Return ""
    End Function

    ' True when the query returns at least one row
    Public Function ValueExists(sql As String, ParamArray params() As MySqlParameter) As Boolean
        Dim conn As MySqlConnection = DBConnection.GetConnection()
        Try
            conn.Open()
            Using cmd As New MySqlCommand(sql, conn)
                cmd.Parameters.AddRange(params)
                Return cmd.ExecuteScalar() IsNot Nothing
            End Using
        Finally
            conn.Close()
        End Try
    End Function

End Module