Class TelaInicialProfessor
    Inherits WindowModel

    Private Presenter As New PresenterProfessorHomePage(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub TelaInicialProfessor_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
    End Sub
End Class
