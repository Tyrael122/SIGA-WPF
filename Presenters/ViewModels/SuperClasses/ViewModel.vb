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
End Class
