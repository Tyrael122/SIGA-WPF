Public Interface IDAL
    Function SelectAll(table As Table) As List(Of IDictionary(Of String, String))
    Function SelectWhere(table As Table, filterFunc As Func(Of IDictionary(Of String, String), Boolean)) As List(Of IDictionary(Of String, String))
    Function ReadAllEntities(table As Table) As List(Of IDAO)
    Sub Save(data As IDictionary, table As Table)
    Sub Edit(entity As IDAO, table As UserType)
    Sub Delete(entity As IDAO)
End Interface
