Imports System.IO

Friend Class PresenterFuncionarioSolicitacao
    Inherits Presenter

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Sub AnexarDocumento(filePath As String, idSolicitacao As String)
        Dim data As New Dictionary(Of String, Object) From {
            {"Documento", File.ReadAllBytes(filePath)},
            {"TituloDocumento", Path.GetFileName(filePath)}
        }

        BusinessRules.Update(idSolicitacao, Table.Solicitacao, data)
    End Sub
End Class
