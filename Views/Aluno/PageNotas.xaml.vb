Class PageNotas
    Inherits SuperPage

    Public Presenter As New PresenterNotasAluno(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub PageNotas_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dataGridNotas.ItemsSource = Presenter.GetNotasAluno()
    End Sub
End Class
