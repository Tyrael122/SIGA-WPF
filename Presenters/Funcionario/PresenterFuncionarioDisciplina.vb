﻿Public Class PresenterFuncionarioDisciplina
    Inherits Presenter

    Private ViewModelDisciplina As New DisciplinaViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelDisciplina)
    End Sub


    Friend Sub RegisterDisciplina()
        Dim data = ViewModelDisciplina.ConvertToDictionary()

        BusinessRules.Save(data, Table.Disciplina)

        ViewModelDisciplina.Clear()
    End Sub

    Friend Sub CarregarDisciplinaParaEdicao(idDisciplina As String)
        SessionCookie.AddCookie("IdDisciplina", idDisciplina)

        Dim data = BusinessRules.FindById(idDisciplina, Table.Disciplina).First()

        ViewModelDisciplina.LoadFromDictionary(data)
    End Sub

    Friend Sub DeleteDisciplina(idDisciplina As String)
        DisciplinaBusinessRules.DeleteDisciplina(idDisciplina)
    End Sub

    Friend Sub UpdateDisciplina()
        Dim data = ViewModelDisciplina.ConvertToDictionary()
        Dim idDisciplina = SessionCookie.GetCookie("IdDisciplina")

        data.Remove("Id")

        BusinessRules.Update(idDisciplina, Table.Disciplina, data)

        ViewModelDisciplina.Clear()
    End Sub
End Class
