Friend Class PresenterFuncionarioAviso
    Inherits Presenter

    Private ViewModelAviso As New AvisoViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelAviso)
    End Sub

    Friend Sub RegisterAviso()
        Dim data = ViewModelAviso.ConvertToDictionary()

        BusinessRules.Save(data, Table.Aviso)

        ViewModelAviso.Clear()
    End Sub
End Class
