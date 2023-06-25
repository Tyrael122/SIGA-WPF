Public Class AlunoHomePage
    Inherits WindowModel

    Private Presenter As New PresenterAlunoHomePage(Me)

    Private Sub AlunoHomePage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        mainFrame.Content = New PageInicioAluno()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        mainFrame.Content = New PageInicioAluno()
    End Sub

    Private Sub btnPlanoEnsino_Click(sender As Object, e As RoutedEventArgs) Handles btnPlanoEnsino.Click
        mainFrame.Content = New PageProvas()
    End Sub

    Private Sub btnConsultarFaltas_Click(sender As Object, e As RoutedEventArgs) Handles btnConsultarFaltas.Click
        mainFrame.Content = New PageFaltas()
    End Sub

    Private Sub btnSolicitacoes_Click(sender As Object, e As RoutedEventArgs) Handles btnSolicitacoes.Click
        mainFrame.Content = New PageSolicitacoes()
    End Sub

    Private Sub btnNotas_Click(sender As Object, e As RoutedEventArgs) Handles btnNotas.Click
        mainFrame.Content = New PageNotas()
    End Sub

    Private Sub bntSair_Click(sender As Object, e As RoutedEventArgs) Handles bntSair.Click
        Dim login As New MainWindow()
        login.Show()
        Close()
    End Sub

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub
End Class
