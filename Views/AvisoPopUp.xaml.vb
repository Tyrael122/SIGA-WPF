Public Class AvisoPopUp
    Private idAviso As String

    Private Presenter As New PresenterAviso(idAviso)

    Public Sub New(idAviso As String)
        Me.idAviso = idAviso
    End Sub

    Private Sub AvisoPopUp_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        InitializeComponent()

        Presenter = New PresenterAviso(idAviso)

        txtTitulo.Content = Presenter.GetTitulo()
        txtConteudo.Text = Presenter.GetConteudo()
    End Sub
End Class
