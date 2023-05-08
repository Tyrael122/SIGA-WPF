Public Class SessionCookie
    Private Shared ReadOnly userData As New Dictionary(Of String, Object)

    Public Shared Sub AddCookie(key As String, data As Object)
        userData(key) = data
    End Sub

    Public Shared Function GetCookie(key As String) As Object
        Return userData(key)
    End Function

    Public Shared Function GetCookie(Of T)(key As String) As T
        Return userData(key)
    End Function
End Class
