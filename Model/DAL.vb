Imports System.Data.SqlClient

Public Class DAL
    Implements IDAL, IDisposable

    Private ReadOnly connection As SqlConnection
    Private sqlCommand As SqlCommand
    Private sqlDataReader As SqlDataReader

    Public Sub New()
        connection = New SqlConnection(Environment.GetEnvironmentVariable("StringConnection"))
        connection.Open()
    End Sub

    Public Function SelectAll(table As Table) As List(Of IDictionary(Of String, String)) Implements IDAL.SelectAll
        Return SelectFields(table, "*")
    End Function

    Public Function SelectFields(table As Table, ParamArray fieldsToSelect() As String) As List(Of IDictionary(Of String, String)) Implements IDAL.SelectFields
        Dim sql = "SELECT " & String.Join(", ", fieldsToSelect) & " FROM " & table.ToString()

        sqlCommand = New SqlCommand(sql, connection)
        sqlDataReader = sqlCommand.ExecuteReader()

        Dim result = ParseResultIntoDictionary(sqlDataReader)
        sqlDataReader.Close()

        Return result
    End Function


    Public Function Save(data As IDictionary, table As Table) As Boolean Implements IDAL.Save
        sqlDataReader = ExecuteInsertQuery(data, table)

        Dim rowsAffected As Integer = 0
        While sqlDataReader.Read()
            rowsAffected += 1
        End While

        sqlDataReader.Close()

        Return rowsAffected = 1
    End Function

    Public Function SaveWithOutput(data As IDictionary, table As Table) As List(Of IDictionary(Of String, String)) Implements IDAL.SaveWithOutput
        sqlDataReader = ExecuteInsertQuery(data, table)

        Dim result = ParseResultIntoDictionary(sqlDataReader)
        sqlDataReader.Close()

        Return result
    End Function

    Public Sub Delete(idEntity As String, table As Table) Implements IDAL.Delete
        Delete(idEntity, "Id", table)
    End Sub

    Public Sub Delete(idEntity As String, idField As String, table As Table) Implements IDAL.Delete
        Dim sql = "DELETE FROM " & table.ToString() & " WHERE " & idField & " = " & idEntity

        sqlCommand = New SqlCommand(sql, connection)
        sqlCommand.ExecuteNonQuery()
    End Sub

    Public Sub Update(id As String, data As IDictionary, table As Table) Implements IDAL.Update
        Dim sql = "UPDATE " & table.ToString() & " SET " & GenerateUpdatePairs(data) & " WHERE Id = " & id

        sqlCommand = New SqlCommand(sql, connection)
        sqlCommand.ExecuteNonQuery()
    End Sub

    Private Function ExecuteInsertQuery(data As IDictionary, table As Table) As SqlDataReader
        Dim sql = "INSERT INTO " & table.ToString() & " (" & GetParseableFields(data) & ") OUTPUT INSERTED.* "
        sql += "VALUES (" & GetParseableData(data) & ")"

        Return New SqlCommand(sql, connection).ExecuteReader()
    End Function


    Private Function ParseResultIntoDictionary(sqlDataReader As SqlDataReader) As List(Of IDictionary(Of String, String))
        Dim result As New List(Of IDictionary(Of String, String))
        While sqlDataReader.Read()
            Dim rowData As New Dictionary(Of String, String)

            Dim columns = sqlDataReader.GetColumnSchema()
            For Each column In columns
                Dim rowValue = sqlDataReader(column.ColumnName)
                rowData.Add(column.ColumnName, rowValue.ToString())
            Next

            result.Add(rowData)
        End While

        Return result
    End Function

    Private Function GenerateUpdatePairs(data As IDictionary) As String
        Dim returnString = ""
        For Each key In data.Keys
            returnString += key & " = " & ParseSqlValue(data(key)) & ", "
        Next

        Return returnString.Remove(returnString.Length - 2)
    End Function

    Private Function GetParseableFields(data As IDictionary(Of String, String)) As String
        Dim temp As ICollection(Of String) = data.Keys

        Return String.Join(", ", temp.ToList())
    End Function

    Private Function GetParseableData(data As IDictionary(Of String, String)) As String
        Dim fieldsToSelect As String = Nothing
        For Each field In data.Values
            fieldsToSelect += ParseSqlValue(field) & ", "
        Next

        Return fieldsToSelect.Remove(fieldsToSelect.Length - 2)
    End Function

    Private Function ParseSqlValue(value As String) As String
        If IsNumeric(value) Then
            Return value
        Else
            Return "'" & value & "'"
        End If
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        connection.Close()
    End Sub


End Class
