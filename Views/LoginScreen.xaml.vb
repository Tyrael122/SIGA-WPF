Class MainWindow
    Implements IView

    Private Presenter As IPresenter = New PresenterForms(Me)

    Private Sub ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    Private Sub btnLogin_Click(sender As Object, e As RoutedEventArgs) Handles btnLogin.Click
        If String.IsNullOrEmpty(txtLogin.Text) Or String.IsNullOrEmpty(txtPassword.Password) Then
            DisplayInfo("The username and password fields cannot be empty.")
            Return
        End If

        Presenter.TryLogin(txtLogin.Text, txtPassword.Password, cmbUserType.SelectedIndex)
    End Sub

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        lblInfo.Content = infoMessage
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Private Sub LoginForm_Initialized(sender As Object, e As EventArgs) Handles LoginForm.Initialized
        cmbUserType.SelectedIndex = 0
    End Sub
End Class
