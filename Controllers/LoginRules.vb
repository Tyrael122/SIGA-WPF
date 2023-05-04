Public Class LoginRules
    Public Shared Function GetUserByCredentials(username As String, password As String, table As Table) As IEnumerable(Of IDictionary(Of String, String))
        Dim dataBridge As IDAL = New DAL()                                                                      ' Maybe later we can define a custom class returned by the businessRules
        Dim resultList As IEnumerable(Of IDictionary(Of String, String))

        resultList = dataBridge.
                        SelectAll(table).
                        Where(Function(dict) dict.Item("Login") = username And dict.Item("Password") = password)

        Return resultList
    End Function
End Class
