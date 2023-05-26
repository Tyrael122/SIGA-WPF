Public Class DisciplinaProfessorPage
    Implements IViewModel

    Private Presenter As New PresenterProfessorDisciplina(Me)

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        Throw New NotImplementedException()
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Throw New NotImplementedException()
    End Sub

    Public Sub SetDataContext(viewModel As Object) Implements IViewModel.SetDataContext
        DataContext = viewModel
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
        Dim notas As New List(Of IDictionary(Of String, String))

        For Each row In NotasAlunosDataGrid.Items
            Dim nota As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
                        {"IdAluno", row("IdAluno")},
                        {"Nota", row("Nota")}
                    }

            notas.Add(nota)
        Next

        Presenter.RegisterNotas(notas)
    End Sub

    Private Sub cmbHorario_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbHorario.SelectionChanged
        PresencaAlunosDataGrid.ItemsSource = Presenter.GetAllPresencaAlunosCadastrados().DefaultView
    End Sub

    Private Sub cmbLancarPresencas_Click(sender As Object, e As RoutedEventArgs) Handles cmbLancarPresencas.Click
        Dim presencas As New List(Of IDictionary(Of String, String))

        For Each row In NotasAlunosDataGrid.Items
            Dim presenca As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
                        {"IdAluno", row("IdAluno")},
                        {"IsPresente", row("IsPresente")}
                    }

            presencas.Add(presenca)
        Next

        Presenter.RegisterPresencas(presencas)
    End Sub

    Private Sub cmbProva_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbProva.SelectionChanged
        Dim comboBox = CType(sender, ComboBox)
        Dim idProva = comboBox.SelectedItem.Tag

        NotasAlunosDataGrid.ItemsSource = Presenter.GetAllNotasAlunosCadastrados(idProva).DefaultView
    End Sub
End Class
