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

    Public Sub Save(entity As IDAO, table As UserType) Implements IDAL.Save
        Throw New NotImplementedException()
    End Sub

    Public Function ReadAll(table As UserType) As List(Of IDAO) Implements IDAL.ReadAll
        Throw New NotImplementedException()
    End Function
    Function ReadByCredentials(table As UserType, credentials As Credentials) As List(Of IDAO) Implements IDAL.ReadByCredentials
        Dim tempEntity As IDAO = BusinessRules.GetEntityFromEnum(table)

        Dim sql As String

        sql = "SELECT " & GetSelectFields(tempEntity) & " FROM logins WHERE " & GetWhereFields(credentials)

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

    Private Shared Function GetSelectFields(tempEntity As IDAO) As String
        Dim fieldsToSelect As String = Nothing
        For Each field In tempEntity.GetFieldsToParse()
            fieldsToSelect += field & ", "
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

    Public Sub CloseConnection()
        connection.Close()
    End Sub
End Class
