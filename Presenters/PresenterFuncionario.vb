Imports System.Data

Public Class PresenterFuncionario
    Private View As IView
    Private disciplinasCurso As New List(Of Disciplina)
    Private disciplinasExcluidasAluno As New List(Of Disciplina)
    Private disciplinasProfessor As New List(Of Disciplina)

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub RegisterAluno(data As IDictionary)
        Dim disciplinasAluno = BusinessRules.GetDisciplinas(Of Curso)(data.Item("Curso")).
            Where(Function(disc) Not disciplinasExcluidasAluno.Contains(disc) And disc("Semester") >= data.Item("SemestreInicio")).ToList()

        Dim relation As New Relation(Of Aluno, Disciplina) With {
            .uniqueEntityData = data,
            .entitiesToRelate = disciplinasAluno
        }

        Dim hasInsertedSucessufully = relation.Save()
        If hasInsertedSucessufully Then
            View.DisplayInfo("Aluno adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar aluno.")
        End If
    End Sub

    Friend Sub RegisterProfessor(data As IDictionary(Of String, String))
        Dim relation As New Relation(Of Aluno, Disciplina) With {
            .uniqueEntityData = data,
            .entitiesToRelate = disciplinasProfessor
        }

        Dim hasInsertedSucessufully = relation.Save()
        If hasInsertedSucessufully Then
            View.DisplayInfo("Professor adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar professor.")
        End If
    End Sub

    Friend Sub RegisterCurso(courseData As IDictionary(Of String, String))
        Dim relation As New Relation(Of Aluno, Disciplina) With {
            .uniqueEntityData = courseData,
            .entitiesToRelate = disciplinasCurso
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

    Friend Sub AddDisciplinaSelecionadaAoCurso(disciplina As Disciplina)
        disciplinasCurso.Add(disciplina)
    End Sub

    Friend Sub RemoveDisciplinaSelecionadaDoCurso(disciplina As Disciplina)
        disciplinasCurso.Remove(disciplina)
    End Sub

    Friend Sub RemoveDisciplinaSelecionadaDoAluno(disciplina As Disciplina)
        disciplinasExcluidasAluno.Add(disciplina)
    End Sub

    Friend Sub AddDisciplinaSelecionadaAoAluno(disciplina As Disciplina)
        disciplinasExcluidasAluno.Remove(disciplina)
    End Sub

    Friend Sub AddDisciplinaSelecionadaAoProfessor(disciplina As Disciplina)
        disciplinasProfessor.Add(disciplina)
    End Sub

    Friend Sub RemoveDisciplinaSelecionadaDoProfessor(disciplina As Disciplina)
        disciplinasProfessor.Remove(disciplina)
    End Sub

    Friend Function GetAllAlunos() As DataTable
        Dim alunos = BusinessRules.GetAll(Table.Aluno)
        Return ConvertDictionariesToDataTable(alunos)
    End Function

    Friend Function GetAllProfessores() As DataTable
        Dim professores = BusinessRules.GetAll(Table.Professor)
        Return ConvertDictionariesToDataTable(professores)
    End Function

    Friend Function GetAllCursos() As DataTable
        Dim cursos = BusinessRules.GetAll(Table.Curso)
        Return ConvertDictionariesToDataTable(cursos)
    End Function

    Friend Function GetAllCursosAsDict() As List(Of IDictionary(Of String, String))
        Return BusinessRules.GetAll(Table.Curso)
    End Function

    Friend Function GetAllDisciplinas() As DataTable
        Dim disciplinas = BusinessRules.GetAll(Table.Disciplina)
        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Friend Function GetDisciplinasCursoSemestreInicio(idCurso As String, semestreInicio As Integer) As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Of Curso)(idCurso)

        disciplinas = disciplinas.Where(Function(disciplina) disciplina("Semester") >= semestreInicio)

        Return ConvertDictionariesToDataTable(disciplinas)
    End Function
End Class
