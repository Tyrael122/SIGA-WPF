Class PageInicioFuncionario
    Inherits PageModel

    Private Presenter As New PresenterFuncionarioAviso(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub btnCadastrarAviso_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarAviso.Click
        Presenter.RegisterAviso()

        dataGridAvisos.ItemsSource = Presenter.GetDataView("Aviso")
    End Sub

    Private Sub ButtonDeletar_Click(sender As Object, e As RoutedEventArgs)
        Presenter.DeleteAviso(GetIdFromButton(sender))

        dataGridAvisos.ItemsSource = Presenter.GetDataView("Aviso")
    End Sub

    Private Sub PageInicioFuncionario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dataGridAvisos.ItemsSource = Presenter.GetDataView("Aviso")
    End Sub
End Class
