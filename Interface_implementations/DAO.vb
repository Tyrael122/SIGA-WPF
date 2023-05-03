Public MustInherit Class DAO
    Implements IDAO

    Protected Overridable Function GetFieldsToParse() As String()
        Dim fields As New List(Of String)

        For Each field In Me.GetType().GetProperties()
            fields.Add(field.Name)
        Next

        Return fields.ToArray()
    End Function

    Public Sub LoadFromDictionary(data As IDictionary) Implements IDAO.LoadFromDictionary
        For Each field In Me.GetFieldsToParse()
            If data.Contains(field) Then ' Should we throw an exception if the field is not in the dict?
                Dim valuePassedIn As Object = data.Item(field)
                If TypeOf valuePassedIn IsNot DBNull Then
                    Me.GetType().GetProperty(field).SetValue(Me, valuePassedIn)
                End If
            Else
                ' TODO: Should only throw exception for required fields
                Throw New ArgumentException("The field " & field & " wasn't found in the dictionary")
            End If
        Next
    End Sub
End Class
