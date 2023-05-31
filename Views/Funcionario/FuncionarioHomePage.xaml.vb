Imports System.IO
Imports Microsoft.Win32

Public Class FuncionarioHomePage
    Implements IView

    Private Presenter As PresenterFuncionario = New PresenterFuncionario(Me)
    Dim imageSource As New BitmapImage
    Private imagePath As String
    Private imageBytes As Byte()

    Private jaAbriuFileDialog = False

    Private Sub FuncionarioHomePage_Loaded(sender As Object, e As RoutedEventArgs) Handles Me.Loaded
        Dim disciplinas = Presenter.GetDataTable("Disciplina").DefaultView
        DisciplinasCursoDataGrid.ItemsSource = disciplinas
        DisciplinasProfessorDataGrid.ItemsSource = disciplinas

        cmbCursosAluno.ItemsSource = Presenter.LoadCursosAlunoComboBox()
        cursoDataGrid.ItemsSource = Presenter.GetDataTable("Curso").DefaultView
    End Sub
    Public Sub DisplayInfo(infoMessage As String) Implements IView.DisplayInfo
        lblInfo.Content = infoMessage

        If tabCadastroProfessor.IsSelected Then
            lblInfoCadastroProfessor.Content = infoMessage
        ElseIf tabCadastroAluno.IsSelected Then
            lblInfoCadastroAluno.Content = infoMessage
        ElseIf tabCadastroCurso.IsSelected Then
            lblInfoCadastroCurso.Content = infoMessage
        ElseIf tabCadastroDisciplina.IsSelected Then
            lblInfoCadastroDisciplina.Content = infoMessage
        End If
    End Sub

    Public Sub DisplayError() Implements IView.DisplayError
        Throw New NotImplementedException()
    End Sub

    Public Sub CloseView() Implements IView.CloseView
        Me.Close()
    End Sub

    Private Sub btnCadastrar_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrar.Click
        Dim button As Button = DirectCast(sender, Button)

        'Dim imageBrush As ImageBrush = button.Background

        'Dim image As BitmapImage = TryCast(imageBrush.ImageSource, BitmapImage)
        Dim image As BitmapImage = TryCast(imageTestComponent.Source, BitmapImage)

        If image IsNot Nothing Then
            Dim encoder As New PngBitmapEncoder()
            encoder.Frames.Add(BitmapFrame.Create(image))
            Using memoryStream As New MemoryStream()
                encoder.Save(memoryStream)
                imageBytes = memoryStream.ToArray()
            End Using
        Else
            imageBytes = Nothing
        End If

        Dim map As IDictionary(Of String, Object) = New Dictionary(Of String, Object) From {
            {"Login", txtLogin.Text},
            {"Password", txtPassword.Text},
            {"SemestreInicio", cmbSemestreInicio.SelectedItem.Content},
            {"Curso", txtNomeCurso.Text},
            {"Foto", imageBytes}
        }

        Presenter.RegisterAluno(map)
    End Sub

    Private Sub btnCadastrarProfessor_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarProfessor.Click
        Dim map As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
            {"Login", txtLoginProfessor.Text},
            {"Password", txtPasswordProfessor.Text}
        }

        Presenter.RegisterProfessor(map)
    End Sub

    Private Sub tabEditarProfessor_GotFocus(sender As Object, e As RoutedEventArgs) Handles tabEditarProfessor.GotFocus
        professorDataGrid.ItemsSource = Presenter.GetDataTable("Professor").DefaultView
    End Sub

    Private Sub tabEditarAlunos_GotFocus(sender As Object, e As RoutedEventArgs) Handles tabEditarAlunos.GotFocus
        alunosDataGrid.ItemsSource = Presenter.GetDataTable("Aluno").DefaultView
    End Sub

    Private Sub btnCadastrarCurso_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarCurso.Click
        Dim map As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
            {"Nome", txtNomeCurso.Text},
            {"Sigla", txtSigla.Text},
            {"Turno", cmbTurno.SelectedIndex}
        }

        Presenter.RegisterCurso(map)
    End Sub

    Private Sub btnCadastrarDisciplina_Click(sender As Object, e As RoutedEventArgs) Handles btnCadastrarDisciplina.Click

        Dim map As IDictionary(Of String, String) = New Dictionary(Of String, String) From {
            {"Name", txtNomeDisciplina.Text},
            {"Semester", txtSemestre.Text},
            {"Workload", txtCargaHoraria.Text},
            {"Description", txtDescricao.Text}
        }


        Presenter.RegisterDisciplina(map)
    End Sub

    Private Sub CheckBox_Click(sender As Object, e As RoutedEventArgs)

        Dim checkBox As CheckBox = CType(sender, CheckBox)
        If checkBox.IsChecked Then
            Presenter.AddDisciplinaSelecionadaAoCurso(checkBox.Tag)
        Else
            Presenter.RemoveDisciplinaSelecionadaDoCurso(checkBox.Tag)
        End If
    End Sub

    Private Sub VerCurso_Click(sender As Object, e As EventArgs)
        Dim button As Button = CType(sender, Button)

        Presenter.ShowCursoPage(button.Tag)
    End Sub

    Private Sub cmbCursosAluno_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbCursosAluno.SelectionChanged
        Dim semestreInicio As Integer = 0
        If cmbSemestreInicio.SelectedValue IsNot Nothing Then
            semestreInicio = Convert.ToInt32(cmbSemestreInicio.SelectedValue.Content)
        End If

        DisciplinasCursoAlunoDataGrid.ItemsSource =
            Presenter.GetDisciplinasPorSemestreDataTable(cmbCursosAluno.SelectedValue.Tag, semestreInicio).DefaultView
    End Sub

    Private Sub cmbSemestreInicio_SelectionChanged(sender As Object, e As SelectionChangedEventArgs) Handles cmbSemestreInicio.SelectionChanged
        Dim semestreInicio As Integer = Convert.ToInt32(cmbSemestreInicio.SelectedValue.Content)

        If cmbCursosAluno.SelectedValue Is Nothing Then
            Return
        End If

        DisciplinasCursoAlunoDataGrid.ItemsSource =
            Presenter.GetDisciplinasPorSemestreDataTable(cmbCursosAluno.SelectedValue.Tag, semestreInicio).DefaultView
    End Sub

    Private Sub CheckBoxDisciplinasAluno_Click(sender As Object, e As RoutedEventArgs)
        Dim checkBox As CheckBox = CType(sender, CheckBox)
        If checkBox.IsChecked Then
            Presenter.AddDisciplinaSelecionadaAoAluno(checkBox.Tag)
        Else
            Presenter.RemoveDisciplinaSelecionadaDoAluno(checkBox.Tag)
        End If
    End Sub

    Private Sub CheckBoxDisciplinaProfessor_Click(sender As Object, e As RoutedEventArgs)
        Dim checkBox As CheckBox = CType(sender, CheckBox)
        If checkBox.IsChecked Then
            Presenter.AddDisciplinaSelecionadaAoProfessor(checkBox.Tag)
        Else
            Presenter.RemoveDisciplinaSelecionadaDoProfessor(checkBox.Tag)
        End If
    End Sub

    Private Sub btnImage_Click(sender As Object, e As RoutedEventArgs) Handles btnImage.Click
        If jaAbriuFileDialog Then
            jaAbriuFileDialog = False
            Return
        End If

        Dim openFileDialog As New OpenFileDialog With {
            .Filter = "Arquivos de Imagem (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Todos os Arquivos (*.*)|*.*"
        }

        Dim usuarioClicouBotaoOk = openFileDialog.ShowDialog() = True
        If usuarioClicouBotaoOk Then
            jaAbriuFileDialog = True

            imagePath = openFileDialog.FileName
            Dim imageSource As New BitmapImage(New Uri(imagePath))
            Dim button As Button = DirectCast(sender, Button)

            Dim imageBrush As New ImageBrush With {
                .ImageSource = imageSource
            }

            button.Background = imageBrush
        End If
    End Sub

    Private Sub Image_MouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)

        imageTestComponent.Source = Presenter.SelecionarImagemAluno()

        If jaAbriuFileDialog Then
            jaAbriuFileDialog = False
            Return
        End If

        Dim openFileDialog As New OpenFileDialog With {
            .Filter = "Arquivos de Imagem (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|Todos os Arquivos (*.*)|*.*"
        }

        Dim usuarioClicouBotaoOk = openFileDialog.ShowDialog() = True
        If usuarioClicouBotaoOk Then
            jaAbriuFileDialog = True

            imagePath = openFileDialog.FileName
            Dim imageSource As New BitmapImage(New Uri(imagePath))

            imageTestComponent.Source = imageSource
        End If
    End Sub
End Class
