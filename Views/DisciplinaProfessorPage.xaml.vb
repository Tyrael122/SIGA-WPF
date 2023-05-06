Public Class DisciplinaProfessorPage
    Implements IView

    Private Presenter As New PresenterProfessor(Me)

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        Throw New NotImplementedException()
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Throw New NotImplementedException()
    End Sub

    Private Sub btnCadastrarProva_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarProva.Click
        Dim map As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
            {"Data", dataProva.SelectedDate},
            {"Tipo", cmbTipoProva.SelectedValue.Content}
        }

        Presenter.RegisterProva(map)
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        AlunosDataGrid.ItemsSource = Presenter.GetAllAlunosCadastrados()
    End Sub
End Class
