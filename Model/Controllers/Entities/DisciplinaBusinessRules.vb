Public Class DisciplinaBusinessRules
    Private Shared ReadOnly dataBridge As IDAL = New DAL()

    Private idDisciplina As String

    Sub New()
        idDisciplina = SessionCookie.GetCookie("idDisciplina")
    End Sub

    Friend Shared Sub DeleteDisciplinas(idEntity As String, whereField As String, table As Table)
        dataBridge.Delete(idEntity, whereField, table)
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

    Friend Function GetAllAlunosCadastrados() As IEnumerable(Of IDictionary(Of String, Object))
        Dim entityRelation = New Relation(Table.Disciplina, Table.Aluno)
        Return entityRelation.GetAllMultipleEntitiesById(idDisciplina)
    End Function
End Class
