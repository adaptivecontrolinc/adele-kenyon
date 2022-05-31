<Command("Tenter Chain Left Right", "Left: |0-999|%  Right: |0-999|%  Deviance: |0-999|%", "", "", ""),
Description("Tenter Chain Left and Right setpoint Percent of master. (115% = 1.15 * MasterSpeed(YPM))"),
Category("Speed Functions")>
Public Class TLR : Inherits MarshalByRefObject : Implements ACCommand

  Private ReadOnly ControlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Enum EState
    Off
    Active
  End Enum
  Property State As EState
  Property TargetLeft As Integer
  Property TargetRight As Integer
  Property TargetDeviance As Integer


  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else
        'Check array bounds just to be on the safe side
        If param.GetUpperBound(0) >= 1 Then TargetLeft = param(1) * 10
        If param.GetUpperBound(0) >= 2 Then TargetRight = param(2) * 10
        If param.GetUpperBound(0) >= 3 Then TargetDeviance = param(3) * 10

        ' Update Setpoints
        .Setpoint_TenterLeft = TargetLeft
        .Setpoint_TenterLeftDeviance = TargetDeviance
        .Setpoint_TenterRight = TargetRight
        .Setpoint_TenterRightDeviance = TargetDeviance

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
