Class PageSolitacacaoFuncionario
    Inherits SuperPage

    Private Presenter As PresenterFuncionarioSolicitacao = New PresenterFuncionarioSolicitacao(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub
    Private Sub btnAnexarDocumento(sender As Object, e As RoutedEventArgs)
        Dim filePath = OpenFileDialog()

        If filePath IsNot Nothing Then
            Presenter.AnexarDocumento(filePath, GetIdFromButton(sender))
            dataGridSolicitacoes.ItemsSource = Presenter.GetSolicitacoes()
        End If
    End Sub

    Private Sub PageSolitacacaoFuncionario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dataGridSolicitacoes.ItemsSource = Presenter.GetSolicitacoes()
    End Sub

    Private Sub dataGridSolicitacoes_AutoGeneratingColumn(sender As Object, e As DataGridAutoGeneratingColumnEventArgs) Handles dataGridSolicitacoes.AutoGeneratingColumn
        If e.PropertyName = "Id" Then
            e.Column.Visibility = Visibility.Collapsed
        End If
    End Sub
End Class
