Public Interface IDAL
    Sub Save(data As IDictionary, table As UserType)
    Function ReadAll(table As UserType) As List(Of IDAO)
    Function ReadByCredentials(table As UserType, credentials As Credentials) As List(Of IDAO)
    Sub Delete(entity As IDAO)
    Sub Edit(entity As IDAO, table As UserType)
End Interface
