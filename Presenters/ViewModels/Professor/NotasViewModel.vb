Public Class NotasViewModel
    Inherits ViewModel

    Private _idProva As String
    Public Property IdProva As String
        Get
            Return _idProva
        End Get
        Set(value As String)
            _idProva = value
            OnPropertyChanged(NameOf(IdProva))
        End Set
    End Property
End Class
