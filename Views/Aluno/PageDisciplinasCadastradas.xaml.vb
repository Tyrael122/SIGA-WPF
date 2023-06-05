Class PageDisciplinasCadastradas
    Implements IView

    Private Presenter As New PresenterAlunoDisciplina(Me)

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

    Private Sub PageDisciplinasCadastradas_Loaded_1(sender As Object, e As RoutedEventArgs) Handles MyBase.Loaded
        lblTitulo.Content = "Minhas Disciplinas"
    End Sub

    Private Sub btnProvas_Click(sender As Object, e As RoutedEventArgs) Handles btnProvas.Click
        lblTitulo.Content = "Provas"
        Frame.Content = New PageProvas()
    End Sub

    Private Sub btnHorario_Click(sender As Object, e As RoutedEventArgs) Handles btnHorario.Click
        lblTitulo.Content = "Horário"
        Frame.Content = New PageHorario()
    End Sub

    Private Sub btnDisciplinas_Click(sender As Object, e As RoutedEventArgs) Handles btnDisciplinas.Click
        lblTitulo.Content = "Minhas Disciplinas"
        Frame.Content = New PageDisciplinas()
    End Sub
End Class
