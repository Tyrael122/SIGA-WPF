Public Class ModelUtils
    Private Shared ReadOnly dataBridge As IDAL = New DAL() ' TODO: Search for a way to cleanly dispose of the connection created by the IDAL.

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

    Friend Shared Function FindById(id As String, tableStr As Table) As IEnumerable(Of IDictionary(Of String, Object))
        Return dataBridge.SelectAll(tableStr).Where(Function(dict) dict("Id") = id)
    End Function

    Public Shared Function GetDisciplinasComCheckBoxColumn(idEntity As String, table As Table) As IEnumerable(Of IDictionary(Of String, Object))
        Dim idDisciplinas = GetDisciplinas(table, idEntity).Select(Function(dict) dict("Id"))

        Dim disciplinas = GetAll(Table.Disciplina)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = idDisciplinas.Contains(disciplina("Id"))
        Next

        Return disciplinas
    End Function

    Friend Shared Sub Update(idEntity As String, table As Table, data As Dictionary(Of String, Object))
        dataBridge.Update(idEntity, data, table)
    End Sub

    ' TODO: Should be in the Presenter Class because it's a UI logic.
    Public Shared Function RemoveKeyFromDict(data As IEnumerable(Of IDictionary(Of String, Object)), keyToRemove As String) As IEnumerable(Of IDictionary(Of String, Object))
        data = data.Select(Function(dict)
                               If dict.ContainsKey(keyToRemove) Then
                                   dict.Remove(keyToRemove)
                               End If
                               Return dict
                           End Function).ToList()
        Return data
    End Function

    Friend Shared Sub Delete(idEntity As String, table As Table)
        dataBridge.Delete(idEntity, table)
    End Sub
End Class
