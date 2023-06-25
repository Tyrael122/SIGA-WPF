Imports System.Runtime.CompilerServices

Module ModelExtensionMethods
    <Extension()>
    Public Function SelectIds(data As IEnumerable(Of IDictionary(Of String, Object)))
        Return data.Select(Function(dict) dict("Id"))
    End Function

End Module
