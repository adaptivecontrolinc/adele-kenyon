' Version 1.1 [2022-05-20] DH
'  Add Expert user function to change setpoints outside the procedure deviation
' Version 1.0.0 [2013-08-28] DH
'  Begin with SpeedController.vb - Version 1.0.5 [2013-07-12] DH
'  Add properties associated with Dancer Trim Control for more historical display

Public Class PadController
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

#Region "PLC RAW Variables - FILLED IN DURING I/O READS"
  'Not using Properties, just declared as public variables

  Public Plc_SpeedActual As Integer               'V
  Public Plc_SpeedDesired As Integer              'V
  Public Plc_SpeedOffset As Integer               'V
  Public Plc_SpeedError As Integer                'V
  Public Plc_OutputGainAdj As Integer             'V
  Public Plc_OutputPercent As Integer             'V

  Public Plc_DncPosPv As Integer                  'V2030 = Scaled Analog Input 0-100%

  Public Prm_AutoEnable As Integer                'V
  Public Prm_SpeedMaxRange As Integer             'V
  Public Prm_SetpointMin As Integer               'V
  Public Prm_SetpointMax As Integer               'V
  Public Prm_GainRamp As Integer                  'V
  Public Prm_GainAtSpd As Integer                 'V
  Public Prm_GainMaxAdj As Integer                'V
  Public Prm_SpdErrorAllow As Integer             'V

  Public Prm_DncEnabled As Integer                'V6530
  Public Prm_DncPosSv As Integer                  'V6531
  Public Prm_DncGainPct As Integer                'V6532
  Public Prm_DncTrimMode As Integer               'V6533
  Public Prm_DncDelayOnSec As Integer             'V6534
  Public Prm_DncDelayAdjSec As Integer            'V6535
  Public Prm_DncSpdMaxAdj As Integer              'V6536

#End Region

#Region "SETPOINT CONTROL"

  Private setpointDesired_ As Integer
  Public ReadOnly Property SetpointDesired() As Integer
    Get
      Return setpointDesired_
    End Get
  End Property

  Private setpointActual_ As Integer
  Public Property SetpointActual As Integer
    Get
      Return setpointActual_
    End Get
    Set(value As Integer)
      setpointActual_ = value
      'Not requesting a new setpoint, update setpoint desired with remote value (may have changed elsewhere)
      If setpointStatus_ <> EUpdateState.Request Then setpointDesired_ = setpointActual_
    End Set
  End Property

  Private setpointStatus_ As EUpdateState
  Public ReadOnly Property SetpointStatus() As EUpdateState
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

  Private ReadOnly Property ISetpointMaximum() As Integer Implements IController.SetpointMaximum
    Get
      Return Prm_SetpointMax
    End Get
  End Property

  Private ReadOnly Property ISetpointMinimum() As Integer Implements IController.SetpointMinimum
    Get
      Return Prm_SetpointMin
    End Get
  End Property

  'PLC Values Decimal 0-999 represent 0-999%.  115 represents 115% or 1.15
  Public Enum EDecimalPosition
    None = 1        '999 = 999
    One = 10        '999 = 99.9
    Two = 100       '999 = 9.99
  End Enum
  Private decimalPosition_ As EDecimalPosition = EDecimalPosition.One
  Private ReadOnly Property ISetpointFactor As Integer Implements IController.SetpointFactor
    Get
      Return decimalPosition_
    End Get
  End Property

  Private adjustResult_ As EControllerAdjustResult
  Public ReadOnly Property IAdjustResult As EControllerAdjustResult Implements IController.AdjustResult
    Get
      Return adjustResult_
    End Get
  End Property

  Private adjustString_ As String
  Public ReadOnly Property IAdjustString() As String Implements IController.AdjustString
    Get
      Return adjustString_
    End Get
  End Property

  Private ReadOnly Property ISetpointCurrent As Integer Implements IController.SetpointCurrent
    Get
      Return SetpointDesired
    End Get
  End Property

  ' TODO
  Public Function Increase(increment As Integer, expert As Boolean) As Boolean Implements IController.Increase
    Try

      Dim newSetpoint As Integer = setpointDesired_ + increment
      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If newSetpoint <= Prm_SetpointMax Then
        setpointDesired_ += increment
        setpointStatus_ = EUpdateState.Request
        Return True
      Else
        adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
        adjustString_ = " New Setpoint " & (newSetpoint / decimalPosition_).ToString & displayUnitsSetpoint_ &
                          " Exceeds Max Setpoint Limit: " & (Prm_SetpointMax / decimalPosition_).ToString & displayUnitsSetpoint_
      End If

    Catch ex As Exception
      'log error
    End Try
    Return False
  End Function

  ' TODO
  Public Function Decrease(increment As Integer, expert As Boolean) As Boolean Implements IController.Decrease
    Try

      Dim newSetpoint As Integer = setpointDesired_ - increment
      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If newSetpoint >= Prm_SetpointMin Then
        setpointDesired_ -= increment
        setpointStatus_ = EUpdateState.Request
        Return True
      Else
        adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
        adjustString_ = " New Setpoint " & (newSetpoint / ISetpointFactor).ToString & " " & IUnits &
                         " Below Min Setpoint Limit: " & (Prm_SetpointMin / ISetpointFactor).ToString & " " & IUnits
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  Private changeSetpointEnable_ As Boolean
  Public ReadOnly Property ChangeSetpointEnabled As Boolean Implements IController.ChangeSetpointEnabled
    Get
      changeSetpointEnable_ = (controlCode.Parameters.MsSetpointAdjustEnable = 1)
      Return changeSetpointEnable_
    End Get
  End Property

  ' TODO
  Friend Function IChangeSetpoint(setpoint As Integer, expert As Boolean) As Boolean Implements IController.IChangeSetpoint
    Try

      If Not controlCode.FirstScanDone Then Exit Function

      Dim newSetpoint As Integer = setpoint
      If newSetpoint = setpointDesired_ Then Exit Function

      'Test to make sure that new setpoint remains within the deviance limits and max/min values
      If (newSetpoint >= ISetpointMinimum) AndAlso (newSetpoint <= ISetpointMaximum) Then
        setpointDesired_ = newSetpoint
        setpointStatus_ = EUpdateState.Request
        adjustString_ = ""

        Return True
      Else
        'Outside of limits (which limit?)
        If newSetpoint < Prm_SetpointMin Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsLowLimit
          adjustString_ = " New Setpoint " & (newSetpoint / ISetpointFactor).ToString & " " & IUnits &
                          " Below Lower Setpoint Limit: " & (Prm_SetpointMin / ISetpointFactor).ToString & " " & IUnits

        ElseIf newSetpoint > Prm_SetpointMax Then
          adjustResult_ = EControllerAdjustResult.SetpointExceedsHighLimit
          adjustString_ = " New Setpoint " & (newSetpoint / ISetpointFactor).ToString & " " & IUnits &
                          " Exceeds Max Setpoint Limit: " & (Prm_SetpointMax / ISetpointFactor).ToString & " " & IUnits

        End If
      End If

    Catch ex As Exception
      'log error
    End Try
  End Function

  Private description_ As String
  Public Property IDescription() As String Implements IController.Description
    Get
      Return description_
    End Get
    Set(ByVal value As String)
      description_ = value
    End Set
  End Property

  Friend ReadOnly Property IUnits As String Implements IController.Units
    Get
      Return displayUnitsSetpoint_
    End Get
  End Property

  Private displayUnitsSetpoint_ As String = ""
  Friend Property IDisplayUnitsSetpoint As String
    Get
      Return displayUnitsSetpoint_
    End Get
    Set(value As String)
      displayUnitsSetpoint_ = value
    End Set
  End Property

  Private displayUnits_ As String = ""
  Friend Property DisplayUnits As String
    Get
      Return displayUnits_
    End Get
    Set(value As String)
      displayUnits_ = value
    End Set
  End Property

  Public ReadOnly Property IDisplayActual As String Implements IController.Actual
    Get
      Return (Plc_SpeedActual / 10).ToString & displayUnits_
    End Get
  End Property

  Public ReadOnly Property IDisplaySetpoint As String Implements IController.Setpoint
    Get
      Return (SetpointDesired / decimalPosition_).ToString & IDisplayUnitsSetpoint
    End Get
  End Property

  Friend ReadOnly Property IDisplaySetpointPercent As String
    Get
      Return "Setpoint: " & (SetpointDesired / decimalPosition_).ToString & IDisplayUnitsSetpoint
    End Get
  End Property

  Friend ReadOnly Property IDisplaySetpointUnits As String
    Get
      Return "Setpoint Speed: " & (Plc_SpeedDesired / 10).ToString & " " & DisplayUnits
    End Get
  End Property

  ' TODO
  Public ReadOnly Property DisplayStatus As String Implements IController.Status
    Get
      Dim response As String = "Idle"
      'Select Case SetpointStatus
      '  Case EUpdateState.Idle, EUpdateState.Sent
      '    response = "Idle" & " Output: " & (IOutputPct / 10).ToString & "%"
      '  Case EUpdateState.Request
      '    response = "Requested"
      '  Case EUpdateState.Offline
      '    response = "Offline"
      'End Select
      response = " Output: " & (IOutputPct / 10).ToString & "%"
      Return response
    End Get
  End Property

  Friend ReadOnly Property IOutputPct As Integer Implements IController.OutputPercent
    Get
      Return Plc_OutputPercent
    End Get
  End Property

  Private zone_ As Integer
  Friend Property Zone As Integer Implements IController.Zone
    Get
      Return zone_
    End Get
    Set(value As Integer)
      zone_ = value
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
      commsTimer_.Seconds = 10
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
    Plc_SpeedActual = 0
    Plc_SpeedDesired = 0
    Plc_OutputPercent = 0
  End Sub

#End Region

#Region "CONTROL VARIABLES"   'Based on active procedure's command parameters 
  '   NOTE 1: not currently stored on the PLC - Transport PLC can override these values, as Microspeeds could as well
  '   NOTE 2: May add to PLC for more setpoint control in future release

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
