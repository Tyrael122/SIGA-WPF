Imports System.ComponentModel

Public Class AlunoViewModel
    Implements INotifyPropertyChanged

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

    Private _curso As String
    Public Property Curso As String
        Get
            Return _curso
        End Get
        Set(value As String)
            _curso = value
            OnPropertyChanged(NameOf(Curso))
        End Set
    End Property

    Private _semestreInicio As String
    Public Property SemestreInicio As String
        Get
            Return _semestreInicio
        End Get
        Set(value As String)
            _semestreInicio = value
            OnPropertyChanged(NameOf(SemestreInicio))
        End Set
    End Property

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Overridable Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
End Class
