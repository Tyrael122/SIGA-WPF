Class PagePresenca
    Inherits PageModel

    Private Presenter As New PresenterProfessorPresenca(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub PagePresenca_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        cmbDiaAula.ItemsSource = Presenter.LoadDiaAulaComboBox()
    End Sub

    Private Sub cmbDiaAula_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbDiaAula.SelectionChanged
        cmbHorario.ItemsSource = Presenter.LoadHorariosComboBox()
    End Sub

    Private Sub cmbHorario_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbHorario.SelectionChanged
        dataGridPresencas.ItemsSource = Presenter.GetAllPresencaAlunosCadastrados()
    End Sub

    Private Sub btnLancarPresencas_Click(sender As Object, e As RoutedEventArgs) Handles btnLancarPresencas.Click
        Dim presencas = LoadReferenceFromSelectedRows(dataGridPresencas, "Id", "IsPresente")

        Presenter.RegisterPresencas(presencas)
    End Sub
End Class
