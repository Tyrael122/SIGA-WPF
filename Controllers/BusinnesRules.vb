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

    Public Shared Function RegisterAluno(data As IDictionary) As Boolean
        Dim dataBridge As IDAL = New DAL()
        dataBridge.Save(data, UserType.Aluno)

        ' TODO: Check if the insertion was correctly done
        Return True
    End Function

    Public Shared Function GetEntityFromEnum(userType As UserType) As IDAO
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

    Friend Shared Function GetAllAlunos() As IEnumerable
        Dim dataBridge As New DAL

        Dim alunos = dataBridge.ReadAll(UserType.Aluno)

        Return alunos.Cast(Of Aluno)

        ' Converter a lista de alunos para uma lista de listas com os valores das propriedades do alunos

        'Dim fieldsToParse As String() = alunos.First().GetFieldsToParse()

        'Dim data = Array.Empty(Of String())
        'For Each aluno In alunos
        '    Dim fieldsAluno = Array.Empty(Of String)

        '    For Each field In fieldsToParse
        '        fieldsAluno = fieldsAluno.Append(aluno.GetType().GetField(field).GetValue(aluno)).ToArray()
        '    Next

        '    data = data.Append(fieldsAluno).ToArray()
        'Next
        'Return data

        'Dim listaDeAlunos As Object()




        'Dim list As Object() = alunos.ForEach(Function(aluno)

        '                                      End Function)

        'Dim test = From aluno In alunos
        '           Select New 

        'Return ' List of list with alunos values


    End Function
End Class
