Class PageDisciplinasFuncionario
    Inherits PageModel

    Private Presenter As PresenterFuncionarioDisciplina = New PresenterFuncionarioDisciplina(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub btnCadastrarDisciplina_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarDisciplina.Click
        Presenter.RegisterDisciplina()
    End Sub

    Private Sub PageDisciplinasFuncionario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dataGridDisciplinas.ItemsSource = Presenter.GetDataView("Disciplina")
    End Sub
End Class
