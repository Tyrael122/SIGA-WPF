Public Class ProfessorHomePage
    Implements IViewModel

    Private Presenter As PresenterProfessorHomePage = New PresenterProfessorHomePage(Me)

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        Throw New NotImplementedException()
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Throw New NotImplementedException()
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        DisciplinasDataGrid.ItemsSource = Presenter.GetDisciplinasCadastradas().DefaultView
    End Sub

    Public Sub VerDisciplina_Click(sender As Object, e As EventArgs)
        Dim button As Button = CType(sender, Button)

        Presenter.ShowDisciplinaPage(button.Tag)
    End Sub

    Public Sub SetDataContext(viewModel As Object) Implements IViewModel.SetDataContext
        DataContext = viewModel
    End Sub
End Class
