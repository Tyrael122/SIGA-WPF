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
        'Dim disciplinas = Presenter.GetDisciplinasCadastradas()
        Dim disciplinas = Presenter.GetAll("Disciplina")

        Dim itemControlSource As New ObservableCollection(Of DisciplinaViewModel)
        For Each disciplina In disciplinas
            Dim disciplinaViewModel As New DisciplinaViewModel()
            disciplinaViewModel.LoadFromDictionary(disciplina)

            itemControlSource.Add(disciplinaViewModel)
        Next

        itemControlDisciplinas.ItemsSource = itemControlSource
    End Sub
End Class
