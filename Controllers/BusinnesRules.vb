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
            Case Table.Disciplina
                Return New Disciplina()
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

    Friend Shared Function Save(data As IDictionary, table As Table) As Object
        Dim dataBridge As IDAL = New DAL()
        If table = Table.Aluno Then
            Dim dictionary As New Dictionary(Of String, String)

            For Each key In data.Keys
                dictionary.Add(key, data(key))
            Next


            Dim curso = dataBridge.SelectAll(Table.Curso).Where(Function(dict) dict("Nome") = data("Curso"))


            data("Curso") = curso.First()("Id")
            dataBridge.Save(data, table)

            Return True
        End If

        dataBridge.Save(data, table)

        ' TODO: Maybe we should check if the insertion was correctly done
        Return True
    End Function

    Friend Shared Function GetAllAlunos() As IEnumerable
        Dim dataBridge As New DAL

        Dim alunos = dataBridge.ReadAllEntities(UserType.Aluno)

        Return alunos.Cast(Of Aluno)
    End Function

    Friend Shared Function GetAllProfessores() As IEnumerable
        Dim dataBridge As New DAL

        Dim professores = dataBridge.ReadAllEntities(UserType.Professor)

        Return professores.Cast(Of Professor)
    End Function

    Friend Shared Function GetAllCursos() As IEnumerable(Of Curso)
        Dim dataBridge As New DAL

        Dim cursos = dataBridge.ReadAllEntities(Table.Curso)

        Return cursos.Cast(Of Curso)
    End Function

    Friend Shared Function GetAllDisciplinas() As IEnumerable(Of Disciplina)
        Dim dataBridge As New DAL

        Dim disciplinas = dataBridge.ReadAllEntities(Table.Disciplina)

        Return disciplinas.Cast(Of Disciplina)
    End Function

    Friend Shared Function SaveDisciplinaCurso(courseData As IDictionary(Of String, String), disciplinasCurso As List(Of Disciplina), disciplinaCurso As Table) As Object
        ' TODO: Generalize the method to work with any table
        ' TODO: Get the course ID somehow. Maybe we should pass it as a parameter.

        Dim dataBridge As IDAL = New DAL()
        Dim data As IEnumerable(Of IDictionary(Of String, String))

        data = dataBridge.SelectAll(Table.Curso).Where(Function(dict)
                                                           For Each key In courseData.Keys
                                                               If dict(key) <> courseData(key) Then
                                                                   Return False
                                                               End If
                                                           Next
                                                           Return True
                                                       End Function)

        Dim CursoId As String = data.First().Item("Id")

        For Each disciplina In disciplinasCurso
            Dim dataDict As New Dictionary(Of String, String) From {
                {"IdCurso", CursoId},
                {"IdDisciplina", disciplina.Id}
            }

            dataBridge.Save(dataDict, disciplinaCurso)
        Next

        Return True
    End Function

    Friend Shared Function GetAllNomeCursos() As IEnumerable
        Dim cursos = GetAllCursos()
        Return cursos.Select(Function(curso) curso.Nome)
    End Function

    Friend Shared Function GetDisciplinasCurso(nomeCurso As String) As IEnumerable(Of Disciplina)
        Dim dataBridge As New DAL()

        Dim idCurso As Integer
        If IsNumeric(nomeCurso) Then
            idCurso = dataBridge.SelectAll(Table.Curso).Where(Function(dict) dict.Item("Id") = nomeCurso).First().Item("Id")

        Else
            idCurso = dataBridge.SelectAll(Table.Curso).Where(Function(dict) dict.Item("Nome") = nomeCurso).First().Item("Id")
        End If

        Dim idDisciplinasCurso = dataBridge.SelectAll(Table.DisciplinaCurso).Where(Function(dict) dict.Item("IdCurso") = idCurso).Select(Function(dict) dict.Item("IdDisciplina"))

        Return GetAllDisciplinas().Where(Function(disciplina) idDisciplinasCurso.Contains(disciplina.Id))
    End Function

    Friend Shared Function SaveDisciplinaAluno(alunoData As IDictionary(Of String, String), disciplinaAluno As List(Of Disciplina), table As Table) As Object
        ' TODO: Generalize the method to work with any table
        ' TODO: Get the course ID somehow. Maybe we should pass it as a parameter.

        Dim dataBridge As IDAL = New DAL()
        Dim data As IEnumerable(Of IDictionary(Of String, String))

        data = dataBridge.SelectAll(Table.Aluno).Where(Function(dict)
                                                           For Each key In alunoData.Keys
                                                               If dict(key) <> alunoData(key) Then
                                                                   Return False
                                                               End If
                                                           Next
                                                           Return True
                                                       End Function)

        Dim IdAluno As String = data.First().Item("ID")

        For Each disciplina In disciplinaAluno
            Dim dataDict As New Dictionary(Of String, String) From {
                {"IdAluno", IdAluno},
                {"IdDisciplina", disciplina.Id}
            }

            dataBridge.Save(dataDict, table)
        Next

        Return True
    End Function
End Class
