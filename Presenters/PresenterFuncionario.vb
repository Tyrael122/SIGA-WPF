Imports System.Data
Imports System.Data.Common
Imports System.IO
Imports System.Security.AccessControl
Imports System.Security.Principal

Public Class PresenterFuncionario
    Inherits Presenter

    Private ViewModelAluno As New AlunoViewModel()
    Private ViewModelProfessor As New ProfessorViewModel()
    Private ViewModelDisciplina As New DisciplinaViewModel()
    Private ViewModelCurso As New CursoViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelDisciplina)
    End Sub

    Public Sub RegisterAluno(idsDisciplinasAluno As IDictionary(Of String, Object))
        'Dim data = ViewModelAluno.ConvertToDictionary()

        'Dim hasInsertedSucessfully = Relation.SaveRelation(Table.Aluno, Table.Disciplina, idsDisciplinasAluno, data)
        'ShowInfoMessage(hasInsertedSucessfully, "Aluno")

        BusinessRules.Save(idsDisciplinasAluno, Table.Aluno)
    End Sub

    Friend Sub RegisterProfessor(idsDisciplinasProfessor As List(Of String))
        'Dim data = ViewModelProfessor.ConvertToDictionary()

        'Dim hasInsertedSucessfully = Relation.SaveRelation(Table.Professor, Table.Disciplina, idsDisciplinasProfessor, data)
        'ShowInfoMessage(hasInsertedSucessfully, "Professor")
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

        Call New CursoPage().Show()
    End Sub

    Friend Sub DeleteAluno(idAluno As String)
        BusinessRules.DeleteAluno(idAluno)
    End Sub

    Friend Sub UpdateAluno(idsDisciplinasAluno As List(Of String))
        'Dim idAluno = SessionCookie.GetCookie("IdAluno")

        'BusinessRules.DeleteDisciplinasAluno(idAluno)

        'Dim data = ViewModelAluno.ConvertToDictionary()

        'data("Curso") = BusinessRules.GetAll(Table.Curso).Where(Function(dict) dict("Nome") = data("Curso")).First()("Id")

        'Dim hasInsertedSucessfully = Relation.SaveRelation(Table.Aluno, Table.Disciplina, idsDisciplinasAluno, data)
        'ShowInfoMessage(hasInsertedSucessfully, "Aluno")

        'ViewModelAluno.Clear()
    End Sub

    Public Sub CarregarAlunoParaEdicao(idAluno As String)
        Dim alunoData = BusinessRules.GetAllById(idAluno, Table.Aluno).First()

        ViewModelAluno.LoadFromDictionary(alunoData)

        Dim nomeCurso = BusinessRules.GetAllById(alunoData("Curso"), Table.Curso).First()("Nome")
        ViewModelAluno.Curso = nomeCurso

        SessionCookie.AddCookie("IdAluno", idAluno)
    End Sub

    Friend Function GetDisciplinasAcimaSemestre(idCurso As String, semestre As Integer) As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, idCurso)
        disciplinas = disciplinas.Where(Function(disciplina) disciplina("Semester") >= semestre)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = True
        Next

        Return ConvertDictionaryToDataTable(disciplinas)
    End Function

    Friend Function GetDisciplinasAluno(idAluno As String) As DataTable
        Dim aluno = BusinessRules.GetAllById(idAluno, Table.Aluno).First()

        Dim idDisciplinasAluno = BusinessRules.GetDisciplinas(Table.Aluno, idAluno).Select(Function(dict) dict("Id"))

        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, aluno("Curso")).
                                    Where(Function(disciplina) disciplina("Semester") >= aluno("SemestreInicio"))

        For Each disciplina In disciplinas
            disciplina("IsChecked") = idDisciplinasAluno.Contains(disciplina("Id"))
        Next

        Return ConvertDictionaryToDataTable(disciplinas)
    End Function


    Private Sub ShowInfoMessage(hasInsertedSucessfully As Boolean, keyWord As String)
        If hasInsertedSucessfully Then
            View.DisplayInfo(keyWord & " adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar " & keyWord.ToLower())
        End If
    End Sub

    Friend Function LoadCursosAlunoComboBox() As IEnumerable
        Return GenerateComboBoxItems(Function() GetAll(Table.Curso), "Nome", "Id")
    End Function

    Friend Function GetDataTableWithCheckboxColumn(table As String) As Object
        Dim data = GetAll(table)

        For Each dict In data
            dict("IsChecked") = False
        Next

        Return ConvertDictionaryToDataTable(data)
    End Function

    Friend Function SelecionarImagemAluno() As ImageSource
    End Function
End Class
