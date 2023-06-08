Imports System.Data
Imports System.IO

Public Class PresenterFuncionarioAluno
    Inherits Presenter

    Private ViewModelAluno As New AlunoViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelAluno)

        ViewModelAluno.Foto = CarregarFotoVazia()
    End Sub

    Public Sub RegisterAluno(idsDisciplinasAluno As IEnumerable(Of String))
        Dim data = ViewModelAluno.ConvertToDictionary()

        data("Foto") = ConvertImageToByteArray(ViewModelAluno.Foto)

        Relation.SaveRelation(Table.Aluno, Table.Disciplina, idsDisciplinasAluno, data)
    End Sub

    Friend Function LoadCursosAlunoComboBox() As IEnumerable
        Return GenerateComboBoxItems(Function() GetAll(Table.Curso), "Nome", "Id")
    End Function

    Friend Function GetDisciplinasCurso() As DataView
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, ViewModelAluno.Curso)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = True
        Next

        Return ConvertDictionaryToDataView(disciplinas)
    End Function

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
End Class
