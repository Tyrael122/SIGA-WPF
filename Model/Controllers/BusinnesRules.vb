Public Class BusinessRules
    Private Shared ReadOnly dataBridge As IDAL = New DAL() ' TODO: Search for a way to cleanly dispose of the connection created by the IDAL.

    Friend Shared Sub DeleteAluno(idAluno As String)
        dataBridge.Delete(idAluno, "IdAluno", Table.Nota)

        dataBridge.Delete(idAluno, Table.Aluno)
    End Sub

    Friend Shared Sub DeleteDisciplinasAluno(id As String)
        dataBridge.Delete(id, "IdAluno", Table.AlunoDisciplina)
    End Sub

    Friend Shared Function SaveWithOutput(aulaData As Dictionary(Of String, String), table As Table) As List(Of IDictionary(Of String, String))
        Return dataBridge.SaveWithOutput(aulaData, table)
    End Function

    Friend Shared Function Save(data As IDictionary, table As Table) As Boolean
        Return dataBridge.Save(data, table)
    End Function

    Friend Shared Function GetAll(table As Table) As IEnumerable(Of IDictionary(Of String, String))
        Return dataBridge.SelectAll(table)
    End Function

    Friend Shared Function GetDisciplinas(table As Table, idEntity As String) As IEnumerable(Of IDictionary(Of String, String))
        Dim relation As New Relation(table, Table.Disciplina)

        Dim relationColumns = relation.GetRelationColumns()

        Dim idDisciplinas = dataBridge.SelectAll(relation.GetRelationTable()).
            Where(Function(dict) dict(relationColumns.uniqueEntity) = idEntity).
            Select(Function(dict) dict(relationColumns.multipleEntity))

        Return GetAll(Table.Disciplina).Where(Function(disciplina) idDisciplinas.Contains(disciplina("Id")))
    End Function

    Friend Shared Function GetAllById(id As String, tableStr As Table) As IEnumerable(Of IDictionary(Of String, String))
        Return dataBridge.SelectAll(tableStr).Where(Function(dict) dict("Id") = id)
    End Function

    Friend Shared Function SalvarAula(aula As Dictionary(Of String, String)) As String
        Dim aulaMatched = GetAll(Table.Aula).Where(Function(dict) Date.Parse(dict("Data")) = Date.Parse(aula("Data")))

        If aulaMatched.Any() Then
            Dim idAula = aulaMatched.First()("Id")

            dataBridge.Delete(idAula, "IdAula", Table.Presenca)

            Return idAula
        End If

        Return SaveWithOutput(aula, Table.Aula).First()("Id")
    End Function
End Class
