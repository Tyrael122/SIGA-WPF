Public Class CadastroHorario
    Inherits WindowModel

    Private Presenter As PresenterHorarioCurso = New PresenterHorarioCurso(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub cmbSemestre_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbSemestre.SelectionChanged
        cmbDisciplinas.ItemsSource = Presenter.LoadDisciplinasPorSemestre()
    End Sub

    Private Sub cmbDisciplinas_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbDisciplinas.SelectionChanged
        cmbProfessor.ItemsSource = Presenter.LoadProfessoresPorDisciplina()
    End Sub

    Private Sub btnCadastrarHorario_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarHorario.Click
        Presenter.RegisterHorarioCurso()
    End Sub

    Private Sub CadastroHorario_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        cmbHorarioInicio.ItemsSource = Presenter.LoadHorarioInicio()
        cmbHorarioFim.ItemsSource = Presenter.LoadHorarioFim()
    End Sub
End Class
