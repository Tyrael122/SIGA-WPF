Public Class PresenterFuncionarioDisciplina
    Inherits Presenter

    Private ViewModelDisciplina As New DisciplinaViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelDisciplina)
    End Sub

    Friend Sub RegisterDisciplina()
        Dim data = ViewModelDisciplina.ConvertToDictionary()

        ModelUtils.Save(data, Table.Disciplina)

        ViewModelDisciplina.Clear()
    End Sub

    Friend Sub CarregarDisciplinaParaEdicao(idDisciplina As String)
        Dim data = ModelUtils.LoadEntityById(idDisciplina, Table.Disciplina)

        ViewModelDisciplina.LoadFromDictionary(data)
    End Sub

    Friend Sub DeleteDisciplina(idDisciplina As String)
        DisciplinaBusinessRules.DeleteDisciplina(idDisciplina)
    End Sub

    Friend Sub UpdateDisciplina()
        Dim data = ViewModelDisciplina.ConvertToDictionary()

        ModelUtils.Update(Table.Disciplina, data)

        ViewModelDisciplina.Clear()
    End Sub
End Class
