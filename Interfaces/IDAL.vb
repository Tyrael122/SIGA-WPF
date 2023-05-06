Public Interface IDAL
    Function SelectFields(table As Table, ParamArray fieldsToSelect As String()) As List(Of IDictionary(Of String, String))
    Function SelectAll(table As Table) As List(Of IDictionary(Of String, String))
    Function ReadAllEntities(Of T As IDAO)() As List(Of T)
    Function Save(data As IDictionary, table As Table) As Boolean
    Function SaveWithOutput(data As IDictionary, table As Table) As List(Of IDictionary(Of String, String))
    Sub Edit(entity As IDAO, table As Table)
    Sub Delete(entity As IDAO)
End Interface
