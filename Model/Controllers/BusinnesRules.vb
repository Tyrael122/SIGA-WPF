Public Class BusinessRules
    Private Shared ReadOnly dataBridge As IDAL = New DAL() ' TODO: Search for a way to cleanly dispose of the connection created by the IDAL.

    Friend Shared Sub DeleteAluno(idAluno As String)
        dataBridge.Delete(idAluno, "IdAluno", Table.Nota)

        dataBridge.Delete(idAluno, Table.Aluno)
    End Sub

    Friend Shared Sub DeleteDisciplinasAluno(idAluno As String)
        dataBridge.Delete(idAluno, "IdAluno", Table.AlunoDisciplina)
    End Sub

    Friend Shared Sub DeleteProfessor(idProfessor As String)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.ProfessorDisciplina)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Prova)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Horario)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Aula)

        dataBridge.Delete(idProfessor, Table.Professor)
    End Sub

    Friend Shared Sub DeleteDisciplinas(idEntity As String, whereField As String, table As Table)
        dataBridge.Delete(idEntity, whereField, table)
    End Sub

    Friend Shared Function SaveWithOutput(aulaData As Dictionary(Of String, Object), table As Table) As List(Of IDictionary(Of String, Object))
        Return dataBridge.SaveWithOutput(aulaData, table)
    End Function

    Friend Shared Function Save(data As IDictionary(Of String, Object), table As Table) As Boolean
        Return dataBridge.Save(data, table)
    End Function

    Friend Shared Function GetAll(table As Table) As IEnumerable(Of IDictionary(Of String, Object))
        Return dataBridge.SelectAll(table)
    End Function

    Friend Shared Function GetDisciplinas(table As Table, idEntity As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim relation As New Relation(table, Table.Disciplina)

        Dim relationColumns = relation.GetRelationColumns()

        Dim idDisciplinas = dataBridge.SelectAll(relation.GetRelationTable()).
            Where(Function(dict) dict(relationColumns.uniqueEntity) = idEntity).
            Select(Function(dict) dict(relationColumns.multipleEntity))

        Return GetAll(Table.Disciplina).Where(Function(disciplina) idDisciplinas.Contains(disciplina("Id")))
    End Function

    Friend Shared Function GetAllById(id As String, tableStr As Table) As IEnumerable(Of IDictionary(Of String, Object))
        Return dataBridge.SelectAll(tableStr).Where(Function(dict) dict("Id") = id)
    End Function

    Friend Shared Function SalvarAula(aula As IDictionary(Of String, Object)) As String
        Dim aulaMatched = GetAll(Table.Aula).Where(Function(dict) Date.Parse(dict("Data")) = Date.Parse(aula("Data")))

        If aulaMatched.Any() Then
            Dim idAula = aulaMatched.First()("Id")

            dataBridge.Delete(idAula, "IdAula", Table.Presenca)

            Return idAula
        End If

        Return SaveWithOutput(aula, Table.Aula).First()("Id")
    End Function

    Public Shared Function GetDisciplinasComCheckBoxColumn(idEntity As String, table As Table) As IEnumerable(Of IDictionary(Of String, Object))
        Dim idDisciplinas = GetDisciplinas(table, idEntity).Select(Function(dict) dict("Id"))

        Dim disciplinas = GetAll(Table.Disciplina)

        For Each disciplina In disciplinas
            disciplina("IsChecked") = idDisciplinas.Contains(disciplina("Id"))
        Next

        Return disciplinas
    End Function

    Friend Shared Sub DeleteCurso(idCurso As Object)
        dataBridge.Delete(idCurso, "IdCurso", Table.CursoDisciplina)
        dataBridge.Delete(idCurso, "IdCurso", Table.Horario)
        dataBridge.Delete(idCurso, "IdCurso", Table.Aula)
        dataBridge.Delete(idCurso, Table.Curso)
    End Sub

    Friend Shared Sub DeleteDisciplina(idDisciplina As String)
        dataBridge.Delete(idDisciplina, "IdDisciplina", Table.ProfessorDisciplina)
        dataBridge.Delete(idDisciplina, "IdDisciplina", Table.AlunoDisciplina)
        dataBridge.Delete(idDisciplina, "IdDisciplina", Table.CursoDisciplina)
        dataBridge.Delete(idDisciplina, "IdDisciplina", Table.Aula)
        dataBridge.Delete(idDisciplina, "IdDisciplina", Table.Horario)

        Dim idsProva = dataBridge.SelectAll(Table.Prova).Where(Function(dict) dict("IdDisciplina") = idDisciplina).Select(Function(dict) dict("Id"))

        For Each idProva In idsProva
            dataBridge.Delete(idProva, "IdProva", Table.Nota)
        Next

        dataBridge.Delete(idDisciplina, "IdDisciplina", Table.Prova)
        dataBridge.Delete(idDisciplina, Table.Disciplina)
    End Sub

    Friend Shared Sub Update(idEntity As String, table As Table, data As Dictionary(Of String, Object))
        dataBridge.Update(idEntity, data, table)
    End Sub

    Friend Shared Function GetPresencasAluno(idAluno As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim idDisciplinas = GetDisciplinas(Table.Aluno, idAluno).Select(Function(dict) dict("Id"))

        Dim aulas = dataBridge.SelectAll(Table.Aula).Where(Function(dict) idDisciplinas.Contains(dict("IdDisciplina")))
        Dim idAulas = aulas.Select(Function(dict) dict("Id"))

        Dim presencas = dataBridge.SelectAll(Table.Presenca).Where(Function(dict) idAulas.Contains(dict("IdAula")) And dict("IdAluno") = idAluno)

        For Each presenca In presencas
            presenca("Data") = aulas.Where(Function(aula) aula("Id") = presenca("IdAula")).First()("Data")

            Dim idDisciplina = GetAllById(presenca("IdAula"), Table.Aula).First()("IdDisciplina")
            presenca("Disciplina") = GetAllById(idDisciplina, Table.Disciplina).First()("Name")
        Next

        presencas = RemoveKeyFromDict(presencas, "IdAluno")
        presencas = RemoveKeyFromDict(presencas, "IdAula")
        Return presencas
    End Function

    Friend Shared Function GetNotasAluno(idAluno As String) As IEnumerable(Of IDictionary(Of String, Object))
        Dim disciplinas = GetDisciplinas(Table.Aluno, idAluno)

        Dim idDisciplinas = disciplinas.Select(Function(dict) dict("Id"))

        Dim provas = dataBridge.SelectAll(Table.Prova).Where(Function(dict) idDisciplinas.Contains(dict("IdDisciplina")))
        Dim idProvas = provas.Select(Function(dict) dict("Id"))

        Dim notasAluno = dataBridge.SelectAll(Table.Nota).Where(Function(dict) idProvas.Contains(dict("IdProva")) And dict("IdAluno") = idAluno)

        For Each nota In notasAluno
            Dim prova = provas.Where(Function(dict) dict("Id") = nota("IdProva")).First()

            nota("Data da prova") = prova("Data")

            nota("Disciplina") = disciplinas.Where(Function(dict) dict("Id") = prova("IdDisciplina")).First()("Name")
        Next

        notasAluno = RemoveKeyFromDict(notasAluno, "IdProva")
        Return notasAluno
    End Function

    Public Shared Function RemoveKeyFromDict(data As IEnumerable(Of IDictionary(Of String, Object)), keyToRemove As String) As IEnumerable(Of IDictionary(Of String, Object))
        data = data.Select(Function(dict)
                               If dict.ContainsKey(keyToRemove) Then
                                   dict.Remove(keyToRemove)
                               End If
                               Return dict
                           End Function).ToList()
        Return data
    End Function

    Friend Shared Function GetAllAlunosCadastrados(idDisciplina As Object) As IEnumerable(Of IDictionary(Of String, Object))
        Dim idAlunos = dataBridge.SelectAll(Table.AlunoDisciplina).Where(Function(dict) dict("IdDisciplina") = idDisciplina).Select(Function(dict) dict("IdAluno"))

        Dim alunos = dataBridge.SelectAll(Table.Aluno).Where(Function(dict) idAlunos.Contains(dict("Id")))

        Return alunos
    End Function
End Class
