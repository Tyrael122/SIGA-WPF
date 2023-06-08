Imports System.Data

Public Class PresenterProfessorHomePage
    Inherits Presenter

    Private ViewModelProfessor As New ProfessorViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelProfessor)

        ViewModelProfessor.Foto = CarregarFotoVazia()
    End Sub

    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)

        'Call New DisciplinaProfessorPage().Show()
    End Sub

    Friend Function GetDisciplinasCadastradas() As DataView
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Professor, SessionCookie.GetCookie("userId"))
        Return ConvertDictionaryToDataView(disciplinas)
    End Function
End Class
