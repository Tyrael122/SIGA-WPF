Public Class DisciplinaProfessorPage

    Sub New(disciplina As Disciplina)

        ' This call is required by the designer.
        InitializeComponent()

        ' Add any initialization after the InitializeComponent() call.
        txtDisciplinaNome.Content = disciplina.Name

    End Sub
End Class
