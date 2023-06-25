Imports System.Data

Public Class PresenterAlunoHomePage
    Inherits Presenter

    Private ViewModelAluno As New AlunoViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        PresenterUtils.LoadUserInfo(ViewModelAluno, Table.Aluno)

        view.SetDataContext(ViewModelAluno)
    End Sub

    Friend Function GetDisciplinasCadastradas() As DataView
        Dim disciplinas = ModelUtils.GetDisciplinas(Table.Aluno, ViewModelAluno.Id)

        Return PresenterUtils.ConvertDictionaryToDataView(disciplinas)
    End Function

    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)
        'Call New DisciplinaAlunoPage().Show()
    End Sub
End Class
