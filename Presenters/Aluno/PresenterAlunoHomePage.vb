Imports System.Data
Imports System.IO

Public Class PresenterAlunoHomePage
    Inherits Presenter
    Private DAL As New DAL()

    Private ReadOnly idAluno As String

    Public Sub New(view As IView)
        Me.View = view

        idAluno = SessionCookie.GetCookie("userId")
    End Sub

    Friend Function GetDisciplinasCadastradas() As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Aluno, idAluno)

        Return ConvertDictionaryToDataTable(disciplinas)
    End Function

    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)
        Call New DisciplinaAlunoPage().Show()
    End Sub


    Public Function CarregarDadosDoAluno(campo As String) As String
        Dim user = DAL.SelectFields(Table.Aluno, campo)
        If user.Count > 0 AndAlso user(0).ContainsKey(campo) Then
            Dim valorCampo As String = user(0)(campo)
            Return valorCampo
        End If
        Return ""
    End Function

    Public Function CarregarImagemPerfilAluno() As ImageBrush
        Dim campoImagem As String = "ImagemPerfil"
        Dim user = DAL.SelectFields(Table.Aluno, campoImagem)
        If user.Count > 0 AndAlso user(0).ContainsKey(campoImagem) Then
            Dim imagemBytes As Byte() = Convert.FromBase64String(user(0)(campoImagem))
            If imagemBytes IsNot Nothing AndAlso imagemBytes.Length > 0 Then
                Dim bitmapImage As New BitmapImage()

                Using memoryStream As New MemoryStream(imagemBytes)
                    bitmapImage.BeginInit()
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad
                    bitmapImage.StreamSource = memoryStream
                    bitmapImage.EndInit()
                End Using

                Return New ImageBrush(bitmapImage)
            End If
        End If
        Return Nothing
    End Function

End Class
