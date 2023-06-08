Class PageDisciplinasFuncionario
    Inherits PageModel

    Private Presenter As PresenterFuncionarioDisciplina = New PresenterFuncionarioDisciplina(Me)

    Public Overrides Sub DisplayInfo(infoMessage As String)
        Throw New NotImplementedException()
    End Sub

    Public Overrides Sub DisplayError()
        Throw New NotImplementedException()
    End Sub
End Class
