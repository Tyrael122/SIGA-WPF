Public Class TelaFuncionario
    Inherits WindowModel

    Private Presenter As PresenterFuncionarioHomePage = New PresenterFuncionarioHomePage(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub WindowModel_Loaded(sender As Object, e As RoutedEventArgs)
        mainFrame.Content = New PageInicioFuncionario()
    End Sub

    Private Sub btnSolicitacoes_Click(sender As Object, e As RoutedEventArgs) Handles btnSolicitacoes.Click
        mainFrame.Content = New PageSolitacacaoFuncionario()
    End Sub

    Private Sub btnAluno_Click(sender As Object, e As RoutedEventArgs) Handles btnAluno.Click
        mainFrame.Content = New PageAlunoFuncionario()
    End Sub

    Private Sub btnProfessor_Click(sender As Object, e As RoutedEventArgs) Handles btnProfessor.Click
        mainFrame.Content = New PageProfessorFuncionario()
    End Sub

    Private Sub btnCurso_Click(sender As Object, e As RoutedEventArgs) Handles btnCurso.Click
        mainFrame.Content = New PageCursoFuncionario()
    End Sub

    Private Sub btnDisciplina_Click(sender As Object, e As RoutedEventArgs) Handles btnDisciplina.Click
        mainFrame.Content = New PageDisciplinasFuncionario()
    End Sub

    Private Sub btnAvisos_Click(sender As Object, e As RoutedEventArgs) Handles btnAvisos.Click
        mainFrame.Content = New PageInicioFuncionario()
    End Sub

    Private Sub btnSair_Click(sender As Object, e As RoutedEventArgs) Handles btnSair.Click
        Dim login As New MainWindow()
        login.Show()
        Close()
    End Sub
End Class
