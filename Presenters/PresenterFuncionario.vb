Imports System.Data

Public Class PresenterFuncionario
    Inherits Presenter

    Private ViewModelAluno As New AlunoViewModel()
    Private ViewModelProfessor As New ProfessorViewModel()
    Private ViewModelDisciplina As New DisciplinaViewModel()
    Private ViewModelCurso As New CursoViewModel()
    Private idDisciplinasCurso As New List(Of String)
    Private idDisciplinasProfessor As New List(Of String)

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelAluno)
    End Sub

    Public Sub RegisterAluno(idsDisciplinasAluno As List(Of String))
        'Dim relation As New Relation(Table.Aluno, Table.Disciplina) With {
        '    .uniqueEntityData = data,
        '    .idEntitiesToRelate = idDisciplinasAluno
        '}

        'Dim hasInsertedSucessufully = relation.Save()
        'If hasInsertedSucessufully Then
        '    View.DisplayInfo("Aluno adicionado com sucesso!")
        'Else
        '    View.DisplayInfo("Erro ao adicionar aluno.")
        'End If
    End Sub

    Friend Sub RegisterProfessor()
        'Dim relation As New Relation(Table.Professor, Table.Disciplina) With {
        '    .uniqueEntityData = Data,
        '    .idEntitiesToRelate = idDisciplinasProfessor
        '}

        'Dim hasInsertedSucessufully = relation.Save()
        'If hasInsertedSucessufully Then
        '    View.DisplayInfo("Professor adicionado com sucesso!")
        'Else
        '    View.DisplayInfo("Erro ao adicionar professor.")
        'End If
    End Sub

    Friend Sub RegisterCurso()
        'Dim relation As New Relation(Table.Curso, Table.Disciplina) With {
        '    .uniqueEntityData = courseData,
        '    .idEntitiesToRelate = idDisciplinasCurso
        '}

        'Dim hasInsertedSucessufully = relation.Save()
        'If hasInsertedSucessufully Then
        '    View.DisplayInfo("Curso adicionado com sucesso!")
        'Else
        '    View.DisplayInfo("Erro ao adicionar curso.")
        'End If
    End Sub

    Friend Sub RegisterDisciplina()
        ' TODO: Presenter should pass the treated data to the business rules, in form of a dictionary.
        ' Where should the mechanism to convert a ViewModel to a dictionary as to be used by the DAL?
        ' In the ViewModel itself? In the businessRules layer?
        ' Ask ChatGPT about it. Maybe it should be at the ViewModel itself.
        Dim hasInsertedSucessufully = BusinessRules.Save(ViewModelDisciplina, Table.Disciplina)

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

    Friend Sub UpdateAluno(idsDisciplinasAluno As List(Of String))
        Dim idAluno = SessionCookie.GetCookie("IdAluno")

        BusinessRules.DeleteDisciplinasAluno(idAluno)

        Dim data = BusinessRules.ConvertViewModelToDictionary(ViewModelAluno)

        data("Curso") = BusinessRules.GetAll(Table.Curso).Where(Function(dict) dict("Nome") = data("Curso")).First()("Id")

        Dim relation As New Relation(Table.Aluno, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idEntitiesToRelate = idsDisciplinasAluno
        }

        Dim hasInsertedSucessufully = relation.Update(idAluno)
        If hasInsertedSucessufully Then
            View.DisplayInfo("Aluno atualizado com sucesso!")
        Else
            View.DisplayInfo("Erro ao atualizar aluno.")
        End If

        ViewModelAluno.Clear()
    End Sub

    Public Sub CarregarAlunoParaEdicao(idAluno As String)
        Dim alunoData = BusinessRules.GetAllById(idAluno, Table.Aluno).First()

        ' TODO: Create method to convert ViewModel to a Dictionary and vice-versa.

        ViewModelAluno.Login = alunoData("Login")
        ViewModelAluno.Password = alunoData("Password")
        ViewModelAluno.SemestreInicio = alunoData("SemestreInicio")

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

        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Friend Function GetDisciplinasAluno(idAluno As String) As DataTable
        Dim aluno = BusinessRules.GetAllById(idAluno, Table.Aluno).First()

        Dim idDisciplinasAluno = BusinessRules.GetDisciplinas(Table.Aluno, idAluno).Select(Function(dict) dict("Id"))

        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, aluno("Curso")).
                                    Where(Function(disciplina) disciplina("Semester") >= aluno("SemestreInicio"))

        For Each disciplina In disciplinas
            disciplina("IsChecked") = idDisciplinasAluno.Contains(disciplina("Id"))
        Next

        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Friend Function LoadCursosAlunoComboBox() As IEnumerable
        Return LoadComboBox(Function() GetAll(Table.Curso), "Nome", "Id")
    End Function
End Class
