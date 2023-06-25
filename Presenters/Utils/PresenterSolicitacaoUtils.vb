Public Class PresenterSolicitacaoUtils
    Public Shared Function CleanSolicitacaoDict(solicitacoesAluno As IEnumerable(Of IDictionary(Of String, Object))) As IEnumerable(Of IDictionary(Of String, Object))
        solicitacoesAluno = PresenterUtils.RemoveKeyFromDict(solicitacoesAluno, "IdAluno", "Documento")

        For Each solicitacao In solicitacoesAluno
            solicitacao("Título do Documento") = solicitacao("TituloDocumento")
            solicitacao("Tipo") = [Enum].GetName(GetType(TipoSolicitacao), solicitacao("Tipo"))
        Next

        solicitacoesAluno = PresenterUtils.RemoveKeyFromDict(solicitacoesAluno, "TituloDocumento")

        Return solicitacoesAluno
    End Function
End Class
