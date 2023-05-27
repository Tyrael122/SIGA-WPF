Public Class DisciplinaProfessorPage
    Inherits WindowModel

    Private Presenter As New PresenterProfessorDisciplina(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub cmbDiaAula_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbDiaAula.SelectionChanged
        Dim diaAula = cmbDiaAula.SelectedItem
        cmbHorario.ItemsSource = Presenter.LoadHorariosComboBox(diaAula)
    End Sub

    Private Sub btnCadastrarProva_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarProva.Click
        Presenter.RegisterProva()
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        AlunosDataGrid.ItemsSource = Presenter.GetAllAlunosCadastrados().DefaultView

        cmbProva.ItemsSource = Presenter.LoadProvasComboBox()
        cmbDiaAula.ItemsSource = Presenter.LoadDiaAulaComoBox()
    End Sub

    Private Sub btnLancarNotas_Click(sender As Object, e As RoutedEventArgs) Handles btnLancarNotas.Click
        Dim notas = LoadReferenceFromSelectedRows(NotasAlunosDataGrid, "Id", "Nota")

        Presenter.RegisterNotas(notas)
    End Sub

    Private Sub cmbHorario_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbHorario.SelectionChanged
        PresencaAlunosDataGrid.ItemsSource = Presenter.GetAllPresencaAlunosCadastrados().DefaultView
    End Sub

    Private Sub cmbLancarPresencas_Click(sender As Object, e As RoutedEventArgs) Handles cmbLancarPresencas.Click
        Dim presencas = LoadReferenceFromSelectedRows(PresencaAlunosDataGrid, "Id", "IsPresente")

        Presenter.RegisterPresencas(presencas)
    End Sub

    Private Sub cmbProva_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbProva.SelectionChanged
        Dim comboBox = CType(sender, ComboBox)
        Dim idProva = comboBox.SelectedItem.Tag

        NotasAlunosDataGrid.ItemsSource = Presenter.GetAllNotasAlunosCadastrados(idProva).DefaultView
    End Sub
End Class
