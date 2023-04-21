Imports System.Runtime.CompilerServices

Public Class PresenterFuncionario
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub RegisterAluno(data As IDictionary)
        Dim hasInsertedSucessufully = BusinessRules.RegisterAluno(data)

        If hasInsertedSucessufully Then
            View.DisplayInfo("Aluno adicionado com sucesso!")
        Else
            View.DisplayInfo("Erro ao adicionar aluno.")
        End If
    End Sub
End Class
