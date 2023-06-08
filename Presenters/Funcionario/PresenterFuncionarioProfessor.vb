Public Class PresenterFuncionarioProfessor
    Inherits Presenter

    Private ViewModelProfessor As New ProfessorViewModel()


    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelProfessor)

        ViewModelProfessor.Foto = CarregarFotoVazia()
    End Sub

    Friend Sub RegisterProfessor(idsDisciplinasProfessor As IEnumerable(Of String))
        Dim data = ViewModelProfessor.ConvertToDictionary()

        Relation.SaveRelation(Table.Professor, Table.Disciplina, idsDisciplinasProfessor, data)
    End Sub
End Class
