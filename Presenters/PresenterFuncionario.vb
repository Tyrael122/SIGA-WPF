Imports System.Runtime.CompilerServices

Public Class PresenterFuncionario
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub RegisterAluno(data As IDictionary)
        Dim hasInsertedSucessufully = BusinessRules.RegisterEntity(data, UserType.Aluno)

        If hasInsertedSucessufully Then
            View.DisplayInfo("Aluno adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar aluno.")
        End If
    End Sub

    Friend Sub RegisterProfessor(data As IDictionary(Of String, String))
        Dim hasInsertedSucessufully = BusinessRules.RegisterEntity(data, UserType.Professor)

        If hasInsertedSucessufully Then
            View.DisplayInfo("Professor adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar professor.")
        End If
    End Sub

    Friend Sub RegisterCurso(data As IDictionary(Of String, String))
        Dim hasInsertedSucessufully = BusinessRules.RegisterEntity(data, Table.Curso)
        If hasInsertedSucessufully Then
            View.DisplayInfo("Curso adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar curso.")
        End If
    End Sub

    Friend Function GetAllAlunos() As IEnumerable
        Return BusinessRules.GetAllAlunos()
    End Function

    Friend Function GetAllProfessores() As IEnumerable
        Return BusinessRules.GetAllProfessores()
    End Function
End Class
