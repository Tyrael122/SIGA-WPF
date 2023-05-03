Public Class BusinessRules
    Private Shared userId As String

    Private Shared ReadOnly dataBridge As IDAL = New DAL() ' TODO: Search for a way to cleanly dispose of the connection created by the IDAL.

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
        Return dataBridge.Save(data, table)
    End Function

    Public Shared Function GetAll(Of T)(table As Table) As IEnumerable(Of T)
        Dim alunos = dataBridge.ReadAllEntities(table)

        Return alunos.Cast(Of T)
    End Function

    Friend Shared Function GetAllCursosAsDict() As List(Of IDictionary(Of String, String))
        Dim dataBridge As IDAL = New DAL()
        Return dataBridge.SelectAll(Table.Curso)
    End Function

    Public Shared Function SaveEntityWithRelation(entityData As IDictionary(Of String, String), entitiesToRelate As IEnumerable(Of Object), relationTable As Table) As Boolean
        Dim entityTable = GenerateEntityTable(relationTable)

        Dim outputData = dataBridge.SaveWithOutput(entityData, entityTable)

        Dim entityId = outputData.First()("Id")

        Dim uniqueEntityColumn = "Id" + relationTable.ToString().Replace(entityTable.ToString(), "")
        Dim multipleEntityColumn = "Id" + entityTable.ToString()

        For Each entity In entitiesToRelate
            Dim dataDict As New Dictionary(Of String, String) From {
                {uniqueEntityColumn, entityId},
                {multipleEntityColumn, entity.Id}
            }

            Dim isInsertSucessefull = dataBridge.Save(dataDict, relationTable)
            If Not isInsertSucessefull Then
                Return False
            End If
        Next

        Return True
    End Function

    Private Shared Function GenerateEntityTable(relationTable As Table) As Table
        Dim chars = relationTable.ToString().Where(
                                            Function(c)
                                                If c.ToString().ToLower() = c Then
                                                    Return False
                                                End If
                                                Return True
                                            End Function)

        Dim start = relationTable.ToString().IndexOf(chars.First())
        Dim upTo = relationTable.ToString().IndexOf(chars.Last())

        Dim entityTable = relationTable.ToString().Substring(start, upTo)
        Return [Enum].Parse(Table.Aluno.GetType(), entityTable)
    End Function

    Friend Shared Function GetDisciplinasCurso(idCurso As String) As IEnumerable(Of Disciplina)
        Dim idDisciplinasCurso = dataBridge.SelectAll(Table.CursoDisciplina).Where(Function(dict) dict.Item("IdCurso") = idCurso).Select(Function(dict) dict.Item("IdDisciplina"))

        Return GetAll(Of Disciplina)(Table.Disciplina).Where(Function(disciplina) idDisciplinasCurso.Contains(disciplina.Id))
    End Function

    Friend Shared Function GetDisciplinasProfessor() As Object
        Dim idDisciplinasProfessor = dataBridge.SelectAll(Table.ProfessorDisciplina).Where(Function(dict) dict.Item("IdProfessor") = userId).Select(Function(dict) dict.Item("IdDisciplina"))

        Return GetAll(Of Disciplina)(Table.Disciplina).Where(Function(disciplina) idDisciplinasProfessor.Contains(disciplina.Id))
    End Function

    Friend Shared Sub SaveUserId(userId As String)
        BusinessRules.userId = userId
    End Sub
End Class
