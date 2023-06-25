Imports System.Data

Friend Class PresenterPresencasAluno
    Inherits Presenter

    Private alunoBusinessRules As New AlunoBusinessRules()

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Function GetPresencasAluno() As DataView
        Dim presencas = alunoBusinessRules.GetPresencasAluno()

        presencas = PresenterUtils.RemoveKeyFromDict(presencas, "IdAluno", "IdAula")

        Return PresenterUtils.ConvertDictionaryToDataView(presencas)
    End Function
End Class
