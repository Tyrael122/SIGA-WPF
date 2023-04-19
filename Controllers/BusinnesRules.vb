Public Class BusinessRules
    Public Property DAO As IDAO
        Get
            Return Nothing
        End Get
        Set(value As IDAO)
        End Set
    End Property

    Public Property FuncionarioAdministrativo As FuncionarioAdministrativo
        Get
            Return Nothing
        End Get
        Set(value As FuncionarioAdministrativo)
        End Set
    End Property

    Public Property Professor As Professor
        Get
            Return Nothing
        End Get
        Set(value As Professor)
        End Set
    End Property

    Public Property Aluno As Aluno
        Get
            Return Nothing
        End Get
        Set(value As Aluno)
        End Set
    End Property

    Public Property Presenter As IPresenter
        Get
            Return Nothing
        End Get
        Set(value As IPresenter)
        End Set
    End Property

    Public Property DAL As IDAL
        Get
            Return Nothing
        End Get
        Set(value As IDAL)
        End Set
    End Property

    Public Shared Function GetEntityFromEnum(userType As UserType) As IDAO
        Select Case userType
            Case 0
                Return New Aluno()
            Case 1
                Return New Professor()
            Case 2
                Return New FuncionarioAdministrativo()
            Case Else
                Throw New ArgumentOutOfRangeException("The userType is not valid, should be between 0 and 2")
        End Select
    End Function
End Class
