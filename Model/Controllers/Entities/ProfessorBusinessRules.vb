Public Class ProfessorBusinessRules
    Inherits Model

    Private idProfessor As String

    Sub New()
        idProfessor = SessionCookie.GetCookie("userId")
    End Sub

    Public Sub UpdateProfessor(data As IDictionary(Of String, Object), idsDisciplinasProfessor As IEnumerable(Of String))
        DisciplinaBusinessRules.DeleteDisciplinas(idProfessor, "IdProfessor", Table.ProfessorDisciplina)

        Dim relation As New Relation(Table.Professor, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idRelatedEntites = idsDisciplinasProfessor
        }

        ModelUtils.UpdateUserWithPhoto(relation, idProfessor)
    End Sub

    Public Shared Sub DeleteProfessor(idProfessor As String)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.ProfessorDisciplina)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Prova)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Horario)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Aula)

        dataBridge.Delete(idProfessor, Table.Professor)
    End Sub

    Friend Shared Sub Register(data As IDictionary(Of String, Object), idsDisciplinasProfessor As IEnumerable(Of String))
        Dim relation As New Relation(Table.Professor, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idRelatedEntites = idsDisciplinasProfessor
        }

        ModelUtils.RegisterUserWithPhoto(relation)
    End Sub
End Class
