Public Class Prova
    Inherits Entity
    Implements IEntity

    Private _Data As String
    Public Property Data As String
        Get
            Return _Data
        End Get
        Set(value As String)
            _Data = value
        End Set
    End Property

    Private _IdDisciplina As String
    Public Property IdDisciplina As String
        Get
            Return _IdDisciplina
        End Get
        Set(value As String)
            _IdDisciplina = value
        End Set
    End Property

    Private _Tipo As TipoProva
    Public Property Tipo As String
        Get
            Return _Tipo
        End Get
        Set(value As String)
            _Tipo = value
        End Set
    End Property
End Class
