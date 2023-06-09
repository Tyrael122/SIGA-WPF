Public Class ButtonCadastrar
    Public Event Click As EventHandler

    Private Sub MyButton_Click(sender As Object, e As EventArgs) Handles btnCadastrar.Click
        RaiseEvent Click(sender, e)
    End Sub
End Class
