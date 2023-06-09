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
End Class
