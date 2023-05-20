Public Class PresenterLogin
    Inherits Presenter

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub TryLogin(username As String, password As String, table As Table)
        Dim user = LoginRules.GetUserByCredentials(username, password, table)

        Dim isCredentialsCorrect = user.Count() = 1
        If Not isCredentialsCorrect Then
            View.DisplayInfo("The credentials are invalid.")
            Return
        End If

        SessionCookie.AddCookie("userId", user.First()("Id"))

        ShowWindowAndCloseCurrent(ChooseWindow(table), View)
    End Sub

    Public Sub ShowChangePasswordScreen()
        ShowWindowAndCloseCurrent(New ChangePasswordScreen, View)
    End Sub

    Private Function ChooseWindow(table As Table) As Window
        Select Case table
            Case Table.Aluno
                Return New AlunoHomePage()
            Case Table.Professor
                Return New ProfessorHomePage()
            Case Table.FuncionarioAdm
                Return New FuncionarioHomePage()
            Case Else
                Throw New ArgumentException("Invalid table.")
        End Select
    End Function
End Class
