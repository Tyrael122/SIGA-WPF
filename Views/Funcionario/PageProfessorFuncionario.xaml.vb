Class PageProfessorFuncionario
    Inherits PageModel

    Private Presenter As New PresenterFuncionarioProfessor(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub btnCadastrarProfessor_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarProfessor.Click
        Dim idsDisciplinasProfessor = LoadIdsFromSelectedRows(dataGridDisciplinasProfessor)

        Presenter.RegisterProfessor(idsDisciplinasProfessor)
    End Sub

    Private Sub PageProfessorFuncionario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dataGridDisciplinasProfessor.ItemsSource = Presenter.GetDataViewWithCheckboxColumn("Disciplina", False)
    End Sub

    Private Sub btnImage_Click(sender As Object, e As RoutedEventArgs) Handles btnImage.Click
        LoadImagePickerDialog(btnImage)
    End Sub
End Class
