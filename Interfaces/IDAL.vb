Public Interface IDAL
    Function SelectFields(table As Table, ParamArray fieldsToSelect As String()) As List(Of IDictionary(Of String, String))
    Function SelectAll(table As Table) As List(Of IDictionary(Of String, String))
    Function Save(data As IDictionary, table As Table) As Boolean
    Function SaveWithOutput(data As IDictionary, table As Table) As List(Of IDictionary(Of String, String))
    Sub Edit(data As IDictionary, table As Table)
    Sub Delete(id As String, whereField As String, table As Table)
    Sub Delete(id As String, table As Table)
End Interface
