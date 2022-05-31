
<Command("Operator", "", "", "", "60"), Description("Run procedure to log values, signal if paused."), Category("Foreground Function")>
Public Class OP : Inherits MarshalByRefObject : Implements ACCommand
  Private ReadOnly ControlCode As ControlCode

  Public Enum EState
    Off
    Pause
    CheckParameters

    SendWidths
    SendWidthsDelay

    RequestTransport
    SendTransport
    SendFanSpeeds
    WaitForTransport
    LoadLockedSetpoints
    Active
  End Enum
  Property State As EState
  Property StateString As String
  Property Timer As New Timer
  Property TimerOverrun As New Timer
  Property PausedTimer As New TimerUp

  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

  Public Sub ParametersChanged(ByVal ParamArray param() As Integer) Implements ACCommand.ParametersChanged
  End Sub

  Public Function Start(ByVal ParamArray param() As Integer) As Boolean Implements ACCommand.Start
    With ControlCode

      If .LockSetpoints Then
        'Do Not Update the setpoint values due to batch parameter (LS) disabling
        State = EState.LoadLockedSetpoints
      Else
        Timer.TimeRemaining = 2
        State = EState.CheckParameters
      End If

    End With
  End Function

  Public Function Run() As Boolean Implements ACCommand.Run
    With ControlCode
      Select Case State

        Case EState.Off
          StateString = ""

        Case EState.Pause
          StateString = "Paused "

        Case EState.CheckParameters
          StateString = "OP: Check Parameters" & Timer.ToString(1)
          If Timer.Finished Then
            'Honeywell Controllers
            For i As Integer = 1 To .AirTemp_Zone.Length - 1
              If (.Setpoint_AirTemp(i) > 0) Then
                .AirTemp_Zone(i).ChangeSetpoint(.Setpoint_AirTemp(i), .Setpoint_AirTempDeviance(i))
              End If
            Next i
            ' Width Screws Locked - Don't request changes
            If .LockSetpointsWidth Then
              ' Load Active Setpoints - Width Screw
              For i As Integer = 1 To .Width_Screw.Length - 1
                If .Setpoint_WidthScrew(i) = 0 Then .Setpoint_WidthScrew(i) = .Width_Screw(i).ActiveSetpointValue
              Next i
              ' Step On
              Timer.TimeRemaining = 1
              TimerOverrun.Seconds = MinMax(.Parameters.ProgramSetpointTimeOverrun, 20, 300)
              State = EState.RequestTransport
            Else
              'Microspeed Controllers - Width Screw
              For i As Integer = 1 To .Width_Screw.Length - 1
                If (.Setpoint_WidthScrew(i) > 0) Then
                  .Width_Screw(i).ChangeTargetSetpoint(.Setpoint_WidthScrew(i), .Setpoint_WidthDeviance)
                End If
              Next i
            End If
            Timer.TimeRemaining = 10
            TimerOverrun.Seconds = MinMax(.Parameters.ProgramSetpointTimeOverrun, 20, 300)
            State = EState.SendWidths
          End If


        Case EState.SendWidths
          'Width Screws 1-5
          For i As Integer = 1 To .Width_Screw.Length - 1
            If (.Width_Screw(i).SetpointStatus = WidthController.EUpdateState.Offline) Then
              'skip to next controller
              Continue For
            Else
              If (.Width_Screw(i).SetpointStatus <> WidthController.EUpdateState.Verified) AndAlso (.Width_Screw(i).Coms_Node > 0) Then
                Timer.TimeRemaining = 10
                StateString = "Verify Setpoint: Width Screw " & i.ToString
              End If
            End If
          Next i
          ' Display advancing status
          If TimerOverrun.Seconds < 3 Then StateString = "Verify Widths Advancing " & TimerOverrun.ToString
          ' Delay for timer
          If Timer.Finished OrElse TimerOverrun.Finished Then
            Timer.TimeRemaining = 5
            TimerOverrun.Seconds = MinMax(.Parameters.ProgramSetpointTimeOverrun, 20, 300)
            State = EState.RequestTransport
          End If


        Case EState.RequestTransport
          'Honeywell Controllers
          For i As Integer = 1 To .AirTemp_Zone.Length - 1
            If (.AirTemp_Zone(i).Coms_Node = 0) OrElse (.AirTemp_Zone(i).AlarmsCommunicationsLoss) Then
              'skip to next controller
              Continue For
            Else
              If (.AirTemp_Zone(i).SetpointStatus <> HoneyWell.UpdateState.Verified) AndAlso (.AirTemp_Zone(i).Coms_Node > 0) Then
                Timer.TimeRemaining = 1
                StateString = "Verify Setpoint: Zone Temp " & i.ToString
              End If
            End If
          Next i
          ' Display advancing status
          If TimerOverrun.Seconds < 3 Then StateString = "Request Transport Advancing " & TimerOverrun.ToString
          If Timer.Finished OrElse TimerOverrun.Finished Then
            ' Tenter Chain
            If (.Setpoint_TenterChain > 0) Then
              .Tenter.IChangeSetpoint(.Setpoint_TenterChain, True)
              .Tenter.Spt_DevianceHigh = .Setpoint_TenterDevianceHigh
              .Tenter.Spt_DevianceLow = .Setpoint_TenterDevianceLow
            End If
            ' Tenter Chain Left
            If (.Setpoint_TenterLeft > 0) Then
              .TenterLeft.IChangeSetpoint(.Setpoint_TenterLeft, True)
              .TenterLeft.Spt_DevianceHigh = .Setpoint_TenterLeftDeviance
              .TenterLeft.Spt_DevianceLow = .Setpoint_TenterLeftDeviance
            End If
            ' Tenter Chain Right
            If (.Setpoint_TenterRight > 0) Then
              .TenterLeft.IChangeSetpoint(.Setpoint_TenterRight, True)
              .TenterLeft.Spt_DevianceHigh = .Setpoint_TenterRightDeviance
              .TenterLeft.Spt_DevianceLow = .Setpoint_TenterRightDeviance
            End If
            'Overfeed Top
            If (.Setpoint_OverfeedTop > 0) Then
              .OverfeedTop.IChangeSetpoint(.Setpoint_OverfeedTop, True)
              .OverfeedTop.Spt_DevianceHigh = .Setpoint_OverfeedTopDeviance
              .OverfeedTop.Spt_DevianceLow = .Setpoint_OverfeedTopDeviance
            End If
            'Overfeed Bottom
            If (.Setpoint_OverfeedBot > 0) Then
              .OverfeedBot.IChangeSetpoint(.Setpoint_OverfeedBot, True)
              .OverfeedBot.Spt_DevianceHigh = .Setpoint_OverfeedBotDeviance
              .OverfeedBot.Spt_DevianceLow = .Setpoint_OverfeedBotDeviance
            End If
            'Padder
            For i As Integer = 1 To .Padder.Length - 1
              If (.Setpoint_Padder(i) > 0) Then
                .Padder(i).IChangeSetpoint(.Setpoint_Padder(i), True)
                .Padder(i).Spt_DevianceHigh = .Setpoint_PadderDeviance(i)
                .Padder(i).Spt_DevianceLow = .Setpoint_PadderDeviance(i)
              End If
            Next i
            ' Selvage Left
            If (.Setpoint_SelvageLeft > 0) Then
              .SelvageLeft.IChangeSetpoint(.Setpoint_SelvageLeft, True)
              .SelvageLeft.Spt_DevianceHigh = .Setpoint_SelvageDeviance
              .SelvageLeft.Spt_DevianceLow = .Setpoint_SelvageDeviance
            End If
            ' Selvage Right
            If (.Setpoint_SelvageRight > 0) Then
              .SelvageRight.IChangeSetpoint(.Setpoint_SelvageRight, True)
              .SelvageRight.Spt_DevianceHigh = .Setpoint_SelvageDeviance
              .SelvageRight.Spt_DevianceLow = .Setpoint_SelvageDeviance
            End If
            ' Stripper Roll
            'If .ST.IsAdust Then
            '  .Stripper.ChangeTargetSetpoint(.Setpoint_Stripper, .Setpoint_StripperDeviance)
            '  .ST.State = ST.EState.Active
            'End If
            ' Reset Timer
            Timer.TimeRemaining = 1
            TimerOverrun.Seconds = MinMax(.Parameters.ProgramSetpointTimeOverrun, 20, 300)
            State = EState.SendTransport
          End If


        Case EState.SendTransport
          'Tenter Chain
          If Not .Tenter.SetpointComplete Then
            Timer.TimeRemaining = 5
            StateString = "Verify Setpoint: Tenter Chain "
          End If

          'Overfeed Top
          If Not .OverfeedTop.SetpointComplete Then
            Timer.TimeRemaining = 5
            StateString = "Verify Setpoint: Overfeed Top "
          End If
          'Overfeed Bottom
          If Not .OverfeedBot.SetpointComplete Then
            Timer.TimeRemaining = 5
            StateString = "Verify Setpoint: Overfeed Bottom "
          End If
          'Padder 1
          If Not .Padder(1).SetpointComplete Then
            Timer.TimeRemaining = 5
            StateString = "Verify Setpoint: Padder 1 "
          End If
          'Padder 2
          If Not .Padder(2).SetpointComplete Then
            Timer.TimeRemaining = 5
            StateString = "Verify Setpoint: Padder 2 "
          End If
          'Padder Pull Roll
          'If Not .PadderPull.SetpointComplete Then
          '  Timer.TimeRemaining = 5
          '  StateString = "Verify Setpoint: Padder Pull "
          'End If
          'Selvage Left
          If Not .SelvageLeft.SetpointComplete Then
            Timer.TimeRemaining = 5
            StateString = "Verify Setpoint: Selvage Left "
          End If
          'Selvage Right
          If Not .SelvageRight.SetpointComplete Then
            Timer.TimeRemaining = 5
            StateString = "Verify Setpoint: Selvage Right "
          End If
          ' Display advancing status
          If TimerOverrun.Seconds < 3 Then StateString = "Verify Transport Advancing " & TimerOverrun.ToString
          If Timer.Finished OrElse TimerOverrun.Finished Then
            'Fan Top Speed Controllers
            For i As Integer = 1 To .FanTop_Speed.Length - 1
              If (.Setpoint_FanTop(i) > 0) AndAlso (.Setpoint_FanTopToAdjust(i)) Then
                .FanTop_Speed(i).SetpointDesired = .Setpoint_FanTop(i)
                .FanTop_Speed(i).IChangeSetpoint(.Setpoint_FanTop(i), False)
                .FanTop_Speed(i).SetpointDevianceHigh = .Setpoint_FanSpeedTopDeviance
                .FanTop_Speed(i).SetpointDevianceLow = .Setpoint_FanSpeedTopDeviance
                .FanTop_Speed(i).SetpointStatus = FanSpeedControl.EState.Request
                .Setpoint_FanTopToAdjust(i) = False
              End If
            Next i
            'Fan Bottom Speed Controllers
            For i As Integer = 1 To .FanBottom_Speed.Length - 1
              If (.Setpoint_FanBottom(i) > 0) AndAlso .Setpoint_FanBottomToAdjust(i) Then
                .FanBottom_Speed(i).SetpointDesired = .Setpoint_FanBottom(i)
                .FanBottom_Speed(i).IChangeSetpoint(.Setpoint_FanBottom(i), False)
                .FanBottom_Speed(i).SetpointDevianceHigh = .Setpoint_FanSpeedBottomDeviance
                .FanBottom_Speed(i).SetpointDevianceLow = .Setpoint_FanSpeedBottomDeviance
                .FanBottom_Speed(i).SetpointStatus = FanSpeedControl.EState.Request
                .Setpoint_FanBottomToAdjust(i) = False
              End If
            Next i
            ' Fan Exhaust Speed Controllers
            For i As Integer = 1 To .FanExhaust_Speed.Length - 1
              If (.Setpoint_FanExhaust(i) > 0) AndAlso .Setpoint_FanExhaustToAdjust(i) Then
                .FanExhaust_Speed(i).SetpointDesired = .Setpoint_FanExhaust(i)
                .FanExhaust_Speed(i).IChangeSetpoint(.Setpoint_FanExhaust(i), False)
                .FanExhaust_Speed(i).SetpointDevianceHigh = .Setpoint_FanExhaustDeviance
                .FanExhaust_Speed(i).SetpointDevianceLow = .Setpoint_FanExhaustDeviance
                .FanExhaust_Speed(i).SetpointStatus = FanSpeedControl.EState.Request
                .Setpoint_FanExhaustToAdjust(i) = False
              End If
            Next i
            Timer.TimeRemaining = 1
            TimerOverrun.Seconds = MinMax(.Parameters.ProgramSetpointTimeOverrun, 20, 300)
            State = EState.SendFanSpeeds
          End If

        Case EState.SendFanSpeeds
          'Fan Speed Controllers
          For i As Integer = 1 To .FanTop_Speed.Length - 1
            If (.FanTop_Speed(i).SetpointStatus = FanSpeedControl.EState.Offline) Then
              'skip to next controller
              Continue For
            Else
              If (.FanTop_Speed(i).SetpointStatus <> FanSpeedControl.EState.Verified) AndAlso (.FanTop_Speed(i).Plc_SetpointRegister > 0) Then
                Timer.TimeRemaining = 1
                StateString = "Verify Setpoint: Fan Top " & i.ToString
              End If
            End If
          Next i
          For i As Integer = 1 To .FanBottom_Speed.Length - 1
            If (.FanBottom_Speed(i).SetpointStatus = FanSpeedControl.EState.Offline) Then
              'skip to next controller
              Continue For
            Else
              If (.FanBottom_Speed(i).SetpointStatus <> FanSpeedControl.EState.Verified) AndAlso (.FanBottom_Speed(i).Plc_SetpointRegister > 0) Then
                Timer.TimeRemaining = 1
                StateString = "Verify Setpoint: Fan Bottom " & i.ToString
              End If
            End If
          Next i
          For i As Integer = 1 To .FanExhaust_Speed.Length - 1
            If (.FanExhaust_Speed(i).SetpointStatus = FanSpeedControl.EState.Offline) Then
              'skip to next controller
              Continue For
            Else
              If (.FanExhaust_Speed(i).SetpointStatus <> FanSpeedControl.EState.Verified) AndAlso (.FanExhaust_Speed(i).Plc_SetpointRegister > 0) Then
                Timer.TimeRemaining = 1
                StateString = "Verify Setpoint: Fan Exhaust " & i.ToString
              End If
            End If
          Next i
          'Once all controller's setpoints have been verified:
          If Timer.Finished Then
            If .TenterLeft.Plc_SpeedActual < 10 Then '   If .Tenter.DisplayValue < 10 Then         
              State = EState.WaitForTransport
            Else
              State = EState.Active
            End If
          End If

        Case EState.WaitForTransport
          StateString = "(" & .Parent.ProgramNumber.ToString & ") Waiting for Active Transport (Tenter Chain)"
          If .TenterLeft.Plc_SpeedActual > 10 Then
            State = EState.Active
          End If

        Case EState.LoadLockedSetpoints
          ' Update Temperature Setpoints
          For i As Integer = 1 To .AirTemp_Zone.Length - 1
            If .Setpoint_AirTemp(i) = 0 Then .Setpoint_AirTemp(i) = .AirTemp_Zone(i).RemoteValue
          Next i
          ' Width Screw
          For i As Integer = 1 To .Width_Screw.Length - 1
            If .Setpoint_WidthScrew(i) = 0 Then .Setpoint_WidthScrew(i) = .Width_Screw(i).ActiveSetpointValue ' .Width_Screw(i).Plc_SetpointActual
          Next i
          ' Transport
          If .Setpoint_TenterChain = 0 Then
            ' Tenter Microspeed uses Tenter.Setpoint2 for main speed, Setpoint1 is Thread speed
            .Setpoint_TenterChain = .Tenter.Prm_SetpointValue1 ' .Tenter.Setpoint2
          End If
          If .Setpoint_OverfeedTop = 0 Then .Setpoint_OverfeedTop = .OverfeedTop.SetpointActual '
          If .Setpoint_OverfeedBot = 0 Then .Setpoint_OverfeedBot = .OverfeedBot.SetpointActual '
          ' Padders 1 & 2
          For i As Integer = 1 To .Padder.Length - 1
            If .Setpoint_Padder(i) = 0 Then .Setpoint_Padder(i) = .Padder(i).SetpointActual ' ActiveSetpointValue
          Next i
          '  If .Setpoint_PadderPullRoll = 0 Then .Setpoint_PadderPullRoll = .PadderPull.ActiveSetpointValue
          If .Setpoint_SelvageLeft = 0 Then .Setpoint_SelvageLeft = .SelvageLeft.SetpointActual '    ' .SelvageLeft.SetpointActual
          If .Setpoint_SelvageRight = 0 Then .Setpoint_SelvageRight = .SelvageRight.SetpointActual ' ' .SelvageRight.SetpointActual
          ' Fan Controllers
          For i As Integer = 1 To .FanTop_Speed.Length - 1
            If .Setpoint_FanTop(i) = 0 Then .Setpoint_FanTop(i) = .FanTop_Speed(i).Plc_SetpointActual
          Next i
          For i As Integer = 1 To .FanBottom_Speed.Length - 1
            If .Setpoint_FanBottom(i) = 0 Then .Setpoint_FanBottom(i) = .FanBottom_Speed(i).Plc_SetpointActual
          Next i
          For i As Integer = 1 To .FanExhaust_Speed.Length - 1
            If .Setpoint_FanExhaust(i) = 0 Then .Setpoint_FanExhaust(i) = .FanExhaust_Speed(i).Plc_SetpointActual
          Next i
          'Step to active state
          State = EState.Active

        Case EState.Active
          'initial config's sent - now operators can change setpoints if they need to...
          StateString = "(" & .Parent.ProgramNumber.ToString & ") Active: " & .CycleTimeDisplay

      End Select
    End With
  End Function

  Public Sub Cancel() Implements ACCommand.Cancel
    State = EState.Off
  End Sub

  Public ReadOnly Property IsOn() As Boolean Implements ACCommand.IsOn
    Get
      Return (State <> EState.Off)
    End Get
  End Property

  Public ReadOnly Property IsActive As Boolean
    Get
      Return (State = EState.Active)
    End Get
  End Property

  Public ReadOnly Property IsPaused() As Boolean
    Get
      Return (State = EState.Pause)
    End Get
  End Property

End Class
