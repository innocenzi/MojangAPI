Imports System.Drawing

Public Class PLAYER_PROFILE_BY_UUID

    Public Structure Result
        Dim UUID As String
        Dim Name As String
        Dim Properties As PlayerProperties


        Dim [Error] As String
        Dim ErrorMessage As String
    End Structure
    Public Structure PlayerProperties
        Dim Name As String ' Property name
        Dim Value As String ' Base64
        Dim SkinURL As String
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

            RequestResults.UUID = Methods.GetValue(RequestString, "id")
            RequestResults.Name = Methods.GetValue(RequestString, "name")

            Dim Properties As String = RequestString.Split("[")(1)
            Dim Base64Response As String = Methods.GetValue(Properties, "value")
            RequestResults.Properties.Name = Methods.GetValue(Properties, "name")
            RequestResults.Properties.Value = Methods.DecodeBase64String(Base64Response)
            RequestResults.Properties.SkinURL = Methods.GetValue(RequestResults.Properties.Value, "url")


            RequestResults.Error = Methods.GetValue(RequestString, "error")
            RequestResults.ErrorMessage = Methods.GetValue(RequestString, "errorMessage")

            Return RequestResults
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return Nothing
        End Try
    End Function

End Class
