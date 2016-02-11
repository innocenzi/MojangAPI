Public Class CheckResponse

    Structure CheckResponse
        ' Specific
        Dim Website As CheckResponseColor
        Dim MinecraftSession As CheckResponseColor
        Dim MojangAccount As CheckResponseColor
        Dim MojangAuth As CheckResponseColor
        Dim MojangAuthServer As CheckResponseColor
        Dim MinecraftSkins As CheckResponseColor
        Dim MojangSessionServer As CheckResponseColor
        Dim MojangAPI As CheckResponseColor
        Dim MinecraftTextures As CheckResponseColor

        ' Common
        Dim [Error] As String
        Dim ErrorMessage As String
    End Structure
    Public Enum CheckResponseColor
        Green = 10
        Yellow = 14
        Red = 4
    End Enum

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
    ''' Get the formatted response. See Authentication.CheckResponse.
    ''' </summary>
    Public ReadOnly Property GetResponse As CheckResponse
        Get
            Return Me._Response
        End Get
    End Property
    Private _Response As CheckResponse

    ''' <summary>
    ''' Parses a RawResponse to a CheckResponse
    ''' </summary>
    Public Sub New(ByVal RawResponse As String)
        Me._RawResponse = RawResponse

        Try
            Me._Response.Website = GetResult(Methods.GetValue(RawResponse, "minecraft.net"))
            Me._Response.MinecraftSession = GetResult(Methods.GetValue(RawResponse, "session.minecraft.net"))
            Me._Response.MojangAccount = GetResult(Methods.GetValue(RawResponse, "account.mojang.com"))
            Me._Response.MojangAuth = GetResult(Methods.GetValue(RawResponse, "auth.mojang.com"))
            Me._Response.MojangAuthServer = GetResult(Methods.GetValue(RawResponse, "skins.minecraft.net"))
            Me._Response.MinecraftSkins = GetResult(Methods.GetValue(RawResponse, "authserver.mojang.com"))
            Me._Response.MojangSessionServer = GetResult(Methods.GetValue(RawResponse, "sessionserver.mojang.com"))
            Me._Response.MojangAPI = GetResult(Methods.GetValue(RawResponse, "api.mojang.com"))
            Me._Response.MinecraftTextures = GetResult(Methods.GetValue(RawResponse, "textures.minecraft.net"))

            ' The errors
            Me._Response.Error = Methods.GetValue(RawResponse, "error")
            Me._Response.ErrorMessage = Methods.GetValue(RawResponse, "errorMessage")
        Catch ex As Exception
            Console.ForegroundColor = ConsoleColor.DarkRed
            Console.WriteLine(ex.ToString)
            Console.ForegroundColor = ConsoleColor.White
        End Try
    End Sub

    ''' <summary>
    ''' Return the CheckResult value of an item's value
    ''' </summary>
    ''' <param name="result">The item's value (e.g. yellow)</param>
    Private Function GetResult(ByVal result As String) As CheckResponseColor
        Select Case result.ToLower.Trim
            Case "yellow"
                Return CheckResponseColor.Yellow
            Case "green"
                Return CheckResponseColor.Green
            Case Else
                Return CheckResponseColor.Red
        End Select
    End Function

End Class


