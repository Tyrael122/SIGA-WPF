Public MustInherit Class Entity
    Implements IEntity

    Private _Id As String
    Public Property Id As String
        Get
            Return _Id
        End Get
        Set(value As String)
            _Id = value
        End Set
    End Property

    Protected Overridable Function GetFieldsToParse() As String()
        Dim fields As New List(Of String)

        For Each field In Me.GetType().GetProperties()
            fields.Add(field.Name)
        Next

        Return fields.ToArray()
    End Function

    Public Sub LoadFromDictionary(data As IDictionary) Implements IEntity.LoadFromDictionary
        For Each field In Me.GetFieldsToParse()
            If data.Contains(field) Then
                Dim valuePassedIn As Object = data.Item(field)

                If TypeOf valuePassedIn IsNot DBNull Then
                    Me.GetType().GetProperty(field).SetValue(Me, valuePassedIn)
                End If
            Else
                Throw New ArgumentException("The field " & field & " wasn't found in the dictionary")
            End If
        Next
    End Sub
End Class
