Public MustInherit Class UserViewModel
    Inherits ViewModel

    Private _id As String
    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
            OnPropertyChanged(NameOf(Id))
        End Set
    End Property

    Private _login As String
    Public Property Login As String
        Get
            Return _login
        End Get
        Set(value As String)
            _login = value
            OnPropertyChanged(NameOf(Login))
        End Set
    End Property

    Private _password As String
    Public Property Password As String
        Get
            Return _password
        End Get
        Set(value As String)
            _password = value
            OnPropertyChanged(NameOf(Password))
        End Set
    End Property

    Private _email As String
    Public Property Email As String
        Get
            Return _email
        End Get
        Set(value As String)
            _email = value
            OnPropertyChanged(NameOf(Email))
        End Set
    End Property

    Private _foto As Object
    Public Property Foto As Object
        Get
            Return _foto
        End Get
        Set(value As Object)
            _foto = value
            OnPropertyChanged(NameOf(Foto))
        End Set
    End Property
End Class
