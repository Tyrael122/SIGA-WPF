Public Interface IDAO
    Function GetFieldsToParse() As String()
    Sub LoadFromDataRow(row() As Object)
End Interface
