Public Class PresenterFuncionarioDisciplina
    Inherits Presenter

    Private ViewModelDisciplina As New DisciplinaViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelDisciplina)
    End Sub


    Friend Sub RegisterDisciplina()
        Dim data = ViewModelDisciplina.ConvertToDictionary()

        BusinessRules.Save(data, Table.Disciplina)
    End Sub
End Class
