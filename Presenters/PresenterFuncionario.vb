Public Class PresenterFuncionario
    Private View As IView
    Private disciplinasCurso As New List(Of Disciplina)
    Private disciplinasExcluidasAluno As New List(Of Disciplina)
    Private disciplinasProfessor As New List(Of Disciplina)

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub RegisterAluno(data As IDictionary)
        Dim disciplinasAluno = BusinessRules.GetDisciplinasCurso(data.Item("Curso")).
            Where(Function(disc) Not disciplinasExcluidasAluno.Contains(disc) And disc.Semester >= data.Item("SemestreInicio")).ToList()

        Dim hasInsertedSucessufully = BusinessRules.SaveWithRelation(data, disciplinasAluno, Table.Aluno, Table.AlunoDisciplina)

        If hasInsertedSucessufully Then
            View.DisplayInfo("Aluno adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar aluno.")
        End If
    End Sub

    Friend Sub RegisterProfessor(data As IDictionary(Of String, String))
        Dim hasInsertedSucessufully = BusinessRules.SaveWithRelation(data, disciplinasProfessor, UserType.Professor, Table.ProfessorDisciplina)

        If hasInsertedSucessufully Then
            View.DisplayInfo("Professor adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar professor.")
        End If
    End Sub

    Friend Sub RegisterCurso(courseData As IDictionary(Of String, String))
        Dim hasInsertedSucessufully = BusinessRules.SaveWithRelation(courseData, disciplinasCurso, Table.Curso, Table.CursoDisciplina)

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

    Friend Sub RemoveDisciplinaSelecionadaDoAluno(disciplina As Object)
        disciplinasExcluidasAluno.Add(disciplina)
    End Sub

    Friend Sub AddDisciplinaSelecionadaAoAluno(disciplina As Object)
        disciplinasExcluidasAluno.Remove(disciplina)
    End Sub

    Friend Sub AddDisciplinaSelecionadaAoProfessor(disciplina As Object)
        disciplinasProfessor.Add(disciplina)
    End Sub

    Friend Sub RemoveDisciplinaSelecionadaDoProfessor(disciplina As Object)
        disciplinasProfessor.Remove(disciplina)
    End Sub

    Friend Sub ShowDisciplinaPage(disciplina As Object)
        Dim disciplinaPage As Window = New DisciplinaProfessorPage(disciplina)

        disciplinaPage.Show()
    End Sub

    Friend Function GetAllAlunos() As IEnumerable
        Return BusinessRules.GetAllAlunos()
    End Function

    Friend Function GetAllProfessores() As IEnumerable
        Return BusinessRules.GetAllProfessores()
    End Function

    Friend Function GetAllCursos() As IEnumerable
        Return BusinessRules.GetAllCursos()
    End Function


    Friend Function GetAllCursosAsDict() As List(Of IDictionary(Of String, String))
        Return BusinessRules.GetAllCursosAsDict()
    End Function

    Friend Function GetAllDisciplinas() As IEnumerable(Of Disciplina)
        Return BusinessRules.GetAllDisciplinas()
    End Function

    Friend Function GetDisciplinasCursoSemestreInicio(curso As String, semestreInicio As Integer) As IEnumerable(Of Disciplina)
        Dim disciplinas = BusinessRules.GetDisciplinasCurso(curso)

        Return disciplinas.Where(Function(disciplina) disciplina.Semester >= semestreInicio)
    End Function

    Friend Function GetDisciplinasCadastradas() As IEnumerable
        Return BusinessRules.GetDisciplinasProfessor()
    End Function
End Class
