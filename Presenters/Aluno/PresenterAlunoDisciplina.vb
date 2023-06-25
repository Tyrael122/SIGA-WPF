Imports System.Data

Public Class PresenterAlunoDisciplina
    Inherits Presenter

    Private ReadOnly idAluno As String

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Function GetDisciplinasCadastradas() As DataView
        Dim disciplinas = ModelUtils.GetDisciplinas(Table.Aluno, SessionCookie.GetCookie("userId"))

        Return PresenterUtils.ConvertDictionaryToDataView(disciplinas)
    End Function

    Public Function ConvertStringToBytes(imageString As String) As Byte()
        Dim imageBytes As Byte() = Convert.FromBase64String(imageString)
        Return imageBytes
    End Function
    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)
        'Call New DisciplinaAlunoPage().Show()
    End Sub

    Friend Function GetNotasDisciplina() As DataView
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim idProvasDaDisciplina = ModelUtils.GetAll(Table.Prova).
                                        Where(Function(prova) prova("IdDisciplina") = idDisciplina).
                                        Select(Function(prova) prova("Id"))

        Dim notas = ModelUtils.GetAll(Table.Nota)

        Dim idAluno = SessionCookie.GetCookie("userId")
        Dim rawData = notas.Where(Function(dict) dict("IdAluno") = idAluno And
                                idProvasDaDisciplina.Contains(dict("IdProva")))

        Return PresenterUtils.ConvertDictionaryToDataView(rawData)
    End Function

    Friend Function GetPresencaDisciplina() As DataView
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim presencasDaDisciplina = ModelUtils.GetAll(Table.Presenca).
                                        Where(Function(presenca) presenca("IdDisciplina") = idDisciplina And
                                                            presenca("IdAluno") = idAluno)

        Return PresenterUtils.ConvertDictionaryToDataView(presencasDaDisciplina)
    End Function
End Class
