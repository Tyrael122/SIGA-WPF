Public Class AlunoHomePage
    Implements IView

    Private Presenter As New PresenterAluno(Me)

    Private Sub AlunoHomePage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        DisciplinasAlunoDataGrid.ItemsSource = Presenter.GetDisciplinasCadastradas().DefaultView
    End Sub

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        Throw New NotImplementedException()
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Throw New NotImplementedException()
    End Sub

    Public Sub VerDisciplina_Click(sender As Object, e As EventArgs)
        Dim button As Button = CType(sender, Button)

        Presenter.ShowDisciplinaPage(button.Tag)
    End Sub
End Class
