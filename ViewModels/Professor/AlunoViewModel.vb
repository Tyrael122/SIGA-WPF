Public Class AlunoViewModel
    Inherits UserViewModel

    Private _curso As String
    Public Property Curso As Object
        Get
            Return _curso
        End Get
        Set(value As Object)
            If value.GetType() = GetType(ComboBoxItem) Then
                _curso = value.Tag
                Return
            End If

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
End Class
