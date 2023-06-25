Imports System.Data

Public Class PresenterProfessorNotas
    Inherits Presenter

    Private ViewModelNotas As New NotasViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelNotas)
    End Sub

    Function LoadProvasComboBox() As IEnumerable(Of ComboBoxItem)
        Return PresenterUtils.GenerateComboBoxItems(Function() GetAllProvas(), "Data", "Id")
    End Function

    Friend Function GetAllProvas() As IEnumerable(Of IDictionary(Of String, Object))
        Dim provas = GetAll(Table.Prova)

        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Return provas.Where(Function(dict) dict("IdDisciplina") = idDisciplina)
    End Function

    Public Sub RegisterNotas(notas As IEnumerable(Of IDictionary(Of String, Object)))
        For Each nota In notas
            Dim matchedNota = GetAll(Table.Nota).Where(Function(dict) dict("IdAluno") = nota("Id") And
                                                        dict("IdProva") = ViewModelNotas.IdProva)

            If matchedNota.Any() Then
                Dim idNotaMatched = matchedNota.First()("Id")

                AlunoBusinessRules.DeleteNotaAluno(idNotaMatched)
            End If

            nota("IdAluno") = nota("Id")
            nota.Remove("Id")

            Dim savebleData = New Dictionary(Of String, Object) From {
                {"IdProva", ViewModelNotas.IdProva},
                {"IdAluno", nota("IdAluno")},
                {"Nota", nota("Nota")}
            }

            ModelUtils.Save(savebleData, Table.Nota)
        Next

        View.DisplayInfo("Notas cadastradas com sucesso!")
    End Sub

    Friend Function GetAllNotasAlunosCadastrados() As DataView
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim entityRelation = New Relation(Table.Disciplina, Table.Aluno)
        Dim alunosCadastrados = entityRelation.GetAllMultipleEntitiesById(idDisciplina)

        For Each aluno In alunosCadastrados
            Dim idAluno = aluno("Id")
            Dim dadosNota = GetAll(Table.Nota).Where(Function(dict) dict("IdAluno") = idAluno And
                                                                                dict("IdProva") = ViewModelNotas.IdProva)

            If dadosNota.Any() Then
                aluno("Nota") = dadosNota.First()("Nota")
            Else
                aluno("Nota") = ""
            End If
        Next

        alunosCadastrados = ModelUtils.RemoveKeyFromDict(alunosCadastrados, "Password")

        Return PresenterUtils.ConvertDictionaryToDataView(alunosCadastrados)
    End Function
End Class
