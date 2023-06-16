Class PageProfessorFuncionario
    Inherits PageModel

    Private Presenter As New PresenterFuncionarioProfessor(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub btnCadastrarProfessor_Click(sender As Object, e As RoutedEventArgs)
        Dim idsDisciplinasProfessor = LoadIdsFromSelectedRows(userControlDisciplinas.DataGrid)

        Presenter.RegisterProfessor(idsDisciplinasProfessor)
        userControlDisciplinas.ItemsSource = Presenter.GetDataViewWithCheckboxColumn("Disciplina", False)
        dataGridProfessor.ItemsSource = Presenter.GetDataView("Professor")
    End Sub

    Private Sub PageProfessorFuncionario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        userControlDisciplinas.ItemsSource = Presenter.GetDataViewWithCheckboxColumn("Disciplina", False)

        dataGridProfessor.ItemsSource = Presenter.GetDataView("Professor")
        SetBotaoParaCadastro()
    End Sub


    Private Sub btnAtualizarAluno_Click(sender As Object, e As RoutedEventArgs)
        Dim idsDisciplinasProfessor = LoadIdsFromSelectedRows(userControlDisciplinas.DataGrid)

        Presenter.UpdateProfessor(idsDisciplinasProfessor)
        SetBotaoParaCadastro()

        dataGridProfessor.ItemsSource = Presenter.GetDataView("Professor")
        userControlDisciplinas.ItemsSource = Presenter.GetDataViewWithCheckboxColumn("Disciplina", False)
    End Sub

    Private Sub ButtonDeletar_Click(sender As Object, e As RoutedEventArgs)
        Presenter.DeleteProfessor(GetIdFromButton(sender))

        dataGridProfessor.ItemsSource = Presenter.GetDataView("Professor")
    End Sub

    Private Sub ButtonEditar_Click(sender As Object, e As RoutedEventArgs)
        Presenter.CarregarProfessorParaEdicao(GetIdFromButton(sender))

        tabControlEditarCadastrar.SelectedItem = tabCadastro

        SetBotaoParaEdicao()

        userControlDisciplinas.ItemsSource = Presenter.GetDisciplinasProfessor()
    End Sub

    Private Sub SetBotaoParaEdicao()
        userControlCadastrar.btnCadastrar.Content = "Atualizar"

        RemoveHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnCadastrarProfessor_Click
        AddHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnAtualizarAluno_Click
    End Sub

    Private Sub SetBotaoParaCadastro()
        userControlCadastrar.btnCadastrar.Content = "Cadastrar"

        RemoveHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnAtualizarAluno_Click
        AddHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnCadastrarProfessor_Click
    End Sub
End Class
