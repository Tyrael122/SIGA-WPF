Public Class Curso
    Inherits DAO
    Implements IDAO

    Private _Turno As Turno
    Public Property Turno As Turno
        Get
            Return _Turno
        End Get
        Set(value As Turno)
            _Turno = value
        End Set
    End Property

    Private _Sigla As String
    Public Property Sigla As String
        Get
            Return Sigla
        End Get
        Set(value As String)
            _Sigla = value
        End Set
    End Property

    Private _Nome As String
    Public Property Nome As String
        Get
            Return Nome
        End Get
        Set(value As String)
            _Nome = value
        End Set
    End Property
End Class
