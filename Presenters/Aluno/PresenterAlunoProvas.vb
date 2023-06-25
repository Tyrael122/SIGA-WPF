Imports System.Data

Public Class PresenterAlunoProvas
    Inherits Presenter

    Private alunoBusinessRules As New AlunoBusinessRules()

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Function GetProvasFuturas() As DataView
        Dim provas = alunoBusinessRules.GetProvasFuturas()

        For Each prova In provas
            prova = AddNomeDisciplinaToProvas(prova)
            prova = AddNomeProfessorToProvas(prova)
        Next

        Return PresenterUtils.ConvertDictionaryToDataView(provas)
    End Function

    Private Function AddNomeProfessorToProvas(prova As IDictionary(Of String, Object)) As IDictionary(Of String, Object)
        Dim nomeProfessor = ModelUtils.FindById(prova("IdProfessor"), Table.Professor).First()("Login")
        prova("NomeProfessor") = nomeProfessor

        Return prova
    End Function

    Private Shared Function AddNomeDisciplinaToProvas(prova As IDictionary(Of String, Object)) As IDictionary(Of String, Object)
        Dim nomeDisciplina = ModelUtils.FindById(prova("IdDisciplina"), Table.Disciplina).First()("Name")
        prova("NomeDisciplina") = nomeDisciplina

        Return prova
    End Function
End Class
