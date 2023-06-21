Friend Class PresenterAviso
    Private idAviso As String

    Public Sub New(idAviso As String)
        Me.idAviso = idAviso
    End Sub

    Friend Function GetTitulo() As String
        Return BusinessRules.FindById(idAviso, Table.Aviso).First()("Titulo")
    End Function

    Friend Function GetConteudo() As String
        Return BusinessRules.FindById(idAviso, Table.Aviso).First()("Conteudo")
    End Function
End Class
