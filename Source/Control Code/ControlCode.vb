Public Class ControlCode : Inherits MarshalByRefObject : Implements ACControlCode

  Public Parent As ACParent
  Public Parameters As Parameters
  Public IO As IO
  Public Alarms As Alarms
  Public User As User

  ' Check to see if we need to delete dyelots every hour
  Private checkDyelotInterval As Integer = 3600
  Private checkDyelotTimer As New Timer

  'Don't delete more than this number of dyelots at a time - don't want to hang up the database
  Private maximumDyelotsToDelete As Integer = 10



  'Motor Controllers
  Friend SpeedControllers As New List(Of SpeedController)

  Public Tenter As TenterController
  Public TenterLeft As SpeedController
  Public TenterRight As SpeedController

  Public OverfeedTop As SpeedController
  Public OverfeedBot As SpeedController
  Public SelvageLeft As SpeedController
  Public SelvageRight As SpeedController
  'Public Pad1 As PadController
  'Public Pad2 As PadController

  Public Debug_X0PowerOnCount As Integer
  Public Debug_X1InAutomaticCount As Integer
  Public Debug_X2InCombined As Integer
  Public Debug_X3TenterStartReq As Integer
  Public Debug_X4TenterSp12 As Integer
  Public Debug_X5DoffinOn As Integer



  Friend Property FastFlasher As New Flasher(400)
  Friend Property SlowFlasher As New Flasher(800)

  Public Sub New(ByVal parent As ACParent)

    'Load local Settings.xml
    Settings.Load()

    'Save the parent reference locally so we can access parent properties / methods
    Me.Parent = parent

    ' Initialize parameters
    Parameters = New Parameters(Me)

    'Create IO and initialize com ports from settings
    IO = New IO(Me)
    Alarms = New Alarms(Me) 'create Alarms and initialize

    ProgramStoppedTimer.Start()




    'Create a new instance of each of the speed controllers
    Tenter = New TenterController(Me)
    With Tenter
      .IDescription = "Tenter"
      .IDisplayUnits = "YPM"
      .IDisplayUnitsSetpoint = "YPM"
    End With

    TenterLeft = New SpeedController(Me)
    With TenterLeft
      .IDescription = "Tenter L"
      .DisplayUnits = "YPM"
      .IDisplayUnitsSetpoint = "%"
    End With
    SpeedControllers.Add(OverfeedTop)

    TenterRight = New SpeedController(Me)
    With TenterRight
      .IDescription = "Tenter R"
      .DisplayUnits = "YPM"
      .IDisplayUnitsSetpoint = "%"
    End With
    SpeedControllers.Add(OverfeedTop)

    OverfeedTop = New SpeedController(Me)
    With OverfeedTop
      .IDescription = "Overfeed T"
      .DisplayUnits = "YPM"
      .IDisplayUnitsSetpoint = "%"
    End With
    SpeedControllers.Add(OverfeedTop)

    OverfeedBot = New SpeedController(Me)
    With OverfeedBot
      .IDescription = "Overfeed B"
      .DisplayUnits = "YPM"
      .IDisplayUnitsSetpoint = "%"
    End With

    SelvageLeft = New SpeedController(Me)
    With SelvageLeft
      .IDescription = "Selvage L"
      .DisplayUnits = "YPM"
      .IDisplayUnitsSetpoint = "%"
    End With

    SelvageRight = New SpeedController(Me)
    With SelvageRight
      .IDescription = "Selvage R"
      .DisplayUnits = "YPM"
      .IDisplayUnitsSetpoint = "%"
    End With

    'Pad1 = New PadController(Me)
    'With Pad1
    '  .IDescription = "Padder 1"
    '  .IDisplayUnits = "YPM"
    '  .IDisplayUnitsSetpoint = "%"
    'End With

    'Pad2 = New PadController(Me)
    'With Pad2
    '  .IDescription = "Padder 2"
    '  .IDisplayUnits = "YPM"
    '  .IDisplayUnitsSetpoint = "%"
    'End With

    For i As Integer = 1 To Padder.Length - 1
      Padder(i) = New PadController(Me)
      With Padder(i)
        .IDescription = "Padder " & i.ToString
        .DisplayUnits = "YPM"
        .IDisplayUnitsSetpoint = "%"
      End With
    Next i



    'Create a new instance of each of the honeywell controllers
    For i As Integer = 1 To AirTemp_Zone.Length - 1
      AirTemp_Zone(i) = New HoneyWell
      AirTemp_Zone(i).Zone = i
      AirTemp_Zone(i).Description = "Air Temp " & i.ToString
    Next i

    ' Create a new instance of each of the microspeed width controller and add it to the collection
    ' Width Screw Microspeeds
    For i As Integer = 1 To Width_Screw.Length - 1
      Width_Screw(i) = New MicroSpeed
      Width_Screw(i).Zone = i
      If i <= 3 Then
        Width_Screw(i).Description = "Width " & i.ToString
      ElseIf i = 4 Then
        Width_Screw(i).Description = "Width " & i.ToString ' "Width Screw Main "
      ElseIf i = 5 Then
        Width_Screw(i).Description = "Width " & i.ToString ' "Width Screw Rear"
      End If
      Microspeeds.Add(Width_Screw(i))
    Next i

    ' Idler Position
    'IdlerPosition = New MicroSpeed
    'IdlerPosition.Description = "Idler Roll"
    'Microspeeds.Add(IdlerPosition)

#If 0 Then
    ' MICROSPEEDS - REMOVED 2022-05-17


    ' Transport Microspeeds - Ordered by original user interface for now
    Tenter = New MicroSpeed
    Tenter.Description = "Tenter Chain"
    Microspeeds.Add(Tenter)

    TenterLeft = New MicroSpeed
    TenterLeft.Description = "Tenter Lt"
    Microspeeds.Add(TenterLeft)

    TenterRight = New MicroSpeed
    TenterRight.Description = "Tenter Rt"
    Microspeeds.Add(TenterRight)

    OverfeedTop = New MicroSpeed
    OverfeedTop.Description = "Top OV"
    Microspeeds.Add(OverfeedTop)

    OverfeedBot = New MicroSpeed
    OverfeedBot.Description = "Bottom OV"
    Microspeeds.Add(OverfeedBot)

    SelvageLeft = New MicroSpeed
    SelvageLeft.Description = "Selvage Lt"
    Microspeeds.Add(SelvageLeft)

    SelvageRight = New MicroSpeed
    SelvageRight.Description = "Selvage Rt"
    Microspeeds.Add(SelvageRight)

    'PadderPull = New MicroSpeed
    'PadderPull.Description = "Padder Pull Roll"
    'Microspeeds.Add(PadderPull)

    For i As Integer = 1 To Padder.Length - 1
      Padder(i) = New MicroSpeed
      Padder(i).Zone = i
      Padder(i).Description = "Padder " & i.ToString
      Microspeeds.Add(Padder(i))
    Next i

    Conveyor = New MicroSpeed
    Conveyor.Description = "Conveyor"
    Microspeeds.Add(Conveyor)

    'EntranceConveyor = New MicroSpeed
    'EntranceConveyor.Description = "Entrance Conveyor"
    'Microspeeds.Add(EntranceConveyor)

    'TenterConveyor = New MicroSpeed
    'TenterConveyor.Description = "Tenter Conveyor"
    'Microspeeds.Add(TenterConveyor)

    Stripper = New MicroSpeed
    Stripper.Description = "Stripper"
    Microspeeds.Add(Stripper)

    'Scray = New MicroSpeed
    'Scray.Description = "Scray Feed In"
    'Microspeeds.Add(Scray)

    'BatcherOverfeed = New MicroSpeed
    'BatcherOverfeed.Description = "Batcher Overfeed"
    'Microspeeds.Add(BatcherOverfeed)

    'PackingRoll = New MicroSpeed
    'PackingRoll.Description = "Packing Roll"
    'Microspeeds.Add(PackingRoll)

    'MainBatcher = New MicroSpeed
    'MainBatcher.Description = "Main Batcher"
    'Microspeeds.Add(MainBatcher)

    'InclineConveyor = New MicroSpeed
    'InclineConveyor.Description = "Incline Conveyor"
    'Microspeeds.Add(InclineConveyor)

    BiancoDogal = New MicroSpeed
    BiancoDogal.Description = "Bianco Dogal"
    Microspeeds.Add(BiancoDogal)

    FanDucon_Speed = New MicroSpeed
    FanDucon_Speed.Zone = 1
    FanDucon_Speed.Description = "Ducon Fan "
    Microspeeds.Add(FanDucon_Speed)

#End If





    'Create a new instance of each of the Fan speed controller and add it to the collection
    For i As Integer = 1 To FanTop_Speed.Length - 1
      FanTop_Speed(i) = New FanSpeedControl(Me)
      FanTop_Speed(i).Zone = i
      FanTop_Speed(i).Description = "Fan Top " & i.ToString
      FanSpeeds.Add(FanTop_Speed(i))
    Next i

    For i As Integer = 1 To FanBottom_Speed.Length - 1
      FanBottom_Speed(i) = New FanSpeedControl(Me)
      FanBottom_Speed(i).Zone = i
      FanBottom_Speed(i).Description = "Fan Bottom " & i.ToString
      FanSpeeds.Add(FanTop_Speed(i))
    Next i

    For i As Integer = 1 To FanExhaust_Speed.Length - 1
      FanExhaust_Speed(i) = New FanSpeedControl(Me)
      FanExhaust_Speed(i).Zone = i
      FanExhaust_Speed(i).Description = "Exhaust " & i.ToString
      FanSpeeds.Add(FanExhaust_Speed(i))
    Next i

    ' User control for supervisor setpoint adjustment
    User = New User

    parent.DbUpdateSchema("ALTER TABLE [dbo].[Dyelots] ADD [ID] [int] IDENTITY(1,1) NOT NULL")

    ' Interface tweaks
    CustomizeExpertToolStrip()
    CustomizeOperatorToolStrip()

    ' Initialize CheckDyelotTimer
    Me.checkDyelotTimer.Seconds = checkDyelotInterval
  End Sub

  Public Sub Run() Implements ACControlCode.Run

    ' Set a global date now at the beginning of each scan so timers don't constantly call system time
    CurrentTime = Date.UtcNow

    'Simulate Demo Conditions
    If (Parent.Mode = Mode.Debug) Then Demo()

    'Turn off all outputs
    IO.ClearOutputs()

    'Set program state change timers and determine Time-In-Step variables
    CheckProgramStateChanges()

    'Set Setpoint values if PowerUp has occurred, making sure that first scan has completed
    CheckPowerUpSetpoints()

    'Generate fast and slow flash - these will be used in a few places
    Static FastFlasher As New Flasher(400) : Dim FastFlash As Boolean = FastFlasher.On
    Static SlowFlasher As New Flasher(800) : Dim SlowFlash As Boolean = SlowFlasher.On

    Dim Halt As Boolean = Parent.IsPaused
    Dim NHalt As Boolean = Not Halt

    'Time to go stuff
    TimeToGo = 0

    'For siren and signal light instead of using signal stuff
    SignalOnRequest = False
    If SignalOnRequest Then
      If SignalRequestedWas = False Then
        SignalRequested = True
        SignalRequestedWas = True
      End If
      'If (IO.RemoteRun = True) Then SignalRequested = False
    Else
      SignalRequested = False
      SignalRequestedWas = False
    End If

    'Convert from C to F
    '    Graph_PlevaTemp1 = MulDiv(IO.PlevaTemp1, 9, 5) + 320
    '    Graph_PlevaTemp2 = MulDiv(IO.PlevaTemp2, 9, 5) + 320
    '    Graph_PlevaTemp3 = MulDiv(IO.PlevaTemp3, 9, 5) + 320
    '    Graph_PlevaTemp4 = MulDiv(IO.PlevaTemp4, 9, 5) + 320

    '    Graph_PlevaHumidity = IO.PlevaHumidity

    'Run Pleva Control Module
    '    Pleva.Run()

    'Run Gas Usage Control Module
    '   With GasUsage
    '     .VolumeFromPLC(IO.CounterCubicFeet)
    '     .HeatContent = Parameters.GasUsageHeatContent
    '     Graph_GasUsed = .GasUseJob.TotalVolume
    '   End With

    '******************************************************************************************************
    ' TEMPERATURE CONTROLLERS
    '******************************************************************************************************
    For i As Integer = 1 To AirTemp_Zone.Length - 1
      With AirTemp_Zone(i)
        .Coms_Node = Parameters.NodeZoneTemp(i)
        .ChangeSetpointEnable = (Parameters.HoneywellSetpointAdjustEnable = 1)
        .SetpointMaximum = Parameters.HoneywellSetpointMax
        .SetpointMinimum = Parameters.HoneywellSetpointMin
        ' Update I/O values
        If Parent.Mode = Mode.Debug Then
          .PresentValue = IO.AirTempActual(1)
          .RemoteValue = IO.RemoteValue(i)
          .WorkingOutput = IO.HoneywellOutput(i)
        End If
        If Not Parent.IsProgramRunning Then
          .LimitLower = AirTemp_Zone(i).SetpointDesired - Parameters.HoneywellDeviationDefault
          .LimitUpper = AirTemp_Zone(i).SetpointDesired + Parameters.HoneywellDeviationDefault
        Else
          .LimitLower = Setpoint_AirTemp(i) - Setpoint_AirTempDeviance(i) ' AirTemp_Zone(i).SetpointDesired - AirTemp_Zone(i).DevianceAllowed
          .LimitUpper = Setpoint_AirTemp(i) + Setpoint_AirTempDeviance(i) ' AirTemp_Zone(i).SetpointDesired + AirTemp_Zone(i).DevianceAllowed
        End If

      End With

      ' Update local communications timespan interval for debugging
      ComsInterval_AirTemp(i) = AirTemp_Zone(i).Coms_ScanInterval
    Next i


    '*****************************************************************************************************
    ' Fan Speed Controllers
    '*****************************************************************************************************
    'Fan Top Speed Controllers
    For i As Integer = 1 To FanTop_Speed.Length - 1
      With FanTop_Speed(i)
        .Plc_SetpointRegister = Parameters.NodeFanSpeedTop(i)
        .WriteEnabled = (Parameters.FanSpeedAdjustEnabled = 1) AndAlso (Parameters.NodeFanSpeedTop(i) > 0)
        .UnitsDisplay = "%"
        .UnitsSetpoint = "%"
        .Prm_Deviance = Parameters.FanSpeedDeviationDefault
        .Prm_SetpointMax = Parameters.FanSpeedSetpointMax
        .Prm_SetpointMin = Parameters.FanSpeedSetpointMin

        If Parameters.FanSpeedDeviationDisable > 0 Then
          .SetpointLimitLower = .SetpointMinimum
          .SetpointLimitUpper = .SetpointMaximum
        Else
          .SetpointLimitLower = Setpoint_FanTop(i) - Setpoint_FanSpeedTopDeviance
          .SetpointLimitUpper = Setpoint_FanTop(i) + Setpoint_FanSpeedTopDeviance
        End If

        ' Single remote PLC for all Fans (Top, Bottom, & Exhaust)
        ComsInterval_FanPlc = .Coms_ScanInterval
      End With
    Next i

    'Fan Bottom Speed Controllers
    For i As Integer = 1 To FanBottom_Speed.Length - 1
      With FanBottom_Speed(i)
        .Plc_SetpointRegister = Parameters.NodeFanSpeedBottom(i)
        .WriteEnabled = (Parameters.FanSpeedAdjustEnabled = 1) AndAlso (Parameters.NodeFanSpeedBottom(i) > 0)
        .UnitsDisplay = "%"
        .UnitsSetpoint = "%"
        .Prm_Deviance = Parameters.FanSpeedDeviationDefault
        .Prm_SetpointMax = Parameters.FanSpeedSetpointMax
        .Prm_SetpointMin = Parameters.FanSpeedSetpointMin

        If Parameters.FanSpeedDeviationDisable > 0 Then
          .SetpointLimitLower = .SetpointMinimum
          .SetpointLimitUpper = .SetpointMaximum
        Else
          .SetpointLimitLower = Setpoint_FanBottom(i) - Setpoint_FanSpeedBottomDeviance
          .SetpointLimitUpper = Setpoint_FanBottom(i) + Setpoint_FanSpeedBottomDeviance
        End If

      End With
    Next i

    'Fan Exhaust Speed Controllers
    For i As Integer = 1 To FanExhaust_Speed.Length - 1
      With FanExhaust_Speed(i)
        .Plc_SetpointRegister = Parameters.NodeFanSpeedExhaust(i)
        .WriteEnabled = (Parameters.FanSpeedAdjustEnabled = 1) AndAlso (Parameters.NodeFanSpeedExhaust(i) > 0)
        .UnitsDisplay = "%"
        .UnitsSetpoint = "%"
        .Prm_Deviance = Parameters.FanSpeedDeviationDefault
        .Prm_SetpointMax = Parameters.ExhaustFanMin
        .Prm_SetpointMin = Parameters.ExhaustFanMax

        If Parameters.FanSpeedDeviationDisable > 0 Then
          .SetpointLimitLower = .SetpointMinimum
          .SetpointLimitUpper = .SetpointMaximum
        Else
          .SetpointLimitLower = Setpoint_FanExhaust(i) - Setpoint_FanExhaustDeviance
          .SetpointLimitUpper = Setpoint_FanExhaust(i) + Setpoint_FanExhaustDeviance
        End If

      End With
    Next i


    '*****************************************************************************************************
    ' FAN DUCON
    '*****************************************************************************************************
#If 0 Then
    With FanDucon_Speed
      .Coms_Node = Parameters.NodeFanSpeedDucon
      .ChangeSetpointEnable = (Parameters.DuconFanAdjustEnabled = 1)
      .UnitsDisplay = "%"
      .UnitsSetpoint = "%"
      .SptDevianceMaximum = Parameters.DuconSpeedDeviationMaximum
      .SetpointMaximum = Parameters.DuconSpeedSetpointMax
      .SetpointMinimum = Parameters.DuconSpeedSetpointMin

      'Upper/Lower Limits based on fan's current setpoint and Command Parameter Deviance Allowed
      If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.DuconSpeedDeviationDefault
      If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.DuconSpeedDeviationDefault


      .LimitLower = Setpoint_FanDucon - Setpoint_FanDuconDeviance
      .LimitUpper = Setpoint_FanDucon + Setpoint_FanDuconDeviance
    End With

#End If



    '--------------------------------------------------------------------------------------------
    ' WIDTH CONTROLLERS                               
    '--------------------------------------------------------------------------------------------
    For i As Integer = 1 To Width_Screw.Length - 1

      With Width_Screw(i)
        .Coms_Node = Parameters.NodeWidthScrew(i)
        .ChangeSetpointEnable = (Parameters.WidthScrewAdjustEnabled >= 1)
        .UnitsDisplay = "in"
        .UnitsSetpoint = "in"
        .SetpointMaximum = Parameters.WidthScrewSetpointMax
        .SetpointMinimum = Parameters.WidthScrewSetpointMin

        'Upper/Lower Limits based on Allowed Width Screw Deviance (Width 1 to Width 2 limited, 2-3, 3-4, etc)
        Dim DevianceAllowed_ As Integer
        ' Use Deviation Limits based on adjacent screw positions - Max Deviation between zones
        If Parent.IsProgramRunning Then
          DevianceAllowed_ = MinMax(Width_Screw(i).SptDevianceHigh, 0, 200) '20.0-inch maximum deviance
        Else
          DevianceAllowed_ = MinMax(Parameters.WidthScrewDeviationDefault, 0, 200)
        End If
        ' Disregard setpoint deviation - set based on operator's request
        If Parameters.WidthScrewDeviationDisable > 0 Then DevianceAllowed_ = MinMax(Parameters.WidthScrewDeviationDefault, 500, 2000)

        ' Set Limit Upper & Lower based on adjacent width screw position to prevent chain damage due to alignment 
        '  This Limit Upper & Lower is used when changing single setpoint - use min/max parameters when changing setpoints all at once
        If i = 1 Then
          'First Width in Array - (Lower= width2.setpoint - width1.devianceallowed)
          .LimitLower = IO.WidthSetpoint(i + 1) - DevianceAllowed_
          .LimitUpper = IO.WidthSetpoint(i + 1) + DevianceAllowed_
        ElseIf i = Width_Screw.Length - 1 Then
          'Last Width Screw in array - (Lower= width4.setpoint - width5.devianceallowed)
          .LimitLower = IO.WidthSetpoint(i - 1) - DevianceAllowed_
          .LimitUpper = IO.WidthSetpoint(i - 1) + DevianceAllowed_
        Else
          'all other width screws
          .LimitLower = FindGreater((IO.WidthSetpoint(i - 1) - DevianceAllowed_), (IO.WidthSetpoint(i + 1) - DevianceAllowed_))
          .LimitUpper = FindLesser((IO.WidthSetpoint(i - 1) + DevianceAllowed_), (IO.WidthSetpoint(i + 1) + DevianceAllowed_))
        End If
      End With

      ' Update local communications timespan interval for debugging
      ComsInterval_Width(i) = Width_Screw(i).Coms_ScanInterval
    Next i


    '--------------------------------------------------------------------------------------------
    ' IDLER ROLL POSITION                             
    '--------------------------------------------------------------------------------------------
#If 0 Then
        With IdlerPosition
          .Coms_Node = Parameters.NodeIdlerRoll
          .ChangeSetpointEnable = (Parameters.IdlerRollAdjustEnable = 1)
          .UnitsDisplay = "in"
          .UnitsSetpoint = "in"
          .SptDevianceMaximum = Parameters.IdlerDeviationMaximum
          .SetpointMaximum = Parameters.IdlerSetpointMax
          .SetpointMinimum = Parameters.IdlerSetpointMin

          'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
          If Parameters.IdlerDeviationDisable > 0 Then
            ' Deviation disabled
            .LimitLower = .SetpointMinimum
            .LimitUpper = .SetpointMaximum
          Else
            .SptDevianceHigh = Setpoint_IdlerRollDeviance
            .SptDevianceLow = Setpoint_IdlerRollDeviance

            If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.IdlerDeviationDefault + 5
            If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.IdlerDeviationDefault

            ' Update Limits
            .LimitLower = MinMax(Setpoint_IdlerRoll - Setpoint_IdlerRollDeviance, Parameters.IdlerSetpointMin, Parameters.IdlerSetpointMax)
            .LimitUpper = MinMax(Setpoint_IdlerRoll + Setpoint_IdlerRollDeviance, Parameters.IdlerSetpointMin + Setpoint_IdlerRollDeviance, Parameters.IdlerSetpointMax)
          End If

          ' Update local communications timespan interval for debugging
          ComsInterval_IdlerPosition = .Coms_ScanInterval
        End With
#End If

    '--------------------------------------------------------------------------------------------
    ' TENTER CHAIN                           
    '--------------------------------------------------------------------------------------------
    With Tenter
      '  .ChangeSetpointEnable = (Parameters.TenterAdjustEnable = 1)

      'Update Setpoint limits
      .Spt_DevianceHigh = Setpoint_TenterDevianceHigh
      .Spt_DevianceLow = Setpoint_TenterDevianceLow

      '[2013-07-18] Bruce & Chad Request 0 deviance to be used on high side when specified
      If Setpoint_TenterDevianceHigh > 0 Then
        .Spt_LimitUpper = Setpoint_TenterChain + Setpoint_TenterDevianceHigh
      Else : .Spt_LimitUpper = Setpoint_TenterChain
      End If

      '[2013-07-18] Bruce & Chad Request parameter SetpointMin used when no low side deviance specified
      If Setpoint_TenterDevianceLow > 0 Then
        .Spt_LimitLower = Setpoint_TenterChain - Setpoint_TenterDevianceLow
      Else : .Spt_LimitLower = .Prm_SetpointMin
      End If


#If 0 Then
      ' MICROSPEED: 
      .Coms_Node = Parameters.NodeTenterChain
      .ChangeSetpointEnable = (Parameters.TenterAdjustEnable = 1)
      .UnitsDisplay = "YPM"
      .UnitsSetpoint = "YPM"
      .SptDevianceMaximum = Parameters.TenterDeviationMaximum
      .SetpointMaximum = Parameters.TenterSetpointMax
      .SetpointMinimum = Parameters.TenterSetpointMin
      '     .Setpoint1Desired = Parameters_TenterSetpointThread ' Update Tenter's additional setpoint properties (display only)
      '     .Setpoint3Desired = Parameters_TenterSetpoint3
      '     .Setpoint4Desired = Parameters_TenterSetpoint4

      'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
      If Parameters.TenterDeviationDisable > 0 Then
        ' Deviation disabled
        .LimitLower = .SetpointMinimum
        .LimitUpper = .SetpointMaximum
      Else
        .SptDevianceHigh = Setpoint_TenterDevianceHigh
        .SptDevianceLow = Setpoint_TenterDevianceLow

        If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.TenterDeviationDefault + 5
        If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.TenterDeviationDefault

        ' Update Limits
        .LimitLower = MinMax(Setpoint_TenterChain - Setpoint_TenterDevianceLow, Parameters.TenterSetpointMin, Parameters.TenterSetpointMax)
        .LimitUpper = MinMax(Setpoint_TenterChain + Setpoint_TenterDevianceHigh, Parameters.TenterSetpointMin + Setpoint_TenterDevianceHigh, Parameters.TenterSetpointMax)
      End If

      ' Update local communications timespan interval for debugging
      ComsInterval_Tenter = .Coms_ScanInterval
#End If

    End With


    '--------------------------------------------------------------------------------------------
    ' TENTER LEFT
    '--------------------------------------------------------------------------------------------
    With TenterLeft
      'Set Default Deviance Values (If Parameter is set)
      If (Setpoint_TenterLeftDeviance = 0) AndAlso (Parameters.TenterLeftDeviationDefault > 0) Then
        Setpoint_TenterLeftDeviance = Parameters.TenterLeftDeviationDefault
      End If

      'Update Setpoint limits
      .Spt_DevianceHigh = Setpoint_TenterLeftDeviance ' Setpoint_TenterDevianceHigh
      .Spt_DevianceLow = Setpoint_TenterLeftDeviance ' Setpoint_TenterDevianceLow




#If 0 Then

      ' Microspeed
      .Coms_Node = Parameters.NodeTenterLeft
      .ChangeSetpointEnable = (Parameters.TenterLeftAdjustEnable = 1)
      .UnitsDisplay = "YPM"
      .UnitsSetpoint = "%"
      .SptDevianceMaximum = Parameters.TenterLeftDeviationMaximum
      .SetpointMaximum = Parameters.TenterLeftSetpointMax
      .SetpointMinimum = Parameters.TenterLeftSetpointMin

      ' Update local communications timespan interval for debugging
      ComsInterval_TenterLeft = .Coms_ScanInterval

      'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
      If Parameters.TenterLeftDeviationDisable > 0 Then
        ' Deviation disabled
        .LimitLower = .SetpointMinimum
        .LimitUpper = .SetpointMaximum
      Else
        If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.TenterLeftDeviationDefault
        If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.TenterLeftDeviationDefault

        .LimitLower = Setpoint_TenterLeft - Setpoint_TenterLeftDeviance
        .LimitUpper = Setpoint_TenterLeft + Setpoint_TenterLeftDeviance
      End If

#End If
    End With


    '--------------------------------------------------------------------------------------------
    ' TENTER RIGHT
    '--------------------------------------------------------------------------------------------
    With TenterRight
      'Set Default Deviance Values (If Parameter is set)
      If (Setpoint_TenterRightDeviance = 0) AndAlso (Parameters.TenterRightDeviationDefault > 0) Then
        Setpoint_TenterRightDeviance = Parameters.TenterRightDeviationDefault
      End If

      'Update Setpoint limits
      .Spt_DevianceHigh = Setpoint_TenterRightDeviance  ' Setpoint_TenterDevianceHigh
      .Spt_DevianceLow = Setpoint_TenterRightDeviance ' Setpoint_TenterDevianceLow


#If 0 Then
            ' Microspeed
      .Coms_Node = Parameters.NodeTenterRight
      .ChangeSetpointEnable = (Parameters.TenterRightAdjustEnable = 1)
      .UnitsDisplay = "YPM"
      .UnitsSetpoint = "%"
      .SptDevianceMaximum = Parameters.TenterRightDeviationMaximum
      .SetpointMaximum = Parameters.TenterRightSetpointMax
      .SetpointMinimum = Parameters.TenterRightSetpointMin

      ' Update local communications timespan interval for debugging
      ComsInterval_TenterRight = .Coms_ScanInterval

      'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
      If Parameters.TenterRightDeviationDisable > 0 Then
        ' Deviation disabled
        .LimitLower = .SetpointMinimum
        .LimitUpper = .SetpointMaximum
      Else
        If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.TenterRightDeviationDefault
        If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.TenterRightDeviationDefault

        .LimitLower = Setpoint_TenterRight - Setpoint_TenterRightDeviance
        .LimitUpper = Setpoint_TenterRight + Setpoint_TenterRightDeviance
      End If

#End If

    End With





    '--------------------------------------------------------------------------------------------
    ' OVERFEED TOP
    '--------------------------------------------------------------------------------------------
    With OverfeedTop
      'Set Default Deviance Values (If Parameter is set)
      If (Setpoint_OverfeedTopDeviance = 0) AndAlso (Parameters.OverfeedTopDeviationDefault > 0) Then
        Setpoint_OverfeedTopDeviance = Parameters.OverfeedTopDeviationDefault
      End If



#If 0 Then

      ' Microspeed
      .Coms_Node = Parameters.NodeOverfeedTop
      .ChangeSetpointEnable = (Parameters.OverfeedTopAdjustEnable = 1)
      .UnitsDisplay = "YPM"
      .UnitsSetpoint = "%"
      .SptDevianceMaximum = Parameters.OverfeedTopDeviationMaximum
      .SetpointMaximum = Parameters.OverfeedTopSetpointMax
      .SetpointMinimum = Parameters.OverfeedTopSetpointMin

      ' Update local communications timespan interval for debugging
      ComsInterval_OverfeedTop = .Coms_ScanInterval

      'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
      If Parameters.OverfeedTopDeviationDisable > 0 Then
        ' Deviation disabled
        .LimitLower = .SetpointMinimum
        .LimitUpper = .SetpointMaximum
      Else
        If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.OverfeedTopDeviationDefault
        If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.OverfeedTopDeviationDefault

        .LimitLower = Setpoint_OverfeedTop - Setpoint_OverfeedTopDeviance
        .LimitUpper = Setpoint_OverfeedTop + Setpoint_OverfeedTopDeviance
      End If

#End If
    End With


    '--------------------------------------------------------------------------------------------
    ' OVERFEED BOTTOM                              
    '--------------------------------------------------------------------------------------------
    With OverfeedBot
      'Set Default Deviance Values (If Parameter is set)
      If (Setpoint_OverfeedBotDeviance = 0) AndAlso (Parameters.OverfeedBottomDeviationDefault > 0) Then
        Setpoint_OverfeedBotDeviance = Parameters.OverfeedBottomDeviationDefault
      End If


#If 0 Then


      ' Microspeed
      .Coms_Node = Parameters.NodeOverfeedBottom
      .ChangeSetpointEnable = (Parameters.OverfeedBottomAdjustEnable = 1)
      .UnitsDisplay = "YPM"
      .UnitsSetpoint = "%"
      .SptDevianceMaximum = Parameters.OverfeedBottomDeviationMaximum
      .SetpointMaximum = Parameters.OverfeedBottomSetpointMax
      .SetpointMinimum = Parameters.OverfeedBottomSetpointMin

      ' Update local communications timespan interval for debugging
      ComsInterval_OverfeedBottom = .Coms_ScanInterval

      'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
      If Parameters.OverfeedBottomDeviationDisable > 0 Then
        ' Deviation disabled
        .LimitLower = .SetpointMinimum
        .LimitUpper = .SetpointMaximum
      Else
        If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.OverfeedBottomDeviationDefault
        If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.OverfeedBottomDeviationDefault

        .LimitLower = Setpoint_OverfeedBot - Setpoint_OverfeedBotDeviance
        .LimitUpper = Setpoint_OverfeedBot + Setpoint_OverfeedBotDeviance
      End If

#End If
    End With


    '--------------------------------------------------------------------------------------------
    ' SELVAGE LEFT                             
    '--------------------------------------------------------------------------------------------
    With SelvageLeft
      'Set Default Deviance Values (If Parameter is set)
      If (Setpoint_SelvageDeviance = 0) AndAlso (Parameters.SelvageLeftDeviationDefault > 0) Then
        Setpoint_SelvageDeviance = Parameters.SelvageLeftDeviationDefault
      End If


#If 0 Then

      ' Microspeed
      .Coms_Node = Parameters.NodeSelageLeft
      .ChangeSetpointEnable = (Parameters.SelvageLeftEnable = 1)
      .UnitsDisplay = "YPM"
      .UnitsSetpoint = "%"
      .SptDevianceMaximum = Parameters.OverfeedBottomDeviationMaximum
      .SetpointMaximum = Parameters.OverfeedBottomSetpointMax
      .SetpointMinimum = Parameters.OverfeedBottomSetpointMin

      ' Update local communications timespan interval for debugging
      ComsInterval_SelvageLeft = .Coms_ScanInterval

      'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
      If Parameters.OverfeedBottomDeviationDisable > 0 Then
        ' Deviation disabled
        .LimitLower = .SetpointMinimum
        .LimitUpper = .SetpointMaximum
      Else
        If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.OverfeedBottomDeviationDefault
        If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.OverfeedBottomDeviationDefault

        .LimitLower = Setpoint_SelvageLeft - Setpoint_SelvageDeviance
        .LimitUpper = Setpoint_SelvageLeft + Setpoint_SelvageDeviance
      End If

#End If
    End With


    '--------------------------------------------------------------------------------------------
    ' SELVAGE RIGHT                            
    '--------------------------------------------------------------------------------------------
    With SelvageRight
      'Set Default Deviance Values (If Parameter is set)
      If (Setpoint_SelvageDeviance = 0) AndAlso (Parameters.SelvageRightDeviationDefault > 0) Then
        Setpoint_SelvageDeviance = Parameters.SelvageRightDeviationDefault
      End If


#If 0 Then

      ' MICROSPEED
      .Coms_Node = Parameters.NodeSelvageRight
      .ChangeSetpointEnable = (Parameters.SelvageRightEnable = 1)
      .UnitsDisplay = "YPM"
      .UnitsSetpoint = "%"
      .SptDevianceMaximum = Parameters.SelvageRightDeviationMaximum
      .SetpointMaximum = Parameters.SelvageRightSetpointMax
      .SetpointMinimum = Parameters.SelvageRightSetpointMin
      ' Update local communications timespan interval for debugging
      ComsInterval_SelvageRight = .Coms_ScanInterval

      'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
      If Parameters.SelvageRightDeviationDisable > 0 Then
        ' Deviation disabled
        .LimitLower = .SetpointMinimum
        .LimitUpper = .SetpointMaximum
      Else
        If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.SelvageRightDeviationDefault
        If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.SelvageRightDeviationDefault

        .LimitLower = Setpoint_SelvageRight - Setpoint_SelvageDeviance
        .LimitUpper = Setpoint_SelvageRight + Setpoint_SelvageDeviance
      End If
#End If

    End With


    '--------------------------------------------------------------------------------------------
    ' PADDER 1 & 2                            
    '--------------------------------------------------------------------------------------------
    For i As Integer = 1 To Padder.Length - 1
      With Padder(i)
        'Set Default Deviance Values (If Parameter is set)
        If (Setpoint_PadderDeviance(i) = 0) AndAlso (Parameters.PadderDeviationDefault > 0) Then
          Setpoint_PadderDeviance(i) = Parameters.PadderDeviationDefault
        End If




#If 0 Then
        ' MICROSPEED VARIABLES:
        .Coms_Node = Parameters.NodePadder(i)
        .ChangeSetpointEnable = (Parameters.PadderAdjustEnable = 1)
        .UnitsDisplay = "YPM"
        .UnitsSetpoint = "%"
        .SptDevianceMaximum = Parameters.PadderDeviationMaximum
        .SetpointMaximum = Parameters.PadderSetpointMax
        .SetpointMinimum = Parameters.PadderSetpointMin
        ' Update local communications timespan interval for debugging
        ComsInterval_Padder = .Coms_ScanInterval

        'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
        If Parameters.PadderDeviationDisable > 0 Then
          .LimitLower = .SetpointMinimum
          .LimitUpper = .SetpointMaximum
        Else
          If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.PadderDeviationDefault
          If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.PadderDeviationDefault

          .LimitLower = Setpoint_Padder(i) - Setpoint_PadderDeviance(i)
          .LimitUpper = Setpoint_Padder(i) + Setpoint_PadderDeviance(i)
        End If

#End If
      End With
    Next i



    '--------------------------------------------------------------------------------------------
    ' PADDER PULL ROLL                               
    '--------------------------------------------------------------------------------------------
#If 0 Then
        With PadderPull
          'Set Default Deviance Values (If Parameter is set)
          If (Setpoint_PadderPullDeviance = 0) AndAlso (Parameters.PadderPullDeviationDefault > 0) Then
            Setpoint_PadderPullDeviance = Parameters.PadderPullDeviationDefault
          End If

          .Coms_Node = Parameters.NodePadderPull
          .ChangeSetpointEnable = (Parameters.PadderPullAdjustEnable = 1)
          .UnitsDisplay = "YPM"
          .UnitsSetpoint = "%"
          .SptDevianceMaximum = Parameters.PadderPullDeviationMaximum
          .SetpointMaximum = Parameters.PadderPullSetpointMax
          .SetpointMinimum = Parameters.PadderPullSetpointMin
          ' Update local communications timespan interval for debugging
          ComsInterval_PadderPull = .Coms_ScanInterval

          'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
          If Parameters.PadderPullDeviationDisable > 0 Then
            .LimitLower = .SetpointMinimum
            .LimitUpper = .SetpointMaximum
          Else
            If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.PadderPullDeviationDefault
            If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.PadderPullDeviationDefault

            .LimitLower = Setpoint_Padder - Setpoint_PadderPullDeviance
            .LimitUpper = Setpoint_Padder + Setpoint_PadderPullDeviance
          End If
        End With

#End If

    '--------------------------------------------------------------------------------------------
    '  CONVEYOR                               
    '--------------------------------------------------------------------------------------------
#If 0 Then

    With Conveyor
      'Set Default Deviance Values (If Parameter is set)
      If (Setpoint_ConveyorDeviance = 0) AndAlso (Parameters.ConveyorDeviationDefault > 0) Then
        Setpoint_ConveyorDeviance = Parameters.ConveyorDeviationDefault
      End If

      .Coms_Node = Parameters.NodeEntConveyor
      .ChangeSetpointEnable = (Parameters.EntConveyorAdjustEnable = 1)
      .UnitsDisplay = "YPM"
      .UnitsSetpoint = "%"
      .SptDevianceMaximum = Parameters.ConveyorDeviationMaximum
      .SetpointMaximum = Parameters.ConveyorSetpointMax
      .SetpointMinimum = Parameters.ConveyorSetpointMin
      ' Update local communications timespan interval for debugging
      ComsInterval_Conveyor = .Coms_ScanInterval

      'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
      If Parameters.ConveyorDeviationDisable > 0 Then
        .LimitLower = .SetpointMinimum
        .LimitUpper = .SetpointMaximum
      Else
        If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.ConveyorDeviationDefault
        If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.ConveyorDeviationDefault

        .LimitLower = Setpoint_Conveyor - Setpoint_ConveyorDeviance
        .LimitUpper = Setpoint_Conveyor + Setpoint_ConveyorDeviance
      End If
    End With
#End If


    '--------------------------------------------------------------------------------------------
    '  TENTER CONVEYOR                               
    '--------------------------------------------------------------------------------------------
#If 0 Then
        With TenterConveyor
          'Set Default Deviance Values (If Parameter is set)
          If (Setpoint_TenterConveyorDeviance = 0) AndAlso (Parameters.TenterConvDeviationDefault > 0) Then
            Setpoint_TenterConveyorDeviance = Parameters.TenterConvDeviationDefault
          End If

          .Coms_Node = Parameters.NodeEntConveyor
          .ChangeSetpointEnable = (Parameters.EntConveyorAdjustEnable = 1)
          .UnitsDisplay = "YPM"
          .UnitsSetpoint = "%"
          .SptDevianceMaximum = Parameters.EntConvDeviationMaximum
          .SetpointMaximum = Parameters.EntConvSetpointMax
          .SetpointMinimum = Parameters.EntConvSetpointMin
          ' Update local communications timespan interval for debugging
          ComsInterval_EntranceConveyor = .Coms_ScanInterval

          'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
          If Parameters.EntConvDeviationDisable > 0 Then
            .LimitLower = .SetpointMinimum
            .LimitUpper = .SetpointMaximum
          Else
            If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.EntConvDeviationDefault
            If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.EntConvDeviationDefault

            .LimitLower = Setpoint_Conveyor - Setpoint_ConveyorDeviance
            .LimitUpper = Setpoint_Conveyor + Setpoint_ConveyorDeviance
          End If
        End With

#End If


    '--------------------------------------------------------------------------------------------
    '  STRIPPER                               
    '--------------------------------------------------------------------------------------------
#If 0 Then

    With Stripper
      'Set Default Deviance Values (If Parameter is set)
      If (Setpoint_StripperDeviance = 0) AndAlso (Parameters.StripperDeviationDefault > 0) Then
        Setpoint_StripperDeviance = Parameters.StripperDeviationDefault
      End If

      ' Microspeed configuration
      .Coms_Node = Parameters.NodeStripper
      .ChangeSetpointEnable = (Parameters.StripperAdjustEnable = 1)
      .UnitsDisplay = "YPM"
      .UnitsSetpoint = "%"
      .SptDevianceMaximum = Parameters.StripperDeviationMaximum
      .SetpointMaximum = Parameters.StripperSetpointMax
      .SetpointMinimum = Parameters.StripperSetpointMin

      ' Update local communications timespan interval for debugging
      ComsInterval_Stripper = .Coms_ScanInterval

      'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
      If Parameters.StripperDeviationDisable > 0 Then
        .LimitLower = .SetpointMinimum
        .LimitUpper = .SetpointMaximum
      Else
        If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.StripperDeviationDefault
        If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.StripperDeviationDefault

        .LimitLower = Setpoint_Stripper - Setpoint_StripperDeviance
        .LimitUpper = Setpoint_Stripper + Setpoint_StripperDeviance
      End If
    End With
#End If


    '--------------------------------------------------------------------------------------------
    '  SCRAY FEED IN                              
    '--------------------------------------------------------------------------------------------
#If 0 Then
        With Scray
          'Set Default Deviance Values (If Parameter is set)
          If (Setpoint_ScrayFeedDeviance = 0) AndAlso (Parameters.ScrayDeviationDefault > 0) Then
            Setpoint_ScrayFeedDeviance = Parameters.ScrayDeviationDefault
          End If

          ' Microspeed configuration
          .Coms_Node = Parameters.NodeScray
          .ChangeSetpointEnable = (Parameters.ScrayAdjustEnable = 1)
          .UnitsDisplay = "YPM"
          .UnitsSetpoint = "%"
          .SptDevianceMaximum = Parameters.ScrayDeviationMaximum
          .SetpointMaximum = Parameters.ScraySetpointMax
          .SetpointMinimum = Parameters.ScraySetpointMin

          ' Update local communications timespan interval for debugging
          ComsInterval_Scray = .Coms_ScanInterval

          'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
          If Parameters.StripperDeviationDisable > 0 Then
            .LimitLower = .SetpointMinimum
            .LimitUpper = .SetpointMaximum
          Else
            If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.ScrayDeviationDefault
            If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.ScrayDeviationDefault

            .LimitLower = Setpoint_ScrayFeed - Setpoint_ScrayFeedDeviance
            .LimitUpper = Setpoint_ScrayFeed + Setpoint_ScrayFeedDeviance
          End If
        End With

#End If


    '--------------------------------------------------------------------------------------------
    '  BATCHER OVERFEED                            
    '--------------------------------------------------------------------------------------------
#If 0 Then
        With BatcherOverfeed
          'Set Default Deviance Values (If Parameter is set)
          If (Setpoint_BatcherOverfeedDeviance = 0) AndAlso (Parameters.BatcherOverfeedDeviationDefault > 0) Then
            Setpoint_BatcherOverfeedDeviance = Parameters.BatcherOverfeedDeviationDefault
          End If

          ' Microspeed configuration
          .Coms_Node = Parameters.NodeBatcherOverfeed
          .ChangeSetpointEnable = (Parameters.BatcherOverfeedAdjustEnable = 1)
          .UnitsDisplay = "YPM"
          .UnitsSetpoint = "%"
          .SptDevianceMaximum = Parameters.BatcherOverfeedDeviationMaximum
          .SetpointMaximum = Parameters.BatcherOverfeedSetpointMax
          .SetpointMinimum = Parameters.BatcherOverfeedSetpointMin

          ' Update local communications timespan interval for debugging
          ComsInterval_BatcherOverfeed = .Coms_ScanInterval

          'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
          If Parameters.BatcherOverfeedDeviationDisable > 0 Then
            .LimitLower = .SetpointMinimum
            .LimitUpper = .SetpointMaximum
          Else
            If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.BatcherOverfeedDeviationDefault
            If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.BatcherOverfeedDeviationDefault

            .LimitLower = Setpoint_BatcherOverfeed - Setpoint_BatcherOverfeedDeviance
            .LimitUpper = Setpoint_BatcherOverfeed + Setpoint_BatcherOverfeedDeviance
          End If
        End With

#End If


    '--------------------------------------------------------------------------------------------
    '  PACKING ROLL
    '--------------------------------------------------------------------------------------------
#If 0 Then
        With PackingRoll
          'Set Default Deviance Values (If Parameter is set)
          If (Setpoint_PackingRollDeviance = 0) AndAlso (Parameters.PackingRollDeviationDefault > 0) Then
            Setpoint_PackingRollDeviance = Parameters.PackingRollDeviationDefault
          End If

          ' Microspeed configuration
          .Coms_Node = Parameters.NodePackingRoll
          .ChangeSetpointEnable = (Parameters.PackingRollAdjustEnable = 1)
          .UnitsDisplay = "YPM"
          .UnitsSetpoint = "%"
          .SptDevianceMaximum = Parameters.PackingRollDeviationMaximum
          .SetpointMaximum = Parameters.PackingRollSetpointMax
          .SetpointMinimum = Parameters.PackingRollSetpointMin

          ' Update local communications timespan interval for debugging
          ComsInterval_PackingRoll = .Coms_ScanInterval

          'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
          If Parameters.PackingRollDeviationDisable > 0 Then
            .LimitLower = .SetpointMinimum
            .LimitUpper = .SetpointMaximum
          Else
            If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.PackingRollDeviationDefault
            If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.PackingRollDeviationDefault

            .LimitLower = Setpoint_PackingRoll - Setpoint_PackingRollDeviance
            .LimitUpper = Setpoint_PackingRoll + Setpoint_PackingRollDeviance
          End If
        End With

#End If


    '--------------------------------------------------------------------------------------------
    '  MAIN BATCHER
    '--------------------------------------------------------------------------------------------
#If 0 Then
        With MainBatcher
          'Set Default Deviance Values (If Parameter is set)
          If (Setpoint_MainBatcherDeviance = 0) AndAlso (Parameters.MainBatcherDeviationDefault > 0) Then
            Setpoint_MainBatcherDeviance = Parameters.MainBatcherDeviationDefault
          End If

          ' Microspeed configuration
          .Coms_Node = Parameters.NodeMainBatcher
          .ChangeSetpointEnable = (Parameters.MainBatcherAdjustEnable = 1)
          .UnitsDisplay = "YPM"
          .UnitsSetpoint = "%"
          .SptDevianceMaximum = Parameters.MainBatcherDeviationMaximum
          .SetpointMaximum = Parameters.MainBatcherSetpointMax
          .SetpointMinimum = Parameters.MainBatcherSetpointMin

          ' Update local communications timespan interval for debugging
          ComsInterval_MainBatcher = .Coms_ScanInterval

          'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
          If Parameters.MainBatcherDeviationDisable > 0 Then
            .LimitLower = .SetpointMinimum
            .LimitUpper = .SetpointMaximum
          Else
            If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.MainBatcherDeviationDefault
            If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.MainBatcherDeviationDefault

            .LimitLower = Setpoint_MainBatcher - Setpoint_MainBatcherDeviance
            .LimitUpper = Setpoint_MainBatcher + Setpoint_MainBatcherDeviance
          End If
        End With

#End If


    '--------------------------------------------------------------------------------------------
    '  INCLINE CONVEYOR / FEED IN
    '--------------------------------------------------------------------------------------------
#If 0 Then

        With InclineConveyor
          'Set Default Deviance Values (If Parameter is set)
          If (Setpoint_InclineConveyorDeviance = 0) AndAlso (Parameters.InclineConveyorDeviationDefault > 0) Then
            Setpoint_InclineConveyorDeviance = Parameters.InclineConveyorDeviationDefault
          End If

          ' Microspeed configuration
          .Coms_Node = Parameters.NodeInclineConveyor
          .ChangeSetpointEnable = (Parameters.InclineFeedAdjustEnable = 1)
          .UnitsDisplay = "YPM"
          .UnitsSetpoint = "%"
          .SptDevianceMaximum = Parameters.InclineConveyorDeviationMaximum
          .SetpointMaximum = Parameters.InclineConveyorSetpointMax
          .SetpointMinimum = Parameters.InclineConveyorSetpointMin

          ' Update local communications timespan interval for debugging
          ComsInterval_InclineConveyor = .Coms_ScanInterval

          'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
          If Parameters.InclineConveyorDeviationDisable > 0 Then
            .LimitLower = .SetpointMinimum
            .LimitUpper = .SetpointMaximum
          Else
            If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.InclineConveyorDeviationDefault
            If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.InclineConveyorDeviationDefault

            .LimitLower = Setpoint_InclineConveyor - Setpoint_InclineConveyorDeviance
            .LimitUpper = Setpoint_InclineConveyor + Setpoint_InclineConveyorDeviance
          End If
        End With
#End If


    '--------------------------------------------------------------------------------------------
    '  BIANCO DOGAL
    '--------------------------------------------------------------------------------------------
#If 0 Then

    With BiancoDogal
      'Set Default Deviance Values (If Parameter is set)
      If (Setpoint_BiancoDogalDeviance = 0) AndAlso (Parameters.BiancoDogalDeviationDefault > 0) Then
        Setpoint_BiancoDogalDeviance = Parameters.BiancoDogalDeviationDefault
      End If

      ' Microspeed configuration
      .Coms_Node = Parameters.NodeBiancoDogal
      .ChangeSetpointEnable = (Parameters.BiancoDogalAdjustEnable = 1)
      .UnitsDisplay = "YPM"
      .UnitsSetpoint = "%"
      .SptDevianceMaximum = Parameters.BiancoDogalDeviationMaximum
      .SetpointMaximum = Parameters.BiancoDogalSetpointMax
      .SetpointMinimum = Parameters.BiancoDogalSetpointMin

      ' Update local communications timespan interval for debugging
      ComsInterval_BiancoDogal = .Coms_ScanInterval

      'Upper/Lower Limits based on microspeed's current setpoint and Command Parameter Deviance Allowed
      If Parameters.BiancoDogalDeviationDisable > 0 Then
        .LimitLower = .SetpointMinimum
        .LimitUpper = .SetpointMaximum
      Else
        If .SptDevianceHigh = 0 Then .SptDevianceHigh = Parameters.BiancoDogalDeviationDefault
        If .SptDevianceLow = 0 Then .SptDevianceLow = Parameters.BiancoDogalDeviationDefault

        .LimitLower = Setpoint_BiancoDogal - Setpoint_BiancoDogalDeviance
        .LimitUpper = Setpoint_BiancoDogal + Setpoint_BiancoDogalDeviance
      End If

    End With
#End If


    '--------------------------------------------------------------------------------------------
    '  GRAPH VALUES                            
    '--------------------------------------------------------------------------------------------
    Graph_Zone1Temp = IO.AirTempActual(1)
    Graph_Zone2Temp = IO.AirTempActual(2)
    Graph_Zone3Temp = IO.AirTempActual(3)
    Graph_Zone4Temp = IO.AirTempActual(4)
    Graph_Zone5Temp = IO.AirTempActual(5)
    Graph_Zone6Temp = IO.AirTempActual(6)
    Graph_Zone7Temp = IO.AirTempActual(7)
    Graph_Zone8Temp = IO.AirTempActual(8)

    Graph_ZoneTempAvg = MulDiv((Graph_Zone6Temp + Graph_Zone7Temp + Graph_Zone8Temp), 1, 3)

    Graph_WidthScrew1 = IO.WidthSetpoint(1)
    Graph_WidthScrew2 = IO.WidthSetpoint(2)
    Graph_WidthScrew3 = IO.WidthSetpoint(3)
    Graph_WidthScrew4 = IO.WidthSetpoint(4)
    Graph_WidthScrew5 = IO.WidthSetpoint(5)
    '     Graph_WidthScrew6 = IO.WidthSetpointActual(6)

    Graph_TenterLeftSpeed = TenterLeft.Plc_SpeedActual
    Graph_TenterRightSpeed = TenterRight.Plc_SpeedActual
    Graph_OverfeedSpeed = OverfeedTop.Plc_SpeedActual
    Graph_Padder1Speed = Padder(1).Plc_SpeedActual
    Graph_Padder2Speed = Padder(2).Plc_SpeedActual

    Graph_SelvageLeftSpeed = SelvageLeft.Plc_SpeedActual
    Graph_SelvageRightSpeed = SelvageRight.Plc_SpeedActual




    '--------------------------------------------------------------------------------------------
    '  DATABASE CLEANUP
    '--------------------------------------------------------------------------------------------
    If Not Parent.IsProgramRunning Then
      ' Check number of dyelots and maybe delete some old ones - every hour
      If checkDyelotTimer.Finished Then
        CheckDyelots()
        checkDyelotTimer.Seconds = checkDyelotInterval
      End If
    End If

    If Parent.IsPaused Then WasPausedTimer.TimeRemaining = 15
    WasPaused = Not (WasPausedTimer.Finished)



    '------------------------------------------------------------------------------------------
    ' Run alarms, delays, utilities, parameters, sleep, hibernate
    '------------------------------------------------------------------------------------------
    Alarms.Run()

    Delay = GetDelay()
    GetDelayString()

    Parameters.Run()

    CalculateProductionVariables()

    SystemIdle = Not Parent.IsProgramRunning

    Parent.PressButtons(False, RemoteHalt, False, False, False)

    ' This should halt the control if sleeping
    If Parent.IsSleeping AndAlso (Not IsSleepingWas) Then
      IsSleepingWas = True
      RemoteHalt = True
    End If
    If (Not Parent.IsSleeping) AndAlso IsSleepingWas Then
      IsSleepingWas = False
    End If

    ' Available Physical Memory in bytes > bytes/1024 = kilobytes/1024 = megabytes "MB"
    AvailableMemory = Convert.ToInt32(My.Computer.Info.AvailablePhysicalMemory / 1024 / 1024).ToString & " MB"

    ' Update first scan complete flag
    FirstScanDone = True
  End Sub

#Region "DEMO"

  Private Sub Demo()
    If (Parameters.Demo > 0) AndAlso (Parent.Mode = Mode.Debug) Then
      If Parameters.Demo = 7295 Then

        'Set once
        IO.AirTempActual(1) = 3250
        IO.AirTempActual(2) = 3250
        IO.AirTempActual(3) = 3250
        IO.AirTempActual(4) = 3600
        IO.AirTempActual(5) = 3800
        IO.AirTempActual(6) = 3800
        IO.AirTempActual(7) = 3800
        IO.AirTempActual(8) = 3800

        IO.RemoteValue(1) = 3250
        IO.RemoteValue(2) = 3250
        IO.RemoteValue(3) = 3250
        IO.RemoteValue(4) = 3600
        IO.RemoteValue(5) = 3850
        IO.RemoteValue(6) = 3850
        IO.RemoteValue(7) = 3850
        IO.RemoteValue(8) = 3850

        IO.HoneywellOutput(1) = 500
        IO.HoneywellOutput(2) = 455
        IO.HoneywellOutput(3) = 350
        IO.HoneywellOutput(4) = 550
        IO.HoneywellOutput(5) = 605
        IO.HoneywellOutput(6) = 707
        IO.HoneywellOutput(7) = 710
        IO.HoneywellOutput(8) = 720

        '  IO.PlevaTemp1 = 1600
        '  IO.PlevaTemp2 = 1720
        '  IO.PlevaTemp3 = 1850
        '  IO.PlevaTemp4 = 1900

        IO.WidthSetpoint(1) = 1690
        IO.WidthSetpoint(2) = 1750
        IO.WidthSetpoint(3) = 1800
        IO.WidthSetpoint(4) = 1850
        IO.WidthSetpoint(5) = 1820

        IO.FanTopActual(1) = 800
        IO.FanTopActual(2) = 800
        IO.FanTopActual(3) = 800
        IO.FanTopActual(4) = 800
        IO.FanTopActual(5) = 800
        IO.FanTopActual(6) = 800
        IO.FanTopActual(7) = 800
        IO.FanTopActual(8) = 800

        IO.FanBottomActual(1) = 400
        IO.FanBottomActual(2) = 400
        IO.FanBottomActual(3) = 400
        IO.FanBottomActual(4) = 400
        IO.FanBottomActual(5) = 400
        IO.FanBottomActual(6) = 400
        IO.FanBottomActual(7) = 400
        IO.FanBottomActual(8) = 400

        IO.FanExhaustActual(1) = 450
        IO.FanExhaustActual(2) = 450

        Parameters.Demo = 1

      ElseIf Parameters.Demo >= 1 Then

        'Set Microspeed & Honeywell controllers to the debug i/o values

        ' HoneyWell Setpoint Values
        For i As Integer = 1 To AirTemp_Zone.Length - 1
          '     AirTemp_Zone(i).PresentValue = IO.AirTempActual(i)
          '     AirTemp_Zone(i).RemoteValue = IO.RemoteValue(i)
          '     AirTemp_Zone(i).WorkingOutput = IO.HoneywellOutput(i)
        Next i

        'Fan Speeds
        For i As Integer = 1 To FanTop_Speed.Length - 1
          '     FanTop_Speed(i).SetpointActual = IO.FanTopActualSetpoint(i)
        Next i

        For i As Integer = 1 To FanBottom_Speed.Length - 1
          '    FanBottom_Speed(i).SetpointActual = IO.FanBottomActualSetpoint(i)
        Next i

        For i As Integer = 1 To FanExhaust_Speed.Length - 1
          '    FanExhaust_Speed(i).SetpointActual = IO.FanExhaustActualSetpoint(i)
        Next i


        ' Width screw controller setpoint Values
        For i As Integer = 1 To Width_Screw.Length - 1
          '    Width_Screw(i).Plc_SetpointRemote = IO.WidthActualSetpoint(i)
          '    Width_Screw(i).Plc_SetpointActual = IO.WidthActualSetpoint(i)
        Next i


        'Older Micrspeed Demo Logic For Transport control - recode if needed with tenter & speed controller
        'Transport Controllers
        '    If TenterChain.ActiveSetpointValue = 300 Then
        '      TenterChain.ActiveSetpoint = 1
        '    Else : TenterChain.ActiveSetpoint = 2
        '    End If
        '    TenterChain.ActiveSetpointValue = IO.TcSpeedDesired
        '    TenterChain.DisplayValue = IO.TcSpeedDesired

        'Setpoint Represents Percent of TenterChain Speed = Actual (Display) Value
        '   If IO.OtSpeedDesired > 0 Then
        '      OverfeedTop.ActiveSetpointValue = IO.OtSpeedDesired
        '   Else
        '     IO.OtSpeedDesired = CType(MulDiv(OverfeedTop.SetpointDesired, IO.TcSpeedDesired, 1000), Short)
        '   End If
        '   OverfeedTop.DisplayValue = MulDiv(IO.OtSpeedDesired, IO.TcSpeedDesired, 1000)

        'Setpoint Represents Percent of Overfeed Actual Speed = Actual (Display) Value
        '  If IO.PdSpeedDesired > 0 Then
        'Padder.ActiveSetpointValue = IO.PdSpeedDesired
        ' Else
        '   IO.PdSpeedDesired = CType(MulDiv(Padder.SetpointDesired, IO.TcSpeedDesired, 1000), Short)
        ' End If
        'Padder.DisplayValue = MulDiv(IO.PdSpeedDesired, IO.TcSpeedDesired, 1000)

        'Setpoint Represents Percent of Overfeed Actual Speed = Actual (Display) Value
        '  If IO.SlSpeedDesired > 0 Then
        ' Selvage_Left.ActiveSetpointValue = IO.SlSpeedDesired
        'Else
        '  IO.SlSpeedDesired = CType(MulDiv(Selvage_Left.SetpointDesired, IO.TcSpeedDesired, 1000), Short)
        'End If
        'Selvage_Left.DisplayValue = MulDiv(IO.SlSpeedDesired, IO.TcSpeedDesired, 1000)

        'Setpoint Represents Percent of Overfeed Actual Speed = Actual (Display) Value
        ' If IO.SrSpeedDesired > 0 Then
        ' Selvage_Right.ActiveSetpointValue = IO.SrSpeedDesired
        'Else
        '  IO.SrSpeedDesired = CType(MulDiv(Selvage_Right.SetpointDesired, IO.TcSpeedDesired, 1000), Short)
        'End If
        'Selvage_Right.DisplayValue = MulDiv(IO.SrSpeedDesired, IO.TcSpeedDesired, 1000)

      End If
    End If
  End Sub

#End Region


  Public ReadOnly Property Temperature() As String
    Get
      Temperature = (Graph_ZoneTempAvg \ 10) & "F"
    End Get
  End Property

  Public ReadOnly Property Status() As String
    Get
      If Parent.Signal <> "" Then
        Return Parent.Signal
      ElseIf Not Parent.IsProgramRunning Then
        Return "Machine Idle: " & ProgramStoppedTimer.ToString
      ElseIf EStop Then
        Return "EStop Active "
      ElseIf OP.IsOn Then
        Return OP.StateString
      End If
      Return ""
    End Get
  End Property


  Public Function ReadInputs(ByVal dinp() As Boolean, ByVal aninp() As Short, ByVal temp() As Short) As Boolean Implements ACControlCode.ReadInputs
    Return IO.ReadInputs(Parent, dinp, aninp, temp, Me)
  End Function

  Public Sub WriteOutputs(ByVal dout() As Boolean, ByVal anout() As Short) Implements ACControlCode.WriteOutputs
    IO.WriteOutputs(dout, anout, Me)
  End Sub

  Public Sub StartUp() Implements ACControlCode.StartUp
    UpdateDatabase()
  End Sub

  Public Sub ShutDown() Implements ACControlCode.ShutDown
    Settings.Save()
    SystemShuttingDown = True
  End Sub

  Public Sub ProgramStart() Implements ACControlCode.ProgramStart
    ProgramStoppedTime = ProgramStoppedTimer.Seconds
    ProgramStoppedTimer.Pause()
    ProgramRunTimer.Start()
    StartTime = Date.Now.ToString
    Delay = DelayValue.NormalRunning

    ' Change to mimic screen
    Parent.PressButton(ButtonPosition.Operator, 2)
  End Sub

  Public Sub ProgramStop() Implements ACControlCode.ProgramStop
    ProgramRunTime = ProgramRunTimer.Seconds
    ProgramRunTimer.Pause()
    ProgramStoppedTimer.Start()
    LastProgramCycleTime = CycleTime
    Delay = DelayValue.NormalRunning

    ' Clear Program Setpoint Flags
    LockSetpoints = False
    LockSetpointsWidth = False

    ' HoneyWell Setpoint Values
    'For i As Integer = 1 To AirTemp_Zone.Length - 1
    '  Setpoint_AirTempToAdjust(i) = False
    'Next i
    ' Microspeed Setpoint Values
    'For i As Integer = 1 To Width_Screw.Length - 1
    '  Setpoint_WidthScrewToAdjust(i) = False
    'Next i
    'Fan Speeds Top
    For i As Integer = 1 To FanTop_Speed.Length - 1
      Setpoint_FanTopToAdjust(i) = False
    Next i
    ' Fan Speeds Bottom
    For i As Integer = 1 To FanBottom_Speed.Length - 1
      Setpoint_FanBottomToAdjust(i) = False
    Next i
    ' Fan Speeds Exhaust
    For i As Integer = 1 To FanExhaust_Speed.Length - 1
      Setpoint_FanExhaustToAdjust(i) = False
    Next i

  End Sub

  Private Sub SetRunMinutes()
    Dim yards As Integer = 0
    Dim yardsPerMin As Integer = 0


  End Sub

  Private Sub CheckProgramStateChanges()
    'Program running state changes
    Static ProgramWasRunning As Boolean

    If Parent.IsProgramRunning Then            'A Program is running
      CycleTime = ProgramRunTimer.Seconds
      CycleTimeDisplay = TimerString(CycleTime)
      'get end time stuff.
      If GetEndTimeTimer.Finished Or (StepStandardTime <> StepStandardTimeWas) Then
        StepStandardTimeWas = StepStandardTime
        EndTimeMins = GetEndTime(Me)
        'see if current step is overruning or not
        If TimeInStepValue <= StepStandardTime Then
          EndTimeMins += StepStandardTime - TimeInStepValue
        Else
          EndTimeMins += 1
        End If
        EndTime = Date.Now.AddMinutes(EndTimeMins).ToString
        GetEndTimeTimer.TimeRemaining = 60
      End If
    Else
      CycleTime = 0
      CycleTimeDisplay = "0"
      'Reset End-Time Stuff
      EndTime = ""
      StartTime = ""
      EndTimeMins = 0
      'Reset Time-In-Step stuff
      TimeInStepValue = 0
      StepStandardTime = 0

      PowerKWS = 0
      PowerKWH = 0
    End If
    ProgramWasRunning = Parent.IsProgramRunning
    If Not FirstScanDone Then
      ComputerName = My.Computer.Name
      ProgramStoppedTimer.Start()
    End If

    'time-in-step routine
    Static DisplayTIS As Boolean
    If TimeInStepValue > StepStandardTime Then
      If TwoSecondTimer.Finished Then
        DisplayTIS = Not DisplayTIS
        TwoSecondTimer.TimeRemaining = 2
      End If
    Else
      DisplayTIS = True
    End If
    If DisplayTIS Then
      TimeInStep = CStr(TimeInStepValue)
    Else
      TimeInStep = "Overrun"
    End If

    Dim tis As String = Parent.TimeInStep
    Dim f As Integer = tis.IndexOf("/")
    If f <> -1 Then
      TimeInStepValue = CType(tis.Substring(0, f), Integer)
      StepStandardTime = CType(tis.Substring(f + 1), Integer)
    Else
      TimeInStepValue = 0 : StepStandardTime = 0
    End If
  End Sub

  Private Sub CheckPowerUpSetpoints()
    Static FirstScanWasComplete As Boolean
    Try

      'Initialize setpoints at powerup just after FirstScan has completed to make sure inputs are read
      If FirstScanDone AndAlso Not FirstScanWasComplete Then

        ' HoneyWell Setpoint Values
        For i As Integer = 1 To AirTemp_Zone.Length - 1
          Setpoint_AirTemp(i) = IO.RemoteValue(i)
          Setpoint_AirTempDeviance(i) = Parameters.HoneywellDeviationDefault
          '    Setpoint_AirTempToAdjust(i) = False
        Next i

        ' Width Screw setpoints
        For i As Integer = 1 To Width_Screw.Length - 1
          Setpoint_WidthScrew(i) = IO.WidthSetpoint(i)
          Setpoint_WidthDeviance = Parameters.WidthScrewDeviationDefault
          '  Setpoint_WidthScrewToAdjust(i) = False
        Next i

        ' Fan Speeds Top
        For i As Integer = 1 To FanTop_Speed.Length - 1
          Setpoint_FanTop(i) = IO.FanTopActual(i)
          Setpoint_FanSpeedTopDeviance = Parameters.FanSpeedDeviationDefault
          Setpoint_FanTopToAdjust(i) = False
        Next i

        ' Fan Speeds Bottom
        For i As Integer = 1 To FanBottom_Speed.Length - 1
          Setpoint_FanBottom(i) = IO.FanBottomActual(i)
          Setpoint_FanSpeedBottomDeviance = Parameters.FanSpeedDeviationDefault
          Setpoint_FanBottomToAdjust(i) = False
        Next i

        ' Fan Speeds Exhaust
        For i As Integer = 1 To FanExhaust_Speed.Length - 1
          Setpoint_FanExhaust(i) = IO.FanExhaustActual(i)
          Setpoint_FanExhaustDeviance = Parameters.FanSpeedDeviationDefault
        Next i

        'Transport Controllers:

        ' Tenter Chain
        Setpoint_TenterChain = Tenter.SetpointActual
        Setpoint_TenterDevianceHigh = Parameters.TenterDeviationDefault
        Setpoint_TenterDevianceLow = 0  ' [2013-07-18] Use SetpointMin as LimitLower

        ' Overfeed Top
        Setpoint_OverfeedTop = OverfeedTop.SetpointActual
        Setpoint_OverfeedTopDeviance = Parameters.OverfeedTopDeviationDefault

        ' Overfeed Bottom
        Setpoint_OverfeedBot = OverfeedBot.SetpointActual
        Setpoint_OverfeedBotDeviance = Parameters.OverfeedBottomDeviationDefault

        ' Selvage Left
        Setpoint_SelvageLeft = SelvageLeft.SetpointActual
        Setpoint_SelvageDeviance = Parameters.SelvageLeftDeviationDefault

        ' Selvage Right
        Setpoint_SelvageRight = SelvageRight.SetpointActual
        Setpoint_SelvageDeviance = Parameters.SelvageRightDeviationDefault

        ' Padder #1
        Setpoint_Padder(1) = Padder(1).SetpointActual
        Setpoint_PadderDeviance(1) = Parameters.PadderDeviationDefault

        ' Padder #2
        Setpoint_Padder(2) = Padder(2).SetpointActual
        Setpoint_PadderDeviance(2) = Parameters.PadderDeviationDefault


        '   Setpoint_Stripper = Stripper.ActiveSetpointValue ' IO.StripperMsActualSetpoint
        '   Setpoint_StripperDeviance = Parameters.StripperDeviationDefault

      End If
      FirstScanWasComplete = FirstScanDone

    Catch ex As Exception

    End Try
  End Sub

  Public Function GetEndTime(ByVal controlCode As ControlCode) As Integer
    Try
      Dim ret As Integer
      With controlCode
        'Get current program and step number
        Dim progNum = .Parent.ProgramNumber, stepNum = .Parent.StepNumber

        'Get all program steps for this program
        Dim programSteps = Memo.Split(.Parent.PrefixedSteps)

        'Loop through each step starting from the beginning of the program
        Dim startChecking As Boolean
        For Each stepString In programSteps
          Dim stepParts = stepString.Split(Convert.ToChar(255))
          If stepParts.Length >= 1 Then
            If startChecking Then ret += CInt(stepParts(3))
            If CInt(stepParts(0)) = progNum And CInt(stepParts(1)) = stepNum - 1 Then startChecking = True
          End If
        Next stepString
      End With
      Return ret
    Catch
      Return 0
    End Try
  End Function

  Private Sub CalculateProductionVariables()

    'Power Used
    'Assume all motors run at about 68% of rated capacity. 750W is equiv to HP.
    'Power factor = 68% of 750 , convert to watts
    Dim PowerFactor As Long, HPNow As Long
    PowerFactor = (68 * 30) \ 4
    HPNow = 0 'Reset power being used now uasge to 0
    If Utilities_Timer.Finished Then
      'MainPumpHP = Parameters.MainPumpHP
      'If IO.PumpRunning Then HPNow = HPNow + MainPumpHP

      PowerKWS = CInt(PowerKWS + ((HPNow * PowerFactor) / 100))
      Utilities_Timer.TimeRemaining = 10 'specifies how often this routine runs, in seconds
    End If
    If (PowerKWS >= 3600) Then
      PowerKWH = PowerKWH + 1
      PowerKWS = PowerKWS - 3600
    End If

  End Sub

  Private Function GetDelay() As DelayValue
    'Look at sleeping flag and set delay
    If Parent.IsSleeping Then Return DelayValue.Sleeping

#If 0 Then

      'Look at alarms and set delays
      If Alarms.EmergencyStop OrElse Alarms.PLC1NotResponding OrElse Alarms.PLC2NotResponding OrElse _
         Alarms.TestModeSelected OrElse Alarms.OverrideModeSelected OrElse Alarms.DebugModeSelected OrElse _
         Alarms.ControlPowerFailure OrElse Alarms.VesselRTDProbeError OrElse Alarms.StockRTDProbeError OrElse _
         Alarms.BlendFillRTDProbeError OrElse Alarms.ExpansionRTDProbeError OrElse Alarms.TempTooHigh OrElse _
         Alarms.LidNotLocked OrElse Alarms.PumpRunningSignalLost Then Return DelayValue.MachineError

      'Look at if control is paused
      If Parent.IsPaused Then Return DelayValue.Paused

      'Operator Hasn't responded to control signal in appropriate time
      Static operatorDelayTimer As New Timer
      If Parent.ButtonText = "" Then operatorDelayTimer.TimeRemaining = Parameters.StandardTimeSample * 60
      If operatorDelayTimer.Finished Then Return DelayValue.Operator

      'Operator Delay due to pump switch not "On/Auto"
      If Alarms.PumpSwitchOff Then Return DelayValue.Operator

      Return DelayValue.NormalRunning
#End If

  End Function

  Private Sub GetDelayString()
    Select Case Delay
      Case DelayValue.NormalRunning
        DelayString = ""
    End Select
  End Sub

#Region " DELETE OLD DYELOTS "

  Private Sub CheckDyelots()
    Dim sql As String = Nothing
    Try
      sql = "SELECT Count(ID) As CountID, Min(ID) As MinID, Max(ID) As MaxID FROM Dyelots WHERE State=3"
      Dim dr As System.Data.DataRow = Utilities.Sql.GetDataRow(Settings.ConnectionStringLocal, sql)

      ' Make sure we have valid data
      If dr Is Nothing Then Exit Sub
      If dr.IsNull("CountID") OrElse dr.IsNull("MinID") OrElse dr.IsNull("MaxID") Then Exit Sub

      ' Get min and max and dyelot count
      Dim countID As Integer = Utilities.Sql.NullToZeroInteger(dr("CountID"))
      Dim minID As Integer = Utilities.Sql.NullToZeroInteger(dr("MinID"))
      Dim maxID As Integer = Utilities.Sql.NullToZeroInteger(dr("MaxID"))

      ' Just in case
      If (countID <= 0) OrElse (maxID < minID) Then Exit Sub

      ' If the dyelot count is greater than the maximum number lots to keep then delete older lots
      Dim numberOfHistoriesToKeep As Integer = Parameters.NumberOfHistories
      If (numberOfHistoriesToKeep > 0) AndAlso (countID > numberOfHistoriesToKeep) Then
        ' See how many histories we need to delete
        Dim numberOfHistoriesToDelete = countID - numberOfHistoriesToKeep
        ' Don't delete too many lots at once we don't want to hang up the database
        If numberOfHistoriesToDelete > maximumDyelotsToDelete Then numberOfHistoriesToDelete = maximumDyelotsToDelete
        ' Purge the dyelots from the database
        PurgeDispenses(minID + numberOfHistoriesToDelete)
      End If

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub

  Private Sub PurgeDispenses(ByVal belowID As Integer)
    Dim sql As String = Nothing
    Try
      ' Delete all dyelots with id less than the parameter
      sql = "DELETE FROM Dyelots WHERE ID < " & belowID.ToString(Settings.DefaultCulture)
      Utilities.Sql.SqlDelete(Settings.ConnectionStringLocal, sql)

    Catch ex As Exception
      Utilities.Log.LogError(ex, sql)
    End Try
  End Sub

#End Region

End Class
