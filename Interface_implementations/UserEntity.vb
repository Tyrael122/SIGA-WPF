Public MustInherit Class UserEntity
    Inherits Entity
    Implements IEntity

    Private _Login As String
    Public Property Login As String
        Get
            Return _Login
        End Get
        Set(value As String)
            _Login = value
        End Set
    End Property

    Private _Password As String
    Public Property Password As String
        Get
            Return _Password
        End Get
        Set(value As String)
            _Password = value
        End Set
    End Property

    Protected Overrides Function GetFieldsToParse() As String()
        Return MyBase.GetFieldsToParse()
    End Function
End Class
