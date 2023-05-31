Class PageDisciplinasCadastradas
    Implements IView

    Private Presenter As New PresenterAluno(Me)

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        Throw New NotImplementedException()
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Throw New NotImplementedException()
    End Sub

    Private Sub PageDisciplinasCadastradas_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DisciplinasAlunoDataGrid.ItemsSource = Presenter.GetDisciplinasCadastradas().DefaultView
    End Sub
End Class
