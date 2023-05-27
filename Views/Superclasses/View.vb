Public MustInherit Class View
    Inherits Window
    Implements IView

    Public MustOverride Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
    Public MustOverride Sub DisplayError() Implements IView.DisplayError

    Public Sub CloseView() Implements IView.CloseView
        Close()
    End Sub

    Protected Function LoadIdsFromSelectedRows(dataGrid As DataGrid) As List(Of String)
        Dim idSelectedRows As New List(Of String)

        For Each row In dataGrid.Items
            If row("IsChecked") Then
                idSelectedRows.Add(row("Id"))
            End If
        Next

        Return idSelectedRows
    End Function
End Class
