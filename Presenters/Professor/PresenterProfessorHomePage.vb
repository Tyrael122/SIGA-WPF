Public Class PresenterProfessorHomePage
    Inherits Presenter

    Private ViewModelProfessor As New ProfessorViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        PresenterUtils.LoadUserInfo(ViewModelProfessor, Table.Professor)

        view.SetDataContext(ViewModelProfessor)
    End Sub

    Public Shared Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)

        Call New TelaProfessor().Show()
    End Sub

    Friend Function GetDisciplinasCadastradas() As IEnumerable(Of IDictionary(Of String, Object))
        Return ModelUtils.GetDisciplinas(Table.Professor, SessionCookie.GetCookie("userId"))
    End Function

    Public Function GetNomeDisciplina() As String
        Dim idDisciplina = SessionCookie.GetCookie("IdDisciplina")

        Return ModelUtils.GetAll(Table.Disciplina).Where(Function(dict) dict("Id") = idDisciplina).First()("Name")
    End Function
End Class
