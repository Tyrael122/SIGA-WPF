Imports System.Runtime.CompilerServices

Public Class PresenterLogin
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub TryLogin(username As String, password As String, userType As UserType) Implements IPresenter.TryLogin
        Dim isCredentialsCorrect As Boolean = LoginRules.ValidateCredentials(username, password, userType)

        If isCredentialsCorrect Then
            Dim window As Window = ChooseWindow(userType)
            window.Show()

            View.CloseView()

            ' TODO: Log the user in
            ' TODO: Display to the user the right plataform dependent on his userType
            ' Problem: a switch statement would litter the method for each case
            ' Solution: use a presenter to get the right form?
            ' The deal is that we will need to call this chooseForm function all over the system.
            ' I wanted something that you have to login once, then you're redirected to your user specific plataform

        Else
            View.DisplayInfo("The credentials are invalid.")
        End If
    End Sub

    Private Function ChooseWindow(userType As UserType) As Window
        Select Case (userType)
            Case UserType.Aluno
                Return New AlunoHomePage()
            Case UserType.Professor
                ' Return New ProfessorHomePage()
            Case UserType.FuncionarioAdm
                Return New FuncionarioHomePage()
        End Select

        Throw New NotImplementedException() ' TODO: Should choose the right form based on the userType
    End Function
End Class
