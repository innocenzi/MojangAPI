Public Class AuthenticationResponse

    Structure AuthenticationResponse
        ' Specific
        Dim AccessToken As String
        Dim ClientToken As String
        Dim PlayerName As String

        ' Common
        Dim [Error] As String
        Dim ErrorMessage As String
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
    ''' Get the formatted response. See Authentication.AuthenticationResponse.
    ''' </summary>
    Public ReadOnly Property GetResponse As AuthenticationResponse
        Get
            Return Me._Response
        End Get
    End Property
    Private _Response As AuthenticationResponse

    ''' <summary>
    ''' Parses a RawResponse to an AuthenticationResponse.
    ''' </summary>
    Public Sub New(ByVal RawResponse As String)
        Me._RawResponse = RawResponse

        Try
            Me._Response.AccessToken = Methods.GetValue(RawResponse, "accessToken")
            Me._Response.ClientToken = Methods.GetValue(RawResponse, "clientToken")
            Me._Response.PlayerName = Methods.GetValue(RawResponse, "name")

            Me._Response.Error = Methods.GetValue(RawResponse, "error")
            Me._Response.ErrorMessage = Methods.GetValue(RawResponse, "errorMessage")
        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.DarkRed
            Console.WriteLine(ex.ToString)
            Console.ForegroundColor = ConsoleColor.White
        End Try
    End Sub

End Class
