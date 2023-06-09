Class PageCursoFuncionario
    Inherits PageModel

    Private Presenter As New PresenterFuncionarioCurso(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub btnCadastroCurso_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastroCurso.Click
        Dim idsDisciplinasCurso = LoadIdsFromSelectedRows(userControlDisciplinas.DataGrid)

        Presenter.RegisterCurso(idsDisciplinasCurso)
    End Sub

    Private Sub PageCursoFuncionario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        userControlDisciplinas.DataGrid.ItemsSource = Presenter.GetDataViewWithCheckboxColumn("Disciplina", False)
    End Sub
End Class
