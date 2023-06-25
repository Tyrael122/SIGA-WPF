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

    Friend Shared Function SalvarAula(aula As IDictionary(Of String, Object)) As String
        Dim aulaMatched = GetAll(Table.Aula).Where(Function(dict) Date.Parse(dict("Data")) = Date.Parse(aula("Data")))

        If aulaMatched.Any() Then
            Dim idAula = aulaMatched.First()("Id")

            dataBridge.Delete(idAula, "IdAula", Table.Presenca)

            Return idAula
        End If

        Return SaveWithOutput(aula, Table.Aula).First()("Id")
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

    Friend Shared Sub Delete(idEntity As String, table As Table)
        dataBridge.Delete(idEntity, table)
    End Sub

    Public Shared Sub RegisterUserWithPhoto(relation As Relation)
        Dim data = relation.uniqueEntityData
        relation.uniqueEntityData("Foto") = PresenterUtils.ConvertImageToByteArray(data("Foto"))

        relation.Save()
    End Sub

    Public Shared Sub UpdateUserWithPhoto(relation As Relation, idUser As String)
        ParseUserPhoto(relation)

        relation.Update(idUser)
    End Sub

    Private Shared Sub ParseUserPhoto(relation As Relation)
        Dim data = relation.uniqueEntityData
        relation.uniqueEntityData("Foto") = PresenterUtils.ConvertImageToByteArray(data("Foto"))
    End Sub
End Class
