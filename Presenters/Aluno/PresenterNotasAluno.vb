Imports System.Data

Public Class PresenterNotasAluno
    Inherits Presenter

    Private alunoBusinessRules As New AlunoBusinessRules()

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Function GetNotasAluno() As DataView
        Dim data = alunoBusinessRules.GetNotasAluno()

        data = BusinessRules.RemoveKeyFromDict(data, "IdAluno")
        data = BusinessRules.RemoveKeyFromDict(data, "Id")
        Return ConvertDictionaryToDataView(data)
    End Function
End Class
