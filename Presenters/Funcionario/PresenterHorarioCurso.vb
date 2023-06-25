Friend Class PresenterHorarioCurso
    Inherits Presenter

    Private ViewModelHorarioCurso As New HorarioCursoViewModel
    Private cursoBusinessRules As New CursoBusinessRules()

    Public Sub New(View As IViewModel)
        Me.View = View

        View.SetDataContext(ViewModelHorarioCurso)
    End Sub

    Public Function LoadDisciplinasPorSemestre() As IEnumerable(Of ComboBoxItem)
        Return PresenterUtils.GenerateComboBoxItems(Function() cursoBusinessRules.GetAllDisciplinasPorSemestre(ViewModelHorarioCurso.Semestre), "Name", "Id")
    End Function

    Friend Function LoadProfessoresPorDisciplina() As IEnumerable(Of ComboBoxItem)
        Dim relation = New Relation(Table.Disciplina, Table.Professor)

        Return PresenterUtils.GenerateComboBoxItems(Function() relation.GetAllMultipleEntitiesById(ViewModelHorarioCurso.IdDisciplina), "Login", "Id")
    End Function

    Friend Sub RegisterHorarioCurso()
        Dim data = ViewModelHorarioCurso.ConvertToDictionary()
        data("DiaSemana") = [Enum].Parse(GetType(DiaSemana), data("DiaSemana"))

        cursoBusinessRules.RegisterHorarioCurso(data)

        ViewModelHorarioCurso.Clear()
    End Sub

    Friend Function LoadHorarioInicio() As IEnumerable(Of ComboBoxItem)
        Dim turno = cursoBusinessRules.GetTurnoFromCurso()

        Select Case turno
            Case Turno.Matutino
                Return GenerateHorarioInicioManha()
            Case Turno.Vespertino
                Return GenerateHorarioInicioTarde()
            Case Turno.Noturno
                Return GenerateHorarioInicioNoite()
        End Select

        Return Nothing
    End Function

    Friend Function LoadHorarioFim() As IEnumerable(Of ComboBoxItem)
        Dim turno = cursoBusinessRules.GetTurnoFromCurso()

        Select Case turno
            Case Turno.Matutino
                Return GenerateHorarioFimManha()
            Case Turno.Vespertino
                Return GenerateHorarioFimTarde()
            Case Turno.Noturno
                Return GenerateHorarioFimNoite()
        End Select

        Return Nothing
    End Function

    Private Function GenerateHorarioFimManha() As IEnumerable(Of ComboBoxItem)
        Return PresenterUtils.GenerateComboBoxItems({"8:40", "10:30", "12:00"})
    End Function

    Private Function GenerateHorarioFimTarde() As IEnumerable(Of ComboBoxItem)
        Return PresenterUtils.GenerateComboBoxItems({"14:40", "16:30", "18:20"})
    End Function

    Private Function GenerateHorarioFimNoite() As IEnumerable(Of ComboBoxItem)
        Return PresenterUtils.GenerateComboBoxItems({"20:40", "22:30"})
    End Function

    Private Function GenerateHorarioInicioManha() As IEnumerable(Of ComboBoxItem)
        Return PresenterUtils.GenerateComboBoxItems({"7:00", "8:50", "10:40"})
    End Function

    Private Function GenerateHorarioInicioTarde() As IEnumerable(Of ComboBoxItem)
        Return PresenterUtils.GenerateComboBoxItems({"13:00", "14:50", "16:40"})
    End Function

    Private Function GenerateHorarioInicioNoite() As IEnumerable(Of ComboBoxItem)
        Return PresenterUtils.GenerateComboBoxItems({"19:00", "20:50"})
    End Function
End Class
