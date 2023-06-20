Public Class SessionCookie
    Private Shared ReadOnly userData As New Dictionary(Of String, Object)

    Public Shared Sub AddCookie(key As String, data As Object)
        userData(key.ToLower()) = data
    End Sub

    Public Shared Function GetCookie(key As String) As Object
        Return userData(key.ToLower())
    End Function
End Class
