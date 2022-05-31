<Command("Fan Speed Deviance", "Top: |0-99|%  Bottom: |0-99|%", "", "", ""),
Description("Fan Speed allowable deviance for bottom and top fans."),
Category("Speed Functions")>
Public Class FD : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public DevianceTop As Integer
  Public DevianceBottom As Integer

  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub
  Public Enum EState
    Off
    Active
  End Enum
  Property State As EState

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else
        DevianceTop = param(1)
        DevianceBottom = param(2)

        .Setpoint_FanSpeedTopDeviance = DevianceTop
        .Setpoint_FanSpeedBottomDeviance = DevianceBottom
      End If

      State = EState.Active
      Return True
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = EState.Off
    DevianceTop = 0
    DevianceBottom = 0
  End Sub

  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property

End Class
