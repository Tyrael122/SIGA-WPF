Imports System.Net
Imports System.Net.Mail

Public Class PresenterLogin
    Inherits Presenter

    Public Sub New(view As IView)
        Me.View = view
    End Sub

    Public Sub TryLogin(username As String, password As String, table As Table)
        Dim user = LoginRules.GetUserByCredentials(username, password, table)

        Dim isCredentialsCorrect = user.Count() = 1
        If Not isCredentialsCorrect Then
            View.DisplayInfo("The credentials are invalid.")
            Return
        End If

        SessionCookie.AddCookie("userId", user.First()("Id"))

        ShowWindowAndCloseCurrent(ChooseWindow(table), View)
    End Sub

    Public Sub ShowChangePasswordScreen()
        ShowWindowAndCloseCurrent(New ChangePasswordScreen, View)
    End Sub

    Private Function ChooseWindow(user As Table) As Window
        Select Case user
            Case Table.Aluno
                Return New AlunoHomePage()
            Case Table.Professor
                Return New TelaInicialProfessor()
            Case Table.FuncionarioAdm
                Return New TelaFuncionario()
            Case Else
                Throw New ArgumentException("Invalid table.")
        End Select
    End Function

    Public Sub EnviarEmailRecuperacaoSenha(emailUsuario As String)
        Dim dal As New DAL()
        Dim credenciaisEmailAluno = dal.SelectFields(Table.Aluno, "Password", "Email").Where(Function(dict) dict("Email") = emailUsuario).FirstOrDefault()
        Dim credenciaisEmailProfessor = dal.SelectFields(Table.Professor, "Password", "Email").Where(Function(dict) dict("Email") = emailUsuario).FirstOrDefault()

        If credenciaisEmailAluno Is Nothing AndAlso credenciaisEmailProfessor Is Nothing Then
            View.DisplayInfo("Nenhum email cadastrado. ")
            Return
        End If

        Dim senha As String = ""
        If credenciaisEmailProfessor Is Nothing Then
            senha = credenciaisEmailAluno("Password")
        Else
            senha = credenciaisEmailProfessor("Password")
        End If

        Dim assunto As String = "Recuperação de Senha"
        Dim corpo As String = "Olá, você solicitou a recuperação de senha. Sua senha é: " & senha

        Try
            View.DisplayInfo("Enviando email...")
            SendEmail(emailUsuario, assunto, corpo)
        Catch ex As Exception
            View.DisplayInfo("Falha ao enviar o email.")
        End Try

        View.DisplayInfo("E-mail de recuperação de senha enviado com sucesso!")
    End Sub

    Private Shared Sub SendEmail(emailReceiver As String, assunto As String, corpo As String)
        Dim emailSender = "kauan.borges01@fatec.sp.gov.br"
        Dim passwordSender = "Potato 1203"

        Dim smtpServer As New SmtpClient With {
            .UseDefaultCredentials = False,
            .EnableSsl = True,
            .Host = "smtp.office365.com",
            .Port = 25,
            .Credentials = New NetworkCredential(emailSender, passwordSender)
        }

        Dim email As New MailMessage(emailSender, emailReceiver, assunto, corpo)
        smtpServer.Send(email)
    End Sub
End Class
