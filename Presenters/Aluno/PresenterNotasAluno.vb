Imports System.Data

Public Class PresenterNotasAluno
    Inherits Presenter

    Private alunoBusinessRules As New AlunoBusinessRules()

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Function GetNotasAluno() As DataView
        Dim data = alunoBusinessRules.GetNotasAluno()

        data = PresenterUtils.RemoveKeyFromDict(data, "IdAluno")
        data = PresenterUtils.RemoveKeyFromDict(data, "Id")
        Return PresenterUtils.ConvertDictionaryToDataView(data)
    End Function
End Class
