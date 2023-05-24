﻿Public Class DisciplinaProfessorPage
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

    Private Sub cmbDiaAula_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbDiaAula.SelectionChanged
        Dim diaAula = cmbDiaAula.SelectedItem

        cmbHorario.ItemsSource = Presenter.LoadHorariosComboBox(diaAula)
    End Sub

    Private Sub btnCadastrarProva_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarProva.Click
        Dim map As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
                {"Data", dataProva.SelectedDate},
                {"Tipo", cmbTipoProva.SelectedValue.Content}
            }

        Presenter.RegisterProva(map)
    End Sub

    Private Sub Window_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim alunosCadastrados = Presenter.GetAllAlunosCadastrados().DefaultView

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

        Dim map As New Dictionary(Of String, Object) From {
            {"IdProva", cmbProva.SelectedValue.Tag},
            {"Notas", notas}
        }

        Presenter.RegisterNotas(map)
    End Sub

    Private Function FindVisualChild(Of T As DependencyObject)(parent As DependencyObject) As T
        If parent IsNot Nothing Then
            Dim count As Integer = VisualTreeHelper.GetChildrenCount(parent)
            For i As Integer = 0 To count - 1
                Dim child As DependencyObject = VisualTreeHelper.GetChild(parent, i)
                If child IsNot Nothing AndAlso TypeOf child Is T Then
                    Return child
                Else
                    Dim foundChild As T = FindVisualChild(Of T)(child)
                    If foundChild IsNot Nothing Then
                        Return foundChild
                    End If
                End If
            Next
        End If
        Return Nothing
    End Function

    Private Sub cmbHorario_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbHorario.SelectionChanged
        PresencaAlunosDataGrid.ItemsSource = Presenter.GetAllAlunosCadastrados().DefaultView
    End Sub

    Private Sub CheckBoxPresencaAluno_Click(sender As Object, e As RoutedEventArgs)
        Dim checkBox As CheckBox = CType(sender, CheckBox)
        If checkBox.IsChecked Then
            Presenter.DarPresencaParaAluno(checkBox.Tag)
        Else
            Presenter.DarFaltaParaAluno(checkBox.Tag)
        End If
    End Sub

    Private Sub cmbLancarPresencas_Click(sender As Object, e As RoutedEventArgs) Handles cmbLancarPresencas.Click
        Dim map As New Dictionary(Of String, String) From {
            {"IdHorario", cmbHorario.SelectedValue.Tag},
            {"Data", cmbDiaAula.SelectedValue}
        }

        Presenter.RegisterPresencas(map)
    End Sub

    Private Sub cmbProva_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbProva.SelectionChanged
        Dim comboBox = CType(sender, ComboBox)
        Dim idProva = comboBox.SelectedItem.Tag

        NotasAlunosDataGrid.ItemsSource = Presenter.GetAllNotasAlunosCadastrados(idProva).DefaultView
    End Sub
End Class
