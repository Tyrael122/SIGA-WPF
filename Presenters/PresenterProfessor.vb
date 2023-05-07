Imports System.Data

Public Class PresenterProfessor
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)

        Call New DisciplinaProfessorPage().Show()
    End Sub

    Friend Function GetDisciplinasCadastradas() As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Of Professor)(SessionCookie.GetCookie("userId"))
        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Public Function GetAllAlunosCadastrados() As DataTable
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim entityRelation = New Relation(Of Disciplina, Aluno)

        Dim alunosCadastrados = entityRelation.GetAllMultipleEntitiesByIdAsDict(idDisciplina)
        Return ConvertDictionariesToDataTable(alunosCadastrados)
    End Function

    Public Function GetAllAlunosCadastradosAsDict() As IEnumerable(Of IDictionary(Of String, String))
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim entityRelation = New Relation(Of Disciplina, Aluno)
        Return entityRelation.GetAllMultipleEntitiesByIdAsDict(idDisciplina)
    End Function

    Friend Sub RegisterProva(data As IDictionary(Of String, String))
        Dim dataProva As Date = data("Data")
        data("Data") = dataProva.ToString("yyyy-MM-dd")

        data("Tipo") = [Enum].Parse(GetType(TipoProva), data("Tipo"))

        data.Add("IdDisciplina", SessionCookie.GetCookie("idDisciplina"))
        BusinessRules.Save(data, Table.Prova)
    End Sub

    Friend Function GetAllProvasAsDict() As IEnumerable(Of IDictionary(Of String, String))
        Dim provas = BusinessRules.GetAll(Table.Prova)

        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Return provas.Where(Function(dict) dict("IdDisciplina") = idDisciplina)
    End Function

    Public Sub RegisterNotas(data As IDictionary(Of String, Object))
        Dim idProva = data("IdProva")
        Dim notas = data("Notas")

        For Each nota In notas
            Dim savebleData = New Dictionary(Of String, String) From {
                {"IdProva", idProva},
                {"IdAluno", nota("IdAluno")},
                {"Nota", nota("Nota")}
            }

            BusinessRules.Save(savebleData, Table.Nota)
        Next
    End Sub
End Class
