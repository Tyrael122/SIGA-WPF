Public MustInherit Class SuperWindow
    Inherits Window
    Implements IView

    Public MustOverride Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
    Public MustOverride Sub DisplayError() Implements IView.DisplayError

    Public Sub CloseView() Implements IView.CloseView
        Close()
    End Sub

    Protected Function LoadIdsFromSelectedRows(dataGrid As DataGrid) As List(Of String)
        Return ViewUtils.LoadIdsFromSelectedRows(dataGrid)
    End Function

    Protected Function LoadReferenceFromSelectedRows(dataGrid As DataGrid, idColumnName As String, referenceColumnName As String) As List(Of IDictionary(Of String, String))
        Return ViewUtils.LoadReferenceFromSelectedRows(dataGrid, idColumnName, referenceColumnName)
    End Function

    Protected Sub LoadImagePickerDialog()
        ViewUtils.LoadImagePickerDialog()
    End Sub
End Class
