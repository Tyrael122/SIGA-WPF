Public Class PresenterProfessor
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Function GetAllAlunos()
        Return BusinessRules.GetAll(Of Aluno)(Table.Aluno)
    End Function

    Friend Sub ShowDisciplinaPage(disciplina As Disciplina)
        Dim disciplinaPage As Window = New DisciplinaProfessorPage(disciplina)

        disciplinaPage.Show()
    End Sub

    Friend Function GetDisciplinasCadastradas() As IEnumerable(Of Disciplina)
        Return BusinessRules.GetDisciplinas(Of Professor)(SessionCookie.userData("Id"))
    End Function
End Class
