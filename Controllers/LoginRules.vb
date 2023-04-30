Public Class LoginRules
    Public Shared Function ValidateCredentials(user As String, password As String, userType As UserType) As Boolean
        Dim dataBridge As IDAL = New DAL()
        Dim resultList As IEnumerable(Of IDictionary(Of String, String))

        resultList = dataBridge.
                        SelectAll(userType).
                        Where(Function(dict) dict.Item("Login") = user And dict.Item("Password") = password)

        Return resultList.Count = 1
    End Function
End Class
