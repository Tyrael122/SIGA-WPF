Public MustInherit Class DAO
    Implements IDAO

    Private _Login As String
    Public Property Login As String
        Get
            Return _Login
        End Get
        Set(value As String)
            _Login = value
        End Set
    End Property

    Private _Password As String
    Public Property Password As String
        Get
            Return _Password
        End Get
        Set(value As String)
            _Password = value
        End Set
    End Property

    Public Overridable Function GetFieldsToParse() As String() Implements IDAO.GetFieldsToParse
        Return {NameOf(Login), NameOf(Password)}
    End Function

    Public Sub LoadFromDataRow(rowData() As Object) Implements IDAO.LoadFromDataRow
        Dim i As Integer = 1
        For Each field In Me.GetFieldsToParse()
            Me.GetType().GetProperty(field).SetValue(Me, rowData(i))
            i += 1
        Next
    End Sub

    Public Sub LoadFromDictionary(data As IDictionary) Implements IDAO.LoadFromDictionary
        For Each field In Me.GetFieldsToParse()
            If data.Contains(field) Then ' Should we throw an exception if the field is not in the dict?
                Dim valuePassedIn As Object = data.Item(field)
                Me.GetType().GetField(field).SetValue(Me, valuePassedIn)
            Else
                ' TODO: Should only throw exception for required fields
                Throw New ArgumentException("The field " & field & " wasn't found in the dictionary")
            End If
        Next
    End Sub
End Class
