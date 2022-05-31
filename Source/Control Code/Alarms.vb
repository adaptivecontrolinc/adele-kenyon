Public Class Alarms : Inherits MarshalByRefObject
  Private ReadOnly controlcode As ControlCode

  'Control System Mode Alarms
  Public TestModeSelected As Boolean
  Public OverrideModeSelected As Boolean
  Public DebugModeSelected As Boolean

  Public PLC1NotResponding As Boolean
  Public PLC2NotResponding As Boolean

  'Temperature Alarms
  '  Public TempTooHigh As Boolean

  Public ControlPowerFailure As Boolean
  Public EmergencyStop As Boolean

  ' Fan speed controller setpoint deviation
  Public FanTopSpeedSetpointDeviance As Boolean
  Public FanBottomSetpointDeviance As Boolean
  Public FanExhaustSetpointDeviance As Boolean


  Public Sub New(ByVal controlCode As ControlCode)
    Me.controlcode = controlCode
  End Sub

  Public Sub Run()
    With controlcode

      ' Give the system some time to get going before we start checking alarms
      Static PowerUpTimer As New Timer With {.Seconds = 32}
      If Not PowerUpTimer.Finished Then Exit Sub


      'Control Code Modes
      TestModeSelected = (.Parent.Mode = Mode.Test)
      OverrideModeSelected = (.Parent.Mode = Mode.Override)
      DebugModeSelected = (.Parent.Mode = Mode.Debug) And Not (.Parameters.Demo = 1)


      'Power Failure - May need to reset power through pushbutton
      ControlPowerFailure = False ' TODO Not .IO.TpPowerOn


      Static ProgramRunningTimer As New Timer With {.Seconds = 20}
      If Not .Parent.IsProgramRunning Then ProgramRunningTimer.Seconds = 20


      ' TODO
#If 0 Then



      'Fan Top Speed Controllers
      Dim fanTopSpeedSpDeviance As Boolean = False
      For i As Integer = 1 To .FanTop_Speed.Length - 1
        fanTopSpeedSpDeviance = (.FanTop_Speed(i).IsSetpointHigh OrElse .FanTop_Speed(i).IsSetpointLow) AndAlso (.Parameters.AlarmFanSpeedDevianceEnable > 0)
        ' If any element is outside tolerance, signal alarm
        If fanTopSpeedSpDeviance Then Exit For
      Next i
      FanTopSpeedSetpointDeviance = fanTopSpeedSpDeviance AndAlso ProgramRunningTimer.Finished

      'Fan Bottom Speed Contrllers
      Dim fanBottomSpDeviance As Boolean = False
      For i As Integer = 1 To .FanBottom_Speed.Length - 1
        fanBottomSpDeviance = (.FanBottom_Speed(i).IsSetpointHigh OrElse .FanBottom_Speed(i).IsSetpointLow) AndAlso (.Parameters.AlarmFanSpeedDevianceEnable > 0)
        ' If any element is outside tolerance, signal alarm
        If fanBottomSpDeviance Then Exit For
      Next i
      FanBottomSetpointDeviance = fanBottomSpDeviance AndAlso ProgramRunningTimer.Finished

      'Fan Exhaust Speed Contrllers
      Dim fanExhaustSpDeviance As Boolean = False
      For i As Integer = 1 To .FanExhaust_Speed.Length - 1
        fanExhaustSpDeviance = (.FanExhaust_Speed(i).IsSetpointHigh OrElse .FanExhaust_Speed(i).IsSetpointLow) AndAlso (.Parameters.AlarmFanSpeedDevianceEnable > 0)
        ' If any element is outside tolerance, signal alarm
        If fanExhaustSpDeviance Then Exit For
      Next i
      FanExhaustSetpointDeviance = fanExhaustSpDeviance AndAlso ProgramRunningTimer.Finished


#End If


    End With
  End Sub

End Class
