Public Interface IDAL
    Sub Save(data As IDictionary, table As Table)
    Function ReadAll(table As Table) As List(Of IDAO)
    Function ReadByCredentials(table As UserType, credentials As Credentials) As List(Of IDAO)
    Sub Delete(entity As IDAO)
    Sub Edit(entity As IDAO, table As UserType)
    Sub CloseConnection()
End Interface
