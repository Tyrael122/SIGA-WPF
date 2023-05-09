Friend Class PresenterCurso
    Inherits Presenter

    Private View As IView

    Public Sub New(View As IView)
        Me.View = View
    End Sub

    Public Function LoadDisciplinasCurso(semestre As Integer) As IEnumerable(Of ComboBoxItem)
        Dim idCurso = SessionCookie.GetCookie("IdCurso")
        Return LoadComboBox(Function() GetAllDisciplinasPorSemestre(idCurso, semestre), "Name", "Id")
    End Function
End Class
