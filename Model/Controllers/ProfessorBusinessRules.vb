Public Class ProfessorBusinessRules
    Inherits BusinessRules

    Private idProfessor As String

    Sub New()
        idProfessor = SessionCookie.GetCookie("userId")
    End Sub

    Public Shared Sub DeleteProfessor(idProfessor As String)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.ProfessorDisciplina)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Prova)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Horario)
        dataBridge.Delete(idProfessor, "IdProfessor", Table.Aula)

        dataBridge.Delete(idProfessor, Table.Professor)
    End Sub
End Class
