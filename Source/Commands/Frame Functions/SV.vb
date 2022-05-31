<Command("Selvage", "Left: |0-999|% Right: |0-999|% Deviance: |0-999|%", "", "", ""),
Description("Selvage speed setpoint Percent of Tenterchain setpoint. (115% = 1.15 * TC_Setpoint)"),
Category("Speed Functions")>
Public Class SV : Inherits MarshalByRefObject : Implements ACCommand

  Private ReadOnly ControlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Enum EState
    Off
    Active
  End Enum
  Property State As EState
  Property Target As Integer
  Property TargetDeviance As Integer


  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else

        'Check array bounds just to be on the safe side
        If param.GetUpperBound(0) >= 1 Then .Setpoint_SelvageLeft = param(1) * 10
        If param.GetUpperBound(0) >= 2 Then .Setpoint_SelvageRight = param(2) * 10
        If param.GetUpperBound(0) >= 3 Then .Setpoint_SelvageDeviance = param(3) * 10

      End If

      State = EState.Active

      Return True
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = EState.Off
  End Sub

  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property

End Class
