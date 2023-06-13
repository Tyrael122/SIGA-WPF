Class PageFaltas
    Inherits SuperPage

    Private Presenter As New PresenterPresencasAluno(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub PageFaltas_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dataGridPresencas.ItemsSource = Nothing
        dataGridPresencas.ItemsSource = Presenter.GetPresencasAluno()
    End Sub
End Class
