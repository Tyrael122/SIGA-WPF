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

    Public Function GetAllAlunosCadastrados() As IEnumerable
        Dim idDisciplina = SessionCookie.GetCookie(Of Disciplina)("disciplina").Id

        Dim entityRelation = New Relation(Of Disciplina, Aluno)
        Return entityRelation.GetAllMultipleEntitiesById(idDisciplina)
    End Function

    Friend Sub RegisterProva(data As IDictionary(Of String, String))
        Dim dataProva As Date = data("Data")
        data("Data") = dataProva.ToString("yyyy-MM-dd")

        data("Tipo") = [Enum].Parse(GetType(TipoProva), data("Tipo"))

        data.Add("IdDisciplina", SessionCookie.GetCookie(Of Disciplina)("disciplina").Id)
        BusinessRules.Save(data, Table.Prova)
    End Sub
End Class
