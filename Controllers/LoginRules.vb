Public Class LoginRules
    Public Shared Function ValidateCredentials(user As String, password As String, userType As UserType) As Boolean
        Dim dataBridge As IDAL = New DAL()
        Dim resultList As IEnumerable(Of IDAO)

        resultList = dataBridge.ReadByCredentials(userType, New Credentials(user, password))
        dataBridge.CloseConnection()

        If resultList.Count <> 1 Then
            Return False
        End If

        Return True
    End Function
End Class
