Imports System.Data

Public Class PresenterNotasAluno
    Inherits Presenter

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Function GetNotasAluno() As DataView
        Dim idAluno = SessionCookie.GetCookie("UserId")

        Dim data = BusinessRules.GetNotasAluno(idAluno)

        data = BusinessRules.RemoveKeyFromDict(data, "IdAluno")
        Return ConvertDictionaryToDataView(data)
    End Function
End Class
