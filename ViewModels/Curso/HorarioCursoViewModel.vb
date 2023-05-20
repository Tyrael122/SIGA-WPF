Public Class HorarioCursoViewModel
    Inherits ViewModel

    Private _idDisciplina As String
    Public Property IdDisciplina As Object
        Get
            Return _idDisciplina
        End Get
        Set(value As Object)
            _idDisciplina = value.Tag
            OnPropertyChanged(NameOf(IdDisciplina))
        End Set
    End Property

    Private _idProfessor As String
    Public Property IdProfessor As Object
        Get
            Return _idProfessor
        End Get
        Set(value As Object)
            _idProfessor = value.Tag
            OnPropertyChanged(NameOf(IdProfessor))
        End Set
    End Property

    Private _semestre As String
    Public Property Semestre As String
        Get
            Return _semestre
        End Get
        Set(value As String)
            _semestre = value
            OnPropertyChanged(NameOf(Semestre))
        End Set
    End Property

    Private _diaSemana As String
    Public Property DiaSemana As String
        Get
            Return _diaSemana
        End Get
        Set(value As String)
            _diaSemana = value
            OnPropertyChanged(NameOf(DiaSemana))
        End Set
    End Property

    Private _horarioInicio As String
    Public Property HorarioInicio As String
        Get
            Return _horarioInicio
        End Get
        Set(value As String)
            _horarioInicio = value
            OnPropertyChanged(NameOf(HorarioInicio))
        End Set
    End Property
    Private _horarioFim As String
    Public Property HorarioFim As String
        Get
            Return _horarioFim
        End Get
        Set(value As String)
            _horarioFim = value
            OnPropertyChanged(NameOf(HorarioFim))
        End Set
    End Property
End Class
