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

        dataGridPresencas.ItemsSource = Nothing
    End Sub

    Private Sub cmbHorario_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbHorario.SelectionChanged
        If cmbHorario.ItemsSource IsNot Nothing Then
            dataGridPresencas.ItemsSource = Presenter.GetAllPresencaAlunosCadastrados()
        End If
    End Sub

    Private Sub btnLancarPresencas_Click(sender As Object, e As RoutedEventArgs) Handles btnLancarPresencas.Click
        Dim presencas = LoadReferenceFromSelectedRows(dataGridPresencas, "Id", "IsPresente")

        Presenter.RegisterPresencas(presencas)

        dataGridPresencas.ItemsSource = Nothing
    End Sub

    Private Sub dataGridPresencas_AutoGeneratingColumn(sender As Object, e As DataGridAutoGeneratingColumnEventArgs) Handles dataGridPresencas.AutoGeneratingColumn
        If e.PropertyName = "IsPresente" Then
            e.Cancel = True
        End If

        If e.PropertyName = "Id" Then
            e.Cancel = True
        End If

        If e.PropertyName = "Foto" Then
            e.Cancel = True
        End If
    End Sub
End Class
