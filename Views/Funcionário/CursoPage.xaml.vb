Public Class CursoPage
    Inherits WindowModel

    Private Presenter As New PresenterCurso(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
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
        Presenter.RegisterHorarioCurso()
    End Sub
End Class
