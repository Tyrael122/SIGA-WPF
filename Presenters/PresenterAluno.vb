Public Class PresenterAluno
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Function GetDisciplinasCadastradas() As IEnumerable(Of Disciplina)
        Return BusinessRules.GetDisciplinas(Of Aluno)(SessionCookie.GetCookie("userId"))
    End Function

    Friend Sub ShowDisciplinaPage(disciplina As Disciplina)
        SessionCookie.AddCookie("disciplina", disciplina)
        Call New DisciplinaAlunoPage().Show()
    End Sub

    Friend Function GetNotasDisciplina() As IEnumerable
        ' TODO: Get notas from database based on disciplinaId
        ' Steps: get the disciplinaId from the cookie, then get the provasId from provas table
        ' then get the notas from notas table based on the provasId and the alunoId which is the userId
    End Function
End Class
