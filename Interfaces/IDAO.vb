Public Interface IDAO
    Function GetFieldsToParse() As String()
    Sub LoadFromDataRow(row() As Object)
    Sub LoadFromDictionary(data As IDictionary)
End Interface
