' Version 1.1.2 [2013.07.15] - DH
' Update for new PLC Transport Control > Main Production Setpoint now SP4 (Not SP2 as with Microspeeds)

Public Class Pleva : Inherits MarshalByRefObject
  Private ReadOnly ControlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.ControlCode = controlCode
  End Sub

#Region "Enumeration"
  Public Enum EPlevaTemp      'Fabric Speed Adjustment Due to Pleva Fabric Temperature Input's
    Off
    Stopped
    Threading
    AtSpeed
    Adjust
  End Enum
  Public Enum EPlevaHumidty   'Exhaust Fan Speed Adjustment Due to Pleva Zone Humidity Input
    Off
    Stopped
    Threading
    AtSpeed
    Adjust
  End Enum
#End Region

  Public Sub Run()

    'Setup Local Values with values from ControlCode
    LoadNecessaryVariables(ControlCode)

    'Determine time, in seconds, at desired temperature
    CalculateTimeAtTempActual()

    'Run Pleva Temperature Dwell State Machine to control/monitor Fabric Speed
    RunPlevaTempStateMachine(ControlCode)

    'Run Pleva Humidity State Machine to control/monitor Exhaust Fan Speed
    RunPlevaHumidityStateMachine(ControlCode)

  End Sub

  Private Sub LoadNecessaryVariables(ByVal controlCode As ControlCode)
    Try
      With controlCode

        'Setup necessary variables
        tempZone1_ = .Graph_PlevaTemp1
        tempZone2_ = .Graph_PlevaTemp2
        tempZone3_ = .Graph_PlevaTemp3
        tempZone4_ = .Graph_PlevaTemp4

        speedFabricActual_ = .IO.TcSpeedDesired   'tenths of yards/min (30.0 YPM = 300)
        speedFanActual_ = .IO.FanExhaustActual(1) 'tenths of rev/min (30.0 RPM = 300)

        'Set Fabric Speed Limits
        speedFabricLimitLow_ = Math.Max(.Tenter.SetpointMinimum, .Tenter.LimitLower)  '  (.Tenter.Prm_SetpointMin, .Tenter.Spt_LimitLower)
        speedFabricLimitHigh_ = Math.Min(.Tenter.SetpointMaximum, .Tenter.LimitUpper) '  (.Tenter.Prm_SetpointMax, .Tenter.Spt_LimitUpper)

        'limit tempdesired based on last maximum of last two zones - never reach higher temp
        tempLimitLow_ = 0

        '[2013-07-18] Bruce Dabbs noted histories "0017005100-K02" and "0017017200K-02" where
        ' where previous job had AT(6) setpoint of 340, and new PT temp desired of 360, but due
        ' to the following line, the actual zone 6 temp was used as a limit:
        '       tempLimitHigh_ = .IO.RemoteValue(6)
        'Use new setpoint as limit instead for zone 6 (if lockSetpoints true, will use last value)
        '[2022-03-03] Using Zone 4 for MW&W - they only have 4 zones
        tempLimitHigh_ = .Setpoint_AirTemp(4)

        'Pleva Humidity
        humidityActual_ = .Graph_PlevaHumidity

        'Set Exhaust Fan Speed Limits
        If .FanExhaust_Speed(1).SetpointLimitLower > 0 Then
          speedFanLimitLow_ = Math.Max(.FanExhaust_Speed(1).SetpointLimitLower, .FanExhaust_Speed(1).SetpointMinimum)
        Else
          speedFanLimitLow_ = .FanExhaust_Speed(1).SetpointMinimum
        End If
        If .FanExhaust_Speed(1).SetpointLimitUpper > 0 Then
          speedFanLimitHigh_ = Math.Min(.FanExhaust_Speed(1).SetpointLimitUpper, .FanExhaust_Speed(1).SetpointMaximum)
        Else
          speedFanLimitHigh_ = .FanExhaust_Speed(1).SetpointMaximum
        End If

      End With
    Catch ex As Exception

    End Try
  End Sub

  Private Sub CalculateTimeAtTempActual()
    Try

      'Determine in which zone fabric reaches desired temp, if desired temp is set
      If tempDesired_ = 0 Then
        distanceAtTemp_ = 5 * Parameters_PlevaZoneLength
        If speedFabricActual_ <> 0 Then
          timeAtTempActual_ = (distanceAtTemp_ * 600) / speedFabricActual_ 'Duration (s) of Fabric within Heating Zones
        Else : timeAtTempActual_ = 0
        End If

      ElseIf tempDesired_ <= tempZone1_ Then
        distanceAtTemp_ = (4 * Parameters_PlevaZoneLength)
        If speedFabricActual_ <> 0 Then
          timeAtTempActual_ = (distanceAtTemp_ * 600) / speedFabricActual_
        Else : timeAtTempActual_ = 0
        End If

      ElseIf tempDesired_ > tempZone1_ AndAlso tempDesired_ <= tempZone2_ Then
        distanceAtTemp_ = DistanceAtTemperature(tempDesired_, tempZone1_, tempZone2_, Parameters_PlevaZoneLength) + (3 * Parameters_PlevaZoneLength)
        If speedFabricActual_ <> 0 Then
          timeAtTempActual_ = (distanceAtTemp_ * 600) / speedFabricActual_
        Else : timeAtTempActual_ = 0
        End If

      ElseIf tempDesired_ > tempZone2_ AndAlso tempDesired_ <= tempZone3_ Then
        distanceAtTemp_ = DistanceAtTemperature(tempDesired_, tempZone2_, tempZone3_, Parameters_PlevaZoneLength) + (2 * Parameters_PlevaZoneLength)
        If speedFabricActual_ <> 0 Then
          timeAtTempActual_ = (distanceAtTemp_ * 600) / speedFabricActual_
        Else : timeAtTempActual_ = 0
        End If

      ElseIf tempDesired_ > tempZone3_ AndAlso tempDesired_ <= tempZone4_ Then
        distanceAtTemp_ = DistanceAtTemperature(tempDesired_, tempZone3_, tempZone4_, Parameters_PlevaZoneLength) + Parameters_PlevaZoneLength
        If speedFabricActual_ <> 0 Then
          timeAtTempActual_ = (distanceAtTemp_ * 600) / speedFabricActual_
        Else : timeAtTempActual_ = 0
        End If

      Else 'tempdesired > tempzone4_ (fabric never reaches desired temperature - slow fabric speed)
        'distanceAtTemp_ = DistanceAtTemperature(tempDesired_, tempZone4_, .IO.AirTempActual(6), Parameters_PlevaZoneLength)
        distanceAtTemp_ = Parameters_PlevaZoneLength
        If speedFabricActual_ <> 0 Then
          timeAtTempActual_ = 0
        Else : speedFabricActual_ = 0
        End If

      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex)
    End Try
  End Sub

#Region "Distance Properties"

  Private distanceAtTemp_ As Double
  Public ReadOnly Property DistanceAtTemp() As Double
    Get
      Return distanceAtTemp_
    End Get
  End Property
  Public ReadOnly Property DistanceAtTempStr() As String
    Get
      Return (distanceAtTemp_ / 10).ToString & " yards"
    End Get
  End Property

  Private Function DistanceAtTemperature(ByVal TempDesired As Integer, ByVal TempZoneLow As Integer, ByVal TempZoneHigh As Integer, ByVal DistanceRemaining As Integer) As Double
    Dim distance As Integer
    Try
      distance = DistanceRemaining - MulDiv(Parameters_PlevaZoneLength, (tempDesired_ - TempZoneLow), (TempZoneHigh - TempZoneLow))
      Return distance
    Catch ex As Exception
    End Try
  End Function

#End Region

  Private Sub RunPlevaTempStateMachine(ByVal controlCode As ControlCode)
    Try
      With controlCode

        Select Case StatePT
          Case EPlevaTemp.Off
            statestringPT_ = "PT: Off "
            adjustFabricSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaSpeedInitializeDelay, 15, 300)
            speedFabricAdjustCount_ = 0
            speedFanAdjustCount_ = 0
            If .Parent.IsProgramRunning AndAlso speedFabricActual_ > 0 Then
              statePT_ = EPlevaTemp.Threading
            End If

          Case EPlevaTemp.Stopped
            statestringPT_ = "PT: Fabric Stopped "
            timeAtTempActual_ = 0
            adjustFabricSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaSpeedInitializeDelay, 15, 300)
            If Not .Parent.IsProgramRunning Then statePT_ = EPlevaTemp.Off
            If (speedFabricActual_ > 0) Then
              statePT_ = EPlevaTemp.Threading
            End If

          Case EPlevaTemp.Threading
            If (.Tenter.ActiveSetpoint = 1) Then
              statestringPT_ = "PT: Fabric Threading (SP " & .Tenter.ActiveSetpoint.ToString & ") "
            ElseIf (.IO.FanExhaustActual(1) <= 6000) Then
              statestringPT_ = "PT: Fan Exhaust 1 <= 600rpm "
            End If
            'Delay at least 20sec when switching to Setpoint 2 to allow temps/speeds to stabilize
            adjustFabricSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaSpeedInitializeDelay, 15, 300)
            If Not .Parent.IsProgramRunning Then statePT_ = EPlevaTemp.Off
            If (speedFabricActual_ = 0) Then statePT_ = EPlevaTemp.Stopped
            If (.Tenter.ActiveSetpoint = 4) AndAlso (.IO.FanExhaustActual(1) > 6000) Then
              statePT_ = EPlevaTemp.AtSpeed
            End If
#If 0 Then
            If (.Tenter.Plc_SetpointChActive <> 4) Then
              statestringPT_ = "PT: Fabric Threading (SP " & .Tenter.Plc_SetpointChActive.ToString & ") "
            ElseIf (.IO.FanExhaustActual(1) <= 6000) Then
              statestringPT_ = "PT: Fan Exhaust 1 <= 600rpm "
            End If
            'Delay at least 20sec when switching to Setpoint 2 to allow temps/speeds to stabilize
            adjustFabricSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaSpeedInitializeDelay, 15, 300)
            If Not .Parent.IsProgramRunning Then statePT_ = EPlevaTemp.Off
            If (speedFabricActual_ = 0) Then statePT_ = EPlevaTemp.Stopped
            If (.Tenter.Plc_SetpointChActive = 4) AndAlso (.IO.FanExhaustActual(1) > 6000) Then
              statePT_ = EPlevaTemp.AtSpeed
            End If
#End If


          Case EPlevaTemp.AtSpeed
            'Check that a program is running
            If Not .Parent.IsProgramRunning Then statePT_ = EPlevaTemp.Off
            'Check Tenter & Exhaust Fan running
            If (speedFabricActual_ = 0) Then statePT_ = EPlevaTemp.Stopped
            'Update Speed Fabric Desired?
            speedFabricDesired_ = speedFabricActual_
            'Verify not threading
            If (.Tenter.ActiveSetpoint = 1) OrElse (.IO.FanExhaustActual(1) <= 6000) Then
              statePT_ = EPlevaTemp.Threading
            End If
            If (Parameters_PlevaTemperatureEnable <> 1) Then
              statestringPT_ = "PT: Disabled <> 1 "
              adjustFabricSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaSpeedInitializeDelay, 15, 300)
            ElseIf (tempDesired_ = 0) Then
              statestringPT_ = "PT: Off - Desired Temp Not Set "
              adjustFabricSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaSpeedInitializeDelay, 15, 300)
            ElseIf (timeAtTempDesired_ = 0) Then
              statestringPT_ = "PT: Off - Desired Time Not Set "
              adjustFabricSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaSpeedInitializeDelay, 15, 300)
            Else
              If .IO.TcSpeedDesired <= (speedFabricLimitLow_ + 50) Then
                statestringPT_ = "PT: At Low Speed Limit - " & TimerString(adjustFabricSpeedTimer_.TimeRemaining)
              ElseIf .IO.TcSpeedDesired >= (speedFabricLimitHigh_ - 50) Then
                statestringPT_ = "PT: At High Speed Limit - " & TimerString(adjustFabricSpeedTimer_.TimeRemaining)
              Else
                statestringPT_ = "PT: At Speed - " & TimerString(adjustFabricSpeedTimer_.TimeRemaining)
              End If
            End If
            'Calculate the Time Error each scan
            timeError_ = CInt(timeAtTempActual_) - timeAtTempDesired_
            'Adjust Timer Finished - Perform Calculation
            If adjustFabricSpeedTimer_.Finished Then
              'Reset Timer
              adjustFabricSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaSpeedAdjustDelay, 1, 300)
              'If time error > error deadband
              If Math.Abs(timeError_) > MinMax(Parameters_PlevaDwellTimeDeadband, 2, 30) Then
                'Calculate Adjustment
                speedFabricAdjust_ = MulDiv(timeError_, Parameters_PlevaSpeedAdjustFactor, 1000)
                'Limit adjustment 
                If speedFabricAdjust_ >= 0 Then
                  If speedFabricAdjust_ > Parameters_PlevaSpeedAdjustMax Then speedFabricAdjust_ = Parameters_PlevaSpeedAdjustMax
                Else
                  If speedFabricAdjust_ < -Parameters_PlevaSpeedAdjustMax Then speedFabricAdjust_ = -Parameters_PlevaSpeedAdjustMax
                End If
                'Calculate new desired setpoint
                speedFabricDesired_ += speedFabricAdjust_
                'Restrict the Pleva Adjustment to the command parameter (TC) allowed TenterChain deviance (+/-)
                speedFabricDesired_ = MinMax(speedFabricDesired_, speedFabricLimitLow_, speedFabricLimitHigh_)
                'Tenter Chain May be limited by max/min parameters - no need for new request
                If .Tenter.SetpointDesired <> speedFabricDesired_ Then
                  'Request the new fabric speed update
                  'PLC Method: 
                  '  .Tenter.IChangeSetpoint(SpeedFabricDesired)
                  ' Microspeed method:
                  'Request the new fabric speed update
                  .Tenter.SetpointDesired = speedFabricDesired_
                  .Tenter.SptDevianceHigh = .Setpoint_TenterDevianceHigh
                  .Tenter.SptDevianceLow = .Setpoint_TenterDevianceLow
                  .Tenter.SetpointStatus = MicroSpeed.EUpdateState.Request

                  statePT_ = EPlevaTemp.Adjust
                  speedFabricAdjustCount_ += 1
                End If
              End If
            End If

          Case EPlevaTemp.Adjust
            statestringPT_ = "PT: Adjust Requested to " & (speedFabricDesired_ / 10).ToString.PadLeft(3) & "YPM"
            Dim tenterComplete As Boolean = (.Tenter.SetpointStatus = MicroSpeed.EUpdateState.Sent) OrElse _
                                            (.Tenter.SetpointStatus = MicroSpeed.EUpdateState.Offline)
            If tenterComplete OrElse (.Tenter.ActiveSetpoint <> 4) Then
              adjustFabricSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaSpeedAccelDelay, 3, 120)
              statePT_ = EPlevaTemp.AtSpeed
            End If

        End Select

      End With
    Catch ex As Exception

    End Try
  End Sub

  Private Sub RunPlevaHumidityStateMachine(ByVal controlCode As ControlCode)
    Try
      With controlCode

        Select Case StatePH
          Case EPlevaHumidty.Off
            statestringPH_ = "PH: Off "
            adjustFanSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaFanSpeedInitializeDelay, 15, 120)
            If .Parent.IsProgramRunning AndAlso speedFabricActual_ > 0 Then
              statePH_ = EPlevaHumidty.Threading
            End If
            'Calculate the Humidity Error each scan (if humidity desired is set)
            If humidityDesired_ > 0 Then
              humidityError_ = humidityActual_ - humidityDesired_
            Else : humidityError_ = 0
            End If

          Case EPlevaHumidty.Stopped
            statestringPH_ = "PH: Fabric Stopped "
            adjustFanSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaFanSpeedInitializeDelay, 15, 120)
            If Not .Parent.IsProgramRunning Then statePH_ = EPlevaHumidty.Off
            If (speedFabricActual_ > 0) Then
              statePH_ = EPlevaHumidty.Threading
            End If
            'Calculate the Humidity Error each scan (if humidity desired is set)
            If humidityDesired_ > 0 Then
              humidityError_ = humidityActual_ - humidityDesired_
            Else : humidityError_ = 0
            End If

          Case EPlevaHumidty.Threading
            'If (.Tenter.Plc_SetpointChActive <> 4) Then
            If (.Tenter.ActiveSetpoint = 1) Then
              statestringPH_ = "PH: Fabric Threading (SP " & .Tenter.ActiveSetpoint.ToString & ") "
            ElseIf (.IO.FanExhaustActual(1) <= 6000) Then
              statestringPH_ = "PH: Fan Exhaust 1 <= 600rpm "
            End If
            'Reset Speed Adjust Request
            speedAdjustRequested_ = False
            'Delay at least 15sec when switching to Setpoint 2 to allow temps/speeds to stabilize
            adjustFanSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaFanSpeedInitializeDelay, 15, 120)
            'Fabric (Tenter Chain) "Stopped" or "Threading"
            If Not .Parent.IsProgramRunning Then statePH_ = EPlevaHumidty.Off
            If (speedFabricActual_ = 0) Then statePH_ = EPlevaHumidty.Stopped
            If (.Tenter.ActiveSetpoint = 2) AndAlso (.IO.FanExhaustActual(1) > 6000) Then
              statePH_ = EPlevaHumidty.AtSpeed
            End If
            'Calculate the Humidity Error each scan (if humidity desired is set)
            If humidityDesired_ > 0 Then
              humidityError_ = humidityActual_ - humidityDesired_
            Else : humidityError_ = 0
            End If

          Case EPlevaHumidty.AtSpeed
            If (Parameters_PlevaHumidityEnable <> 1) Then
              statestringPH_ = "PH: PlevaHumidityEnable <> 1 "
              adjustFanSpeedTimer_.TimeRemaining = 30
              speedFanDesired_ = speedFanActual_
            ElseIf (humidityDesired_ = 0) Then
              statestringPH_ = "PH: Off - Desired Humidity Not Set "
              adjustFanSpeedTimer_.TimeRemaining = 30
              speedFanDesired_ = speedFanActual_
            Else
              If .IO.FanExhaustActual(1) <= (speedFanLimitLow_ + 50) Then
                statestringPH_ = "PH: At Low Speed Limit - " & TimerString(adjustFanSpeedTimer_.TimeRemaining)
              ElseIf .IO.FanExhaustActual(1) >= (speedFanLimitHigh_ - 50) Then
                statestringPH_ = "PH: At High Speed Limit - " & TimerString(adjustFanSpeedTimer_.TimeRemaining)
              Else
                statestringPH_ = "PH: At Speed - " & TimerString(adjustFanSpeedTimer_.TimeRemaining)
              End If
            End If
            'Fabric (Tenter Chain) "Stopped" or "Threading"
            If (speedFabricActual_ = 0) Then statePH_ = EPlevaHumidty.Stopped
            If (.Tenter.ActiveSetpoint = 1) OrElse (.IO.FanExhaustActual(1) <= 6000) Then
              statePH_ = EPlevaHumidty.Threading
            End If
            'Program Stopped
            If Not .Parent.IsProgramRunning Then statePH_ = EPlevaHumidty.Off
            'Calculate the Humidity Error each scan
            humidityError_ = humidityActual_ - humidityDesired_
            'Adjust Timer Finished - Perform Calculation
            If adjustFanSpeedTimer_.Finished Then
              adjustFanSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaFanSpeedAdjustDelay, 5, 120)
              If Math.Abs(humidityError_) > MinMax(Parameters_PlevaHumidityDeadband, 1, 250) Then
                speedFanAdjust_ = MulDiv(humidityError_, Parameters_PlevaFanSpeedAdjustFactor, 1000)
                If speedFanAdjust_ < -Parameters_PlevaFanSpeedAdjustMax Then speedFanAdjust_ = -Parameters_PlevaFanSpeedAdjustMax
                If speedFanAdjust_ > Parameters_PlevaFanSpeedAdjustMax Then speedFanAdjust_ = Parameters_PlevaFanSpeedAdjustMax
                speedFanDesired_ = speedFanActual_ + speedFanAdjust_

                'Restrict the Pleva Adjustment to the command parameter (ES) allowed Exhaust Fan Speed deviance (+/-)
                speedFanDesired_ = MinMax(speedFanDesired_, speedFanLimitLow_, speedFanLimitHigh_)

                'Fan Exhaust Speed May be limited by max/min parameters - no need for new request
                If .IO.FanExhaustActual(1) = speedFanDesired_ Then
                  'Already at setpoint, reset timer for next calculation
                  adjustFanSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaFanSpeedAdjustDelay, 5, 120)
                Else
                  'Request the new fabric speed update
                  .FanExhaust_Speed(1).SetpointDesired = speedFanDesired_
                  '  .FanExhaust_Speed(1).SetpointDevianceHigh = .Setpoint_ExhaustFanDeviance
                  '  .FanExhaust_Speed(1).SetpointDevianceLow = .Setpoint_ExhaustFanDeviance
                  .FanExhaust_Speed(1).SetpointStatus = FanSpeedControl.EState.Request
                  speedAdjustRequested_ = True
                  speedFanAdjustCount_ += 1
                End If

                'Bruce want's the ability to update both exhaust fan speeds with the same new value
                If Parameters_PlevaSetBothExhaustFans = 1 Then
                  'Fan Exhaust Speed May be limited by max/min parameters - no need for new request
                  If .IO.FanExhaustActual(2) = speedFanDesired_ Then
                    'Already at setpoint, reset timer for next calculation
                    adjustFanSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaFanSpeedAdjustDelay, 5, 120)
                  Else
                    .FanExhaust_Speed(2).SetpointDesired = speedFanDesired_
                    '  .FanExhaust_Speed(2).SetpointDevianceHigh = .Setpoint_ExhaustFanDeviance
                    '  .FanExhaust_Speed(2).SetpointDevianceLow = .Setpoint_ExhaustFanDeviance
                    .FanExhaust_Speed(2).SetpointStatus = FanSpeedControl.EState.Request
                    speedAdjustRequested_ = True
                  End If
                End If

                'Is a new speed Setpoint required?
                If speedAdjustRequested_ Then statePH_ = EPlevaHumidty.Adjust

              Else
                speedFanDesired_ = speedFanActual_
              End If
            End If

          Case EPlevaHumidty.Adjust
            statestringPH_ = "PH: Adjust Requested to " & (speedFanDesired_ / 10).ToString.PadLeft(3) & "RPM"
            'Check Both Exhaust Fan Controllers for Requested Setpoint Update Status
            Dim fanExhaustComplete1 As Boolean = (.FanExhaust_Speed(1).SetpointStatus = MicroSpeed.EUpdateState.Verified) OrElse
                                            (.FanExhaust_Speed(1).SetpointStatus = MicroSpeed.EUpdateState.Offline) OrElse
                                            (Not .FanExhaust_Speed(1).WriteEnabled)
            Dim fanExhaustComplete2 As Boolean = (.FanExhaust_Speed(2).SetpointStatus = MicroSpeed.EUpdateState.Verified) OrElse
                                            (.FanExhaust_Speed(2).SetpointStatus = MicroSpeed.EUpdateState.Offline) OrElse
                                            (Not .FanExhaust_Speed(2).WriteEnabled) OrElse
                                            (Parameters_PlevaSetBothExhaustFans <> 1)
            'If Both Exhaust Fans were updated (and Exhaust Fan 2 was enabled for mirror update: Parameters_PlevaSetBothExhaustFans=1)
            If fanExhaustComplete1 AndAlso fanExhaustComplete2 Then
              speedAdjustRequested_ = False
              adjustFanSpeedTimer_.TimeRemaining = MinMax(Parameters_PlevaFanSpeedAccelDelay, 5, 120)
              statePH_ = EPlevaHumidty.AtSpeed
            End If
            'Calculate the Humidity Error each scan
            humidityError_ = humidityActual_ - humidityDesired_

        End Select

      End With
    Catch ex As Exception

    End Try
  End Sub

End Class
