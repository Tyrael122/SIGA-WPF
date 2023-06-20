Imports System.ComponentModel

Public Class ImagePicker
    Inherits UserControl

    Public Shared ReadOnly ImageSourceProperty As DependencyProperty = DependencyProperty.Register(
            NameOf(ImageSource),
            GetType(ImageSource),
            GetType(ImagePicker),
            New PropertyMetadata(Nothing))

    Public Property ImageSource As ImageSource
        Get
            Return CType(GetValue(ImageSourceProperty), ImageSource)
        End Get
        Set(value As ImageSource)
            SetValue(ImageSourceProperty, value)
        End Set
    End Property

    Private Sub imgPerfil_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles imgPerfil.MouseLeftButtonDown
        Dim imageSource As ImageSource = LoadImagePickerDialog()

        If imageSource IsNot Nothing Then
            Me.ImageSource = imageSource
        End If
    End Sub
End Class
