Imports System.Net
Imports System.Text
Imports System.IO


''' <summary>
''' This class is used to send requests to the Mojang API.
''' </summary>
''' <example>Dim Authentication As New API.Request(API.Request.Method.POST, API.URL.AUTHENTICATE)</example>
Public Class Request

    ' To the one who reads this : you have to know that the code below is not as good as it should be. Plus, the way I parse JSON responses is kinda dirty.
    ' However, it works (but it'll no longer work when Mojang will change its API)
    ' You are allowed to copy this code and to modify it. But you have to write my name (Hawezo) as a commentary in the top of all your copied contents.

    ''' <summary>
    ''' This enum is the required value when you create the Request object.
    ''' </summary>
    Public Enum Method
        [POST]
        [GET]
    End Enum

    ''' <summary>
    ''' Here is the type of your request. The type's type (haha) is Request.Method.
    ''' </summary>
    Public Property Type As Request.Method

    ''' <summary>
    ''' This is the needed request URL to make a request to. You can find adresses at API.URL.
    ''' </summary>
    Public Property RequestURL As Uri

    ''' <summary>
    ''' Creates a Request object.
    ''' </summary>
    ''' <param name="Type">The API.Request.Method object that will define the request type.</param>
    ''' <param name="RequestURL">The request URL. See API.URL links.</param>
    ''' <param name="RequestParameters">The parameters of the specified link. See the choosen API.URl description.</param>
    ''' <exception cref="ArgumentOutOfRangeException">Will be thrown if RequestParameters misses arguments.</exception>
    Public Sub New(ByVal Type As Request.Method, ByVal RequestURL As String, ByVal ParamArray RequestParameters() As String)
        Me._Type = Type

        If RequestURL.Contains("{") Then ' Check if parameters are needed
            If Methods.GetOccurences("{", RequestURL) > RequestParameters.Count Then ' Check if the good amount of parameters have been given
                Throw New ArgumentOutOfRangeException("RequestParameters", "Too few arguments. See the URL help message to get the amount of arguments.")
            Else
                Me._RequestURL = New Uri(String.Format(RequestURL, RequestParameters))
            End If
        Else
            Me._RequestURL = New Uri(RequestURL)
        End If

        Console.WriteLine(Me._RequestURL.ToString)
    End Sub

    ''' <summary>
    ''' Execute the request. If this is a POST, you will NEED a header.
    ''' </summary>
    Public Function Execute(ByVal RequestHeader As String) As String
        Try
            Dim WebRequest As WebRequest = WebRequest.Create(Me.RequestURL)
            Dim RequestData As Byte() = Nothing

            WebRequest.ContentType = "application/json"

            Select Case Me._Type
                Case Method.GET  ' GET METHOD
                    WebRequest.Method = "GET"

                Case Method.POST ' POST METHOD
                    WebRequest.Method = "POST"
                    RequestData = Encoding.UTF8.GetBytes(RequestHeader)
                    WebRequest.ContentLength = RequestData.Length


                    ' Writing request
                    Dim RequestStream As Stream = WebRequest.GetRequestStream()
                    Try
                        RequestStream.Write(RequestData, 0, RequestData.Length)
                        RequestStream.Close()
                    Catch ex As Exception
                        ' TODO
                        Console.WriteLine(ex.ToString)
                    End Try

            End Select

            ' Reading response
            Dim ResponseReader As New StreamReader(WebRequest.GetResponse.GetResponseStream())
            Dim RequestResult As String = (ResponseReader.ReadToEnd)

            ' Closing objects
            WebRequest = Nothing
            ResponseReader.Close()
            ResponseReader.Dispose()
            ResponseReader = Nothing
            GC.Collect()


            Return RequestResult
        Catch ConnexionFailed As WebException ' Erreur d'authentification
            Dim ResponseReader As New StreamReader(ConnexionFailed.Response.GetResponseStream())
            Dim RequestResult As String = (ResponseReader.ReadToEnd)
            Return RequestResult
        Catch ex As Exception
            Return ex.ToString
        End Try

    End Function


End Class



''' <summary>
''' The list of usable URLs to make request to.
''' </summary>
Public Class URL

    ''' <summary>
    ''' Method : GET ; 
    ''' Returns : a JSON with all Mojang's services' states.
    ''' </summary>
    Public Shared ReadOnly CHECK As String = "https://status.mojang.com/check"

    ''' <summary>
    ''' Method : GET ; 
    ''' Params : 1 (case-sensitive pseudo) ; 
    ''' Returns : the ID of a player.
    ''' </summary>
    Public Shared ReadOnly UUID_BY_NAME As String = "https://api.mojang.com/users/profiles/minecraft/{0}"

    ''' <summary>
    ''' Method : GET ; 
    ''' Params : 2 (case-sensitive pseudo/JAVA timestamp) ; 
    ''' Returns the ID of a player at a certain timestamp.
    ''' </summary>
    Public Shared ReadOnly UUID_BY_NAME_AT_TIME As String = "https://api.mojang.com/users/profiles/minecraft/{0}&?at={1}"

    ''' <summary>
    ''' Method : GET ; 
    ''' Params : 1 (UUID) ; 
    ''' Returns : the usernames the user has used in past ; 
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly NAME_HISTORY_BY_UUID As String = "https://api.mojang.com/user/profiles/{0}/names"

    ''' <summary>
    ''' Method : POST ; 
    ''' Header : JSON-formatted list of names (e.g. ["Hawezo", "tmort06"]) ; 
    ''' Returns : 
    '''    - the player(s) UUID(s) ; 
    '''    - the player name(s) (case-corrected) ; 
    '''    - if legacy (account not migrated) ; 
    '''    - if demo (account unpaid)
    ''' </summary>
    Public Shared ReadOnly PLAYER_INFOS_BY_NAME As String = "https://api.mojang.com/profiles/minecraft"

    ''' <summary>
    ''' Method : GET ;
    ''' Params : 1 (pseudo) ;
    ''' Returns : JSON-formatted user profile ; base-64-formatted skin and cape ; 
    ''' Limit : 1/minute
    ''' </summary>
    Public Shared ReadOnly PLAYER_PROFILE_BY_UUID As String = "https://sessionserver.mojang.com/session/minecraft/profile/{0}"

    ''' <summary>
    ''' Method : POST ;
    ''' Header : something like this : "{"agent": {"name": "Minecraft","version": 1},"username": " [id] ","password"": " [pass] "}"
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared ReadOnly AUTHENTICATE As String = "https://authserver.mojang.com/authenticate"

End Class
