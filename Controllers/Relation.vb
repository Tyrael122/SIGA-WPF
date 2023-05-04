﻿Public Class Relation(Of T As IDAO, V As IDAO)
    ''' <summary>
    ''' The first type is the unique entity type, the second is the multiple entity type.
    ''' </summary>

    Public uniqueEntityData As IDictionary(Of String, String)
    Public entitiesToRelate As IEnumerable(Of Object)

    Public Function GetRelationColumns() As RelationColumn
        Dim relationStruct = New RelationColumn With {
            .uniqueEntity = NameOf(T),
            .multipleEntity = NameOf(V)
        }

        Return relationStruct
    End Function

    Public Function GetRelationTable() As Table
        Dim relationTableString = NameOf(T) + NameOf(V)
        Return [Enum].Parse(Table.Aluno.GetType(), relationTableString)
    End Function

    Public Function GetUniqueEntityTable() As Table
        Dim relationTableString = NameOf(T)
        Return [Enum].Parse(Table.Aluno.GetType(), relationTableString)
    End Function


    Public Function GetMultipleEntityTable() As Table
        Dim relationTableString = NameOf(V)
        Return [Enum].Parse(Table.Aluno.GetType(), relationTableString)
    End Function

    Public Function Save() As Boolean
        Dim uniqueEntityTable = GetUniqueEntityTable()

        Dim dataBridge As IDAL = New DAL()

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

    'Public Function GetAllMultipleEntitiesBasedOn(uniqueEntityId As String) As IEnumerable(Of V)
    '    Dim relationColumns = GetRelationColumns()
    '    Dim relationTable = GetRelationTable()

    '    Dim dataBridge As IDAL = New DAL()

    '    Dim idList = dataBridge.SelectAll(relationTable).
    '        Where(Function(dict) dict(relationColumns.uniqueEntity) = uniqueEntityId).
    '        Select(Function(dict) dict(relationColumns.multipleEntity))

    '    Return BusinessRules.GetAll(Of V)(relationTable).Where(Function(multipleEntity) idList.Contains(multipleEntity.Id))
    'End Function
End Class
