' Version 1.0.3 [2014-07-02 PLC V1.29] DH
' Glen Raven - Kenyon Frame Transport Control Application
' Automation Direct DL260 w/ H2-ECOM100 Ethernet module
' Controls Dancer Pressure E/P

Public Class DancerPressController
  Inherits MarshalByRefObject

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

#Region " PLC RAW PARAMETER VALUES - FILLED IN DURING I/O READS"

  Public Prm_AutoEnable As Integer                'V6700
  Public Prm_DncPress6701 As Integer                  'V6701
  Public Prm_InputUnitsMin As Integer             'V6702
  Public Prm_InputUnitsMax As Integer             'V6703
  Public Prm_SetpointAlarm As Integer             'V6704
  Public Prm_SetpointMin As Integer               'V6705
  Public Prm_SetpointMax As Integer               'V6706
  Public Prm_SetpointIdle As Integer              'V6707

  Public Prm_DancerPressRange As Integer          'V6710



#End Region

#Region " PLC RAW VARIABLE VALUES - FILLED IN DURING I/O READS"

  Public Plc_PressActual As Integer

  Private plc_PressDesired_ As Integer
  Public Property Plc_PressDesired As Integer
    Get
      Return plc_PressDesired_
    End Get
    Set(value As Integer)
      plc_PressDesired_ = value
    End Set
  End Property

  Public Plc_PressError As Integer
  Public Plc_OutputPercent As Integer

#End Region

#Region "SETPOINT CONTROL"

  Public SetpointDesired As Integer

  Private setpointActual_ As Integer
  Public Property SetpointActual As Integer
    Get
      Return setpointActual_
    End Get
    Set(value As Integer)
      setpointActual_ = value
      'Not requesting a new setpoint, update setpoint desired with remove value (may have changed elsewhere)
      If setpointStatus_ <> EUpdateState.Request Then SetpointDesired = setpointActual_
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
  Friend ReadOnly Property SetpointStatus() As EUpdateState
    Get
      Return setpointStatus_
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

  Public Sub SetpointStatusClear()
    'Clear the Setpoint Request
    setpointStatus_ = EUpdateState.Idle
  End Sub

#End Region

#Region "INTERFACES"

  'PLC Values Decimal 0-999 represent 0-999%.  115 represents 115% or 1.15
  Public Enum EDecimalPosition
    None = 1        '999 = 999
    One = 10        '999 = 99.9
    Two = 100       '999 = 9.99
  End Enum
  Private decimalPosition_ As EDecimalPosition = EDecimalPosition.One
  Private ReadOnly Property ISetpointFactor As Integer ' Implements IController.SetpointFactor
    Get
      Return decimalPosition_
    End Get
  End Property

  Private displayUnits_ As String = "psi"
  Friend Property DisplayUnits As String
    Get
      Return displayUnits_
    End Get
    Set(value As String)
      displayUnits_ = value
    End Set
  End Property

  Public ReadOnly Property DisplayActual As String
    Get
      Return (Plc_PressActual / 10).ToString & (" ") & DisplayUnits
    End Get
  End Property

  Public ReadOnly Property DisplaySetpoint As String
    Get
      Return (Plc_PressDesired / 10).ToString & (" ") & DisplayUnits
    End Get
  End Property

  Private adjustResult_ As EControllerAdjustResult
  Public ReadOnly Property IAdjustResult As EControllerAdjustResult ' Implements IController.AdjustResult
    Get
      Return adjustResult_
    End Get
  End Property

  Private adjustString_ As String
  Public ReadOnly Property IAdjustString() As String ' Implements IController.AdjustString
    Get
      Return adjustString_
    End Get
  End Property

  Friend Function ChangeSetpoint(setpoint As Integer) As Boolean ' Implements IController.ChangeSetpoint
    Try

      If Not controlCode.FirstScanDone Then Exit Function

      Dim newSetpoint As Integer = setpoint
      If newSetpoint = SetpointDesired Then Exit Function

      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If (newSetpoint >= Prm_SetpointMin) AndAlso (newSetpoint <= Prm_SetpointMax) Then
        SetpointDesired = newSetpoint
        setpointStatus_ = EUpdateState.Request
        adjustString_ = ""

        Return True
      Else
        'Outside of limits (which limit?)
        If newSetpoint < Prm_SetpointMin Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / ISetpointFactor).ToString & " " & DisplayUnits & _
                          " Below Lower Setpoint Limit: " & (Prm_SetpointMin / ISetpointFactor).ToString & " " & DisplayUnits

        ElseIf newSetpoint > Prm_SetpointMin Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / ISetpointFactor).ToString & " " & DisplayUnits & _
                          " Exceeds Max Setpoint Limit: " & (Prm_SetpointMax / ISetpointFactor).ToString & " " & DisplayUnits

        End If
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  Private spt_DevianceHigh_ As Integer
  Public Property Spt_DevianceHigh As Integer
    Get
      Return spt_DevianceHigh_
    End Get
    Set(value As Integer)
      spt_DevianceHigh_ = MinMax(value, Prm_SetpointMin, Prm_SetpointMax)
    End Set
  End Property

  Private spt_DevianceLow_ As Integer
  Public Property Spt_DevianceLow As Integer
    Get
      Return spt_DevianceLow_
    End Get
    Set(value As Integer)
      spt_DevianceLow_ = MinMax(value, Prm_SetpointMin, Prm_SetpointMax)
    End Set
  End Property

  Private spt_LimitUpper_ As Integer
  Public Property Spt_LimitUpper As Integer
    Get
      Return spt_LimitUpper_
    End Get
    Set(value As Integer)
      spt_LimitUpper_ = MinMax(value, Prm_SetpointMin + 100, Prm_SetpointMax)
    End Set
  End Property

  Private spt_LimitLower_ As Integer
  Public Property Spt_LimitLower As Integer
    Get
      Return spt_LimitLower_
    End Get
    Set(value As Integer)
      spt_LimitLower_ = MinMax(value, Prm_SetpointMin, Prm_SetpointMax)
    End Set
  End Property

#End Region

End Class
