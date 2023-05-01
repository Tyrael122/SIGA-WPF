Public Class BusinessRules
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
                Throw New ArgumentOutOfRangeException(NameOf(UserType), "The table is not bound to any valid entity.")
        End Select
    End Function

    Friend Shared Function Save(data As IDictionary, table As Table) As Boolean
        Dim dataBridge As IDAL = New DAL()

        Return dataBridge.Save(data, table)
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

    Friend Shared Function GetAllCursosAsDict() As List(Of IDictionary(Of String, String))
        Dim dataBridge As New DAL

        Return dataBridge.SelectAll(Table.Curso)
    End Function

    Friend Shared Function GetAllDisciplinas() As IEnumerable(Of Disciplina)
        Dim dataBridge As New DAL

        Dim disciplinas = dataBridge.ReadAllEntities(Table.Disciplina)

        Return disciplinas.Cast(Of Disciplina)
    End Function

    Public Shared Function SaveWithRelation(entityData As IDictionary(Of String, String), entitiesToRelate As IEnumerable(Of Object), entityTable As Table, relationTable As Table) As Boolean
        Dim dataBridge As IDAL = New DAL()
        Dim outputData = dataBridge.SaveWithOutput(entityData, entityTable)

        Dim entityId = outputData.First()("Id")

        Dim possessedColumn = "Id" + relationTable.ToString().Replace(entityTable.ToString(), "")
        Dim possessorColumn = "Id" + entityTable.ToString()

        For Each entity In entitiesToRelate
            Dim dataDict As New Dictionary(Of String, String) From {
                {possessorColumn, entityId},
                {possessedColumn, entity.Id}
            }

            Dim isInsertSucessefull = dataBridge.Save(dataDict, relationTable)
            If Not isInsertSucessefull Then
                Return False
            End If
        Next

        Return True
    End Function

    Friend Shared Function GetDisciplinasCurso(idCurso As String) As IEnumerable(Of Disciplina)
        Dim dataBridge As New DAL()

        Dim idDisciplinasCurso = dataBridge.SelectAll(Table.CursoDisciplina).Where(Function(dict) dict.Item("IdCurso") = idCurso).Select(Function(dict) dict.Item("IdDisciplina"))

        Return GetAllDisciplinas().Where(Function(disciplina) idDisciplinasCurso.Contains(disciplina.Id))
    End Function
End Class
