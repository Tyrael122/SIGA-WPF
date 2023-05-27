Partial Public Class FuncionarioHomePage
    Inherits View
    Implements IViewModel

    Private ReadOnly Presenter As New PresenterFuncionario(Me)

    Private Sub FuncionarioHomePage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim disciplinas = Presenter.GetDataTable("Disciplina").DefaultView
        DisciplinasCursoDataGrid.ItemsSource = disciplinas
        DisciplinasProfessorDataGrid.ItemsSource = disciplinas

        cmbCursosAluno.ItemsSource = Presenter.LoadCursosAlunoComboBox()
        cursoDataGrid.ItemsSource = Presenter.GetDataTable("Curso").DefaultView
        alunosDataGrid.ItemsSource = Presenter.GetDataTable("Aluno").DefaultView

        AddHandler btnCadastrar.Click, AddressOf btnCadastrar_Click
    End Sub

    Public Overrides Sub DisplayInfo(infoMessage As String)
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

    Public Overrides Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Private Sub btnCadastrar_Click(sender As Object, e As RoutedEventArgs)
        Dim idsDisciplinasAluno = LoadIdsFromSelectedRows(DisciplinasCursoAlunoDataGrid)

        Presenter.RegisterAluno(idsDisciplinasAluno)
    End Sub

    Private Sub btnEditar_Click(sender As Object, e As RoutedEventArgs)
        Dim idsDisciplinasAluno = LoadIdsFromSelectedRows(DisciplinasCursoAlunoDataGrid)

        Presenter.UpdateAluno(idsDisciplinasAluno)

        RemoveHandler btnCadastrar.Click, AddressOf btnEditar_Click
        AddHandler btnCadastrar.Click, AddressOf btnCadastrar_Click

        btnCadastrar.Content = "Cadastrar"
    End Sub

    Private Sub btnCadastrarProfessor_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarProfessor.Click
        Dim idsDisciplinasProfessor = LoadIdsFromSelectedRows(DisciplinasProfessorDataGrid)

        Presenter.RegisterProfessor(idsDisciplinasProfessor)
    End Sub

    Private Sub tabEditarProfessor_GotFocus(sender As Object, e As RoutedEventArgs) Handles tabEditarProfessor.GotFocus
        professorDataGrid.ItemsSource = Presenter.GetDataTable("Professor").DefaultView
    End Sub

    Private Sub btnCadastrarCurso_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarCurso.Click
        Dim idsDisciplinasSelecionadas = LoadIdsFromSelectedRows(DisciplinasCursoDataGrid)

        Presenter.RegisterCurso(idsDisciplinasSelecionadas)
    End Sub

    Private Sub btnCadastrarDisciplina_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarDisciplina.Click
        Presenter.RegisterDisciplina()
    End Sub

    Private Sub VerCurso_Click(sender As Object, e As EventArgs)
        Dim button As Button = CType(sender, Button)

        Presenter.ShowCursoPage(button.Tag)
    End Sub

    Private Sub cmbCursosAluno_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbCursosAluno.SelectionChanged
        Dim semestreInicio As Integer = 0

        If cmbCursosAluno.SelectedItem Is Nothing Then
            Return
        End If

        If cmbSemestreInicio.SelectedValue IsNot Nothing Then
            semestreInicio = Convert.ToInt32(cmbSemestreInicio.SelectedItem.Content)
        End If

        DisciplinasCursoAlunoDataGrid.ItemsSource =
            Presenter.GetDisciplinasAcimaSemestre(cmbCursosAluno.SelectedItem.Tag, semestreInicio).DefaultView
    End Sub

    Private Sub cmbSemestreInicio_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbSemestreInicio.SelectionChanged
        If cmbSemestreInicio.SelectedItem Is Nothing Then
            Return
        End If

        Dim semestreInicio As Integer = Convert.ToInt32(cmbSemestreInicio.SelectedItem.Content)

        If cmbCursosAluno.SelectedValue Is Nothing Then
            Return
        End If

        DisciplinasCursoAlunoDataGrid.ItemsSource =
            Presenter.GetDisciplinasAcimaSemestre(cmbCursosAluno.SelectedItem.Tag, semestreInicio).DefaultView
    End Sub

    Private Sub DeletarAluno_Click(sender As Object, e As EventArgs)
        Dim button As Button = CType(sender, Button)

        Dim idAluno = button.Tag
        Presenter.DeleteAluno(idAluno)

        alunosDataGrid.ItemsSource = Presenter.GetDataTable("Aluno").DefaultView
    End Sub

    Private Sub EditarAluno_Click(sender As Object, e As EventArgs)
        Dim button As Button = CType(sender, Button)

        Dim idAluno = button.Tag
        Presenter.CarregarAlunoParaEdicao(idAluno)

        tabControlAluno.SelectedIndex = 0

        DisciplinasCursoAlunoDataGrid.ItemsSource =
            Presenter.GetDisciplinasAluno(idAluno).DefaultView

        RemoveHandler btnCadastrar.Click, AddressOf btnCadastrar_Click
        AddHandler btnCadastrar.Click, AddressOf btnEditar_Click

        btnCadastrar.Content = "Editar"
    End Sub

    Public Sub SetDataContext(viewModel As Object) Implements IViewModel.SetDataContext
        DataContext = viewModel
    End Sub
End Class
