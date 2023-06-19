Imports System.Data
Imports System.IO

Public Class PresenterFuncionarioAluno
    Inherits Presenter

    Public ViewModelAluno As New AlunoViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelAluno)

        ViewModelAluno.Foto = CarregarFotoVazia()
    End Sub

    Public Sub RegisterAluno(idsDisciplinasAluno As IEnumerable(Of String))
        Dim data = ViewModelAluno.ConvertToDictionary()

        data("Curso") = BusinessRules.GetAll(Table.Curso).Where(Function(dict) dict("Nome") = ViewModelAluno.Curso).First()("Id")
        data("Foto") = ConvertImageToByteArray(ViewModelAluno.Foto)

        Relation.SaveRelation(Table.Aluno, Table.Disciplina, idsDisciplinasAluno, data)

        ViewModelAluno.Clear()
    End Sub

    Friend Function LoadCursosAlunoComboBox() As IEnumerable
        Return GenerateComboBoxItems(Function() GetAll(Table.Curso), "Nome", "Id")
    End Function

    Public Function GetDisciplinas() As DataView
        If ViewModelAluno.Curso Is Nothing Then
            Return Nothing
        End If

        Dim queryData = BusinessRules.GetAll(Table.Curso).Where(Function(dict) dict("Nome") = ViewModelAluno.Curso)
        If Not queryData.Any() Then
            Return Nothing
        End If

        Dim idCurso = queryData.First()("Id")
        Dim disciplinas As IEnumerable(Of IDictionary(Of String, Object)) = Nothing
        Try
            Dim idAluno = SessionCookie.GetCookie("IdAluno")
            disciplinas = GetDisciplinasAluno(idAluno, idCurso)

        Catch ex As KeyNotFoundException
            disciplinas = GetDisciplinasCurso(idCurso)
        End Try

        Return ConvertDictionaryToDataView(disciplinas)
    End Function

    Private Function GetDisciplinasCurso(idCurso As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, idCurso)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = True
        Next

        Return disciplinas
    End Function

    Private Function GetDisciplinasAluno(idAluno As String, idCurso As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim aluno = BusinessRules.GetAllById(idAluno, Table.Aluno).First()

        Dim idDisciplinasAluno = BusinessRules.GetDisciplinas(Table.Aluno, idAluno).Select(Function(dict) dict("Id"))

        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, idCurso)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = idDisciplinasAluno.Contains(disciplina("Id"))
        Next

        Return disciplinas
    End Function

    Friend Sub DeleteAluno(idAluno As String)
        BusinessRules.DeleteAluno(idAluno)
    End Sub

    Friend Sub UpdateAluno(idsDisciplinasAluno As List(Of String))
        Dim idAluno = SessionCookie.GetCookie("IdAluno")

        BusinessRules.DeleteDisciplinasAluno(idAluno)

        Dim data = ViewModelAluno.ConvertToDictionary()
        data("Curso") = BusinessRules.GetAll(Table.Curso).Where(Function(dict) dict("Nome") = ViewModelAluno.Curso).First()("Id")

        Dim relation As New Relation(Table.Aluno, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idRelatedEntites = idsDisciplinasAluno
        }

        relation.Update(idAluno)

        ViewModelAluno.Clear()
    End Sub

    Public Sub CarregarAlunoParaEdicao(idAluno As String)
        Dim alunoData = BusinessRules.GetAllById(idAluno, Table.Aluno).First()

        ViewModelAluno.LoadFromDictionary(alunoData)

        Dim nomeCurso = BusinessRules.GetAllById(alunoData("Curso"), Table.Curso).First()("Nome")
        ViewModelAluno.Curso = nomeCurso

        SessionCookie.AddCookie("IdAluno", idAluno)
    End Sub
End Class
