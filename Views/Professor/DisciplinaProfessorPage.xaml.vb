Public Class DisciplinaProfessorPage
    Inherits WindowModel

    Private Presenter As New PresenterProfessorDisciplina(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub btnCadastrarProva_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarProva.Click
        Presenter.RegisterProva()
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        AlunosDataGrid.ItemsSource = Presenter.GetAllAlunosCadastrados().DefaultView

        cmbProva.ItemsSource = Presenter.LoadProvasComboBox()

        presencaFrame.Content = New CadastroPresenca()
    End Sub

    Private Sub btnLancarNotas_Click(sender As Object, e As RoutedEventArgs) Handles btnLancarNotas.Click
        Dim notas = LoadReferenceFromSelectedRows(NotasAlunosDataGrid, "Id", "Nota")

        Presenter.RegisterNotas(notas)
    End Sub

    Private Sub cmbProva_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbProva.SelectionChanged
        Dim comboBox = CType(sender, ComboBox)
        Dim idProva = comboBox.SelectedItem.Tag

        NotasAlunosDataGrid.ItemsSource = Presenter.GetAllNotasAlunosCadastrados(idProva).DefaultView
    End Sub
End Class
