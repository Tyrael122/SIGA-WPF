﻿Public Class AulaViewModel
    Inherits ViewModel

    Private _data As Date
    Public Property Data As Object
        Get
            Return _data
        End Get
        Set(value As Object)
            _data = value
            OnPropertyChanged(NameOf(Data))
        End Set
    End Property

    Private _idHorario As String
    Public Property IdHorario As Object
        Get
            Return _idHorario
        End Get
        Set(value As Object)
            If value Is Nothing Then
                Exit Property
            End If

            _idHorario = value.Tag
            OnPropertyChanged(NameOf(IdHorario))
        End Set
    End Property
End Class
