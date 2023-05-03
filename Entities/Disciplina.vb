Public Class Disciplina
    Inherits DAO
    Implements IDAO

    Private _Id As String
    Public Property Id As String
        Get
            Return _Id
        End Get
        Set(value As String)
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

    Private _Semester As String
    Public Property Semester As String
        Get
            Return _Semester
        End Get
        Set(value As String)
            _Semester = value
        End Set
    End Property

    Private _Workload As String
    Public Property Workload As String
        Get
            Return _Workload
        End Get
        Set(value As String)
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
