Public Class IO : Inherits MarshalByRefObject
  Friend DateTimeNow As DateTime

  Public ComsHoneyWell As Ports.Modbus
  Public ComsHoneyWellTimer As New Timer
  Public ComsHoneyWellPort As String
  Public ComsHoneyWellEnabled As Boolean = False

  Public ComsMicroSpeed As Ports.MicroSpeed196
  Public ComsMicroSpeedTimer As New Timer
  Public ComsMicroSpeedPort As String                           'Defined in Settings.xml
  Public ComsMicroSpeedEnabled As Boolean = False               'Defined in Settings.xml


  Public Plc1 As Ports.Modbus
  Public Plc1Description As String = "Transport PLC"
  Public Plc1Timer As New Timer
  Public Plc1Writes As Integer
  Public Plc1WriteFaults As Integer


  Public ComsPlcFans As Ports.Modbus
  Public ComsPlcFansTimer As New Timer
  Public ComsPlcFansIp As String
  Public ComsPlcFansEnabled As Boolean = False


  Public ComsPlcFansAninpRaw(24) As Short
  Public ComsPlcFansAnoutRaw(24) As Short


#Region " I/O DEFINITIONS "


  ' CARD #1 - DIGITAL INPUTS 1-16
  <IO(IOType.Dinp, 1), Description("24vdc Power On")> Public PowerOn As Boolean
  <IO(IOType.Dinp, 2), Description("Automatic Switch On")> Public InAuto As Boolean
  <IO(IOType.Dinp, 3), Description("")> Public InCombined As Boolean
  <IO(IOType.Dinp, 4), Description("")> Public SystemStart As Boolean
  <IO(IOType.Dinp, 5), Description("")> Public RunSpeed As Boolean
  <IO(IOType.Dinp, 6), Description("")> Public DoffinActive As Boolean
  <IO(IOType.Dinp, 7), Description("")> Public Dinp7 As Boolean
  <IO(IOType.Dinp, 8), Description("")> Public Dinp8 As Boolean
  <IO(IOType.Dinp, 9), Description("")> Public Dinp9 As Boolean ' TclToggleUp As Boolean
  <IO(IOType.Dinp, 10), Description("")> Public Dinp10 As Boolean ' TclToggleDwn As Boolean
  <IO(IOType.Dinp, 11), Description("")> Public Dinp11 As Boolean ' TcrToggleUp As Boolean
  <IO(IOType.Dinp, 12), Description("")> Public Dinp12 As Boolean ' TcrToggleDwn As Boolean
  <IO(IOType.Dinp, 13), Description("")> Public Dinp13 As Boolean ' OtToggleup As Boolean
  <IO(IOType.Dinp, 14), Description("")> Public Dinp14 As Boolean ' OtToggleDwn As Boolean
  <IO(IOType.Dinp, 15), Description("")> Public Dinp15 As Boolean ' SlToggleUp As Boolean
  <IO(IOType.Dinp, 16), Description("")> Public Dinp16 As Boolean ' SlToggleDwn As Boolean

  ' CARD #2 - DIGITAL INPUTS 17-32
  '<IO(IOType.Dinp, 17), Description("")> Public SrToggleUp As Boolean
  '<IO(IOType.Dinp, 18), Description("")> Public SrToggleDwn As Boolean
  '<IO(IOType.Dinp, 19), Description("")> Public PdToggleUp As Boolean
  '<IO(IOType.Dinp, 20), Description("")> Public PdToggleDwn As Boolean
  '<IO(IOType.Dinp, 21), Description("")> Public Dinp21 As Boolean
  '<IO(IOType.Dinp, 22), Description("")> Public Dinp22 As Boolean
  '<IO(IOType.Dinp, 23), Description("")> Public Dinp23 As Boolean
  '<IO(IOType.Dinp, 24), Description("")> Public Dinp24 As Boolean
  '<IO(IOType.Dinp, 25), Description("")> Public Dinp25 As Boolean
  '<IO(IOType.Dinp, 26), Description("")> Public Dinp26 As Boolean
  '<IO(IOType.Dinp, 27), Description("")> Public Dinp27 As Boolean
  '<IO(IOType.Dinp, 28), Description("")> Public Dinp28 As Boolean
  '<IO(IOType.Dinp, 29), Description("")> Public Dinp29 As Boolean
  '<IO(IOType.Dinp, 30), Description("")> Public Dinp30 As Boolean
  '<IO(IOType.Dinp, 31), Description("")> Public Dinp31 As Boolean
  '<IO(IOType.Dinp, 32), Description("")> Public Dinp32 As Boolean




  ' ANALOG INPUTS
  'Raw analog inputs (before scaling or smoothing) - for calibration work
  Public Aninp1Raw As Short         '
  Public Aninp2Raw As Short         '
  Public Aninp3Raw As Short         '
  Public Aninp4Raw As Short         '
  Public Aninp5Raw As Short         '
  Public Aninp6Raw As Short         '
  Public Aninp7Raw As Short         '
  Public Aninp8Raw As Short         '


  ' Analog Inputs from F2-08AD-1
  <IO(IOType.Aninp, 1, Override.Prevent, "", "%t%"), Description("")> Public Dancer1Position As Short
  <IO(IOType.Aninp, 2, Override.Prevent, "", "%t%"), Description("")> Public Dancer2Position As Short
  <IO(IOType.Aninp, 3, Override.Prevent, "", "%t%"), Description("")> Public Aninp3 As Short
  <IO(IOType.Aninp, 4, Override.Prevent, "", "%t%"), Description("")> Public Aninp4 As Short
  <IO(IOType.Aninp, 5, Override.Prevent, "", "%t%"), Description("")> Public Aninp5 As Short
  <IO(IOType.Aninp, 6, Override.Prevent, "", "%t%"), Description("")> Public Aninp6 As Short
  <IO(IOType.Aninp, 7, Override.Prevent, "", "%t%"), Description("")> Public Aninp7 As Short
  <IO(IOType.Aninp, 8, Override.Prevent, "", "%t%"), Description("")> Public Aninp8 As Short


  ' CALCULATED TRANSPORT PLC FEEDBACK VALUES:
  <IO(IOType.Aninp, 11, Override.Prevent, "", "%tYPM"), Description("")> Public TcSpeedDesired As Short

  <IO(IOType.Aninp, 12, Override.Prevent, "", "%tYPM"), Description("")> Public TclSpeedActual As Short
  <IO(IOType.Aninp, 13, Override.Prevent, "", "%tYPM"), Description("")> Public TclSpeedDesired As Short

  <IO(IOType.Aninp, 14, Override.Prevent, "", "%tYPM"), Description("")> Public TcrSpeedActual As Short
  <IO(IOType.Aninp, 15, Override.Prevent, "", "%tYPM"), Description("")> Public TcrSpeedDesired As Short

  <IO(IOType.Aninp, 16, Override.Prevent, "", "%tYPM"), Description("")> Public OtSpeedActual As Short
  <IO(IOType.Aninp, 17, Override.Prevent, "", "%tYPM"), Description("")> Public OtSpeedDesired As Short

  <IO(IOType.Aninp, 18, Override.Prevent, "", "%tYPM"), Description("")> Public SlSpeedActual As Short
  <IO(IOType.Aninp, 19, Override.Prevent, "", "%tYPM"), Description("")> Public SlSpeedDesired As Short

  <IO(IOType.Aninp, 20, Override.Prevent, "", "%tYPM"), Description("")> Public SrSpeedActual As Short
  <IO(IOType.Aninp, 21, Override.Prevent, "", "%tYPM"), Description("")> Public SrSpeedDesired As Short

  <IO(IOType.Aninp, 22, Override.Prevent, "", "%tYPM"), Description("")> Public Pd1SpeedActual As Short
  <IO(IOType.Aninp, 23, Override.Prevent, "", "%tYPM"), Description("")> Public Pd1SpeedDesired As Short

  <IO(IOType.Aninp, 24, Override.Prevent, "", "%tYPM"), Description("")> Public Pd2SpeedActual As Short
  <IO(IOType.Aninp, 25, Override.Prevent, "", "%tYPM"), Description("")> Public Pd2SpeedDesired As Short

  <IO(IOType.Aninp, 26, Override.Prevent, "", "%tYPM"), Description("")> Public ObSpeedActual As Short
  <IO(IOType.Aninp, 27, Override.Prevent, "", "%tYPM"), Description("")> Public ObSpeedDesired As Short



#If 0 Then
  ' Moved to Analog Outputs for better visibility
  <IO(IOType.Aninp, 31, Override.Prevent, "", "%t%"), Description("Display Analog Output Percent")> Public TclRefOut As Short
  <IO(IOType.Aninp, 32, Override.Prevent, "", "%t%"), Description("Display Analog Output Percent")> Public TcrRefOut As Short
  <IO(IOType.Aninp, 33, Override.Prevent, "", "%t%"), Description("Display Analog Output Percent")> Public OtRefOut As Short
  <IO(IOType.Aninp, 34, Override.Prevent, "", "%t%"), Description("Display Analog Output Percent")> Public SlRefOut As Short
  <IO(IOType.Aninp, 35, Override.Prevent, "", "%t%"), Description("Display Analog Output Percent")> Public SrRefOut As Short
  <IO(IOType.Aninp, 36, Override.Prevent, "", "%t%"), Description("Display Analog Output Percent")> Public Pd1RefOut As Short
  <IO(IOType.Aninp, 37, Override.Prevent, "", "%t%"), Description("Display Analog Output Percent")> Public Pd2RefOut As Short
  <IO(IOType.Aninp, 38, Override.Prevent, "", "%t%"), Description("Display Analog Output Percent")> Public ObRefOut As Short
#End If


#If 0 Then
   ' MICROSPEED VALUES:
  '  <IO(IOType.Aninp, 5, Override.Allow), Description("Padder Pull Roll Setpoint")> Public PadderPullActualSetpoint As Short
  <IO(IOType.Aninp, 8, Override.Allow), Description("Left Selvage Setpoint")> Public SelvageLeftSpeed As Short
  <IO(IOType.Aninp, 9, Override.Allow), Description("Right Selvage Setpoint")> Public SelvageRightSpeed As Short
  <IO(IOType.Aninp, 10, Override.Allow), Description("Conveyor Setpoint")> Public ConveyorSpeed As Short

  '  <IO(IOType.Aninp, 9, Override.Allow), Description("Tentor Conveyor Setpoint")> Public TentConveyorSetpoint As Short
  <IO(IOType.Aninp, 11, Override.Allow), Description("Stripper Setpoint")> Public StripperSpeed As Short
  '  <IO(IOType.Aninp, 5, Override.Allow), Description("Folder Setpoint")> Public FolderSetpoint As Short
#End If

#If 0 Then
  ' Adele Kenyon K2 Does not have a Pleva PLC - it has another vendor's fabric monitoring panel with Siemens PLC
  ' 2022-03-14 - Zack to speak with suppliers about connecting this 2nd panel

  ' AI(60-): Pleva PLC Feedback
  Public PlevaTempInputRaw1 As Short
  Public PlevaTempInputRaw2 As Short
  Public PlevaTempInputRaw3 As Short
  Public PlevaTempInputRaw4 As Short
  <IO(IOType.Aninp, 60, Override.Prevent, "", "%t%"), Description("Pleva Temp (%) input for Zone 2-3")> Public PlevaTempInput1 As Short
  <IO(IOType.Aninp, 61, Override.Prevent, "", "%t%"), Description("Pleva Temp (%) input for Zone 3-4")> Public PlevaTempInput2 As Short
  <IO(IOType.Aninp, 62, Override.Prevent, "", "%t%"), Description("Pleva Temp (%) input for Zone 4-5")> Public PlevaTempInput3 As Short
  <IO(IOType.Aninp, 63, Override.Prevent, "", "%t%"), Description("Pleva Temp (%) input for Zone 5-6")> Public PlevaTempInput4 As Short

  Public PlevaHumidityRaw As Short
  Public PlevaSpareInputRaw6 As Short
  Public PlevaSpareInputRaw7 As Short
  Public PlevaSpareInputRaw8 As Short
  <IO(IOType.Aninp, 64, Override.Prevent, "", "%t%"), Description("Pleva - Exhaust Humidity")> Public PlevaHumidityPercent As Short
  <IO(IOType.Aninp, 65, Override.Prevent, "", "%t%"), Description("Pleva - Spare Input 6")> Public PlevaSpareInput6 As Short
  <IO(IOType.Aninp, 66, Override.Prevent, "", "%t%"), Description("Pleva - Spare Input 7")> Public PlevaSpareInput7 As Short
  <IO(IOType.Aninp, 67, Override.Prevent, "", "%t%"), Description("Pleva - Spare Input 8")> Public PlevaSpareInput8 As Short

  <IO(IOType.Aninp, 70, Override.Prevent, "", "%tg/kg"), Description("Pleva - Exhaust Humidity g/kg")> Public PlevaHumidity As Short

#End If


  ' WIDTH SCREW SYSTEM FEEDBACK
  <IO(IOType.Aninp, 201, Override.Prevent, "", "%tin"), Description("Width Screw Actual Position")> Public WidthActual(5) As Short
  <IO(IOType.Aninp, 206, Override.Prevent, "", "%tin"), Description("Width Screw Actual/Working Setpoint")> Public WidthSetpoint(5) As Short
  '  <IO(IOType.Aninp, 31, Override.Prevent, "", "%tin"), Description("Width Screw Local/Manual Setpoint")> Public WidthSetpointManual(5) As Short
  '  <IO(IOType.Aninp, 36, Override.Prevent, "", "%tin"), Description("Width Screw Remote/Adaptive Setpoint")> Public WidthSetpointAdaptive(5) As Short

  ' HONEYWELL UDC PROCESS CONTROLLER OUTPUTS
  <IO(IOType.Aninp, 301, Override.Prevent, "", "%t%"), Description("Width Screw Actual/Working Setpoint")> Public HoneywellOutput(8) As Short


  ' A/D Terminator T1K-EBC100 PLC in Fan Control Cabinet
  <IO(IOType.Aninp, 101, Override.Prevent, "", "%t%"), Description("Fan Speed Top Actual")> Public FanTopActual(8) As Short
  <IO(IOType.Aninp, 109, Override.Prevent, "", "%t%"), Description("Fan Speed Bottom Actual")> Public FanBottomActual(8) As Short
  <IO(IOType.Aninp, 117, Override.Prevent, "", "%t%"), Description("Fan Speed Exhaust Actual")> Public FanExhaustActual(2) As Short
  <IO(IOType.Aninp, 119, Override.Prevent, "", "%t%"), Description("Fan Speed Pollution Actual")> Public FanPollution As Short
  <IO(IOType.Aninp, 120, Override.Prevent, "", "%t%"), Description("Terminator Spare channels")> Public FanSpareAninps(5) As Short

  <IO(IOType.Aninp, 131, Override.Prevent, "", "%t%"), Description("Fan Speed Top Setpoint")> Public FanTopSetpoint(8) As Short
  <IO(IOType.Aninp, 139, Override.Prevent, "", "%t%"), Description("Fan Speed Bottom Setpoint")> Public FanBottomSP(8) As Short
  <IO(IOType.Aninp, 147, Override.Prevent, "", "%t%"), Description("Fan Speed Exhaust Setpoint")> Public FanExhaustSetpoint(2) As Short
  <IO(IOType.Aninp, 149, Override.Prevent, "", "%t%"), Description("Fan Speed Pollution Setpoint")> Public FanPollutionSetpoint As Short
  <IO(IOType.Aninp, 150, Override.Prevent, "", "%t%"), Description("Terminator Spare analog output channels")> Public FanSpareAninpsSetpoint(5) As Short




  ' TEMPERATURES - 
  ' Temp(1-18): Honeywell TempController Feedback
  <IO(IOType.Temp, 1, Override.Prevent, "", "%tF"), Description("Actual Temperature Value from Honeywell")> Public AirTempActual(8) As Short
  <IO(IOType.Temp, 9, Override.Prevent, "", "%tF"), Description("Actual Working Setpoint from Honeywell")> Public RemoteValue(8) As Short

#If 0 Then
  <IO(IOType.Temp, 25, Override.Prevent, "", "%tC"), Description("Pleva Temp (C) input for Zone 2-3")> Public PlevaTemp1 As Short
  <IO(IOType.Temp, 26, Override.Prevent, "", "%tC"), Description("Pleva Temp (C) input for Zone 3-4")> Public PlevaTemp2 As Short
  <IO(IOType.Temp, 27, Override.Prevent, "", "%tC"), Description("Pleva Temp (C) input for Zone 4-5")> Public PlevaTemp3 As Short
  <IO(IOType.Temp, 28, Override.Prevent, "", "%tC"), Description("Pleva Temp (C) input for Zone 5-6")> Public PlevaTemp4 As Short
#End If




  ' HIGH-SPEED COUNTER INPUTS
  <IO(IOType.Counter, 1), Description("Raw Count")> Public TenterLeftRawCount As Integer
  <IO(IOType.Counter, 2), Description("Raw Count")> Public TenterRightRawCount As Integer
  <IO(IOType.Counter, 3), Description("Raw Count")> Public OvrFeedTopRawCount As Integer
  <IO(IOType.Counter, 4), Description("Raw Count")> Public LftSelvageRawCount As Integer

  <IO(IOType.Counter, 5), Description("Raw Count")> Public RtSelvageRawCount As Integer
  <IO(IOType.Counter, 6), Description("Raw Count")> Public Padder1RawCount As Integer
  <IO(IOType.Counter, 7), Description("Raw Count")> Public Padder2RawCount As Integer
  <IO(IOType.Counter, 8), Description("Raw Count")> Public OvrFeedBotRawCount As Integer


  ' ANALOG OUTPUTS READ UP FROM PLC TRANSPORT CONTROL
  <IO(IOType.Anout, 1, Override.Prevent, "", "%t%"), Description("PLC Analog Output Percent")> Public TclRefOut As Short
  <IO(IOType.Anout, 2, Override.Prevent, "", "%t%"), Description("PLC Analog Output Percent")> Public TcrRefOut As Short
  <IO(IOType.Anout, 3, Override.Prevent, "", "%t%"), Description("PLC Analog Output Percent")> Public OtRefOut As Short
  <IO(IOType.Anout, 4, Override.Prevent, "", "%t%"), Description("PLC Analog Output Percent")> Public SlRefOut As Short
  <IO(IOType.Anout, 5, Override.Prevent, "", "%t%"), Description("PLC Analog Output Percent")> Public SrRefOut As Short
  <IO(IOType.Anout, 6, Override.Prevent, "", "%t%"), Description("PLC Analog Output Percent")> Public Pd1RefOut As Short
  <IO(IOType.Anout, 7, Override.Prevent, "", "%t%"), Description("PLC Analog Output Percent")> Public Pd2RefOut As Short
  <IO(IOType.Anout, 8, Override.Prevent, "", "%t%"), Description("PLC Analog Output Percent")> Public ObRefOut As Short



#End Region

  Public Sub New(ByVal controlCode As ControlCode)
    'Setup Communication Ports 
    '>> Use this method so that if the xml file doesn't declare the port, no "is nothing" exceptions will be thrown <<
    Dim port As String = ""

    ' Transport PLC - H0-ECOM100
    port = controlCode.Parent.Setting("Plc1")
    If Not String.IsNullOrEmpty(port) Then
      Try
        Plc1 = New Ports.Modbus(New Ports.ModbusTcp(port, 502))
      Catch ex As Exception : End Try
    End If

    ' Microspeed 196 RS422
    port = controlCode.Parent.Setting("Microspeed")
    If Not String.IsNullOrEmpty(port) Then
      Try
        ComsMicroSpeedPort = port
        ComsMicroSpeedEnabled = True
        ComsMicroSpeed = New Ports.MicroSpeed196(New Ports.SerialPort(ComsMicroSpeedPort, 19200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One))
      Catch ex As Exception : End Try
    End If

    ' Honeywell RS485
    port = controlCode.Parent.Setting("Honeywell")
    If Not String.IsNullOrEmpty(port) Then
      Try
        ComsHoneyWellPort = port
        ComsHoneyWellEnabled = True
        ComsHoneyWell = New Ports.Modbus(New Ports.SerialPort(ComsHoneyWellPort, 19200, System.IO.Ports.Parity.None, 8, System.IO.Ports.StopBits.One))
      Catch ex As Exception : End Try
    End If

    ' Fans Use Automation Direct Terminator Modbus TCP PLC T1K-EBC100
    port = controlCode.Parent.Setting("Fans")
    If Not String.IsNullOrEmpty(port) Then
      Try
        ComsPlcFansIp = port
        ComsPlcFansEnabled = True
        ComsPlcFans = New Ports.Modbus(New Ports.ModbusTcp(port, 502)) ' New Ports.ModbusTcp(port, 502)
      Catch ex As Exception : End Try
    End If

    'Reset Communication Port Alarm Timeout
    ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
    ComsHoneyWellTimer.Seconds = MinMax(controlCode.Parameters.PLCComsTime, 2, 30)
    Plc1Timer.Seconds = MinMax(controlCode.Parameters.PLCComsTime, 2, 30)

    '    ComsPlc1Timer.Seconds = ComsTime
    '    ComsPlc2Timer.Seconds = ComsTime
    '    ComsPlc3Timer.Seconds = ComsTime
    '    ComsPlc4Timer.Seconds = ComsTime
    '    ComsPlc5Timer.Seconds = ComsTime
    '     ComsMahloTimer.Seconds = ComsTime

  End Sub

  Public Function ReadInputs(ByVal parent As ACParent, ByVal dinp() As Boolean, ByVal aninp() As Short, ByVal temp() As Short, ByVal controlCode As ControlCode) As Boolean
    Dim returnResult As Boolean = False
    DateTimeNow = CurrentTime

    If ReadPlc1(parent, dinp, aninp, temp, controlCode) Then returnResult = True
    '    If ReadPlc2(parent, dinp, aninp, temp, controlCode) Then returnResult = True
    '    If ReadPlc3(parent, dinp, aninp, temp, controlCode) Then returnResult = True
    '    If ReadPlc4(parent, dinp, aninp, temp, controlCode) Then returnResult = True
    '    If ReadPlc5(parent, dinp, aninp, temp, controlCode) Then returnResult = True

    ' ReadHoneywells(parent, dinp, aninp, temp, controlCode)
    If ReadHoneywells(parent, dinp, aninp, temp, controlCode) Then returnResult = True
    '    ReadMahlo(parent, dinp, aninp, temp, controlCode)

    RunMicrospeedStateMachine(controlCode)
    ReadMicrospeeds(parent, dinp, aninp, temp, controlCode)

    If ReadPlcFans(parent, dinp, aninp, temp, controlCode) Then returnResult = True

    Return returnResult
  End Function

  Public Sub WriteOutputs(ByVal dout() As Boolean, ByVal anout() As Short, ByVal controlCode As ControlCode)
    DateTimeNow = CurrentTime

    WriteHoneywells(dout, anout, controlCode)
    RunMicrospeedStateMachine(controlCode)
    WriteMicrospeeds(dout, anout, controlCode)
    WritePlcFans(dout, anout, controlCode)
    WritePlc1(dout, anout, controlCode)

  End Sub


#Region " PLC1 - TRANSPORT PLC "

  'SLOTCPU  - DL262 CPU
  'SLOT0    - D2-12TR      - Relay Outputs             - 
  'SLOT1    - D2-16ND3-2   - 16-ch Digital Inputs      -
  'SLOT2    - D2-16ND3-2   - 16-ch Digital Inputs      - 
  'SLOT3    - F2-08AD-2    - 8-ch Analog Inputs        - 
  'SLOT4    - H2-CTRIO     - 4-ch High Speed Counter   - 
  'SLOT5    - H2-CTRIO     - 4-ch High Speed Counter   - 
  'SLOT6    - F2-08DA-2    - 8-ch Analog Outputs       - 
  'SLOT7    - H2-ECOM100   -                           - Ethernet Communications   

  Private Function ReadPlc1(ByVal parent As ACParent, ByVal dinp() As Boolean, ByVal aninp() As Short, ByVal temp() As Short, ByVal controlCode As ControlCode) As Boolean

    'Return true if any read succeeds
    Dim returnValue As Boolean = False
    Dim dateNow As Date = Date.Now

    'Make sure the driver is initialized
    If Plc1 Is Nothing Then Exit Function
    Dim plcAlarmTime = MinMax(controlCode.Parameters.PLCComsTime, 10, 100000)

    'READ DIGITAL INPUTS
    Dim DinpMain(16) As Boolean '32
    Select Case Plc1.Read(1, 10001 + 2048, DinpMain)         'Inputs (X) hxecomm.pdf page 5-12
      Case Ports.Modbus.Result.Fault
      Case Ports.Modbus.Result.OK
        'An array to configure the hardware inputs so that they appear in order in the control code IO Screen
        Dim i As Integer
        For i = 1 To 16 : dinp(i) = DinpMain(i) : Next i

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select

    'READ ANALOG INPUTS
    'V2000 octal = 1024 decimal - Location of Raw analog input BIN value
    'V2020 octal = 1040 decimal - Location of Scaled analog input BIN value (Scaling occurs in PLC, so we just display the value here)
    Dim valueSetAnalogInputs(50) As Short
    Select Case Plc1.Read(1, 40001 + 1024, valueSetAnalogInputs)
      Case Ports.Modbus.Result.Fault
      Case Ports.Modbus.Result.OK
        Aninp1Raw = valueSetAnalogInputs(1)                             'V2000 - AI1_Raw                  (1)
        Aninp2Raw = valueSetAnalogInputs(2)                             'V2001 - AI2_Raw                  (2)
        Aninp3Raw = valueSetAnalogInputs(3)                             'V2002 - AI3_Raw                  (3)
        Aninp4Raw = valueSetAnalogInputs(4)                             'V2003 - AI4_Raw                  (4)
        Aninp5Raw = valueSetAnalogInputs(5)                             'V2004 - AI5_Raw                  (5)
        Aninp6Raw = valueSetAnalogInputs(6)                             'V2005 - AI6_Raw                  (6)
        Aninp7Raw = valueSetAnalogInputs(7)                             'V2006 - AI7_Raw                  (7)
        Aninp8Raw = valueSetAnalogInputs(8)                             'V2007 - AI8_Raw                  (8)
        '                                                               'V2010-V2017: AI1-8 Absolute      (9-16)
        '                                                               'V2020-V2027: AI1-8 Smoothed      (17-24)
        aninp(1) = valueSetAnalogInputs(25)                             'V2030 - AI1_DancerScaled         (25)
        aninp(2) = valueSetAnalogInputs(26)                             'V2031 - AI2_Scaled               (26)
        aninp(3) = valueSetAnalogInputs(27)                             'V2032 - AI3_Scaled               (27)
        aninp(4) = valueSetAnalogInputs(28)                             'V2033 - AI4_Scaled               (28)
        aninp(5) = valueSetAnalogInputs(29)                             'V2034 - AI5_Scaled               (29)
        aninp(6) = valueSetAnalogInputs(30)                             'V2035 - AI6_Scaled               (30)
        aninp(7) = valueSetAnalogInputs(31)                             'V2036 - AI7_Scaled               (31)
        aninp(8) = valueSetAnalogInputs(32)                             'V2037 - AI8_Scaled               (32)
        '                                                               'V2040-V2047                      (33-40)
        '                                                               'V2050-V2057                      (41-48)
        '                                                               'V2060-V2067                      (49-56)
        '                                                               'V2070-V2077                      (57-64)

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select

    'READ COUNTERS 
    'hxecomm.pdf page 5-13          
    ' V2100 octal = 1088 decimal = Counters 1-4
    ' V2200 octal = 1152 decimal = Counters 5-8
    Dim valueSetCounters1(80) As UShort
    Select Case Plc1.Read(1, 40001 + 1088, valueSetCounters1)
      Case Ports.Modbus.Result.Fault
      Case Ports.Modbus.Result.OK
        '                                                               'V2100 = CT#1 Rate - Word 1       (1)
        '                                                               'V2101 = CT#1 Rate - Word 2       (2)
        TenterLeftRawCount = valueSetCounters1(3)                       'V2102 = CT#1 Raw Count - Word 1  (3)
        '                                                               'V2103 = CT#1 Raw Count - Word 2  (4)
        '                                                               'V2104 = CT#2 Rate - Word 1       (5)
        '                                                               'V2105 = CT#2 Rate - Word 2       (6)
        TenterRightRawCount = valueSetCounters1(7)                      'V2106 = CT#2 Raw Count - Word 1  (7)
        '                                                               'V2107 = CT#2 Raw Count - Word 2  (8)
        '                                                               'V2110 = CT#3 Rate - Word 1       (9)
        '                                                               'V2111 = CT#3 Rate - Word 2       (10)
        OvrFeedTopRawCount = valueSetCounters1(11)                      'V2112 = CT#3 Raw Count - Word 1  (11)
        '                                                               'V2113 = CT#3 Raw Count - Word 2  (12)
        '                                                               'V2114 = CT#4 Rate - Word 1       (13)
        '                                                               'V2115 = CT#4 Rate - Word 2       (14)
        LftSelvageRawCount = valueSetCounters1(15)                      'V2116 = CT#4 Raw Count - Word 1  (15)
        '                                                               'V2117 = CT#4 Raw Count - Word 2  (16)
        '                                                               'V2120-V2127                      (17-24)
        '                                                               'V2130-V2137                      (25-32)
        '                                                               'V2140-V2147                      (33-40)
        '                                                               'V2150-V2157                      (41-48)
        '                                                               'V2160-V2167                      (49-56)
        '                                                               'V2170-V2177                      (57-64)
        '                                                               'V2200 = CT#5 Rate - Word 1       (65)
        '                                                               'V2201 = CT#5 Rate - Word 2       (66)
        RtSelvageRawCount = valueSetCounters1(67)                       'V2202 = CT#5 Raw Count - Word 1  (67)
        '                                                               'V2203 = CT#5 Raw Count - Word 2  (68)
        '                                                               'V2204 = CT#6 Rate - Word 1       (69)
        '                                                               'V2205 = CT#6 Rate - Word 2       (70)
        Padder1RawCount = valueSetCounters1(71)                          'V2206 = CT#6 Raw Count - Word 1  (71)
        '                                                               'V2207 = CT#6 Raw Count - Word 2  (72)
        '                                                               'V2210 = CT#7 Rate - Word 1       (73)
        '                                                               'V2211 = CT#7 Rate - Word 2       (74)
        Padder2RawCount = valueSetCounters1(75)                         'V2212 = CT#7 Raw Count - Word 1  (75)
        '                                                               'V2213 = CT#7 Raw Count - Word 2  (76)
        '                                                               'V2114 = CT#8 Rate - Word 1       (77)
        '                                                               'V2215 = CT#8 Rate - Word 2       (78)
        OvrFeedBotRawCount = valueSetCounters1(79)                        'V2216 = CT#8 Raw Count - Word 1  (79)
        '                                                               'V2217 = CT#8 Raw Count - Word 2  (80)
        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select


    'READ DEBUG COUNTERS            ** ADDED 2013-09-26 w/ PLC Version 127 **
    ' V3000 octal = 1536 decimal = Debug Counters X0-X5 for first (6) inputs
    Dim valueSetCounters2(10) As Short
    Select Case Plc1.Read(1, 40001 + 1536, valueSetCounters2)
      Case Ports.Modbus.Result.Fault
      Case Ports.Modbus.Result.OK
        controlCode.Debug_X0PowerOnCount = valueSetCounters2(1)         'V3000 = X0Count
        controlCode.Debug_X1InAutomaticCount = valueSetCounters2(2)     'V3001 = X1Count
        controlCode.Debug_X2InCombined = valueSetCounters2(3)           'V3002 = X2Count
        controlCode.Debug_X3TenterStartReq = valueSetCounters2(4)       'V3003 = X3Count
        controlCode.Debug_X4TenterSp12 = valueSetCounters2(5)           'V3004 = X4Count
        controlCode.Debug_X5DoffinOn = valueSetCounters2(6)             'V3005 = X5Count

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select


    ' Read remote setpoint values
    Dim vsSetpoints(8) As Short
    Select Case Plc1.Read(1, 40001 + 2561, vsSetpoints)   ' V5001 Octal = 2561 decimal
      Case Ports.Modbus.Result.OK
        With controlCode
          .TenterLeft.SetpointActual = vsSetpoints(1)     ' V5001
          .TenterRight.SetpointActual = vsSetpoints(2)    ' V5002
          .OverfeedTop.SetpointActual = vsSetpoints(3)    ' V5003
          .SelvageLeft.SetpointActual = vsSetpoints(4)    ' V5004
          .SelvageRight.SetpointActual = vsSetpoints(5)   ' V5005
          .Padder(1).SetpointActual = vsSetpoints(6)      ' V5006
          .Padder(2).SetpointActual = vsSetpoints(7)      ' V5007
          .OverfeedBot.SetpointActual = vsSetpoints(8)    ' V5010
        End With

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select


    '******************************************************************************************************
    '****************                             PLC BIN Range                            ****************
    '******************************************************************************************************
    '  Read Decimal V-Memory Ranges:       
    '  TENTER MAIN = V5500 octal = 2880 decimal
    Dim vsParamTenter(64) As Short
    Select Case Plc1.Read(1, 40001 + 2880, vsParamTenter)
      Case Ports.Modbus.Result.OK

        'Tenter Virtual Control: V5500 - V5599
        With controlCode.Tenter
          'V5500 - V5507 (1 - 8)
          .Prm_AutoEnable = vsParamTenter(1)                            'V5500 AutoEnable
          .Prm_SpeedMaxRange = vsParamTenter(2)                         'V5501 MaxRPM
          ' vsParamTenter(3)                                            'V5502 FeedbackPPR
          ' vsParamTenter(4)                                            'V5503 Alarm High Speed
          ' vsParamTenter(5)                                            'V5504 Alarm Low Speed
          ' vsParamTenter(6)                                            'V5505 At Speed Deviation (Ramp Complete)
          .Prm_SetpointValue1 = vsParamTenter(7)                        'V5506 SP1
          .Prm_SetpointValue2 = vsParamTenter(8)                        'V5507 SP2

          'V5510 - V5517 (9 - 16)
          .Prm_SetpointValue3 = vsParamTenter(9)                        'V5510 SP3
          .Prm_SetpointValue4 = vsParamTenter(10)                       'V5511 SP4
          .Prm_SetpointMax = vsParamTenter(11)                          'V5512 SPMax
          .Prm_SetpointMin = vsParamTenter(12)                          'V5513 SPMin
          ' vsParamTenter(13)                                           'V5514
          ' vsParamTenter(14)                                           'V5515
          ' vsParamTenter(15)                                           'V5516
          ' vsParamTenter(16)                                           'V5517

          'V5520 - V5527 (17 - 24)
          .Plc_SetpointChActive = vsParamTenter(17)                     ' V5520 ActiveSp#
          ' vsParamTenter(18)                                           ' V5521 ActiveSP#Last
          .Plc_SetpointCh2Request = vsParamTenter(19)                   ' V5572 Remote SP2 Request
          .SetpointActual = vsParamTenter(20)                           ' V5523 Setpoint Actual

          'Use for displaying issues with communications & clearing local values
          .Coms_ScanLast = dateNow
        End With

        aninp(11) = vsParamTenter(20)

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select


    '  SC1 = TENTER LEFT  V6000 octal = 3072 decimal  
    Dim vsParamTenterLeft(64) As Short
    Select Case Plc1.Read(1, 40001 + 3072, vsParamTenterLeft)
      Case Ports.Modbus.Result.OK
        'Pass ValueSet to decode subroutine to minimize copy/paste work
        DecodeFollowerParameters(controlCode.TenterLeft, vsParamTenterLeft)

        'Another copy for the I/O Tab
        aninp(12) = vsParamTenterLeft(41) ' controlCode.TenterLeft.Plc_SpeedActual
        aninp(13) = vsParamTenterLeft(42) ' Speed Desired

        '  aninp(31) = vsParamTenterLeft(47) ' Output percent
        TclRefOut = vsParamTenterLeft(47)

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select


    '  SC2 = TENTER RIGHT  V6100 octal = 3136 decimal  
    Dim vsParamTenterRight(64) As Short
    Select Case Plc1.Read(1, 40001 + 3136, vsParamTenterRight)
      Case Ports.Modbus.Result.OK
        'Pass ValueSet to decode subroutine to minimize copy/paste work
        DecodeFollowerParameters(controlCode.TenterRight, vsParamTenterRight)

        'Another copy for the I/O Tab
        aninp(14) = vsParamTenterRight(41) ' Speed Actual
        aninp(15) = vsParamTenterRight(42) ' Speed Desired

        '  aninp(32) = vsParamTenterRight(47)   ' PLC Output percent
        TcrRefOut = vsParamTenterRight(47)      ' PLC Output Percent

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select



    '  SC3 = Overfeed Top = V6200 octal = 3200 decimal
    Dim vsParamOverfeedTop(64) As Short
    Select Case Plc1.Read(1, 40001 + 3200, vsParamOverfeedTop)
      Case Ports.Modbus.Result.OK
        'Pass ValueSet to decode subroutine to minimize copy/paste work
        DecodeFollowerParameters(controlCode.OverfeedTop, vsParamOverfeedTop)

        'Copy for I/O Tab
        aninp(16) = CShort(controlCode.OverfeedTop.Plc_SpeedActual)
        aninp(17) = CShort(controlCode.OverfeedTop.Plc_SpeedDesired)

        'aninp(33) = CShort(controlCode.OverfeedTop.Plc_OutputPercent)
        OtRefOut = vsParamOverfeedTop(47)      ' PLC Output Percent

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select



    '  SC4 = Selvage Left = V6300 octal = 3264 decimal 
    Dim vsParamSelvageLeft(64) As Short
    Select Case Plc1.Read(1, 40001 + 3264, vsParamSelvageLeft)
      Case Ports.Modbus.Result.OK
        'Pass ValueSet to decode subroutine to minimize copy/paste work
        DecodeFollowerParameters(controlCode.SelvageLeft, vsParamSelvageLeft)

        'Copy for I/O Tab
        aninp(18) = CShort(controlCode.SelvageLeft.Plc_SpeedActual)
        aninp(19) = CShort(controlCode.SelvageLeft.Plc_SpeedDesired)
        ' aninp(34) = CShort(controlCode.SelvageLeft.Plc_OutputPercent)
        SlRefOut = vsParamSelvageLeft(47)      ' PLC Output Percent

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select


    '  SC5 = Selvage Right = V6400 octal = 3328 decimal 
    Dim vsParamSelvageRight(64) As Short
    Select Case Plc1.Read(1, 40001 + 3328, vsParamSelvageRight)
      Case Ports.Modbus.Result.OK
        'Pass ValueSet to decode subroutine to minimize copy/paste work
        DecodeFollowerParameters(controlCode.SelvageRight, vsParamSelvageRight)

        'Copy for I/O Tab
        aninp(20) = CShort(controlCode.SelvageRight.Plc_SpeedActual)
        aninp(21) = CShort(controlCode.SelvageRight.Plc_SpeedDesired)
        'aninp(35) = CShort(controlCode.SelvageRight.Plc_OutputPercent)
        SrRefOut = vsParamSelvageRight(47)      ' PLC Output Percent

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select


    '  SC6 = Padder 1 = V6500 octal = 3392 decimal
    Dim vsParamPadder1(64) As Short
    Select Case Plc1.Read(1, 40001 + 3392, vsParamPadder1)
      Case Ports.Modbus.Result.OK
        'PadController has more details than standard SpeedController (Dancer Position and Trim Correction), Decode manually
        With controlCode.Padder(1)
          .Prm_AutoEnable = vsParamPadder1(1)                        'V6x00 Auto Enable 
          .Prm_SpeedMaxRange = vsParamPadder1(2)                     'V6x01 Max Speed Range
          'values(3)                                                'V6x02 Feedback PPR
          'values(4)                                                'V6x03 Alarm Error High
          'values(5)                                                'V6x04 Alarm Error Low
          'values(6)                                                'V6x05 At Speed Deviation
          'values(7)                                                'V6x06 -
          'values(8)                                                'V6x07 -

          'values(9)                                                'V6x10 -
          'values(10)                                               'V6x11 -
          .Prm_SetpointMax = vsParamPadder1(11)                      'V6x12 Setpoint Max
          .Prm_SetpointMin = vsParamPadder1(12)                      'V6x13 Setpoint Min
          'values(13)                                               'V6x14 -
          'values(14)                                               'V6x15 -
          'values(15)                                               'V6x16 -
          'values(16)                                               'V6x17 -

          .Prm_GainRamp = vsParamPadder1(17)                         'V6x20 Gain Ramp
          .Prm_GainAtSpd = vsParamPadder1(18)                        'V6x21 Gain At Speed 
          .Prm_GainMaxAdj = vsParamPadder1(19)                       'V6x22 Gain Max Adjustment
          .Prm_SpdErrorAllow = vsParamPadder1(20)                    'V6x23 Speed Error Allowed
          'values(21)                                               'V6x24 -
          'values(22)                                               'V6x25 -
          'values(23)                                               'V6x26 -
          'values(24)                                               'V6x27 -

          .Prm_DncEnabled = vsParamPadder1(25)                       'V6x30 Dancer Position Trim Enabled (=1)
          .Prm_DncPosSv = vsParamPadder1(26)                         'V6x31 Dancer Position Desired (0-1000)
          .Prm_DncGainPct = vsParamPadder1(27)                       'V6x32 Dancer Gain Percent (0-1000)
          .Prm_DncTrimMode = vsParamPadder1(28)                      'V6x33 Dancer Trim Mode (0-1)
          .Prm_DncDelayOnSec = vsParamPadder1(29)                    'V6x34 Dancer Delay ON Seconds
          .Prm_DncDelayAdjSec = vsParamPadder1(30)                   'V6535 Dancer Delay Adjust Seconds                                               'V6x35 -
          .Prm_DncSpdMaxAdj = vsParamPadder1(31)                     'V6x36 Dancer Speed Max Adjustment
          'values(32)                                               'V6x37 -

          .Plc_SpeedActual = vsParamPadder1(41)                      'V6x50 Speed Actual (Remaining Registers are Copy of REALS)
          .Plc_SpeedDesired = vsParamPadder1(42)                     'V6x51 Speed Desired
          .Plc_SpeedError = vsParamPadder1(43)                       'V6x52 Speed Error
          ' values(44)                                              'V6x53
          ' values(45)                                              'V6x54
          .Plc_OutputGainAdj = vsParamPadder1(46)                    'V6x55 Gain Adjust
          .Plc_OutputPercent = vsParamPadder1(47)                    'V6x56 Ouptut Percent

          Pd1RefOut = vsParamPadder1(47)                            'V6x56 Output %

          'Use for displaying issues with communications & clearing local values
          .Coms_ScanLast = Date.Now
        End With

        'Copy for I/O Tab
        aninp(22) = CShort(controlCode.Padder(1).Plc_SpeedActual)
        aninp(23) = CShort(controlCode.Padder(1).Plc_SpeedDesired)
        'aninp(36) = CShort(controlCode.Padder(1).Plc_OutputPercent)

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select



    '  SC7 = Padder 2 = V6600 octal = 3456 decimal
    Dim vsParamPadder2(64) As Short
    Select Case Plc1.Read(1, 40001 + 3456, vsParamPadder2)
      Case Ports.Modbus.Result.OK
        'PadController has more details than standard SpeedController (Dancer Position and Trim Correction), Decode manually
        With controlCode.Padder(2)
          .Prm_AutoEnable = vsParamPadder2(1)                       'V6x00 Auto Enable 
          .Prm_SpeedMaxRange = vsParamPadder2(2)                    'V6x01 Max Speed Range
          'values(3)                                                'V6x02 Feedback PPR
          'values(4)                                                'V6x03 Alarm Error High
          'values(5)                                                'V6x04 Alarm Error Low
          'values(6)                                                'V6x05 At Speed Deviation
          'values(7)                                                'V6x06 -
          'values(8)                                                'V6x07 -

          'values(9)                                                'V6x10 -
          'values(10)                                               'V6x11 -
          .Prm_SetpointMax = vsParamPadder2(11)                      'V6x12 Setpoint Max
          .Prm_SetpointMin = vsParamPadder2(12)                      'V6x13 Setpoint Min
          'values(13)                                               'V6x14 -
          'values(14)                                               'V6x15 -
          'values(15)                                               'V6x16 -
          'values(16)                                               'V6x17 -

          .Prm_GainRamp = vsParamPadder2(17)                         'V6x20 Gain Ramp
          .Prm_GainAtSpd = vsParamPadder2(18)                        'V6x21 Gain At Speed 
          .Prm_GainMaxAdj = vsParamPadder2(19)                       'V6x22 Gain Max Adjustment
          .Prm_SpdErrorAllow = vsParamPadder2(20)                    'V6x23 Speed Error Allowed
          'values(21)                                               'V6x24 -
          'values(22)                                               'V6x25 -
          'values(23)                                               'V6x26 -
          'values(24)                                               'V6x27 -

          .Prm_DncEnabled = vsParamPadder2(25)                       'V6x30 Dancer Position Trim Enabled (=1)
          .Prm_DncPosSv = vsParamPadder2(26)                         'V6x31 Dancer Position Desired (0-1000)
          .Prm_DncGainPct = vsParamPadder2(27)                       'V6x32 Dancer Gain Percent (0-1000)
          .Prm_DncTrimMode = vsParamPadder2(28)                      'V6x33 Dancer Trim Mode (0-1)
          .Prm_DncDelayOnSec = vsParamPadder2(29)                    'V6x34 Dancer Delay ON Seconds
          .Prm_DncDelayAdjSec = vsParamPadder2(30)                   'V6535 Dancer Delay Adjust Seconds                                               'V6x35 -
          .Prm_DncSpdMaxAdj = vsParamPadder2(31)                     'V6x36 Dancer Speed Max Adjustment
          'values(32)                                               'V6x37 -

          .Plc_SpeedActual = vsParamPadder2(41)                      'V6x50 Speed Actual (Remaining Registers are Copy of REALS)
          .Plc_SpeedDesired = vsParamPadder2(42)                     'V6x51 Speed Desired
          .Plc_SpeedError = vsParamPadder2(43)                       'V6x52 Speed Error
          ' values(44)                                              'V6x53
          ' values(45)                                              'V6x54
          .Plc_OutputGainAdj = vsParamPadder2(46)                    'V6x55 Gain Adjust
          .Plc_OutputPercent = vsParamPadder2(47)                    'V6x56 Ouptut Percent

          Pd2RefOut = vsParamPadder2(47)                            'V6x56 Output %

          'Use for displaying issues with communications & clearing local values
          .Coms_ScanLast = Date.Now
        End With

        'Copy for I/O Tab
        aninp(24) = CShort(controlCode.Padder(2).Plc_SpeedActual)
        aninp(25) = CShort(controlCode.Padder(2).Plc_SpeedDesired)
        'aninp(37) = CShort(controlCode.Padder(2).Plc_OutputPercent)

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select


    '  SC8 = OVERFEED BOTTOM = V6700 octal = 3520 decimal
    Dim vsParamOverfeedBottom(64) As Short
    Select Case Plc1.Read(1, 40001 + 3520, vsParamOverfeedBottom)
      Case Ports.Modbus.Result.OK
        'Pass ValueSet to decode subroutine to minimize copy/paste work
        DecodeFollowerParameters(controlCode.OverfeedBot, vsParamOverfeedBottom)

        ObRefOut = vsParamOverfeedBottom(47)    'V6x56 Output %

        'Copy for I/O Tab
        aninp(26) = CShort(controlCode.OverfeedBot.Plc_SpeedActual)
        aninp(27) = CShort(controlCode.OverfeedBot.Plc_SpeedDesired)
        '  aninp(38) = CShort(controlCode.OverfeedBot.Plc_OutputPercent)

        'Reset Comms Timer and Update Return Value flag
        Plc1Timer.Seconds = plcAlarmTime
        returnValue = True
    End Select


  End Function

  Private Shared Sub DecodeFollowerParameters(controller As SpeedController, values As Short())
    With controller
      'V6x00 - V6x07 (1 - 8)
      .Prm_AutoEnable = values(1)                         'V6x00 Auto Enable 
      .Prm_SpeedMaxRange = values(2)                      'V6x01 Max Speed Range
      'values(3)                                          'V6x02 Feedback PPR
      'values(4)                                          'V6x03 Alarm Error High
      'values(5)                                          'V6x04 Alarm Error Low
      'values(6)                                          'V6x05 At Speed Deviation
      'values(7)                                          'V6x06 -
      'values(8)                                          'V6x07 -

      'V6x10 - V6x17 (9 - 16)
      'values(9)                                          'V6x10 -
      'values(10)                                         'V6x11 -
      .Prm_SetpointMax = values(11)                       'V6x12 Setpoint Max
      .Prm_SetpointMin = values(12)                       'V6x13 Setpoint Min
      'values(13)                                         'V6x14 -
      'values(14)                                         'V6x15 -
      'values(15)                                         'V6x16 -
      'values(16)                                         'V6x17 -

      'V6x20 - V6x20 (17 - 24)
      .Prm_GainRamp = values(17)                          'V6x20 Gain Ramp
      .Prm_GainAtSpd = values(18)                         'V6x21 Gain At Speed 
      .Prm_GainMaxAdj = values(19)                        'V6x22 Gain Max Adjustment
      .Prm_SpdErrorAllow = values(20)                     'V6x23 Speed Error Allowed
      'values(21)                                         'V6x24 -
      'values(22)                                         'V6x25 -
      'values(23)                                         'V6x26 -
      'values(24)                                         'V6x27 -

      'V6x30 - V6x37 (25 - 32)


      'V6x40 - V6x47 (33 - 40)
      ' .SetpointActual = values(33)                        'V6x40 Setpoint % Actual


      'V6x50 - V6x57 (41 - 48)
      .Plc_SpeedActual = values(41)                       'V6x50 Speed Actual (Remaining Registers are Copy of REALS)
      .Plc_SpeedDesired = values(42)                      'V6x51 Speed Desired
      .Plc_SpeedError = values(43)                        'V6x52 Speed Desired
      ' values(44)                                        'V6x53
      ' values(45)                                        'V6x54
      .Plc_OutputGainAdj = values(46)                     'V6x55 Gain Adjust
      .Plc_OutputPercent = values(47)                     'V6x56 Ouptut Percent
      ' values(48)                                        'V6x57

      'V6x60 - V6x67 (49 - 56)
      .Plc_SpeedOffset = values(49)                       'V6560 Auxillary Trim Speed Setpoint Offset
      '.Plc_SpeedOffsetNeg = values(50)                    'V6561 Auxillary Trim Speed Setpoint Offset Sign (0=positive, 1=negative speed offset)
      ' values(51)                                        'V6x62
      ' values(52)                                        'V6x63
      ' values(53)                                        'V6x64
      ' values(54)                                        'V6x65
      ' values(55)                                        'V6x66
      ' values(56)                                        'V6x67

      'V6x70 - V6x77 (57 - 64)
      ' values(57)                                        'V6x70
      ' values(58)                                        'V6x71
      ' values(59)                                        'V6x72
      ' values(60)                                        'V6x73
      ' values(61)                                        'V6x74
      ' values(62)                                        'V6x75
      ' values(63)                                        'V6x76
      ' values(64)                                        'V6x77

      'Use for displaying issues with communications & clearing local values
      .Coms_ScanLast = Date.Now
    End With

  End Sub


  Private Sub WritePlc1(ByVal dout() As Boolean, ByVal anout() As Short, ByVal controlCode As ControlCode)

    'Make sure the driver is initialized
    If Plc1 Is Nothing Then Exit Sub
    Dim plcAlarmTime = MinMax(controlCode.Parameters.PLCComsTime, 10, 100000)

    'Write Each Setpoint Down Independantly - Use One-Shot logic

    ' SC1 = Tenter Production Setpoint = V5020 = 2576
    With controlCode.Tenter
      Dim vsTenterSetpoint(1) As Short : vsTenterSetpoint(1) = CShort(.SetpointDesired)
      If .SetpointStatus = TenterController.EUpdateState.Request Then
        Select Case Plc1.Write(1, 40001 + 2576, vsTenterSetpoint, Ports.WriteMode.Always)
          Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
            .SetpointStatusClear()
            Plc1Timer.Seconds = plcAlarmTime
        End Select
      End If
    End With


#If 0 Then
    ' 2022-05-25 - AdeleKenyon_Transport version 15 does not have this flag
    '            - Pushbuttons used to control active setpoint selected
    'Tenter Thread Setpoint Channel 2 Request = V5522 = 2898
    With controlCode.Tenter
      If .Setpoint2RequestStatus = TenterController.EUpdateState.Request Then
        Dim SetpointCh2RequestValue As Integer
        If .Plc_SetpointChActive <> 2 Then
          SetpointCh2RequestValue = 1
        Else : SetpointCh2RequestValue = 0
        End If
        Select Case Plc1.Write(1, 40001 + 2898, {CShort(0), CShort(SetpointCh2RequestValue)}, Ports.WriteMode.Always)
          Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
            .Setpoint2RequestClear()
            Plc1Timer.Seconds = plcAlarmTime
        End Select
      End If
    End With
#End If


    '  SC1 = Tenter Left Setpoint Percent = V5021 = 2577
    With controlCode.TenterLeft
      If .SetpointStatus = SpeedController.EUpdateState.Request Then
        Select Case Plc1.Write(1, 40001 + 2577, {CShort(0), CShort(.SetpointDesired)}, Ports.WriteMode.Always)
          Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
            .SetpointStatusClear()
            Plc1Timer.Seconds = plcAlarmTime
        End Select
      End If
    End With


    '  SC2 = Tenter Right Setpoint Percent = V5022 = 2578
    With controlCode.TenterRight
      If .SetpointStatus = SpeedController.EUpdateState.Request Then
        Select Case Plc1.Write(1, 40001 + 2578, {CShort(0), CShort(.SetpointDesired)}, Ports.WriteMode.Always)
          Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
            .SetpointStatusClear()
            Plc1Timer.Seconds = plcAlarmTime
        End Select
      End If
    End With


    '  SC3 = Overfeed Top Setpoint Percent = V5023 = 2579
    With controlCode.OverfeedTop
      If .SetpointStatus = SpeedController.EUpdateState.Request Then
        Select Case Plc1.Write(1, 40001 + 2579, {CShort(0), CShort(.SetpointDesired)}, Ports.WriteMode.Always)
          Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
            .SetpointStatusClear()
            Plc1Timer.Seconds = plcAlarmTime
        End Select
      End If
    End With


    '  SC4 = Selvage Left Setpoint Percent = V5024 = 2580
    With controlCode.SelvageLeft
      If .SetpointStatus = SpeedController.EUpdateState.Request Then
        Select Case Plc1.Write(1, 40001 + 2580, {CShort(0), CShort(.SetpointDesired)}, Ports.WriteMode.Always)
          Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
            .SetpointStatusClear()
            Plc1Timer.Seconds = plcAlarmTime
        End Select
      End If
    End With


    '  SC5 = Selvage Right Setpoint Percent = V5025 = 2581
    With controlCode.SelvageRight
      If .SetpointStatus = SpeedController.EUpdateState.Request Then
        Select Case Plc1.Write(1, 40001 + 2581, {CShort(0), CShort(.SetpointDesired)}, Ports.WriteMode.Always)
          Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
            .SetpointStatusClear()
            Plc1Timer.Seconds = plcAlarmTime
        End Select
      End If
    End With


    '  SC6 = Padder 1 Setpoint Percent = V5026 = 2582
    With controlCode.Padder(1)
      If .SetpointStatus = SpeedController.EUpdateState.Request Then
        Select Case Plc1.Write(1, 40001 + 2582, {CShort(0), CShort(.SetpointDesired)}, Ports.WriteMode.Always)
          Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
            .SetpointStatusClear()
            Plc1Timer.Seconds = plcAlarmTime
        End Select
      End If
    End With


    '  SC7 = Padder 2 Setpoint Percent = V5027 = 2583
    With controlCode.Padder(2)
      If .SetpointStatus = SpeedController.EUpdateState.Request Then
        Select Case Plc1.Write(1, 40001 + 2583, {CShort(0), CShort(.SetpointDesired)}, Ports.WriteMode.Always)
          Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
            .SetpointStatusClear()
            Plc1Timer.Seconds = plcAlarmTime
        End Select
      End If
    End With


    '  SC8 = Overfeed Bottom Setpoint Percent = V5030 = 2584
    With controlCode.OverfeedBot
      If .SetpointStatus = SpeedController.EUpdateState.Request Then
        Select Case Plc1.Write(1, 40001 + 2584, {CShort(0), CShort(.SetpointDesired)}, Ports.WriteMode.Always)
          Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
            .SetpointStatusClear()
            Plc1Timer.Seconds = plcAlarmTime
        End Select
      End If
    End With


  End Sub

#End Region


#Region " HONEYWELL AIR TEMP CONTROLLERS - RS485 "

  Private Function ReadHoneywells(ByVal parent As ACParent, ByVal dinp() As Boolean, ByVal aninp() As Short, ByVal temp() As Short, ByVal controlCode As ControlCode) As Boolean
    ' Return Result
    Dim returnResult As Boolean = False

    'Make sure the coms driver has been initialized
    If ComsHoneyWell Is Nothing Then Exit Function

    'Zone Temp Controllers 1-8
    Dim i As Integer
    For i = 1 To controlCode.AirTemp_Zone.Length - 1
      With controlCode.AirTemp_Zone(i)
        Dim hwNode As Integer = .Coms_Node
        If hwNode = 0 Then
          'This Honeywell controller not cofigured for node address, skip to next controller
          .SetpointStatus = HoneyWell.UpdateState.Offline
          Continue For
        Else
          'Don't read while attempting to write:
          If .DelayTimer.Finished Then

            'Read PresentValue, RemoteValue, & WorkingSetpoint
            Dim hwStatusSet1(4) As Short
            Select Case ComsHoneyWell.Read(hwNode, 40001, hwStatusSet1)
              Case Ports.Modbus.Result.Fault
                Dim fault As Integer = 0

              Case Ports.Modbus.Result.OK
                'HoneyWell - RTU Serial Comms User Manual 02/09 - Table A.3 Page 31
                ' NOTE: UDC3300 communications must be set to MODB3K to emulate UDC3000 Modbus communications!
                ' Update I/O values
                temp(i) = CType(hwStatusSet1(1), Short)        'TempActual = Present Value (PV)
                temp(i + 8) = CType(hwStatusSet1(2), Short)    'RemoteValue = Remote Setpoint Point (RV; SP2)
                aninp(i + 300) = CType(hwStatusSet1(3), Short)   'WorkingOutput = SP2

                'Fill in the class modules (at the same time)
                .PresentValue = CType(hwStatusSet1(1), Short)
                .RemoteValue = CType(hwStatusSet1(2), Short)
                .WorkingOutput = CType(hwStatusSet1(3), Short)
                .Coms_ScanLast = CurrentTime

                returnResult = True
            End Select

          End If
        End If
      End With
    Next i

    Return returnResult
  End Function

  Private Sub WriteHoneywells(ByVal dout() As Boolean, ByVal anout() As Short, ByVal controlCode As ControlCode)

    'Make sure the driver is initialized
    If ComsHoneyWell Is Nothing Then Exit Sub

    'Useful variables
    Dim HoneyWellSp(1) As Short
    Dim node As Integer = 0

    'HoneyWell Temperature Controllers
    For i As Integer = 1 To controlCode.AirTemp_Zone.Length - 1
      With controlCode.AirTemp_Zone(i)
        HoneyWellSp(1) = CType(Math.Min(.SetpointDesired, Short.MaxValue), Short)
        node = .Coms_Node
        If node = 0 Then
          'If node not set, then do nothing - skip to next device
          Continue For
        Else

          If .SetpointStatus = HoneyWell.UpdateState.Requested Then
            'Write the HoneyWell Setpoints
            Select Case ComsHoneyWell.Write(node, 40002, HoneyWellSp, Ports.WriteMode.Optimised)
              Case Ports.Modbus.Result.Fault
              Case Ports.Modbus.Result.OK
                .SetpointStatus = HoneyWell.UpdateState.Sent
                .DelayTimer.TimeRemaining = 10
            End Select
          End If

        End If
      End With
    Next i

  End Sub

#End Region


#Region " MICROSPSEED CONTROLS - RS422 "

  '******************************************************************************************************
  '****************                             MICROSPEED196                            ****************
  '******************************************************************************************************
  'Use this to control comms state 
  Public Enum EMicrospeedComsState
    Idle
    Read = 1
    WaitForDisabled = 2
    Write = 3
    ReadAfterWrite = 4
    WriteExecute = 5
  End Enum
  Public MicrospeedComsState As EMicrospeedComsState = EMicrospeedComsState.Read

  Public Enum ETenterSetpointNext
    MasterSetpoint1
    MasterSetpoint2
    MasterSetpoint3
    MasterSetpoint4
  End Enum
  Public MicrospeedComsTcSpNext As ETenterSetpointNext = ETenterSetpointNext.MasterSetpoint1

  Private Sub RunMicrospeedStateMachine(ByVal controlCode As ControlCode)

    'Make sure the driver is initialized
    If ComsMicroSpeed Is Nothing Then Exit Sub

    'Check to see if any microspeeds are trying to write 
    '  if they are stop reading so we can write these values more quickly
    Select Case MicrospeedComsState
      Case EMicrospeedComsState.Read
        ' Normal read state - check to see if we need to write new values
        For Each microSpeed As MicroSpeed In controlCode.Microspeeds
          If (microSpeed.SetpointStatus = Global.MicroSpeed.EUpdateState.Request) OrElse (microSpeed.SetpointStatus = Global.MicroSpeed.EUpdateState.Execute) Then
            'Disable coms then wait for any pending transactions to complete
            MicrospeedComsState = EMicrospeedComsState.WaitForDisabled
            ComsMicroSpeed.SetDisabled(True)
            Exit For
          End If
        Next microSpeed

      Case EMicrospeedComsState.WaitForDisabled
        'All pending coms have completed so now we can send the write commands
        If ComsMicroSpeed.Disabled Then
          MicrospeedComsState = EMicrospeedComsState.Write
          ComsMicroSpeed.SetDisabled(False)
        End If

      Case EMicrospeedComsState.Write
        'Once all write requests have completed switch back to read
        Dim ChangeToReadState As Boolean = True
        For Each microSpeed As MicroSpeed In controlCode.Microspeeds
          If (microSpeed.SetpointStatus = Global.MicroSpeed.EUpdateState.Request) OrElse (microSpeed.SetpointStatus = Global.MicroSpeed.EUpdateState.Execute) Then
            ChangeToReadState = False
            Exit For
          End If
        Next microSpeed
        'Check to see if we are ready to change back to read mode
        If ChangeToReadState Then
          MicrospeedComsState = EMicrospeedComsState.Read
        End If

    End Select

  End Sub

  Private Sub ReadMicrospeeds(ByVal parent As ACParent, ByVal dinp() As Boolean, ByVal aninp() As Short, ByVal temp() As Short, ByVal controlCode As ControlCode)
    Try

      ' Reset Microspeed communication state logic if lost
      If controlCode.Parameters.MicrospeedComsTime > 0 Then
        If ComsMicroSpeedTimer.Finished Then
          If Not ComsMicroSpeed.Disabled Then
            ComsMicroSpeed.SetDisabled(True)
            ComsMicroSpeedTimer.Seconds = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 10)
          Else
            ComsMicroSpeed.SetDisabled(False)
            ComsMicroSpeedTimer.Seconds = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 10)
          End If
        End If
      End If

      'Make sure the coms driver has been initialized
      If ComsMicroSpeed Is Nothing Then Exit Sub

      'If we have been disabled then make sure we don't do any more coms until we're enabled
      If ComsMicroSpeed.Disabled Then Exit Sub

      'If we're writing - don't read
      If (MicrospeedComsState = EMicrospeedComsState.Write) Then Exit Sub

      ' Conveyor
      '      ReadMicrospeed(controlCode.Conveyor, controlCode)
      '      aninp(10) = CType(Math.Min(controlCode.Conveyor.DisplayValue, Short.MaxValue), Short)

      'Stripper
      '      ReadMicrospeed(controlCode.Stripper, controlCode)
      '      aninp(11) = CType(Math.Min(controlCode.Stripper.DisplayValue, Short.MaxValue), Short)

      ' Folder
      '      ReadMicrospeed(controlCode.Folder, controlCode)
      '      aninp(5) = CType(Math.Min(controlCode.Folder.DisplayValue, Short.MaxValue), Short)

      'Width Screw
      For i As Integer = 1 To controlCode.Width_Screw.Length - 1
        ReadMicrospeed(controlCode.Width_Screw(i), controlCode)
        aninp(i + 200) = CType(Math.Min(controlCode.Width_Screw(i).DisplayValue, Short.MaxValue), Short)
        aninp(i + 205) = CType(Math.Min(controlCode.Width_Screw(i).ActiveSetpointValue, Short.MaxValue), Short)
      Next i

    Catch ex As Exception
      Dim message As String = ex.Message
    End Try
  End Sub

  Private Sub ReadMicrospeed(ByVal Microspeed As MicroSpeed, ByVal controlCode As ControlCode)
    Try
      'Make sure the coms driver has been initialized
      If ComsMicroSpeed Is Nothing Then Exit Sub

      'If we have been disabled then make sure we don't do any more coms until we're enabled
      If ComsMicroSpeed.Disabled Then Exit Sub

      'Make sure we have an object
      If Microspeed Is Nothing Then Exit Sub

      'Read the Serial Status
      Dim ss, decimalLocation As Integer
      Dim setpoint As Integer = 0
      Dim node = Microspeed.Coms_Node

      If node = 0 Then
        Microspeed.SetpointStatus = Global.MicroSpeed.EUpdateState.Offline
      Else

        ' Variable 49 - ReadOnly - New Setpoint Value
        If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.NewSetPoint, ss, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
          ' Perform Update inside Microspeed
          Microspeed.UpdateSetpointNewValue(ss, decimalLocation)
          ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
        End If

        ' Variable 50 - Serial Status
        If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.SerialStatus, ss, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
          Microspeed.OperationStatus = ss \ 1000
          If ((ss \ 100) Mod 10) = 1 Then
            Microspeed.ModeOperation = Global.MicroSpeed.EModeOperation.Remote
          Else : Microspeed.ModeOperation = Global.MicroSpeed.EModeOperation.Manual
          End If
          'Microspeed.IsRemote = (((ss \ 100) Mod 10) = 1)
          Microspeed.ActiveSetpoint = (ss \ 10) Mod 10
          Microspeed.Coms_ReadLast = CurrentTime
          ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
        End If

        ' Variable 62 - ReadOnly - Display Value
        If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.Display, ss, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
          ' Perform Update inside Microspeed
          Microspeed.UpdateDisplayValue(ss, decimalLocation)
          ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
        End If

        ' Variable 51 - Master(0) or Follower(1) Mode
        If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.MasterOrFollowerMode, ss, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
          If ss = 1 Then
            Microspeed.ModeSetpoint = Global.MicroSpeed.EModeSetpoint.Follower
          Else
            Microspeed.ModeSetpoint = Global.MicroSpeed.EModeSetpoint.Master
          End If
          ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
        End If

        'Set the Active Setpoint based on Master/Follower Mode as well as based on the Actively used Setpoint (1-4)
        If Microspeed.ModeSetpoint = Global.MicroSpeed.EModeSetpoint.Master Then
          'Read the active Master Setpoint
          If Microspeed.ActiveSetpoint = 4 Then
            'Read the Master Sepoint 4
            If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.MasterSetPoint4, setpoint, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
              Microspeed.UpdateActiveSetpointValue(setpoint, decimalLocation)
            End If
          ElseIf Microspeed.ActiveSetpoint = 3 Then
            'Read the Master Sepoint 3
            If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.MasterSetPoint3, setpoint, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
              Microspeed.UpdateActiveSetpointValue(setpoint, decimalLocation)
            End If
          ElseIf Microspeed.ActiveSetpoint = 2 Then
            'Read the Master Sepoint 2
            If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.MasterSetPoint2, setpoint, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
              Microspeed.UpdateActiveSetpointValue(setpoint, decimalLocation)
            End If
          Else
            'Read the Master Sepoint 1
            If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.MasterSetPoint1, setpoint, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
              Microspeed.UpdateActiveSetpointValue(setpoint, decimalLocation)
            End If
          End If
        Else
          'Read the active Follower Sepoint (Probably only used on the initial transport controllers > Padder, Overfeed, Selvage Left, & Selvage Right)
          If Microspeed.ActiveSetpoint = 4 Then
            'Read the Follower Setpoint 4
            If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.FollowerSetPoint4, setpoint, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
              Microspeed.UpdateActiveSetpointValue(setpoint, decimalLocation)
            End If
          ElseIf Microspeed.ActiveSetpoint = 3 Then
            'Read the Follower Setpoint 3
            If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.FollowerSetPoint3, setpoint, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
              Microspeed.UpdateActiveSetpointValue(setpoint, decimalLocation)
            End If
          ElseIf Microspeed.ActiveSetpoint = 2 Then
            'Read the Follower Setpoint 2
            If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.FollowerSetPoint2, setpoint, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
              Microspeed.UpdateActiveSetpointValue(setpoint, decimalLocation)
            End If
          Else
            'Read the Follower Setpoint 1
            If ComsMicroSpeed.ReadVariable(node, Ports.MicroSpeed196.Variable.FollowerSetPoint1, setpoint, decimalLocation) = Ports.MicroSpeed196.Result.OK Then
              Microspeed.UpdateActiveSetpointValue(setpoint, decimalLocation)
            End If
          End If
        End If

      End If

    Catch ex As Exception
      'Some Code
    End Try
    'Return response
  End Sub

  Private Sub WriteMicrospeeds(ByVal dout() As Boolean, ByVal anout() As Short, ByVal controlCode As ControlCode)

    'Useful variables
    Dim setpoint As Integer = 0
    Dim node As Integer = 0

    'Make sure the driver is initialized
    If ComsMicroSpeed Is Nothing Then Exit Sub

    'If we have been disabled then make sure we don't do any more coms until we're enabled
    If ComsMicroSpeed.Disabled Then Exit Sub

    'Don't run this code if we're in read mode
    If (MicrospeedComsState = EMicrospeedComsState.Read) OrElse (MicrospeedComsState = EMicrospeedComsState.ReadAfterWrite) Then Exit Sub


    '--------------------------------------------------------------------------------------------
    '  WIDTH SCREWS                           
    '--------------------------------------------------------------------------------------------
    For i As Integer = 1 To controlCode.Width_Screw.Length - 1
      With controlCode.Width_Screw(i)
        node = .Coms_Node
        If node = 0 Then
        Else
          If .SetpointStatus = MicroSpeed.EUpdateState.Request Then
            Select Case ComsMicroSpeed.ChangeSetPoint(node, .SetpointDesired, 0)
              Case Ports.MicroSpeed196.Result.OK
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ChangeSp", .SetpointDesired.ToString & ", 0", "OK")
                ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
              Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ChangeSp", .SetpointDesired.ToString & ", 0", "HwFault")
                ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
            End Select
          End If
        End If
      End With
    Next i


    '--------------------------------------------------------------------------------------------
    '  CONVEYOR                     
    '--------------------------------------------------------------------------------------------
#If 0 Then

    With controlCode.Conveyor
      node = .Coms_Node
      If node = 0 Then
      Else
        If .SetpointStatus = MicroSpeed.EUpdateState.Request Then

          If .DisplayValue > 10 Then
            'Currently Running (Use MasterSetpoint to update Variable 49, Then use Execute Master Setpoint to copy the sent value to active setpoint)
            If .ModeSetpoint = MicroSpeed.EModeSetpoint.Master Then
              Select Case ComsMicroSpeed.LoadMasterSetPoint(node, .SetpointDesired, .DecimalLocation)
                Case Ports.MicroSpeed196.Result.OK
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadMsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "OK")
                  ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
                Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadMsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "HwFault")
              End Select
            Else
              Select Case ComsMicroSpeed.LoadFollowerSetPoint(node, .SetpointDesired, .DecimalLocation)
                Case Ports.MicroSpeed196.Result.OK
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadFsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "OK")
                  ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
                Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadFsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "HwFault")
              End Select
            End If

          Else
            'Stopped (Use ChangeSetpoint to change active setpoint)
            Select Case ComsMicroSpeed.ChangeSetPoint(node, .SetpointDesired, .DecimalLocation)
              Case Ports.MicroSpeed196.Result.OK
                .SetpointStatus = MicroSpeed.EUpdateState.Verified 'Not necessary to first set to sent, then load and verify
                .Coms_UpdateLastWrite("ChangeSp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "OK")
                ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
              Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                .SetpointStatus = MicroSpeed.EUpdateState.Verified 'Not necessary to first set to sent, then load and verify
                .Coms_UpdateLastWrite("ChangeSp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "HwFault")
            End Select

          End If

        ElseIf .SetpointStatus = MicroSpeed.EUpdateState.Sent Then
          'We need to load the setpoint that we just sent
          If .ModeSetpoint = MicroSpeed.EModeSetpoint.Master Then
            Select Case ComsMicroSpeed.ExecuteLoadedMasterSetPoint(node, .ActiveSetpoint)
              Case Ports.MicroSpeed196.Result.OK
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddMsp", .ActiveSetpoint.ToString, "OK")
                ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
              Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddMsp", .ActiveSetpoint.ToString, "HwFault")
            End Select
          Else
            Select Case ComsMicroSpeed.ExecuteLoadedFollowerSetPoint(node, .ActiveSetpoint)
              Case Ports.MicroSpeed196.Result.OK
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddFsp", .ActiveSetpoint.ToString, "OK")
                ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
              Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddFsp", .ActiveSetpoint.ToString, "HwFault")
            End Select
          End If
        End If

      End If
    End With

#End If


    '--------------------------------------------------------------------------------------------
    '  STRIPPER                     
    '--------------------------------------------------------------------------------------------
#If 0 Then

    With controlCode.Stripper
      node = .Coms_Node
      If node = 0 Then
      Else
        If .SetpointStatus = MicroSpeed.EUpdateState.Request Then

          If .DisplayValue > 10 Then
            'Currently Running (Use MasterSetpoint to update Variable 49, Then use Execute Master Setpoint to copy the sent value to active setpoint)
            If .ModeSetpoint = MicroSpeed.EModeSetpoint.Master Then
              Select Case ComsMicroSpeed.LoadMasterSetPoint(node, .SetpointDesired, .DecimalLocation)
                Case Ports.MicroSpeed196.Result.OK
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadMsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "OK")
                  ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
                Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadMsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "HwFault")
              End Select
            Else
              Select Case ComsMicroSpeed.LoadFollowerSetPoint(node, .SetpointDesired, .DecimalLocation)
                Case Ports.MicroSpeed196.Result.OK
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadFsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "OK")
                  ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
                Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadFsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "HwFault")
              End Select
            End If

          Else
            'Stopped (Use ChangeSetpoint to change active setpoint)
            Select Case ComsMicroSpeed.ChangeSetPoint(node, .SetpointDesired, .DecimalLocation)
              Case Ports.MicroSpeed196.Result.OK
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ChangeSp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "OK")
                ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
              Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ChangeSp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "HwFault")
            End Select
          End If

        ElseIf .SetpointStatus = MicroSpeed.EUpdateState.Sent Then
          'We need to load the setpoint that we just sent
          If .ModeSetpoint = MicroSpeed.EModeSetpoint.Master Then
            Select Case ComsMicroSpeed.ExecuteLoadedMasterSetPoint(node, .ActiveSetpoint)
              Case Ports.MicroSpeed196.Result.OK
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddMsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "OK")
                ComsMicroSpeedTimer.TimeRemaining = MinMax(controlCode.Parameters.MicrospeedComsTime, 2, 30)
              Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddMsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "HwFault")
            End Select
          Else
            Select Case ComsMicroSpeed.ExecuteLoadedFollowerSetPoint(node, .ActiveSetpoint)
              Case Ports.MicroSpeed196.Result.OK
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddFsp", .ActiveSetpoint.ToString, "OK")
              Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddFsp", .ActiveSetpoint.ToString, "HwFault")
            End Select
          End If

        End If
      End If
    End With

#End If

#If 0 Then

    '--------------------------------------------------------------------------------------------
    '  FOLDER                     
    '--------------------------------------------------------------------------------------------
    With controlCode.Folder
      node = .Coms_Node
      If node = 0 Then
      Else
        If .SetpointStatus = MicroSpeed.EUpdateState.Request Then

          If .DisplayValue > 10 Then
            'Currently Running (Use MasterSetpoint to update Variable 49, Then use Execute Master Setpoint to copy the sent value to active setpoint)
            If .ModeSetpoint = MicroSpeed.EModeSetpoint.Master Then
              Select Case ComsMicroSpeed.LoadMasterSetPoint(node, .SetpointDesired, .DecimalLocation)
                Case Ports.MicroSpeed196.Result.OK
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadMsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "OK")
                Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadMsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "HwFault")
              End Select
            Else
              Select Case ComsMicroSpeed.LoadFollowerSetPoint(node, .SetpointDesired, .DecimalLocation)
                Case Ports.MicroSpeed196.Result.OK
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadFsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "OK")
                Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                  .SetpointStatus = MicroSpeed.EUpdateState.Sent
                  .Coms_UpdateLastWrite("LoadFsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "HwFault")
              End Select
            End If

          Else
            'Stopped (Use ChangeSetpoint to change active setpoint)
            Select Case ComsMicroSpeed.ChangeSetPoint(node, .SetpointDesired, .DecimalLocation)
              Case Ports.MicroSpeed196.Result.OK
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ChangeSp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "OK")
              Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ChangeSp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "HwFault")
            End Select
          End If

        ElseIf .SetpointStatus = MicroSpeed.EUpdateState.Sent Then
          'We need to load the setpoint that we just sent
          If .ModeSetpoint = MicroSpeed.EModeSetpoint.Master Then
            Select Case ComsMicroSpeed.ExecuteLoadedMasterSetPoint(node, .ActiveSetpoint)
              Case Ports.MicroSpeed196.Result.OK
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddMsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "OK")
              Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddMsp", .SetpointDesired.ToString & ", " & .DecimalLocation.ToString, "HwFault")
            End Select
          Else
            Select Case ComsMicroSpeed.ExecuteLoadedFollowerSetPoint(node, .ActiveSetpoint)
              Case Ports.MicroSpeed196.Result.OK
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddFsp", .ActiveSetpoint.ToString, "OK")
              Case Ports.MicroSpeed196.Result.Fault, Ports.MicroSpeed196.Result.HwFault
                .SetpointStatus = MicroSpeed.EUpdateState.Verified
                .Coms_UpdateLastWrite("ExeLoaddFsp", .ActiveSetpoint.ToString, "HwFault")
            End Select
          End If

        End If
      End If
    End With

#End If
  End Sub

#End Region


#Region " TERMINATOR - MODBUS TCP "

  ' SLOT0 - T1H-EBC100 - Ethernet Communications
  ' SLOT1 - T1F-16AD-2 - 8 Channel Analog Input                
  ' SLOT2 - T1F-08AD-2 - 8 Channel Analog Input                
  ' SLOT3 - T1F-16DA-2 - 16 Channel Analog Output 
  ' SLOT4 - T1F-08DA-2 - 8 Channel Analog ouput

  Private Function ReadPlcFans(ByVal parent As ACParent, ByVal dinp() As Boolean, ByVal aninp() As Short, ByVal temp() As Short, ByVal controlCode As ControlCode) As Boolean
    Try

      'Make sure the driver is initialized
      If ComsPlcFans Is Nothing Then Exit Function

      Dim returnResult As Boolean = False
      Dim valueSetAnalogInputs(48) As Short
      Select Case ComsPlcFans.Read(1, 30001, valueSetAnalogInputs)
        Case Ports.Modbus.Result.OK
          ComsPlcFansTimer.Seconds = controlCode.Parameters.PLCComsTime
          returnResult = True

          ComsPlcFansAninpRaw(1) = valueSetAnalogInputs(1)
          ComsPlcFansAninpRaw(2) = valueSetAnalogInputs(3)
          ComsPlcFansAninpRaw(3) = valueSetAnalogInputs(5)
          ComsPlcFansAninpRaw(4) = valueSetAnalogInputs(7)
          ComsPlcFansAninpRaw(5) = valueSetAnalogInputs(9)
          ComsPlcFansAninpRaw(6) = valueSetAnalogInputs(11)
          ComsPlcFansAninpRaw(7) = valueSetAnalogInputs(13)
          ComsPlcFansAninpRaw(8) = valueSetAnalogInputs(15)
          ComsPlcFansAninpRaw(9) = valueSetAnalogInputs(17)
          ComsPlcFansAninpRaw(10) = valueSetAnalogInputs(19)
          ComsPlcFansAninpRaw(11) = valueSetAnalogInputs(21)
          ComsPlcFansAninpRaw(12) = valueSetAnalogInputs(23)
          ComsPlcFansAninpRaw(13) = valueSetAnalogInputs(25)
          ComsPlcFansAninpRaw(14) = valueSetAnalogInputs(27)
          ComsPlcFansAninpRaw(15) = valueSetAnalogInputs(29)
          ComsPlcFansAninpRaw(16) = valueSetAnalogInputs(31)
          ComsPlcFansAninpRaw(17) = valueSetAnalogInputs(33)
          ComsPlcFansAninpRaw(18) = valueSetAnalogInputs(35)
          ComsPlcFansAninpRaw(19) = valueSetAnalogInputs(37)
          ComsPlcFansAninpRaw(20) = valueSetAnalogInputs(39)
          ComsPlcFansAninpRaw(21) = valueSetAnalogInputs(41)
          ComsPlcFansAninpRaw(22) = valueSetAnalogInputs(42)
          ComsPlcFansAninpRaw(23) = valueSetAnalogInputs(43)
          ComsPlcFansAninpRaw(24) = valueSetAnalogInputs(45)

          ' Rescale Raw Inputs: 0-8191 = 0-1000
          For i = 1 To 24
            aninp(i + 100) = ReScale(ComsPlcFansAninpRaw(i), 0, 8191, 0, 1000)
          Next i

          ' Update Controllers
          For i = 1 To controlCode.FanTop_Speed.Length - 1
            With controlCode.FanTop_Speed(i)
              .Plc_SpeedActual = aninp(100 + i)
            End With
          Next i
          For i = 1 To controlCode.FanBottom_Speed.Length - 1
            With controlCode.FanBottom_Speed(i)
              .Plc_SpeedActual = aninp(108 + i)
            End With
          Next i
          For i = 1 To controlCode.FanExhaust_Speed.Length - 1
            With controlCode.FanExhaust_Speed(i)
              .Plc_SpeedActual = aninp(116 + i)
            End With
          Next i
          ' Fan Pollution TODO?

      End Select


      ' READ SETPOINTS 
      ' ANALOG OUTPUTS BEGIN AT HOLDING REGISTERS: 40001
      Dim valueSetAnalogOutputs(48) As Short
      Select Case ComsPlcFans.Read(1, 40001, valueSetAnalogOutputs)
        Case Ports.Modbus.Result.OK
          ComsPlcFansTimer.Seconds = controlCode.Parameters.PLCComsTime
          returnResult = True

          ComsPlcFansAnoutRaw(1) = valueSetAnalogOutputs(1)
          ComsPlcFansAnoutRaw(2) = valueSetAnalogOutputs(3)
          ComsPlcFansAnoutRaw(3) = valueSetAnalogOutputs(5)
          ComsPlcFansAnoutRaw(4) = valueSetAnalogOutputs(7)
          ComsPlcFansAnoutRaw(5) = valueSetAnalogOutputs(9)
          ComsPlcFansAnoutRaw(6) = valueSetAnalogOutputs(11)
          ComsPlcFansAnoutRaw(7) = valueSetAnalogOutputs(13)
          ComsPlcFansAnoutRaw(8) = valueSetAnalogOutputs(15)
          ComsPlcFansAnoutRaw(9) = valueSetAnalogOutputs(17)
          ComsPlcFansAnoutRaw(10) = valueSetAnalogOutputs(19)
          ComsPlcFansAnoutRaw(11) = valueSetAnalogOutputs(21)
          ComsPlcFansAnoutRaw(12) = valueSetAnalogOutputs(23)
          ComsPlcFansAnoutRaw(13) = valueSetAnalogOutputs(25)
          ComsPlcFansAnoutRaw(14) = valueSetAnalogOutputs(27)
          ComsPlcFansAnoutRaw(15) = valueSetAnalogOutputs(29)
          ComsPlcFansAnoutRaw(16) = valueSetAnalogOutputs(31)
          ComsPlcFansAnoutRaw(17) = valueSetAnalogOutputs(33)
          ComsPlcFansAnoutRaw(18) = valueSetAnalogOutputs(35)
          ComsPlcFansAnoutRaw(19) = valueSetAnalogOutputs(37)
          ComsPlcFansAnoutRaw(20) = valueSetAnalogOutputs(39)
          ComsPlcFansAnoutRaw(21) = valueSetAnalogOutputs(41)
          ComsPlcFansAnoutRaw(22) = valueSetAnalogOutputs(42)
          ComsPlcFansAnoutRaw(23) = valueSetAnalogOutputs(43)
          ComsPlcFansAnoutRaw(24) = valueSetAnalogOutputs(45)


          ' Rescale Raw Outputs: 0-8191 = 0-1000
          For i = 1 To 24
            aninp(i + 130) = ReScale(ComsPlcFansAnoutRaw(i), 0, 4095, 0, 1000)
          Next i

          ' Update Controllers
          For i = 1 To controlCode.FanTop_Speed.Length - 1
            With controlCode.FanTop_Speed(i)
              .Plc_SetpointActual = aninp(130 + i)
            End With
          Next i
          For i = 1 To controlCode.FanBottom_Speed.Length - 1
            With controlCode.FanBottom_Speed(i)
              .Plc_SetpointActual = aninp(138 + i)
            End With
          Next i
          For i = 1 To controlCode.FanExhaust_Speed.Length - 1
            With controlCode.FanExhaust_Speed(i)
              .Plc_SetpointActual = aninp(146 + i)
            End With
          Next i
          ' Fan Pollution TODO?


      End Select


      Return returnResult
    Catch ex As Exception
      Dim message As String = ex.Message
    End Try
  End Function

  Private Sub WritePlcFans(ByVal dout() As Boolean, ByVal anout() As Short, ByVal controlCode As ControlCode)

    'Make sure the driver is initialized
    If ComsPlcFans Is Nothing Then Exit Sub

    '------------------------------------------------------------------------------------------------------------------
    ' Fan Speeds Top
    '------------------------------------------------------------------------------------------------------------------
    For i As Integer = 1 To controlCode.FanTop_Speed.Length - 1
      With controlCode.FanTop_Speed(i)
        If .SetpointStatus = FanSpeedControl.EState.Request Then
          Select Case ComsPlcFans.Write(1, 40001 + (.Plc_SetpointRegister - 1), {CShort(0), CShort(.SetpointDesiredPlc)}, Ports.WriteMode.Always)
            Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
              .SetpointStatusClear()
              .Coms_WriteLastUpdate(.SetpointDesired, "Success")
          End Select
        End If
      End With
    Next i

    '------------------------------------------------------------------------------------------------------------------
    ' Fan Speeds Bottom
    '------------------------------------------------------------------------------------------------------------------
    For i As Integer = 1 To controlCode.FanBottom_Speed.Length - 1
      With controlCode.FanBottom_Speed(i)
        If .SetpointStatus = FanSpeedControl.EState.Request Then
          Select Case ComsPlcFans.Write(1, 40001 + (.Plc_SetpointRegister - 1), {CShort(0), CShort(.SetpointDesiredPlc)}, Ports.WriteMode.Always)
            Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
              .SetpointStatusClear()
              .Coms_WriteLastUpdate(.SetpointDesired, "Success")
          End Select
        End If
      End With
    Next i


    '------------------------------------------------------------------------------------------------------------------
    ' Fan Speeds Exaust
    '------------------------------------------------------------------------------------------------------------------
    For i As Integer = 1 To controlCode.FanExhaust_Speed.Length - 1
      With controlCode.FanExhaust_Speed(i)
        If .SetpointStatus = FanSpeedControl.EState.Request Then
          Select Case ComsPlcFans.Write(1, 40001 + (.Plc_SetpointRegister - 1), {CShort(0), CShort(.SetpointDesiredPlc)}, Ports.WriteMode.Always)
            Case Ports.Modbus.Result.OK, Ports.Modbus.Result.Fault, Ports.Modbus.Result.HwFault
              .SetpointStatusClear()
              .Coms_WriteLastUpdate(.SetpointDesired, "Success")
          End Select
        End If
      End With
    Next i

  End Sub

#End Region

  Public Sub ClearOutputs()
    'Set all Outputs = false/0 here to reset conditions
  End Sub

  Protected Overrides Sub Finalize()
    MyBase.Finalize()
  End Sub

End Class
