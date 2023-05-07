﻿Imports System.Data

Module Presenter
    Function ConvertDictionariesToDataTable(data As IEnumerable(Of IDictionary(Of String, String))) As DataTable
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
End Module
