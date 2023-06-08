Imports Microsoft.Win32

Class PageAlunoFuncionario
    Inherits PageModel

    Private Presenter As PresenterFuncionarioAluno = New PresenterFuncionarioAluno(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub PageAlunoFuncionario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        cmbCursos.ItemsSource = Presenter.LoadCursosAlunoComboBox()

        dataGridAlunos.ItemsSource = Presenter.GetDataTable("Aluno").DefaultView
    End Sub

    Private Sub cmbCursos_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbCursos.SelectionChanged
        dataGridDisciplinasAluno.ItemsSource = Presenter.GetDisciplinasCurso().DefaultView
    End Sub

    Private Sub btnCadastrarAluno_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarAluno.Click
        Dim idsDisciplinasAluno = LoadIdsFromSelectedRows(dataGridDisciplinasAluno)

        Presenter.RegisterAluno(idsDisciplinasAluno)
    End Sub

    Private Sub btnImage_Click(sender As Object, e As RoutedEventArgs) Handles btnImage.Click
        LoadImagePickerDialog(btnImage)
    End Sub

    Protected Sub LoadImagePickerDialog(imageContainer As Object)
        Dim fileDialog = New OpenFileDialog With {
                    .Filter = "Image files (*.png;*.jpeg)|*.png;*.jpeg|All files (*.*)|*.*"
                }
        Dim hasUserClickedOk = fileDialog.ShowDialog() = True
        If hasUserClickedOk Then
            Dim imageBrush = New ImageBrush With {
                .ImageSource = New BitmapImage(New Uri(fileDialog.FileName))
            }

            imageContainer.Background = imageBrush
        End If
    End Sub
End Class
