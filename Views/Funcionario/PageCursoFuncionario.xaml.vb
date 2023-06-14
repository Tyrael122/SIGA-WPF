Class PageCursoFuncionario
    Inherits PageModel

    Private Presenter As New PresenterFuncionarioCurso(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub PageCursoFuncionario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        userControlDisciplinas.ItemsSource = Presenter.GetDataViewWithCheckboxColumn("Disciplina", False)

        dataGridCurso.ItemsSource = Presenter.GetDataView("Curso")

        SetBotaoParaCadastro()
    End Sub

    Private Sub btnCadastroCurso_Click(sender As Object, e As RoutedEventArgs)
        Dim idsDisciplinasCurso = LoadIdsFromSelectedRows(userControlDisciplinas.DataGrid)

        Presenter.RegisterCurso(idsDisciplinasCurso)

        dataGridCurso.ItemsSource = Presenter.GetDataView("Curso")
    End Sub

    Private Sub btnAtualizarCurso_Click(sender As Object, e As RoutedEventArgs)
        Dim idsDisciplinasCurso = LoadIdsFromSelectedRows(userControlDisciplinas.DataGrid)

        Presenter.UpdateCurso(idsDisciplinasCurso)

        SetBotaoParaCadastro()

        userControlDisciplinas.ItemsSource = Presenter.GetDataViewWithCheckboxColumn("Disciplina", False)
        dataGridCurso.ItemsSource = Presenter.GetDataView("Curso")
    End Sub

    Private Sub EditarHorariosCurso_Click(sender As Object, e As RoutedEventArgs)
        Presenter.ShowCursoPage(GetIdFromButton(sender))
    End Sub

    Private Sub ButtonDeletar_Click(sender As Object, e As RoutedEventArgs)
        Presenter.DeleteCurso(GetIdFromButton(sender))

        dataGridCurso.ItemsSource = Presenter.GetDataView("Curso")
    End Sub

    Private Sub ButtonEditar_Click(sender As Object, e As RoutedEventArgs)
        Presenter.CarregarCursoParaEdicao(GetIdFromButton(sender))

        tabControlEditarCadastrar.SelectedItem = tabCadastro

        SetBotaoParaEdicao()

        userControlDisciplinas.ItemsSource = Presenter.GetDisciplinasCurso()
    End Sub

    Private Sub SetBotaoParaEdicao()
        userControlCadastrar.btnCadastrar.Content = "Atualizar"

        RemoveHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnCadastroCurso_Click
        AddHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnAtualizarCurso_Click
    End Sub

    Private Sub SetBotaoParaCadastro()
        userControlCadastrar.btnCadastrar.Content = "Cadastrar"

        RemoveHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnAtualizarCurso_Click
        AddHandler userControlCadastrar.btnCadastrar.Click, AddressOf btnCadastroCurso_Click
    End Sub
End Class
