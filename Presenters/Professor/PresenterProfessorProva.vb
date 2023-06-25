Public Class PresenterProfessorProva
    Inherits Presenter

    Private ViewModelProva As New ProvaViewModel()

    Public Sub New(view As IViewModel)
        Me.View = view

        ViewModelProva.Data = Date.Now()
        view.SetDataContext(ViewModelProva)
    End Sub

    Public Sub RegisterProva()
        Dim data = ViewModelProva.ConvertToDictionary()

        data("Tipo") = [Enum].Parse(GetType(TipoProva), data("Tipo"))

        data("IdDisciplina") = SessionCookie.GetCookie("idDisciplina")
        data("IdProfessor") = SessionCookie.GetCookie("userId")

        data("Data") = ViewModelProva.Data.ToString("yyyy-MM-dd")

        ModelUtils.Save(data, Table.Prova)

        View.DisplayInfo("Prova cadastrada com sucesso!")
    End Sub
End Class
