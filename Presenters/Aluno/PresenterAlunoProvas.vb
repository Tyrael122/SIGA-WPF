Imports System.Data

Public Class PresenterAlunoProvas
    Inherits Presenter

    Private ReadOnly idAluno As String

    Public Sub New(view As IView)
        Me.View = view

        idAluno = SessionCookie.GetCookie("userId")
    End Sub

    Friend Function GetProvasFuturas() As DataView
        Dim idDisciplinas = ModelUtils.GetDisciplinas(Table.Aluno, idAluno).Select(Function(dict) dict("Id"))

        Dim idCurso = ModelUtils.GetAll(Table.Aluno).First(Function(dict) dict("Id") = idAluno)("Curso")

        Dim provas = ModelUtils.GetAll(Table.Prova).Where(Function(dict) idDisciplinas.Contains(dict("IdDisciplina")) And
                                                                 dict("Data") > Date.Now)

        For Each prova In provas
            Dim nomeDisciplina = ModelUtils.GetAll(Table.Disciplina).Where(Function(dict) dict("Id") = prova("IdDisciplina")).First()("Name")

            Dim nomeProfessor = ModelUtils.GetAll(Table.Professor).Where(Function(dict) dict("Id") = prova("IdProfessor")).First()("Login")

            prova("NomeDisciplina") = nomeDisciplina
            prova("NomeProfessor") = nomeProfessor
        Next

        Return ConvertDictionaryToDataView(provas)
    End Function
End Class
