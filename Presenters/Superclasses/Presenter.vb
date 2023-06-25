Imports System.Data

Public MustInherit Class Presenter
    Protected View As IView

    Public Sub ShowAviso(idAviso As String)
        Dim aviso As New AvisoPopUp(idAviso)
        aviso.Show()
    End Sub

    Public Function GetAll(tableStr As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim table As Table = [Enum].Parse(GetType(Table), tableStr)
        Return PresenterUtils.GetAll(table)
    End Function

    Public Function GetDataView(tableStr As String) As DataView
        Return PresenterUtils.GetDataTable(tableStr).DefaultView
    End Function

    Public Function GetDisciplinasComColunaCheckBox(defaultValue As Boolean) As DataView
        Dim disciplinas = GetAll(Table.Disciplina)

        For Each disciplina In disciplinas
            disciplina.Add("IsChecked", defaultValue)
        Next

        Return PresenterUtils.ConvertDictionaryToDataView(disciplinas)
    End Function
End Class
