Public Class FuncionarioHomePage
    Implements IView

    Private Presenter As PresenterFuncionario = New PresenterFuncionario(Me)

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        Throw New NotImplementedException()
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Me.Close()
    End Sub

    Private Sub btnCadastrar_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrar.Click
        Dim map As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
            {"Login", txtLogin.Text},
            {"Password", txtPassword.Text}
        }

        Presenter.RegisterAluno(map)
    End Sub
End Class
