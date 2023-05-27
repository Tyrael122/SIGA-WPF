Public Class Relation
    Private ReadOnly dataBridge As IDAL = New DAL()
    Private ReadOnly UniqueEntityTable As Table
    Private ReadOnly MultipleEntityTable As Table

    Public uniqueEntityData As IDictionary(Of String, String)
    Public idRelatedEntites As IEnumerable(Of String)

    Public Sub New(uniqueEntityTable As Table, multipleEntityTable As Table)
        Me.UniqueEntityTable = uniqueEntityTable
        Me.MultipleEntityTable = multipleEntityTable
    End Sub


    Public Function GetRelationColumns() As RelationColumn
        Dim relationStruct = New RelationColumn With {
            .uniqueEntity = "Id" + UniqueEntityTable.ToString(),
            .multipleEntity = "Id" + MultipleEntityTable.ToString()
        }

        Return relationStruct
    End Function

    Public Function GetRelationTable() As Table
        Dim relationTableString = UniqueEntityTable.ToString() + MultipleEntityTable.ToString()
        Try
            Return [Enum].Parse(GetType(Table), relationTableString)
        Catch ex As ArgumentException
            relationTableString = MultipleEntityTable.ToString() + UniqueEntityTable.ToString()

            Return [Enum].Parse(GetType(Table), relationTableString)
        End Try
    End Function

    Public Function Save() As Boolean
        Dim outputData = dataBridge.SaveWithOutput(uniqueEntityData, UniqueEntityTable)
        Dim entityId = outputData.First()("Id")

        Dim columns = GetRelationColumns()

        For Each id In idRelatedEntites
            Dim dataDict As New Dictionary(Of String, String) From {
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

    Public Function GetAllMultipleEntitiesById(uniqueEntityId As String) As IEnumerable(Of IDictionary(Of String, String))
        Dim relationColumns = GetRelationColumns()
        Dim relationTable = GetRelationTable()

        Dim dataBridge As IDAL = New DAL()

        Dim idList = dataBridge.SelectAll(relationTable).
            Where(Function(dict) dict(relationColumns.uniqueEntity) = uniqueEntityId).
            Select(Function(dict) dict(relationColumns.multipleEntity))

        Return dataBridge.SelectAll(MultipleEntityTable).Where(Function(dict) idList.Contains(dict("Id")))
    End Function

    Friend Function Update(entityId As String) As Object
        dataBridge.Update(entityId, uniqueEntityData, UniqueEntityTable)

        Dim columns = GetRelationColumns()

        For Each id In idRelatedEntites
            Dim dataDict As New Dictionary(Of String, String) From {
                {columns.uniqueEntity, entityId},
                {columns.multipleEntity, id}
            }

            dataBridge.Save(dataDict, GetRelationTable())
        Next

        Return True
    End Function

    Public Shared Function SaveRelation(uniqueEntityTable As Table, relatedEntitiesTable As Table, idRelatedEntities As List(Of String), uniqueEntityData As Dictionary(Of String, String)) As Boolean
        Dim relation As New Relation(uniqueEntityTable, relatedEntitiesTable) With {
            .uniqueEntityData = uniqueEntityData,
            .idRelatedEntites = idRelatedEntities
        }

        Return relation.Save()
    End Function
End Class
