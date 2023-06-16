Class PageProvas
    Inherits SuperPage

    Private Presenter As PresenterAlunoProvas = New PresenterAlunoProvas(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub PageProvas_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dataGridProvas.ItemsSource = Presenter.GetProvasFuturas()
    End Sub
End Class
