Imports System.Data

Public Class PresenterAlunoSolicitacoes
    Inherits Presenter

    Private ViewModelSolicitacao As New SolicitacaoViewModel()
    Private alunoBusinessRules As New AlunoBusinessRules()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelSolicitacao)
    End Sub

    Friend Sub RegisterSolicitacao()
        Dim data = ViewModelSolicitacao.ConvertToDictionary()
        Dim tipoSolicitacao = ViewModelSolicitacao.Tipo.Replace(" ", "")
        data("Tipo") = [Enum].Parse(GetType(TipoSolicitacao), tipoSolicitacao)

        SolicitacaoBusinessRules.RegisterSolicitacao(data)
    End Sub

    Friend Sub DownloadDocumento(idSolicitacao As String)
        Dim solicitacao = ModelUtils.GetAll(Table.Solicitacao).Where(Function(dict) dict("Id") = idSolicitacao).First()

        If IsDBNull(solicitacao("Documento")) Then
            View.DisplayInfo("A solicitação não possui documento anexado.")
            Return
        End If

        Dim hasSavedSucessfully = ModelUtils.SaveDocumento(solicitacao("Documento"), solicitacao("TituloDocumento"))
        If hasSavedSucessfully Then
            View.DisplayInfo("Arquivo salvo com sucesso!")
        Else
            View.DisplayInfo("Ocorreu um erro ao salvar o arquivo")
        End If
    End Sub

    Friend Function GetAllSolitacacoesAluno() As DataView
        Dim solicitacoesAluno = alunoBusinessRules.GetSolicitacoes()
        solicitacoesAluno = PresenterSolicitacaoUtils.CleanSolicitacaoDict(solicitacoesAluno)

        Return PresenterUtils.ConvertDictionaryToDataView(solicitacoesAluno)
    End Function
End Class
