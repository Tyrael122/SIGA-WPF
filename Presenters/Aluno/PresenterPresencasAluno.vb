Imports System.Data

Friend Class PresenterPresencasAluno
    Inherits Presenter

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Function GetPresencasAluno() As DataView
        Dim idAluno = SessionCookie.GetCookie("UserId")

        Dim data = BusinessRules.GetPresencasAluno(idAluno)

        Return ConvertDictionaryToDataView(data)
    End Function
End Class
