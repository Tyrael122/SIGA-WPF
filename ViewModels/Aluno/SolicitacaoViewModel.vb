Friend Class SolicitacaoViewModel
    Inherits ViewModel

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
