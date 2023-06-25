Public Class SolicitacaoBusinessRules
    Inherits Model

    Friend Shared Sub RegisterSolicitacao(data As Dictionary(Of String, Object))
        data("IdAluno") = SessionCookie.GetCookie("UserId")

        ModelUtils.Save(data, Table.Solicitacao)
    End Sub
End Class
