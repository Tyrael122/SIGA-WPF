Class PageAlunos
    Inherits SuperPage

    Private Presenter As New PresenterProfessorAluno(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub

    Private Sub PageAlunos_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        dataGridAlunos.ItemsSource = Presenter.GetAllAlunosCadastrados()
    End Sub

    Private Sub dataGridAlunos_AutoGeneratingColumn(sender As Object, e As DataGridAutoGeneratingColumnEventArgs) Handles dataGridAlunos.AutoGeneratingColumn
        If e.PropertyName = "Foto" Then
            e.Cancel = True
        End If
    End Sub
End Class
