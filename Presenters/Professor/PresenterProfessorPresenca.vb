Imports System.Data

Public Class PresenterProfessorPresenca
    Inherits Presenter

    Private ViewModelAula As New AulaViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelAula)
    End Sub

    Public Function GetAllPresencaAlunosCadastrados() As DataView
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim entityRelation = New Relation(Table.Disciplina, Table.Aluno)
        Dim alunosCadastrados = entityRelation.GetAllMultipleEntitiesById(idDisciplina)

        Dim idProfessor = SessionCookie.GetCookie("userId")

        Dim aulas = ModelUtils.GetAll(Table.Aula).Where(Function(dict) dict("IdHorario") = ViewModelAula.IdHorario And
                                                                dict("IdProfessor") = idProfessor And
                                                                dict("IdDisciplina") = idDisciplina And
                                                                dict("Data") = ViewModelAula.Data)
        Dim idAula = Nothing
        If aulas.Any() Then
            idAula = aulas.First()("Id")
        End If

        For Each aluno In alunosCadastrados
            Dim presencaData = ModelUtils.GetAll(Table.Presenca).Where(Function(dict) dict("IdAula") = idAula And
                                                                                    dict("IdAluno") = aluno("Id"))

            If presencaData.Any() Then
                aluno("IsPresente") = presencaData.First()("IsPresente")
            Else
                aluno("IsPresente") = True
            End If
        Next

        alunosCadastrados = ModelUtils.RemoveKeyFromDict(alunosCadastrados, "Password")

        Return PresenterUtils.ConvertDictionaryToDataView(alunosCadastrados)
    End Function

    Friend Function LoadDiaAulaComboBox() As IEnumerable(Of String)
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim horarios = ModelUtils.GetAll(Table.Horario).
                             Where(Function(horario) horario("IdDisciplina") = idDisciplina)

        Dim startDate As New DateTime(2023, 2, 2)
        Dim endDate As New DateTime(2023, 6, 30)

        Dim targetDaysOfWeek As IEnumerable(Of DayOfWeek) = horarios.Select(Of DayOfWeek)(Function(dict) [Enum].Parse(GetType(DayOfWeek), dict("DiaSemana")))

        Dim currentDate As Date = startDate
        Dim matchedDates As New List(Of Date)()

        While currentDate <= endDate
            If targetDaysOfWeek.Contains(currentDate.DayOfWeek) Then
                matchedDates.Add(currentDate)
            End If

            currentDate = currentDate.AddDays(1)
        End While

        Return matchedDates.Select(Function(matchedDate) matchedDate.ToString("dd-MM-yyyy"))
    End Function

    Friend Function LoadHorariosComboBox() As IEnumerable(Of ComboBoxItem)
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim dayOfWeek = Date.Parse(ViewModelAula.Data).DayOfWeek

        Dim selector = Function()
                           Return ModelUtils.GetAll(Table.Horario).
                                                Where(Function(horario) horario("IdDisciplina") = idDisciplina And
                                                horario("DiaSemana") = dayOfWeek)
                       End Function

        Dim comboBoxItems As New List(Of ComboBoxItem)

        For Each dict In selector()
            Dim comboBoxItem As New ComboBoxItem With {
                .Content = dict("HorarioInicio").ToString() & " - " & dict("HorarioFim").ToString(),
                .Tag = dict("Id")
            }
            comboBoxItems.Add(comboBoxItem)
        Next

        Return comboBoxItems
    End Function

    Friend Sub RegisterPresencas(presencas As List(Of IDictionary(Of String, Object)))
        Dim data = ViewModelAula.ConvertToDictionary()

        Dim dataAula As Date = ViewModelAula.Data

        data("Data") = dataAula.ToString("yyyy-MM-dd")
        data("IdProfessor") = SessionCookie.GetCookie("userId")
        data("IdDisciplina") = SessionCookie.GetCookie("idDisciplina")

        Dim idAula = AulaBusinessRules.SalvarAula(data)

        For Each presenca In presencas
            presenca("IdAluno") = presenca("Id")
            presenca.Remove("Id")

            presenca("IdAula") = idAula

            ModelUtils.Save(presenca, Table.Presenca)
        Next

        ViewModelAula.Clear()
    End Sub
End Class
