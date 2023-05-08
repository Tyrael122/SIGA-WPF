Imports System.Data
Imports System.Data.Common
Imports System.Security.Principal

Public Class PresenterFuncionario
    Inherits Presenter

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

    Friend Function GetDisciplinasCursoSemestreInicio(idCurso As String, semestreInicio As Integer) As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, idCurso)

        disciplinas = disciplinas.Where(Function(disciplina) disciplina("Semester") >= semestreInicio)

        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Friend Function LoadCursosAlunoComboBox() As IEnumerable
        Return LoadComboBox(Function() GetAll(Table.Curso), "Nome", "Id")
    End Function
End Class
