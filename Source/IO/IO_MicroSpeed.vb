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

Public Class IO_MicroSpeed : Inherits MarshalByRefObject : Implements IController

  Public Enum EUpdateState
    Idle
    Request
    Sent            'Coms ok response from Write method
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

  Private setpointFactor_ As Integer
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

  Public Function Increase(ByVal increment As Integer) As Boolean Implements IController.Increase
    Try

      Dim newSetpoint As Integer = SetpointDesired + increment
      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If newSetpoint = SetpointDesired Then Exit Function

      ' Attempt to set new setpoint
      If newSetpoint <= SetpointMaximum Then
        If newSetpoint <= LimitUpper Then
          SetpointDesired += increment
          SetpointStatus = EUpdateState.Request
          SetpointRequestLast = Timers.LocalDateNow
          Return True
        Else
          SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
          SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & _
                          " Exceeds Upper Deviance Limit: " & (LimitUpper / 10).ToString & UnitsSetpoint
        End If
      Else
        SetpointAdjustResult = EControllerAdjustResult.SetpointExceedsHighLimit
        SetpointAdjustString = " New Setpoint " & (newSetpoint / 10).ToString & UnitsSetpoint & _
                          " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & UnitsSetpoint
      End If

    Catch ex As Exception
      'log error
    End Try
    Return False
  End Function

  Public Function Decrease(ByVal increment As Integer) As Boolean Implements IController.Decrease
    Try

      Dim newSetpoint As Integer = setpointDesired_ - increment
      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If newSetpoint > setpointMinimum_ Then
        If newSetpoint > limitLower_ Then
          setpointDesired_ -= increment
          setpointStatus_ = EUpdateState.Request
          Return True
        Else
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & _
                          " Below Lower Deviance Limit: " & (limitLower_ / 10).ToString & unitsSetpoint_
        End If
      Else
        adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
        adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & _
                          " Below Min Setpoint Limit: " & (setpointMinimum_ / 10).ToString & unitsSetpoint_
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

  Public ReadOnly Property SetpointChangeEnabled() As Boolean Implements IController.ChangeSetpointEnabled
    Get
      Return changeSetpointEnable_
    End Get
  End Property

  Public Function ChangeSetpoint(ByVal setpoint As Integer) As Boolean Implements IController.ChangeSetpoint
    Try

      Dim newSetpoint As Integer = setpoint
      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If (newSetpoint >= MinimumSetpoint) AndAlso (newSetpoint <= MaximumSetpoint) Then
        setpointDesired_ = newSetpoint
        setpointStatus_ = EUpdateState.Request
        Return True
      Else
        'Outside of limits (which limit?)
        If newSetpoint < setpointMinimum_ Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & _
                          " Below Lower Setpoint Limit: " & (setpointMinimum_ / 10).ToString & unitsSetpoint_

        ElseIf newSetpoint < limitLower_ Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & _
                          " Below Lower Deviance Limit: " & (limitLower_ / 10).ToString & unitsSetpoint_

        ElseIf newSetpoint > limitUpper_ Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & _
                          " Exceeds Upper Deviance Limit: " & (limitUpper_ / 10).ToString & unitsSetpoint_

        ElseIf newSetpoint > setpointMaximum_ Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & unitsSetpoint_ & _
                          " Exceeds Max Setpoint Limit: " & (setpointMaximum_ / 10).ToString & unitsSetpoint_

        End If
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  Private ReadOnly Property MinimumSetpoint() As Integer Implements IController.SetpointMinimum
    Get
      If limitLower_ < setpointMinimum_ Then
        Return setpointMinimum_
      Else : Return limitLower_
      End If
    End Get
  End Property

  Private ReadOnly Property MaximumSetpoint() As Integer Implements IController.SetpointMaximum
    Get
      If limitUpper_ > setpointMaximum_ Then
        Return setpointMaximum_
      Else : Return limitUpper_
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

        If operationStatus_ = (EOperationStatus.EStop) Then
          Return modeOperation_ & "E-Stop"
        ElseIf operationStatus_ = (EOperationStatus.Deceling) Then
          Return modeOperation_ & "Deceling"
        ElseIf operationStatus_ = (EOperationStatus.Stopping) Then
          Return modeOperation_ & "Stopping"
        ElseIf operationStatus_ = (EOperationStatus.Acceling) Then
          Return modeOperation_ & "Acceling"
        ElseIf operationStatus_ = (EOperationStatus.Running) Then
          Return modeOperation_ & "Running"
        ElseIf operationStatus_ = (EOperationStatus.ReadyMode) Then
          Return modeOperation_ & "Ready Mode"
        ElseIf operationStatus_ = (EOperationStatus.NoFeedback) Then
          Return modeOperation_ & "No Feedback"
        ElseIf operationStatus_ = (EOperationStatus.DigitalSpeedPot) Then
          Return modeOperation_ & "Digital Speed Pot "
        Else
          Return modeOperation_ & "UnDefined Status "
        End If
      End If

    End Get
  End Property

#End Region

#Region " Raw Values from Microspeed Controllers - Filled in by I/O Reads "

  Private operationStatus_ As Integer
  Public Property OperationStatus() As Integer
    Get
      Return operationStatus_
    End Get
    Set(ByVal value As Integer)
      operationStatus_ = value
    End Set
  End Property

  Public Enum EModeOperation
    Manual = 1
    Remote = 2
  End Enum
  Public ModeOperation As EModeOperation

  Public Enum EModeSetpoint
    Master = 1
    Follower = 2
  End Enum
  Public ModeSetpoint As EModeSetpoint

  Private activeSetpoint_ As Integer                'Currently active setpoint used by device (1-4)
  Public Property ActiveSetpoint() As Integer
    Get
      Return activeSetpoint_
    End Get
    Set(ByVal value As Integer)
      activeSetpoint_ = value
    End Set
  End Property

  Private activeSetpointValue_ As Integer           'Value of Currently Active Setpoint (Can be Master Setpoint 1-4 or Follower Setpoint 1-4)
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
#End If
    End Set
  End Property

  Private displayValue_ As Integer                  'Value Currently Displayed on Numeric Display of Microspeed
  Public Property DisplayValue() As Integer
    Get
      Return displayValue_
    End Get
    Set(ByVal value As Integer)
      displayValue_ = value
    End Set
  End Property

  Private decimalLocation_ As Integer
  Public Property DecimalLocation() As Integer
    Get
      Return decimalLocation_
    End Get
    Set(ByVal value As Integer)
      decimalLocation_ = value
    End Set
  End Property
#End Region

#Region " Communications "

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

  Private scanLast_ As Date
  Public Property Coms_ScanLast() As Date
    Get
      Return scanLast_
    End Get
    Set(ByVal value As Date)
      commsTimer_.TimeRemaining = 60
      scanInterval_ = CInt((value - Coms_ScanLast).TotalMilliseconds)
      scanLast_ = value
    End Set
  End Property

  Private writeLast_ As Date
  Public ReadOnly Property Coms_WriteLast As Date
    Get
      Return writeLast_
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
    writeInterval_ = CInt((Date.Now - Coms_WriteLast).Milliseconds)
    writeLast_ = Date.Now
  End Sub

  Private scanInterval_ As Integer
  Public ReadOnly Property Coms_ScanInterval() As Integer
    Get
      Return scanInterval_
    End Get
  End Property

  Private writeInterval_ As Integer
  Public ReadOnly Property Coms_WriteInterval() As Integer
    Get
      Return writeInterval_
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
    activeSetpointValue_ = 0
    displayValue_ = 0
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
      sptDevianceHigh_ = MinMax(value, setpointMinimum_, setpointMaximum_)
    End Set
  End Property

  Private sptDevianceLow_ As Integer
  Public Property SptDevianceLow() As Integer
    Get
      Return sptDevianceLow_
    End Get
    Set(ByVal value As Integer)
      sptDevianceLow_ = MinMax(value, setpointMinimum_, setpointMaximum_)
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

  Public ReadOnly Property SetpointStatusDisplay() As String
    Get
      If setpointStatus_ = EUpdateState.Offline Then
        Return "Offline"
      ElseIf setpointStatus_ = EUpdateState.Verified Then
        Return "Verified"
      ElseIf setpointStatus_ = EUpdateState.Sent Then
        Return "Sent"
      ElseIf setpointStatus_ = EUpdateState.Request Then
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
    Set(ByVal value As EUpdateState)
      'v1.2.3 [2013-03-01] - delay interval for working w/ tenter chain timout
      If value = EUpdateState.Request Then setpointRequestLast_ = Date.Now
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

  Private setpointRequestLast_ As Date
  Public Property SetpointRequestLast As Date
    Get
      Return setpointRequestLast_
    End Get
    Set(value As Date)
      setpointRequestLast_ = value
    End Set
  End Property

  Private setpoint1_ As Integer
  Public Property Setpoint1 As Integer
    Get
      Return setpoint1_
    End Get
    Set(value As Integer)
      setpoint1_ = value
    End Set
  End Property

  Private setpoint1Desired_ As Integer
  Public Property Setpoint1Desired As Integer
    Get
      Return setpoint1Desired_
    End Get
    Set(value As Integer)
      setpoint1Desired_ = value
    End Set
  End Property

  Private setpoint1Update_ As Boolean
  Public Property Setpoint1Update() As Boolean
    Get
      Return setpoint1Update_
    End Get
    Set(ByVal value As Boolean)
      setpoint1Update_ = value
    End Set
  End Property

  Private setpoint1Writes_ As Integer
  Public Property Setpoint1Writes As Integer
    Get
      Return setpoint1Writes_
    End Get
    Set(value As Integer)
      setpoint1Writes_ = value
    End Set
  End Property

  Private setpoint3_ As Integer
  Public Property Setpoint3 As Integer
    Get
      Return setpoint3_
    End Get
    Set(value As Integer)
      setpoint3_ = value
    End Set
  End Property

  Private setpoint3Desired_ As Integer
  Public Property Setpoint3Desired As Integer
    Get
      Return setpoint3Desired_
    End Get
    Set(value As Integer)
      setpoint3Desired_ = value
    End Set
  End Property

  Private setpoint3Update_ As Boolean
  Public Property Setpoint3Update() As Boolean
    Get
      Return setpoint3Update_
    End Get
    Set(ByVal value As Boolean)
      setpoint3Update_ = value
    End Set
  End Property

  Private setpoint3Writes_ As Integer
  Public Property Setpoint3Writes As Integer
    Get
      Return setpoint3Writes_
    End Get
    Set(value As Integer)
      setpoint3Writes_ = value
    End Set
  End Property

  Private setpoint4_ As Integer
  Public Property Setpoint4 As Integer
    Get
      Return setpoint4_
    End Get
    Set(value As Integer)
      setpoint4_ = value
    End Set
  End Property

  Private setpoint4Desired_ As Integer
  Public Property Setpoint4Desired As Integer
    Get
      Return setpoint4Desired_
    End Get
    Set(value As Integer)
      setpoint4Desired_ = value
    End Set
  End Property

  Private setpoint4Update_ As Boolean
  Public Property Setpoint4Update() As Boolean
    Get
      Return setpoint4Update_
    End Get
    Set(ByVal value As Boolean)
      setpoint4Update_ = value
    End Set
  End Property

  Private setpoint4Writes_ As Integer
  Public Property Setpoint4Writes As Integer
    Get
      Return setpoint4Writes_
    End Get
    Set(value As Integer)
      setpoint4Writes_ = value
    End Set
  End Property

#End Region

End Class
