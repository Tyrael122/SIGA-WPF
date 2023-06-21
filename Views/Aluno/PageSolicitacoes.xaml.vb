Class PageSolicitacoes
    Inherits PageModel

    Private Presenter As PresenterAlunoSolicitacoes = New PresenterAlunoSolicitacoes(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        MsgBox(infoMessage)
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub btnSolicitar_Click(sender As Object, e As RoutedEventArgs) Handles btnSolicitar.Click
        Presenter.RegisterSolicitacao()

        dataGridSolicitacoes.ItemsSource = Presenter.GetAllSolitacacoesAluno()
    End Sub

    Private Sub dataGridSolicitacoes_Loaded(sender As Object, e As RoutedEventArgs) Handles dataGridSolicitacoes.Loaded
        dataGridSolicitacoes.ItemsSource = Presenter.GetAllSolitacacoesAluno()
    End Sub

    Private Sub btnDownload(sender As Object, e As RoutedEventArgs)
        Presenter.DownloadDocumento(GetIdFromButton(sender))
    End Sub

    Private Sub dataGridSolicitacoes_AutoGeneratingColumn(sender As Object, e As DataGridAutoGeneratingColumnEventArgs) Handles dataGridSolicitacoes.AutoGeneratingColumn
        If e.PropertyName = "Id" Then
            e.Column.Visibility = Visibility.Collapsed
        End If
    End Sub
End Class
