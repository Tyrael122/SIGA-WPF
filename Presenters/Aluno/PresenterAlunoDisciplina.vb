Imports System.Data
Imports System.IO

Public Class PresenterAlunoDisciplina
    Inherits Presenter

    Private ReadOnly idAluno As String

    Public Sub New(view As IView)
        Me.View = view

    Friend Function GetDisciplinasCadastradas() As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Aluno, SessionCookie.GetCookie("userId"))

        Return ConvertDictionariesToDataTable(disciplinas)
    End Function



    Public Sub DisplayImage(imageBytes As Byte(), imageBox As Ellipse)
        If imageBytes IsNot Nothing AndAlso imageBytes.Length > 0 Then
            Dim bitmapImage As New BitmapImage()

            Using memoryStream As New MemoryStream(imageBytes)
                bitmapImage.BeginInit()
                bitmapImage.CacheOption = BitmapCacheOption.OnLoad
                bitmapImage.StreamSource = memoryStream
                bitmapImage.EndInit()
            End Using

            imageBox.Fill = New ImageBrush(bitmapImage)
        Else
            ' Caso não haja imagem, pode exibir uma imagem padrão ou limpar a imagem existente
            imageBox.Fill = Brushes.Transparent
        End If
    End Sub

    Public Function ConvertStringToBytes(imageString As String) As Byte()
        Dim imageBytes As Byte() = Convert.FromBase64String(imageString)
        Return imageBytes
    End Function
    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)
        Call New DisciplinaAlunoPage().Show()
    End Sub

    Friend Function GetNotasDisciplina() As DataTable
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim idProvasDaDisciplina = BusinessRules.GetAll(Table.Prova).
                                        Where(Function(prova) prova("IdDisciplina") = idDisciplina).
                                        Select(Function(prova) prova("Id"))

        Dim notas = BusinessRules.GetAll(Table.Nota)

        Dim idAluno = SessionCookie.GetCookie("userId")
        Dim rawData = notas.Where(Function(dict) dict("IdAluno") = idAluno And
                                idProvasDaDisciplina.Contains(dict("IdProva")))

        Return ConvertDictionaryToDataTable(rawData)
    End Function

    Friend Function GetPresencaDisciplina() As DataTable
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim presencasDaDisciplina = BusinessRules.GetAll(Table.Presenca).
                                        Where(Function(presenca) presenca("IdDisciplina") = idDisciplina And
                                                            presenca("IdAluno") = idAluno)

        Return ConvertDictionaryToDataTable(presencasDaDisciplina)
    End Function

    Friend Function CarregarImagemPerfilAluno() As Brush
        If 1 = 1 Then
            Return New ImageBrush()
        Else
            Return Brushes.Transparent
        End If
    End Function
End Class
