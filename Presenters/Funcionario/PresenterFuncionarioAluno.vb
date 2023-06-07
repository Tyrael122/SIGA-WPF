Imports System.Data

Public Class PresenterFuncionarioAluno
    Inherits Presenter

    Private ViewModelAluno As New AlunoViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelAluno)
    End Sub

    Friend Function LoadCursosAlunoComboBox() As IEnumerable
        Return GenerateComboBoxItems(Function() GetAll(Table.Curso), "Nome", "Id")
    End Function

    Friend Function GetDisciplinasCurso() As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, ViewModelAluno.Curso)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = True
        Next

        Return ConvertDictionaryToDataTable(disciplinas)
    End Function
End Class
