Public Class AlunoBusinessRules
    Inherits Model

    Private ReadOnly idAluno As String

    Sub New()
        idAluno = SessionCookie.GetCookie("userId")
    End Sub

    Public Shared Sub RegisterAluno(data As IDictionary(Of String, Object), idsDisciplinasAluno As IEnumerable(Of String))
        Dim relation As New Relation(Table.Aluno, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idRelatedEntites = idsDisciplinasAluno
        }

        ModelUtils.RegisterUserWithPhoto(relation)
    End Sub

    Public Sub UpdateAluno(data As IDictionary(Of String, Object), idsDisciplinasAluno As IEnumerable(Of String))
        DisciplinaBusinessRules.DeleteDisciplinas(idAluno, "IdAluno", Table.AlunoDisciplina)

        Dim relation As New Relation(Table.Aluno, Table.Disciplina) With {
            .uniqueEntityData = data,
            .idRelatedEntites = idsDisciplinasAluno
        }

        ModelUtils.UpdateUserWithPhoto(relation, idAluno)
    End Sub

    Public Shared Sub DeleteAluno(idAluno As String)
        dataBridge.Delete(idAluno, "IdAluno", Table.Nota)

        dataBridge.Delete(idAluno, Table.Aluno)
    End Sub

    Public Shared Sub DeleteNotaAluno(idNota As String)
        dataBridge.Delete(idNota, Table.Nota)
    End Sub

    ' TODO: Refactor this method
    Friend Function GetPresencasAluno() As IEnumerable(Of IDictionary(Of String, Object))
        Dim idDisciplinas = ModelUtils.GetDisciplinas(Table.Aluno, idAluno).SelectIds()

        Dim aulas = ModelUtils.GetAll(Table.Aula).Where(Function(dict) idDisciplinas.Contains(dict("IdDisciplina")))
        Dim idAulas = aulas.SelectIds()

        Dim presencas = ModelUtils.GetAll(Table.Presenca).Where(Function(dict) idAulas.Contains(dict("IdAula")) And dict("IdAluno") = idAluno)

        For Each presenca In presencas
            presenca("Data") = aulas.Where(Function(aula) aula("Id") = presenca("IdAula")).First()("Data")

            Dim idDisciplina = ModelUtils.FindById(presenca("IdAula"), Table.Aula).First()("IdDisciplina")
            presenca("Disciplina") = ModelUtils.FindById(idDisciplina, Table.Disciplina).First()("Name")
        Next

        Return presencas
    End Function

    ' TODO: Refactor this method
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

    Friend Function GetProvasFuturas() As IEnumerable(Of IDictionary(Of String, Object))
        Dim idDisciplinas = ModelUtils.GetDisciplinas(Table.Aluno, idAluno).Select(Function(dict) dict("Id"))

        Return ModelUtils.FindInListOfId(idDisciplinas, Table.Prova).Where(Function(dict) dict("Data") > Date.Now)
    End Function

    Public Function GetSolicitacoes()
        Return ModelUtils.FindById(idAluno, Table.Solicitacao)
    End Function
End Class
