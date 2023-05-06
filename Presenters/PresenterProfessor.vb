Public Class PresenterProfessor
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Sub ShowDisciplinaPage(disciplina As Disciplina)
        SessionCookie.AddCookie("disciplina", disciplina)

        Call New DisciplinaProfessorPage().Show()
    End Sub

    Friend Function GetDisciplinasCadastradas() As IEnumerable(Of Disciplina)
        Return BusinessRules.GetDisciplinas(Of Professor)(SessionCookie.GetCookie("userId"))
    End Function

    Public Function GetAllAlunosCadastrados() As IEnumerable(Of Aluno)
        Dim idDisciplina = SessionCookie.GetCookie(Of Disciplina)("disciplina").Id

        Dim entityRelation = New Relation(Of Disciplina, Aluno)
        Return entityRelation.GetAllMultipleEntitiesById(idDisciplina)
    End Function

    Public Function GetAllAlunosCadastradosAsDict() As IEnumerable(Of IDictionary(Of String, String))
        Dim idDisciplina = SessionCookie.GetCookie(Of Disciplina)("disciplina").Id

        Dim entityRelation = New Relation(Of Disciplina, Aluno)
        Return entityRelation.GetAllMultipleEntitiesByIdAsDict(idDisciplina)
    End Function

    Friend Sub RegisterProva(data As IDictionary(Of String, String))
        Dim dataProva As Date = data("Data")
        data("Data") = dataProva.ToString("yyyy-MM-dd")

        data("Tipo") = [Enum].Parse(GetType(TipoProva), data("Tipo"))

        data.Add("IdDisciplina", SessionCookie.GetCookie(Of Disciplina)("disciplina").Id)
        BusinessRules.Save(data, Table.Prova)
    End Sub

    Friend Function GetAllProvasAsDict() As IEnumerable(Of IDictionary(Of String, String))
        Dim provas = BusinessRules.GetAllAsDict(Table.Prova)

        Dim idDisciplina = SessionCookie.GetCookie(Of Disciplina)("disciplina").Id

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
