Public Class PresenterProfessorHomePage
    Inherits Presenter

    Private ViewModelProfessor As New ProfessorViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        LoadUserInfo(ViewModelProfessor, Table.Professor)

        view.SetDataContext(ViewModelProfessor)

        'ViewModelProfessor.Foto = CarregarFotoVazia()
    End Sub

    Public Shared Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)

        Call New TelaProfessor().Show()
    End Sub

    Friend Function GetDisciplinasCadastradas() As IEnumerable(Of IDictionary(Of String, Object))
        Return BusinessRules.GetDisciplinas(Table.Professor, SessionCookie.GetCookie("userId"))
    End Function
End Class
