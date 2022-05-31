<Command("Air Temp", "Zone: |0-8| Setpoint: |0-450|F Deviance: |0-50|F", "", "'2", ""),
  Description("Air Temperature setpoints for oven zone, including deviance."), Category("Oven Functions")>
Public Class AT : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly controlCode As ControlCode

  Public Zone As Integer
  Public Target As Integer
  Public Deviance As Integer
  Public Targets(8) As Integer
  Public Deviances(8) As Integer

  Public Enum EState
    Off
    Active
  End Enum
  Property State As EState

  Public Sub New(ByVal controlCode As ControlCode)
    Me.controlCode = controlCode
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With controlCode
      ' Verify command parameters
      If param.GetUpperBound(0) < 3 Then Return True
      If param(1) < 0 OrElse param(1) > 8 Then Return True  ' Zones 1-8, or 0 for all set to same value
      If param(2) < 0 OrElse param(1) > 450 Then Return True

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else
        Zone = param(1)
        Target = MinMax(param(2) * 10, .Parameters.HoneywellSetpointMin, .Parameters.HoneywellSetpointMax)
        Deviance = MinMax(param(3) * 10, 0, 500)

        Targets(Zone) = Target
        Deviances(Zone) = Deviance

        ' Set all Honeywells the same speed of zone = 0
        If Zone = 0 Then
          For i As Integer = 0 To Targets.GetUpperBound(0)
            Targets(i) = Target
            Deviances(i) = Deviance

            .Setpoint_AirTemp(i) = Target
            .Setpoint_AirTempDeviance(i) = Deviance
          Next i
        Else
          .Setpoint_AirTemp(Zone) = Target
          .Setpoint_AirTempDeviance(Zone) = Deviance
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
    Zone = 0
    Target = 0
    Deviance = 0
    ' Clear these on cancel so the IOTopFan and IOBottomFan properties will return 0 if the command is not active
    For i As Integer = 0 To Targets.GetUpperBound(0)
      Targets(i) = 0
      Targets(i) = 0
    Next i
  End Sub

  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property

End Class
