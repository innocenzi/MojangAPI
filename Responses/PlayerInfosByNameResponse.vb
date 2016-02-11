Public Class PlayerInfosByNameResponse

    Structure PlayerInfosByNameResponse
        ' Specific
        Dim PlayerInfos As List(Of PlayerInfo)

        ' Common
        Dim [Error] As String
        Dim ErrorMessage As String
    End Structure
    Structure PlayerInfo
        Dim UUID As String
        Dim PlayerName As String
        Dim Legacy As Boolean
        Dim Demo As Boolean
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
    ''' Get the formatted response. See Authentication.PlayerInfosByNameResponse.
    ''' </summary>
    Public ReadOnly Property GetResponse As PlayerInfosByNameResponse
        Get
            Return Me._Response
        End Get
    End Property
    Private _Response As PlayerInfosByNameResponse

    ''' <summary>
    ''' Parses a RawResponse to a PlayerInfosByNameResponse
    ''' </summary>
    Public Sub New(ByVal RawResponse As String)
        Me._RawResponse = RawResponse

        Try
            Me._Response.PlayerInfos = New List(Of PlayerInfo)

            ' Running through block
            Dim BlockCount As Integer = Methods.GetOccurences("{", RawResponse)
            For i = 0 To BlockCount - 1
                Dim CurrentBlock As String = RawResponse.Split("{")(i + 1)

                ' Parsing block
                Dim ID As String = Methods.GetValue(CurrentBlock, "id")
                Dim Name As String = Methods.GetValue(CurrentBlock, "name")
                Dim Legacy As String = Methods.GetValue(CurrentBlock, "legacy")
                Dim Demo As String = Methods.GetValue(CurrentBlock, "demo")

                ' Adding an entry
                Me._Response.PlayerInfos.Add(
                    New PlayerInfo() With {
                            .UUID = ID,
                            .PlayerName = Name,
                            .Legacy = IIf(Legacy = Nothing, False, True),
                            .Demo = IIf(Demo = Nothing, False, True)
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


