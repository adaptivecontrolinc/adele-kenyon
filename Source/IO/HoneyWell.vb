' Version 1.1 [2022-03-18] DH
'  Add Expert user function to change setpoints outside the procedure deviation
Public Class HoneyWell : Inherits MarshalByRefObject : Implements IController

  Public Enum UpdateState
    Idle
    Requested
    Sent            'Coms ok response from Write method
    Verified        'desired setpoint read from device corresponds to what we wrote
    Offline
  End Enum

  Public Enum ModeState
    Manual
    Automatic
  End Enum

  Private setpointDesired_ As Integer
  Public Property SetpointDesired() As Integer
    Get
      Return setpointDesired_
    End Get
    Set(ByVal value As Integer)
      setpointDesired_ = value
    End Set
  End Property

#Region "Interfaces - Pass to Configuration Controls"

  Private units_ As String = "F"
  Public ReadOnly Property Units() As String Implements IController.Units
    Get
      Return units_
    End Get
  End Property

  Public ReadOnly Property DisplayActual() As String Implements IController.Actual
    Get
      Return (PresentValue / 10).ToString & Units
    End Get
  End Property

  Public ReadOnly Property DisplaySetpoint() As String Implements IController.Setpoint
    Get
      Return (SetpointDesired / 10).ToString & Units
    End Get
  End Property

  Private ReadOnly Property OutputPercent As Integer Implements IController.OutputPercent
    Get
      Return WorkingOutput
    End Get
  End Property

  Private ReadOnly Property SetpointFactor As Integer Implements IController.SetpointFactor
    Get
      Return 10
    End Get
  End Property

  Private ReadOnly Property SetpointCurrent() As Integer Implements IController.SetpointCurrent
    Get
      Return SetpointDesired
    End Get
  End Property

  Private description_ As String
  Public Property Description() As String Implements IController.Description
    Get
      Return description_
    End Get
    Set(ByVal value As String)
      description_ = value
    End Set
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

  Private adjustResult_ As EControllerAdjustResult
  Public ReadOnly Property AdjustResult() As EControllerAdjustResult Implements IController.AdjustResult
    Get
      Return adjustResult_
    End Get
  End Property

  Private adjustString_ As String
  Public ReadOnly Property AdjustmentString() As String Implements IController.AdjustString
    Get
      Return adjustString_
    End Get
  End Property

  Public Function Increase(ByVal increment As Integer, expert As Boolean) As Boolean Implements IController.Increase
    Try
      Dim newSetpoint As Integer = SetpointDesired + increment
      ' Setpoint is the same
      If newSetpoint = SetpointDesired Then Exit Function

      ' Expert must remain within setpoint limits
      If expert Then
        If newSetpoint < SetpointMaximum Then
          setpointDesired_ += increment
          setpointStatus_ = UpdateState.Requested
          Return True
        Else
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & " " & Units
        End If
      Else
        ' Not Expert - Must remain within tolerances
        If newSetpoint < SetpointMaximum Then
          If newSetpoint < LimitUpper Then
            setpointDesired_ += increment
            setpointStatus_ = UpdateState.Requested
            Return True
          Else
            adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Exceeds Upper Deviance Limit: " & (LimitUpper / 10).ToString & " " & Units
          End If
        Else
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & " " & Units
        End If
      End If

    Catch ex As Exception
      'log error
    End Try
    Return False
  End Function

  Public Function Decrease(ByVal increment As Integer, expert As Boolean) As Boolean Implements IController.Decrease
    Try
      Dim newSetpoint As Integer = setpointDesired_ - increment
      ' Setpoint is the same
      If newSetpoint = SetpointDesired Then Exit Function

      ' Expert must remain within setpoint limits
      If expert Then
        If newSetpoint > SetpointMinimum Then
          setpointDesired_ -= increment
          setpointStatus_ = UpdateState.Requested
          Return True
        Else
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & " " & Units
        End If
      Else
        ' Not Expert - Must remain within tolerances
        If newSetpoint > SetpointMinimum Then
          If newSetpoint > limitLower_ Then
            setpointDesired_ -= increment
            setpointStatus_ = UpdateState.Requested
            Return True
          Else
            adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Below Lower Deviance Limit: " & (LimitLower / 10).ToString & " " & Units
          End If
        Else
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & " " & Units
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
      Return changeSetpointEnable_
    End Get
  End Property

  Public Sub ChangeSetpoint(setpoint As Integer, devianceAllowed As Integer)
    Dim newSetpoint As Integer = setpoint
    ' No change
    If newSetpoint = SetpointDesired Then
      setpointStatus_ = UpdateState.Idle
      Me.DevianceAllowed = devianceAllowed
    Else
      If (newSetpoint >= SetpointMinimum) AndAlso (newSetpoint <= SetpointMaximum) Then
        SetpointDesired = newSetpoint
        Me.DevianceAllowed = devianceAllowed
        setpointStatus_ = UpdateState.Requested
      Else
        'Outside of limits (which limit?)
        If newSetpoint < setpointMinimum_ Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & " " & Units
        ElseIf newSetpoint > setpointMaximum_ Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & " " & Units
        End If
        setpointStatus_ = UpdateState.Idle
      End If
    End If
  End Sub

  Public Function IChangeSetpoint(setpoint As Integer, expert As Boolean) As Boolean Implements IController.IChangeSetpoint
    Try
      Dim newSetpoint As Integer = setpoint
      ' No change
      If newSetpoint = SetpointDesired Then
        setpointStatus_ = UpdateState.Idle
        Return True
      End If
      ' Expert must remain within setpoint limits
      If expert Then
        If (newSetpoint >= SetpointMinimum) AndAlso (newSetpoint <= SetpointMaximum) Then
          setpointDesired_ = newSetpoint
          setpointStatus_ = UpdateState.Requested
          Return True
        Else
          'Outside of Setpoint Min/Max Limits
          If newSetpoint < setpointMinimum_ Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & " " & Units
          ElseIf newSetpoint > setpointMaximum_ Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & " " & Units
          End If
        End If
      Else
        'Test to make sure that new setpoint remains within the deviance limits and max/min values
        If (newSetpoint >= MinimumSetpoint) AndAlso (newSetpoint <= MaximumSetpoint) Then
          setpointDesired_ = newSetpoint
          setpointStatus_ = UpdateState.Requested
          Return True
        Else
          'Outside of limits (which limit?)
          If newSetpoint < setpointMinimum_ Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Below Min Setpoint Limit: " & (SetpointMinimum / 10).ToString & " " & Units

          ElseIf newSetpoint < limitLower_ Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Below Lower Deviance Limit: " & (LimitLower / 10).ToString & " " & Units

          ElseIf newSetpoint > limitUpper_ Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Exceeds Upper Deviance Limit: " & (LimitUpper / 10).ToString & " " & Units

          ElseIf newSetpoint > setpointMaximum_ Then
            adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
            adjustString_ = " New Setpoint " & (newSetpoint / 10).ToString & " " & Units & " Exceeds Max Setpoint Limit: " & (SetpointMaximum / 10).ToString & " " & Units

          End If
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
      'Mode is not working due to the registry configuration on the older honeywell controllers installed...
      ' Instead - display the WorkingSetpoint for each controller which represents the 0-100% output for the unit
      Return "Output: " & (workingOutput_ / 10).ToString & "%"

    End Get
  End Property

#End Region

#Region "Raw Values - From Hardware / Filled in by I/O Read Section"

  'Table A.3 Loop Value Integer Register Map (Signed 16 bit integer - Short)
  Private presentValue_ As Short                    'Register 40001 - PV (*10)
  Public Property PresentValue() As Short
    Get
      Return presentValue_
    End Get
    Set(ByVal value As Short)
      presentValue_ = value
    End Set
  End Property

  Private remoteValue_ As Short                     'Register 40002 - RV; Remote Set Point; SP2 (*10)
  Public Property RemoteValue() As Short
    Get
      Return remoteValue_
    End Get
    Set(ByVal value As Short)
      remoteValue_ = value

      If setpointStatus_ = UpdateState.Sent Then
        'Setpoint Change Requested - see if new setpoint has been accepted
        If remoteValue_ = setpointDesired_ Then
          setpointStatus_ = UpdateState.Verified
        Else
          'Increase VerifyCount to ReSend the new setpoint (delay)
          VerifySetpointCount += 1
        End If
      ElseIf setpointStatus_ = UpdateState.Verified Then
        'No Setpoint Change Requested (Idle), if it's different, it must have been manually changed
        If remoteValue_ <> setpointDesired_ Then
          setpointDesired_ = remoteValue_
        End If
      ElseIf setpointStatus_ = UpdateState.Idle Then
        'No Setpoint Change Requested (Idle), if it's different, it must have been manually changed
        If remoteValue_ <> setpointDesired_ Then
          setpointDesired_ = remoteValue_
        End If
      End If

    End Set
  End Property

  Private workingOutput_ As Integer               'Register 40003 - Working Set Point (*10)
  Public Property WorkingOutput() As Integer
    Get
      Return workingOutput_
    End Get
    Set(ByVal value As Integer)
      workingOutput_ = value
    End Set
  End Property

#End Region

#Region "Variables"

  Private limitLower_ As Integer
  Public Property LimitLower() As Integer
    Get
      Return limitLower_
    End Get
    Set(ByVal value As Integer)
      limitLower_ = value
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

  Private devianceAllowed_ As Integer
  Public Property DevianceAllowed() As Integer
    Get
      Return devianceAllowed_
    End Get
    Set(ByVal value As Integer)
      devianceAllowed_ = MinMax(value, 0, 300) 'TODO - 30.0F reasonable limit? (probably 10/15F)
    End Set
  End Property

  Private devianceActual_ As Integer
  Public ReadOnly Property DevianceActual() As Integer
    Get
      devianceActual_ = Math.Abs(setpointDesired_ - presentValue_)
      Return devianceActual_
    End Get
  End Property

#End Region

  Private delayTimer_ As New Timer
  Public Property DelayTimer() As Timer
    Get
      Return delayTimer_
    End Get
    Set(ByVal value As Timer)
      delayTimer_ = value
    End Set
  End Property

  Private mode_ As Integer
  Public Property Mode() As Integer
    Get
      Return mode_
    End Get
    Set(ByVal value As Integer)
      mode_ = value
    End Set
  End Property

  Public ReadOnly Property SetpointStatusDisplay() As String
    Get
      If setpointStatus_ = UpdateState.Offline Then
        Return "Offline"
      ElseIf setpointStatus_ = UpdateState.Verified Then
        Return "Verified"
      ElseIf setpointStatus_ = UpdateState.Sent Then
        Return "Sent"
      ElseIf setpointStatus_ = UpdateState.Requested Then
        Return "Requested"
      Else
        Return "Idle"
      End If
    End Get
  End Property

  Private setpointStatus_ As UpdateState
  Public Property SetpointStatus() As UpdateState
    Get
      Return setpointStatus_
    End Get
    Set(ByVal value As UpdateState)
      setpointStatus_ = value
    End Set
  End Property

  Public ReadOnly Property IO_WriteSetpoint() As Boolean
    Get
      Return (setpointStatus_ = UpdateState.Requested)
    End Get
  End Property

  Private verifySetpointCount_ As Integer
  Public Property VerifySetpointCount() As Integer
    Get
      Return verifySetpointCount_
    End Get
    Set(ByVal value As Integer)
      If (setpointStatus_ <> UpdateState.Sent) Then
        verifySetpointCount_ = 0
      Else
        If value >= 10 Then
          'we've sent the update, but it hasn't been verified - resend
          setpointStatus_ = UpdateState.Requested
          verifySetpointCount_ = 0
        Else
          verifySetpointCount_ = value
        End If
      End If
    End Set
  End Property

#Region " Communications "

  Private node_ As Integer                          'Communications Network Node set by parameter per device 
  Public Property Coms_Node() As Integer
    Get
      Return node_
    End Get
    Set(ByVal value As Integer)
      node_ = value
      If node_ = 0 Then
        ClearValues()
        commsTimer_.TimeRemaining = 10
      End If
      If (setpointStatus_ = UpdateState.Offline) AndAlso (node_ > 0) Then setpointStatus_ = UpdateState.Idle
    End Set
  End Property

  Private scanLast_ As Date
  Public Property Coms_ScanLast() As Date
    Get
      Return scanLast_
    End Get
    Set(ByVal value As Date)
      commsTimer_.TimeRemaining = 10
      scanInterval_ = CInt((value - Coms_ScanLast).TotalMilliseconds)
      scanLast_ = value
    End Set
  End Property

  Private scanInterval_ As Integer
  Public ReadOnly Property Coms_ScanInterval() As Integer
    Get
      Return scanInterval_
    End Get
  End Property

  Private commsTimer_ As New Timer
  Public ReadOnly Property AlarmsCommunicationsLoss() As Boolean
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
    presentValue_ = 0
    remoteValue_ = 0
    workingOutput_ = 0
  End Sub

#End Region

End Class
