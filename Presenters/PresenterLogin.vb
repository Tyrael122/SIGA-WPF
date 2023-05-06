Public Class PresenterLogin
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub TryLogin(username As String, password As String, table As Table) 'Implements IPresenter.TryLogin
        Dim user = LoginRules.GetUserByCredentials(username, password, table)
        Dim isCredentialsCorrect = user.Count() = 1

        If isCredentialsCorrect Then
            SessionCookie.AddCookie("userId", user.First()("Id"))

            Dim window As Window = ChooseWindow(table)
            window.Show()

            View.CloseView()
        Else
            View.DisplayInfo("The credentials are invalid.")
        End If
    End Sub

    Private Function ChooseWindow(table As Table) As Window
        Select Case table
            Case Table.Aluno
                Return New AlunoHomePage()
            Case Table.Professor
                Return New ProfessorHomePage()
            Case Table.FuncionarioAdm
                Return New FuncionarioHomePage()
        End Select

        Throw New NotImplementedException() ' TODO: Should choose the right form based on the userType
    End Function
End Class
