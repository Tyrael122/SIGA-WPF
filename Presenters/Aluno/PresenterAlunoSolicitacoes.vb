Imports System.Data
Imports System.IO
Imports Microsoft.Win32

Public Class PresenterAlunoSolicitacoes
    Inherits Presenter

    Private ViewModelSolicitacao As New SolicitacaoViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelSolicitacao)
    End Sub

    Friend Sub RegisterSolicitacao()
        Dim data = ViewModelSolicitacao.ConvertToDictionary()

        data("IdAluno") = SessionCookie.GetCookie("UserId")

        Dim tipoSolicitacao = ViewModelSolicitacao.Tipo.Replace(" ", "")
        data("Tipo") = [Enum].Parse(GetType(TipoSolicitacao), tipoSolicitacao)

        ModelUtils.Save(data, Table.Solicitacao)
    End Sub

    Friend Sub DownloadDocumento(idSolicitacao As String)
        Dim solicitacao = ModelUtils.GetAll(Table.Solicitacao).Where(Function(dict) dict("Id") = idSolicitacao).First()

        If Not IsDBNull(solicitacao("Documento")) Then
            SaveDocumento(solicitacao("Documento"), solicitacao("TituloDocumento"))
        Else
            View.DisplayInfo("A solicitação não possui documento anexado.")
        End If
    End Sub

    Private Sub SaveDocumento(dataToSave As Byte(), tituloDocumento As String)
        Dim saveFileDialog As New SaveFileDialog With {
            .FileName = Path.GetFileNameWithoutExtension(tituloDocumento),
            .DefaultExt = Path.GetExtension(tituloDocumento)
        }

        Dim hasUserClickedOk = saveFileDialog.ShowDialog()

        If hasUserClickedOk Then
            Dim filePath As String = saveFileDialog.FileName

            File.WriteAllBytes(filePath, dataToSave)

            View.DisplayInfo("File saved successfully.")
        End If
    End Sub

    Friend Function GetAllSolitacacoesAluno() As DataView
        Dim idAluno = SessionCookie.GetCookie("UserId")

        Dim solicitacoesAluno = ModelUtils.GetAll(Table.Solicitacao).Where(Function(dict) dict("IdAluno") = idAluno)

        solicitacoesAluno = ModelUtils.RemoveKeyFromDict(solicitacoesAluno, "IdAluno")
        solicitacoesAluno = ModelUtils.RemoveKeyFromDict(solicitacoesAluno, "Documento")

        For Each solicitacao In solicitacoesAluno
            solicitacao("Título do Documento") = solicitacao("TituloDocumento")
            solicitacao("Tipo") = [Enum].GetName(GetType(TipoSolicitacao), solicitacao("Tipo"))
        Next

        solicitacoesAluno = ModelUtils.RemoveKeyFromDict(solicitacoesAluno, "TituloDocumento")

        Return PresenterUtils.ConvertDictionaryToDataView(solicitacoesAluno)
    End Function
End Class
