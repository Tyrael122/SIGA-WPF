Imports System.Data

Public Class PresenterProfessorAluno
    Inherits Presenter

    Public Sub New(view As IView)
        Me.View = view
    End Sub


    Public Function GetAllAlunosCadastrados() As DataView
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim entityRelation = New Relation(Table.Disciplina, Table.Aluno)
        Dim alunosCadastrados = entityRelation.GetAllMultipleEntitiesById(idDisciplina)

        alunosCadastrados = BusinessRules.RemoveKeyFromDict(alunosCadastrados, "Password")

        Return ConvertDictionaryToDataView(alunosCadastrados)
    End Function

End Class
