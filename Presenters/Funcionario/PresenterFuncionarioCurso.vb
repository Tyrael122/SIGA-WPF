Public Class PresenterFuncionarioCurso
    Inherits Presenter

    Private ViewModelCurso As New CursoViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelCurso)
    End Sub

    Friend Sub RegisterCurso(idsDisciplinasCurso As List(Of String))
        Dim data = ViewModelCurso.ConvertToDictionary()

        data("Turno") = [Enum].Parse(GetType(Turno), data("Turno"))

        Relation.SaveRelation(Table.Curso, Table.Disciplina, idsDisciplinasCurso, data)
    End Sub


    Friend Sub ShowCursoPage(idCurso As String)
        SessionCookie.AddCookie("IdCurso", idCurso)

        'Call New CursoPage().Show()
    End Sub
End Class
