Public Class AUTHENTICATE

    Public Structure Result
        Dim AccessToken As String
        Dim ClientToken As String
        Dim PlayerName As String

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
            RequestResults.AccessToken = Methods.GetValue(RequestString, "accessToken")
            RequestResults.ClientToken = Methods.GetValue(RequestString, "clientToken")
            RequestResults.PlayerName = Methods.GetValue(RequestString, "name")

            RequestResults.Error = Methods.GetValue(RequestString, "error")
            RequestResults.ErrorMessage = Methods.GetValue(RequestString, "errorMessage")

            Return RequestResults
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return Nothing
        End Try
    End Function

End Class
