Imports System.Runtime.CompilerServices

Class MainWindow
    Implements IComparer
    Private Sub ComboBox_SelectionChanged(sender As Object, e As SelectionChangedEventArgs)

    End Sub

    Public Function Compare(x As Object, y As Object) As Integer Implements IComparer.Compare
        Throw New NotImplementedException()
    End Function

    Private Sub Button_Click(sender As Object, e As RoutedEventArgs)
        'Me.Close()
        Debug.Write(Me.GetType())
        Dim newForm As AlunoHomePage = New AlunoHomePage()
        newForm.Show()
    End Sub
End Class
