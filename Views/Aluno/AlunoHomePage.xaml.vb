Public Class AlunoHomePage
    Implements IView

    Private Presenter As New PresenterAlunoHomePage(Me)


    Private Sub AlunoHomePage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim valorCampo As String = Presenter.CarregarDadosDoAluno("Login")
        txtNomeAluno.Text = valorCampo
        Dim imageBrush As ImageBrush = Presenter.CarregarImagemPerfilAluno()
        If imageBrush IsNot Nothing Then
            imgPerfil.Fill = imageBrush
        Else
            ' Caso não haja imagem, pode exibir uma imagem padrão ou limpar a imagem existente
            imgPerfil.Fill = Brushes.Transparent
        End If
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
        contentFrame.Content = New PageInicio()
    End Sub

    Private Sub btnPlano_Click(sender As Object, e As RoutedEventArgs) Handles btnPlano.Click
        contentFrame.Content = New PageDisciplinasCadastradas()
    End Sub
End Class
