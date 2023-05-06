Public Class BusinessRules
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
                Throw New ArgumentOutOfRangeException(NameOf(table), "The table is not bound to any valid entity.")
        End Select
    End Function

    Friend Shared Function Save(data As IDictionary, table As Table) As Boolean
        Return dataBridge.Save(data, table)
    End Function

    Public Shared Function GetAll(Of T As IDAO)() As IEnumerable(Of T)
        Dim alunos = dataBridge.ReadAllEntities(Of T)

        Return alunos.Cast(Of T)
    End Function

    Friend Shared Function GetAllCursosAsDict() As List(Of IDictionary(Of String, String))
        Dim dataBridge As IDAL = New DAL()
        Return dataBridge.SelectAll(Table.Curso)
    End Function

    Friend Shared Function GetDisciplinas(Of T As IDAO)(idEntity As String) As IEnumerable(Of Disciplina)
        Dim relation As New Relation(Of T, Disciplina)

        Dim relationColumns = relation.GetRelationColumns()

        Dim idDisciplinas = dataBridge.SelectAll(relation.GetRelationTable()).
            Where(Function(dict) dict(relationColumns.uniqueEntity) = idEntity).
            Select(Function(dict) dict(relationColumns.multipleEntity))

        Return GetAll(Of Disciplina)().Where(Function(disciplina) idDisciplinas.Contains(disciplina.Id))
    End Function
End Class
