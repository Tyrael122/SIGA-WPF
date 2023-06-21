Public Class TelaProfessor
    Inherits WindowModel

    Private Presenter As PresenterProfessorHomePage = New PresenterProfessorHomePage(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub btnInicio_Click(sender As Object, e As RoutedEventArgs) Handles btnInicio.Click
        mainFrame.Content = New PageAlunos()
    End Sub

    Private Sub btnProvas_Click(sender As Object, e As RoutedEventArgs) Handles btnProvas.Click
        mainFrame.Content = New PageCadastroProva()
    End Sub

    Private Sub btnNotas_Click(sender As Object, e As RoutedEventArgs) Handles btnNotas.Click
        mainFrame.Content = New PageLancarNotas()
    End Sub

    Private Sub btnPresenca_Click(sender As Object, e As RoutedEventArgs) Handles btnPresenca.Click
        mainFrame.Content = New PagePresenca()
    End Sub

    Private Sub TelaProfessor_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        mainFrame.Content = New PageAlunos()

        txtNomeDisciplina.Text = Presenter.GetNomeDisciplina()
    End Sub

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        Close()
    End Sub
End Class
