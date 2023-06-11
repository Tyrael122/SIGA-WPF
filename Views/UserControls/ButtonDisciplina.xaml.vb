Public Class ButtonDisciplina
    Private Sub btnVerDisciplina_Click(sender As Object, e As RoutedEventArgs) Handles btnVerDisciplina.Click
        Dim button As Button = CType(sender, Button)
        Call PresenterProfessorHomePage.ShowDisciplinaPage(button.Tag)
    End Sub
End Class
