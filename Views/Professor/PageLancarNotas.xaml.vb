Class PageLancarNotas
    Inherits PageModel

    Private Presenter As New PresenterProfessorNotas(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub PageLancarNotas_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        cmbProva.ItemsSource = Presenter.LoadProvasComboBox()
    End Sub

    Private Sub cmbProva_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbProva.SelectionChanged
        dataGridNotasAluno.ItemsSource = Presenter.GetAllNotasAlunosCadastrados()
    End Sub

    Private Sub btnLancarNotas_Click(sender As Object, e As RoutedEventArgs) Handles btnLancarNotas.Click
        Dim notas = LoadReferenceFromSelectedRows(dataGridNotasAluno, "Id", "Nota")
        Presenter.RegisterNotas(notas)
    End Sub
End Class
