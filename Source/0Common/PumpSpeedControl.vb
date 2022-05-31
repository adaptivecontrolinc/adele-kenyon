' Version 1.1 [2022-03-17] DH
'  Add Expert user function to change setpoints outside the procedure deviation
' Version 1.0.0 [2021-01-30] DH
'   Jason's Smarty hardware to replace Microspeeds for pump extractor control with Percent units
'   Setpoint and Feedback Registers are not linear order.  Use Jason's provided worksheet to define properties with case feedback based on zone number to ease read/write section.

Public Class PumpSpeedControl
  Inherits MarshalByRefObject
  Implements IController

  'Keep a local reference to the control code object for convenience
  Private ReadOnly controlCode As ControlCode
  Public Sub New(ByVal controlCode As ControlCode)
    Me.controlCode = controlCode
  End Sub

  Public Enum EUpdateState
    Idle
    Request
    Sent            'Coms ok response from Write method
    Verified        'desired setpoint read from device corresponds to what we wrote
    Offline
  End Enum

  Public Enum EModeState
    Manual
    Automatic
  End Enum

  Public Enum EOperationStatus
    EStop
    Idle
  End Enum

#Region " PARAMETERS "

  Private prm_AutoEnable_ As Integer
  Public Property Prm_AutoEnable As Integer
    Get
      Return prm_AutoEnable_
    End Get
    Set(value As Integer)
      prm_AutoEnable_ = value
    End Set
  End Property

  Private prm_SetpointMin_ As Integer
  Public Property Prm_SetpointMin() As Integer
    Get
      Return prm_SetpointMin_
    End Get
    Set(ByVal value As Integer)
      prm_SetpointMin_ = value
    End Set
  End Property

  Private prm_SetpointMax_ As Integer
  Public Property Prm_SetpointMax() As Integer
    Get
      Return prm_SetpointMax_
    End Get
    Set(ByVal value As Integer)
      prm_SetpointMax_ = value
    End Set
  End Property

  Private prm_Deviance_ As Integer
  Public Property Prm_Deviance() As Integer
    Get
      Return prm_Deviance_
    End Get
    Set(ByVal value As Integer)
      prm_Deviance_ = value
    End Set
  End Property

  Private cmd_Deviance_ As Integer
  Public Property Cmd_Deviance() As Integer
    Get
      Return cmd_Deviance_
    End Get
    Set(ByVal value As Integer)
      cmd_Deviance_ = value
    End Set
  End Property

#End Region

#Region " PLC RAW VARIABLE VALUES - FILLED IN DURING I/O READS "

  Private plc_SetpointRegister_ As Integer = 0
  Public Property Plc_SetpointRegister As Integer
    Get
      Return plc_SetpointRegister_
    End Get
    Set(value As Integer)
      plc_SetpointRegister_ = value
    End Set
  End Property

  Private plc_SetpointActualRaw_ As Integer
  Public Property Plc_SetpointActualRaw As Integer
    Get
      Return plc_SetpointActualRaw_
    End Get
    Set(value As Integer)
      plc_SetpointActualRaw_ = value
      ' Update working setpoint value 
      Plc_SetpointActual = CInt(value / 10)
    End Set
  End Property

  Private plc_SetpointActual_ As Integer
  Public Property Plc_SetpointActual As Integer
    Get
      Return plc_SetpointActual_
    End Get
    Set(value As Integer)
      plc_SetpointActual_ = value

      'Verify Setpoint - if we successfully read the setpoint then it's the new setpoint
      If setpointStatus_ = EUpdateState.Sent Then
        setpointStatus_ = EUpdateState.Verified
        setpointDesired_ = value
      ElseIf setpointStatus_ = EUpdateState.Verified Then
        'No Setpoint Change Requested (Idle), if it's different, it must have been manual changed
        If value <> setpointDesired_ Then
          setpointDesired_ = value
        End If
      ElseIf (setpointStatus_ = EUpdateState.Idle) Then
        'No Setpoint Change Requested (Idle), if it's different, it must have been manual changed
        If value <> setpointDesired_ Then
          setpointDesired_ = value
        End If
      ElseIf (setpointStatus_ = EUpdateState.Offline) Then
        setpointStatus_ = EUpdateState.Idle
      End If

    End Set
  End Property

  Private plc_SpeedRegister_ As Integer = 0
  Public Property Plc_SpeedRegister As Integer
    Get
      Return plc_SpeedRegister_
    End Get
    Set(value As Integer)
      plc_SpeedRegister_ = value
    End Set
  End Property

  Private plc_SpeedActualRaw_ As Integer
  Public Property Plc_SpeedActualRaw As Integer         ' Percents are in hunderds 000.00 %
    Get
      Return plc_SpeedActualRaw_
    End Get
    Set(value As Integer)
      plc_SpeedActualRaw_ = value
      ' Update working setpoint value 
      plc_SpeedActual_ = CInt(value / 10)
    End Set
  End Property

  Private plc_SpeedActual_ As Integer
  Public Property Plc_SpeedActual As Integer
    Get
      Return plc_SpeedActual_
    End Get
    Set(value As Integer)
      plc_SpeedActual_ = value
    End Set
  End Property

  ' Monitoring Smarty Range parameter with SM100 application used internally to drive analog output and scale feedback
  Private plc_SpeedRange_ As Integer
  Public Property Plc_SpeedRange As Integer
    Get
      Return plc_SpeedRange_
    End Get
    Set(value As Integer)
      plc_SpeedRange_ = value
    End Set
  End Property

  Private plc_Torque_ As Integer
  Public Property Plc_Torque As Integer
    Get
      Return plc_Torque_
    End Get
    Set(value As Integer)
      plc_Torque_ = value
    End Set
  End Property

  Private plc_Amps_ As Integer
  Public Property Plc_Amps As Integer
    Get
      Return plc_Amps_
    End Get
    Set(value As Integer)
      plc_Amps_ = value
    End Set
  End Property

  Private plc_FaultStatus_ As Integer
  Public Property Plc_FaultStatus As Integer
    Get
      Return plc_FaultStatus_
    End Get
    Set(value As Integer)
      plc_FaultStatus_ = value
    End Set
  End Property

  Public ReadOnly Property Plc_FaultIsError As Boolean
    Get
      Return Plc_FaultStatus > 0
    End Get
  End Property
  Public ReadOnly Property Plc_FaultMessage As String
    Get
      Select Case Plc_FaultStatus
        Case 0 : Return ""
        Case 1 : Return "Brake Overcurrent"
        Case 2 : Return "Brake Overload"
        Case 3 : Return "Output Overcurrent(O - I)"
        Case 4 : Return "Output Overload"
        Case 5 : Return "Output Overcurrent (P5)"
        Case 6 : Return "DC Bus Overvoltage"
        Case 7 : Return "DC Bus Undervoltage"
        Case 8 : Return "Heatsink Overtemperature"
        Case 9 : Return "Undertemperature"
        Case 10 : Return "Factory Default"
        Case 11 : Return "External Trip"
        Case 12 : Return "Communications Fault"
        Case 13 : Return "Excessive DC Ripple"
        Case 14 : Return "Input Phase Loss"
        Case 15 : Return "Output Overcurrent (h O-I)"
        Case 16 : Return "Heatsink Thermistor Fault"
        Case 17 : Return "Memory Fault (F)"
        Case 18 : Return "4-20mA Signal Lost"
        Case 19 : Return "Memory Fault (E)"
        Case 20 : Return "User Defaults"
        Case 21 : Return "Motor PTC Overtemperature"
        Case 22 : Return "Cooling Fan Fault"
        Case 23 : Return "Ambient Temperature High"
        Case 24 : Return "Torque Limit Exceeded"
        Case 25 : Return "Torque Too Low"
        Case 26 : Return "Drive Output Fault"
      ' Case 27 N/A
      ' Case 28 N/A
      ' Case 29 N/A
        Case 30 : Return "Encoder Feedback Fault"
        Case 31 : Return "Encoder Speed Error"
        Case 32 : Return "Incorrect Encoder PPR"
        Case 33 : Return "Encoder Channel A Fault"
        Case 34 : Return "Encoder Channel B Fault"
        Case 35 : Return "Encoder Channel A & B Fault"
        Case 36 : Return "RS 485 Channel Error"
        Case 37 : Return "IO Comms Loss"
        Case 38 : Return "Wrong Type Encoder"
        Case 39 : Return "KTY Trip"
        Case 40 : Return "Autotune Failed (01)"
        Case 41 : Return "Autotune Failed (02)"
        Case 40 : Return "Autotune Failed (03)"
        Case 43 : Return "Autotune Failed (04)"
        Case 44 : Return "Autotune Failed (05)"
        Case 45 : Return "Phase Sequence Error"
      ' Case 46 N/A
      ' Case 47 N/A
      ' Case 48 N/A
        Case 49 : Return "Output Phase Loss"
        Case 50 : Return "Modbus Comms Fault"
        Case 51 : Return "CANopen Comms Trip"
        Case 52 : Return "Comms Option Module Fault"
        Case 53 : Return "IO Card Comms Trip"
      End Select
      Return ""
    End Get
  End Property

  ' Boolean Flags
  Private plc_StartRequest_ As Boolean
  Public Property Plc_StartRequest As Boolean
    Get
      Return Plc_StartRequest
    End Get
    Set(value As Boolean)
      plc_StartRequest_ = value
    End Set
  End Property

  Private plc_Running_ As Boolean
  Public Property Plc_Running As Boolean
    Get
      Return plc_Running_
    End Get
    Set(value As Boolean)
      plc_Running_ = value
    End Set
  End Property

  ' hardware limits for upper & lower setpoints
  Private plc_LimitLower_ As Integer
  Public Property Plc_LimitLower As Integer
    Get
      Return plc_LimitLower_
    End Get
    Set(value As Integer)
      plc_LimitLower_ = value
    End Set
  End Property

  Private plc_LimitUpper_ As Integer
  Public Property Plc_LimitUpper As Integer
    Get
      Return plc_LimitUpper_
    End Get
    Set(value As Integer)
      plc_LimitUpper_ = value
    End Set
  End Property

#End Region


#Region " SETPOINT CONTROL "

  Private setpointDesired_ As Integer              'Setpoint set by program to be written to device
  Public Property SetpointDesired() As Integer
    Get
      Return setpointDesired_
    End Get
    Set(ByVal value As Integer)
      setpointDesired_ = value
    End Set
  End Property

  Public ReadOnly Property SetpointDesiredPlc As Integer
    Get
      Return SetpointDesired * 10
    End Get
  End Property

  Private SetpointActual_ As Integer
  Public Property SetpointActual As Integer
    Get
      Return SetpointActual_
    End Get
    Set(value As Integer)
      SetpointActual_ = value
      'Not requesting a new setpoint, update setpoint desired with remove value (may have changed elsewhere)
      If setpointStatus_ <> EUpdateState.Request Then setpointDesired_ = SetpointActual_
    End Set
  End Property

  Public ReadOnly Property SetpointStatusDisplay() As String
    Get
      If SetpointStatus = EUpdateState.Offline Then
        Return "Offline"
      ElseIf SetpointStatus = EUpdateState.Verified Then
        Return "Verified"
      ElseIf SetpointStatus = EUpdateState.Sent Then
        Return "Sent"
      ElseIf SetpointStatus = EUpdateState.Request Then
        Return "Requested"
      Else
        Return "Idle"
      End If
    End Get
  End Property

  Private setpointStatus_ As EUpdateState
  Public Property SetpointStatus() As EUpdateState
    Get
      Return setpointStatus_
    End Get
    Set(value As EUpdateState)
      'v1.2.3 [2013-03-01] - delay interval for working w/ tenter chain timout
      If value = EUpdateState.Request Then setpointRequestLast_ = Date.UtcNow
      If value = EUpdateState.Verified Then
        adjustResult_ = EControllerAdjustResult.OK
        adjustString_ = ""
      End If
      setpointStatus_ = value
    End Set
  End Property

  Friend ReadOnly Property SetpointComplete As Boolean
    Get
      Select Case SetpointStatus
        Case EUpdateState.Idle, EUpdateState.Sent, EUpdateState.Verified, EUpdateState.Offline
          Return True
        Case EUpdateState.Request
          Return False
      End Select
    End Get
  End Property

  Public Sub SetpointStatusClear()
    'Clear the Setpoint Request
    setpointStatus_ = EUpdateState.Sent
  End Sub

  Private setpointLimitUpper_ As Integer
  Public Property SetpointLimitUpper As Integer
    Get
      Return setpointLimitUpper_
    End Get
    Set(value As Integer)
      setpointLimitUpper_ = MinMax(value, Prm_SetpointMin + 100, Prm_SetpointMax)
    End Set
  End Property

  Private setpointLimitLower_ As Integer
  Public Property SetpointLimitLower As Integer
    Get
      Return setpointLimitLower_
    End Get
    Set(value As Integer)
      setpointLimitLower_ = MinMax(value, Prm_SetpointMin, Prm_SetpointMax)
    End Set
  End Property

  Private setpointDevianceHigh_ As Integer
  Public Property SetpointDevianceHigh() As Integer
    Get
      Return setpointDevianceHigh_
    End Get
    Set(ByVal value As Integer)
      setpointDevianceHigh_ = MinMax(value, prm_SetpointMin_, prm_SetpointMax_)
    End Set
  End Property

  Private setpointDevianceLow_ As Integer
  Public Property SetpointDevianceLow() As Integer
    Get
      Return setpointDevianceLow_
    End Get
    Set(ByVal value As Integer)
      setpointDevianceLow_ = MinMax(value, prm_SetpointMin_, prm_SetpointMax_)
    End Set
  End Property

  Private setpointRequestLast_ As Date
  Public Property SetpointRequestLast As Date
    Get
      Return setpointRequestLast_
    End Get
    Set(value As Date)
      setpointRequestLast_ = value
    End Set
  End Property

#End Region

#Region " COMMUNICATIONS "

  Private scanLast_ As Date
  Public Property Coms_ScanLast() As Date
    Get
      Return scanLast_
    End Get
    Set(ByVal value As Date)
      commsTimer_.TimeRemaining = 60
      coms_ScanInterval_ = CInt((value - Coms_ScanLast).TotalMilliseconds)
      scanLast_ = value

      If (setpointStatus_ = EUpdateState.Offline) Then setpointStatus_ = EUpdateState.Idle
    End Set
  End Property

  Private coms_ScanInterval_ As Integer
  Public ReadOnly Property Coms_ScanInterval() As Integer
    Get
      Return coms_ScanInterval_
    End Get
  End Property

  Private commsTimer_ As New Timer
  Public ReadOnly Property Coms_Timeout() As Boolean
    Get
      If commsTimer_.Finished Then
        ClearValues()
        Return True
      Else
        Return False
      End If
    End Get
  End Property

  Public Sub ClearValues()
    ' TODO - do we clear this...
    '    plc_SpeedActual_ = 0
    '    plc_SetpointActual_ = 0
  End Sub

  Public Sub Coms_WriteLastUpdate(ByVal setpoint As Integer, result As String)
    ' Update local variables for history logging
    coms_WriteLastValue_ = setpoint
    coms_WriteLastResult_ = result

    ' Update last coms write log
    commsTimer_.TimeRemaining = 60
    If Coms_WriteLast > Date.MinValue Then
      ' Write Last variable has been set, calculate and update the interval since then
      coms_WriteInterval_ = CInt((Date.UtcNow - Coms_WriteLast).Milliseconds)
      coms_WriteLast_ = Date.UtcNow
    Else : coms_WriteLast_ = Date.UtcNow
    End If
  End Sub

  Private coms_WriteLast_ As Date
  Public ReadOnly Property Coms_WriteLast As Date
    Get
      Return coms_WriteLast_
    End Get
  End Property

  Private coms_WriteInterval_ As Integer
  Public ReadOnly Property Coms_WriteInterval As Integer
    Get
      Return coms_WriteInterval_
    End Get
  End Property

  Private coms_WriteLastValue_ As Integer
  Public ReadOnly Property Coms_WriteLastValue As Integer
    Get
      Return coms_WriteLastValue_
    End Get
  End Property

  Private coms_WriteLastResult_ As String
  Public ReadOnly Property Coms_WriteLastResult As String
    Get
      Return coms_WriteLastResult_
    End Get
  End Property

#End Region

#Region " INTERFACES "

  Private description_ As String
  Public Property Description() As String Implements IController.Description
    Get
      Return description_
    End Get
    Set(ByVal value As String)
      description_ = value
    End Set
  End Property

  Public ReadOnly Property DisplayActual As String Implements IController.Actual
    Get
      Return (plc_SpeedActual_ / 10).ToString & UnitsDisplay
    End Get
  End Property

  Public ReadOnly Property DisplaySetpoint As String Implements IController.Setpoint
    Get
      Return (setpointDesired_ / 10).ToString & UnitsSetpoint
    End Get
  End Property

  Public ReadOnly Property DisplayStatus As String Implements IController.Status
    Get
      Dim response As String = "Idle"

      If (Not Coms_Timeout) AndAlso Plc_StartRequest Then
        If Not Plc_Running Then
          response = "Starting"
        Else : response = "On"
        End If
      ElseIf (Not Coms_Timeout) AndAlso Not Plc_StartRequest Then
        If Plc_Running Then
          response = "Stopping"
        Else : response = "Off"
        End If
      Else
        Select Case SetpointStatus
          Case EUpdateState.Offline
            response = "Offline"
          Case EUpdateState.Request
            response = "Requested"
          Case EUpdateState.Sent
            response = "Sent"
          Case EUpdateState.Idle
            response = "Idle"
        End Select
      End If

      Return response
    End Get
  End Property

  Public ReadOnly Property SetpointChangeEnabled As Boolean Implements IController.ChangeSetpointEnabled
    Get
      Return (Prm_AutoEnable = 1)
    End Get
  End Property

  Public ReadOnly Property SetpointCurrent As Integer Implements IController.SetpointCurrent
    Get
      Return SetpointDesired
    End Get
  End Property

  'PLC Values Decimal 0-999 represent 0-999%.  115 represents 115% or 1.15
  Public Enum EDecimalPosition
    None = 1        '999 = 999
    One = 10        '999 = 99.9
    Two = 100       '999 = 9.99
  End Enum
  Private decimalPosition_ As EDecimalPosition = EDecimalPosition.Two
  Public ReadOnly Property ISetpointFactor As Integer Implements IController.SetpointFactor
    Get
      Return decimalPosition_
    End Get
  End Property

  Public ReadOnly Property SetpointMaximum As Integer Implements IController.SetpointMaximum
    Get
      If (Plc_LimitUpper > 0) AndAlso (Plc_LimitUpper < Prm_SetpointMax) Then
        Return Plc_LimitUpper
      Else
        Return Prm_SetpointMax
      End If
    End Get
  End Property

  Public ReadOnly Property SetpointMinimum As Integer Implements IController.SetpointMinimum
    Get
      If (Plc_LimitLower > 0) AndAlso (Plc_LimitLower < Prm_SetpointMin) Then
        Return Plc_LimitLower
      Else
        Return Prm_SetpointMin
      End If
    End Get
  End Property

  Private ReadOnly Property IUnits As String Implements IController.Units
    Get
      Return unitsSetpoint_
    End Get
  End Property

  Private unitsDisplay_ As String
  Public Property UnitsDisplay() As String
    Get
      Return unitsDisplay_
    End Get
    Set(ByVal value As String)
      unitsDisplay_ = value
    End Set
  End Property

  Private unitsSetpoint_ As String = ""
  Friend Property UnitsSetpoint As String
    Get
      Return unitsSetpoint_
    End Get
    Set(value As String)
      unitsSetpoint_ = value
    End Set
  End Property


  Private adjustResult_ As EControllerAdjustResult
  Public ReadOnly Property IAdjustResult As EControllerAdjustResult Implements IController.AdjustResult
    Get
      Return adjustResult_
    End Get
  End Property

  Private adjustString_ As String
  Public ReadOnly Property IAdjustString As String Implements IController.AdjustString
    Get
      Return adjustString_
    End Get
  End Property

  Public ReadOnly Property OutputPercent As Integer Implements IController.OutputPercent
    Get
      Return 0
    End Get
  End Property

  Private zone_ As Integer
  Public Property Zone() As Integer Implements IController.Zone
    Get
      Return zone_
    End Get
    Set(ByVal value As Integer)
      zone_ = value
    End Set
  End Property

  ' TODO
  Public Function IChangeSetpoint(setpoint As Integer, expert As Boolean) As Boolean Implements IController.IChangeSetpoint
    Try

      Dim newSetpoint As Integer = setpoint
      'Test to make sure that new setpoint remains within the deviance limits and max/min values

      If (newSetpoint >= SetpointMinimum) AndAlso (newSetpoint <= SetpointMaximum) Then
        setpointDesired_ = newSetpoint
        setpointStatus_ = EUpdateState.Request
        Return True
      Else
        'Outside of limits (which limit?)
        If newSetpoint < SetpointMinimum Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ &
                          " Below Lower Setpoint Limit: " & (SetpointMinimum / 10).ToString & unitsSetpoint_

        ElseIf newSetpoint < SetpointLimitLower Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ &
                          " Below Lower Deviance Limit: " & (SetpointLimitLower / 10).ToString & unitsSetpoint_

        ElseIf newSetpoint > SetpointLimitUpper Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ &
                          " Exceeds Upper Deviance Limit: " & (SetpointLimitUpper / 10).ToString & unitsSetpoint_

        ElseIf newSetpoint > SetpointMaximum Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ &
                          " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & unitsSetpoint_

        End If
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  ' TODO
  Public Function Increase(increment As Integer, expert As Boolean) As Boolean Implements IController.Increase
    Try
      Dim newSetpoint As Integer = SetpointDesired + increment
      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If newSetpoint < SetpointMaximum Then
        If newSetpoint < SetpointLimitUpper Then
          setpointDesired_ += increment
          setpointStatus_ = EUpdateState.Request
          Return True
        Else
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ &
                          " Exceeds Upper Deviance Limit: " & (SetpointLimitUpper / 10).ToString & unitsSetpoint_
        End If
      Else
        adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
        adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ &
                          " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & unitsSetpoint_
      End If

    Catch ex As Exception
      'log error
    End Try
    Return False
  End Function

  ' TODO
  Public Function Decrease(increment As Integer, expert As Boolean) As Boolean Implements IController.Decrease
    Try
      Dim newSetpoint As Integer = SetpointDesired - increment
      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If newSetpoint > SetpointMinimum Then
        If newSetpoint > SetpointLimitLower Then
          setpointDesired_ -= increment
          setpointStatus_ = EUpdateState.Request
          Return True
        Else
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ &
                          " Below Lower Deviance Limit: " & (SetpointLimitLower / 10).ToString & unitsSetpoint_
        End If
      Else
        adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
        adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ &
                          " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & unitsSetpoint_
      End If

    Catch ex As Exception
      'log error
    End Try
    Return False
  End Function


#End Region

End Class
