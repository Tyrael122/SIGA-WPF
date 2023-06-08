Imports System.Data
Imports System.IO

Public Class PresenterFuncionarioAluno
    Inherits Presenter

    Private Const EmptyUserImagePath As String = "C:\Users\Suporte\OneDrive - Fatec Centro Paula Souza\Programs\VB\SIGA\SIGAWPF\Views\Images\user-icon-removebg-preview.png"
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

    Private Function ConvertImageToByteArray(foto As ImageSource) As Byte()
        Dim memoryStream = New MemoryStream()
        Dim converter = New ImageSourceConverter()

        converter.ConvertTo(foto, GetType(Byte()))

        Dim bytes = memoryStream.ToArray()
    End Function

    Private Function CarregarFotoVazia() As ImageSource


        Return New BitmapImage(New Uri(EmptyUserImagePath, UriKind.Absolute))
    End Function

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
