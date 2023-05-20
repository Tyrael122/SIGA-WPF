Imports System.Data

Public Class PresenterFuncionario
    Inherits Presenter

    Private ViewModel As New AlunoViewModel()
    Private View As IView
    Private idDisciplinasCurso As New List(Of String)
    Private idDisciplinasExcluidasAluno As New List(Of String)
    Private idDisciplinasProfessor As New List(Of String)

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub RegisterAluno(data As IDictionary)
        Dim idDisciplinasAluno = BusinessRules.GetDisciplinas(Table.Curso, data("Curso")).
            Where(Function(disciplina) Not idDisciplinasExcluidasAluno.Contains(disciplina("Id")) And disciplina("Semester") >= data("SemestreInicio")).
            Select(Function(disciplina) disciplina("Id")).
            ToList()

        Dim relation As New Relation(Table.Aluno, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idEntitiesToRelate = idDisciplinasAluno
        }

        Dim hasInsertedSucessufully = relation.Save()
        If hasInsertedSucessufully Then
            View.DisplayInfo("Aluno adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar aluno.")
        End If
    End Sub

    Friend Sub RegisterProfessor(data As IDictionary(Of String, String))
        Dim relation As New Relation(Table.Professor, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idEntitiesToRelate = idDisciplinasProfessor
        }

        Dim hasInsertedSucessufully = relation.Save()
        If hasInsertedSucessufully Then
            View.DisplayInfo("Professor adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar professor.")
        End If
    End Sub

    Friend Sub RegisterCurso(courseData As IDictionary(Of String, String))
        Dim relation As New Relation(Table.Curso, Table.Disciplina) With {
            .uniqueEntityData = courseData,
            .idEntitiesToRelate = idDisciplinasCurso
        }

        Dim hasInsertedSucessufully = relation.Save()
        If hasInsertedSucessufully Then
            View.DisplayInfo("Curso adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar curso.")
        End If
    End Sub

    Friend Sub RegisterDisciplina(data As IDictionary(Of String, String))
        Dim hasInsertedSucessufully = BusinessRules.Save(data, Table.Disciplina)

        If hasInsertedSucessufully Then
            View.DisplayInfo("Disciplina adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar disciplina.")
        End If
    End Sub

    Friend Sub AddDisciplinaSelecionadaAoCurso(idDisciplina As String)
        idDisciplinasCurso.Add(idDisciplina)
    End Sub

    Friend Sub RemoveDisciplinaSelecionadaDoCurso(idDisciplina As String)
        idDisciplinasCurso.Remove(idDisciplina)
    End Sub

    Friend Sub RemoveDisciplinaSelecionadaDoAluno(idDisciplina As String)
        idDisciplinasExcluidasAluno.Add(idDisciplina)
    End Sub

    Friend Sub AddDisciplinaSelecionadaAoAluno(idDisciplina As String)
        idDisciplinasExcluidasAluno.Remove(idDisciplina)
    End Sub

    Friend Sub AddDisciplinaSelecionadaAoProfessor(idDisciplina As String)
        idDisciplinasProfessor.Add(idDisciplina)
    End Sub

    Friend Sub RemoveDisciplinaSelecionadaDoProfessor(idDisciplina As String)
        idDisciplinasProfessor.Remove(idDisciplina)
    End Sub

    Friend Sub ShowCursoPage(idCurso As String)
        SessionCookie.AddCookie("IdCurso", idCurso)

        Call New CursoPage().Show()
    End Sub

    Friend Sub DeleteAluno(idAluno As String)
        BusinessRules.DeleteAluno(idAluno)
    End Sub

    Friend Sub EditarAluno(tag As Object)
        Throw New NotImplementedException()
    End Sub

    Friend Sub UpdateAluno(data As IDictionary(Of String, String))
        Dim idDisciplinasAluno = BusinessRules.GetDisciplinas(Table.Curso, data("Curso")).
                Where(Function(disciplina) Not idDisciplinasExcluidasAluno.Contains(disciplina("Id")) And disciplina("Semester") >= data("SemestreInicio")).
                Select(Function(disciplina) disciplina("Id")).
                ToList()

        Dim idAluno = SessionCookie.GetCookie("idAluno")
        data("Id") = idAluno

        BusinessRules.DeleteDisciplinasAluno(idAluno)

        Dim relation As New Relation(Table.Aluno, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idEntitiesToRelate = idDisciplinasAluno
        }

        Dim hasInsertedSucessufully = relation.Update(idAluno)
        If hasInsertedSucessufully Then
            View.DisplayInfo("Aluno atualizado com sucesso!")
        Else
            View.DisplayInfo("Erro ao atualizar aluno.")
        End If
    End Sub

    Friend Function GetDisciplinasPorSemestreDataTable(idCurso As String, semestre As Integer) As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, idCurso)
        disciplinas = disciplinas.Where(Function(disciplina) disciplina("Semester") = semestre)

        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Friend Function GetDisciplinasAluno(idCurso As String, semestre As Integer, idAluno As String) As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, idCurso)
        disciplinas = disciplinas.Where(Function(disciplina) disciplina("Semester") >= semestre)

        Dim idDisciplinasAluno = BusinessRules.GetDisciplinas(Table.Aluno, idAluno).Select(Function(dict) dict("Id"))

        For Each disciplina In disciplinas
            disciplina("IsChecked") = idDisciplinasAluno.Contains(disciplina("Id"))
        Next

        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Friend Function LoadCursosAlunoComboBox() As IEnumerable
        Return LoadComboBox(Function() GetAll(Table.Curso), "Nome", "Id")
    End Function

    Friend Function GetWindowDataContext() As Object
        ViewModel.Login = "Bananas"
        Return ViewModel
    End Function
End Class
