Public Class LoginRules
    Public Shared Function GetUserByCredentials(username As String, password As String, table As Table) As IEnumerable(Of IDictionary(Of String, String))
        Dim dataBridge As IDAL = New DAL()

        Return dataBridge.
                SelectAll(table).
                Where(Function(dict) dict.Item("Login") = username And dict.Item("Password") = password)
    End Function
End Class
