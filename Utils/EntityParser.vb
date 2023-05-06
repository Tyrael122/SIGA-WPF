Public Class EntityParser
    Public Shared Function ParseListOfDict(Of T As IDAO)(data As IEnumerable(Of IDictionary(Of String, String))) As IEnumerable(Of T)
        Dim entitiesRead As New List(Of T)

        For Each dictionary In data
            Dim entityTable = [Enum].Parse(GetType(Table), GetType(T).Name)

            Dim tempEntity As IDAO = BusinessRules.GetNewEntityOf(entityTable)
            tempEntity.LoadFromDictionary(dictionary)

            entitiesRead.Add(tempEntity)
        Next

        Return entitiesRead.Cast(Of T)
    End Function
End Class

