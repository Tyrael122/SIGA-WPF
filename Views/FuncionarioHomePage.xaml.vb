Public Class FuncionarioHomePage
    Implements IView

    Private Presenter As PresenterFuncionario = New PresenterFuncionario(Me)

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        lblInfo.Content = infoMessage

        If tabCadastroProfessor.IsSelected Then
            lblInfoCadastroProfessor.Content = infoMessage
        ElseIf tabCadastroAluno.IsSelected Then
            lblInfoCadastroAluno.Content = infoMessage
        ElseIf tabCadastroCurso.IsSelected Then
            lblInfoCadastroCurso.Content = infoMessage
        End If
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
    End Sub

    Private Sub btnCadastrarProfessor_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarProfessor.Click
        Dim map As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
            {"Login", txtLoginProfessor.Text},
            {"Password", txtPasswordProfessor.Text}
        }

        Presenter.RegisterProfessor(map)
    End Sub

    Private Sub tabEditarAlunos_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles tabEditarAlunos.MouseLeftButtonDown
    End Sub

    Private Sub tabEditarProfessor_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles tabEditarProfessor.MouseLeftButtonDown
    End Sub

    Private Sub tabEditarProfessor_GotFocus(sender As Object, e As RoutedEventArgs) Handles tabEditarProfessor.GotFocus
        professorDataGrid.ItemsSource = Presenter.GetAllProfessores()
    End Sub

    Private Sub tabEditarAlunos_GotFocus(sender As Object, e As RoutedEventArgs) Handles tabEditarAlunos.GotFocus
        alunosDataGrid.ItemsSource = Presenter.GetAllAlunos()
    End Sub

    Private Sub TabControl_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    Private Sub btnCadastrarCurso_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarCurso.Click
        Dim map As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
            {"Nome", txtNomeCurso.Text},
            {"Sigla", txtSigla.Text},
            {"Turno", cmbTurno.SelectedIndex}
        }

        Presenter.RegisterCurso(map)
    End Sub

    Private Sub tabEditarCurso_GotFocus(sender As Object, e As RoutedEventArgs) Handles tabEditarCurso.GotFocus
        cursoDataGrid.ItemsSource = Presenter.GetAllCursos()
    End Sub
End Class
