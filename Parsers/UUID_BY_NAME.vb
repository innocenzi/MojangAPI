Public Class UUID_BY_NAME

    Public Structure Result
        Dim ID As String
        Dim Name As String

        Dim [Error] As String
        Dim ErrorMessage As String
    End Structure

    ' ---

    Public Property RequestString As String
    Public Property RequestResults As Result

    Public Sub New(ByVal RequestString As String)
        Me.RequestString = RequestString
        Me.RequestResults = GetResultsByString(RequestString)
    End Sub

    Private Function GetResultsByString(ByVal RequestString As String) As Result
        Try
            Dim RequestResults As New Result
            RequestResults.ID = Methods.GetValue(RequestString, "id")
            RequestResults.Name = Methods.GetValue(RequestString, "name")

            RequestResults.Error = Methods.GetValue(RequestString, "error")
            RequestResults.ErrorMessage = Methods.GetValue(RequestString, "errorMessage")

            Return RequestResults
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return Nothing
        End Try
    End Function


End Class
