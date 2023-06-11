Imports System.ComponentModel

Public MustInherit Class ViewModel
    Implements INotifyPropertyChanged

    Public Event PropertyChanged As PropertyChangedEventHandler Implements INotifyPropertyChanged.PropertyChanged

    Protected Overridable Sub OnPropertyChanged(propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub

    Public Sub Clear()
        For Each prop In Me.GetType().GetProperties()
            If prop.PropertyType Is GetType(String) Then
                prop.SetValue(Me, Nothing)
            End If
        Next
    End Sub

    Public Function ConvertToDictionary() As Dictionary(Of String, Object)
        Dim dict As New Dictionary(Of String, Object)

        For Each viewModelProperty In Me.GetType().GetProperties()
            Dim value = viewModelProperty.GetValue(Me)

            If value Is Nothing Then
                Continue For
            End If

            dict(viewModelProperty.Name) = value
        Next

        Return dict
    End Function

    Public Sub LoadFromDictionary(dict As Dictionary(Of String, Object))
        For Each viewModelProperty In Me.GetType().GetProperties()
            Try
                Dim data = dict(viewModelProperty.Name)

                If IsDBNull(data) Then
                    Continue For
                End If

                If viewModelProperty.PropertyType = GetType(String) Then
                    dict(viewModelProperty.Name) = data.ToString()
                End If

                viewModelProperty.SetValue(Me, dict(viewModelProperty.Name))
            Catch ex As KeyNotFoundException
                Continue For
            End Try
        Next
    End Sub
End Class
