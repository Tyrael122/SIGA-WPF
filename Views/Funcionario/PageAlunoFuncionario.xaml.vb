Class PageAlunoFuncionario
    Inherits PageModel

    Private Presenter As New PresenterFuncionarioAluno(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub PageAlunoFuncionario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        cmbCursos.ItemsSource = Presenter.LoadCursosAlunoComboBox()

        dataGridAlunos.ItemsSource = Presenter.GetDataView("Aluno")
    End Sub

    Private Sub cmbCursos_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbCursos.SelectionChanged
        userControlDataGridDisciplinas.ItemsSource = Presenter.GetDisciplinasCurso()
    End Sub

    Private Sub btnCadastrarAluno_Click(sender As Object, e As RoutedEventArgs)
        Dim idsDisciplinasAluno = LoadIdsFromSelectedRows(userControlDataGridDisciplinas.DataGrid)

        Presenter.RegisterAluno(idsDisciplinasAluno)
    End Sub

    Private Sub btnAtualizarAluno_Click(sender As Object, e As RoutedEventArgs)
        Dim idsDisciplinasAluno = LoadIdsFromSelectedRows(userControlDataGridDisciplinas.DataGrid)

        Presenter.UpdateAluno(idsDisciplinasAluno)
        SetBotaoAlunoParaCadastro()
    End Sub


    Private Sub btnImage_Click(sender As Object, e As RoutedEventArgs) Handles btnImage.Click
        Dim backGround As New ImageBrush With {
        .ImageSource = LoadImagePickerDialog()
    }

        btnImage.Background = backGround
    End Sub

    Private Sub btnEditarAluno_Click(sender As Object, e As RoutedEventArgs)
        tabControlAluno.SelectedItem = tabCadastroAluno

        Dim button As Button = CType(sender, Button)

        Presenter.CarregarAlunoParaEdicao(button.Tag)

        SetBotaoAlunoParaEdicao()
    End Sub

    Private Sub SetBotaoAlunoParaEdicao()
        btnCadastrarAluno.Content = "Atualizar aluno"
        RemoveHandler btnCadastrarAluno.Click, AddressOf btnCadastrarAluno_Click
        AddHandler btnCadastrarAluno.Click, AddressOf btnAtualizarAluno_Click
    End Sub

    Private Sub SetBotaoAlunoParaCadastro()
        btnCadastrarAluno.Content = "Cadastrar"
        RemoveHandler btnCadastrarAluno.Click, AddressOf btnAtualizarAluno_Click
        AddHandler btnCadastrarAluno.Click, AddressOf btnCadastrarAluno_Click
    End Sub

    Private Sub btnApagarAluno_Click(sender As Object, e As RoutedEventArgs)
        Dim button As Button = CType(sender, Button)

        Presenter.DeleteAluno(button.Tag)

        dataGridAlunos.ItemsSource = Presenter.GetDataView("Aluno")
    End Sub
End Class
