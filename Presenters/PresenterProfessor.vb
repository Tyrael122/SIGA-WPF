Imports System.Data

Public Class PresenterProfessor
    Inherits Presenter

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
        'Dim cursos = BusinessRules.GetAll(Table.Curso)

        'Dim disciplinasCursos = BusinessRules.GetAll(Table.CursoDisciplina)

        'Dim selectorFunction = Function(disciplinaId, cursoId)
        '                           Return disciplinasCursos.Where(Function(dict) dict("IdCurso") = cursoId And
        '                                                              dict("IdDisciplina") = disciplinaId).First() IsNot Nothing
        '                       End Function




        'Dim data = disciplinas.Join(cursos, Function(disciplina) disciplina("Id"), Function(curso) curso("Id"), selectorFunction)
























        Dim idDisciplinas = disciplinas.Select(Function(dict) dict("Id"))

        'Dim idCursos = BusinessRules.GetAll(Table.CursoDisciplina).Where(Function(curso) idDisciplinas.Contains(curso("IdDisciplina"))).
        '                                                Select(Function(dict) dict("IdCurso"))

        'Dim cursos = BusinessRules.GetAll(Table.Curso).Where(Function(curso) idCursos.Contains(curso("Id"))).
        '                                                Select(Function(dict)
        '                                                           Return New Dictionary(Of String, String) From {
        '                                                                {"Id", dict("Id")},
        '                                                                {"Nome", dict("Nome")}
        '                                                           }
        '                                                       End Function)

        'Dim finalData = disciplinas.Select(Function(disciplina)
        '                                       Return New Dictionary(Of String, String) From {
        '                                           {"Id", disciplina("Id")},
        '                                           {"Name", disciplina("Name")},
        '                                           {"IdCurso", cursos.Where(Function(curso) idCursos.Contains(curso("Id"))).First()("Nome")},
        '                                           {"NomeCurso", cursos.First(Function(curso) curso("Id") = disciplina("IdCurso"))("Nome")}
        '                                       }
        '                                   End Function)

        Return ConvertDictionariesToDataTable(BusinessRules.GetJoin(String.Join(", ", idDisciplinas)))
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

    Friend Function LoadAulasComboBox() As IEnumerable(Of ComboBoxItem)
        Dim idDisciplina = SessionCookie.GetCookie("idDisciplina")


    End Function
End Class
