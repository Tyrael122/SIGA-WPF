Imports System.Data

Public Class PresenterFuncionarioCurso
    Inherits Presenter

    Private ViewModelCurso As New CursoViewModel()
    Private cursoBusinessRules As New CursoBusinessRules()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelCurso)
    End Sub

    Friend Sub RegisterCurso(idsDisciplinasCurso As List(Of String))
        Dim data = ViewModelCurso.ConvertToDictionary()
        data("Turno") = [Enum].Parse(GetType(Turno), data("Turno"))

        cursoBusinessRules.RegisterCurso(data, idsDisciplinasCurso)

        ViewModelCurso.Clear()
    End Sub

    Friend Function ShowCursoPage(idCurso As String)
        SessionCookie.AddCookie("IdCurso", idCurso)

        Call New CadastroHorario().Show()
    End Function

    Friend Sub DeleteCurso(idCurso As Object)
        CursoBusinessRules.DeleteCurso(idCurso)
    End Sub

    Friend Sub CarregarCursoParaEdicao(idCurso As String)
        Dim data = ModelUtils.LoadEntityById(idCurso, Table.Curso)

        ViewModelCurso.LoadFromDictionary(data)
        ViewModelCurso.Turno = [Enum].Parse(GetType(Turno), data("Turno")).ToString()
    End Sub

    Friend Sub UpdateCurso(idsDisciplinasCurso As List(Of String))
        Dim data = ViewModelCurso.ConvertToDictionary()
        data("Turno") = [Enum].Parse(GetType(Turno), ViewModelCurso.Turno)

        cursoBusinessRules.UpdateCurso(data, idsDisciplinasCurso)

        ViewModelCurso.Clear()
    End Sub

    Friend Function GetDisciplinasCurso() As DataView
        Dim idCurso = SessionCookie.GetCookie("IdCurso")

        Dim disciplinas = ModelUtils.GetDisciplinasComCheckBoxColumn(idCurso, Table.Curso)

        Return PresenterUtils.ConvertDictionaryToDataView(disciplinas)
    End Function

    Friend Function GetAllCursos() As DataView
        Dim cursos = ModelUtils.GetAll(Table.Curso)

        For Each curso In cursos
            curso("Turno") = [Enum].Parse(GetType(Turno), curso("Turno")).ToString()
        Next

        Return PresenterUtils.ConvertDictionaryToDataView(cursos)
    End Function
End Class
