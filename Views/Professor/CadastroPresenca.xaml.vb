Class CadastroPresenca
    Inherits PageModel

    Private Presenter As New PresenterCadastroPresenca(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        cmbDiaAula.ItemsSource = Presenter.LoadDiaAulaComoBox()
    End Sub

    Private Sub cmbDiaAula_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbDiaAula.SelectionChanged
        Dim diaAula = cmbDiaAula.SelectedItem
        cmbHorario.ItemsSource = Presenter.LoadHorariosComboBox(diaAula)
    End Sub

    Private Sub cmbHorario_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbHorario.SelectionChanged
        PresencaAlunosDataGrid.ItemsSource = Presenter.GetAllPresencaAlunosCadastrados().DefaultView
    End Sub

    Private Sub cmbLancarPresencas_Click(sender As Object, e As RoutedEventArgs) Handles cmbLancarPresencas.Click
        Dim presencas = LoadReferenceFromSelectedRows(PresencaAlunosDataGrid, "Id", "IsPresente")

        Presenter.RegisterPresencas(presencas)
    End Sub
End Class
