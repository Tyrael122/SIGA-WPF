﻿Imports System.Data

Public Class PresenterNotasAluno
    Inherits Presenter

    Private alunoBusinessRules As New AlunoBusinessRules()

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Function GetNotasAluno() As DataView
        Dim data = alunoBusinessRules.GetNotasAluno()

        data = ModelUtils.RemoveKeyFromDict(data, "IdAluno")
        data = ModelUtils.RemoveKeyFromDict(data, "Id")
        Return ConvertDictionaryToDataView(data)
    End Function
End Class
