<Command("Padder Roll", "Zone: |0-2|  Setpoint: |35-175|%  Deviance: |0-50|%", "", "", ""),
Description("Padder Roll speed setpoint Percent of Tenterchain setpoint. (115% = 1.15 * TC_Setpoint)"),
Category("Speed Functions")>
Public Class PR : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public Zone As Integer
  Public Target As Integer
  Public Deviance As Integer
  Public Targets(2) As Integer
  Public Deviances(2) As Integer

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
      ' Check parameter array
      If param.GetUpperBound(0) < 3 Then Return True
      If param(1) < 0 OrElse param(1) > 3 Then Return True

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else
        Zone = param(1)
        Target = MinMax(param(2) * 10, .Parameters.PadderSetpointMin * 10, .Parameters.PadderSetpointMax * 10)     ' 0-100% = 0-10000
        Deviance = MinMax(param(3) * 10, 0, 500)

        Targets(Zone) = Target
        Deviances(Zone) = Deviance

        ' Set all pads to the same speed
        If Zone = 0 Then
          For i As Integer = 0 To Targets.GetUpperBound(0)
            Targets(i) = Target
            Deviances(i) = Deviance

            .Setpoint_Padder(i) = Target
            .Setpoint_PadderDeviance(i) = Deviance
          Next i
        Else
          .Setpoint_Padder(Zone) = Target
          .Setpoint_PadderDeviance(Zone) = Deviance
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
    ' Clear these on cancel so the IOTopFan and IOBottomFan properties will return 0 if the command is not active
    For i As Integer = 0 To Targets.GetUpperBound(0)
      Targets(i) = 0
      Deviances(i) = 0
    Next i
  End Sub

  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property

End Class
