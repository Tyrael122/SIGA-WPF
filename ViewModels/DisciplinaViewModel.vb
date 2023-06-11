Public Class DisciplinaViewModel
    Inherits ViewModel


    Private _id As String
    Public Property Id As String
        Get
            Return _id
        End Get
        Set(value As String)
            _id = value
            OnPropertyChanged(NameOf(Id))
        End Set
    End Property

    Private _name As String
    Public Property Name As String
        Get
            Return _name
        End Get
        Set(value As String)
            _name = value
            OnPropertyChanged(NameOf(Name))
        End Set
    End Property

    Private _semester As String
    Public Property Semester As String
        Get
            Return _semester
        End Get
        Set(value As String)
            _semester = value
            OnPropertyChanged(NameOf(Semester))
        End Set
    End Property

    Private _workload As String
    Public Property Workload As String
        Get
            Return _workload
        End Get
        Set(value As String)
            _workload = value
            OnPropertyChanged(NameOf(Workload))
        End Set
    End Property

    Private _description As String
    Public Property Description As String
        Get
            Return _description
        End Get
        Set(value As String)
            _description = value
            OnPropertyChanged(NameOf(Description))
        End Set
    End Property
End Class
