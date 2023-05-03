Public Class Aluno
    Inherits UserDAO
    Implements IDAO

    Private _Curso As String
    Public Property Curso As String
        Get
            Return _Curso
        End Get
        Set(value As String)
            _Curso = value
        End Set
    End Property

    Protected Overrides Function GetFieldsToParse() As String()
        Dim data As String() = MyBase.GetFieldsToParse()

        Dim additionalData As String() =
            {NameOf(Curso)}

        For Each field In additionalData
            data = data.Append(field).ToArray()
        Next

        Return data
    End Function
End Class
