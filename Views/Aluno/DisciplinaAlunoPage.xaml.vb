Public Class DisciplinaAlunoPage
    Implements IView

    Private Presenter As New PresenterAlunoDisciplina(Me)

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        Throw New NotImplementedException()
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Throw New NotImplementedException()
    End Sub

    Private Sub DisciplinaAlunoPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        NotasDisciplinaDataGrid.ItemsSource = Presenter.GetNotasDisciplina().DefaultView
    End Sub
End Class
