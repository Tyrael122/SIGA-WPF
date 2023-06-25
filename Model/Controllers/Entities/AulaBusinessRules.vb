Public Class AulaBusinessRules
    Inherits Model

    Friend Shared Function SalvarAula(aula As IDictionary(Of String, Object)) As String
        Dim aulaMatched = ModelUtils.GetAll(Table.Aula).Where(Function(dict) Date.Parse(dict("Data")) = Date.Parse(aula("Data")))

        If aulaMatched.Any() Then
            Dim idAula = aulaMatched.First()("Id")

            dataBridge.Delete(idAula, "IdAula", Table.Presenca)

            Return idAula
        End If

        Return ModelUtils.SaveWithOutput(aula, Table.Aula).First()("Id")
    End Function
End Class
