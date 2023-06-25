Imports System.Data

Public Class PresenterFuncionarioProfessor
    Inherits Presenter

    Private ViewModelProfessor As New ProfessorViewModel()


    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelProfessor)

        ViewModelProfessor.Foto = PresenterUtils.CarregarFotoVazia()
    End Sub

    Friend Sub RegisterProfessor(idsDisciplinasProfessor As IEnumerable(Of String))
        Dim data = ViewModelProfessor.ConvertToDictionary()
        data("Foto") = PresenterUtils.ConvertImageToByteArray(ViewModelProfessor.Foto)

        Relation.SaveRelation(Table.Professor, Table.Disciplina, idsDisciplinasProfessor, data)

        ViewModelProfessor.Clear()
    End Sub

    Friend Sub DeleteProfessor(idProfessor As String)
        ProfessorBusinessRules.DeleteProfessor(idProfessor)
    End Sub

    Friend Sub CarregarProfessorParaEdicao(idProfessor As String)
        Dim data = ModelUtils.FindById(idProfessor, Table.Professor).First()

        data("Foto") = PresenterUtils.ConvertByteArrayToImage(data("Foto"))
        ViewModelProfessor.LoadFromDictionary(data)

        SessionCookie.AddCookie("IdProfessor", idProfessor)
    End Sub

    Friend Sub UpdateProfessor(idsDisciplinasProfessor As List(Of String))
        Dim idProfessor = SessionCookie.GetCookie("IdProfessor")

        DisciplinaBusinessRules.DeleteDisciplinas(idProfessor, "IdProfessor", Table.ProfessorDisciplina)

        Dim data = ViewModelProfessor.ConvertToDictionary()
        data("Foto") = PresenterUtils.ConvertImageToByteArray(ViewModelProfessor.Foto)

        data.Remove("Id")

        Dim relation As New Relation(Table.Professor, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idRelatedEntites = idsDisciplinasProfessor
        }

        relation.Update(idProfessor)

        ViewModelProfessor.Clear()
    End Sub

    Friend Function GetDisciplinasProfessor() As DataView
        Dim idProfessor = SessionCookie.GetCookie("IdProfessor")

        Dim disciplinas = ModelUtils.GetDisciplinasComCheckBoxColumn(idProfessor, Table.Professor)

        Return PresenterUtils.ConvertDictionaryToDataView(disciplinas)
    End Function
End Class
