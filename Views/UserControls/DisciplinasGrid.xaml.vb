Public Class DisciplinasGrid
    Property GridItemsSource As IEnumerable
        Get
            Return itemControlDisciplinas.ItemsSource
        End Get
        Set(value As IEnumerable)
            itemControlDisciplinas.ItemsSource = value
        End Set
    End Property
End Class
