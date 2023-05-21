Imports System.ComponentModel

Public Class UserViewModel
    Inherits ViewModel

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
End Class
