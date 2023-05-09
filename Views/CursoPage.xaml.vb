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
        cmbDisciplinas.ItemsSource = Presenter.LoadDisciplinasCurso(semestre)
    End Sub
End Class
