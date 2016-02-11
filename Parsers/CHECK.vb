Public Class Check

    Public Enum CheckResult
        Green = 10
        Yellow = 14
        Red = 4
    End Enum
    Public Structure Result
        Dim Website As CheckResult
        Dim MinecraftSession As CheckResult
        Dim MojangAccount As CheckResult
        Dim MojangAuth As CheckResult
        Dim MojangAuthServer As CheckResult
        Dim MinecraftSkins As CheckResult
        Dim MojangSessionServer As CheckResult
        Dim MojangAPI As CheckResult
        Dim MinecraftTextures As CheckResult

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
            RequestResults.Website = GetResult(Methods.GetValue(RequestString, "minecraft.net"))
            RequestResults.MinecraftSession = GetResult(Methods.GetValue(RequestString, "session.minecraft.net"))
            RequestResults.MojangAccount = GetResult(Methods.GetValue(RequestString, "account.mojang.com"))
            RequestResults.MojangAuth = GetResult(Methods.GetValue(RequestString, "auth.mojang.com"))
            RequestResults.MojangAuthServer = GetResult(Methods.GetValue(RequestString, "skins.minecraft.net"))
            RequestResults.MinecraftSkins = GetResult(Methods.GetValue(RequestString, "authserver.mojang.com"))
            RequestResults.MojangSessionServer = GetResult(Methods.GetValue(RequestString, "sessionserver.mojang.com"))
            RequestResults.MojangAPI = GetResult(Methods.GetValue(RequestString, "api.mojang.com"))
            RequestResults.MinecraftTextures = GetResult(Methods.GetValue(RequestString, "textures.minecraft.net"))

            RequestResults.Error = Methods.GetValue(RequestString, "error")
            RequestResults.ErrorMessage = Methods.GetValue(RequestString, "errorMessage")

            Return RequestResults
        Catch ex As Exception
            Console.WriteLine(ex.ToString)
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Return the CheckResult value of an item's value
    ''' </summary>
    ''' <param name="result">The item's value (e.g. yellow)</param>
    Private Function GetResult(ByVal result As String) As CheckResult
        Select Case result.ToLower.Trim
            Case "yellow"
                Return CheckResult.Yellow
            Case "green"
                Return CheckResult.Green
            Case Else
                Return CheckResult.Red
        End Select
    End Function

End Class


