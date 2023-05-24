Imports System.Data

Public Class PresenterAluno
    Inherits Presenter

    Private ReadOnly idAluno As String

    Public Sub New(view As IView)
        Me.View = view

        idAluno = SessionCookie.GetCookie("userId")
    End Sub

    Friend Function GetDisciplinasCadastradas() As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Aluno, SessionCookie.GetCookie("userId"))

        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)
        Call New DisciplinaAlunoPage().Show()
    End Sub

    Friend Function GetNotasDisciplina() As DataTable
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim idProvasDaDisciplina = BusinessRules.GetAll(Table.Prova).
                                        Where(Function(prova) prova("IdDisciplina") = idDisciplina).
                                        Select(Function(prova) prova("Id"))

        Dim notas = BusinessRules.GetAll(Table.Nota)

        Dim idAluno = SessionCookie.GetCookie("userId")
        Dim rawData = notas.Where(Function(dict) dict("IdAluno") = idAluno And
                                idProvasDaDisciplina.Contains(dict("IdProva")))

        Return ConvertDictionariesToDataTable(rawData)
    End Function

    Friend Function GetPresencaDisciplina() As DataTable
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim presencasDaDisciplina = BusinessRules.GetAll(Table.Presenca).
                                        Where(Function(presenca) presenca("IdDisciplina") = idDisciplina And
                                                            presenca("IdAluno") = idAluno)

        Return ConvertDictionariesToDataTable(presencasDaDisciplina)
    End Function
End Class
