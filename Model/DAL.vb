Imports System.Data.SqlClient
Imports System.Text

Public Class DAL
    Implements IDAL, IDisposable

    Private ReadOnly connection As SqlConnection
    Private sqlCommand As SqlCommand
    Private sqlDataReader As SqlDataReader

    Public Sub New()
        connection = New SqlConnection(Environment.GetEnvironmentVariable("StringConnection"))
        connection.Open()
    End Sub

    Public Function SelectAll(table As Table) As IEnumerable(Of IDictionary(Of String, Object)) Implements IDAL.SelectAll
        Return SelectFields(table, "*")
    End Function

    Public Function SelectFields(table As Table, ParamArray fieldsToSelect() As String) As IEnumerable(Of IDictionary(Of String, Object)) Implements IDAL.SelectFields
        Dim sql = "SELECT " & String.Join(", ", fieldsToSelect) & " FROM " & table.ToString()

        sqlCommand = New SqlCommand(sql, connection)
        sqlDataReader = sqlCommand.ExecuteReader()

        Dim result = ParseResultIntoDictionary(sqlDataReader)
        sqlDataReader.Close()

        Return result
    End Function

    Public Function Save(data As IDictionary(Of String, Object), table As Table) As Boolean Implements IDAL.Save
        sqlDataReader = ExecuteInsertQuery(data, table)

        Dim rowsAffected As Integer = 0
        While sqlDataReader.Read()
            rowsAffected += 1
        End While

        sqlDataReader.Close()

        Return rowsAffected = 1
    End Function

    Public Function SaveWithOutput(data As IDictionary, table As Table) As IEnumerable(Of IDictionary(Of String, Object)) Implements IDAL.SaveWithOutput
        sqlDataReader = ExecuteInsertQuery(data, table)

        Dim result = ParseResultIntoDictionary(sqlDataReader)
        sqlDataReader.Close()

        Return result
    End Function

    Public Sub Delete(idEntity As String, table As Table) Implements IDAL.Delete
        Delete(idEntity, "Id", table)
    End Sub

    Public Sub Delete(idEntity As String, whereField As String, table As Table) Implements IDAL.Delete
        Dim sql = "DELETE FROM " & table.ToString() & " WHERE " & whereField & " = " & idEntity

        sqlCommand = New SqlCommand(sql, connection)
        sqlCommand.ExecuteNonQuery()
    End Sub

    Public Sub Update(idEntity As String, data As IDictionary(Of String, Object), table As Table) Implements IDAL.Update
        Dim sql = "UPDATE " & table.ToString() & " SET " & GenerateUpdateParametersString(data) & " WHERE Id = " & idEntity

        sqlCommand = New SqlCommand(sql, connection)

        AddParameters(data, sqlCommand.Parameters)

        sqlCommand.ExecuteNonQuery()
    End Sub

    Private Function ExecuteInsertQuery(data As IDictionary(Of String, Object), table As Table) As SqlDataReader
        Dim sql = "INSERT INTO " & table.ToString() & " (" & GetParseableFields(data) & ") OUTPUT INSERTED.* "
        sql += "VALUES (" & GenerateParametersString(data) & ")"

        sqlCommand = New SqlCommand(sql, connection)

        AddParameters(data, sqlCommand.Parameters)

        Return sqlCommand.ExecuteReader()
    End Function

    Private Sub AddParameters(data As IDictionary, sqlParameters As SqlParameterCollection)
        For Each parameterName As String In GenerateParameters(data)
            Dim sqlParameter = New SqlParameter(parameterName, data(parameterName.Replace("@", "")))

            sqlParameters.Add(sqlParameter)
        Next
    End Sub

    Private Function GenerateParameters(data As IDictionary(Of String, Object)) As IEnumerable(Of String)
        Dim retorno = New List(Of String)
        For Each key In data.Keys
            retorno.Add("@" & key)
        Next

        Return retorno
    End Function

    Private Function GenerateParametersString(data As IDictionary(Of String, Object)) As String
        Dim parameters = GenerateParameters(data)

        Return String.Join(", ", parameters)
    End Function

    Private Function GenerateUpdateParametersString(data As IDictionary) As String
        Dim parameters = GenerateParameters(data)
        Return ParseParametersIntoUpdateString(parameters)
    End Function


    Private Function ParseResultIntoDictionary(sqlDataReader As SqlDataReader) As List(Of IDictionary(Of String, Object))
        Dim result As New List(Of IDictionary(Of String, Object))
        While sqlDataReader.Read()
            Dim rowData As New Dictionary(Of String, Object)

            Dim columns = sqlDataReader.GetColumnSchema()
            For Each column In columns
                Dim rowValue = sqlDataReader(column.ColumnName)
                rowData.Add(column.ColumnName, rowValue)
            Next

            result.Add(rowData)
        End While

        Return result
    End Function

    Private Function GetParseableFields(data As IDictionary(Of String, Object)) As String
        Dim temp As ICollection(Of String) = data.Keys

        Return String.Join(", ", temp.ToList())
    End Function

    Private Shared Function ParseParametersIntoUpdateString(parameters As IEnumerable(Of String)) As String
        Dim returnString = New StringBuilder()
        For Each value In parameters
            Dim column = value.Replace("@", "")
            returnString.Append(column & " = " & value & ", ")
        Next

        Return returnString.Remove(returnString.Length - 2, returnString.Length).ToString()
    End Function

    Public Sub Dispose() Implements IDisposable.Dispose
        connection.Close()
    End Sub
End Class
