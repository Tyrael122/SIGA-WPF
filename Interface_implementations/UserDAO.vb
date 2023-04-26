Public MustInherit Class UserDAO
    Inherits DAO
    Implements IDAO

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

    Public Overrides Function GetFieldsToParse() As String() Implements IDAO.GetFieldsToParse
        Dim data As String() = MyBase.GetFieldsToParse()

        Dim additionalData As String() =
            {NameOf(Login), NameOf(Password)}

        For Each field In additionalData
            data = data.Append(field).ToArray()
        Next

        Return data
    End Function
End Class
