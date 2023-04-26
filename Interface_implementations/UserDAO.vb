﻿Public MustInherit Class UserDAO
    Inherits DAO
    Implements IDAO

    Private _Login As String
    Public Property Login As String
        Get
            Return _Login
        End Get
        Set(value As String)
            _Login = value
        End Set
    End Property

    Private _Password As String
    Public Property Password As String
        Get
            Return _Password
        End Get
        Set(value As String)
            _Password = value
        End Set
    End Property

    Public Overrides Function GetFieldsToParse() As String() Implements IDAO.GetFieldsToParse
        Return MyBase.GetFieldsToParse()
    End Function
End Class
