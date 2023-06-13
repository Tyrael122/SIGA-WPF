Imports System.Data

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


    Friend Function ShowCursoPage(idCurso As String)
        SessionCookie.AddCookie("IdCurso", idCurso)

        Call New CadastroHorario().Show()
    End Function

    Friend Sub DeleteCurso(idCurso As Object)
        BusinessRules.DeleteCurso(idCurso)
    End Sub

    Friend Sub CarregarCursoParaEdicao(idCurso As String)
        Dim data = BusinessRules.GetAllById(idCurso, Table.Curso).First()

        ViewModelCurso.LoadFromDictionary(data)

        ViewModelCurso.Turno = [Enum].Parse(GetType(Turno), data("Turno")).ToString()

        SessionCookie.AddCookie("IdCurso", idCurso)
    End Sub

    Friend Sub UpdateCurso(idsDisciplinasCurso As List(Of String))
        Dim idCurso = SessionCookie.GetCookie("IdCurso")

        BusinessRules.DeleteDisciplinas(idCurso, "IdCurso", Table.CursoDisciplina)

        Dim data = ViewModelCurso.ConvertToDictionary()

        data("Turno") = [Enum].Parse(GetType(Turno), ViewModelCurso.Turno)

        Dim relation As New Relation(Table.Curso, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idRelatedEntites = idsDisciplinasCurso
        }

        relation.Update(idCurso)

        ViewModelCurso.Clear()
    End Sub

    Friend Function GetDisciplinasCurso() As DataView
        Dim idCurso = SessionCookie.GetCookie("IdCurso")

        Dim disciplinas = BusinessRules.GetDisciplinasComCheckBoxColumn(idCurso, Table.Curso)

        Return ConvertDictionaryToDataView(disciplinas)
    End Function
End Class
