﻿Public Class PresenterProfessorNotas
    Inherits Presenter

    Private ViewModelNotas As New NotasViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelNotas)
    End Sub

    Function LoadProvasComboBox() As IEnumerable(Of ComboBoxItem)
        Return GenerateComboBoxItems(Function() GetAllProvas(), "Data", "Id")
    End Function

    Friend Function GetAllProvas() As IEnumerable(Of IDictionary(Of String, Object))
        Dim provas = GetAll(Table.Prova)

        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Return provas.Where(Function(dict) dict("IdDisciplina") = idDisciplina)
    End Function

    Public Sub RegisterNotas(notas As IEnumerable(Of IDictionary(Of String, String)))
        For Each nota In notas
            nota("IdAluno") = nota("Id")
            nota.Remove("Id")

            Dim savebleData = New Dictionary(Of String, Object) From {
                {"IdProva", ViewModelNotas.IdProva},
                {"IdAluno", nota("IdAluno")},
                {"Nota", nota("Nota")}
            }

            BusinessRules.Save(savebleData, Table.Nota)
        Next
    End Sub

    Friend Function GetAllNotasAlunosCadastrados() As Object
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim entityRelation = New Relation(Table.Disciplina, Table.Aluno)
        Dim alunosCadastrados = entityRelation.GetAllMultipleEntitiesById(idDisciplina)

        For Each aluno In alunosCadastrados
            Dim idAluno = aluno("Id")
            Dim dadosNota = BusinessRules.GetAll(Table.Nota).Where(Function(dict) dict("IdAluno") = idAluno And
                                                                                dict("IdProva") = ViewModelNotas.IdProva)

            If dadosNota.Any() Then
                aluno("Nota") = dadosNota.First()("Nota")
            Else
                aluno("Nota") = ""
            End If
        Next

        Return ConvertDictionaryToDataView(alunosCadastrados)
    End Function
End Class