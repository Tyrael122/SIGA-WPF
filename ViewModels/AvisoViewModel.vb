Friend Class AvisoViewModel
    Inherits ViewModel

    Private _titulo As String
    Public Property Titulo As String
        Get
            Return _titulo
        End Get
        Set(value As String)
            _titulo = value
            OnPropertyChanged(NameOf(Titulo))
        End Set
    End Property

    Private _conteudo As String
    Public Property Conteudo As String
        Get
            Return _conteudo
        End Get
        Set(value As String)
            _conteudo = value
            OnPropertyChanged(NameOf(Conteudo))
        End Set
    End Property
End Class
