Public Class DisciplinaAlunoPage
    Implements IView

    Private Presenter As New PresenterAluno(Me)

    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        Throw New NotImplementedException()
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Throw New NotImplementedException()
    End Sub

    Private Sub DisciplinaAlunoPage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        NotasDisciplinaDataGrid.ItemsSource = Presenter.GetNotasDisciplina().DefaultView
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
End Class
