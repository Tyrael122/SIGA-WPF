Public Class ChangePasswordScreen
    Implements IView

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        lblInfo.Content = infoMessage
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Throw New NotImplementedException()
    End Sub

    Private Sub btnViewPassword_Click(sender As Object, e As RoutedEventArgs) Handles btnViewPassword.Click
        Dim view As New MainWindow()
        view.Show()
        Me.Close()
    End Sub

    Private Sub btnLogin_Click(sender As Object, e As RoutedEventArgs) Handles btnLogin.Click

        Dim presenter As New PresenterLogin(Me)
        If Not txtLogin.Text.Any() Then
            DisplayInfo("Preencha todos os campos. ")
        Else
            presenter.EnviarEmailRecuperacaoSenha(txtLogin.Text)
        End If

    End Sub
End Class
