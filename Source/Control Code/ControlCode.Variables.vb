Partial Class ControlCode

  Public ComputerName As String

  ' Flash variables
  Friend FlashSlow As Boolean
  Friend FlashFast As Boolean

  '==============================================================================
  'Program and step time displays
  '==============================================================================
  Private CycleTime As Integer
  Private LastProgramCycleTime As Integer
  Public CycleTimeDisplay As String

  'Time control system has been idle
  Public ProgramStoppedTimer As New TimerUp
  Public ProgramStoppedTime As Integer

  'Time program has been running
  Public ProgramRunTimer As New TimerUp
  Public ProgramRunTime As Integer

  'Time to go for this step - displayed on status line
  Public TimeToGo As Integer

  'Current time in step and step overrun
  Private TwoSecondTimer As New Timer
  Public TimeInStepValue As Integer
  Public StepStandardTime As Integer
  Public TimeInStep As String
  Public EStop As Boolean

  'Variables for troubleshooting histories
  Public WasPaused As Boolean
  Private WasPausedTimer As New Timer

  'importing stuff from database
  Public StandardTime As Integer
  Public StartTime As String
  Public EndTime As String
  Public EndTimeMins As Integer
  Public GetEndTimeTimer As New Timer
  Public StepStandardTimeWas As Integer

  'Delay variable values
  Public Delay As DelayValue
  Public DelayString As String

  'For Production Reports
  Public Utilities_Timer As New Timer
  Public OperatorDelayTimer As New Timer
  Public PowerKWS As Integer
  Public PowerKWH As Integer
  Private MainPumpHP As Integer

  '==============================================================================
  'Analog variables to display on the graph
  '==============================================================================

  'Configure Graph to Display width setpoints from 0-2500 (don't display on graph if value less than '1')
  <GraphTrace(10, 2500, 0, 2500, "Black", "%tin"), GraphLabel("50", 500), GraphLabel("100", 1000), GraphLabel("150", 1500), _
    GraphLabel("200", 2000), GraphLabel("WIDTH - 250", 2500)> Public Graph_WidthScrew1 As Integer
  <GraphTrace(1, 2500, 0, 2500, "Black", "%tin")> Public Graph_WidthScrew2 As Integer
  <GraphTrace(1, 2500, 0, 2500, "Black", "%tin")> Public Graph_WidthScrew3 As Integer
  <GraphTrace(1, 2500, 0, 2500, "Black", "%tin")> Public Graph_WidthScrew4 As Integer
  <GraphTrace(1, 2500, 0, 2500, "Black", "%tin")> Public Graph_WidthScrew5 As Integer
  '  <GraphTrace(1, 2500, 0, 2500, "Black", "%tin")> Public Graph_WidthScrew6 As Integer
  '  <GraphTrace(1, 2500, 0, 2500, "Black", "%tin")> Public Graph_WidthScrew7 As Integer

  'Display Temperatures & Humidity from 2700-10000
  Public Graph_ZoneTempAvg As Integer 'Last 3 zones average temp

  <GraphTrace(10, 4500, 2700, 10000, "DarkRed", "%tF"), GraphLabel("50", 500), GraphLabel("100", 1000), GraphLabel("150", 1500),
    GraphLabel("200", 2000), GraphLabel("250", 2500), GraphLabel("300", 3000), GraphLabel("350", 3500), GraphLabel("400", 4000),
    GraphLabel("TEMP/SPEED - 450", 4500)> Public Graph_Zone1Temp As Integer
  <GraphTrace(10, 4500, 2700, 10000, "DarkRed", "%tF")> Public Graph_Zone2Temp As Integer
  <GraphTrace(10, 4500, 2700, 10000, "DarkRed", "%tF")> Public Graph_Zone3Temp As Integer
  <GraphTrace(10, 4500, 2700, 10000, "DarkRed", "%tF")> Public Graph_Zone4Temp As Integer
  <GraphTrace(10, 4500, 2700, 10000, "DarkRed", "%tF")> Public Graph_Zone5Temp As Integer
  <GraphTrace(10, 4500, 2700, 10000, "DarkRed", "%tF")> Public Graph_Zone6Temp As Integer
  <GraphTrace(10, 4500, 2700, 10000, "DarkRed", "%tF")> Public Graph_Zone7Temp As Integer
  <GraphTrace(10, 4500, 2700, 10000, "DarkRed", "%tF")> Public Graph_Zone8Temp As Integer

  Private PlevaHumidityRange, PlevaHumidityUncorrected As Integer

  '  <GraphTrace(10, 4500, 2700, 10000, "Blue", "%tF")> Public Graph_FabricTemp1 As Integer
  '  <GraphTrace(10, 4500, 2700, 10000, "Navy", "%tF")> Public Graph_FabricTemp2 As Integer
  '  <GraphTrace(10, 4500, 2700, 10000, "DodgerBlue", "%tF")> Public Graph_FabricTemp3 As Integer
  '  <GraphTrace(10, 4500, 2700, 10000, "RoyalBlue", "%tF")> Public Graph_FabricTemp4 As Integer
  '  <GraphTrace(10, 4500, 2700, 10000, "Indigo", "%tg/kg")> Public Graph_PlevaHumidity As Integer

  <GraphTrace(10, 1200, 2700, 10000, "DarkBlue", "%tYPM")> Public Graph_TenterLeftSpeed As Integer
  <GraphTrace(10, 1200, 2700, 10000, "Blue", "%tYPM")> Public Graph_TenterRightSpeed As Integer
  <GraphTrace(10, 1200, 2700, 10000, "DarkGreen", "%tYPM")> Public Graph_OverfeedSpeed As Integer
  <GraphTrace(10, 1200, 2700, 10000, "Black", "%tYPM")> Public Graph_Padder1Speed As Integer
  <GraphTrace(10, 1200, 2700, 10000, "Black", "%tYPM")> Public Graph_Padder2Speed As Integer
  <GraphTrace(10, 1200, 2700, 10000, "DarkOrange", "%tYPM")> Public Graph_SelvageLeftSpeed As Integer
  <GraphTrace(10, 1200, 2700, 10000, "DarkOrange", "%tYPM")> Public Graph_SelvageRightSpeed As Integer

  '  <GraphTrace(10, 1200, 2700, 10000, "DarkSlateGrey", "%d cf")> Public Graph_GasUsed As Integer

  '==============================================================================
  'Various Variables
  '==============================================================================

  Public Setpoint_AirTemp(8) As Integer
  Public Setpoint_AirTempDeviance(8) As Integer

  Public Setpoint_FanTop(8) As Integer
  Public Setpoint_FanSpeedTopDeviance As Integer
  Public Setpoint_FanTopToAdjust(8) As Boolean

  Public Setpoint_FanSpeedBottomDeviance As Integer
  Public Setpoint_FanBottom(8) As Integer
  Public Setpoint_FanBottomToAdjust(8) As Boolean        '

  Public Setpoint_FanExhaust(2) As Integer
  Public Setpoint_FanExhaustDeviance As Integer
  Public Setpoint_FanExhaustToAdjust(2) As Boolean

  Public Setpoint_FanDucon As Integer
  Public Setpoint_FanDuconDeviance As Integer

  Public Setpoint_WidthScrew(5) As Integer
  Public Setpoint_WidthDeviance As Integer

  '  Public Setpoint_IdlerRoll As Integer
  '  Public Setpoint_IdlerRollDeviance As Integer

  Public Setpoint_TenterChain As Integer
  Public Setpoint_TenterDevianceHigh As Integer
  Public Setpoint_TenterDevianceLow As Integer
  Public Setpoint_TenterLeft As Integer
  Public Setpoint_TenterLeftDeviance As Integer
  Public Setpoint_TenterRight As Integer
  Public Setpoint_TenterRightDeviance As Integer

  Public Setpoint_OverfeedTop As Integer
  Public Setpoint_OverfeedTopDeviance As Integer
  Public Setpoint_OverfeedBot As Integer
  Public Setpoint_OverfeedBotDeviance As Integer
  Public Setpoint_SelvageLeft As Integer
  Public Setpoint_SelvageRight As Integer
  Public Setpoint_SelvageDeviance As Integer
  Public Setpoint_Padder(2) As Integer
  Public Setpoint_PadderDeviance(2) As Integer
  ' Public Setpoint_PadderPullRoll As Integer
  ' Public Setpoint_PadderPullDeviance As Integer

  Public Setpoint_Conveyor As Integer
  Public Setpoint_ConveyorDeviance As Integer
  '  Public Setpoint_TenterConveyor As Integer
  '  Public Setpoint_TenterConveyorDeviance As Integer

  Public Setpoint_Stripper As Integer
  Public Setpoint_StripperDeviance As Integer

  '  Public Setpoint_ScrayFeed As Integer
  '  Public Setpoint_ScrayFeedDeviance As Integer

  '  Public Setpoint_BatcherOverfeed As Integer
  '  Public Setpoint_BatcherOverfeedDeviance As Integer

  '  Public Setpoint_PackingRoll As Integer
  '  Public Setpoint_PackingRollDeviance As Integer

  '  Public Setpoint_MainBatcher As Integer
  '  Public Setpoint_MainBatcherDeviance As Integer

  '  Public Setpoint_InclineConveyor As Integer
  '  Public Setpoint_InclineConveyorDeviance As Integer

  Public Setpoint_BiancoDogal As Integer
  Public Setpoint_BiancoDogalDeviance As Integer



  '  Public Setpoint_DancerPress As Integer            'Setpoint: |0-999|psi Deviance: |0-999|psi
  '  Public Setpoint_DancerPressDeviance As Integer    '
  '  Public Setpoint_Extractor(2) As Integer           '
  '  Public Setpoint_ExtractorDeviance(2) As Integer   


  Private lockSetpoints_ As Boolean
  Public Property LockSetpoints As Boolean
    Get
      ' Check parameter with value
      If (Parameters.ProgramSetpointEnable = 0) AndAlso Not lockSetpoints_ Then lockSetpoints_ = True
      Return lockSetpoints_
    End Get
    Set(value As Boolean)
      lockSetpoints_ = value
      ' System must be enabled to release new job setpoints
      If Parameters.ProgramSetpointEnable = 0 Then lockSetpoints_ = True
    End Set
  End Property

  Private lockSetpointsWidth_ As Boolean
  Public Property LockSetpointsWidth As Boolean
    Get
      ' Check parameter with value
      If (Parameters.ProgramSetpointWidthEnable = 0) AndAlso Not lockSetpointsWidth_ Then lockSetpointsWidth_ = True
      Return lockSetpointsWidth_
    End Get
    Set(value As Boolean)
      lockSetpointsWidth_ = value
      ' System must be enabled to release new job setpoints
      If Parameters.ProgramSetpointWidthEnable = 0 Then lockSetpointsWidth_ = True
    End Set
  End Property


  ' Air Temp Controllers
  Public AirTemp_Zone(8) As HoneyWell
  '  Public ScrayRoll As HoneyWell           ' TODO

  ' Microspeeds 
  Friend Microspeeds As New List(Of MicroSpeed)

  Public Width_Screw(5) As MicroSpeed
  '  Public IdlerPosition As MicroSpeed


  Public Padder(2) As PadController


#If 0 Then
    Public Tenter As MicroSpeed
  Public TenterLeft As MicroSpeed
  Public TenterRight As MicroSpeed
  Public OverfeedTop As MicroSpeed
  Public OverfeedBot As MicroSpeed
  Public SelvageLeft As MicroSpeed
  Public SelvageRight As MicroSpeed
  '  Public PadderPull As MicroSpeed
  Public Padder(2) As MicroSpeed
  Public Conveyor As MicroSpeed
  '  Public EntranceConveyor As MicroSpeed
  '  Public TenterConveyor As MicroSpeed
  Public Stripper As MicroSpeed
  ' Public Scray As MicroSpeed

  '  Public BatcherOverfeed As MicroSpeed
  '  Public PackingRoll As MicroSpeed
  '  Public MainBatcher As MicroSpeed
  '  Public InclineConveyor As MicroSpeed
  Public BiancoDogal As MicroSpeed


  'Motor Controllers - Fans
  Public FanDucon_Speed As MicroSpeed
#End If

  ' Terminator PLC Fan Speed controller
  Friend FanSpeeds As New List(Of FanSpeedControl)
  Public FanTop_Speed(8) As FanSpeedControl
  Public FanBottom_Speed(8) As FanSpeedControl
  Public FanExhaust_Speed(2) As FanSpeedControl

  '  Public Pleva As New Pleva(Me)
  '  Public GasUsage As GasUsage

  'Testing (May keep Or Remove)
  Public ComsInterval_FanPlc As Integer


  Public ComsInterval_FanDucon As Integer

  ' Mircospeeds:
  Public ComsInterval_Width(7) As Integer
  '  Public ComsInterval_IdlerPosition As Integer
  Public ComsInterval_Tenter As Integer
  Public ComsInterval_TenterLeft As Integer
  Public ComsInterval_TenterRight As Integer
  Public ComsInterval_OverfeedTop As Integer
  Public ComsInterval_OverfeedBottom As Integer
  Public ComsInterval_SelvageLeft As Integer
  Public ComsInterval_SelvageRight As Integer
  Public ComsInterval_PadderPull As Integer
  Public ComsInterval_Padder As Integer
  '  Public ComsInterval_EntranceConveyor As Integer
  '  Public ComsInterval_TenterConveyor As Integer
  Public ComsInterval_Conveyor As Integer

  Public ComsInterval_Stripper As Integer
  Public ComsInterval_Scray As Integer
  Public ComsInterval_BatcherOverfeed As Integer
  Public ComsInterval_PackingRoll As Integer
  Public ComsInterval_MainBatcher As Integer
  Public ComsInterval_InclineConveyor As Integer
  Public ComsInterval_BiancoDogal As Integer

  ' Honeywells:
  Public ComsInterval_AirTemp(8) As Integer
  '  Public ComsInterval_Extractor(2) As Integer

  'Debug Values from Transport PLC
  '  Public Debug_X0PowerOnCount As Integer
  '  Public Debug_X1InAutomaticCount As Integer
  '  Public Debug_X2InCombined As Integer
  '  Public Debug_X3TenterStartReq As Integer
  '  Public Debug_X4TenterSp12 As Integer
  '  Public Debug_X5DoffinOn As Integer

  'SystemIdle variables
  Public SystemIdleTimer As New Timer
  Public SystemIdle As Boolean

  Public AvailableMemory As String

  'booleans for signal stuff
  Public SignalRequested As Boolean
  Public SignalRequestedWas As Boolean
  Public SignalOnRequest As Boolean

  'pause the program if sleeping
  Public IsSleepingWas As Boolean
  Public RemoteHalt As Boolean

  'System shutdown flag
  Friend SystemShuttingDown As Boolean
  Public FirstScanDone As Boolean

End Class
