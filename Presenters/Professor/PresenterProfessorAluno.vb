Imports System.Data

Public Class PresenterProfessorAluno
    Inherits Presenter

    Private disciplinaBusinessRules As New DisciplinaBusinessRules()

    Public Sub New(view As IView)
        Me.View = view
    End Sub


    Public Function GetAllAlunosCadastrados() As DataView
        Dim alunosCadastrados = disciplinaBusinessRules.GetAllAlunosCadastrados()

        alunosCadastrados = BusinessRules.RemoveKeyFromDict(alunosCadastrados, "Password")

        Return ConvertDictionaryToDataView(alunosCadastrados)
    End Function

End Class
