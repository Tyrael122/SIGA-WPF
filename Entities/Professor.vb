Public Class Professor
    Implements IDAO

    Public login As String
    Public password As String

    Public Sub LoadFromDataRow(row() As Object) Implements IDAO.LoadFromDataRow
        Throw New NotImplementedException()
    End Sub

    Private Function GetFieldsToParse() As String() Implements IDAO.GetFieldsToParse
        Throw New NotImplementedException()
    End Function
End Class
