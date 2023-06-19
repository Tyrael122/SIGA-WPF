Imports System.Data

Public Class PresenterFuncionarioProfessor
    Inherits Presenter

    Private ViewModelProfessor As New ProfessorViewModel()


    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelProfessor)

        ViewModelProfessor.Foto = CarregarFotoVazia()
    End Sub

    Friend Sub RegisterProfessor(idsDisciplinasProfessor As IEnumerable(Of String))
        Dim data = ViewModelProfessor.ConvertToDictionary()

        If data("Foto").GetType() <> GetType(Byte()) Then
            data("Foto") = ConvertImageToByteArray(ViewModelProfessor.Foto)
        End If

        Relation.SaveRelation(Table.Professor, Table.Disciplina, idsDisciplinasProfessor, data)

        ViewModelProfessor.Clear()
    End Sub

    Friend Sub DeleteProfessor(idProfessor As String)
        BusinessRules.DeleteProfessor(idProfessor)
    End Sub

    Friend Sub CarregarProfessorParaEdicao(idProfessor As String)
        Dim data = BusinessRules.GetAllById(idProfessor, Table.Professor).First()

        ViewModelProfessor.LoadFromDictionary(data)

        SessionCookie.AddCookie("IdProfessor", idProfessor)
    End Sub

    Friend Sub UpdateProfessor(idsDisciplinasProfessor As List(Of String))
        Dim idProfessor = SessionCookie.GetCookie("IdProfessor")

        BusinessRules.DeleteDisciplinas(idProfessor, "IdProfessor", Table.ProfessorDisciplina)

        Dim data = ViewModelProfessor.ConvertToDictionary()

        If data("Foto").GetType() <> GetType(Byte()) Then
            data("Foto") = ConvertImageToByteArray(ViewModelProfessor.Foto)
        End If

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

        Dim disciplinas = BusinessRules.GetDisciplinasComCheckBoxColumn(idProfessor, Table.Professor)

        Return ConvertDictionaryToDataView(disciplinas)
    End Function
End Class
