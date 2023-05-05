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
            Where(Function(disc) Not disciplinasExcluidasAluno.Contains(disc) And disc.Semester >= data.Item("SemestreInicio")).ToList()

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

    Friend Sub AddDisciplinaSelecionadaAoCurso(discplina As Disciplina)
        disciplinasCurso.Add(discplina)
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

    Friend Function GetAllAlunos() As IEnumerable
        Return BusinessRules.GetAll(Of Aluno)(Table.Aluno)
    End Function

    Friend Function GetAllProfessores() As IEnumerable
        Return BusinessRules.GetAll(Of Professor)(Table.Professor)
    End Function

    Friend Function GetAllCursos() As IEnumerable
        Return BusinessRules.GetAll(Of Curso)(Table.Curso)
    End Function


    Friend Function GetAllCursosAsDict() As List(Of IDictionary(Of String, String))
        Return BusinessRules.GetAllCursosAsDict()
    End Function

    Friend Function GetAllDisciplinas() As IEnumerable(Of Disciplina)
        Return BusinessRules.GetAll(Of Disciplina)(Table.Disciplina)
    End Function

    Friend Function GetDisciplinasCursoSemestreInicio(idCurso As String, semestreInicio As Integer) As IEnumerable(Of Disciplina)
        Dim disciplinas = BusinessRules.GetDisciplinas(Of Curso)(idCurso)

        Return disciplinas.Where(Function(disciplina) disciplina.Semester >= semestreInicio)
    End Function
End Class
