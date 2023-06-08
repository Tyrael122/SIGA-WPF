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

    Function LoadReferenceFromSelectedRows(dataGrid As DataGrid, idColumnName As String, referenceColumnName As String) As List(Of IDictionary(Of String, String))
        Dim list As New List(Of IDictionary(Of String, String))

        For Each row In dataGrid.Items
            Dim item As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
                        {idColumnName, row(idColumnName)},
                        {referenceColumnName, row(referenceColumnName)}
                    }

            list.Add(item)
        Next

        Return list
    End Function

    Sub LoadImagePickerDialog(imageContainer As Object)
        Dim fileDialog = New OpenFileDialog With {
                    .Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*"
                }
        Dim hasUserClickedOk = fileDialog.ShowDialog() = True
        If hasUserClickedOk Then
            Dim imageBrush = New ImageBrush With {
                .ImageSource = New BitmapImage(New Uri(fileDialog.FileName))
            }

            imageContainer.Background = imageBrush
        End If
    End Sub
End Module
