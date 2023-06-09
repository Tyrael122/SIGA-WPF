Public Class ImagePicker
    Public Shared ReadOnly ImageSourceProperty As DependencyProperty =
        DependencyProperty.Register(
                "ImageSource",
                GetType(ImageSource),
                GetType(ImagePicker))

    Public Property ImageSource As ImageSource
        Get
            Return CType(GetValue(ImageSourceProperty), ImageSource)
        End Get
        Set(value As ImageSource)
            SetValue(ImageSourceProperty, value)
        End Set
    End Property

    Private Sub imgPerfil_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs) Handles imgPerfil.MouseLeftButtonDown
        Dim backGround As New ImageBrush With {
            .ImageSource = LoadImagePickerDialog()
        }

        imgPerfil.Fill = backGround
    End Sub
End Class
