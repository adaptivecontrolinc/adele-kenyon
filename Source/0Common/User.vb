Public Class User : Inherits MarshalByRefObject

  Friend UserData As New Utilities.UserXml  ' Declared Friend so we don't see the password values in Variables

  Friend Supervisor As Integer
  Friend Expert As Integer
  Friend Shutdown As Integer

  Property AutoUpdateTimer As New Timer With {.Seconds = 32}

  Property AutoLogOffTimer As New Timer With {.Seconds = 240}

  Private autoLogOffSeconds_ As Integer = 240
  Property AutoLogOffSeconds As Integer
    Get
      Return autoLogOffSeconds_
    End Get
    Set(value As Integer)
      autoLogOffSeconds_ = 240
      If value > 0 Then autoLogOffSeconds_ = value
    End Set
  End Property

  Private name_ As String = "Operator"
  Property Name As String
    Get
      If AutoLogOffTimer.Finished Then name_ = "Operator"
      Return name_
    End Get
    Set(value As String)
      ' New user
      If value <> name_ Then
        AutoLogOffTimer.Seconds = AutoLogOffSeconds
        name_ = value
      End If
    End Set
  End Property

  Sub New()
    Load
  End Sub

  Sub Load()
    UserData.Load()

    ' A token effort to hide the passwords
    Supervisor = GetMaskedValue(UserData.Supervisor)
    Expert = GetMaskedValue(UserData.Expert)
    Shutdown = GetMaskedValue(UserData.Shutdown)
  End Sub

  Private Function GetMaskedValue(password As String) As Integer
    Dim tryInteger As Integer
    If Integer.TryParse(password, tryInteger) Then
      Return tryInteger * 88
    End If
    Return -1
  End Function

  Sub SetOperator()
    Name = "Operator"
  End Sub

  Sub SetSupervisor()
    Name = "Supervisor"
  End Sub

  Sub SetExpert()
    Name = "Expert"
  End Sub

  ReadOnly Property IsSupervisor As Boolean
    Get
      Return name_ = "Supervisor"
    End Get
  End Property

  ReadOnly Property IsExpert As Boolean
    Get
      Return name_ = "Expert"
    End Get
  End Property

End Class