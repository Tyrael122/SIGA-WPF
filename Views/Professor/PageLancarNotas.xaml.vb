Class PageLancarNotas
    Inherits PageModel

    Private Presenter As New PresenterProfessorNotas(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        originalContent = lblInfo.Content

        lblInfo.Content = infoMessage

        timer.Start()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub PageLancarNotas_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        cmbProva.ItemsSource = Presenter.LoadProvasComboBox()

        AddHandler timer.Tick, AddressOf Timer_Tick
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        lblInfo.Content = originalContent

        timer.Stop()
    End Sub

    Private Sub cmbProva_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbProva.SelectionChanged
        dataGridNotasAluno.ItemsSource = Presenter.GetAllNotasAlunosCadastrados()
    End Sub

    Private Sub btnLancarNotas_Click(sender As Object, e As RoutedEventArgs) Handles btnLancarNotas.Click
        Dim notas = LoadReferenceFromSelectedRows(dataGridNotasAluno, "Id", "Nota")
        Presenter.RegisterNotas(notas)
    End Sub

    Private Sub dataGridNotasAluno_AutoGeneratingColumn(sender As Object, e As DataGridAutoGeneratingColumnEventArgs) Handles dataGridNotasAluno.AutoGeneratingColumn
        If e.PropertyName = "Foto" Then
            e.Cancel = True
        End If
    End Sub
End Class
