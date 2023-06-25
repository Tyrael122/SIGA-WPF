Public Class AlunoBusinessRules
    Private Shared ReadOnly dataBridge As IDAL = New DAL()

    Private ReadOnly idAluno As String

    Sub New()
        idAluno = SessionCookie.GetCookie("userId")
    End Sub

    Public Shared Sub DeleteAluno(idAluno As String)
        dataBridge.Delete(idAluno, "IdAluno", Table.Nota)

        dataBridge.Delete(idAluno, Table.Aluno)
    End Sub

    Public Shared Sub DeleteDisciplinasAluno(idAluno As String)
        dataBridge.Delete(idAluno, "IdAluno", Table.AlunoDisciplina)
    End Sub
    Public Shared Sub DeleteNotaAluno(idNota As String)
        dataBridge.Delete(idNota, Table.Nota)
    End Sub

    Friend Function GetPresencasAluno() As IEnumerable(Of IDictionary(Of String, Object))
        Dim idDisciplinas = ModelUtils.GetDisciplinas(Table.Aluno, idAluno).Select(Function(dict) dict("Id"))

        Dim aulas = dataBridge.SelectAll(Table.Aula).Where(Function(dict) idDisciplinas.Contains(dict("IdDisciplina")))
        Dim idAulas = aulas.Select(Function(dict) dict("Id"))

        Dim presencas = dataBridge.SelectAll(Table.Presenca).Where(Function(dict) idAulas.Contains(dict("IdAula")) And dict("IdAluno") = idAluno)

        For Each presenca In presencas
            presenca("Data") = aulas.Where(Function(aula) aula("Id") = presenca("IdAula")).First()("Data")

            Dim idDisciplina = ModelUtils.FindById(presenca("IdAula"), Table.Aula).First()("IdDisciplina")
            presenca("Disciplina") = ModelUtils.FindById(idDisciplina, Table.Disciplina).First()("Name")
        Next

        presencas = PresenterUtils.RemoveKeyFromDict(presencas, "IdAluno")
        presencas = PresenterUtils.RemoveKeyFromDict(presencas, "IdAula")
        Return presencas
    End Function

    Friend Function GetNotasAluno() As IEnumerable(Of IDictionary(Of String, Object))
        Dim disciplinas = ModelUtils.GetDisciplinas(Table.Aluno, idAluno)

        Dim idDisciplinas = disciplinas.Select(Function(dict) dict("Id"))

        Dim provas = dataBridge.SelectAll(Table.Prova).Where(Function(dict) idDisciplinas.Contains(dict("IdDisciplina")))
        Dim idProvas = provas.Select(Function(dict) dict("Id"))

        Dim notasAluno = dataBridge.SelectAll(Table.Nota).Where(Function(dict) idProvas.Contains(dict("IdProva")) And dict("IdAluno") = idAluno)

        For Each nota In notasAluno
            Dim prova = provas.Where(Function(dict) dict("Id") = nota("IdProva")).First()

            nota("Data da prova") = prova("Data")

            nota("Disciplina") = disciplinas.Where(Function(dict) dict("Id") = prova("IdDisciplina")).First()("Name")
        Next

        notasAluno = PresenterUtils.RemoveKeyFromDict(notasAluno, "IdProva")
        Return notasAluno
    End Function
End Class
