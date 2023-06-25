Friend Class PresenterFuncionarioHomePage
    Inherits Presenter

    Private ViewModelFuncionario As New FuncionarioViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        PresenterUtils.LoadUserInfo(ViewModelFuncionario, Table.FuncionarioAdm)

        view.SetDataContext(ViewModelFuncionario)
    End Sub
End Class
