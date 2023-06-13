Imports System.Data
Imports System.IO

Public MustInherit Class Presenter
    Protected View As IView

    Private Const EmptyUserImagePath As String = "Views\Images\user-icon-removebg-preview.png"

    Protected Function CarregarFotoVazia() As ImageSource
        Dim baseDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Dim projectFolder = New Uri(Directory.GetParent(baseDirectory).Parent.Parent.ToString())

        Dim imageUri = New Uri(projectFolder, EmptyUserImagePath)
        Return New BitmapImage(imageUri)
    End Function

    Protected Function ConvertImageToByteArray(foto As ImageSource) As Byte()
        Dim bitmapSource As BitmapSource = TryCast(foto, BitmapSource)
        If bitmapSource Is Nothing Then
            Throw New ArgumentException("The image provided is invalid")
        End If

        Dim encoder As New PngBitmapEncoder()
        encoder.Frames.Add(BitmapFrame.Create(bitmapSource))

        Using memoryStream As New MemoryStream()
            encoder.Save(memoryStream)
            Return memoryStream.ToArray()
        End Using
    End Function

    Protected Sub ShowWindowAndCloseCurrent(window As Window, view As IView)
        window.Show()
        view.CloseView()
    End Sub

    Protected Function ConvertDictionaryToDataView(data As IEnumerable(Of IDictionary(Of String, Object))) As DataView
        Return ConvertDictionaryToDataTable(data).DefaultView
    End Function

    Private Function ConvertDictionaryToDataTable(data As IEnumerable(Of IDictionary(Of String, Object))) As DataTable
        Dim dataTable As New DataTable()

        If Not data.Any() Then
            Return dataTable
        End If

        For Each key In data(0).Keys
            dataTable.Columns.Add(key)
        Next

        For Each dict In data
            Dim newRow As DataRow = dataTable.NewRow()
            For Each keyValuePair In dict
                newRow(keyValuePair.Key) = keyValuePair.Value
            Next

            dataTable.Rows.Add(newRow)
        Next

        Return dataTable
    End Function

    Protected Function GenerateComboBoxItems(selector As Func(Of IEnumerable(Of IDictionary(Of String, Object))), content As String, tag As String) As IEnumerable(Of ComboBoxItem)
        Dim comboBoxItems As New List(Of ComboBoxItem)

        For Each dict In selector()
            Dim comboBoxItem As New ComboBoxItem With {
                .content = dict(content),
                .tag = dict(tag)
            }
            comboBoxItems.Add(comboBoxItem)
        Next

        Return comboBoxItems
    End Function

    Protected Function GetAll(table As Table) As IEnumerable(Of IDictionary(Of String, Object))
        Return BusinessRules.GetAll(table)
    End Function

    Protected Function GetDataTable(table As Table) As DataTable
        Dim data = GetAll(table)
        Return ConvertDictionaryToDataTable(data)
    End Function

    Public Function GetAll(tableStr As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim table As Table = [Enum].Parse(GetType(Table), tableStr)
        Return GetAll(table)
    End Function

    Public Function GetAllById(id As String, tableStr As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim table As Table = [Enum].Parse(GetType(Table), tableStr)
        Return BusinessRules.GetAllById(id, table)
    End Function

    Private Function GetDataTable(tableStr As String) As DataTable
        Dim table As Table = [Enum].Parse(GetType(Table), tableStr)
        Return GetDataTable(table)
    End Function

    Public Function GetDataView(tableStr As String) As DataView
        Return GetDataTable(tableStr).DefaultView
    End Function

    Protected Function GetAllDisciplinasPorSemestre(idCurso As String, semestre As Integer) As IEnumerable(Of IDictionary(Of String, Object))
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, idCurso)

        Return disciplinas.Where(Function(disciplina) disciplina("Semester") = semestre)
    End Function

    Public Function GetDataViewWithCheckboxColumn(tableStr As String, defaultValue As Boolean) As DataView
        Dim data = GetAll(tableStr)

        For Each dict In data
            dict.Add("IsChecked", defaultValue)
        Next

        Return ConvertDictionaryToDataView(data)
    End Function
End Class
