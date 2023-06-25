Imports System.Data
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

        ModelUtils.Update(idSolicitacao, Table.Solicitacao, data)
    End Sub

    Friend Function GetSolicitacoes() As DataView
        Dim solicitacoesAluno = ModelUtils.GetAll(Table.Solicitacao)

        solicitacoesAluno = PresenterUtils.RemoveKeyFromDict(solicitacoesAluno, "IdAluno")
        solicitacoesAluno = PresenterUtils.RemoveKeyFromDict(solicitacoesAluno, "Documento")

        For Each solicitacao In solicitacoesAluno
            solicitacao("Título do Documento") = solicitacao("TituloDocumento")
            solicitacao("Tipo") = [Enum].GetName(GetType(TipoSolicitacao), solicitacao("Tipo"))
        Next

        solicitacoesAluno = PresenterUtils.RemoveKeyFromDict(solicitacoesAluno, "TituloDocumento")

        Return PresenterUtils.ConvertDictionaryToDataView(solicitacoesAluno)
    End Function
End Class
