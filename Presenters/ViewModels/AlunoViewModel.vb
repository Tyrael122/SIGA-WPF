Imports System.ComponentModel

Public Class AlunoViewModel
    Inherits UserViewModel
    Implements INotifyPropertyChanged

    Private _curso As String
    Public Property Curso As String
        Get
            Return _curso
        End Get
        Set(value As String)
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

    Private _idDisciplinas As List(Of String)
    Public Property IdDisciplinas As List(Of String)
        Get
            Return _idDisciplinas
        End Get
        Set(value As List(Of String))
            _idDisciplinas = value
            OnPropertyChanged(NameOf(IdDisciplinas))
        End Set
    End Property
End Class
