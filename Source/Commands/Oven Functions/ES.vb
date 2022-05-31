<Command("Exhaust Fan Speed", "Number: |0-2| Setpoint: |0-100|% Deviance: |0-50|%"),
  Description("Set exhaust fan speed."), Category("Oven Functions")>
Public Class ES : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public Fan As Integer
  Public Target As Integer
  Public Deviance As Integer
  Public Targets(2) As Integer
  Public Deviances(2) As Integer

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
      If param(1) < 0 OrElse param(1) > 3 Then Return True

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
      Else
        Fan = param(1)
        Target = MinMax(param(2) * 10, .Parameters.ExhaustFanMin, .Parameters.ExhaustFanMax)     ' 0-100% = 0-1000
        If Target > 1000 Then Target = 1000
        Deviance = MinMax(param(3) * 10, 0, 500)

        Targets(Fan) = Target
        Deviances(Fan) = Deviance

        ' Set all fans the same speed
        If Fan = 0 Then
          For i As Integer = 0 To Targets.GetUpperBound(0)
            Targets(i) = Target
            Deviances(i) = Deviance

            .Setpoint_FanExhaust(i) = Target
            .Setpoint_FanExhaustDeviance = Deviance
            .Setpoint_FanExhaustToAdjust(i) = True
          Next i
        Else
          .Setpoint_FanExhaust(Fan) = Target
          .Setpoint_FanExhaustDeviance = Deviance
          .Setpoint_FanExhaustToAdjust(Fan) = True
        End If
      End If

      State = EState.Active
      Return True
    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    'State = EState.Off
    Target = 0
    Deviance = 0
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
