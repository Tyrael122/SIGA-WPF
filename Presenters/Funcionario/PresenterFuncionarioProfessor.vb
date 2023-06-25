Imports System.Data

Public Class PresenterFuncionarioProfessor
    Inherits Presenter

    Private ViewModelProfessor As New ProfessorViewModel()
    Private professorBusinessRules As New ProfessorBusinessRules()


    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelProfessor)

        ViewModelProfessor.Foto = PresenterUtils.CarregarFotoVazia()
    End Sub

    Friend Sub RegisterProfessor(idsDisciplinasProfessor As IEnumerable(Of String))
        Dim data = ViewModelProfessor.ConvertToDictionary()

        ProfessorBusinessRules.Register(data, idsDisciplinasProfessor)

        ViewModelProfessor.Clear()
    End Sub

    Friend Sub DeleteProfessor(idProfessor As String)
        ProfessorBusinessRules.DeleteProfessor(idProfessor)
    End Sub

    Friend Sub CarregarProfessorParaEdicao(idProfessor As String)
        Dim data = ModelUtils.LoadUserWithPhotoById(idProfessor, Table.Professor)

        ViewModelProfessor.LoadFromDictionary(data)
    End Sub

    Friend Sub UpdateProfessor(idsDisciplinasProfessor As List(Of String))
        Dim data = ViewModelProfessor.ConvertToDictionary()
        data.Remove("Id")

        professorBusinessRules.UpdateProfessor(data, idsDisciplinasProfessor)

        ViewModelProfessor.Clear()
    End Sub

    Friend Function GetDisciplinasProfessor() As DataView
        Dim idProfessor = SessionCookie.GetCookie("IdProfessor")

        Dim disciplinas = ModelUtils.GetDisciplinasComCheckBoxColumn(idProfessor, Table.Professor)

        Return PresenterUtils.ConvertDictionaryToDataView(disciplinas)
    End Function
End Class
