Imports System.Data

Public Class PresenterAlunoHomePage
    Inherits Presenter

    Private ReadOnly idAluno As String

    Public Sub New(view As IView)
        Me.View = view

        idAluno = SessionCookie.GetCookie("userId")
    End Sub

    Friend Function GetDisciplinasCadastradas() As DataView
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Aluno, idAluno)

        Return ConvertDictionaryToDataView(disciplinas)
    End Function

    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)
        'Call New DisciplinaAlunoPage().Show()
    End Sub

    Friend Function CarregarDadosDoAluno(v As String) As String
        Throw New NotImplementedException()
    End Function

    Friend Function CarregarImagemPerfilAluno() As ImageBrush
        Throw New NotImplementedException()
    End Function
End Class
