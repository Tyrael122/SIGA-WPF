Public Class BindingText
    Inherits UserControl

    Public Shared ReadOnly TextProperty As DependencyProperty =
            DependencyProperty.Register(NameOf(TextOne),
                            GetType(String),
                            GetType(BindingText),
                            New FrameworkPropertyMetadata(
                                String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault))

    Public Property TextOne As Object
        Get
            Return CType(GetValue(TextProperty), String)
        End Get

        Set(value As Object)
            SetValue(TextProperty, value)
        End Set
    End Property

    Public Sub New()
        InitializeComponent()
    End Sub
End Class
