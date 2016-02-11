Public Class PLAYER_INFOS_BY_NAME

    Public Structure Result
        Dim Names As Dictionary(Of String, String)

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
            RequestResults.Names = New Dictionary(Of String, String)

            RequestResults.Error = Methods.GetValue(RequestString, "error")
            RequestResults.ErrorMessage = Methods.GetValue(RequestString, "errorMessage")

            Dim PseudoCount As Integer = Methods.GetOccurences("{", RequestString)
            For i = 0 To PseudoCount - 1
                Dim CurrentPseudo As String = RequestString.Split("{")(i + 1)

                Dim ID = CurrentPseudo.Split({"""id"":"}, StringSplitOptions.None)(1).Split("""")(1)
                Dim Name = CurrentPseudo.Split({"""name"":"}, StringSplitOptions.None)(1).Split("""")(1)

                RequestResults.Names.Add(Name, ID)
            Next

            Return RequestResults
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return Nothing
        End Try
    End Function

End Class
