﻿Friend Class PresenterFuncionarioAviso
    Inherits Presenter

    Private ViewModelAviso As New AvisoViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelAviso)
    End Sub

    Friend Sub RegisterAviso()
        Dim data = ViewModelAviso.ConvertToDictionary()

        ModelUtils.Save(data, Table.Aviso)

        ViewModelAviso.Clear()
    End Sub

    Friend Sub DeleteAviso(idAviso As String)
        ModelUtils.Delete(idAviso, Table.Aviso)
    End Sub
End Class
