
Public Class Parameters : Inherits MarshalByRefObject
  'Save a local reference for convenience
  Private controlCode As ControlCode





#Region " ALARM CONFIGURATION "
  <Parameter(0, 10), Category("Alarm Configuration"), Description("Set greater than zero to enable fan speed setpoint deviance alarms.")>
  Public AlarmFanSpeedDevianceEnable As Integer



#End Region

#Region " SETUP "
  Private Const sec_Setup As String = "Setup"

  <Parameter(0, 600), Category(sec_Setup), Description("Automatically log off Supervisor after this time (seconds)")>
  Public AutoLogOffTime As Integer


  <Parameter(0, 10000), Category(sec_Setup), Description("Resets all parameters to default value if magic value is entered.  Set to '1001' for Kenyon 1 and '1002' or Kenyon 2 initialize.")>
  Public InitializeParameters As Integer

  <Parameter(1000, 10000), Category(sec_Setup), Description("Communications timout to turn off all outputs.  Value in seconds.")>
  Public WatchdogTimeout As Integer

  <Parameter(1, 10000), Category(sec_Setup), Description("Time, in seconds, to wait after communication lost before signalling and turning off all outputs")>
  Public PLCComsTime As Integer


  <Parameter(1, 10000), Category(sec_Setup), Description("Time, in seconds, delay when resetting microspeed communications, if lost.")>
  Public MicrospeedComsTime As Integer

  <Parameter(0, 1), Category(sec_Setup), Description("Set to '1' to disregard communications timeout from network loss.")>
  Public PLCComsLossDisregard As Integer

  <Parameter(0, 10), Category(sec_Setup), Description("Smoothing rate for the Raw Analog Inputs & temperatures to prevent bouncing/noise")>
  Public SmoothRate As Integer

  <Parameter(0, 10), Category(sec_Setup), Description("Set to '1' to enable microspeed setpoint adjustment through configuration tab double-click selection.")>
  Public MsSetpointAdjustEnable As Integer

  <Parameter(0, 10000), Category(sec_Setup), Description("Maximum number of histories to keep in the database (0 = unlimited).")>
  Public NumberOfHistories As Integer

  <Parameter(0, 1), Category(sec_Setup), Description("Set to '1' to enable setpoint increment adjustment through configuration tab.")>
  Public SetpointIncrementEnable As Integer

  <Parameter(0, 1), Category(sec_Setup), Description("Set to '1' to enable new program overwriting existing setpoints.  Phase 1 to only be used with Monitoring.  Control Configuration works for single element control.")>
  Public ProgramSetpointEnable As Integer

  <Parameter(0, 10), Category(sec_Setup), Description("Set to '1' to enable new program overwriting existing width setpoints.  Phase 1 to only be used with Monitoring.  Control Configuration works for single element control.")>
  Public ProgramSetpointWidthEnable As Integer

  <Parameter(0, 300), Category(sec_Setup), Description("Seconds to allow attempting to successfully change an device's setpoint during OP command.  Min value 20-seconds.")>
  Public ProgramSetpointTimeOverrun As Integer

  <Parameter(0, 10), Category(sec_Setup), Description("Set to '1' to display all command setpoints, regardless of being used within the program command list.")>
  Public JobDisplayAllCommands As Integer


#End Region


  ' COMMUNICATION NODES
  <Parameter(0, 255), Category("HoneyWell - Communications"), Description("The Communications Node address for the specific zone honeywell controller (0-255).")> Public NodeZoneTemp(8) As Integer

  ' HONEYWELL
  <Parameter(0, 1000), Category("HoneyWell"), Description("The Communications Node address for the specific zone honeywell controller (1-100).")> Public HoneywellSetpointAdjustEnable As Integer
  <Parameter(0, 1000), Category("HoneyWell"), Description("The default allowed deviation when no program deviances have been set (10.0F-100.0F = 100-1000).")> Public HoneywellDeviationDefault As Integer
  <Parameter(2500, 3400), Category("HoneyWell"), Description("The maximum allowed setpoint for the honeywell controllers (350.0F - 375.0F = 3500-3750).")> Public HoneywellSetpointMax As Integer
  <Parameter(2500, 3400), Category("HoneyWell"), Description("The minimum allowed setpoint for the honeywell controllers (250.0F - 340.0F = 3000-3400).")> Public HoneywellSetpointMin As Integer


  ' MICROSPEED PARAMETERS - WIDTH
  Private Const sec_MicrospeedsWidth As String = "Microspeeds - Width"
  <Parameter(0, 1), Category(sec_MicrospeedsWidth), Description("Set to '1' to enable remote/automatic width screw adjustment from Adaptive controller.")> Public WidthScrewAdjustEnabled As Integer
  <Parameter(0, 1000), Category(sec_MicrospeedsWidth), Description("The default allowed deviation when no program deviances have been set (10.0-100.0inches = 100-1000).")> Public WidthScrewDeviationDefault As Integer
  <Parameter(0, 1000), Category(sec_MicrospeedsWidth), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public WidthScrewDeviationDisable As Integer
  <Parameter(0, 3000), Category(sec_MicrospeedsWidth), Description("The maximum allowed setpoint for the microspeed width controllers (0-300inches = 0-300.0).")> Public WidthScrewSetpointMax As Integer
  <Parameter(0, 1000), Category(sec_MicrospeedsWidth), Description("The minimum allowed setpoint for the microspeed width controllers (0-300inches = 0-300.0).")> Public WidthScrewSetpointMin As Integer
  <Parameter(0, 1000), Category(sec_MicrospeedsWidth), Description("Set to '1' to enable remote control of microspeed width controllers.")> Public Parameters_WidthScrewEnable As Integer





#Region "MICROSPEED TRANSPORT PARAMETERS"


  ' SERIAL COMMUNICATIONS NODES
  Private Const sec_MsComs As String = "Microspeeds - Communications "
  Private Const sec_MsComsDesc As String = " serial commmunication node address value (0-255)."

  <Parameter(0, 255), Category(sec_MsComs), Description("Batcher Overfeed" & sec_MsComsDesc)> Public NodeBatcherOverfeed As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Bianco Dogal & sec_MsComsDesc")> Public NodeBiancoDogal As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Entrance Conveyor & sec_MsComsDesc")> Public NodeEntConveyor As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Folder & sec_MsComsDesc")> Public NodeFolder As Integer
  ' <Parameter(0, 255), Category(sec_MsComs), Description("Idler Roll & sec_MsComsDesc")> Public NodeIdlerRoll As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Incline Conveyor Feed In & sec_MsComsDesc")> Public NodeInclineFeedIn As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Incline Conveyor & sec_MsComsDesc")> Public NodeInclineConveyor As Integer

  <Parameter(0, 255), Category(sec_MsComs), Description("Main Batcher" & sec_MsComsDesc)> Public NodeMainBatcher As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Overfeed Top" & sec_MsComsDesc)> Public NodeOverfeedTop As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Overfeed Top" & sec_MsComsDesc)> Public NodeOverfeedBottom As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Selvage Left" & sec_MsComsDesc)> Public NodeSelageLeft As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Selvage Right" & sec_MsComsDesc)> Public NodeSelvageRight As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Packing Roll" & sec_MsComsDesc)> Public NodePackingRoll As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Padder" & sec_MsComsDesc)> Public NodePadder(2) As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Padder Pull" & sec_MsComsDesc)> Public NodePadderPull As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Scray Roll" & sec_MsComsDesc)> Public NodeScray As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Stripper" & sec_MsComsDesc)> Public NodeStripper As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Tenter Chain" & sec_MsComsDesc)> Public NodeTenterChain As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Tenter Left" & sec_MsComsDesc)> Public NodeTenterLeft As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Tenter Right" & sec_MsComsDesc)> Public NodeTenterRight As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Tenter Conveyor" & sec_MsComsDesc)> Public NodeTenterConveyor As Integer
  <Parameter(0, 255), Category(sec_MsComs), Description("Width Screws" & sec_MsComsDesc)> Public NodeWidthScrew(7) As Integer


  ' SETPOINT ADJUST ENABLE
  Private Const sec_MsEnable As String = "Microspeeds - Enable" ' TODO - add these as functions
  Private Const sec_MsEnableDesc As String = "Set to '1' to enable remote control of the microspeed for the "

  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Batcher Overfeed.")> Public BatcherOverfeedAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Bianco Dogal.")> Public BiancoDogalAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Entrance Conveyor Feed In.")> Public EntConveyorAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Incline Conveyor Feed In.")> Public InclineFeedAdjustEnable As Integer
  ' <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Idler Roll.")> Public IdlerRollAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Main Batcher.")> Public MainBatcherAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Padder.")> Public PadderAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Padder Pull Roll.")> Public PadderPullAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Top Overfeed.")> Public OverfeedTopAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Bottom Overfeed.")> Public OverfeedBottomAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Packing Roll.")> Public PackingRollAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Scray Feed In.")> Public ScrayAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Stripper.")> Public StripperAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Stripper.")> Public StripperAutoEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Folder.")> Public FolderAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Selvage Left.")> Public SelvageLeftEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Selvage Right.")> Public SelvageRightEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Tenter chain.")> Public TenterAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Tenter left.")> Public TenterLeftAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Tenter right.")> Public TenterRightAdjustEnable As Integer
  <Parameter(0, 1), Category(sec_MsEnable), Description(sec_MsEnableDesc & "Tenter conveyor.")> Public TentConveyorAdjustEnable As Integer




  ' MICROSPEED PARAMETERS - IDLER ROLL
  'Private Const sec_MicrospeedsIdlerRoll As String = "Microspeeds - Idler Roll"
  '<Parameter(0, 1000), Category(sec_MicrospeedsIdlerRoll), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public IdlerDeviationDefault As Integer
  '<Parameter(0, 1), Category(sec_MicrospeedsIdlerRoll), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public IdlerDeviationDisable As Integer
  '<Parameter(0, 200), Category(sec_MicrospeedsIdlerRoll), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public IdlerDeviationMaximum As Integer
  '<Parameter(0, 1500), Category(sec_MicrospeedsIdlerRoll), Description("The maximum allowed setpoint for the microspeed Overfeed Top speed controllers (0-150.0% = 0-1500).")> Public IdlerSetpointMax As Integer
  '<Parameter(0, 1000), Category(sec_MicrospeedsIdlerRoll), Description("The minimum allowed setpoint for the microspeed Overfeed Top speed controllers (100.0-110.0% = 1000-1100).")> Public IdlerSetpointMin As Integer


  ' MICROSPEED PARAMETERS - TENTER CHAIN "
  Private Const sec_MicrospeedsTenterChain As String = "Microspeeds - Tenter Chain"
  <Parameter(0, 200), Category(sec_MicrospeedsTenterChain), Description("The default allowed deviation when no program deviances have been set (0-200 = 0-20.0 ypm).")> Public TenterDeviationDefault As Integer
  <Parameter(0, 1), Category(sec_MicrospeedsTenterChain), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public TenterDeviationDisable As Integer
  <Parameter(0, 200), Category(sec_MicrospeedsTenterChain), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public TenterDeviationMaximum As Integer
  <Parameter(0, 300), Category(sec_MicrospeedsTenterChain), Description("Interval, in seconds, to disregard a requested setpoint change.  Use to cancel a setpoint adjust request if successful write is delayed.  Set to '0' to disable feature.")> Public TenterMsRequestStopSec As Integer
  <Parameter(0, 300), Category(sec_MicrospeedsTenterChain), Description("Interval, in seconds, to disregard a sent setpoint change.  Use to cancel a setpoint execute command if successful write is delayed.  Set to '0' to disable feature.")> Public TenterMsSentStopSec As Integer
  <Parameter(0, 1250), Category(sec_MicrospeedsTenterChain), Description("The maximum allowed setpoint for the microspeed Tenter Chain speed controllers (0-125.0yards/min = 0-1250).")> Public TenterSetpointMax As Integer
  <Parameter(0, 1000), Category(sec_MicrospeedsTenterChain), Description("The minimum allowed setpoint for the microspeed Tenter Chain speed controllers (0-50.0yards/min = 0-500).")> Public TenterSetpointMin As Integer


  ' MICROSPEED PARAMETERS - TENTER LEFT
  Private Const sec_MicrospeedsTenterLeft As String = "Microspeeds - Tenter Left"
  <Parameter(0, 1000), Category(sec_MicrospeedsTenterLeft), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public TenterLeftDeviationDefault As Integer
  <Parameter(0, 1), Category(sec_MicrospeedsTenterLeft), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public TenterLeftDeviationDisable As Integer
  <Parameter(0, 200), Category(sec_MicrospeedsTenterLeft), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public TenterLeftDeviationMaximum As Integer
  <Parameter(0, 1500), Category(sec_MicrospeedsTenterLeft), Description("The maximum allowed setpoint for the microspeed Tenter Left speed controllers (0-150.0% = 0-1500).")> Public TenterLeftSetpointMax As Integer
  <Parameter(0, 1000), Category(sec_MicrospeedsTenterLeft), Description("The minimum allowed setpoint for the microspeed Tenter Left speed controllers (100.0-110.0% = 1000-1100).")> Public TenterLeftSetpointMin As Integer


  ' MICROSPEED PARAMETERS - TENTER RIGHT
  Private Const sec_MicrospeedsTenterRight As String = "Microspeeds - Tenter Right"
  <Parameter(0, 1000), Category(sec_MicrospeedsTenterRight), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public TenterRightDeviationDefault As Integer
  <Parameter(0, 1), Category(sec_MicrospeedsTenterRight), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public TenterRightDeviationDisable As Integer
  <Parameter(0, 200), Category(sec_MicrospeedsTenterRight), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public TenterRightDeviationMaximum As Integer
  <Parameter(0, 1500), Category(sec_MicrospeedsTenterRight), Description("The maximum allowed setpoint for the microspeed Tenter Right speed controllers (0-150.0% = 0-1500).")> Public TenterRightSetpointMax As Integer
  <Parameter(0, 1000), Category(sec_MicrospeedsTenterRight), Description("The minimum allowed setpoint for the microspeed Tenter Right speed controllers (100.0-110.0% = 1000-1100).")> Public TenterRightSetpointMin As Integer


  ' MICROSPEED PARAMETERS - OVERFEED TOP
  Private Const sec_MicrospeedsOverfeedTop As String = "Microspeeds - Overfeed Top"
  <Parameter(0, 1000), Category(sec_MicrospeedsOverfeedTop), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public OverfeedTopDeviationDefault As Integer
  <Parameter(0, 1), Category(sec_MicrospeedsOverfeedTop), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public OverfeedTopDeviationDisable As Integer
  <Parameter(0, 200), Category(sec_MicrospeedsOverfeedTop), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public OverfeedTopDeviationMaximum As Integer
  <Parameter(0, 1500), Category(sec_MicrospeedsOverfeedTop), Description("The maximum allowed setpoint for the microspeed Overfeed Top speed controllers (0-150.0% = 0-1500).")> Public OverfeedTopSetpointMax As Integer
  <Parameter(0, 1000), Category(sec_MicrospeedsOverfeedTop), Description("The minimum allowed setpoint for the microspeed Overfeed Top speed controllers (100.0-110.0% = 1000-1100).")> Public OverfeedTopSetpointMin As Integer


  ' MICROSPEED PARAMETERS - OVERFEED BOTTOM
  Private Const sec_MsOverfeed As String = "Microspeeds - Overfeed Bottom"
  <Parameter(0, 1000), Category(sec_MsOverfeed), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public OverfeedBottomDeviationDefault As Integer
  <Parameter(0, 1), Category(sec_MsOverfeed), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public OverfeedBottomDeviationDisable As Integer
  <Parameter(0, 200), Category(sec_MsOverfeed), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public OverfeedBottomDeviationMaximum As Integer
  <Parameter(0, 1500), Category(sec_MsOverfeed), Description("The maximum allowed setpoint for the microspeed Overfeed Bottom speed controller (0-150.0% = 0-1500).")> Public OverfeedBottomSetpointMax As Integer
  <Parameter(0, 1000), Category(sec_MsOverfeed), Description("The minimum allowed setpoint for the microspeed Overfeed Bottom speed controller (100.0-110.0% = 1000-1100).")> Public OverfeedBottomSetpointMin As Integer


  ' MICROSPEED PARAMETERS - SELVAGE LEFT
  Private Const sec_MsSelvageLeft As String = "Microspeeds - Selvage Left"
  <Parameter(0, 1000), Category(sec_MsSelvageLeft),
  Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public SelvageLeftDeviationDefault As Integer
  <Parameter(0, 1), Category(sec_MsSelvageLeft), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public SelvageLeftDeviationDisable As Integer
  <Parameter(0, 200), Category(sec_MsSelvageLeft), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public SelvageLeftDeviationMaximum As Integer
  <Parameter(0, 1500), Category(sec_MsSelvageLeft), Description("The maximum allowed setpoint for the microspeed Selvage Left speed controller (0-150.0% = 0-1500).")> Public SelvageLeftSetpointMax As Integer
  <Parameter(0, 1000), Category(sec_MsSelvageLeft), Description("The minimum allowed setpoint for the microspeed Selvage Left speed controller (100.0-110.0% = 1000-1100).")> Public SelvageLeftSetpointMin As Integer


  ' MICROSPEED PARAMETERS - SELVAGE RIGHT
  Private Const sec_MsSelvageRight As String = "Microspeeds - Selvage Right"
  <Parameter(0, 1000), Category(sec_MsSelvageRight), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public SelvageRightDeviationDefault As Integer
  <Parameter(0, 1), Category(sec_MsSelvageRight), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public SelvageRightDeviationDisable As Integer
  <Parameter(0, 200), Category(sec_MsSelvageRight), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public SelvageRightDeviationMaximum As Integer
  <Parameter(0, 1500), Category(sec_MsSelvageRight), Description("The maximum allowed setpoint for the microspeed Selvage Right speed controller (0-150.0% = 0-1500).")> Public SelvageRightSetpointMax As Integer
  <Parameter(0, 1000), Category(sec_MsSelvageRight), Description("The minimum allowed setpoint for the microspeed Selvage Right speed controller (100.0-110.0% = 1000-1100).")> Public SelvageRightSetpointMin As Integer


  ' MICROSPEED PARAMETERS - PADDER
  Private Const sec_MsPadder As String = "Microspeeds - Padder"
  <Parameter(0, 1000), Category(sec_MsPadder), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public PadderDeviationDefault As Integer
  <Parameter(0, 1), Category(sec_MsPadder), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public PadderDeviationDisable As Integer
  <Parameter(0, 200), Category(sec_MsPadder), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public PadderDeviationMaximum As Integer
  <Parameter(0, 1500), Category(sec_MsPadder), Description("The maximum allowed setpoint for the microspeed Padder speed controller (0-150.0% = 0-1500).")> Public PadderSetpointMax As Integer
  <Parameter(0, 1000), Category(sec_MsPadder), Description("The minimum allowed setpoint for the microspeed Padder speed controller (100.0-110.0% = 1000-1100).")> Public PadderSetpointMin As Integer




  'MICROSPEED PARAMETERS - PADDER PULL
  'Private Const sec_MsPadderPull As String = "Microspeeds - Padder Pull"
  '<Parameter(0, 1000), Category(sec_MsPadderPull), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public PadderPullDeviationDefault As Integer
  '<Parameter(0, 1), Category(sec_MsPadderPull), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public PadderPullDeviationDisable As Integer
  '<Parameter(0, 200), Category(sec_MsPadderPull), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public PadderPullDeviationMaximum As Integer
  '<Parameter(0, 1500), Category(sec_MsPadderPull), Description("The maximum allowed setpoint for the microspeed Padder Pull speed controller (0-150.0% = 0-1500).")> Public PadderPullSetpointMax As Integer
  '<Parameter(0, 1000), Category(sec_MsPadderPull), Description("The minimum allowed setpoint for the microspeed Padder Pull speed controller (100.0-110.0% = 1000-1100).")> Public PadderPullSetpointMin As Integer


  'MICROSPEED PARAMETERS - ENTRANCE CONVEYOR
  'Private Const sec_MsEntConv As String = "Microspeeds - Entrance Conveyor"
  '<Parameter(0, 1000), Category(sec_MsEntConv), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public EntConvDeviationDefault As Integer
  '<Parameter(0, 1), Category(sec_MsEntConv), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public EntConvDeviationDisable As Integer
  '<Parameter(0, 200), Category(sec_MsEntConv), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public EntConvDeviationMaximum As Integer
  '<Parameter(0, 1500), Category(sec_MsEntConv), Description("The maximum allowed setpoint for the microspeed Entrance Conveyor controller (0-150.0% = 0-1500).")> Public EntConvSetpointMax As Integer
  '<Parameter(0, 1000), Category(sec_MsEntConv), Description("The minimum allowed setpoint for the microspeed Entrance Conveyor speed controller (100.0-110.0% = 1000-1100).")> Public EntConvSetpointMin As Integer


  ''MICROSPEED PARAMETERS - TENTER CONVEYOR
  'Private Const sec_MsTentConv As String = "Microspeeds - Tenter Conveyor"
  '<Parameter(0, 1000), Category(sec_MsTentConv), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")>
  'Public TenterConvDeviationDefault As Integer
  '<Parameter(0, 1), Category(sec_MsTentConv), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")>
  'Public TenterConvDeviationDisable As Integer
  '<Parameter(0, 200), Category(sec_MsTentConv), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")>
  'Public TenterConvDeviationMaximum As Integer
  '<Parameter(0, 1500), Category(sec_MsTentConv), Description("The maximum allowed setpoint for the microspeed Tenter Conveyor controller (0-150.0% = 0-1500).")>
  'Public TenterConvSetpointMax As Integer
  '<Parameter(0, 1000), Category(sec_MsTentConv), Description("The minimum allowed setpoint for the microspeed Tenter Conveyor speed controller (100.0-110.0% = 1000-1100).")>
  'Public TenterConvSetpointMin As Integer

  'MICROSPEED PARAMETERS - CONVEYOR
  Private Const sec_MsTentConv As String = "Microspeeds - Conveyor"
  <Parameter(0, 1000), Category(sec_MsTentConv), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")>
  Public ConveyorDeviationDefault As Integer
  <Parameter(0, 1), Category(sec_MsTentConv), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")>
  Public ConveyorDeviationDisable As Integer
  <Parameter(0, 200), Category(sec_MsTentConv), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")>
  Public ConveyorDeviationMaximum As Integer
  <Parameter(0, 1500), Category(sec_MsTentConv), Description("The maximum allowed setpoint for the microspeed Conveyor controller (0-150.0% = 0-1500).")>
  Public ConveyorSetpointMax As Integer
  <Parameter(0, 1000), Category(sec_MsTentConv), Description("The minimum allowed setpoint for the microspeed Conveyor speed controller (100.0-110.0% = 1000-1100).")>
  Public ConveyorSetpointMin As Integer

  'MICROSPEED PARAMETERS -  STRIPPER
  Private Const sec_MicrospeedsStripper As String = "Microspeeds - Stripper"
  <Parameter(0, 1000), Category(sec_MicrospeedsStripper), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")>
  Public StripperDeviationDefault As Integer
  <Parameter(0, 1), Category(sec_MicrospeedsStripper), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")>
  Public StripperDeviationDisable As Integer
  <Parameter(0, 200), Category(sec_MicrospeedsStripper), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")>
  Public StripperDeviationMaximum As Integer
  <Parameter(0, 1500), Category(sec_MicrospeedsStripper), Description("The maximum allowed setpoint for the microspeed Stripper speed controllers (0 - 150.0% = 0-1500).")>
  Public StripperSetpointMax As Integer
  <Parameter(0, 350), Category(sec_MicrospeedsStripper), Description("The minimum allowed setpoint for the microspeed Stripper speed controllers (100.0-110.0% = 1000-1100).")>
  Public StripperSetpointMin As Integer


  ''MICROSPEED PARAMETERS -  SCRAY
  'Private Const sec_MicrospeedsSray As String = "Microspeeds - Scray"
  '<Parameter(0, 1000), Category(sec_MicrospeedsSray), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public ScrayDeviationDefault As Integer
  '<Parameter(0, 1), Category(sec_MicrospeedsSray), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public ScrayDeviationDisable As Integer
  '<Parameter(0, 200), Category(sec_MicrospeedsSray), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public ScrayDeviationMaximum As Integer
  '<Parameter(0, 1500), Category(sec_MicrospeedsSray), Description("The maximum allowed setpoint for the microspeed Scray speed controllers (0 - 150.0% = 0-1500).")> Public ScraySetpointMax As Integer
  '<Parameter(0, 350), Category(sec_MicrospeedsSray), Description("The minimum allowed setpoint for the microspeed Scray speed controllers (100.0-110.0% = 1000-1100).")> Public ScraySetpointMin As Integer



  ''MICROSPEED PARAMETERS -  BATCHER OVERFEED
  'Private Const sec_MicrospeedsBatcherOverfeed As String = "Microspeeds - Batcher Overfeed"
  '<Parameter(0, 1000), Category(sec_MicrospeedsBatcherOverfeed), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public BatcherOverfeedDeviationDefault As Integer
  '<Parameter(0, 1), Category(sec_MicrospeedsBatcherOverfeed), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")> Public BatcherOverfeedDeviationDisable As Integer
  '<Parameter(0, 200), Category(sec_MicrospeedsBatcherOverfeed), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")> Public BatcherOverfeedDeviationMaximum As Integer
  '<Parameter(0, 1500), Category(sec_MicrospeedsBatcherOverfeed), Description("The maximum allowed setpoint for the microspeed Batcher Overfeed speed controllers (0 - 150.0% = 0-1500).")> Public BatcherOverfeedSetpointMax As Integer
  '<Parameter(0, 350), Category(sec_MicrospeedsBatcherOverfeed), Description("The minimum allowed setpoint for the microspeed Batcher Overfeed speed controllers (100.0-110.0% = 1000-1100).")> Public BatcherOverfeedSetpointMin As Integer


  ''MICROSPEED PARAMETERS -  PACKING ROLL
  'Private Const sec_MicrospeedsPackingRoll As String = "Microspeeds - Packing Roll"
  '<Parameter(0, 1000), Category(sec_MicrospeedsPackingRoll), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")>
  'Public PackingRollDeviationDefault As Integer
  '<Parameter(0, 1), Category(sec_MicrospeedsPackingRoll), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")>
  'Public PackingRollDeviationDisable As Integer
  '<Parameter(0, 200), Category(sec_MicrospeedsPackingRoll), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")>
  'Public PackingRollDeviationMaximum As Integer
  '<Parameter(0, 1500), Category(sec_MicrospeedsPackingRoll), Description("The maximum allowed setpoint for the microspeed Packing Roll speed controllers (0 - 150.0% = 0-1500).")>
  'Public PackingRollSetpointMax As Integer
  '<Parameter(0, 350), Category(sec_MicrospeedsPackingRoll), Description("The minimum allowed setpoint for the microspeed Packing Roll speed controllers (100.0-110.0% = 1000-1100).")>
  'Public PackingRollSetpointMin As Integer


  ''MICROSPEED PARAMETERS - MAIN BATCHER
  'Private Const sec_MicrospeedsMainBatcher As String = "Microspeeds - Main Batcher"
  '<Parameter(0, 1000), Category(sec_MicrospeedsMainBatcher), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")>
  'Public MainBatcherDeviationDefault As Integer
  '<Parameter(0, 1), Category(sec_MicrospeedsMainBatcher), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")>
  'Public MainBatcherDeviationDisable As Integer
  '<Parameter(0, 200), Category(sec_MicrospeedsMainBatcher), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")>
  'Public MainBatcherDeviationMaximum As Integer
  '<Parameter(0, 1500), Category(sec_MicrospeedsMainBatcher), Description("The maximum allowed setpoint for the microspeed Main Batcher speed controllers (0 - 150.0% = 0-1500).")>
  'Public MainBatcherSetpointMax As Integer
  '<Parameter(0, 350), Category(sec_MicrospeedsMainBatcher), Description("The minimum allowed setpoint for the microspeed Main Batcher speed controllers (100.0-110.0% = 1000-1100).")>
  'Public MainBatcherSetpointMin As Integer


  ''MICROSPEED PARAMETERS - INCLINE CONVEYOR
  'Private Const sec_MicrospeedsInclineConveyor As String = "Microspeeds - Incline Conveyor"
  '<Parameter(0, 1000), Category(sec_MicrospeedsInclineConveyor), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")>
  'Public InclineConveyorDeviationDefault As Integer
  '<Parameter(0, 1), Category(sec_MicrospeedsInclineConveyor), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")>
  'Public InclineConveyorDeviationDisable As Integer
  '<Parameter(0, 200), Category(sec_MicrospeedsInclineConveyor), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")>
  'Public InclineConveyorDeviationMaximum As Integer
  '<Parameter(0, 1500), Category(sec_MicrospeedsInclineConveyor), Description("The maximum allowed setpoint for the microspeed Incline Conveyor speed controller (0 - 150.0% = 0-1500).")>
  'Public InclineConveyorSetpointMax As Integer
  '<Parameter(0, 350), Category(sec_MicrospeedsInclineConveyor), Description("The minimum allowed setpoint for the microspeed Incline Conveyor speed controller (100.0-110.0% = 1000-1100).")>
  'Public InclineConveyorSetpointMin As Integer


  'MICROSPEED PARAMETERS - BIANCO DOGAL
  Private Const sec_MicrospeedsBiancoDogal As String = "Microspeeds - Bianco Dogal"
  <Parameter(0, 1000), Category(sec_MicrospeedsBiancoDogal), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")>
  Public BiancoDogalDeviationDefault As Integer
  <Parameter(0, 1), Category(sec_MicrospeedsBiancoDogal), Description("Set to '1' to disable the deviation limits.  If disabled, limits used are: 'Setpoint Max and 'Setpoint Min'.")>
  Public BiancoDogalDeviationDisable As Integer
  <Parameter(0, 200), Category(sec_MicrospeedsBiancoDogal), Description("The maximum allowed deviation for a specific device (0-20.0% = 0-200).")>
  Public BiancoDogalDeviationMaximum As Integer
  <Parameter(0, 1500), Category(sec_MicrospeedsBiancoDogal), Description("The maximum allowed setpoint for the microspeed Bianco Dogal speed controller (0 - 150.0% = 0-1500).")>
  Public BiancoDogalSetpointMax As Integer
  <Parameter(0, 350), Category(sec_MicrospeedsBiancoDogal), Description("The minimum allowed setpoint for the microspeed Bianco Dogal speed controller (100.0-110.0% = 1000-1100).")>
  Public BiancoDogalSetpointMin As Integer




#End Region








  <Parameter(0, 255), Category("Communications Nodes"), Description("The Communications Node address for the specific zone microspeed controller - Fan Speed Top (0-255).")>
Public NodeFanSpeedTop(8) As Integer
<Parameter(0, 255), Category("Communications Nodes"), Description("The Communications Node address for the specific zone microspeed controller - Fan Speed Bottom (0-255).")>
Public NodeFanSpeedBottom(8) As Integer
<Parameter(0, 255), Category("Communications Nodes"), Description("The Communications Node address for the specific zone microspeed controller - Fan Speed Exhaust (0-255).")>
Public NodeFanSpeedExhaust(2) As Integer
<Parameter(0, 255), Category("Communications Nodes"), Description("The Communications Node address for the specific zone microspeed controller - Fan Speed Exhaust (0-255).")>
Public NodeFanSpeedDucon As Integer










#Region " Circulation Fans "

  <Parameter(0, 1), Category("Circulation Fans"), Description("Set to '1' to enable remote/automatic Fan Speed adjustment from Adaptive controller.")>
  Public FanSpeedAdjustEnabled As Integer
  <Parameter(0, 1000), Category("Circulation Fans"), Description("Set to '1' to disable procedure defined setpoint device")>
  Public FanSpeedDeviationDisable As Integer
  <Parameter(0, 1000), Category("Circulation Fans"), Description("The default allowed deviation when no program deviances have been set (1000-3600RPM = 1000-36000).")>
  Public FanSpeedDeviationDefault As Integer
  <Parameter(0, 1000), Category("Circulation Fans"), Description("The maximum allowed deviation for a specific device (0-500RPM = 0-5000).")>
  Public FanSpeedDeviationMaximum As Integer
  <Parameter(0, 1000), Category("Circulation Fans"), Description("Maximum circulation fan speed, tenths %")>
  Public FanSpeedSetpointMax As Integer
  <Parameter(0, 1000), Category("Circulation Fans"), Description("Minimum circulation fan ratio, tenths %")>
  Public FanSpeedSetpointMin As Integer
  <Parameter(0, 500), Category("Circulation Fans"), Description("Maximum amount the operator can increase the speed above the setpoint, tenths %")>
  Public CircFanMaxIncrease As Integer
  <Parameter(0, 500), Category("Circulation Fans"), Description("Maximum amount the operator can decrease the speed below the setpoint, tenths %")>
  Public CircFanMaxDecrease As Integer
  <Parameter(0, 36000), Category("Circulation Fans"), Description("Maximum circulation fan speed range, tenths RPM")>
  Public CircFanMaxRange As Integer

#End Region

#Region " DUCON FAN "

  <Parameter(0, 1), Category("Circulation Fans"), Description("Set to '1' to enable remote/automatic Fan Speed adjustment from Adaptive controller.")>
  Public DuconFanAdjustEnabled As Integer
  <Parameter(0, 1000), Category("Circulation Fans"), Description("The default allowed deviation when no program deviances have been set (1000-3600RPM = 1000-36000).")>
  Public DuconSpeedDeviationDefault As Integer
  <Parameter(0, 1000), Category("Circulation Fans"), Description("The maximum allowed deviation for a specific device (0-500RPM = 0-5000).")>
  Public DuconSpeedDeviationMaximum As Integer
  <Parameter(0, 1000), Category("Circulation Fans"), Description("Maximum circulation fan speed, tenths %")>
  Public DuconSpeedSetpointMax As Integer
  <Parameter(0, 1000), Category("Circulation Fans"), Description("Minimum circulation fan ratio, tenths %")>
  Public DuconSpeedSetpointMin As Integer



#End Region


#Region " Exhaust Fans "

  <Parameter(0, 1000), Category("Exhaust Fans"), Description("Maximum exhaust fan speed, tenths %")>
  Public ExhaustFanMax As Integer

  <Parameter(0, 500), Category("Exhaust Fans"), Description("Maximum amount the operator can increase the speed above the setpoint, tenths %")>
  Public ExhaustFanMaxIncrease As Integer

  <Parameter(0, 500), Category("Exhaust Fans"), Description("Maximum amount the operator can decrease the speed below the setpoint, tenths %")>
  Public ExhaustFanMaxDecrease As Integer

  <Parameter(0, 1000), Category("Exhaust Fans"), Description("Minimum exhaust fan speed, tenths %")>
  Public ExhaustFanMin As Integer

#End Region





#Region " VACUUM PUMPS "
#If 0 Then

  <Parameter(0, 1000), Category("Vacuum Pumps"), Description("Maximum vacuump pump speed, tenths %")>
  Public VacuumPumpMax As Integer

  <Parameter(0, 500), Category("Vacuum Pumps"), Description("Maximum amount the operator can increase the vacuump pump speed above the setpoint, tenths %")>
  Public VacuumPumpMaxIncrease As Integer

  <Parameter(0, 500), Category("Vacuum Pumps"), Description("Maximum amount the operator can decrease the vacuump pump speed below the setpoint, tenths %")>
  Public VacuumpPumpMaxDecrease As Integer

  <Parameter(0, 1000), Category("Vacuum Pumps"), Description("Minimum vacuump pump speed, tenths %")>
  Public VacuumpPumpMin As Integer

#End If

#End Region



#If 0 Then

  '  PLC PARAMETER VALUES - TO BE SENT DOWN TO THE PLC

  'TRANSPORT PARAMETERS - TENTER CHAIN
  <Parameter(0, 1), Category("Transport - Tenter Chain"), Description("V6000 -")> Public Tc00AutoEnable As Integer
  <Parameter(0, 2000), Category("Transport - Tenter Chain"), Description("V6001 - The Maximum speed range for the tenter chain at 100% output (126.0 yards/min = 1260).")> Public Tc01MaxRange As Integer
  <Parameter(0, 10000), Category("Transport - Tenter Chain"), Description("V6002 - ")> Public Tc02FeedbackPpr As Integer
  <Parameter(0, 10000), Category("Transport - Tenter Chain"), Description("V6003 - Value to enable high side error deviation alarm.  Not currently used.")> Public Tc03AlarmHigh As Integer
  <Parameter(0, 10000), Category("Transport - Tenter Chain"), Description("V6004 - Value to enable low side error deviation alarm.  Not currently used.")> Public Tc04AlarmLow As Integer
  <Parameter(0, 10000), Category("Transport - Tenter Chain"), Description("V6005 - Value to signal tenter ramp is complete.")> Public Tc05ErrorDb As Integer
  <Parameter(100, 500), Category("Transport - Tenter Chain"), Description("V6006 - The desired 'thread' setpoint for the Tenter Chain speed controller (10.0-50.0yards/min = 100-500).")> Public Tc06Setpoint1 As Integer
  <Parameter(100, 500), Category("Transport - Tenter Chain"), Description("V6007 - The desired setpoint for the Tenter Chain speed controller while active setpoint is 2 (10.0-50.0yards/min = 100-500).")> Public Tc07Setpoint2 As Integer
  <Parameter(100, 900), Category("Transport - Tenter Chain"), Description("V6010 - The desired setpoint for the Tenter Chain speed controller while active setpoint is 3 (10.0-90.0 yards/min = 100-900).")> Public Tc10Setpoint3 As Integer
  'NOTE: DO NOT WRITE SP4 VALUE DOWN TO PLC AS THIS IS THE WORKING SETPOINT WHILE IN RUN MODE - Needs to only be updated using one-shot logic as it can be remotely set from various interfaces
  '  <Parameter(100, 900), Category("Transport - Tenter Chain"), Description("V6011 - The desired setpoint for the Tenter Chain speed controller while active setpoint is 4 (10.0-90.0 yards/min = 100-900).")> Public Tc11Setpoint4 As Integer
  <Parameter(0, 1500), Category("Transport - Tenter Chain"), Description("V6012 - The maximum allowed setpoint for the Tenter Chain speed controllers (0-125.0 yards/min = 0-1250).")> Public Tc12SetpointMax As Integer
  <Parameter(0, 100), Category("Transport - Tenter Chain"), Description("V6013 - The minimum allowed setpoint for the Tenter Chain speed controllers (0-10.0 yards/min = 0-100).")> Public Tc13SetpointMin As Integer

  '  LOcCAL PARAMETERS
  <Parameter(0, 1), Category("Transport - Tenter Chain"), Description("Set to '1' to write existing Tenter specific parameters.  Auto Resets to '0' once written.")> Public TcAutoWriteParams As Integer
  <Parameter(0, 1000), Category("Transport - Tenter Chain"), Description("The default allowed deviation when no program deviances have been set (0-100.0yards/min = 0-1000).")> Public TcDeviationDefault As Integer


  'TRANSPORT PARAMETERS -  OVERFEED TOP
  <Parameter(0, 1), Category("Transport - Overfeed Top"), Description("V6100 -")> Public Ot00AutoEnable As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6101 -")> Public Ot01MaxRange As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6102 -")> Public Ot02FeedbackPpr As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6103 -")> Public Ot03AlarmHigh As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6104 -")> Public Ot04AlarmLow As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6105 -")> Public Ot05ErrorDb As Integer
  '<Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6106 -")> Public OtV6106 As Integer
  '<Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6107 -")> Public OtV6107 As Integer

  '<Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6110 -")> Public OtV6110 As Integer
  '<Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6111 -")> Public OtV6111 As Integer
  <Parameter(0, 1500), Category("Transport - Overfeed Top"), Description("V6112 - The maximum setpoint for the Overfeed speed controller (0 - 150.0% = 0-1500).")> Public Ot12SetpointMax As Integer
  <Parameter(0, 350), Category("Transport - Overfeed Top"), Description("V6113 - The minimum setpoint for the Overfeed speed controller (100.0-110.0% = 1000-1100).")> Public Ot13SetpointMin As Integer
  '<Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6114 -")> Public Ot14 As Integer
  '<Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6115 -")> Public Ot15 As Integer
  '<Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6116 -")> Public Ot16 As Integer
  '<Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6117 -")> Public Ot17 As Integer

  <Parameter(0, 2000), Category("Transport - Overfeed Top"), Description("V6120 - Gain Used while ramping.")> Public Ot20GainRamp As Integer
  <Parameter(0, 2000), Category("Transport - Overfeed Top"), Description("V6121 - Gain Used while at speed.")> Public Ot21GainAtSpd As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6122 - Gain Max Adjustment.")> Public Ot22GainMaxAdj As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6123 - Speed Error Allowed before gain adjustment")> Public Ot23GainDb As Integer
  ' <Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6124 -")> Public Ot24 As Integer
  ' <Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6125 -")> Public Ot25 As Integer
  ' <Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6126 -")> Public Ot26 As Integer
  ' <Parameter(0, 1000), Category("Transport - Overfeed Top"), Description("V6127 -")> Public Ot27 As Integer

  <Parameter(0, 1), Category("Transport - Overfeed Top"),
  Description("Set to '1' to write existing Tenter specific parameters.  Auto Resets to '0' once written. Parameter Write Enable must also be set to '1'.")>
  Public OtAutoWriteParams As Integer

  <Parameter(0, 1000), Category("Transport - Overfeed Top"),
  Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")>
  Public OtDeviationDefault As Integer



  'TRANSPORT PARAMETERS -  OVERFEED BOTTOM
  <Parameter(0, 1), Category("Transport - Overfeed Bottom"), Description("V6200 - Enable Automatic Control for Bottom Overfeed Motor")> Public Ob00AutoEnable As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6201 -")> Public Ob01MaxRange As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6202 -")> Public Ob02FeedbackPpr As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6203 -")> Public Ob03AlarmHigh As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6204 -")> Public Ob04AlarmLow As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6205 -")> Public Ob05ErrorDb As Integer
  ' <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6206 -")> Public ObV6206 As Integer
  ' <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6207 -")> Public ObV6207 As Integer

  ' <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6210 -")> Public ObV6210 As Integer
  ' <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6211 -")> Public ObV6211 As Integer
  <Parameter(0, 1500), Category("Transport - Overfeed Bottom"), Description("V6211 - The maximum setpoint for the Overfeed speed controller (0 - 150.0% = 0-1500).")> Public Ob12SetpointMax As Integer
  <Parameter(0, 350), Category("Transport - Overfeed Bottom"), Description("V6213 - The minimum setpoint for the Overfeed speed controller (100.0-110.0% = 1000-1100).")> Public Ob13SetpointMin As Integer
  ' <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6214 -")> Public ObV6214 As Integer
  ' <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6215 -")> Public ObV6215 As Integer
  ' <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6216 -")> Public ObV6216 As Integer
  ' <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6217 -")> Public ObV6217 As Integer

  <Parameter(0, 2000), Category("Transport - Overfeed Bottom"), Description("V6220 - Gain Used while ramping.")> Public Ob20GainRamp As Integer
  <Parameter(0, 2000), Category("Transport - Overfeed Bottom"), Description("V6221 - Gain Used while at speed.")> Public Ob21GainAtSpd As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6222 - Gain Max Adjustment.")> Public Ob22GainMaxAdj As Integer
  <Parameter(0, 1000), Category("Transport - Overfeed Bottom"), Description("V6223 - Speed Error Allowed before gain adjustment")> Public Ob23GainDb As Integer


  <Parameter(0, 1), Category("Transport - Overfeed Bottom"),
  Description("Set to '1' to write existing Tenter specific parameters.  Auto Resets to '0' once written. Parameter Write Enable must also be set to '1'.")>
  Public ObAutoWriteParams As Integer

  <Parameter(0, 1000), Category("Transport - Overfeed Bottom"),
    Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")>
  Public ObDeviationDefault As Integer


  ' TRANSPORT PARAMETERS -  SELVAGE LEFT
  <Parameter(0, 1), Category("Transport -  Selvage Left"), Description("V6300 -")> Public Sl00AutoEnable As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6301 -")> Public Sl01MaxRange As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6302 -")> Public Sl02FeedbackPpr As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6303 -")> Public Sl03AlarmHigh As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6304 -")> Public Sl04AlarmLow As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6305 -")> Public Sl05ErrorDb As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6306 -")> Public SlV6306 As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6307 -")> Public SlV6307 As Integer

  '  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6310 -")> Public SlV6310 As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6311 -")> Public SlV6311 As Integer
  <Parameter(0, 1500), Category("Transport - Selvage Left"), Description("V6312 - The maximum setpoint (0 - 150.0% = 0-1500).")> Public Sl12SetpointMax As Integer
  <Parameter(0, 350), Category("Transport - Selvage Left"), Description("V6313 - The minimum setpoint (100.0-110.0% = 1000-1100).")> Public Sl13SetpointMin As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6314 -")> Public SlV6314 As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6315 -")> Public SlV6315 As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6316 -")> Public SlV6316 As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6317 -")> Public SlV6317 As Integer

  <Parameter(0, 2000), Category("Transport - Selvage Left"), Description("V6320 - Gain Used while ramping.")> Public Sl20GainRamp As Integer
  <Parameter(0, 2000), Category("Transport - Selvage Left"), Description("V6321 - Gain Used while at speed.")> Public Sl21GainAtSpd As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6322 - Gain Max Adjustment.")> Public Sl22GainMaxAdj As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("V6323 - Speed Error Allowed before gain adjustment")> Public Sl23GainDb As Integer

  <Parameter(0, 1), Category("Transport - Selvage Left"), Description("Set to '1' to write existing Tenter specific parameters.  Auto Resets to '0' once written. Parameter Write Enable must also be set to '1'.")> Public SlAutoWriteParams As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Left"), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")> Public SlDeviationDefault As Integer


  ' TRANSPORT PARAMETERS -  SELVAGE RIGHT
  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6400 -")> Public Sr00AutoEnable As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6401 -")> Public Sr01MaxRange As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6402 -")> Public Sr02FeedbackPpr As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6403 -")> Public Sr03AlarmHigh As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6404 -")> Public Sr04AlarmLow As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6405 -")> Public Sr05ErrorDb As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6406 -")> Public      SrV6406 As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6407 -")> Public      SrV6407 As Integer

  '  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6410 -")> Public      SrV6410 As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6411 -")> Public      SrV6411 As Integer
  <Parameter(0, 1500), Category("Transport - Selvage Right"), Description("V6412 - The maximum setpoint (0 - 150.0% = 0-1500).")> Public Sr12SetpointMax As Integer
  <Parameter(0, 350), Category("Transport - Selvage Right"), Description("V6413 - The minimum setpoint (100.0-110.0% = 1000-1100).")> Public Sr13SetpointMin As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6414 -")> Public      SrV6414 As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6415 -")> Public      SrV6415 As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6416 -")> Public      SrV6416 As Integer
  '  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6417 -")> Public      SrV6417 As Integer

  <Parameter(0, 2000), Category("Transport - Selvage Right"), Description("V6420 - Gain Used while ramping.")> Public Sr20GainRamp As Integer
  <Parameter(0, 2000), Category("Transport - Selvage Right"), Description("V6421 - Gain Used while at speed.")> Public Sr21GainAtSpd As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6422 - Gain Max Adjustment.")> Public Sr22GainMaxAdj As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("V6423 - Speed Error Allowed before gain adjustment")> Public Sr23GainDb As Integer

  <Parameter(0, 1), Category("Transport - Selvage Right"), Description("Set to '1' to write existing specific parameters.  Auto Resets to '0' once written. Parameter Write Enable must also be set to '1'.")>
  Public SrAutoWriteParams As Integer
  <Parameter(0, 1000), Category("Transport - Selvage Right"), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")>
  Public SrDeviationDefault As Integer



  ' TRANSPORT PARAMETERS - PADDER
  <Parameter(0, 1000), Category("Transport - Padder"), Description("V6500 -")> Public Pd00AutoEnable As Integer
  <Parameter(0, 1000), Category("Transport - Padder"), Description("V6501 -")> Public Pd01MaxRange As Integer
  <Parameter(0, 1000), Category("Transport - Padder"), Description("V6502 -")> Public Pd02FeedbackPpr As Integer
  <Parameter(0, 1000), Category("Transport - Padder"), Description("V6503 -")> Public Pd03AlarmHigh As Integer
  <Parameter(0, 1000), Category("Transport - Padder"), Description("V6504 -")> Public Pd04AlarmLow As Integer
  <Parameter(0, 1000), Category("Transport - Padder"), Description("V6505 -")> Public Pd05ErrorDb As Integer
  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6506 -")> Public      PdV6506 As Integer
  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6507 -")> Public      PdV6507 As Integer

  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6510 -")> Public      PdV6510 As Integer
  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6511 -")> Public      PdV6511 As Integer
  <Parameter(0, 1500), Category("Transport - Padder"), Description("The maximum allowed setpoint (0 - 150.0% = 0-150.0).")> Public Pd12SetpointMax As Integer
  <Parameter(0, 350), Category("Transport - Padder"), Description("The minimum allowed setpoint (100.0-110.0% = 1000-1100).")> Public Pd13SetpointMin As Integer
  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6514 -")> Public      PdV6514 As Integer
  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6515 -")> Public      PdV6515 As Integer
  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6516 -")> Public      PdV6516 As Integer
  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6517 -")> Public      PdV6517 As Integer

  <Parameter(0, 2000), Category("Transport - Padder"), Description("V6520 - Gain Used while ramping.")> Public Pd20GainRamp As Integer
  <Parameter(0, 2000), Category("Transport - Padder"), Description("V6521 - Gain Used while at speed.")> Public Pd21GainAtSpd As Integer
  <Parameter(0, 300), Category("Transport - Padder"), Description("V6522 - Gain Max Adjustment.")> Public Pd22GainMaxAdj As Integer
  <Parameter(0, 250), Category("Transport - Padder"), Description("V6523 - Speed Error Allowed before gain adjustment")> Public Pd23GainDb As Integer
  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6524 -")> Public      Pd24 As Integer
  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6525 -")> Public      Pd25 As Integer
  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6526 -")> Public      Pd26 As Integer
  ' <Parameter(0, 1000), Category("Transport - Padder"), Description("V6527 -")> Public      Pd27 As Integer

  <Parameter(0, 1), Category("Transport - Padder Dancer"), Description("V6530 - Auxillary Trim Dancer Enabled")> Public Pd30DncEnable As Integer
  <Parameter(0, 1000), Category("Transport - Padder Dancer"), Description("V6531 - Dancer Position Desired (300 = 30.0%)")> Public Pd31DncPositionSp As Integer
  <Parameter(0, 1000), Category("Transport - Padder Dancer"), Description("V6532 - ")> Public Pd32DncGainPct As Integer
  <Parameter(0, 1000), Category("Transport - Padder Dancer"), Description("V6533 - Trim Speed Offset Inc/Dec based on Error.  Mode 0: (+)dec, (-)inc. Mode 1: (+)inc,(-)dec.")> Public Pd33DncTrimMode As Integer
  <Parameter(0, 1000), Category("Transport - Padder Dancer"), Description("V6534 - Dancer Position Delay Trim Mode, tenths of seconds.")> Public Pd34DncDelayOnTime As Integer
  <Parameter(0, 1000), Category("Transport - Padder Dancer"), Description("V6535 - Dancer Position Trim Interval, tenths of second.")> Public Pd35DncAdjTime As Integer
  <Parameter(0, 1000), Category("Transport - Padder Dancer"), Description("V6536 - ")> Public Pd36DncGainMaxAdj As Integer
  '  <Parameter(0, 1000), Category("Transport - Padder Dancer"), Description("V6537 - Dancer Position Desired")> Public      Pd37Dnc As Integer

  <Parameter(0, 1), Category("Transport - Padder"), Description("Set to '1' to write existing specific parameters.  Auto Resets to '0' once written. Parameter Write Enable must also be set to '1'.")>
  Public PdAutoWriteParams As Integer
  <Parameter(0, 1000), Category("Transport - Padder"), Description("The default allowed deviation when no program deviances have been set (1-100 = 10-1000)).")>
  Public PdDeviationDefault As Integer
  

  ' TRANSPORT PARAMETERS - PADDER DANCER
  <Parameter(0, 1), Category("Transport - Padder Dancer Pressure"), Description("Set to '1' to write existing specific parameters.  Auto Resets to '0' once written. Parameter Write Enable must also be set to '1'.")>
  Public PdDncAutoWriteParams As Integer

  <Parameter(0, 1), Category("Transport - Padder Dancer Pressure"), Description("V6700 - Dancer Pressure Auto Enabled. Set to '1' to enable Pressure Control.")>
  Public PdDncPressAuto As Integer

  <Parameter(0, 9999), Category("Transport - Padder Dancer Pressure"), Description("V6701 - Maximum Operating Pressure Range associated with analog output.  Pressure value, in tenths, while I/P at 100% output. (1400 = 140.0psi)")>
  Public PdDncPressRange As Integer

  <Parameter(0, 9999), Category("Transport - Padder Dancer Pressure"), Description("V6702 - Minimum pressure input from analog input pressure transmitter. (15 = 1.5psi)")>
  Public PdDncPressUnitsMin As Integer

  <Parameter(0, 9999), Category("Transport - Padder Dancer Pressure"), Description("V6703 - Maximum pressure input from analog input pressure transmitter. (1255 = 125.5psi)")>
  Public PdDncPressUnitsMax As Integer

  <Parameter(0, 9999), Category("Transport - Padder Dancer Pressure"), Description("V6704 - Error deadband in tenths, above which to activate pressure error alarm. ")>
  Public PdDncPressAlarmErr As Integer

  <Parameter(0, 9999), Category("Transport - Padder Dancer Pressure"), Description("V6705 - Minimum Dancer Pressure Setpoint, in tenths, allowed.")>
  Public PdDncPressSetpointMin As Integer

  <Parameter(0, 9999), Category("Transport - Padder Dancer Pressure"), Description("V6706 - Maximum Dancer Pressure Setpoint, in tenths, allowed.")>
  Public PdDncPressSetpointMax As Integer

  <Parameter(0, 9999), Category("Transport - Padder Dancer Pressure"), Description("V6707 - Idle/Doffin Dancer Pressure Setpoint, in tenths, allowed.")>
  Public PdDncPressSetpointIdle As Integer

  <Parameter(0, 9999), Category("Transport - Padder Dancer Pressure"), Description("V6710 - Maximum dancer pressure feedback from pressure transitter, in tenths of psi, allowed.")>
  Public PdDncPressInputRange As Integer ' [2014-07-02 PLC V1.29]

  <Parameter(0, 9999), Category("Transport - Padder Dancer Pressure"), Description("Default Dancer Pressure Setpoint Deviation, in tenths, allowed.")>
  Public PdDncPressDeviationDefault As Integer


  ' TRANSPORT PARAMETERS - STRIPPER ROLLER
  <Parameter(0, 1), Category("Transport - Stripper"), Description("Set to '1' to write existing specific parameters.  Auto Resets to '0' once written. Parameter Write Enable must also be set to '1'.")>
  Public StAutoWriteParams As Integer


#End If

#If 0 Then



  ' PLEVA INPUT CALIBRATION
  <Parameter(0, 1000), Category("Calibration - Pleva Inputs"),
  Description("Maximum Raw Analog Value for the Pleva Humidity input, in tenths.  Used to calibrate Pleva Humidity Inputs (1 & 2).")>
  Public PlevaHumidityInputMax As Integer
  <Parameter(0, 1000), Category("Calibration - Pleva Inputs"),
  Description("Minimum Raw Analog Value for the Pleva Humidity input, in tenths.  Used to calibrate Pleva Humidity Inputs (1 & 2).")>
  Public PlevaHumidityInputMin As Integer
  <Parameter(0, 1000), Category("Calibration - Pleva Inputs"),
  Description("Range associated with 100% Pleva Humidity analog input, in tenths.  Used to determine Pleva Humidity from Inputs (1 & 2).")>
  Public PlevaHumidityInputRange As Integer

  <Parameter(0, 1000), Category("Calibration - Pleva Inputs"),
  Description("Maximum Raw Analog Value for the pleva temp input, in tenths.  Used to calibrate Temp Input.")>
  Public PlevaTemp1TransmitterMax As Integer
  <Parameter(0, 1000), Category("Calibration - Pleva Inputs"),
  Description("Minimum Raw Analog Value for the pleva temp input, in tenths.  Used to calibrate Temp Input.")>
  Public PlevaTemp1TransmitterMin As Integer
  <Parameter(0, 5000), Category("Calibration - Pleva Inputs"),
  Description("Pleva Temp equivalent to 20ma or 100% input in tenths C. (5000 = 500.0C)")>
  Public PlevaTemp1RangeC As Integer

  <Parameter(0, 1000), Category("Calibration - Pleva Inputs"),
  Description("Maximum Raw Analog Value for the pleva temp input, in tenths.  Used to calibrate Temp Input.")>
  Public PlevaTemp2TransmitterMax As Integer
  <Parameter(0, 1000), Category("Calibration - Pleva Inputs"),
  Description("Minimum Raw Analog Value for the pleva temp input, in tenths.  Used to calibrate Temp Input.")>
  Public PlevaTemp2TransmitterMin As Integer
  <Parameter(0, 5000), Category("Calibration - Pleva Inputs"),
  Description("Pleva Temp equivalent to 20ma or 100% input in tenths C. (5000 = 500.0C)")>
  Public PlevaTemp2RangeC As Integer

  <Parameter(0, 1000), Category("Calibration - Pleva Inputs"),
  Description("Maximum Raw Analog Value for the pleva temp input, in tenths.  Used to calibrate Temp Input.")>
  Public PlevaTemp3TransmitterMax As Integer
  <Parameter(0, 1000), Category("Calibration - Pleva Inputs"),
  Description("Minimum Raw Analog Value for the pleva temp input, in tenths.  Used to calibrate Temp Input.")>
  Public PlevaTemp3TransmitterMin As Integer
  <Parameter(0, 5000), Category("Calibration - Pleva Inputs"),
  Description("Pleva Temp equivalent to 20ma or 100% input in tenths C. (5000 = 500.0C)")>
  Public PlevaTemp3RangeC As Integer

  <Parameter(0, 1000), Category("Calibration - Pleva Inputs"),
  Description("Maximum Raw Analog Value for the pleva temp input, in tenths.  Used to calibrate Temp Input.")>
  Public PlevaTemp4TransmitterMax As Integer
  <Parameter(0, 1000), Category("Calibration - Pleva Inputs"),
  Description("Minimum Raw Analog Value for the pleva temp input, in tenths.  Used to calibrate Temp Input.")>
  Public PlevaTemp4TransmitterMin As Integer
  <Parameter(0, 5000), Category("Calibration - Pleva Inputs"),
  Description("Pleva Temp equivalent to 20ma or 100% input in tenths C. (5000 = 500.0C)")>
  Public PlevaTemp4RangeC As Integer

  <Parameter(0, 5000), Category("Calibration - Pleva Inputs"),
  Description("Pleva Zone Length")>
  Public PlevaZoneLength As Integer

  ' TODO
  Public PlevaTemperatureEnable As Integer
  Public PlevaDwellTimeDeadband As Integer
  Public PlevaHumidityDeadband As Integer
  Public PlevaHumidityEnable As Integer
   
#End If

#If 0 Then

  ' GAS USAGE UTILITIES - FIRST OUT INDICATOR
  <Parameter(0, 1), Category("Gas Usage"),
    Description("Set to '1' to enable monitoring & recording Gas Usage Values from First Out Indicator PLC.")>
  Public GasUsageEnable As Integer
  <Parameter(0, 1500), Category("Gas Usage"),
    Description("Set to approximate Heat Content of Natural Gas Onsite.  Default value (used in PLC) is '1024'.")>
  Public GasUsageHeatContent As Integer


#End If



  ' DEMO SECTION
  <Parameter(0, 10000), Category("Demo Mode"), Description("Place controller in demo mode, 1=True, 2=Simulate inputs")>
  Public Demo As Integer
  <Parameter(0, 10000), Category("Demo Mode"), Description("Delay Time to simulate inputs, 1s to 5s.")>
  Public DemoTime As Integer


  Public Sub New(controlCode As ControlCode)
    Me.controlCode = controlCode
  End Sub

  Public Sub Run()
    If (InitializeParameters = 9387) Or
       (InitializeParameters = 8741) Or
       (InitializeParameters = 8742) Or
       (InitializeParameters = 8743) Or
       (InitializeParameters = 1001) Or
       (InitializeParameters = 1002) Or
       (InitializeParameters = 8744) Then
      SetDefaults()
    End If
  End Sub

  Private Sub SetDefaults()

    If (InitializeParameters = 1001) Then
      'Initialize Values for Kenyon 1

      'Pleva - Calibration
      'PlevaHumidityInputMax = 1000
      'PlevaHumidityInputMin = 0
      'PlevaHumidityInputRange = 3076


      'Communication
      NodeZoneTemp(1) = 1
      NodeZoneTemp(2) = 2
      NodeZoneTemp(3) = 3
      NodeZoneTemp(4) = 4
      NodeZoneTemp(5) = 5
      NodeZoneTemp(6) = 6

      'Honeywell
      HoneywellDeviationDefault = 100
      HoneywellSetpointAdjustEnable = 0
      HoneywellSetpointMax = 4500
      HoneywellSetpointMin = 600

      'Microspeeds - Fan Speeds
      FanSpeedDeviationDefault = 2500
      FanSpeedDeviationMaximum = 10000
      FanSpeedSetpointMax = 18000
      FanSpeedSetpointMin = 0




#If 0 Then

      'Microspeeds - Overfeed
      OtDeviationDefault = 100
      Ot12SetpointMax = 1200
      Ot13SetpointMin = 800

      'Microspeeds - Padder
      PdDeviationDefault = 100
      Pd12SetpointMax = 1200
      Pd13SetpointMin = 800

      'Microspeeds - Selvage
      SlDeviationDefault = 100
      Sl12SetpointMax = 1250
      Sl13SetpointMin = 800

      'Microspeeds - Tenter Chain
      TcDeviationDefault = 100
      Tc12SetpointMax = 900
      Tc13SetpointMin = 150
      Tc06Setpoint1 = 300

#End If
      'Microspeeds - Width
      WidthScrewDeviationDefault = 100
      WidthScrewSetpointMax = 800
      WidthScrewSetpointMin = 150

      'Setup
      PLCComsLossDisregard = 0
      PLCComsTime = 100
      SmoothRate = 0
      WatchdogTimeout = 0



    ElseIf (InitializeParameters = 1002) Then
      'Initialize Values for Kenyon 2

      'Pleva - Calibration
      'PlevaHumidityInputMax = 1000
      'PlevaHumidityInputMin = 0
      'PlevaHumidityInputRange = 3076

      'Communication
      NodeZoneTemp(1) = 1
      NodeZoneTemp(2) = 2
      NodeZoneTemp(3) = 3
      NodeZoneTemp(4) = 4
      NodeZoneTemp(5) = 5
      NodeZoneTemp(6) = 6

      'Honeywell
      HoneywellDeviationDefault = 100
      HoneywellSetpointAdjustEnable = 0
      HoneywellSetpointMax = 4500
      HoneywellSetpointMin = 600

      'Microspeeds - Fan Speeds
      FanSpeedDeviationDefault = 2500
      FanSpeedDeviationMaximum = 10000
      FanSpeedSetpointMax = 18000
      FanSpeedSetpointMin = 0




#If 0 Then
      
      'Microspeeds - Overfeed
      OtDeviationDefault = 100
      Ot12SetpointMax = 1200
      Ot13SetpointMin = 800

      'Microspeeds - Padder
      PdDeviationDefault = 100
      Pd12SetpointMax = 1200
      Pd13SetpointMin = 800

      'Microspeeds - Selvage
      SlDeviationDefault = 100
      Sl12SetpointMax = 1250
      Sl13SetpointMin = 800

      'Microspeeds - Tenter Chain
      TcDeviationDefault = 100
      Tc12SetpointMax = 900
      Tc13SetpointMin = 150
      Tc06Setpoint1 = 300
      
#End If

      'Microspeeds - Width
      WidthScrewDeviationDefault = 100
      WidthScrewSetpointMax = 800
      WidthScrewSetpointMin = 150

      'Setup
      PLCComsLossDisregard = 0
      PLCComsTime = 100
      SmoothRate = 0
      WatchdogTimeout = 0


    End If

    InitializeParameters = 0

  End Sub

End Class
