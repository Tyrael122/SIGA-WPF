Imports Microsoft.Win32

Module ViewUtils
    Function LoadIdsFromSelectedRows(dataGrid As DataGrid) As List(Of String)
        Dim idSelectedRows As New List(Of String)

        For Each row In dataGrid.Items
            If row("IsChecked") Then
                idSelectedRows.Add(row("Id"))
            End If
        Next

        Return idSelectedRows
    End Function

    Function LoadReferenceFromSelectedRows(dataGrid As DataGrid, idColumnName As String, referenceColumnName As String) As List(Of IDictionary(Of String, Object))
        Dim list As New List(Of IDictionary(Of String, Object))

        For Each row In dataGrid.Items
            Dim item As IDictionary(Of String, Object) = New Dictionary(Of String, Object) From {
                        {idColumnName, row(idColumnName)},
                        {referenceColumnName, row(referenceColumnName)}
                    }

            list.Add(item)
        Next

        Return list
    End Function

    Function LoadImagePickerDialog() As ImageSource
        Dim fileDialog = New OpenFileDialog With {
                    .Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*"
                }
        Dim hasUserClickedOk = fileDialog.ShowDialog() = True
        If hasUserClickedOk Then
            Return New BitmapImage(New Uri(fileDialog.FileName))
        End If

        Return Nothing
    End Function

    Function OpenFileDialog() As String
        Dim fileDialog = New OpenFileDialog()
        Dim hasUserClickedOk = fileDialog.ShowDialog() = True
        If hasUserClickedOk Then
            Return fileDialog.FileName
        End If

        Return Nothing
    End Function

    Friend Function GetIdFromButton(sender As Object) As Object
        Dim button As Button = CType(sender, Button)

        Return button.Tag
    End Function
End Module
