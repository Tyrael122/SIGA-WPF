Public Class FuncionarioHomePage
    Implements IView

    Private Presenter As PresenterFuncionario = New PresenterFuncionario(Me)

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        lblInfo.Content = infoMessage
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

    Private Sub FuncionarioHomePage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        alunosDataGrid.ItemsSource = BusinessRules.GetAllAlunos()
        'Dim aluno As Aluno = New Aluno
        'aluno.Curso = Curso.ADS
        'aluno.Login = "owec"
        'aluno.Password = "oicioq"


        'alunosDataGrid.ItemsSource = {aluno}
    End Sub
End Class
