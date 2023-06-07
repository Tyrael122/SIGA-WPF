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
    End Sub

    Private Sub cmbCursos_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbCursos.SelectionChanged
        dataGridDisciplinasAluno.ItemsSource = Presenter.GetDisciplinasCurso().DefaultView
    End Sub
End Class
