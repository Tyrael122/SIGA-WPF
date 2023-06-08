Imports System.Data
Imports System.Data.Common
Imports System.IO
Imports System.Security.AccessControl
Imports System.Security.Principal

Public Class PresenterFuncionario
    Inherits Presenter

    Private ViewModelDisciplina As New DisciplinaViewModel()
    Private ViewModelCurso As New CursoViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelDisciplina)
    End Sub

    Friend Sub RegisterCurso(idsDisciplinasCurso As List(Of String))
        'Dim data = ViewModelCurso.ConvertToDictionary()

        'data("Turno") = [Enum].Parse(GetType(Turno), data("Turno"))

        'Dim hasInsertedSucessfully = Relation.SaveRelation(Table.Curso, Table.Disciplina, idsDisciplinasCurso, data)
        'ShowInfoMessage(hasInsertedSucessfully, "Curso")
    End Sub

    Friend Sub RegisterDisciplina()
        Dim data = ViewModelDisciplina.ConvertToDictionary()

        Dim hasInsertedSucessufully = BusinessRules.Save(data, Table.Disciplina)
        ShowInfoMessage(hasInsertedSucessufully, "Disciplina")
    End Sub

    Friend Sub ShowCursoPage(idCurso As String)
        SessionCookie.AddCookie("IdCurso", idCurso)

        'Call New CursoPage().Show()
    End Sub

    Friend Function GetDisciplinasAcimaSemestre(idCurso As String, semestre As Integer) As DataView
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, idCurso)
        disciplinas = disciplinas.Where(Function(disciplina) disciplina("Semester") >= semestre)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = True
        Next

        Return ConvertDictionaryToDataView(disciplinas)
    End Function

    Friend Function GetDisciplinasAluno(idAluno As String) As DataView
        Dim aluno = BusinessRules.GetAllById(idAluno, Table.Aluno).First()

        Dim idDisciplinasAluno = BusinessRules.GetDisciplinas(Table.Aluno, idAluno).Select(Function(dict) dict("Id"))

        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, aluno("Curso")).
                                    Where(Function(disciplina) disciplina("Semester") >= aluno("SemestreInicio"))

        For Each disciplina In disciplinas
            disciplina("IsChecked") = idDisciplinasAluno.Contains(disciplina("Id"))
        Next

        Return ConvertDictionaryToDataView(disciplinas)
    End Function


    Private Sub ShowInfoMessage(hasInsertedSucessfully As Boolean, keyWord As String)
        If hasInsertedSucessfully Then
            View.DisplayInfo(keyWord & " adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar " & keyWord.ToLower())
        End If
    End Sub

    Friend Function GetDataTableWithCheckboxColumn(table As String) As DataView
        Dim data = GetAll(table)

        For Each dict In data
            dict("IsChecked") = False
        Next

        Return ConvertDictionaryToDataView(data)
    End Function
End Class
