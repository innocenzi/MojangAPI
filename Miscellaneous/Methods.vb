Imports System.Drawing
Imports System.IO

Friend Class Methods

    ''' <summary>
    ''' Return the value of a response's item
    ''' </summary>
    ''' <param name="value">A response item (e.g. minecraft.net)</param>
    <DebuggerHidden()>
    Friend Shared Function GetValue(ByVal RequestString As String, ByVal value As String) As String
        Try
            Return RequestString.Split({"""" & value & """:"}, StringSplitOptions.None)(1).Split("""")(1)
        Catch ex As Exception
            Console.WriteLine("Warning: " & value & " not found")
            Return Nothing
        End Try
    End Function

    ''' <summary>
    ''' Returns the number of occurences in a string
    ''' </summary>
    <DebuggerHidden()>
    Friend Shared Function GetOccurences(ByVal ch As Char, ByVal value As String) As Integer
        Dim cnt As Integer = 0
        For Each c As Char In value
            If c = ch Then cnt += 1
        Next
        Return cnt
    End Function

    ''' <summary>
    ''' Returns a decoded string from a base 64 string
    ''' </summary>
    Friend Shared Function DecodeBase64String(ByVal base64string As String) As String
        Dim data() As Byte = System.Convert.FromBase64String(base64string)
        Return System.Text.ASCIIEncoding.ASCII.GetString(data)
    End Function

End Class
