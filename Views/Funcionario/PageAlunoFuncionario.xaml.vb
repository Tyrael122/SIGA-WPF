﻿Class PageAlunoFuncionario
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

        SetBotaoAlunoParaCadastro()
    End Sub

    Private Sub cmbCursos_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbCursos.SelectionChanged
        userControlDataGridDisciplinas.ItemsSource = Presenter.GetDisciplinas()
    End Sub

    Private Sub btnCadastrarAluno_Click(sender As Object, e As RoutedEventArgs)
        Dim idsDisciplinasAluno = LoadIdsFromSelectedRows(userControlDataGridDisciplinas.DataGrid)

        Presenter.RegisterAluno(idsDisciplinasAluno)

        dataGridAlunos.ItemsSource = Presenter.GetDataView("Aluno")
    End Sub

    Private Sub btnAtualizarAluno_Click(sender As Object, e As RoutedEventArgs)
        Dim idsDisciplinasAluno = LoadIdsFromSelectedRows(userControlDataGridDisciplinas.DataGrid)

        Presenter.UpdateAluno(idsDisciplinasAluno)
        SetBotaoAlunoParaCadastro()

        dataGridAlunos.ItemsSource = Presenter.GetDataView("Aluno")
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
        userControlAluno.btnCadastrar.Content = "Atualizar aluno"
        RemoveHandler userControlAluno.btnCadastrar.Click, AddressOf btnCadastrarAluno_Click
        AddHandler userControlAluno.btnCadastrar.Click, AddressOf btnAtualizarAluno_Click
    End Sub

    Private Sub SetBotaoAlunoParaCadastro()
        userControlAluno.btnCadastrar.Content = "Cadastrar"
        RemoveHandler userControlAluno.btnCadastrar.Click, AddressOf btnAtualizarAluno_Click
        AddHandler userControlAluno.btnCadastrar.Click, AddressOf btnCadastrarAluno_Click
    End Sub

    Private Sub btnApagarAluno_Click(sender As Object, e As RoutedEventArgs)
        Dim button As Button = CType(sender, Button)

        Presenter.DeleteAluno(button.Tag)

        dataGridAlunos.ItemsSource = Presenter.GetDataView("Aluno")
    End Sub
End Class
