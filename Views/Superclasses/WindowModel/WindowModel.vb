Public MustInherit Class WindowModel
    Inherits SuperWindow
    Implements IViewModel

    Public Sub SetDataContext(viewModel As Object) Implements IViewModel.SetDataContext
        DataContext = viewModel
    End Sub
End Class
