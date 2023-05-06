Public Class Relation(Of T As IDAO, V As IDAO)
    ''' <summary>
    ''' The first type is the unique entity type, the second is the multiple entity type.
    ''' </summary>

    Private ReadOnly dataBridge As IDAL = New DAL()
    Public uniqueEntityData As IDictionary(Of String, String)
    Public entitiesToRelate As IEnumerable(Of Object)

    Public Function GetRelationColumns() As RelationColumn
        Dim relationStruct = New RelationColumn With {
            .uniqueEntity = "Id" + GetType(T).Name,
            .multipleEntity = "Id" + GetType(V).Name
        }

        Return relationStruct
    End Function

    Public Function GetRelationTable() As Table
        Dim relationTableString = GetType(T).Name + GetType(V).Name
        Try
            Return [Enum].Parse(GetType(Table), relationTableString)
        Catch ex As ArgumentException
            relationTableString = GetType(V).Name + GetType(T).Name

            Return [Enum].Parse(GetType(Table), relationTableString)
        End Try
    End Function

    Public Function GetUniqueEntityTable() As Table
        Dim relationTableString = GetType(T).Name
        Return [Enum].Parse(GetType(Table), relationTableString)
    End Function


    Public Function GetMultipleEntityTable() As Table
        Dim relationTableString = GetType(V).Name
        Return [Enum].Parse(GetType(Table), relationTableString)
    End Function

    Public Function Save() As Boolean
        Dim uniqueEntityTable = GetUniqueEntityTable()

        Dim outputData = dataBridge.SaveWithOutput(uniqueEntityData, uniqueEntityTable)
        Dim entityId = outputData.First()("Id")

        Dim columns = GetRelationColumns()

        For Each entity In entitiesToRelate
            Dim dataDict As New Dictionary(Of String, String) From {
                {columns.uniqueEntity, entityId},
                {columns.multipleEntity, entity.Id}
            }

            Dim isInsertSucessefull = dataBridge.Save(dataDict, GetRelationTable())
            If Not isInsertSucessefull Then
                Return False
            End If
        Next

        Return True
    End Function

    Public Function GetAllMultipleEntitiesById(uniqueEntityId As String) As IEnumerable(Of V)
        Dim multipleEntities = GetAllMultipleEntitiesByIdAsDict(uniqueEntityId)

        Return EntityParser.ParseListOfDict(Of V)(multipleEntities)
    End Function

    Public Function GetAllMultipleEntitiesByIdAsDict(uniqueEntityId As String) As IEnumerable(Of IDictionary(Of String, String))
        Dim relationColumns = GetRelationColumns()
        Dim relationTable = GetRelationTable()

        Dim dataBridge As IDAL = New DAL()

        Dim idList = dataBridge.SelectAll(relationTable).
            Where(Function(dict) dict(relationColumns.uniqueEntity) = uniqueEntityId).
            Select(Function(dict) dict(relationColumns.multipleEntity))

        Return dataBridge.SelectAll(GetMultipleEntityTable()).Where(Function(dict) idList.Contains(dict("Id")))
    End Function
End Class
