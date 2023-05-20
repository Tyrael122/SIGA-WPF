Imports System.Data

Public Class PresenterCadastroPresenca
    Inherits Presenter

    Private ViewModelAula As New AulaViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        view.SetDataContext(ViewModelAula)
    End Sub

    Public Function GetAllPresencaAlunosCadastrados() As DataTable
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim entityRelation = New Relation(Table.Disciplina, Table.Aluno)
        Dim alunosCadastrados = entityRelation.GetAllMultipleEntitiesById(idDisciplina)

        Dim idProfessor = SessionCookie.GetCookie("userId")

        Dim aulas = BusinessRules.GetAll(Table.Aula).Where(Function(dict) dict("IdHorario") = ViewModelAula.IdHorario And
                                                                dict("IdProfessor") = idProfessor And
                                                                dict("IdDisciplina") = idDisciplina)
        Dim idAula = Nothing
        If aulas.Any() Then
            idAula = aulas.First()("Id")
        End If

        For Each aluno In alunosCadastrados
            Dim presencaData = BusinessRules.GetAll(Table.Presenca).Where(Function(dict) dict("IdAula") = idAula And
                                                                                    dict("IdAluno") = aluno("Id"))

            If presencaData.Any() Then
                aluno("IsPresente") = presencaData.First()("IsPresente")

            Else
                aluno("IsPresente") = False
            End If
        Next

        Return ConvertDictionaryToDataTable(alunosCadastrados)
    End Function

    Friend Function LoadDiaAulaComoBox() As IEnumerable(Of String)
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim horarios = BusinessRules.GetAll(Table.Horario).
                             Where(Function(horario) horario("IdDisciplina") = idDisciplina)

        Dim startDate As Date = Date.Now
        Dim endDate As New DateTime(2023, 6, 30)

        Dim targetDates As IEnumerable(Of DayOfWeek) = horarios.Select(Of DayOfWeek)(Function(dict) [Enum].Parse(GetType(DayOfWeek), dict("DiaSemana")))

        Dim currentDate As Date = startDate
        Dim matchedDates As New List(Of Date)()

        While currentDate <= endDate
            If targetDates.Contains(currentDate.DayOfWeek) Then
                matchedDates.Add(currentDate)
            End If

            currentDate = currentDate.AddDays(1)
        End While

        Return matchedDates.Select(Function(matchedDate) matchedDate.ToString("dd-MM-yyyy"))
    End Function

    Friend Function LoadHorariosComboBox(data As String) As IEnumerable(Of ComboBoxItem)
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim dayOfWeek = Date.Parse(data).DayOfWeek

        Dim selector = Function()
                           Return BusinessRules.GetAll(Table.Horario).
                                                Where(Function(horario) horario("IdDisciplina") = idDisciplina And
                                                horario("DiaSemana") = dayOfWeek)
                       End Function

        Dim comboBoxItems As New List(Of ComboBoxItem)

        For Each dict In selector()
            Dim comboBoxItem As New ComboBoxItem With {
                .Content = dict("HorarioInicio") & " - " & dict("HorarioFim"),
                .Tag = dict("Id")
            }
            comboBoxItems.Add(comboBoxItem)
        Next

        Return comboBoxItems
    End Function

    Friend Sub RegisterPresencas(presencas As List(Of IDictionary(Of String, String)))
        Dim data = ViewModelAula.ConvertToDictionary()

        data("Data") = ViewModelAula.Data.ToString("yyyy-MM-dd")
        data("IdProfessor") = SessionCookie.GetCookie("userId")
        data("IdDisciplina") = SessionCookie.GetCookie("idDisciplina")

        Dim idAula = BusinessRules.SalvarAula(data)

        For Each presenca In presencas
            presenca("IdAluno") = presenca("Id")
            presenca.Remove("Id")

            presenca("IdAula") = idAula

            BusinessRules.Save(presenca, Table.Presenca)
        Next
    End Sub
End Class
