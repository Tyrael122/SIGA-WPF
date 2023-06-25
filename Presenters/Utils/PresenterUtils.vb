Imports System.Data
Imports System.IO

Public Class PresenterUtils
    Private Const EmptyUserImagePath As String = "Views\Images\user-icon-removebg-preview.png"

    Public Shared Function CarregarFotoVazia() As ImageSource
        Dim baseDirectory As String = AppDomain.CurrentDomain.BaseDirectory
        Dim projectFolder = New Uri(Directory.GetParent(baseDirectory).Parent.Parent.ToString())

        Dim imageUri = New Uri(projectFolder, EmptyUserImagePath)
        Return New BitmapImage(imageUri)
    End Function

    Public Shared Sub LoadUserInfo(viewModel As ViewModel, table As Table)
        Dim idUser = SessionCookie.GetCookie("userId")

        Dim data = ModelUtils.FindById(idUser, table).First()

        If data.ContainsKey("Foto") Then
            If IsDBNull(data("Foto")) Then
                data("Foto") = CarregarFotoVazia()
            Else
                data("Foto") = ConvertByteArrayToImage(data("Foto"))
            End If
        End If

        viewModel.LoadFromDictionary(data)
    End Sub

    Public Shared Function ConvertImageToByteArray(foto As ImageSource) As Byte()
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

    Public Shared Function ConvertByteArrayToImage(byteArray As Byte()) As ImageSource
        'Dim imageSourceConverter As New ImageSourceConverter()
        'Return DirectCast(imageSourceConverter.ConvertFrom(byteArray), ImageSource)
        Using stream As New MemoryStream(byteArray)
            Dim imageSource As New BitmapImage()
            imageSource.BeginInit()
            imageSource.StreamSource = stream
            imageSource.CacheOption = BitmapCacheOption.OnLoad
            imageSource.EndInit()
            imageSource.Freeze()
            Return imageSource
        End Using
    End Function

    Public Shared Sub ShowWindowAndCloseCurrent(window As Window, view As IView)
        window.Show()
        view.CloseView()
    End Sub

    Public Shared Function ConvertDictionaryToDataView(data As IEnumerable(Of IDictionary(Of String, Object))) As DataView
        Return ConvertDictionaryToDataTable(data).DefaultView
    End Function

    Private Shared Function ConvertDictionaryToDataTable(data As IEnumerable(Of IDictionary(Of String, Object))) As DataTable
        Dim dataTable As New DataTable()

        If Not data.Any() Then
            Return dataTable
        End If

        For Each key In data(0).Keys
            If key = "Foto" Then
                Dim dataColumn As New DataColumn(key, GetType(ImageSource))
                dataTable.Columns.Add(dataColumn)
                Continue For
            End If

            dataTable.Columns.Add(key)
        Next

        For Each dict In data
            Dim newRow As DataRow = dataTable.NewRow()
            For Each keyValuePair In dict

                If keyValuePair.Value.GetType() Is GetType(Byte()) Then
                    newRow(keyValuePair.Key) = ConvertByteArrayToImage(keyValuePair.Value)
                    Continue For
                End If

                newRow(keyValuePair.Key) = keyValuePair.Value
            Next

            dataTable.Rows.Add(newRow)
        Next

        Return dataTable
    End Function

    Public Shared Function GenerateComboBoxItems(selector As Func(Of IEnumerable(Of IDictionary(Of String, Object))), content As String, tag As String) As IEnumerable(Of ComboBoxItem)
        Dim comboBoxItems As New List(Of ComboBoxItem)

        For Each dict In selector()
            Dim comboBoxItem As New ComboBoxItem With {
                .Content = dict(content),
                .Tag = dict(tag)
            }

            comboBoxItems.Add(comboBoxItem)
        Next

        Return comboBoxItems
    End Function

    Public Shared Function GenerateComboBoxItems(data As IEnumerable(Of Object)) As IEnumerable(Of ComboBoxItem)
        Dim comboBoxItems As New List(Of ComboBoxItem)

        For Each item In data
            Dim comboBoxItem As New ComboBoxItem With {
                .Content = item
            }

            comboBoxItems.Add(comboBoxItem)
        Next

        Return comboBoxItems
    End Function

    Public Shared Function RemoveKeyFromDict(data As IEnumerable(Of IDictionary(Of String, Object)), ParamArray keysToRemove As String()) As IEnumerable(Of IDictionary(Of String, Object))
        For Each keyToRemove In keysToRemove
            data = data.Select(Function(dict)
                                   If dict.ContainsKey(keyToRemove) Then
                                       dict.Remove(keyToRemove)
                                   End If
                                   Return dict
                               End Function).ToList()
        Next

        Return data
    End Function

    Public Shared Function GetAll(table As Table) As IEnumerable(Of IDictionary(Of String, Object))
        Return ModelUtils.GetAll(table)
    End Function

    Private Shared Function GetDataTable(table As Table) As DataTable
        Dim data = GetAll(table)
        Return ConvertDictionaryToDataTable(data)
    End Function

    Public Shared Function GetDataTable(tableStr As String) As DataTable
        Dim table As Table = [Enum].Parse(GetType(Table), tableStr)
        Return GetDataTable(table)
    End Function
End Class
