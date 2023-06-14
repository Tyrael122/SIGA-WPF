Friend Class PresenterHorarioCurso
    Inherits Presenter

    Private ViewModelHorarioCurso As New HorarioCursoViewModel

    Public Sub New(View As IViewModel)
        Me.View = View

        View.SetDataContext(ViewModelHorarioCurso)
    End Sub

    Public Function LoadDisciplinasPorSemestre() As IEnumerable(Of ComboBoxItem)
        Dim idCurso = SessionCookie.GetCookie("IdCurso")

        Return GenerateComboBoxItems(Function() GetAllDisciplinasPorSemestre(idCurso, ViewModelHorarioCurso.Semestre), "Name", "Id")
    End Function

    Friend Function LoadProfessoresPorDisciplina() As IEnumerable(Of ComboBoxItem)
        Dim relation = New Relation(Table.Disciplina, Table.Professor)

        Return GenerateComboBoxItems(Function() relation.GetAllMultipleEntitiesById(ViewModelHorarioCurso.IdDisciplina), "Login", "Id")
    End Function

    Friend Sub RegisterHorarioCurso()
        Dim data = ViewModelHorarioCurso.ConvertToDictionary()

        data("DiaSemana") = [Enum].Parse(GetType(DiaSemana), data("DiaSemana"))
        data("IdCurso") = SessionCookie.GetCookie("IdCurso")

        BusinessRules.Save(data, Table.Horario)

        ViewModelHorarioCurso.Clear()
    End Sub
End Class
