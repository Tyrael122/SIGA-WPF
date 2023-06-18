Imports System.Data

Public Class DataGridDisciplina
    Public Property DataGrid As DataGrid
        Get
            Return MainDataGrid
        End Get
        Set(value As DataGrid)
            MainDataGrid = value
        End Set
    End Property

    Public Property ItemsSource As DataView
        Get
            Return MainDataGrid.ItemsSource
        End Get
        Set(value As DataView)
            MainDataGrid.ItemsSource = value
        End Set
    End Property

    Private Sub MainDataGrid_AutoGeneratingColumn(sender As Object, e As DataGridAutoGeneratingColumnEventArgs) Handles MainDataGrid.AutoGeneratingColumn
        If e.PropertyName = "IsChecked" Then
            e.Cancel = True
        End If
    End Sub
End Class
