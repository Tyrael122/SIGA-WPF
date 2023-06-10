Class PageCadastroProva
    Inherits PageModel

    Private Presenter As New PresenterProfessorProva(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub btnCadastrarProva_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarProva.Click
        Presenter.RegisterProva()
    End Sub
End Class
