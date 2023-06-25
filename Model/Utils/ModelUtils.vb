Public NotInheritable Class ModelUtils
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

    Public Shared Function LoadEntityById(id As String, table As Table) As IDictionary(Of String, Object)
        Dim data = FindById(id, table).First()

        SessionCookie.AddCookie("Id" & table.ToString(), id)

        Return data
    End Function

    Public Shared Function LoadUserWithPhotoById(id As String, table As Table) As IDictionary(Of String, Object)
        Dim data = LoadEntityById(id, table)
        data("Foto") = PresenterUtils.ConvertByteArrayToImage(data("Foto"))

        Return data
    End Function

    Public Shared Function GetDisciplinasComCheckBoxColumn(idEntity As String, table As Table) As IEnumerable(Of IDictionary(Of String, Object))
        Dim idDisciplinas = GetDisciplinas(table, idEntity).Select(Function(dict) dict("Id"))

        Dim disciplinas = GetAll(Table.Disciplina)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = idDisciplinas.Contains(disciplina("Id"))
        Next

        Return disciplinas
    End Function

    Friend Shared Sub Update(table As Table, data As IDictionary(Of String, Object))
        Dim idEntity = SessionCookie.GetCookie("Id" & table.ToString())

        Update(idEntity, table, data)
    End Sub

    Friend Shared Sub Update(idEntity As String, table As Table, data As IDictionary(Of String, Object))
        data.Remove("Id")

        dataBridge.Update(idEntity, data, table)
    End Sub

    Friend Shared Sub Delete(idEntity As String, table As Table)
        dataBridge.Delete(idEntity, table)
    End Sub

    Public Shared Sub RegisterUserWithPhoto(relation As Relation)
        ParseUserPhoto(relation)

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
