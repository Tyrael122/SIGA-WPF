Public Class Relation
    Private ReadOnly dataBridge As IDAL = New DAL()
    Private ReadOnly uniqueEntityTable As Table
    Private ReadOnly MultipleEntityTable As Table

    Public uniqueEntityData As IDictionary(Of String, Object)
    Public idRelatedEntites As IEnumerable(Of String)

    Public Sub New(uniqueEntityTable As Table, multipleEntityTable As Table)
        Me.uniqueEntityTable = uniqueEntityTable
        Me.MultipleEntityTable = multipleEntityTable
    End Sub


    Public Function GetRelationColumns() As RelationColumn
        Dim relationStruct = New RelationColumn With {
            .uniqueEntity = "Id" & uniqueEntityTable.ToString(),
            .multipleEntity = "Id" & MultipleEntityTable.ToString()
        }

        Return relationStruct
    End Function

    Public Function GetRelationTable() As Table
        Dim relationTableString = uniqueEntityTable.ToString() & MultipleEntityTable.ToString()
        Try
            Return [Enum].Parse(GetType(Table), relationTableString)
        Catch ex As ArgumentException
            relationTableString = MultipleEntityTable.ToString() & uniqueEntityTable.ToString()

            Return [Enum].Parse(GetType(Table), relationTableString)
        End Try
    End Function

    Public Function Save() As Boolean
        Dim outputData = dataBridge.SaveWithOutput(uniqueEntityData, uniqueEntityTable)
        Dim entityId = outputData.First()("Id")

        Dim columns = GetRelationColumns()

        For Each id In idRelatedEntites
            Dim dataDict As New Dictionary(Of String, Object) From {
                {columns.uniqueEntity, entityId},
                {columns.multipleEntity, id}
            }

            Dim isInsertSucessefull = dataBridge.Save(dataDict, GetRelationTable())
            If Not isInsertSucessefull Then
                Return False
            End If
        Next

        Return True
    End Function

    Public Function GetAllMultipleEntitiesById(uniqueEntityId As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim relationColumns = GetRelationColumns()
        Dim relationTable = GetRelationTable()

        Dim dataBridge As IDAL = New DAL()

        Dim idList = dataBridge.SelectAll(relationTable).
            Where(Function(dict) dict(relationColumns.uniqueEntity) = uniqueEntityId).
            Select(Function(dict) dict(relationColumns.multipleEntity))

        Return dataBridge.SelectAll(MultipleEntityTable).Where(Function(dict) idList.Contains(dict("Id")))
    End Function

    Friend Function Update(entityId As String) As Object
        dataBridge.Update(entityId, uniqueEntityData, uniqueEntityTable)

        Dim columns = GetRelationColumns()

        For Each id In idRelatedEntites
            Dim dataDict As New Dictionary(Of String, Object) From {
                {columns.uniqueEntity, entityId},
                {columns.multipleEntity, id}
            }

            dataBridge.Save(dataDict, GetRelationTable())
        Next

        Return True
    End Function
End Class
