Public Class PresenterLogin
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub TryLogin(username As String, password As String, userType As UserType) 'Implements IPresenter.TryLogin
        Dim user = LoginRules.GetUserByCredentials(username, password, userType)
        Dim isCredentialsCorrect = user.Count() = 1

        If isCredentialsCorrect Then
            BusinessRules.SaveUserId(user.First()("Id"))

            Dim window As Window = ChooseWindow(userType)
            window.Show()

            View.CloseView()
        Else
            View.DisplayInfo("The credentials are invalid.")
        End If
    End Sub

    Private Function ChooseWindow(userType As UserType) As Window
        Select Case (userType)
            Case UserType.Aluno
                Return New AlunoHomePage()
            Case UserType.Professor
                Return New ProfessorHomePage()
            Case UserType.FuncionarioAdm
                Return New FuncionarioHomePage()
        End Select

        Throw New NotImplementedException() ' TODO: Should choose the right form based on the userType
    End Function
End Class
