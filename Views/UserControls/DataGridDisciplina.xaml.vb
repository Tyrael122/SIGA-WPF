Public Class DataGridDisciplina
    Public Property DataGrid As DataGrid
        Get
            Return MainDataGrid
        End Get
        Set(value As DataGrid)
            MainDataGrid = value
        End Set
    End Property
End Class
