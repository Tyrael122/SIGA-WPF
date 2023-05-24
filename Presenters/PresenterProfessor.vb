Imports System.Data

Public Class PresenterProfessor
    Inherits Presenter

    Private IdAlunosFaltantes As New List(Of String)
    Private View As IView

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Friend Sub ShowDisciplinaPage(idDisciplina As String)
        SessionCookie.AddCookie("idDisciplina", idDisciplina)

        Call New DisciplinaProfessorPage().Show()
    End Sub

    Function LoadProvasComboBox() As IEnumerable(Of ComboBoxItem)
        Return LoadComboBox(Function() GetAllProvas(), "Data", "Id")
    End Function

    Friend Function GetDisciplinasCadastradas() As DataTable
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Professor, SessionCookie.GetCookie("userId"))
        Return ConvertDictionariesToDataTable(disciplinas)
    End Function

    Public Function GetAllAlunosCadastrados() As DataTable
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim entityRelation = New Relation(Table.Disciplina, Table.Aluno)
        Dim alunosCadastrados = entityRelation.GetAllMultipleEntitiesById(idDisciplina)

        Return ConvertDictionariesToDataTable(alunosCadastrados)
    End Function

    Friend Sub RegisterProva(data As IDictionary(Of String, String))
        Dim dataProva As Date = data("Data")
        data("Data") = dataProva.ToString("yyyy-MM-dd")

        data("Tipo") = [Enum].Parse(GetType(TipoProva), data("Tipo"))

        data.Add("IdDisciplina", SessionCookie.GetCookie("idDisciplina"))
        data.Add("IdProfessor", SessionCookie.GetCookie("userId"))
        BusinessRules.Save(data, Table.Prova)
    End Sub

    Friend Function GetAllProvas() As IEnumerable(Of IDictionary(Of String, String))
        Dim provas = GetAll(Table.Prova)

        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Return provas.Where(Function(dict) dict("IdDisciplina") = idDisciplina)
    End Function

    Public Sub RegisterNotas(data As IDictionary(Of String, Object))
        Dim idProva = data("IdProva")
        Dim notas = data("Notas")

        For Each nota In notas
            Dim savebleData = New Dictionary(Of String, String) From {
                {"IdProva", idProva},
                {"IdAluno", nota("IdAluno")},
                {"Nota", nota("Nota")}
            }

            BusinessRules.Save(savebleData, Table.Nota)
        Next
    End Sub

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

        'Dim comboBoxItems As New List(Of ComboBoxItem)

        'For Each matchedDate In matchedDates
        '    Dim diaSemana = [Enum].Parse(GetType(DiaSemana), matchedDate.DayOfWeek)

        '    Dim comboBoxItem As New ComboBoxItem With {
        '        .Content = matchedDate.ToString("dd-MM-yyyy")
        '    }
        '    comboBoxItems.Add(comboBoxItem)
        'Next

        'Return comboBoxItems
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

    Friend Sub DarFaltaParaAluno(idAluno As String)
        IdAlunosFaltantes.Add(idAluno)
    End Sub

    Friend Sub DarPresencaParaAluno(idAluno As String)
        IdAlunosFaltantes.Remove(idAluno)
    End Sub

    Friend Sub RegisterPresencas(map As Dictionary(Of String, String))
        ' TODO: Change this to use ViewModels.

        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")
        Dim aulaData = New Dictionary(Of String, String) From {
                {"IdDisciplina", idDisciplina},
                {"IdHorario", map("IdHorario")},
                {"Data", Date.Parse(map("Data")).ToString("yyyy-MM-dd")},
                {"IdProfessor", SessionCookie.GetCookie("userId")}
            }

        Dim idAula = BusinessRules.SaveWithOutput(aulaData, Table.Aula).First()("Id")

        map("IdAula") = idAula

        map.Remove("Data")
        map.Remove("IdHorario")

        Dim entityRelation = New Relation(Table.Disciplina, Table.Aluno)
        Dim alunosCadastrados = entityRelation.GetAllMultipleEntitiesById(idDisciplina)

        For Each aluno In alunosCadastrados
            Dim isAlunoPresente = Not IdAlunosFaltantes.Contains(aluno("Id"))
            map("IdAluno") = aluno("Id")
            map("EstaPresente") = isAlunoPresente

            BusinessRules.Save(map, Table.Presenca)
        Next
    End Sub

    Friend Function GetAllNotasAlunosCadastrados(idProva As String) As Object
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")

        Dim entityRelation = New Relation(Table.Disciplina, Table.Aluno)
        Dim alunosCadastrados = entityRelation.GetAllMultipleEntitiesById(idDisciplina)

        For Each aluno In alunosCadastrados
            Dim idAluno = aluno("Id")
            Dim dadosNota = BusinessRules.GetAll(Table.Nota).Where(Function(dict) dict("IdAluno") = idAluno And
                                                                                dict("IdProva") = idProva).FirstOrDefault()
            aluno("Nota") = dadosNota("Nota")
        Next

        Return ConvertDictionariesToDataTable(alunosCadastrados)
    End Function
End Class
