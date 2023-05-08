Public Class FuncionarioHomePage
    Implements IView

    Private Presenter As PresenterFuncionario = New PresenterFuncionario(Me)

    Private Sub FuncionarioHomePage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim disciplinas = Presenter.GetDataTable("Disciplina").DefaultView
        DisciplinasCursoDataGrid.ItemsSource = disciplinas
        DisciplinasProfessorDataGrid.ItemsSource = disciplinas

        For Each curso In Presenter.GetAll("Curso")
            Dim comboBoxItem As New ComboBoxItem With {
                .Content = curso("Nome"),
                .Tag = curso("Id")
            }

            cmbCursosAluno.Items.Add(comboBoxItem)
        Next
    End Sub

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        lblInfo.Content = infoMessage

        If tabCadastroProfessor.IsSelected Then
            lblInfoCadastroProfessor.Content = infoMessage
        ElseIf tabCadastroAluno.IsSelected Then
            lblInfoCadastroAluno.Content = infoMessage
        ElseIf tabCadastroCurso.IsSelected Then
            lblInfoCadastroCurso.Content = infoMessage
        ElseIf tabCadastroDisciplina.IsSelected Then
            lblInfoCadastroDisciplina.Content = infoMessage
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
                {"Password", txtPassword.Text},
                {"Curso", cmbCursosAluno.SelectedValue.Tag},
                {"SemestreInicio", cmbSemestreInicio.SelectedValue.Content}
            }

        Presenter.RegisterAluno(map)
    End Sub

    Private Sub btnCadastrarProfessor_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarProfessor.Click
        Dim map As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
            {"Login", txtLoginProfessor.Text},
            {"Password", txtPasswordProfessor.Text}
        }

        Presenter.RegisterProfessor(map)
    End Sub

    Private Sub tabEditarProfessor_GotFocus(sender As Object, e As RoutedEventArgs) Handles tabEditarProfessor.GotFocus
        professorDataGrid.ItemsSource = Presenter.GetDataTable("Professor").DefaultView
    End Sub

    Private Sub tabEditarAlunos_GotFocus(sender As Object, e As RoutedEventArgs) Handles tabEditarAlunos.GotFocus
        alunosDataGrid.ItemsSource = Presenter.GetDataTable("Aluno").DefaultView
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
        cursoDataGrid.ItemsSource = Presenter.GetDataTable("Curso").DefaultView
    End Sub

    Private Sub btnCadastrarDisciplina_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarDisciplina.Click
        Dim map As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
            {"Name", txtNomeDisciplina.Text},
            {"Semester", txtSemestre.Text},
            {"Workload", txtCargaHoraria.Text},
            {"Description", txtDescricao.Text}
        }

        Presenter.RegisterDisciplina(map)
    End Sub

    Private Sub CheckBox_Click(sender As Object, e As RoutedEventArgs)
        Dim checkBox As CheckBox = CType(sender, CheckBox)
        If checkBox.IsChecked Then
            Presenter.AddDisciplinaSelecionadaAoCurso(checkBox.Tag)
        Else
            Presenter.RemoveDisciplinaSelecionadaDoCurso(checkBox.Tag)
        End If
    End Sub

    Private Sub cmbCursosAluno_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbCursosAluno.SelectionChanged
        Dim semestreInicio As Integer = 0
        If cmbSemestreInicio.SelectedValue IsNot Nothing Then
            semestreInicio = Convert.ToInt32(cmbSemestreInicio.SelectedValue.Content)
        End If

        DisciplinasCursoAlunoDataGrid.ItemsSource =
            Presenter.GetDisciplinasCursoSemestreInicio(cmbCursosAluno.SelectedValue.Tag, semestreInicio).DefaultView
    End Sub

    Private Sub cmbSemestreInicio_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbSemestreInicio.SelectionChanged
        Dim semestreInicio As Integer = Convert.ToInt32(cmbSemestreInicio.SelectedValue.Content)

        If cmbCursosAluno.SelectedValue Is Nothing Then
            Return
        End If

        DisciplinasCursoAlunoDataGrid.ItemsSource =
            Presenter.GetDisciplinasCursoSemestreInicio(cmbCursosAluno.SelectedValue.Tag, semestreInicio).DefaultView
    End Sub

    Private Sub CheckBoxDisciplinasAluno_Click(sender As Object, e As RoutedEventArgs)
        Dim checkBox As CheckBox = CType(sender, CheckBox)
        If checkBox.IsChecked Then
            Presenter.AddDisciplinaSelecionadaAoAluno(checkBox.Tag)
        Else
            Presenter.RemoveDisciplinaSelecionadaDoAluno(checkBox.Tag)
        End If
    End Sub

    Private Sub CheckBoxDisciplinaProfessor_Click(sender As Object, e As RoutedEventArgs)
        Dim checkBox As CheckBox = CType(sender, CheckBox)
        If checkBox.IsChecked Then
            Presenter.AddDisciplinaSelecionadaAoProfessor(checkBox.Tag)
        Else
            Presenter.RemoveDisciplinaSelecionadaDoProfessor(checkBox.Tag)
        End If
    End Sub
End Class
