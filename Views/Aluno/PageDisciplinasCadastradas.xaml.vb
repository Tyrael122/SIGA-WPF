Class PageDisciplinasCadastradas

    Private Presenter As New PresenterAluno(Me)
    Private Sub PageDisciplinasCadastradas_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DisciplinasAlunoDataGrid.ItemsSource = Presenter.GetDisciplinasCadastradas().DefaultView
    End Sub
End Class
