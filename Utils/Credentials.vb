Public Class Credentials
    Public Property Login As String
    Public Property Password As String

    Public Sub New(username As String, password As String)
        Me.Login = username
        Me.Password = password
    End Sub

    Public Sub New(username As String)
        Me.Login = username
    End Sub
End Class
