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

    Public Sub EnviarEmailRecuperacaoSenha(email As String)

        Dim dal As New DAL()
        Dim credenciaisEmailAluno = dal.SelectFields(Table.Aluno, "Password", "Login", "Email").Where(Function(dict) dict("Email") = email).FirstOrDefault()
        Dim credenciaisEmailProfessor = dal.SelectFields(Table.Professor, "Password", "Login", "Email").Where(Function(dict) dict("Email") = email).FirstOrDefault()

        Dim remetente As String = ""
        Dim senha As String = ""


        If credenciaisEmailAluno Is Nothing And credenciaisEmailProfessor Is Nothing Then
            View.DisplayInfo("Nenhum email cadastrado. ")
            Return
        End If

        ' Aluno
        If credenciaisEmailProfessor Is Nothing Then
            remetente = credenciaisEmailAluno("Login")
            senha = credenciaisEmailAluno("Password")
        Else
            remetente = credenciaisEmailProfessor("Login")
            senha = credenciaisEmailProfessor("Password")
        End If


        ' Configurações do e-mail
        Dim assunto As String = "Recuperação de Senha"
        Dim corpo As String = "Olá, você solicitou a recuperação de senha. Sua senha é: " & senha

        ' Configurações do servidor SMTP
        Dim smtpHost As String = "smtp-mail.outlook.com"
        Dim smtpPort As Integer = 587
        Dim enableSsl As Boolean = True

        ' Criando a mensagem de e-mail 
        Dim mensagem As New MailMessage(remetente, email, assunto, corpo)

        ' Configurando o cliente SMTP
        Dim clienteSmtp As New SmtpClient(smtpHost, smtpPort)
        clienteSmtp.EnableSsl = enableSsl
        clienteSmtp.Credentials = New NetworkCredential(remetente, senha)

        ' Enviando o e-mail
        clienteSmtp.Send(mensagem)
        View.DisplayInfo("E-mail de recuperação de senha enviado com sucesso!")

    End Sub
End Class
