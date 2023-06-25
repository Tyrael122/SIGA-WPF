Public Class CursoBusinessRules
    Inherits Model

    Public Shared Sub DeleteCurso(idCurso As Object)
        dataBridge.Delete(idCurso, "IdCurso", Table.CursoDisciplina)
        dataBridge.Delete(idCurso, "IdCurso", Table.Horario)
        dataBridge.Delete(idCurso, "IdCurso", Table.Aula)
        dataBridge.Delete(idCurso, Table.Curso)
    End Sub
End Class
