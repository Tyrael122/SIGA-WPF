Public Class ProvaViewModel
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

    Private _tipo As String
    Public Property Tipo As String
        Get
            Return _tipo
        End Get
        Set(value As String)
            _tipo = value
            OnPropertyChanged(NameOf(Tipo))
        End Set
    End Property
End Class
