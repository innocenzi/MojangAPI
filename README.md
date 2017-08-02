# Before you go

I wrote this a long time ago, and between now and this time, I improved myself enough to write a way better library. You can check out [MojangSharp](hawezo/MojangSharp) instead of losing time using this library.

***

# (Deprecated) MojangAPI

MojangAPI is a class library created in VB.NET (usable in C# too) to facilitate the process of sending requests to Mojang's API.
It is simple to uses.


## Authentication

To get your authentication token from Mojang, you will need to send a request to http://authserver.mojang.com/authenticate.
The process is totally simplified, thanks to this API.

### Usage

```vbnet
Dim AuthRequest As New Request(Method.POST, URL.AUTHENTICATION.SIGN_IN)
Dim RawResponse As String = AuthRequest.Execute(Headers.Authentication.Signin("Hawezo", "[password]"))
Dim AuthenticationResponse As New AuthenticationResponse(RawResponse)

Console.WriteLine("PlayerName: " & AuthenticationResponse.GetResponse.PlayerName)
Console.WriteLine("   AccessToken: " & AuthenticationResponse.GetResponse.AccessToken)
Console.WriteLine("   ClientToken: " & AuthenticationResponse.GetResponse.ClientToken)
```

## Player Informations

This is the second and the last method to use a POST.
You can find the header in the class MojangAPI.Headers.

### Usage

```vbnet
Dim MyRequest As New Request(Method.POST, URL.PLAYER_INFOS_BY_NAME)
Dim RawResponse As String = MyRequest.Execute(Headers.PlayersInfoByName("Hawezo", "tmort06")) ' Up to 100 players a time
Dim PlayersInfoByNameResponse As New PlayerInfosByNameResponse(RawResponse)

For Each PlayerInfo As PlayerInfosByNameResponse.PlayerInfo In PlayersInfoByNameResponse.GetResponse.PlayerInfos
  Console.WriteLine("PlayerName: " & PlayerInfo.PlayerName)
  Console.WriteLine("  UUID: " & PlayerInfo.UUID)
  Console.WriteLine("  Account migrated: " & Not PlayerInfo.Legacy)
  Console.WriteLine("  Account paid: " & Not PlayerInfo.Demo)
Next
```

## Other GET methods

The other methods use the GET request method, so the header may be empty.
However, you will have to specify some arguments in the declaration of your request.

### Example

In this example, I will retrieve the history of name changes of a player.

```vbnet
MyRequest = New Request(Method.GET, URL.NAME_HISTORY_BY_UUID, "[Your UUID]")
RawResponse = MyRequest.Execute()
Dim NameHistoryByUuidResponse As New NameHistoryByUuidResponse(RawResponse)

For Each HistoryEntry As NameHistoryByUuidResponse.HistoryEntry In NameHistoryByUuidResponse.GetResponse.HistoryEntries
  Console.WriteLine("PlayerName: " & HistoryEntry.Name)
  Console.WriteLine("  ChangedToAt: " & HistoryEntry.ChangedToAt)
Next
```

As you see, you have to specify an argument in the declaration of your requests. To know what arguments you have to put, read the description of the constructor.


## Handling errors

Sometimes you do something wrong. When it comes, you get an error. Every Response class has a Error and ErrorMessage value in its structure.
For example, your user has entered the wrong credentials. Here is the way to handle it:

```vbnet
MyRequest = New Request(Method.POST, URL.AUTHENTICATE)
RawResponse = MyRequest.Execute(Headers.Authenticate("SomeUsername", "SomePassword"))
Dim AuthenticationResponse As New AuthenticationResponse(RawResponse)

If AuthenticationResponse.GetResponse.Error = Nothing Then
  Console.WriteLine("PlayerName: " & AuthenticationResponse.GetResponse.PlayerName)
  Console.WriteLine("   AccessToken: " & AuthenticationResponse.GetResponse.AccessToken)
  Console.WriteLine("   ClientToken: " & AuthenticationResponse.GetResponse.ClientToken)
Else
  Console.WriteLine("Authentication failed.")
  Console.WriteLine("PlayerName: " & AuthenticationResponse.GetResponse.PlayerName)
  Console.WriteLine("   ErrorType: " & AuthenticationResponse.GetResponse.Error)
  Console.WriteLine("   ErrorMessage: " & AuthenticationResponse.GetResponse.ErrorMessage)
End If
```

In this case, you just have to check either the GetResponse.Error is empty or not. If it is, there is not error, if it is not, there is.

## Issues

If you spotted an issue, please start a new ticket. I will answer as soon as possible. Thank you.
