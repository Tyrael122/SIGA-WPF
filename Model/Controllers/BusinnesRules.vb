Imports System.Collections.ObjectModel
Imports System.Data
Imports System.Data.SqlClient

Public Class BusinessRules
    Private Shared ReadOnly dataBridge As IDAL = New DAL() ' TODO: Search for a way to cleanly dispose of the connection created by the IDAL.

    Friend Shared Function Save(data As IDictionary, table As Table) As Boolean
        Return dataBridge.Save(data, table)
    End Function

    Friend Shared Function GetAll(table As Table) As IEnumerable(Of IDictionary(Of String, String))
        Return dataBridge.SelectAll(table)
    End Function

    Friend Shared Function GetDisciplinas(table As Table, idEntity As String) As IEnumerable(Of IDictionary(Of String, String))
        Dim relation As New Relation(table, Table.Disciplina)

        Dim relationColumns = relation.GetRelationColumns()

        Dim idDisciplinas = dataBridge.SelectAll(relation.GetRelationTable()).
            Where(Function(dict) dict(relationColumns.uniqueEntity) = idEntity).
            Select(Function(dict) dict(relationColumns.multipleEntity))

        Return GetAll(Table.Disciplina).Where(Function(disciplina) idDisciplinas.Contains(disciplina("Id")))
    End Function

    Friend Shared Function GetJoin(idDisciplinas As String) As IEnumerable(Of IDictionary(Of String, String))
        Dim connection = New SqlConnection(Environment.GetEnvironmentVariable("StringConnection"))
        connection.Open()

        Dim sql = "SELECT Curso.Id AS CursoId, Disciplina.Id AS DisciplinaId, Curso.Nome, Disciplina.Name
                    FROM Curso
                    JOIN CursoDisciplina ON Curso.Id = CursoDisciplina.IdCurso
                    JOIN Disciplina ON CursoDisciplina.IdDisciplina = Disciplina.Id
                    WHERE Disciplina.Id IN (" & idDisciplinas & ")"

        Dim sqlDataReader = New SqlCommand(sql, connection).ExecuteReader()

        Return New DAL().ParseResultIntoDictionary(sqlDataReader)
    End Function
End Class
