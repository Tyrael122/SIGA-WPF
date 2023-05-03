Friend Class PresenterProfessor
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Function GetAllAlunos()
        Return BusinessRules.GetAll(Of Aluno)(Table.Aluno)
    End Function
End Class
