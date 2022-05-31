<Command("Fan Speed", "Zone: |0-8| Top: |0-100|%  Bottom: |0-100|%"),
  Description("Set circulation fan speed."), Category("Oven Functions")>
Public Class FS : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public Zone As Integer
  Public TargetTop As Integer
  Public TargetBottom As Integer
  Public Targets(2, 8) As Integer  ' 1=top 2=bottom 

  Public Enum EState
    Off
    Active
  End Enum
  Property State As EState
  Property Timer As New Timer

  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode
      ' Check parameter array
      If param.GetUpperBound(0) < 3 Then Return True
      If param(1) < 0 OrElse param(1) > 8 Then Return True

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else
        Zone = param(1)
        TargetTop = MinMax(param(2) * 10, .Parameters.FanSpeedSetpointMin, .Parameters.FanSpeedSetpointMax)     ' 0-100% = 0-1000
        TargetBottom = MinMax(param(3) * 10, .Parameters.FanSpeedSetpointMin, .Parameters.FanSpeedSetpointMax)  ' 0-100% = 0-1000
        If TargetTop > 1000 Then TargetTop = 1000
        If TargetBottom > 1000 Then TargetBottom = 1000

        Targets(1, Zone) = TargetTop
        Targets(2, Zone) = TargetBottom

        ' Set all fans the same speed
        If Zone = 0 Then
          For i As Integer = 0 To Targets.GetUpperBound(1)
            Targets(1, i) = TargetTop
            Targets(2, i) = TargetBottom

            .Setpoint_FanTop(i) = TargetTop
            .Setpoint_FanBottom(i) = TargetBottom
            .Setpoint_FanTopToAdjust(i) = True
            .Setpoint_FanBottomToAdjust(i) = True
          Next i
        Else
          ' Update setpoints for specific zone
          .Setpoint_FanTop(Zone) = TargetTop
          .Setpoint_FanBottom(Zone) = TargetBottom
          .Setpoint_FanTopToAdjust(Zone) = True
          .Setpoint_FanBottomToAdjust(Zone) = True
        End If

      End If

      State = EState.Active
      Return True
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = EState.Off
    TargetTop = 0
    TargetBottom = 0
    ' Clear these on cancel so the IOTopFan and IOBottomFan properties will return 0 if the command is not active
    For i As Integer = 0 To Targets.GetUpperBound(1)
      Targets(1, i) = 0
      Targets(2, i) = 0
    Next i
  End Sub

  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property

End Class

