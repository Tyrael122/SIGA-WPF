Public MustInherit Class SuperPage
    Inherits Page
    Implements IView

    Public MustOverride Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
    Public MustOverride Sub DisplayError() Implements IView.DisplayError

    Public Sub CloseView() Implements IView.CloseView
        Throw New InvalidOperationException("Can't close a Page.")
    End Sub

    Protected Function LoadIdsFromSelectedRows(dataGrid As DataGrid) As List(Of String)
        Return ViewUtils.LoadIdsFromSelectedRows(dataGrid)
    End Function

    Protected Function LoadReferenceFromSelectedRows(dataGrid As DataGrid, idColumnName As String, referenceColumnName As String) As List(Of IDictionary(Of String, Object))
        Return ViewUtils.LoadReferenceFromSelectedRows(dataGrid, idColumnName, referenceColumnName)
    End Function

    Protected Function LoadImagePickerDialog()
        Return ViewUtils.LoadImagePickerDialog()
    End Function
End Class
