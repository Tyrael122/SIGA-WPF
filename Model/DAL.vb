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
        sqlDataReader = SavePrivate(data, table)

        Dim rowsAffected As Integer = 0
        While sqlDataReader.Read()
            rowsAffected += 1
        End While

        sqlDataReader.Close()

        Return rowsAffected = 1
    End Function

    Public Function SaveWithOutput(data As IDictionary, table As Table) As List(Of IDictionary(Of String, String)) Implements IDAL.SaveWithOutput
        sqlDataReader = SavePrivate(data, table)

        Dim result = ParseResultIntoDictionary(sqlDataReader)
        sqlDataReader.Close()

        Return result
    End Function

    Private Function SavePrivate(data As IDictionary, table As Table) As SqlDataReader ' SavePrivate indicates that this method is private and is the one that actually saves the data.
        Dim sql = "INSERT INTO " & table.ToString() & " (" & GetParseableFields(data) & ") OUTPUT INSERTED.* "
        sql += "VALUES (" & GetParseableData(data) & ")"

        Return New SqlCommand(sql, connection).ExecuteReader()
    End Function

    Private Function GetParseableFields(data As IDictionary) As String
        Dim temp As ICollection(Of String) = data.Keys

        Return String.Join(", ", temp.ToList())
    End Function

    Private Function GetParseableData(data As IDictionary(Of String, String)) As String
        Dim fieldsToSelect As String = Nothing
        For Each field In data.Values
            If Not IsNumeric(field) Then
                fieldsToSelect += "'" & field & "'" & ", "
            Else
                fieldsToSelect += field & ", "
            End If
        Next

        Return fieldsToSelect.Remove(fieldsToSelect.Length - 2)
    End Function

    Public Function ParseResultIntoDictionary(sqlDataReader As SqlDataReader) As List(Of IDictionary(Of String, String))
        Dim result As New List(Of IDictionary(Of String, String))
        While sqlDataReader.Read()
            Dim row As New Dictionary(Of String, String)
            For i As Integer = 0 To sqlDataReader.FieldCount - 1
                row.Add(sqlDataReader.GetName(i), sqlDataReader.GetValue(i))
            Next
            result.Add(row)
        End While

        Return result
    End Function

    Public Sub Edit(data As IDictionary, table As Table) Implements IDAL.Edit
        Throw New NotImplementedException()
    End Sub

    Public Sub Delete(entity As IDictionary, table As Table) Implements IDAL.Delete
        Throw New NotImplementedException()
    End Sub

    Public Sub Delete(id As String, table As Table) Implements IDAL.Delete
        Throw New NotImplementedException()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        connection.Close()
    End Sub
End Class
