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

    Public Function ConvertToDictionary() As Dictionary(Of String, String)
        Dim dict As New Dictionary(Of String, String)

        For Each viewModelProperty In Me.GetType().GetProperties()
            Try
                dict(viewModelProperty.Name) = viewModelProperty.GetValue(Me).ToString()
            Catch ex As NullReferenceException
                Continue For
            End Try
        Next


        Return dict
    End Function

    Public Sub LoadFromDictionary(dict As Dictionary(Of String, String))
        For Each viewModelProperty In Me.GetType().GetProperties()
            Try
                viewModelProperty.SetValue(Me, dict(viewModelProperty.Name))
            Catch ex As KeyNotFoundException
                Continue For
            End Try
        Next
    End Sub
End Class
