Public Class PlayerProfileByUUIDresponse

    Structure PlayerProfileByUUIDresponse
        ' Specific
        Dim UUID As String
        Dim Name As String
        Dim Properties As PlayerProperties

        ' Common
        Dim [Error] As String
        Dim ErrorMessage As String
    End Structure
    Public Structure PlayerProperties
        Dim Name As String ' Property name
        Dim Value As String ' Base64
        Dim SkinURL As String
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
    ''' Get the formatted response. See Authentication.PlayerProfileByUUIDresponse.
    ''' </summary>
    Public ReadOnly Property GetResponse As PlayerProfileByUUIDresponse
        Get
            Return Me._Response
        End Get
    End Property
    Private _Response As PlayerProfileByUUIDresponse

    ''' <summary>
    ''' Parses a RawResponse to a PlayerProfileByUUIDresponse
    ''' </summary>
    Public Sub New(ByVal RawResponse As String)
        Me._RawResponse = RawResponse

        Try
            ' The name and UUID
            Me._Response.UUID = Methods.GetValue(RawResponse, "id")
            Me._Response.Name = Methods.GetValue(RawResponse, "name")

            ' The properties vars
            Dim Properties As String = RawResponse.Split("[")(1)
            Dim Base64Response As String = Methods.GetValue(Properties, "value")

            ' The properties
            Me._Response.Properties.Name = Methods.GetValue(Properties, "name")
            Me._Response.Properties.Value = Methods.DecodeBase64String(Base64Response)
            Me._Response.Properties.SkinURL = Methods.GetValue(Me._Response.Properties.Value, "url")

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


