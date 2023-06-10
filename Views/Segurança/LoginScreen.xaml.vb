Class MainWindow
    Implements IView

    Private Presenter As PresenterLogin = New PresenterLogin(Me)

    Private Sub btnLogin_Click(sender As Object, e As RoutedEventArgs) Handles btnLogin.Click
        If String.IsNullOrEmpty(txtLogin.Text) OrElse String.IsNullOrEmpty(txtPassword.Password) Then
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

    Public Sub CloseView() Implements IView.CloseView
        Me.Close()
    End Sub

    Private Sub LoginForm_Initialized(sender As Object, e As EventArgs) Handles LoginForm.Initialized
        cmbUserType.SelectedIndex = 0
    End Sub

    Private Sub btnViewPassword_Click(sender As Object, e As RoutedEventArgs) Handles btnViewPassword.Click
        Dim isPasswordVisible = Panel.GetZIndex(txtVisiblePassword) > Panel.GetZIndex(txtPassword)

        If isPasswordVisible Then
            HidePassword()
        Else
            ShowPassword()
        End If

    End Sub

    Private Sub HidePassword()
        Panel.SetZIndex(txtVisiblePassword, 0)
        Panel.SetZIndex(txtPassword, 1)
        Panel.SetZIndex(btnViewPassword, 2)
        txtPassword.Password = txtVisiblePassword.Text
        txtPassword.Visibility = Visibility.Visible
        txtVisiblePassword.Visibility = Visibility.Hidden
        txtPassword.Focus()
        txtPassword.SelectAll()
    End Sub

    Private Sub ShowPassword()
        Panel.SetZIndex(txtPassword, 0)
        Panel.SetZIndex(txtVisiblePassword, 1)
        Panel.SetZIndex(btnViewPassword, 2)
        txtVisiblePassword.Text = txtPassword.Password
        txtVisiblePassword.Visibility = Visibility.Visible
        txtPassword.Visibility = Visibility.Hidden
        txtVisiblePassword.Focus()
        txtVisiblePassword.Select(txtVisiblePassword.Text.Length, 0)
    End Sub

    Private Sub btnChancePassword_Click(sender As Object, e As RoutedEventArgs) Handles btnChangePassword.Click
        Presenter.ShowChangePasswordScreen()
    End Sub
End Class


