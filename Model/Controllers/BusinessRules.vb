Public MustInherit Class BusinessRules
    Protected Shared ReadOnly dataBridge As IDAL = New DAL() ' TODO: Search for a way to cleanly dispose of the connection created by the IDAL.

End Class
