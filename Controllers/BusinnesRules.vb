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

    Public Shared Function GetNewEntityOf(table As Table) As IDAO
        Select Case table
            Case Table.Aluno
                Return New Aluno()
            Case Table.Professor
                Return New Professor()
            Case Table.FuncionarioAdm
                Return New FuncionarioAdministrativo()
            Case Table.Curso
                Return New Curso()
            Case Else
                Throw New ArgumentOutOfRangeException(NameOf(UserType), "The userType is not valid, should be between 0 and 2")
        End Select
    End Function

    Public Shared Function GetTypeOf(userType As UserType) As IDAO
        Select Case userType
            Case UserType.Aluno
                Return New Aluno()
            Case UserType.Professor
                Return New Professor()
            Case UserType.FuncionarioAdm
                Return New FuncionarioAdministrativo()
            Case Else
                Throw New ArgumentOutOfRangeException(NameOf(userType), "The userType is not valid, should be between 0 and 2")
        End Select
    End Function

    Friend Shared Function RegisterEntity(data As Object, entityType As Table) As Object
        Dim dataBridge As IDAL = New DAL()
        dataBridge.Save(data, entityType)

        dataBridge.CloseConnection()

        ' TODO: Check if the insertion was correctly done
        Return True
    End Function

    Friend Shared Function GetAllAlunos() As IEnumerable
        Dim dataBridge As New DAL

        Dim alunos = dataBridge.ReadAll(UserType.Aluno)

        Return alunos.Cast(Of Aluno)
    End Function

    Friend Shared Function GetAllProfessores() As IEnumerable
        Dim dataBridge As New DAL

        Dim professores = dataBridge.ReadAll(UserType.Professor)

        Return professores.Cast(Of Professor)
    End Function

    Friend Shared Function GetAllCursos() As IEnumerable
        Dim dataBridge As New DAL

        Dim cursos = dataBridge.ReadAll(Table.Curso)

        Return cursos.Cast(Of Curso)
    End Function
End Class
