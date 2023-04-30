Public Class Disciplina
    Inherits DAO
    Implements IDAO

    Private _Id As Integer
    Public Property Id As Integer
        Get
            Return _Id
        End Get
        Set(value As Integer)
            _Id = value
        End Set
    End Property

    Private _Name As String
    Public Property Name As String
        Get
            Return _Name
        End Get
        Set(value As String)
            _Name = value
        End Set
    End Property

    Private _Semester As Integer
    Public Property Semester As Integer
        Get
            Return _Semester
        End Get
        Set(value As Integer)
            _Semester = value
        End Set
    End Property

    Private _Workload As Integer
    Public Property Workload As Integer
        Get
            Return _Workload
        End Get
        Set(value As Integer)
            _Workload = value
        End Set
    End Property

    Private _Description As String
    Public Property Description As String
        Get
            Return _Description
        End Get
        Set(value As String)
            _Description = value
        End Set
    End Property
End Class
