Class PageInicioAluno
    Inherits PageModel

    Private Presenter As New PresenterAlunoHomePage(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub PageInicioAluno_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dataGridAvisos.ItemsSource = Presenter.GetDataView("Aviso")
    End Sub

    Private Sub btnVerAviso(sender As Object, e As RoutedEventArgs)
        Presenter.ShowAviso(GetIdFromButton(sender))
    End Sub

    Private Sub dataGridAvisos_AutoGeneratingColumn(sender As Object, e As DataGridAutoGeneratingColumnEventArgs) Handles dataGridAvisos.AutoGeneratingColumn
        If e.PropertyName = "Id" Then
            e.Column.Visibility = Visibility.Collapsed
        End If
    End Sub
End Class
