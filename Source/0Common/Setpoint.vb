Public Class Setpoint : Inherits MarshalByRefObject

  Property Name As Integer

  Property Register As Integer
  Property Anout As Integer

  Property CurrentValue As Integer
  Property TargetValue As Integer

  Property WriteEnabled As Boolean = False
  Property WriteAlways As Boolean = False

  Property LastUpdate As Date
  Property LastWrite As Date

  Public Enum EState
    Off
    Write            ' Write required
    WriteSuccess     ' Write succeeded
    WriteError       ' Write error
    NoChange         ' Current value matches target - no write required
  End Enum
  Property State As EState
  Property Timer As New Timer With {.Seconds = 32}


  Public Sub Write(value As Integer)
    TargetValue = value
    LastUpdate = Date.UtcNow

    ' Don't schedule a write if this setpoint is not enabled
    If Not WriteEnabled Then Exit Sub

    ' Don't schedule a write if we are not reading registers
    ' TODO

    ' Schedule write - skip if values already match
    State = EState.Write
    If CurrentValue = TargetValue Then State = EState.NoChange
  End Sub

  Public ReadOnly Property WriteRequest As Boolean
    Get
      If Not WriteEnabled Then Return False

      If State = EState.Write Then Return True
    End Get
  End Property

  Public Sub SetWriteResult(result As Ports.Modbus.Result)
    If result = Ports.Modbus.Result.OK Then
      State = EState.WriteSuccess
      LastWrite = Date.UtcNow
    Else
      State = EState.WriteError
    End If
  End Sub

End Class
