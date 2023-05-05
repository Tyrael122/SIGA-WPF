Public Class ProfessorHomePage
    Implements IView

    Private Presenter As PresenterProfessor = New PresenterProfessor(Me)

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        Throw New NotImplementedException()
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Throw New NotImplementedException()
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs)
        DisciplinasDataGrid.ItemsSource = Presenter.GetDisciplinasCadastradas()
    End Sub

    Public Sub VerDisciplina_Click(sender As Object, e As EventArgs)
        Dim button As Button = CType(sender, Button)

        Presenter.ShowDisciplinaPage(button.Tag)
    End Sub
End Class
