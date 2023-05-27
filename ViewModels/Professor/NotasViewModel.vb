Public Class NotasViewModel
    Inherits ViewModel

    Private _idProva As String
    Public Property IdProva As Object
        Get
            Return _idProva
        End Get
        Set(value As Object)
            _idProva = value.Tag
            OnPropertyChanged(NameOf(IdProva))
        End Set
    End Property
End Class
