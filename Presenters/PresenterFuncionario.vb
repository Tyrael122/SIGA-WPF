Imports System.Runtime.CompilerServices

Public Class PresenterFuncionario
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub RegisterAluno(data As IDictionary)
        BusinessRules.RegisterAluno(data)
    End Sub

End Class
