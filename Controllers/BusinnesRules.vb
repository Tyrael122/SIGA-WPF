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

    'Public Property Presenter As IPresenter
    '    Get
    '        Return Nothing
    '    End Get
    '    Set(value As IPresenter)
    '    End Set
    'End Property

    ' We are disabling this property for now,
    ' but eventually we will use it
    ' because we need a way to transmit data from the model/business rules to the presenter
    ' and that will be done through this interface
    ' through a method looking like this: loadsDataFromModel(data As IDicionary)

    Public Property DAL As IDAL
        Get
            Return Nothing
        End Get
        Set(value As IDAL)
        End Set
    End Property

    Public Shared Function RegisterAluno(data As IDictionary) As Boolean
        Dim Aluno As New Aluno()
        Try
            Aluno.LoadFromDictionary(data)
        Catch ex As ArgumentException
            Return False
        End Try

        Dim dataBridge As IDAL = New DAL()
        dataBridge.Save(Aluno, UserType.Aluno)

        ' TODO: Check if the insertion was correctly done
    End Function

    Public Shared Function GetEntityFromEnum(userType As UserType) As IDAO
        Select Case userType
            Case 0
                Return New Aluno()
            Case 1
                Return New Professor()
            Case 2
                Return New FuncionarioAdministrativo()
            Case Else
                Throw New ArgumentOutOfRangeException(NameOf(userType), "The userType is not valid, should be between 0 and 2")
        End Select
    End Function
End Class
