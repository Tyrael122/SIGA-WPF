Public Interface IDAO
    Function GetFieldsToParse() As String()
    Sub LoadFromDataRow(data() As Object)
    Sub LoadFromDictionary(data As IDictionary)
End Interface
