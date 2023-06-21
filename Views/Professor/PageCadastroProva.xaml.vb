Class PageCadastroProva
    Inherits PageModel

    Private Presenter As New PresenterProfessorProva(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        originalContent = lblInfo.Content

        lblInfo.Content = infoMessage

        timer.Start()
    End Sub

    Private Sub Timer_Tick(sender As Object, e As EventArgs)
        lblInfo.Content = originalContent

        timer.Stop()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub btnCadastrarProva_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarProva.Click
        Presenter.RegisterProva()
    End Sub

    Private Sub PageCadastroProva_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        AddHandler timer.Tick, AddressOf Timer_Tick
    End Sub
End Class
