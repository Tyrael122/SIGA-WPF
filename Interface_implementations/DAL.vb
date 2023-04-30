Imports System.Collections.ObjectModel
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Reflection

Public Class DAL
    Implements IDAL

    Private connection As SqlConnection
    Private sqlCommand As SqlCommand
    Private sqlDataReader As SqlDataReader

    Public Sub New()
        ConnectToDatabase()
    End Sub

    Public Sub Save(data As IDictionary, table As Table) Implements IDAL.Save
        Dim sql As String
        sql = "INSERT INTO " & table.ToString() & " (" & GetParseableFields(data) & ")"
        sql += " VALUES (" & GetParseableData(data) & ")"

        sqlCommand = New SqlCommand(sql, connection)
        sqlCommand.ExecuteNonQuery() ' How do we know the insert operation succeeded?
    End Sub

    Public Function ReadAllEntities(table As Table) As List(Of IDAO) Implements IDAL.ReadAllEntities
        Dim sql As String
        sql = "SELECT * FROM " & table.ToString()

        sqlCommand = New SqlCommand(sql, connection)
        sqlDataReader = sqlCommand.ExecuteReader()

        Dim entitiesRead As New List(Of IDAO)

        Dim list As New List(Of IDictionary(Of String, String))
        list.Where(Function(dict) dict.Item("ID") > 5)

        Dim rowData(sqlDataReader.FieldCount) As Object

        While sqlDataReader.Read()
            Dim columnsSchema As ReadOnlyCollection(Of DbColumn) = sqlDataReader.GetColumnSchema()
            Dim columns As IEnumerable(Of String) = columnsSchema.Select(Function(dbColumn) dbColumn.ColumnName)

            sqlDataReader.GetValues(rowData)

            Dim tempEntity As IDAO = BusinessRules.GetNewEntityOf(table)

            Dim tempDict As New Dictionary(Of String, Object)
            Dim i = 0
            For Each column In columns
                tempDict.Add(column, rowData(i))
                i += 1
            Next
            'tempDict = columns.Zip(rowData).ToDictionary(Function(columnName) columnName)

            tempEntity.LoadFromDictionary(tempDict)

            entitiesRead.Add(tempEntity)
        End While

        sqlDataReader.Close()
        Return entitiesRead
    End Function
    Function ReadByCredentials(userType As UserType, credentials As Credentials) As List(Of IDAO) Implements IDAL.ReadByCredentials
        Dim tempEntity As IDAO = BusinessRules.GetNewEntityOf(userType)

        Dim sql As String
        sql = "SELECT " & GetParseableFields(tempEntity) & " FROM " & userType.ToString() & " WHERE " & GetWhereFields(credentials)

        sqlCommand = New SqlCommand(sql, connection)
        sqlDataReader = sqlCommand.ExecuteReader()

        Dim entitiesRead As New List(Of IDAO)
        Dim rowData(sqlDataReader.FieldCount) As Object

        While sqlDataReader.Read()
            sqlDataReader.GetValues(rowData)
            tempEntity.LoadFromDataRow(rowData)

            entitiesRead.Add(tempEntity)
        End While

        Return entitiesRead
    End Function

    Private Shared Function GetParseableFields(tempEntity As IDAO) As String
        Dim fieldsArray As String() = tempEntity.GetFieldsToParse()

        Dim data As New Dictionary(Of String, String)
        data = fieldsArray.ToDictionary(Function(field) field)

        Return GetParseableFields(data)
    End Function

    Private Shared Function GetParseableFields(data As IDictionary) As String
        Dim fieldsToSelect As String = Nothing
        For Each field In data.Keys
            fieldsToSelect += field & ", "
        Next
        fieldsToSelect = fieldsToSelect.Remove(fieldsToSelect.Length - 2)
        Return fieldsToSelect
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

    Public Shared Function GetWhereFields(credentials As Credentials) As String
        Dim properties As PropertyInfo() = credentials.GetType().GetProperties()

        Dim whereClause As String = Nothing
        For Each credentialProperty In properties
            If credentialProperty.GetValue(credentials) IsNot Nothing Then
                whereClause += credentialProperty.Name & " = '" & credentialProperty.GetValue(credentials) & "' AND "
            End If
        Next

        whereClause = whereClause.Remove(whereClause.Length - 5)
        Return whereClause
    End Function

    Public Sub ConnectToDatabase()
        connection = New SqlConnection(Environment.GetEnvironmentVariable("StringConnection"))

        connection.Open()
    End Sub

    Public Sub CloseConnection() Implements IDAL.CloseConnection
        connection.Close()
    End Sub

    Protected Overrides Sub Finalize()
        CloseConnection()
        MyBase.Finalize()
    End Sub
End Class
