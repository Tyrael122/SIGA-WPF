Public Class CursoPage
    Implements IView

    Private Presenter As New PresenterCurso(Me)

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        Throw New NotImplementedException()
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Throw New NotImplementedException()
    End Sub

    Private Sub cmbSemestre_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbSemestre.SelectionChanged
        Dim semestre = cmbSemestre.SelectedItem.Content
        cmbDisciplinas.ItemsSource = Presenter.LoadDisciplinasPorSemestre(semestre)
    End Sub

    Private Sub cmbDisciplinas_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbDisciplinas.SelectionChanged
        If cmbDisciplinas.SelectedItem Is Nothing Then
            Return
        End If

        Dim idDisciplina = cmbDisciplinas.SelectedItem.Tag
        cmbProfessor.ItemsSource = Presenter.LoadProfessoresPorDisciplina(idDisciplina)
    End Sub

    Private Sub btnCadastrarHorarioCurso_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarHorarioCurso.Click
        Dim map As New Dictionary(Of String, String) From {
            {"IdDisciplina", cmbDisciplinas.SelectedItem.Tag},
            {"IdProfessor", cmbProfessor.SelectedItem.Tag},
            {"Semestre", cmbSemestre.SelectedItem.Content},
            {"DiaSemana", cmbDiaSemana.SelectedItem.Content},
            {"HorarioInicio", cmbHorarioInicio.SelectedItem.Content},
            {"HorarioFim", cmbHorarioFim.SelectedItem.Content}
        }

        Presenter.RegisterHorarioCurso()
    End Sub
End Class
