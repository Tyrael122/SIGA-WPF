Module EntityParser
    Public Function ParseListOfDict(Of T As IEntity)(data As IEnumerable(Of IDictionary(Of String, String))) As IEnumerable(Of T)
        Dim entitiesRead As New List(Of T)

        For Each dictionary In data
            Dim entityTable = [Enum].Parse(GetType(Table), GetType(T).Name)

            Dim tempEntity As IEntity = BusinessRules.GetNewEntityOf(entityTable)
            tempEntity.LoadFromDictionary(dictionary)

            entitiesRead.Add(tempEntity)
        Next

        Return entitiesRead.Cast(Of T)
    End Function
End Module

