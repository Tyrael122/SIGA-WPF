Imports System.Collections.ObjectModel

Class TelaInicialProfessor
    Inherits WindowModel

    Private Presenter As New PresenterProfessorHomePage(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub TelaInicialProfessor_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim disciplinas = Presenter.GetDisciplinasCadastradas()

        Dim itemControlSource As New ObservableCollection(Of DisciplinaViewModel)
        For Each disciplina In disciplinas
            Dim disciplinaViewModel As New DisciplinaViewModel()
            disciplinaViewModel.LoadFromDictionary(disciplina)

            itemControlSource.Add(disciplinaViewModel)
        Next

        gridDisciplinas.GridItemsSource = itemControlSource

        dataGridAvisos.ItemsSource = Presenter.GetDataView("Aviso")
    End Sub

    Private Sub btnSair_Click(sender As Object, e As RoutedEventArgs) Handles btnSair.Click
        Dim login As New MainWindow()
        login.Show()
        Close()
    End Sub

    Private Sub btnVerAviso(sender As Object, e As RoutedEventArgs)
        Presenter.ShowAviso(GetIdFromButton(sender))
    End Sub
End Class
