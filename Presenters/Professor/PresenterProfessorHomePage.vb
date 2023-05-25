Imports System.Data

Public Class PresenterProfessorHomePage
    Inherits Presenter

    Public Sub New(view As IView)
        Me.View = view
        ViewModel = New AulaViewModel()
    End Sub

    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)

        Call New DisciplinaProfessorPage().Show()
    End Sub

    Friend Function GetDisciplinasCadastradas() As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Professor, SessionCookie.GetCookie("userId"))
        Return ConvertDictionariesToDataTable(disciplinas)
    End Function
End Class
