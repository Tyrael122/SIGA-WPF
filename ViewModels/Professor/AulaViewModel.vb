Public Class AulaViewModel
    Inherits ViewModel

    Private _data As Date
    Public Property Data As Date
        Get
            Return _data
        End Get
        Set(value As Date)
            _data = value
            OnPropertyChanged(NameOf(Data))
        End Set
    End Property

    Private _idHorario As String
    Public Property IdHorario As Object
        Get
            Return _idHorario
        End Get
        Set(value As Object)
            _idHorario = value.Tag
            OnPropertyChanged(NameOf(IdHorario))
        End Set
    End Property
End Class
