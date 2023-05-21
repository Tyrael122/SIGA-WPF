Imports System.Data
Imports System.Runtime.CompilerServices

Public Class PresenterFuncionario
    Inherits Presenter

    Private ViewModelAluno As New AlunoViewModel()
    Private ViewModelProfessor As New ProfessorViewModel()
    Private ViewModelDisciplina As New DisciplinaViewModel()
    Private ViewModelCurso As New CursoViewModel()
    Private View As IView
    Private idDisciplinasCurso As New List(Of String)
    Private idDisciplinasExcluidasAluno As New List(Of String)
    Private idDisciplinasProfessor As New List(Of String)

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub RegisterAluno()
        Dim idDisciplinasAluno = BusinessRules.GetDisciplinas(Table.Curso, ViewModelAluno.Curso).
            Where(Function(disciplina) Not idDisciplinasExcluidasAluno.Contains(disciplina("Id")) And disciplina("Semester") >= ViewModelAluno.SemestreInicio).
            Select(Function(disciplina) disciplina("Id")).
            ToList()

        'TODO: Refactor relation class to accept complex relation tables, and join clauses.

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
        ' Where should the mechanism to convert a ViewModel to a dictionary be,
        ' as to be used by the DAL? In the ViewModel itself? In the businessRules layer?
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

    Friend Sub UpdateAluno()
        Dim idDisciplinasAluno = BusinessRules.GetDisciplinas(Table.Curso, ViewModelAluno.Curso).
                Where(Function(disciplina) Not idDisciplinasExcluidasAluno.Contains(disciplina("Id")) And disciplina("Semester") >= ViewModelAluno.SemestreInicio).
                Select(Function(disciplina) disciplina("Id")).
                ToList()

        Dim idAluno = SessionCookie.GetCookie("IdAluno")

        BusinessRules.DeleteDisciplinasAluno(idAluno)

        'Dim relation As New Relation(Table.Aluno, Table.Disciplina) With {
        '    .uniqueEntityData = Data,
        '    .idEntitiesToRelate = idDisciplinasAluno
        '}

        'Dim hasInsertedSucessufully = relation.Update(idAluno)
        'If hasInsertedSucessufully Then
        '    View.DisplayInfo("Aluno atualizado com sucesso!")
        'Else
        '    View.DisplayInfo("Erro ao atualizar aluno.")
        'End If
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

    Friend Function GetDisciplinasAcimaSemestreDataTable(idCurso As String, semestre As Integer) As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, idCurso)
        disciplinas = disciplinas.Where(Function(disciplina) disciplina("Semester") >= semestre)

        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Friend Function GetDisciplinasAluno(idCurso As String, semestre As Integer, idAluno As String) As DataTable
        idDisciplinasExcluidasAluno = New List(Of String)

        Dim idDisciplinasAluno = BusinessRules.GetDisciplinas(Table.Aluno, idAluno).Select(Function(dict) dict("Id"))

        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, idCurso).
                                    Where(Function(disciplina) disciplina("Semester") >= semestre)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = idDisciplinasAluno.Contains(disciplina("Id"))
        Next

        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Friend Function LoadCursosAlunoComboBox() As IEnumerable
        Return LoadComboBox(Function() GetAll(Table.Curso), "Nome", "Id")
    End Function

    Friend Function GetWindowDataContext() As Object
        ViewModelAluno.Login = "Bananas"
        Return ViewModelAluno
    End Function
End Class
