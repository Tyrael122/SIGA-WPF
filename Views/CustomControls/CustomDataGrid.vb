﻿' Follow steps 1a or 1b and then 2 to use this custom control in a XAML file.
'
' Step 1a) Using this custom control in a XAML file that exists in the current project.
' Add this XmlNamespace attribute to the root element of the markup file where it is 
' to be used:
'
'     xmlns:MyNamespace="clr-namespace:SIGAWPF"
'
'
' Step 1b) Using this custom control in a XAML file that exists in a different project.
' Add this XmlNamespace attribute to the root element of the markup file where it is 
' to be used:
'
'     xmlns:MyNamespace="clr-namespace:SIGAWPF;assembly=SIGAWPF"
'
' You will also need to add a project reference from the project where the XAML file lives
' to this project and Rebuild to avoid compilation errors:
'
'     Right click on the target project in the Solution Explorer and
'     "Add Reference"->"Projects"->[Browse to and select this project]
'
'
' Step 2)
' Go ahead and use your control in the XAML file. Note that Intellisense in the
' XML editor does not currently work on custom controls and its child elements.
'
'     <MyNamespace:CustomDataGrid/>
'

Public Class CustomDataGrid
    Inherits Control

    Shared Sub New()
        'This OverrideMetadata call tells the system that this element wants to provide a style that is different than its base class.
        'This style is defined in themes\generic.xaml
        DefaultStyleKeyProperty.OverrideMetadata(GetType(CustomDataGrid), New FrameworkPropertyMetadata(GetType(CustomDataGrid)))
    End Sub

    Public Overrides Sub OnApplyTemplate()
        MyBase.OnApplyTemplate()

        Dim resourceUri As New Uri("SIGAWPF\Views\CustomControls\ResourceDictionaries\CustomDataGrid.xaml", UriKind.RelativeOrAbsolute)
        Dim resourceDictionary As ResourceDictionary = CType(Application.LoadComponent(resourceUri), ResourceDictionary)
        Me.Resources.MergedDictionaries.Add(resourceDictionary)
    End Sub

End Class
