Imports System.Data

Public Class PresenterAlunoHomePage
    Inherits Presenter

    Private ViewModelAluno As New AlunoViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        PresenterUtils.LoadUserInfo(ViewModelAluno, Table.Aluno)

        view.SetDataContext(ViewModelAluno)
    End Sub
End Class
