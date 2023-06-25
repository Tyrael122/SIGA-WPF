Public Class CursoBusinessRules
    Inherits Model

    Private idCurso = SessionCookie.GetCookie("IdCurso")

    Public Shared Sub DeleteCurso(idCurso As Object)
        dataBridge.Delete(idCurso, "IdCurso", Table.CursoDisciplina)
        dataBridge.Delete(idCurso, "IdCurso", Table.Horario)
        dataBridge.Delete(idCurso, "IdCurso", Table.Aula)
        dataBridge.Delete(idCurso, Table.Curso)
    End Sub

    Friend Sub UpdateCurso(data As IDictionary(Of String, Object), idsDisciplinasCurso As IEnumerable(Of String))
        DisciplinaBusinessRules.DeleteDisciplinas(idCurso, "IdCurso", Table.CursoDisciplina)

        Dim relation As New Relation(Table.Curso, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idRelatedEntites = idsDisciplinasCurso
        }

        relation.Update(idCurso)
    End Sub

    Friend Sub RegisterCurso(data As IDictionary(Of String, Object), idsDisciplinasCurso As IEnumerable(Of String))
        Dim relation As New Relation(Table.Curso, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idRelatedEntites = idsDisciplinasCurso
        }

        relation.Save()
    End Sub

    Public Sub RegisterHorarioCurso(data As IDictionary(Of String, Object))
        data("IdCurso") = idCurso

        ModelUtils.Save(data, Table.Horario)
    End Sub

    Friend Function GetTurnoFromCurso() As Turno
        Return ModelUtils.LoadEntityById(idCurso, Table.Curso)("Turno")
    End Function

    Public Function GetAllDisciplinasPorSemestre(semestre As Integer) As IEnumerable(Of IDictionary(Of String, Object))
        Dim disciplinas = ModelUtils.GetDisciplinas(Table.Curso, idCurso)

        Return disciplinas.Where(Function(disciplina) disciplina("Semester") = semestre)
    End Function
End Class
