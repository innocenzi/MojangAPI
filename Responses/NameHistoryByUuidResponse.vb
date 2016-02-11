Public Class NameHistoryByUuidResponse

    Structure NameHistoryByUuidResponse
        ' Specific
        Dim HistoryEntries As List(Of HistoryEntry)

        ' Common
        Dim [Error] As String
        Dim ErrorMessage As String
    End Structure
    Structure HistoryEntry
        Dim Name As String
        Dim ChangedToAt As String
    End Structure

    ''' <summary>
    ''' Get the original JSON response.
    ''' </summary>
    Public ReadOnly Property RawResponse As String
        Get
            Return Me._RawResponse
        End Get
    End Property
    Private _RawResponse As String

    ''' <summary>
    ''' Get the formatted response. See Authentication.NameHistoryByUuidResponse.
    ''' </summary>
    Public ReadOnly Property GetResponse As NameHistoryByUuidResponse
        Get
            Return Me._Response
        End Get
    End Property
    Private _Response As NameHistoryByUuidResponse

    ''' <summary>
    ''' Parses a RawResponse to a NameHistoryByUuidResponse
    ''' </summary>
    Public Sub New(ByVal RawResponse As String)
        Me._RawResponse = RawResponse

        Try
            Me._Response.HistoryEntries = New List(Of HistoryEntry)

            ' Running through block
            Dim BlockCount As Integer = Methods.GetOccurences("{", RawResponse)
            For i = 0 To BlockCount - 1
                Dim CurrentBlock As String = RawResponse.Split("{")(i + 1)

                ' Parsing block
                Dim Name As String = Methods.GetValue(CurrentBlock, "name")
                Dim ChangedToAt As String = "Never changed."
                If CurrentBlock.Contains("changedToAt") Then
                    ChangedToAt = Methods.GetValue(CurrentBlock, "changedToAt")
                End If

                ' Adding an entry
                Me._Response.HistoryEntries.Add(
                    New HistoryEntry() With {
                            .Name = Name,
                            .ChangedToAt = ChangedToAt
                        })
            Next

            ' The errors
            Me._Response.Error = Methods.GetValue(RawResponse, "error")
            Me._Response.ErrorMessage = Methods.GetValue(RawResponse, "errorMessage")
        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.DarkRed
            Console.WriteLine(ex.ToString)
            Console.ForegroundColor = ConsoleColor.White
        End Try
    End Sub

End Class


