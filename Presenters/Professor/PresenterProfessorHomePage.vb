Imports System.Data

Public Class PresenterProfessorHomePage
    Inherits Presenter

    Private ViewModelAula As New AulaViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelAula)
    End Sub

    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)

        Call New DisciplinaProfessorPage().Show()
    End Sub

    Friend Function GetDisciplinasCadastradas() As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Professor, SessionCookie.GetCookie("userId"))
        Return ConvertDictionaryToDataTable(disciplinas)
    End Function
End Class
