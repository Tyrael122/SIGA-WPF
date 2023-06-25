Imports System.Data

Public Class PresenterFuncionarioAluno
    Inherits Presenter

    Public ViewModelAluno As New AlunoViewModel()
    Private alunoBusinessRules As New AlunoBusinessRules()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelAluno)

        ViewModelAluno.Foto = PresenterUtils.CarregarFotoVazia()
    End Sub

    Public Sub RegisterAluno(idsDisciplinasAluno As IEnumerable(Of String))
        Dim data = ViewModelAluno.ConvertToDictionary()
        data("Curso") = ModelUtils.GetAll(Table.Curso).Where(Function(dict) dict("Nome") = ViewModelAluno.Curso).First()("Id")

        AlunoBusinessRules.RegisterAluno(data, idsDisciplinasAluno)

        ViewModelAluno.Clear()
    End Sub

    Friend Function LoadCursosAlunoComboBox() As IEnumerable(Of ComboBoxItem)
        Return PresenterUtils.GenerateComboBoxItems(Function() GetAll(Table.Curso), "Nome", "Id")
    End Function

    Public Function GetDisciplinas() As DataView
        If ViewModelAluno.Curso Is Nothing Then
            Return Nothing
        End If

        Dim queryData = ModelUtils.GetAll(Table.Curso).Where(Function(dict) dict("Nome") = ViewModelAluno.Curso)
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

        Return PresenterUtils.ConvertDictionaryToDataView(disciplinas)
    End Function

    Private Function GetDisciplinasCurso(idCurso As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim disciplinas = ModelUtils.GetDisciplinas(Table.Curso, idCurso)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = True
        Next

        Return disciplinas
    End Function

    Private Function GetDisciplinasAluno(idAluno As String, idCurso As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim idDisciplinasAluno = ModelUtils.GetDisciplinas(Table.Aluno, idAluno).Select(Function(dict) dict("Id"))

        Dim disciplinas = ModelUtils.GetDisciplinas(Table.Curso, idCurso)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = idDisciplinasAluno.Contains(disciplina("Id"))
        Next

        Return disciplinas
    End Function

    Friend Sub DeleteAluno(idAluno As String)
        AlunoBusinessRules.DeleteAluno(idAluno)
    End Sub

    Friend Sub UpdateAluno(idsDisciplinasAluno As List(Of String))
        Dim data = ViewModelAluno.ConvertToDictionary()
        data("Curso") = ModelUtils.GetAll(Table.Curso).Where(Function(dict) dict("Nome") = ViewModelAluno.Curso).First()("Id")
        data.Remove("Id")

        alunoBusinessRules.UpdateAluno(data, idsDisciplinasAluno)

        ViewModelAluno.Clear()
    End Sub

    Public Sub CarregarAlunoParaEdicao(idAluno As String)
        Dim alunoData = ModelUtils.LoadUserWithPhotoById(idAluno, Table.Aluno)

        ViewModelAluno.LoadFromDictionary(alunoData)

        Dim nomeCurso = ModelUtils.FindById(alunoData("Curso"), Table.Curso).First()("Nome")
        ViewModelAluno.Curso = nomeCurso
    End Sub
End Class
