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
        End If
    End Sub

    Private Sub PageSolitacacaoFuncionario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dataGridSolicitacoes.ItemsSource = Presenter.GetDataView("Solicitacao")
    End Sub
End Class
