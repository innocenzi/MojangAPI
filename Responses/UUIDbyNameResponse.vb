Public Class UuiByNameResponse

    Structure UuidByNameResponse
        ' Specific
        Dim ID As String
        Dim Name As String
        Dim Legacy As String

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
    ''' Get the formatted response. See Authentication.UuidByNameResponse
    ''' </summary>
    Public ReadOnly Property GetResponse As UuidByNameResponse
        Get
            Return Me._Response
        End Get
    End Property
    Private _Response As UuidByNameResponse

    ''' <summary>
    ''' Parses a RawResponse to a UuidByNameResponse
    ''' </summary>
    Public Sub New(ByVal RawResponse As String)
        Me._RawResponse = RawResponse

        Try
            Me._Response.ID = Methods.GetValue(RawResponse, "id")
            Me._Response.Name = Methods.GetValue(RawResponse, "name")
            Me._Response.Legacy = Methods.GetValue(RawResponse, "legacy")

            Me._Response.Error = Methods.GetValue(RawResponse, "id")
            Me._Response.ErrorMessage = Methods.GetValue(RawResponse, "name")
        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.DarkRed
            Console.WriteLine(ex.ToString)
            Console.ForegroundColor = ConsoleColor.White
        End Try
    End Sub

End Class

