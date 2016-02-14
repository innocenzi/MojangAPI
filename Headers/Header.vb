Public Class Header

    Public Class Authentication


        ''' <summary>
        ''' Returns a normal authentication header with username and password.
        ''' </summary>
        ''' <param name="Username">The username of your player. Or its mail adress.</param>
        ''' <param name="Password">The password.</param>
        Public Shared Function Signin(ByVal Username As String, ByVal Password As String)
            Return "{ ""agent"": { ""name"": ""Minecraft"", ""version"": 1 }, ""username"": """ & Username & """, ""password"": """ & Password & """ }"
        End Function

        ''' <summary>
        ''' Returns a normal authentication header with username and password.
        ''' </summary>
        ''' <param name="Username">The username of your player. Or its mail adress.</param>
        ''' <param name="Password">The password.</param>
        ''' <param name="ClientToken">The token of your client (if you have not, let it blank)</param>
        Public Shared Function Signin(ByVal Username As String, ByVal Password As String, ByVal ClientToken As String)
            Return "{ ""agent"": { ""name"": ""Minecraft"", ""version"": 1 }, ""username"": """ & Username & """, ""password"": """ & Password & """, ""clientToken"": """ & ClientToken & """ }"
        End Function



        ''' <summary>
        ''' Returns a normal refresh header with access/client token.
        ''' </summary>
        ''' <param name="AccessToken">The access token currently available.</param>
        ''' <param name="ClientToken">The associated client token.</param>
        Public Shared Function Refresh(ByVal AccessToken As String, ByVal ClientToken As String)
            Return "{" & String.Format("""accessToken"": ""{0}"", ""clientToken"": ""{1}""", AccessToken, ClientToken) & "}"
        End Function



        ''' <summary>
        ''' Returns a normal signout header with username and password.
        ''' </summary>
        ''' <param name="Username">The username of your player. Or its mail adress.</param>
        ''' <param name="Password">The password.</param>
        Public Shared Function Signout(ByVal Username As String, ByVal Password As String)
            Return "{" & String.Format("""username"": ""{0}"", ""password"": ""{1}""", Username, Password) & "}"
        End Function



        ''' <summary>
        ''' Returns a normal Validate header with access/client token.
        ''' </summary>
        ''' <param name="AccessToken">The access token currently available.</param>
        ''' <param name="ClientToken">The associated client token.</param>
        Public Shared Function Validate(ByVal AccessToken As String, ByVal ClientToken As String)
            Return "{" & String.Format("""accessToken"": ""{0}"", ""clientToken"": ""{1}""", AccessToken, ClientToken) & "}"
        End Function



        ''' <summary>
        ''' Returns a normal Invalidate header with access/client token.
        ''' </summary>
        ''' <param name="AccessToken">The access token currently available.</param>
        ''' <param name="ClientToken">The associated client token.</param>
        Public Shared Function Invalidate(ByVal AccessToken As String, ByVal ClientToken As String)
            Return "{" & String.Format("""accessToken"": ""{0}"", ""clientToken"": ""{1}""", AccessToken, ClientToken) & "}"
        End Function


    End Class







    ' ----


    ''' <summary>
    ''' Returns a JSON parsed string for this type of request.
    ''' </summary>
    ''' <param name="Players">The list of players you want to get info from. Maximum is 100.</param>
    Public Shared Function PlayersInfoByName(ByVal ParamArray Players() As String) As String
        Dim Header As String = "["
        For Each Player As String In Players
            Header &= """" & Player & """"
        Next
        Header = Header.Replace("""""", """, """)
        Header &= "]"
        Return Header
    End Function

End Class
