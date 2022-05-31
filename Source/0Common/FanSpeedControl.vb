' Version 1.1 [2022-03-22] DH
'  Add Expert user function to change setpoints outside the procedure deviation
' Version 1.0.0 [2022-03-09] DH
' TERMINATOR PLC I/O 

Public Class FanSpeedControl : Inherits MarshalByRefObject : Implements IController
  Private ReadOnly controlCode As ControlCode

  Property Anout As Integer

  Property CurrentValue As Integer
  Property TargetValue As Integer

  Property WriteEnabled As Boolean = False
  Property WriteAlways As Boolean = False

  Property LastUpdate As Date
  Property LastWrite As Date

  Public Sub New(ByVal controlCode As ControlCode)
    Me.controlCode = controlCode
  End Sub

  Public Enum EState
    Idle
    Request
    Sent            'Coms ok response from Write method
    Verified        'desired setpoint read from device corresponds to what we wrote
    Offline

    ' ML's Polartec Kenyon
    '    Write            ' Write required
    '    WriteSuccess     ' Write succeeded
    '    WriteError       ' Write error
    '    NoChange         ' Current value matches target - no write requiredOffline
  End Enum
  Property State As EState
  Property Timer As New Timer With {.Seconds = 32}

#Region " PARAMETERS "

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
      Plc_SetpointActual = value * 10
    End Set
  End Property

  Private plc_SetpointActual_ As Integer
  Public Property Plc_SetpointActual As Integer
    Get
      Return plc_SetpointActual_
    End Get
    Set(value As Integer)
      plc_SetpointActual_ = value
      Coms_ScanLast = CurrentTime

      'Verify Setpoint - if we successfully read the setpoint then it's the new setpoint
      If setpointStatus_ = EState.Sent Then
        setpointStatus_ = EState.Verified
        setpointDesired_ = value
      ElseIf setpointStatus_ = EState.Verified Then
        'No Setpoint Change Requested (Idle), if it's different, it must have been manual changed
        If value <> setpointDesired_ Then
          setpointDesired_ = value
        End If
      ElseIf (setpointStatus_ = EState.Idle) Then
        'No Setpoint Change Requested (Idle), if it's different, it must have been manual changed
        If value <> setpointDesired_ Then
          setpointDesired_ = value
        End If
      ElseIf (setpointStatus_ = EState.Offline) Then
        setpointStatus_ = EState.Idle
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
  Public Property Plc_SpeedActualRaw As Integer
    Get
      Return plc_SpeedActualRaw_
    End Get
    Set(value As Integer)
      plc_SpeedActualRaw_ = value
      ' Update working setpoint value 
      plc_SpeedActual_ = value * 10
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
      'Return CInt(SetpointDesired / 10)
      Return CInt(MulDiv(SetpointDesired, 4095, 1000))
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
      If setpointStatus_ <> EState.Request Then setpointDesired_ = SetpointActual_
    End Set
  End Property

  Public ReadOnly Property SetpointStatusDisplay() As String
    Get
      If SetpointStatus = EState.Offline Then
        Return "Offline"
      ElseIf SetpointStatus = EState.Verified Then
        Return "Verified"
      ElseIf SetpointStatus = EState.Sent Then
        Return "Sent"
      ElseIf SetpointStatus = EState.Request Then
        Return "Requested"
      Else
        Return "Idle"
      End If
    End Get
  End Property

  Private setpointStatus_ As EState
  Public Property SetpointStatus() As EState
    Get
      Return setpointStatus_
    End Get
    Set(value As EState)
      'v1.2.3 [2013-03-01] - delay interval for working timout
      If value = EState.Request Then setpointRequestLast_ = CurrentTime
      If value = EState.Verified Then
        adjustResult_ = EControllerAdjustResult.OK
        adjustString_ = ""
      End If
      setpointStatus_ = value
    End Set
  End Property

  Friend ReadOnly Property SetpointComplete As Boolean
    Get
      Select Case SetpointStatus
        Case EState.Idle, EState.Sent, EState.Verified, EState.Offline
          Return True
        Case EState.Request
          Return False
      End Select
    End Get
  End Property

  Public Sub SetpointStatusClear()
    'Clear the Setpoint Request
    setpointStatus_ = EState.Sent
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

  ReadOnly Property IsSetpointLow As Boolean
    Get
      Return SetpointActual < SetpointLimitLower
    End Get
  End Property
  ReadOnly Property IsSetpointHigh As Boolean
    Get
      Return SetpointActual > SetpointLimitLower
    End Get
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

      If (setpointStatus_ = EState.Offline) Then setpointStatus_ = EState.Idle
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
      coms_WriteInterval_ = CInt((CurrentTime - Coms_WriteLast).Milliseconds)
      coms_WriteLast_ = CurrentTime
    Else : coms_WriteLast_ = CurrentTime
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
          Case EState.Offline
            response = "Offline"
          Case EState.Request
            response = "Requested"
          Case EState.Sent
            response = "Sent"
          Case EState.Idle
            response = "Idle"
        End Select
      End If

      Return response
    End Get
  End Property

  Public ReadOnly Property SetpointChangeEnabled As Boolean Implements IController.ChangeSetpointEnabled
    Get
      Return WriteEnabled
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
  Private decimalPosition_ As EDecimalPosition = EDecimalPosition.One
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

  Public Function IChangeSetpoint(setpoint As Integer, expert As Boolean) As Boolean Implements IController.IChangeSetpoint
    Try
      Dim newSetpoint As Integer = setpoint
      ' Expert must remain within setpoint limits
      If expert Then
        ' Remain in procedure tolerances
        If (newSetpoint >= SetpointMinimum) AndAlso (newSetpoint <= SetpointMaximum) Then
          setpointDesired_ = newSetpoint
          setpointStatus_ = EState.Request
          Return True
        Else
          'Outside of limits (which limit?)
          If newSetpoint < SetpointMinimum Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Below Lower Setpoint Limit: " & (SetpointMinimum / 10).ToString & UnitsSetpoint
          ElseIf newSetpoint > SetpointMaximum Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & UnitsSetpoint
          End If
        End If
      Else
        ' Remain in procedure tolerances
        If (newSetpoint >= SetpointMinimum) AndAlso (newSetpoint <= SetpointMaximum) Then
          setpointDesired_ = newSetpoint
          setpointStatus_ = EState.Request
          Return True
        Else
          'Outside of limits (which limit?)
          If newSetpoint < SetpointMinimum Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & " Below Lower Setpoint Limit: " & (SetpointMinimum / 10).ToString & UnitsSetpoint

          ElseIf newSetpoint < SetpointLimitLower Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & " Below Lower Deviance Limit: " & (SetpointLimitLower / 10).ToString & UnitsSetpoint

          ElseIf newSetpoint > SetpointLimitUpper Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & " Exceeds Upper Deviance Limit: " & (SetpointLimitUpper / 10).ToString & UnitsSetpoint

          ElseIf newSetpoint > SetpointMaximum Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & UnitsSetpoint

          End If
        End If
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  Public Function Increase(increment As Integer, expert As Boolean) As Boolean Implements IController.Increase
    Try
      Dim newSetpoint As Integer = SetpointDesired + increment
      ' Expert must remain within setpoint limits
      If expert Then
        If (newSetpoint < SetpointMaximum) Then
          setpointDesired_ += increment
          setpointStatus_ = EState.Request
          Return True
        Else
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & UnitsSetpoint
        End If
      Else
        'Test to make sure that new setpoint remains within the deviance limits and max/min values
        If newSetpoint < SetpointMaximum Then
          If newSetpoint < SetpointLimitUpper Then
            setpointDesired_ += increment
            setpointStatus_ = EState.Request
            Return True
          Else
            adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & " Exceeds Upper Deviance Limit: " & (SetpointLimitUpper / 10).ToString & UnitsSetpoint
          End If
        Else
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & UnitsSetpoint
        End If
      End If

    Catch ex As Exception
      'log error
    End Try
    Return False
  End Function

  Public Function Decrease(increment As Integer, expert As Boolean) As Boolean Implements IController.Decrease
    Try
      Dim newSetpoint As Integer = SetpointDesired - increment
      ' Expert must remain within setpoint limits
      If expert Then
        If newSetpoint > SetpointMinimum Then
          setpointDesired_ -= increment
          setpointStatus_ = EState.Request
          Return True
        Else
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & " Below Lower Deviance Limit: " & (SetpointLimitLower / 10).ToString & UnitsSetpoint
        End If
      Else
        'Test to make sure that new setpoint remains within the deviance limits and max/min values
        If newSetpoint > SetpointMinimum Then
          If newSetpoint > SetpointLimitLower Then
            setpointDesired_ -= increment
            setpointStatus_ = EState.Request
            Return True
          Else
            adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & " Below Lower Deviance Limit: " & (SetpointLimitLower / 10).ToString & UnitsSetpoint
          End If
        Else
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & UnitsSetpoint
        End If
      End If

    Catch ex As Exception
      'log error
    End Try
    Return False
  End Function

#End Region

End Class
