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

    Private Sub btnCadastrarAluno_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarAluno.Click
        Dim idsDisciplinasAluno = LoadIdsFromSelectedRows(userControlDataGridDisciplinas.DataGrid)

        Presenter.RegisterAluno(idsDisciplinasAluno)
    End Sub

    Private Sub btnImage_Click(sender As Object, e As RoutedEventArgs) Handles btnImage.Click
        Dim backGround As New ImageBrush With {
            .ImageSource = LoadImagePickerDialog()
        }

        btnImage.Background = backGround
    End Sub

    Private Sub btnEditarAluno_Click(sender As Object, e As RoutedEventArgs) Handles btnEditarAluno.Click

    End Sub
End Class
