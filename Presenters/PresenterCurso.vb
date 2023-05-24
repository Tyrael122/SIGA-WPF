Friend Class PresenterCurso
    Inherits Presenter

    Public Sub New(View As IView)
        Me.View = View
    End Sub

    Friend Sub RegisterHorarioCurso(map As Dictionary(Of String, String))
        map("DiaSemana") = [Enum].Parse(GetType(DiaSemana), map("DiaSemana"))
        map("IdCurso") = SessionCookie.GetCookie("IdCurso")

        BusinessRules.Save(map, Table.Horario)
    End Sub

    Public Function LoadDisciplinasPorSemestre(semestre As Integer) As IEnumerable(Of ComboBoxItem)
        Dim idCurso = SessionCookie.GetCookie("IdCurso")
        Return LoadComboBox(Function() GetAllDisciplinasPorSemestre(idCurso, semestre), "Name", "Id")
    End Function

    Friend Function LoadProfessoresPorDisciplina(idDisciplina As Object) As IEnumerable(Of ComboBoxItem)
        Dim idCurso = SessionCookie.GetCookie("IdCurso")

        Dim relation = New Relation(Table.Disciplina, Table.Professor)
        Dim professores = relation.GetAllMultipleEntitiesById(idDisciplina)

        Return LoadComboBox(Function() relation.GetAllMultipleEntitiesById(idDisciplina), "Login", "Id")
    End Function
End Class
