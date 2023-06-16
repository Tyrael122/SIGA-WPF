Public Class AlunoHomePage
    Implements IView

    Private Presenter As New PresenterAlunoHomePage(Me)


    Private Sub AlunoHomePage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        mainFrame.Content = New PageInicioAluno()

        'Dim valorCampo As String = Presenter.CarregarDadosDoAluno("Login")
        'txtNomeAluno.Text = valorCampo
        'Dim imageBrush As ImageBrush = Presenter.CarregarImagemPerfilAluno()
        'If imageBrush IsNot Nothing Then
        '    imgPerfil.Fill = imageBrush
        'Else
        '    ' Caso não haja imagem, pode exibir uma imagem padrão ou limpar a imagem existente
        '    imgPerfil.Fill = Brushes.Transparent
        'End If
    End Sub

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        Throw New NotImplementedException()
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Throw New NotImplementedException()
    End Sub

    Public Sub VerDisciplina_Click(sender As Object, e As EventArgs)
        Dim button As Button = CType(sender, Button)
        Presenter.ShowDisciplinaPage(button.Tag)
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
End Class
