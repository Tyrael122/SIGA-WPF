Public Class AlunoViewModel
    Inherits UserViewModel

    Private _curso As String
    Public Property Curso As Object
        Get
            Return _curso
        End Get
        Set(value As Object)
            _curso = value.Tag
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

    Private _foto As ImageSource
    Public Property Foto As ImageSource
        Get
            Return _foto
        End Get
        Set(value As ImageSource)
            _foto = value
            OnPropertyChanged(NameOf(Foto))
        End Set
    End Property
End Class
