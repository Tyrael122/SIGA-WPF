Public Class AlunoHomePage
    Implements IView

    Private Presenter As New PresenterAluno(Me)
    Private DAL As New DAL()

    Private Sub AlunoHomePage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded



        Dim campo As String = "Foto"
        Dim result = DAL.SelectFields(Table.Aluno, campo)
        If result.Count > 0 AndAlso result(0).ContainsKey(campo) Then
            Dim imageString As String = result(0)(campo)
            Dim imageByte As Byte() = Presenter.ConvertStringToBytes(imageString)
            Presenter.DisplayImage(imageByte, imgPerfil)
        End If

        Dim loginCampo As String = "Login"
        Dim user = DAL.SelectFields(Table.Aluno, loginCampo)
        If user.Count > 0 AndAlso user(0).ContainsKey(loginCampo) Then
            Dim loginString As String = user(0)(loginCampo)
            txtNomeAluno.Text = loginString
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
