Imports System.Data

Public Class PresenterProfessor
    Inherits Presenter

    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)

        Call New DisciplinaProfessorPage().Show()
    End Sub

    Function LoadProvasComboBox() As IEnumerable(Of ComboBoxItem)
        Return LoadComboBox(Function() GetAllProvas(), "Data", "Id")
    End Function

    Friend Function GetDisciplinasCadastradas() As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Professor, SessionCookie.GetCookie("userId"))
        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Public Function GetAllAlunosCadastrados() As DataTable
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim entityRelation = New Relation(Table.Disciplina, Table.Aluno)
        Dim alunosCadastrados = entityRelation.GetAllMultipleEntitiesById(idDisciplina)

        Return ConvertDictionariesToDataTable(alunosCadastrados)
    End Function

    Friend Sub RegisterProva(data As IDictionary(Of String, String))
        Dim dataProva As Date = data("Data")
        data("Data") = dataProva.ToString("yyyy-MM-dd")

        data("Tipo") = [Enum].Parse(GetType(TipoProva), data("Tipo"))

        data.Add("IdDisciplina", SessionCookie.GetCookie("idDisciplina"))
        BusinessRules.Save(data, Table.Prova)
    End Sub

    Friend Function GetAllProvas() As IEnumerable(Of IDictionary(Of String, String))
        Dim provas = GetAll(Table.Prova)

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
