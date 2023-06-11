Class PageDisciplinasFuncionario
    Inherits PageModel

    Private Presenter As PresenterFuncionarioDisciplina = New PresenterFuncionarioDisciplina(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub btnCadastrarDisciplina_Click(sender As Object, e As RoutedEventArgs)
        Presenter.RegisterDisciplina()
    End Sub

    Private Sub PageDisciplinasFuncionario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dataGridDisciplinas.ItemsSource = Presenter.GetDataView("Disciplina")
    End Sub

    Private Sub btnAtualizarDisciplina_Click(sender As Object, e As RoutedEventArgs)
        Presenter.UpdateDisciplina()
        SetBotaoParaCadastro()

        dataGridDisciplinas.ItemsSource = Presenter.GetDataView("Disciplina")
    End Sub

    Private Sub ButtonDeletar_Click(sender As Object, e As RoutedEventArgs)
        Presenter.DeleteDisciplina(GetIdFromButton(sender))

        dataGridDisciplinas.ItemsSource = Presenter.GetDataView("Disciplina")
    End Sub

    Private Sub ButtonEditar_Click(sender As Object, e As RoutedEventArgs)
        Presenter.CarregarDisciplinaParaEdicao(GetIdFromButton(sender))

        tabControlEditarCadastrar.SelectedItem = tabCadastro

        SetBotaoParaEdicao()
    End Sub

    Private Sub SetBotaoParaEdicao()
        userControlCadastrar.btnCadastrar.Content = "Atualizar"

        RemoveHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnCadastrarDisciplina_Click
        AddHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnAtualizarDisciplina_Click
    End Sub

    Private Sub SetBotaoParaCadastro()
        userControlCadastrar.btnCadastrar.Content = "Cadastrar"

        RemoveHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnAtualizarDisciplina_Click
        AddHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnCadastrarDisciplina_Click
    End Sub
End Class
