Public Class AulaViewModel
    Inherits ViewModel

    Private _data As String
    Public Property Data As String
        Get
            Return _data
        End Get
        Set(value As String)
            _data = value
            OnPropertyChanged(NameOf(Data))
        End Set
    End Property

    Private _idHorario As String
    Public Property IdHorario As String
        Get
            Return _idHorario
        End Get
        Set(value As String)
            _idHorario = value
            OnPropertyChanged(NameOf(IdHorario))
        End Set
    End Property
End Class
