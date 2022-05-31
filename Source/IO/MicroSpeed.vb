' Version 1.1.8 [2022-04-19] DH
'  Add Expert user function to change setpoints outside the procedure deviation
' Version 1.1.7 [2016.07.06] - DH

#Region " Microspeed Communication Notes "
' Copied from Version file because it seemed more appropriate here...

' Run MicrospeedStateMachine:
'   loop through each microspeed controller and determine if a new setpoint is being requested 
'       (either by SetpointStatus = Requested or Sent)
'   if new setpoint is requested, step to "WaitForDisabled" state, to delay until last requested "read" 
'       transaction is complete.  Once all pending reads have completed, step to "Write" state
'   Write all requested communications to microspeeds.  Once all writes have completed, step to "read" state
'   This Read/Delay/Write routine was required due to the communications delay for the 20+ devices
'       (+4seconds reading, +-6.5seconds writing) and the comms getting backed up with previous transit
'       data on a later/current transit request

' To Read from Microspeeds:
'   Must be enabled by ComsMicroSpeed being initialized (com port defined in xml and configured properly)
'       as well as no active write requests once delay for comms completion has finished
'   Read in sequence of Widths, Transport (Tenter, Overfeed, Padder, Selvage L/R), then Fan Speeds
'   Set all corresponding IO points directly after reading the Microspeed

' Reading Individual Microspeeds:
'   Must be enabled (as defined above)
'   Use configured Node (Parameter for each device defined) - if '0' do not read and set "offline"
'   Read first registry "SerialStatus" 
'       - OperationStatus (ENUM = idle, estop, accel, decel, running)
'       - ModeOperation (remote/manual - switch in panel, remote setpoint changes will not update in manual)
'       - ActiveSetpoint - integer value 1-4 based on microspeed's current active setpoint
'       - Set "ScanLast" timestamp to be used for communications troubleshooting
'   Read second registry "Display"
'       - Set DisplayValue which represents the actual running value (fans/transport) or position (width screws)
'           currently active.
'   read third registry "MasterFollowerMode"
'       - Set modesetpoint (ENUM = master, follower) used when reading the current setpoint value as well as
'           when updating setpoints while running so that correct setpoint (master 1-4 or follower 1-4) is 
'           updated with desired value
'   Read fouth registry based on ModeSetpoint (master/follower) and ActiveSetpoint (1-4) 
'       depending on the specific active setpoint, need to read from one of 8 possible registry addresses
'       when reading the Active Setpoint value, use the read Decimal Location to determine the received value
'       in proper increments based on the decimalLocation value (0-4)

' Writing Individual Microspeeds:
'   Must be enabled (as defined above)
'   Important Note: Cannot Update ActiveSetpoint on Running Microspeed Controller, must use specific master/follower
'     LoadSetpoint commmand to send the value down to a memory location, then use a specific master/follower
'     ExecuteLoadedSetpoint command to copy the desired value over to the active setpoint.  Must know the
'     active setpoint to be written to as well as the master/follower mode in order to properly update the sepoint.
'         If microspeed is not running, can use ChangeSetpoint command to immediatley
'   Width Screws:
'     b/c they are not running, but move to new setpoint and stop, use ChangeSetpoint command with setpoint desired
'     once changesetpoint command completes w/ response, set status to Verified so that write comms are complete
'   Tenter Chain:
'     Setpoint 1 is used for the "Thread" speed, currently always set to 30ypm.
'     Setpoint 2 is used for the "Running" speed, the desired recipe setpoint for fabric speed

#End Region

Public Class MicroSpeed : Inherits MarshalByRefObject : Implements IController

  Public Enum EUpdateState
    Idle
    Request
    Sent            ' Coms ok response from Write method
    Execute         ' To be Executed after sent
    Verified        ' Desired setpoint read from device corresponds to what we wrote
    Offline
  End Enum

  Public Enum EOperationStatus
    EStop
    Deceling
    Stopping
    Acceling
    Running
    ReadyMode
    NoFeedback = 7
    DigitalSpeedPot
  End Enum

#Region " INTERFACES "

  Public Property UnitsDisplay As String

  Public Property UnitsSetpoint As String
  Private ReadOnly Property Units As String Implements IController.Units
    Get
      Return UnitsSetpoint
    End Get
  End Property
  Public ReadOnly Property DisplayActual As String Implements IController.Actual
    Get
      Return (DisplayValue / 10).ToString & UnitsDisplay
    End Get
  End Property
  Public ReadOnly Property DisplaySetpoint As String Implements IController.Setpoint
    Get
      Return (SetpointDesired / 10).ToString & UnitsSetpoint
    End Get
  End Property
  Private ReadOnly Property SetpointCurrent() As Integer Implements IController.SetpointCurrent
    Get
      Return SetpointDesired
    End Get
  End Property
  Private ReadOnly Property OutputPercent As Integer Implements IController.OutputPercent
    Get
      Return 0
    End Get
  End Property

  Private setpointFactor_ As Integer = 10
  Public ReadOnly Property SetpointFactor As Integer Implements IController.SetpointFactor
    Get
      Return setpointFactor_
    End Get
  End Property
  Public Property Description() As String Implements IController.Description

  Private zone_ As Integer
  Public Property Zone() As Integer Implements IController.Zone
    Get
      Return zone_
    End Get
    Set(ByVal value As Integer)
      zone_ = value
    End Set
  End Property

  Public ReadOnly Property AdjustResult() As EControllerAdjustResult Implements IController.AdjustResult
    Get
      Return SetpointAdjustResult
    End Get
  End Property

  Public ReadOnly Property AdjustmentString() As String Implements IController.AdjustString
    Get
      Return SetpointAdjustString
    End Get
  End Property

  Public Function Increase(increment As Integer, expert As Boolean) As Boolean Implements IController.Increase
    Try
      Dim newSetpoint As Integer = SetpointDesired + increment
      ' Setpoint is the same
      If newSetpoint = SetpointDesired Then Exit Function

      ' Expert must remain within setpoint limits
      If expert Then
        ' Check below maximum setpoint
        If newSetpoint <= SetpointMaximum Then
          SetpointDesired += increment
          SetpointStatus = EUpdateState.Request
          SetpointRequestLast = CurrentTime
          Return True
        Else
          SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
          SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & UnitsSetpoint
        End If
      Else
        ' Not Expert - Must remain within tolerances
        If newSetpoint <= SetpointMaximum Then
          If newSetpoint <= LimitUpper Then
            SetpointDesired += increment
            SetpointStatus = EUpdateState.Request
            SetpointRequestLast = CurrentTime
            Return True
          Else
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Exceeds Upper Deviance Limit: " & (LimitUpper / 10).ToString & UnitsSetpoint
          End If
        Else
          SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
          SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & UnitsSetpoint
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
      ' Test to make sure that new setpoint remains within the deviance limits and max/min values
      If newSetpoint = SetpointDesired Then Exit Function

      ' Expert must remain within setpoint limits
      If expert Then
        ' Check above minimum setpoint
        If newSetpoint >= SetpointMinimum Then
          SetpointDesired -= increment
          SetpointStatus = EUpdateState.Request
          SetpointRequestLast = CurrentTime
          Return True
        Else
          SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsLowLimit
          SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & UnitsSetpoint
        End If
      Else
        ' Not Expert - Must remain within tolerances
        If newSetpoint >= SetpointMinimum Then
          If newSetpoint >= LimitLower Then
            SetpointDesired -= increment
            SetpointStatus = EUpdateState.Request
            SetpointRequestLast = CurrentTime
            Return True
          Else
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsLowLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Below Lower Deviance Limit: " & (LimitLower / 10).ToString & UnitsSetpoint
          End If
        Else
          SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsLowLimit
          SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & UnitsSetpoint
        End If
      End If

    Catch ex As Exception
      'log error
    End Try
    Return False
  End Function

  Private changeSetpointEnable_ As Boolean
  Public Property ChangeSetpointEnable() As Boolean
    Get
      Return changeSetpointEnable_
    End Get
    Set(ByVal value As Boolean)
      changeSetpointEnable_ = value
    End Set
  End Property

  Private ReadOnly Property SetpointChangeEnabled() As Boolean Implements IController.ChangeSetpointEnabled
    Get
      Return ChangeSetpointEnable
    End Get
  End Property

  Public Function IChangeSetpoint(setpoint As Integer, expert As Boolean) As Boolean Implements IController.IChangeSetpoint
    Try
      Dim newSetpoint As Integer = setpoint
      ' Expert must remain within setpoint limits
      If expert Then
        If (newSetpoint >= SetpointMinimum) AndAlso (newSetpoint <= SetpointMaximum) Then
          SetpointDesired = newSetpoint
          SetpointStatus = EUpdateState.Request
          SetpointRequestLast = CurrentTime
          SetpointAdjustResult = EControllerAdjustResult.OK
          SetpointAdjustString = " Setpoint Requested " & (newSetpoint / 10).ToString & UnitsSetpoint
          Return True
        Else
          'Outside of Setpoint Min/Max Limits
          If newSetpoint < SetpointMinimum Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsLowLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & UnitsSetpoint
          ElseIf newSetpoint > SetpointMaximum Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & UnitsSetpoint
          End If
        End If
      Else
        'Test to make sure that new setpoint remains within the deviance limits and max/min values
        If (newSetpoint >= MinimumSetpoint) AndAlso (newSetpoint <= MaximumSetpoint) Then
          ' SetpointDesired = newSetpoint

          ' Value assumed to be in tenths, so convert here to appropriate number value based on decimal location
          ' Tenter chain value >> 90 = 9.0ypm
          '       if decimal location = 4, then value to be written down should be 9
          '    SetpointDesired = GetRegisterValue(newSetpoint, DecimalLocation)

          SetpointDesired = newSetpoint
          SetpointStatus = EUpdateState.Request
          SetpointRequestLast = CurrentTime
          Return True
        Else
          'Outside of limits (which limit?)
          If newSetpoint < SetpointMinimum Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsLowLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & UnitsSetpoint

          ElseIf newSetpoint < LimitLower Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsLowLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Below Lower Deviance Limit: " & (LimitLower / 10).ToString & UnitsSetpoint

          ElseIf newSetpoint > LimitUpper Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Exceeds Upper Deviance Limit: " & (LimitUpper / 10).ToString & UnitsSetpoint

          ElseIf newSetpoint > SetpointMaximum Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & UnitsSetpoint

          End If
        End If
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  ' TODO - Not used anymore - Remove?
  Public Function ChangeSetpoint(ByVal setpoint As Integer, expert As Boolean, ByVal allScrews As Boolean) As Boolean
    Try
      ' Function used when changing all setpoints at one time - Currently only Widths setpoints with Configuration Button
      Dim newSetpoint As Integer = setpoint
      ' Expert must remain within setpoint limits
      If expert Then
        'Test to make sure that new setpoint remains within the deviance limits and max/min values
        If (newSetpoint >= SetpointMinimum) AndAlso (newSetpoint <= SetpointMaximum) Then
          SetpointDesired = newSetpoint
          SetpointStatus = EUpdateState.Request
          SetpointRequestLast = CurrentTime
          Return True
        Else
          'Outside of limits, provide explaination
          If newSetpoint < SetpointMinimum Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsLowLimit
            SetpointAdjustString = " All Screws - New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Below Setpoint Minimum Limit: " & (SetpointMinimum / 10).ToString & UnitsSetpoint
          ElseIf newSetpoint > SetpointMaximum Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
            SetpointAdjustString = " All Screws - New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & " Exceeds Setpoint Maximum Limit: " & (SetpointMaximum / 10).ToString & UnitsSetpoint
          End If
        End If
      Else
        'Test to make sure that new setpoint remains within the deviance limits and max/min values
        If (newSetpoint >= MinimumSetpoint) AndAlso (newSetpoint <= MaximumSetpoint) Then
          SetpointDesired = newSetpoint
          setpointStatus_ = EUpdateState.Request
          Return True
        Else
          'Outside of limits (which limit?)
          If newSetpoint < setpointMinimum_ Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsLowLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & " " & UnitsSetpoint & " Below Minimum Setpoint Limit: " & (setpointMinimum_ / 10).ToString & " " & UnitsSetpoint

          ElseIf newSetpoint < limitLower_ Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsLowLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & " " & UnitsSetpoint & " Below Lower Deviance Limit: " & (limitLower_ / 10).ToString & " " & UnitsSetpoint

          ElseIf newSetpoint > limitUpper_ Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & " " & UnitsSetpoint & " Exceeds Upper Deviance Limit: " & (limitUpper_ / 10).ToString & " " & UnitsSetpoint

          ElseIf newSetpoint > setpointMaximum_ Then
            SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
            SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & " " & UnitsSetpoint & " Exceeds Maximum Setpoint Limit: " & (setpointMaximum_ / 10).ToString & " " & UnitsSetpoint

          End If
        End If
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  Public Sub ChangeTargetSetpoint(setpoint As Integer, devianceAllowed As Integer)
    Dim newSetpoint As Integer = setpoint
    ' No change
    If newSetpoint = SetpointDesired Then
      setpointStatus_ = EUpdateState.Idle
      SptDevianceHigh = devianceAllowed
      SptDevianceLow = devianceAllowed
    Else
      ' New Setpoint - Check limits
      If (newSetpoint >= SetpointMinimum) AndAlso (newSetpoint <= SetpointMaximum) Then
        SetpointDesired = newSetpoint
        setpointStatus_ = EUpdateState.Request
        SptDevianceHigh = devianceAllowed
        SptDevianceLow = devianceAllowed
      Else
        'Outside of limits (which limit?)
        If newSetpoint < setpointMinimum_ Then
          SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsLowLimit
          SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & " " & Units
        ElseIf newSetpoint > setpointMaximum_ Then
          SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
          SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & " " & Units
        End If
        SetpointStatus = EUpdateState.Idle
      End If
    End If
  End Sub


  Private ReadOnly Property MinimumSetpoint() As Integer Implements IController.SetpointMinimum
    Get
      If LimitLower < SetpointMinimum Then
        Return SetpointMinimum
      Else : Return LimitLower
      End If
    End Get
  End Property

  Private ReadOnly Property MaximumSetpoint() As Integer Implements IController.SetpointMaximum
    Get
      If LimitUpper > SetpointMaximum Then
        Return SetpointMaximum
      Else : Return LimitUpper
      End If
    End Get
  End Property

  Public ReadOnly Property DisplayStatus() As String Implements IController.Status
    Get
      If setpointStatus_ = EUpdateState.Offline Then
        Return "Offline"
      Else
        Dim modeOperation_ As String
        If ModeOperation = EModeOperation.Manual Then
          modeOperation_ = "Manual: "
        Else : modeOperation_ = ""      'No Tag for Remote (Default Status)
        End If

        Select Case OperationStatus
          Case EOperationStatus.EStop
            Return ModeOperation & "E-Stop"
          Case EOperationStatus.Deceling
            Return modeOperation_ & "Deceling"
          Case EOperationStatus.Stopping
            Return modeOperation_ & "Stopping"
          Case EOperationStatus.Acceling
            Return modeOperation_ & "Acceling"
          Case EOperationStatus.Running
            Return modeOperation_ & "Running"
          Case EOperationStatus.ReadyMode
            Return modeOperation_ & "Ready"
          Case EOperationStatus.NoFeedback
            Return modeOperation_ & "No Feedback"
          Case EOperationStatus.DigitalSpeedPot
            Return modeOperation_ & "Digital Speed Pot"
          Case Else
            Return modeOperation_ & "UnDefined"
        End Select

      End If
    End Get
  End Property

#End Region

#Region " RAW VALUES FROM MICROSPEED CONTROLLERS - FILLED IN BY I/O READS "

  Public Sub UpdateSetpointNewValue(ByVal rawDisplayValue As Integer, ByVal decimalLocation As Integer)
    Try
      Dim value As Integer = rawDisplayValue

      Me.DecimalLocation = decimalLocation
      Me.SetpointValueNew = GetIntegerValue(value, Me.DecimalLocation)

    Catch ex As Exception
      Dim message As String = ex.Message
    End Try
  End Sub

  Private setpointValueNew_ As Integer                'Remote Variable 49
  Public Property SetpointValueNew As Integer
    Get
      Return setpointValueNew_
    End Get
    Set(value As Integer)
      setpointValueNew_ = value
    End Set
  End Property

  Public Property OperationStatus() As Integer        'Remote Variable 50
  Public Enum EModeOperation
    Manual = 1
    Remote = 2
  End Enum
  Public ModeOperation As EModeOperation
  Public Enum EModeSetpoint
    Master = 1
    Follower = 2
  End Enum
  Public ModeSetpoint As EModeSetpoint                'Remote Variable 51

  Public Property ActiveSetpoint() As Integer         'Currently active setpoint used by device (1-4)

  Private activeSetpointValue_ As Integer             'Value of Currently Active Setpoint (Can be Master Setpoint 1-4 or Follower Setpoint 1-4)
  Public Property ActiveSetpointValue() As Integer
    Get
      Return activeSetpointValue_
    End Get
    Set(ByVal value As Integer)
      activeSetpointValue_ = value

#If 0 Then
      'Not verifying active setpoints due to: 
      '     operators manually prevent width screws from adjusting (either by Estopping the widths <override trip> or holding 
      '     manual adjust toggle) to load/"sew on" next job and prevent damaging fabric
      '     
      'Verfiy Setpoint
      If setpointStatus_ = UpdateState.Sent Then
        'Setpoint Change Requested - see if new setpoint has been accepted
        If value < (setpointDesired_ + 20) AndAlso value > (setpointDesired_ - 20) Then
          setpointStatus_ = UpdateState.Verified
        Else
          'Increase VerifyCount to ReSend the new setpoint (delay)
          VerifySetpointCount += 1
        End If
      ElseIf setpointStatus_ = UpdateState.Verified Then
        'No Setpoint Change Requested (Idle), if it's different, it must have been manual changed
        If value <> setpointDesired_ Then
          setpointDesired_ = value
        End If
      ElseIf (setpointStatus_ = UpdateState.Idle) Then
        'No Setpoint Change Requested (Idle), if it's different, it must have been manual changed
        If value <> setpointDesired_ Then
          setpointDesired_ = value
        End If
      ElseIf (setpointStatus_ = UpdateState.Offline) Then
        setpointStatus_ = UpdateState.Idle
      End If
#Else
      'Verify Setpoint - if we successfully read the setpoint then it's the new setpoint
      If setpointStatus_ = EUpdateState.Sent Then

        If ModeSetpoint = EModeSetpoint.Master Then
          SetpointStatus = EUpdateState.Verified
          SetpointDesired = value
        Else
          ' Must wait for Microspeeds in Follower mode to have setpoint copied over to active setpoint in I/O write section 
        End If

      ElseIf setpointStatus_ = EUpdateState.Verified Then
        'No Setpoint Change Requested (Idle), if it's different, it must have been manual changed
        If value <> SetpointDesired Then
          SetpointDesired = value
        End If
      ElseIf (setpointStatus_ = EUpdateState.Idle) Then
        'No Setpoint Change Requested (Idle), if it's different, it must have been manual changed
        If value <> SetpointDesired Then
          SetpointDesired = value
        End If
      ElseIf (setpointStatus_ = EUpdateState.Offline) Then
        setpointStatus_ = EUpdateState.Idle
      End If
#End If
    End Set
  End Property

  Private displayValue_ As Integer                  'Value Currently Displayed on Numeric Display of Microspeed
  Public Property DisplayValue() As Integer
    Get
      Return displayValue_
    End Get
    Private Set(ByVal value As Integer)
      displayValue_ = value
    End Set
  End Property

  Private decimalLocation_ As Integer
  Public Property DecimalLocation() As Integer
    Get
      Return decimalLocation_
    End Get
    Private Set(ByVal value As Integer)
      decimalLocation_ = value
    End Set
  End Property

  Public ReadOnly Property DecimalLocationStr As String
    Get
      Select Case DecimalLocation
        Case 0                        '(0) = X.XXX (1.050)
          Return "X.XXX"
        Case 1                        '(1) = XX.XX (10.50)
          Return "XX.XX"
        Case 2                        '(2) = XXX.X (105.0)
          Return "XXX.X"
        Case 3                        '(3) = XXXX. (1050.)
          Return "XXXX."
        Case Else                     '(4) = XXXX (1050)
          Return "XXXX"
      End Select
    End Get
  End Property

  Public Sub UpdateDisplayValue(ByVal rawDisplayValue As Integer, ByVal decimalLocation As Integer)
    Try
      Dim displayValueInput As Integer = rawDisplayValue

      Me.DecimalLocation = decimalLocation
      Me.DisplayValue = GetIntegerValue(displayValueInput, Me.DecimalLocation)

    Catch ex As Exception
      Dim message As String = ex.Message
    End Try
  End Sub

#If 0 Then

    Public Sub UpdateSetpointValue(ByVal rawSetpointValue As Integer, ByVal decimalLocation As Integer, ByVal setpointNumber As Integer)
    Try
      ' Select Setpoint: 1 - 4
      Select Case setpointNumber
        Case 1
          Me.Setpoint1 = GetIntegerValue(rawSetpointValue, decimalLocation)
          If ActiveSetpoint = 1 Then ActiveSetpointValue = Setpoint1

        Case 2
          Me.Setpoint2 = GetIntegerValue(rawSetpointValue, decimalLocation)
          If ActiveSetpoint = 2 Then ActiveSetpointValue = Setpoint2

        Case 3
          Me.Setpoint3 = GetIntegerValue(rawSetpointValue, decimalLocation)
          If ActiveSetpoint = 3 Then ActiveSetpointValue = Setpoint3

        Case 4
          Me.Setpoint4 = GetIntegerValue(rawSetpointValue, decimalLocation)
          If ActiveSetpoint = 4 Then ActiveSetpointValue = Setpoint4

      End Select

    Catch ex As Exception
    End Try
  End Sub

#End If

  Public Sub UpdateActiveSetpointValue(ByVal setpoint As Integer, ByVal decimalLocation As Integer)
    Try
      Me.ActiveSetpointValue = GetIntegerValue(setpoint, decimalLocation)
    Catch ex As Exception
    End Try
  End Sub

  ''' <summary>Returns a standardized integer value necessary for the control to correspond to the hardware</summary>
  ''' <returns>Standardized integer value in tenths for uniform values throughout the control system.</returns>
  ''' <param name="integerValue">Raw value read from the hardware</param>
  ''' <param name="decimalLocation">Hardware decimal location used to scale the return value.</param>
  ''' 
  Private Function GetIntegerValue(ByVal registerValue As Integer, ByVal decimalLocation As Integer) As Integer
    Dim Value As Integer = 0
    Try
      Select Case decimalLocation
        Case 0                                            'X.XXX (1.050)
          Value = registerValue
          setpointFactor_ = 1

        Case 1                                            'XX.XX (10.50)
          Value = registerValue * 100
          setpointFactor_ = 100

        Case 2                                            'XXX.X (105.0)  width 1 > ss=983, decimallocation=2 <display = 098.3>
          Value = registerValue
          setpointFactor_ = 10

        Case 3                                            'XXXX. (1050.)
          Value = registerValue * 10
          setpointFactor_ = 10

        Case 4                                            'XXXX (1050)
          Value = registerValue * 10
          setpointFactor_ = 10

      End Select
    Catch ex As Exception
    End Try
    Return Value
  End Function

  ''' <summary>Returns a standardized integer value necessary for the hardware to correspond to the desired value</summary>
  ''' <returns>Standardized integer value necessary for the hardware to correspond to the desired value</returns>
  ''' <param name="integerValue">Desired value requested on the control side: 140 = 14.0yards/min.</param>
  ''' <param name="decimalLocation">Hardware decimal location used to scale the return value that will be written down to the hardware device.</param>
  ''' 
  Private Function GetRegisterValue(ByVal integerValue As Integer, ByVal decimalLocation As Integer) As Integer
    Dim Value As Integer = 0
    Try
      Select Case decimalLocation
        Case 0                                            'X.XXX (1.050)
          Value = integerValue

        Case 1                                            'XX.XX (10.50)
          Value = CInt(integerValue / 10)

        Case 2                                            'XXX.X (105.0)  width 1 > ss=983, decimallocation=2 <display = 098.3>
          Value = integerValue

        Case 3                                            'XXXX. (1050.)
          Value = CInt(integerValue / 10)

        Case 4                                            'XXXX (no decimal place)
          ' Value of 14 would represent 14ypm on TenterChain
          Value = CInt(integerValue / 10)

      End Select
    Catch ex As Exception
    End Try
    Return Value
  End Function

#End Region


#Region " COMMUNICATIONS "

  Private node_ As Integer                          'Communications Network Node set by parameter per device 
  Public Property Coms_Node() As Integer
    Get
      Return node_
    End Get
    Set(ByVal value As Integer)
      node_ = value
      If node_ = 0 Then
        If Coms_Node <> 0 Then ClearValues()
        commsTimer_.TimeRemaining = 60
      End If
      If (setpointStatus_ = EUpdateState.Offline) AndAlso (node_ > 0) Then setpointStatus_ = EUpdateState.Idle
    End Set
  End Property

  Private coms_ReadLast_ As Date
  Public Property Coms_ReadLast() As Date
    Get
      Return coms_ReadLast_
    End Get
    Set(ByVal value As Date)
      commsTimer_.TimeRemaining = 60

      scanInterval_ = CInt((value - Coms_ReadLast).TotalMilliseconds)
      coms_ReadLast_ = value
    End Set
  End Property

  Private coms_WriteLast_ As Date
  Public ReadOnly Property Coms_WriteLast As Date
    Get
      Return coms_WriteLast_
    End Get
  End Property

  Private coms_WriteLastValue_ As String
  Public ReadOnly Property Coms_WriteLastValue As String
    Get
      Return coms_WriteLastValue_
    End Get
  End Property

  Private coms_WriteLastCommand_ As String
  Public ReadOnly Property Coms_WriteLastCommand As String
    Get
      Return coms_WriteLastCommand_
    End Get
  End Property

  Private coms_WriteLastResult_ As String
  Public ReadOnly Property Coms_WriteLastResult As String
    Get
      Return coms_WriteLastResult_
    End Get
  End Property

  Public Sub Coms_UpdateLastWrite(ByVal command As String, ByVal value As String, ByVal result As String)
    'Update local variables (for history logging)
    coms_WriteLastCommand_ = command
    coms_WriteLastValue_ = value
    coms_WriteLastResult_ = result

    'Update Last Coms write log
    commsTimer_.TimeRemaining = 60
    coms_WriteInterval_ = CInt((CurrentTime - Coms_WriteLast).Milliseconds)
    coms_WriteLast_ = CurrentTime
  End Sub

  Private scanInterval_ As Integer
  Public ReadOnly Property Coms_ScanInterval() As Integer
    Get
      Return scanInterval_
    End Get
  End Property

  Private coms_WriteInterval_ As Integer
  Public ReadOnly Property Coms_WriteInterval() As Integer
    Get
      Return coms_WriteInterval_
    End Get
  End Property

  Private commsTimer_ As New Timer
  Public ReadOnly Property AlarmsCommunicationsLoss() As Boolean
    Get
      If commsTimer_.Finished Then
        If Coms_Node <> 0 Then ClearValues()
        Return True
      Else
        Return False
      End If
    End Get
  End Property

  Public Sub ClearValues()
    ActiveSetpointValue = 0
    DisplayValue = 0

  End Sub

#End Region

#Region " VARIABLES "

  Private limitLower_ As Integer
  Public Property LimitLower() As Integer
    'fan's will have a lower limit based on deviance
    'width screws will limit based on adjacent width deviation
    'ie - for width 2, limit of 20in between width 1-2 and between 2-3
    Get
      Return limitLower_
    End Get
    Set(ByVal value As Integer)
      If value >= 0 Then
        limitLower_ = value
      Else : limitLower_ = 0
      End If
    End Set
  End Property

  Private limitUpper_ As Integer
  Public Property LimitUpper() As Integer
    Get
      Return limitUpper_
    End Get
    Set(ByVal value As Integer)
      limitUpper_ = value
      If limitUpper_ > setpointMaximum_ Then limitUpper_ = setpointMaximum_
    End Set
  End Property

  Private setpointMinimum_ As Integer
  Public Property SetpointMinimum() As Integer
    Get
      Return setpointMinimum_
    End Get
    Set(ByVal value As Integer)
      setpointMinimum_ = value
    End Set
  End Property

  Private setpointMaximum_ As Integer
  Public Property SetpointMaximum() As Integer
    Get
      Return setpointMaximum_
    End Get
    Set(ByVal value As Integer)
      setpointMaximum_ = value
    End Set
  End Property

  Private sptDevianceHigh_ As Integer
  Public Property SptDevianceHigh() As Integer
    Get
      Return sptDevianceHigh_
    End Get
    Set(ByVal value As Integer)
      sptDevianceHigh_ = MinMax(value, SetpointMinimum, SetpointMaximum)
    End Set
  End Property

  Private sptDevianceLow_ As Integer
  Public Property SptDevianceLow() As Integer
    Get
      Return sptDevianceLow_
    End Get
    Set(ByVal value As Integer)
      sptDevianceLow_ = MinMax(value, SetpointMinimum, SetpointMaximum)
    End Set
  End Property

  Private sptDevianceMaximum_ As Integer
  Public Property SptDevianceMaximum() As Integer
    Get
      Return sptDevianceMaximum_
    End Get
    Set(ByVal value As Integer)
      sptDevianceMaximum_ = value
    End Set
  End Property

#End Region

#Region " SETPOINT CONTROL "

  Public SetpointDesired As Integer

  Public ReadOnly Property SetpointDesiredValue As Integer
    Get
      Return GetRegisterValue(SetpointDesired, DecimalLocation)
    End Get
  End Property

  Private setpointAdjustResult_ As EControllerAdjustResult = EControllerAdjustResult.OK
  Friend Property SetpointAdjustResult As EControllerAdjustResult
    Get
      Return setpointAdjustResult_
    End Get
    Private Set(value As EControllerAdjustResult)
      setpointAdjustResult_ = value
    End Set
  End Property

  Private setpointAdjustString_ As String = ""
  Public Property SetpointAdjustString As String
    Get
      Return setpointAdjustString_
    End Get
    Private Set(value As String)
      setpointAdjustString_ = value
    End Set
  End Property

  Private setpointStatus_ As EUpdateState
  Public Property SetpointStatus() As EUpdateState
    Get
      ' 2022-03-16 - Time out after writing 
      If (setpointStatus_ = EUpdateState.Request) OrElse (SetpointStatus = EUpdateState.Sent) Then
        If CInt((CurrentTime - Coms_WriteLast).Milliseconds) > 5000 Then
          setpointStatus_ = EUpdateState.Idle
        End If
      End If
      Return setpointStatus_
    End Get
    Set(ByVal value As EUpdateState)
      'v1.2.3 [2013-03-01] - delay interval for working w/ tenter chain timout
      If value = EUpdateState.Request Then
        setpointRequestLast_ = CurrentTime
      End If
      If value = EUpdateState.Verified Then
        SetpointAdjustResult = EControllerAdjustResult.OK
        SetpointAdjustString = ""
      End If
      setpointStatus_ = value
    End Set
  End Property

  Public ReadOnly Property SetpointStatusDisplay() As String
    Get
      Dim returnValue As String = "Offline"
      Select Case SetpointStatus
        Case EUpdateState.Idle
          returnValue = "Idle"
        Case EUpdateState.Request
          returnValue = "Requested"
        Case EUpdateState.Sent
          returnValue = "Sent"
        Case EUpdateState.Verified
          returnValue = "Verified"
        Case EUpdateState.Offline
          returnValue = "Offline"
      End Select
      ' Return final value
      Return returnValue
    End Get
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

  Private setpointRequestLast_ As Date
  Public Property SetpointRequestLast As Date
    Get
      Return setpointRequestLast_
    End Get
    Set(value As Date)
      setpointRequestLast_ = value
    End Set
  End Property

  Public Sub UpdateSetpointValue(ByVal setpointNumber As Integer, ByVal setpoint As Integer, ByVal decimalLocation As Integer)
    Try
      Select Case setpointNumber
        Case 1
          Me.Setpoint1 = GetIntegerValue(setpoint, decimalLocation)
          If ActiveSetpoint = 1 Then ActiveSetpointValue = Setpoint1

        Case 2
          Me.Setpoint2 = GetIntegerValue(setpoint, decimalLocation)
          If ActiveSetpoint = 2 Then ActiveSetpointValue = Setpoint2

        Case 3
          Me.Setpoint3 = GetIntegerValue(setpoint, decimalLocation)
          If ActiveSetpoint = 3 Then ActiveSetpointValue = Setpoint3

        Case 4
          Me.Setpoint4 = GetIntegerValue(setpoint, decimalLocation)
          If ActiveSetpoint = 4 Then ActiveSetpointValue = Setpoint4

      End Select

    Catch ex As Exception
    End Try
  End Sub

  Public Property Setpoint1 As Integer
  Public Property Setpoint1Desired As Integer
  Public Property Setpoint1Update() As Boolean
  Public Property Setpoint1Writes As Integer

  Public Property Setpoint2 As Integer
  Public Property Setpoint2Desired As Integer
  Public Property Setpoint2Update() As Boolean
  Public Property Setpoint2Writes As Integer

  Public Property Setpoint3 As Integer
  Public Property Setpoint3Desired As Integer
  Public Property Setpoint3Update() As Boolean
  Public Property Setpoint3Writes As Integer

  Public Property Setpoint4 As Integer
  Public Property Setpoint4Desired As Integer
  Public Property Setpoint4Update() As Boolean
  Public Property Setpoint4Writes As Integer

#End Region

End Class
