Public Class FuncionarioAdministrativo
    Implements IDAO

    Public login As String
    Public password As String

    Public Function GetFieldsToParse() As String() Implements IDAO.GetFieldsToParse
        Return {NameOf(login), NameOf(password)}
    End Function

    Public Sub LoadFromDataRow(rowData() As Object) Implements IDAO.LoadFromDataRow
        Dim i As Integer = 1
        For Each field In Me.GetFieldsToParse()
            Me.GetType().GetField(field).SetValue(Me, rowData(i))
            i += 1
        Next
    End Sub
End Class
