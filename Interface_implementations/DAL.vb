Imports System.Data.SqlClient

Public Class DAL
    Implements IDAL, IDisposable

    Private connection As SqlConnection
    Private sqlCommand As SqlCommand
    Private sqlDataReader As SqlDataReader

    Public Sub New()
        ConnectToDatabase()
    End Sub


    Public Function SelectFields(table As Table, ParamArray fieldsToSelect() As String) As List(Of IDictionary(Of String, String)) Implements IDAL.SelectFields
        Dim sql As String
        sql = "SELECT " & String.Join(", ", fieldsToSelect) & " FROM " & table.ToString()

        sqlCommand = New SqlCommand(sql, connection)
        sqlDataReader = sqlCommand.ExecuteReader()

        Dim result As New List(Of IDictionary(Of String, String))
        While sqlDataReader.Read()
            Dim row As New Dictionary(Of String, String)
            For i As Integer = 0 To sqlDataReader.FieldCount - 1
                row.Add(sqlDataReader.GetName(i), sqlDataReader.GetValue(i))
            Next
            result.Add(row)
        End While

        sqlDataReader.Close()

        Return result
    End Function

    Public Function SelectAll(table As Table) As List(Of IDictionary(Of String, String)) Implements IDAL.SelectAll
        Return SelectFields(table, "*")
    End Function

    Public Sub Save(data As IDictionary, table As Table) Implements IDAL.Save
        Dim sql As String
        sql = "INSERT INTO " & table.ToString() & " (" & GetParseableFields(data) & ")"
        sql += " VALUES (" & GetParseableData(data) & ")"

        sqlCommand = New SqlCommand(sql, connection)

        sqlCommand.ExecuteNonQuery() ' How do we know the insert operation succeeded?
    End Sub

    Public Function ReadAllEntities(table As Table) As List(Of IDAO) Implements IDAL.ReadAllEntities
        Dim entitiesRead As New List(Of IDAO)

        For Each dictionary In SelectAll(table)
            Dim tempEntity As IDAO = BusinessRules.GetNewEntityOf(table)
            tempEntity.LoadFromDictionary(dictionary)

            entitiesRead.Add(tempEntity)
        Next

        Return entitiesRead
    End Function

    Private Shared Function GetParseableFields(data As IDictionary) As String
        Dim temp As ICollection(Of String) = data.Keys

        Return String.Join(", ", temp.ToList())
    End Function

    ' Evident copy and paste, refactor later.
    Private Shared Function GetParseableData(data As IDictionary) As String
        Dim fieldsToSelect As String = Nothing
        For Each field In data.Values
            If Not IsNumeric(field) Then
                fieldsToSelect += "'" & field & "'" & ", "
            Else
                fieldsToSelect += field & ", "
            End If
        Next

        fieldsToSelect = fieldsToSelect.Remove(fieldsToSelect.Length - 2)
        Return fieldsToSelect
    End Function

    Public Sub Delete(entity As IDAO) Implements IDAL.Delete
        Throw New NotImplementedException()
    End Sub

    Public Sub Edit(entity As IDAO, table As UserType) Implements IDAL.Edit
        Throw New NotImplementedException()
    End Sub

    Public Sub ConnectToDatabase()
        connection = New SqlConnection(Environment.GetEnvironmentVariable("StringConnection"))
        connection.Open()
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        connection.Close()
    End Sub
End Class
