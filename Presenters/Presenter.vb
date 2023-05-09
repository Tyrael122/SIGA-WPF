﻿Imports System.Data

Public Class Presenter
    Protected Shared Sub ShowWindowAndCloseCurrent(window As Window, view As IView)
        window.Show()
        view.CloseView()
    End Sub

    Protected Function ConvertDictionariesToDataTable(data As IEnumerable(Of IDictionary(Of String, String))) As DataTable
        Dim dataTable As New DataTable()

        ' Add columns to DataTable based on the keys of the first dictionary in the list
        For Each key In data(0).Keys
            dataTable.Columns.Add(key)
        Next

        ' Add rows to DataTable for each dictionary in the list
        For Each dict In data
            Dim dataRow As DataRow = dataTable.NewRow()
            For Each keyValuePair In dict
                dataRow(keyValuePair.Key) = keyValuePair.Value
            Next

            dataTable.Rows.Add(dataRow)
        Next

        Return dataTable
    End Function

    Protected Function LoadComboBox(selector As Func(Of IEnumerable(Of IDictionary(Of String, String))), content As String, tag As String) As IEnumerable(Of ComboBoxItem)
        Dim comboBoxItems As New List(Of ComboBoxItem)

        For Each dict In selector()
            Dim comboBoxItem As New ComboBoxItem With {
                .Content = dict(content),
                .Tag = dict(tag)
            }
            comboBoxItems.Add(comboBoxItem)
        Next

        Return comboBoxItems
    End Function

    Protected Function GetAll(table As Table) As IEnumerable(Of IDictionary(Of String, String))
        Return BusinessRules.GetAll(table)
    End Function

    Protected Function GetDataTable(table As Table) As DataTable
        Dim data = GetAll(table)
        Return ConvertDictionariesToDataTable(data)
    End Function

    Public Function GetAll(tableStr As String) As IEnumerable(Of IDictionary(Of String, String))
        Dim table As Table = [Enum].Parse(GetType(Table), tableStr)
        Return GetAll(table)
    End Function

    Public Function GetDataTable(tableStr As String) As DataTable
        Dim table As Table = [Enum].Parse(GetType(Table), tableStr)
        Return GetDataTable(table)
    End Function

    Protected Function GetAllDisciplinasPorSemestre(idCurso As String, semestre As Integer) As IEnumerable(Of IDictionary(Of String, String))
        Dim disciplinas = BusinessRules.GetDisciplinas(Table.Curso, idCurso)

        Return disciplinas.Where(Function(disciplina) disciplina("Semester") = semestre)
    End Function
End Class
