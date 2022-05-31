<Command("Overfeed", "Top: |35-175|%  Bottom: |35-175|%  Deviance: |0-50|%", "", "", ""),
Description("Set overfeed, Top = % of frame speed, Bottom = % of top overfeed speed. (115% = 1.15"),
Category("Speed Functions")>
Public Class OV : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public TargetTop As Integer
  Public TargetBottom As Integer
  Public TargetDeviance As Integer

  Public Enum EState
    Off
    Active
  End Enum
  Property State As EState

  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      ' Check array boundarys and value limits
      If param.GetUpperBound(0) < 3 Then Return True
      If param(1) < 35 OrElse param(1) > 175 Then Return True
      If param(2) < 35 OrElse param(2) > 175 Then Return True
      If param(3) < 0 OrElse param(3) > 50 Then Return True

      TargetTop = MinMax(param(1) * 10, .Parameters.OverfeedTopSetpointMin, .Parameters.OverfeedTopSetpointMax)
      TargetBottom = MinMax(param(2) * 10, .Parameters.OverfeedBottomSetpointMin, .Parameters.OverfeedBottomSetpointMax)
      TargetDeviance = MinMax(param(2) * 10, 0, 500)

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling

      Else
        'Check array bounds just to be on the safe side
        .Setpoint_OverfeedTop = TargetTop
        .Setpoint_OverfeedTopDeviance = TargetDeviance
        .Setpoint_OverfeedBot = TargetBottom
        .Setpoint_OverfeedBotDeviance = TargetDeviance
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
