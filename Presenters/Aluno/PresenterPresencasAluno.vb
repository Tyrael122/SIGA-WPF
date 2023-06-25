Imports System.Data

Friend Class PresenterPresencasAluno
    Inherits Presenter

    Private alunoBusinessRules As New AlunoBusinessRules()

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Function GetPresencasAluno() As DataView
        Dim data = alunoBusinessRules.GetPresencasAluno()

        Return PresenterUtils.ConvertDictionaryToDataView(data)
    End Function
End Class
