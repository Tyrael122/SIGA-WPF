Public Interface IDAL
    Function SelectFields(table As Table, ParamArray fieldsToSelect As String()) As IEnumerable(Of IDictionary(Of String, Object))
    Function SelectAll(table As Table) As IEnumerable(Of IDictionary(Of String, Object))
    Function Save(data As IDictionary(Of String, Object), table As Table) As Boolean
    Function SaveWithOutput(data As IDictionary(Of String, Object), table As Table) As IEnumerable(Of IDictionary(Of String, Object))
    Sub Update(idEntity As String, data As IDictionary(Of String, Object), table As Table)
    Sub Delete(idEntity As String, whereField As String, table As Table)
    Sub Delete(idEntity As String, table As Table)
End Interface
