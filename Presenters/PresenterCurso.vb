Friend Class PresenterCurso
    Inherits Presenter

    Private ViewModelHorarioCurso As New HorarioCursoViewModel

    Public Sub New(View As IViewModel)
        Me.View = View

        View.SetDataContext(ViewModelHorarioCurso)
    End Sub

    Friend Sub RegisterHorarioCurso()
        Dim data = ViewModelHorarioCurso.ConvertToDictionary()

        data("DiaSemana") = [Enum].Parse(GetType(DiaSemana), data("DiaSemana"))
        data("IdCurso") = SessionCookie.GetCookie("IdCurso")

        ' TODO: Figure out a way to clean this up.
        ' The problem is that I can't bind the tag of the auto-generated ComboBoxItems to the ViewModel,
        ' so I'm binding the text instead.
        data("IdProfessor") = BusinessRules.GetAll(Table.Professor).First(Function(dict) dict("Login") = data("IdProfessor"))("Id")
        data("IdDisciplina") = BusinessRules.GetAll(Table.Disciplina).First(Function(dict) dict("Name") = data("IdDisciplina"))("Id")

        BusinessRules.Save(data, Table.Horario)
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
