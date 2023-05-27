Public Class CursoViewModel
    Inherits ViewModel

    Private _nome As String
    Public Property Nome As String
        Get
            Return _nome
        End Get
        Set(value As String)
            _nome = value
            OnPropertyChanged(NameOf(Nome))
        End Set
    End Property

    Private _sigla As String
    Public Property Sigla As String
        Get
            Return _sigla
        End Get
        Set(value As String)
            _sigla = value
            OnPropertyChanged(NameOf(Sigla))
        End Set
    End Property

    Private _turno As String
    Public Property Turno As String
        Get
            Return _turno
        End Get
        Set(value As String)
            _turno = value
            OnPropertyChanged(NameOf(Turno))
        End Set
    End Property
End Class
