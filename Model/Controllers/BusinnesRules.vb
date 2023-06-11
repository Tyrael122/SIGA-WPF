Public Class BusinessRules
    Private Shared ReadOnly dataBridge As IDAL = New DAL() ' TODO: Search for a way to cleanly dispose of the connection created by the IDAL.

    Friend Shared Sub DeleteAluno(idAluno As String)
        dataBridge.Delete(idAluno, "IdAluno", Table.Nota)

        dataBridge.Delete(idAluno, Table.Aluno)
    End Sub

    Friend Shared Sub DeleteDisciplinasAluno(idAluno As String)
        dataBridge.Delete(idAluno, "IdAluno", Table.AlunoDisciplina)
    End Sub

    Friend Shared Sub DeleteProfessor(idProfessor As String)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.ProfessorDisciplina)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Prova)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Horario)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Aula)

        dataBridge.Delete(idProfessor, Table.Professor)
    End Sub

    Friend Shared Sub DeleteDisciplinas(idEntity As String, whereField As String, table As Table)
        dataBridge.Delete(idEntity, whereField, table)
    End Sub

    Friend Shared Function SaveWithOutput(aulaData As Dictionary(Of String, Object), table As Table) As List(Of IDictionary(Of String, Object))
        Return dataBridge.SaveWithOutput(aulaData, table)
    End Function

    Friend Shared Function Save(data As IDictionary(Of String, Object), table As Table) As Boolean
        Return dataBridge.Save(data, table)
    End Function

    Friend Shared Function GetAll(table As Table) As IEnumerable(Of IDictionary(Of String, Object))
        Return dataBridge.SelectAll(table)
    End Function

    Friend Shared Function GetDisciplinas(table As Table, idEntity As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim relation As New Relation(table, Table.Disciplina)

        Dim relationColumns = relation.GetRelationColumns()

        Dim idDisciplinas = dataBridge.SelectAll(relation.GetRelationTable()).
            Where(Function(dict) dict(relationColumns.uniqueEntity) = idEntity).
            Select(Function(dict) dict(relationColumns.multipleEntity))

        Return GetAll(Table.Disciplina).Where(Function(disciplina) idDisciplinas.Contains(disciplina("Id")))
    End Function

    Friend Shared Function GetAllById(id As String, tableStr As Table) As IEnumerable(Of IDictionary(Of String, Object))
        Return dataBridge.SelectAll(tableStr).Where(Function(dict) dict("Id") = id)
    End Function

    Friend Shared Function SalvarAula(aula As IDictionary(Of String, Object)) As String
        Dim aulaMatched = GetAll(Table.Aula).Where(Function(dict) Date.Parse(dict("Data")) = Date.Parse(aula("Data")))

        If aulaMatched.Any() Then
            Dim idAula = aulaMatched.First()("Id")

            dataBridge.Delete(idAula, "IdAula", Table.Presenca)

            Return idAula
        End If

        Return SaveWithOutput(aula, Table.Aula).First()("Id")
    End Function

    Public Shared Function GetDisciplinasWithCheckBoxColumn(idEntity As String, table As Table) As IEnumerable(Of IDictionary(Of String, Object))
        Dim idDisciplinas = GetDisciplinas(table, idEntity).Select(Function(dict) dict("Id"))

        Dim disciplinas = GetAll(Table.Disciplina)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = idDisciplinas.Contains(disciplina("Id"))
        Next

        Return disciplinas
    End Function
End Class
